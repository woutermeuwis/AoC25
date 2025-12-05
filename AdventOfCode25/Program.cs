var config = new
{
	Day = 5,
	Days = new[] { 1 },
	RunMultiple = false,
	RunExamples = true,
	RunInput = true,
	RunPartOne = true,
	RunPartTwo = true,
};

using var outerLogger = new LogHandle();
if (config.RunMultiple)
	config.Days.ForEach(RunDay);
else
	RunDay(config.Day);
outerLogger.Log("Finished running");


void RunDay(int day)
{
	var runner = Type.GetType($"AdventOfCode25.Day_{day:D2}.Solution") ?? throw new InvalidOperationException();
	var instance = Activator.CreateInstance(runner) as BaseSolution ?? throw new InvalidOperationException();

	if (config is { RunPartOne: true, RunExamples: true })
		Try(() => instance.SolveExampleOne(), $"Day {day} Part 1");
	if (config is { RunPartOne: true, RunInput: true })
		Try(() => instance.SolvePartOne(), $"Day {day} Part 1");
	if (config is { RunPartTwo: true, RunExamples: true })
		Try(() => instance.SolveExampleTwo(), $"Day {day} Part 2");
	if (config is { RunPartTwo: true, RunInput: true })
		Try(() => instance.SolvePartTwo(), $"Day {day} Part 2");
}

void Try(Action func, string desc)
{
	try
	{
		func();
	}
	catch (NotImplementedException)
	{
		Console.WriteLine(desc + " not implemented!");
	}
	catch (Exception e)
	{
		Console.WriteLine(e.Message);
		Console.WriteLine(e.ToString());
		Console.WriteLine(e.StackTrace);
	}
}