namespace Multitasking;

public class _05_CancellationToken
{
	static void Main(string[] args)
	{
		//CancellationToken
		//Standardweg, um Aufgaben vorzeitig abzubrechen
		//Wird an vielen Stellen in C# verwendet

		//Zwei Teile: Sender, Empfänger
		//Sender: CancellationTokenSource, produziert beliebig viele CT's
		//Über die Source wird allen Tokens das Cancel-Signal gesendet

		CancellationTokenSource cts = new();
		CancellationToken ct = cts.Token; //Token aus der Source entnehmen

		Task t1 = new Task(Run, ct);
		t1.Start();

		Thread.Sleep(500);
		cts.Cancel();

		Console.ReadKey();
	}

	static void Run(object o)
	{
		if (o is CancellationToken ct)
		{
			for (int i = 0; i < 100; i++)
			{
				//ct.ThrowIfCancellationRequested();

				if (ct.IsCancellationRequested)
					break;

				Thread.Sleep(25);
				Console.WriteLine($"Task: {i}");
			}
		}
	}
}
