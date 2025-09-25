using GLB00700COMMON;
using GLB00700COMMON.DTO_s;
using GLB00700COMMON.DTO_s.Helper;
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

namespace GLB00700BACK
{
    public class GLB00700Cls
    {
        //variable & constructors
        private GLB00700Logger _logger;
        private readonly ActivitySource _activitySource;
        public GLB00700Cls()
        {
            _logger = GLB00700Logger.R_GetInstanceLogger();
            _activitySource = GLB00700Activity.R_GetInstanceActivitySource();
        }

        //method
        public TodayDTO GetTodayRecord(GeneralParamDTO poParam)
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
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
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
        public GLSysParamDTO GetGLSysParamRecord(GeneralParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loException = new R_Exception();
            GLSysParamDTO loResult = null;
            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection();
                DbCommand loCmd = loDb.GetCommand();

                string lcQuery = "RSP_GL_GET_SYSTEM_PARAM";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, int.MaxValue, poParam.CLANG_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLSysParamDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }
        public LastRateRevaluationDTO GetLastRateRevaluationRecord(LastRateRevaluationParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loException = new R_Exception();
            LastRateRevaluationDTO loResult = null;
            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection();
                DbCommand loCmd = loDb.GetCommand();

                string lcQuery = "RSP_GL_GET_LAST_RATE_REVALUATION";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, int.MaxValue, poParam.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poParam.CUSER_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<LastRateRevaluationDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        //helper function
        private void ShowLogDebug(string pcQuery, DbParameterCollection poParam)
        {
            var paramValues = string.Join(", ", poParam.Cast<DbParameter>().Select(p => $"{p.ParameterName} '{p.Value}'"));
            _logger.LogDebug($"EXEC {pcQuery} {paramValues}");
        }
        private void ShowLogError(Exception ex)
        {
            _logger.LogError(ex);
        }


    }
}
