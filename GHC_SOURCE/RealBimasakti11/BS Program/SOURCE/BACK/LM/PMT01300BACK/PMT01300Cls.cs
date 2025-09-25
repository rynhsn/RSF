using PMT01300COMMON;
using R_BackEnd;
using R_Common;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using R_CommonFrontBackAPI;
using System.Transactions;
using System.Data.SqlClient;

namespace PMT01300BACK
{
    public class PMT01300Cls 
    {
        private RSP_PM_MAINTAIN_AGREEMENTResources.Resources_Dummy_Class _loRSP1 = new RSP_PM_MAINTAIN_AGREEMENTResources.Resources_Dummy_Class();
        private RSP_PM_MAINTAIN_AGREEMENT_UNITResources.Resources_Dummy_Class _loRSP2 = new RSP_PM_MAINTAIN_AGREEMENT_UNITResources.Resources_Dummy_Class();
        private RSP_PM_MAINTAIN_AGREEMENT_UTILITIESResources.Resources_Dummy_Class _loRSP3 = new RSP_PM_MAINTAIN_AGREEMENT_UTILITIESResources.Resources_Dummy_Class();
        private RSP_PM_MAINTAIN_AGREEMENT_CHARGESResources.Resources_Dummy_Class _loRSP4 = new RSP_PM_MAINTAIN_AGREEMENT_CHARGESResources.Resources_Dummy_Class();
        private RSP_PM_MAINTAIN_AGREEMENT_CHARGES_REVENUEResources.Resources_Dummy_Class _loRSP5 = new RSP_PM_MAINTAIN_AGREEMENT_CHARGES_REVENUEResources.Resources_Dummy_Class();
        private RSP_PM_MAINTAIN_AGREEMENT_DEPOSITResources.Resources_Dummy_Class _loRSP6 = new RSP_PM_MAINTAIN_AGREEMENT_DEPOSITResources.Resources_Dummy_Class();
        private RSP_PM_MAINTAIN_AGREEMENT_CHARGES_REVENUE_HDResources.Resources_Dummy_Class _loRSP7 = new RSP_PM_MAINTAIN_AGREEMENT_CHARGES_REVENUE_HDResources.Resources_Dummy_Class();
        private RSP_PM_UPLOAD_LEASE_AGREEMENTResources.Resources_Dummy_Class _loRSP8 = new RSP_PM_UPLOAD_LEASE_AGREEMENTResources.Resources_Dummy_Class();
        //private RSP_PM_SUBMIT_TRANS_HDResources.Resources_Dummy_Class _loRSP7 = new RSP_PM_SUBMIT_TRANS_HDResources.Resources_Dummy_Class();

        private LoggerPMT01300 _Logger;
        private LoggerPMT01300Print _Printlogger;
        private readonly ActivitySource _activitySource;

        public PMT01300Cls()
        {
            _Logger = LoggerPMT01300.R_GetInstanceLogger();
            var loActivity = PMT01300ActivitySourceBase.R_GetInstanceActivitySource();
            if (loActivity is null)
            {
                _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
            }
            else
            {
                _activitySource = loActivity;
            }
        }

