using System.Reflection;

namespace Raydreams.Drawing;

public static class WebColors
{
    /// <summary>All the web colors as SKColor</summary>
    /// <remarks>Keys are lower cased so lets make a helper func</remarks>
    public static readonly Dictionary<string, RGBA> Colors = new Dictionary<string, RGBA>();

    /// <summary></summary>
    static WebColors()
    {
        Colors = typeof(WebColor).GetFields(BindingFlags.Static | BindingFlags.Public)
            .ToDictionary(c => c.Name.ToLower(), c => (RGBA)c.GetValue(null));
    }
}

/// <summary></summary>
/// <remarks>Do not add any NON color fields or the Dictionary builder will not work.</remarks>
public struct WebColor
{
    /// <summary>Gets the predefined color of alice blue, or #F0F8FF.</summary>
    public static RGBA AliceBlue = new RGBA(0xF0, 0xF8, 0xFF, 0xFF);

    /// <summary>Gets the predefined color of antique white, or #FAEBD7.</summary>
    public static RGBA AntiqueWhite = new RGBA(0xFA, 0xEB, 0xD7, 0xFF);

    /// <summary>Gets the predefined color of aqua, or #00FFFF.</summary>
    public static RGBA Aqua = new RGBA(0x00, 0xFF, 0xFF, 0xFF);

    /// <summary>Gets the predefined color of aquamarine, or #7FFFD4.</summary>
    public static RGBA Aquamarine = new RGBA(0x7F, 0xFF, 0xD4, 0xFF);

    /// <summary>Gets the predefined color of azure, or #F0FFFF.</summary>
    public static RGBA Azure = new RGBA(0xF0, 0xFF, 0xFF, 0xFF);

    /// <summary>Gets the predefined color of beige, or #F5F5DC.</summary>
    public static RGBA Beige = new RGBA(0xF5, 0xF5, 0xDC, 0xFF);

    /// <summary>Gets the predefined color of bisque, or #FFE4C4.</summary>
    public static RGBA Bisque = new RGBA(0xFF, 0xE4, 0xC4, 0xFF);

    /// <summary>Gets the predefined color of black, or #000000.</summary>
    public static RGBA Black = new RGBA(0x00, 0x00, 0x00, 0xFF);

    /// <summary>Gets the predefined color of blanched almond, or #FFEBCD.</summary>
    public static RGBA BlanchedAlmond = new RGBA(0xFF, 0xEB, 0xCD, 0xFF);

    /// <summary>Gets the predefined color of blue, or #0000FF.</summary>
    public static RGBA Blue = new RGBA(0x00, 0x00, 0xFF, 0xFF);

    /// <summary>Gets the predefined color of blue violet, or #8A2BE2.</summary>
    public static RGBA BlueViolet = new RGBA(0x8A, 0x2B, 0xE2, 0xFF);

    /// <summary>Gets the predefined color of brown, or #A52A2A.</summary>
    public static RGBA Brown = new RGBA(0xA5, 0x2A, 0x2A, 0xFF);

    /// <summary>Gets the predefined color of burly wood, or #DEB887.</summary>
    public static RGBA BurlyWood = new RGBA(0xDE, 0xB8, 0x87, 0xFF);

    /// <summary>Gets the predefined color of cadet blue, or #5F9EA0.</summary>
    public static RGBA CadetBlue = new RGBA(0x5F, 0x9E, 0xA0, 0xFF);

    /// <summary>Gets the predefined color of chartreuse, or #7FFF00.</summary>
    public static RGBA Chartreuse = new RGBA(0x7F, 0xFF, 0x00, 0xFF);

    /// <summary>Gets the predefined color of chocolate, or #D2691E.</summary>
    public static RGBA Chocolate = new RGBA(0xD2, 0x69, 0x1E, 0xFF);

    /// <summary>Gets the predefined color of coral, or #FF7F50.</summary>
    public static RGBA Coral = new RGBA(0xFF, 0x7F, 0x50, 0xFF);

