using DiceGame.Core.Interfaces;
using DiceGame.Core.Models;

namespace DiceGame.Core.Services.Strategies;

// Baza dla kategorii Jedynki–Szóstki, sumuje kości pokazujące wybraną wartość
public abstract class FaceValueStrategy : IScoringStrategy
{
    private readonly int _faceValue;

    protected FaceValueStrategy(int faceValue)
    {
        if (faceValue < GameConstants.DieMinValue || faceValue > GameConstants.DieMaxValue)
            throw new ArgumentOutOfRangeException(nameof(faceValue));

        _faceValue = faceValue;
    }

    public abstract ScoreCategory Category { get; }

    public int Calculate(IReadOnlyList<Die> dice)
        => dice.Where(d => d.Value == _faceValue).Sum(d => d.Value);

    public bool IsConditionMet(IReadOnlyList<Die> dice)
        => dice.Any(d => d.Value == _faceValue);
}
