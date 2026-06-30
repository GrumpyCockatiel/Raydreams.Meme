using SkiaSharp;

namespace Raydreams.SKia.Images;

/// <summary>Just makes a sold fill image square image</summary>
public class NullImage : SkiaImage
{
    /// <summary></summary>
    public NullImage( int size, SKColor color ) : base( size, size )
    {
        this.BackgroundColor = color;
    }

    /// <summary></summary>
    public override void Draw()
    {
        this.Clear();

        this.CalcFrame();
    }
}