    /// <summary>Gets the predefined color of cornflower blue, or #6495ED.</summary>
    public static RGBA CornflowerBlue = new RGBA(0x64, 0x95, 0xED, 0xFF);

    /// <summary>Gets the predefined color of cornsilk, or #FFF8DC.</summary>
    public static RGBA Cornsilk = new RGBA(0xFF, 0xF8, 0xDC, 0xFF);

    /// <summary>Gets the predefined color of crimson, or #DC143C.</summary>
    public static RGBA Crimson = new RGBA(0xDC, 0x14, 0x3C, 0xFF);

    /// <summary>Gets the predefined color of cyan, or #00FFFF.</summary>
    public static RGBA Cyan = new RGBA(0x00, 0xFF, 0xFF, 0xFF);

    /// <summary>Gets the predefined color of dark blue, or #00008B.</summary>
    public static RGBA DarkBlue = new RGBA(0x00, 0x00, 0x8B, 0xFF);

    /// <summary>Gets the predefined color of dark cyan, or #008B8B.</summary>
    public static RGBA DarkCyan = new RGBA(0x00, 0x8B, 0x8B, 0xFF);

    /// <summary>Gets the predefined color of dark goldenrod, or #B8860B.</summary>
    public static RGBA DarkGoldenrod = new RGBA(0xB8, 0x86, 0x0B, 0xFF);

    /// <summary>Gets the predefined color of dark gray, or #A9A9A9.</summary>
    public static RGBA DarkGray = new RGBA(0xA9, 0xA9, 0xA9, 0xFF);

    /// <summary>Gets the predefined color of dark green, or #006400.</summary>
    public static RGBA DarkGreen = new RGBA(0x00, 0x64, 0x00, 0xFF);

    /// <summary>Gets the predefined color of dark khaki, or #BDB76B.</summary>
    public static RGBA DarkKhaki = new RGBA(0xBD, 0xB7, 0x6B, 0xFF);

    /// <summary>Gets the predefined color of dark magenta, or #8B008B.</summary>
    public static RGBA DarkMagenta = new RGBA(0x8B, 0x00, 0x8B, 0xFF);

    /// <summary>Gets the predefined color of dark olive green, or #556B2F.</summary>
    public static RGBA DarkOliveGreen = new RGBA(0x55, 0x6B, 0x2F, 0xFF);

    /// <summary>Gets the predefined color of dark orange, or #FF8C00.</summary>
    public static RGBA DarkOrange = new RGBA(0xFF, 0x8C, 0x00, 0xFF);

    /// <summary>Gets the predefined color of dark orchid, or #9932CC.</summary>
    public static RGBA DarkOrchid = new RGBA(0x99, 0x32, 0xCC, 0xFF);

    /// <summary>Gets the predefined color of dark red, or #8B0000.</summary>
    public static RGBA DarkRed = new RGBA(0x8B, 0x00, 0x00, 0xFF);

    /// <summary>Gets the predefined color of dark salmon, or #E9967A.</summary>
    public static RGBA DarkSalmon = new RGBA(0xE9, 0x96, 0x7A, 0xFF);

    /// <summary>Gets the predefined color of dark sea green, or #8FBC8B.</summary>
    public static RGBA DarkSeaGreen = new RGBA(0x8F, 0xBC, 0x8B, 0xFF);

    /// <summary>Gets the predefined color of dark slate blue, or #483D8B.</summary>
    public static RGBA DarkSlateBlue = new RGBA(0x48, 0x3D, 0x8B, 0xFF);

    /// <summary>Gets the predefined color of dark slate gray, or #2F4F4F.</summary>
    public static RGBA DarkSlateGray = new RGBA(0x2F, 0x4F, 0x4F, 0xFF);

    /// <summary>Gets the predefined color of dark turquoise, or #00CED1.</summary>
    public static RGBA DarkTurquoise = new RGBA(0x00, 0xCE, 0xD1, 0xFF);

