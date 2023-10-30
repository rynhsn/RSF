using GLI00100Back;
using GLI00100Common;
using GLI00100Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace GLI00100Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class GLI00100InitController : ControllerBase, IGLI00100Init
{
    private LoggerGLI00100 _logger;

    public GLI00100InitController(ILogger<GLI00100InitController> logger)
    {
        //Initial and Get Logger
        LoggerGLI00100.R_InitializeLogger(logger);
        _logger = LoggerGLI00100.R_GetInstanceLogger();
    }
    
    [HttpPost]
    public GLI00100GSMCompanyDTO GLI00100GetGSMCompany()
    {
        _logger.LogInfo("Start - Get GSM Company");
        var loEx = new R_Exception();
        var loCls = new GLI00100InitCls();
        var loDbParams = new GLI00100InitParameterDb();
        var loReturn = new GLI00100GSMCompanyDTO();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            
            _logger.LogInfo("Get GSM Company");
            loReturn = loCls.GLI00100GetCompanyDb(loDbParams);
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
    public GLI00100GLSystemParamDTO GLI00100GetGLSystemParam()
    {
        _logger.LogInfo("Start - Get GL System Param");
        var loEx = new R_Exception();
        var loCls = new GLI00100InitCls();
        var loDbParams = new GLI00100InitParameterDb();
        var loReturn = new GLI00100GLSystemParamDTO();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            
            _logger.LogInfo("Get GL System Param");
            loReturn = loCls.GLI00100GetSystemParamDb(loDbParams);
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
    public GLI00100GSMPeriodDTO GLI00100GetGSMPeriod()
    {
        _logger.LogInfo("Start - Get GSM Period");
        var loEx = new R_Exception();
        var loCls = new GLI00100InitCls();
        var loDbParams = new GLI00100InitParameterDb();
        var loReturn = new GLI00100GSMPeriodDTO();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            
            _logger.LogInfo("Get GSM Period");
            loReturn = loCls.GLI00100GetPeriodDb(loDbParams);
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
    public GLI00100PeriodCountDTO GLI00100GetPeriodCount(GLI00100YearParamsDTO poParams)
    {
        _logger.LogInfo("Start - Get Period Count");
        var loEx = new R_Exception();
        var loCls = new GLI00100InitCls();
        var loDbParams = new GLI00100InitParameterDb();
        var loReturn = new GLI00100PeriodCountDTO();
        
        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CYEAR = poParams.CYEAR;
            
            _logger.LogInfo("Get Period Count");
            var lnResult = loCls.GLI00100GetPeriodInfoDb(loDbParams);
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
    public IAsyncEnumerable<GLI00100AccountGridDTO> GLI00100GetGLAccountListStream()
    {
        _logger.LogInfo("Start - Get GL Account List");
        var loEx = new R_Exception();
        var loCls = new GLI00100InitCls();
        var loDbParams = new GLI00100InitParameterDb();
        List<GLI00100AccountGridDTO> loResult = null;
        IAsyncEnumerable<GLI00100AccountGridDTO> loReturn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            
            _logger.LogInfo("Get GL Account List");
            loResult = loCls.GLI00100GetAccountListDb(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get GL Account List");
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