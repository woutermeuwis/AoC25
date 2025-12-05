using System.Numerics;

namespace AdventOfCode25.Models;

public class Range<T> where T : INumber<T>
{
	public T Start { get; init; }
	public T End { get; init; }

	public Range(T start, T end)
	{
		Start = start;
		End = end;
	}

	public bool CheckInRange(T value, bool inclusive = true)
		=> inclusive
			? Start <= value && value <= End
			: Start < value && value < End;

	public T Count() => End - Start + T.One;
}