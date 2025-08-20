using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02501;
using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON.Loggers;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500BACK
{
    public class GSM02502Cls : R_BusinessObjectAsync<GSM02502ParameterDTO>
    {
        RSP_GS_MAINTAIN_UNIT_TYPE_CATEGORYResources.Resources_Dummy_Class _loRsp = new RSP_GS_MAINTAIN_UNIT_TYPE_CATEGORYResources.Resources_Dummy_Class();

        private LoggerGSM02502 _logger;
        private readonly ActivitySource _activitySource;
        public GSM02502Cls()
        {
            _logger = LoggerGSM02502.R_GetInstanceLogger();
            _activitySource = GSM02502ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public async Task<List<PropertyTypeDTO>> GetPropertyTypeList(PropertyTypeParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetPropertyTypeList");
            R_Exception loException = new R_Exception();
            List<PropertyTypeDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = await loDb.GetConnectionAsync();

                lcQuery = $"EXEC RSP_GS_GET_PROPERTY_TYPE_LIST " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CLOGIN_USER_ID, " +
                    $"@LSINGLE_UNIT";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@LSINGLE_UNIT", DbType.Boolean, 50, false);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_PROPERTY_TYPE_LIST {@Parameters} || GetPropertyTypeList(Cls) ", loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PropertyTypeDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<List<GSM02502DTO>> GetUnitTypeCategoryList(GetUnitTypeCategoryListDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetUnitTypeCategoryList");
            R_Exception loException = new R_Exception();
            List<GSM02502DTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = await loDb.GetConnectionAsync();

                lcQuery = $"EXEC RSP_GS_GET_UNIT_TYPE_CTG_LIST " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CPROPERTY_ID, " +
                    $"@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_UNIT_TYPE_CTG_LIST {@Parameters} || GetUnitTypeCategoryList(Cls) ", loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM02502DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task RSP_GS_ACTIVE_INACTIVE_UNIT_TYPE_CATEGORYMethod(GSM02500ActiveInactiveParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_ACTIVE_INACTIVE_UNIT_TYPE_CATEGORYMethod");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = await loDb.GetConnectionAsync();

                lcQuery = $"EXEC RSP_GS_ACTIVE_INACTIVE_UNIT_TYPE_CATEGORY " +
                    $"@CCOMPANY_ID, " +
                    $"@CPROPERTY_ID, " +
                    $"@CUNIT_TYPE_CATEGORY_ID, " +
                    $"@LACTIVE, " +
                    $"@CUSER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_CATEGORY_ID", DbType.String, 50, poEntity.CUNIT_TYPE_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.String, 50, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_ACTIVE_INACTIVE_UNIT_TYPE_CATEGORY {@Parameters} || RSP_GS_ACTIVE_INACTIVE_UNIT_TYPE_CATEGORYMethod(Cls) ", loDbParam);

                await loDb.SqlExecNonQueryAsync(loConn, loCmd, true);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        private async Task RSP_GS_MAINTAIN_UNIT_TYPE_CATEGORYMethod(GSM02502ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_MAINTAIN_UNIT_TYPE_CATEGORYMethod");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand(); 

                lcQuery = $"EXEC RSP_GS_MAINTAIN_UNIT_TYPE_CATEGORY " +
                                 $"@CCOMPANY_ID, " +
                                 $"@CPROPERTY_ID, " +
                                 $"@CUNIT_TYPE_CATEGORY_ID, " +
                                 $"@CUNIT_TYPE_CATEGORY_NAME, " +
                                 $"@CPROPERTY_TYPE, " +
                                 $"@IINVITATION_INVOICE_PERIOD, " +
                                 $"@CDESCRIPTION, " +
                                 $"@LACTIVE, " +
                                 $"@CACTION, " +
                                 $"@CUSER_LOGIN_ID, " +
                                 $"@CDEPT_CODE";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_CATEGORY_ID", DbType.String, 20, poEntity.Data.CUNIT_TYPE_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_CATEGORY_NAME", DbType.String, 200, poEntity.Data.CUNIT_TYPE_CATEGORY_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_TYPE", DbType.String, 2, poEntity.Data.CPROPERTY_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@IINVITATION_INVOICE_PERIOD", DbType.Int32, 50, poEntity.Data.IINVITATION_INVOICE_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, int.MaxValue, poEntity.Data.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 10, poEntity.Data.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 20, poEntity.CUSER_LOGIN_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poEntity.Data.CDEPT_CODE);


                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_MAINTAIN_UNIT_TYPE_CATEGORY {@Parameters} || RSP_GS_MAINTAIN_UNIT_TYPE_CATEGORYMethod(Cls) ", loDbParam);

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

        protected override async Task R_DeletingAsync(GSM02502ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Deleting");
            R_Exception loException = new R_Exception();

            try
            {
                await RSP_GS_MAINTAIN_UNIT_TYPE_CATEGORYMethod(poEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }

        protected override async Task<GSM02502ParameterDTO> R_DisplayAsync(GSM02502ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            R_Exception loException = new R_Exception();
            GSM02502ParameterDTO loResult = new GSM02502ParameterDTO();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = await loDb.GetConnectionAsync();

                lcQuery = $"EXEC RSP_GS_GET_UNIT_TYPE_CTG_DETAIL " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CPROPERTY_ID, " +
                    $"@CUNIT_TYPE_CATEGORY_ID, " +
                    $"@CUSER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_CATEGORY_ID", DbType.String, 50, poEntity.Data.CUNIT_TYPE_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_UNIT_TYPE_CTG_DETAIL {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

                loResult.Data = R_Utility.R_ConvertTo<GSM02502DetailDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override async Task R_SavingAsync(GSM02502ParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            R_Exception loException = new R_Exception();

            try
            {
               await RSP_GS_MAINTAIN_UNIT_TYPE_CATEGORYMethod(poNewEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }
    }
}
