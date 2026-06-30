using SkiaSharp;
using Raydreams.Drawing.Model;

namespace Raydreams.SKia.Images;

/// <summary>Abstract base Skia Image class for drawing images using Skia</summary>
public abstract class SkiaImage
{
    #region [ Fields ]

    /// <summary>Default font family to use if none specified</summary>
    protected static string defaultFontFamily = "Arial";

    #endregion [ Fields ]

    #region [ Constructors ]

    /// <summary>Constructor</summary>
    /// <param name="height">Height in pixels of the prime area</param>
    /// <param name="width">Width in pixels of the prime area</param>
    /// <param name="type">The color type which is set to RGBA 8 bit all as the default</param>
    /// <remarks>Dimensions DO NOT include the border size</remarks>
    public SkiaImage( int width = 100, int height = 100, SKColorType type = SKColorType.Rgba8888 )
    {
        width = Math.Clamp( width, 25, 4000 );
        height = Math.Clamp( height, 25, 4000 );

        // define the surface properties
        this.Info = new SKImageInfo( width, height, type, SKAlphaType.Premul );

        // construct a base surface for drawing
        this.Surface = SKSurface.Create( this.Info );

        this.BorderSize = 1;
    }

    #endregion [ Constructors ]

    #region [ Properties ]

    /// <summary>Get the image surface</summary>
    public SKSurface Surface {get; protected set;}

    /// <summary>Get the image info</summary>
    public SKImageInfo Info { get; protected set;}

    /// <summary>Actual pixel thickness of the border</summary>
    public float BorderSize
    {
        get;
        set => field = Math.Clamp( value, 0, 300 );
    }

    /// <summary></summary>
    public SKColor BackgroundColor { get; set; } = SKColors.White;

    /// <summary>The color of the border if any</summary>
    public SKColor BorderColor { get; set; } = SKColors.Black;

    /// <summary>Default Export filename to use</summary>
    public virtual string Filename { get; set; } = "RayImage";

    /// <summary>Draw the layout helper grid</summary>
    public bool Debug { get; set; } = false;

    /// <summary>The color of the border if any</summary>
    public SKColor DebugColor { get; protected set; } = SKColors.Red;

    /// <summary>Any rects to draw debug lines around</summary>
    protected List<(SKRect, SKColor)> DebugRects { get; set; } = [];

    /// <summary>What font family to use for the X Labels</summary>
    public SKTypeface LabelFontFamily = SKTypeface.FromFamilyName( defaultFontFamily );

    #endregion [ Properties ]

    #region [ Calculated Properties ]

    /// <summary>The length of the shorter side</summary>
    public int MinDimension => ( this.Width > this.Height ) ? this.Height : this.Width;

    /// <summary>Determines the orientation of the image</summary>
    /// <returns></returns>
    public ImageOrientation Orientation => this.Width == this.Height ? ImageOrientation.Square : ( this.Width > this.Height ) ? ImageOrientation.Landscape : ImageOrientation.Portrait;

    /// <summary>The origin/center of the graph canvas itself</summary>
    protected SKPoint Origin => new( this.Info.Rect.MidX, this.Info.Rect.MidY );

    /// <summary>Echos the width</summary>
    public int Width => this.Info.Width;

    /// <summary>Echos the height</summary>
    public int Height => this.Info.Height;

    /// <summary>The center x line from top to bottom</summary>
    protected float VerticalCenter => this.Info.Rect.MidX;

    /// <summary>The center horizontal line from left to right</summary>
    protected float HorizontalCenter => this.Info.Rect.MidY;

    #endregion [ Calculated Properties ]

    /// <summary>Gets a raw unencoded SKImage for byte manipulation</summary>
    public SKImage GetImage()
    {
        return this.Surface.Snapshot();
    }

    /// <summary>Gets the drawing as a PNG</summary>
    public SKData GetPNG()
    {
        return this.Surface.Snapshot().Encode( SKEncodedImageFormat.Png, 100 );
    }

