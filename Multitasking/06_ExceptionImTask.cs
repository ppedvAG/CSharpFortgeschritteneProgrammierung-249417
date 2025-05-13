namespace Multitasking;

public class _06_ExceptionImTask
{
	static void Main(string[] args)
	{
		//Exception Handling in Tasks:
		//- try-catch
		//- ContinueWith
		//- AggregateException
		Task<int> t1 = new Task<int>(Run);
		t1.Start();

		try
		{
			t1.Wait();

			Console.WriteLine(t1.Result);

			Task.WaitAll(t1);
		}
		catch (AggregateException ex)
		{
			foreach(Exception e in ex.InnerExceptions)
			{
				Console.WriteLine(e.Message);
			}
		}

		Console.ReadKey();
	}

	static int Run()
	{
		throw new Exception();
	}
}