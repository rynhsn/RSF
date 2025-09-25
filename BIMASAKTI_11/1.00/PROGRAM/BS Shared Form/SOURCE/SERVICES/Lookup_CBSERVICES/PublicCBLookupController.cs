using System.Diagnostics;
using Lookup_CBBACK;
using Lookup_CBCOMMON;
using Lookup_CBCOMMON.DTOs;
using Lookup_CBCOMMON.DTOs.CBL00100;
using Lookup_CBCOMMON.DTOs.CBL00200;
using Lookup_CBCOMMON.DTOs.Loggers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace Lookup_CBSERVICES;

[ApiController]
[Route("api/[controller]/[action]"), AllowAnonymous]
public class PublicCBLookupController : ControllerBase, IPublicCBLookup
{
    private LoggerCBPublicLookup _loggerAp;
    public readonly ActivitySource _activitySource;

    public PublicCBLookupController(ILogger<LoggerCBPublicLookup> logger)
    {
        //Initial and Get Logger
        LoggerCBPublicLookup.R_InitializeLogger(logger);
        _loggerAp = LoggerCBPublicLookup.R_GetInstanceLogger();
        _activitySource = Lookup_CBBACKActivity.R_InitializeAndGetActivitySource(nameof(PublicCBLookupController));
    }

    [HttpPost]
    public CBL00100DTO CBL00100InitialProcessLookup()
    {
        using Activity activity = _activitySource.StartActivity(nameof(CBL00100InitialProcessLookup));
        _loggerAp.LogInfo("Start CBL00100InitialProcessLookup");
        var loException = new R_Exception();
        CBL00100DTO loRtn = null;
        try
        {
            var loCls = new PublicCBLookupCls();
            var poParam = new CBL00100ParameterDTO();

            _loggerAp.LogInfo("Set Param CBL00100InitialProcessLookup");
            poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;


            _loggerAp.LogInfo("Call Back Method GetInitialPaymentToSupplierLookup");
            loRtn = loCls.InitialProcessCBL00100(poParam);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        _loggerAp.LogInfo("End GetInitialProcessCBL00100");
        return loRtn;
    }
    [HttpPost]
    public CBL00100DTO InitialProcessCBL00100Month()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public IAsyncEnumerable<CBL00100DTO> CBL00100ReceiptFromCustomerLookup()
    {
        using Activity activity = _activitySource.StartActivity(nameof(CBL00100ReceiptFromCustomerLookup));
        _loggerAp.LogInfo("Start CBL00100ReceiptFromCustomerLookup");
        var loException = new R_Exception();
        IAsyncEnumerable<CBL00100DTO> loRtn = null;
        try
        {
            var loCls = new PublicCBLookupCls();
            var poParam = new CBL00100ParameterDTO();

            _loggerAp.LogInfo("Set Param CBL00100ReceiptFromCustomerLookup");
            poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
            poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
            poParam.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CDEPT_CODE);
            poParam.CPERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPERIOD);
            _loggerAp.LogInfo("Call Back Method SchedulePaymentLookup");
            var loResult = loCls.ReceiptFromCustomerLookup(poParam);

            _loggerAp.LogInfo("Call Stream Method Data CBL00100ReceiptFromCustomerLookup");
            loRtn = GetStream<CBL00100DTO>(loResult);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        _loggerAp.LogInfo("End CBL00100ReceiptFromCustomerLookup");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<CBL00200DTO> CBL00200JournalLookup()
    {
        using Activity activity = _activitySource.StartActivity(nameof(CBL00200JournalLookup));
        _loggerAp.LogInfo("Start CBL00200JournalLookup");
        var loException = new R_Exception();
        IAsyncEnumerable<CBL00200DTO> loRtn = null;
        try
        {
            var loCls = new PublicCBLookupCls();
            var poParam = new CBL00200ParameterDTO();

            _loggerAp.LogInfo("Set Param CBL00200JournalLookup");
            poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
            poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            poParam.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CDEPT_CODE);
            poParam.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTRANS_CODE);
            poParam.CPERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPERIOD);
            _loggerAp.LogInfo("Call Back Method SchedulePaymentLookup");
            var loResult = loCls.CBJournalLookup(poParam);

            _loggerAp.LogInfo("Call Stream Method Data CBL00200JournalLookup");
            loRtn = GetStream<CBL00200DTO>(loResult);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        _loggerAp.LogInfo("End CBL00200JournalLookup");
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