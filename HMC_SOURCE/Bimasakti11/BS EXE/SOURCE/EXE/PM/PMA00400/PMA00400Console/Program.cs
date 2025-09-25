using log4net;
using log4net.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Cache;
using R_CommonFrontBackAPI;
using R_MultiTenantDb;
using System.Data.Common;
using R_ReportServerClient;
using PMA00400Common.Parameter;
using PMA00400Back;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using PMA00400Console;
using R_ApplicationBack;
using PMA00400Console.Logger;
using R_CommonFrontBackAPI.Log;
using PMA00400Console.MultiTenant;
using R_MultiTenantDb.Abstract;
using static System.Collections.Specialized.BitVector32;
using PMA00400Common.DTO;

XmlConfigurator.Configure(new FileInfo("log4net.config"));
var logger = LogManager.GetLogger(typeof(Program));

try
{
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
               //services.R_LoadDistributedCache();
               services.AddScoped<ConsoleMain>();
               services.AddScoped<MultiTenantHelper>();
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

    //Get Logger Service
    //logger = app.Services.GetRequiredService<Serilog.ILogger>();
    // Konfigurasi layanan cache terdistribusi
    R_DistributedCache.ConfigureService(app.Services.GetService<IDistributedCache>()); //NEW
    //logger.Info("Start Console Program");

    try
    {
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
            PROGRAM_ID = "DatabaseAndMultiTenantConsole",
            USER_ID = "System",
            USER_NAME = "ConsoleSystem",
            CORRELATION_ID = Guid.NewGuid().ToString(),
        };
        R_NetCoreLogUtility.R_SetNetCoreLogKey<R_NetCoreLogKeyDTO>(loConsoleLogKey);

        // Konfigurasi multi-tenant database
        R_MultiTenantDbRepository.ConfigureService(
            app.Services.GetService<IMemoryCache>(),
        null
        //app.Services.GetService<IDistributedCache>()
        );
        app.Services.GetService<R_ReportServerClientService>();
        //Check Single Tenant args
        IsSingleTenant = false; //default
        if (args.Length > 0 && bool.TryParse(args[0], out IsSingleTenant) == false)
        {
            IsSingleTenant = false;
        }

        GetDomainDTO loDataDomainTypeURL = new ();
        if (IsSingleTenant == false)
        {
            MultiTenantHelper loHelper = app.Services.GetRequiredService<MultiTenantHelper>();

            PMA00400Cls PMA00400Cls = new();
            loDataDomainTypeURL = PMA00400Cls.GetDataDomainType_URL(pcConnectionMultiName: "MultiConnectionString");

            loHelper.InjectMultiTenantConnectionString(pcAppId: "Bimasakti", pcConnectionMultiName: "MultiConnectionString");
        }
        logger.Info("Get Service ConsoleMain");
        //Get Instant Service
        ConsoleMain loMainClass = app.Services.GetRequiredService<ConsoleMain>();

        //Call Method main
        logger.Info("Call Method Main");
        await loMainClass.Main(IsSingleTenant, R_Db.eDbConnectionStringType.MainConnectionString, loDataDomainTypeURL);
    }
    catch (Exception ex)
    {
        logger.Error(ex);
        throw;
    }
}
catch (Exception ex)
{
    logger.Error("Unhandled exception occurred.", ex);
}
logger.Info("End Console Program");


/*

// Configure Log4Net
XmlConfigurator.Configure(new FileInfo("log4net.config"));
var logger = LogManager.GetLogger(typeof(Program));

#region MultiTenant Region
///*
try
{
    logger.Info("Application starting...");
    var builder = Host.CreateDefaultBuilder(args);

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
         logger.Info("Adding services...");
         //services.R_LoadDistributedCache();
         services.AddMemoryCache();
         //services.R_LoadReportClientService();
         logger.Info("Services added successfully.");

         logger.Info("Adding Scope...");
         services.AddScoped<GetConnectionName>();
         services.AddScoped<HelperBackCls>();
     });

    // Registrasi DbProviderFactories untuk SQL Server
    DbProviderFactories.RegisterFactory("System.Data.SqlClient", System.Data.SqlClient.SqlClientFactory.Instance);

    var app = builder.Build();

    // Konfigurasi layanan cache terdistribusi
    R_DistributedCache.ConfigureService(app.Services.GetService<IDistributedCache>());

    // Konfigurasi multi-tenant database
    R_MultiTenantDbRepository.ConfigureService(
        app.Services.GetService<IMemoryCache>(),
        null
        //app.Services.GetService<IDistributedCache>()
    );
    app.Services.GetService<R_ReportServerClientService>();


    ///*This command to check Dbsection configuration appsettings.json on this project
    var abc = R_MultiTenantDbRepository.IsMultiTenantDb;
    var loDbSection = R_ConfigurationUtility.R_GetSection<R_DBSection>("R_DBSection");
    foreach (var db in loDbSection.R_DBConfigs)
    {
        logger.Info($"Available DB: {db.Name}");
    }
    HelperBackCls loHelper = app.Services.GetService<HelperBackCls>();
    loHelper.InjectMultiTenantConnectionString("Bimasakti", "MultiConnectionString");

    logger.Info("Get connection string name");
    PMA00400Console.GetConnectionName loGetConnName = app.Services.GetService<GetConnectionName>();
    List<string> loNames = loGetConnName.GetConnectionNames();

    if (loNames.Count > 0)
    {
        logger.Info("Process data based on Tenant");
        foreach (var itemConnectionString in loNames)
        {
            string pcTenantCustomerId = itemConnectionString.Replace("_CONNECTIONSTRING", "");
            string pcUserId = "SYSTEM";
            PMA00400Cls PMA00400Cls = new PMA00400Cls();
            await PMA00400Cls.ProcessHandoverDistribute(pcTenantCustomerId, pcUserId, itemConnectionString);

            ParamSendEmailDTO loParamSendEmail = new ParamSendEmailDTO()
            {
                CPROGRAM_ID = "PMA00400",
                CGET_FILE_API_URL = "",
                CDB_TENANT_ID = pcTenantCustomerId,
                CLANG_ID = "en"
            };
            logger!.Info(string.Format("START process Send Email"));
            PMA00400Cls.SendEmail(loParamSendEmail);

        }

        logger.Info("Application executed successfully.");
    }
}
catch (Exception ex)
{
    logger.Fatal("Unhandled exception occurred.", ex);
}

#endregion
*/