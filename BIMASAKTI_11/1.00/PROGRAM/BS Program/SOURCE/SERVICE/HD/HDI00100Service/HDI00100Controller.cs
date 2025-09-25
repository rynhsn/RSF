using System.Diagnostics;
using HDI00100Back;
using HDI00100Common;
using HDI00100Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace HDI00100Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class HDI00100Controller : ControllerBase, IHDI00100
{
    private LoggerHDI00100 _logger;
    private readonly ActivitySource _activitySource;

    public HDI00100Controller(ILogger<HDI00100Controller> logger)
    {
        //Initial and Get Logger
        LoggerHDI00100.R_InitializeLogger(logger);
        _logger = LoggerHDI00100.R_GetInstanceLogger();
        _activitySource = HDI00100Activity.R_InitializeAndGetActivitySource(nameof(HDI00100Controller));
    }

    [HttpPost]
    public HDI00100ListDTO<HDI00100PropertyDTO> HDI00100GetPropertyList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(HDI00100GetPropertyList));
        _logger.LogInfo("Start - Get Property List");
        R_Exception loEx = new();
        HDI00100ListDTO<HDI00100PropertyDTO> loRtn = null;
        List<HDI00100PropertyDTO> loResult;
        HDI00100ParameterDb loDbPar;
        HDI00100Cls loCls;

        try
        {
            loDbPar = new HDI00100ParameterDb();

            _logger.LogInfo("Set Parameter");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

            loCls = new HDI00100Cls();

            _logger.LogInfo("Get Property List");
            loResult = loCls.GetPropertyList(loDbPar);
            loRtn = new HDI00100ListDTO<HDI00100PropertyDTO> { Data = loResult };
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Property List");
        return loRtn;
    }
    
    [HttpPost]
    public IAsyncEnumerable<HDI00100TaskSchedulerDTO> HDI00100GetTaskSchedulerListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(HDI00100GetTaskSchedulerListStream));
        _logger.LogInfo("Start - Get Task Scheduler List");
        R_Exception loEx = new();
        HDI00100ParameterDb loDbPar;
        List<HDI00100TaskSchedulerDTO> loRtnTmp;
        HDI00100Cls loCls;
        IAsyncEnumerable<HDI00100TaskSchedulerDTO> loRtn = null;

        try
        {
            loDbPar = new HDI00100ParameterDb();

            _logger.LogInfo("Set Parameter");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(HDI00100ContextConstant.CPROPERTY_ID);
            loDbPar.CYEAR = R_Utility.R_GetStreamingContext<string>(HDI00100ContextConstant.CYEAR);
            loDbPar.CFROM_DATE = R_Utility.R_GetStreamingContext<string>(HDI00100ContextConstant.CFROM_DATE);
            loDbPar.CTO_DATE = R_Utility.R_GetStreamingContext<string>(HDI00100ContextConstant.CTO_DATE);
            loDbPar.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(HDI00100ContextConstant.CBUILDING_ID);
            loDbPar.CASSET_CODE = R_Utility.R_GetStreamingContext<string>(HDI00100ContextConstant.CASSET_CODE);
            loDbPar.CSTATUS = R_Utility.R_GetStreamingContext<string>(HDI00100ContextConstant.CSTATUS);
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

            loCls = new HDI00100Cls();
            _logger.LogInfo("Get Task Scheduler List");
            loRtnTmp = loCls.GetPublicLocationList(loDbPar);

            loRtn = GetStream(loRtnTmp);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Task Scheduler List");
        return loRtn;
    }
    
    
    #region "Helper GetSalesTaxStream Functions"

    private async IAsyncEnumerable<T> GetStream<T>(List<T> poParameter)
    {
        foreach (var item in poParameter)
        {
            yield return item;
        }
    }

    #endregion
}