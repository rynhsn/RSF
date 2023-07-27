using System.Reflection;
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
public class GLM00500HeaderController : ControllerBase, IGLM00500Header
{
    [HttpPost]
    public R_ServiceGetRecordResultDTO<GLM00500BudgetHDDTO> R_ServiceGetRecord(
        R_ServiceGetRecordParameterDTO<GLM00500BudgetHDDTO> poParameter)
    {
        var loEx = new R_Exception();
        var loRtn = new R_ServiceGetRecordResultDTO<GLM00500BudgetHDDTO>();

        try
        {
            var loCls = new GLM00500HeaderCls();
            loRtn = new R_ServiceGetRecordResultDTO<GLM00500BudgetHDDTO>();
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
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
    public R_ServiceSaveResultDTO<GLM00500BudgetHDDTO> R_ServiceSave(
        R_ServiceSaveParameterDTO<GLM00500BudgetHDDTO> poParameter)
    {
        var loEx = new R_Exception();
        R_ServiceSaveResultDTO<GLM00500BudgetHDDTO> loRtn = null;
        GLM00500HeaderCls loCls;

        try
        {
            loCls = new GLM00500HeaderCls();
            loRtn = new R_ServiceSaveResultDTO<GLM00500BudgetHDDTO>();

            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

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
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GLM00500BudgetHDDTO> poParameter)
    {
        var loEx = new R_Exception();
        var loRtn = new R_ServiceDeleteResultDTO();
        GLM00500HeaderCls loCls;

        try
        {
            loCls = new GLM00500HeaderCls();
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
    public GLM00500ListDTO<GLM00500BudgetHDDTO> GLM00500GetBudgetHDList()
    {
        var loEx = new R_Exception();
        var loCls = new GLM00500HeaderCls();
        var loDbParams = new GLM00500ParameterDb();
        var loReturn = new GLM00500ListDTO<GLM00500BudgetHDDTO>();


        try
        {
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            loDbParams.CYEAR = R_Utility.R_GetStreamingContext<string>(GLM00500ContextContant.CYEAR);
            loReturn.Data = loCls.GLM00500GetBudgetHDListDb(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    [HttpPost]
    public GLM00500GSMPeriodDTO GLM00500GetPeriods()
    {
        var loEx = new R_Exception();
        var loCls = new GLM00500HeaderCls();
        var loDbParams = new GLM00500ParameterDb();
        var loReturn = new GLM00500GSMPeriodDTO();

        try
        {
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loReturn = loCls.GLM00500GetPeriodsDb(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    [HttpPost]
    public GLM00500GLSystemParamDTO GLM00500GetSystemParams()
    {
        var loEx = new R_Exception();
        var loCls = new GLM00500HeaderCls();
        var loDbParams = new GLM00500ParameterDb();
        var loReturn = new GLM00500GLSystemParamDTO();

        try
        {
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            loReturn = loCls.GLM00500GetSystemParamDb(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    [HttpPost]
    public GLM00500ListDTO<GLM00500FunctionDTO> GLM00500GetCurrencyTypeList()
    {
        var loEx = new R_Exception();
        var loCls = new GLM00500HeaderCls();
        var loDbParams = new GLM00500ParameterDb();
        var loReturn = new GLM00500ListDTO<GLM00500FunctionDTO>();

        try
        {
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            loReturn.Data = loCls.GLM00500GetCurrencyTypeListDb(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    [HttpPost]
    public void GLM00500FinalizeBudget()
    {
        var loEx = new R_Exception();
        var loCls = new GLM00500HeaderCls();
        var loDbParams = new GLM00500ParameterDb();

        try
        {
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CREC_ID = R_Utility.R_GetStreamingContext<string>(GLM00500ContextContant.CREC_ID);
            loCls.GLM00500FinalizeBudgetDb(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    [HttpPost]
    public GLM00500AccountBudgetExcelDTO GLM00500DownloadTemplateFile()
    {
        var loEx = new R_Exception();
        var loRtn = new GLM00500AccountBudgetExcelDTO();

        try
        {
            Assembly loAsm = Assembly.Load("BIMASAKTI_GL_API");
            var lcResourceFile = "BIMASAKTI_GL_API.Template.GL_ACCOUNT_BUDGET_UPLOAD.xlsx";

            using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
            {
                var ms = new MemoryStream();
                resFilestream.CopyTo(ms);
                var bytes = ms.ToArray();

                loRtn.FileBytes = bytes;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
}