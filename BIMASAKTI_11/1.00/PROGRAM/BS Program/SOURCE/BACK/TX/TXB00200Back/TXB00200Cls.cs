using System.Data;
using System.Data.Common;
using System.Diagnostics;
using R_BackEnd;
using R_Common;
using RSP_TX_SOFT_CLOSE_TAXResources;
using TXB00200Common;
using TXB00200Common.DTOs;

namespace TXB00200Back;

public class TXB00200Cls
{
    Resources_Dummy_Class _resources = new();

    private LoggerTXB00200 _logger;
    private readonly ActivitySource _activitySource;

    public TXB00200Cls()
    {
        _logger = LoggerTXB00200.R_GetInstanceLogger();
        _activitySource = TXB00200Activity.R_GetInstanceActivitySource();
    }

    public TXB00200DTO GetSoftClosePeriod(TXB00200ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetSoftClosePeriod));
        R_Exception loEx = new();
        TXB00200DTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        var CLOSE_TYPE = "S";
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = $"EXEC RSP_TX_GET_SOFT_CLOSE_PERIOD '{poParam.CCOMPANY_ID}', '{poParam.CUSER_LOGIN_ID}'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<TXB00200DTO>(loDataTable).FirstOrDefault();
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

    public List<TXB00200PropertyDTO> GetPropertyList(TXB00200ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetPropertyList));
        R_Exception loEx = new();
        List<TXB00200PropertyDTO> loRtn = null;
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

            loRtn = R_Utility.R_ConvertTo<TXB00200PropertyDTO>(loDataTable).ToList();
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

    public TXB00200NextPeriodDTO GetNextPeriod(TXB00200ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetPeriodList));
        R_Exception loEx = new();
        TXB00200NextPeriodDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        var CLOSE_TYPE = "S";
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = $"EXEC RSP_GS_GET_TAX_NEXT_PERIOD '{poParam.CCOMPANY_ID}', '{CLOSE_TYPE}'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<TXB00200NextPeriodDTO>(loDataTable).FirstOrDefault();
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

    public List<TXB00200PeriodDTO> GetPeriodList(TXB00200ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetPeriodList));
        R_Exception loEx = new();
        List<TXB00200PeriodDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        var CLOSE_TYPE = "S";
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            // lcQuery = "RSP_GS_GET_PERIOD_DT_LIST";
            lcQuery = "RSP_TX_GET_PERIOD_DT_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 4, poParam.CYEAR);
            loDb.R_AddCommandParameter(loCmd, "@CLOSE_TYPE", DbType.String, 1, CLOSE_TYPE);


            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CYEAR" or
                        "@CLOSE_TYPE"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<TXB00200PeriodDTO>(loDataTable).ToList();
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

    // public void ProcessSoftClose(TXB00200ParameterDb poParam)
    // {
    //     using var loActivity = _activitySource.StartActivity(nameof(ProcessSoftClose));
    //     R_Exception loEx = new();
    //     R_Db loDb;
    //     DbConnection loConn = null;
    //     DbCommand loCmd;
    //     string lcQuery;
    //     try
    //     {
    //         loDb = new R_Db();
    //         loConn = loDb.GetConnection();
    //         loCmd = loDb.GetCommand();
    //         
    //         R_ExternalException.R_SP_Init_Exception(loConn);
    //
    //         lcQuery = "RSP_TX_SOFT_CLOSE_TAX";
    //         loCmd.CommandType = CommandType.StoredProcedure;
    //         loCmd.CommandText = lcQuery;
    //
    //         loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
    //         loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
    //         loDb.R_AddCommandParameter(loCmd, "@CTAX_TYPE", DbType.String, 20, poParam.CTAX_TYPE);
    //         loDb.R_AddCommandParameter(loCmd, "@CTAX_PERIOD_YEAR", DbType.String, 4, poParam.CPERIOD_YEAR);
    //         loDb.R_AddCommandParameter(loCmd, "@CTAX_PERIOD_MONTH", DbType.String, 2, poParam.CPERIOD_MONTH);
    //         loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 8, poParam.CUSER_LOGIN_ID);
    //
    //         var loDbParam = loCmd.Parameters.Cast<DbParameter>()
    //             .Where(x =>
    //                 x.ParameterName is
    //                     "@CCOMPANY_ID" or
    //                     "@CPROPERTY_ID" or
    //                     "@CTAX_TYPE" or
    //                     "@CTAX_PERIOD_YEAR" or
    //                     "@CTAX_PERIOD_MONTH" or
    //                     "@CUSER_LOGIN_ID"
    //             )
    //             .Select(x => x.Value);
    //
    //         _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
    //         try
    //         {
    //             loDb.SqlExecNonQuery(loConn, loCmd, false);
    //         }
    //         catch (Exception ex)
    //         {
    //             loEx.Add(ex);            
    //             _logger.LogError(loEx);
    //         }
    //
    //         loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
    //     }
    //     catch (Exception ex)
    //     {
    //         loEx.Add(ex);
    //         _logger.LogError(loEx);
    //     }
    //     finally
    //     {
    //         if (loConn != null)
    //         {
    //             if (loConn.State != ConnectionState.Closed)
    //             {
    //                 loConn.Close();
    //             }
    //
    //             loConn.Dispose();
    //         }
    //     }
    //
    //     EndBlock:
    //     loEx.ThrowExceptionIfErrors();
    // }

    public List<TXB00200SoftClosePeriodToDoListDTO> TXB00200ValidateSoftClosePeriod(TXB00200ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(TXB00200ValidateSoftClosePeriod));
        R_Exception loEx = new();
        List<TXB00200SoftClosePeriodToDoListDTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_TX_VALIDATE_SOFTCLOSE_PRD";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD_YEAR", DbType.String, 4, poParams.CPERIOD_YEAR);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD_MONTH", DbType.String, 2, poParams.CPERIOD_MONTH);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParams.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CYEAR" or
                        "@CPERIOD" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<TXB00200SoftClosePeriodToDoListDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public void TXB00200SoftClosePeriod(TXB00200ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(TXB00200SoftClosePeriod));
        R_Exception loEx = new();
        R_Db loDb;
        DbConnection loConn = null;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            
            R_ExternalException.R_SP_Init_Exception(loConn);

            lcQuery = "RSP_TX_SOFT_CLOSE_TAX";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 30, poParams.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_PERIOD_YEAR", DbType.String, 4, poParams.CPERIOD_YEAR);
            loDb.R_AddCommandParameter(loCmd, "@CTAX_PERIOD_MONTH", DbType.String, 2, poParams.CPERIOD_MONTH);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 20, poParams.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CTAX_PERIOD_YEAR" or
                        "@CTAX_PERIOD_MONTH" or
                        "@CUSER_LOGIN_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            try
            {
                loDb.SqlExecNonQuery(loConn, loCmd, false);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);            
                _logger.LogError(loEx);
            }
            
            loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        finally
        {
            if (loConn != null)
            {
                if (loConn.State != ConnectionState.Closed)
                {
                    loConn.Close();
                }
        
                loConn.Dispose();
            }
        }

        loEx.ThrowExceptionIfErrors();
    }


    public void UpdateSoftClosePeriod(TXB00200ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(UpdateSoftClosePeriod));
        R_Exception loEx = new();
        // TXB00200PeriodYearRangeDTO loReturn = new();
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = $"RSP_TX_MAINTAIN_SOFT_CLOSE";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD_YEAR", DbType.String, 4, poParams.CPERIOD_YEAR);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD_MONTH", DbType.String, 2, poParams.CPERIOD_MONTH);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 4, "EDIT");
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParams.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPERIOD_YEAR" or
                        "@CPERIOD_MONTH" or
                        "@CACTION" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);


            loDb.SqlExecNonQuery(loConn, loCmd, true);
            // var DataTable = loDb.SqlExecNonQuery(loConn, loCmd, true);

            // loReturn = R_Utility.R_ConvertTo<TXB00200PeriodYearRangeDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
    }
}