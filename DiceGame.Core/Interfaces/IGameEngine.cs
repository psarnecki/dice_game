using DiceGame.Core.Models;
using DiceGame.Core.Models.Results;

namespace DiceGame.Core.Interfaces;

public interface IGameEngine
{
    // Wymaga 2–4 nazw graczy
    StartGameResult StartGame(IReadOnlyList<string> playerNames);

    // Pierwszy rzut - wszystkie kości
    // Kolejne - tylko niezatrzymane
    RollResult RollDice();

    HoldResult ToggleHold(int dieIndex);

    ScoreResult SelectCategory(ScoreCategory category);

    GameState GetCurrentState();
}
