using IntList = System.Collections.Generic.List<int>;

namespace Sprachfeatures;

internal class Program
{
	static void Main(string[] args)
	{
		Lebewesen l = new Mensch();
		//Genauer Typvergleich
		//Typ von l muss genau Lebewesen sein
		//Wichtig bei Vererbung
		if (l.GetType() == typeof(Lebewesen))
		{
			//false
		}

		if (l.GetType() == typeof(Mensch))
		{
			//true
		}

		//Typvergleich mit Rücksicht auf Vererbung
		//Auch true, wenn die Variable ein Untertyp von dem gegebenen Typ ist
		//Wichtig bei Interfaces
		if (l is Lebewesen)
		{
			//true
		}

		if (l is Mensch)
		{
			//true
		}

		//Typswitch
		switch (l)
		{
			case Mensch:
				break;
			case Hund:
				break;
			case Lebewesen:
				break;
		}

		//Tupel
		//Liste von BENANNTEN Elementen
		(string, int, bool) x = ("Hallo", 123, true);
		Console.WriteLine(x.Item1);
		Console.WriteLine(x.Item2);
		Console.WriteLine(x.Item3);

		//(string x, int y, bool z) = ("Hallo", 123, true);
		//Console.WriteLine(x);
		//Console.WriteLine(y);
		//Console.WriteLine(z);

		var (name, alter) = new Hund();

		//Zwei verschiedene Arten von Typen
		//class und struct

		//class
		//Referenztyp (reference type)
		//Eigenschaft 1: Wenn ein Objekt eines Referenztypens zugewiesen wird (Variable), wird eine Referenz erzeugt
		Hund h1 = new Hund();
		Hund h2 = h1; //Hier wird eine Referenz auf das Objekt unter h1 angelegt
		h2.Alter = 10; //Alter wird bei dem einen Objekt verändert

		//Eigenschaft 2: Wenn zwei Objekte einen Referenztypens verglichen werden (==), werden die Speicheradressen verglichen
		Console.WriteLine(h1 == h2);
		Console.WriteLine(h1.GetHashCode() == h2.GetHashCode());
		Console.WriteLine(h1.GetHashCode());
		Console.WriteLine(h2.GetHashCode());

		//struct
		//Wertetyp (value type)
		//Eigenschaft 1: Wenn ein Objekt eines Wertetypens zugewiesen wird (Variable), wird eine Kopie erzeugt
		int i1 = 10;
		int i2 = i1; //Kopie von i1 wird erzeugt
		i2 = 20; //i1 bleibt unverändert

		//Eigenschaft 2: Wenn zwei Objekte einen Wertetypens verglichen werden (==), werden die Inhalte verglichen
		Console.WriteLine(i1 == i2); //10 == 20

		//ref
		//Macht eine Variable/Parameter/Rückgabewert referenzierbar
		float f1 = 10;
		ref float f2 = ref f1; //Lege eine Referenz auf f1
		f2 = 20; //Beide Variablen werden verändert

		int a = 10;
		/*static*/ void Test() //Mit static kein Zugriff auf a möglich
		{
			Console.WriteLine(a);
		}

		int[] zahlen = Enumerable.Range(0, 100).ToArray();
		Console.WriteLine(zahlen[5..15]);

		Range r = new Range(5, 15);
		Console.WriteLine(zahlen[r]);

		Index last = new Index(1, true); //^1
		Console.WriteLine(zahlen[last]);

		///////////////////////////////////////////////

		string str = "Hallo";
	
		//Ohne Operator
		if (str is not null)
			Console.WriteLine(str);
		else
			Console.WriteLine("String ist leer");

		//?-Operator
		Console.WriteLine(str != null ? str : "String ist leer");

		//??-Operator
		//Nimm die Linke Seite, wenn diese nicht null ist, sonst die rechte Seite
		Console.WriteLine(str ?? "String ist leer");

		List<int> z = null;
		if (z is null)
			z = new List<int>();

		z ??= new List<int>();

		//Verbatim-String (@-String): String, welcher Escape-Sequenzen ignoriert
		string pfad = @"C:\Program Files\dotnet\shared\Microsoft.NETCore.App\9.0.2\System.Security.Claims.dll";

#nullable enable

#nullable disable

		int temp = 4;
		switch (temp)
		{
			case > 0 and < 5:
				Console.WriteLine("Kalt");
				break;
			case > 5 and < 10:
				Console.WriteLine("Frisch");
				break;
		}

		Person p = new(0, "Max");
		//p.ID = 1;
		Console.WriteLine(p);

		////////////////////////////

		string interpolation = $$"""Hallo {} {{p}} "Welt""";

		List<int> nummern = Enumerable.Range(0, 10).ToList();
		List<int> nummern2 = [.. Enumerable.Range(0, 10)];

		IntList list = [];

		Person2 p2 = new Person2(10, "Tim");

		DateTime dt1 = DateTime.Now;
		DateTime dt2 = new DateTime(2000, 1, 1);
		Console.WriteLine(dt1 + TimeSpan.FromMinutes(5));

		Console.WriteLine(dt1 > dt2);

		int i = 10;
		double d = i;

		/////////////////////////////////////

		//string str1 = "";
		//for (int j = 0; j < 1_000_000; j++)
		//{
		//	str1 += $"{j}"; //ACHTUNG: Wenn ein String mit += verknüpft wird, muss eine Kopie erzeugt werden
		//}
		//Console.WriteLine(sw.ElapsedMilliseconds);

		StringBuilder sb = new();
		Stopwatch sw = Stopwatch.StartNew();
		for (int k = 0; k < 1_000_000; k++)
		{
			sb.Append(k); //Speichert die Elemente, welche zusammengehängt werden sollen
		}
		string result = sb.ToString(); //Hängt alle Elemente einmalig zusammen
		Console.WriteLine(sw.ElapsedMilliseconds);
	}

	public string Wochentag() => DateTime.Now switch
	{
		{ DayOfWeek: DayOfWeek.Monday } => "Montag",
		{ DayOfWeek: DayOfWeek.Friday, Day: 13 } => "Freitag der 13.",
		{ DayOfWeek: DayOfWeek.Tuesday } => "Dienstag",
		_ => "Anderer Tag",
	};
}


public class Lebewesen;

public class Mensch : Lebewesen;

public class Hund : Lebewesen
{
	public string Name { get; set; }

	public int Alter { get; set; }

	public void Deconstruct(out string name, out int alter)
	{
		name = Name;
		alter = Alter;
	}
}


public class AccessModifier
{
	public string Vorname { get; set; } //public: Kann von überall angegriffen werden

	private string Nachname { get; set; } //private: Kann nur innerhalb der Klasse verwendet werden

	internal int Alter { get; set; } //internal: Kann von überall angegriffen werden, aber nur innerhalb vom Projekt

	/////////////////////////////////////////////////////////

	protected double Groesse { get; set; } //protected: Wie private, aber auch in Unterklassen

	protected private int Gehalt { get; set; } //protected private: Wie private, aber auch in Unterklassen, ABER nur innerhalb vom Projekt

	protected internal string Adresse { get; set; } //protected internal: Wie internal, aber auch in Unterklassen außerhalb vom Projekt
}


//public class Person
//{
//	public int ID { get; init; }

//	public string Name { get; init; }

//	public Person(int iD, string name)
//	{
//		ID = iD;
//		Name = name;
//	}
//}

public record Person(int ID, string Name)
{
	public void Test()
	{
		//...
	}
}

public class Person2(int id, string name)
{
	public int ID => id;

	public string Name => name;
}