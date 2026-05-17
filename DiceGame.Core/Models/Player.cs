namespace DiceGame.Core.Models;

public class Player
{
    public string Id { get; }
    public string Name { get; }
    public ScoreCard Card { get; }

    public Player(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Player name is required", nameof(name));

        Id = Guid.NewGuid().ToString("N");
        Name = name.Trim();
        Card = new ScoreCard();
    }
}
