using System.Diagnostics;
using GLR00100Back;
using GLR00100Common;
using GLR00100Common.DTOs;
using GLR00100Common.Params;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace GLR00100Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class GLR00100Controller : IGLR00100
{
    private LoggerGLR00100 _logger;
    private readonly ActivitySource _activitySource;
    
    public GLR00100Controller(ILogger<GLR00100Controller> logger)
    {
        //Initial and Get Logger
        LoggerGLR00100.R_InitializeLogger(logger);
        _logger = LoggerGLR00100.R_GetInstanceLogger();
        _activitySource = GLR00100Activity.R_InitializeAndGetActivitySource(nameof(GLR00100Controller));
    }

    [HttpPost]
    public GLR00100SingleDTO<GLR00100SystemParamDTO> GLR00100GetSystemParam()
    {
        using var loActivity = _activitySource.StartActivity(nameof(GLR00100GetSystemParam));
        _logger.LogInfo("Start - Get GL System Param");
        var loEx = new R_Exception();
        var loCls = new GLR00100Cls();
        var loDbParams = new GLR00100ParameterDb();
        var loReturn = new GLR00100SingleDTO<GLR00100SystemParamDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            
            _logger.LogInfo("Get GL System Param");
            var loResult = loCls.GetSystemParamDb(loDbParams);
            loReturn.Data = loResult;

        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get GL System Param");
        return loReturn;
    }

    [HttpPost]
    public GLR00100SingleDTO<GLR00100PeriodDTO> GLR00100GetPeriod()
    {
        using var loActivity = _activitySource.StartActivity(nameof(GLR00100GetPeriod));
        _logger.LogInfo("Start - Get GSM Period");
        var loEx = new R_Exception();
        var loCls = new GLR00100Cls();
        var loDbParams = new GLR00100ParameterDb();
        var loReturn = new GLR00100SingleDTO<GLR00100PeriodDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            
            _logger.LogInfo("Get GSM Period");
            var loResult = loCls.GetPeriodDb(loDbParams);
            loReturn.Data = loResult;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get GSM Period");
        return loReturn;
    }
    
    [HttpPost]
    public GLR00100ListDTO<GLR00100PeriodDTDTO> GLR00100GetPeriodList(GLR00100PeriodDTParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GLR00100GetPeriodList));
        _logger.LogInfo("Start - Get Period DT List");
        var loEx = new R_Exception();
        var loCls = new GLR00100Cls();
        var loDbParams = new GLR00100ParameterDb();
        var loReturn = new GLR00100ListDTO<GLR00100PeriodDTDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CYEAR = poParam.CYEAR;
            
            _logger.LogInfo("Get Period DT List");
            var loResult = loCls.GetPeriodListDb(loDbParams);
            loReturn.Data = loResult;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Period DT List");
        return loReturn;
    }

    [HttpPost]
    public IAsyncEnumerable<GLR00100TransCodeDTO> GLR00100GetTransCodeListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(GLR00100GetTransCodeListStream));
        _logger.LogInfo("Start - Get TransCodeListStream");
        var loEx = new R_Exception();
        var loCls = new GLR00100Cls();
        var loDbParams = new GLR00100ParameterDb();
        List<GLR00100TransCodeDTO> loResult = null;
        IAsyncEnumerable<GLR00100TransCodeDTO> loReturn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Get TransCodeListStream");
            loResult = loCls.GetTransCodeListDb(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get TransCodeListStream");
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