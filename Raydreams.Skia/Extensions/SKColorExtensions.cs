using SkiaSharp;

namespace Raydreams.SKGraphs.Extensions;

/// <summary></summary>
public static class SKColorExtensions
{
    /// <summary>Convert Color to SKColor</summary>
    public static SKColor ToSK( this System.Drawing.Color c ) => new SKColor( c.R, c.G, c.B, c.A );

    /// <summary>Changes just the luminosity of the specified color</summary>
    /// <param name="color">Base color to start with</param>
    /// <param name="delta">Delta change value from [-100,100]</param>
    /// <returns></returns>
    public static SKColor Luminosity( this SKColor color, float delta )
    {
        if ( delta == 0 )
            return color;

        if ( delta <= -100F )
            return SKColors.Black;

        // get HSV values
        float h = 0;
        float s = 0;
        float v = 0;
        color.ToHsv( out h, out s, out v );

        float value = Math.Clamp( v + delta, 0, 100F );

        return SKColor.FromHsv( h, s, value );
    }

}
