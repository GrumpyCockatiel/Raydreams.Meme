using Newtonsoft.Json;
using Raydreams.Drawing;

namespace Raydreams.Drawing.Serializers;

/// <summary>(De)Serializers a JSON string back and forth from a RGBA Hex string to RGBA</summary>
public class RGBAJsonSerializer : JsonConverter<RGBA>
{
    /// <summary>Writes back as a JSON Color String rgba(#,#,#,#)</summary>
    public override void WriteJson( JsonWriter writer, RGBA? value, JsonSerializer serializer )
    {
        string s = "rgba(0,0,0,0)";

        if (value != null && value != RGBA.Empty)
            s = value.ToString();

        writer.WriteValue( s );
    }

    /// <summary>Parse a color string into RGBA</summary>
    /// <remarks>The input color string can be in various formats</remarks>
    public override RGBA ReadJson( JsonReader reader, Type objectType, RGBA? existingValue, bool hasExistingValue, JsonSerializer serializer )
    {
        string? value = reader.Value?.ToString();

        if (String.IsNullOrWhiteSpace(value))
            return RGBA.Empty;

        bool results = RGBA.TryParse(value, out RGBA color);

        return (results) ? color : RGBA.Empty;
    }
}

/// <summary>(De)Serializers a list of RGBA Hex strings to RGBA</summary>
public class RGBAListJsonSerializer : JsonConverter<IEnumerable<RGBA>>
{
    /// <summary></summary>
    public override IEnumerable<RGBA> ReadJson( JsonReader reader, Type objectType, IEnumerable<RGBA>? value, bool hasExistingValue, JsonSerializer serializer )
    {
        List<RGBA> colors = new();

        string? s = reader.ReadAsString();

        if ( s == null )
        {
            var x = reader.TokenType;
            return colors;
        }

        do
        {
            // only add if s actually parses to a color
            if ( !String.IsNullOrWhiteSpace( s ) && RGBA.TryParse( s, out RGBA color ) )
                colors.Add( color );

            s = reader.ReadAsString();

        } while ( reader.TokenType != JsonToken.EndArray );

        return colors;
    }

    /// <summary></summary>
    public override void WriteJson(JsonWriter writer, IEnumerable<RGBA>? value, JsonSerializer serializer)
    {
        // if no incoming value then just create empty array
			if ( value == null )
			{
				writer.WriteStartArray();
				writer.WriteEndArray();
				return;
			}

			// cast to an array of enums
			List<RGBA> colors = ((IEnumerable<RGBA>)value).ToList();

			// write each value as upper
			writer.WriteStartArray();
			foreach ( RGBA color in colors )
				writer.WriteValue( color.ToString() );
			writer.WriteEndArray();
	}
}

/// <summary>(De)Serializers a JSON string back and forth from a RGBA Hex string to nullable RGBA</summary>
/// <remarks>Consider removing for just tansparent</remarks>
public class NullableRGBAJsonSerializer : JsonConverter<RGBA?>
{
    /// <summary>Writes back as a Hex Color String with no leading hash</summary>
    public override void WriteJson(JsonWriter writer, RGBA? value, JsonSerializer serializer)
    {
        if (value == null)
            writer.WriteValue(String.Empty);

        writer.WriteValue( value?.ToString() );
    }

    /// <summary>Parse a color string into nullable SKColor</summary>
    /// <remarks>The input color string can be in various formats</remarks>
    public override RGBA? ReadJson(JsonReader reader, Type objectType, RGBA? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        string? value = reader.Value?.ToString();

        if (String.IsNullOrWhiteSpace(value))
            return null;

        bool results = RGBA.TryParse( value, out RGBA color );

        return ( results ) ? color : RGBA.Empty;
    }
}

