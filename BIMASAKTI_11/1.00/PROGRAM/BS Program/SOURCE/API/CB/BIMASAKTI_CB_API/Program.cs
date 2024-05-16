using R_APIStartUp;
using R_CrossPlatformSecurity;

var builder = WebApplication.CreateBuilder(args);

//builder.R_RegisterServices();

builder.R_RegisterServices(startup =>
{
    //startup.R_DisableOpenTelemetry();
    //startup.R_DisableAuthentication();
    //startup.R_DisableGlobalException();
    //startup.R_DisableContext();
    //startup.R_DisableDatabase();
    //startup.R_DisableCache();
    //startup.R_DisableFastReport();
    startup.R_DisableAuthorization();
});

builder.Services.AddSingleton<R_ISymmetricProvider, R_SymmetricAESProvider>();

var app = builder.Build();

app.R_SetupMiddleware();

app.UseStaticFiles(); //blazor

app.Run();
