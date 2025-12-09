using AdventOfCode25.Helpers.Algorithms.PointInPolygon;

namespace AdventOfCode25.Day_09;

public class Solution : BaseSolution
{
	protected override void SolveOne(string fileName)
	{
		GetAreas(GetTiles(fileName))
			.Max(r => r.GetArea())
			.Log(Logger, area => $"The largest area is {area}");
	}

	protected override void SolveTwo(string fileName)
	{
		var redTiles = GetTiles(fileName);
		var rayCast = new RayCasting(GetColoredEdges(redTiles).ToArray());
		Logger("Parsed Input");

		var areas = GetAreas(redTiles);
		Logger("Calculated Areas");
		var ordered = areas.OrderByDescending(r => r.GetArea()).ToList();
		Logger($"Ordered Areas ({ordered.Count})");
		var max = ordered
			.FirstOrDefault(r => r.GetFullEdge().All(p => rayCast.IsInside(p)));
		Logger("Found Max Area");
		max?.GetArea()
			.Log(Logger, area => $"The largest area is {area}");
	}

	private IEnumerable<Rectangle> GetAreas(Point[] redTiles)
	{
		for (var i = 0; i < redTiles.Length; i++)
		{
			for (var j = i + 1; j < redTiles.Length; j++)
			{
				var p1 = redTiles[i];
				var p2 = redTiles[j];
				var rect = new Rectangle(p1, p2);
				yield return rect;
			}
		}
	}

	private List<Point> GetColoredEdges(Point[] red)
	{
		var edges = new List<Point>();

		// start with the last tile to easily wrap around the list
		var prev = red[^1];
		// Add all edges between red tiles
		foreach (var cur in red)
		{
			if (prev.X == cur.X)
			{
				var y1 = Math.Min(prev.Y, cur.Y);
				var y2 = Math.Max(prev.Y, cur.Y);
				for (var y = y1; y <= y2; y++)
					edges.Add(new(prev.X, y));
			}
			else
			{
				var x1 = Math.Min(prev.X, cur.X);
				var x2 = Math.Max(prev.X, cur.X);
				for (var x = x1; x <= x2; x++)
					edges.Add(new(x, prev.Y));
			}

			prev = cur;
		}

		return edges;
	}

	private Point[] GetTiles(string file)
		=> InputReader.ReadAllLines(GetDay(), file)
			.Select(line => line.Split(','))
			.Select(tile => new Point(tile[0].ToLong(), tile[1].ToLong()))
			.ToArray();
}

class Rectangle
{
	public Point Corner1 { get; init; }
	public Point Corner2 { get; init; }

	public long X0 => Math.Min(Corner1.X, Corner2.X);
	public long X1 => Math.Max(Corner1.X, Corner2.X);
	public long Y0 => Math.Min(Corner1.Y, Corner2.Y);
	public long Y1 => Math.Max(Corner1.Y, Corner2.Y);

	public Rectangle(Point corner1, Point corner2)
		=> (Corner1, Corner2) = (corner1, corner2);

	public Point[] GetFullEdge()
	{
		var x1 = Math.Min(Corner1.X, Corner2.X);
		var y1 = Math.Min(Corner1.Y, Corner2.Y);
		var x2 = Math.Max(Corner1.X, Corner2.X);
		var y2 = Math.Max(Corner1.Y, Corner2.Y);

		var edge = new List<Point>();

		for (var x = x1; x <= x2; x++)
		{
			edge.Add(new(x, y1));
			edge.Add(new(x, y2));
		}

		for (var y = y1 + 1; y < y2; y++)
		{
			edge.Add(new(x1, y));
			edge.Add(new(x2, y));
		}

		return edge.ToArray();
	}

	public long GetArea()
		=> (X1 - X0 + 1) * (Y1 - Y0 + 1);
}