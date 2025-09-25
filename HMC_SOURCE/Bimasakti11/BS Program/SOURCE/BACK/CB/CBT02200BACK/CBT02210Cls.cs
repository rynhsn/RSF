using CBT02200BACK.OpenTelemetry;
using CBT02200COMMON.DTO.CBT02210;
using CBT02200COMMON.Logger;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBT02200COMMON.DTO.CBT02200;

namespace CBT02200BACK
{
    public class CBT02210Cls : R_BusinessObject<CBT02210ParameterDTO>
    {
        RSP_CB_SAVE_CHEQUE_HDResources.Resources_Dummy_Class _locSaveRsp = new RSP_CB_SAVE_CHEQUE_HDResources.Resources_Dummy_Class();
        RSP_CB_SAVE_CHEQUE_HD_DEPOSITResources.Resources_Dummy_Class _locSaveDepositRsp = new RSP_CB_SAVE_CHEQUE_HD_DEPOSITResources.Resources_Dummy_Class();

        //RSP_CB_UPDATE_TRANS_HD_STATUSResources.Resources_Dummy_Class _loUpdateStatus = new RSP_CB_UPDATE_TRANS_HD_STATUSResources.Resources_Dummy_Class();

        private LoggerCBT02210 _logger;
        private readonly ActivitySource _activitySource;
        public CBT02210Cls()
        {
            _logger = LoggerCBT02210.R_GetInstanceLogger();
            _activitySource = CBT02210ActivitySourceBase.R_GetInstanceActivitySource();
        }

        //public CBT02210DTO GetChequeEntryHeader(GetCBT02210ParameterDTO poParameter)
        //{
        //    using Activity activity = _activitySource.StartActivity("GetChequeEntryHeader");
        //    R_Exception loException = new R_Exception();
        //    R_Db loDb = new R_Db();
        //    DbConnection loConn = null;
        //    string lcQuery;
        //    CBT02210DTO loResult = null;
        //    DbCommand loCmd = null;

        //    try
        //    {
        //        loCmd = loDb.GetCommand();
        //        loConn = loDb.GetConnection();

        //        lcQuery = $"EXEC RSP_CB_GET_TRANS_HD " +
        //            $"@CLOGIN_COMPANY_ID, " +
        //            $"@CLOGIN_USER_ID, " +
        //            $"@CCheque_ID, " +
        //            $"@CLANGUAGE_ID";

        //        loCmd.CommandText = lcQuery;

        //        loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
        //        loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poParameter.CLOGIN_USER_ID);
        //        loDb.R_AddCommandParameter(loCmd, "@CCheque_ID", DbType.String, 50, poParameter.CCheque_ID);
        //        loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParameter.CLANGUAGE_ID);

        //        var loDbParam = loCmd.Parameters.Cast<DbParameter>()
        //            .Where(x =>
        //            x != null && x.ParameterName.StartsWith("@"))
        //            .Select(x => x.Value);

        //        _logger.LogDebug("EXEC RSP_CB_GET_TRANS_HD {@Parameters} || GetChequeEntryHeader(Cls) ", loDbParam);

        //        var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

        //        loResult = R_Utility.R_ConvertTo<CBT02210DTO>(loDataTable).FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        loException.Add(ex);
        //        _logger.LogError(loException);
        //    }

        //    loException.ThrowExceptionIfErrors();

        //    return loResult;
        //}

