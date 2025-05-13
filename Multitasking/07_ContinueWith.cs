namespace Multitasking;

public class _07_ContinueWith
{
	static void Main(string[] args)
	{
		//ContinueWith
		//Tasks verketten
		//t1 -> t2 -> t3 -> t4 -> ...
		//   -> t2.1
		//   -> t2.2 -> t3.1

		//Aufgabe: Wenn Run1 fertig ist, soll Run2 gestartet werden
		Task t1 = new Task(Run1);

		//Wait() wäre möglich, blockiert den Main Thread
		//Alternativen: ContinueWith, await (später)

		t1.ContinueWith(vorherigerTask => Run2());

		//ContinueWith gibt IMMER Zugriff auf den vorherigen Task
		//ContinueWith startet die Methode im Body der Lambda-Expression immer als Task -> kein Task.Run/new Task notwendig

		t1.Start();

		//while (true)
		//{
		//	Console.WriteLine("-");
		//	Thread.Sleep(50);
		//}

		/////////////////////////////////////////////////////////////
		
		//Aufgabe: Ergebnis soll mittendrin erscheinen (wenn es fertig ist)
		Task<int> t2 = new Task<int>(Calculate);
		t2.ContinueWith(v => Console.WriteLine(v.Result));
		t2.Start();

		for (int i = 0; i < 100; i++)
		{
			Thread.Sleep(20);
			Console.WriteLine($"Main Thread: {i}");
		}

		/////////////////////////////////////////////////////////////

		//TaskContinuationOptions
		//Starten Folgetasks nur dann, wenn eine bestimmte Bedingung gegeben ist
		Task t3 = new Task(VielleichtFehler);
		t3.ContinueWith(v => Console.WriteLine("Erfolg"), TaskContinuationOptions.OnlyOnRanToCompletion);
		t3.ContinueWith(v => Console.WriteLine(v.Exception.Message), TaskContinuationOptions.OnlyOnFaulted);
		t3.Start();

		Console.ReadKey();
	}

	static void Run1()
	{
		//Thread.Sleep(Random.Shared.Next(500, 2000));
		Console.WriteLine("Run1 fertig");
	}

	static void Run2()
	{
		//Thread.Sleep(200);
		Console.WriteLine("Run2 fertig");
	}

	static int Calculate()
	{
		Thread.Sleep(Random.Shared.Next(500, 1000));
		return Random.Shared.Next();
	}

	static void VielleichtFehler()
	{
		if (Random.Shared.Next() % 2 == 0)
			throw new Exception("50% Chance getroffen");
	}
}
