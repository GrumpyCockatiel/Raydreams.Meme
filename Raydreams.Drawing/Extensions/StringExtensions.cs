namespace Raydreams.Drawing.Extensions;

/// <summary>Static Functions to more safely convert data types from a string to a specific data type.</summary>
/// <remarks>Not all data types have been added yet and more are added as they are needed</remarks>
public static class DataTypeConverter
{
    /// <summary></summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static T GetEnumValue<T>(this short value) where T : struct, IConvertible 
    {
		Type type = typeof(T);

		if (!type.IsEnum || !Enum.IsDefined(type, value))
			return default;

		return Enum.GetValues(type).Cast<T>().First(e => value == Convert.ToInt16(e));
	}

    /// <summary>Converts a string to an enum value of enum T failing to default(T)</summary>
    /// <param name="ignoreCase">Ignore case by default</param>
    /// <returns></returns>
    /// <remarks>Case is ignored</remarks>
    public static T GetEnumValue<T>(this string value, bool ignoreCase = true) where T : struct, IConvertible
    {
        T result = default;

        if (String.IsNullOrWhiteSpace(value))
            return result;

        if (Enum.TryParse<T>(value.Trim(), ignoreCase, out result))
            return result;

        return default(T);
    }

    /// <summary>Converts a string to an enum value with the specified default on fail</summary>
    /// <param name="def">Explicit default value if parsing fails</param>
    /// <param name="ignoreCase">Ignore case by default</param>
    /// <returns></returns>
    public static T GetEnumValue<T>(this string value, T def, bool ignoreCase = true) where T : struct, IConvertible
    {
        T result = def;

        if (String.IsNullOrWhiteSpace(value))
            return result;

        if (Enum.TryParse<T>(value.Trim(), ignoreCase, out result))
            return result;

        return def;
    }

    /// <summary>Converts a string to an enum value - returning null on failure</summary>
    /// <param name="value">The value to attempt to parse</param>
    /// <param name="ignoreCase">Ignore case by default</param>
    /// <returns></returns>
    public static Nullable<T> GetNullableEnumValue<T>(this string value, bool ignoreCase = true) where T : struct, IConvertible
    {
        if (String.IsNullOrWhiteSpace(value))
            return null;

        if (Enum.TryParse<T>(value.Trim(), ignoreCase, out T result))
            return result;

        return null;
    }

}
