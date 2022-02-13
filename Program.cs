using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using EICS.WordleBlazor;
using EICS.WordleBlazor.Game;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<Game>();
builder.Services.AddSingleton<GameInput>();
builder.Services.AddSingleton(
    sp => {
        var httpClient = sp.GetRequiredService<HttpClient>();
        var filePath = "word-list.txt";
        return new AnswerProvider(httpClient, filePath);
    });
builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

await builder.Build().RunAsync();
