using DiceGame.Core.Models;

namespace DiceGame.Core.Services.Strategies;

public sealed class FoursStrategy : FaceValueStrategy
{
    public FoursStrategy() : base(4) { }

    public override ScoreCategory Category => ScoreCategory.Fours;
}
