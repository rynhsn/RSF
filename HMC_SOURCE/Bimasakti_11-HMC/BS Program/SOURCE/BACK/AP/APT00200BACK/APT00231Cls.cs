using APT00200COMMON.DTOs.APT00211;
using APT00200COMMON.DTOs.APT00231;
using APT00200COMMON.Loggers;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APT00200COMMON.DTOs.APT00210;
using APT00200BACK.OpenTelemetry;
using System.Diagnostics;

namespace APT00200BACK
{
    public class APT00231Cls : R_BusinessObject<APT00231ParameterDTO>
    {
        RSP_AP_DELETE_TRANS_ADDResources.Resources_Dummy_Class _loRspDeleteTransAdd = new RSP_AP_DELETE_TRANS_ADDResources.Resources_Dummy_Class();

        private LoggerAPT00231 _logger;
        private readonly ActivitySource _activitySource;
        public APT00231Cls()
        {
            _logger = LoggerAPT00231.R_GetInstanceLogger();
            _activitySource = APT00231ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public List<APT00231DTO> GetAdditionalList(GetAdditionalListParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetAdditionalList");
            R_Exception loException = new R_Exception();
            List<APT00231DTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_AP_GET_TRANS_ADD_LIST " +
                    "@CREC_ID, " +
                    "@CADDITIONAL_TYPE, " +
                    "@CLANGUAGE_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParameter.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CADDITIONAL_TYPE", DbType.String, 50, poParameter.CADDITIONAL_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParameter.CLANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_AP_GET_TRANS_ADD_LIST {@Parameters} || GetAdditionalList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<APT00231DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        protected override void R_Deleting(APT00231ParameterDTO poEntity)
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

                lcQuery = "EXEC RSP_AP_DELETE_TRANS_ADD @CREC_ID";

                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.Data.CREC_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_AP_DELETE_TRANS_ADD {@Parameters} || R_Deleting(Cls) ", loDbParam);

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

        protected override APT00231ParameterDTO R_Display(APT00231ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            R_Exception loException = new R_Exception();
            APT00231DTO loResult = null;
            APT00231ParameterDTO loRtn = new APT00231ParameterDTO();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_AP_GET_TRANS_ADD " +
                    "@CREC_ID, " +
                    "@CLANGUAGE_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.Data.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_AP_GET_TRANS_ADD {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<APT00231DTO>(loDataTable).FirstOrDefault();
                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            return loRtn;
        }

        protected override void R_Saving(APT00231ParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = "EXEC RSP_AP_SAVE_TRANS_ADD " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CPROPERTY_ID, " + 
                    "@CLOGIN_USER_ID, " +
                    "@CPurchaseReturn_REC_ID, " +
                    "@CACTION, " +
                    "@CREC_ID, " +
                    "@CREF_NO, " +
                    "@CDEPT_CODE, " +
                    "@CTRANS_CODE, " +
                    "@CTRX_TYPE, " +
                    "@CADDITIONAL_TYPE, " +
                    "@CADD_DEPT_CODE, " +
                    "@CCHARGES_ID, " +
                    "@CCHARGES_DESC, " +
                    "@NADDITION_AMOUNT";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poNewEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poNewEntity.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPurchaseReturn_REC_ID", DbType.String, 50, poNewEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poNewEntity.Data.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poNewEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, "120010");
                loDb.R_AddCommandParameter(loCmd, "@CTRX_TYPE", DbType.String, 50, "M");
                loDb.R_AddCommandParameter(loCmd, "@CADDITIONAL_TYPE", DbType.String, 50, poNewEntity.CADDITIONAL_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CADD_DEPT_CODE", DbType.String, 50, poNewEntity.Data.CADD_DEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poNewEntity.Data.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_DESC", DbType.String, 50, poNewEntity.Data.CCHARGES_DESC);
                loDb.R_AddCommandParameter(loCmd, "@NADDITION_AMOUNT", DbType.Int32, 50, poNewEntity.Data.NADDITION_AMOUNT);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_AP_SAVE_TRANS_ADD {@Parameters} || R_Saving(Cls) ", loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);

                    APT00231SaveResultDTO loResult = R_Utility.R_ConvertTo<APT00231SaveResultDTO>(loDataTable).FirstOrDefault();
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
