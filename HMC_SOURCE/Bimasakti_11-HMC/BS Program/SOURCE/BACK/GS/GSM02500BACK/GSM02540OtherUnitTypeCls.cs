using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02530;
using GSM02500COMMON.DTOs.GSM02540;
using GSM02500COMMON.DTOs.GSM02541;
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
    public class GSM02540OtherUnitTypeCls : R_BusinessObject<GSM02540ParameterDTO>
    {
        RSP_GS_MAINTAIN_OTHER_UNIT_TYPEResources.Resources_Dummy_Class _loRsp = new RSP_GS_MAINTAIN_OTHER_UNIT_TYPEResources.Resources_Dummy_Class();

        private LoggerGSM02540 _logger;
        private readonly ActivitySource _activitySource;
        public GSM02540OtherUnitTypeCls()
        {
            _logger = LoggerGSM02540.R_GetInstanceLogger();
            _activitySource = GSM02540OtherUnitTypeActivitySourceBase.R_GetInstanceActivitySource();
        }

        public void RSP_GS_ACTIVE_INACTIVE_OTHER_UNIT_TYPEMethod(GSM02500ActiveInactiveParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_ACTIVE_INACTIVE_OTHER_UNIT_TYPEMethod");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_GS_ACTIVE_INACTIVE_OTHER_UNIT_TYPE " +
                                 $"@CCOMPANY_ID, " +
                                 $"@CPROPERTY_ID, " +
                                 $"@COTHER_UNIT_TYPE_ID, " +
                                 $"@LACTIVE, " +
                                 $"@CUSER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@COTHER_UNIT_TYPE_ID", DbType.String, 50, poEntity.COTHER_UNIT_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 10, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_ACTIVE_INACTIVE_OTHER_UNIT_TYPE {@Parameters} || RSP_GS_ACTIVE_INACTIVE_OTHER_UNIT_TYPEMethod(Cls) ", loDbParam);

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

        private void RSP_GS_MAINTAIN_OTHER_UNIT_TYPEMethod(GSM02540ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_MAINTAIN_OTHER_UNIT_TYPEMethod");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = $"EXEC RSP_GS_MAINTAIN_OTHER_UNIT_TYPE " +
                                 $"@CLOGIN_COMPANY_ID, " +
                                 $"@CSELECTED_PROPERTY_ID, " +
                                 $"@COTHER_UNIT_TYPE_ID, " +
                                 $"@COTHER_UNIT_TYPE_NAME, " +
                                 $"@LACTIVE, " +
                                 $"@CDESCRIPTION, " +
                                 $"@NGROSS_AREA_SIZE, " +
                                 $"@NNET_AREA_SIZE, " +
                                 $"@LEVENT, " +
                                 $"@CACTION, " +
                                 $"@CLOGIN_USER_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@COTHER_UNIT_TYPE_ID", DbType.String, 50, poEntity.Data.COTHER_UNIT_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@COTHER_UNIT_TYPE_NAME", DbType.String, 50, poEntity.Data.COTHER_UNIT_TYPE_NAME);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 10, poEntity.Data.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 50, poEntity.Data.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@NGROSS_AREA_SIZE", DbType.Decimal, 10, poEntity.Data.NGROSS_AREA_SIZE);
                loDb.R_AddCommandParameter(loCmd, "@NNET_AREA_SIZE", DbType.Decimal, 10, poEntity.Data.NNET_AREA_SIZE);
                loDb.R_AddCommandParameter(loCmd, "@LEVENT", DbType.Boolean, 10, poEntity.Data.LEVENT);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_MAINTAIN_OTHER_UNIT_TYPE {@Parameters} || RSP_GS_MAINTAIN_OTHER_UNIT_TYPEMethod(Cls) ", loDbParam);

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

        public List<GSM02540DTO> GetOtherUnitTypeList(GetOtherUnitTypeListParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetOtherUnitTypeList");
            R_Exception loException = new R_Exception();
            List<GSM02540DTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_GS_GET_OTHER_UNIT_TYPE " +
                    $"@CLOGIN_COMPANY_ID," +
                    $"@CSELECTED_PROPERTY_ID, " +
                    $"@COTHER_UNIT_TYPE_ID, " +
                    $"@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@COTHER_UNIT_TYPE_ID", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_OTHER_UNIT_TYPE {@Parameters} || GetOtherUnitTypeList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM02540DTO>(loDataTable).ToList();

                loResult.ForEach(x => x.COTHER_UNIT_TYPE_ID_NAME = x.COTHER_UNIT_TYPE_NAME + '(' + x.COTHER_UNIT_TYPE_ID + ')');
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }
        protected override void R_Deleting(GSM02540ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Deleting");
            R_Exception loException = new R_Exception();

            try
            {
                RSP_GS_MAINTAIN_OTHER_UNIT_TYPEMethod(poEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }

        protected override GSM02540ParameterDTO R_Display(GSM02540ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            R_Exception loException = new R_Exception();
            GSM02540ParameterDTO loResult = new GSM02540ParameterDTO();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_GS_GET_OTHER_UNIT_TYPE " +
                                 $"@CLOGIN_COMPANY_ID, " +
                                 $"@CSELECTED_PROPERTY_ID, " +
                                 $"@COTHER_UNIT_TYPE_ID, " +
                                 $"@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@COTHER_UNIT_TYPE_ID", DbType.String, 50, poEntity.Data.COTHER_UNIT_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_OTHER_UNIT_TYPE {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult.Data = R_Utility.R_ConvertTo<GSM02540DetailDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(GSM02540ParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            R_Exception loException = new R_Exception();

            try
            {
                RSP_GS_MAINTAIN_OTHER_UNIT_TYPEMethod(poNewEntity);
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
