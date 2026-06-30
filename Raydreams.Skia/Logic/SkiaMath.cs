using Raydreams.Skia.Model;
using SkiaSharp;

namespace Raydreams.Skia.Logic;

/// <summary></summary>
/// <param name="text"></param>
/// <param name="rect"></param>
/// <param name="typeFace"></param>
/// <returns></returns>
public delegate LabelMetrics CalculateTextMetrics( string text, SKRect rect, SKTypeface typeFace);

public static class SkiaMath
{
    /// <summary>Normalizes the input angle to an equivalent positive angle between 0 and 360.</summary>
    /// <param name="theta">Input angle in degrees.</param>
    /// <returns>Normalized angle in degrees.</returns>
    /// <example>An input of -1677831.2621266 would return 128.737873400096</example>
    public static float Revolution( float theta )
    {
        while ( theta < 0.0 || theta > 360.0F )
        {
            if ( theta > 360.0F )
                theta -= 360.0F;
            else if ( theta < 0.0 )
                theta += 360.0F;
            else break;
        }

        return theta;
    }

    /// <summary>Returns a point in cartesian coordinates from angle in radians</summary>
    /// <remarks>
    /// This is how most of the icons are drawn using a center origin and angle around the origin to various points.
    /// Precalculated anlges are in Raydreams.Common.Logic.Angles
    /// 0 rad is to the right going CCW
    /// </remarks>
    public static SKPoint Polar2CartesianRad( double radius, double radians )
    {
        return new SKPoint( Convert.ToSingle( radius * Math.Cos( radians ) ), Convert.ToSingle( radius * Math.Sin( radians ) ) );
    }

    /// <summary>Returns a point in cartesian coordinates from angle in degrees</summary>
    public static SKPoint Polar2CartesianDeg( double radius, double deg )
    {
        return Polar2CartesianRad( radius, deg * Math.PI / 180.0 );
    }

    /// <summary>Calculates the chord length from a given angle and radius/summary>
    /// <param name="angle">Angle in radians</param>
    /// <param name="rad">Radius</param>
    public static double ChordLength( double radius, double angle )
    {
        if ( radius < 0 || angle < 0 )
            return 0;

        return Math.Sin( angle / 2.0 ) * 2.0 * radius;
    }

    /// <summary>Calculates the chord length from a given angle and radius/summary>
    /// <param name="length">Length of the chord</param>
    /// <param name="rad">Radius</param>
    public static double ChordAngle( double radius, double length )
    {
        if ( radius < 0 || length < 0 )
            return 0;

        return ( Math.Asin( length / ( 2.0 * radius ) ) ) * 2.0;
    }
}