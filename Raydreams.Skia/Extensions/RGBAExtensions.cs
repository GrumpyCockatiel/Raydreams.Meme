using Raydreams.Drawing;
using SkiaSharp;

namespace Raydreams.SKGraphs.Extensions;

public static class RGBAExtensions
{
	/// <summary>Converts RGBA to SKColor</summary>
	/// <param name="color"></param>
	/// <returns></returns>
	public static SKColor ToSK( this RGBA color ) => new SKColor( color.Red, color.Green, color.Blue, color.Alpha );

	/// <summary>Converts HSVA to SKColor</summary>
	public static SKColor ToSK(this HSV color) => SKColor.FromHsv( color.Hue, color.Saturation, color.Value, color.Alpha );
}


