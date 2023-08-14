using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;
using GLM00500Common.DTOs;
using R_BackEnd;
using R_Common;

namespace GLM00500Back;

public class GLM00500UploadCls
{
    public GLM00500UploadCheckErrorDTO Validate(GLM00500ParameterUploadDb poParam, List<GLM00500UploadToSystemDTO> poData)
    {
        var loEx = new R_Exception();
        var loRtn = new GLM00500UploadCheckErrorDTO();
        var loDb = new R_Db();
        var loConn = loDb.GetConnection();
        var loCmd = loDb.GetCommand();
        var lcQuery = "";

        try
        {
            
            loDb = new R_Db();

            using (var TransScope = new TransactionScope(TransactionScopeOption.Required))
            {
                lcQuery = @"CREATE TABLE #GLM00500_BUDGET_UPLOAD(
                            BUDGET_YEAR VARCHAR(10) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_BUDGET_YEAR DEFAULT (''),
                            BUDGET_NO VARCHAR(20) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_BUDGET_NO DEFAULT (''),
                            BUDGET_NAME NVARCHAR(100) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_BUDGET_NAME DEFAULT (''),
                            CURRENCY_TYPE VARCHAR(10) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_CURRENCY_TYPE DEFAULT (''),
                            ACCOUNT_TYPE VARCHAR(10) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_GLACCOUNT_TYPE DEFAULT (''),
                            ACCOUNT_NO VARCHAR(20) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_GLACCOUNT_NO DEFAULT (''),
                            CENTER VARCHAR(10) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_CENTER_CODE DEFAULT (''),
                            PERIOD_1 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD1 DEFAULT (0),
                            PERIOD_2 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD2 DEFAULT (0),
                            PERIOD_3 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD3 DEFAULT (0),
                            PERIOD_4 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD4 DEFAULT (0),
                            PERIOD_5 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD5 DEFAULT (0),
                            PERIOD_6 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD6 DEFAULT (0),
                            PERIOD_7 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD7 DEFAULT (0),
                            PERIOD_8 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD8 DEFAULT (0),
                            PERIOD_9 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD9 DEFAULT (0),
                            PERIOD_10 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD10 DEFAULT (0),
                            PERIOD_11 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD11 DEFAULT (0),
                            PERIOD_12 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD12 DEFAULT (0),
                            PERIOD_13 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD13 DEFAULT (0),
                            PERIOD_14 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD14 DEFAULT (0),
                            PERIOD_15 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD15 DEFAULT (0),
                        )";
                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert((SqlConnection)loConn, "#GLM00500_BUDGET_UPLOAD", poData);

                lcQuery = "RSP_GL_PROCESS_BUDGET_UPLOAD";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParam.CUSER_ID);

                // var sdlk = loDb.SqlExecObjectQuery<GLM00500UploadCheckErrorDTO>(lcQuery, loConn, true);
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loRtn = R_Utility.R_ConvertTo<GLM00500UploadCheckErrorDTO>(loDataTable).FirstOrDefault();
                
                TransScope.Complete();
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
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
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }
    
    public void Upload(GLM00500ParameterUploadDb poParam, List<GLM00500UploadToSystemDTO> poData)
    {
        var loEx = new R_Exception();
        R_Db loDb = new();
        var loConn = loDb.GetConnection();
        var loCmd = loDb.GetCommand();
        var lcQuery = "";

        try
        {
            loDb = new R_Db();

            using (var TransScope = new TransactionScope(TransactionScopeOption.Required))
            {
                lcQuery = @"CREATE TABLE #GLM00500_BUDGET_UPLOAD(
                            BUDGET_YEAR VARCHAR(10) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_BUDGET_YEAR DEFAULT (''),
                            BUDGET_NO VARCHAR(20) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_BUDGET_NO DEFAULT (''),
                            BUDGET_NAME NVARCHAR(100) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_BUDGET_NAME DEFAULT (''),
                            CURRENCY_TYPE VARCHAR(10) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_CURRENCY_TYPE DEFAULT (''),
                            ACCOUNT_TYPE VARCHAR(10) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_GLACCOUNT_TYPE DEFAULT (''),
                            ACCOUNT_NO VARCHAR(20) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_GLACCOUNT_NO DEFAULT (''),
                            CENTER VARCHAR(10) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_CENTER_CODE DEFAULT (''),
                            PERIOD_1 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD1 DEFAULT (0),
                            PERIOD_2 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD2 DEFAULT (0),
                            PERIOD_3 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD3 DEFAULT (0),
                            PERIOD_4 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD4 DEFAULT (0),
                            PERIOD_5 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD5 DEFAULT (0),
                            PERIOD_6 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD6 DEFAULT (0),
                            PERIOD_7 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD7 DEFAULT (0),
                            PERIOD_8 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD8 DEFAULT (0),
                            PERIOD_9 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD9 DEFAULT (0),
                            PERIOD_10 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD10 DEFAULT (0),
                            PERIOD_11 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD11 DEFAULT (0),
                            PERIOD_12 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD12 DEFAULT (0),
                            PERIOD_13 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD13 DEFAULT (0),
                            PERIOD_14 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD14 DEFAULT (0),
                            PERIOD_15 NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLM_BUDGET_UPLOAD_PERIOD15 DEFAULT (0),
                        )";
                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert((SqlConnection)loConn, "#GLM00500_BUDGET_UPLOAD", poData);

                lcQuery = "RSP_GL_SAVE_BUDGET_UPLOAD ";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParam.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROCESS_ID", DbType.String, 50, poParam.CPROCESS_ID);

                loDb.SqlExecNonQuery(loConn, loCmd, false);
                
                TransScope.Complete();
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
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
        }

        loEx.ThrowExceptionIfErrors();
    }

    public List<GLM00500UploadFromSystemDTO> GetUploadList(GLM00500ParameterUploadDb poParam)
    {
        R_Exception loEx = new R_Exception();
        List<GLM00500UploadFromSystemDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_GET_BUDGET_UPLOAD_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CPROCESS_ID", DbType.String, 50, poParam.CPROCESS_ID);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GLM00500UploadFromSystemDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
    
    public GLM00500UploadErrorDTO GetErrorMsg(GLM00500ParameterUploadDb poParam)
    {
        R_Exception loEx = new R_Exception();
        GLM00500UploadErrorDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_GET_DATA_UPLOAD_VALIDATION_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParam.CREC_ID);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GLM00500UploadErrorDTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
}