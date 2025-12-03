namespace AdventOfCode25.Extensions;

public static class ArrayExtensions
{
	public static T[] WithoutIndex<T>(this T[] array, int index) => array.Where((_, i) => i != index).ToArray();

	public static bool IsInBounds<T>(this T[] array, int index) => index >= 0 && index < array.Length;

	public static int IndexOf<T>(this T[] array, T element) where T : notnull
		=> array.Index().FirstOrDefault(x => element.Equals(x.Item)).Index;
}