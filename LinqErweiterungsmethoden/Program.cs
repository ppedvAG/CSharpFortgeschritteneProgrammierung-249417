using System.Diagnostics;

namespace LinqErweiterungsmethoden;

internal class Program
{
	static void Main(string[] args)
	{
		#region Listentheorie
		//IEnumerable
		//Interface, welches die Basis für alle Listentypen darstellt

		//Inhalt:
		//- GetEnumerator()
		//Wenn eine Klasse die GetEnumerator() Methode hat, ist es eine Listenklasse

		//IEnumerable ist nur eine Anleitung zur Erstellung von Daten
		IEnumerable<int> x = Enumerable.Range(0, (int) 1E9); //1ms, nur eine Anleitung

		//List<int> zahlen = x.ToList(); //800ms, Anleitung wird ausgeführt; 4GB RAM werden verwendet

		//Jede Linq-Funktion gibt immer ein IEnumerable zurück
		IEnumerable<int> filter = x.Where(e => e % 2 == 0); //Deferred Execution: Die Daten werden erst erzeugt, wenn sie verwendet werden

		////////////////////////////////////////////////////////////////////

		//IEnumerator
		//Komponente, welche eine Iteration der Liste ermöglicht

		//Inhalt:
		//- Current: Zeigt auf das jetztige Element und gibt dieses zurück
		//- MoveNext(): Bewegt den Zeiger auf das nächste Element
		//- Reset(): Setzt den Zeiger zurück an den Anfang

		//Der Enumerator wird bei der foreach-Schleife verwendet, um die Elemente anzugreifen
		List<int> bis100 = Enumerable.Range(0, 100).ToList();
		foreach (int z in bis100)
		{
			Console.WriteLine(z);
		}

		//Ohne Schleife
		IEnumerator<int> enumerator = bis100.GetEnumerator();
		enumerator.MoveNext();
	start:
		Console.WriteLine(enumerator.Current);
		bool hasNext = enumerator.MoveNext();
		if (hasNext)
			goto start;
		enumerator.Reset();
		#endregion

		#region Einfaches Linq
		List<int> zahlen = Enumerable.Range(1, 20).ToList();
		Console.WriteLine(zahlen.Average()); //Kann ohne Parameter definiert werden, oder mit einem Selektor
		Console.WriteLine(zahlen.Min());
		Console.WriteLine(zahlen.Max());
		Console.WriteLine(zahlen.Sum());

		Console.WriteLine(zahlen.First()); //Erstes Element, Exception wenn kein Element gefunden
		Console.WriteLine(zahlen.Last());

		Console.WriteLine(zahlen.FirstOrDefault()); //Erstes Element, default wenn kein Element gefunden
		Console.WriteLine(zahlen.LastOrDefault());

		zahlen.First(TeilbarDurch2); //Mit externer Func
		zahlen.First(e => e % 2 == 0); //Mit Lambda-Expression
		#endregion

		#region Linq mit Objekten
		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new Fahrzeug(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		//Fahrzeuge nach Marke, Geschwindigkeit sortieren
		fahrzeuge
			.OrderBy(x => x.Marke)
			.ThenBy(e => e.MaxV);

		fahrzeuge
			.OrderByDescending(x => x.Marke)
			.ThenByDescending(e => e.MaxV);

		//MinBy, MaxBy
		fahrzeuge.Min(e => e.MaxV); //Die Geschwindigkeit (int)
		fahrzeuge.MinBy(e => e.MaxV); //Das Fahrzeug mit der kleinsten Geschwindigkeit

		//Skip, Take
		//Überspringe X Elemente, nehme X Elemente

		//Was sind die 3 schnellsten Fahrzeuge?
		fahrzeuge.OrderByDescending(x => x.MaxV).Take(3);

		//Beispiel: Webshop
		int page = 1;
		fahrzeuge.Skip(page * 10).Take(10);

		//Select
		//Transformiert eine Liste

		//Beispiele:
		//Liste von 0 bis 10 mit 0.1 Schritten
		List<double> a = [];
		for (int i = 0; i < 100; i++)
			a.Add(i / 10.0);

		Enumerable.Range(0, 100).Select(durch10); //Nimm jeden Wert aus der Liste, wirf ihn in die gegebene Funktion, erzeuge aus den Rückgabewerten eine neue Liste
		Enumerable.Range(0, 100).Select(e => e / 10.0);

		double durch10(int e)
		{
			return e / 10.0;
		}

		//Ganze Liste zu einem String umwandeln
		Enumerable.Range(0, 10).Select(e => e.ToString());

		//Ganze Liste casten
		Enumerable.Range(0, 10).Select(e => (byte) e);

		//Einzelnes Feld aus einer Liste entnehmen
		fahrzeuge.Select(e => e.Marke);

		//SelectMany
		//Glättet eine Liste von Listen zu einer 1D-Liste herunter
		List<List<int>> ints = [[1, 2, 3], [4, 5, 6], [7, 8, 9]];
		ints.SelectMany(e => e);

		//Chunk
		//Teilt eine Liste in X große Teile auf
		fahrzeuge.Chunk(5);
		#endregion

		#region Erweiterungsmethoden
		//Methoden, welche an beliebige Objekte in C# angehängt werden können
		int zahl = 10;
		zahl.Quersumme();

		Console.WriteLine(13856719.Quersumme());
		#endregion
	}

	static bool TeilbarDurch2(int wert) => wert % 2 == 0;
}

[DebuggerDisplay("MaxV: {MaxV}, Marke: {Marke}")]
public class Fahrzeug
{
	public int MaxV { get; set; }

	public FahrzeugMarke Marke { get; set; }

	public Fahrzeug(int maxV, FahrzeugMarke marke)
	{
		MaxV = maxV;
		Marke = marke;
	}
}

public enum FahrzeugMarke { Audi, BMW, VW }

public static class ExtensionMethods
{
	public static int Quersumme(this int x) //this Parameter: Bezeichnet den Typen an den die Erweiterungsmethode angehängt wird
	{
		//int summe = 0;
		//string zahlAlsString = x.ToString();
		//for (int i = 0; i < zahlAlsString.Length; i++)
		//{
		//	summe += (int) char.GetNumericValue(zahlAlsString[i]);
		//}
		//return summe;

		return (int) x.ToString().Sum(char.GetNumericValue);
	}

	//public static IEnumerable<T> Flatten<T>(this IEnumerable<T> values)
	//{
	//	return values.SelectMany(e => e);
	//}
}