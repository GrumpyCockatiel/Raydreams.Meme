namespace Raydreams.Drawing.Model;

/// <summary>Base data object for holding image options as many of them are repetitive</summary>
public class ImageOptions
{
    #region [ Fields ]

    /// <summary></summary>
    private int _width = 640;

    /// <summary></summary>
    private int _height = 480;

    /// <summary></summary>
    private float _bs = 0;

    #endregion [ Fields ]

    #region [ Constructors ]

    public ImageOptions( int width, int height )
    {
        this.Width = width;
        this.Height = height;
    }

    #endregion [ Constructors ]

    #region [ Properties ]

    /// <summary></summary>
    public int Width { get => this._width; set => this._width = Math.Clamp( value, 32, 4000 ); }

    /// <summary></summary>
    public int Height { get => this._height; set => this._height = Math.Clamp( value, 32, 4000 ); }

    /// <summary></summary>
    public bool Watermark { get; set; } = false;

    /// <summary>Draw a dot at the origin</summary>
    public bool Origin { get; set; } = false;

    /// <summary>The image border color</summary>
    public RGBA BorderColor { get; set; } = WebColor.Red;

    /// <summary>The Background Color</summary>
    public RGBA BackgroundColor { get; set; } = WebColor.White;

    /// <summary></summary>
    public DebugOptions? Debug { get; set; } = null;

    /// <summary></summary>
    public GradientOptions? Gradient { get; set; } = null;

    /// <summary>The actual thickness of the border</summary>
    public float BorderSize
    {
        get => this._bs;
        set => this._bs = Math.Clamp( value, 0, new int[] { this.Width, this.Height }.Min() * 0.06F );
    }

    #endregion [ Properties ]
}

/// <summary></summary>
public class GradientOptions
{
    /// <summary></summary>
    private int _angle = 0;

    /// <summary>If the gradient color is anything other than null then a background gradient is created from the background and this color</summary>
    public RGBA Color { get; set; } = RGBA.Empty;

    /// <summary>Angle of the gradient</summary>
    public int Angle
    {
        get => this._angle;
        set => this._angle = Math.Clamp( value, 0, 360 );
    }
}

/// <summary></summary>
public class DebugOptions
{
    /// <summary></summary>
    public RGBA Color { get; set; } = WebColor.Red;
}
