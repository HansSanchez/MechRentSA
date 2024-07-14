using MechRentSA.Client;
using MechRentSA.Client.Services;
using MechRentSA.Client.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CurrieTechnologies.Razor.SweetAlert2;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5095") });
builder.Services.AddScoped<IExcavatorService, ExcavatorService>();
//builder.Services.AddScoped<IPublicWorkService, PublicWorkService>();
//builder.Services.AddScoped<IExcavatorWorkLogService, ExcavatorWorkLogService>();

builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
