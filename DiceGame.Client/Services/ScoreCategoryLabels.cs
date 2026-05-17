using DiceGame.Core.Models;

namespace DiceGame.Client.Services;

// Mapowanie ScoreCategory → polska etykieta wyświetlana użytkownikowi
public static class ScoreCategoryLabels
{
    private static readonly IReadOnlyDictionary<ScoreCategory, string> Labels = new Dictionary<ScoreCategory, string>
    {
        [ScoreCategory.Ones] = "Jedynki",
        [ScoreCategory.Twos] = "Dwójki",
        [ScoreCategory.Threes] = "Trójki",
        [ScoreCategory.Fours] = "Czwórki",
        [ScoreCategory.Fives] = "Piątki",
        [ScoreCategory.Sixes] = "Szóstki",
        [ScoreCategory.ThreeOfAKind] = "Trójka",
        [ScoreCategory.FourOfAKind] = "Kareta",
        [ScoreCategory.Full] = "Full",
        [ScoreCategory.SmallStraight] = "Mały Strit",
        [ScoreCategory.LargeStraight] = "Duży Strit",
        [ScoreCategory.Yahtzee] = "Generał",
        [ScoreCategory.Chance] = "Szansa"
    };

    public static string Display(ScoreCategory category) => Labels[category];
}
