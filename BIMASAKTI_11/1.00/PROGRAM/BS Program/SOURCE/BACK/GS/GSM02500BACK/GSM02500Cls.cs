using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.Loggers;
using R_BackEnd;
using R_Common;
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
    public class GSM02500Cls
    {
        private LoggerGSM02500 _logger;
        private readonly ActivitySource _activitySource;
        public GSM02500Cls()
        {
            _logger = LoggerGSM02500.R_GetInstanceLogger();
            _activitySource = GSM02500ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public SelectedPropertyDTO GetSelectedProperty(SelectedPropertyParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetSelectedProperty");
            R_Exception loException = new R_Exception();
            SelectedPropertyDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_PROPERTY_DETAIL " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CPROPERTY_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.Data.CPROPERTY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_PROPERTY_DETAIL {@Parameters} || GetSelectedProperty(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<SelectedPropertyDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public SelectedBuildingDTO GetSelectedBuilding(SelectedBuildingParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetSelectedBuilding");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            string lcQuery;
            SelectedBuildingDTO loResult = null;
            DbCommand loCmd = null;

            try
            {
                loCmd = loDb.GetCommand();
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_BUILDING_DETAIL " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CSELECTED_PROPERTY_ID, " +
                    $"@CBUILDING_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 50, poEntity.Data.CBUILDING_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_BUILDING_DETAIL {@Parameters} || GetSelectedBuilding(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<SelectedBuildingDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public SelectedFloorDTO GetSelectedFloor(SelectedFloorParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetSelectedFloor");
            R_Exception loException = new R_Exception();
            SelectedFloorDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_BUILDING_FLOOR_DETAIL " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CSELECTED_PROPERTY_ID, " +
                    $"@CSELECTED_BUILDING_ID, " +
                    $"@CFLOOR_ID, " +
                    $"@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_BUILDING_ID", DbType.String, 50, poEntity.CSELECTED_BUILDING_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 50, poEntity.Data.CFLOOR_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_BUILDING_FLOOR_DETAIL {@Parameters} || GetSelectedFloor(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<SelectedFloorDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public SelectedUnitTypeDTO GetSelectedUnitType(SelectedUnitTypeParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetSelectedUnitType");
            R_Exception loException = new R_Exception();
            SelectedUnitTypeDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_UNIT_TYPE_DETAIL " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CSELECTED_PROPERTY_ID, " +
                    $"@CUNIT_TYPE_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_ID", DbType.String, 50, poEntity.Data.CUNIT_TYPE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_UNIT_TYPE_DETAIL {@Parameters} || GetSelectedUnitType(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<SelectedUnitTypeDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public SelectedUnitDTO GetSelectedUnit(SelectedUnitParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetSelectedUnit");
            R_Exception loException = new R_Exception();
            SelectedUnitDTO loResult = null;
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

                _logger.LogDebug("EXEC RSP_GS_GET_BUILDING_UNIT_DETAIL {@Parameters} || GetSelectedUnit(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<SelectedUnitDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GetUnitTypeDTO> GetUnitTypeList(GetUnitTypeParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetUnitTypeList");
            R_Exception loException = new R_Exception();
            List<GetUnitTypeDTO> loResult = null;
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
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_CATEGORY_ID", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_UNIT_TYPE_LIST {@Parameters} || GetUnitTypeList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetUnitTypeDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GetUnitCategoryDTO> GetUnitCategoryList(GetUnitCategoryParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetUnitCategoryList");
            R_Exception loException = new R_Exception();
            List<GetUnitCategoryDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO " +
                    $"('BIMASAKTI', " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"'_BS_UNIT_CATEGORY', " +
                    $"'', " +
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
                    "'_BS_UNIT_CATEGORY', '' , {1}) || GetUnitCategoryList(Cls)", loCompanyIdLog, loUserLanLog);
                _logger.LogDebug(loDebugLogResult);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetUnitCategoryDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public GetCUOMFromPropertyDTO GetCUOMFromProperty(GetCUOMFromPropertyParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetCUOMFromProperty");
            R_Exception loException = new R_Exception();
            GetCUOMFromPropertyDTO loResult = new GetCUOMFromPropertyDTO();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_PROPERTY_DETAIL " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CSELECTED_PROPERTY_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_PROPERTY_DETAIL {@Parameters} || GetCUOMFromProperty(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetCUOMFromPropertyDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GetUnitViewDTO> GetUnitViewList(GetUnitViewParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetUnitViewList");
            R_Exception loException = new R_Exception();
            List<GetUnitViewDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_UNIT_VIEW_LIST " +
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

                _logger.LogDebug("EXEC RSP_GS_GET_UNIT_VIEW_LIST {@Parameters} || GetUnitViewList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetUnitViewDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

    }
}
