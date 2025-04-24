using System.Data;
using System.Data.Common;
using System.Diagnostics;
using GLM00500Common;
using GLM00500Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GLM00500Back;

public class GLM00500DetailCls : R_BusinessObject<GLM00500BudgetDTDTO>
{
    RSP_GL_SAVE_BUDGET_DTResources.Resources_Dummy_Class _rscSave = new();
    RSP_GL_DELETE_BUDGET_DTResources.Resources_Dummy_Class _rscDelete = new();
    RSP_GL_GENERATE_ACCOUNT_BUDGETResources.Resources_Dummy_Class _rscGenerate = new();
    // RSP_GL_BUDGET_UPLOADResources.Resources_Dummy_Class _rsc = new();
    // RSP_GL_PROCESS_BUDGET_UPLOADResources.Resources_Dummy_Class _rscProcess = new();
    // RSP_GL_SAVE_BUDGET_UPLOADResources.Resources_Dummy_Class _rscSave = new();
    
    private LoggerGLM00500 _logger;
    private readonly ActivitySource _activitySource;

    public GLM00500DetailCls()
    {
        _logger = LoggerGLM00500.R_GetInstanceLogger();
        _activitySource =GLM00500Activity.R_GetInstanceActivitySource();
    }

    protected override GLM00500BudgetDTDTO R_Display(GLM00500BudgetDTDTO poEntity)
    {
        using var loScope = _activitySource.StartActivity(nameof(R_Display));
        var loEx = new R_Exception();
        GLM00500BudgetDTDTO loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_GET_BUDGET_DT";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 255, poEntity.CREC_ID);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CREC_ID" or
                        "@CLANGUAGE_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLM00500BudgetDTDTO>(DataTable).FirstOrDefault();
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

    protected override void R_Saving(GLM00500BudgetDTDTO poNewEntity, eCRUDMode poCRUDMode)
    {
        using var loScope = _activitySource.StartActivity(nameof(R_Saving));
        R_Exception loEx = new();
        string lcQuery;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn = null;
        string lcRecId = "";
        string lcAction = "";

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            R_ExternalException.R_SP_Init_Exception(loConn);

            switch (poCRUDMode)
            {
                case eCRUDMode.AddMode:
                    lcRecId = "";
                    lcAction = "NEW";
                    break;
                case eCRUDMode.EditMode:
                    lcRecId = poNewEntity.CREC_ID;
                    lcAction = "EDIT";
                    break;
            }

            lcQuery = "RSP_GL_SAVE_BUDGET_DT";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
            loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, lcRecId);
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUDGET_ID", DbType.String, 50, poNewEntity.CBUDGET_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUDGET_NO", DbType.String, 255, poNewEntity.CBUDGET_NO);
            loDb.R_AddCommandParameter(loCmd, "@CGLACCOUNT_TYPE", DbType.String, 50, poNewEntity.CGLACCOUNT_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CGLACCOUNT_NO", DbType.String, 50, poNewEntity.CGLACCOUNT_NO);
            loDb.R_AddCommandParameter(loCmd, "@CCENTER_CODE", DbType.String, 50, poNewEntity.CCENTER_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CINPUT_METHOD", DbType.String, 50, poNewEntity.CINPUT_METHOD);
            loDb.R_AddCommandParameter(loCmd, "@NBUDGET", DbType.Decimal, 22, poNewEntity.NBUDGET);
            loDb.R_AddCommandParameter(loCmd, "@CROUNDING_METHOD", DbType.String, 50, poNewEntity.CROUNDING_METHOD);
            loDb.R_AddCommandParameter(loCmd, "@CDIST_METHOD", DbType.String, 50, poNewEntity.CDIST_METHOD);
            loDb.R_AddCommandParameter(loCmd, "@CBW_CODE", DbType.String, 50, poNewEntity.CBW_CODE);
            loDb.R_AddCommandParameter(loCmd, "@NPERIOD1", DbType.Decimal, 22, poNewEntity.NPERIOD1);
            loDb.R_AddCommandParameter(loCmd, "@NPERIOD2", DbType.Decimal, 22, poNewEntity.NPERIOD2);
            loDb.R_AddCommandParameter(loCmd, "@NPERIOD3", DbType.Decimal, 22, poNewEntity.NPERIOD3);
            loDb.R_AddCommandParameter(loCmd, "@NPERIOD4", DbType.Decimal, 22, poNewEntity.NPERIOD4);
            loDb.R_AddCommandParameter(loCmd, "@NPERIOD5", DbType.Decimal, 22, poNewEntity.NPERIOD5);
            loDb.R_AddCommandParameter(loCmd, "@NPERIOD6", DbType.Decimal, 22, poNewEntity.NPERIOD6);
            loDb.R_AddCommandParameter(loCmd, "@NPERIOD7", DbType.Decimal, 22, poNewEntity.NPERIOD7);
            loDb.R_AddCommandParameter(loCmd, "@NPERIOD8", DbType.Decimal, 22, poNewEntity.NPERIOD8);
            loDb.R_AddCommandParameter(loCmd, "@NPERIOD9", DbType.Decimal, 22, poNewEntity.NPERIOD9);
            loDb.R_AddCommandParameter(loCmd, "@NPERIOD10", DbType.Decimal, 22, poNewEntity.NPERIOD10);
            loDb.R_AddCommandParameter(loCmd, "@NPERIOD11", DbType.Decimal, 22, poNewEntity.NPERIOD11);
            loDb.R_AddCommandParameter(loCmd, "@NPERIOD12", DbType.Decimal, 22, poNewEntity.NPERIOD12);
            loDb.R_AddCommandParameter(loCmd, "@NPERIOD13", DbType.Decimal, 22, poNewEntity.NPERIOD13);
            loDb.R_AddCommandParameter(loCmd, "@NPERIOD14", DbType.Decimal, 22, poNewEntity.NPERIOD14);
            loDb.R_AddCommandParameter(loCmd, "@NPERIOD15", DbType.Decimal, 22, poNewEntity.NPERIOD15);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CUSER_ID" or
                        "@CACTION" or
                        "@CREC_ID" or
                        "@CCOMPANY_ID" or
                        "@CBUDGET_ID" or
                        "@CBUDGET_NO" or
                        "@CGLACCOUNT_TYPE" or
                        "@CGLACCOUNT_NO" or
                        "@CCENTER_CODE" or
                        "@CINPUT_METHOD" or
                        "@NBUDGET" or
                        "@CROUNDING_METHOD" or
                        "@CDIST_METHOD" or
                        "@CBW_CODE" or
                        "@NPERIOD1" or
                        "@NPERIOD2" or
                        "@NPERIOD3" or
                        "@NPERIOD4" or
                        "@NPERIOD5" or
                        "@NPERIOD6" or
                        "@NPERIOD7" or
                        "@NPERIOD8" or
                        "@NPERIOD9" or
                        "@NPERIOD10" or
                        "@NPERIOD11" or
                        "@NPERIOD12" or
                        "@NPERIOD13" or
                        "@NPERIOD14" or
                        "@NPERIOD15"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            try
            {
                // loDb.SqlExecNonQuery(loConn, loCmd, false);
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                var loResult = R_Utility.R_ConvertTo<GLM00500BudgetDTDTO>(loDataTable).FirstOrDefault();
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

    protected override void R_Deleting(GLM00500BudgetDTDTO poEntity)
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

            lcQuery = "RSP_GL_DELETE_BUDGET_DT";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.CREC_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CREC_ID"
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

    public List<GLM00500BudgetDTGridDTO> GLM00500GetBudgetDTListDb(GLM00500ParameterDb poParams)
    {
        using var loScope = _activitySource.StartActivity(nameof(GLM00500GetBudgetDTListDb));
        R_Exception loEx = new();
        List<GLM00500BudgetDTGridDTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_GET_BUDGET_DT_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUDGET_ID", DbType.String, 50, poParams.CBUDGET_ID);
            loDb.R_AddCommandParameter(loCmd, "@CGLACCOUNT_TYPE", DbType.String, 50, poParams.CGLACCOUNT_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParams.CLANGUAGE_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CBUDGET_ID" or
                        "@CGLACCOUNT_TYPE" or
                        "@CLANGUAGE_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLM00500BudgetDTGridDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public GLM00500PeriodInfoDTO GLI0010GetPeriodInfoDb(GLM00500ParameterDb poParams)
    {
        using var loScope = _activitySource.StartActivity(nameof(GLI0010GetPeriodInfoDb));
        R_Exception loEx = new();
        GLM00500PeriodInfoDTO loReturn = new();
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = $"EXEC RSP_GS_GET_PERIOD_YEAR_INFO '{poParams.CCOMPANY_ID}', '{poParams.CYEAR}'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loReturn = R_Utility.R_ConvertTo<GLM00500PeriodInfoDTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public List<GLM00500FunctionDTO> GLM00500GetRoundingMethodListDb(GLM00500ParameterDb poParams)
    {
        using var loScope = _activitySource.StartActivity(nameof(GLM00500GetRoundingMethodListDb));
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

            lcQuery = "RSP_GS_GET_GSB_CODE_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CAPPLICATION", DbType.String, 20, "BIMASAKTI");
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CCLASS_ID", DbType.String, 40, "_GL_ROUNDING_METHOD");
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

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLM00500FunctionDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public List<GLM00500BudgetWeightingDTO> GLM00500GetBudgetWeightingListDb(GLM00500ParameterDb poParams)
    {
        using var loScope = _activitySource.StartActivity(nameof(GLM00500GetBudgetWeightingListDb));
        R_Exception loEx = new();
        List<GLM00500BudgetWeightingDTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_GET_BUDGET_WEIGHTING_LIST";
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

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLM00500BudgetWeightingDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public GLM00500GSMCompanyDTO GLM00500GetGSMCompanyDb(GLM00500ParameterDb poParams)
    {
        using var loScope = _activitySource.StartActivity(nameof(GLM00500GetGSMCompanyDb));
        R_Exception loEx = new();
        GLM00500GSMCompanyDTO lnReturn = new();
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = $"EXEC RSP_GS_GET_COMPANY_INFO '{poParams.CCOMPANY_ID}'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            lnReturn = R_Utility.R_ConvertTo<GLM00500GSMCompanyDTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return lnReturn;
    }

    public GLM00500BudgetCalculateDTO GLM00500BudgetCalculateDb(GLM00500ParameterDb poParams)
    {
        using var loScope = _activitySource.StartActivity(nameof(GLM00500BudgetCalculateDb));
        R_Exception loEx = new();
        GLM00500BudgetCalculateDTO lnReturn = new();
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

            lcQuery = @"RSP_GL_CALCULATE_BUDGET";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@IPERIOD_COUNT", DbType.String, 50, poParams.NPERIOD_COUNT);
            loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_TYPE", DbType.String, 50, poParams.CCURRENCY_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@NBUDGET", DbType.Decimal, 50, poParams.NBUDGET);
            loDb.R_AddCommandParameter(loCmd, "@CROUNDING_METHOD", DbType.String, 50, poParams.CROUNDING_METHOD);
            loDb.R_AddCommandParameter(loCmd, "@CDIST_METHOD", DbType.String, 50, poParams.CDIST_METHOD);
            loDb.R_AddCommandParameter(loCmd, "@CBW_CODE", DbType.String, 50, poParams.CBW_CODE);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@IPERIOD_COUNT" or
                        "@CCURRENCY_TYPE" or
                        "@NBUDGET" or
                        "@CROUNDING_METHOD" or
                        "@CDIST_METHOD" or
                        "@CBW_CODE"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            try
            {
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                lnReturn = R_Utility.R_ConvertTo<GLM00500BudgetCalculateDTO>(loDataTable).FirstOrDefault();
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
        return lnReturn;
    }

    public void GLM00500GenerateBudget(GLM00500ParameterGenerateDb poParams)
    {
        using var loScope = _activitySource.StartActivity(nameof(GLM00500GenerateBudget));
        R_Exception loEx = new R_Exception();
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_GENERATE_ACCOUNT_BUDGET";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParams.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUDGET_NO", DbType.String, 20, poParams.CBUDGET_NO);
            loDb.R_AddCommandParameter(loCmd, "@CBUDGET_ID", DbType.String, 50, poParams.CBUDGET_ID);
            loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_TYPE", DbType.String, 1, poParams.CCURRENCY_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CGLACCOUNT_TYPE", DbType.String, 1, poParams.CGLACCOUNT_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_GLACCOUNT_NO", DbType.String, 20, poParams.CFROM_GLACCOUNT_NO);
            loDb.R_AddCommandParameter(loCmd, "@CTO_GLACCOUNT_NO", DbType.String, 20, poParams.CTO_GLACCOUNT_NO);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_CENTER_CODE", DbType.String, 8, poParams.CFROM_CENTER_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CTO_CENTER_CODE", DbType.String, 8, poParams.CTO_CENTER_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CBASED_ON", DbType.String, 2, poParams.CBASED_ON);
            loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 4, poParams.CYEAR);
            loDb.R_AddCommandParameter(loCmd, "@CSOURCE_BUDGET_NO", DbType.String, 20, poParams.CSOURCE_BUDGET_NO);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_PERIOD_NO", DbType.String, 2, poParams.CFROM_PERIOD_NO);
            loDb.R_AddCommandParameter(loCmd, "@CTO_PERIOD_NO", DbType.String, 2, poParams.CTO_PERIOD_NO);
            loDb.R_AddCommandParameter(loCmd, "@CBY", DbType.String, 1, poParams.CBY);
            loDb.R_AddCommandParameter(loCmd, "@NBY_PCT", DbType.Decimal, 5, poParams.NBY_PCT);
            loDb.R_AddCommandParameter(loCmd, "@NBY_AMOUNT", DbType.Decimal, 19, poParams.NBY_AMOUNT);
            loDb.R_AddCommandParameter(loCmd, "@CUPDATE_METHOD", DbType.String, 1, poParams.CUPDATE_METHOD);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CUSER_ID" or
                        "@CCOMPANY_ID" or
                        "@CBUDGET_NO" or
                        "@CBUDGET_ID" or
                        "@CCURRENCY_TYPE" or
                        "@CGLACCOUNT_TYPE" or
                        "@CFROM_GLACCOUNT_NO" or
                        "@CTO_GLACCOUNT_NO" or
                        "@CFROM_CENTER_CODE" or
                        "@CTO_CENTER_CODE" or
                        "@CBASED_ON" or
                        "@CYEAR" or
                        "@CSOURCE_BUDGET_NO" or
                        "@CFROM_PERIOD_NO" or
                        "@CTO_PERIOD_NO" or
                        "@CBY" or
                        "@NBY_PCT" or
                        "@NBY_AMOUNT" or
                        "@CUPDATE_METHOD"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            loDb.SqlExecQuery(loConn, loCmd, true);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
    }
}