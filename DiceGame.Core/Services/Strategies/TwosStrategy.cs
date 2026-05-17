using DiceGame.Core.Models;

namespace DiceGame.Core.Services.Strategies;

public sealed class TwosStrategy : FaceValueStrategy
{
    public TwosStrategy() : base(2) { }

    public override ScoreCategory Category => ScoreCategory.Twos;
}
