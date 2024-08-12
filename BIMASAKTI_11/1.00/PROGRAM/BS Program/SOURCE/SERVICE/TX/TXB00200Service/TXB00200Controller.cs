using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using TXB00200Back;
using TXB00200Common;
using TXB00200Common.DTOs;
using TXB00200Common.Params;

namespace TXB00200Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class TXB00200Controller : ControllerBase, ITXB00200
{
    private LoggerTXB00200 _logger;
    private readonly ActivitySource _activitySource;

    public TXB00200Controller(ILogger<TXB00200Controller> logger)
    {
        //Initial and Get Logger
        LoggerTXB00200.R_InitializeLogger(logger);
        _logger = LoggerTXB00200.R_GetInstanceLogger();
        _activitySource = TXB00200Activity.R_InitializeAndGetActivitySource(nameof(TXB00200Controller));
    }

    [HttpPost]
    public TXB00200ListDTO<TXB00200PropertyDTO> TXB00200GetPropertyList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(TXB00200GetPropertyList));

        _logger.LogInfo("Start - Get Property");
        var loEx = new R_Exception();
        var loCls = new TXB00200Cls();
        var loDbParams = new TXB00200ParameterDb();
        var loReturn = new TXB00200ListDTO<TXB00200PropertyDTO>();

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
    public TXB00200ListDTO<TXB00200PeriodDTO> TXB00200GetPeriodList(TXB00200YearParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(TXB00200GetPeriodList));

        _logger.LogInfo("Start - Get Property");
        var loEx = new R_Exception();
        var loCls = new TXB00200Cls();
        var loDbParams = new TXB00200ParameterDb();
        var loReturn = new TXB00200ListDTO<TXB00200PeriodDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CYEAR = poParam.CYEAR;

            _logger.LogInfo("Get Property");
            loReturn.Data = loCls.GetPeriodList(loDbParams);
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
    public TXB00200ListDTO<TXB00200SoftCloseParam> TXB00200ProcessSoftClose(TXB00200SoftCloseParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(TXB00200ProcessSoftClose));
        _logger.LogInfo("Start - Process Soft Close");
        var loEx = new R_Exception();
        var loCls = new TXB00200Cls();
        var loDbParams = new TXB00200ParameterDb();
        var loReturn = new TXB00200ListDTO<TXB00200SoftCloseParam>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPROPERTY_ID = poParam.CPROPERTY_ID;
            loDbParams.CTAX_TYPE = poParam.CTAX_TYPE;
            loDbParams.CTAX_PERIOD_YEAR = poParam.CTAX_PERIOD_YEAR;
            loDbParams.CTAX_PERIOD_MONTH = poParam.CTAX_PERIOD_MONTH;

            _logger.LogInfo("Process Soft Close");
            loCls.ProcessSoftClose(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Process Soft Close");
        return loReturn;
    }
}