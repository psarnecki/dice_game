using DiceGame.Core.Models;

namespace DiceGame.Core.Services.Strategies;

public sealed class LargeStraightStrategy : StraightStrategy
{
    public LargeStraightStrategy() : base(5, GameConstants.LargeStraightPoints) { }

    public override ScoreCategory Category => ScoreCategory.LargeStraight;
}
