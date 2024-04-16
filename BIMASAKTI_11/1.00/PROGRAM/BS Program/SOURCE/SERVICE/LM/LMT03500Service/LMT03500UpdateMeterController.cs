using System.Diagnostics;
using PMT03500Back;
using LMT03500Common;
using LMT03500Common.DTOs;
using LMT03500Common.Params;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMT03500Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class LMT03500UpdateMeterController : ControllerBase, ILMT03500UpdateMeter
{
    private LoggerPMT03500 _logger;
    private readonly ActivitySource _activitySource;

    public LMT03500UpdateMeterController(ILogger<LMT03500UpdateMeterController> logger)
    {
        //Initial and Get Logger
        LoggerPMT03500.R_InitializeLogger(logger);
        _logger = LoggerPMT03500.R_GetInstanceLogger();
        _activitySource = PMT03500Activity.R_InitializeAndGetActivitySource(nameof(LMT03500UpdateMeterController));
    }
    
    [HttpPost]
    public IAsyncEnumerable<PMT03500UtilityMeterDTO> LMT03500GetUtilityMeterListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(LMT03500GetUtilityMeterListStream));
        _logger.LogInfo("Start - Get UtilityMeter List Stream");
        var loEx = new R_Exception();
        var loCls = new PMT03500UpdateMeterCls();
        var loDbParams = new PMT03500ParameterDb();
        List<PMT03500UtilityMeterDTO> loResult = null;
        IAsyncEnumerable<PMT03500UtilityMeterDTO> loReturn = null;


        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CPROPERTY_ID);
            loDbParams.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CBUILDING_ID);
            loDbParams.CTENANT_ID = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CTENANT_ID);

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
    public IAsyncEnumerable<PMT03500BuildingUnitDTO> LMT03500GetBuildingUnitListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(LMT03500GetBuildingUnitListStream));
        _logger.LogInfo("Start - Get BuildingUnit List Stream");
        var loEx = new R_Exception();
        var loCls = new PMT03500UpdateMeterCls();
        var loDbParams = new PMT03500ParameterDb();
        List<PMT03500BuildingUnitDTO> loResult = null;
        IAsyncEnumerable<PMT03500BuildingUnitDTO> loReturn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CPROPERTY_ID);
            loDbParams.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CBUILDING_ID);
            loDbParams.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CFLOOR_ID);

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

    public LMT03500SingleDTO<PMT03500BuildingUnitDTO> LMT03500GetBuildingUnitRecord(LMT03500SearchTextDTO poParam)
    {
        using Activity activity = _activitySource.StartActivity(nameof(LMT03500GetBuildingUnitRecord));
        _logger.LogInfo("Start Start - Lookup Building Unit Record");
        
        var loEx = new R_Exception();
        LMT03500SingleDTO<PMT03500BuildingUnitDTO> loReturn = new();
        var loCls = new PMT03500UpdateMeterCls();
        var loDbPar = new PMT03500ParameterDb();
        
        try
        {
            _logger.LogInfo("Set Parameter");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CPROPERTY_ID = poParam.CPROPERTY_ID;
            loDbPar.CBUILDING_ID = poParam.CBUILDING_ID;
            loDbPar.CFLOOR_ID = poParam.CFLOOR_ID;
            
            _logger.LogInfo("Call Back Method - Lookup Building Unit Record");
            var loResult = loCls.GetBuildingUnitList(loDbPar);

            _logger.LogInfo("Filter Search by text - Lookup Building Unit Record");
            loReturn.Data = loResult.Find(x => x.CUNIT_ID.Trim() == poParam.CSEARCH_TEXT);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        _logger.LogInfo("End Lookup Building Unit Record");
        return loReturn;
    }

    [HttpPost]
    public LMT03500SingleDTO<PMT03500UtilityMeterDetailDTO> LMT03500GetUtilityMeterDetail(LMT03500UtilityMeterDetailParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(LMT03500GetUtilityMeterDetail));
        _logger.LogInfo("Start - Get UtilityMeter Detail");
        var loEx = new R_Exception();
        var loCls = new PMT03500UpdateMeterCls();
        var loDbParams = new PMT03500ParameterDb();
        var loResult = new LMT03500SingleDTO<PMT03500UtilityMeterDetailDTO>();

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
    public LMT03500SingleDTO<PMT03500AgreementUtilitiesDTO> LMT03500GetAgreementUtilities(LMT03500AgreementUtilitiesParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(LMT03500GetAgreementUtilities));
        _logger.LogInfo("Start - Get Agreement Utilities");
        var loEx = new R_Exception();
        var loCls = new PMT03500UpdateMeterCls();
        var loDbParams = new PMT03500ParameterDb();
        var loResult = new LMT03500SingleDTO<PMT03500AgreementUtilitiesDTO>();

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

    public void LMT03500UpdateMeterNo(LMT03500UpdateChangeMeterNoParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(LMT03500UpdateMeterNo));
        _logger.LogInfo("Start - Generate Account Budget");
        var loEx = new R_Exception();
        var loCls = new PMT03500UpdateMeterCls();
        var loDbParams = new PMT03500ParameterDb();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPROPERTY_ID = poParam.CPROPERTY_ID;
            loDbParams.CREF_NO = poParam.CREF_NO;
            loDbParams.CUNIT_ID = poParam.CUNIT_ID;
            loDbParams.CTENANT_ID = poParam.CTENANT_ID;
            loDbParams.CUTILITY_TYPE = poParam.CUTILITY_TYPE;
            loDbParams.CMETER_NO = poParam.CMETER_NO;
            loDbParams.IMETER_START = poParam.IMETER_START;
            loDbParams.CSTART_INV_PRD = poParam.CSTART_INV_PRD;
            loDbParams.CSTART_DATE = poParam.CSTART_DATE;

            _logger.LogInfo("Generate Account Budget");
            loCls.PMT03500UpdateMeterNo(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Generate Account Budget");  
    }

    public void LMT03500ChangeMeterNo(LMT03500UpdateChangeMeterNoParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(LMT03500ChangeMeterNo));
        _logger.LogInfo("Start - Generate Account Budget");
        var loEx = new R_Exception();
        var loCls = new PMT03500UpdateMeterCls();
        var loDbParams = new PMT03500ParameterDb();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPROPERTY_ID = poParam.CPROPERTY_ID;
            loDbParams.CREF_NO = poParam.CREF_NO;
            loDbParams.CUNIT_ID = poParam.CUNIT_ID;
            loDbParams.CTENANT_ID = poParam.CTENANT_ID;
            loDbParams.CUTILITY_TYPE = poParam.CUTILITY_TYPE;
            loDbParams.IFROM_METER_NO = poParam.IFROM_METER_NO;
            loDbParams.IMETER_END = poParam.IMETER_END;
            loDbParams.ITO_METER_NO = poParam.ITO_METER_NO;
            loDbParams.IMETER_START = poParam.IMETER_START;
            loDbParams.CSTART_INV_PRD = poParam.CSTART_INV_PRD;
            loDbParams.CSTART_DATE = poParam.CSTART_DATE;

            _logger.LogInfo("Generate Account Budget");
            loCls.PMT03500ChangeMeterNo(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Generate Account Budget");  
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