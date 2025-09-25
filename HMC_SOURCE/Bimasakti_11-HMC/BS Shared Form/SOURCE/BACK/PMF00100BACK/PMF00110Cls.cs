using PMF00100BACK.OpenTelemetry;
using PMF00100COMMON.DTOs.PMF00100;
using PMF00100COMMON.Logger;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMF00100COMMON.DTOs.PMF00110;
using R_CommonFrontBackAPI;
using System.Reflection.Metadata;

namespace PMF00100BACK
{
    public class PMF00110Cls : R_BusinessObject<PMF00110ParameterDTO>
    {
        RSP_PM_DELETE_ALLOCATIONResources.Resources_Dummy_Class _loRspDelete = new RSP_PM_DELETE_ALLOCATIONResources.Resources_Dummy_Class();
        RSP_PM_SAVE_ALLOCATIONResources.Resources_Dummy_Class _loRspSave = new RSP_PM_SAVE_ALLOCATIONResources.Resources_Dummy_Class();
        RSP_PM_SUBMIT_ALLOCATIONResources.Resources_Dummy_Class _loRspSubmit = new RSP_PM_SUBMIT_ALLOCATIONResources.Resources_Dummy_Class();
        RSP_PM_UPDATE_ALLOCATION_STATUSResources.Resources_Dummy_Class _loRspUpdate = new RSP_PM_UPDATE_ALLOCATION_STATUSResources.Resources_Dummy_Class();
        
        private LoggerPMF00110 _logger;
        private readonly ActivitySource _activitySource;

