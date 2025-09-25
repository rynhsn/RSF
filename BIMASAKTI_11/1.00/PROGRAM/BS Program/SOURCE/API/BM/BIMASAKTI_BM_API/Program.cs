using R_APIStartUp;
using R_CrossPlatformSecurity;

var builder = WebApplication.CreateBuilder(args);


builder.R_RegisterServices(
    option =>
    {
        option.R_DisableAuthorization();
        option.R_DisableReportServerClient();
    }
    );

//Fast Report
builder.Services.AddFastReport();

builder.Services.AddSingleton<R_ISymmetricProvider, R_SymmetricAESProvider>();

var app = builder.Build();

//Use fast Report
app.UseFastReport();

app.R_SetupMiddleware();

app.UseStaticFiles(); //blazor

app.Run();  