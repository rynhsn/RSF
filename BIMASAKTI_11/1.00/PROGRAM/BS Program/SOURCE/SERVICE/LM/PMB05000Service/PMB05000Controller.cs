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
    public PMB05000SingleDTO<PMB05000SystemParamDTO> PMB05000GetSystemParam()
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

            _logger.LogInfo("Get System Param");
            loReturn.Data = loCls.GetSystemParam(loDbParams);
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
    public PMB05000SingleDTO<PMB05000PeriodYearRangeDTO> PMB05000GetPeriod()
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
            loReturn.Data = loCls.GetPeriod(loDbParams);
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
    public PMB05000SingleDTO<PMB05000PeriodParam> PMB05000UpdateSoftPeriod(PMB05000PeriodParam poParams)
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

            _logger.LogInfo("Update Soft Period");
            loCls.UpdateSoftClosePeriod(loDbParams);
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
    public IAsyncEnumerable<PMB05000ValidateSoftCloseDTO> PMB05000ValidateSoftPeriod()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMB05000ValidateSoftPeriod));
        _logger.LogInfo("Start - Validate Soft Close List Stream");
        var loEx = new R_Exception();
        var loCls = new PMB05000Cls();
        var loDbParams = new PMB05000ParameterDb();
        List<PMB05000ValidateSoftCloseDTO> loResult;
        IAsyncEnumerable<PMB05000ValidateSoftCloseDTO> loReturn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPERIOD = R_Utility.R_GetStreamingContext<string>(PMB05000ContextConstant.CPERIOD);

            _logger.LogInfo("Validate Soft Close List Stream");
            loResult = loCls.ValidateSoftClosePeriod(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Validate Soft Close List Stream");
        return loReturn;
    }
    
    [HttpPost]
    public PMB05000SingleDTO<PMB05000SoftClosePeriodDTO> PMB05000ProcessSoftPeriod(PMB05000PeriodParam poParams)
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

            _logger.LogInfo("Process Soft Period");
            loReturn.Data = loCls.ProcessSoftClosePeriod(loDbParams);
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