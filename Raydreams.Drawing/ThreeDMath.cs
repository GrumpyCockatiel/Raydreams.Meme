using Raydreams.Drawing.Model;

namespace Raydreams.Drawing;

public static class ThreeDMath
{
	//public static bool IsZero(double d) => Math.Abs(d) < 0.0000000010;

	/// <summary>Translate a 3D point by the specified offset</summary>
	/// <returns></returns>
	public static PointF3D Translate(this PointF3D pt, PointF3D offset) => new PointF3D(pt.X + offset.X, pt.Y + offset.Y, pt.Z + offset.Z);

	/// <summary>Return a points distance from the origin or magnitude</summary>
	/// <returns></returns>
	public static float DistanceFromOrigin(this PointF3D point) => Convert.ToSingle(Math.Sqrt(point.X * point.X + point.Y * point.Y + point.Z * point.Z));

	/// <summary>Translate a 3D Vector so p1 is at the origin</summary>
	public static PointF3D Vector2Origin(this (PointF3D p1, PointF3D p2) vec) => new PointF3D(vec.p2.X - vec.p1.X, vec.p2.Y - vec.p1.Y, vec.p2.Z - vec.p1.Z);

	/// <summary>Creat a 3D Transformation Matrix</summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <param name="z"></param>
	/// <returns></returns>
	public static (float[,] rx, float[,] ry, float[,] rz) TransformationMatrices(float x, float y, float z)
	{
		float[,] rx = { {1, 0, 0},
			{0, Convert.ToSingle(Math.Cos(x)), -Convert.ToSingle(Math.Sin(x)) },
			{0, Convert.ToSingle(Math.Sin(x)), Convert.ToSingle(Math.Cos(x)) }
		};

		float[,] ry = { {Convert.ToSingle(Math.Cos(y)), 0, Convert.ToSingle(Math.Sin(y))},
			{0, 1, 0 },
			{-Convert.ToSingle(Math.Sin(y)), 0, Convert.ToSingle(Math.Cos(y)) }
		};

		float[,] rz = { {Convert.ToSingle(Math.Cos(z)), -Convert.ToSingle(Math.Sin(z)), 0},
			{Convert.ToSingle(Math.Sin(z)), Convert.ToSingle(Math.Cos(z)), 0 },
			{0, 0, 1 }
		};

		//var rotated = Multiply2DMatrices(rx, pt.ToArray());
		//return new PointF3D(rotated[0, 0], rotated[1, 0], rotated[2, 0]);

		return (rx, ry, rz);
	}

	/// <summary>Rotate around the X axes, + is like tilting-up</summary>
	/// <param name="pt"></param>
	/// <param name="rads"></param>
	/// <returns></returns>
	public static PointF3D RotateX(this PointF3D pt, float rads)
	{
		// precalc the Cos and Sin of the angle
		float cos = Convert.ToSingle(Math.Cos(rads));
		float sin = Convert.ToSingle(Math.Sin(rads));

		// origin factors out
		return new PointF3D(pt.X, (pt.Y * cos) - (pt.Z * sin), (pt.Y * sin) + (pt.Z * cos));
	}

	/// <summary>Rotate around the Y axes, + is like tilting-up</summary>
	/// <param name="pt"></param>
	/// <param name="rads"></param>
	/// <returns></returns>
	public static PointF3D RotateY( this PointF3D pt, float rads )
	{
        // precalc the Cos and Sin of the angle
        float cos = Convert.ToSingle( Math.Cos( rads ) );
        float sin = Convert.ToSingle( Math.Sin( rads ) );

        // origin factors out
        return new PointF3D( ( pt.X * cos ) + ( pt.Z * sin ), pt.Y, -( pt.X * sin ) + ( pt.Z * cos ) );
    }

	/// <summary>Rotate around the Z axes, + CCw</summary>
	/// <param name="pt"></param>
	/// <param name="rads"></param>
	/// <returns></returns>
	public static PointF3D RotateZ(this PointF3D pt, float rads)
	{
		// precalc the Cos and Sin of the angle
		float cos = Convert.ToSingle(Math.Cos(rads));
		float sin = Convert.ToSingle(Math.Sin(rads));

		// origin factors out
		return new PointF3D((pt.X * cos) - (pt.Y * sin), (pt.X * sin) + (pt.Y * cos), pt.Z);
	}

	/// <summary></summary>
	/// <param name="a"></param>
	/// <param name="b"></param>
	/// <returns></returns>
	public static float[,] Multiply2DMatrices(float[,] a, float[,] b)
	{
		int rA = a.GetLength(0);
		int cA = a.GetLength(1);
		int rB = b.GetLength(0);
		int cB = b.GetLength(1);

		if (cA != rB)
			throw new ArgumentException("Matrices cannot be multiplied!");

		float temp = 0;
		float[,] x = new float[rA, cB];

		// iterate rows of a
		for (int i = 0; i < rA; ++i)
		{
			// iterate cols of b
			for (int j = 0; j < cB; ++j)
			{
				temp = 0;
				// iterate cols of a
				for (int k = 0; k < cA; ++k)
				{
					temp += a[i, k] * b[k, j];
				}
				x[i, j] = temp;
			}
		}

		return x;
	}

	/// <summary>Get the cross product of 2 3D Vectors</summary>
	public static PointF3D CrossProduct(PointF3D a, PointF3D b)
	{
		var i = a.Y * b.Z - a.Z * b.Y;
		var j = a.Z * b.X - a.X * b.Z;
		var k = a.X * b.Y - a.Y * b.X;

		return new PointF3D(i, j, k);
	}
}

// https://www.cuemath.com/geometry/angle-between-vectors/
// Angle Between Two Vectors in 3D
// Let us consider an example to find the angle between two vectors in 3D. Let a = i + 2j + 3k and b = 3i - 2j + k. We will compute the dot product and the magnitudes first:

// a · b = <1, 2, 3> ·<3, -2, 1> = 1(3) + (-2)(-2) + 3(1) = 3 - 4 + 3 = 2.
// |a| = √(1)² + (2)² + 3² = √1 + 4 +9 = √14
// |b| = √(3)² + (-2)² + 1² = √9 + 4 + 1 = √14
// We have θ = cos-1 [ (a · b) / (|a| |b|) ].

// Then θ = cos-1 (2 / √14 · √14) = cos-1 (2 / 14) = cos-1 (1/7) ≈ 81.79°.