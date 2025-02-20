using System.Data;
using System.Data.Common;
using System.Diagnostics;
using HDI00100Common;
using HDI00100Common.DTOs;
using R_BackEnd;
using R_Common;

namespace HDI00100Back;

public class HDI00100Cls
{
    
    private LoggerHDI00100 _logger;
    private readonly ActivitySource _activitySource;

    public HDI00100Cls()
    {
        _logger = LoggerHDI00100.R_GetInstanceLogger();
        _activitySource = HDI00100Activity.R_GetInstanceActivitySource();
    }
    
    

    public List<HDI00100TaskSchedulerDTO> GetPublicLocationList(HDI00100ParameterDb poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetPublicLocationList));
        R_Exception loEx = new();
        List<HDI00100TaskSchedulerDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_HD_GET_TASK_SCHEDULER_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 4, poParameter.CYEAR);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_DATE", DbType.String, 8, poParameter.CFROM_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CTO_DATE", DbType.String, 8, poParameter.CTO_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poParameter.CBUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 20, poParameter.CASSET_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CSTATUS", DbType.String, 2, poParameter.CSTATUS);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CYEAR" or
                        "@CFROM_DATE" or
                        "@CTO_DATE" or
                        "@CBUILDING_ID" or
                        "@CASSET_CODE" or
                        "@CSTATUS" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<HDI00100TaskSchedulerDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    public List<HDI00100PropertyDTO> GetPropertyList(HDI00100ParameterDb poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetPropertyList));
        R_Exception loEx = new();
        List<HDI00100PropertyDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_PROPERTY_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poParameter.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<HDI00100PropertyDTO>(loDataTable).ToList();
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