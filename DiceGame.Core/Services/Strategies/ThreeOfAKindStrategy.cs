using DiceGame.Core.Models;

namespace DiceGame.Core.Services.Strategies;

public sealed class ThreeOfAKindStrategy : OfAKindStrategy
{
    public ThreeOfAKindStrategy() : base(3) { }

    public override ScoreCategory Category => ScoreCategory.ThreeOfAKind;
}
