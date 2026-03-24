using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RpgSheetHub;
using RpgSheetHub.Models;
using RpgSheetHub.Services;
using System.Net.Http.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure the navigation engine so it's possible to download the templates from wwwroot
var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
builder.Services.AddScoped(sp => http);

builder.Services.AddScoped<ThemeService>();
builder.Services.AddScoped<ISheetStorageService, BrowserStorageService>();

// Load the system templates from the JSON file and register the TemplateService as a singleton
var templates = await http.GetFromJsonAsync<List<SystemTemplate>>("data/templates.json");
builder.Services.AddSingleton(new TemplateService(templates ?? new List<SystemTemplate>()));

await builder.Build().RunAsync();
