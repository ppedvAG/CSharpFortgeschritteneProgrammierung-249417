namespace Multitasking;

public class _03_TaskMitParameter
{
	static void Main(string[] args)
	{
		Task t1 = new Task(Run, 20);
		t1.Start();

		Console.ReadKey();
	}

	static void Run(object? o)
	{
		if (o is int x)
		{
			for (int i = 0; i < x; i++)
				Console.WriteLine($"Task: {i}");
		}
	}
}