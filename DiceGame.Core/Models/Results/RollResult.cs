using DiceGame.Core.Models;

namespace DiceGame.Core.Models.Results;

public record RollResult(
    bool Success,
    string? Error,
    IReadOnlyList<Die> Dice,
    int RollsUsed,
    int RollsRemaining);
