namespace DelegatesEvents;

/// <summary>
/// Anwenderseite
/// </summary>
public class User
{
	static void Main(string[] args)
	{
		Component comp = new();
		comp.Start += Comp_Start;
		comp.End += Comp_End;
		comp.Progress += Comp_Progress;
		comp.Run();
	}

	private static void Comp_Start()
	{
		Console.WriteLine("Prozess gestartet");
	}

	private static void Comp_End()
	{
		Console.WriteLine("Prozess beendet");
	}

	private static void Comp_Progress(int obj)
	{
		Console.WriteLine($"Fortschritt: {obj}");
	}
}