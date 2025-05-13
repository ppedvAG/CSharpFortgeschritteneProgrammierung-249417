namespace Multitasking;

public class _04_TaskWarten
{
	static void Main(string[] args)
	{
		Task t1 = new Task(Run);
		t1.Start();

		Task t2 = Task.Run(Run);
		Task t3 = Task.Run(Run);

		t1.Wait(); //Warte bis t1 fertig ist (blockiert den Main Thread)

		Task.WaitAll(t1, t2, t3); //Warte auf mehrere Tasks (blockiert den Main Thread)

		int x = Task.WaitAny(t1, t2, t3); //Wartet auf einen der gegebenen Tasks (Main Thread läuft weiter, sobald ein Task fertig ist)
		//x: Welcher Task war zuerst fertig? (Index)
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Task: {i}");
		}
	}
}