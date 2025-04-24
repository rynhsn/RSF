using System.Diagnostics;
using PMT03500Back;
using PMT03500Common;
using PMT03500Common.DTOs;
using PMT03500Common.Params;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace PMT03500Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class PMT03500UpdateMeterController : ControllerBase, IPMT03500UpdateMeter
{
    private LoggerPMT03500 _logger;
    private readonly ActivitySource _activitySource;

    public PMT03500UpdateMeterController(ILogger<PMT03500UpdateMeterController> logger)
    {
        //Initial and Get Logger
        LoggerPMT03500.R_InitializeLogger(logger);
        _logger = LoggerPMT03500.R_GetInstanceLogger();
        _activitySource = PMT03500Activity.R_InitializeAndGetActivitySource(nameof(PMT03500UpdateMeterController));
    }
    
    [HttpPost]
    public IAsyncEnumerable<PMT03500UtilityMeterDTO> PMT03500GetUtilityMeterListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT03500GetUtilityMeterListStream));
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
            
            // loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CPROPERTY_ID);
            // loDbParams.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CBUILDING_ID);
            // loDbParams.CTENANT_ID = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CTENANT_ID);
            
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CPROPERTY_ID);
            loDbParams.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CDEPT_CODE);
            loDbParams.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CTRANS_CODE);
            loDbParams.CREF_NO = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CREF_NO);
            loDbParams.CUNIT_ID = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CUNIT_ID);
            loDbParams.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CFLOOR_ID);
            loDbParams.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CBUILDING_ID);

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
    public IAsyncEnumerable<PMT03500BuildingUnitDTO> PMT03500GetBuildingUnitListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT03500GetBuildingUnitListStream));
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

    [HttpPost]
    public PMT03500ListDTO<PMT03500MeterNoDTO> PMT03500GetMeterNoList(PMT03500MeterNoParam poParam)
    {
        using Activity activity = _activitySource.StartActivity(nameof(PMT03500GetBuildingUnitRecord));
        _logger.LogInfo("Start - GetMeterNoList");
        
        var loEx = new R_Exception();
        PMT03500ListDTO<PMT03500MeterNoDTO> loReturn = new();
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
            loDbPar.CUNIT_ID = poParam.CUNIT_ID;
            loDbPar.CCHARGES_TYPE = poParam.CCHARGES_TYPE;
            
            _logger.LogInfo("GetMeterNoList");
            loReturn.Data = loCls.GetMeterNoList(loDbPar);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        _logger.LogInfo("End GetMeterNoList");
        return loReturn;
    }
    
    [HttpPost]
    public PMT03500ListDTO<PMT03500PeriodRangeDTO> PMT03500GetPeriodRangeList(PMT03500PeriodRangeParam poParam)
    {
        
        using Activity activity = _activitySource.StartActivity(nameof(PMT03500GetPeriodRangeList));
        _logger.LogInfo("Start - GetPeriodRangeList");
        
        var loEx = new R_Exception();
        PMT03500ListDTO<PMT03500PeriodRangeDTO> loReturn = new();
        var loCls = new PMT03500UpdateMeterCls();
        var loDbPar = new PMT03500ParameterDb();
        
        try
        {
            _logger.LogInfo("Set Parameter");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CFROM_PERIOD = poParam.CFROM_PERIOD;
            loDbPar.CTO_PERIOD = poParam.CTO_PERIOD;
            
            _logger.LogInfo("GetPeriodRangeList");
            loReturn.Data = loCls.GetPeriodRangeList(loDbPar);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        _logger.LogInfo("End GetPeriodRangeList");
        return loReturn;
    }

    [HttpPost]
    public PMT03500SingleDTO<PMT03500BuildingUnitDTO> PMT03500GetBuildingUnitRecord(PMT03500SearchTextDTO poParam)
    {
        using Activity activity = _activitySource.StartActivity(nameof(PMT03500GetBuildingUnitRecord));
        _logger.LogInfo("Start Start - Lookup Building Unit Record");
        
        var loEx = new R_Exception();
        PMT03500SingleDTO<PMT03500BuildingUnitDTO> loReturn = new();
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
    public PMT03500SingleDTO<PMT03500UtilityMeterDetailDTO> PMT03500GetUtilityMeterDetail(PMT03500UtilityMeterDetailParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT03500GetUtilityMeterDetail));
        _logger.LogInfo("Start - Get UtilityMeter Detail");
        var loEx = new R_Exception();
        var loCls = new PMT03500UpdateMeterCls();
        var loDbParams = new PMT03500ParameterDb();
        var loResult = new PMT03500SingleDTO<PMT03500UtilityMeterDetailDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            
            // loDbParams.CPROPERTY_ID = poParam.CPROPERTY_ID;
            // loDbParams.CBUILDING_ID = poParam.CBUILDING_ID;
            // loDbParams.CUNIT_ID = poParam.CUNIT_ID;
            // loDbParams.CCHARGES_TYPE = poParam.CCHARGES_TYPE;
            // loDbParams.CCHARGES_ID = poParam.CCHARGES_ID;
            
           loDbParams.CPROPERTY_ID = poParam.CPROPERTY_ID;
           loDbParams.CDEPT_CODE = poParam.CDEPT_CODE;
           loDbParams.CTRANS_CODE = poParam.CTRANS_CODE;
           loDbParams.CREF_NO = poParam.CREF_NO;
           loDbParams.CUNIT_ID = poParam.CUNIT_ID;
           loDbParams.CFLOOR_ID = poParam.CFLOOR_ID;
           loDbParams.CBUILDING_ID = poParam.CBUILDING_ID;
           loDbParams.CCHARGES_TYPE = poParam.CCHARGES_TYPE;
           loDbParams.CCHARGES_ID = poParam.CCHARGES_ID;
           loDbParams.CSEQ_NO = poParam.CSEQ_NO;

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
    public PMT03500SingleDTO<PMT03500AgreementUtilitiesDTO> PMT03500GetAgreementUtilities(PMT03500AgreementUtilitiesParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT03500GetAgreementUtilities));
        _logger.LogInfo("Start - Get Agreement Utilities");
        var loEx = new R_Exception();
        var loCls = new PMT03500UpdateMeterCls();
        var loDbParams = new PMT03500ParameterDb();
        var loResult = new PMT03500SingleDTO<PMT03500AgreementUtilitiesDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPROPERTY_ID = poParam.CPROPERTY_ID;
            loDbParams.CBUILDING_ID = poParam.CBUILDING_ID;
            loDbParams.CUNIT_ID = poParam.CUNIT_ID;
            loDbParams.CFLOOR_ID = poParam.CFLOOR_ID;
            loDbParams.LOTHER_UNIT = poParam.LOTHER_UNIT;

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

    [HttpPost]
    public PMT03500UtilityMeterDetailDTO PMT03500UpdateMeterNo(PMT03500UpdateChangeMeterNoParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT03500UpdateMeterNo));
        _logger.LogInfo("Start - Update Meter No");
        var loEx = new R_Exception();
        var loCls = new PMT03500UpdateMeterCls();
        var loDbParams = new PMT03500ParameterDb();
        var loReturn = new PMT03500UtilityMeterDetailDTO();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            
            loDbParams.CPROPERTY_ID = poParam.CPROPERTY_ID;
            loDbParams.CREF_NO = poParam.CREF_NO;
            loDbParams.CUNIT_ID = poParam.CUNIT_ID;
            loDbParams.CTENANT_ID = poParam.CTENANT_ID;
            loDbParams.CCHARGES_TYPE = poParam.CCHARGES_TYPE;
            loDbParams.CMETER_NO = poParam.CMETER_NO;
            loDbParams.CSTART_INV_PRD = poParam.CSTART_INV_PRD;
            loDbParams.CSTART_DATE = poParam.CSTART_DATE;
            loDbParams.NMETER_START = poParam.NMETER_START;
            loDbParams.NBLOCK1_START = poParam.NBLOCK1_START;
            loDbParams.NBLOCK2_START = poParam.NBLOCK2_START;

            // loDbParams.EUTYLITY_TYPE = (EPMT03500UtilityUsageTypeDb)poParam.EUTILITY_TYPE;
            //
            // if (poParam.EUTILITY_TYPE == EPMT03500UtilityUsageType.EC)
            // {
            //     loDbParams.IBLOCK1_START = poParam.IBLOCK1_START;
            //     loDbParams.IBLOCK2_START = poParam.IBLOCK2_START;
            // }
            // else if (poParam.EUTILITY_TYPE == EPMT03500UtilityUsageType.WG)
            // {
            //     loDbParams.IMETER_START = poParam.IMETER_START;
            // }

            _logger.LogInfo("Update Meter No");
            loCls.PMT03500UpdateMeterNo(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Update Meter No");
        return loReturn;
    }

    [HttpPost]
    public PMT03500UtilityMeterDetailDTO PMT03500ChangeMeterNo(PMT03500UpdateChangeMeterNoParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT03500ChangeMeterNo));
        _logger.LogInfo("Start - Change Meter No");
        var loEx = new R_Exception();
        var loCls = new PMT03500UpdateMeterCls();
        var loDbParams = new PMT03500ParameterDb();
        var loReturn = new PMT03500UtilityMeterDetailDTO();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            
            loDbParams.CPROPERTY_ID = poParam.CPROPERTY_ID;
            loDbParams.CDEPT_CODE = poParam.CDEPT_CODE;
            loDbParams.CTRANS_CODE = poParam.CTRANS_CODE;
            loDbParams.CREF_NO = poParam.CREF_NO;
            loDbParams.CUNIT_ID = poParam.CUNIT_ID;
            loDbParams.CFLOOR_ID = poParam.CFLOOR_ID;
            loDbParams.CBUILDING_ID = poParam.CBUILDING_ID;
            loDbParams.CTENANT_ID = poParam.CTENANT_ID;
            loDbParams.CCHARGES_TYPE = poParam.CCHARGES_TYPE;
            loDbParams.CCHARGES_ID = poParam.CCHARGES_ID;
            loDbParams.CFROM_METER_NO = poParam.CMETER_NO;
            loDbParams.NMETER_END = poParam.NMETER_END;
            loDbParams.NBLOCK1_END = poParam.NBLOCK1_END;
            loDbParams.NBLOCK2_END = poParam.NBLOCK2_END;
            loDbParams.CTO_METER_NO = poParam.CTO_METER_NO;
            loDbParams.NMETER_START = poParam.NMETER_START;
            loDbParams.NBLOCK1_START = poParam.NBLOCK1_START;
            loDbParams.NBLOCK2_START = poParam.NBLOCK2_START;
            loDbParams.CSTART_INV_PRD = poParam.CSTART_INV_PRD;
            loDbParams.CSTART_DATE = poParam.CSTART_DATE;

            _logger.LogInfo("Change Meter No");
            loCls.PMT03500ChangeMeterNo(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Change Meter No");
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