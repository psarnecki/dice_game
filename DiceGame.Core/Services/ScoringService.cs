using DiceGame.Core.Interfaces;
using DiceGame.Core.Models;

namespace DiceGame.Core.Services;

// Dispatch do strategii
public class ScoringService
{
    public ScoringService(IEnumerable<IScoringStrategy> strategies) => throw new NotImplementedException();

    public int CalculateScore(ScoreCategory category, IReadOnlyList<Die> dice) => throw new NotImplementedException();

    public IReadOnlyDictionary<ScoreCategory, int> GetAllPossibleScores(IReadOnlyList<Die> dice) => throw new NotImplementedException();

    public bool IsUpperBonusEarned(ScoreCard card) => throw new NotImplementedException();
}
