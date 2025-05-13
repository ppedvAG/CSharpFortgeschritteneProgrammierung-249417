using System.Collections.Concurrent;

namespace Multitasking;

public class _09_ConcurrentCollections
{
	static void Main(string[] args)
	{
		ConcurrentDictionary<string, int> dict = [];
		
		if (!dict.TryAdd("Eins", 1))
		{
			//...
		}

		dict.GetOrAdd("Eins", 1);

		dict.AddOrUpdate("Eins", 1, (k, v) => v);

		///////////////////////////////////////////////

		//SynchronizedCollection: Äquivalent zur List<T>
		//Paket installieren: System.ServiceModel.Primitives
		SynchronizedCollection<int> list = [];
		list.Add(1);
		list.Add(2);
		list.Add(3);
		Console.WriteLine(list[0]);
		Console.WriteLine(list.Where(e => e % 2 == 0));
	}
}
