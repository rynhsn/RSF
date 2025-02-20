using System.Diagnostics;
using Lookup_HDBACK;
using Lookup_HDCOMMON;
using Lookup_HDCOMMON.DTOs;
using Lookup_HDCOMMON.DTOs.HDL00100;
using Lookup_HDCOMMON.DTOs.HDL00200;
using Lookup_HDCOMMON.DTOs.HDL00300;
using Lookup_HDCOMMON.DTOs.HDL00400;
using Lookup_HDCOMMON.Loggers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace Lookup_HDSERVICESE;

[ApiController]
[Route("api/[controller]/[action]"), AllowAnonymous]
public class PublicLookupHDRecordController : ControllerBase, IPublicHDLookupRecord
{
    private LoggerHDPublicLookup _loggerHD;
    public readonly ActivitySource _activitySource;

    public PublicLookupHDRecordController(ILogger<LoggerHDPublicLookup> logger)
    {
        //Initial and Get Logger
        LoggerHDPublicLookup.R_InitializeLogger(logger);
        _loggerHD = LoggerHDPublicLookup.R_GetInstanceLogger();
        _activitySource = Lookup_HDBackActivity.R_InitializeAndGetActivitySource(nameof(PublicLookupHDController));
    }

    [HttpPost]
    public HDLGenericRecord<HDL00100DTO> HDL00100GetRecord(HDL00100ParameterDTO poEntity)
    {
        using Activity activity = _activitySource.StartActivity("HDL00100GetRecord");
        var loEx = new R_Exception();
        HDLGenericRecord<HDL00100DTO> loRtn = new();
        _loggerHD.LogInfo("Start HDL00100GetRecord");

        try
        {
            var loCls = new PublicHDLookUpCls();
            _loggerHD.LogInfo("Set Param HDL00100GetPriceListHeaderLookup");
            poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poEntity.CUSER_ID = R_BackGlobalVar.USER_ID;
            poEntity.CLANG_ID = R_BackGlobalVar.CULTURE;
            var loResult = loCls.PriceListLookupHeader(poEntity);

            _loggerHD.LogInfo("Filter Search by text HDL00100GetRecord");
            loRtn.Data = loResult.Find(x =>
                x.CPRICELIST_ID.Equals(poEntity.CSEARCH_TEXT_ID.Trim(), StringComparison.OrdinalIgnoreCase));
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _loggerHD.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        _loggerHD.LogInfo("End HDL00100GetRecord");
        return loRtn;
    }

    [HttpPost]
    public HDLGenericRecord<HDL00200DTO> HDL00200GetRecord(HDL00200ParameterDTO poEntity)
    {
        using Activity activity = _activitySource.StartActivity("HDL00200GetRecord");
        var loEx = new R_Exception();
        HDLGenericRecord<HDL00200DTO> loRtn = new();
        _loggerHD.LogInfo("Start HDL00100GetRecord");

        try
        {
            var loCls = new PublicHDLookUpCls();
            _loggerHD.LogInfo("Set Param HDL00200GetPriceListItemLookup");
            poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poEntity.CUSER_ID = R_BackGlobalVar.USER_ID;
            poEntity.CLANG_ID = R_BackGlobalVar.CULTURE;
            var loResult = loCls.PriceListItemLookup(poEntity);

            _loggerHD.LogInfo("Filter Search by text HDL00200GetRecord");
            loRtn.Data = loResult.Find(x =>
                x.CVALID_ID.Equals(poEntity.CSEARCH_TEXT_ID.Trim(), StringComparison.OrdinalIgnoreCase));
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _loggerHD.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        _loggerHD.LogInfo("End HDL00200GetRecord");
        return loRtn;
    }

    [HttpPost]
    public HDLGenericRecord<HDL00300DTO> HDL00300GetRecord(HDL00300ParameterDTO poEntity)
    {
        using Activity activity = _activitySource.StartActivity("HDL00300GetRecord");
        var loEx = new R_Exception();
        HDLGenericRecord<HDL00300DTO> loRtn = new();
        _loggerHD.LogInfo("Start HDL00100GetRecord");

        try
        {
            var loCls = new PublicHDLookUpCls();
            _loggerHD.LogInfo("Set Param HDL00300GetPublicLocationLookup");
            poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poEntity.CUSER_ID = R_BackGlobalVar.USER_ID;
            var loResult = loCls.PublicLocationLookup(poEntity);

            _loggerHD.LogInfo("Filter Search by text HDL02100GetRecord");
            loRtn.Data = loResult.Find(x =>
                x.CPUBLIC_LOC_ID.Equals(poEntity.CSEARCH_TEXT_ID.Trim(), StringComparison.OrdinalIgnoreCase));
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _loggerHD.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        _loggerHD.LogInfo("End HDL00300GetRecord");
        return loRtn;
    }

    [HttpPost]
    public HDLGenericRecord<HDL00400DTO> HDL00400GetRecord(HDL00400ParameterDTO poEntity)
    {
        using Activity activity = _activitySource.StartActivity("HDL00400GetRecord");
        var loEx = new R_Exception();
        HDLGenericRecord<HDL00400DTO> loRtn = new();
        _loggerHD.LogInfo("Start HDL00400GetRecord");

        try
        {
            var loCls = new PublicHDLookUpCls();
            _loggerHD.LogInfo("Set Param HDL00400GetRecord");
            poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poEntity.CUSER_ID = R_BackGlobalVar.USER_ID;
            var loResult = loCls.AssetLookup(poEntity);

            _loggerHD.LogInfo("Filter Search by text HDL00400GetRecord");
            loRtn.Data = loResult.Find(x =>
                x.CASSET_CODE.Equals(poEntity.CSEARCH_TEXT_ID.Trim(), StringComparison.OrdinalIgnoreCase));
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _loggerHD.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        _loggerHD.LogInfo("End HDL00400GetRecord");
        return loRtn;
    }
}