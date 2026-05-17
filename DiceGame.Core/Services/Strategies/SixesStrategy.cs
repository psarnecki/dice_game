using DiceGame.Core.Models;

namespace DiceGame.Core.Services.Strategies;

public sealed class SixesStrategy : FaceValueStrategy
{
    public SixesStrategy() : base(6) { }

    public override ScoreCategory Category => ScoreCategory.Sixes;
}
