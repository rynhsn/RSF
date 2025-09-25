using APT00100COMMON.DTOs.APT00111;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APT00100COMMON.DTOs.APT00121;
using APT00100COMMON.Loggers;
using APT00100COMMON.DTOs.APT00110;
using R_CommonFrontBackAPI;
using APT00100BACK.OpenTelemetry;
using System.Diagnostics;

namespace APT00100BACK
{
    public class APT00121Cls : R_BusinessObject<APT00121ParameterDTO>
    {
        RSP_AP_DELETE_TRANS_HDResources.Resources_Dummy_Class _loRspDeleteTransHd = new RSP_AP_DELETE_TRANS_HDResources.Resources_Dummy_Class();
        RSP_AP_DELETE_TRANS_PDResources.Resources_Dummy_Class _loRspDeleteTransPd = new RSP_AP_DELETE_TRANS_PDResources.Resources_Dummy_Class();


        private LoggerAPT00121 _logger;
        private readonly ActivitySource _activitySource;
        public APT00121Cls()
        {
            _logger = LoggerAPT00121.R_GetInstanceLogger();
            _activitySource = APT00121ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public List<GetProductTypeDTO> GetProductTypeList(GetProductTypeParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetProductTypeList");
            R_Exception loException = new R_Exception();
            List<GetProductTypeDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_GS_GET_GSB_CODE_LIST " +
                    "@CAPPLICATION, " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CCLASS_ID, " +
                    "@CLANGUAGE_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CAPPLICATION", DbType.String, 50, "BIMASAKTI");
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCLASS_ID", DbType.String, 50, "_AP_PRODUCT_TYPE");
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParameter.CLANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_GSB_CODE_LIST {@Parameters} || GetProductTypeList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetProductTypeDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public APT00121DTO RefreshInvoiceItem(APT00121RefreshParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("RefreshInvoiceItem");
            R_Exception loException = new R_Exception();
            APT00121DTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_AP_GET_TRANS_PD " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CPROPERTY_ID, " +
                    "@CDEPT_CODE, " +
                    "@CTRANS_CODE, " +
                    "@CREF_NO, " +
                    "@CSEQ_NO, " +
                    "@CREC_ID, " +
                    "@CLOGIN_LANGUAGE_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CSEQ_NO", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParameter.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_LANGUAGE_ID", DbType.String, 50, poParameter.CLOGIN_LANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_AP_GET_TRANS_PD {@Parameters} || RefreshInvoiceItem(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<APT00121DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        protected override void R_Deleting(APT00121ParameterDTO poEntity)
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
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_AP_DELETE_TRANS_PD @CREC_ID";

                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.Data.CREC_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_AP_DELETE_TRANS_HD {@Parameters} || R_Deleting(Cls) ", loDbParam);

                loDb.SqlExecNonQuery(loConn, loCmd, true);
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

        protected override APT00121ParameterDTO R_Display(APT00121ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            R_Exception loException = new R_Exception();
            APT00121ParameterDTO loResult = new APT00121ParameterDTO();
            APT00121RefreshParameterDTO loParam = null;

            try
            {
                loParam = new APT00121RefreshParameterDTO()
                {
                    CLOGIN_COMPANY_ID = poEntity.CLOGIN_COMPANY_ID,
                    CLOGIN_LANGUAGE_ID = poEntity.CLANGUAGE_ID,
                    CREC_ID = poEntity.Data.CREC_ID
                };
                loResult.Data = RefreshInvoiceItem(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        protected override void R_Saving(APT00121ParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            R_Exception loEx = new R_Exception();
            APT00121SaveResultDTO loResult = null;
            string lcQuery = "";
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loCmd = loDb.GetCommand();
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "RSP_AP_SAVE_TRANS_PD";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPARENT_ID", DbType.String, 50, poNewEntity.Header.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poNewEntity.Data.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poNewEntity.Header.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.Header.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, "110010");
                loDb.R_AddCommandParameter(loCmd, "@CPROD_DEPT_CODE", DbType.String, 50, poNewEntity.Data.CPROD_DEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CPROD_TYPE", DbType.String, 50, poNewEntity.Data.CPROD_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CPRODUCT_ID", DbType.String, 50, poNewEntity.Data.CPRODUCT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CALLOC_ID", DbType.String, 50, poNewEntity.Data.CALLOC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDETAIL_DESC", DbType.String, 255, poNewEntity.Data.CDETAIL_DESC);
                loDb.R_AddCommandParameter(loCmd, "@IBILL_UNIT", DbType.Int16, 50, poNewEntity.Data.IBILL_UNIT);
                loDb.R_AddCommandParameter(loCmd, "@CBILL_UNIT", DbType.String, 50, poNewEntity.Data.CBILL_UNIT);
                loDb.R_AddCommandParameter(loCmd, "@NSUPP_CONV_FACTOR", DbType.Decimal, 50, poNewEntity.Data.NSUPP_CONV_FACTOR);
                loDb.R_AddCommandParameter(loCmd, "@NBILL_UNIT_QTY", DbType.Decimal, 50, poNewEntity.Data.NBILL_UNIT_QTY);
                loDb.R_AddCommandParameter(loCmd, "@NUNIT_PRICE", DbType.Decimal, 50, poNewEntity.Data.NUNIT_PRICE);
                loDb.R_AddCommandParameter(loCmd, "@CDISC_TYPE", DbType.String, 50, poNewEntity.Data.CDISC_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@NDISC_PCT", DbType.Decimal, 50, poNewEntity.Data.NDISC_PCT);
                loDb.R_AddCommandParameter(loCmd, "@NDISC_AMOUNT", DbType.Decimal, 50, poNewEntity.Data.NDISC_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 50, poNewEntity.Data.CTAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@NTAX_PCT", DbType.Decimal, 50, poNewEntity.Data.NTAX_PCT);
                loDb.R_AddCommandParameter(loCmd, "@NTAX_AMOUNT", DbType.Decimal, 50, poNewEntity.Data.NTAX_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@COTHER_TAX_ID", DbType.String, 50, poNewEntity.Data.COTHER_TAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@NOTHER_TAX_PCT", DbType.Decimal, 50, poNewEntity.Data.NOTHER_TAX_PCT);
                loDb.R_AddCommandParameter(loCmd, "@NOTHER_TAX_AMOUNT", DbType.Decimal, 50, poNewEntity.Data.NOTHER_TAX_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@NDIST_DISCOUNT", DbType.Decimal, 50, poNewEntity.Data.NDIST_DISCOUNT);
                loDb.R_AddCommandParameter(loCmd, "@NDIST_ADD_ON", DbType.Decimal, 50, poNewEntity.Data.NDIST_ADD_ON);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _logger.LogDebug("EXEC RSP_AP_SAVE_TRANS_PD {@poParameter}", loDbParam);

                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);

                    loResult = R_Utility.R_ConvertTo<APT00121SaveResultDTO>(loDataTable).FirstOrDefault();

                    _logger.LogInfo("Set CREC_ID IF ADD Data");
                    if (poCRUDMode == eCRUDMode.AddMode)
                    {
                        poNewEntity.Data.CREC_ID = loResult.CREC_ID;
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
                _logger.LogError(loEx);
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
        }
    }
}
