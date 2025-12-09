using AdventOfCode25.Helpers.Algorithms.UnionFind;

namespace AdventOfCode25.Day_08;

public class Solution : BaseSolution
{
	protected override void SolveOne(string fileName)
	{
		var junctions = GetInput(fileName);
		var unions = new UnionFind<Point3D>(junctions);

		var goal = IsExample(fileName) ? 10 : 1000;
		var byDist = GetClosestJunctions(junctions);

		for (var i = 0; i < goal; i++)
		{
			var (point1, point2) = byDist[0];
			byDist.RemoveAt(0);
			if (unions.CheckConnection(point1, point2)) continue;
			unions.Unify(point1, point2);
		}

		unions.GetRoots().Select(r => unions.GetComponentSize(r)).OrderByDescending(s => s).Take(3)
			.Take(3)
			.Aggregate(1L, (acc, c) => acc * c)
			.Log(Logger, count => $"The three largest circuits result in a value of {count}");
	}

	protected override void SolveTwo(string fileName)
	{
		var junctions = GetInput(fileName);
		var unions = new UnionFind<Point3D>(junctions);
		var byDist = GetClosestJunctions(junctions);

		(Point3D point1, Point3D point2) pair;
		do
		{
			pair = byDist[0];
			byDist.RemoveAt(0);
			if (unions.CheckConnection(pair.point1, pair.point2)) continue;
			unions.Unify(pair.point1, pair.point2);
		} while (unions.Count() > 1);

		Logger($"The length of the last cord is: {pair.point1.X * pair.point2.X}");
	}

	List<(Point3D, Point3D)> GetClosestJunctions(Point3D[] junctions)
	{
		var dict = new Dictionary<(Point3D, Point3D), decimal>();
		List<Point3D> history = [junctions[0]];
		for (var i = 1; i < junctions.Length; i++)
		{
			var cur = junctions[i];
			history.ForEach(h => dict.Add((h, cur), (cur - h).Size));
			history.Add(cur);
		}

		return dict.OrderBy(kvp => kvp.Value).Select(kvp => kvp.Key).ToList();
	}

	private Point3D[] GetInput(string file)
		=> InputReader.ReadAllLines(GetDay(), file)
			.Select(l => l.Split(',').Select(stringExtensions.ToLong).ToArray())
			.Select(arr => new Point3D(arr[0], arr[1], arr[2]))
			.ToArray();
}