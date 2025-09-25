using HDA00100Console.DTO;
using HDA00100Console.MulltiTenant;
using log4net;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_MultiTenantDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static R_BackEnd.R_Db;

namespace HDA00100Console
{
    public class ConsoleMain
    {
        const string DEFAULT_CONNECTION_NAME = "R_DefaultConnectionString";
        const string REPORT_CONNECTION_NAME = "R_ReportConnectionString";
        private static ILog _logger;
        private MultiTenantHelper _multiTenantHelper;

        public ConsoleMain(MultiTenantHelper multiTenantHelper)
        {
            _multiTenantHelper = multiTenantHelper;
            _logger = LogManager.GetLogger(typeof(ConsoleMain));
        }
        public async Task Main(bool plSingleTenant, R_Db.eDbConnectionStringType peConnectionType)
        {
            string? lcMethodName = nameof(Main);
            _logger!.Info(string.Format("START process method {0} ", lcMethodName));
            R_Exception loException = new R_Exception();
            List<TenantDTO> loTenants;
            string lcConnStrName;
            HDA00100Process loProcess = new();
            try
            {
                if (plSingleTenant)
                {
                    _logger.Info("Single Tenant Database Connection");
                    lcConnStrName = _getConnectionName("", peConnectionType);
                    string lcTenantNameDefault = "TENANT_DF";
                    await loProcess.Process_HD_SCHEDULER_ACTION(ConnectionString: lcConnStrName);
                }
                else
                {
                    _logger.Info("Multi Tenant Database Connection");
                    _logger.Info("Get Tenants");
                    loTenants = _multiTenantHelper.getTenants();

                    foreach (TenantDTO loTenant in loTenants)
                    {
                        _logger!.Info(string.Format("Tenant {0}", loTenant.CTENANT_ID));
                        lcConnStrName = _getConnectionName(loTenant.CTENANT_ID, peConnectionType);

                        await loProcess.Process_HD_SCHEDULER_ACTION(ConnectionString: lcConnStrName);
                        _logger.Info(string.Format("Connection name={0} : ", lcConnStrName));
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
            {
                _logger.Error(loException);
            }
            _logger!.Info(string.Format("END process method {0} ", lcMethodName));

        }
        private string _getConnectionName(string pcTenantId, R_Db.eDbConnectionStringType peConnectionType)
        {
            string? lcMethodName = nameof(_getConnectionName);
            _logger!.Info(string.Format("START process method {0} ", lcMethodName));
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

            _logger!.Info(string.Format("END process method {0} ", lcMethodName));
            return lcRtn;
        }

    }
}
