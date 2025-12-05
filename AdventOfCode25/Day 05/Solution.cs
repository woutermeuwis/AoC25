namespace AdventOfCode25.Day_05;

public class Solution : BaseSolution
{
	protected override void SolveOne(string fileName)
	{
		var (fresh, ingredients) = GetInput(fileName);
		ingredients.Count(i => fresh.Any(f => f.CheckInRange(i)))
			.Log(Logger, count => $"There are {count} fresh ingredients.");
	}

	protected override void SolveTwo(string fileName)
	{
		var (fresh, ingredients) = GetInput(fileName);
		var definitive = new List<Range<long>>();
		foreach (var range in fresh)
		{
			var toRemove = definitive
				.Where(d => d.End >= range.Start - 1 && d.Start <= range.End + 1)
				.ToList();
			toRemove.ForEach(r => definitive.Remove(r));
			toRemove.Add(range);
			definitive.Add(new(toRemove.Min(r=>r.Start), toRemove.Max(r=>r.End)));
		}
		definitive
			.Sum(d=>d.Count())
			.Log(Logger, count => $"There are {count} fresh ingredient IDs.");
	}

	private (List<Range<long>> Fresh, List<long> Ingredients) GetInput(string file)
	{
		var lines = InputReader.ReadAllLines(GetDay(), file);
		var split = lines.IndexOf("");
		var fresh = lines[0..split]
			.Select(range => new Range<long>(range.Split('-')[0].ToLong(), range.Split('-')[1].ToLong()))
			.ToList();
		var ingredients = lines[(split + 1)..].Select(l => l.ToLong()).ToList();
		return (fresh, ingredients);
	}
}