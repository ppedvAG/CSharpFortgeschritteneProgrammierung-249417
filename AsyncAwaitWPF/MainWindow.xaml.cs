using System.Net.Http;
using System.Windows;

namespace AsyncAwaitWPF;

public partial class MainWindow : Window
{
	public MainWindow() => InitializeComponent();

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		for (int i = 0; i < 50; i++)
		{
			Thread.Sleep(25);
			Output.Text += i + "\n";
			Scroll.ScrollToBottom();
		}
	}

	private void Button_Click_TaskRun(object sender, RoutedEventArgs e)
	{
		Task.Run(() =>
		{
			for (int i = 0; i < 50; i++)
			{
				Thread.Sleep(25);
				//Dispatcher: Ermöglicht, Code auf den Thread zu legen, in dem sich die Komponente befindet, die hinter dem Dispatcher steht
				Dispatcher.Invoke(() =>
				{
					Output.Text += i + "\n"; //Problem: Task stürzt ab, weil ein Task/Thread nicht auf den Main Thread greifen darf
					Scroll.ScrollToBottom(); //Lösung: Dispatcher
				});
			}
		});
	}

	private async void Button_Click_Async(object sender, RoutedEventArgs e)
	{
		for (int i = 0; i < 50; i++)
		{
			await Task.Delay(25);
			Output.Text += i + "\n";
			Scroll.ScrollToBottom();
		}
	}

	private async void Request(object sender, RoutedEventArgs e)
	{
		//Aufbau vom Asynchronem Code
		//1. Asynchrone Aufgabe (Task) starten
		//2. Zwischenschritte (User informieren, Buttons blockieren, ...)
		//3. Auf Ergebnis warten (await)

		//WebRequest
		//URL ansprechen, Content laden
		//Content auslesen, in der GUI anzeigen
		string url = "http://www.gutenberg.org/files/54700/54700-0.txt";

		using HttpClient client = new();

		//1. Task starten
		Task<HttpResponseMessage> req = client.GetAsync(url);

		//2. Zwischenschritte
		Output.Text = "Text wird geladen...";
		ReqButton.IsEnabled = false;

		//3. Warten
		HttpResponseMessage message = await req;

		///////////////////////////////

		if (message.IsSuccessStatusCode)
		{
			//1. Task starten
			Task<string> content = message.Content.ReadAsStringAsync();

			//2. Zwischenschritte
			Output.Text = "Text wird angezeigt...";

			//3. Warten
			//string text = await content;
			//Output.Text = text;
			Output.Text = await content;
		}
		else
			Output.Text = "Laden fehlgeschlagen";

		ReqButton.IsEnabled = true;
	}

	private async void Button_Click_AsyncDataSource(object sender, RoutedEventArgs e)
	{
		AsyncDataSource ads = new();
		await foreach(int x in ads.GeneriereZahlen()) //await foreach: Warte auf das nächste Element
		{
			Output.Text += $"{x}\n";
		}
	}
}