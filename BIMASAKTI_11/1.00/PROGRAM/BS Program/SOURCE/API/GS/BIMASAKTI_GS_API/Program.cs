using R_APIStartUp;
using R_CrossPlatformSecurity;

var builder = WebApplication.CreateBuilder(args);


//===========just testing ===================
// List<WriteToDTO> writeTo = builder.Configuration.GetSection("Serilog:WriteTo").Get<List<WriteToDTO>>();
//
// WriteToDTO srWriteTo= writeTo.Where(x=> x.Name== "OpenTelemetry").FirstOrDefault();
//
// if (srWriteTo != null)
// {
//     Console.WriteLine(srWriteTo.Args.endpoint);
// }
//
// List<ConnectionDTO> connectionsDto = builder.Configuration.GetSection("R_DBSection:R_DBConfigs").Get<List<ConnectionDTO>>();
//
// ConnectionDTO connectionDto = connectionsDto.Where(x => x.Name == "R_DefaultConnectionString").FirstOrDefault();
// if (connectionDto != null)
// {
//     Console.WriteLine(connectionDto.ConnectionString);
// }

//===========just testing ===================


builder.R_RegisterServices(
    startup =>
    {
        startup.R_DisableAuthorization();
    }
    );

builder.Services.AddSingleton<R_ISymmetricProvider, R_SymmetricAESProvider>();

var app = builder.Build();

app.R_SetupMiddleware();

app.UseStaticFiles(); //blazor

app.Run();