    /// <summary>Gets the drawing as a JPEG</summary>
    public SKData GetJPEG( int quality = 75 )
    {
        return this.Surface.Snapshot().Encode( SKEncodedImageFormat.Jpeg, quality );
    }

    /// <summary>Draws everything. Call this explicitly before getting the final image.</summary>
    /// <returns></returns>
    public abstract void Draw();

    /// <summary>Calculate major anchor points</summary>
    /// <param name="info"></param>
    /// <param name="canvas"></param>
    protected virtual void CalcFrame()
    {
    }

    /// <summary>Clears and fills the background</summary>
    protected virtual void Clear() => this.Fill( this.BackgroundColor );

    /// <summary>Fills the entire image with color</summary>
    protected void Fill( SKColor color ) => this.Surface.Canvas.Clear( color );

    /// <summary></summary>
    /// <param name="text"></param>
    // protected void AddText(string text)
    // {
    //     if (String.IsNullOrWhiteSpace(text))
    //         return;

    //     // init the paint from the typeface being used
    //     using SKPaint paint = new SKPaint()
    //     {
    //         IsAntialias = true,
    //         IsStroke = false,
    //         Color = SKColors.Black
    //     };

    //     var font = new SKFont(this.LabelFontFamily);
    //     font.GetFontMetrics
    //     //font.MeasureText(text, )

    //     this.Surface.Canvas.DrawText(text, this.Width / 2f, 64.0f, paint);
    // }

    /// <summary>Draws an inset border</summary>
    /// <returns></returns>
    protected void AddInsetBorder(float thickness, SKColor color)
    {
        thickness = Math.Clamp(thickness, 1, this.Width / 4F);

        using SKPaint paint = new SKPaint()
        {
            Color = color,
            Style = SKPaintStyle.Stroke,
            StrokeCap = SKStrokeCap.Butt,
            IsStroke = true,
            IsAntialias = true,
            StrokeWidth = thickness
        };

        this.Surface.Canvas.DrawRect(
            thickness / 2F,
            thickness / 2F,
            this.Info.Width - thickness,
            this.Info.Height - thickness, paint );
    }

    /// <summary>Enlarges the image with an added border</summary>
    /// <returns></returns>
    /// <remarks>Needs to be modified so only the border is drawn and not a filled image incase of translucent</remarks>
    protected void AddOutsetBorder(float thickness, SKColor color)
    {
        if (thickness < 1) return;

        thickness = Math.Clamp(thickness, 1, this.Width / 4F);

        // create a new surface and increase the size by 2 * border
        SKImageInfo temp = this.Info;
        temp.Height += Convert.ToInt32( Math.Ceiling( thickness * 2.0F ) );
        temp.Width += Convert.ToInt32( Math.Ceiling( thickness * 2.0F ) );

        // create a new surface
        SKSurface surf = SKSurface.Create( temp );

        // BUG - fill with the background color which is not really correct
        surf.Canvas.Clear( color );

        // draw the original surface on top of the temp
        surf.Canvas.DrawSurface( this.Surface, thickness, thickness );

        // replace the primary surface and info
        this.Surface = surf;
        this.Info = temp;
    }

    /// <summary>Draws a simple dot at the specified location</summary>
    /// <param name="size"></param>
    /// <returns></returns>
    protected bool DrawDot( SKPoint loc, float size, SKColor color )
    {
        using SKPaint dot = new SKPaint()
        {
            Color = color,
            Style = SKPaintStyle.Stroke,
            StrokeCap = SKStrokeCap.Round,
            IsAntialias = true,
            StrokeWidth = size
        };

        this.Surface.Canvas.DrawPoint( loc, dot );

        return true;
    }

