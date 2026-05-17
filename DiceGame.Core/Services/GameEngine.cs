using DiceGame.Core.Interfaces;
using DiceGame.Core.Models;
using DiceGame.Core.Models.Results;

namespace DiceGame.Core.Services;

public class GameEngine : IGameEngine
{
    public GameEngine(IDiceRoller roller, ScoringService scoringService) => throw new NotImplementedException();

    public StartGameResult StartGame(IReadOnlyList<string> playerNames) => throw new NotImplementedException();

    public RollResult RollDice() => throw new NotImplementedException();

    public HoldResult ToggleHold(int dieIndex) => throw new NotImplementedException();

    public ScoreResult SelectCategory(ScoreCategory category) => throw new NotImplementedException();

    public GameState GetCurrentState() => throw new NotImplementedException();
}
