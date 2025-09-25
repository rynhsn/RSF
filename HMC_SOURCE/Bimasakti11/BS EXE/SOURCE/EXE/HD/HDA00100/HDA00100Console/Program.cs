// Configure Log4Net
using log4net.Config;
using log4net;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using R_CommonFrontBackAPI;
using Microsoft.Extensions.DependencyInjection;
using R_ReportServerClient;
using HDA00100Console;
using HDA00100Console.MulltiTenant;
using Serilog;
using Microsoft.Extensions.Logging;
using R_CommonFrontBackAPI.Log;
using System.Data.Common;
using Microsoft.Extensions.Caching.Distributed;
using R_Cache;
using HDA00100Console.Logger;
using Microsoft.Extensions.Caching.Memory;
using R_ApplicationBack;
using R_BackEnd;
using R_MultiTenantDb.Abstract;
using R_MultiTenantDb;
using Newtonsoft.Json;

XmlConfigurator.Configure(new FileInfo("log4net.config"));
var logger = LogManager.GetLogger(typeof(Program));
//Serilog.ILogger? logger = null;
try
{
    IHostBuilder builder = Host.CreateDefaultBuilder(args);
    bool IsSingleTenant = false;
    logger.Info("Application starting...");

    builder
        .ConfigureAppConfiguration((context, configBuilder) =>
        {
            logger.Info("Configuring application settings...");
            var configuration = configBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"appsettings.json")
                .AddEnvironmentVariables()
                .AddCommandLine(args)
            .Build();

            R_ConfigurationUtility.Configure(configuration);
            logger.Info("Application configuration loaded successfully.");
        })
     .ConfigureServices((context, services) =>
     {
         //logger.Info("Adding services...");
         services.AddMemoryCache();
         services.R_LoadReportClientService();
         services.AddScoped<ConsoleMain>();
         services.AddScoped<MultiTenantHelper>();
         //logger.Info("Services added successfully.");

     })
     .UseSerilog((context, loggerConfig) =>
        {
            //logger.Info("Adding Serilog...");
            loggerConfig.ReadFrom.Configuration(context.Configuration);
            loggerConfig.Enrich.WithCorrelationIdHeader(R_CommonConstant.R_CORRELATION_ID);
            //logger.Info(" Serilog Sucessfully...");
        });


    //Set Logger Factory for Custom Logger
    //R_NetCoreLoggerFactory.ConfigureFactoryLogger(new LoggerFactory().AddSerilog());
    // Registrasi DbProviderFactories untuk SQL Server
    DbProviderFactories.RegisterFactory("System.Data.SqlClient", System.Data.SqlClient.SqlClientFactory.Instance);
    var app = builder.Build();
    //Get Logger Service

    //logger = app.Services.GetRequiredService<Serilog.ILogger>();
    logger.Info("Finished Configured Service at program cs");
    // Konfigurasi layanan cache terdistribusi
    R_DistributedCache.ConfigureService(app.Services.GetService<IDistributedCache>()); //NEW
    logger.Info("Configure service Succesfully");

    try
    {
        //Set Domain
        R_DomainConfigurationDTO loDomain = R_ConfigurationUtility.R_GetSection<R_DomainConfigurationDTO>("R_DomainSection");
        R_DBSection dbConfigSection = R_ConfigurationUtility.R_GetSection<R_DBSection>("R_DBSection");
        //var _DbSection = R_ConfigurationUtility.R_GetSection<R_DatabaseSection>("R_DBSection");
        //Set Log Key
        ConsoleLogKeyDTO loConsoleLogKey = new ConsoleLogKeyDTO()
        {
            APPLICATION_ID = loDomain.ApplicationId,
            TENANT_ID = "All",
            COMPANY_ID = "All",
            PROGRAM_ID = "DatabaseAndMultiTenantConsole",
            USER_ID = "System",
            USER_NAME = "ConsoleSystem",
            CORRELATION_ID = Guid.NewGuid().ToString(),
        };
        logger.Debug(JsonConvert.SerializeObject(loConsoleLogKey));
        R_NetCoreLogUtility.R_SetNetCoreLogKey<R_NetCoreLogKeyDTO>(loConsoleLogKey);

        // Konfigurasi multi-tenant database
        R_MultiTenantDbRepository.ConfigureService(
            app.Services.GetService<IMemoryCache>(),
        null
        //app.Services.GetService<IDistributedCache>()
        );

        //Check Single Tenant args
        IsSingleTenant = false; //default
        logger.Info("Try to get Args on main");

        //var abc = args;
        if (args.Length > 0 && bool.TryParse(args[0], out IsSingleTenant) == false)
        {
            IsSingleTenant = false;
        }

        if (IsSingleTenant == false)
        {
            logger.Info("Get Helper MultiTenantHelper");
            MultiTenantHelper loHelper = app.Services.GetRequiredService<MultiTenantHelper>();
            loHelper.InjectMultiTenantConnectionString(pcAppId: "Bimasakti", pcConnectionMultiName: "MultiConnectionString");
        }


        logger.Info("Get Service ConsoleMain");
        //Get Instant Service
        ConsoleMain loMainClass = app.Services.GetRequiredService<ConsoleMain>();

        //Call Method main
        logger.Info("Call Method Main");
        await loMainClass.Main(IsSingleTenant, R_Db.eDbConnectionStringType.MainConnectionString);
    }
    catch (Exception ex)
    {
        logger.Error(ex + "Error at Console program");
        throw;
    }
    logger.Info("End Console Program");

}
catch (Exception ex)
{
    logger.Error("Unhandled exception occurred.", ex);
}