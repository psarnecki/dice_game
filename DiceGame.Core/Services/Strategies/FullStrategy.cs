using DiceGame.Core.Interfaces;
using DiceGame.Core.Models;

namespace DiceGame.Core.Services.Strategies;

public sealed class FullStrategy : IScoringStrategy
{
    public ScoreCategory Category => ScoreCategory.Full;

    public int Calculate(IReadOnlyList<Die> dice)
        => IsConditionMet(dice) ? GameConstants.FullPoints : 0;

    public bool IsConditionMet(IReadOnlyList<Die> dice)
    {
        var counts = dice.GroupBy(d => d.Value)
                         .Select(g => g.Count())
                         .OrderByDescending(c => c)
                         .ToList();

        // Dokładnie dwie różne wartości w układzie 3+2
        return counts.Count == 2 && counts[0] == 3 && counts[1] == 2;
    }
}
