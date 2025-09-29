using PMR00800COMMON;
using PMR00800COMMON.DTO_s;
using PMR00800COMMON.DTO_s.Print;
using R_BackEnd;
using R_Common;
using R_Storage;
using R_StorageCommon;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;

namespace PMR00800BACK
{
    public class PMR00800Cls
    {
        private PMR00800Logger _logger;

        private ActivitySource _activitySource;

        public PMR00800Cls()
        {
            _logger = PMR00800Logger.R_GetInstanceLogger();
            _activitySource = PMR00800Activity.R_GetInstanceActivitySource();
        }

        public List<PMR00800SpResultDTO> GetSummaryData(PMR00800SpParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<PMR00800SpResultDTO> loRtn = null;
            R_Db loDB = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery = "";
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_PMR00800_LEASE_REVENUE_ANALYSIS_REPORT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poParam.CUSER_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poParam.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CFROM_BUILDING", DbType.String, int.MaxValue, poParam.CFROM_BUILDING);
                loDB.R_AddCommandParameter(loCmd, "@CTO_BUILDING", DbType.String, int.MaxValue, poParam.CTO_BUILDING);
                loDB.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, int.MaxValue, poParam.CPERIOD);
                loDB.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, int.MaxValue, poParam.CLANG_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<PMR00800SpResultDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        //public PrintLogoResultDTO GetCompanyLogo(string pcCompanyId)
        //{
        //    using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
        //    var loEx = new R_Exception();
        //    PrintLogoResultDTO loResult = null;
        //    try
        //    {
        //        R_Db loDb = new();
        //        var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
        //        var loCmd = loDb.GetCommand();
        //        var lcQuery = "SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO";
        //        loCmd.CommandText = lcQuery;
        //        loCmd.CommandType = CommandType.Text;
        //        loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, pcCompanyId);

        //        //Debug Logs
        //        ShowLogDebug(lcQuery, loCmd.Parameters);
        //        var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
        //        loResult = R_Utility.R_ConvertTo<PrintLogoResultDTO>(loDataTable).FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //        ShowLogError(loEx);
        //    }
        //    loEx.ThrowExceptionIfErrors();
        //    return loResult;
        //}

        public PMR00800PrintBaseHeaderLogoDTO GetBaseHeaderLogoCompany(PMR00800SpParamDTO poParam)
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetBaseHeaderLogoCompany));
            var loEx = new R_Exception();
            PMR00800PrintBaseHeaderLogoDTO loResult = null;
            R_Db loDb = null; // Database object    
            DbConnection loConn = null;
            DbCommand loCmd = null;


            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                loCmd = loDb.GetCommand();



                var lcQuery = "RSP_GS_GET_PROPERTY_DETAIL";
                loCmd = loDb.GetCommand();
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poParam.CPROPERTY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);
                _logger.LogDebug("EXEC {lcQuery} {@Parameters}", lcQuery, loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<PMR00800PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault();

                if (string.IsNullOrEmpty(loResult.CSTORAGE_ID) == false)
                {
                    var loReadParameter = new R_ReadParameter()
                    {
                        StorageId = loResult.CSTORAGE_ID
                    };

                    var loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);

                    loResult.BLOGO = loReadResult.Data;
                }

                //ambil company name
                lcQuery = $"EXEC RSP_GS_GET_COMPANY_INFO '{poParam.CCOMPANY_ID}'"; // Query to get company name
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                //Debug Logs
                _logger.LogDebug(lcQuery);
                loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                var loCompanyNameResult = R_Utility.R_ConvertTo<PMR00800PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault();

                loResult!.CCOMPANY_NAME = loCompanyNameResult?.CCOMPANY_NAME;
                loResult.CDATETIME_NOW = loCompanyNameResult.CDATETIME_NOW;

            }
            catch (Exception ex)
            {
                loEx.Add(ex); // Add the exception to the exception object
                _logger.LogError(loEx); // Log the exception
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }
                if (loCmd != null)
                {
                    loCmd.Dispose();
                    loCmd = null;
                }
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public PrintLogoResultDTO GetCompanyName(string pcCompanyId)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);

            var loEx = new R_Exception();
            PrintLogoResultDTO loResult = null;
            try
            {
                R_Db loDb = new();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();
                var lcQuery = "SELECT CCOMPANY_NAME FROM SAM_COMPANIES WHERE CCOMPANY_ID = @CCOMPANY_ID";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, pcCompanyId);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PrintLogoResultDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        private void ShowLogDebug(string pcQuery, DbParameterCollection poParam)
        {
            var paramValues = string.Join(", ", poParam.Cast<DbParameter>().Select(p => $"{p.ParameterName} : '{p.Value}'"));
            _logger.LogDebug($"EXEC {pcQuery} {paramValues}");
        }

        private void ShowLogError(Exception poException)
        {
            _logger.LogError(poException);
        }
    }
}