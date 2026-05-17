using DiceGame.Core.Models;

namespace DiceGame.Core.Services.Strategies;

public sealed class ThreesStrategy : FaceValueStrategy
{
    public ThreesStrategy() : base(3) { }

    public override ScoreCategory Category => ScoreCategory.Threes;
}
