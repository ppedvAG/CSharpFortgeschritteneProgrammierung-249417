namespace Generics;

public class Constraints
{
	static void Main(string[] args)
	{

	}

	public static void Test<T>() where T : new()
	{
		T x = new T(); //Hier muss das new Constraint verwendet werden
	}
}

public class DataStore2<T> where T : Constraints; //Constraints selbst oder Unterklasse von Constraints

public class DataStore3<T> where T : ICloneable; //Interface

public class DataStore4<T> where T : class; //T muss ein Referenztyp sein

public class DataStore5<T> where T : struct; //T muss ein Wertetyp sein

public class DataStore6<T> where T : new(); //T muss einen leeren Konstruktor haben: public Person() { }

public class DataStore7<T> where T : Enum;

public class DataStore8<T> where T : Delegate;

public class DataStore9<T> where T : unmanaged; //Basisdatentyp/Pointer: int, double, bool, float, long, nint, nuint, ...

public class DataStore10<T> where T : notnull;

public class DataStore11<T1, T2>
	where T1 : class, new()
	where T2 : struct;