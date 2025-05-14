namespace AsyncAwaitWPF;

public class AsyncDataSource
{
	/// <summary>
	/// IAsyncEnumerable:
	/// Listentyp, welcher eine unbestimmte Menge an Daten herausgibt
	/// Beispiel: Livestream
	///
	/// Der Server wirft Bilder zu uns
	/// Wir zeigen diese Bilder an
	/// Solange wir mit dem Server verbunden sind, läuft der Stream weiter
	/// </summary>
	public async IAsyncEnumerable<int> GeneriereZahlen()
	{
		//yield return: Returne einen Wert (Funktion wird nicht beendet)
		//Wird generell in einer Schleife verwendet
		while (true)
		{
			await Task.Delay(Random.Shared.Next(500, 5000));
			yield return Random.Shared.Next();
		}
	}
}