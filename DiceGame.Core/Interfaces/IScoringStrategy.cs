using DiceGame.Core.Models;

namespace DiceGame.Core.Interfaces;

public interface IScoringStrategy
{
    ScoreCategory Category { get; }

    // Zwraca 0 gdy warunek kategorii niespełniony
    int Calculate(IReadOnlyList<Die> dice);

    // Używane do podświetlania możliwych kategorii w UI
    bool IsConditionMet(IReadOnlyList<Die> dice);
}
