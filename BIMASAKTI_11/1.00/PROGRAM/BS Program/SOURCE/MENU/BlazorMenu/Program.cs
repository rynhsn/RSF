using Blazored.LocalStorage;
using BlazorMenu.Authentication;
using BlazorMenu.Extensions;
using BlazorMenu.Services;
using BlazorMenu.Shared;
using BlazorMenu.Shared.Tabs;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using R_BlazorFrontEnd.Controls.Extensions;
using R_BlazorFrontEnd.Controls.Menu;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<BlazorMenu.App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddTelerikBlazor();

builder.Services.R_AddBlazorFrontEndControls();
//builder.Services.AddUIServices();

R_ConfigurationUtility.Configure(builder.Configuration);

builder.Services.R_AddBlazorFrontEnd();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, BlazorMenuAuthenticationStateProvider>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSingleton(typeof(R_ILocalizer<>), typeof(R_Localizer<>));
builder.Services.AddSingleton<R_ILocalizer, R_Localizer>();
builder.Services.AddSingleton<R_IMainBody, MainBody>();

builder.Services.AddScoped<MenuTabSetTool>();

var host = builder.Build();

await host.R_UseBlazorFrontEnd();

await host.RunAsync();
