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
}