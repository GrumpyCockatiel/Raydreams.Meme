namespace Raydreams.Drawing.Model;

/// <summary></summary>
/// <remarks>Rename Landscape Image Options</remarks>
public class BackgroundImageOptions : ImageOptions
{
    #region [ Fields ]

    /// <summary></summary>
    private int _offset = 50;

    /// <summary></summary>
    private float _mortar = 1;

    /// <summary></summary>
    private float _rowHeight = 1;

    /// <summary></summary>
    private float _brickWidth = 1;

    #endregion [ Fields ]

    public BackgroundImageOptions( int width, int height ) : base( width, height )
    { }

    /// <summary></summary>
    public RGBA MortarColor { get; set; } = WebColor.Black;

    /// <summary></summary>
    public float RowHeight
    {
        get => this._rowHeight;
        set { this._rowHeight = Math.Clamp( value, 4, this.Height / 2F ); }
    }

    /// <summary></summary>
    public float BrickWidth
    {
        get => this._brickWidth;
        set { this._brickWidth = Math.Clamp( value, 4, this.Width / 2F ); }
    }

    /// <summary></summary>
    public float MortarThickness
    {
        get => this._mortar;
        set { this._mortar = Math.Clamp( value, 1, this.Width / 20F ); }
    }

    /// <summary>a percent value how much each row if offset from the previous with 50% being 1/2 way</summary>
    public int Offset
    {
        get => this._offset;
        set { this._offset = Math.Clamp( value, 0, 100 ); }
    }
}
