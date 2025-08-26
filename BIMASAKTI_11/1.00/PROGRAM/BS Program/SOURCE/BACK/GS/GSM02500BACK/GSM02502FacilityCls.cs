using GSM02500COMMON.DTOs.GSM02502Charge;
using GSM02500COMMON.DTOs.GSM02502Facility;
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
using GSM02500COMMON.Loggers;
using System.Diagnostics;
using GSM02500BACK.OpenTelemetry;


namespace GSM02500BACK
{
    public class GSM02502FacilityCls : R_BusinessObjectAsync<GSM02502FacilityParameterDTO>
    {
        private LoggerGSM02502Facility _logger;
        private readonly ActivitySource _activitySource;
        public GSM02502FacilityCls()
        {
            _logger = LoggerGSM02502Facility.R_GetInstanceLogger();
            _activitySource = GSM02502FacilityActivitySourceBase.R_GetInstanceActivitySource();
        }

        public async Task<List<GSM02502FacilityDTO>> GetFacilityList(GetFacilityListDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetFacilityList");
            R_Exception loException = new R_Exception();
            List<GSM02502FacilityDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = await loDb.GetConnectionAsync();

                lcQuery = $"EXEC RSP_GS_GET_UNIT_TYPE_CTG_FACILITY_LIST " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CSELECTED_PROPERTY_ID, " +
                    $"@CSELECTED_UNIT_TYPE_CATEGORY_ID, " +
                    $"@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_UNIT_TYPE_CATEGORY_ID", DbType.String, 50, poEntity.CUNIT_TYPE_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_UNIT_TYPE_CTG_Facility_LIST {@Parameters} || GetFacilityList(Cls) ", loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM02502FacilityDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<List<GSM02502FacilityTypeDTO>> GetFacilityTypeList()
        {
            using Activity activity = _activitySource.StartActivity("GetFacilityTypeList");
            R_Exception loException = new R_Exception();
            List<GSM02502FacilityTypeDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = await loDb.GetConnectionAsync();

                lcQuery = $"SELECT CCODE, CDESCRIPTION " +
                    $"FROM RFT_GET_GSB_CODE_INFO " +
                    $"('BIMASAKTI', " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"'_BS_FACILITY_TYPE', " +
                    $"'', " +
                    $"@CLOGIN_LANGUAGE_ID)";
                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_LANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

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
                    "'_BS_FACILITY_TYPE', '' , {1}) || GetFacilityTypeList(Cls)", loCompanyIdLog, loUserLanLog);
                _logger.LogDebug(loDebugLogResult);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM02502FacilityTypeDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        private async Task RSP_GS_MAINTAIN_UNIT_TYPE_CTG_FACILITYMethod(GSM02502FacilityParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("RSP_GS_MAINTAIN_UNIT_TYPE_CTG_FACILITYMethod");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();

                lcQuery = $"EXEC RSP_GS_MAINTAIN_UNIT_TYPE_CTG_FACILITY " +
                                 $"@CLOGIN_COMPANY_ID, " +
                                 $"@CSELECTED_PROPERTY_ID, " +
                                 $"@CSELECTED_UNIT_TYPE_CATEGORY_ID, " +
                                 $"@CFACILITY_TYPE, " +
                                 $"@IQTY, " +
                                 $"@IYEARS, " +
                                 $"@IMONTHS, " +
                                 $"@IDAYS, " +
                                 $"@LACTIVE, " +
                                 $"@CACTION, " +
                                 $"@CLOGIN_USER_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_UNIT_TYPE_CATEGORY_ID", DbType.String, 50, poEntity.CUNIT_TYPE_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFACILITY_TYPE", DbType.String, 50, poEntity.Data.CFACILITY_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@IQTY", DbType.Int32, 50, poEntity.Data.IQTY);
                loDb.R_AddCommandParameter(loCmd, "@IYEARS", DbType.Int32, 50, poEntity.Data.IYEARS);
                loDb.R_AddCommandParameter(loCmd, "@IMONTHS", DbType.Int32, 50, poEntity.Data.IMONTHS);
                loDb.R_AddCommandParameter(loCmd, "@IDAYS", DbType.Int32, 50, poEntity.Data.IDAYS);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 10, poEntity.Data.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_MAINTAIN_UNIT_TYPE_CTG_FACILITY {@Parameters} || RSP_GS_MAINTAIN_UNIT_TYPE_CTG_FACILITYMethod(Cls) ", loDbParam);

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

        protected override async Task<GSM02502FacilityParameterDTO> R_DisplayAsync(GSM02502FacilityParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            R_Exception loException = new R_Exception();
            GSM02502FacilityParameterDTO loResult = new GSM02502FacilityParameterDTO();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = await loDb.GetConnectionAsync();

                lcQuery = $"EXEC RSP_GS_GET_UNIT_TYPE_CTG_FACILITY_DT " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CSELECTED_PROPERTY_ID, " +
                    $"@CSELECTED_UNIT_TYPE_CATEGORY_ID, " +
                    $"@CFACILITY_TYPE, " +
                    $"@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;


                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_UNIT_TYPE_CATEGORY_ID", DbType.String, 50, poEntity.CUNIT_TYPE_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFACILITY_TYPE", DbType.String, 50, poEntity.Data.CFACILITY_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_UNIT_TYPE_CTG_FACILITY_DT {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

                loResult.Data = R_Utility.R_ConvertTo<GSM02502FacilityDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override async Task R_SavingAsync(GSM02502FacilityParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            R_Exception loException = new R_Exception();

            try
            {
                await RSP_GS_MAINTAIN_UNIT_TYPE_CTG_FACILITYMethod(poNewEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }

        protected override async Task R_DeletingAsync(GSM02502FacilityParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Deleting");
            R_Exception loException = new R_Exception();

            try
            {
                await RSP_GS_MAINTAIN_UNIT_TYPE_CTG_FACILITYMethod(poEntity);
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

