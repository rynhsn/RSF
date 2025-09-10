using PMR00210COMMON;
using R_BackEnd;
using R_Common;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Security.Authentication.ExtendedProtection;
using PMR00210COMMON.DTO_s;

namespace PMR00210BACK
{
    public class PMR00210GeneralCls
    {
        private PMR00210Logger _logger;

        private readonly ActivitySource _activitySource;

        public PMR00210GeneralCls()
        {
            _logger = PMR00210Logger.R_GetInstanceLogger();
            _activitySource = PMR00210Activity.R_GetInstanceActivitySource() ;
        }

        public List<PropertyDTO> GetPropertyList(PropertyDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<PropertyDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_GS_GET_PROPERTY_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poEntity.CUSER_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<PropertyDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public List<PeriodDtDTO> GetPeriodDtList(string pcCompanyId,string pcYear)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<PeriodDtDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_GS_GET_PERIOD_DT_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, pcCompanyId);
                loDB.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, int.MaxValue, pcYear);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<PeriodDtDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public TodayDTO GetTodayDateRecord(string pcCompanyId)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            TodayDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "SELECT dbo.RFN_GET_DB_TODAY(@CCOMPANY_ID) AS DTODAY";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, pcCompanyId);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<TodayDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public PeriodYearDTO GetPeriodYearRecord(PeriodYearDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            PeriodYearDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_PERIOD_YEAR_RANGE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, int.MaxValue, poParam.CYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CMODE", DbType.String, int.MaxValue, poParam.CMODE);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PeriodYearDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
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
