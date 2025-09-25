using System.Diagnostics;
using CBB00200Back;
using CBB00200Common;
using CBB00200Common.DTOs;
using CBB00200Common.Params;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace CBB00200Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class CBB00200Controller : ControllerBase, ICBB00200
{
    private LoggerCBB00200 _logger;
    private readonly ActivitySource _activitySource;


    public CBB00200Controller(ILogger<CBB00200Controller> logger)
    {
        //Initial and Get Logger
        LoggerCBB00200.R_InitializeLogger(logger);
        _logger = LoggerCBB00200.R_GetInstanceLogger();
        _activitySource = CBB00200Activity.R_InitializeAndGetActivitySource(nameof(CBB00200Controller));
    }

    [HttpPost]
    public CBB00200SingleDTO<CBB00200SystemParamDTO> CBB00200GetSystemParam()
    {
        using var loActivity = _activitySource.StartActivity(nameof(CBB00200GetSystemParam));
        _logger.LogInfo("Start - Get CB System Param");
        var loEx = new R_Exception();
        var loCls = new CBB00200Cls();
        var loDbParams = new CBB00200ParameterDb();
        var loReturn = new CBB00200SingleDTO<CBB00200SystemParamDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

            _logger.LogInfo("Get CB System Param");
            loReturn.Data = loCls.CBB00200GetSystemParamDb(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get CB System Param");
        return loReturn;
    }

    // [HttpPost]
    // public CBB00200SingleDTO<CBB00200ClosePeriodResultDTO> CBB00200ClosePeriod(CBB00200ClosePeriodParam poParams)
    // {
    //     using var loActivity = _activitySource.StartActivity(nameof(CBB00200ClosePeriod));
    //     _logger.LogInfo("Start - Get CB System Param");
    //     var loEx = new R_Exception();
    //     var loCls = new CBB00200Cls();
    //     var loDbParams = new CBB00200ParameterDb();
    //     var loReturn = new CBB00200SingleDTO<CBB00200ClosePeriodResultDTO>();
    //
    //     try
    //     {
    //         _logger.LogInfo("Set Parameter");
    //         loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
    //         loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
    //         loDbParams.CPERIOD = poParams.CPERIOD;
    //         loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
    //
    //         _logger.LogInfo("Get CB System Param");
    //         loReturn.Data = loCls.CBB00200ClosePeriodDb(loDbParams);
    //     }
    //     catch (Exception ex)
    //     {
    //         loEx.Add(ex);
    //         _logger.LogError(loEx);
    //     }
    //
    //     loEx.ThrowExceptionIfErrors();
    //     _logger.LogInfo("End - Get CB System Param");
    //     return loReturn;
    // }

    [HttpPost]
    public IAsyncEnumerable<CBB00200ClosePeriodToDoListDTO> CBB00200SoftClosePeriodStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(CBB00200SoftClosePeriodStream));
        _logger.LogInfo("Start - Get SoftClosePeriod List Stream");
        var loEx = new R_Exception();
        var loCls = new CBB00200Cls();
        var loDbParams = new CBB00200ParameterDb();
        List<CBB00200ClosePeriodToDoListDTO> loResult = null;
        IAsyncEnumerable<CBB00200ClosePeriodToDoListDTO> loReturn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            loDbParams.CPERIOD = R_Utility.R_GetStreamingContext<string>(CBB00200ContextConstant.CPERIOD);

            _logger.LogInfo("Get SoftClosePeriod List Stream");
            loResult = loCls.CBB00200ValidateSoftClosePeriod(loDbParams);
            loReturn = GetStream(loResult);

            if (loResult.Count == 0)
            {
                _logger.LogInfo("Soft Close Period");
                loCls.CBB00200SoftClosePeriod(loDbParams);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get SoftClosePeriod List Stream");
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