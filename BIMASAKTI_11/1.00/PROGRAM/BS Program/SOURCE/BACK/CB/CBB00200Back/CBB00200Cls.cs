using System.Data;
using System.Data.Common;
using System.Diagnostics;
using CBB00200Common;
using CBB00200Common.DTOs;
using R_BackEnd;
using R_Common;
using RSP_CB_CLOSE_PERIODResources;

namespace CBB00200Back;

public class CBB00200Cls
{
    Resources_Dummy_Class _resources = new();
    
    private LoggerCBB00200 _logger;
    private readonly ActivitySource _activitySource;
    
    
    public CBB00200Cls()
    {
        _logger = LoggerCBB00200.R_GetInstanceLogger();
        _activitySource = CBB00200Activity.R_GetInstanceActivitySource();
    }
    
    public CBB00200SystemParamDTO CBB00200GetSystemParamDb(CBB00200ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(CBB00200GetSystemParamDb));
        R_Exception loEx = new();
        CBB00200SystemParamDTO loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = $"EXEC RSP_CB_GET_SYSTEM_PARAM '{poParams.CCOMPANY_ID}', '{poParams.CLANGUAGE_ID}'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;
            
            _logger.LogDebug("{pcQuery}", lcQuery);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<CBB00200SystemParamDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }
    
    public CBB00200ClosePeriodResultDTO CBB00200ClosePeriodDb(CBB00200ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(CBB00200ClosePeriodDb));
        R_Exception loEx = new();
        CBB00200ClosePeriodResultDTO loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            
            lcQuery = "RSP_CB_CLOSE_PERIOD";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParams.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 6, poParams.CPERIOD);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CUSER_ID" or
                        "@CPERIOD"
                )
                .Select(x => x.Value);
            
            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
            
            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            
            // #region "Dummy Data"
            //
            // var loDataTable = new DataTable();
            // loDataTable.Columns.Add("IERROR_COUNT", typeof(int));
            //
            // loDataTable.Rows.Add(1);
            //
            // #endregion

            loReturn = R_Utility.R_ConvertTo<CBB00200ClosePeriodResultDTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public List<CBB00200ClosePeriodToDoListDTO> CBB00200GetClosePeriodToDoList(CBB00200ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(CBB00200GetClosePeriodToDoList));
        R_Exception loEx = new();
        List<CBB00200ClosePeriodToDoListDTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            
            lcQuery = "RSP_CB_GET_CLOSE_PERIOD_TODO_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 6, poParams.CPERIOD);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParams.CLANGUAGE_ID);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPERIOD" or
                        "@CLANGUAGE_ID"
                )
                .Select(x => x.Value);
            
            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
            
            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            // #region "Dummy Data"
            //
            // var loDataTable = new DataTable();
            // loDataTable.Columns.Add("INO", typeof(int));
            // loDataTable.Columns.Add("CDESCRIPTION", typeof(string));
            // loDataTable.Columns.Add("CSOLUTION", typeof(string));
            //
            // for (var i = 1; i <= 20; i++)
            // {
            //     loDataTable.Rows.Add(i, $"Close AP Period not yet processed {i}", $"Close AP Period {i}");
            // }
            // #endregion

            loReturn = R_Utility.R_ConvertTo<CBB00200ClosePeriodToDoListDTO>(loDataTable).ToList();
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