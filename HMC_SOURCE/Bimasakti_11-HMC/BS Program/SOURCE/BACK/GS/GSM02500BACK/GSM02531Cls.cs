using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON.DTOs;
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
    public class GSM02531Cls : R_BusinessObject<GSM02531UtilityParameterDTO>
    {
        private LoggerGSM02531 _logger;
        private readonly ActivitySource _activitySource;
        public GSM02531Cls()
        {
            _logger = LoggerGSM02531.R_GetInstanceLogger();
            _activitySource = GSM02531ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public void RSP_GS_ACTIVE_INACTIVE_BUILDING_UNIT_UTILITIESMethod(GSM02500ActiveInactiveParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_ACTIVE_INACTIVE_BUILDING_UNIT_UTILITIESMethod");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                lcQuery = $"EXEC RSP_GS_ACTIVE_INACTIVE_BUILDING_UNIT_UTILITIES " +
                                 $"@CCOMPANY_ID, " +
                                 $"@CPROPERTY_ID, " +
                                 $"@CBUILDING_ID, " +
                                 $"@CFLOOR_ID, " +
                                 $"@CUNIT_ID, " +
                                 $"@CUTILITIES_TYPE, " +
                                 $"@CSEQUENCE, " +
                                 $"@LACTIVE, " +
                                 $"@CUSER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 50, poEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 50, poEntity.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 50, poEntity.CUNIT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUTILITIES_TYPE", DbType.String, 50, poEntity.CUTILITIES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE", DbType.String, 50, poEntity.CSEQUENCE);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 10, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_ACTIVE_INACTIVE_BUILDING_UNIT_UTILITIES {@Parameters} || RSP_GS_ACTIVE_INACTIVE_BUILDING_UNIT_UTILITIESMethod(Cls) ", loDbParam);

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

        private void RSP_GS_MAINTAIN_BUILDING_UNIT_UTILITIESMethod(GSM02531UtilityParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_MAINTAIN_BUILDING_UNIT_UTILITIESMethod");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = $"EXEC RSP_GS_MAINTAIN_BUILDING_UNIT_UTILITIES " +
                                 $"@CLOGIN_COMPANY_ID, " +
                                 $"@CSELECTED_PROPERTY_ID, " +
                                 $"@CSELECTED_BUILDING_ID, " +
                                 $"@CSELECTED_FLOOR_ID, " +
                                 $"@CSELECTED_UNIT_ID, " +
                                 $"@CSELECTED_UTILITIES_TYPE_ID, " +
                                 $"@CSEQUENCE, " +
                                 $"@CMETER_NO, " +
                                 $"@CALIAS_METER_NO, " +
                                 $"@NCALCULATION_FACTOR, " +
                                 $"@NCAPACITY, " +
                                 $"@IMETER_MAX_RESET, " +
                                 $"@LACTIVE, " +
                                 $"@CACTION, " +
                                 $"@CLOGIN_USER_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_BUILDING_ID", DbType.String, 50, poEntity.CSELECTED_BUILDING_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_FLOOR_ID", DbType.String, 50, poEntity.CSELECTED_FLOOR_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_UNIT_ID", DbType.String, 50, poEntity.CSELECTED_UNIT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_UTILITIES_TYPE_ID", DbType.String, 50, poEntity.CSELECTED_UTILITIES_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE", DbType.String, 50, poEntity.Data.CSEQUENCE);
                loDb.R_AddCommandParameter(loCmd, "@CMETER_NO", DbType.String, 50, poEntity.Data.CMETER_NO);
                loDb.R_AddCommandParameter(loCmd, "@CALIAS_METER_NO", DbType.String, 50, poEntity.Data.CALIAS_METER_NO);
                loDb.R_AddCommandParameter(loCmd, "@NCALCULATION_FACTOR", DbType.Decimal, 10, poEntity.Data.NCALCULATION_FACTOR);
                loDb.R_AddCommandParameter(loCmd, "@NCAPACITY", DbType.Decimal, 10, poEntity.Data.NCAPACITY);
                loDb.R_AddCommandParameter(loCmd, "@IMETER_MAX_RESET", DbType.Int32, 10, poEntity.Data.IMETER_MAX_RESET);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 10, poEntity.Data.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_MAINTAIN_BUILDING_UNIT_UTILITIES {@Parameters} || RSP_GS_MAINTAIN_BUILDING_UNIT_UTILITIESMethod(Cls) ", loDbParam);

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

        public List<GSM02531UtilityTypeDTO> GetUtilityTypeList(GetUtilityTypeParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetUtilityTypeList");
            R_Exception loException = new R_Exception();
            List<GSM02531UtilityTypeDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = $"SELECT CCODE, CDESCRIPTION " +
                    $"FROM RFT_GET_GSB_CODE_INFO " +
                    $"('BIMASAKTI', " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"'_BS_UTILITY_CHARGES_TYPE', " +
                    $"'01,02,03,04', " +
                    $"@CLOGIN_LANGUAGE_ID)";
                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_LANGUAGE_ID", DbType.String, 50, poEntity.CLOGIN_LANGUAGE_ID);

                string loCompanyIdLog = "";
                string loUserLanLog = "";
                List<DbParameter> loDbParam = loCmd.Parameters.Cast<DbParameter>().ToList();
                loDbParam.ForEach(x =>
                {
                    switch (x.ParameterName)
                    {
                        case "@CLOGIN_COMPANY_ID":
                            loCompanyIdLog = (string)x.Value;
                            break;
                        case "@CLOGIN_LANGUAGE_ID":
                            loUserLanLog = (string)x.Value;
                            break;
                    }
                });
                var loDebugLogResult = string.Format("SELECT CCODE, CDESCRIPTION FROM " +
                    "RFT_GET_GSB_CODE_INFO('BIMASAKTI', {0} , " +
                    "'_BS_UTILITY_CHARGES_TYPE', '' , {1}) || GetUtilityTypeList(Cls)", loCompanyIdLog, loUserLanLog);
                _logger.LogDebug(loDebugLogResult);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM02531UtilityTypeDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSM02531UtilityDTO> GetUtilitiesList(GetUtilitiesListParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetUtilitiesList");
            R_Exception loException = new R_Exception();
            List<GSM02531UtilityDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_GS_GET_BUILDING_UTILITIES_LIST " +
                                 $"@CLOGIN_COMPANY_ID, " +
                                 $"@CSELECTED_PROPERTY_ID, " +
                                 $"@CSELECTED_BUILDING_ID, " +
                                 $"@CSELECTED_FLOOR_ID, " +
                                 $"@CSELECTED_UNIT_ID, " +
                                 $"@CSELECTED_UTILITIES_TYPE_ID, " +
                                 $"@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_BUILDING_ID", DbType.String, 50, poEntity.CSELECTED_BUILDING_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_FLOOR_ID", DbType.String, 50, poEntity.CSELECTED_FLOOR_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_UNIT_ID", DbType.String, 50, poEntity.CSELECTED_UNIT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_UTILITIES_TYPE_ID", DbType.String, 50, poEntity.CSELECTED_UTILITIES_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_BUILDING_UTILITIES_LIST {@Parameters} || GetUtilitiesList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM02531UtilityDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override GSM02531UtilityParameterDTO R_Display(GSM02531UtilityParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            R_Exception loException = new R_Exception();
            GSM02531UtilityParameterDTO loResult = new GSM02531UtilityParameterDTO();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                lcQuery = $"EXEC RSP_GS_GET_BUILDING_UTILITIES_DETAIL " +
                                 $"@CLOGIN_COMPANY_ID, " +
                                 $"@CSELECTED_PROPERTY_ID, " +
                                 $"@CSELECTED_BUILDING_ID, " +
                                 $"@CSELECTED_FLOOR_ID, " +
                                 $"@CSELECTED_UNIT_ID, " +
                                 $"@CSELECTED_UTILITIES_TYPE_ID, " +
                                 $"@CSEQUENCE, " +
                                 $"@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_BUILDING_ID", DbType.String, 50, poEntity.CSELECTED_BUILDING_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_FLOOR_ID", DbType.String, 50, poEntity.CSELECTED_FLOOR_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_UNIT_ID", DbType.String, 50, poEntity.CSELECTED_UNIT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_UTILITIES_TYPE_ID", DbType.String, 50, poEntity.CSELECTED_UTILITIES_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE", DbType.String, 50, poEntity.Data.CSEQUENCE);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_BUILDING_UTILITIES_DETAIL {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult.Data = R_Utility.R_ConvertTo<GSM02531UtilityDetailDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(GSM02531UtilityParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            R_Exception loException = new R_Exception();

            try
            {
                RSP_GS_MAINTAIN_BUILDING_UNIT_UTILITIESMethod(poNewEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }

        protected override void R_Deleting(GSM02531UtilityParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Deleting");
            R_Exception loException = new R_Exception();

            try
            {
                RSP_GS_MAINTAIN_BUILDING_UNIT_UTILITIESMethod(poEntity);
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
