namespace AdventOfCode25.Extensions;

public static class IEnumerableExtensions
{
	public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> enumerable, bool condition, Func<T, bool> predicate)
		=> condition
			? enumerable.Where(predicate)
			: enumerable;

	public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> predicate)
	{
		foreach (var element in enumerable)
			predicate(element);
	}
	
	public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T,int> predicate)
	{
		foreach (var tuple in enumerable.Select((element, index) => (Element: element, Index: index)))
			predicate(tuple.Element, tuple.Index);
	}

	public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
		=> enumerable.Where(x => !predicate(x));

	public static bool None<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
		=> !enumerable.Any(predicate);

	public static void Log<T>(this IEnumerable<T> list, string? separator = null, Func<T, string>? toString = null)
	{
		separator ??= "\n";
		var str = toString is null
			? string.Join(separator, list)
			: string.Join(separator, list.Select(toString));
		Console.WriteLine(str);
	}
}