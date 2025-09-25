using GLM00500Back;
using GLM00500Common;
using GLM00500Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GLM00500Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class GLM00500DetailController : ControllerBase, IGLM00500Detail
{
    private LoggerGLM00500 _logger;

    public GLM00500DetailController(ILogger<GLM00500DetailController> logger)
    {
        //Initial and Get Logger
        LoggerGLM00500.R_InitializeLogger(logger);
        _logger = LoggerGLM00500.R_GetInstanceLogger();
    }

    [HttpPost]
    public R_ServiceGetRecordResultDTO<GLM00500BudgetDTDTO> R_ServiceGetRecord(
        R_ServiceGetRecordParameterDTO<GLM00500BudgetDTDTO> poParameter)
    {
        _logger.LogInfo("Start - Get Account Budget Detail Record");
        var loEx = new R_Exception();
        var loRtn = new R_ServiceGetRecordResultDTO<GLM00500BudgetDTDTO>();

        try
        {
            var loCls = new GLM00500DetailCls();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

            _logger.LogInfo("Get Account Budget Detail Record");
            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Account Budget Detail Record");

        return loRtn;
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<GLM00500BudgetDTDTO> R_ServiceSave(
        R_ServiceSaveParameterDTO<GLM00500BudgetDTDTO> poParameter)
    {
        _logger.LogInfo("Start - Save Account Budget Detail Entity");
        var loEx = new R_Exception();
        R_ServiceSaveResultDTO<GLM00500BudgetDTDTO> loRtn = null;

        try
        {
            var loCls = new GLM00500DetailCls();
            loRtn = new R_ServiceSaveResultDTO<GLM00500BudgetDTDTO>();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
            poParameter.Entity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

            _logger.LogInfo("Save Account Budget Detail Entity");
            loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Save Account Budget Detail Entity");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GLM00500BudgetDTDTO> poParameter)
    {
        _logger.LogInfo("Start - Delete Account Budget Detail Entity");
        var loEx = new R_Exception();
        var loRtn = new R_ServiceDeleteResultDTO();

        try
        {
            var loCls = new GLM00500DetailCls();
            
            _logger.LogInfo("Delete Account Budget Detail Entity");
            loCls.R_Delete(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Delete Account Budget Detail Entity");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<GLM00500BudgetDTGridDTO> GLM00500GetBudgetDTListStream()
    {
        _logger.LogInfo("Start - Get Account Budget Detail List Stream");
        var loEx = new R_Exception();
        var loCls = new GLM00500DetailCls();
        var loDbParams = new GLM00500ParameterDb();
        List<GLM00500BudgetDTGridDTO> loResult = null;
        IAsyncEnumerable<GLM00500BudgetDTGridDTO> loReturn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CBUDGET_ID = R_Utility.R_GetStreamingContext<string>(GLM00500ContextContant.CBUDGET_ID);
            loDbParams.CGLACCOUNT_TYPE =
                R_Utility.R_GetStreamingContext<string>(GLM00500ContextContant.CGLACCOUNT_TYPE);
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            
            _logger.LogInfo("Get Account Budget Detail List Stream");
            loResult = loCls.GLM00500GetBudgetDTListDb(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Account Budget Detail List Stream");
        return loReturn;
    }

    [HttpPost]
    public GLM00500ListDTO<GLM00500FunctionDTO> GLM00500GetRoundingMethodList()
    {
        _logger.LogInfo("Start - Get Rounding Method List");
        var loEx = new R_Exception();
        var loCls = new GLM00500DetailCls();
        var loDbParams = new GLM00500ParameterDb();
        var loReturn = new GLM00500ListDTO<GLM00500FunctionDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            
            _logger.LogInfo("Get Rounding Method List");
            loReturn.Data = loCls.GLM00500GetRoundingMethodListDb(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Rounding Method List");
        return loReturn;
    }

    [HttpPost]
    public IAsyncEnumerable<GLM00500BudgetWeightingDTO> GLM00500GetBudgetWeightingListStream()
    {
        _logger.LogInfo("Start - Get Budget Weighting List Stream");
        var loEx = new R_Exception();
        var loCls = new GLM00500DetailCls();
        var loDbParams = new GLM00500ParameterDb();
        List<GLM00500BudgetWeightingDTO> loResult = null;
        IAsyncEnumerable<GLM00500BudgetWeightingDTO> loReturn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            
            _logger.LogInfo("Get Budget Weighting List Stream");
            loResult = loCls.GLM00500GetBudgetWeightingListDb(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Budget Weighting List Stream");
        return loReturn;
    }

    [HttpPost]
    public GLM00500PeriodCountDTO GLM00500GetPeriodCount(GLM00500YearParamsDTO poParams)
    {
        _logger.LogInfo("Start - Get Period Count");
        var loEx = new R_Exception();
        var loCls = new GLM00500DetailCls();
        var loDbParams = new GLM00500ParameterDb();
        var loReturn = new GLM00500PeriodCountDTO();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CYEAR = poParams.CYEAR;
            
            _logger.LogInfo("Get Period Count");
            var lnResult = loCls.GLI0010GetPeriodInfoDb(loDbParams);
            loReturn.INO_PERIOD = lnResult.INO_PERIOD;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Period Count");
        return loReturn;
    }

    [HttpPost]
    public GLM00500GSMCompanyDTO GLM00500GetGSMCompany()
    {
        _logger.LogInfo("Start - Get GSM Company");
        var loEx = new R_Exception();
        var loCls = new GLM00500DetailCls();
        var loDbParams = new GLM00500ParameterDb();
        var loReturn = new GLM00500GSMCompanyDTO();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            
            _logger.LogInfo("Get GSM Company");
            var lnResult = loCls.GLM00500GetGSMCompanyDb(loDbParams);
            loReturn = lnResult;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get GSM Company");
        return loReturn;
    }

    [HttpPost]
    public GLM00500BudgetCalculateDTO GLM00500BudgetCalculate(GLM00500CalculateParamDTO poParams)
    {
        _logger.LogInfo("Start - Calculate Budget");
        var loEx = new R_Exception();
        var loCls = new GLM00500DetailCls();
        var loDbParams = new GLM00500ParameterDb();
        var loReturn = new GLM00500BudgetCalculateDTO();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.NPERIOD_COUNT = poParams.INO_PERIOD;
            loDbParams.CCURRENCY_TYPE = poParams.CCURRENCY_TYPE;
            loDbParams.NBUDGET = poParams.NBUDGET;
            loDbParams.CROUNDING_METHOD = poParams.CROUNDING_METHOD;
            loDbParams.CDIST_METHOD = poParams.CDIST_METHOD;
            loDbParams.CBW_CODE = poParams.CBW_CODE;
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            // loDbParams.NPERIOD_COUNT = R_Utility.R_GetStreamingContext<int>(GLM00500ContextContant.NPERIOD_COUNT);
            // loDbParams.CCURRENCY_TYPE = R_Utility.R_GetStreamingContext<string>(GLM00500ContextContant.CCURRENCY_TYPE);
            // loDbParams.NBUDGET = R_Utility.R_GetStreamingContext<decimal>(GLM00500ContextContant.NBUDGET);
            // loDbParams.CROUNDING_METHOD =
            //     R_Utility.R_GetStreamingContext<string>(GLM00500ContextContant.CROUNDING_METHOD);
            // loDbParams.CDIST_METHOD = R_Utility.R_GetStreamingContext<string>(GLM00500ContextContant.CDIST_METHOD);
            // loDbParams.CBW_CODE = R_Utility.R_GetStreamingContext<string>(GLM00500ContextContant.CBW_CODE);

            
            _logger.LogInfo("Calculate Budget");
            loReturn = loCls.GLM00500BudgetCalculateDb(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Calculate Budget");
        return loReturn;
    }

    [HttpPost]
    public void GLM00500GenerateBudget(GLM00500GenerateAccountBudgetDTO poGenerateAccountBudgetDTO)
    {
        _logger.LogInfo("Start - Generate Account Budget");
        var loEx = new R_Exception();
        var loCls = new GLM00500DetailCls();
        var loDbParams = new GLM00500ParameterGenerateDb();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CBUDGET_NO = poGenerateAccountBudgetDTO.CBUDGET_NO;
            loDbParams.CBUDGET_ID = poGenerateAccountBudgetDTO.CBUDGET_ID;
            loDbParams.CCURRENCY_TYPE = poGenerateAccountBudgetDTO.CCURRENCY_TYPE;
            loDbParams.CGLACCOUNT_TYPE = poGenerateAccountBudgetDTO.CGLACCOUNT_TYPE;
            loDbParams.CFROM_GLACCOUNT_NO = poGenerateAccountBudgetDTO.CFROM_GLACCOUNT_NO;
            loDbParams.CTO_GLACCOUNT_NO = poGenerateAccountBudgetDTO.CTO_GLACCOUNT_NO;
            loDbParams.CFROM_CENTER_CODE = poGenerateAccountBudgetDTO.CFROM_CENTER_CODE;
            loDbParams.CTO_CENTER_CODE = poGenerateAccountBudgetDTO.CTO_CENTER_CODE;
            loDbParams.CBASED_ON = poGenerateAccountBudgetDTO.CBASED_ON;
            loDbParams.CYEAR = poGenerateAccountBudgetDTO.CYEAR;
            loDbParams.CSOURCE_BUDGET_NO = poGenerateAccountBudgetDTO.CSOURCE_BUDGET_NO;
            loDbParams.CFROM_PERIOD_NO = poGenerateAccountBudgetDTO.CFROM_PERIOD_NO;
            loDbParams.CTO_PERIOD_NO = poGenerateAccountBudgetDTO.CTO_PERIOD_NO;
            loDbParams.CBY = poGenerateAccountBudgetDTO.CBY;
            loDbParams.NBY_PCT = poGenerateAccountBudgetDTO.NBY_PCT;
            loDbParams.NBY_AMOUNT = poGenerateAccountBudgetDTO.NBY_AMOUNT;
            loDbParams.CUPDATE_METHOD = poGenerateAccountBudgetDTO.CUPDATE_METHOD;

            _logger.LogInfo("Generate Account Budget");
            loCls.GLM00500GenerateBudget(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Generate Account Budget");   
    }

    #region "Helper ListStream Functions"

    private async IAsyncEnumerable<T> GetStream<T>(List<T> poParameter)
    {
        foreach (T item in poParameter)
        {
            yield return item;
        }
    }

    #endregion
}