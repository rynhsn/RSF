using System.Reflection;
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
public class GLM00500HeaderController : ControllerBase, IGLM00500Header
{
    private LoggerGLM00500 _logger;

    public GLM00500HeaderController(ILogger<GLM00500HeaderController> logger)
    {
        //Initial and Get Logger
        LoggerGLM00500.R_InitializeLogger(logger);
        _logger = LoggerGLM00500.R_GetInstanceLogger();
    }

    [HttpPost]
    public R_ServiceGetRecordResultDTO<GLM00500BudgetHDDTO> R_ServiceGetRecord(
        R_ServiceGetRecordParameterDTO<GLM00500BudgetHDDTO> poParameter)
    {
        _logger.LogInfo("Start - Get Account Budget Record");
        var loEx = new R_Exception();
        var loRtn = new R_ServiceGetRecordResultDTO<GLM00500BudgetHDDTO>();

        try
        {
            var loCls = new GLM00500HeaderCls();
            
            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
            poParameter.Entity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            
            _logger.LogInfo("Get Account Budget Record");
            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Account Budget Record");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<GLM00500BudgetHDDTO> R_ServiceSave(
        R_ServiceSaveParameterDTO<GLM00500BudgetHDDTO> poParameter)
    {
        _logger.LogInfo("Start - Save Account Budget Entity");
        var loEx = new R_Exception();
        R_ServiceSaveResultDTO<GLM00500BudgetHDDTO> loRtn = null;
        GLM00500HeaderCls loCls;

        try
        {
            loCls = new GLM00500HeaderCls();
            loRtn = new R_ServiceSaveResultDTO<GLM00500BudgetHDDTO>();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Save Account Budget Entity");
            loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Save Account Budget Entity");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GLM00500BudgetHDDTO> poParameter)
    {
        _logger.LogInfo("Start - Delete Account Budget Entity");
        var loEx = new R_Exception();
        var loRtn = new R_ServiceDeleteResultDTO();
        GLM00500HeaderCls loCls;

        try
        {
            loCls = new GLM00500HeaderCls();
            
            _logger.LogInfo("Delete Account Budget Entity");
            loCls.R_Delete(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Delete Account Budget Entity");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<GLM00500BudgetHDDTO> GLM00500GetBudgetHDListStream()
    {
        _logger.LogInfo("Start - Get Account Budget List Stream");
        var loEx = new R_Exception();
        var loCls = new GLM00500HeaderCls();
        var loDbParams = new GLM00500ParameterDb();
        List<GLM00500BudgetHDDTO> loResult = null;
        IAsyncEnumerable<GLM00500BudgetHDDTO> loReturn = null;


        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            loDbParams.CYEAR = R_Utility.R_GetStreamingContext<string>(GLM00500ContextContant.CYEAR);
            
            _logger.LogInfo("Get Account Budget List Stream");
            loResult = loCls.GLM00500GetBudgetHDListDb(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Account Budget List Stream");
        return loReturn;
    }

    [HttpPost]
    public GLM00500GSMPeriodDTO GLM00500GetPeriods()
    {
        _logger.LogInfo("Start - Get Periods");
        var loEx = new R_Exception();
        var loCls = new GLM00500HeaderCls();
        var loDbParams = new GLM00500ParameterDb();
        var loReturn = new GLM00500GSMPeriodDTO();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            
            _logger.LogInfo("Get Periods");
            loReturn = loCls.GLM00500GetPeriodsDb(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Periods");
        return loReturn;
    }

    [HttpPost]
    public GLM00500GLSystemParamDTO GLM00500GetSystemParams()
    {
        _logger.LogInfo("Start - Get System Params");
        var loEx = new R_Exception();
        var loCls = new GLM00500HeaderCls();
        var loDbParams = new GLM00500ParameterDb();
        var loReturn = new GLM00500GLSystemParamDTO();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            
            _logger.LogInfo("Get System Params");
            loReturn = loCls.GLM00500GetSystemParamDb(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get System Params");
        return loReturn;
    }

    [HttpPost]
    public GLM00500ListDTO<GLM00500FunctionDTO> GLM00500GetCurrencyTypeList()
    {
        _logger.LogInfo("Start - Get Currency Type List");
        var loEx = new R_Exception();
        var loCls = new GLM00500HeaderCls();
        var loDbParams = new GLM00500ParameterDb();
        var loReturn = new GLM00500ListDTO<GLM00500FunctionDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            
            _logger.LogInfo("Get Currency Type List");
            loReturn.Data = loCls.GLM00500GetCurrencyTypeListDb(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Currency Type List");
        return loReturn;
    }

    [HttpPost]
    public void GLM00500FinalizeBudget(GLM00500CrecParamsDTO poParams)
    {
        _logger.LogInfo("Start - Finalize Budget");
        var loEx = new R_Exception();
        var loCls = new GLM00500HeaderCls();
        var loDbParams = new GLM00500ParameterDb();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CREC_ID = poParams.CREC_ID;
            
            _logger.LogInfo("Finalize Budget");
            loCls.GLM00500FinalizeBudgetDb(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Finalize Budget");
    }

    [HttpPost]
    public GLM00500AccountBudgetExcelDTO GLM00500DownloadTemplateFile()
    {
        _logger.LogInfo("Start - Download Template File");
        var loEx = new R_Exception();
        var loRtn = new GLM00500AccountBudgetExcelDTO();

        try
        {
            _logger.LogInfo("Get Template File");
            var loAsm = Assembly.Load("BIMASAKTI_GL_API");
            
            _logger.LogInfo("Set Resource File");
            var lcResourceFile = "BIMASAKTI_GL_API.Template.GL_ACCOUNT_BUDGET_UPLOAD.xlsx";

            _logger.LogInfo("Get Resource File");
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
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Download Template File");
        return loRtn;
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