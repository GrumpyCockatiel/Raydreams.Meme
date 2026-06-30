using System.Text.RegularExpressions;
using Raydreams.Drawing;
using Raydreams.Drawing.Model;
using Raydreams.SKGraphs.Extensions;
using Raydreams.Skia.Logic;
using SkiaSharp;

namespace Raydreams.SKia.Images;

/// <summary></summary>
public class RayImage : SkiaImage
{
    /// <summary>The max allowed length of the body</summary>
    public static readonly int MaxBodyLength = 1024;

    public static readonly string DefaultFooter = "memed by raydreams.com";

    #region [ Constructor ]

    /// <summary>RayImage starts with an image background</summary>
    /// <param name="bmp"></param>
    /// <param name="op"></param>
    /// <remarks>Info from the Bitmap is sent as the image info to the base class</remarks>
    public RayImage( SKBitmap bmp ) : base( bmp.Width, bmp.Height, bmp.ColorType )
    {
        this.Bitmap = bmp;
        this.TitleFontFamily = defaultFontFamily;
        this.TitleFontPercent = 100;
        this.TitleStrokeWeight = 100;
        this.BodyFontFamily = defaultFontFamily;
        this.Body = String.Empty;
        this.Footer = DefaultFooter;
    }

    #endregion [ Constructor ]

    #region [ Properties ]

    /// <summary>The background bitmap</summary>
    protected SKBitmap Bitmap {get; init;}

    /// <summary>The title text</summary>
    public string Title { get; set; } = String.Empty;

    /// <summary>All text font family</summary>
    public string TitleFontFamily
    {
        get => field;
        set => field = !String.IsNullOrWhiteSpace( value ) ? value.Trim() : defaultFontFamily;
    }

    /// <summary>Title text color</summary>
    public SKColor TitleColor { get; set; } = SKColors.White;

    /// <summary>Title Text Stroke Width is between 0 and 100 for % of font size where 100 = 1% of the font size</summary>
    public int TitleStrokeWeight
    {
        get;
        set { field = Math.Clamp( value, 0, 100 ); }
    }

    /// <summary>The body text stroke color</summary>
    public SKColor TitleStrokeColor { get; set; } = SKColors.Red;

    /// <summary>The title font size is calculated to the max size possible size = 100% default. Value < 100 reduce the size</summary>
    public int TitleFontPercent
    {
        get;
        set { field = Math.Clamp( value, 1, 100 ); }
    }

    /// <summary>The body text</summary>
    public string Body
    {
        get;
        set {
            if ( String.IsNullOrWhiteSpace( value ) )
                field = String.Empty;
            else
            {
                value = value.Trim();
                field = value.Length > MaxBodyLength ? value.Substring(0, MaxBodyLength) : value;
            }
        }
    }

    /// <summary>All text font family</summary>
    public string BodyFontFamily
    {
        get;
        set => field = !String.IsNullOrWhiteSpace( value ) ? value.Trim() : defaultFontFamily;
    }

    /// <summary>The body text color</summary>
    public SKColor BodyTextColor { get; set; } = SKColors.White;

    /// <summary>The body text font size</summary>
    public float BodyFontSize { get; set; } = 24.0F;

    /// <summary>Body Text Stroke Width is between 0 and 100 for % of font size where 100 = 1% of the font size</summary>
    public int BodyStrokeWeight
    {
        get;
        set { field = Math.Clamp( value, 0, 100 ); }
    }

    /// <summary>The body text stroke color</summary>
    public SKColor BodyStrokeColor { get; set; } = SKColors.White;

    /// <summary>Body vetical alignment of the text</summary>
    public VerticalTextAlign VerticalAlign { get; set; } = VerticalTextAlign.Bottom;

    /// <summary>The filter to apply when drawn</summary>
    public FilterType Filter {get; set; } = FilterType.Identity;

    /// <summary>Left, top and right padding for now</summary>
    /// <remarks>Later to break out into 4 sub properties</remarks>
    public int Padding { get; set; } = 20;

    /// <summary>Bottom padding is broken out for the footer</summary>
    public int BottomPadding { get; set; } = 0;

    /// <summary>Gets the formatted body text cleansed with new lines</summary>
    public string FormattedBody => new Regex( @"\|+", RegexOptions.None ).Replace( this.Body, Environment.NewLine );

    /// <summary>The footer text</summary>
    public string Footer
    {
        get;
        set => field = value == "|||" ? String.Empty : DefaultFooter;
    }

    /// <summary>The footer text</summary>
    public SKColor FooterTextColor => SKColors.White;// !this.TitleColor.IsTranslucent ? this.TitleColor : this.BodyTextColor;

    #endregion [ Properties ]

    #region [ Calculated Properties ]

    /// <summary>The inset region inside the padded area</summary>
    protected SKRect PaddedRect = new();

    /// <summary></summary>
    protected SKRect HeaderRect = new();

    #endregion [ Calculated Properties ]

    /// <summary></summary>
    public override void Draw()
    {
        // clear the canvas with the background color
        this.Surface.Canvas.Clear( this.BackgroundColor );

        // base calculations
        this.CalcFrame();

        this.DrawBitmap();

        var next = this.DrawTitle();

        this.DrawBody(next);

        this.DrawDebug();

        this.AddOutsetBorder(this.BorderSize, this.BorderColor);
    }

