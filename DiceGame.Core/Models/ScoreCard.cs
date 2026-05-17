namespace DiceGame.Core.Models;

// Karta wyników gracza — zapisane kategorie i punktacja sekcji
public class ScoreCard
{
    public IReadOnlyDictionary<ScoreCategory, int> Scores => throw new NotImplementedException();

    public bool IsCategoryUsed(ScoreCategory category) => throw new NotImplementedException();

    public void RecordScore(ScoreCategory category, int points) => throw new NotImplementedException();

    public int UpperSectionTotal => throw new NotImplementedException();

    public int LowerSectionTotal => throw new NotImplementedException();

    public bool HasUpperBonus => throw new NotImplementedException();

    public int GrandTotal => throw new NotImplementedException();

    public bool IsComplete => throw new NotImplementedException();
}
