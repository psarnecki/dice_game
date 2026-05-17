namespace DiceGame.Core.Models.Results;

public record ScoreResult(
    bool Success,
    string? Error,
    ScoreCategory? Category,
    int PointsScored)
{
    public static ScoreResult Ok(ScoreCategory category, int points)
        => new(true, null, category, points);

    public static ScoreResult Fail(string error)
        => new(false, error, null, 0);
}
