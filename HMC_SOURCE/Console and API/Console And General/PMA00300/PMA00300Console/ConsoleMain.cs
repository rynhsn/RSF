using Microsoft.Extensions.Logging;
using R_Common;
using R_MultiTenantDb.Abstract;
using PMA00300Logger;
using PMA00300BACK;
using R_BackEnd;
using System.Data.Common;
using System.Data;
using PMA00300Console.DTO;
using Grpc.Net.Client.Configuration;
using PMA00300COMMON;
using PMA00300COMMON.Param_DTO;

namespace PMA00300Console
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
            R_Exception loException = new R_Exception();
            List<R_TenantDTO> loTenants;
            string lcConnStrName;
            PMA00300Cls PMA00300Cls = new PMA00300Cls();
            List<CompanyDTO> loCompanies;
            List<PropertyDTO> loProperties;
            PMA00300ReportClientParameterDTO GetClientReportParameter;
            PMA00300ParamReportDTO poReportParam;
            try { 
                if (plSingleTenant)
                {
                    _logger.LogInfo("Single Tenant Database Connection");
                    _logger!.LogInfo(string.Format("Get Connection Name"));
                    lcConnStrName = await _multiTenantHelper.R_GetConnectionNameByTenantAsync("", peConnectionType);
                    string lcTenantNameDefault = "TENANT_DF";
                    _logger!.LogInfo(string.Format("START process method {0} on Cls", lcConnStrName));
                    _logger!.LogInfo(string.Format("Get Companies for tenant {0}", lcTenantNameDefault));
                    loCompanies = await _GetCompaniesAsync(lcConnStrName);
                    foreach (CompanyDTO loCompany in loCompanies)
                    {
                        _logger!.LogInfo(string.Format("Get Properties for tenant {0} and company {1}", lcTenantNameDefault, loCompany.CCOMPANY_ID));
                        loProperties = await _GetPropertiesAsync(lcConnStrName, loCompany.CCOMPANY_ID);
                        _logger!.LogInfo(string.Format("Get Client Report Format for Company {1}", loCompany.CCOMPANY_ID));
                        GetClientReportParameter = PMA00300Cls.GetClientReportFormat(loCompany.CCOMPANY_ID);

                        foreach (PropertyDTO loProperty in loProperties)
                        {
                           GetClientReportParameter.CTENANT_CUSTOMER_ID = lcTenantNameDefault;
                            poReportParam = new PMA00300ParamReportDTO()
                            {
                                CCOMPANY_ID = loCompany.CCOMPANY_ID,
                                CPROPERTY_ID = loProperty.CPROPERTY_ID,
                                CPERIOD = DateTime.Now.ToString("yyyyMM"),
                                CUSER_ID = "System",
                                CLANG_ID = GetClientReportParameter.CREPORT_CULTURE
                            };
                            _logger!.LogInfo(string.Format("Process Report Start For Property {0}", loProperty.CPROPERTY_ID));
                            PMA00300Cls ProcessReport = new PMA00300Cls();
                            await ProcessReport.ProcessReport(GetClientReportParameter, poReportParam);
                        }
                    }
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
                        _logger!.LogInfo(string.Format("Get Companies for tenant {0}", loTenant.CTENANT_ID));
                        loCompanies = await _GetCompaniesAsync(lcConnStrName);

                        foreach (CompanyDTO loCompany in loCompanies)
                        {
                            _logger!.LogInfo(string.Format("Get Properties for tenant {0} and company {1}", loTenant.CTENANT_ID, loCompany.CCOMPANY_ID));
                            loProperties = await _GetPropertiesAsync(lcConnStrName, loCompany.CCOMPANY_ID);
                            _logger!.LogInfo(string.Format("Get Client Report Format for Company {1}", loCompany.CCOMPANY_ID));
                            GetClientReportParameter = PMA00300Cls.GetClientReportFormat(loCompany.CCOMPANY_ID);
                            foreach (PropertyDTO loProperty in loProperties)
                            {

                                GetClientReportParameter.CTENANT_CUSTOMER_ID = loTenant.CTENANT_ID;
                                poReportParam = new PMA00300ParamReportDTO()
                                {
                                    CCOMPANY_ID = loCompany.CCOMPANY_ID,
                                    CPROPERTY_ID = loProperty.CPROPERTY_ID,
                                    CPERIOD = DateTime.Now.ToString("yyyyMM"),
                                    CUSER_ID = "System",
                                    CLANG_ID = GetClientReportParameter.CREPORT_CULTURE
                                };
                                _logger!.LogInfo(string.Format("Process Report Start For Property {0}", loProperty.CPROPERTY_ID));
                                PMA00300Cls ProcessReport = new PMA00300Cls();
                                await ProcessReport.ProcessReport(GetClientReportParameter, poReportParam);
                            }
                        }
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
                _logger.LogError(loException);
            }
        }



#region "Helper"
        public async Task<List<CompanyDTO>> _GetCompaniesAsync(string pcConnectionName)
        {
            R_Exception loException = new();
            List<CompanyDTO>? loResult = null;
            R_Db loDb = null;
            DbConnection loConn = null;
            string lcMethodName=null;
            try
            {
                lcMethodName = nameof(_GetCompaniesAsync);
                _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
                loDb = new R_Db();

                loConn = await loDb.GetConnectionAsync(pcConnectionName);
                var lcQuery = "Select CCOMPANY_ID, CCOMPANY_NAME from SAM_COMPANIES(nolock)";
                _logger!.LogInfo(string.Format("Execute query on method", lcMethodName));
                loResult = await loDb.SqlExecObjectQueryAsync<CompanyDTO>(lcQuery,loConn, false);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError("Error " + ex);
            }
            finally
            {
                if (loConn != null)
                {
                    if ((loConn.State == ConnectionState.Closed) == false)
                    {
                        loConn.Close();
                        loConn.Dispose();
                    }
                    loConn = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            return loResult!;
        }

        public async Task<List<PropertyDTO>> _GetPropertiesAsync(string pcConnectionName, string pcCompanyId)
        {
            string lcMethodName = null;

            R_Exception loException = new();
            List<PropertyDTO>? loResult = null;
            DbCommand loCommand = null;
            R_Db loDb = null;
            DbConnection loConn = null;
            try
            {
                lcMethodName = nameof(_GetPropertiesAsync);
                _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
                loDb = new R_Db();

                loConn = await loDb.GetConnectionAsync(pcConnectionName);
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_GS_GET_PROPERTY_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, pcCompanyId);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, "Admin");
                loDb.R_AddCommandParameter(loCommand, "@LPM_SYSTEM_PARAM ", DbType.Boolean, 10, 1);

                _logger!.LogInfo(string.Format("Execute query on method", lcMethodName));
                var loReturnTemp = await loDb.SqlExecQueryAsync(loConn, loCommand, false);
                loResult = R_Utility.R_ConvertTo<PropertyDTO>(loReturnTemp).Where(x=> x.LACTIVE==true).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError("Error " + ex);
            }
            finally
            {
                if (loConn != null)
                {
                    if ((loConn.State == ConnectionState.Closed) == false)
                    {
                        loConn.Close();
                        loConn.Dispose();
                    }
                    loConn = null;
                }
                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            return loResult!;
        }
#endregion




    }
}
