using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PMA00400Back;
using PMA00400Common.DTO;
using PMA00400Console;
using PMA00400Console.Logger;
using R_ApplicationBack;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_MultiTenantDb;
using R_MultiTenantDb.Abstract;
using R_ReportServerClient;
using R_Scheduler.MultiTenantDb;
using Serilog;
using System.Data.Common;

var loEx = new R_Exception();
Serilog.ILogger? logger = null;

try
{
    #region Service Registration
    IHostBuilder builder = Host.CreateDefaultBuilder(args);
    bool IsSingleTenant = false;
    builder
        .ConfigureAppConfiguration((context, configBuilder) =>
        {
            var configuration = configBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"appsettings.json")
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            R_ConfigurationUtility.Configure(configuration);
        })
        .ConfigureServices((context, services) =>
        {
            services.AddMemoryCache();
            services.R_LoadReportClientService();
            services.AddScoped<ConsoleMain>();
            services.AddScoped<R_ILocalCacheMultiTenantFromDb, MultiTenantHelper>();
        })
        .UseSerilog((context, loggerConfig) =>
        {
            loggerConfig.ReadFrom.Configuration(context.Configuration);
            loggerConfig.Enrich.WithCorrelationIdHeader(R_CommonConstant.R_CORRELATION_ID);
        });

    //Set Logger Factory for Custom Logger
    R_NetCoreLoggerFactory.ConfigureFactoryLogger(new LoggerFactory().AddSerilog());

    // Registrasi DbProviderFactories untuk SQL Server
    DbProviderFactories.RegisterFactory("System.Data.SqlClient", System.Data.SqlClient.SqlClientFactory.Instance);

    var app = builder.Build();

    // Konfigurasi layanan cache terdistribusi
    R_DistributedCache.ConfigureService(app.Services.GetService<IDistributedCache>()); //NEW
    //Get Logger Service
    logger = app.Services.GetRequiredService<Serilog.ILogger>();

    //Set Domain
    R_DomainConfigurationDTO loDomain = R_ConfigurationUtility.R_GetSection<R_DomainConfigurationDTO>("R_DomainSection");
    R_DBSection dbConfigSection = R_ConfigurationUtility.R_GetSection<R_DBSection>("R_DBSection");
    var _DbSection = R_ConfigurationUtility.R_GetSection<R_DatabaseSection>("R_DBSection");

    //Set Log Key
    ConsoleLogKeyDTO loConsoleLogKey = new ConsoleLogKeyDTO()
    {
        APPLICATION_ID = loDomain.ApplicationId,
        TENANT_ID = "All",
        COMPANY_ID = "All",
        PROGRAM_ID = "PMA00400 Console",
        USER_ID = "System",
        USER_NAME = "ConsoleSystem",
        CORRELATION_ID = Guid.NewGuid().ToString(),
    };
    R_NetCoreLogUtility.R_SetNetCoreLogKey<R_NetCoreLogKeyDTO>(loConsoleLogKey);

    // Konfigurasi multi-tenant database
    R_MultiTenantDbRepository.ConfigureService(
        app.Services.GetService<IMemoryCache>(),
        null
    );
    app.Services.GetService<R_ReportServerClientService>();
    #endregion


    logger.Information("Start Console Program");

    try
    {
        //Check Single Tenant args
        IsSingleTenant = false; //default
        if (args.Length > 0 && bool.TryParse(args[0], out IsSingleTenant) == false)
        {
            IsSingleTenant = false;
        }

        #region Get Domain Type Info
        GetDomainDTO loDataDomainTypeURL = new();
        if (IsSingleTenant == false)
        {
            PMA00400Cls PMA00400Cls = new();
            loDataDomainTypeURL = await PMA00400Cls.GetDataDomainType_URLAsync(pcConnectionMultiName: "MultiConnectionString");

            R_ILocalCacheMultiTenantFromDb loHelper = app.Services.GetRequiredService<R_ILocalCacheMultiTenantFromDb>();
            await loHelper.R_InjectMultiTenantConnectionStringAsync("MultiConnectionString", false, pnCacheTimeoutInMinutes: 600);
        }
        #endregion

        logger.Information("Get Service ConsoleMain");
        //Get Instant Service
        ConsoleMain loMainClass = app.Services.GetRequiredService<ConsoleMain>();

        //Call Method main
        logger.Information("Call Method Main");
        await loMainClass.Main(IsSingleTenant, R_MultiTenantDbEnumerationConstant.eDbConnectionStringType.MainConnectionString, loDataDomainTypeURL);
    }
    catch (Exception ex)
    {
        logger.Error(ex, "Error");
    }
}
catch (Exception ex)
{
    loEx.Add(ex);
}

if (loEx.Haserror)
{
    logger?.Error(loEx, "Unhandled exception occurred.");
    Console.WriteLine("Unhandled exception occurred: " + loEx.Message);

}
logger?.Information("End Console Program");
Console.WriteLine("End Console Program");


