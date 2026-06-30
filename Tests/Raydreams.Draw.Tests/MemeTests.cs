using Raydreams.Drawing;
using Raydreams.Drawing.Model;
using Raydreams.Meme;

namespace Raydreams.Draw.Tests;

[TestClass]
public sealed class MemeTests
{
    public static readonly string DesktopPath = Environment.GetFolderPath( Environment.SpecialFolder.DesktopDirectory );

    [TestMethod]
    public void MakeMemeTest()
    {
        MemeMaker app = new();

        MemeImageOptions options = new()
        {
            Source = "https://upload.wikimedia.org/wikipedia/commons/1/1c/Mikrofoto.de-volvox-4.jpg",
            Title = "VOLVOX",
            Body = "Is Volvox a plant or animal?\nThis is line 2",
            BodyFontFamily = "Tahoma",
            BodyFontSize = 80,
            BodyTextColor = WebColor.DarkGreen,
            TitleColor = WebColor.White,
            TitleStrokeColor = WebColor.Green,
            TitleFontPercent = 80,
            BorderSize = 20,
            BorderColor = WebColor.DarkGreen,
            VerticalAlign = VerticalTextAlign.Top
        };

        var results = app.Run(options, 1024, true);

        using FileStream stream = File.Open( Path.Combine(DesktopPath, "ray-test.png"), FileMode.Create );
        results.GetPNG().SaveTo( stream );
        stream.Flush();
    }
}