    /// <summary>Gets the predefined color of dark violet, or #9400D3.</summary>
    public static RGBA DarkViolet = new RGBA(0x94, 0x00, 0xD3, 0xFF);

    /// <summary>Gets the predefined color of deep pink, or #FF1493.</summary>
    public static RGBA DeepPink = new RGBA(0xFF, 0x14, 0x93, 0xFF);

    /// <summary>Gets the predefined color of deep sky blue, or #00BFFF.</summary>
    public static RGBA DeepSkyBlue = new RGBA(0x00, 0xBF, 0xFF, 0xFF);

    /// <summary>Gets the predefined color of dim gray, or #696969.</summary>
    public static RGBA DimGray = new RGBA(0x69, 0x69, 0x69, 0xFF);

    /// <summary>Gets the predefined color of dodger blue, or #1E90FF.</summary>
    public static RGBA DodgerBlue = new RGBA(0x1E, 0x90, 0xFF, 0xFF);

    /// <summary>Gets the predefined color of firebrick, or #B22222.</summary>
    public static RGBA Firebrick = new RGBA(0xB2, 0x22, 0x22, 0xFF);

    /// <summary>Gets the predefined color of floral white, or #FFFAF0.</summary>
    public static RGBA FloralWhite = new RGBA(0xFF, 0xFA, 0xF0, 0xFF);

    /// <summary>Gets the predefined color of forest green, or #228B22.</summary>
    public static RGBA ForestGreen = new RGBA(0x22, 0x8B, 0x22, 0xFF);

    /// <summary>Gets the predefined color of fuchsia, or #FF00FF.</summary>
    public static RGBA Fuchsia = new RGBA(0xFF, 0x00, 0xFF, 0xFF);

    /// <summary>Gets the predefined color of gainsboro, or #DCDCDC.</summary>
    public static RGBA Gainsboro = new RGBA(0xDC, 0xDC, 0xDC, 0xFF);

    /// <summary>Gets the predefined color of ghost white, or #F8F8FF.</summary>
    public static RGBA GhostWhite = new RGBA(0xF8, 0xF8, 0xFF, 0xFF);

    /// <summary>Gets the predefined color of gold, or #FFD700.</summary>
    public static RGBA Gold = new RGBA(0xFF, 0xD7, 0x00, 0xFF);

    /// <summary>Gets the predefined color of goldenrod, or #DAA520.</summary>
    public static RGBA Goldenrod = new RGBA(0xDA, 0xA5, 0x20, 0xFF);

    /// <summary>Gets the predefined color of gray, or #808080.</summary>
    public static RGBA Gray = new RGBA(0x80, 0x80, 0x80, 0xFF);

    /// <summary>Gets the predefined color of green, or #008000.</summary>
    public static RGBA Green = new RGBA(0x00, 0x80, 0x00, 0xFF);

    /// <summary>Gets the predefined color of green yellow, or #ADFF2F.</summary>
    public static RGBA GreenYellow = new RGBA(0xAD, 0xFF, 0x2F, 0xFF);

    /// <summary>Gets the predefined color of honeydew, or #F0FFF0.</summary>
    public static RGBA Honeydew = new RGBA(0xF0, 0xFF, 0xF0, 0xFF);

    /// <summary>Gets the predefined color of hot pink, or #FF69B4.</summary>
    public static RGBA HotPink = new RGBA(0xFF, 0x69, 0xB4, 0xFF);

    /// <summary>Gets the predefined color of indian red, or #CD5C5C.</summary>
    public static RGBA IndianRed = new RGBA(0xCD, 0x5C, 0x5C, 0xFF);

    /// <summary>Gets the predefined color of indigo, or #4B0082.</summary>
    public static RGBA Indigo = new RGBA(0x4B, 0x00, 0x82, 0xFF);

    /// <summary>Gets the predefined color of ivory, or #FFFFF0.</summary>
    public static RGBA Ivory = new RGBA(0xFF, 0xFF, 0xF0, 0xFF);

