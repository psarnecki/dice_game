namespace DiceGame.Core.Models;

// Korzeń agregatu — mutowalny stan rozgrywki hot-seat
public class Game
{
    private readonly List<Player> _players;
    private List<Die> _dice;
    private int _currentPlayerIndex;
    private int _rollsUsed;
    private TurnPhase _phase;

    public IReadOnlyList<Player> Players => _players;
    public int CurrentPlayerIndex => _currentPlayerIndex;
    public Player CurrentPlayer => _players[_currentPlayerIndex];
    public IReadOnlyList<Die> Dice => _dice;
    public int RollsUsed => _rollsUsed;
    public int RollsRemaining => GameConstants.MaxRollsPerTurn - _rollsUsed;
    public TurnPhase Phase => _phase;
    public bool IsOver => _players.All(p => p.Card.IsComplete);

    public Player? Winner => IsOver
        ? _players.OrderByDescending(p => p.Card.GrandTotal).First()
        : null;

    public Game(IReadOnlyList<Player> players)
    {
        ArgumentNullException.ThrowIfNull(players);
        if (players.Count < GameConstants.MinPlayers || players.Count > GameConstants.MaxPlayers)
        {
            throw new ArgumentException(
                $"Player count must be between {GameConstants.MinPlayers} and {GameConstants.MaxPlayers}",
                nameof(players));
        }

        _players = players.ToList();
        _currentPlayerIndex = 0;
        _dice = CreateInitialDice();
        _rollsUsed = 0;
        _phase = TurnPhase.Rolling;
    }

    public void RecordRoll(IReadOnlyList<Die> rolledDice)
    {
        ArgumentNullException.ThrowIfNull(rolledDice);
        if (rolledDice.Count != GameConstants.DiceCount)
            throw new ArgumentException("Invalid dice count", nameof(rolledDice));

        _dice = rolledDice.ToList();
        _rollsUsed++;

        // Po wykorzystaniu maksymalnej liczby rzutów gracz musi wybrać kategorię do zapisu punktów
        _phase = _rollsUsed >= GameConstants.MaxRollsPerTurn
            ? TurnPhase.AwaitingScore
            : TurnPhase.Rolling;
    }

    public void ToggleHold(int dieIndex)
    {
        if (dieIndex < 0 || dieIndex >= _dice.Count)
            throw new ArgumentOutOfRangeException(nameof(dieIndex));

        var die = _dice[dieIndex];
        _dice[dieIndex] = die with { IsHeld = !die.IsHeld };
    }

    public void AdvanceToNextPlayer()
    {
        _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
        _rollsUsed = 0;
        _dice = CreateInitialDice();
        _phase = IsOver ? TurnPhase.GameOver : TurnPhase.Rolling;
    }

    private static List<Die> CreateInitialDice()
        => Enumerable.Range(0, GameConstants.DiceCount)
            .Select(_ => new Die(GameConstants.DieMinValue, IsHeld: false))
            .ToList();
}
