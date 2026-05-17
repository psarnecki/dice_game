namespace DiceGame.Core.Models.Results;

public record ScoreResult(
    bool Success,
    string? Error,
    ScoreCategory Category,
    int PointsScored);
