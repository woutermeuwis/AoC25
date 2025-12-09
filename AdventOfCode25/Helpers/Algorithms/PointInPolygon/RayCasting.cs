using AdventOfCode25.Models;

namespace AdventOfCode25.Helpers.Algorithms.PointInPolygon;

/// <summary>
/// This algorithm casts a Ray from the given point in a random direction.
/// If it passes the polygon boundary an odd number of times, the point is inside.
/// This algorithm is slower than BoundingBox, but accurate
/// </summary>
public class RayCasting
{
	private readonly Dictionary<long, long[]> _edgePoints;
	private Dictionary<Point, bool> _cache = [];

	/// <summary>
	/// Creates a ray casting algorithm for the given polygon.
	/// </summary>
	/// <param name="polygon">A list of all the points of the polygon edge</param>
	public RayCasting(Point[] polygon) => _edgePoints = polygon
		.Distinct()
		.GroupBy(p => p.Y)
		.ToDictionary(g => g.Key, g => g.OrderBy(p => p.X).Select(p => p.X).ToArray());

	public bool IsInside(Point p)
	{
		if (_cache.TryGetValue(p, out var result))
			return result;

		if (!_edgePoints.TryGetValue(p.Y, out var xPositions))
			return Store(p, false);

		if (xPositions.Contains(p.X))
			return Store(p, true);

		var edgePointsLeft = xPositions.Where(x => x < p.X).ToArray();

		if (edgePointsLeft.Length == 0)
			return Store(p, false);

		var crossings = 1;
		var prevX = edgePointsLeft[0];
		for (var i = 1; i < edgePointsLeft.Length; i++)
		{
			if (edgePointsLeft[i] == prevX + 1)
			{
				prevX++;
				continue;
			}

			crossings++;
			prevX = edgePointsLeft[i];
		}

		return Store(p, crossings % 2 == 1);
	}

	private bool Store(Point p, bool value)
	{
		_cache[p] = value;
		return value;
	}
}