using GLM00200COMMON;
using GLM00200COMMON.DTO_s.Helper_DTO_s;
using GLM00200COMMON.Loggers;
using R_BackEnd;
using R_Common;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;
using System.Transactions;

namespace GLM00200BACK
{
    public class GLM00200GeneralCls
    {
        private RSP_GL_SAVE_RECURRING_JRNResources.Resources_Dummy_Class _saveRecurringRsc = new();

        private RSP_GL_PROCESS_RECURRINGResources.Resources_Dummy_Class _processRecurringRsc = new();

        private RSP_GL_UPDATE_RECURRING_STATUSResources.Resources_Dummy_Class _updateRecurringStatusRsc = new();

        private LoggerGLM00200 _logger;

        private readonly ActivitySource _activitySource;

        public GLM00200GeneralCls()
        {
            _logger = LoggerGLM00200.R_GetInstanceLogger();
            _activitySource = GLM00200Activity.R_GetInstanceActivitySource();
        }

        public AllInitRecordDTO GetAllInitRecord(GeneralParamDTO poParam)
        {
            var loEx = new R_Exception();
            AllInitRecordDTO loRtn = new AllInitRecordDTO();

            try
            {
                loRtn.COMPANY_INFO = GetCompanyInfoRecord(poParam);
                var loSystemParam = GetGLSysParamRecord(poParam);
                loRtn.GL_SYSTEM_PARAM = loSystemParam;
                if (loSystemParam != null)
                {
                    loRtn.CURRENT_PERIOD_START_DATE = GetPeriodDtInfoRecord(poParam, new PeriodDTInfoDTO { CCYEAR = loSystemParam.CCURRENT_PERIOD_YY, CPERIOD_NO = loSystemParam.CCURRENT_PERIOD_MM });
                    loRtn.SOFT_PERIOD_START_DATE = GetPeriodDtInfoRecord(poParam, new PeriodDTInfoDTO { CCYEAR = loSystemParam.CSOFT_PERIOD_YY, CPERIOD_NO = loSystemParam.CSOFT_PERIOD_MM });
                }
                loRtn.IUNDO_COMMIT_JRN = GetIUndoCommitJrnRecord(poParam);
                loRtn.GSM_TRANSACTION_CODE = GetTransCodeRecord(poParam);
                loRtn.PERIOD_YEAR = GetPeriodYearRangeRecord(poParam);
                loRtn.DTODAY = GetTodayRecord(poParam).DTODAY;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        private CompanyDTO GetCompanyInfoRecord(GeneralParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loException = new R_Exception();
            CompanyDTO loResult = null;

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection();
                DbCommand loCmd = loDb.GetCommand();

                string lcQuery = "RSP_GS_GET_COMPANY_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<CompanyDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        private GLSysParamDTO GetGLSysParamRecord(GeneralParamDTO poParam)
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

        private PeriodDTInfoDTO GetPeriodDtInfoRecord(GeneralParamDTO poGeneralParam, PeriodDTInfoDTO poPeriodDtInfoParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loException = new R_Exception();
            PeriodDTInfoDTO loResult = null;
            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection();
                DbCommand loCmd = loDb.GetCommand();

                string lcQuery = "RSP_GS_GET_PERIOD_DT_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poGeneralParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, int.MaxValue, poPeriodDtInfoParam.CCYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_NO", DbType.String, int.MaxValue, poPeriodDtInfoParam.CPERIOD_NO);
                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loRtnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PeriodDTInfoDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        private IUndoCommitJrnDTO GetIUndoCommitJrnRecord(GeneralParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loException = new R_Exception();
            IUndoCommitJrnDTO loResult = null;
            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection();
                DbCommand loCmd = loDb.GetCommand();

                string lcQuery = "RSP_GL_GET_SYSTEM_ENABLE_OPTION_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@COPTION_CODE", DbType.String, int.MaxValue, RecurringJournalContext.COPTION_CODE);
                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loRtnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<IUndoCommitJrnDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        private TransCodeDTO GetTransCodeRecord(GeneralParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loException = new R_Exception();
            TransCodeDTO loResult = null;
            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection();
                DbCommand loCmd = loDb.GetCommand();

                string lcQuery = "RSP_GS_GET_TRANS_CODE_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, int.MaxValue, RecurringJournalContext.TRANSACTION_CODE);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<TransCodeDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        private PeriodDTO GetPeriodYearRangeRecord(GeneralParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loException = new R_Exception();
            PeriodDTO loResult = null;
            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection();
                DbCommand loCmd = loDb.GetCommand();

                string lcQuery = "RSP_GS_GET_PERIOD_YEAR_RANGE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, int.MaxValue, "");
                loDb.R_AddCommandParameter(loCmd, "@CMODE", DbType.String, int.MaxValue, "");

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PeriodDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public List<StatusDTO> GetStatusList(GeneralParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loException = new R_Exception();
            List<StatusDTO> loResult = null;
            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection();
                DbCommand loCmd = loDb.GetCommand();

                string lcQuery = "RSP_GS_GET_GSB_CODE_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CAPPLICATION", DbType.String, int.MaxValue, RecurringJournalContext.CAPPLICATION_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCLASS_ID", DbType.String, int.MaxValue, RecurringJournalContext.CCLASS_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, int.MaxValue, poParam.CLANG_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<StatusDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<CurrencyDTO> GetCurrencyList(GeneralParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<CurrencyDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_GS_GET_CURRENCY_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poParam.CUSER_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<CurrencyDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public CurrencyRateResultDTO GetLastCurrencyRecord(CurrencyRateResultDTO poEntity)
        {
            var loEx = new R_Exception();
            CurrencyRateResultDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_LAST_CURRENCY_RATE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, int.MaxValue, poEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CRATETYPE_CODE", DbType.String, int.MaxValue, poEntity.CRATETYPE_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CRATE_DATE", DbType.String, int.MaxValue, poEntity.CRATE_DATE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<CurrencyRateResultDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

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
