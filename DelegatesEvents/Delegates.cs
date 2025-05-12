namespace DelegatesEvents;

internal class Delegates
{
	public delegate void Vorstellung(string name); //Definition eines Delegates

	static void Main(string[] args)
	{
		//Delegates
		//Behälter für Methodenzeiger
		//Jedes Delegate gibt eine Struktur vor, jeder Methodenzeiger muss diese Struktur haben
		Vorstellung v = new Vorstellung(VorstellungDE); //Erstellung eines Delegates mit Initialmethode

		v("Max"); //Ausführung eines Delegates

		//Verwendung: Delegate-Parameter
		//Idee: Code schreiben, bei dem der Benutzer, selbst Code hinzufügen kann
		//Beispiel: Task Klasse; nimmt als Parameter eine Action
		//-> Action ist ein Delegate
		//-> Delegate ist ein Funktionszeiger
		//-> Task nimmt einen Funktionszeiger
		//Über den Funktionszeiger kann dem Task gesagt werden, welchen Code er ausführen soll

		//Mit += kann eine Methode an ein Delegate angehängt werden
		v += VorstellungEN;
		v("Tim"); //Zwei Outputs

		//Mit -= kann eine Methode von einem Delegate abgehängt werden
		v -= VorstellungDE;
		v("Udo");

		v -= VorstellungEN;
		//v("Max"); //Achtung: Wenn von einem Delegate alle Methoden abgenommen werden, ist es null

		if (v is not null)
			v("Max");

		v?.Invoke("Max"); //Null propagation: Führt den Code nach dem Fragezeichen nur aus, wenn die Variable nicht null ist

		//Alle Methoden eines Delegates anschauen
		foreach (Delegate dg in v.GetInvocationList())
		{
			Console.WriteLine(dg.Method.Name);
		}
	}

	static void VorstellungDE(string n)
	{
		Console.WriteLine($"Hallo mein Name ist {n}");
	}

	static void VorstellungEN(string n)
	{
		Console.WriteLine($"Hello my name is {n}");
	}
}
