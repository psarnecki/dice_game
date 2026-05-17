using DiceGame.Core.Interfaces;
using DiceGame.Core.Models;

namespace DiceGame.Core.Services;

public class RandomDiceRoller : IDiceRoller
{
    private readonly Random _random;

    // Opcjonalny Random pozwala wstrzyknąć seed dla powtarzalnych testów
    public RandomDiceRoller(Random? random = null)
    {
        _random = random ?? Random.Shared;
    }

    public IReadOnlyList<Die> Roll(IReadOnlyList<Die> current)
    {
        ArgumentNullException.ThrowIfNull(current);

        var result = new Die[current.Count];
        for (var i = 0; i < current.Count; i++)
        {
            var die = current[i];
            result[i] = die.IsHeld
                ? die
                : die with { Value = NextDieValue() };
        }
        return result;
    }

    // Random.Next, aby brać pod uwagę górną granicę, która jest wyłączna
    private int NextDieValue()
        => _random.Next(GameConstants.DieMinValue, GameConstants.DieMaxValue + 1);
}
