using CBT00200COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Transactions;

namespace CBT00200BACK
{
    public class CBT00200Cls
    {
        RSP_CB_DELETE_TRANS_JRNResources.Resources_Dummy_Class _loRSP01 = new RSP_CB_DELETE_TRANS_JRNResources.Resources_Dummy_Class();
        RSP_CB_SAVE_CA_WT_JOURNALResources.Resources_Dummy_Class _loRSP02 = new RSP_CB_SAVE_CA_WT_JOURNALResources.Resources_Dummy_Class();
        RSP_CB_SAVE_TRANS_JRNResources.Resources_Dummy_Class _loRSP03 = new RSP_CB_SAVE_TRANS_JRNResources.Resources_Dummy_Class();
        RSP_CB_UPDATE_TRANS_HD_STATUSResources.Resources_Dummy_Class _loRSP04 = new RSP_CB_UPDATE_TRANS_HD_STATUSResources.Resources_Dummy_Class();
        RSP_CB_SAVE_CA_WT_JOURNAL_DEPOSITResources.Resources_Dummy_Class _loRSP05 = new RSP_CB_SAVE_CA_WT_JOURNAL_DEPOSITResources.Resources_Dummy_Class();

        private LoggerCBT00200 _Logger;
        private readonly ActivitySource _activitySource;
        public CBT00200Cls()
        {
            _Logger = LoggerCBT00200.R_GetInstanceLogger();
            _activitySource = CBT00200ActivityInitSourceBase.R_GetInstanceActivitySource();
        }
        public List<CBT00200DTO> GetJournalList(CBT00200ParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetJournalList");
            var loEx = new R_Exception();
            List<CBT00200DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_CB_SEARCH_TRANS_HD_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, ContextConstant.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 50, poEntity.CPERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CSTATUS", DbType.String, 50, poEntity.CSTATUS);
                loDb.R_AddCommandParameter(loCmd, "@CSEARCH_TEXT", DbType.String, 50, poEntity.CSEARCH_TEXT);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GL_SEARCH_JOURNAL_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<CBT00200DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public CBT00200DTO GetJournalDisplay(CBT00200DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetJournalDisplay");
            var loEx = new R_Exception();
            CBT00200DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_CB_GET_TRANS_HD";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, int.MaxValue, string.IsNullOrWhiteSpace(poEntity.CREC_ID) ? "" : poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPARTMENT_CODE", DbType.String, 20, string.IsNullOrWhiteSpace(poEntity.PARAM_DEPT_CODE) ? "" : poEntity.PARAM_DEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 10, ContextConstant.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, string.IsNullOrWhiteSpace(poEntity.PARAM_REF_NO) ? "" : poEntity.PARAM_REF_NO);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<CBT00200DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public CBT00200DTO SaveJournal(CBT00200DTO poNewEntity, eCRUDMode poCRUDMode, ePARAM_CALLER poPARAMCALLER)
        {
            using Activity activity = _activitySource.StartActivity("SaveJournal");
            var loEx = new R_Exception();
            CBT00200DTO loRtn = null;

            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (poPARAMCALLER == ePARAM_CALLER.TRANSACTION)
                    {
                        SavingJournalSP(poNewEntity, poCRUDMode);
                    }
                    else
                    {
                        SavingJournalDepositSP(poNewEntity, poCRUDMode);
                    }

                    transactionScope.Complete();
                }

                loRtn = GetJournalDisplay(poNewEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        private void SavingJournalSP(CBT00200DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            R_Db loDB = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            try
            {
                // set action 
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "NEW";
                    poNewEntity.CREC_ID = "";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";
                }

                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);

                lcQuery = "RSP_CB_SAVE_CA_WT_JOURNAL";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);
                loDB.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDB.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poNewEntity.CREC_ID);
                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, ContextConstant.VAR_TRANS_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CPAYMENT_TYPE", DbType.String, 50, "CA");

