using DiceGame.Core.Models;

namespace DiceGame.Core.Services.Strategies;

public sealed class SmallStraightStrategy : StraightStrategy
{
    public SmallStraightStrategy() : base(4, GameConstants.SmallStraightPoints) { }

    public override ScoreCategory Category => ScoreCategory.SmallStraight;
}
