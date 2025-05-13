namespace Multitasking;

/// <summary>
/// Wenn die Run Methode nicht gelockt wird, wird der Counter inkonsistent
/// -> unterschiedliche Zahlen an Orten, wo diese nicht sein sollen
/// </summary>
public class _08_Lock
{
	public static int Counter { get; set; }

	private static object LockObject = new();

	static void Main(string[] args)
	{
		List<Task> tasks = [];
		for (int i = 0; i < 50; i++)
			tasks.Add(new Task(Run));
		tasks.ForEach(x => x.Start());

		Console.ReadKey();
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
		{
			//Lock-Block: Hält Tasks auf, welche diesen Code ausführen wollen, bis der jetztige Task fertig ist
			lock (LockObject)
			{
				Counter++;
				Console.WriteLine(Counter);
			}

			//Monitor: Identischer Effekt zum Lock-Block (aber mit Methoden)
			Monitor.Enter(LockObject);
			Counter++;
			Console.WriteLine(Counter);
			Monitor.Exit(LockObject);

			//Interlocked: Integer-Operationen (++, --, +=, -=, ...) die automatisch gelockt werden
			//Interlocked.Add(ref Counter, 1);
		}
	}
}