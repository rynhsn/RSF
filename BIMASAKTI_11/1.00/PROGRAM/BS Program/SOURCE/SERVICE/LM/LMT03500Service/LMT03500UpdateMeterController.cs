﻿using System.Diagnostics;
using LMT03500Back;
using LMT03500Common;
using LMT03500Common.DTOs;
using LMT03500Common.Params;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace LMT03500Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class LMT03500UpdateMeterController : ControllerBase, ILMT03500UpdateMeter
{
    private LoggerLMT03500 _logger;
    private readonly ActivitySource _activitySource;

    public LMT03500UpdateMeterController(ILogger<LMT03500UpdateMeterController> logger)
    {
        //Initial and Get Logger
        LoggerLMT03500.R_InitializeLogger(logger);
        _logger = LoggerLMT03500.R_GetInstanceLogger();
        _activitySource = LMT03500Activity.R_InitializeAndGetActivitySource(nameof(LMT03500UpdateMeterController));
    }
    [HttpPost]
    public IAsyncEnumerable<LMT03500UtilityMeterDTO> LMT03500GetUtilityMeterListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(LMT03500GetUtilityMeterListStream));
        _logger.LogInfo("Start - Get UtilityMeter List Stream");
        var loEx = new R_Exception();
        var loCls = new LMT03500UpdateMeterCls();
        var loDbParams = new LMT03500ParameterDb();
        List<LMT03500UtilityMeterDTO> loResult = null;
        IAsyncEnumerable<LMT03500UtilityMeterDTO> loReturn = null;


        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(LMT03500ContextConstant.CPROPERTY_ID);
            loDbParams.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(LMT03500ContextConstant.CBUILDING_ID);
            loDbParams.CTENANT_ID = R_Utility.R_GetStreamingContext<string>(LMT03500ContextConstant.CTENANT_ID);

            _logger.LogInfo("Get UtilityMeter List Stream");
            loResult = loCls.GetUtilityMeterList(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get UtilityMeter List Stream");
        return loReturn;
    }

    [HttpPost]
    public IAsyncEnumerable<LMT03500BuildingUnitDTO> LMT03500GetBuildingUnitListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(LMT03500GetBuildingUnitListStream));
        _logger.LogInfo("Start - Get BuildingUnit List Stream");
        var loEx = new R_Exception();
        var loCls = new LMT03500UpdateMeterCls();
        var loDbParams = new LMT03500ParameterDb();
        List<LMT03500BuildingUnitDTO> loResult = null;
        IAsyncEnumerable<LMT03500BuildingUnitDTO> loReturn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(LMT03500ContextConstant.CPROPERTY_ID);
            loDbParams.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(LMT03500ContextConstant.CBUILDING_ID);
            loDbParams.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(LMT03500ContextConstant.CFLOOR_ID);

            _logger.LogInfo("Get BuildingUnit List Stream");
            loResult = loCls.GetBuildingUnitList(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get BuildingUnit List Stream");
        return loReturn;
    }
    
    [HttpPost]
    public LMT03500SingleDTO<LMT03500UtilityMeterDetailDTO> LMT03500GetUtilityMeterDetail(LMT03500UtilityMeterDetailParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(LMT03500GetUtilityMeterDetail));
        _logger.LogInfo("Start - Get UtilityMeter Detail");
        var loEx = new R_Exception();
        var loCls = new LMT03500UpdateMeterCls();
        var loDbParams = new LMT03500ParameterDb();
        var loResult = new LMT03500SingleDTO<LMT03500UtilityMeterDetailDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPROPERTY_ID = poParam.CPROPERTY_ID;
            loDbParams.CBUILDING_ID = poParam.CBUILDING_ID;
            loDbParams.CUNIT_ID = poParam.CUNIT_ID;
            loDbParams.CCHARGES_TYPE = poParam.CCHARGES_TYPE;
            loDbParams.CCHARGES_ID = poParam.CCHARGES_ID;

            _logger.LogInfo("Get UtilityMeter Detail");
            loResult.Data = loCls.GetUtilityMeterDetail(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get UtilityMeter Detail");
        return loResult;
    }
    
    [HttpPost]
    public LMT03500SingleDTO<LMT03500AgreementUtilitiesDTO> LMT03500GetAgreementUtilities(LMT03500AgreementUtilitiesParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(LMT03500GetAgreementUtilities));
        _logger.LogInfo("Start - Get Agreement Utilities");
        var loEx = new R_Exception();
        var loCls = new LMT03500UpdateMeterCls();
        var loDbParams = new LMT03500ParameterDb();
        var loResult = new LMT03500SingleDTO<LMT03500AgreementUtilitiesDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPROPERTY_ID = poParam.CPROPERTY_ID;
            loDbParams.CBUILDING_ID = poParam.CBUILDING_ID;
            loDbParams.CUNIT_ID = poParam.CUNIT_ID;

            _logger.LogInfo("Get Agreement Utilities");
            loResult.Data = loCls.GetAgreementUtilities(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Agreement Utilities");
        return loResult;
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