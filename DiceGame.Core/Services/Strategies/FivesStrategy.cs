using DiceGame.Core.Models;

namespace DiceGame.Core.Services.Strategies;

public sealed class FivesStrategy : FaceValueStrategy
{
    public FivesStrategy() : base(5) { }

    public override ScoreCategory Category => ScoreCategory.Fives;
}
