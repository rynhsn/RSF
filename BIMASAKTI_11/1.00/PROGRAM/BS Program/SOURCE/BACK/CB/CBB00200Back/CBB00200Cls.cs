using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;
using CBB00200Common;
using CBB00200Common.DTOs;
using R_BackEnd;
using R_Common;
using RSP_CB_CLOSE_PERIODResources;

namespace CBB00200Back;

public class CBB00200Cls
{
    RSP_CB_CLOSE_PERIODResources.Resources_Dummy_Class _resources = new();

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

            lcQuery = "RSP_CB_GET_SYSTEM_PARAM ";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParams.CLANGUAGE_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CLANGUAGE_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<CBB00200SystemParamDTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public List<CBB00200ClosePeriodToDoListDTO> CBB00200ValidateSoftClosePeriod(CBB00200ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(CBB00200ValidateSoftClosePeriod));
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

            lcQuery = "RSP_CB_VALIDATE_CLOSE_PERIOD ";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParams.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 6, poParams.CPERIOD);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParams.CLANGUAGE_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CUSER_ID" or
                        "@CPERIOD" or
                        "@CLANGUAGE_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

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

    //public CBB00200ClosePeriodResultDTO CBB00200SoftClosePeriod_old(CBB00200ParameterDb poParams)
    //{
    //    using var loActivity = _activitySource.StartActivity(nameof(CBB00200SoftClosePeriod));
    //    R_Exception loEx = new();
    //    R_Db loDb;
    //    DbConnection loConn;
    //    DbCommand loCmd;
    //    string lcQuery;
    //    CBB00200ClosePeriodResultDTO loReturn = null;

    //    try
    //    {
    //        loDb = new R_Db();
    //        loConn = loDb.GetConnection();
    //        loCmd = loDb.GetCommand();

    //        const bool llCancelSoftClose = false;
                
    //        lcQuery = "RSP_CB_CLOSE_PERIOD ";
    //        loCmd.CommandType = CommandType.StoredProcedure;
    //        loCmd.CommandText = lcQuery;

    //        loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
    //        loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParams.CUSER_ID);
    //        loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 6, poParams.CPERIOD);
    //        //loDb.R_AddCommandParameter(loCmd, "@LCANCEL_SOFT_CLOSE", DbType.Boolean, 1, llCancelSoftClose);

    //        var loDbParam = loCmd.Parameters.Cast<DbParameter>()
    //            .Where(x =>
    //                x.ParameterName is
    //                    "@CCOMPANY_ID" or
    //                    "@CUSER_ID" or
    //                    "@CPERIOD"
    //            )
    //            .Select(x => x.Value);

    //        _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
    //        R_ExternalException.R_SP_Init_Exception(loConn);
    //        try
    //        {
    //            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
    //            loReturn = R_Utility.R_ConvertTo<CBB00200ClosePeriodResultDTO>(loDataTable).FirstOrDefault();
    //        }
    //        catch (Exception ex)
    //        {
    //            loEx.Add(ex);
    //        }
    //        loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
    //    }
    //    catch (Exception ex)
    //    {
    //        loEx.Add(ex);
    //        _logger.LogError(loEx);
    //    }
    //    finally
    //    {
    //        if (loConn != null)
    //        {
    //            if (loConn.State != ConnectionState.Closed)
    //                loConn.Close();

    //            loConn.Dispose();
    //            loConn = null;
    //        }
    //        if (loCmd != null)
    //        {
    //            loCmd.Dispose();
    //            loCmd = null;
    //        }
    //    }

    //    loEx.ThrowExceptionIfErrors();
    //    return loReturn;
    //}


    public CBB00200ClosePeriodResultDTO CBB00200SoftClosePeriod(CBB00200ParameterDb poParam)
    {
        using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
        R_Exception loEx = new();
        R_Db loDb = new();
        DbConnection loConn = null;
        DbCommand loCmd = null;
        CBB00200ClosePeriodResultDTO loReturn = null;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            var lcQuery = "RSP_CB_CLOSE_PERIOD ";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParam.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 6, poParam.CPERIOD);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CUSER_ID" or
                        "@CPERIOD"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
            R_ExternalException.R_SP_Init_Exception(loConn);
            try
            {
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loReturn = R_Utility.R_ConvertTo<CBB00200ClosePeriodResultDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
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
        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

}