using System.Data;
using System.Data.Common;
using System.Diagnostics;
using PMT03500Common;
using PMT03500Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using R_Storage;
using R_StorageCommon;

namespace PMT03500Back;

public class PMT03500UtilityUsageCls
{
    private LoggerPMT03500 _logger;
    private readonly ActivitySource _activitySource;

    public PMT03500UtilityUsageCls()
    {
        _logger = LoggerPMT03500.R_GetInstanceLogger();
        _activitySource = PMT03500Activity.R_GetInstanceActivitySource();
    }
    
    public PMT03500SystemParamDTO GetSystemParam(PMT03500ParameterDb poParam)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GetSystemParam));
        R_Exception loEx = new();
        PMT03500SystemParamDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = @"RSP_PM_GET_SYSTEM_PARAM";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 8, poParam.CLANGUAGE_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);


            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CLANGUAGE_ID" or
                        "@CPROPERTY_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT03500SystemParamDTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    public List<PMT03500BuildingDTO> GetBuildingList(PMT03500ParameterDb poParam)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GetBuildingList));
        R_Exception loEx = new();
        List<PMT03500BuildingDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_BUILDING_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poParam.CUSER_ID);


            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT03500BuildingDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    public List<PMT03500UtilityUsageDTO> GetUtilityUsageList(PMT03500ParameterDb poParam)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GetUtilityUsageList));
        R_Exception loEx = new();
        List<PMT03500UtilityUsageDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        var lcQuery = "";

        List<string> loTypeEC = new() { "01", "02" };
        List<string> loTypeWG = new() { "03", "04" };
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            if (loTypeEC.Contains(poParam.CUTILITY_TYPE))
            {
                lcQuery = "RSP_PM_GET_UTILITY_USAGE_LIST_EC";
            }
            else if (loTypeWG.Contains(poParam.CUTILITY_TYPE))
            {
                lcQuery = "RSP_PM_GET_UTILITY_USAGE_LIST_WG";
            }

            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poParam.CBUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUTILITY_TYPE", DbType.String, 2, poParam.CUTILITY_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 255, poParam.CFLOOR_ID);
            // loDb.R_AddCommandParameter(loCmd, "@LALL_FLOOR", DbType.Boolean, 1, poParam.LALL_FLOOR);
            loDb.R_AddCommandParameter(loCmd, "@CINVOICE_PRD", DbType.String, 10, poParam.CINVOICE_PRD);
            loDb.R_AddCommandParameter(loCmd, "@LINVOICED", DbType.Boolean, 1, poParam.LINVOICED);
            loDb.R_AddCommandParameter(loCmd, "@CUTILITY_PRD", DbType.String, 10, poParam.CUTILITY_PRD);
            loDb.R_AddCommandParameter(loCmd, "@CUTILITY_PRD_FROM_DATE", DbType.String, 8,
                poParam.CUTILITY_PRD_FROM_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CUTILITY_PRD_TO_DATE", DbType.String, 8, poParam.CUTILITY_PRD_TO_DATE);
            loDb.R_AddCommandParameter(loCmd, "@LOTHER_UNIT", DbType.Boolean, 1, poParam.LOTHER_UNIT);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParam.CUSER_ID);


            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CBUILDING_ID" or
                        "@CUTILITY_TYPE" or
                        "@CFLOOR_ID" or
                        // "@LALL_FLOOR" or
                        "@CINVOICE_PRD" or
                        "@LINVOICED" or
                        "@CUTILITY_PRD" or
                        "@CUTILITY_PRD_FROM_DATE" or
                        "@CUTILITY_PRD_TO_DATE" or
                        "@LOTHER_UNIT" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT03500UtilityUsageDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
    
    public List<PMT03500UtilityUsageDTO> GetUtilityCutOffList(PMT03500ParameterDb poParam)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GetUtilityCutOffList));
        R_Exception loEx = new();
        List<PMT03500UtilityUsageDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        var lcQuery = "";

        List<string> loTypeEC = new() { "01", "02" };
        List<string> loTypeWG = new() { "03", "04" };
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            if (loTypeEC.Contains(poParam.CUTILITY_TYPE))
            {
                lcQuery = "RSP_PM_GET_UTILITY_CUTOFF_LIST_EC";
            }
            else if (loTypeWG.Contains(poParam.CUTILITY_TYPE))
            {
                lcQuery = "RSP_PM_GET_UTILITY_CUTOFF_LIST_WG";
            }
            
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poParam.CBUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUTILITY_TYPE", DbType.String, 2, poParam.CUTILITY_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 255, poParam.CFLOOR_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUTILITY_PRD", DbType.String, 10, poParam.CINVOICE_PRD);
            loDb.R_AddCommandParameter(loCmd, "@LOTHER_UNIT", DbType.Boolean, 1, poParam.LOTHER_UNIT);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParam.CUSER_ID);


            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CBUILDING_ID" or
                        "@CUTILITY_TYPE" or
                        "@CFLOOR_ID" or
                        "@CUTILITY_PRD" or
                        "@LOTHER_UNIT" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT03500UtilityUsageDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    public PMT03500UtilityUsageDetailDTO GetUtilityUsageDetailPhoto(PMT03500ParameterDb poParam)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GetUtilityUsageDetailPhoto));
        var loEx = new R_Exception();
        PMT03500UtilityUsageDetailDTO loResult = null;
        R_Db loDb;
        DbConnection loConn;
        R_ReadParameter loReadParameter;
        R_ReadResult loReadResult = null;
        try
        {
            loResult = GetUtilityUsageDetail(poParam);
    
            loDb = new R_Db();
            loConn = loDb.GetConnection();

            if (String.IsNullOrEmpty(loResult.CSTART_PHOTO1_STORAGE_ID) == false)
            {
                loReadParameter = new R_ReadParameter()
                {
                    StorageId = loResult.CSTART_PHOTO1_STORAGE_ID
                };

                loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);
                loResult.ODATA_START_PHOTO1 = loReadResult.Data;
                loResult.CFILE_NAME_START_PHOTO1 = loReadResult.FileName;
                loResult.CFILE_EXTENSION_START_PHOTO1 = loReadResult.FileExtension;
            }
            
            if (String.IsNullOrEmpty(loResult.CEND_PHOTO1_STORAGE_ID) == false)
            {
                loReadParameter = new R_ReadParameter()
                {
                    StorageId = loResult.CEND_PHOTO1_STORAGE_ID
                };

                loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);
                loResult.ODATA_END_PHOTO1 = loReadResult.Data;
                loResult.CFILE_NAME_END_PHOTO1 = loReadResult.FileName;
                loResult.CFILE_EXTENSION_END_PHOTO1 = loReadResult.FileExtension;
            }
            
            if (String.IsNullOrEmpty(loResult.CSTART_PHOTO2_STORAGE_ID) == false)
            {
                loReadParameter = new R_ReadParameter()
                {
                    StorageId = loResult.CSTART_PHOTO2_STORAGE_ID
                };

                loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);
                loResult.ODATA_START_PHOTO2 = loReadResult.Data;
                loResult.CFILE_NAME_START_PHOTO2 = loReadResult.FileName;
                loResult.CFILE_EXTENSION_START_PHOTO2 = loReadResult.FileExtension;
            }
            
            if (String.IsNullOrEmpty(loResult.CEND_PHOTO2_STORAGE_ID) == false)
            {
                loReadParameter = new R_ReadParameter()
                {
                    StorageId = loResult.CEND_PHOTO2_STORAGE_ID
                };

                loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);
                loResult.ODATA_END_PHOTO2 = loReadResult.Data;
                loResult.CFILE_NAME_END_PHOTO2 = loReadResult.FileName;
                loResult.CFILE_EXTENSION_END_PHOTO2 = loReadResult.FileExtension;
            }
            
            if (String.IsNullOrEmpty(loResult.CSTART_PHOTO3_STORAGE_ID) == false)
            {
                loReadParameter = new R_ReadParameter()
                {
                    StorageId = loResult.CSTART_PHOTO3_STORAGE_ID
                };

                loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);
                loResult.ODATA_START_PHOTO3 = loReadResult.Data;
                loResult.CFILE_NAME_START_PHOTO3 = loReadResult.FileName;
                loResult.CFILE_EXTENSION_START_PHOTO3 = loReadResult.FileExtension;
            }
            
            if (String.IsNullOrEmpty(loResult.CEND_PHOTO3_STORAGE_ID) == false)
            {
                loReadParameter = new R_ReadParameter()
                {
                    StorageId = loResult.CEND_PHOTO3_STORAGE_ID
                };

                loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);
                loResult.ODATA_END_PHOTO3 = loReadResult.Data;
                loResult.CFILE_NAME_END_PHOTO3 = loReadResult.FileName;
                loResult.CFILE_EXTENSION_END_PHOTO3 = loReadResult.FileExtension;
            }     
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        loEx.ThrowExceptionIfErrors();

        return loResult;


    }
    
    public PMT03500UtilityUsageDetailDTO GetUtilityUsageDetail(PMT03500ParameterDb poParam)
    {
        R_Exception loEx = new();
        PMT03500UtilityUsageDetailDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        R_ReadParameter loReadParameter;
        R_ReadResult loReadResult = null;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_PM_GET_UTILITY_INFO";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParam.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poParam.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poParam.CREF_NO);
            loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 20, poParam.CUNIT_ID);
            loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 20, poParam.CFLOOR_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poParam.CBUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 2, poParam.CCHARGES_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 20, poParam.CCHARGES_ID);
            
            loDb.R_AddCommandParameter(loCmd, "@CCHARGES_SEQ_NO", DbType.String, 3, poParam.CSEQ_NO);
            
            loDb.R_AddCommandParameter(loCmd, "@CINV_PRD", DbType.String, 6, poParam.CINV_PRD);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParam.CUSER_ID);
            
            //
            // loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
            // loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            // loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 2, poParam.CCHARGES_TYPE);
            // loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poParam.CREF_NO);
            // loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParam.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CDEPT_CODE" or
                        "@CTRANS_CODE" or
                        "@CREF_NO" or
                        "@CUNIT_ID" or
                        "@CFLOOR_ID" or
                        "@CBUILDING_ID" or
                        "@CCHARGES_TYPE" or
                        "@CCHARGES_ID" or
                        "@CCHARGES_SEQ_NO" or
                        "@CSEQ_NO" or
                        "@CINV_PRD" or
                        "@CUSER_ID"

                    // "@CCOMPANY_ID" or
                        // "@CPROPERTY_ID" or
                        // "@CCHARGES_TYPE" or
                        // "@CREF_NO" or
                        // "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT03500UtilityUsageDetailDTO>(loDataTable).FirstOrDefault();
            
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    public List<PMT03500FunctDTO> GetUtilityTypeList(PMT03500ParameterDb poParam)
    {
        R_Exception loEx = new();
        List<PMT03500FunctDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery =
                @$"SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO ('BIMASAKTI', '{poParam.CCOMPANY_ID}', '_BS_UTILITY_CHARGES_TYPE', ',01,02,03,04', '{poParam.CLANGUAGE_ID}')";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT03500FunctDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    public List<PMT03500FloorDTO> GetFloorList(PMT03500ParameterDb poParam)
    {
        R_Exception loEx = new();
        List<PMT03500FloorDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_BUILDING_FLOOR_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poParam.CBUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poParam.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CBUILDING_ID" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT03500FloorDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    public List<PMT03500PeriodDTO> GetPeriodList(PMT03500ParameterDb poParam)
    {
        R_Exception loEx = new();
        List<PMT03500PeriodDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_PERIOD_DT_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 4, poParam.CYEAR);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CYEAR"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT03500PeriodDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
    
    public PMT03500PeriodDTO GetPeriod(PMT03500ParameterDb poParam)
    {
        R_Exception loEx = new();
        PMT03500PeriodDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_PERIOD_DT_INFO";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 4, poParam.CYEAR);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD_NO", DbType.String, 4, poParam.CPERIOD_NO);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CYEAR" or
                        "@CPERIOD_NO"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT03500PeriodDTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
    
    
    public List<PMT03500YearDTO> GetYearList(PMT03500ParameterDb poParam)
    {
        R_Exception loEx = new();
        List<PMT03500YearDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_PERIOD_YEAR_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT03500YearDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    public List<PMT03500RateWGListDTO> GetRateWGList(PMT03500ParameterDb poParam)
    {
        R_Exception loEx = new();
        List<PMT03500RateWGListDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_PM_GET_UTILITY_INFO_RATE_WG";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 20, poParam.CCHARGES_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 20, poParam.CCHARGES_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParam.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CCHARGES_DATE", DbType.String, 8, poParam.CSTART_DATE);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CCHARGE_TYPE_ID" or
                        "@CCHARGES_ID" or
                        "@CUSER_ID" or  
                        "@CCHARGES_DATE"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT03500RateWGListDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }
}