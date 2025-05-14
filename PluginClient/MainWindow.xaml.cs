using PluginBase;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace PluginClient;

public partial class MainWindow : Window
{
	public ObservableCollection<MethodInfo> Methods { get; } = [];

	public IPlugin Plugin { get; set; }

	public MainWindow() => InitializeComponent();

	/// <summary>
	/// DLL laden
	/// User die Methoden präsentieren
	/// </summary>
	private void LoadPlugin(object sender, RoutedEventArgs e)
	{
		Assembly a = Assembly.LoadFrom("C:\\Users\\lk3\\source\\repos\\CSharp_Fortgeschritten_2025_05_12\\PluginCalculator\\bin\\Debug\\net9.0\\PluginCalculator.dll");

		Type plugin = a.GetTypes().First(e => e.GetInterface(nameof(IPlugin)) != null); //Suche nach dem Typen, der das IPlugin Interface hat

		//Problem: Jedes Plugin ist ein Object
		//Lösung: Gemeinsame PluginBase
		//Projekt PluginBase bei Plugins + Client als Dependency hinzufügen
		Plugin = (IPlugin) Activator.CreateInstance(plugin);

		TB_Name.Text = Plugin.Name;
		TB_Desc.Text = Plugin.Description;
		TB_Version.Text = Plugin.Version;
		TB_Author.Text = Plugin.Author;

		foreach (MethodInfo mi in plugin.GetMethods().Where(e => e.GetCustomAttribute<ReflectionVisible>() != null))
			Methods.Add(mi);
	}

	private void ExecuteMethod(object sender, RoutedEventArgs e)
	{
		Button b = (Button) sender;
		MethodInfo m = (MethodInfo) b.DataContext;
		m.Invoke(Plugin, null);
	}
}