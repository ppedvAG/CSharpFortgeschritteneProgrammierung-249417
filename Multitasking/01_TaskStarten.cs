namespace Multitasking;

internal class _01_TaskStarten
{
	static void Main(string[] args)
	{
		Task t1 = new Task(Run);
		t1.Start();

		Task t2 = Task.Factory.StartNew(Run); //ab .NET 4.0

		Task t3 = Task.Run(Run); //ab .NET 4.5

		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Main Thread: {i}");
		}

		//Alle Tasks sind immer Hintergrundthreads
		//Hintergrundthreads werden abgebrochen, wenn alle Vordergrundthreads fertig sind
		//Der Main Thread ist ein Vordergrundthread
		//Um den Abbruch zu verhindern, muss der Main Thread geblockt werden
		Console.ReadKey();
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Task: {i}");
		}
	}
}
