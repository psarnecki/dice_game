using DiceGame.Core.Interfaces;
using DiceGame.Core.Models;
using DiceGame.Core.Models.Results;
using DiceGame.Core.Services;

namespace DiceGame.Client.Services;

// Zarządza stanem aplikacji i powiadamia komponenty Blazora o konieczności odświeżenia interfejsu
public class GameStateService
{
    private readonly IGameEngine _engine;
    private readonly ScoringService _scoringService;

    public GameStateService(IGameEngine engine, ScoringService scoringService)
    {
        ArgumentNullException.ThrowIfNull(engine);
        ArgumentNullException.ThrowIfNull(scoringService);

        _engine = engine;
        _scoringService = scoringService;
        State = _engine.GetCurrentState();
    }

    public GameState State { get; private set; }

    public event Action? StateChanged;

    public StartGameResult StartGame(IReadOnlyList<string> playerNames)
    {
        var result = _engine.StartGame(playerNames);
        RefreshState();
        return result;
    }

    public RollResult RollDice()
    {
        var result = _engine.RollDice();
        RefreshState();
        return result;
    }

    public HoldResult ToggleHold(int dieIndex)
    {
        var result = _engine.ToggleHold(dieIndex);
        RefreshState();
        return result;
    }

    public ScoreResult SelectCategory(ScoreCategory category)
    {
        var result = _engine.SelectCategory(category);
        RefreshState();
        return result;
    }
    
    // Wylicza potencjalne punkty dla wszystkich kategorii, co pozwala wyświetlić podpowiedzi w tabeli wyników
    public IReadOnlyDictionary<ScoreCategory, int> GetPossibleScores()
        => _scoringService.GetAllPossibleScores(State.Dice);

    private void RefreshState()
    {
        State = _engine.GetCurrentState();
        StateChanged?.Invoke();
    }
}
