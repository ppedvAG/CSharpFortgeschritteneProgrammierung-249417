namespace DelegatesEvents;

/// <summary>
/// Entwicklerseite
/// </summary>
public class Component
{
	public event Action Start;

	public event Action End;

	public event Action<int> Progress;

	/// <summary>
	/// Simuliert einen Prozess, der etwas länger andauert
	/// </summary>
	public void Run()
	{
		Start?.Invoke();

		for (int i = 0; i < 10; i++)
		{
			Thread.Sleep(200);
			Progress?.Invoke(i);
		}

		End?.Invoke();
	}
}