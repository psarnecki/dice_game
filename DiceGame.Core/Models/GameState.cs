namespace DiceGame.Core.Models;

// Niezmienna migawka stanu gry zwracana do warstwy UI
public record GameState(
    IReadOnlyList<Player> Players,
    int CurrentPlayerIndex,
    IReadOnlyList<Die> Dice,
    int RollsUsed,
    int RollsRemaining,
    TurnPhase Phase,
    Player? Winner);
