namespace DelegatesEvents;

public class ActionFunc
{
	static void Main(string[] args)
	{
		//Action: Vorgegebener Delegate Typ, welcher bis zu 16 Parameter hat, und immer void zurückgibt
		Action<int, int> addiere = Addiere; //Methode mit void und 2 Parametern (Typ int)
		addiere?.Invoke(12, 23);

		//Beispiel: List.ForEach
		List<int> zahlen = Enumerable.Range(0, 20).ToList();

		//Mit separater Variable
		Action<int> a = PrintZahl;
		zahlen.ForEach(a);

		//Ohne separate Variable
		zahlen.ForEach(PrintZahl);

		/////////////////////////////////////////////////////////

		//Func: Vorgegebener Delegate Typ, welcher bis zu 16 Parameter hat, und einen beliebigen Wert zurückgibt
		//WICHTIG: Der letzte Generic ist der Rückgabetyp
		Func<int, int, double> f = Multipliziere;
		//double? d = f?.Invoke(3, 5); //double?: Nullable Double
		double d = f?.Invoke(3, 5) ?? double.NaN; //Wenn die Linke Seite einen Wert zurückgibt, verwende diesen, sonst NaN

		//Beispiel: List.Where
		//Mit separater Variable
		Func<int, bool> b = TeilbarDurch2;
		zahlen.Where(b);

		//Ohne separate Variable
		zahlen.Where(TeilbarDurch2);

		/////////////////////////////////////////////////////////

		//Anonyme Methoden
		//Methoden, welche nicht separat angelegt werden
		f += delegate (int x, int y) { return x + y; }; //Anonyme Methode

		f += (int x, int y) => { return x + y; }; //Kürzere Form

		f += (x, y) => { return x - y; };

		f += (x, y) => (double) x / y; //Kürzeste, häufigste Form

		//Delegates können immer über die Lambda-Schreibweise angegeben werden
		zahlen.ForEach(e => Console.WriteLine($"Die Zahl ist: {e}"));
		zahlen.Where(e => e % 2 == 0);

		zahlen.ForEach(Console.WriteLine); //Auch möglich, weil WriteLine genau passt
	}

	#region Action
	static void Addiere(int z1, int z2)
	{
		Console.WriteLine($"{z1} + {z2} = {z1 + z2}");
	}

	static void PrintZahl(int x)
	{
		Console.WriteLine($"Die Zahl ist: {x}");
	}
	#endregion

	#region	Func
	static double Multipliziere(int x, int y)
	{
		return x * y;
	}

	static bool TeilbarDurch2(int x)
	{
		return x % 2 == 0;
	}
	#endregion
}