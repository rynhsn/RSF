using GLT00100COMMON;
using R_BackEnd;
using R_Common;
using System.Data.Common;
using System.Data;
using R_CommonFrontBackAPI;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Reflection;
using System.Transactions;

namespace GLT00100BACK
{
    public class GLT00110Cls
    {
        private RSP_GL_SAVE_JOURNALResources.Resources_Dummy_Class saveJournal = new();

        private RSP_GL_UPDATE_JOURNAL_STATUSResources.Resources_Dummy_Class updateJournalnew = new();

        private RSP_PM_SAVE_DEPOSIT_DTResources.Resources_Dummy_Class saveDeposit = new();

        private LoggerGLT00100 _logger;

        private readonly ActivitySource _activitySource;

        public GLT00110Cls()
        {
            _logger = LoggerGLT00100.R_GetInstanceLogger();
            _activitySource = GLT00100Activity.R_GetInstanceActivitySource();
        }

        public GLT00110LastCurrencyRateDTO GetLastCurrency(GLT00110LastCurrencyRateDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            GLT00110LastCurrencyRateDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_LAST_CURRENCY_RATE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 50, string.IsNullOrWhiteSpace(poEntity.CCURRENCY_CODE) ? "" : poEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CRATETYPE_CODE", DbType.String, 50, poEntity.CRATETYPE_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CRATE_DATE", DbType.String, 50, string.IsNullOrWhiteSpace(poEntity.CRATE_DATE) ? "" : poEntity.CRATE_DATE);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLT00110LastCurrencyRateDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public GLT00110DTO GetJournalDisplay(GLT00110DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            GLT00110DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GL_GET_JOURNAL";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 100, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLT00110DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public GLT00110DTO SaveJournal(GLT00110HeaderDetailDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            GLT00110DTO loRtn = null;

            try
            {
                using TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required);
                loRtn = SaveJournalSP(poEntity);
                _logger.LogDebug($"Inerting to PM Deposit DT");
                if (poEntity.HeaderData.CACTION == "NEW" && poEntity.HeaderData.CSOURCE_MODULE == "PM")
                {
                    poEntity.HeaderData.CREC_ID=loRtn.CREC_ID;
                    GenerateJournalDepositSP(loRtn);
                }
                transactionScope.Complete();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;

        }

        public GLT00110DTO SaveJournalSP(GLT00110HeaderDetailDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            GLT00110DTO loRtn = poEntity.HeaderData;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                //Bulk Insert Data
                lcQuery = @"CREATE TABLE #GLT0100_JOURNAL_DETAIL 
                            (
                                CGLACCOUNT_NO   VARCHAR(20),
                                CCENTER_CODE    VARCHAR(10),
                                CDBCR           CHAR(1),
                                NAMOUNT         NUMERIC(19, 2),
                                CDETAIL_DESC    NVARCHAR(200),
                                CDOCUMENT_NO    VARCHAR(20),
                                CDOCUMENT_DATE  VARCHAR(8),
                                CINPUT_TYPE     CHAR(1)  
                            )";
                ShowLogDebug(lcQuery, loCmd.Parameters);//log create
                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                _logger.LogDebug($"INSERT INTO #GLT0100_JOURNAL_DETAIL VALUES {poEntity.DetailData}");//log insert
                loDb.R_BulkInsert<GLT00111DTO>((SqlConnection)loConn, "#GLT0100_JOURNAL_DETAIL", poEntity.DetailData);

                lcQuery = "RSP_GL_SAVE_JOURNAL";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CJRN_ID", DbType.String, int.MaxValue, poEntity.HeaderData.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, int.MaxValue, poEntity.HeaderData.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, int.MaxValue, poEntity.HeaderData.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, int.MaxValue, ContextConstant.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, int.MaxValue, poEntity.HeaderData.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_NO", DbType.String, int.MaxValue, poEntity.HeaderData.CDOC_NO);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_DATE", DbType.String, int.MaxValue, poEntity.HeaderData.CDOC_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, int.MaxValue, poEntity.HeaderData.CREF_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CREVERSE_DATE", DbType.String, int.MaxValue, "");
                loDb.R_AddCommandParameter(loCmd, "@LREVERSE", DbType.Boolean, 10, poEntity.HeaderData.LREVERSE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_DESC", DbType.String, int.MaxValue, poEntity.HeaderData.CTRANS_DESC);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, int.MaxValue, poEntity.HeaderData.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@NLBASE_RATE", DbType.Decimal, int.MaxValue, poEntity.HeaderData.NLBASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NLCURRENCY_RATE", DbType.Decimal, int.MaxValue, poEntity.HeaderData.NLCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NBBASE_RATE", DbType.Decimal, int.MaxValue, poEntity.HeaderData.NBBASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NBCURRENCY_RATE", DbType.Decimal, int.MaxValue, poEntity.HeaderData.NBCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NPRELIST_AMOUNT", DbType.Decimal, int.MaxValue, poEntity.HeaderData.NPRELIST_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CSOURCE_TRANS_CODE", DbType.String, int.MaxValue, poEntity.HeaderData.CSOURCE_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CSOURCE_REF_NO", DbType.String, int.MaxValue, poEntity.HeaderData.CSOURCE_REF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CSOURCE_MODULE", DbType.String, int.MaxValue, poEntity.HeaderData.CSOURCE_MODULE);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    ShowLogDebug(lcQuery, loCmd.Parameters);
                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);

                    var loTempResult = R_Utility.R_ConvertTo<ConvertRecID>(loDataTable).FirstOrDefault();

                    loRtn.CREC_ID = loTempResult.CJRN_ID;
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
                    if (loConn.State != System.Data.ConnectionState.Closed)
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
            return loRtn;
        }

        private void GenerateJournalDepositSP(GLT00110DTO poNewEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            R_Db loDB = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);
                lcQuery = "RSP_PM_SAVE_DEPOSIT_DT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, R_BackGlobalVar.COMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, int.MaxValue, poNewEntity.CDEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, int.MaxValue, poNewEntity.CSOURCE_TRANS_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, int.MaxValue, poNewEntity.CSOURCE_REF_NO);
                loDB.R_AddCommandParameter(loCmd, "@CSEQ_NO", DbType.String, int.MaxValue, poNewEntity.CSOURCE_SEQ_NO);
                loDB.R_AddCommandParameter(loCmd, "@CLINK_MODULE", DbType.String, int.MaxValue, "GS");
                loDB.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, int.MaxValue, poNewEntity.CREC_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, R_BackGlobalVar.USER_ID);
                try
                {
                    //Debug Logs
                    ShowLogDebug(lcQuery, loCmd.Parameters);
                    loDB.SqlExecNonQuery(loConn, loCmd, false);
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
                    {
                        loConn.Close();
                    }
                    loConn.Dispose();
                }
                if (loCmd != null)
                {
                    loCmd.Dispose();
                    loCmd = null;
                }
            }
            loEx.ThrowExceptionIfErrors();
        }


        #region log activity

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
    internal class ConvertRecID
    {
        public string CJRN_ID { get; set; }
    }





}