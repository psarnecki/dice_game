using DiceGame.Core.Models;

namespace DiceGame.Core.Services.Strategies;

public sealed class OnesStrategy : FaceValueStrategy
{
    public OnesStrategy() : base(1) { }

    public override ScoreCategory Category => ScoreCategory.Ones;
}
