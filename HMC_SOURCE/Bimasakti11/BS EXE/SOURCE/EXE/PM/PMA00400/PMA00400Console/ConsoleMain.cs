using PMA00400Console.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMA00400Console.MultiTenant;
using Microsoft.Extensions.Logging;
using R_MultiTenantDb;
using R_Common;
using PMA00400Back;
using PMA00400Common.Parameter;
using R_BackEnd;
using static R_BackEnd.R_Db;
using PMA00400Common.DTO;
using log4net;

namespace PMA00400Console
{
    public class ConsoleMain
    {
        const string DEFAULT_CONNECTION_NAME = "R_DefaultConnectionString";
        const string REPORT_CONNECTION_NAME = "R_ReportConnectionString";
        private static ILog _logger;
        private MultiTenantHelper _multiTenantHelper;

        public ConsoleMain(MultiTenantHelper multiTenantHelper)
        {
            // ConsoleLogger.R_InitializeLogger(poLogger);
            _logger = LogManager.GetLogger(typeof(MultiTenantHelper));
            _multiTenantHelper = multiTenantHelper;
        }

        public async Task Main(bool plSingleTenant, R_Db.eDbConnectionStringType peConnectionType, GetDomainDTO poDomainDTO)
        {
            R_Exception loException = new R_Exception();
            List<TenantDTO> loTenants;
            string lcConnStrName;
            PMA00400Cls PMA00400Cls = new();
            try
            {
                if (plSingleTenant)
                {
                    _logger.Info("Single Tenant Database Connection");
                    lcConnStrName = _getConnectionName("", peConnectionType);
                    GetDomainDTO loDataDomainTypeURL = PMA00400Cls.GetDataDomainType_URL(pcConnectionMultiName: "MultiConnectionString");
                    string lcTenantNameDefault = "TENANT_DF";
                    await PMA00400Cls.ProcessHandoverDistribute(TenantCustomerId: lcTenantNameDefault, UserId: "SYSTEM", ConnectionString: lcConnStrName);
                    _logger!.Info(string.Format("START process method {0} on Cls", lcConnStrName));
                    //_logger.Info("Connection name={0}, Database Name={1}", lcConnStrName);//
                    ParamSendEmailDTO loParamSendEmail = new ParamSendEmailDTO()
                    {
                        CPROGRAM_ID = "PMA00400",
                        CGET_FILE_API_URL = loDataDomainTypeURL.CDOMAIN_URL! + loDataDomainTypeURL.CDOMAIN_TYPE_URL,
                        CDB_TENANT_ID = lcTenantNameDefault,
                        CLANG_ID = "en"
                    };
                    PMA00400Cls.SendEmail(loParamSendEmail, poConnection: lcConnStrName);
                }
                else
                {
                    _logger.Info("Multi Tenant Database Connection");
                    //lcConnStrName = _getConnectionName("", peConnectionType);
                    //GetDomainDTO loDataDomainTypeURL = PMA00400Cls.GetDataDomainType_URL(pcConnectionMultiName: "MultiConnectionString");
                    _logger.Info("Get Tenants");
                    loTenants = _multiTenantHelper.getTenants();

                    foreach (TenantDTO loTenant in loTenants)
                    {
                        lcConnStrName = _getConnectionName(loTenant.CTENANT_ID, peConnectionType);
                        GetDomainDTO loDataDomainTypeURL = poDomainDTO;
                        _logger!.Info(string.Format("START process method {0} on Cls", loTenant.CTENANT_ID));
                        await PMA00400Cls.ProcessHandoverDistribute(TenantCustomerId: loTenant.CTENANT_ID, UserId: "SYSTEM", ConnectionString: lcConnStrName);
                        _logger!.Info(string.Format("START process method {0} on Cls", lcConnStrName));

                        ParamSendEmailDTO loParamSendEmail = new ParamSendEmailDTO()
                        {
                            CPROGRAM_ID = "PMA00400",
                            CGET_FILE_API_URL = loDataDomainTypeURL.CDOMAIN_URL! + loDataDomainTypeURL.CDOMAIN_TYPE_URL,
                            CDB_TENANT_ID = loTenant.CTENANT_ID,
                            CLANG_ID = "en"
                        };
                        PMA00400Cls.SendEmail(loParamSendEmail, poConnection: lcConnStrName);
                    }
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);

            }
            //EndBlock:
            if (loException.Haserror)
            {
                _logger.Error(loException);
            }
        }
        private string _getConnectionName(string pcTenantId, R_Db.eDbConnectionStringType peConnectionType)
        {
            string lcRtn;
            if (string.IsNullOrWhiteSpace(pcTenantId))
            {
                lcRtn = peConnectionType == eDbConnectionStringType.MainConnectionString ? DEFAULT_CONNECTION_NAME : REPORT_CONNECTION_NAME;
            }
            else
            {
                string lcSuffix;
                lcSuffix = peConnectionType == eDbConnectionStringType.MainConnectionString ? "" : "report_";
                lcRtn = String.Format("{0}_{1}connectionstring", pcTenantId.ToLower().Trim(), lcSuffix);

                if (peConnectionType == eDbConnectionStringType.ReportConnectionString)
                {
                    var loTest = R_MultiTenantDbRepository.R_GetConnectionStrings().Where(x => x.Name.ToLower() == lcRtn).SingleOrDefault();
                    if (loTest == null)
                    {
                        lcRtn = String.Format("{0}_connectionstring", pcTenantId.ToLower().Trim());
                    }
                }
            }

            return lcRtn;
        }

    }
}
