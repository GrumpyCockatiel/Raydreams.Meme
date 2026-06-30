namespace Raydreams.Drawing;

/// <summary>An HSV Tuple</summary>
public record HSV
{
    /// <summary>Black as HSV</summary>
    public static HSV Black => new HSV(0, 0, 0, 255);

    /// <summary>White as HSV</summary>
    public static HSV White => new HSV(0, 0, 100, 255);

    /// <summary>White as HSV</summary>
    public static HSV Red => new HSV(0, 100, 100, 255);

    /// <summary>Gray as HSV</summary>
    public static HSV Gray => new HSV(0, 0, 50, 255);

    public HSV( float hue, float saturation, float value, byte alpha = 255)
    {
        this.Hue = Math.Clamp( hue, 0, 360.0F );
        this.Saturation = Math.Clamp( saturation, 0, 100.0F );
        this.Value = Math.Clamp( value, 0, 100.0F );
        this.Alpha = Math.Clamp(alpha, (byte)0, (byte)255);
    }

    #region [ Properties ]

    /// <summary>A value from 0 to 360</summary>
    public float Hue { get; }

    /// <summary>A value from 0 to 100</summary>
    public float Saturation { get; }

    /// <summary>A value from 0 to 100</summary>
    public float Value { get; private set; }

    /// <summary>Alpha</summary>
    public byte Alpha { get; } = 255;

    /// <summary></summary>
    public bool IsDarkest => this.Value <= 0;

    /// <summary></summary>
    public bool IsLightest => this.Value >= 100F;

    #endregion [ Properties ]

    /// <summary>Change the luminosity value from -100 to +100</summary>
    public void AdjustValue( float amount ) => this.Value = Math.Clamp( amount + this.Value, 0, 100.0F);

    /// <summary></summary>
    /// <param name="rgba"></param>
    /// <returns></returns>
    public static HSV FromRGBA( RGBA rgba )
    {
        // convert to 0-1
        float red = rgba.Red / 255.0F;
        float green = rgba.Green / 255.0F;
        float blue = rgba.Blue / 255.0F;

        // determine the min and max channel
        float cMax = new float[] { red, green, blue }.Max();
        float cMin = new float[] { red, green, blue }.Min();

        float delta = cMax - cMin;

        // hue defaults to 0 unless delta is not 0
        float hue = 0;

        if ( delta != 0 )
        {
            // max was red
            if ( cMax == red )
                hue = ( ( green - blue ) / delta ) % 6F;
            // max was green
            else if ( cMax == green )
                hue = ( ( blue - red ) / delta ) + 2F;
            // max was blue
            else if ( cMax == blue )
                hue = ( ( red - green ) / delta ) + 4F;
            
            // finally
            hue *= 60.0F;
        }

        // saturation
        float saturation = ( cMax != 0 ) ? delta / cMax : 0;

        return new HSV( hue, saturation * 100F, cMax * 100F, rgba.Alpha );
    }
}
