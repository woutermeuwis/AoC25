namespace AdventOfCode25.Day_10;

public class Solution : BaseSolution
{
	protected override void SolveOne(string fileName)
	{
		InputReader.ReadAllLines(GetDay(), fileName)
			.Select(m => new Machine(m))
			.Sum(m => m.GeShortestCombinationToMatchLights())
			.Log(Logger, sum => $"The total amount of buttons pressed is {sum}");
	}

	protected override void SolveTwo(string fileName)
	{
		var machines = InputReader.ReadAllLines(GetDay(), fileName)
			.Select(m => new Machine(m))
			.ToList();
		throw new NotImplementedException();
	}
}

class Machine
{
	private int RequestedIndicatorLights { get; set; }
	private List<int> Buttons { get; set; } = [];
	private List<int> Joltages { get; set; } = [];

	public Machine(string input)
	{
		foreach (var block in input.Split(' '))
		{
			switch (block[0])
			{
				case '[':
					RequestedIndicatorLights = block[1..^1].Select((c, i) => c == '#' ? (int)Math.Pow(2, i) : 0).Sum();
					break;
				case '(':
					Buttons.Add(block[1..^1].Split(',').Select(stringExtensions.ToInt).Select(i => (int)Math.Pow(2, i)).Sum());
					break;
				case '{':
					Joltages.AddRange(block[1..^1].Split(',').Select(stringExtensions.ToInt));
					break;
			}
		}
	}

	public int GeShortestCombinationToMatchLights()
	{
		List<List<int>> Match(List<int> options, int cnt)
		{
			if (cnt == 0) return [];
			var result = new List<List<int>>();
			foreach (var option in options)
			{
				if (cnt == 1)
					result.Add([option]);
				else
					result.AddRange(Match(options, cnt - 1).Select(c => c.Append(option).ToList()));
			}

			return result;
		}

		for (var i = 1; i <= Buttons.Count; i++)
		{
			var combinations = Match(Buttons, i);
			if (combinations.Any(b => b.Aggregate(0, (a, c) => a ^ c) == RequestedIndicatorLights))
				return i;
		}

		throw new("No combination found");
	}

	public int GeShortestCombinationToMatchJoltages()
	{
		var maxPressesPerButton = Joltages.Max();
		// TODO: DFS here I guess :shrug:

		throw new("No combination found");
	}

	private bool CheckLink(int bits, int i) => (bits & (1 << i)) == 1;
	
	private int CheckPressesWithJoltages(List<int> presses)
	{
		var result = presses.Select((cnt, i) => Buttons.Select(b => CheckLink(b, i) ? cnt : 0).ToArray()).ToArray().Transpose().Select(arr => arr.Sum()).ToArray();
		for (var i = 0; i < result.Length; i++)
		{
			if (result[i] < Joltages[i])
				return -1;
			if (result[i] > Joltages[i])
				return 1;
		}

		return 0;
	}

}