using System.Data;
using System.Data.Common;
using System.Diagnostics;
using PMB05000Common;
using PMB05000Common.DTOs;
using R_BackEnd;
using R_Common;

namespace PMB05000Back;

public class PMB05000Cls
{
    private LoggerPMB05000 _logger;
    private readonly ActivitySource _activitySource;

    public PMB05000Cls()
    {
        _logger = LoggerPMB05000.R_GetInstanceLogger();
        _activitySource = PMB05000Activity.R_GetInstanceActivitySource();
    }

    public async Task<PMB05000SystemParamDTO> GetSystemParam(PMB05000ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetSystemParam));
        R_Exception loEx = new();
        PMB05000SystemParamDTO loReturn = new();
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_PM_GET_SYSTEM_PARAM";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 8, poParams.CLANGUAGE_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParams.CPROPERTY_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CLANGUAGE_ID" or
                        "@CPROPERTY_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var DataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<PMB05000SystemParamDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }
    
    public async Task<List<PMB05000PropertyDTO>> GetProperties(PMB05000ParameterDb poParam)
    {

        using var loActivity = _activitySource.StartActivity(nameof(GetProperties));
        R_Exception loEx = new();
        List<PMB05000PropertyDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync();
            loCmd = loDb.GetCommand();

            lcQuery = @$"EXEC RSP_GS_GET_PROPERTY_LIST '{poParam.CCOMPANY_ID}', '{poParam.CUSER_ID}'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMB05000PropertyDTO>(loDataTable).ToList();
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
    
    public async Task<PMB05000PeriodYearRangeDTO> GetPeriod(PMB05000ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetPeriod));
        R_Exception loEx = new();
        PMB05000PeriodYearRangeDTO loReturn = new();
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync();
            loCmd = loDb.GetCommand();

            lcQuery = $"EXEC RSP_GS_GET_PERIOD_YEAR_RANGE '{poParams.CCOMPANY_ID}', '', ''";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var DataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<PMB05000PeriodYearRangeDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public async Task UpdateSoftClosePeriod(PMB05000ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(UpdateSoftClosePeriod));
        R_Exception loEx = new();
        // PMB05000PeriodYearRangeDTO loReturn = new();
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync();
            loCmd = loDb.GetCommand();

            lcQuery = $"RSP_PM_UPDATE_SOFT_PERIOD";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParams.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 6, poParams.CPERIOD);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParams.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CPERIOD" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
            await loDb.SqlExecNonQueryAsync(loConn, loCmd, true);
            // var DataTable = await loDb.SqlExecNonQueryAsync(loConn, loCmd, true);

            // loReturn = R_Utility.R_ConvertTo<PMB05000PeriodYearRangeDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task<List<PMB05000ValidateSoftCloseDTO>> ValidateSoftClosePeriod(PMB05000ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ValidateSoftClosePeriod));
        R_Exception loEx = new();
        List<PMB05000ValidateSoftCloseDTO> loReturn = new();
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync();
            loCmd = loDb.GetCommand();

            lcQuery = $"RSP_PM_VALIDATE_SOFT_CLOSE";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParams.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 6, poParams.CPERIOD);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParams.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CPERIOD" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);


            var DataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<PMB05000ValidateSoftCloseDTO>(DataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public async Task<PMB05000SoftClosePeriodDTO> ProcessSoftClosePeriod(PMB05000ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ProcessSoftClosePeriod));
        R_Exception loEx = new();
        PMB05000SoftClosePeriodDTO loReturn = new();
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync();
            loCmd = loDb.GetCommand();

            lcQuery = $"RSP_PM_SOFT_CLOSE_PERIOD_PROCESS";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParams.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 6, poParams.CPERIOD);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParams.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CPERIOD" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);


            var DataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<PMB05000SoftClosePeriodDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }
}