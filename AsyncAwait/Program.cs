using System.Diagnostics;

namespace AsyncAwait;

internal class Program
{
	static async Task Main(string[] args)
	{
		//Async/Await
		//Bessere Kontrolle über den Flow der asynchronen Operationen
		//Vergleichbar mit t.Wait() und ContinueWith()

		///////////////////////////////////////////////////////////

		#region Frühstück Synchron
		//Stopwatch sw = Stopwatch.StartNew();
		//Toast();
		//Tasse();
		//Kaffee();
		//Console.WriteLine(sw.ElapsedMilliseconds); //7s
		#endregion

		///////////////////////////////////////////////////////////

		#region	Frühstück mit Tasks
		//Stopwatch sw = Stopwatch.StartNew();
		//bool hasPrinted = false;

		//Task t1 = new Task(Toast);
		//t1.ContinueWith(x => //Problem: In der Realität sind Zeiten generell nicht bekannt
		//{
		//	if (hasPrinted)
		//		return;
		//	Console.WriteLine(sw.ElapsedMilliseconds); //Problem: Es wird nicht auf t2 gewartet
		//	hasPrinted = true;
		//});
		//t1.Start();

		//Task t2 = new Task(Tasse);
		//t2.ContinueWith(x => Kaffee())
		//	.ContinueWith(x =>
		//	{
		//		if (hasPrinted)
		//			return;
		//		Console.WriteLine(sw.ElapsedMilliseconds); //Problem: Es wird nicht auf t1 gewartet
		//		hasPrinted = true;
		//	});
		//t2.Start();

		////Task.WaitAll(t1, t2); //Blockiert
		////Console.WriteLine(sw.ElapsedMilliseconds); //Ohne WaitAll() wird der Timer sofort beendet (Lösung: ContinueWith auf t1)
		#endregion

		///////////////////////////////////////////////////////////

		//async/await
		//async/await blockiert nicht

		//async
		//Modifier für eine Methode
		//Besagt, das diese Methode immer asynchron ausgeführt wird
		//WICHTIG: Wenn eine async Methode ausgeführt wird, wird sie als Task ausgeführt

		#region Methodenaufruf als Task
		//Stopwatch sw = Stopwatch.StartNew();
		//ToastAsync();
		//Console.WriteLine(sw.ElapsedMilliseconds); //0s, weil ToastAsync() als Task gestartet wird
		//Console.ReadKey();
		#endregion

		//Aufbauten von async Methoden
		//async void: Kann await enthalten, kann selbst NICHT awaited werden
		//async Task: Kann await enthalten, kann selbst awaited werden
		//async Task<T>: Kann await enthalten, kann selbst awaited werden und hat einen Rückgabewert

		//await
		//Effektiv t.Wait(), aber ohne Blockade
		//Generiert im Hintergrund eine große Menge Code, um dies zu gewährleisten
		//Wenn ein Task einen Rückgabewert hat, kann bei await dieser Wert in eine Variable gespeichert werden

		#region Frühstück mit Async/Await
		//Stopwatch sw = Stopwatch.StartNew();
		//Task t1 = ToastAsync(); //Starte den Toast
		//Task t2 = TasseAsync(); //Starte die Tasse
		//await t2; //Warte auf die Tasse
		//Task t3 = KaffeeAsync(); //Starte den Kaffee
		//await t3; //Warte auf den Kaffee
		//await t1; //Warte auf den Toast
		//Console.WriteLine(sw.ElapsedMilliseconds);

		//Optimierungen
		//Stopwatch sw = Stopwatch.StartNew();
		//Task t1 = ToastAsync();
		//await TasseAsync(); //Starten und Warten in einem Schritt
		////await KaffeeAsync(); //Starten und Warten in einem Schritt
		////await t1;
		//await Task.WhenAll(KaffeeAsync(), t1); //WhenAll(): Effektiv WaitAll(), aber async
		//Console.WriteLine(sw.ElapsedMilliseconds);
		#endregion

		//Aufbau vom Asynchronem Code
		//1. Asynchrone Aufgabe (Task) starten
		//2. Zwischenschritte (User informieren, Buttons blockieren, ...)
		//3. Auf Ergebnis warten (await)

		#region Frühstück mit Objekten
		Stopwatch sw = Stopwatch.StartNew();
		Task<Toast> t1 = ToastObjectAsync();
		Task<Tasse> t2 = TasseObjectAsync();
		Tasse tasse = await t2;
		Task<Kaffee> t3 = KaffeeObjectAsync(tasse);
		Kaffee kaffee = await t3;
		Toast toast = await t1;
		Frühstück f = new Frühstück(toast, kaffee);
		Console.WriteLine(sw.ElapsedMilliseconds);
		#endregion

		Console.ReadKey();
	}

	#region Synchron
	public static void Toast()
	{
		Thread.Sleep(4000);
		Console.WriteLine("Toast fertig");
	}

	public static void Tasse()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Tasse fertig");
	}

	public static void Kaffee()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Kaffee fertig");
	}
	#endregion

	#region Asynchron
	public static async Task ToastAsync()
	{
		await Task.Delay(4000); //Äquivalent zu Thread.Sleep
		Console.WriteLine("Toast fertig");
		//Kein return
	}

	public static async Task TasseAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Tasse fertig");
	}

	public static async Task KaffeeAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
	}
	#endregion

	#region Async mit Objekten
	public static async Task<Toast> ToastObjectAsync()
	{
		await Task.Delay(4000); //Äquivalent zu Thread.Sleep
		Console.WriteLine("Toast fertig");
		return new Toast();
	}

	public static async Task<Tasse> TasseObjectAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Tasse fertig");
		return new Tasse();
	}

	public static async Task<Kaffee> KaffeeObjectAsync(Tasse t)
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
		return new Kaffee(t);
	}
	#endregion
}

public class Toast;

public class Tasse;

public class Kaffee(Tasse t);

public class Frühstück(Toast t, Kaffee f);