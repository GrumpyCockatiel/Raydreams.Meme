using System.CommandLine;
using Newtonsoft.Json;
using Raydreams.Drawing;
using Raydreams.Drawing.Extensions;
using Raydreams.SKGraphs.Extensions;
using Raydreams.SKia.Images;
using SkiaSharp;

namespace Raydreams.Meme;

/// <summary></summary>
public class MemeMaker
{
    /// <summary>A default test image path to use</summary>
    public static readonly string DesktopPath = Environment.GetFolderPath( Environment.SpecialFolder.DesktopDirectory );

    const int minSize = 100;
    const int maxSize = 4000;

    static void Main(string[] args)
    {
        MemeMaker app = new();

        // get the project settings file
        var fileOption = new Option<string>("--params", "-p")
        {
            Required = true,
            AllowMultipleArgumentsPerToken = false,
            Description = "File path to the JSON options file.",
            DefaultValueFactory = v => Path.Combine(DesktopPath, "Test Files", "volvox.json")
        };

        // debug mode
        var debugOption = new Option<bool>("--debug", "-d")
        {
            Required = false,
            AllowMultipleArgumentsPerToken = false,
            Description = "To show debug hints.",
            DefaultValueFactory = v => false
        };

        // output filename
        var nameOption = new Option<string>("--output", "-o")
        {
            Required = false,
            AllowMultipleArgumentsPerToken = false,
            Description = "Output file name to use in the same folder as the options.",
            DefaultValueFactory = v => "raymeme"
        };

        // resize to size
        var sizeOption = new Option<int>("--size", "-s")
        {
            Required = false,
            AllowMultipleArgumentsPerToken = false,
            Description = "Output image dimension.",
            DefaultValueFactory = v => -1
        };

        // file format
        var formatOption = new Option<string>("--format", "-f")
        {
            Required = false,
            AllowMultipleArgumentsPerToken = false,
            Description = "The final image file format.",
            DefaultValueFactory = v => "png"
        };

        var rootCmd = new RootCommand("Meme Generator") { fileOption, debugOption, nameOption, sizeOption, formatOption };

        try
        {
            ParseResult parseResult = rootCmd.Parse(args);

            if (parseResult.Errors.Count < 1 && parseResult.GetValue(fileOption) is string optionsPath)
            {
                FileInfo optionsFile = new(optionsPath);

                if (optionsFile == null || !optionsFile.Exists)
                    throw new ArgumentException("An options files is required.");

                // save the options dir path
                string dirPath = optionsFile.DirectoryName ?? DesktopPath;
                string filename = parseResult.GetValue(nameOption) ?? $"{nameOption.GetDefaultValue}";
                ImageFormat ext = parseResult.GetValue(formatOption)?.GetEnumValue<ImageFormat>(true) ?? ImageFormat.PNG;

                // load all settings
                var options = JsonConvert.DeserializeObject<MemeImageOptions>( File.ReadAllText( optionsFile.FullName ) );
                RayImage image = app.Run(options, parseResult.GetValue(sizeOption), parseResult.GetValue(debugOption) );

                // write the image
                var outputPath = Path.Combine(dirPath, $"{filename}.{ext.ToString().ToLowerInvariant()}");
                app.WriteFile(image, outputPath, ext);


                Console.WriteLine($"Memed image {options.Source}");
            }
        }
        catch (System.Exception exp)
        {
            Console.WriteLine(exp.Message);    
        }
    }

    /// <summary>Bootstrap and Run the app</summary>
    public RayImage Run(MemeImageOptions options, int size, bool debug = false)
    {
        SKBitmap bmp;

        size = size > 0 ? Math.Clamp(size, minSize, maxSize) : -1;

        // determine image source
        if (RGBA.TryParse(options.Source, out RGBA srcColor))
        {
            bmp = this.LoadImageFromColor(srcColor, size);
        }
        else if (options.IsUrl)
        {
            bmp = this.LoadImageFromURL(options.Source, size);
        }
        else
        {
            bmp = this.LoadImageFromFile(options.Source, size);
        }

        // apply any crop
        if ( options.Cropping != Drawing.Model.CropType.Original )
        {
            SKRect crop = bmp.CalcCropRect(options.Cropping);
            SKImage cropped = bmp.CropBitmap(crop);
            bmp = SKBitmap.FromImage(cropped);

            if (bmp.GreaterThan(maxSize))
                bmp = bmp.Resize(maxSize);
        }

        var image = new RayImage(bmp)
        {
            BorderSize = options.BorderSize,
            BorderColor = options.BorderColor.ToSK(),
            Title = options.Title.Trim(),
            TitleColor = options.TitleColor.ToSK(),
            TitleFontFamily = options.TitleFontFamily,
            TitleFontPercent = options.TitleFontPercent,
            TitleStrokeColor = options.TitleStrokeColor.ToSK(),
            TitleStrokeWeight = options.TitleStrokeWeight,
            Body = options.Body,
            BodyFontSize = options.BodyFontSize,
            BodyTextColor = options.BodyTextColor.ToSK(),
            BodyFontFamily = options.BodyFontFamily,
            BodyStrokeColor = options.BodyStrokeColor.ToSK(),
            BodyStrokeWeight = options.BodyStrokeWeight,
            VerticalAlign = options.VerticalAlign,
            Filter = options.Filter,
            Padding = options.Padding,
            Debug = debug
        };

        image.Draw();
        return image;
    }

    /// <summary>Start with a solid color</summary>
    /// <returns></returns>
    public SKBitmap LoadImageFromColor(RGBA color, int size)
    {
        var bkg = new NullImage(size < minSize ? 1000 : size, color.ToSK());
        bkg.Draw();
        return SKBitmap.FromImage(bkg.GetImage());
    }

    /// <summary>Load an image from the Web</summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public SKBitmap LoadImageFromURL(string url, int size)
    {
        byte[] imageBytes = HttpFetch.FetchImageFromURL(url, 59999999).Result;

        if (imageBytes.Length < 1)
            return LoadImageFromColor(WebColor.Red, size);
        
        SKBitmap bmp = SKBitmap.Decode(imageBytes);
        return size < minSize ? bmp : bmp.Resize(size);
    }

    /// <summary>Load the image from a local file</summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public SKBitmap LoadImageFromFile(string path, int size)
    {
        // get a handle to the file
        FileInfo fi = new( path );

        if ( !fi.Exists )
            return LoadImageFromColor(WebColor.Red, size);

        // write to file - never overwrite
        using FileStream fs = new( path, FileMode.Open, FileAccess.Read );

        SKBitmap bmp = SKBitmap.Decode(fs);
        return size < minSize ? bmp : bmp.Resize(size);
    }

    /// <summary>Write the image</summary>
    public void WriteFile(RayImage image, string outputPath, ImageFormat ext)
    {
        using FileStream stream = File.Open(outputPath, FileMode.Create );
        SKData bytes = ext == ImageFormat.PNG ? image.GetPNG() : image.GetJPEG();
        bytes.SaveTo( stream );
        stream.Flush();
    }

}
