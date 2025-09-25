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
    public class GLT00110Cls : R_IBatchProcess
    {

        private LoggerGLT00100 _logger;

        private readonly ActivitySource _activitySource;
        
        public GLT00110Cls()
        {
            _logger = LoggerGLT00100.R_GetInstanceLogger();
            _activitySource = GLT00100Activity.R_GetInstanceActivitySource();
        }

        #region Batch Proses
        
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            var loDb = new R_Db();

            try
            {
                if (loDb.R_TestConnection() == false)
                {
                    loEx.Add("01", "Database Connection Failed");
                    goto EndBlock;
                }

                var loTask = Task.Run(() =>
                {
                    _BatchProcess(poBatchProcessPar);
                });

                while (!loTask.IsCompleted)
                {
                    Thread.Sleep(100);
                }

                if (loTask.IsFaulted)
                {
                    loEx.Add(loTask.Exception.InnerException != null ?
                        loTask.Exception.InnerException :
                        loTask.Exception);

                    goto EndBlock;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

        EndBlock:

            loEx.ThrowExceptionIfErrors();
        }
        
        private async Task _BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            R_Db loDb = new R_Db();
            DbCommand loCmd = null;
            DbConnection loConn = null;
            var lcQuery = "";

            try
            {
                //// must delay for wait this method is completed in syncronous
                //await Task.Delay(100);

                //var loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<LMM06501ErrorValidateDTO>>(poBatchProcessPar.BigObject);
                //var loObject = R_Utility.R_ConvertCollectionToCollection<LMM06501ErrorValidateDTO, LMM06501RequestDTO>(loTempObject);

                //var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.CPROPERTY_ID)).FirstOrDefault().Value;
                //var lcPropertyId = ((System.Text.Json.JsonElement)loVar).GetString();

                //loConn = loDb.GetConnection();
                //loCmd = loDb.GetCommand();

                //lcQuery = @"CREATE TABLE #GLT0100_JOURNAL_DETAIL 
                //            (
                //                CGLACCOUNT_NO   VARCHAR(20),
                //                CCENTER_CODE    VARCHAR(10),
                //                CDBCR           CHAR(1),
                //                NAMOUNT         NUMERIC(19, 2),
                //                CDETAIL_DESC    NVARCHAR(200),
                //                CDOCUMENT_NO    VARCHAR(20),
                //                CDOCUMENT_DATE  VARCHAR(8)
                //            )";
                ShowLogDebug(lcQuery, loCmd.Parameters);
                //loDb.SqlExecNonQuery(lcQuery, loConn, false);

                //loDb.R_BulkInsert<LMM06501RequestDTO>((SqlConnection)loConn, "#GLT0100_JOURNAL_DETAIL", loObject);

                lcQuery = "RSP_GL_SAVE_JOURNAL";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 100, poBatchProcessPar.Key.KEY_GUID);

                loDb.SqlExecNonQuery(loConn, loCmd, false);
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
                    if (!(loConn.State == ConnectionState.Closed))
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

            if (loEx.Haserror)
            {
                lcQuery = "INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE) VALUES" +
                    string.Format("('{0}', '{1}', ", poBatchProcessPar.Key.COMPANY_ID, poBatchProcessPar.Key.USER_ID) +
                    string.Format("'{0}', -1, '{1}')", poBatchProcessPar.Key.KEY_GUID, loEx.ErrorList[0].ErrDescp);
                loDb.SqlExecNonQuery(lcQuery);

                lcQuery = string.Format("EXEC RSP_WriteUploadProcessStatus '{0}', ", poBatchProcessPar.Key.COMPANY_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.USER_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.KEY_GUID) +
                   string.Format("100, '{0}', 9", loEx.ErrorList[0].ErrDescp);

                loDb.SqlExecNonQuery(lcQuery);
            }
        }
        
        #endregion
        
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
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            GLT00110DTO loRtn = poEntity.HeaderData;

            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
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
                    transactionScope.Complete();
                }
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