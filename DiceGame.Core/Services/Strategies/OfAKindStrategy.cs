using DiceGame.Core.Interfaces;
using DiceGame.Core.Models;

namespace DiceGame.Core.Services.Strategies;

// Wspólna baza dla 3 jednakowe i 4 jednakowe, sprawdza minimum N powtórzeń i sumuje cały rzut
public abstract class OfAKindStrategy : IScoringStrategy
{
    private readonly int _minCount;

    protected OfAKindStrategy(int minCount)
    {
        if (minCount < 1) throw new ArgumentOutOfRangeException(nameof(minCount));
        _minCount = minCount;
    }

    public abstract ScoreCategory Category { get; }

    public int Calculate(IReadOnlyList<Die> dice)
        => IsConditionMet(dice) ? dice.Sum(d => d.Value) : 0;

    public bool IsConditionMet(IReadOnlyList<Die> dice)
        => dice.GroupBy(d => d.Value).Any(g => g.Count() >= _minCount);
}
