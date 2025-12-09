using System.Numerics;

namespace AdventOfCode25.Extensions;

public static class NumberExtensions
{
	public static T AbsDiff<T>(this T a, T b) where T : INumber<T>
		=> a - b > T.Zero ? a - b : b - a;
}