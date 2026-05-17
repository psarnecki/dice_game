using DiceGame.Core.Interfaces;
using DiceGame.Core.Services.Strategies;

namespace DiceGame.Client.Services;

public static class ServiceCollectionExtensions
{
    // Rejestruje wszystkie 13 strategii, możliwość automatycznego wstrzyknięcia do serwisu punktacji
    public static IServiceCollection AddScoringStrategies(this IServiceCollection services)
    {
        services.AddSingleton<IScoringStrategy, OnesStrategy>();
        services.AddSingleton<IScoringStrategy, TwosStrategy>();
        services.AddSingleton<IScoringStrategy, ThreesStrategy>();
        services.AddSingleton<IScoringStrategy, FoursStrategy>();
        services.AddSingleton<IScoringStrategy, FivesStrategy>();
        services.AddSingleton<IScoringStrategy, SixesStrategy>();
        services.AddSingleton<IScoringStrategy, ThreeOfAKindStrategy>();
        services.AddSingleton<IScoringStrategy, FourOfAKindStrategy>();
        services.AddSingleton<IScoringStrategy, FullStrategy>();
        services.AddSingleton<IScoringStrategy, SmallStraightStrategy>();
        services.AddSingleton<IScoringStrategy, LargeStraightStrategy>();
        services.AddSingleton<IScoringStrategy, YahtzeeStrategy>();
        services.AddSingleton<IScoringStrategy, ChanceStrategy>();
        
        return services;
    }
}
