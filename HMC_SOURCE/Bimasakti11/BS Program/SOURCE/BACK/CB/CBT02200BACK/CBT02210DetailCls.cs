using CBT02200COMMON.DTO.CBT02210;
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
using CBT02200BACK.OpenTelemetry;
using CBT02200COMMON.Logger;

namespace CBT02200BACK
{
    public class CBT02210DetailCls : R_BusinessObject<CBT02210DetailParameterDTO>
    {
        RSP_CB_SAVE_CHEQUE_JRNResources.Resources_Dummy_Class loSave = new RSP_CB_SAVE_CHEQUE_JRNResources.Resources_Dummy_Class();
        RSP_CB_DELETE_CHEQUE_JRNResources.Resources_Dummy_Class loDelete = new RSP_CB_DELETE_CHEQUE_JRNResources.Resources_Dummy_Class();

        private LoggerCBT02210Detail _logger;
        private readonly ActivitySource _activitySource;
        public CBT02210DetailCls()
        {
            _logger = LoggerCBT02210Detail.R_GetInstanceLogger();
            _activitySource = CBT02210DetailActivitySourceBase.R_GetInstanceActivitySource();
        }

        public List<CBT02210DetailDTO> GetChequeEntryDetailList(GetCBT02210DetailParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetChequeEntryDetailList");
            R_Exception loException = new R_Exception();
            List<CBT02210DetailDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_CB_GET_CHEQUE_JRN_LIST " +
                    $"@CCHEQUE_ID, " +
                    $"@CLANGUAGE_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCHEQUE_ID", DbType.String, 50, poParameter.CCHEQUE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParameter.CLANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_CB_GET_CHEQUE_JRN_LIST {@Parameters} || GetChequeEntryDetailList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<CBT02210DetailDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GetCenterListDTO> GetCenterList(GetCenterListParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetCenterList");
            R_Exception loException = new R_Exception();
            List<GetCenterListDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_GS_GET_CENTER_LIST " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poParameter.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_CENTER_LIST {@Parameters} || GetCenterList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetCenterListDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Deleting(CBT02210DetailParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            R_Exception loException = new R_Exception();
            CBT02210DetailDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = $"EXEC RSP_CB_DELETE_CHEQUE_JRN " +
                                 $"@CREC_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.Data.CREC_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_CB_DELETE_CHEQUE_JRN {@Parameters} || R_Deleting(Cls) ", loDbParam);

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

        protected override CBT02210DetailParameterDTO R_Display(CBT02210DetailParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            R_Exception loException = new R_Exception();
            CBT02210DetailParameterDTO loResult = new CBT02210DetailParameterDTO();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_CB_GET_CHEQUE_JRN " +
                    $"@CREC_ID, " +
                    $"@CLANGUAGE_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.Data.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_CB_GET_CHEQUE_JRN {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult.Data = R_Utility.R_ConvertTo<CBT02210DetailDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(CBT02210DetailParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            R_Exception loException = new R_Exception();
            CBT02210DetailDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = $"EXEC RSP_CB_SAVE_CHEQUE_JRN " +
                                 $"@CLOGIN_USER_ID, " +
                                 $"@CACTION, " +
                                 $"@CCHEQUE_ID, " +
                                 $"@CREC_ID, " +
                                 $"@CLOGIN_COMPANY_ID, " +
                                 $"@CDEPT_CODE, " +
                                 $"@CTRANS_CODE, " +
                                 $"@CREF_NO, " +
                                 $"@CREF_DATE, " +
                                 $"@CINPUT_TYPE, " +
                                 $"@CGLACCOUNT_NO, " +
                                 $"@CCENTER_CODE, " +
                                 $"@CCASH_FLOW_GROUP_CODE, " +
                                 $"@CCASH_FLOW_CODE, " +
                                 $"@CDBCR, " +
                                 $"@CCURRENCY_CODE, " +
                                 $"@NTRANS_AMOUNT, " +
                                 $"@CDETAIL_DESC, " +
                                 $"@CDOCUMENT_NO, " +
                                 $"@CDOCUMENT_DATE, " +
                                 $"@LSUSPENSE_ACCOUNT";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poNewEntity.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CCHEQUE_ID", DbType.String, 50, poNewEntity.CCHEQUE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poNewEntity.Data.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poNewEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poNewEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poNewEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 50, poNewEntity.CREF_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CINPUT_TYPE", DbType.String, 50, "M");
                loDb.R_AddCommandParameter(loCmd, "@CGLACCOUNT_NO", DbType.String, 50, poNewEntity.Data.CGLACCOUNT_NO);
                loDb.R_AddCommandParameter(loCmd, "@CCENTER_CODE", DbType.String, 50, poNewEntity.Data.CCENTER_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP_CODE", DbType.String, 50, poNewEntity.Data.CCASH_FLOW_GROUP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_CODE", DbType.String, 50, poNewEntity.Data.CCASH_FLOW_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDBCR", DbType.String, 50, poNewEntity.Data.CDBCR);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 50, poNewEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@NTRANS_AMOUNT", DbType.Int32, 50, poNewEntity.Data.NTRANS_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CDETAIL_DESC", DbType.String, 50, poNewEntity.Data.CDETAIL_DESC);
                loDb.R_AddCommandParameter(loCmd, "@CDOCUMENT_NO", DbType.String, 50, poNewEntity.Data.CDOCUMENT_NO);
                loDb.R_AddCommandParameter(loCmd, "@CDOCUMENT_DATE", DbType.String, 50, poNewEntity.Data.CDOCUMENT_DATE);
                loDb.R_AddCommandParameter(loCmd, "@LSUSPENSE_ACCOUNT", DbType.Boolean, 50, false);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_CB_SAVE_TRANS_JRN {@Parameters} || RSP_GS_MAINTAIN_UNIT_TYPE_CATEGORYMethod(Cls) ", loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                    loResult = R_Utility.R_ConvertTo<CBT02210DetailDTO>(loDataTable).FirstOrDefault();
                    poNewEntity.Data.CREC_ID = loResult.CREC_ID;
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
