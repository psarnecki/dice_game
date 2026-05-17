using DiceGame.Core.Models;

namespace DiceGame.Core.Interfaces;

public interface IDiceRoller
{
    // Przerzuca tylko kości z IsHeld=false, zwracając nowy zestaw
    IReadOnlyList<Die> Roll(IReadOnlyList<Die> current);
}
