using PMR00210COMMON;
using PMR00210COMMON.DTO_s;
using PMR00210COMMON.Print_DTO;
using PMR00210COMMON.DTO_s;
using PMR00210COMMON.Print_DTO;
using R_BackEnd;
using R_Common;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;

namespace PMR00210BACK
{
    public class PMR00210Cls
    {
        private PMR00210Logger _logger;

        private readonly ActivitySource _activitySource;

        public PMR00210Cls()
        {
            _logger = PMR00210Logger.R_GetInstanceLogger();
            _activitySource = PMR00210Activity.R_GetInstanceActivitySource();
        }


        public List<PMR00210SPResultDTO> GetSummaryReportData(PMR00210SPParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<PMR00210SPResultDTO> loRtn = null;
            R_Db loDB = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery = "";
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_PMR00210_GET_REPORT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poParam.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CFROM_DEPT_CODE", DbType.String, int.MaxValue, poParam.CFROM_DEPARTMENT_ID);
                loDB.R_AddCommandParameter(loCmd, "@CTO_DEPT_CODE", DbType.String, int.MaxValue, poParam.CTO_DEPARTMENT_ID);
                loDB.R_AddCommandParameter(loCmd, "@CFROM_SALESMAN_ID", DbType.String, int.MaxValue, poParam.CFROM_SALESMAN_ID);
                loDB.R_AddCommandParameter(loCmd, "@CTO_SALESMAN_ID", DbType.String, int.MaxValue, poParam.CTO_SALESMAN_ID);
                loDB.R_AddCommandParameter(loCmd, "@CFROM_PERIOD", DbType.String, int.MaxValue, poParam.CFROM_PERIOD);
                loDB.R_AddCommandParameter(loCmd, "@CTO_PERIOD", DbType.String, int.MaxValue, poParam.CTO_PERIOD);
                loDB.R_AddCommandParameter(loCmd, "@CREPORT_TYPE", DbType.String, int.MaxValue, poParam.CREPORT_TYPE);
                loDB.R_AddCommandParameter(loCmd, "@LOUTSTANDING_AGREEMENT", DbType.Boolean, 2, poParam.LIS_OUTSTANDING);
                loDB.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, int.MaxValue, poParam.CLANG_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<PMR00210SPResultDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public List<PMR00211SPResultDTO> GetDetailReportData(PMR00210SPParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<PMR00211SPResultDTO> loRtn = null;
            R_Db loDB = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery = "";
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_PMR00210_GET_REPORT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poParam.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CFROM_DEPT_CODE", DbType.String, int.MaxValue, poParam.CFROM_DEPARTMENT_ID);
                loDB.R_AddCommandParameter(loCmd, "@CTO_DEPT_CODE", DbType.String, int.MaxValue, poParam.CTO_DEPARTMENT_ID);
                loDB.R_AddCommandParameter(loCmd, "@CFROM_SALESMAN_ID", DbType.String, int.MaxValue, poParam.CFROM_SALESMAN_ID);
                loDB.R_AddCommandParameter(loCmd, "@CTO_SALESMAN_ID", DbType.String, int.MaxValue, poParam.CTO_SALESMAN_ID);
                loDB.R_AddCommandParameter(loCmd, "@CFROM_PERIOD", DbType.String, int.MaxValue, poParam.CFROM_PERIOD);
                loDB.R_AddCommandParameter(loCmd, "@CTO_PERIOD", DbType.String, int.MaxValue, poParam.CTO_PERIOD);
                loDB.R_AddCommandParameter(loCmd, "@CREPORT_TYPE", DbType.String, int.MaxValue, poParam.CREPORT_TYPE);
                loDB.R_AddCommandParameter(loCmd, "@LOUTSTANDING_AGREEMENT", DbType.Boolean, 2, poParam.LIS_OUTSTANDING);
                loDB.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, int.MaxValue, poParam.CLANG_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<PMR00211SPResultDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public PrintLogoResultDTO GetCompanyLogo(string pcCompanyId)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            PrintLogoResultDTO loResult = null;
            try
            {
                R_Db loDb = new();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();
                var lcQuery = "SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO";
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
                ShowLogError(loEx);
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

        #region log method helper

        private void ShowLogDebug(string pcQuery, DbParameterCollection poParam)
        {
            var paramValues = string.Join(", ", poParam.Cast<DbParameter>().Select(p => $"{p.ParameterName} : '{p.Value}'"));
            _logger.LogDebug($"EXEC {pcQuery} {paramValues}");
        }

        private void ShowLogError(Exception poException)
        {
            _logger.LogError(poException);
        }

        #endregion
    }
}
