namespace AdventOfCode25.Models;

public record Bounds3D(long X, long Y, long Z, long Width, long Height, long Depth)
{
	public long Left => Width > 0
		? X
		: X + Width - 1;

	public long Right => Width > 0
		? X + Width - 1
		: X;

	public long Top => Height > 0
		? Y
		: Y + Height - 1;

	public long Bottom => Height > 0
		? Y + Height - 1
		: Y;

	public long Front => Depth > 0
		? Z
		: Z + Depth - 1;

	public long Back => Depth > 0
		? Z + Depth - 1
		: Z;

	public Bounds3D(long width, long height, long depth) : this(0, 0, 0, width, height, depth)
	{
	}

	public Bounds3D(long side) : this(side, side, side)
	{
	}
}