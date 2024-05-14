using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02520;
using GSM02500COMMON.DTOs.GSM02530;
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
    public class GSM02530Cls : R_BusinessObject<GSM02530ParameterDTO>
    {
        RSP_GS_MAINTAIN_BUILDING_UNITResources.Resources_Dummy_Class _loRsp = new RSP_GS_MAINTAIN_BUILDING_UNITResources.Resources_Dummy_Class();

        private LoggerGSM02530 _logger;
        private readonly ActivitySource _activitySource;
        public GSM02530Cls()
        {
            _logger = LoggerGSM02530.R_GetInstanceLogger();
            _activitySource = GSM02530ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public void RSP_GS_ACTIVE_INACTIVE_BUILDING_UNITMethod(GSM02500ActiveInactiveParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_ACTIVE_INACTIVE_BUILDING_UNITMethod");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                lcQuery = $"EXEC RSP_GS_ACTIVE_INACTIVE_BUILDING_UNIT " +
                                 $"@CCOMPANY_ID, " +
                                 $"@CPROPERTY_ID, " +
                                 $"@CBUILDING_ID, " +
                                 $"@CFLOOR_ID, " +
                                 $"@CUNIT_ID, " +
                                 $"@LACTIVE, " +
                                 $"@CUSER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 50, poEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 50, poEntity.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 50, poEntity.CUNIT_ID);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 10, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_ACTIVE_INACTIVE_BUILDING_UNIT {@Parameters} || RSP_GS_ACTIVE_INACTIVE_BUILDING_UNITMethod(Cls) ", loDbParam);

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

        private void RSP_GS_MAINTAIN_BUILDING_UNITMethod(GSM02530ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_MAINTAIN_BUILDING_UNITMethod");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            
            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = $"EXEC RSP_GS_MAINTAIN_BUILDING_UNIT " +
                                 $"@CLOGIN_COMPANY_ID, " +
                                 $"@CSELECTED_PROPERTY_ID, " +
                                 $"@CSELECTED_BUILDING_ID, " +
                                 $"@CSELECTED_FLOOR_ID, " +
                                 $"@CUNIT_ID, " +
                                 $"@CUNIT_NAME, " +
                                 $"@CDESCRIPTION, " +
                                 $"@CUNIT_TYPE_ID, " +
                                 $"@NGROSS_AREA_SIZE, " +
                                 $"@NNET_AREA_SIZE, " +
                                 $"@NCOMMON_AREA_SIZE, " +
                                 $"@CUNIT_VIEW_ID, " +
                                 $"@CUNIT_CATEGORY_ID, " +
                                 $"@LACTIVE, " +
                                 $"@CACTION, " +
                                 $"@CLOGIN_USER_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_BUILDING_ID", DbType.String, 50, poEntity.CSELECTED_BUILDING_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_FLOOR_ID", DbType.String, 50, poEntity.CSELECTED_FLOOR_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 50, poEntity.Data.CUNIT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_NAME", DbType.String, 50, poEntity.Data.CUNIT_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 50, poEntity.Data.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_ID", DbType.String, 50, poEntity.Data.CUNIT_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@NGROSS_AREA_SIZE", DbType.Decimal, 18, poEntity.Data.NGROSS_AREA_SIZE);
                loDb.R_AddCommandParameter(loCmd, "@NNET_AREA_SIZE", DbType.Decimal, 18, poEntity.Data.NNET_AREA_SIZE);
                loDb.R_AddCommandParameter(loCmd, "@NCOMMON_AREA_SIZE", DbType.Decimal, 18, poEntity.Data.NCOMMON_AREA_SIZE);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_VIEW_ID", DbType.String, 50, poEntity.Data.CUNIT_VIEW_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_CATEGORY_ID", DbType.String, 50, poEntity.Data.CUNIT_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 10, poEntity.Data.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_MAINTAIN_BUILDING_UNIT {@Parameters} || RSP_GS_MAINTAIN_BUILDING_UNITMethod(Cls) ", loDbParam);

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

        public List<GSM02530DTO> GetUnitInfoList(GetUnitInfoListParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetUnitInfoList");
            R_Exception loException = new R_Exception();
            List<GSM02530DTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_BUILDING_UNIT_LIST " +
                                 $"@CLOGIN_COMPANY_ID, " +
                                 $"@CSELECTED_PROPERTY_ID, " +
                                 $"@CSELECTED_BUILDING_ID, " +
                                 $"@CSELECTED_FLOOR_ID, " +
                                 $"@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_BUILDING_ID", DbType.String, 50, poEntity.CSELECTED_BUILDING_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_FLOOR_ID", DbType.String, 50, poEntity.CSELECTED_FLOOR_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_BUILDING_UNIT_LIST {@Parameters} || GetUnitInfoList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM02530DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }


        protected override GSM02530ParameterDTO R_Display(GSM02530ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            R_Exception loException = new R_Exception();
            GSM02530ParameterDTO loResult = new GSM02530ParameterDTO();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_BUILDING_UNIT_DETAIL " +
                                 $"@CLOGIN_COMPANY_ID, " +
                                 $"@CSELECTED_PROPERTY_ID, " +
                                 $"@CSELECTED_BUILDING_ID, " +
                                 $"@CSELECTED_FLOOR_ID, " +
                                 $"@CUNIT_ID, " +
                                 $"@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_BUILDING_ID", DbType.String, 50, poEntity.CSELECTED_BUILDING_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_FLOOR_ID", DbType.String, 50, poEntity.CSELECTED_FLOOR_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 50, poEntity.Data.CUNIT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_BUILDING_UNIT_DETAIL {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult.Data = R_Utility.R_ConvertTo<GSM02530DetailDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(GSM02530ParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            R_Exception loException = new R_Exception();

            try
            {
                RSP_GS_MAINTAIN_BUILDING_UNITMethod(poNewEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }

        protected override void R_Deleting(GSM02530ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Deleting");
            R_Exception loException = new R_Exception();

            try
            {
                RSP_GS_MAINTAIN_BUILDING_UNITMethod(poEntity);
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
