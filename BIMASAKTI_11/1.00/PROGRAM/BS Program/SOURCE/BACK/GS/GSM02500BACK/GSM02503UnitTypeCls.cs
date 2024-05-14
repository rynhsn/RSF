using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02501;
using GSM02500COMMON.DTOs.GSM02503;
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
    public class GSM02503UnitTypeCls : R_BusinessObject<GSM02503UnitTypeParameterDTO>
    {
        RSP_GS_MAINTAIN_UNIT_TYPEResources.Resources_Dummy_Class _loRsp = new RSP_GS_MAINTAIN_UNIT_TYPEResources.Resources_Dummy_Class();

        private LoggerGSM02503UnitType _logger;
        private readonly ActivitySource _activitySource;
        public GSM02503UnitTypeCls()
        {
            _logger = LoggerGSM02503UnitType.R_GetInstanceLogger();
            _activitySource = GSM02503UnitTypeActivitySourceBase.R_GetInstanceActivitySource();
        }

        public List<GSM02503UnitTypeDTO> GetUnitTypeList(GetUnitTypeParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetUnitTypeList");
            R_Exception loException = new R_Exception();
            List<GSM02503UnitTypeDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_UNIT_TYPE_LIST " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CPROPERTY_ID, " +
                    $"@CUNIT_TYPE_CATEGORY_ID, " +
                    $"@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_CATEGORY_ID", DbType.String, 50, poEntity.CUNIT_TYPE_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_UNIT_TYPE_LIST {@Parameters} || GetUnitTypeList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM02503UnitTypeDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public void RSP_GS_ACTIVE_INACTIVE_UNIT_TYPEMethod(GSM02500ActiveInactiveParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_ACTIVE_INACTIVE_UNIT_TYPEMethod");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_ACTIVE_INACTIVE_UNIT_TYPE " +
                    $"@CCOMPANY_ID, " +
                    $"@CPROPERTY_ID, " +
                    $"@CUNIT_TYPE_ID, " +
                    $"@LACTIVE, " +
                    $"@CUSER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_ID", DbType.String, 50, poEntity.CUNIT_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 10, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_ACTIVE_INACTIVE_UNIT_TYPE {@Parameters} || RSP_GS_ACTIVE_INACTIVE_UNIT_TYPEMethod(Cls) ", loDbParam);

                loDb.SqlExecNonQuery(loConn, loCmd, true);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        private void RSP_GS_MAINTAIN_UNIT_TYPEMethod(GSM02503UnitTypeParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_MAINTAIN_UNIT_TYPEMethod");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = $"EXEC RSP_GS_MAINTAIN_UNIT_TYPE " +
                                 $"@CCOMPANY_ID, " +
                                 $"@CPROPERTY_ID, " +
                                 $"@CUNIT_TYPE_CATEGORY_ID, " +
                                 $"@CUNIT_TYPE_ID, " +
                                 $"@CUNIT_TYPE_NAME, " +
                                 $"@CDESCRIPTION, " +
                                 $"@NGROSS_AREA_SIZE, " +
                                 $"@NNET_AREA_SIZE, " +
                                 $"@NCOMMON_AREA_SIZE, " +
                                 $"@LACTIVE, " +
                                 $"@CACTION, " +
                                 $"@CUSER_LOGIN_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_CATEGORY_ID", DbType.String, 50, poEntity.CUNIT_TYPE_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_ID", DbType.String, 50, poEntity.Data.CUNIT_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_NAME", DbType.String, 50, poEntity.Data.CUNIT_TYPE_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 50, poEntity.Data.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@NGROSS_AREA_SIZE", DbType.Decimal, 10, poEntity.Data.NGROSS_AREA_SIZE);
                loDb.R_AddCommandParameter(loCmd, "@NNET_AREA_SIZE", DbType.Decimal, 10, poEntity.Data.NNET_AREA_SIZE);
                loDb.R_AddCommandParameter(loCmd, "@NCOMMON_AREA_SIZE", DbType.Decimal, 10, poEntity.Data.NCOMMON_AREA_SIZE);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 10, poEntity.Data.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, poEntity.CUSER_LOGIN_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_MAINTAIN_UNIT_TYPE {@Parameters} || RSP_GS_MAINTAIN_UNIT_TYPEMethod(Cls) ", loDbParam);

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

        protected override void R_Deleting(GSM02503UnitTypeParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Deleting");
            R_Exception loException = new R_Exception();

            try
            {
                RSP_GS_MAINTAIN_UNIT_TYPEMethod(poEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        protected override GSM02503UnitTypeParameterDTO R_Display(GSM02503UnitTypeParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            R_Exception loException = new R_Exception();
            GSM02503UnitTypeParameterDTO loResult = new GSM02503UnitTypeParameterDTO();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_UNIT_TYPE_DETAIL " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CPROPERTY_ID, " +
                    $"@CUNIT_TYPE_ID";


                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_ID", DbType.String, 50, poEntity.Data.CUNIT_TYPE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_UNIT_TYPE_DETAIL {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult.Data = R_Utility.R_ConvertTo<GSM02503UnitTypeDetailDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(GSM02503UnitTypeParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            R_Exception loException = new R_Exception();

            try
            {
                RSP_GS_MAINTAIN_UNIT_TYPEMethod(poNewEntity);
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
