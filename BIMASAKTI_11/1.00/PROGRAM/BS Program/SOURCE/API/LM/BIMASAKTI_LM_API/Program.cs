using R_APIStartUp;
using R_CrossPlatformSecurity;

var builder = WebApplication.CreateBuilder(args);

builder.R_RegisterServices();
//builder.R_RegisterServices(startup =>
//{
//    startup.R_DisableAuthentication();
//});

builder.Services.AddSingleton<R_ISymmetricProvider, R_SymmetricAESProvider>();

var app = builder.Build();

app.R_SetupMiddleware();

app.UseStaticFiles(); //blazor

app.Run();
