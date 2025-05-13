namespace DelegatesEvents;

/// <summary>
/// Event
/// 
/// Statischer Punkt, welcher immer aus einem Delegate besteht (meistens EventHandler)
/// Benutzer können an diesen Punkt Methoden anhängen
/// Diese Methoden werden ausgeführt, wenn das Event gefeuert wird
/// 
/// Zweiteilige Programmierung:
/// - Entwicklerseite
/// - Anwenderseite
/// 
/// Entwicklerseite legt das Event an (event Keyword), führt das Event aus
/// Anwenderseite legt die Methode an, welche beim Event ausgeführt werden soll, und hängt diese an
/// </summary>
public class Events
{
	public event EventHandler TestEvent; //Definition vom Event (Entwicklerseite)

	public event EventHandler<FileSystemEventArgs> ArgsEvent;

	private event EventHandler accessorEvent;

	public event EventHandler AccessorEvent
	{
		//Event mit Accessoren
		//Kann Code vor/nach hinzufügen/abnehmen von Methoden ausführen
		add
		{
			accessorEvent += value;
			Console.WriteLine("Methode angehängt");
		}
		remove
		{
			accessorEvent -= value;
			Console.WriteLine("Methode abgehängt");
		}
	}

	static void Main(string[] args) => new Events().Start();

	public void Start()
	{
		TestEvent += Events_TestEvent; //Anhängen des Events (Anwenderseite)
		TestEvent?.Invoke(this, EventArgs.Empty); //Ausführen des Events (Entwicklerseite)

		///////////////////////////////////////////////

		ArgsEvent += Events_ArgsEvent;
		ArgsEvent?.Invoke(this, new FileSystemEventArgs(WatcherChangeTypes.Created, Environment.CurrentDirectory, "Test.txt")); //Diese Daten werden an die Methode weitergegeben

		///////////////////////////////////////////////

		AccessorEvent += Events_AccessorEvent;
		accessorEvent?.Invoke(this, EventArgs.Empty);
		AccessorEvent -= Events_AccessorEvent;
	}

	/// <summary>
	/// Definition der Methode (Anwenderseite)
	/// </summary>
	private void Events_TestEvent(object? sender, EventArgs e)
	{
		Console.WriteLine("Hallo Welt");
	}

	/// <summary>
	/// Die Daten die bei Invoke gesendet werden, werden in der Methode empfangen
	/// </summary>
	private void Events_ArgsEvent(object? sender, FileSystemEventArgs e)
	{
		Console.WriteLine(e.ChangeType);
		Console.WriteLine(e.FullPath);
		Console.WriteLine(e.Name);
	}

	private void Events_AccessorEvent(object? sender, EventArgs e)
	{
		Console.WriteLine("Event mit add/remove");
	}
}