using System.Runtime.InteropServices;

namespace AdventOfCode25.Day_04;

public class Solution : BaseSolution
{
	protected override void SolveOne(string fileName)
	{
		FindAccessible(GetRolls(fileName))
			.Count()
			.Log(Logger, count => $"There are {count} accessible rolls.");
	}

	protected override void SolveTwo(string fileName)
	{
		var rolls = GetRolls(fileName);
		int removed = 0, totalRemoved = 0;
		do
		{
			var accessible = FindAccessible(rolls).ToArray();
			removed = accessible.Length;
			totalRemoved += removed;
			accessible.ForEach(p => rolls.Set(p, '.'));
		} while (removed > 0);

		Logger($"A total of {totalRemoved} rolls can be removed.");
	}

	private IEnumerable<Point> FindAccessible(char[][] rolls)
	{
		var bounds = new Bounds(0, 0, rolls[0].Length, rolls.Length);
		for (var y = 0; y < bounds.Height; y++)
		{
			for (var x = 0; x < bounds.Width; x++)
			{
				var point = new Point(x, y);
				if (!rolls.Check(point, '@'))
					continue;

				if (point.GetAllNeighbours(bounds).Count(n => rolls.Check(n, '@')) < 4)
					yield return point;
			}
		}
	}

	private char[][] GetRolls(string file)
		=> InputReader.ReadAllLines(GetDay(), file)
			.Select(line => line.ToCharArray())
			.ToArray();
}