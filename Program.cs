namespace GoldSplitter;
using System;

internal static class Program
{
	public static NamedAction<GoldPackage>[] actions =
	{
		new ("Split", package =>
		{
			Console.WriteLine("Split with how many?");
			int people = int.Parse(Console.ReadLine() ?? "0");
			Console.WriteLine(package / people);
		}),
		new ("To All Copper", package =>
		{
			Console.WriteLine(package.ToAllCopper().Copper);
		}),
		new ("Multiply", package =>
		{
			Console.WriteLine("Multiply how many times?");
			int times = int.Parse(Console.ReadLine() ?? "0");
			Console.WriteLine(package * times);
		}),
		new ("Add", package =>
		{
			Console.WriteLine("[1] Add with another package, [2] to add just copper.");
			switch (int.Parse(Console.ReadLine() ?? "1"))
			{
				case 1:
					Console.WriteLine(package + GoldPackage.HumanInput());
					break;
				case 2:
					Console.Write("Copper: ");
					Console.WriteLine(package + int.Parse(Console.ReadLine() ?? "0"));
					break;
				default:
					throw new InvalidOperationException();
			}
		}),
		new ("Remove", package =>
		{
			Console.WriteLine("[1] Remove with another package, [2] to remove just copper.");
			switch (int.Parse(Console.ReadLine() ?? "1"))
			{
				case 1:
					Console.WriteLine(package - GoldPackage.HumanInput());
					break;
				case 2:
					Console.Write("Copper: ");
					Console.WriteLine(package - int.Parse(Console.ReadLine() ?? "0"));
					break;
				default:
					throw new InvalidOperationException();
			}
		}),
	};
	static void Main()
	{
		GoldPackage gold = GoldPackage.HumanInput();
		Console.WriteLine("Choose an action:");
		for (int i = 0; i < actions.Length; i++)
			Console.WriteLine($"{i + 1}. {actions[i].Name}");
		actions[int.Parse(Console.ReadLine() ?? "0") - 1].Action.Invoke(gold);
		Console.ReadLine();
	}
}
public record NamedAction<T>(string Name, Action<T> Action);