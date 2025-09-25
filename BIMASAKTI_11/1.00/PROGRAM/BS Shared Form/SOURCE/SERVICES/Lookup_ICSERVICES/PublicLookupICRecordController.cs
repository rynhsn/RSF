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
public class PublicLookupICRecordController : ControllerBase, IPublicICLookupRecord
{
    private LoggerICPublicLookup _loggerIC;
    public readonly ActivitySource _activitySource;

    public PublicLookupICRecordController(ILogger<LoggerICPublicLookup> logger)
    {
        //Initial and Get Logger
        LoggerICPublicLookup.R_InitializeLogger(logger);
        _loggerIC = LoggerICPublicLookup.R_GetInstanceLogger();
        _activitySource = Lookup_ICBackActivity.R_InitializeAndGetActivitySource(nameof(PublicLookupICController));
    }

    [HttpPost]
    public ICLGenericRecord<ICL00100DTO> ICL00100GetRecord(ICL00100ParameterDTO poEntity)
    {
        using Activity activity = _activitySource.StartActivity("ICL00100GetRecord");
        var loEx = new R_Exception();
        ICLGenericRecord<ICL00100DTO> loRtn = new();
        _loggerIC.LogInfo("Start ICL00100GetRecord");

        try
        {
            var loCls = new PublicICLookupCls();
            _loggerIC.LogInfo("Set Param ICL00100GetRequestLookup");
            poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            var loResult = loCls.RequestLookup(poEntity);

            _loggerIC.LogInfo("Filter Search by text ICL00100GetRecord");
            loRtn.Data = loResult.Find(x =>
                x.CREQUEST_CODE.Equals(poEntity.CSEARCH_TEXT_ID.Trim(), StringComparison.OrdinalIgnoreCase));
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _loggerIC.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        _loggerIC.LogInfo("End ICL00100GetRecord");
        return loRtn;
    }
[HttpPost]
    public ICLGenericRecord<ICL00200DTO> ICL00200GetRecord(ICL00200ParameterDTO poEntity)
    {
        using Activity activity = _activitySource.StartActivity("ICL00200GetRecord");
        var loEx = new R_Exception();
        ICLGenericRecord<ICL00200DTO> loRtn = new();
        _loggerIC.LogInfo("Start ICL00200GetRecord");

        try
        {
            var loCls = new PublicICLookupCls();
            _loggerIC.LogInfo("Set Param ICL00200GetRequestNoLookup");
            poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            var loResult = loCls.RequestNoLookup(poEntity);

            _loggerIC.LogInfo("Filter Search by text ICL00200GetRecord");
            loRtn.Data = loResult.Find(x =>
                x.CREF_NO.Equals(poEntity.CSEARCH_TEXT_ID.Trim(), StringComparison.OrdinalIgnoreCase));
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _loggerIC.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        _loggerIC.LogInfo("End ICL00200GetRecord");
        return loRtn;
    }
[HttpPost]
    public ICLGenericRecord<ICL00300DTO> ICL00300GetRecord(ICL00300ParameterDTO poEntity)
    {
        throw new NotImplementedException();
    }
}