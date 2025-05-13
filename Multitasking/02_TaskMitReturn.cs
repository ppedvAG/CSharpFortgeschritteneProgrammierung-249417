namespace Multitasking;

class _02_TaskMitReturn
{
	static void Main(string[] args)
	{
		Task<int> t1 = new Task<int>(Calculate);
		t1.Start();

		bool hasPrinted = false;

		//Console.WriteLine(t1.Result); 
		//Mit Result kann auf das Resultat zugegriffen werden
		//Problem: Main Thread wird geblockt
		//Lösungen: ContinueWith, await

		for (int i = 0; i < 100; i++)
		{
			Thread.Sleep(25);
			Console.WriteLine($"Main Thread: {i}");

			if (t1.IsCompletedSuccessfully && !hasPrinted)
			{
				Console.WriteLine(t1.Result);
				hasPrinted = true;
			}
		}

		Console.ReadKey();
	}

	/// <summary>
	/// Berechnet etwas, und benötigt eine unbestimmte Zeit
	/// </summary>
	static int Calculate()
	{
		Thread.Sleep(Random.Shared.Next(500, 1000));
		return Random.Shared.Next();
	}
}
