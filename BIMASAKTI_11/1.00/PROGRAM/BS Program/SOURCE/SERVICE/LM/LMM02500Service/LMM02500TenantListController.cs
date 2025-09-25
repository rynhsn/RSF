using LMM02500Back;
using LMM02500Common;
using LMM02500Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM02500Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class LMM02500TenantListController : ILMM02500TenantList
{
    private LoggerLMM02500 _logger;

    public LMM02500TenantListController(ILogger<LMM02500TenantListController> logger)
    {
        LoggerLMM02500.R_InitializeLogger(logger);
        _logger = LoggerLMM02500.R_GetInstanceLogger();
    }
    
    [HttpPost]
    public R_ServiceGetRecordResultDTO<LMM02500TenantListGroupDTO> R_ServiceGetRecord(
        R_ServiceGetRecordParameterDTO<LMM02500TenantListGroupDTO> poParameter)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<LMM02500TenantListGroupDTO> R_ServiceSave(
        R_ServiceSaveParameterDTO<LMM02500TenantListGroupDTO> poParameter)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM02500TenantListGroupDTO> poParameter)
    {
        throw new NotImplementedException();
    }


    [HttpPost]
    public IAsyncEnumerable<LMM02500TenantListGroupDTO> LMM02500GetTenantListGroup()
    {
        _logger.LogInfo("Start - Get List");
        R_Exception loEx = new();
        LMM02500ParameterDb loDbPar;
        List<LMM02500TenantListGroupDTO> loRtnTmp;
        LMM02500TenantListCls loCls;
        IAsyncEnumerable<LMM02500TenantListGroupDTO> loRtn = null;

        try
        {
            _logger.LogInfo("Set Parameter");

            loDbPar = new();
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(LMM02500ContextConstant.CPROPERTY_ID);

            loCls = new LMM02500TenantListCls();
            _logger.LogInfo("Get List");
            loRtnTmp = loCls.GetTenantGroupList(loDbPar);

            loRtn = GetStream(loRtnTmp);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get List");
        return loRtn;
    }

    #region "Helper GetStream Function"
    private async IAsyncEnumerable<T> GetStream<T>(List<T> poParameter)
    {
        foreach (var item in poParameter)
        {
            yield return item;
        }
    }
    #endregion
}