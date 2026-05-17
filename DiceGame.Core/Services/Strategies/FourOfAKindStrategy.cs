using DiceGame.Core.Models;

namespace DiceGame.Core.Services.Strategies;

public sealed class FourOfAKindStrategy : OfAKindStrategy
{
    public FourOfAKindStrategy() : base(4) { }

    public override ScoreCategory Category => ScoreCategory.FourOfAKind;
}
