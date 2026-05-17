using DiceGame.Core.Interfaces;
using DiceGame.Core.Models;

namespace DiceGame.Core.Services;

public class RandomDiceRoller : IDiceRoller
{
    // Opcjonalny Random pozwala wstrzyknąć źródło dla powtarzalnych testów
    public RandomDiceRoller(Random? random = null) => throw new NotImplementedException();

    public IReadOnlyList<Die> Roll(IReadOnlyList<Die> current) => throw new NotImplementedException();
}