    /// <summary>Draws all debug info</summary>
    /// <param name="rect"></param>
    /// <remarks>Overriding methods should just add to the Debug Rects and Lines</remarks>
    protected virtual void DrawDebug()
    {
        if ( !this.Debug )
            return;

        using SKPaint paint = new SKPaint
        {
            Color = this.DebugColor,
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 1F
        };

        // draw cross hairs
        this.Surface.Canvas.DrawLine( this.Origin.X, 0, this.Origin.X, this.Info.Height, paint );
        this.Surface.Canvas.DrawLine( 0, this.Origin.Y, this.Info.Width, this.Origin.Y, paint );

        // draw any rects
        foreach ( var rect in this.DebugRects )
        {
            paint.Color = rect.Item2;
            this.Surface.Canvas.DrawRect( rect.Item1, paint );
        }
            
    }
}


/// <summary>Draws text centered in the specified rect</summary>
/// <remarks>This will find the largest font that fits the width</remarks>
// protected void DrawCenteredText(string text, SKRect rect, SKTypeface font, SKColor color)
// {
//     // first find the minimum font size that will fit in the rect by width
//     LabelMetrics met = SkiaMath.CalculateLabelWidth(text, rect.Width, font);

//     using SKPaint textPaint = new SKPaint()
//     {
//         Color = color,
//         TextSize = met.FontSize,
//         TextAlign = SKTextAlign.Center,
//         IsAntialias = true,
//         IsStroke = false,
//         Typeface = font
//     };

//     //float y = rect.Bottom - met.Descent;

//     this.Surface.Canvas.DrawText(text, rect.MidX, rect.Bottom, textPaint);
// }

/// <summary>Uses a different approach to measuring text using TextRendererSk</summary>
/// <param name="text"></param>
/// <param name="flags"></param>
/// <param name="startFontSize"></param>
/// <returns></returns>
//protected float CalculateMaxFontSize(string text, TextFormatFlags flags, SKTypeface ff, float startFontSize = 1000.0F)
//{
//	SKRect draw = new SKRect(0, 0, this.Info.Width - 1, this.Info.Height - 1);

//	float curSize = startFontSize;

//	Font font = new Font(ff, curSize, FontStyle.Bold);
//	SKSize s = new TextRendererSk().MeasureText(text, font, draw.Width, flags);

//	// if height exceeds insetRect height we need to make it smaller
//	if (s.Width > draw.Width)
//		return (draw.Width / s.Width) * startFontSize;
//	else
//		return startFontSize;
//}

/// <summary>Draw a label inside the specified rect</summary>
/// <param name="canvas"></param>
//protected void DrawLabel( string text, SKCanvas canvas, SKRect rect, TextFormatFlags flags )
//{
//    Font font = new Font( SKTypeface.FromFamilyName( this.LabelFontName ), this.LabelFontSize, FontStyle.Regular );

//    _ = TextRendererSk.MeasureText( text, font, rect.Width, flags );

//    TextRendererSk.DrawText( canvas, text, font, rect, this.LabelFontColor, flags );
//}

///// <summary>Draws the X axis labels</summary>
//private void DrawLabel( LabelMetrics label, SKCanvas canvas )
//{
//    // draw prime label
//    if ( !String.IsNullOrWhiteSpace( this.Message ) )
//    {
//        using ( SKPaint textPaint = new SKPaint() { Color = this.TextColor, TextSize = label.FontSize, IsAntialias = true } )
//        {
//            textPaint.TextAlign = SKTextAlign.Center;

//            SKRect rect = new SKRect( 0, this.Origin.Y - ( label.Height / 2.0F ), this.Info.Width, this.Origin.Y + ( label.Height / 2.0F ) );

//            // show the label rect
//            if ( this.LayoutGrid )
//                canvas.DrawRect( rect, new SKPaint() { Color = Debug, StrokeWidth = 1, Style = SKPaintStyle.Stroke } );

//            // draw the text centered on X
//            canvas.DrawText( this.Message, rect.MidX, rect.Bottom - label.Descent, textPaint );
//        }
//    }

//}