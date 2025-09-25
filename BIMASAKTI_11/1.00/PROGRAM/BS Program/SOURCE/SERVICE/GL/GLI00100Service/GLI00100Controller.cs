using System.Diagnostics;
using GLI00100Back;
using GLI00100Common;
using GLI00100Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_OpenTelemetry;
using R_BackEnd;
using R_Common;

namespace GLI00100Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class GLI00100Controller : ControllerBase, IGLI00100
{
    private LoggerGLI00100 _logger;
    private readonly ActivitySource _activitySource;

    public GLI00100Controller(ILogger<GLI00100Controller> logger)
    {
        //Initial and Get Logger
        LoggerGLI00100.R_InitializeLogger(logger);
        _logger = LoggerGLI00100.R_GetInstanceLogger();
        _activitySource = GLI00100Activity.R_InitializeAndGetActivitySource(nameof(GLI00100Controller));
    }
    
    [HttpPost]
    public GLI00100AccountDTO GLI00100GetAccountDetail(GLI00100AccountParameterDTO poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GLI00100GetAccountDetail));
        _logger.LogInfo("Start - Get Account Detail");
        var loEx = new R_Exception();
        var loCls = new GLI00100Cls();
        var loDbParams = new GLI00100ParameterDb();
        var loDbOptParams = new GLI00100AccountParamDb();
        GLI00100AccountDTO loReturn = null;
        
        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            loDbOptParams.CGLACCOUNT_NO = poParams.CGLACCOUNT_NO;
            
            _logger.LogInfo("Get Account Detail");
            loReturn = loCls.GLI00100GetDetailAccountDb(loDbParams, loDbOptParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Account Detail");
        return loReturn;
    }

    [HttpPost]
    public GLI00100AccountAnalysisDTO GLI00100GetAccountAnalysisDetail(GLI00100AccountAnalysisParameterDTO poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GLI00100GetAccountAnalysisDetail));
        _logger.LogInfo("Start - Get Account Analysis Detail");
        var loEx = new R_Exception();
        var loCls = new GLI00100Cls();
        var loDbParams = new GLI00100ParameterDb();
        var loDbOptParams = new GLI00100AccountAnalysisParamDb();
        GLI00100AccountAnalysisDTO loReturn = null;
        
        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbOptParams.CGLACCOUNT_NO = poParams.CGLACCOUNT_NO;
            loDbOptParams.CYEAR = poParams.CYEAR;
            loDbOptParams.CCURRENCY_TYPE = poParams.CCURRENCY_TYPE;
            loDbOptParams.CCENTER_CODE = poParams.CCENTER_CODE;
            loDbOptParams.CBUDGET_NO = poParams.CBUDGET_NO;
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            
            _logger.LogInfo("Get Account Analysis Detail");
            loReturn = loCls.GLI00100GetDetailAccountAnalysisDb(loDbParams, loDbOptParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Account Analysis Detail");
        return loReturn;
    }

    [HttpPost]
    public IAsyncEnumerable<GLI00100BudgetDTO> GLI00100GetBudgetStream(GLI00100BudgetParamsDTO poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GLI00100GetBudgetStream));
        _logger.LogInfo("Start - Get Budget Stream");
        var loEx = new R_Exception();
        var loCls = new GLI00100Cls();
        var loDbParams = new GLI00100ParameterDb();
        var loDbOptParams = new GLI00100BudgetParamDb();
        List<GLI00100BudgetDTO> loResult;
        IAsyncEnumerable<GLI00100BudgetDTO> loReturn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            loDbOptParams.CYEAR = poParams.CYEAR;
            loDbOptParams.CCURRENCY_TYPE = poParams.CCURRENCY_TYPE;
            
            _logger.LogInfo("Get Budget Stream");
            loResult = loCls.GLI00100GetBudgetDb(loDbParams, loDbOptParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Budget Stream");
        return loReturn;
    }
    
    #region "Helper ListStream Functions"
    private async IAsyncEnumerable<T> GetStream<T>(List<T> poParameter)
    {
        foreach (var item in poParameter)
        {
            yield return item;
        }
    }
    #endregion
}