    /// <summary>Gets the predefined color of khaki, or #F0E68C.</summary>
    public static RGBA Khaki = new RGBA(0xF0, 0xE6, 0x8C, 0xFF);

    /// <summary>Gets the predefined color of lavender, or #E6E6FA.</summary>
    public static RGBA Lavender = new RGBA(0xE6, 0xE6, 0xFA, 0xFF);

    /// <summary>Gets the predefined color of lavender blush, or #FFF0F5.</summary>
    public static RGBA LavenderBlush = new RGBA(0xFF, 0xF0, 0xF5, 0xFF);

    /// <summary>Gets the predefined color of lawn green, or #7CFC00.</summary>
    public static RGBA LawnGreen = new RGBA(0x7C, 0xFC, 0x00, 0xFF);

    /// <summary>Gets the predefined color of lemon chiffon, or #FFFACD.</summary>
    public static RGBA LemonChiffon = new RGBA(0xFF, 0xFA, 0xCD, 0xFF);

    /// <summary>Gets the predefined color of light blue, or #ADD8E6.</summary>
    public static RGBA LightBlue = new RGBA(0xAD, 0xD8, 0xE6, 0xFF);

    /// <summary>Gets the predefined color of light coral, or #F08080.</summary>
    public static RGBA LightCoral = new RGBA(0xF0, 0x80, 0x80, 0xFF);

    /// <summary>Gets the predefined color of light cyan, or #E0FFFF.</summary>
    public static RGBA LightCyan = new RGBA(0xE0, 0xFF, 0xFF, 0xFF);

    /// <summary>Gets the predefined color of light goldenrod yellow, or #FAFAD2.</summary>
    public static RGBA LightGoldenrodYellow = new RGBA(0xFA, 0xFA, 0xD2, 0xFF);

    /// <summary>Gets the predefined color of light gray, or #D3D3D3.</summary>
    public static RGBA LightGray = new RGBA(0xD3, 0xD3, 0xD3, 0xFF);

    /// <summary>Gets the predefined color of light green, or #90EE90.</summary>
    public static RGBA LightGreen = new RGBA(0x90, 0xEE, 0x90, 0xFF);

    /// <summary>Gets the predefined color of light pink, or #FFB6C1.</summary>
    public static RGBA LightPink = new RGBA(0xFF, 0xB6, 0xC1, 0xFF);

    /// <summary>Gets the predefined color of light salmon, or #FFA07A.</summary>
    public static RGBA LightSalmon = new RGBA(0xFF, 0xA0, 0x7A, 0xFF);

    /// <summary>Gets the predefined color of light sea green, or #20B2AA.</summary>
    public static RGBA LightSeaGreen = new RGBA(0x20, 0xB2, 0xAA, 0xFF);

    /// <summary>Gets the predefined color of light sky blue, or #87CEFA.</summary>
    public static RGBA LightSkyBlue = new RGBA(0x87, 0xCE, 0xFA, 0xFF);

    /// <summary>Gets the predefined color of light slate gray, or #778899.</summary>
    public static RGBA LightSlateGray = new RGBA(0x77, 0x88, 0x99, 0xFF);

    /// <summary>Gets the predefined color of light steel blue, or #B0C4DE.</summary>
    public static RGBA LightSteelBlue = new RGBA(0xB0, 0xC4, 0xDE, 0xFF);

    /// <summary>Gets the predefined color of light yellow, or #FFFFE0.</summary>
    public static RGBA LightYellow = new RGBA(0xFF, 0xFF, 0xE0, 0xFF);

    /// <summary>Gets the predefined color of lime, or #00FF00.</summary>
    public static RGBA Lime = new RGBA(0x00, 0xFF, 0x00, 0xFF);

    /// <summary>Gets the predefined color of lime green, or #32CD32.</summary>
    public static RGBA LimeGreen = new RGBA(0x32, 0xCD, 0x32, 0xFF);

