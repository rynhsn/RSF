using APB00200COMMON;
using APB00200COMMON.DTO_s;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace APB00200BACK
{
    public class APB00200Cls
    {
        //class & constructor
        private RSP_AP_CLOSE_PROCESSResources.Resources_Dummy_Class _RSP_AP_CLOSE_PROCESS = new();

        private LoggerAPB00200 _logger;
        private readonly ActivitySource _activitySource;

        public APB00200Cls()
        {
            _logger = LoggerAPB00200.R_GetInstanceLogger();
            _activitySource = APB00200Activity.R_GetInstanceActivitySource();
        }

        //method
        public ClosePeriodDTO GetRecord_ClosePeriod(ClosePeriodParam poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            ClosePeriodDTO loResult = null;
            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();
                var lcQuery = "RSP_AP_GET_CLOSE_PERIOD";

                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, int.MaxValue, poParam.CUSER_LOGIN_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<ClosePeriodDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public CloseAPProcessResultDTO CloseAPProcess(CloseAPProcessParam poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new();
            R_Db loDb = new();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            CloseAPProcessResultDTO loResult = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();
                var lcQuery = "RSP_AP_CLOSE_PROCESS";

                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_YEAR", DbType.String, int.MaxValue, poParam.CPERIOD_YEAR);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_MONTH", DbType.String, int.MaxValue, poParam.CPERIOD_MONTH);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poParam.CUSER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);
                try
                {
                    ShowLogDebug(lcQuery, loCmd.Parameters);
                    var loDatatable = loDb.SqlExecQuery(loConn, loCmd, false);
                    loResult = R_Utility.R_ConvertTo<CloseAPProcessResultDTO>(loDatatable).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }
                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
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

        public List<ErrorCloseAPProcessDTO> GetList_ErrorCloseAPProcess(CloseAPProcessParam poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new();
            List<ErrorCloseAPProcessDTO> loResult = null;
            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();
                var lcQuery = "RSP_AP_CLOSE_PRD_TODO_LIST";

                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_YEAR", DbType.String, int.MaxValue, poParam.CPERIOD_YEAR);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_MONTH", DbType.String, int.MaxValue, poParam.CPERIOD_MONTH);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poParam.CUSER_ID);

                var loRtnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                ShowLogDebug(lcQuery, loCmd.Parameters);
                loResult = R_Utility.R_ConvertTo<ErrorCloseAPProcessDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        //log helper
        private void ShowLogDebug(string query, DbParameterCollection parameters)
        {
            var paramValues = string.Join(", ", parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} '{p.Value}'"));
            _logger.LogDebug($"EXEC {query} {paramValues}");
        }

        private void ShowLogError(Exception ex)
        {
            _logger.LogError(ex);
        }
    }
}