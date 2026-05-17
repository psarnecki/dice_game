using DiceGame.Core.Interfaces;
using DiceGame.Core.Models;

namespace DiceGame.Core.Services;

// Dispatch do strategii
public class ScoringService
{
    private readonly IReadOnlyDictionary<ScoreCategory, IScoringStrategy> _strategiesByCategory;

    public ScoringService(IEnumerable<IScoringStrategy> strategies)
    {
        ArgumentNullException.ThrowIfNull(strategies);

        // Tworzymy słownik strategii, dla powtórzonej kategorii system zgłosi błąd przy starcie
        var map = strategies.ToDictionary(s => s.Category);

        // Sprawdzamy czy wszystkie 13 kategorii ma przypisane swoje strategie, aby uniknąć błędów w trakcie rozgrywki
        var missing = Enum.GetValues<ScoreCategory>().Except(map.Keys).ToList();
        if (missing.Count > 0)
        {
            throw new InvalidOperationException(
                $"Brak strategii dla kategorii: {string.Join(", ", missing)}");
        }

        _strategiesByCategory = map;
    }

    public int CalculateScore(ScoreCategory category, IReadOnlyList<Die> dice)
    {
        ArgumentNullException.ThrowIfNull(dice);
        return _strategiesByCategory[category].Calculate(dice);
    }

    public IReadOnlyDictionary<ScoreCategory, int> GetAllPossibleScores(IReadOnlyList<Die> dice)
    {
        ArgumentNullException.ThrowIfNull(dice);
        return _strategiesByCategory.ToDictionary(
            kv => kv.Key,
            kv => kv.Value.Calculate(dice));
    }

    public bool IsUpperBonusEarned(ScoreCard card)
    {
        ArgumentNullException.ThrowIfNull(card);
        return card.UpperSectionTotal >= GameConstants.UpperBonusThreshold;
    }
}
