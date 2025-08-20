using System.Diagnostics;
using Lookup_ICBACK;
using Lookup_ICCOMMON;
using Lookup_ICCOMMON.DTOs;
using Lookup_ICCOMMON.DTOs.ICL00100;
using Lookup_ICCOMMON.DTOs.ICL00200;
using Lookup_ICCOMMON.DTOs.ICL00300;
using Lookup_ICCOMMON.Loggers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace Lookup_ICSERVICES;

[ApiController]
[Route("api/[controller]/[action]"), AllowAnonymous]
public class PublicLookupICController : ControllerBase, IPublicICLookup
{
    private LoggerICPublicLookup _loggerIC;
    public readonly ActivitySource _activitySource;

    public PublicLookupICController(ILogger<LoggerICPublicLookup> logger)
    {
        //Initial and Get Logger
        LoggerICPublicLookup.R_InitializeLogger(logger);
        _loggerIC = LoggerICPublicLookup.R_GetInstanceLogger();
        _activitySource = Lookup_ICBackActivity.R_InitializeAndGetActivitySource(nameof(PublicLookupICController));
    }

    [HttpPost]
    public IAsyncEnumerable<ICL00100DTO> ICL00100RequestLookup()
    {
        using Activity activity = _activitySource.StartActivity(nameof(ICL00100RequestLookup));
        _loggerIC.LogInfo("Start ICL00100RequesLookup");
        var loException = new R_Exception();
        IAsyncEnumerable<ICL00100DTO> loRtn = null;

        try
        {
            var loCls = new PublicICLookupCls();
            var poParam = new ICL00100ParameterDTO();

            _loggerIC.LogInfo("Set Param ICL00100RequesLookup");
            poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
            poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
            poParam.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CDEPT_CODE);

            _loggerIC.LogInfo("Call Back Method GetTaskLookup");
            var loResult = loCls.RequestLookup(poParam);

            _loggerIC.LogInfo("Call Stream Method Data ICL00100RequesLookup");
            loRtn = GetStream<ICL00100DTO>(loResult);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        _loggerIC.LogInfo("End ICL00100RequesLookup");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<ICL00200DTO> ICL00200RequestNoLookup()
    {
        using Activity activity = _activitySource.StartActivity(nameof(ICL00200RequestNoLookup));
        _loggerIC.LogInfo("Start ICL00200RequesNoLookup");
        var loException = new R_Exception();
        IAsyncEnumerable<ICL00200DTO> loRtn = null;

        try
        {
            var loCls = new PublicICLookupCls();
            var poParam = new ICL00200ParameterDTO();

            _loggerIC.LogInfo("Set Param ICL00200RequesNoLookup");
            poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
            poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
            poParam.CALLOC_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CALLOC_ID);
            poParam.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CDEPT_CODE);
            poParam.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTRANS_CODE);

            _loggerIC.LogInfo("Call Back Method GetTaskLookup");
            var loResult = loCls.RequestNoLookup(poParam);

            _loggerIC.LogInfo("Call Stream Method Data ICL00200RequesNoLookup");
            loRtn = GetStream<ICL00200DTO>(loResult);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        _loggerIC.LogInfo("End ICL00200RequesNoLookup");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<ICL00300DTO> ICL00300TransactionLookup()
    {
        using Activity activity = _activitySource.StartActivity(nameof(ICL00300TransactionLookup));
        _loggerIC.LogInfo("Start ICL00300TransactionLookup");
        var loException = new R_Exception();
        IAsyncEnumerable<ICL00300DTO> loRtn = null;

        try
        {
            var loCls = new PublicICLookupCls();
            var poParam = new ICL00300ParameterDTO();

            _loggerIC.LogInfo("Set Param ICL00300TransactionLookup");
            poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParam.CLANG_ID = R_BackGlobalVar.CULTURE;
            poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
            poParam.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTRANS_CODE);
            poParam.CPERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPERIOD);
            poParam.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CDEPT_CODE);
            poParam.CWAREHOUSE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CWAREHOUSE_ID);
            poParam.CALLOC_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CALLOC_ID);
            poParam.CTRANS_STATUS = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTRANS_STATUS);

            _loggerIC.LogInfo("Call Back Method GetTransaction");
            var loResult = loCls.TransactionLookup(poParam);

            _loggerIC.LogInfo("Call Stream Method Data ICL00300TransactionLookup");
            loRtn = GetStream<ICL00300DTO>(loResult);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        _loggerIC.LogInfo("End ICL00300TransactionLookup");
        return loRtn;
    }

    [HttpPost]
    public ICL00300PeriodDTO ICLInitiateTransactionLookup()
    {
        using Activity activity = _activitySource.StartActivity(nameof(ICLInitiateTransactionLookup));
        _loggerIC.LogInfo("Start ICL00100InitialProcess");
        var loException = new R_Exception();
        ICL00300PeriodDTO loRtn = null;
        try
        {
            var loCls = new PublicICLookupCls();
            var poParam = new ICL00300ParameterDTO();

            _loggerIC.LogInfo("Set Param ICL00100InitialProcess");
            poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;


            _loggerIC.LogInfo("Call Back Method InitialProcess");
            loRtn = loCls.InitialProcessICL00300(poParam);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        _loggerIC.LogInfo("End InitialProcess");
        return loRtn;
    }


    private async IAsyncEnumerable<T> GetStream<T>(List<T> poParam)
    {
        foreach (var item in poParam)
        {
            yield return item;
        }
    }
}