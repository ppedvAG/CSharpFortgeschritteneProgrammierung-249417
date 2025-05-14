using System.Reflection;

namespace Reflection;

internal class Program
{
	static void Main(string[] args)
	{
		//Reflection
		//Zur Laufzeit Ermittlung von allen möglichen Eigenschaften eines Objekts
		//Wird vorallem bei der Arbeit mit unbekannten Objekten verwendet

		//Type
		//Alles im Bereich Reflection geht von dem Type Objekt aus
		Program p = new Program();
		Type t1 = p.GetType(); //Über Objekt (direkt)

		Type t2 = typeof(Program); //Über Typname (indirekt)

		//Informationen erlangen über die Get... Methoden
		t1.GetProperties();

		t1.GetMethods();

		t1.GetFields();

		t1.GetConstructors();

		t1.GetEvents();

		/////////////////////////////////////////////////////

		t1.GetProperty("Text").SetValue(p, "Hallo");

		t1.GetMethod("Hallo").Invoke(p, null);

		//Aufgabe: Component aus DelegatesEvents per Reflection erstellen und benutzen

		//Assembly
		//"Projekt", bzw. eine fertige C# DLL
		Assembly a = Assembly.LoadFrom("C:\\Users\\lk3\\source\\repos\\CSharp_Fortgeschritten_2025_05_12\\DelegatesEvents\\bin\\Debug\\net9.0\\DelegatesEvents.dll");
		Type t = a.GetType("DelegatesEvents.Component"); //Typ Objekt aus dem Assembly entnehmen

		//Activator
		//Objekte erstellen per Methodenaufruf (ohne new)
		object comp = Activator.CreateInstance(t); //Component comp = new Component();

		t.GetEvent("Start").AddEventHandler(comp, () => Console.WriteLine("Prozess gestartet")); //comp.Start += ...
		t.GetEvent("End").AddEventHandler(comp, () => Console.WriteLine("Prozess beendet")); //comp.End += ...
		t.GetEvent("Progress").AddEventHandler(comp, (int i) => Console.WriteLine($"Fortschritt: {i}")); //comp.Progress += ...

		t.GetMethod("Run").Invoke(comp, null); //comp.Run()
	}

	public string Text { get; set; }

	public void Hallo()
	{
		Console.WriteLine("Hallo Welt");
	}
}
