using DiceGame.Core.Interfaces;
using DiceGame.Core.Models;

namespace DiceGame.Core.Services.Strategies;

// Strategia dla kategorii "Szansa", zawsze zwraca sumę oczek ze wszystkich 5 kości
public sealed class ChanceStrategy : IScoringStrategy
{
    public ScoreCategory Category => ScoreCategory.Chance;

    public int Calculate(IReadOnlyList<Die> dice) => dice.Sum(d => d.Value);

    public bool IsConditionMet(IReadOnlyList<Die> dice) => true;
}
