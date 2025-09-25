using PMT04200Back;
using PMT04200Common.Loggers;
using PMT04200Common.DTOs;
using PMT04200Common;
using R_BackEnd;
using R_Common;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;
using System.Transactions;
using PMT04200Common.DTOs;
using R_CommonFrontBackAPI;

namespace PMT04200Back
{
    public class PMT04200Cls
    {
        RSP_PM_SAVE_CA_WT_CUST_RECEIPTResources.Resources_Dummy_Class _loRSP01 = new RSP_PM_SAVE_CA_WT_CUST_RECEIPTResources.Resources_Dummy_Class();
        RSP_PM_SUBMIT_CA_WT_CUST_RECEIPTResources.Resources_Dummy_Class _loRSP02 = new RSP_PM_SUBMIT_CA_WT_CUST_RECEIPTResources.Resources_Dummy_Class();
        
        private LoggerPMT04200 _logger;

        private readonly ActivitySource _activitySource;

        public PMT04200Cls()
        {
            _logger = LoggerPMT04200.R_GetInstanceLogger();
            _activitySource = PMT04200ActivityInitSourceBase.R_GetInstanceActivitySource();
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
        
        public List<PMT04200DTO> GetJournalList(PMT04200ParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            List<PMT04200DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_SEARCH_CA_WT_CUST_RECEIPT_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, ContextConstant.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCUSTOMER_ID", DbType.String, 50, poEntity.CCUSTOMER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 50, poEntity.CPERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CSTATUS", DbType.String, 50, poEntity.CSTATUS);
                loDb.R_AddCommandParameter(loCmd, "@CSEARCH_TEXT", DbType.String, 50, poEntity.CSEARCH_TEXT);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPAYMENT_TYPE", DbType.String, 50, ContextConstant.CPAYMENT_TYPE);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMT04200DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        
        public List<PMT04200AllocationGridDTO> GetJournalAllocationGridList (PMT04200ParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            List<PMT04200AllocationGridDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_ALLOCATION_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, ContextConstant.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMT04200AllocationGridDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        

        public PMT04200DTO GetTransactionDisplay(PMT04200DTO poEntity)
        {
            var loEx = new R_Exception();
            PMT04200DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_CA_WT_CUST_RECEIPT";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 100, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 3, poEntity.CLANGUAGE_ID);


                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMT04200DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
    
            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        
        public PMT04200DTO SaveJournal(PMT04200DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("SaveJournal");
            var loEx = new R_Exception();
            PMT04200DTO loRtn = null;

            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    SavingJournalSP(poNewEntity, poCRUDMode);

                    transactionScope.Complete();
                }

                loRtn = GetTransactionDisplay(poNewEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        
         private void SavingJournalSP(PMT04200DTO poNewEntity, eCRUDMode poCRUDMode)
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

                lcQuery = "RSP_PM_SAVE_CA_WT_CUST_RECEIPT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poNewEntity.CUSER_ID);
                loDB.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 20, poNewEntity.CACTION);
                loDB.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poNewEntity.CREC_ID);
                loDB.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);
                loDB.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, ContextConstant.VAR_TRANS_CODE);

                loDB.R_AddCommandParameter(loCmd, "@CPAYMENT_TYPE", DbType.String, 2, "WT");
                loDB.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 8, poNewEntity.CREF_DATE);
                loDB.R_AddCommandParameter(loCmd, "@CCUST_SUPP_ID", DbType.String, 20, poNewEntity.CCUST_SUPP_ID);
                loDB.R_AddCommandParameter(loCmd, "@CLOI_AGRMT_ID", DbType.String, 50, poNewEntity.CLOI_AGRMT_ID);
                loDB.R_AddCommandParameter(loCmd, "@CCB_CODE", DbType.String, 8, poNewEntity.CCB_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CCB_ACCOUNT_NO", DbType.String, 20, poNewEntity.CCB_ACCOUNT_NO);
                loDB.R_AddCommandParameter(loCmd, "@CDOC_NO", DbType.String, 30, poNewEntity.CDOC_NO);
                loDB.R_AddCommandParameter(loCmd, "@CDOC_DATE", DbType.String, 8, poNewEntity.CDOC_DATE);
                loDB.R_AddCommandParameter(loCmd, "@CTRANS_DESC", DbType.String, int.MaxValue, poNewEntity.CTRANS_DESC);
                loDB.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 3, poNewEntity.CCURRENCY_CODE);

                loDB.R_AddCommandParameter(loCmd, "@NTRANS_AMOUNT", DbType.Decimal, 50, poNewEntity.NTRANS_AMOUNT);
                loDB.R_AddCommandParameter(loCmd, "@NLBASE_RATE", DbType.Decimal, 50, poNewEntity.NLBASE_RATE);
                loDB.R_AddCommandParameter(loCmd, "@NLCURRENCY_RATE", DbType.Decimal, 50, poNewEntity.NLCURRENCY_RATE);
                loDB.R_AddCommandParameter(loCmd, "@NBBASE_RATE", DbType.Decimal, 50, poNewEntity.NBBASE_RATE);
                loDB.R_AddCommandParameter(loCmd, "@NBCURRENCY_RATE", DbType.Decimal, 50, poNewEntity.NBCURRENCY_RATE);

                loDB.R_AddCommandParameter(loCmd, "@CSMODULE", DbType.String, 10, "PM");
                loDB.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP_CODE", DbType.String, 20, poNewEntity.CCASH_FLOW_GROUP_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CCASH_FLOW_CODE", DbType.String, 20, poNewEntity.CCASH_FLOW_CODE);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _logger.LogDebug("EXEC RSP_CB_SAVE_CHEQUE_HD {@poParameter}", loDbParam);

                    var loDataTable = loDB.SqlExecQuery(loConn, loCmd, false);

                    var loTempResult = R_Utility.R_ConvertTo<PMT04200DTO>(loDataTable).FirstOrDefault();

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
                _logger.LogError(loEx);
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
        
        public void UpdateJournalStatus(PMT04200UpdateStatusDTO poEntity)
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
                    _logger.LogDebug("EXEC RSP_CB_UPDATE_TRANS_HD_STATUS {@Parameters}", loDbParam);

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
                _logger.LogError(loException);
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

        public void SubmitCashReceiptSP(PMT04200UpdateStatusDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("SubmitCashReceiptSP");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_PM_SUBMIT_CA_WT_CUST_RECEIPT";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, int.MaxValue, poEntity.CREC_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _logger.LogDebug("EXEC RSP_PM_SUBMIT_CA_WT_CUST_RECEIPT {@Parameters}", loDbParam);

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
                _logger.LogError(loException);
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



