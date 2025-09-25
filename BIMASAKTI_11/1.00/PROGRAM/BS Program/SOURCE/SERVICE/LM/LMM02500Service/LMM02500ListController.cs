using LMM02500Back;
using LMM02500Common;
using LMM02500Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace LMM02500Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class LMM02500ListController : ILMM02500List
{
    private LoggerLMM02500 _logger;

    public LMM02500ListController(ILogger<LMM02500ListController> logger)
    {
        LoggerLMM02500.R_InitializeLogger(logger);
        _logger = LoggerLMM02500.R_GetInstanceLogger();
    }

    [HttpPost]
    public IAsyncEnumerable<LMM02500TenantGroupDTO> LMM02500GetTenantGroupList()
    {
        _logger.LogInfo("Start - Get List");
        R_Exception loEx = new();
        LMM02500ParameterDb loDbPar;
        List<LMM02500TenantGroupDTO> loRtnTmp;
        LMM02500ListCls loCls;
        IAsyncEnumerable<LMM02500TenantGroupDTO> loRtn = null;

        try
        {
            _logger.LogInfo("Set Parameter");

            loDbPar = new();
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(LMM02500ContextConstant.CPROPERTY_ID);

            loCls = new LMM02500ListCls();
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