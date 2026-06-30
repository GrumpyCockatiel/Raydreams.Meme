using System.Drawing;

namespace Raydreams.Drawing.Model;

/// <summary>Options to draw an image icon</summary>
/// <remarks>Rename to IconImageOptions</remarks>
public class IconImageOptions : ImageOptions
{
    #region [ Constructors ]

    public IconImageOptions( int size ) : base(size, size)
    { }

    #endregion [ Constructors ]

    /// <summary>The size of the icon sizes - square</summary>
    public int Size
    {
        get => ( this.Width > this.Height ) ? this.Height : this.Width;
        set { base.Width = value; base.Height = value; }
    }

    /// <summary>How much to XY scale the icon by from default 100% which is 1/2 up to double (150%)</summary>
    public int Scale
    {
        get;
        set => field = value < 25 || value > 150 ? 100 : value;
    }

    /// <summary>A value to translate the icon center by</summary>
    public PointF Offset { get; set; }

    /// <summary>Rotation in Degrees</summary>
    public int RotationAngle
    {
        get;
        set => field = Math.Clamp( value, 0, 360 );
    }
}