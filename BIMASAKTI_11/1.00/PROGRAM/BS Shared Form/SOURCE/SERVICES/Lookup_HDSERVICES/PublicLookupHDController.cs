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
public class PublicLookupHDController : ControllerBase, IPublicHDLookup
{
    private LoggerHDPublicLookup _loggerHD;
    public readonly ActivitySource _activitySource;

    public PublicLookupHDController(ILogger<LoggerHDPublicLookup> logger)
    {
        //Initial and Get Logger
        LoggerHDPublicLookup.R_InitializeLogger(logger);
        _loggerHD = LoggerHDPublicLookup.R_GetInstanceLogger();
        _activitySource = Lookup_HDBackActivity.R_InitializeAndGetActivitySource(nameof(PublicLookupHDController));
    }
    
    [HttpPost]
    public IAsyncEnumerable<HDL00100DTO> HDL00100PriceListLookup()
    {
        using Activity activity = _activitySource.StartActivity(nameof(HDL00100PriceListLookup));
        _loggerHD.LogInfo("Start HDL00100PriceListLookup");
        var loException = new R_Exception();
        IAsyncEnumerable<HDL00100DTO> loRtn = null;

        try
        {
            var loCls = new PublicHDLookUpCls();
            var poParam = new HDL00100ParameterDTO();
                
            _loggerHD.LogInfo("Set Param HDL00100PriceListLookup");
            poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParam.CLANG_ID = R_BackGlobalVar.CULTURE;
            poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
            poParam.CTAXABLE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTAXABLE);

            _loggerHD.LogInfo("Call Back Method GetPrieListLookup");
            var loResult = loCls.PriceListLookupHeader(poParam);
                
            _loggerHD.LogInfo("Call Stream Method Data HDL00100PriceListLookup");
            loRtn = GetStream<HDL00100DTO>(loResult);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        _loggerHD.LogInfo("End HDL00100PriceListLookup");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<HDL00100DTO> HDL00100PriceListDetailLookup()
    {
        using Activity activity = _activitySource.StartActivity(nameof(HDL00100PriceListDetailLookup));
        _loggerHD.LogInfo("Start HDL00100PriceListDetailLookup");
        var loException = new R_Exception();
        IAsyncEnumerable<HDL00100DTO> loRtn = null;

        try
        {
            var loCls = new PublicHDLookUpCls();
            var poParam = new HDL00100ParameterDTO();
                
            _loggerHD.LogInfo("Set Param HDL00100PriceListDetailLookup");
            poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParam.CLANG_ID = R_BackGlobalVar.CULTURE;
            poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
            poParam.CSTATUS = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CSTATUS);
            poParam.CPRICELIST_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPRICELIST_ID);
            poParam.CLOOKUP_DATE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CLOOKUP_DATE);
            poParam.CSLA_CALL_TYPE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CSLA_CALL_TYPE_ID);
            poParam.CTAXABLE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTAXABLE);

            _loggerHD.LogInfo("Call Back Method GetPriceListDetail");
            var loResult = loCls.PriceListLookupDetail(poParam);
                
            _loggerHD.LogInfo("Call Stream Method Data HDL00100PriceListDetailLookup");
            loRtn = GetStream<HDL00100DTO>(loResult);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        _loggerHD.LogInfo("End HDL00100PriceListDetailLookup");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<HDL00200DTO> HDL00200PriceListItemLookup()
    {
        using Activity activity = _activitySource.StartActivity(nameof(HDL00200PriceListItemLookup));
        _loggerHD.LogInfo("Start HDL00200PriceListItemLookup");
        var loException = new R_Exception();
        IAsyncEnumerable<HDL00200DTO> loRtn = null;

        try
        {
            var loCls = new PublicHDLookUpCls();
            var poParam = new HDL00200ParameterDTO();
                
            _loggerHD.LogInfo("Set Param HDL00200PriceListItemLookup");
            poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParam.CLANG_ID = R_BackGlobalVar.CULTURE;
            poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
            poParam.CSTATUS = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CSTATUS);
            poParam.CPRICELIST_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPRICELIST_ID);
            poParam.CLOOKUP_DATE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CLOOKUP_DATE);
            poParam.CSLA_CALL_TYPE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CSLA_CALL_TYPE_ID);
            poParam.CTAXABLE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTAXABLE);

            _loggerHD.LogInfo("Call Back Method GetPriceListDetail");
            var loResult = loCls.PriceListItemLookup(poParam);
                
            _loggerHD.LogInfo("Call Stream Method Data HDL00200PriceListItemLookup");
            loRtn = GetStream<HDL00200DTO>(loResult);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        _loggerHD.LogInfo("End HDL00200PriceListItemLookup");
        return loRtn;
    }
[HttpPost]
    public IAsyncEnumerable<HDL00300DTO> HDL00300PublicLocationLookup()
    {
        using Activity activity = _activitySource.StartActivity(nameof(HDL00300PublicLocationLookup));
        _loggerHD.LogInfo("Start HDL00300PublicLocationLookup");
        var loException = new R_Exception();
        IAsyncEnumerable<HDL00300DTO> loRtn = null;

        try
        {
            var loCls = new PublicHDLookUpCls();
            var poParam = new HDL00300ParameterDTO();
                
            _loggerHD.LogInfo("Set Param HDL00300PublicLocationLookup");
            poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
            poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
            poParam.LACTIVE = R_Utility.R_GetStreamingContext<bool>(ContextConstantPublicLookup.LACTIVE);
            _loggerHD.LogInfo("Call Back Method GetPublicLocationLookup");
            var loResult = loCls.PublicLocationLookup(poParam);
                
            _loggerHD.LogInfo("Call Stream Method Data HDL00300PublicLocationLookup");
            loRtn = GetStream<HDL00300DTO>(loResult);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        _loggerHD.LogInfo("End HDL00300PublicLocationLookup");
        return loRtn;
    }
[HttpPost]
    public IAsyncEnumerable<HDL00400DTO> HDL00400AssetLookup()
    {
        using Activity activity = _activitySource.StartActivity(nameof(HDL00300PublicLocationLookup));
        _loggerHD.LogInfo("Start HDL00400AssetLookup");
        var loException = new R_Exception();
        IAsyncEnumerable<HDL00400DTO> loRtn = null;

        try
        {
            var loCls = new PublicHDLookUpCls();
            var poParam = new HDL00400ParameterDTO();
                
            _loggerHD.LogInfo("Set Param HDL00400AssetLookup");
            poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
            poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
            poParam.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CBUILDING_ID);
            poParam.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CFLOOR_ID);
            poParam.CUNIT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CUNIT_ID);
            poParam.CLOCATION_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CLOCATION_ID);
            
            _loggerHD.LogInfo("Call Back Method GetAssetLookup");
            var loResult = loCls.AssetLookup(poParam);
                
            _loggerHD.LogInfo("Call Stream Method Data HDL00400AssetLookup");
            loRtn = GetStream<HDL00400DTO>(loResult);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        _loggerHD.LogInfo("End HDL00300PublicLocationLookup");
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