    /// <summary>Calculate the text frame based on the padding and border size</summary>
    protected override void CalcFrame()
    {
        base.CalcFrame();

        // calc the inset rect area
        this.PaddedRect = new SKRect( this.Padding, this.Padding, this.Info.Width - this.Padding, this.Info.Height - this.BottomPadding);

        // calc a max header rect
        this.HeaderRect = new SKRect(this.PaddedRect.Left, this.PaddedRect.Top, this.PaddedRect.Right, 
            this.PaddedRect.Top + this.PaddedRect.Height / 4F );

        //this.DebugRects.Add((this.PaddedRect, SKColors.Green));
        this.DebugRects.Add((this.HeaderRect, SKColors.Black));
    }

    /// <summary>Draws the title</summary>
    /// <returns>Next starting point</returns>
    protected float DrawTitle()
    {
        // no header so start after max
        if ( String.IsNullOrWhiteSpace( this.Title ) )
            return this.HeaderRect.Bottom;

        using SKPaint fillPaint = new()
        {
            Color = this.TitleColor,
            IsAntialias = true,
            Style = SKPaintStyle.Fill,
            StrokeWidth = 0
        };
        
        // initial measure
        SKTypeface style = SKTypeface.FromFamilyName(this.TitleFontFamily, SKFontStyle.Bold );
        SKFont font = new SKFont(style, this.HeaderRect.Height * 1.5F );
        var textWidth = font.MeasureText(this.Title, fillPaint);
        SKFontMetrics metrics;
        _ = font.GetFontMetrics(out metrics);

        float lineHeight = metrics.Descent - metrics.Ascent;

        // scale width first
        if (textWidth > this.HeaderRect.Width)
        {
            font.Size *= this.HeaderRect.Width / textWidth;
            _ = font.GetFontMetrics(out metrics);
            lineHeight = metrics.Descent - metrics.Ascent;
        }
        
        // scale height
        if (lineHeight > this.HeaderRect.Height)
        {
            font.Size *= this.HeaderRect.Height / lineHeight;
            _ = font.GetFontMetrics(out metrics);
            lineHeight = metrics.Descent - metrics.Ascent;
        }

        // now scale by the header size
        font.Size *= this.TitleFontPercent / 100F;
        _ = font.GetFontMetrics(out metrics);
        lineHeight = metrics.Descent - metrics.Ascent;

        // max height all letters
        var y = this.HeaderRect.Top + lineHeight - metrics.Descent;

        // stroke the title first
        if ( this.TitleStrokeWeight > 0 )
        {
            using SKPaint strokePaint = new SKPaint()
            {
                Color = this.TitleStrokeColor,
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = font.Size * ( this.TitleStrokeWeight / 1000F ),
            };

            this.Surface.Canvas.DrawText(this.Title, this.HeaderRect.MidX, y, SKTextAlign.Center, font, strokePaint);
        }

        this.Surface.Canvas.DrawText(this.Title, this.HeaderRect.MidX, y, SKTextAlign.Center, font, fillPaint);

        // return the full line height + padding as the next starting point
        return this.HeaderRect.Top + lineHeight + this.Padding;
    }

    /// <summary>Draw the body text</summary>
    /// <returns></returns>
    protected void DrawBody(float top)
    {
        if ( String.IsNullOrWhiteSpace( this.Body ) )
            return;

        top = Math.Clamp(top, 0, this.Height / 2F);

        // fill paint
        using SKPaint fillPaint = new()
        {
            Color = this.BodyTextColor,
            IsAntialias = true,
            Style = SKPaintStyle.Fill,
            StrokeWidth = 0
        };

        SKTypeface style = SKTypeface.FromFamilyName(this.BodyFontFamily, SKFontStyle.Bold );
        SKFont font = new(style, this.BodyFontSize );
        SKFontMetrics metrics;
        _ = font.GetFontMetrics(out metrics);

        // adjust padded rect to account for descenders
        if ( this.PaddedRect.Bottom > this.Info.Height - metrics.Descent * 2F )
            this.PaddedRect.Bottom = this.Info.Height - metrics.Descent * 2F;

        // body bounds
        var bounds = new SKRect(this.PaddedRect.Left, top, this.PaddedRect.Right, this.PaddedRect.Bottom);

        this.DebugRects.Add((bounds, SKColors.Red));

        SkiaTextProcessor logic = new(this.PaddedRect.Width, this.PaddedRect.Bottom - top, font, fillPaint)
            { VTextAlign = this.VerticalAlign };
        var results = logic.Process(this.Body);

        // translate each word
        foreach (var info in logic.Words)
        {
            // offset each word bounds by the body position OR just draw on a Canvas at this position
            SKRect pos = info.Position.Translate(bounds.Left, bounds.Top);
            this.DebugRects.Add((pos, SKColors.Pink));

            // stroke the text first
            if ( this.BodyStrokeWeight > 0 )
            {
                using var strokePaint = new SKPaint
                {
                    Color = this.BodyStrokeColor,
                    IsAntialias = true,
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = font.Size * ( this.BodyStrokeWeight / 1000F )
                };

                this.Surface.Canvas.DrawText(info.Word, pos.Left, pos.Bottom - metrics.Descent, SKTextAlign.Left, font, strokePaint);
            }

            this.Surface.Canvas.DrawText(info.Word, pos.Left, pos.Bottom - metrics.Descent, SKTextAlign.Left, font, fillPaint);
        }
    }

    /// <summary>Redraw the bitmap with a filter</summary>
    /// <param name="type">Filter type</param>
    /// <remarks>Need to combine crop and filter in one</remarks>
    protected void DrawBitmap()
    {
        if ( this.Bitmap == null || this.Bitmap.IsEmpty || this.Bitmap.IsNull )
            return;

        using SKPaint filter = new SKPaint();
        filter.ColorFilter = SKColorFilter.CreateColorMatrix( ImageFilters.Matrices[this.Filter] );

        this.Surface.Canvas.DrawBitmap( this.Bitmap, 0, 0, filter );
    }

}