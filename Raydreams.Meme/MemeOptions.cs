using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Raydreams.Drawing;
using Raydreams.Drawing.Model;
using Raydreams.Drawing.Serializers;

namespace Raydreams.Meme;

public struct MemeImageOptions
{
    #region [ Fields ]

    #endregion [ Fields ]

    public MemeImageOptions()
    {
    }

    /// <summary>Simple test for a url</summary>
    public bool IsUrl => Uri.IsWellFormedUriString( this.Source, UriKind.Absolute );

    /// <summary>The source of an image can be a blob name, URL, Web color or rgb() type string</summary>
    [JsonProperty( "src" )]
    public string Source
    {
        set;
        get => !String.IsNullOrWhiteSpace( field ) ? field.Trim() : "white";
    }

    /// <summary>Cropping to apply</summary>
    [JsonProperty( "crop" )]
    [JsonConverter( typeof( StringEnumConverter ) )]
    public CropType Cropping { get; set; }

    /// <summary>Filter to apply</summary>
    [JsonProperty( "filter" )]
    [JsonConverter( typeof( StringEnumConverter ) )]
    public FilterType Filter { get; set; }

    /// <summary>Border Color</summary>
    [JsonProperty( "border" )]
    [JsonConverter( typeof( RGBAJsonSerializer ) )]
    public RGBA BorderColor { get; set; } = WebColor.Red;

    /// <summary>The thickness of the border</summary>
    [JsonProperty( "os" )]
    public int BorderSize { get; set; }

    /// <summary>The title text</summary>
    [JsonProperty( "title" )]
    public string Title { get; set; } = String.Empty;

    /// <summary>Title text font family</summary>
    [JsonProperty( "tf" )]
    public string TitleFontFamily { get; set; } = "Arial";

    /// <summary>Title text color</summary>
    [JsonProperty( "tc" )]
    [JsonConverter( typeof( RGBAJsonSerializer ) )]
    public RGBA TitleColor { get; set; } = WebColor.White;

    /// <summary>The title font size is calculated to the max size possible size = 100% default. Value < 100 reduce the size</summary>
    [JsonProperty( "ts" )]
    public int TitleFontPercent { get; set; }

    /// <summary>Title Text Stroke Width is between 0 and 100 for % of font size where 100 = 1% of the font size</summary>
    [JsonProperty( "tsw" )]
    public int TitleStrokeWeight { get; set; }

    /// <summary>The body text stroke color</summary>
    [JsonProperty( "tsc" )]
    [JsonConverter( typeof( RGBAJsonSerializer ) )]
    public RGBA TitleStrokeColor { get; set; } = WebColor.Red;

    /// <summary>The body text</summary>
    [JsonProperty( "body" )]
    public string Body { get; set; } = String.Empty;

    /// <summary>All text font family</summary>
    [JsonProperty( "bf" )]
    public string BodyFontFamily { get; set; } = "Arial";

    /// <summary>The body text color</summary>
    [JsonProperty( "bc" )]
    [JsonConverter( typeof( RGBAJsonSerializer ) )]
    public RGBA BodyTextColor { get; set; } = WebColor.White;

    /// <summary>The body text font size</summary>
    [JsonProperty( "bs" )]
    public float BodyFontSize { get; set; }

    /// <summary>Body Text Stroke Width is between 0 and 100 for % of font size where 100 = 1% of the font size</summary>
    [JsonProperty( "bsw" )]
    public int BodyStrokeWeight { get; set; }

    /// <summary>The body text stroke color</summary>
    [JsonProperty( "bsc" )]
    [JsonConverter( typeof( RGBAJsonSerializer ) )]
    public RGBA BodyStrokeColor { get; set; } = WebColor.White;

    /// <summary>Body vetical alignment of the text</summary>
    [JsonProperty( "valign" )]
    [JsonConverter( typeof( StringEnumConverter ) )]
    public VerticalTextAlign VerticalAlign { get; set; }

    /// <summary>Left, top and right padding for now</summary>
    /// <remarks>Later to break out into 4 sub properties</remarks>
    [JsonProperty( "pad" )]
    public int Padding { get; set; } = 20;

    /// <summary>Bottom padding is broken out</summary>
    public int BottomPadding { get; set; } = 30;

}