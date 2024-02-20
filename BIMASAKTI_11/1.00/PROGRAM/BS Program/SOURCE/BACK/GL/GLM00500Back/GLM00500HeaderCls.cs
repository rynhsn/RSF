using System.Data;
using System.Data.Common;
using System.Diagnostics;
using GLM00500Common;
using GLM00500Common.DTOs;
using R_OpenTelemetry;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GLM00500Back;

public class GLM00500HeaderCls : R_BusinessObject<GLM00500BudgetHDDTO>
{
    RSP_GL_SAVE_BUDGET_HDResources.Resources_Dummy_Class _rscSave = new();
    RSP_GL_DELETE_BUDGETResources.Resources_Dummy_Class _rscDelete = new();
    RSP_GL_FINALIZE_BUDGETResources.Resources_Dummy_Class _rscFinalize = new();
    
    private LoggerGLM00500 _logger;
    private readonly ActivitySource _activitySource;
    
    public GLM00500HeaderCls()
    {
        _logger = LoggerGLM00500.R_GetInstanceLogger();
        _activitySource = GLM00500Activity.R_GetInstanceActivitySource();
    }
    
    protected override GLM00500BudgetHDDTO R_Display(GLM00500BudgetHDDTO poEntity)
    {
        using var loScope = _activitySource.StartActivity(nameof(R_Display));
        var loEx = new R_Exception();
        GLM00500BudgetHDDTO loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_GET_BUDGET_HD";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;


            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUDGET_ID", DbType.String, 255, poEntity.CREC_ID);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CBUDGET_ID" or
                        "@CLANGUAGE_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
            
            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLM00500BudgetHDDTO>(DataTable).FirstOrDefault();
            loReturn.CREC_ID = poEntity.CREC_ID;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    protected override void R_Deleting(GLM00500BudgetHDDTO poEntity)
    {
        using var loScope = _activitySource.StartActivity(nameof(R_Deleting));
        R_Exception loEx = new();
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn = null;
        string lcQuery;
        
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            
            R_ExternalException.R_SP_Init_Exception(loConn);

            lcQuery = "RSP_GL_DELETE_BUDGET";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CBUDGET_ID", DbType.String, 255, poEntity.CREC_ID);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CBUDGET_ID"
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

    protected override void R_Saving(GLM00500BudgetHDDTO poNewEntity, eCRUDMode poCRUDMode)
    {
        using var loScope = _activitySource.StartActivity(nameof(R_Saving));
        R_Exception loEx = new();
        string lcQuery;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn = null;
        string lcRecId = "";
        string lcAction = "";
        bool llFinal = false; 
        
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            
            R_ExternalException.R_SP_Init_Exception(loConn);

            if (poCRUDMode == eCRUDMode.AddMode)
            {
                lcRecId = "";
                lcAction = "NEW";
            }
            else if (poCRUDMode == eCRUDMode.EditMode)
            {
                lcRecId = poNewEntity.CREC_ID;
                lcAction = "EDIT";
                llFinal = poNewEntity.LFINAL;
            }
            
            lcQuery = "RSP_GL_SAVE_BUDGET_HD";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
            loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 255, lcRecId);
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUDGET_NO", DbType.String, 255, poNewEntity.CBUDGET_NO);
            loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 255, poNewEntity.CYEAR);
            loDb.R_AddCommandParameter(loCmd, "@CBUDGET_NAME", DbType.String, 255, poNewEntity.CBUDGET_NAME);
            loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_TYPE", DbType.String, 255, poNewEntity.CCURRENCY_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@LFINAL", DbType.Boolean, 1, llFinal);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CUSER_ID" or 
                        "@CACTION" or 
                        "@CREC_ID" or 
                        "@CCOMPANY_ID" or 
                        "@CBUDGET_NO" or 
                        "@CYEAR" or 
                        "@CBUDGET_NAME" or 
                        "@CCURRENCY_TYPE" or 
                        "@LFINAL"
                )
                .Select(x => x.Value);
            
            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            try
            {
                // loDb.SqlExecNonQuery(loConn, loCmd, false);
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                var loResult = R_Utility.R_ConvertTo<GLM00500BudgetHDDTO>(loDataTable).FirstOrDefault();
                poNewEntity.CREC_ID = loResult.CREC_ID;
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
                {
                    loConn.Close();
                }

                loConn.Dispose();
            }
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }
    
    public void GLM00500FinalizeBudgetDb(GLM00500ParameterDb poParams)
    {
        using var loScope = _activitySource.StartActivity(nameof(GLM00500FinalizeBudgetDb));
        R_Exception loEx = new();
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn = null;
        string lcQuery;
        
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            
            R_ExternalException.R_SP_Init_Exception(loConn);

            lcQuery = "RSP_GL_FINALIZE_BUDGET";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 255, poParams.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 255, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUDGET_ID", DbType.String, 255, poParams.CREC_ID);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CUSER_ID" or 
                        "@CCOMPANY_ID" or 
                        "@CBUDGET_ID"
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

    public List<GLM00500BudgetHDDTO> GLM00500GetBudgetHDListDb(GLM00500ParameterDb poParams)
    {
        using var loScope = _activitySource.StartActivity(nameof(GLM00500GetBudgetHDListDb));
        R_Exception loEx = new();
        List<GLM00500BudgetHDDTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_GET_BUDGET_HD_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 50, poParams.CYEAR);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParams.CLANGUAGE_ID);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CYEAR" or 
                        "@CLANGUAGE_ID"
                )
                .Select(x => x.Value);
            
            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
            
            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLM00500BudgetHDDTO>(DataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public GLM00500GSMPeriodDTO GLM00500GetPeriodsDb(GLM00500ParameterDb poParams)
    {
        using var loScope = _activitySource.StartActivity(nameof(GLM00500GetPeriodsDb));
        R_Exception loEx = new();
        GLM00500GSMPeriodDTO loReturn = null;
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

            loReturn = R_Utility.R_ConvertTo<GLM00500GSMPeriodDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public GLM00500GLSystemParamDTO GLM00500GetSystemParamDb(GLM00500ParameterDb poParams)
    {
        using var loScope = _activitySource.StartActivity(nameof(GLM00500GetSystemParamDb));
        R_Exception loEx = new();
        GLM00500GLSystemParamDTO loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_GET_SYSTEM_PARAM";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;


            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParams.CLANGUAGE_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CLANGUAGE_ID"
                )
                .Select(x => x.Value);
            
            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
            
            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLM00500GLSystemParamDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public List<GLM00500FunctionDTO> GLM00500GetCurrencyTypeListDb(GLM00500ParameterDb poParams)
    {
        using var loScope = _activitySource.StartActivity(nameof(GLM00500GetCurrencyTypeListDb));
        R_Exception loEx = new();
        List<GLM00500FunctionDTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = $"RSP_GS_GET_GSB_CODE_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            
            loDb.R_AddCommandParameter(loCmd, "@CAPPLICATION", DbType.String, 20, "BIMASAKTI");
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CCLASS_ID", DbType.String, 40, "_CURRENCY_TYPE");
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParams.CLANGUAGE_ID);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CAPPLICATION" or 
                        "@CCOMPANY_ID" or 
                        "@CCLASS_ID" or 
                        "@CLANGUAGE_ID"
                )
                .Select(x => x.Value);
            
            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
            
            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLM00500FunctionDTO>(DataTable).ToList();
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