using Newtonsoft.Json;

namespace Raydreams.Drawing.Model;

/// <summary>A 3D Plane</summary>
public enum Plane3D
{
	X = 0,
	Y = 1,
	Z = 2
}

/// <summary>A 3D Point</summary>
public struct PointF3D
{
	public PointF3D() : this(0, 0, 0)
	{ }

	public PointF3D(float x, float y, float z)
	{
		this.X = x;
		this.Y = y;
		this.Z = z;
	}

	[JsonProperty("x")]
	public float X { get; set; }

	[JsonProperty("y")]
	public float Y { get; set; }

	[JsonProperty("z")]
	public float Z { get; set; }

	/// <summary>Returns a 3x1 array</summary>
	public float[,] ToArray() => new float[,] { { this.X }, { this.Y }, { this.Z } };
}

