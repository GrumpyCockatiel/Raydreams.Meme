namespace Raydreams.Drawing;

/// <summary>An RGBA Tuple where each channel is a value [0,255]</summary>
/// <remarks>Need to add IFormattable</remarks>
public record RGBA
{
    static RGBA()
    {
        Empty = new RGBA(0, 0, 0, 0);
    }

    public RGBA(): this(255,255,255) {}

    public RGBA(byte red, byte green, byte blue, byte alpha = 255)
    {
        this.Red = Math.Clamp(red, (byte)0, (byte)255);
        this.Green = Math.Clamp(green, (byte)0, (byte)255);
        this.Blue = Math.Clamp(blue, (byte)0, (byte)255);
        this.Alpha = Math.Clamp(alpha, (byte)0, (byte)255);
    }

    /// <summary>Red</summary>
    public byte Red { get; } = 255;

    /// <summary>Green</summary>
    public byte Green { get; } = 255;

    /// <summary>Blue</summary>
    public byte Blue { get; } = 255;

    /// <summary>Alpha</summary>
    public byte Alpha { get; } = 255;

    /// <summary>Make a fully opaque version of this color</summary>
    public RGBA Opaque => new RGBA(this.Red, this.Green, this.Blue, 255);

    /// <summary>Is this a fully translucent color where the alpha value equals 0</summary>
    public bool IsTranslucent => this.Alpha == 0x00;

    /// <summary>An Object with all 0s no value</summary>
    public static readonly RGBA Empty;

    /// <summary>Writes out as a hex string</summary>
    /// <param name="withAlpha">include the alpha value</param>
    /// <param name="prefixHash">Prefix with the # symbol</param>
    /// <returns></returns>
    public string ToHex(bool withAlpha, bool prefixHash = false)
    {
        string s = $"{this.Red:X2}{this.Green:X2}{this.Blue:X2}";

        if ( prefixHash ) 
            s = $"#{s}";

        return (withAlpha) ? $"{s}{this.Alpha:X2}" : s;
    }

    /// <summary>Converts to the full JSON rgba() String representation</summary>
    /// <returns></returns>
    public override string ToString()
    {
        double alpha = (this.Alpha / 255.0);
        return $"rgba({this.Red},{this.Green},{this.Blue},{alpha:f2})";
    }

    /// <summary>Gets the RGB part as an SVG string</summary>
    public string SVG => ( this.IsTranslucent ) ? "none" : $"rgb({this.Red},{this.Green},{this.Blue})";

    /// <summary>Gets the Alpha value as a value between 0 and 1.0</summary>
    public float SVGAlpha => ( this.IsTranslucent ) ? 0 : this.Alpha / 255.0F;

    /// <summary>Converts to double array values between 0 and 1.0 in RGBA format</summary>
    /// <returns></returns>
    public double[] ToDouble() => [this.Red / 255.0, this.Green / 255.0, this.Blue / 255.0, this.Alpha / 255.0];

    /// <summary>Converts to byte array values between 0 and 255 in RGBA format</summary>
    /// <returns></returns>
    public byte[] ToByte() => [this.Red, this.Green, this.Blue, this.Alpha];

    /// <summary>Parses from a string in various input formats</summary>
    /// <param name="value">some string to try to parse to a color</param>
    /// <param name="def">Default value if the parse fails</param>
    /// <returns></returns>
    public static RGBA FromString( string value, RGBA? def = null )
    {
        if ( TryParse(value, out RGBA color) )
            return color;

        return def ?? RGBA.Empty;
    }

    /// <summary>Should be able to parse hex strings, rgb(), rgba(), and color names</summary>
    /// <param name="color"></param>
    /// <param name="rgba"></param>
    /// <returns></returns>
    public static bool TryParse( string color, out RGBA rgba )
    {
        // no vlaue
        if ( String.IsNullOrWhiteSpace(color) )
        {
            rgba = Empty;
            return false;
        }
            
        color = color.Trim().ToLower();

        // check for empty color alias which is 0 or empty
        if ( color == "0" || color.Equals("empty", StringComparison.InvariantCultureIgnoreCase) )
        {
            rgba = Empty;
            return true;
        }

        // remove any leading hash
        if (color[0] == '#')
            color = color.Substring(1);

        // defined web colors
        if ( WebColors.Colors.ContainsKey( color.ToLower() ) )
        {
            rgba = WebColors.Colors[color.ToLower()];
            return true;
        }

        // really should do a regex match here
        if ( color.StartsWith("rgba") || color.StartsWith("rgb"))
        {
            byte[] values = ParseColorString(color);
            byte alpha = (values.Length > 3) ? values[3] : (byte)255;
            rgba = new RGBA(values[0], values[1], values[2],  alpha);
            return true;
        }

        // needs to only have hex digits at this point
        if (color.Any(c => !System.Uri.IsHexDigit(c)))
        {
            rgba = Empty;
            return false;
        }

        // so long as there are 3-8 hex chars
        if (color.Length > 2 && color.Length < 9)
        {
            // if length is 3 or 4
            if (color.Length == 3)
                color = $"{color[0]}{color[0]}{color[1]}{color[1]}{color[2]}{color[2]}";
            else if (color.Length == 4)
                color = $"{color[0]}{color[0]}{color[1]}{color[1]}{color[2]}{color[2]}{color[3]}{color[3]}";

            // now pad anything less than 8 with F
            if (color.Length < 8)
                color = color.PadRight(8, 'F');

            // convert only the 8 hex bytes to a color
            byte r = Convert.ToByte(color.Substring(0, 2), 16);
            byte g = Convert.ToByte(color.Substring(2, 2), 16);
            byte b = Convert.ToByte(color.Substring(4, 2), 16);
            byte a = Convert.ToByte(color.Substring(6, 2), 16);

            rgba = new RGBA(r, g, b, a);
            return true;
        }

        // set the results to empty but did not parse
        rgba = Empty;
        return false;
    }

    /// <summary>Parses a JSON Color string value rgba() or rgb() to a byte array</summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <remarks>
    /// The Alpha value is a percent double from 0-1
    /// cmyk() and lab() are not yet supported</remarks>
    public static byte[] ParseColorString( string value )
    {
        if (value.StartsWith("rgba"))
            value = value.Substring(4);
        else if (value.StartsWith("rgb"))
            value = value.Substring(3);
        else
            return new byte[] { 0, 0, 0 };

        string[] parts = value.Split(',', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length < 1)
            return new byte[] { 0, 0, 0 };

        byte[] values = new byte[parts.Length];

        Byte.TryParse(parts[0].Trim(new char[] { '(', ')' }), out values[0]);

        if (parts.Length > 1)
            Byte.TryParse(parts[1].Trim(new char[] { '(', ')' }), out values[1]);

        if (parts.Length > 2)
            Byte.TryParse(parts[2].Trim(new char[] { '(', ')' }), out values[2]);

        if (parts.Length > 3 && Double.TryParse(parts[3].Trim(new char[] { '(', ')' }), out double alpha))
        {
            double a = Math.Clamp(Math.Round(alpha * 255.0), 0, 255);
            values[3] = (byte)a;
        }

        return values;
    }
}


