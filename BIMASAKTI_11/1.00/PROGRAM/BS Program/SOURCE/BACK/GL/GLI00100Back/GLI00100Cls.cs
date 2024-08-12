using System.Data;
using System.Data.Common;
using System.Diagnostics;
using GLI00100Common;
using GLI00100Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GLI00100Back;

public class GLI00100Cls
{
    private LoggerGLI00100 _logger;
    private readonly ActivitySource _activitySource;

    public GLI00100Cls()
    {
        _logger = LoggerGLI00100.R_GetInstanceLogger();
        _activitySource = GLI00100Activity.R_GetInstanceActivitySource();
    }

    public GLI00100AccountDTO GLI00100GetDetailAccountDb(GLI00100ParameterDb poParam, GLI00100AccountParamDb poOptParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GLI00100GetDetailAccountDb));
        var loEx = new R_Exception();
        GLI00100AccountDTO loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_GET_ACCOUNT";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CGLACCOUNT_NO", DbType.String, 20, poOptParam.CGLACCOUNT_NO);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 2, poParam.CLANGUAGE_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CGLACCOUNT_NO" or
                        "@CLANGUAGE_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLI00100AccountDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public GLI00100AccountAnalysisDTO GLI00100GetDetailAccountAnalysisDb(GLI00100ParameterDb poParam,
        GLI00100AccountAnalysisParamDb poOptParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GLI00100GetDetailAccountAnalysisDb));
        var loEx = new R_Exception();
        GLI00100AccountAnalysisDTO loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_GET_ACCOUNT_ANALYSIS";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CGLACCOUNT_NO", DbType.String, 20, poOptParam.CGLACCOUNT_NO);
            loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 4, poOptParam.CYEAR);
            loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_TYPE", DbType.String, 1, poOptParam.CCURRENCY_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CCENTER_CODE", DbType.String, 10, poOptParam.CCENTER_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CBUDGET_NO", DbType.String, 20, poOptParam.CBUDGET_NO);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 2, poParam.CLANGUAGE_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CGLACCOUNT_NO" or
                        "@CYEAR" or
                        "@CCURRENCY_TYPE" or
                        "@CCENTER_CODE" or
                        "@CBUDGET_NO" or
                        "@CLANGUAGE_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLI00100AccountAnalysisDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public List<GLI00100BudgetDTO> GLI00100GetBudgetDb(GLI00100ParameterDb poParams, GLI00100BudgetParamDb poOptParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GLI00100GetBudgetDb));
        R_Exception loEx = new();
        List<GLI00100BudgetDTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_GET_BUDGET_FOR_TB_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 4, poOptParam.CYEAR);
            loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_TYPE", DbType.String, 1, poOptParam.CCURRENCY_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 2, poParams.CLANGUAGE_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CYEAR" or
                        "@CCURRENCY_TYPE" or
                        "@CLANGUAGE_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLI00100BudgetDTO>(DataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public GLI00100AccountAnalysisDTO GLI00100GetDetailAccountAnalysisReportDb(GLI00100ParameterDb poParam,
        GLI00100AccountAnalysisParamDb poOptParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GLI00100GetDetailAccountAnalysisReportDb));
        var loEx = new R_Exception();
        GLI00100AccountAnalysisDTO loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_GET_ACCOUNT_ANALYSIS";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CGLACCOUNT_NO", DbType.String, 20, poOptParam.CGLACCOUNT_NO);
            loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 4, poOptParam.CYEAR);
            loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_TYPE", DbType.String, 1, poOptParam.CCURRENCY_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CCENTER_CODE", DbType.String, 10, poOptParam.CCENTER_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CBUDGET_NO", DbType.String, 20, poOptParam.CBUDGET_NO);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 2, poParam.CLANGUAGE_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CGLACCOUNT_NO" or
                        "@CYEAR" or
                        "@CCURRENCY_TYPE" or
                        "@CCENTER_CODE" or
                        "@CBUDGET_NO" or
                        "@CLANGUAGE_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLI00100AccountAnalysisDTO>(DataTable).FirstOrDefault();
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