        protected override CBT02210ParameterDTO R_Display(CBT02210ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            R_Exception loException = new R_Exception();
            CBT02210ParameterDTO loResult = new CBT02210ParameterDTO();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_CB_GET_CHEQUE_HD " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CLOGIN_USER_ID, " +
                    $"@CREC_ID, " +
                    $"@CLANGUAGE_ID, " +
                    $"@CDEPT_CODE, " +
                    $"@CTRANS_CODE, " +
                    $"@CREF_NO";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.Data.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.Data.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poEntity.Data.CREF_NO);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_CB_GET_CHEQUE_HD {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult.Data = R_Utility.R_ConvertTo<CBT02210DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(CBT02210ParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            R_Exception loException = new R_Exception();
            CBT02210DTO loResult = null;
            try
            {
                if (poNewEntity.LPAGE)
                {
                    loResult = RefundDepositTransactionEntrySave(poNewEntity);
                }
                else
                {
                    loResult = TransactionEntrySave(poNewEntity);
                }
                if (loResult != null)
                {
                    poNewEntity.Data.CREC_ID = loResult.CREC_ID;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
        }

        private CBT02210DTO TransactionEntrySave(CBT02210ParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            CBT02210DTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = $"EXEC RSP_CB_SAVE_CHEQUE_HD " +
                                 $"@CLOGIN_USER_ID, " +
                                 $"@CACTION, " +
                                 $"@CREC_ID, " +
                                 $"@CLOGIN_COMPANY_ID, " +
                                 $"@CDEPT_CODE, " +
                                 $"@CTRANS_CODE, " +
                                 $"@CREF_NO, " +
                                 $"@CREF_DATE, " +
                                 $"@CCHEQUE_BANK, " +
                                 $"@CCHEQUE_NO, " +
                                 $"@CCHEQUE_DATE, " +
                                 $"@CDUE_DATE, " +
                                 $"@CCB_CODE, " +
                                 $"@CCB_ACCOUNT_NO, " +
                                 $"@CTRANS_DESC, " +
                                 $"@CCURRENCY_CODE, " +
                                 $"@NTRANS_AMOUNT, " +
                                 $"@NLBASE_RATE, " +
                                 $"@NLCURRENCY_RATE, " +
                                 $"@NBBASE_RATE, " +
                                 $"@NBCURRENCY_RATE";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poParameter.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poParameter.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParameter.Data.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.Data.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poParameter.Data.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 50, poParameter.Data.CREF_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CCHEQUE_BANK", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CCHEQUE_NO", DbType.String, 50, poParameter.Data.CCHEQUE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CCHEQUE_DATE", DbType.String, 50, poParameter.Data.CCHEQUE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CDUE_DATE", DbType.String, 50, poParameter.Data.CDUE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CCB_CODE", DbType.String, 50, poParameter.Data.CCB_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCB_ACCOUNT_NO", DbType.String, 50, poParameter.Data.CCB_ACCOUNT_NO);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_DESC", DbType.String, 50, poParameter.Data.CTRANS_DESC);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 50, poParameter.Data.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@NTRANS_AMOUNT", DbType.Int32, 50, poParameter.Data.NTRANS_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@NLBASE_RATE", DbType.Int32, 50, poParameter.Data.NLBASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NLCURRENCY_RATE", DbType.Int32, 50, poParameter.Data.NLCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NBBASE_RATE", DbType.Int32, 50, poParameter.Data.NBBASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NBCURRENCY_RATE", DbType.Int32, 50, poParameter.Data.NBCURRENCY_RATE);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_CB_SAVE_CHEQUE_HD {@Parameters} || R_Saving(Cls) ", loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                    loResult = R_Utility.R_ConvertTo<CBT02210DTO>(loDataTable).FirstOrDefault();
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
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        private CBT02210DTO RefundDepositTransactionEntrySave(CBT02210ParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            CBT02210DTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = $"EXEC RSP_CB_SAVE_CHEQUE_HD_DEPOSIT " +
                                 $"@CLOGIN_USER_ID, " +
                                 $"@CACTION, " +
                                 $"@CREC_ID, " +
                                 $"@CLOGIN_COMPANY_ID, " +
                                 $"@CDEPT_CODE, " +
                                 $"@CTRANS_CODE, " +
                                 $"@CREF_NO, " +
                                 $"@CREF_DATE, " +
                                 $"@CCHEQUE_BANK, " +
                                 $"@CCHEQUE_NO, " +
                                 $"@CCHEQUE_DATE, " +
                                 $"@CDUE_DATE, " +
                                 $"@CCB_CODE, " +
                                 $"@CCB_ACCOUNT_NO, " +
                                 $"@CTRANS_DESC, " +
                                 $"@CCURRENCY_CODE, " +
                                 $"@NTRANS_AMOUNT, " +
                                 $"@NLBASE_RATE, " +
                                 $"@NLCURRENCY_RATE, " +
                                 $"@NBBASE_RATE, " +
                                 $"@NBCURRENCY_RATE, " +
                                 $"@CCALLER_TRANS_CODE, " +
                                 $"@CCALLER_REF_NO, " +
                                 $"@CCALLER_ID, " +
                                 $"@CGLACCOUNT_NO, " +
                                 $"@CCENTER_CODE, " +
                                 $"@CCASH_FLOW_GROUP_CODE, " +
                                 $"@CCASH_FLOW_CODE";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poParameter.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poParameter.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParameter.Data.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.Data.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poParameter.Data.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 50, poParameter.Data.CREF_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CCHEQUE_BANK", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CCHEQUE_NO", DbType.String, 50, poParameter.Data.CCHEQUE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CCHEQUE_DATE", DbType.String, 50, poParameter.Data.CCHEQUE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CDUE_DATE", DbType.String, 50, poParameter.Data.CDUE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CCB_CODE", DbType.String, 50, poParameter.Data.CCB_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCB_ACCOUNT_NO", DbType.String, 50, poParameter.Data.CCB_ACCOUNT_NO);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_DESC", DbType.String, 50, poParameter.Data.CTRANS_DESC);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 50, poParameter.Data.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@NTRANS_AMOUNT", DbType.Int32, 50, poParameter.Data.NTRANS_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@NLBASE_RATE", DbType.Int32, 50, poParameter.Data.NLBASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NLCURRENCY_RATE", DbType.Int32, 50, poParameter.Data.NLCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NBBASE_RATE", DbType.Int32, 50, poParameter.Data.NBBASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NBCURRENCY_RATE", DbType.Int32, 50, poParameter.Data.NBCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@CCALLER_TRANS_CODE", DbType.String, 50, poParameter.CCALLER_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCALLER_REF_NO", DbType.String, 50, poParameter.CCALLER_REF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CCALLER_ID", DbType.String, 50, poParameter.CCALLER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CGLACCOUNT_NO", DbType.String, 50, poParameter.CGLACCOUNT_NO);
                loDb.R_AddCommandParameter(loCmd, "@CCENTER_CODE", DbType.String, 50, poParameter.CCENTER_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP_CODE", DbType.String, 50, poParameter.CCASH_FLOW_GROUP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_CODE", DbType.String, 50, poParameter.CCASH_FLOW_CODE);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_CB_SAVE_CHEQUE_HD_DEPOSIT {@Parameters} || R_Saving(Cls) ", loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                    loResult = R_Utility.R_ConvertTo<CBT02210DTO>(loDataTable).FirstOrDefault();
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
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public void UpdateStatus(UpdateStatusParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("UpdateStatus");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = $"EXEC RSP_CB_UPDATE_CHEQUE_HD_STATUS " +
                                 $"@CLOGIN_COMPANY_ID, " +
                                 $"@CLOGIN_USER_ID, " +
                                 $"@CREC_ID_LIST, " +
                                 $"@CNEW_STATUS";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poParameter.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID_LIST", DbType.String, 50, poParameter.CREC_ID_LIST);
                loDb.R_AddCommandParameter(loCmd, "@CNEW_STATUS", DbType.String, 50, poParameter.CNEW_STATUS);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_CB_UPDATE_CHEQUE_HD_STATUS {@Parameters} || UpdateStatus(Cls) ", loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
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
            }
            loException.ThrowExceptionIfErrors();
        }

        protected override void R_Deleting(CBT02210ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Deleting");
            R_Exception loException = new R_Exception();

            try
            {
                UpdateStatus(new UpdateStatusParameterDTO()
                {
                    CLOGIN_USER_ID = poEntity.CLOGIN_USER_ID,
                    CLOGIN_COMPANY_ID = poEntity.CLOGIN_COMPANY_ID,
                    CNEW_STATUS = "99",
                    CREC_ID_LIST = poEntity.Data.CREC_ID,
                });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
        }

        public RefreshCurrencyRateDTO RefreshCurrencyRate(RefreshCurrencyRateParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("RefreshCurrencyRate");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            string lcQuery;
            RefreshCurrencyRateDTO loResult = null;
            DbCommand loCmd = null;

            try
            {
                loCmd = loDb.GetCommand();
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_GS_GET_LAST_CURRENCY_RATE " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CCURRENCY_CODE, " +
                    $"@CRATETYPE_CODE, " +
                    $"@CREF_DATE";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 50, poParameter.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CRATETYPE_CODE", DbType.String, 50, poParameter.CRATETYPE_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 50, poParameter.CREF_DATE);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_LAST_CURRENCY_RATE {@Parameters} || RefreshCurrencyRate(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<RefreshCurrencyRateDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}
