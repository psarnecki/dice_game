using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DiceGame.Client;
using DiceGame.Client.Services;
using DiceGame.Core.Interfaces;
using DiceGame.Core.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoringStrategies();
builder.Services.AddSingleton<ScoringService>();
builder.Services.AddSingleton<IDiceRoller, RandomDiceRoller>();
builder.Services.AddScoped<IGameEngine, GameEngine>();
builder.Services.AddScoped<GameStateService>();

await builder.Build().RunAsync();
