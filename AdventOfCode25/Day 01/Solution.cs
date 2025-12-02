namespace AdventOfCode25.Day_01;

public class Solution : BaseSolution
{
	protected override void SolveOne(string fileName)
	{
		var input = GetInput(fileName);
		var current = 50;
		var result = 0;
		foreach (var (dir, dist) in input)
		{
			current = dir switch
			{
				Direction.Left => current - dist,
				Direction.Right => current + dist,
				_ => throw new ArgumentOutOfRangeException(nameof(dir))
			};
			current = current.PosMod(100);
			if (current == 0)
				result++;
		}

		Logger($"Password = {result}");
	}

	protected override void SolveTwo(string fileName)
	{
		var input = GetInput(fileName);
		var current = 50;
		var result = 0;
		foreach (var (dir, dist) in input)
		{
			switch (dir)
			{
				case Direction.Left:
					for (var i = 0; i < dist; i++)
					{
						current--;
						if (current == 0)
							result++;
						if (current == -1)
							current = 99;
					}

					break;
				case Direction.Right:
					for (var i = 0; i < dist; i++)
					{
						current++;
						if (current == 100)
							current = 0;
						if (current == 0)
							result++;
					}

					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(dir));
			}

			current = current.PosMod(100);
		}

		Logger($"Password = {result}");
	}

	private (Direction dir, int dist)[] GetInput(string file)
	{
		return InputReader.ReadAllLines(GetDay(), file)
			.Select(line => (line[0] == 'L' ? Direction.Left : Direction.Right, line[1..].ToInt()))
			.ToArray();
	}
}