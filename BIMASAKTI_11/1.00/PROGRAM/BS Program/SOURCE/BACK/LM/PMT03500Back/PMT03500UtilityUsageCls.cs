using System.Data;
using System.Data.Common;
using System.Diagnostics;
using PMT03500Common;
using PMT03500Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

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

    public List<PMT03500BuildingDTO> GetBuildingList(PMT03500ParameterDb poParam)
    {
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

            // lcQuery = peType == ELMT03500UtilityUsageTypeDb.EC
            //     ? "RSP_LM_GET_UTILITY_USAGE_LIST_EC"
            //     : "RSP_LM_GET_UTILITY_USAGE_LIST_WG";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poParam.CBUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUTILITY_TYPE", DbType.String, 2, poParam.CUTILITY_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CFLOOR_LIST", DbType.String, 255, poParam.CFLOOR_LIST);
            loDb.R_AddCommandParameter(loCmd, "@LALL_FLOOR", DbType.Boolean, 1, poParam.LALL_FLOOR);
            loDb.R_AddCommandParameter(loCmd, "@CINVOICE_PRD", DbType.String, 10, poParam.CINVOICE_PRD);
            loDb.R_AddCommandParameter(loCmd, "@LINVOICED", DbType.Boolean, 1, poParam.LINVOICED);
            loDb.R_AddCommandParameter(loCmd, "@CUTILITY_PRD", DbType.String, 10, poParam.CUTILITY_PRD);
            loDb.R_AddCommandParameter(loCmd, "@CUTILITY_PRD_FROM_DATE", DbType.String, 8,
                poParam.CUTILITY_PRD_FROM_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CUTILITY_PRD_TO_DATE", DbType.String, 8, poParam.CUTILITY_PRD_TO_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParam.CUSER_ID);


            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CBUILDING_ID" or
                        "@CUTILITY_TYPE" or
                        "@CFLOOR_LIST" or
                        "@LALL_FLOOR" or
                        "@CINVOICE_PRD" or
                        "@LINVOICED" or
                        "@CUTILITY_PRD" or
                        "@CUTILITY_PRD_FROM_DATE" or
                        "@CUTILITY_PRD_TO_DATE" or
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

    public PMT03500UtilityUsageDetailDTO GetUtilityUsageDetail(PMT03500ParameterDb poParam)
    {
        R_Exception loEx = new();
        PMT03500UtilityUsageDetailDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
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
            loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 2, poParam.CCHARGES_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poParam.CREF_NO);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParam.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CCHARGES_TYPE" or
                        "@CREF_NO" or
                        "@CUSER_ID"
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
}