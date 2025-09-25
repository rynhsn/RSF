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
    public TXB00200SingleDTO<TXB00200DTO> TXB00200GetSoftClosePeriod()
    {
        using var loActivity = _activitySource.StartActivity(nameof(TXB00200GetNextPeriod));

        _logger.LogInfo("Start - Get Next period");
        var loEx = new R_Exception();
        var loCls = new TXB00200Cls();
        var loDbParams = new TXB00200ParameterDb();
        var loReturn = new TXB00200SingleDTO<TXB00200DTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Get Soft Close period");
            loReturn.Data = loCls.GetSoftClosePeriod(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Soft Close period");
        return loReturn;
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
    public TXB00200SingleDTO<TXB00200NextPeriodDTO> TXB00200GetNextPeriod()
    {
        using var loActivity = _activitySource.StartActivity(nameof(TXB00200GetNextPeriod));

        _logger.LogInfo("Start - Get Next period");
        var loEx = new R_Exception();
        var loCls = new TXB00200Cls();
        var loDbParams = new TXB00200ParameterDb();
        var loReturn = new TXB00200SingleDTO<TXB00200NextPeriodDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

            _logger.LogInfo("Get Next period");
            loReturn.Data = loCls.GetNextPeriod(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Next period");
        return loReturn;
    }

    [HttpPost]
    public TXB00200ListDTO<TXB00200PeriodDTO> TXB00200GetPeriodList(TXB00200PeriodParam poParam)
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
    public TXB00200ListDTO<TXB00200SoftClosePeriodToDoListDTO> TXB00200SoftClosePeriodStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(TXB00200SoftClosePeriodStream));
        _logger.LogInfo("Start - Get SoftClosePeriod List Stream");
        var loEx = new R_Exception();
        var loCls = new TXB00200Cls();
        var loDbParams = new TXB00200ParameterDb();
        List<TXB00200SoftClosePeriodToDoListDTO> loResult = null;
        var loReturn = new TXB00200ListDTO<TXB00200SoftClosePeriodToDoListDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(TXB00200ContextConstant.CPROPERTY_ID);
            loDbParams.CPERIOD_YEAR = R_Utility.R_GetStreamingContext<string>(TXB00200ContextConstant.CPERIOD_YEAR);
            loDbParams.CPERIOD_MONTH = R_Utility.R_GetStreamingContext<string>(TXB00200ContextConstant.CPERIOD_MONTH);

            _logger.LogInfo("Get SoftClosePeriod List Stream");
            
            loResult = loCls.TXB00200ValidateSoftClosePeriod(loDbParams);
            loReturn.Data = loResult;
            // loReturn = GetStream(loResult);

            if (loResult.Count == 0)
            {
                _logger.LogInfo("Soft Close Period");
                loCls.TXB00200SoftClosePeriod(loDbParams);
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
    
    [HttpPost]
    public TXB00200SingleDTO<TXB00200PeriodParam> TXB00200UpdateSoftPeriod(TXB00200PeriodParam poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(TXB00200UpdateSoftPeriod));
        _logger.LogInfo("Start - Update Soft Period");
        var loEx = new R_Exception();
        var loCls = new TXB00200Cls();
        var loDbParams = new TXB00200ParameterDb();
        var loReturn = new TXB00200SingleDTO<TXB00200PeriodParam>(); //dikirim kosong 

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CPERIOD_YEAR = poParams.CYEAR;
            loDbParams.CPERIOD_MONTH = poParams.CMONTH;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;

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
    // [HttpPost]
    // public TXB00200ListDTO<TXB00200SoftCloseParam> TXB00200ProcessSoftClose(TXB00200SoftCloseParam poParam)
    // {
    //     using var loActivity = _activitySource.StartActivity(nameof(TXB00200ProcessSoftClose));
    //     _logger.LogInfo("Start - Process Soft Close");
    //     var loEx = new R_Exception();
    //     var loCls = new TXB00200Cls();
    //     var loDbParams = new TXB00200ParameterDb();
    //     var loReturn = new TXB00200ListDTO<TXB00200SoftCloseParam>();
    //
    //     try
    //     {
    //         _logger.LogInfo("Set Parameter");
    //         loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
    //         loDbParams.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
    //         loDbParams.CPROPERTY_ID = poParam.CPROPERTY_ID;
    //         loDbParams.CTAX_TYPE = poParam.CTAX_TYPE;
    //         loDbParams.CPERIOD_YEAR = poParam.CPERIOD_YEAR;
    //         loDbParams.CPERIOD_MONTH = poParam.CPERIOD_MONTH;
    //
    //         _logger.LogInfo("Process Soft Close");
    //         loCls.ProcessSoftClose(loDbParams);
    //     }
    //     catch (Exception ex)
    //     {
    //         loEx.Add(ex);
    //         _logger.LogError(loEx);
    //     }
    //
    //     loEx.ThrowExceptionIfErrors();
    //     _logger.LogInfo("End - Process Soft Close");
    //     return loReturn;
    // }


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