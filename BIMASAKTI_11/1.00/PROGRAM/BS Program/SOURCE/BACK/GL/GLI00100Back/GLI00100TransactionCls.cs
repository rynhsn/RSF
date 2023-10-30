using System.Data;
using System.Data.Common;
using GLI00100Common;
using GLI00100Common.DTOs;
using R_BackEnd;
using R_Common;

namespace GLI00100Back;

public class GLI00100TransactionCls
{
    private LoggerGLI00100 _logger;

    public GLI00100TransactionCls()
    {
        _logger = LoggerGLI00100.R_GetInstanceLogger();
    }

    public GLI00100JournalDTO GetJournalDetailDb(GLI00100ParameterDb poParam, GLI00100JournalParamDb poOptParam)
    {
        var loEx = new R_Exception();
        GLI00100JournalDTO loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_GET_JOURNAL";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poOptParam.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poOptParam.CREC_ID);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 2, poParam.CLANGUAGE_ID);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CUSER_ID" or
                        "@CREC_ID" or
                        "@CLANGUAGE_ID"
                )
                .Select(x => x.Value);
            
            _logger .LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
            
            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLI00100JournalDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }
    
    public List<GLI00100JournalGridDTO> GetJournalListDb(GLI00100ParameterDb poParam, GLI00100JournalParamDb poOptParam)
    {
        R_Exception loEx = new();
        List<GLI00100JournalGridDTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_GET_JOURNAL_DETAIL_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            
            loDb.R_AddCommandParameter(loCmd, "@CJRN_ID", DbType.String, 50, poOptParam.CREC_ID);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 2, poParam.CLANGUAGE_ID);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CJRN_ID" or 
                        "@CLANGUAGE_ID"
                )
                .Select(x => x.Value);
            
            _logger .LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLI00100JournalGridDTO>(DataTable).ToList();
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