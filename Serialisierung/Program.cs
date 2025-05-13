using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Serialisierung;

internal class Program
{
	static void Main(string[] args)
	{
		string folderPath = "Test";

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		string filePath = "Test.txt";

		string fullPath = Path.Combine(folderPath, filePath);

		/////////////////////////////////////////////////////////////////////

		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new LKW(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		//XML
		//1. Serialisierung/Deserialisierung
		XmlSerializer xml = new XmlSerializer(fahrzeuge.GetType());
		using (FileStream fs = new(fullPath, FileMode.Create))
		{
			xml.Serialize(fs, fahrzeuge);
		}

		using (FileStream fs = new(fullPath, FileMode.Open))
		{
			List<Fahrzeug> fzg = (List<Fahrzeug>) xml.Deserialize(fs);
		}

		//2. Attribute
		//XmlIgnore: Property ignorieren
		//XmlInclude: Vererbung beachten
		//XmlAttribute: Wandelt ein Property in die XML-Attribut Syntax um

		//3. Per Hand
		XmlDocument doc = new XmlDocument();
		using (FileStream fs = new(fullPath, FileMode.Open))
		{
			doc.Load(fs);
			foreach (XmlNode node in doc.DocumentElement)
			{
				int maxV = int.Parse(node.Attributes["MaxV"].InnerText);
				FahrzeugMarke marke = Enum.Parse<FahrzeugMarke>(node.Attributes["Marke"].InnerText);

				Console.WriteLine($"MaxV: {maxV}, Marke: {marke}");
			}
		}
	}

	static void SystemJson()
	{
		//string folderPath = "Test";

		//if (!Directory.Exists(folderPath))
		//	Directory.CreateDirectory(folderPath);

		//string filePath = "Test.txt";

		//string fullPath = Path.Combine(folderPath, filePath);

		///////////////////////////////////////////////////////////////////////

		//List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		//{
		//	new LKW(251, FahrzeugMarke.BMW),
		//	new Fahrzeug(274, FahrzeugMarke.BMW),
		//	new Fahrzeug(146, FahrzeugMarke.BMW),
		//	new Fahrzeug(208, FahrzeugMarke.Audi),
		//	new Fahrzeug(189, FahrzeugMarke.Audi),
		//	new Fahrzeug(133, FahrzeugMarke.VW),
		//	new Fahrzeug(253, FahrzeugMarke.VW),
		//	new Fahrzeug(304, FahrzeugMarke.BMW),
		//	new Fahrzeug(151, FahrzeugMarke.VW),
		//	new Fahrzeug(250, FahrzeugMarke.VW),
		//	new Fahrzeug(217, FahrzeugMarke.Audi),
		//	new Fahrzeug(125, FahrzeugMarke.Audi)
		//};

		////System Json
		////1. Serialisierung/Deserialisierung
		//string json = JsonSerializer.Serialize(fahrzeuge);
		//File.WriteAllText(fullPath, json);

		//string readJson = File.ReadAllText(fullPath);
		//Fahrzeug[] fzg = JsonSerializer.Deserialize<Fahrzeug[]>(readJson);

		////2. Settings
		//JsonSerializerOptions options = new();
		//options.WriteIndented = true;

		////WICHTIG: Bei Serialisierung/Deserialisierung die Options mitgeben
		//json = JsonSerializer.Serialize(fahrzeuge, options);
		//File.WriteAllText(fullPath, json);

		////3. Attribute
		////JsonIgnore: Ignoriert das Property
		////JsonExtensionData: Fängt alle Felder im Json auf, welche kein entsprechendes Property haben
		////JsonDerivedType: Vererbung (muss auf der Oberklasse implementiert werden, für JEDE Klasse in der Vererbungshierarchie)

		////4. Per Hand
		////Wenn das Json nur schwer in ein Objekt konvertiert werden kann
		//JsonDocument doc = JsonDocument.Parse(readJson);
		//foreach (JsonElement element in doc.RootElement.EnumerateArray()) //Aus dem RootElement alle Elemente in Form eines Arrays entnehmen
		//{
		//	//element: Das Json Objekt: { "$type": "L", "MaxV": 251, "Marke": 1 }
		//	//GetProperty: Greift auf ein Feld zu
		//	int maxV = element.GetProperty("MaxV").GetInt32();
		//	FahrzeugMarke marke = (FahrzeugMarke) element.GetProperty("Marke").GetInt32();
		//	string type = string.Empty;

		//	if (element.TryGetProperty("$type", out JsonElement prop))
		//	{
		//		type = prop.GetString();
		//	}

		//	Console.WriteLine($"MaxV: {maxV}, Marke: {marke}, Type: {type}");
		//}
	}

	static void NewtonsoftJson()
	{
		string folderPath = "Test";

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		string filePath = "Test.txt";

		string fullPath = Path.Combine(folderPath, filePath);

		/////////////////////////////////////////////////////////////////////

		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new LKW(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		//Newtonsoft Json
		//1. Serialisierung/Deserialisierung
		string json = JsonConvert.SerializeObject(fahrzeuge);
		File.WriteAllText(fullPath, json);

		string readJson = File.ReadAllText(fullPath);
		Fahrzeug[] fzg = JsonConvert.DeserializeObject<Fahrzeug[]>(readJson);

		//2. Settings
		JsonSerializerSettings settings = new();
		settings.Formatting = Newtonsoft.Json.Formatting.Indented;
		settings.TypeNameHandling = TypeNameHandling.Objects;

		//WICHTIG: Bei Serialisierung/Deserialisierung die Options mitgeben
		json = JsonConvert.SerializeObject(fahrzeuge, settings);
		File.WriteAllText(fullPath, json);

		readJson = File.ReadAllText(fullPath);
		fzg = JsonConvert.DeserializeObject<Fahrzeug[]>(readJson, settings);

		//3. Attribute
		//JsonIgnore: Ignoriert das Property
		//JsonExtensionData: Fängt alle Felder im Json auf, welche kein entsprechendes Property haben
		//Vererbung fällt weg; wird über das TypeNameHandling Setting gelöst

		//4. Per Hand
		//JToken als allgemeines Object
		//Wird für alles in Newtonsoft Json verwendet (gesamtes Json, Array, Element, Property, Value, Kommentar, ...)
		JToken doc = JToken.Parse(readJson);
		foreach (JToken element in doc)
		{
			int maxV = element["MaxV"].Value<int>();
			FahrzeugMarke marke = (FahrzeugMarke) element["Marke"].Value<int>();
			string type = element["$type"].Value<string>();

			Console.WriteLine($"MaxV: {maxV}, Marke: {marke}, Type: {type}");
		}
	}
}

[DebuggerDisplay("MaxV: {MaxV}, Marke: {Marke}")]

//[JsonDerivedType(typeof(Fahrzeug), "F")]
//[JsonDerivedType(typeof(LKW), "L")]

[XmlInclude(typeof(Fahrzeug))]
[XmlInclude(typeof(LKW))]
public class Fahrzeug
{
	[XmlAttribute]
	public int MaxV { get; set; }

	[XmlAttribute]
	public FahrzeugMarke Marke { get; set; }

	//[JsonExtensionData]
	//public Dictionary<string, object> additionalData { get; set; }

	public Fahrzeug(int MaxV, FahrzeugMarke Marke)
	{
		this.MaxV = MaxV;
		this.Marke = Marke;
	}

	public Fahrzeug()
	{
		
	}
}

public class LKW : Fahrzeug
{
	public LKW(int MaxV, FahrzeugMarke Marke) : base(MaxV, Marke)
	{
	}

	public LKW()
	{
		
	}
}

//public record Fahrzeug(int MaxV, FahrzeugMarke Marke);

public enum FahrzeugMarke
{
	Audi, BMW, VW
}