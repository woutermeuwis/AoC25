namespace AdventOfCode25.Day_07;

public class Solution : BaseSolution
{
	protected override void SolveOne(string fileName)
	{
		var (width, height, start, splitters) = GetInput(fileName);
		List<long> beams = [start.X];
		var splits = 0;
		for (var y = start.Y + 1; y < height; y++)
		{
			var collisions = beams
				.Where(x => splitters.Contains(new(x, y)))
				.ToList();
			collisions.ForEach(x => beams.Remove(x));
			collisions.ForEach(x =>
			{
				if (x > 0 && !beams.Contains(x - 1)) beams.Add(x - 1);
				if (x < width - 1 && !beams.Contains(x + 1)) beams.Add(x + 1);
			});
			splits += collisions.Count;
		}

		Logger($"There are {splits} splits.");
	}

	protected override void SolveTwo(string fileName)
	{
		var (width, height, start, splitters) = GetInput(fileName);
		List<(long X, long Value)> beams = [(start.X, 1)];
		for (var y = start.Y + 1; y < height; y++)
		{
			var collisions = beams
				.Where(b => splitters.Contains(new(b.X, y)))
				.ToList();
			collisions.ForEach(c => beams.Remove(c));
			collisions.ForEach(c =>
			{
				if (c.X > 0)
				{
					if (beams.Any(b => b.X == c.X - 1))
					{
						var existing = beams.FirstOrDefault(b => b.X == c.X - 1);
						beams.Remove(existing);
						beams.Add((existing.X, existing.Value + c.Value));
					}
					else
					{
						beams.Add((c.X - 1, c.Value));
					}
				}

				if (c.X < width - 1)
				{
					if (beams.Any(b => b.X == c.X + 1))
					{
						var existing = beams.FirstOrDefault(b => b.X == c.X + 1);
						beams.Remove(existing);
						beams.Add((existing.X, existing.Value + c.Value));
					}
					else
					{
						beams.Add((c.X + 1, c.Value));
					}
				}
			});
		}

		Logger($"There are {beams.Sum(b => b.Value)} timelines.");
	}

	private Diagram GetInput(string file)
	{
		var map = InputReader.ReadAllLines(GetDay(), file)
			.Select(line => line.ToCharArray())
			.ToArray();

		var height = map.Length;
		var width = map[0].Length;

		List<Point> splitters = [];
		Point? start = null;

		for (var y = 0; y < height; y++)
		for (var x = 0; x < width; x++)
			if (map.Check(x, y, 'S'))
				start = new(x, y);
			else if (map.Check(x, y, '^'))
				splitters.Add(new(x, y));

		if (start is null)
			throw new("No start point found");
		return new(width, height, start, splitters);
	}

	record Diagram(int Width, int Height, Point Start, List<Point> Splitters);
}