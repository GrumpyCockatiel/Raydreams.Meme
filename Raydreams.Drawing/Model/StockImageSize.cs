using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Raydreams.Drawing.Model;

/// <summary>Enumerates standard image sizes</summary>
public enum ImageSizeKey : byte
{
	[EnumMember(Value = "t")]
	Thumbnail = 0,
	[EnumMember(Value = "p")]
	XXSmall = 1,
	[EnumMember(Value = "xs")]
	XSmall = 2,
	[EnumMember(Value = "s")]
	Small = 3,
	[EnumMember(Value = "m")]
	Medium = 4,
	[EnumMember(Value = "l")]
	Large = 5,
	[EnumMember(Value = "xl")]
	XLarge = 6
}

/// <summary></summary>
public record StockImageSize
{
	public StockImageSize(ImageSizeKey value)
	{
		this.Value = value;
	}

	[JsonProperty("key", Order = 2)]
    [JsonConverter(typeof(StringEnumConverter))]
    public ImageSizeKey Value {get; init;}

	[JsonProperty("width", Order = 4)]
	public int MaxWidth
	{
		get;
		set => field = Math.Clamp(value, 0, 4000);
	}

	/// <summary>Gets the enum member value</summary>
	[JsonProperty("alias", Order = 5)]
	public string Alias {get; init;} = String.Empty;

	/// <summary>Returns the NATO alphabet list</summary>
	public static Dictionary<ImageSizeKey, StockImageSize> Sizes => new Dictionary<ImageSizeKey, StockImageSize>()
	{
		{ ImageSizeKey.Thumbnail, new StockImageSize(ImageSizeKey.Thumbnail) { MaxWidth = 64, Alias = "t" } },
		{ ImageSizeKey.XXSmall, new StockImageSize(ImageSizeKey.XXSmall) { MaxWidth = 128, Alias = "p"  } },
		{ ImageSizeKey.XSmall, new StockImageSize(ImageSizeKey.XSmall) { MaxWidth = 160, Alias = "xs"  } },
		{ ImageSizeKey.Small, new StockImageSize(ImageSizeKey.Small) { MaxWidth = 256, Alias = "s"  } },
		{ ImageSizeKey.Medium, new StockImageSize(ImageSizeKey.Medium) { MaxWidth = 320, Alias = "m"  } },
		{ ImageSizeKey.Large, new StockImageSize(ImageSizeKey.Large) { MaxWidth = 640, Alias = "l"  } },
		{ ImageSizeKey.XLarge, new StockImageSize(ImageSizeKey.XLarge) { MaxWidth = 1280, Alias = "xl"  } }
	};

	/// <summary>Get a specific item by a string name. Case insensitive and compares all the alt names to the specified input string.</summary>
	public static StockImageSize GetByAlias(string name)
	{
		if (String.IsNullOrEmpty(name))
			return Sizes[ImageSizeKey.Thumbnail];

		name = name.Trim().ToLowerInvariant();

		// try to get by enum value by key first
		if (Enum.TryParse<ImageSizeKey>(name, true, out ImageSizeKey convert))
        	return Sizes[convert];

		// now look in the size attributes
		var size = Sizes.FirstOrDefault(s => s.Value.Alias.Equals(name));
	
		return size.Value ?? Sizes[ImageSizeKey.Thumbnail];
	}

	/// <summary>Gets all the maturity types as a list of type items for lookups</summary>
	//public static List<StockImageSize> Sizes => Enum.GetValues(typeof(ImageSizeKey)).Cast<ImageSizeKey>().Select(i => new StockImageSize(i)).ToList();
}
