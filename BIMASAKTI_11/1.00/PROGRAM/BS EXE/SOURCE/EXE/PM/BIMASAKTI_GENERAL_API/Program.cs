using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using R_APIStartUp;
using R_CrossPlatformSecurity;
using R_MultiTenantDb.Abstract;

var builder = WebApplication.CreateBuilder(args);

//builder.R_RegisterServices();

builder.R_RegisterServices(startup =>
{
    //startup.R_DisableOpenTelemetry();
    startup.R_DisableAuthentication();
    //startup.R_DisableGlobalException();
    //startup.R_DisableContext();
    //startup.R_DisableDatabase();
    //startup.R_DisableCache();
    //startup.R_DisableFastReport();
    startup.R_DisableAuthorization();
    //startup.R_DisableReportServerClient();
});

builder.Services.AddSingleton<R_ISymmetricProvider, R_SymmetricAESProvider>();
builder.Services.AddHealthChecks()
       .AddCheck("self", () => HealthCheckResult.Healthy());


var app = builder.Build();

app.R_SetupMiddleware();

app.UseStaticFiles(); //blazor

app.UseHealthChecks("/live", new HealthCheckOptions
{
    Predicate = r => r.Name.Contains("self")
});
app.UseHealthChecks("/ready", new HealthCheckOptions
{
    Predicate = r => r.Tags.Contains("self")
});

ThreadPool.SetMinThreads(100, 100);


app.Run();
