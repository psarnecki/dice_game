namespace DiceGame.Core.Models;

public class Player
{
    public string Id { get; } = default!;
    public string Name { get; } = default!;
    public ScoreCard Card { get; } = default!;

    public Player(string name) => throw new NotImplementedException();
}