        public PMT01300Cls(LoggerPMT01300Print poParam)
        {
            _Printlogger = LoggerPMT01300Print.R_GetInstanceLogger();
            _activitySource = PMT01300PrintActivitySourceBase.R_GetInstanceActivitySource();
        }
        public List<PMT01300DTO> GetAllLOI(string pcPropertyId, string pcTransStatusList)
        {
            using Activity activity = _activitySource.StartActivity("GetAllLOI");
            var loEx = new R_Exception();
            List<PMT01300DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_AGREEMENT_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, pcPropertyId);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, ContextConstant.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_REF_DATE", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_STATUS_LIST", DbType.String, 50, pcTransStatusList);
                loDb.R_AddCommandParameter(loCmd, "@CPROGRAM_ID", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_AGREEMENT_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMT01300DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public PMT01300DTO GetLOIDisplay(PMT01300DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetLOIDisplay");
            var loEx = new R_Exception();
            PMT01300DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_PM_GET_AGREEMENT_DETAIL";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, string.IsNullOrWhiteSpace(poEntity.CTRANS_CODE) ? ContextConstant.VAR_TRANS_CODE : poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_AGREEMENT_DETAIL {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMT01300DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public PMT01300DTO SaveLOI(PMT01300DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("SaveLOI");
            var loEx = new R_Exception();
            PMT01300DTO loRtn = null;

            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    SavingLOISP(poNewEntity, poCRUDMode);

                    transactionScope.Complete();
                }

                loRtn = GetLOIDisplay(poNewEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        private void SavingLOISP(PMT01300DTO poNewEntity, eCRUDMode poCRUDMode)
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

                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, ContextConstant.VAR_TRANS_CODE);

                loDB.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);
                loDB.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 8, poNewEntity.CREF_DATE);
                loDB.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poNewEntity.CBUILDING_ID);
                loDB.R_AddCommandParameter(loCmd, "@CDOC_NO", DbType.String, 30, string.IsNullOrWhiteSpace(poNewEntity.CDOC_NO) ? "" : poNewEntity.CDOC_NO);
                loDB.R_AddCommandParameter(loCmd, "@CDOC_DATE", DbType.String, 8, string.IsNullOrWhiteSpace(poNewEntity.CDOC_DATE) ? "" : poNewEntity.CDOC_DATE);

                loDB.R_AddCommandParameter(loCmd, "@CSTART_DATE", DbType.String, 8, poNewEntity.CSTART_DATE);
                loDB.R_AddCommandParameter(loCmd, "@CEND_DATE", DbType.String, 8, poNewEntity.CEND_DATE);
                loDB.R_AddCommandParameter(loCmd, "@IHOURS", DbType.Int32, 50, 0);
                loDB.R_AddCommandParameter(loCmd, "@IDAYS", DbType.Int32, 50, poNewEntity.IDAYS);
                loDB.R_AddCommandParameter(loCmd, "@IMONTHS", DbType.Int32, 50, poNewEntity.IMONTHS);
                loDB.R_AddCommandParameter(loCmd, "@IYEARS", DbType.Int32, 50, poNewEntity.IYEARS);

                loDB.R_AddCommandParameter(loCmd, "@CSALESMAN_ID", DbType.String, 8, poNewEntity.CSALESMAN_ID);
                loDB.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 20, poNewEntity.CTENANT_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUNIT_DESCRIPTION", DbType.String, 255, poNewEntity.CUNIT_DESCRIPTION);
                loDB.R_AddCommandParameter(loCmd, "@CNOTES", DbType.String, int.MaxValue, poNewEntity.CNOTES);
                loDB.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 3, poNewEntity.CCURRENCY_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CLEASE_MODE", DbType.String, 2, poNewEntity.CLEASE_MODE);
                loDB.R_AddCommandParameter(loCmd, "@CCHARGE_MODE", DbType.String, 2, poNewEntity.CCHARGE_MODE);

                loDB.R_AddCommandParameter(loCmd, "@CORIGINAL_REF_NO", DbType.String, 30, "");
                loDB.R_AddCommandParameter(loCmd, "@CFOLLOW_UP_DATE", DbType.String, 8, poNewEntity.CFOLLOW_UP_DATE);
                loDB.R_AddCommandParameter(loCmd, "@CEXPIRED_DATE", DbType.String, 8, "");
                loDB.R_AddCommandParameter(loCmd, "@LWITH_FO", DbType.Boolean, 20, poNewEntity.LWITH_FO);
                loDB.R_AddCommandParameter(loCmd, "@CHO_PLAN_DATE", DbType.String, 8, poNewEntity.CHO_PLAN_DATE);

                loDB.R_AddCommandParameter(loCmd, "@CBILLING_RULE_TYPE", DbType.String, 20, "02");
                loDB.R_AddCommandParameter(loCmd, "@CBILLING_RULE_CODE", DbType.String, 20, poNewEntity.CBILLING_RULE_CODE);
                loDB.R_AddCommandParameter(loCmd, "@NBOOKING_FEE", DbType.Decimal, 100, poNewEntity.NBOOKING_FEE);
                loDB.R_AddCommandParameter(loCmd, "@CTC_CODE", DbType.String, 20, poNewEntity.CTC_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CLINK_TRANS_CODE", DbType.String, 30, poNewEntity.CLINK_TRANS_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CLINK_REF_NO", DbType.String, 30, poNewEntity.CLINK_REF_NO);

                loDB.R_AddCommandParameter(loCmd, "@CTRANS_MODE", DbType.String, 1, "N");
                loDB.R_AddCommandParameter(loCmd, "@CFROM_AGRMT_ID", DbType.String, 30, "");

                loDB.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_PM_MAINTAIN_AGREEMENT {@poParameter}", loDbParam);

                    var loDataTable = loDB.SqlExecQuery(loConn, loCmd, false);

                    var loTempResult = R_Utility.R_ConvertTo<PMT01300DTO>(loDataTable).FirstOrDefault();

                    if (loTempResult != null)
                        poNewEntity.CREF_NO = loTempResult.CREF_NO;
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
        public void UpdateAgreementTransStatus(PMT01300SubmitRedraftDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("UpdateAgreementTransStatus");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_PM_UPDATE_AGREEMENT_TRANS_STS";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CNEW_STATUS", DbType.String, 50, poEntity.CNEW_STATUS);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_PM_UPDATE_AGREEMENT_TRANS_STS {@Parameters}", loDbParam);

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

        #region Report
        public PMT01300ParameterPrintLogoResultDTO GetBaseHeaderLogoCompany(PMT01300ParameterPrintLogoResultDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetBaseHeaderLogoCompany");
            var loEx = new R_Exception();
            PMT01300ParameterPrintLogoResultDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();


                var lcQuery = "SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 15, poEntity.CCOMPANY_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Printlogger.LogDebug("SELECT dbo.RFN_GET_COMPANY_LOGO({@CCOMPANY_ID}) as CLOGO", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMT01300ParameterPrintLogoResultDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Printlogger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<PMT01300ResultSPPrintDTO> GetPrintResultSP(PMT01300ParameterPrintLogoResultDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetPrintResultSP");
            var loEx = new R_Exception();
            List<PMT01300ResultSPPrintDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_REP_TENANT_INVOICE_HD";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Printlogger.LogDebug("EXEC RSP_PM_REP_TENANT_INVOICE_HD {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMT01300ResultSPPrintDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Printlogger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

    }
}