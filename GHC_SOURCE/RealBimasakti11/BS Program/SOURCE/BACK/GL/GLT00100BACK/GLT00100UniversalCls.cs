using GLT00100COMMON;
using R_BackEnd;
using R_Common;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;

namespace GLT00100BACK
{
    public class GLT00100UniversalCls
    {
        private LoggerGLT00100Universal _logger;
        private readonly ActivitySource _activitySource;

        public GLT00100UniversalCls()
        {
            _logger =  LoggerGLT00100Universal.R_GetInstanceLogger();
            _activitySource = GLT00100Activity.R_GetInstanceActivitySource();
        }

        #region Universal Back
        public GLT00100GSCompanyInfoDTO GetCompanyInfoRecord()
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            GLT00100GSCompanyInfoDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_COMPANY_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);


                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLT00100GSCompanyInfoDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public GLT00100GLSystemParamDTO GetGLSystemParamRecord()
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            GLT00100GLSystemParamDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GL_GET_SYSTEM_PARAM";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLT00100GLSystemParamDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        
        public GLT00100GSPeriodDTInfoDTO GetPeriodDTInfoRecord(GLT00100ParamGSPeriodDTInfoDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            GLT00100GSPeriodDTInfoDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_PERIOD_DT_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 50, poEntity.CCYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_NO", DbType.String, 50, poEntity.CPERIOD_NO);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLT00100GSPeriodDTInfoDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        
        public GLT00100GLSystemEnableOptionInfoDTO GetGLSystemEnableOptionRecord()
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            GLT00100GLSystemEnableOptionInfoDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GL_GET_SYSTEM_ENABLE_OPTION_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@COPTION_CODE", DbType.String, 50, "GL014001");

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLT00100GLSystemEnableOptionInfoDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        
        public GLT00100GSTransInfoDTO GetTransCodeInfoRecord()
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            GLT00100GSTransInfoDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_TRANS_CODE_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, ContextConstant.VAR_TRANS_CODE);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLT00100GSTransInfoDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        
        public GLT00100GSPeriodYearRangeDTO GetPeriodYearRangeRecord()
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            GLT00100GSPeriodYearRangeDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_PERIOD_YEAR_RANGE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CMODE", DbType.String, 50, "");

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLT00100GSPeriodYearRangeDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        
        public GLT00100TodayDateDTO GetTodayDateRecord()
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            GLT00100TodayDateDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "SELECT dbo.RFN_GET_DB_TODAY(@CCOMPANY_ID) AS DTODAY";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);

                //Debug Logs
               ShowLogDebug(lcQuery, loCmd.Parameters);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLT00100TodayDateDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        
        public List<GLT00100GSGSBCodeDTO> GetGSBCodeList()
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            List<GLT00100GSGSBCodeDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_GSB_CODE_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CAPPLICATION", DbType.String, 50, "BIMASAKTI");
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCLASS_ID", DbType.String, 50, "_GL_JOURNAL_STATUS");
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLT00100GSGSBCodeDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        
        public List<GLT00100GSCurrencyDTO> GetCurrencyList()
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            List<GLT00100GSCurrencyDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_CURRENCY_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLT00100GSCurrencyDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        
        public List<GLT00100GSCenterDTO> GetCenterList()
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            List<GLT00100GSCenterDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_CENTER_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLT00100GSCenterDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #endregion

        #region logmethodhelper

        private void ShowLogDebug(string query, DbParameterCollection parameters)
        {
            var paramValues = string.Join(", ", parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} '{p.Value}'"));
            _logger.LogDebug($"EXEC {query} {paramValues}");
        }

        private void ShowLogError(Exception ex)
        {
            _logger.LogError(ex);
        }

        #endregion

    }
}