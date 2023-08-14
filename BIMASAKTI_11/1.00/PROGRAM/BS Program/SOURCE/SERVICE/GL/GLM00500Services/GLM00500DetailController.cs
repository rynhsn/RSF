using GLM00500Back;
using GLM00500Common;
using GLM00500Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GLM00500Services;

[ApiController]
[Route("api/[controller]/[action]")]
public class GLM00500DetailController : ControllerBase, IGLM00500Detail
{
    [HttpPost]
    public R_ServiceGetRecordResultDTO<GLM00500BudgetDTDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GLM00500BudgetDTDTO> poParameter)
    {
        var loEx = new R_Exception();
        var loRtn = new R_ServiceGetRecordResultDTO<GLM00500BudgetDTDTO>();

        try
        {
            var loCls = new GLM00500DetailCls();
            loRtn = new R_ServiceGetRecordResultDTO<GLM00500BudgetDTDTO>();
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<GLM00500BudgetDTDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GLM00500BudgetDTDTO> poParameter)
    {
        var loEx = new R_Exception();
        R_ServiceSaveResultDTO<GLM00500BudgetDTDTO> loRtn = null;
        
        try
        {
            var loCls = new GLM00500DetailCls();
            loRtn = new R_ServiceSaveResultDTO<GLM00500BudgetDTDTO>();

            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
            poParameter.Entity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

            loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GLM00500BudgetDTDTO> poParameter)
    {
        var loEx = new R_Exception();
        var loRtn = new R_ServiceDeleteResultDTO();

        try
        {
            var loCls = new GLM00500DetailCls();
            loCls.R_Delete(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    [HttpPost]
    public GLM00500ListDTO<GLM00500BudgetDTGridDTO> GLM00500GetBudgetDTList()
    {
        var loEx = new R_Exception();
        var loCls = new GLM00500DetailCls();
        var loDbParams = new GLM00500ParameterDb();
        var loReturn = new GLM00500ListDTO<GLM00500BudgetDTGridDTO>();

        try
        {
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CBUDGET_ID = R_Utility.R_GetStreamingContext<string>(GLM00500ContextContant.CBUDGET_ID);
            loDbParams.CGLACCOUNT_TYPE = R_Utility.R_GetStreamingContext<string>(GLM00500ContextContant.CGLACCOUNT_TYPE);
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            
            loReturn.Data = loCls.GLM00500GetBudgetDTListDb(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    [HttpPost]
    public GLM00500ListDTO<GLM00500FunctionDTO> GLM00500GetRoundingMethodList()
    {
        var loEx = new R_Exception();
        var loCls = new GLM00500DetailCls();
        var loDbParams = new GLM00500ParameterDb();
        var loReturn = new GLM00500ListDTO<GLM00500FunctionDTO>();
        
        try
        {
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            loReturn.Data = loCls.GLM00500GetRoundingMethodListDb(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }
    
    [HttpPost]
    public GLM00500ListDTO<GLM00500BudgetWeightingDTO> GLM00500GetBudgetWeightingList()
    {
        var loEx = new R_Exception();
        var loCls = new GLM00500DetailCls();
        var loDbParams = new GLM00500ParameterDb();
        var loReturn = new GLM00500ListDTO<GLM00500BudgetWeightingDTO>();
        
        try
        {
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            loReturn.Data = loCls.GLM00500GetBudgetWeightingListDb(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    [HttpPost]
    public GLM00500PeriodCount GLM00500GetPeriodCount()
    {
        var loEx = new R_Exception();
        var loCls = new GLM00500DetailCls();
        var loDbParams = new GLM00500ParameterDb();
        var loReturn = new GLM00500PeriodCount();
        
        try
        {
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CYEAR = R_Utility.R_GetStreamingContext<string>(GLM00500ContextContant.CYEAR);
            var lnResult = loCls.GLM00500GetPeriodCountDb(loDbParams);
            loReturn = lnResult;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    [HttpPost]
    public GLM00500GSMCompanyDTO GLM00500GetGSMCompany()
    {
        var loEx = new R_Exception();
        var loCls = new GLM00500DetailCls();
        var loDbParams = new GLM00500ParameterDb();
        var loReturn = new GLM00500GSMCompanyDTO();
        
        try
        {
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            var lnResult = loCls.GLM00500GetGSMCompanyDb(loDbParams);
            loReturn = lnResult;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    [HttpPost]
    public GLM00500BudgetCalculateDTO GLM00500BudgetCalculate()
    {
        var loEx = new R_Exception();
        
        var loCls = new GLM00500DetailCls();
        var loDbParams = new GLM00500ParameterDb();
        var loReturn = new GLM00500BudgetCalculateDTO();
        
        try
        {
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.NPERIOD_COUNT = R_Utility.R_GetStreamingContext<int>(GLM00500ContextContant.NPERIOD_COUNT);
            loDbParams.CCURRENCY_TYPE = R_Utility.R_GetStreamingContext<string>(GLM00500ContextContant.CCURRENCY_TYPE);
            loDbParams.NBUDGET = R_Utility.R_GetStreamingContext<decimal>(GLM00500ContextContant.NBUDGET);
            loDbParams.CROUNDING_METHOD = R_Utility.R_GetStreamingContext<string>(GLM00500ContextContant.CROUNDING_METHOD);
            loDbParams.CDIST_METHOD = R_Utility.R_GetStreamingContext<string>(GLM00500ContextContant.CDIST_METHOD);
            loDbParams.CBW_CODE = R_Utility.R_GetStreamingContext<string>(GLM00500ContextContant.CBW_CODE);
            
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            loReturn = loCls.GLM00500BudgetCalculateDb(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    [HttpPost]
    public void GLM00500GenerateBudget(GLM00500GenerateAccountBudgetDTO poGenerateAccountBudgetDTO)
    {
        var loEx = new R_Exception();
        
        var loCls = new GLM00500DetailCls();
        var loDbParams = new GLM00500ParameterGenerateDb();
        
        try
        {
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
            
            loCls.GLM00500GenerateBudget(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }
}