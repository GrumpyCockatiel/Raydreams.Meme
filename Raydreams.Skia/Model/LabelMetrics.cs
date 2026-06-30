namespace Raydreams.Skia.Model;

/// <summary>Captures sizes needed for the X and Y label from pre calculations for later usage.</summary>
public struct LabelMetrics
{
    /// <summary></summary>
    /// <param name="fs"></param>
    /// <param name="h"></param>
    /// <param name="desc"></param>
    /// <param name="capHeight"></param>
    public LabelMetrics( float fs, float h, float desc, float capHeight )
    {
        this.FontSize = fs;
        this.FullHeight = h;
        this.Descent = desc;
        this.CapHeight = capHeight;
    }

    /// <summary>The calculated font size</summary>
    public float FontSize { get; set; }

    /// <summary>Full height minus the descent</summary>
    public float BaseHeight => FullHeight - Descent;

    /// <summary>Height of Capitol letters</summary>
    /// <remarks>Use this as the height knowing ascenders and descenders may overflow</remarks>
    public float CapHeight { get; set; }

    /// <summary>The calculcated FULL height needed from Ascent to Descent to cover the max height of all possible characters with descenders and accents.</summary>
    public float FullHeight { get; set; }

    /// <summary>The calculcated descent portion of the font needed</summary>
    /// <remarks>Usually need to push the text up by this amount to keep it from falling below some line</remarks>
    public float Descent { get; set; }
}
