namespace AdventOfCode25.Day_11;

public class Solution : BaseSolution
{
	protected override void SolveOne(string fileName)
	{
		new Solver("you", "out", GetNodes(fileName.Replace("example", "example1")))
			.Solve()
			.Log(Logger, dist => $"There are {dist} paths leading from you to out.");
	}

	protected override void SolveTwo(string fileName)
	{
		var nodes = GetNodes(fileName.Replace("example", "example2"));
		var step1 = new Solver("svr", "fft", nodes).Solve();
		var step2 = new Solver("fft", "dac", nodes).Solve();
		var step3 = new Solver("dac", "out", nodes).Solve();
		
		var x = new Solver("svr", "out", nodes).Solve();
		Logger($"There are {step1 * step2 * step3} paths leading from svr to out, that pass through fft and dac.");
	}

	private List<Node> GetNodes(string file)
	{
		var dict = InputReader.ReadAllLines(GetDay(), file)
			.ToDictionary(
				line => line[..3],
				line => line[5..].Split(' ').ToList()
			);
		dict.Add("out", []);

		var nodes = dict.Keys
			.Select(k => new Node(k))
			.ToList();
		nodes.ForEach(node => dict[node.Name].Select(name => nodes.First(n => n.Name == name)).ForEach(node.AddChild));
		return nodes;
	}
}

class Node
{
	public string Name { get;  }
	public List<Node> Children { get; } = [];
	private Node[][]? _pathsToOutput;

	public Node(string name)
	{
		Name = name;
	}

	public void AddChild(Node child) => Children.Add(child);

	public Node[][] GetPathsToOutput()
	{
		if (Name == "out") return [[this]];

		if (_pathsToOutput is null)
			_pathsToOutput = Children.SelectMany(c => c.GetPathsToOutput()).Select(p => p.Append(this).ToArray()).ToArray();
		return _pathsToOutput;
	}
}

class Solver
{
	private readonly string _start;
	private readonly string _end;
	private readonly List<Node> _nodes;
	private readonly Dictionary<string, long> _cache = [];

	public Solver(string start, string end, List<Node> nodes)
	{
		_start = start;
		_end = end;
		_nodes = nodes;
	}

	public long Solve()
	{
		var start = _nodes.First(n => n.Name == _start);
		return GetNumberOfPathsToOutput(start);
	}

	long GetNumberOfPathsToOutput(Node node)
	{
		if(node.Name == _end) 
			return 1;
		
		var total = 0L;
		node.Children.ForEach(child => total += _cache.TryGetValue(child.Name, out var numberOfPaths) ? numberOfPaths : _cache[child.Name] = GetNumberOfPathsToOutput(child));
		return total;
	}
}