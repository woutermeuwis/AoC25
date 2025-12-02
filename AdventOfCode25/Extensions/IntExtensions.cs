namespace AdventOfCode25.Extensions;

public static class IntExtensions
{
	public static int PosMod(this int a, int b)
	{
		while (a < 0)
			a += b;
		return a % b;
	}
}