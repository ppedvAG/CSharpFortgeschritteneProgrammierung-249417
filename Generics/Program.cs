namespace Generics;

internal class Program
{
	static void Main(string[] args)
	{
		//Generics
		//Platzhalter für einen Typen
		//Wird generell als T bezeichnet

		List<int> ints = []; //Alle T's werden durch int ersetzt
		ints.Add(1);

		List<string> str = [];
		str.Add("Hallo");

		//////////////////////////////////

		DataStore<int> ds = new(10);
		ds.Set(0, 123);
		int zahl = ds.Get(0);
	}

	public T Test<T>(T item)
	{
		T variable = item;
		if (item is int)
		{
			Console.WriteLine("T ist vom Typ Integer");
		}
		return variable;
	}

	public void Test2<T>()
	{
		//Keywords:
		//- default
		//- typeof
		//- nameof

		Console.WriteLine(default(T)); //Standardwert von T
		Console.WriteLine(typeof(T)); //Type Objekt aus T
		Console.WriteLine(nameof(T)); //Name des Typens als String
	}
}

/// <summary>
/// Durch T beim Klassennamen kann jetzt der Typ T innerhalb der Klasse verwendet werden
/// </summary>
public class DataStore<T>
{
	private T[] _data;

	public List<T> Data => _data.ToList();

	public DataStore(int size)
	{
		_data = new T[size];
	}

	public void Set(int index, T item)
	{
		_data[index] = item;
	}

	public T Get(int index)
	{
		return _data[index];
	}
}