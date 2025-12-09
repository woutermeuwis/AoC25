namespace AdventOfCode25.Models;

/// <summary>
/// Represents points in a X/Y/Z coordinate system
/// X goes from left to right (so a positive X is to the right of the origin)
/// Y goes from top to bottom (so a positive Y is below the origin)
/// Z goes from front to back (so a positive Z is behind the origin) 
/// </summary>
/// <param name="X"></param>
/// <param name="Y"></param>
/// <param name="Z"></param>
public record Point3D(long X, long Y, long Z)
{
	#region Factory

	public static Point3D Zero => new(0, 0, 0);

	#endregion

	#region Operators

	public static Point3D operator +(Point3D p1, Point3D p2)
		=> new(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);

	public static Point3D operator -(Point3D p1, Point3D p2)
		=> new(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);

	public static Point3D operator *(Point3D p, int scalar)
		=> new(p.X * scalar, p.Y * scalar, p.Z * scalar);

	public static Point3D operator *(int scalar, Point3D p)
		=> p * scalar;

	public static Point3D operator *(Point3D p, long scalar)
		=> new(p.X * scalar, p.Y * scalar, p.Z * scalar);

	public static Point3D operator *(long scalar, Point3D p)
		=> p * scalar;

	public static Point3D operator /(Point3D p, int scalar)
		=> new(p.X / scalar, p.Y / scalar, p.Z / scalar);

	public static long operator /(Point3D p1, Point3D p2)
	{
		var x = p1.X / p2.X;
		var y = p1.Y / p2.Y;
		var z = p1.Z / p2.Z;
		if (x != y || y != z)
			throw new ArgumentException($"Dividing {p1} by {p2} does not yield a proper scalar!");
		return x;
	}

	public static Point3D operator %(Point3D p1, Point3D p2)
		=> new(p1.X % p2.X, p1.Y % p2.Y, p1.Z % p2.Z);

	#endregion

	#region Bounds

	public bool IsInBounds(long x1, long y1, long z1, long x2, long y2, long z2)
		=> x1 <= X && X <= x2 && y1 <= Y && Y <= y2 && z1 <= Z && Z <= z2;

	public bool IsInBounds(Bounds3D bounds)
		=> IsInBounds(bounds.Left, bounds.Top, bounds.Front, bounds.Right, bounds.Bottom, bounds.Back);

	#endregion

	#region Comparison

	public bool IsLeftOf(Point3D p)
		=> p.X > X;

	public bool IsRightOf(Point3D p)
		=> p.X < X;

	public bool IsAbove(Point3D p)
		=> p.Y > Y;

	public bool IsBelow(Point3D p)
		=> p.Y < Y;

	public bool IsInFrontOf(Point3D p)
		=> p.Z > Z;

	public bool IsBehind(Point3D p)
		=> p.Z < Z;

	#endregion

	public bool IsNeighbour(Point3D p)
		=> Math.Abs(p.X - X) <= 1
		   && Math.Abs(p.Y - Y) <= 1
		   && Math.Abs(p.Z - Z) <= 1;

	public decimal Size
		=> (decimal)Math.Sqrt((long)Math.Pow(X, 2) + (long)Math.Pow(Y, 2) + (long)Math.Pow(Z, 2));

	public bool IsDivisibleBy(Point p)
		=> X / p.X == Y / p.Y;

	public long GetManhattanDistTo(Point3D p)
		=> Math.Abs(p.X - X) + Math.Abs(p.Y - Y) + Math.Abs(p.Z - Z);
	
	public override string ToString()
		=> $"({X}, {Y}, {Z})";
}