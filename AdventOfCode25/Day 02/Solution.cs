using System.Text;

namespace AdventOfCode25.Day_02;

public class Solution : BaseSolution
{
	protected override void SolveOne(string fileName)
	{
		GetInput(fileName)
			.SelectMany(range => ExpandRange(range.Start, range.End))
			.Where(id => !IsValidId_One(id))
			.Sum()
			.Log(Logger, sum => $"The sum of all invalid IDs is: {sum}");
	}

	protected override void SolveTwo(string fileName)
	{
		GetInput(fileName)
			.SelectMany(range => ExpandRange(range.Start, range.End))
			.Where(id => !IsValidId_Two(id))
			.Sum()
			.Log(Logger, sum => $"The sum of all invalid IDs is: {sum}");
	}

	private (long Start, long End)[] GetInput(string file)
	{
		return InputReader.ReadAllText(GetDay(), file)
			.Split(',')
			.Select(range => (range.Split('-')[0].ToLong(), range.Split('-')[1].ToLong()))
			.ToArray();
	}

	private IEnumerable<long> ExpandRange(long start, long end)
	{
		for (var i = start; i <= end; i++)
			yield return i;
	}

	private bool IsValidId_One(long id)
	{
		var str = id.ToString();
		var len = str.Length;
		if (len % 2 > 0) return true;
		return str[..(len / 2)] != str[(len / 2)..];
	}

	private bool IsValidId_Two(long id)
	{
		var str = id.ToString();
		var len = str.Length;

		// substring length
		for (var l = 1; l <= len / 2; l++)
		{
			var reps = len / l;
			if (reps * l != len)
				continue;
			if (str == BuildString(str[..l], reps))
				return false;
		}

		return true;
	}

	private string BuildString(string str, int reps)
	{
		var sb = new StringBuilder();
		for (var i = 0; i < reps; i++) sb.Append(str);
		return sb.ToString();
	}
}