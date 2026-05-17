using DiceGame.Core.Interfaces;
using DiceGame.Core.Models;

namespace DiceGame.Core.Services.Strategies;

public sealed class YahtzeeStrategy : IScoringStrategy
{
    public ScoreCategory Category => ScoreCategory.Yahtzee;

    public int Calculate(IReadOnlyList<Die> dice)
        => IsConditionMet(dice) ? GameConstants.YahtzeePoints : 0;

    public bool IsConditionMet(IReadOnlyList<Die> dice)
        => dice.Count == GameConstants.DiceCount
           && dice.Select(d => d.Value).Distinct().Count() == 1;
}
