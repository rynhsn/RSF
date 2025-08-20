using GSM02500COMMON.DTOs.GSM02502Charge;
using GSM02500COMMON.DTOs.GSM02502Utility;
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
using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON.Loggers;
using System.Diagnostics;
using GSM02500BACK.OpenTelemetry;

namespace GSM02500BACK
{
    public class GSM02502UtilityCls : R_BusinessObjectAsync<GSM02502UtilityParameterDTO>
    {
        private LoggerGSM02502Utility _logger;
        private readonly ActivitySource _activitySource;
        public GSM02502UtilityCls()
        {
            _logger = LoggerGSM02502Utility.R_GetInstanceLogger();
            _activitySource = GSM02502UtilityActivitySourceBase.R_GetInstanceActivitySource();
        }

        public async Task<List<GSM02502UtilityDTO>> GetUtilityList(GetUtilityListDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetUtilityList");
            R_Exception loException = new R_Exception();
            List<GSM02502UtilityDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = await loDb.GetConnectionAsync();

                lcQuery = $"EXEC RSP_GS_GET_UNIT_TYPE_CTG_UTILITY_LIST " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CSELECTED_PROPERTY_ID, " +
                    $"@CSELECTED_UNIT_TYPE_CATEGORY_ID, " +
                    $"@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_UNIT_TYPE_CATEGORY_ID", DbType.String, 50, poEntity.CSELECTED_UNIT_TYPE_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_UNIT_TYPE_CTG_UTILITY_LIST {@Parameters} || GetUtilityList(Cls) ", loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM02502UtilityDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        private async Task RSP_GS_MAINTAIN_UNIT_TYPE_CATEGORY_UTILITYMethod(GSM02502UtilityParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_MAINTAIN_UNIT_TYPE_CATEGORY_UTILITYMethod");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();

                lcQuery = $"EXEC RSP_GS_MAINTAIN_UNIT_TYPE_CATEGORY_UTILITY " +
                                 $"@CLOGIN_COMPANY_ID, " +
                                 $"@CSELECTED_PROPERTY_ID, " +
                                 $"@CSELECTED_UNIT_TYPE_CATEGORY_ID, " +
                                 $"@CUTILITY_TYPE, " +
                                 $"@LACTIVE, " +
                                 $"@CACTION, " +
                                 $"@CLOGIN_USER_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 20, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 20, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_UNIT_TYPE_CATEGORY_ID", DbType.String, 20, poEntity.CSELECTED_UNIT_TYPE_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUTILITY_TYPE", DbType.String, 2, poEntity.Data.CUTILITY_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 10, poEntity.Data.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 20, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_MAINTAIN_UNIT_TYPE_CATEGORY_UTILITY {@Parameters} || RSP_GS_MAINTAIN_UNIT_TYPE_CATEGORY_UTILITYMethod(Cls) ", loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
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

        protected override async Task<GSM02502UtilityParameterDTO> R_DisplayAsync(GSM02502UtilityParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            R_Exception loException = new R_Exception();
            GSM02502UtilityParameterDTO loResult = new GSM02502UtilityParameterDTO();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = await loDb.GetConnectionAsync();

                lcQuery = $"EXEC RSP_GS_GET_UNIT_TYPE_CTG_UTILITY_DT " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CSELECTED_PROPERTY_ID, " +
                    $"@CSELECTED_UNIT_TYPE_CATEGORY_ID, " +
                    $"@CUTILITY_TYPE, " +
                    $"@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_UNIT_TYPE_CATEGORY_ID", DbType.String, 50, poEntity.CSELECTED_UNIT_TYPE_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUTILITY_TYPE", DbType.String, 50, poEntity.Data.CUTILITY_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_UNIT_TYPE_CTG_UTILITY_DT {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

                loResult.Data = R_Utility.R_ConvertTo<GSM02502UtilityDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override async Task R_SavingAsync(GSM02502UtilityParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            R_Exception loException = new R_Exception();

            try
            {
                await RSP_GS_MAINTAIN_UNIT_TYPE_CATEGORY_UTILITYMethod(poNewEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }

        protected override Task R_DeletingAsync(GSM02502UtilityParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
    }
}
