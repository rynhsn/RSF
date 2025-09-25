using Microsoft.Extensions.Logging;
using R_Common;
using R_MultiTenantDb.Abstract;

namespace HDA00100Console
{
    public class ConsoleMain
    {
        const string DEFAULT_CONNECTION_NAME = "R_DefaultConnectionString";
        const string REPORT_CONNECTION_NAME = "R_ReportConnectionString";
        private ConsoleLogger _logger;
        private R_ILocalCacheMultiTenantFromDb _multiTenantHelper;

        public ConsoleMain(ILogger<ConsoleMain> poLogger, R_ILocalCacheMultiTenantFromDb poMultiTenantHelper)
        {
            ConsoleLogger.R_InitializeLogger(poLogger);
            _logger = ConsoleLogger.R_GetInstanceLogger();
            _multiTenantHelper = poMultiTenantHelper;
        }
        public async Task Main(bool plSingleTenant, R_MultiTenantDbEnumerationConstant.eDbConnectionStringType peConnectionType)
        {
            string? lcMethodName = nameof(Main);
            _logger!.LogInfo(string.Format("START process method {0} ", lcMethodName));
            R_Exception loException = new R_Exception();
            List<R_TenantDTO> loTenants;
            string lcConnStrName;
            HDA00100Process loProcess = new();
            try
            {
                if (plSingleTenant)
                {
                    _logger.LogInfo("Single Tenant Database Connection");
                    lcConnStrName = await _multiTenantHelper.R_GetConnectionNameByTenantAsync("", peConnectionType);
                    string lcTenantNameDefault = "TENANT_DF";

                    _logger!.LogInfo(string.Format("Process_HD_SCHEDULER_ACTION for tenant {0} and Connection Name {1}", lcTenantNameDefault, lcConnStrName));
                    await loProcess.Process_HD_SCHEDULER_ACTION(ConnectionString: lcConnStrName);
                }
                else
                {
                    _logger.LogInfo("Multi Tenant Database Connection");
                    _logger.LogInfo("Get Tenants");
                    loTenants = _multiTenantHelper.R_GetTenants();

                    foreach (R_TenantDTO loTenant in loTenants)
                    {
                        _logger!.LogInfo(string.Format("Get Connection Name for tenant {0}", loTenant.CTENANT_ID));
                        lcConnStrName = await _multiTenantHelper.R_GetConnectionNameByTenantAsync(loTenant.CTENANT_ID, peConnectionType);

                        _logger!.LogInfo(string.Format("Process_HD_SCHEDULER_ACTION for tenant {0} and Connection Name {1}", loTenant.CTENANT_ID, lcConnStrName));
                        await loProcess.Process_HD_SCHEDULER_ACTION(ConnectionString: lcConnStrName);
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
            {
                _logger.LogError(loException);
            }
            _logger!.LogInfo(string.Format("END process method {0} ", lcMethodName));

        }

    }
}
