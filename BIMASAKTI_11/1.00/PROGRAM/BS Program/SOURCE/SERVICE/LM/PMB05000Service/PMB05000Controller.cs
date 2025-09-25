using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMB05000Back;
using PMB05000Common;
using PMB05000Common.DTOs;
using PMB05000Common.Params;
using R_BackEnd;
using R_Common;

namespace PMB05000Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class PMB05000Controller : ControllerBase, IPMB05000
{
    private LoggerPMB05000 _logger;
    private readonly ActivitySource _activitySource;

    public PMB05000Controller(ILogger<PMB05000Controller> logger)
    {
        //Initial and Get Logger
        LoggerPMB05000.R_InitializeLogger(logger);
        _logger = LoggerPMB05000.R_GetInstanceLogger();
        _activitySource = PMB05000Activity.R_InitializeAndGetActivitySource(nameof(PMB05000Controller));
    }

    [HttpPost]
    public async Task<PMB05000SingleDTO<PMB05000SystemParamDTO>> PMB05000GetSystemParam(PMB05000SystemParamParam loParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMB05000GetSystemParam));
        _logger.LogInfo("Start - Get System Param");
        var loEx = new R_Exception();
        var loCls = new PMB05000Cls();
        var loDbParams = new PMB05000ParameterDb();
        var loReturn = new PMB05000SingleDTO<PMB05000SystemParamDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            loDbParams.CPROPERTY_ID = loParam.CPROPERTY_ID;

            _logger.LogInfo("Get System Param");
            loReturn.Data = await loCls.GetSystemParam(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get System Param");
        return loReturn;
    }

    [HttpPost]
    public async Task<PMB05000ListDTO<PMB05000PropertyDTO>> PMB05000GetProperties()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMB05000GetProperties));

        _logger.LogInfo("Start - Get Property");
        var loEx = new R_Exception();
        var loCls = new PMB05000Cls();
        var loDbParams = new PMB05000ParameterDb();
        var loReturn = new PMB05000ListDTO<PMB05000PropertyDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Get Property");
            loReturn.Data = await loCls.GetProperties(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Property");
        return loReturn;
    }
    
    [HttpPost]
    public async Task<PMB05000SingleDTO<PMB05000PeriodYearRangeDTO>> PMB05000GetPeriod()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMB05000GetPeriod));
        _logger.LogInfo("Start - Get Period");
        var loEx = new R_Exception();
        var loCls = new PMB05000Cls();
        var loDbParams = new PMB05000ParameterDb();
        var loReturn = new PMB05000SingleDTO<PMB05000PeriodYearRangeDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

            _logger.LogInfo("Get Period");
            loReturn.Data = await loCls.GetPeriod(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Period");
        return loReturn;
    }

    [HttpPost]
    public async Task<PMB05000SingleDTO<PMB05000PeriodParam>> PMB05000UpdateSoftPeriod(PMB05000PeriodParam poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMB05000GetPeriod));
        _logger.LogInfo("Start - Update Soft Period");
        var loEx = new R_Exception();
        var loCls = new PMB05000Cls();
        var loDbParams = new PMB05000ParameterDb();
        var loReturn = new PMB05000SingleDTO<PMB05000PeriodParam>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPERIOD = poParams.CCURRENT_SOFT_PERIOD;
            loDbParams.CPROPERTY_ID = poParams.CPROPERTY_ID;

            _logger.LogInfo("Update Soft Period");
            await loCls.UpdateSoftClosePeriod(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Update Soft Period");
        return loReturn;
    }

    [HttpPost]
    public async IAsyncEnumerable<PMB05000ValidateSoftCloseDTO> PMB05000ValidateSoftPeriod()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMB05000ValidateSoftPeriod));
        _logger.LogInfo("Start - Validate Soft Close List Stream");
        var loEx = new R_Exception();
        var loCls = new PMB05000Cls();
        var loDbParams = new PMB05000ParameterDb();
        List<PMB05000ValidateSoftCloseDTO> loReturnTmp = new();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPERIOD = R_Utility.R_GetStreamingContext<string>(PMB05000ContextConstant.CPERIOD);
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMB05000ContextConstant.CPROPERTY_ID);

            _logger.LogInfo("Validate Soft Close List Stream");
            loReturnTmp = await loCls.ValidateSoftClosePeriod(loDbParams);
            // loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Validate Soft Close List Stream");
        
        foreach (var loItem in loReturnTmp)
        {
            yield return loItem;
        }
    }
    
    [HttpPost]
    public async Task<PMB05000SingleDTO<PMB05000SoftClosePeriodDTO>> PMB05000ProcessSoftPeriod(PMB05000PeriodParam poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMB05000ProcessSoftPeriod));
        _logger.LogInfo("Start - Process Soft Period");
        var loEx = new R_Exception();
        var loCls = new PMB05000Cls();
        var loDbParams = new PMB05000ParameterDb();
        var loReturn = new PMB05000SingleDTO<PMB05000SoftClosePeriodDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPERIOD = poParams.CCURRENT_SOFT_PERIOD;
            loDbParams.CPROPERTY_ID = poParams.CPROPERTY_ID;

            _logger.LogInfo("Process Soft Period");
            loReturn.Data = await loCls.ProcessSoftClosePeriod(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Process Soft Period");
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