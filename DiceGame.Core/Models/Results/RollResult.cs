using DiceGame.Core.Models;

namespace DiceGame.Core.Models.Results;

public record RollResult(
    bool Success,
    string? Error,
    IReadOnlyList<Die> Dice,
    int RollsUsed,
    int RollsRemaining)
{
    public static RollResult Ok(IReadOnlyList<Die> dice, int rollsUsed, int rollsRemaining)
        => new(true, null, dice, rollsUsed, rollsRemaining);

    public static RollResult Fail(string error)
        => new(false, error, Array.Empty<Die>(), 0, 0);
}