        public PMF00110Cls()
        {
            _logger = LoggerPMF00110.R_GetInstanceLogger();
            _activitySource = PMF00110ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public List<GetTransactionTypeDTO> GetTransactionTypeList(GetTransactionTypeParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetTransactionTypeList");
            R_Exception loException = new R_Exception();
            List<GetTransactionTypeDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_PM_GET_ALLOC_TRX_TYPE_LIST " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CTRANS_CODE, " +
                    "@CLANGUAGE_ID";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParameter.CLANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_GET_ALLOC_TRX_TYPE_LIST {@Parameters} || GetTransactionTypeList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetTransactionTypeDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;

        }

        public void SubmitAllocationProcess(SubmitAllocationParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("SubmitAllocationProcess");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = "EXEC RSP_PM_SUBMIT_ALLOCATION " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CPROPERTY_ID, " +
                    "@CLOGIN_USER_ID, " +
                    "@CALLOCATION_REC_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poParameter.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CALLOCATION_REC_ID", DbType.String, 50, poParameter.CALLOCATION_REC_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_SUBMIT_ALLOCATION {@Parameters} || SubmitAllocationProcess(Cls) ", loDbParam);

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

        public void RedraftAllocationProcess(RedraftAllocationParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("RedraftAllocationProcess");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = "EXEC RSP_PM_UPDATE_ALLOCATION_STATUS " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CLOGIN_USER_ID, " +
                    "@CREC_ID, " +
                    "@CNEW_STATUS";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poParameter.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParameter.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CNEW_STATUS", DbType.String, 50, "00");

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_UPDATE_ALLOCATION_STATUS {@Parameters} || RedraftAllocationProcess(Cls) ", loDbParam);

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

        public PMF00110DTO GetAllocationDetail(GetAllocationDetailParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetAllocationDetail");
            R_Exception loException = new R_Exception();
            PMF00110DTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_PM_GET_ALLOCATION " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CPROPERTY_ID, " +
                    "@CDEPT_CODE, " +
                    "@CVAR_TRANS_CODE, " +
                    "@CREF_NO, " +
                    "@CREC_ID, " +
                    "@CLANGUAGE_ID, " +
                    "@CTRANS_CODE";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParam.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poParam.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParam.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CVAR_TRANS_CODE", DbType.String, 50, poParam.CVAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poParam.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParam.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParam.CLANGUAGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poParam.CTRANS_CODE);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_GET_ALLOCATION {@Parameters} || GetAllocationDetail(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMF00110DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        protected override PMF00110ParameterDTO R_Display(PMF00110ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            R_Db loDb = new R_Db();
            R_Exception loException = new R_Exception();
            PMF00110ParameterDTO loResult = new PMF00110ParameterDTO();
            GetAllocationDetailParameterDTO loParameter = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loParameter = new GetAllocationDetailParameterDTO()
                {
                    CLOGIN_COMPANY_ID = poEntity.CLOGIN_COMPANY_ID,
                    CDEPT_CODE = poEntity.CDEPT_CODE,
                    CVAR_TRANS_CODE = poEntity.CVAR_TRANS_CODE,
                    CTRANS_CODE = poEntity.CCALLER_TRANS_CODE,
                    CREF_NO = poEntity.CREF_NO,
                    CREC_ID = poEntity.CALLOCATION_ID,
                    CLANGUAGE_ID = poEntity.CLANGUAGE_ID
                };

                loResult.Data = GetAllocationDetail(loParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(PMF00110ParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            PMF00110SaveResultDTO loResult = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_PM_SAVE_ALLOCATION " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CPROPERTY_ID, " +
                    "@CLOGIN_USER_ID, " +
                    "@CACTION, " +
                    "@CALLOCATION_ID, " +
                    "@CDEPT_CODE, " +
                    "@CTRANS_CODE, " +
                    "@CREF_NO, " +
                    "@CALLOC_DATE, " +
                    "@CTENANT_ID, " +
                    "@CTENANT_SEQ_NO, " +
                    "@CSOURCE_MODULE, " +
                    "@CFR_REC_ID, " +
                    "@CFR_DEPT_CODE, " +
                    "@CFR_TRANS_CODE, " +
                    "@CFR_REF_NO, " +
                    "@CFR_CURRENCY_CODE, " +
                    "@NFR_AR_AMOUNT, " +
                    "@NFR_TAX_AMOUNT, " +
                    "@NFR_DISC_AMOUNT, " +
                    "@NFR_LBASE_RATE, " +
                    "@NFR_LCURRENCY_RATE, " +
                    "@NFR_BBASE_RATE, " +
                    "@NFR_BCURRENCY_RATE, " +
                    "@NFR_TAX_BASE_RATE, " +
                    "@NFR_TAX_CURRENCY_RATE, " +
                    "@CTO_REC_ID, " +
                    "@CTO_DEPT_CODE, " +
                    "@CTO_TRANS_CODE, " +
                    "@CTO_REF_NO, " +
                    "@CTO_CURRENCY_CODE, " +
                    "@NTO_AR_AMOUNT, " +
                    "@NTO_TAX_AMOUNT, " +
                    "@NTO_DISC_AMOUNT, " +
                    "@NTO_LBASE_RATE, " +
                    "@NTO_LCURRENCY_RATE, " +
                    "@NTO_BBASE_RATE, " +
                    "@NTO_BCURRENCY_RATE, " +
                    "@NTO_TAX_BASE_RATE, " +
                    "@NTO_TAX_CURRENCY_RATE, " +
                    "@NLFOREX_GAINLOSS, " +
                    "@NBFOREX_GAINLOSS";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poNewEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poNewEntity.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CALLOCATION_ID", DbType.String, 50, poNewEntity.CALLOCATION_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poNewEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poNewEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CALLOC_DATE", DbType.String, 50, poNewEntity.CALLOC_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 50, poNewEntity.CTENANT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_SEQ_NO", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CSOURCE_MODULE", DbType.String, 50, "PM");
                loDb.R_AddCommandParameter(loCmd, "@CFR_REC_ID", DbType.String, 50, poNewEntity.CFR_REC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFR_DEPT_CODE", DbType.String, 50, poNewEntity.CFR_DEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CFR_TRANS_CODE", DbType.String, 50, poNewEntity.CFR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CFR_REF_NO", DbType.String, 50, poNewEntity.CFR_REF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CFR_CURRENCY_CODE", DbType.String, 50, poNewEntity.CFR_CURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@NFR_AR_AMOUNT", DbType.Int32, 50, poNewEntity.NFR_AR_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@NFR_TAX_AMOUNT", DbType.Int32, 50, poNewEntity.NFR_TAX_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@NFR_DISC_AMOUNT", DbType.Int32, 50, poNewEntity.NFR_DISC_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@NFR_LBASE_RATE", DbType.Int32, 50, poNewEntity.NFR_LBASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NFR_LCURRENCY_RATE", DbType.Int32, 50, poNewEntity.NFR_LCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NFR_BBASE_RATE", DbType.Int32, 50, poNewEntity.NFR_BBASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NFR_BCURRENCY_RATE", DbType.Int32, 50, poNewEntity.NFR_BCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NFR_TAX_BASE_RATE", DbType.Int32, 50, poNewEntity.NFR_TAX_BASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NFR_TAX_CURRENCY_RATE", DbType.Int32, 50, poNewEntity.NFR_TAX_CURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@CTO_REC_ID", DbType.String, 50, poNewEntity.CTO_REC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTO_DEPT_CODE", DbType.String, 50, poNewEntity.CTO_DEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTO_TRANS_CODE", DbType.String, 50, poNewEntity.CTO_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTO_REF_NO", DbType.String, 50, poNewEntity.CTO_REF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CTO_CURRENCY_CODE", DbType.String, 50, poNewEntity.CTO_CURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@NTO_AR_AMOUNT", DbType.Int32, 50, poNewEntity.NTO_AR_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@NTO_TAX_AMOUNT", DbType.Int32, 50, poNewEntity.NTO_TAX_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@NTO_DISC_AMOUNT", DbType.Int32, 50, poNewEntity.NTO_DISC_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@NTO_LBASE_RATE", DbType.Int32, 50, poNewEntity.NTO_LBASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NTO_LCURRENCY_RATE", DbType.Int32, 50, poNewEntity.NTO_LCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NTO_BBASE_RATE", DbType.Int32, 50, poNewEntity.NTO_BBASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NTO_BCURRENCY_RATE", DbType.Int32, 50, poNewEntity.NTO_BCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NTO_TAX_BASE_RATE", DbType.Int32, 50, poNewEntity.NTO_TAX_BASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NTO_TAX_CURRENCY_RATE", DbType.Int32, 50, poNewEntity.NTO_TAX_CURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NLFOREX_GAINLOSS", DbType.Int32, 50, poNewEntity.NLFOREX_GAINLOSS);
                loDb.R_AddCommandParameter(loCmd, "@NBFOREX_GAINLOSS", DbType.Int32, 50, poNewEntity.NBFOREX_GAINLOSS);
                
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_SAVE_ALLOCATION {@Parameters} || R_Saving(Cls) ", loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);

                    loResult = R_Utility.R_ConvertTo<PMF00110SaveResultDTO>(loDataTable).FirstOrDefault();

                    poNewEntity.CALLOCATION_ID = loResult.CREC_ID;
                    poNewEntity.CCALLER_TRANS_CODE = poNewEntity.CTO_TRANS_CODE;
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

        protected override void R_Deleting(PMF00110ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Deleting");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = "EXEC RSP_PM_DELETE_ALLOCATION @CALLOCATION_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CALLOCATION_ID", DbType.String, 50, poEntity.CALLOCATION_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_DELETE_ALLOCATION {@Parameters} || R_Deleting(Cls) ", loDbParam);

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
    }
}
