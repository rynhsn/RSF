using System.Data;
using System.Data.Common;
using GLI00100Common;
using GLI00100Common.DTOs;
using R_BackEnd;
using R_Common;

namespace GLI00100Back;

public class GLI00100AccountJournalCls
{
    private LoggerGLI00100 _logger;

    public GLI00100AccountJournalCls()
    {
        _logger = LoggerGLI00100.R_GetInstanceLogger();
    }

    public GLI00100AccountAnalysisDetailDTO GetAccountAnalysisDetailDb(GLI00100ParameterDb poParam, GLI00100AccountAnalysisDetailTransactionParamDb poOptParam)
    {
        var loEx = new R_Exception();
        GLI00100AccountAnalysisDetailDTO loReturn = new();
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_GET_ACCOUNT_ANALYSIS_DETAIL";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CGLACCOUNT_NO", DbType.String, 20, poOptParam.CGLACCOUNT_NO);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 6, poOptParam.CPERIOD);
            loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_TYPE", DbType.String, 1, poOptParam.CCURRENCY_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CCENTER_CODE", DbType.String, 10, poOptParam.CCENTER_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 2, poParam.CLANGUAGE_ID);

            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CGLACCOUNT_NO" or
                        "@CPERIOD" or
                        "@CCURRENCY_TYPE" or
                        "@CCENTER_CODE" or
                        "@CLANGUAGE_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLI00100AccountAnalysisDetailDTO>(DataTable).FirstOrDefault() ?? new GLI00100AccountAnalysisDetailDTO();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public List<GLI00100TransactionGridDTO> GetTransactionListDb(GLI00100ParameterDb poParam,
        GLI00100AccountAnalysisDetailTransactionParamDb poOptParam)
    {
        R_Exception loEx = new();
        List<GLI00100TransactionGridDTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_GET_ACCOUNT_TRANSACTION_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CGLACCOUNT_NO", DbType.String, 20, poOptParam.CGLACCOUNT_NO);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 6, poOptParam.CPERIOD);
            loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_TYPE", DbType.String, 1, poOptParam.CCURRENCY_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CCENTER_CODE", DbType.String, 10, poOptParam.CCENTER_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 2, poParam.CLANGUAGE_ID);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CGLACCOUNT_NO" or
                        "@CPERIOD" or
                        "@CCURRENCY_TYPE" or
                        "@CCENTER_CODE" or
                        "@CLANGUAGE_ID"
                )
                .Select(x => x.Value);
            
            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLI00100TransactionGridDTO>(DataTable).ToList();
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