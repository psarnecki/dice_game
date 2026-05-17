namespace DiceGame.Core;

// Stałe domenowe gry — brak magicznych liczb
public static class GameConstants
{
    public const int DiceCount = 5;
    public const int MaxRollsPerTurn = 3;
    public const int MinPlayers = 2;
    public const int MaxPlayers = 4;
    public const int DieMinValue = 1;
    public const int DieMaxValue = 6;
    public const int UpperBonusThreshold = 63;
    public const int UpperBonusPoints = 35;
    public const int FullPoints = 25;
    public const int SmallStraightPoints = 30;
    public const int LargeStraightPoints = 40;
    public const int YahtzeePoints = 50;
    public const int TotalCategories = 13;
}
