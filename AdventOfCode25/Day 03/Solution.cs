namespace AdventOfCode25.Day_03;

public class Solution : BaseSolution
{
	protected override void SolveOne(string fileName)
	{
		var packs = GetInput(fileName);
		packs.Select(pack => GetPackJoltage(pack, 2))
			.Sum()
			.Log(Logger, sum => $"The total joltage output is {sum}");
	}

	protected override void SolveTwo(string fileName)
	{
		var packs = GetInput(fileName);
		packs.Select(pack => GetPackJoltage(pack, 12))
			.Sum()
			.Log(Logger, sum => $"The total joltage output is {sum}");	}

	private long GetPackJoltage(int[] pack, int numberOfBatteries)
	{
		if (numberOfBatteries == 1) return pack.Max();
		var max = pack[..^(numberOfBatteries - 1)].Max();
		var index = pack.IndexOf(max);
		return (max * (long)Math.Pow(10, numberOfBatteries - 1)) + GetPackJoltage(pack[(index + 1)..], numberOfBatteries - 1);
	}

	private int[][] GetInput(string filename)
		=> InputReader.ReadAllLines(GetDay(), filename)
			.Select(line => line.ToCharArray().Select(CharExtensions.ToDigit).ToArray())
			.ToArray();
}