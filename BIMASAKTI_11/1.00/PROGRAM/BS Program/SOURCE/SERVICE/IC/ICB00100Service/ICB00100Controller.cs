using System.Diagnostics;
using ICB00100Back;
using ICB00100Common;
using ICB00100Common.DTOs;
using ICB00100Common.Params;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace ICB00100Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class ICB00100Controller : ControllerBase, IICB00100
{
    private LoggerICB00100 _logger;
    private readonly ActivitySource _activitySource;

    public ICB00100Controller(ILogger<ICB00100Controller> logger)
    {
        //Initial and Get Logger
        LoggerICB00100.R_InitializeLogger(logger);
        _logger = LoggerICB00100.R_GetInstanceLogger();
        _activitySource = ICB00100Activity.R_InitializeAndGetActivitySource(nameof(ICB00100Controller));
    }

    [HttpPost]
    public ICB00100ListDTO<ICB00100PropertyDTO> ICB00100GetPropertyList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(ICB00100GetPropertyList));

        _logger.LogInfo("Start - Get Property");
        var loEx = new R_Exception();
        var loCls = new ICB00100Cls();
        var loDbParams = new ICB00100ParameterDb();
        var loReturn = new ICB00100ListDTO<ICB00100PropertyDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Get Property");
            loReturn.Data = loCls.GetPropertyList(loDbParams);
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
    public ICB00100SingleDTO<ICB00100SystemParamDTO> ICB00100GetSystemParam(ICB00100SystemParamParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ICB00100GetSystemParam));
        _logger.LogInfo("Start - Get System Params");
        var loEx = new R_Exception();
        var loCls = new ICB00100Cls();
        var loDbParams = new ICB00100ParameterDb();
        var loReturn = new ICB00100SingleDTO<ICB00100SystemParamDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CPROPERTY_ID = poParam.CPROPERTY_ID;
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            
            _logger.LogInfo("Get System Params");
            loReturn.Data = loCls.GetSystemParam(loDbParams);
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
    public ICB00100SingleDTO<ICB00100PeriodYearRangeDTO> ICB00100GetPeriodYearRange()
    {
        using var loActivity = _activitySource.StartActivity(nameof(ICB00100GetPeriodYearRange));
        _logger.LogInfo("Start - Get Period Year Range");
        var loEx = new R_Exception();
        var loCls = new ICB00100Cls();
        var loDbParams = new ICB00100ParameterDb();
        var loReturn = new ICB00100SingleDTO<ICB00100PeriodYearRangeDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

            _logger.LogInfo("Get Period Year Range");
            loReturn.Data = loCls.GetPeriod(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Period Year Range");
        return loReturn;
    }

    [HttpPost]
    public ICB00100SingleDTO<ICB00100PeriodParam> ICB00100UpdateSoftPeriod(ICB00100PeriodParam poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ICB00100UpdateSoftPeriod));
        _logger.LogInfo("Start - Update Soft Period");
        var loEx = new R_Exception();
        var loCls = new ICB00100Cls();
        var loDbParams = new ICB00100ParameterDb();
        var loReturn = new ICB00100SingleDTO<ICB00100PeriodParam>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPERIOD_YEAR = poParams.CPERIOD_YEAR;
            loDbParams.CPERIOD_MONTH = poParams.CPERIOD_MONTH;

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
    public IAsyncEnumerable<ICB00100ValidateSoftCloseDTO> ICB00100ValidateSoftPeriod()
    {
        using var loActivity = _activitySource.StartActivity(nameof(ICB00100ValidateSoftPeriod));
        _logger.LogInfo("Start - Validate Soft Close List Stream");
        var loEx = new R_Exception();
        var loCls = new ICB00100Cls();
        var loDbParams = new ICB00100ParameterDb();
        List<ICB00100ValidateSoftCloseDTO> loResult;
        IAsyncEnumerable<ICB00100ValidateSoftCloseDTO> loReturn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ICB00100ContextConstant.CPROPERTY_ID);
            loDbParams.CPERIOD_YEAR = R_Utility.R_GetStreamingContext<string>(ICB00100ContextConstant.CPERIOD_YEAR);
            loDbParams.CPERIOD_MONTH = R_Utility.R_GetStreamingContext<string>(ICB00100ContextConstant.CPERIOD_MONTH);

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
    public ICB00100SingleDTO<ICB00100SoftClosePeriodDTO> ICB00100ProcessSoftPeriod(ICB00100PeriodParam poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ICB00100ProcessSoftPeriod));
        _logger.LogInfo("Start - Process Soft Period");
        var loEx = new R_Exception();
        var loCls = new ICB00100Cls();
        var loDbParams = new ICB00100ParameterDb();
        var loReturn = new ICB00100SingleDTO<ICB00100SoftClosePeriodDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPROPERTY_ID = poParams.CPROPERTY_ID;
            loDbParams.CPERIOD_YEAR = poParams.CPERIOD_YEAR;
            loDbParams.CPERIOD_MONTH = poParams.CPERIOD_MONTH;

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