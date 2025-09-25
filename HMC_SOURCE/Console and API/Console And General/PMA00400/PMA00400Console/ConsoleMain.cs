using Microsoft.Extensions.Logging;
using PMA00400Back;
using PMA00400Common.DTO;
using PMA00400Common.Parameter;
using PMA00400Logger;
using R_Common;
using R_MultiTenantDb.Abstract;

namespace PMA00400Console
{
    public class ConsoleMain
    {
        const string DEFAULT_CONNECTION_NAME = "R_DefaultConnectionString";
        const string REPORT_CONNECTION_NAME = "R_ReportConnectionString";
        private readonly ConsoleLogger _logger;
        private readonly R_ILocalCacheMultiTenantFromDb _multiTenantHelper;
        private readonly string lcTenantNameDefault = "TENANT_DF";

        public ConsoleMain(ILogger<ConsoleMain> poLogger, R_ILocalCacheMultiTenantFromDb poMultiTenantHelper)
        {
            ConsoleLogger.R_InitializeLogger(poLogger);
            _logger = ConsoleLogger.R_GetInstanceLogger();
            _multiTenantHelper = poMultiTenantHelper;
        }

        public async Task Main(bool plSingleTenant, R_MultiTenantDbEnumerationConstant.eDbConnectionStringType peConnectionType, GetDomainDTO poDomainDTO)
        {
            R_Exception loException = new R_Exception();
            List<R_TenantDTO> loTenants;
            string lcConnStrName;
            PMA00400Cls PMA00400Cls = new();
            try
            {
                if (plSingleTenant)
                {
                    _logger.LogInfo("Single Tenant Database Connection");
                    lcConnStrName = await _multiTenantHelper.R_GetConnectionNameByTenantAsync("", peConnectionType);
                    GetDomainDTO loDataDomainTypeURL = await PMA00400Cls.GetDataDomainType_URLAsync(pcConnectionMultiName: "MultiConnectionString");

                    _logger.LogInfo(string.Format("START ProcessHandoverDistribute with ConnectionString: {0}", lcConnStrName));
                    await PMA00400Cls.ProcessHandoverDistribute(TenantCustomerId: lcTenantNameDefault, UserId: "SYSTEM", ConnectionString: lcConnStrName);

                    ParamSendEmailDTO loParamSendEmail = new ParamSendEmailDTO()
                    {
                        CPROGRAM_ID = "PMA00400",
                        CGET_FILE_API_URL = loDataDomainTypeURL.CDOMAIN_URL + loDataDomainTypeURL.CDOMAIN_TYPE_URL,
                        CDB_TENANT_ID = lcTenantNameDefault,
                        CLANG_ID = "en"
                    };
                    await PMA00400Cls.SendEmailAsync(loParamSendEmail, pcConnectionName: lcConnStrName);
                }
                else
                {
                    _logger.LogInfo("Multi Tenant Database Connection");
                    _logger.LogInfo("Get Tenants");
                    loTenants = _multiTenantHelper.R_GetTenants();

                    foreach (R_TenantDTO loTenant in loTenants)
                    {
                        lcConnStrName = await _multiTenantHelper.R_GetConnectionNameByTenantAsync(loTenant.CTENANT_ID, peConnectionType);
                        GetDomainDTO loDataDomainTypeURL = poDomainDTO;

                        _logger.LogInfo(string.Format("START ProcessHandoverDistribute for TenantId: {0} with ConnectionString: {1}", loTenant.CTENANT_ID, lcConnStrName));
                        await PMA00400Cls.ProcessHandoverDistribute(TenantCustomerId: loTenant.CTENANT_ID, UserId: "SYSTEM", ConnectionString: lcConnStrName);

                        ParamSendEmailDTO loParamSendEmail = new ParamSendEmailDTO()
                        {
                            CPROGRAM_ID = "PMA00400",
                            CGET_FILE_API_URL = loDataDomainTypeURL.CDOMAIN_URL + loDataDomainTypeURL.CDOMAIN_TYPE_URL,
                            CDB_TENANT_ID = loTenant.CTENANT_ID,
                            CLANG_ID = "en"
                        };
                        await PMA00400Cls.SendEmailAsync(loParamSendEmail, pcConnectionName: lcConnStrName);
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
        }
    }
}
