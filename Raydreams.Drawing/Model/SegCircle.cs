using System;

namespace Raydreams.Drawing.Model;

/// <summary>A circle consiting of line segments</summary>
public struct SegCircle
{
	private List<PointF3D> _vertices = [];

	public SegCircle() : this(5,24)
	{}

	/// <summary></summary>
	/// <param name="r">Radius of the circle</param>
	/// <param name="seg">Number of edge segments in the circle</param>
	public SegCircle(float r, int seg)
	{
		this.Radius = r;
		this.Segments = Math.Clamp(seg, 8, 100);
		this.Angle = Convert.ToSingle(2.0 * Math.PI / this.Segments);
		this.CalcVertices();
	}

	/// <summary></summary>
	public float Radius { get; init; }

	/// <summary></summary>
	public int Segments { get; init; }

	/// <summary>Radians between each segment</summary>
	public float Angle { get; init; }

	public List<PointF3D> Vertices => this._vertices;

	/// <summary>Translate every point in the circle to the specified point</summary>
	public void Translate(PointF3D to)
	{
		this._vertices = this._vertices.Select(pt => pt.Translate(to)).ToList();
	}

	/// <summary>Rotate every point in the circle by the specified angle</summary>
	public void Rotate(float rads, Plane3D plane)
	{
		if (plane == Plane3D.Y)
			this._vertices = this._vertices.Select(pt => pt.RotateY(rads)).ToList();
		else if (plane == Plane3D.Z)
			this._vertices = this._vertices.Select(pt => pt.RotateZ(rads)).ToList();
		else
			this._vertices = this._vertices.Select(pt => pt.RotateX(rads)).ToList();
	}

	/// <summary>Determines the location of the vertices on the edge</summary>
	/// <remarks>https://stackoverflow.com/questions/11774038/how-to-render-a-circle-with-as-few-vertices-as-possible</remarks>
	private void CalcVertices()
	{
		this._vertices = [];

		for (int i = 0; i < this.Segments; ++i)
		{
			this._vertices.Add(new PointF3D(Convert.ToSingle(Math.Cos(i * this.Angle)) * this.Radius, Convert.ToSingle(Math.Sin(i * this.Angle)) * this.Radius, 0));
		}
	}
}