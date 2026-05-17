using DiceGame.Core.Interfaces;
using DiceGame.Core.Models;
using DiceGame.Core.Models.Results;

namespace DiceGame.Core.Services;

public class GameEngine : IGameEngine
{
    private readonly IDiceRoller _roller;
    private readonly ScoringService _scoringService;
    private Game? _game;

    public GameEngine(IDiceRoller roller, ScoringService scoringService)
    {
        ArgumentNullException.ThrowIfNull(roller);
        ArgumentNullException.ThrowIfNull(scoringService);

        _roller = roller;
        _scoringService = scoringService;
    }

    public StartGameResult StartGame(IReadOnlyList<string> playerNames)
    {
        if (playerNames is null)
            return StartGameResult.Fail("Player names list is required");

        if (playerNames.Count < GameConstants.MinPlayers || playerNames.Count > GameConstants.MaxPlayers)
            return StartGameResult.Fail(
                $"Player count must be between {GameConstants.MinPlayers} and {GameConstants.MaxPlayers}");

        var trimmed = playerNames.Select(n => n?.Trim() ?? string.Empty).ToList();
        if (trimmed.Any(string.IsNullOrEmpty))
            return StartGameResult.Fail("All player names must be non-empty");

        if (trimmed.Distinct(StringComparer.OrdinalIgnoreCase).Count() != trimmed.Count)
            return StartGameResult.Fail("Player names must be unique");

        var players = trimmed.Select(name => new Player(name)).ToList();
        _game = new Game(players);

        // Pierwszy rzut automatyczny dla pierwszego gracza
        AutoRollForCurrentPlayer();

        return StartGameResult.Ok();
    }

    public RollResult RollDice()
    {
        if (_game is null)
            return RollResult.Fail("Game has not started");

        if (_game.Phase != TurnPhase.Rolling)
            return RollResult.Fail("Cannot roll in current phase");

        var rolled = _roller.Roll(_game.Dice);
        _game.RecordRoll(rolled);

        return RollResult.Ok(_game.Dice, _game.RollsUsed, _game.RollsRemaining);
    }

    public HoldResult ToggleHold(int dieIndex)
    {
        if (_game is null)
            return HoldResult.Fail("Game has not started");

        if (_game.Phase != TurnPhase.Rolling)
            return HoldResult.Fail("Hold can only be toggled during rolling phase");

        if (dieIndex < 0 || dieIndex >= GameConstants.DiceCount)
            return HoldResult.Fail($"Die index {dieIndex} is out of range");

        _game.ToggleHold(dieIndex);
        return HoldResult.Ok();
    }

    public ScoreResult SelectCategory(ScoreCategory category)
    {
        if (_game is null)
            return ScoreResult.Fail("Game has not started");

        if (_game.Phase == TurnPhase.GameOver)
            return ScoreResult.Fail("Game is already over");

        if (_game.Phase != TurnPhase.Rolling && _game.Phase != TurnPhase.AwaitingScore)
            return ScoreResult.Fail("Cannot score in current phase");

        var card = _game.CurrentPlayer.Card;
        if (card.IsCategoryUsed(category))
            return ScoreResult.Fail($"Category {category} is already used by current player");

        var points = _scoringService.CalculateScore(category, _game.Dice);
        card.RecordScore(category, points);

        _game.AdvanceToNextPlayer();

        // Pierwszy rzut automatyczny dla kolejnego gracza, o ile gra jeszcze trwa
        if (!_game.IsOver)
            AutoRollForCurrentPlayer();

        return ScoreResult.Ok(category, points);
    }

    public GameState GetCurrentState()
    {
        if (_game is null)
        {
            return new GameState(
                Players: Array.Empty<Player>(),
                CurrentPlayerIndex: 0,
                Dice: Array.Empty<Die>(),
                RollsUsed: 0,
                RollsRemaining: GameConstants.MaxRollsPerTurn,
                Phase: TurnPhase.NotStarted,
                Winner: null);
        }

        return new GameState(
            _game.Players,
            _game.CurrentPlayerIndex,
            _game.Dice,
            _game.RollsUsed,
            _game.RollsRemaining,
            _game.Phase,
            _game.Winner);
    }

    private void AutoRollForCurrentPlayer()
    {
        if (_game is null) return;

        var rolled = _roller.Roll(_game.Dice);
        _game.RecordRoll(rolled);
    }
}
