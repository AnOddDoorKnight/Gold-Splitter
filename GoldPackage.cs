using System;

public record GoldPackage(int Platinum, int Gold, int Silver, int Copper)
{
	public const int copperRepresentative = 1, silverRepresentative = 10, goldRepresentative = 100, platinumRepresentative = 1000;
	public static explicit operator int(GoldPackage package)
		=> package.ToAllCopper().Copper;
	public static GoldPackage operator +(GoldPackage packageLeft, GoldPackage packageRight)
		=> new GoldPackage(0, 0, 0, packageLeft.ToAllCopper().Copper + packageRight.ToAllCopper().Copper).DisperseToAppropriateCoinValues();
	public static GoldPackage operator +(GoldPackage packageLeft, int input)
		=> new GoldPackage(0, 0, 0, packageLeft.ToAllCopper().Copper + input).DisperseToAppropriateCoinValues();
	public static GoldPackage operator -(GoldPackage packageLeft, GoldPackage packageRight)
		=> new GoldPackage(0, 0, 0, packageLeft.ToAllCopper().Copper - packageRight.ToAllCopper().Copper).DisperseToAppropriateCoinValues();
	public static GoldPackage operator -(GoldPackage packageLeft, int input)
		=> new GoldPackage(0, 0, 0, packageLeft.ToAllCopper().Copper - input).DisperseToAppropriateCoinValues();
	public static GoldPackage operator /(GoldPackage package, int people)
		=> new GoldPackage(0, 0, 0, package.ToAllCopper().Copper / people).DisperseToAppropriateCoinValues();
	public static GoldPackage operator *(GoldPackage package, int times)
		=> new GoldPackage(package.Platinum * times, package.Gold * times, package.Silver * times, package.Copper * times).DisperseToAppropriateCoinValues();

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