using Serilog;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;

using R_ApplicationBack;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_MultiTenantDb;
using System.Data.Common;
using R_MultiTenantDb.Abstract;
using R_Scheduler.MultiTenantDb;
using R_BackEnd;
using R_Cache;
using R_ReportServerClient;
using Microsoft.Extensions.Caching.Distributed;

using PMA00300BACK;
using PMA00300COMMON;
using PMA00300COMMON.Param_DTO;
using PMA00300Console;
using Serilog.Core;
using System.Collections.Generic;

Serilog.ILogger logger = null;

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
               services.AddScoped<ConsoleMain>();
               services.AddScoped<R_ILocalCacheMultiTenantFromDb, MultiTenantHelper>();
           })
            .UseSerilog((context, loggerConfig) =>
            {
                loggerConfig.ReadFrom.Configuration(context.Configuration);
                loggerConfig.Enrich.WithCorrelationIdHeader(R_CommonConstant.R_CORRELATION_ID);
            });

    // Set Logger Factory for Custom Logger
    R_NetCoreLoggerFactory.ConfigureFactoryLogger(new LoggerFactory().AddSerilog());

    // Registrasi DbProviderFactories untuk SQL Server
    DbProviderFactories.RegisterFactory("System.Data.SqlClient", System.Data.SqlClient.SqlClientFactory.Instance);

    var app = builder.Build();

    //Get Logger Service
    logger = app.Services.GetRequiredService<Serilog.ILogger>();
    // Konfigurasi layanan cache terdistribusi
    R_DistributedCache.ConfigureService(app.Services.GetService<IDistributedCache>()); //NEW
    logger.Information("Start Console Program");

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
            PROGRAM_ID = "PMA00300 Console",
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
        //Check Single Tenant args
        IsSingleTenant = false; //default
        if (args.Length > 0 && bool.TryParse(args[0], out IsSingleTenant) == false)
        {
            IsSingleTenant = false;
        }

        if (IsSingleTenant == false)
        {
            R_ILocalCacheMultiTenantFromDb loHelper = app.Services.GetService<R_ILocalCacheMultiTenantFromDb>();
            await loHelper.R_InjectMultiTenantConnectionStringAsync("MultiConnectionString", false, pnCacheTimeoutInMinutes: 600);
        }
        logger.Information("Get Service ConsoleMain");
        //Get Instant Service
        ConsoleMain loMainClass = app.Services.GetRequiredService<ConsoleMain>();

        //Call Method main
        logger.Information("Call Method Main");
        await loMainClass.Main(IsSingleTenant, R_MultiTenantDbEnumerationConstant.eDbConnectionStringType.MainConnectionString);

        logger.Information("Application executed successfully.");
        // Console.ReadLine();
    }
    catch (Exception ex)
    {
        logger.Error(ex, "Error");
        throw;
    }





    //// Konfigurasi multi-tenant database and in this program not implement multi tenant
    //R_MultiTenantDbRepository.ConfigureService(
    //    app.Services.GetService<IMemoryCache>(), null
    //);
    //app.Services.GetService<R_ReportServerClientService>();

    //R_DBSection dbConfigSection = R_ConfigurationUtility.R_GetSection<R_DBSection>("R_DBSection");

    //if (dbConfigSection == null || dbConfigSection.R_DBConfigs.Count == 0)
    //{
    //    logger.Error("No database configuration found in appsettings.json.");
    //    return;
    //}

    //logger.Info("Get data config parameter");
    //ConfigParameterDTO loConfigParameter = R_ConfigurationUtility.R_GetSection<ConfigParameterDTO>("AppParameter");

    //PMA00300Cls PMA00300Cls = new PMA00300Cls();
    //PMA00300ReportClientParameterDTO GetClientReportParameter = PMA00300Cls.GetClientReportFormat(loConfigParameter.CCOMPANY_ID);
    //GetClientReportParameter.CTENANT_CUSTOMER_ID = loConfigParameter.CTENANT_CUSTOMER_ID;

    //PMA00300ParamReportDTO poParam = new PMA00300ParamReportDTO()
    //{
    //    CCOMPANY_ID = loConfigParameter.CCOMPANY_ID,
    //    CPROPERTY_ID = loConfigParameter.CPROPETY_ID,
    //    CPERIOD = DateTime.Now.ToString("yyyyMM"),
    //    CUSER_ID = "System",
    //    CLANG_ID = GetClientReportParameter.CREPORT_CULTURE
    //};

    //PMA00300Cls ProcessReport = new PMA00300Cls();
    //await ProcessReport.ProcessReport(GetClientReportParameter, poParam);
    ////#region MyRegion
    ////Console.Write("Masukkan nama Anda: ");
    ////string nama = Console.ReadLine();

    ////Console.WriteLine("Halo, " + nama + "!");
    ////#endregion
    //logger.Info("Application executed successfully.");


}
catch (Exception ex)
{
    logger.Fatal("Unhandled exception occurred.", ex);
}