                loDB.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poNewEntity.CREF_NO);
                loDB.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 8, poNewEntity.CREF_DATE);
                loDB.R_AddCommandParameter(loCmd, "@CDOC_NO", DbType.String, 50, poNewEntity.CDOC_NO);
                loDB.R_AddCommandParameter(loCmd, "@CDOC_DATE", DbType.String, 8, poNewEntity.CDOC_DATE);

                loDB.R_AddCommandParameter(loCmd, "@CCB_CODE", DbType.String, 8, poNewEntity.CCB_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CCB_ACCOUNT_NO", DbType.String, 50, poNewEntity.CCB_ACCOUNT_NO);
                loDB.R_AddCommandParameter(loCmd, "@CTRANS_DESC", DbType.String, int.MaxValue, poNewEntity.CTRANS_DESC);
                loDB.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 50, poNewEntity.CCURRENCY_CODE);
                loDB.R_AddCommandParameter(loCmd, "@NTRANS_AMOUNT", DbType.Decimal, 50, poNewEntity.NTRANS_AMOUNT);
                loDB.R_AddCommandParameter(loCmd, "@NLBASE_RATE", DbType.Decimal, 50, poNewEntity.NLBASE_RATE);
                loDB.R_AddCommandParameter(loCmd, "@NLCURRENCY_RATE", DbType.Decimal, 50, poNewEntity.NLCURRENCY_RATE);
                loDB.R_AddCommandParameter(loCmd, "@NBBASE_RATE", DbType.Decimal, 50, poNewEntity.NBBASE_RATE);
                loDB.R_AddCommandParameter(loCmd, "@NBCURRENCY_RATE", DbType.Decimal, 50, poNewEntity.NBCURRENCY_RATE);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_CB_SAVE_CA_WT_JOURNAL {@poParameter}", loDbParam);

                    var loDataTable = loDB.SqlExecQuery(loConn, loCmd, false);

                    var loTempResult = R_Utility.R_ConvertTo<CBT00200DTO>(loDataTable).FirstOrDefault();

                    poNewEntity.CREC_ID = loTempResult.CREC_ID;
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
                _Logger.LogError(loEx);
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
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        private void SavingJournalDepositSP(CBT00200DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            R_Db loDB = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            try
            {
                // set action 
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "NEW";
                    poNewEntity.CREC_ID = "";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";
                }

                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);

                lcQuery = "RSP_CB_SAVE_CA_WT_JOURNAL_DEPOSIT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);
                loDB.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDB.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poNewEntity.CREC_ID);
                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, ContextConstant.VAR_TRANS_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CPAYMENT_TYPE", DbType.String, 50, "CA");

                loDB.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poNewEntity.CREF_NO);
                loDB.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 8, poNewEntity.CREF_DATE);
                loDB.R_AddCommandParameter(loCmd, "@CDOC_NO", DbType.String, 50, poNewEntity.CDOC_NO);
                loDB.R_AddCommandParameter(loCmd, "@CDOC_DATE", DbType.String, 8, poNewEntity.CDOC_DATE);

                loDB.R_AddCommandParameter(loCmd, "@CCB_CODE", DbType.String, 8, poNewEntity.CCB_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CCB_ACCOUNT_NO", DbType.String, 50, poNewEntity.CCB_ACCOUNT_NO);
                loDB.R_AddCommandParameter(loCmd, "@CTRANS_DESC", DbType.String, int.MaxValue, poNewEntity.CTRANS_DESC);
                loDB.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 50, poNewEntity.CCURRENCY_CODE);
                loDB.R_AddCommandParameter(loCmd, "@NTRANS_AMOUNT", DbType.Decimal, 50, poNewEntity.NTRANS_AMOUNT);
                loDB.R_AddCommandParameter(loCmd, "@NLBASE_RATE", DbType.Decimal, 50, poNewEntity.NLBASE_RATE);
                loDB.R_AddCommandParameter(loCmd, "@NLCURRENCY_RATE", DbType.Decimal, 50, poNewEntity.NLCURRENCY_RATE);
                loDB.R_AddCommandParameter(loCmd, "@NBBASE_RATE", DbType.Decimal, 50, poNewEntity.NBBASE_RATE);
                loDB.R_AddCommandParameter(loCmd, "@NBCURRENCY_RATE", DbType.Decimal, 50, poNewEntity.NBCURRENCY_RATE);

                loDB.R_AddCommandParameter(loCmd, "@CSTRANS_CODE", DbType.String, 10, poNewEntity.PARAM_CALLER_TRANS_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CSREF_NO", DbType.String, 30, poNewEntity.PARAM_CALLER_REF_NO);
                loDB.R_AddCommandParameter(loCmd, "@CSMODULE", DbType.String, 10, poNewEntity.PARAM_CALLER_ID.Substring(0, 2));
                loDB.R_AddCommandParameter(loCmd, "@CDP_GLACCOUNT_NO", DbType.String, 20, poNewEntity.PARAM_GLACCOUNT_NO);
                loDB.R_AddCommandParameter(loCmd, "@CDP_CENTER_CODE", DbType.String, 8, poNewEntity.PARAM_CENTER_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CDP_CASH_FLOW_GROUP_CODE", DbType.String, 20, poNewEntity.PARAM_CASH_FLOW_GROUP_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CDP_CASH_FLOW_CODE", DbType.String, 20, poNewEntity.PARAM_CASH_FLOW_CODE);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_CB_SAVE_CA_WT_JOURNAL_DEPOSIT {@poParameter}", loDbParam);

                    var loDataTable = loDB.SqlExecQuery(loConn, loCmd, false);

                    var loTempResult = R_Utility.R_ConvertTo<CBT00200DTO>(loDataTable).FirstOrDefault();

                    poNewEntity.CREC_ID = loTempResult.CREC_ID;
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
                _Logger.LogError(loEx);
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
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        public void UpdateJournalStatus(CBT00200UpdateStatusDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("UpdateJournalStatus");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_CB_UPDATE_TRANS_HD_STATUS";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CAPPROVE_BY", DbType.String, 20, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID_LIST", DbType.String, int.MaxValue, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CNEW_STATUS", DbType.String, 20, poEntity.CNEW_STATUS);
                loDb.R_AddCommandParameter(loCmd, "@LAUTO_COMMIT", DbType.Boolean, 20, poEntity.LAUTO_COMMIT);
                loDb.R_AddCommandParameter(loCmd, "@LUNDO_COMMIT", DbType.Boolean, 20, poEntity.LUNDO_COMMIT);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_CB_UPDATE_TRANS_HD_STATUS {@Parameters}", loDbParam);

                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }

                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Logger.LogError(loException);
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
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}