    /// <summary>Gets the predefined color of linen, or #FAF0E6.</summary>
    public static RGBA Linen = new RGBA(0xFA, 0xF0, 0xE6, 0xFF);

    /// <summary>Gets the predefined color of magenta, or #FF00FF.</summary>
    public static RGBA Magenta = new RGBA(0xFF, 0x00, 0xFF, 0xFF);

    /// <summary>Gets the predefined color of maroon, or #800000.</summary>
    public static RGBA Maroon = new RGBA(0x80, 0x00, 0x00, 0xFF);

    /// <summary>Gets the predefined color of medium aquamarine, or #66CDAA.</summary>
    public static RGBA MediumAquamarine = new RGBA(0x66, 0xCD, 0xAA, 0xFF);

    /// <summary>Gets the predefined color of medium blue, or #0000CD.</summary>
    public static RGBA MediumBlue = new RGBA(0x00, 0x00, 0xCD, 0xFF);

    /// <summary>Gets the predefined color of medium orchid, or #BA55D3.</summary>
    public static RGBA MediumOrchid = new RGBA(0xBA, 0x55, 0xD3, 0xFF);

    /// <summary>Gets the predefined color of medium purple, or #9370DB.</summary>
    public static RGBA MediumPurple = new RGBA(0x93, 0x70, 0xDB, 0xFF);

    /// <summary>Gets the predefined color of medium sea green, or #3CB371.</summary>
    public static RGBA MediumSeaGreen = new RGBA(0x3C, 0xB3, 0x71, 0xFF);

    /// <summary>Gets the predefined color of medium slate blue, or #7B68EE.</summary>
    public static RGBA MediumSlateBlue = new RGBA(0x7B, 0x68, 0xEE, 0xFF);

    /// <summary>Gets the predefined color of medium spring green, or #00FA9A.</summary>
    public static RGBA MediumSpringGreen = new RGBA(0x00, 0xFA, 0x9A, 0xFF);

    /// <summary>Gets the predefined color of medium turquoise, or #48D1CC.</summary>
    public static RGBA MediumTurquoise = new RGBA(0x48, 0xD1, 0xCC, 0xFF);

    /// <summary>Gets the predefined color of medium violet red, or #C71585.</summary>
    public static RGBA MediumVioletRed = new RGBA(0xC7, 0x15, 0x85, 0xFF);

    /// <summary>Gets the predefined color of midnight blue, or #191970.</summary>
    public static RGBA MidnightBlue = new RGBA(0x19, 0x19, 0x70, 0xFF);

    /// <summary>Gets the predefined color of mint cream, or #F5FFFA.</summary>
    public static RGBA MintCream = new RGBA(0xF5, 0xFF, 0xFA, 0xFF);

    /// <summary>Gets the predefined color of misty rose, or #FFE4E1.</summary>
    public static RGBA MistyRose = new RGBA(0xFF, 0xE4, 0xE1, 0xFF);

    /// <summary>Gets the predefined color of moccasin, or #FFE4B5.</summary>
    public static RGBA Moccasin = new RGBA(0xFF, 0xE4, 0xB5, 0xFF);

    /// <summary>Gets the predefined color of navajo white, or #FFDEAD.</summary>
    public static RGBA NavajoWhite = new RGBA(0xFF, 0xDE, 0xAD, 0xFF);

    /// <summary>Gets the predefined color of navy, or #000080.</summary>
    public static RGBA Navy = new RGBA(0x00, 0x00, 0x80, 0xFF);

    /// <summary>Gets the predefined color of old lace, or #FDF5E6.</summary>
    public static RGBA OldLace = new RGBA(0xFD, 0xF5, 0xE6, 0xFF);

    /// <summary>Gets the predefined color of olive, or #808000.</summary>
    public static RGBA Olive = new RGBA(0x80, 0x80, 0x00, 0xFF);

