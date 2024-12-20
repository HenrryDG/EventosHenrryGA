    using EventosHenrryGA.Client;
using EventosHenrryGA.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5139/") });

builder.Services.AddScoped<ClienteService>();

builder.Services.AddScoped<EventoService>();

await builder.Build().RunAsync();
