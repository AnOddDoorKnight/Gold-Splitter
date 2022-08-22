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
		new ("Stabilize, Takes some values higher than ten and puts them as their higher counterparts.", package =>
		{
			Console.WriteLine(package.DisperseToAppropriateCoinValues());
		}),
	};
	static void Main()
	{
		PrintTitle();
		Console.WriteLine("[1] Use raw copper values, [2] Use a 'package'");
		GoldPackage gold;
		switch (int.Parse(Console.ReadLine() ?? "1"))
		{
			case 1:
				Console.Write("Copper: ");
				gold = new GoldPackage(0, 0, 0, int.Parse(Console.ReadLine() ?? "0"));
				break;
			case 2:
				gold = GoldPackage.HumanInput();
				break;
			default:
				throw new IndexOutOfRangeException();
		}
		Console.WriteLine("Choose an action:");
		for (int i = 0; i < actions.Length; i++)
			Console.WriteLine($"{i + 1}. {actions[i].Name}");
		actions[int.Parse(Console.ReadLine() ?? "0") - 1].Action.Invoke(gold);
		Console.ReadLine();
	}
	static void PrintTitle()
	{
		Console.Title = "Gold Splitter : https://github.com/AnOddDoorKnight/Gold-Splitter";
	}
}
public record NamedAction<T>(string Name, Action<T> Action);