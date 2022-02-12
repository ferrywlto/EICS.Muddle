using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using EICS.WordleBlazor;
using EICS.WordleBlazor.Game;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

List<string> WordList = new() {"hello", "world", "fiddle", "middle", "sharp", "server"};
string SelectAnswer() => WordList[new Random().Next() % WordList.Count];
var answer = SelectAnswer();
builder.Services.AddSingleton(typeof(Game), new Game(answer));
builder.Services.AddSingleton(typeof(GameInput), new GameInput(answer.Length));
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
await builder.Build().RunAsync();
