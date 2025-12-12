namespace AdventOfCode25.Day_12;

public class Solution : BaseSolution
{
	protected override void SolveOne(string fileName)
	{
		var (presents, areas) = GetInput(fileName);
		areas.Count(a => a.FitPresents(presents))
			.Log(Logger, cnt => $"{cnt} Areas can fit their presents");
	}

	protected override void SolveTwo(string fileName)
	{
		throw new NotImplementedException();
	}

	private (Present[] Presents, Area[] Areas) GetInput(string file)
	{
		var lines = InputReader.ReadAllLines(GetDay(), file);
		var presents = new List<Present>();
		var areas = new List<Area>();
		for (var i = 0; i < lines.Length; i++)
		{
			var line = lines[i];
			switch (line.Length)
			{
				case > 1 when line[1] == ':':
				{
					i++;
					line = lines[i];
					List<string> input = [];
					while (line.Trim().Length > 0)
					{
						input.Add(line);
						line = lines[++i];
					}

					presents.Add(new(input));
					break;
				}
				case > 4:
					areas.Add(new(line));
					break;
			}
		}

		return (presents.ToArray(), areas.ToArray());
	}
}

class Present
{
	private readonly bool[][] _shape;

	public Present(List<string> input) => _shape = input.Select(line => line.Select(c => c == '#').ToArray()).ToArray();

	public int Size => _shape.Sum(row => row.Count(x => x));
}

class Area
{
	private readonly int _width;
	private readonly int _depth;
	private readonly int[] _presents;

	private int Size => _width * _depth;

	public Area(string input)
	{
		_width = input.Split('x')[0].ToInt();
		_depth = input.Split('x')[1].Split(':')[0].ToInt();
		_presents = input.Split(' ')[1..].Select(x => x.ToInt()).ToArray();
	}

	public bool FitPresents(Present[] presents)
	{
		var usedArea = _presents.Select((count, index) => count * presents[index].Size).Sum();
		return usedArea <= Size;
	}
}