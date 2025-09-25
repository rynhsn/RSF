using PMM04500COMMON;
using PMM04500COMMON.DTO_s;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Transactions;

namespace PMM04500BACK
{
    public class PMM04501Cls : R_BusinessObject<PricingRateSaveParamDTO>
    {

        private LoggerPMM04500 _logger;

        private readonly ActivitySource _activitySource;

        RSP_PM_MAINTAIN_PRICINGResources.Resources_Dummy_Class _rspMaintainPricing = new();

        RSP_PM_MAINTAIN_PRICING_RATEResources.Resources_Dummy_Class _rspMaintainPricingRate = new();

        RSP_PM_ACTIVE_INACTIVE_PRICINGResources.Resources_Dummy_Class _rspActiveInactivePricing = new();

        public PMM04501Cls()
        {
            _logger = LoggerPMM04500.R_GetInstanceLogger();
            _activitySource = PMM04500Activity.R_GetInstanceActivitySource();
        }

        public List<PricingRateDTO> GetPricingRateList(PricingRateSaveParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new();
            List<PricingRateDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_PM_GET_PRICING_RATE_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poEntity.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUNIT_CATEGORY_ID", DbType.String, int.MaxValue, poEntity.CUNIT_TYPE_CATEGORY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPRICE_TYPE", DbType.String, int.MaxValue, poEntity.CPRICE_TYPE);
                loDB.R_AddCommandParameter(loCmd, "@CRATE_DATE", DbType.String, int.MaxValue, poEntity.CRATE_DATE);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poEntity.CUSER_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<PricingRateDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public List<PricingRateDTO> GetPricingRateDateList(PricingRateSaveParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new();
            List<PricingRateDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_PM_GET_PRICNG_RATE_DATE_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poEntity.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPRICE_TYPE", DbType.String, int.MaxValue, poEntity.CPRICE_TYPE);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poEntity.CUSER_ID);

                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<PricingRateDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public void SavePricingRate(PricingRateSaveParamDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    SavingSPPricingDate(poParam);

                    transactionScope.Complete();
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void SavingSPPricingDate(PricingRateSaveParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            R_Db loDB = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery = "";
            try
            {

                loDB = new R_Db();
                loCmd = loDB.GetCommand();
                loConn = loDB.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);

                // creating temptable
                lcQuery = @"CREATE TABLE #LEASE_PRICING_RATE (CCURRENCY_CODE CHAR(3),NBASE_RATE_AMOUNT NUMERIC(18,2), NCURRENCY_RATE_AMOUNT NUMERIC(18,2))";
                _logger.LogDebug($"{lcQuery}");
                loDB.SqlExecNonQuery(lcQuery, loConn, false);

                //insert list to temptable
                loDB.R_BulkInsert<PricingRateBulkSaveDTO>((SqlConnection)loConn, "#LEASE_PRICING_RATE", poParam.PRICING_RATE_LIST);

                //exec sp
                lcQuery = "RSP_PM_MAINTAIN_PRICING_RATE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                //param
                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poParam.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPRICE_TYPE", DbType.String, int.MaxValue, poParam.CPRICE_TYPE);
                loDB.R_AddCommandParameter(loCmd, "@CRATE_DATE", DbType.String, int.MaxValue, poParam.CRATE_DATE);
                loDB.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, int.MaxValue, poParam.CACTION);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poParam.CUSER_ID);

                try
                {
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
        protected override void R_Deleting(PricingRateSaveParamDTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override PricingRateSaveParamDTO R_Display(PricingRateSaveParamDTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override void R_Saving(PricingRateSaveParamDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            throw new NotImplementedException();
        }

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