    /// <summary>Gets the predefined color of olive drab, or #6B8E23.</summary>
    public static RGBA OliveDrab = new RGBA(0x6B, 0x8E, 0x23, 0xFF);

    /// <summary>Gets the predefined color of orange, or #FFA500.</summary>
    public static RGBA Orange = new RGBA(0xFF, 0xA5, 0x00, 0xFF);

    /// <summary>Gets the predefined color of orange red, or #FF4500.</summary>
    public static RGBA OrangeRed = new RGBA(0xFF, 0x45, 0x00, 0xFF);

    /// <summary>Gets the predefined color of orchid, or #DA70D6.</summary>
    public static RGBA Orchid = new RGBA(0xDA, 0x70, 0xD6, 0xFF);

    /// <summary>Gets the predefined color of pale goldenrod, or #EEE8AA.</summary>
    public static RGBA PaleGoldenrod = new RGBA(0xEE, 0xE8, 0xAA, 0xFF);

    /// <summary>Gets the predefined color of pale green, or #98FB98.</summary>
    public static RGBA PaleGreen = new RGBA(0x98, 0xFB, 0x98, 0xFF);

    /// <summary>Gets the predefined color of pale turquoise, or #AFEEEE.</summary>
    public static RGBA PaleTurquoise = new RGBA(0xAF, 0xEE, 0xEE, 0xFF);

    /// <summary>Gets the predefined color of pale violet red, or #DB7093.</summary>
    public static RGBA PaleVioletRed = new RGBA(0xDB, 0x70, 0x93, 0xFF);

    /// <summary>Gets the predefined color of papaya whip, or #FFEFD5.</summary>
    public static RGBA PapayaWhip = new RGBA(0xFF, 0xEF, 0xD5, 0xFF);

    /// <summary>Gets the predefined color of peach puff, or #FFDAB9.</summary>
    public static RGBA PeachPuff = new RGBA(0xFF, 0xDA, 0xB9, 0xFF);

    /// <summary>Gets the predefined color of peru, or #CD853F.</summary>
    public static RGBA Peru = new RGBA(0xCD, 0x85, 0x3F, 0xFF);

    /// <summary>Gets the predefined color of pink, or #FFC0CB.</summary>
    public static RGBA Pink = new RGBA(0xFF, 0xC0, 0xCB, 0xFF);

    /// <summary>Gets the predefined color of plum, or #DDA0DD.</summary>
    public static RGBA Plum = new RGBA(0xDD, 0xA0, 0xDD, 0xFF);

    /// <summary>Gets the predefined color of powder blue, or #B0E0E6.</summary>
    public static RGBA PowderBlue = new RGBA(0xB0, 0xE0, 0xE6, 0xFF);

    /// <summary>Gets the predefined color of purple, or #800080.</summary>
    public static RGBA Purple = new RGBA(0x80, 0x00, 0x80, 0xFF);

    /// <summary>Gets the predefined color of red, or #FF0000.</summary>
    public static RGBA Red = new RGBA(0xFF, 0x00, 0x00, 0xFF);

    /// <summary>Gets the predefined color of rosy brown, or #BC8F8F.</summary>
    public static RGBA RosyBrown = new RGBA(0xBC, 0x8F, 0x8F, 0xFF);

    /// <summary>Gets the predefined color of royal blue, or #4169E1.</summary>
    public static RGBA RoyalBlue = new RGBA(0x41, 0x69, 0xE1, 0xFF);

    /// <summary>Gets the predefined color of saddle brown, or #8B4513.</summary>
    public static RGBA SaddleBrown = new RGBA(0x8B, 0x45, 0x13, 0xFF);

    /// <summary>Gets the predefined color of salmon, or #FA8072.</summary>
    public static RGBA Salmon = new RGBA(0xFA, 0x80, 0x72, 0xFF);

    /// <summary>Gets the predefined color of sandy brown, or #F4A460.</summary>
    public static RGBA SandyBrown = new RGBA(0xF4, 0xA4, 0x60, 0xFF);

