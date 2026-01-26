using System.Data;
using System.Data.Common;
using System.Diagnostics;
using PMT06000Common;
using PMT06000Common.DTOs;
using R_BackEnd;
using R_Common;

namespace PMT06000Back;

public class PMT06000InitCls
{
    private LoggerPMT06000 _logger;
    private readonly ActivitySource _activitySource;

    public PMT06000InitCls()
    {
        _logger = LoggerPMT06000.R_GetInstanceLogger();
        _activitySource = PMT06000Activity.R_GetInstanceActivitySource();
    }
    
    public List<PMT06000PropertyDTO> GetPropertyList(PMT06000ParameterDb poParam)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GetPropertyList));
        R_Exception loEx = new();
        List<PMT06000PropertyDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = @$"EXEC RSP_GS_GET_PROPERTY_LIST '{poParam.CCOMPANY_ID}', '{poParam.CUSER_ID}'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT06000PropertyDTO>(loDataTable).ToList();
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
    
    public PMT06000YearRangeDTO GetYearRange(PMT06000ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetYearRange));
        R_Exception loEx = new();
        PMT06000YearRangeDTO loReturn = new();
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = $"EXEC RSP_GS_GET_PERIOD_YEAR_RANGE '{poParams.CCOMPANY_ID}', '', ''";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;
            
            _logger.LogDebug("{pcQuery}", lcQuery);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<PMT06000YearRangeDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }
    
    public List<PMT06000PeriodDTO> GetPeriodList(PMT06000ParameterDb poParam)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GetPeriodList));
        R_Exception loEx = new();
        List<PMT06000PeriodDTO> loRtn = null;
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

            loRtn = R_Utility.R_ConvertTo<PMT06000PeriodDTO>(loDataTable).ToList();
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

    public PMT06000GetSystemParamResultDTO PMT06000GetSystemParam(PMT06000GetSystemParamParameterDTO poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT06000GetSystemParam));
        R_Exception loEx = new();
        List<PMT06000GetSystemParamResultDTO> loReturnTemp = new List<PMT06000GetSystemParamResultDTO>();
        PMT06000GetSystemParamResultDTO loReturn = new PMT06000GetSystemParamResultDTO();
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_PM_GET_SYSTEM_PARAM ";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 8, poParams.CLANGUAGE_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParams.CPROPERTY_ID);

            _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p =>
            {
                string lcValue = p.Value?.ToString() ?? "NULL";
                // Add quotes for string and boolean types
                if (p.DbType == DbType.String || p.DbType == DbType.StringFixedLength || p.DbType == DbType.AnsiString || p.DbType == DbType.AnsiStringFixedLength ||
                    p.DbType == DbType.Boolean || p.Value is bool || p.Value is string)
                {
                    return $" {p.ParameterName} ='{lcValue}'";
                }
                return $" {p.ParameterName} ={lcValue}";
            })));

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturnTemp = R_Utility.R_ConvertTo<PMT06000GetSystemParamResultDTO>(DataTable).ToList();
            loReturn = loReturnTemp.FirstOrDefault() ?? new PMT06000GetSystemParamResultDTO();
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