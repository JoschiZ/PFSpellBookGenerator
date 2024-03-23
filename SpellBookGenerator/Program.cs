using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Shared;
using SpellBookGenerator;
using SpellBookGenerator.Core.Sources;
using SpellBookGenerator.Core.Spells;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddScoped<SpellService<Pathfinder1Spell>>();
builder.Services.AddScoped<SpellService<Pathfinder2Spell>>();
builder.Services.AddSingleton<LoadingService>();
builder.Services.AddScoped<SourceService>();

await builder.Build().RunAsync();