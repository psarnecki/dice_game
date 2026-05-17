namespace DiceGame.Core.Models;

// Główna klasa zarządzająca stanem gry i zmianą tur graczy
public class Game
{
    public IReadOnlyList<Player> Players => throw new NotImplementedException();
    public int CurrentPlayerIndex => throw new NotImplementedException();
    public Player CurrentPlayer => throw new NotImplementedException();
    public IReadOnlyList<Die> Dice => throw new NotImplementedException();
    public int RollsUsed => throw new NotImplementedException();
    public TurnPhase Phase => throw new NotImplementedException();
    public bool IsOver => throw new NotImplementedException();

    public Game(IReadOnlyList<Player> players) => throw new NotImplementedException();
}
