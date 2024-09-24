using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMB00300Back;
using PMB00300Common;
using PMB00300Common.DTOs;
using PMB00300Common.Params;
using R_BackEnd;
using R_Common;

namespace PMB00300Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class PMB00300Controller : ControllerBase, IPMB00300
{
    private LoggerPMB00300 _logger;
    private readonly ActivitySource _activitySource;

    public PMB00300Controller(ILogger<PMB00300Controller> logger)
    {
        //ial and Get Logger
        LoggerPMB00300.R_InitializeLogger(logger);
        _logger = LoggerPMB00300.R_GetInstanceLogger();
        _activitySource = PMB00300Activity.R_InitializeAndGetActivitySource(nameof(PMB00300Controller));
    }

    [HttpPost]
    public PMB00300ListDTO<PMB00300PropertyDTO> PMB00300GetPropertyList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMB00300GetPropertyList));

        _logger.LogInfo("Start - Get Property");
        var loEx = new R_Exception();
        var loCls = new PMB00300Cls();
        var loDbParams = new PMB00300ParameterDb();
        var loReturn = new PMB00300ListDTO<PMB00300PropertyDTO>();

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
    public IAsyncEnumerable<PMB00300RecalcDTO> PMB00300GetRecalcListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMB00300GetRecalcListStream));
        _logger.LogInfo("Start - Get Recalc List Stream");
        var loEx = new R_Exception();
        var loCls = new PMB00300Cls();
        var loDbParams = new PMB00300ParameterDb();
        List<PMB00300RecalcDTO> loResult = null;
        IAsyncEnumerable<PMB00300RecalcDTO> loReturn = null;


        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANG_ID = R_BackGlobalVar.CULTURE;
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMB00300ContextConstant.CPROPERTY_ID);

            _logger.LogInfo("Get Recalc List Stream");
            loResult = loCls.GetRecalcList(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Recalc List Stream");
        return loReturn;
    }

    [HttpPost]
    public IAsyncEnumerable<PMB00300RecalcChargesDTO> PMB00300GetRecalcChargesListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMB00300GetRecalcChargesListStream));
        _logger.LogInfo("Start - Get RecalcCharges List Stream");
        var loEx = new R_Exception();
        var loCls = new PMB00300Cls();
        var loDbParams = new PMB00300ParameterDb();
        List<PMB00300RecalcChargesDTO> loResult = null;
        IAsyncEnumerable<PMB00300RecalcChargesDTO> loReturn = null;


        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANG_ID = R_BackGlobalVar.CULTURE;
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMB00300ContextConstant.CPROPERTY_ID);
            loDbParams.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(PMB00300ContextConstant.CDEPT_CODE);
            loDbParams.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(PMB00300ContextConstant.CTRANS_CODE);
            loDbParams.CREF_NO = R_Utility.R_GetStreamingContext<string>(PMB00300ContextConstant.CREF_NO);

            _logger.LogInfo("Get RecalcCharges List Stream");
            loResult = loCls.GetRecalcChargesList(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get RecalcCharges List Stream");
        return loReturn;
    }

    [HttpPost]
    public IAsyncEnumerable<PMB00300RecalcRuleDTO> PMB00300GetRecalcRuleListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMB00300GetRecalcRuleListStream));
        _logger.LogInfo("Start - Get RecalcRule List Stream");
        var loEx = new R_Exception();
        var loCls = new PMB00300Cls();
        var loDbParams = new PMB00300ParameterDb();
        List<PMB00300RecalcRuleDTO> loResult = null;
        IAsyncEnumerable<PMB00300RecalcRuleDTO> loReturn = null;


        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANG_ID = R_BackGlobalVar.CULTURE;
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMB00300ContextConstant.CPROPERTY_ID);
            loDbParams.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(PMB00300ContextConstant.CDEPT_CODE);
            loDbParams.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(PMB00300ContextConstant.CTRANS_CODE);
            loDbParams.CREF_NO = R_Utility.R_GetStreamingContext<string>(PMB00300ContextConstant.CREF_NO);
            loDbParams.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(PMB00300ContextConstant.CBUILDING_ID);
            loDbParams.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(PMB00300ContextConstant.CFLOOR_ID);
            loDbParams.CUNIT_ID = R_Utility.R_GetStreamingContext<string>(PMB00300ContextConstant.CUNIT_ID);    
            loDbParams.CCHARGES_ID = R_Utility.R_GetStreamingContext<string>(PMB00300ContextConstant.CCHARGES_ID);    

            _logger.LogInfo("Get RecalcRule List Stream");
            loResult = loCls.GetRecalcRuleList(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get RecalcRule List Stream");
        return loReturn;
    }

    [HttpPost]
    public PMB00300SingleDTO<bool> PMB00300RecalcBillingRuleProcess(PMB00300RecalcProcessParam poEntity)
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMB00300RecalcBillingRuleProcess));
        _logger.LogInfo("Start - RecalcBillingRuleProcess");
        var loEx = new R_Exception();
        var loCls = new PMB00300Cls();
        var loDbParams = new PMB00300ParameterDb();
        // var loReturn = new PMB00300RecalcDTO();
        var loReturn = new PMB00300SingleDTO<bool>();


        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANG_ID = R_BackGlobalVar.CULTURE;
            loDbParams.CPROPERTY_ID = poEntity.CPROPERTY_ID;
            loDbParams.CDEPT_CODE = poEntity.CDEPT_CODE;
            loDbParams.CTRANS_CODE = poEntity.CTRANS_CODE;
            loDbParams.CREF_NO = poEntity.CREF_NO;
            loDbParams.CBUILDING_ID = poEntity.CBUILDING_ID;
            loDbParams.CUNIT_ID = poEntity.CUNIT_ID;
            loDbParams.CFLOOR_ID = poEntity.CFLOOR_ID;
            loDbParams.NACTUAL_AREA_SIZE = poEntity.NACTUAL_AREA_SIZE;

            _logger.LogInfo("RecalcBillingRuleProcess");
            loCls.RecalcBillingRuleProcess(loDbParams);
            
            loReturn.Data = true;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - RecalcBillingRuleProcess");
        return loReturn;
    }


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