using log4net;
using log4net.Config;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PMA00300BACK;
using PMA00300COMMON;
using PMA00300COMMON.Param_DTO;
using R_BackEnd;
using R_CommonFrontBackAPI;
using R_MultiTenantDb;
using System.Data.Common;
using R_ReportServerClient;

// Configure Log4Net
XmlConfigurator.Configure(new FileInfo("log4net.config"));
var logger = LogManager.GetLogger(typeof(Program));

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
          services.AddMemoryCache();
          services.R_LoadReportClientService();
          logger.Info("Services added successfully.");

      });

    // Registrasi DbProviderFactories untuk SQL Server
    DbProviderFactories.RegisterFactory("System.Data.SqlClient", System.Data.SqlClient.SqlClientFactory.Instance);

    var app = builder.Build();

    // Konfigurasi multi-tenant database and in this program not implement multi tenant
    R_MultiTenantDbRepository.ConfigureService(
        app.Services.GetService<IMemoryCache>(), null
    );
    app.Services.GetService<R_ReportServerClientService>();

    R_DBSection dbConfigSection = R_ConfigurationUtility.R_GetSection<R_DBSection>("R_DBSection");

    if (dbConfigSection == null || dbConfigSection.R_DBConfigs.Count == 0)
    {
        logger.Error("No database configuration found in appsettings.json.");
        return;
    }

    logger.Info("Get data config parameter");
    ConfigParameterDTO loConfigParameter = R_ConfigurationUtility.R_GetSection<ConfigParameterDTO>("AppParameter");

    PMA00300Cls PMA00300Cls = new PMA00300Cls();
    PMA00300ReportClientParameterDTO GetClientReportParameter = PMA00300Cls.GetClientReportFormat(loConfigParameter.CCOMPANY_ID);
    GetClientReportParameter.CTENANT_CUSTOMER_ID = loConfigParameter.CTENANT_CUSTOMER_ID;

    PMA00300ParamReportDTO poParam = new PMA00300ParamReportDTO()
    {
        CCOMPANY_ID = loConfigParameter.CCOMPANY_ID,
        CPROPERTY_ID = loConfigParameter.CPROPETY_ID,
        CPERIOD = DateTime.Now.ToString("yyyyMM"),
        CUSER_ID = "System",
        CLANG_ID = GetClientReportParameter.CREPORT_CULTURE
    };

    PMA00300Cls ProcessReport = new PMA00300Cls();
    await ProcessReport.ProcessReport(GetClientReportParameter, poParam);
    //#region MyRegion
    //Console.Write("Masukkan nama Anda: ");
    //string nama = Console.ReadLine();

    //Console.WriteLine("Halo, " + nama + "!");
    //#endregion
    logger.Info("Application executed successfully.");
}
catch (Exception ex)
{
    logger.Fatal("Unhandled exception occurred.", ex);
}

