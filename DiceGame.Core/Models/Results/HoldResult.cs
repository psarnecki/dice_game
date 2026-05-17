namespace DiceGame.Core.Models.Results;

public record HoldResult(bool Success, string? Error)
{
    public static HoldResult Ok() => new(true, null);
    public static HoldResult Fail(string error) => new(false, error);
}
