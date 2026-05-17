namespace DiceGame.Core.Models;

// Karta wyników gracza — zapisane kategorie i punktacja sekcji
public class ScoreCard
{
    private static readonly IReadOnlySet<ScoreCategory> UpperSectionCategories = new HashSet<ScoreCategory>
    {
        ScoreCategory.Ones,
        ScoreCategory.Twos,
        ScoreCategory.Threes,
        ScoreCategory.Fours,
        ScoreCategory.Fives,
        ScoreCategory.Sixes
    };

    private readonly Dictionary<ScoreCategory, int> _scores = new();

    public IReadOnlyDictionary<ScoreCategory, int> Scores => _scores;

    public bool IsCategoryUsed(ScoreCategory category) => _scores.ContainsKey(category);

    public void RecordScore(ScoreCategory category, int points)
    {
        if (_scores.ContainsKey(category))
            throw new InvalidOperationException($"Category {category} already scored");
        if (points < 0)
            throw new ArgumentOutOfRangeException(nameof(points));

        _scores[category] = points;
    }

    public int UpperSectionTotal
        => _scores.Where(kv => UpperSectionCategories.Contains(kv.Key)).Sum(kv => kv.Value);

    public int LowerSectionTotal
        => _scores.Where(kv => !UpperSectionCategories.Contains(kv.Key)).Sum(kv => kv.Value);

    public bool HasUpperBonus => UpperSectionTotal >= GameConstants.UpperBonusThreshold;

    public int GrandTotal
        => UpperSectionTotal
           + (HasUpperBonus ? GameConstants.UpperBonusPoints : 0)
           + LowerSectionTotal;

    public bool IsComplete => _scores.Count == GameConstants.TotalCategories;
}