    /// <summary>Gets the predefined color of sea green, or #2E8B57.</summary>
    public static RGBA SeaGreen = new RGBA(0x2E, 0x8B, 0x57, 0xFF);

    /// <summary>Gets the predefined color of sea shell, or #FFF5EE.</summary>
    public static RGBA SeaShell = new RGBA(0xFF, 0xF5, 0xEE, 0xFF);

    /// <summary>Gets the predefined color of sienna, or #A0522D.</summary>
    public static RGBA Sienna = new RGBA(0xA0, 0x52, 0x2D, 0xFF);

    /// <summary>Gets the predefined color of silver, or #C0C0C0.</summary>
    public static RGBA Silver = new RGBA(0xC0, 0xC0, 0xC0, 0xFF);

    /// <summary>Gets the predefined color of sky blue, or #87CEEB.</summary>
    public static RGBA SkyBlue = new RGBA(0x87, 0xCE, 0xEB, 0xFF);

    /// <summary>Gets the predefined color of slate blue, or #6A5ACD.</summary>
    public static RGBA SlateBlue = new RGBA(0x6A, 0x5A, 0xCD, 0xFF);

    /// <summary>Gets the predefined color of slate gray, or #708090.</summary>
    public static RGBA SlateGray = new RGBA(0x70, 0x80, 0x90, 0xFF);

    /// <summary>Gets the predefined color of snow, or #FFFAFA.</summary>
    public static RGBA Snow = new RGBA(0xFF, 0xFA, 0xFA, 0xFF);

    /// <summary>Gets the predefined color of spring green, or #00FF7F.</summary>
    public static RGBA SpringGreen = new RGBA(0x00, 0xFF, 0x7F, 0xFF);

    /// <summary>Gets the predefined color of steel blue, or #4682B4.</summary>
    public static RGBA SteelBlue = new RGBA(0x46, 0x82, 0xB4, 0xFF);

    /// <summary>Gets the predefined color of tan, or #D2B48C.</summary>
    public static RGBA Tan = new RGBA(0xD2, 0xB4, 0x8C, 0xFF);

    /// <summary>Gets the predefined color of teal, or #008080.</summary>
    public static RGBA Teal = new RGBA(0x00, 0x80, 0x80, 0xFF);

    /// <summary>Gets the predefined color of thistle, or #D8BFD8.</summary>
    public static RGBA Thistle = new RGBA(0xD8, 0xBF, 0xD8, 0xFF);

    /// <summary>Gets the predefined color of tomato, or #FF6347.</summary>
    public static RGBA Tomato = new RGBA(0xFF, 0x63, 0x47, 0xFF);

    /// <summary>Gets the predefined color of turquoise, or #40E0D0.</summary>
    public static RGBA Turquoise = new RGBA(0x40, 0xE0, 0xD0, 0xFF);

    /// <summary>Gets the predefined color of violet, or #EE82EE.</summary>
    public static RGBA Violet = new RGBA(0xEE, 0x82, 0xEE, 0xFF);

    /// <summary>Gets the predefined color of wheat, or #F5DEB3.</summary>
    public static RGBA Wheat = new RGBA(0xF5, 0xDE, 0xB3, 0xFF);

    /// <summary>Gets the predefined color of white, or #FFFFFF.</summary>
    public static RGBA White = new RGBA(0xFF, 0xFF, 0xFF, 0xFF);

    /// <summary>Gets the predefined color of white smoke, or #F5F5F5.</summary>
    public static RGBA WhiteSmoke = new RGBA(0xF5, 0xF5, 0xF5, 0xFF);

    /// <summary>Gets the predefined color of yellow, or #FFFF00.</summary>
    public static RGBA Yellow = new RGBA(0xFF, 0xFF, 0x00, 0xFF);

    /// <summary>Gets the predefined color of yellow green, or #9ACD32.</summary>
    public static RGBA YellowGreen = new RGBA(0x9A, 0xCD, 0x32, 0xFF);
}
