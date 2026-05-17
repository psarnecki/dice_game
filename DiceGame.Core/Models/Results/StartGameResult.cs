namespace DiceGame.Core.Models.Results;

public record StartGameResult(bool Success, string? Error)
{
    public static StartGameResult Ok() => new(true, null);
    public static StartGameResult Fail(string error) => new(false, error);
}
