using System.Diagnostics;
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
public class LMT03500UtilityUsageController : ControllerBase, ILMT03500UtilityUsage
{
    private LoggerLMT03500 _logger;
    private readonly ActivitySource _activitySource;

    public LMT03500UtilityUsageController(ILogger<LMT03500UtilityUsageController> logger)
    {
        //Initial and Get Logger
        LoggerLMT03500.R_InitializeLogger(logger);
        _logger = LoggerLMT03500.R_GetInstanceLogger();
        _activitySource = LMT03500Activity.R_InitializeAndGetActivitySource(nameof(LMT03500InitController));
    }

    [HttpPost]
    public IAsyncEnumerable<LMT03500BuildingDTO> LMT03500GetBuildingListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(LMT03500GetBuildingListStream));
        _logger.LogInfo("Start - Get Building List Stream");
        var loEx = new R_Exception();
        var loCls = new LMT03500UtilityUsageCls();
        var loDbParams = new LMT03500ParameterDb();
        List<LMT03500BuildingDTO> loResult = null;
        IAsyncEnumerable<LMT03500BuildingDTO> loReturn = null;


        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(LMT03500ContextConstant.CPROPERTY_ID);

            _logger.LogInfo("Get Building List Stream");
            loResult = loCls.GetBuildingList(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Building List Stream");
        return loReturn;
    }

    [HttpPost]
    public IAsyncEnumerable<LMT03500UtilityUsageDTO> LMT03500GetUtilityUsageListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(LMT03500GetUtilityUsageListStream));
        _logger.LogInfo("Start - Get Building List Stream");
        var loEx = new R_Exception();
        var loCls = new LMT03500UtilityUsageCls();
        var loDbParams = new LMT03500ParameterDb();
        List<LMT03500UtilityUsageDTO> loResult = null;
        IAsyncEnumerable<LMT03500UtilityUsageDTO> loReturn = null;


        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(LMT03500ContextConstant.CPROPERTY_ID);
            loDbParams.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(LMT03500ContextConstant.CBUILDING_ID);
            loDbParams.CUTILITY_TYPE = R_Utility.R_GetStreamingContext<string>(LMT03500ContextConstant.CUTILITY_TYPE);
            loDbParams.CFLOOR_LIST = R_Utility.R_GetStreamingContext<string>(LMT03500ContextConstant.CFLOOR_LIST);
            loDbParams.LALL_FLOOR = R_Utility.R_GetStreamingContext<bool>(LMT03500ContextConstant.LALL_FLOOR);
            loDbParams.CINVOICE_PRD = R_Utility.R_GetStreamingContext<string>(LMT03500ContextConstant.CINVOICE_PRD);
            loDbParams.LINVOICED = R_Utility.R_GetStreamingContext<bool>(LMT03500ContextConstant.LINVOICED);
            loDbParams.CUTILITY_PRD = R_Utility.R_GetStreamingContext<string>(LMT03500ContextConstant.CUTILITY_PRD);
            loDbParams.CUTILITY_PRD_FROM_DATE =
                R_Utility.R_GetStreamingContext<string>(LMT03500ContextConstant.CUTILITY_PRD_FROM_DATE);
            loDbParams.CUTILITY_PRD_TO_DATE =
                R_Utility.R_GetStreamingContext<string>(LMT03500ContextConstant.CUTILITY_PRD_TO_DATE);
            // var leType = (ELMT03500UtilityUsageTypeDb)peType;

            _logger.LogInfo("Get Building List Stream");
            loResult = loCls.GetUtilityUsageList(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Building List Stream");
        return loReturn;
    }
    
    [HttpPost]
    public LMT03500SingleDTO<LMT03500UtilityUsageDetailDTO> LMT03500GetUtilityUsageDetail(LMT03500UtilityUsageDetailParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(LMT03500GetUtilityTypeList));
        _logger.LogInfo("Start - Get Floor List");

        var loEx = new R_Exception();
        var loCls = new LMT03500UtilityUsageCls();
        var loDbParams = new LMT03500ParameterDb();
        var loReturn = new LMT03500SingleDTO<LMT03500UtilityUsageDetailDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPROPERTY_ID = poParam.CPROPERTY_ID;
            loDbParams.CREF_NO = poParam.CREF_NO;

            _logger.LogInfo("Get Floor List");
            loReturn.Data = loCls.GetUtilityUsageDetail(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Floor List");
        return loReturn;
        
    }

    [HttpPost]
    public LMT03500ListDTO<LMT03500FunctDTO> LMT03500GetUtilityTypeList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(LMT03500GetUtilityTypeList));
        _logger.LogInfo("Start - Get Utility Type List");

        var loEx = new R_Exception();
        var loCls = new LMT03500UtilityUsageCls();
        var loDbParams = new LMT03500ParameterDb();
        var loReturn = new LMT03500ListDTO<LMT03500FunctDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

            _logger.LogInfo("Get Utility Type List");
            loReturn.Data = loCls.GetUtilityTypeList(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Utility Type List");
        return loReturn;
    }

    [HttpPost]
    public LMT03500ListDTO<LMT03500FloorDTO> LMT03500GetFloorList(LMT03500FloorParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(LMT03500GetUtilityTypeList));
        _logger.LogInfo("Start - Get Floor List");

        var loEx = new R_Exception();
        var loCls = new LMT03500UtilityUsageCls();
        var loDbParams = new LMT03500ParameterDb();
        var loReturn = new LMT03500ListDTO<LMT03500FloorDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPROPERTY_ID = poParam.CPROPERTY_ID;
            loDbParams.CBUILDING_ID = poParam.CBUILDING_ID;

            _logger.LogInfo("Get Floor List");
            loReturn.Data = loCls.GetFloorList(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Floor List");
        return loReturn;
    }

    [HttpPost]
    public LMT03500ListDTO<LMT03500PeriodDTO> LMT03500GetPeriodList(LMT03500PeriodParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(LMT03500GetUtilityTypeList));
        _logger.LogInfo("Start - Get Period List");

        var loEx = new R_Exception();
        var loCls = new LMT03500UtilityUsageCls();
        var loDbParams = new LMT03500ParameterDb();
        var loReturn = new LMT03500ListDTO<LMT03500PeriodDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CYEAR = poParam.CYEAR;

            _logger.LogInfo("Get Period List");
            loReturn.Data = loCls.GetPeriodList(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Period List");
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