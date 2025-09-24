using System.Diagnostics;
using Lookup_CBBACK;
using Lookup_CBCOMMON;
using Lookup_CBCOMMON.DTOs;
using Lookup_CBCOMMON.DTOs.CBL00100;
using Lookup_CBCOMMON.DTOs.CBL00200;
using Lookup_CBCOMMON.DTOs.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace Lookup_CBSERVICES;

public class PublicCBLookupRecordController : ControllerBase, IPublicCBLookupRecord
{
    private LoggerCBPublicLookup _loggerAp;
    public readonly ActivitySource _activitySource;

    public PublicCBLookupRecordController(ILogger<LoggerCBPublicLookup> logger)
    {
        //Initial and Get Logger
        LoggerCBPublicLookup.R_InitializeLogger(logger);
        _loggerAp = LoggerCBPublicLookup.R_GetInstanceLogger();
        _activitySource = Lookup_CBBACKActivity.R_InitializeAndGetActivitySource(nameof(PublicCBLookupRecordController));
    }
    
    [HttpPost]
    public CBLGenericRecord<CBL00100DTO> CBL00100GetRecord(CBL00100ParameterDTO poEntity)
    {
        using Activity activity = _activitySource.StartActivity("CBL00100GetRecord");
        var loEx = new R_Exception();
        CBLGenericRecord<CBL00100DTO> loRtn = new();
        _loggerAp.LogInfo("Start CBL00100GetRecord");

        try
        {
            var loCls = new PublicCBLookupCls();
            _loggerAp.LogInfo("Set Param CBL00100ReceiptLookUp");
            poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poEntity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            var loResult = loCls.ReceiptFromCustomerLookup(poEntity);

            _loggerAp.LogInfo("Filter Search by text CBL00100GetRecord");
            loRtn.Data = loResult.Find(x => x.CTENANT_ID_NAME.Equals(poEntity.CSEARCH_TEXT.Trim(), StringComparison.OrdinalIgnoreCase));
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _loggerAp.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        _loggerAp.LogInfo("End CBL00110GetRecord");
        return loRtn;
    }
    [HttpPost]
    public CBLGenericRecord<CBL00200DTO> CBL00200GetRecord(CBL00200ParameterDTO poEntity)
    {
        using Activity activity = _activitySource.StartActivity("CBL00200GetRecord");
        var loEx = new R_Exception();
        CBLGenericRecord<CBL00200DTO> loRtn = new();
        _loggerAp.LogInfo("Start CBL00200GetRecord");

        try
        {
            var loCls = new PublicCBLookupCls();
            _loggerAp.LogInfo("Set Param CBL00200CBJournalLookUp");
            poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poEntity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            var loResult = loCls.CBJournalLookup(poEntity);

            _loggerAp.LogInfo("Filter Search by text CBL00100GetRecord");
            loRtn.Data = loResult.Find(x => x.CCB_CODE.Equals(poEntity.CSEARCH_TEXT.Trim(), StringComparison.OrdinalIgnoreCase));
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _loggerAp.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        _loggerAp.LogInfo("End CBL00110GetRecord");
        return loRtn;
    }
}