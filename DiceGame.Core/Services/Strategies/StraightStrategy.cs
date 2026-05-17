using DiceGame.Core.Interfaces;
using DiceGame.Core.Models;

namespace DiceGame.Core.Services.Strategies;

// Baza dla strita, sprawdza istnienie sekwencji N kolejnych unikalnych wartości
public abstract class StraightStrategy : IScoringStrategy
{
    private readonly int _requiredLength;
    private readonly int _points;

    protected StraightStrategy(int requiredLength, int points)
    {
        if (requiredLength < 2) throw new ArgumentOutOfRangeException(nameof(requiredLength));
        _requiredLength = requiredLength;
        _points = points;
    }

    public abstract ScoreCategory Category { get; }

    public int Calculate(IReadOnlyList<Die> dice)
        => IsConditionMet(dice) ? _points : 0;

    public bool IsConditionMet(IReadOnlyList<Die> dice)
    {
        var sorted = dice.Select(d => d.Value).Distinct().OrderBy(v => v).ToList();
        if (sorted.Count < _requiredLength) return false;

        var run = 1;
        for (var i = 1; i < sorted.Count; i++)
        {
            run = sorted[i] == sorted[i - 1] + 1 ? run + 1 : 1;
            if (run >= _requiredLength) return true;
        }

        return false;
    }
}
