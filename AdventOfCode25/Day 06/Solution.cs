namespace AdventOfCode25.Day_06;

public class Solution : BaseSolution
{
	protected override void SolveOne(string fileName)
	{
		InputReader.ReadAllLines(GetDay(), fileName)
			.Select(line => line.Split(' ').Where(s => s.Length > 0).ToArray())
			.ToArray()
			.Transpose()
			.Sum(p => p[^1] == "*" ? p[..^1].Aggregate(1L, (agg, next) => agg * next.ToLong()) : p[..^1].Sum(stringExtensions.ToLong))
			.Log(Logger, sum => $"The sum of all the problems is {sum}");
	}

	protected override void SolveTwo(string fileName)
	{
		var lines = InputReader.ReadAllLines(GetDay(), fileName);
		var longestLine = lines.Max(l => l.Length);

		List<int> splits = [];
		for (var i = 0; i < longestLine; i++)
			if (lines.All(l => l[i] == ' '))
				splits.Add(i);

		var prev = -1;
		List<string[]> problems = [];
		splits.ForEach(split =>
		{
			problems.Add(lines.Select(l => l[(prev+1)..split]).ToArray());
			prev = split;
		});
		problems.Add(lines.Select(l => l[(prev+1)..]).ToArray());

		problems.Sum(p =>
			{
				var numbers = p[..^1];
				var width = numbers.Max(n => n.Length);
				var rtl = Enumerable.Range(0, width)
					.Select(i => new string(numbers
							.Where(n => n.Length > i)
							.Select(n => n[i])
							.ToArray())
						.ToLong()
					).Reverse()
					.ToArray();
				
				return p[^1].Trim() == "*"
					? rtl.Aggregate(1L, (agg, next) => agg * next)
					: rtl.Sum();
			})
			.Log(Logger, sum => $"The sum of all the problems is {sum}");
	}
}