using System;

namespace GoldSplitter;
using System;

internal static class Program
{
	public static NamedAction[] actions =
	{
		new ("Split", package =>
		{
			Console.WriteLine("Split with how many?");
			int people = int.Parse(Console.ReadLine() ?? "0");
			Console.WriteLine(package.Split(people));
		}),
		new ("To All Copper", package =>
		{
			Console.WriteLine(package.ToAllCopper().Copper);
		})
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
public record NamedAction(string Name, Action<GoldPackage> Action);
public record GoldPackage(int Platinum, int Gold, int Silver, int Copper)
{
	public const int copperRepresentative = 1, silverRepresentative = 10, goldRepresentative = 100, platinumRepresentative = 1000;
	public static explicit operator int(GoldPackage package)
		=> package.ToAllCopper().Copper;
	public static GoldPackage HumanInput()
	{
		Console.Write("Platinum: ");
		int plat = int.Parse(Console.ReadLine() ?? "0");
		Console.Write("Gold: ");
		int gold = int.Parse(Console.ReadLine() ?? "0");
		Console.Write("Silver: ");
		int silver = int.Parse(Console.ReadLine() ?? "0");
		Console.Write("Copper: ");
		int copper = int.Parse(Console.ReadLine() ?? "0");
		return new GoldPackage(plat, gold, silver, copper);
	}

	public GoldPackage Split(int people)
	{
		int copper = ToAllCopper().Copper;
		copper /= people;
		return new GoldPackage(0, 0, 0, copper).DisperseToAppropriateCoinValues();
	}
	public GoldPackage ToAllCopper() => new(0, 0, 0, (Copper * copperRepresentative) 
		+ (Silver * silverRepresentative) + (Gold * goldRepresentative) 
		+ (Platinum * platinumRepresentative));
	public GoldPackage DisperseToAppropriateCoinValues()
	{
		int copperTotal = ToAllCopper().Copper;
		int platinumOut = copperTotal / platinumRepresentative;
		copperTotal -= platinumOut * platinumRepresentative;
		int goldOut = copperTotal / goldRepresentative;
		copperTotal -= goldOut * goldRepresentative;
		int silverOut = copperTotal / silverRepresentative;
		copperTotal -= silverOut * silverRepresentative;
		int copperOut = copperTotal;
		return new GoldPackage(platinumOut, goldOut, silverOut, copperOut);
	}
}