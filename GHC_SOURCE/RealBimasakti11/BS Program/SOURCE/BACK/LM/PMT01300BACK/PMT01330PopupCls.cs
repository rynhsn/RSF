using PMT01300COMMON;
using R_BackEnd;
using R_Common;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using R_CommonFrontBackAPI;
using System.Data.SqlClient;
using System.Transactions;

namespace PMT01300BACK
{
    public class PMT01330PopupCls 
    {
        private LoggerPMT01330Popup _Logger;
        private readonly ActivitySource _activitySource;

        public PMT01330PopupCls()
        {
            _Logger = LoggerPMT01330Popup.R_GetInstanceLogger();
            _activitySource = PMT01330PopupActivitySourceBase.R_GetInstanceActivitySource();
        }

        #region Charge Revenue HD
        public List<PMT01331DTO> GetAllChargeRevenueHD(PMT01331DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetAllChargeRevenueHD");
            var loEx = new R_Exception();
            List<PMT01331DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_AGREEMENT_CHARGES_REVENUE_HD";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, ContextConstant.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_SEQ_NO", DbType.String, 50, poEntity.CCHARGE_SEQ_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_AGREEMENT_CHARGES_REVENUE_HD {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMT01331DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public void SaveDeleteChargeRevenueHD(PMT01331DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("SaveChargeRevenueHD");
            var loEx = new R_Exception();

            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    SavingDeleteChargeRevenueHDSP(poNewEntity, poCRUDMode);

                    transactionScope.Complete();
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void SavingDeleteChargeRevenueHDSP(PMT01331DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("SavingDeleteChargeRevenueHDSP");
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
                    poNewEntity.CACTION = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";
                }
                else if(poCRUDMode == eCRUDMode.DeleteMode)
                {
                    poNewEntity.CACTION = "DELETE";
                }

                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);

                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_CHARGES_REVENUE_HD";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, ContextConstant.VAR_TRANS_CODE);

                loDB.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);
                loDB.R_AddCommandParameter(loCmd, "@CCHARGE_SEQ_NO", DbType.String, 3, poNewEntity.CCHARGE_SEQ_NO);
                loDB.R_AddCommandParameter(loCmd, "@CREVENUE_SHARING_ID", DbType.String, 20, poNewEntity.CREVENUE_SHARING_ID);
                loDB.R_AddCommandParameter(loCmd, "@CREVENUE_SHARING_NAME", DbType.String, 100, poNewEntity.CREVENUE_SHARING_NAME);

                loDB.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_PM_MAINTAIN_AGREEMENT_CHARGES_REVENUE_HD {@poParameter}", loDbParam);

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
        #endregion

        #region  Charge Revenue
        public List<PMT01332DTO> GetAllChargeRevenue(PMT01332DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetAllChargeRevenue");
            var loEx = new R_Exception();
            List<PMT01332DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_AGREEMENT_CHARGES_REVENUE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, ContextConstant.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_SEQ_NO", DbType.String, 50, poEntity.CCHARGE_SEQ_NO);
                loDb.R_AddCommandParameter(loCmd, "@CREVENUE_SHARING_ID", DbType.String, 50, poEntity.CREVENUE_SHARING_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_AGREEMENT_CHARGES_REVENUE {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMT01332DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public PMT01332DTO SaveDeleteChargeRevenue(PMT01332DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("SaveChargeRevenue");
            var loEx = new R_Exception();
            PMT01332DTO loRtn = null;

            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    loRtn = SavingDeleteChargeRevenueSP(poNewEntity, poCRUDMode);

                    transactionScope.Complete();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        private PMT01332DTO SavingDeleteChargeRevenueSP(PMT01332DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("SavingDeleteChargeRevenueSP");
            R_Exception loEx = new R_Exception();
            R_Db loDB = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            PMT01332DTO loRtn = poNewEntity;

            try
            {
                // set action 
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "ADD";
                    poNewEntity.CSEQ_NO = "";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";
                }
                else if(poCRUDMode == eCRUDMode.DeleteMode)
                {
                    poNewEntity.CACTION = "DELETE";
                }

                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);

                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_CHARGES_REVENUE";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, ContextConstant.VAR_TRANS_CODE);

                loDB.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);
                loDB.R_AddCommandParameter(loCmd, "@CCHARGE_SEQ_NO", DbType.String, 3, poNewEntity.CCHARGE_SEQ_NO);
                loDB.R_AddCommandParameter(loCmd, "@CREVENUE_SHARING_ID", DbType.String, 20, poNewEntity.CREVENUE_SHARING_ID);
                loDB.R_AddCommandParameter(loCmd, "@CSEQ_NO", DbType.String, 3, poNewEntity.CSEQ_NO);
                loDB.R_AddCommandParameter(loCmd, "@NMONTHLY_REVENUE_FROM", DbType.Decimal, 100, poNewEntity.NMONTHLY_REVENUE_FROM);
                loDB.R_AddCommandParameter(loCmd, "@NMONTHLY_REVENUE_TO", DbType.Decimal, 100, poNewEntity.NMONTHLY_REVENUE_TO);
                loDB.R_AddCommandParameter(loCmd, "@NSHARE_PCT", DbType.Decimal, 100, poNewEntity.NSHARE_PCT);

                loDB.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_PM_MAINTAIN_AGREEMENT_CHARGES_REVENUE {@poParameter}", loDbParam);

                    if (poCRUDMode == eCRUDMode.DeleteMode)
                    {
                        loDB.SqlExecNonQuery(loConn, loCmd, false);
                    }
                    else
                    {
                        var loDataTable = loDB.SqlExecQuery(loConn, loCmd, false);

                        var loTempResult = R_Utility.R_ConvertTo<PMT01320DTO>(loDataTable).FirstOrDefault();

                        if (loTempResult != null)
                            loRtn.CSEQ_NO = loTempResult.CSEQ_NO;
                    }
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
            return loRtn;
        }
        #endregion

        #region Revenue Mint Rent
        public List<PMT01333DTO> GetAllRevenueMintRent(PMT01333DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetAllRevenueMintRent");
            var loEx = new R_Exception();
            List<PMT01333DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_REVENUE_MIN_RENT";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, ContextConstant.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_SEQ_NO", DbType.String, 50, poEntity.CCHARGE_SEQ_NO);
                loDb.R_AddCommandParameter(loCmd, "@CREVENUE_SHARING_ID", DbType.String, 50, poEntity.CREVENUE_SHARING_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_REVENUE_MIN_RENT {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMT01333DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public void SaveRevenueMintRent(PMT01333DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("SaveRevenueMintRent");
            var loEx = new R_Exception();

            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    SavingRevenueMintRentSP(poNewEntity, poCRUDMode);

                    transactionScope.Complete();
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void SavingRevenueMintRentSP(PMT01333DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("SavingRevenueMintRentSP");
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
                    poNewEntity.CACTION = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";
                }

                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);

                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_MIN_RENT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, ContextConstant.VAR_TRANS_CODE);

                loDB.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);
                loDB.R_AddCommandParameter(loCmd, "@CCHARGE_SEQ_NO", DbType.String, 3, poNewEntity.CCHARGE_SEQ_NO);
                loDB.R_AddCommandParameter(loCmd, "@CREVENUE_SHARING_ID", DbType.String, 20, poNewEntity.CREVENUE_SHARING_ID);
                loDB.R_AddCommandParameter(loCmd, "@IYEAR", DbType.Int32, 100, poNewEntity.IYEAR);
                loDB.R_AddCommandParameter(loCmd, "@NRENT_RATE", DbType.Decimal, 100, poNewEntity.NRENT_RATE);
                loDB.R_AddCommandParameter(loCmd, "@NMINIMUM_RENT", DbType.Decimal, 100, poNewEntity.NMINIMUM_RENT);

                loDB.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_PM_MAINTAIN_AGREEMENT_MIN_RENT {@poParameter}", loDbParam);

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
        #endregion
    }
}