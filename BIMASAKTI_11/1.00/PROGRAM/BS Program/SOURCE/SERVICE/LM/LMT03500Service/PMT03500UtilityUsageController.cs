using System.Diagnostics;
using System.Reflection;
using PMT03500Common;
using PMT03500Common.DTOs;
using PMT03500Common.Params;
using PMT03500Back;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace PMT03500Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class PMT03500UtilityUsageController : ControllerBase, IPMT03500UtilityUsage
{
    private LoggerPMT03500 _logger;
    private readonly ActivitySource _activitySource;

    public PMT03500UtilityUsageController(ILogger<PMT03500UtilityUsageController> logger)
    {
        //Initial and Get Logger
        LoggerPMT03500.R_InitializeLogger(logger);
        _logger = LoggerPMT03500.R_GetInstanceLogger();
        _activitySource = PMT03500Activity.R_InitializeAndGetActivitySource(nameof(PMT03500UtilityUsageController));
    }

    
    [HttpPost]
    public PMT03500SingleDTO<PMT03500SystemParamDTO> PMT03500GetSystemParam(PMT03500SystemParamParameter poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT03500GetSystemParam));
        
        _logger.LogInfo("Start - Get SystemParam");
        var loEx = new R_Exception();
        var loCls = new PMT03500UtilityUsageCls();
        var loDbParams = new PMT03500ParameterDb();
        var loReturn = new PMT03500SingleDTO<PMT03500SystemParamDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CTRANS_CODE = "";
            loDbParams.CPROPERTY_ID = poParam.CPROPERTY_ID;
            
            _logger.LogInfo("Get SystemParam");
            loReturn.Data = loCls.GetSystemParam(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get SystemParam");
        return loReturn;
    }
    
    [HttpPost]
    public IAsyncEnumerable<PMT03500BuildingDTO> PMT03500GetBuildingListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT03500GetBuildingListStream));
        _logger.LogInfo("Start - Get Building List Stream");
        var loEx = new R_Exception();
        var loCls = new PMT03500UtilityUsageCls();
        var loDbParams = new PMT03500ParameterDb();
        List<PMT03500BuildingDTO> loResult = null;
        IAsyncEnumerable<PMT03500BuildingDTO> loReturn = null;


        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CPROPERTY_ID);

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
    public PMT03500SingleDTO<PMT03500BuildingDTO> PMT03500GetBuildingRecord(PMT03500SearchTextDTO poText)
    {
        using Activity activity = _activitySource.StartActivity(nameof(PMT03500GetBuildingRecord));
        _logger.LogInfo("Start Start - Lookup Building Record");
        
        var loEx = new R_Exception();
        PMT03500SingleDTO<PMT03500BuildingDTO> loReturn = new();
        var loCls = new PMT03500UtilityUsageCls();
        var loDbPar = new PMT03500ParameterDb();
        
        try
        {
            _logger.LogInfo("Set Parameter");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CPROPERTY_ID = poText.CPROPERTY_ID;
            
            _logger.LogInfo("Call Back Method - Lookup Building Record");
            var loResult = loCls.GetBuildingList(loDbPar);

            _logger.LogInfo("Filter Search by text - Lookup Building Record");
            loReturn.Data = loResult.Find(x => x.CBUILDING_ID.Trim() == poText.CSEARCH_TEXT);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        _logger.LogInfo("End Lookup Building Record");
        return loReturn;
    }

    [HttpPost]
    public IAsyncEnumerable<PMT03500UtilityUsageDTO> PMT03500GetUtilityUsageListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT03500GetUtilityUsageListStream));
        _logger.LogInfo("Start - Get Utility Usage List Stream");
        var loEx = new R_Exception();
        var loCls = new PMT03500UtilityUsageCls();
        var loDbParams = new PMT03500ParameterDb();
        List<PMT03500UtilityUsageDTO> loResult = null;
        IAsyncEnumerable<PMT03500UtilityUsageDTO> loReturn = null;


        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CPROPERTY_ID);
            loDbParams.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CBUILDING_ID);
            loDbParams.CUTILITY_TYPE = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CUTILITY_TYPE);
            loDbParams.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CFLOOR_ID);
            // loDbParams.LALL_FLOOR = R_Utility.R_GetStreamingContext<bool>(PMT03500ContextConstant.LALL_FLOOR);
            loDbParams.CINVOICE_PRD = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CINVOICE_PRD);
            loDbParams.LINVOICED = R_Utility.R_GetStreamingContext<bool>(PMT03500ContextConstant.LINVOICED);
            loDbParams.CUTILITY_PRD = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CUTILITY_PRD);
            loDbParams.CUTILITY_PRD_FROM_DATE =
                R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CUTILITY_PRD_FROM_DATE);
            loDbParams.CUTILITY_PRD_TO_DATE = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CUTILITY_PRD_TO_DATE);
            loDbParams.LOTHER_UNIT = R_Utility.R_GetStreamingContext<bool>(PMT03500ContextConstant.LOTHER_UNIT);
            // var leType = (EPMT03500UtilityUsageTypeDb)peType;

            _logger.LogInfo("Get Utility Usage List Stream");
            loResult = loCls.GetUtilityUsageList(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Utility Usage List Stream");
        return loReturn;
    }
    
    [HttpPost]
    public IAsyncEnumerable<PMT03500UtilityUsageDTO> PMT03500GetUtilityCutOffListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT03500GetUtilityCutOffListStream));
        _logger.LogInfo("Start - Get Utility CutOff List Stream");
        var loEx = new R_Exception();
        var loCls = new PMT03500UtilityUsageCls();
        var loDbParams = new PMT03500ParameterDb();
        List<PMT03500UtilityUsageDTO> loResult = null;
        IAsyncEnumerable<PMT03500UtilityUsageDTO> loReturn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CPROPERTY_ID);
            loDbParams.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CBUILDING_ID);
            loDbParams.CUTILITY_TYPE = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CUTILITY_TYPE);
            loDbParams.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CFLOOR_ID);
            loDbParams.CUTILITY_PRD = R_Utility.R_GetStreamingContext<string>(PMT03500ContextConstant.CUTILITY_PRD);
            loDbParams.LOTHER_UNIT = R_Utility.R_GetStreamingContext<bool>(PMT03500ContextConstant.LOTHER_UNIT);
            

            _logger.LogInfo("Get Utility CutOff List Stream");
            loResult = loCls.GetUtilityCutOffList(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Utility CutOff List Stream");
        return loReturn;
    }
    
    [HttpPost]
    public PMT03500SingleDTO<PMT03500UtilityUsageDetailDTO> PMT03500GetUtilityUsageDetail(PMT03500UtilityUsageDetailParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT03500GetUtilityUsageDetail));
        _logger.LogInfo("Start - Get UtilityUsageDetail List");

        var loEx = new R_Exception();
        var loCls = new PMT03500UtilityUsageCls();
        var loDbParams = new PMT03500ParameterDb();
        var loReturn = new PMT03500SingleDTO<PMT03500UtilityUsageDetailDTO>();

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
            loDbParams.CCHARGES_TYPE = poParam.CCHARGES_TYPE;
            loDbParams.CCHARGES_ID = poParam.CCHARGES_ID;
            loDbParams.CSEQ_NO = poParam.CSEQ_NO;
            loDbParams.CINV_PRD = poParam.CINV_PRD;
            // loDbParams.CSTART_PHOTO1_STORAGE_ID = poParam.CSTART_PHOTO1_STORAGE_ID;
            // loDbParams.CSTART_PHOTO2_STORAGE_ID = poParam.CSTART_PHOTO2_STORAGE_ID;
            // loDbParams.CSTART_PHOTO3_STORAGE_ID = poParam.CSTART_PHOTO3_STORAGE_ID;
            // loDbParams.CEND_PHOTO1_STORAGE_ID = poParam.CEND_PHOTO1_STORAGE_ID;
            // loDbParams.CEND_PHOTO2_STORAGE_ID = poParam.CEND_PHOTO2_STORAGE_ID;
            // loDbParams.CEND_PHOTO3_STORAGE_ID = poParam.CEND_PHOTO3_STORAGE_ID;

            _logger.LogInfo("Get UtilityUsageDetail List");
            loReturn.Data = loCls.GetUtilityUsageDetail(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
    
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get UtilityUsageDetail List");
        return loReturn;
        
    }
    
    [HttpPost]
    public PMT03500SingleDTO<PMT03500UtilityUsageDetailDTO> PMT03500GetUtilityUsageDetailPhoto(PMT03500UtilityUsageDetailParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT03500GetUtilityUsageDetailPhoto));
        _logger.LogInfo("Start - Get UtilityUsageDetailPhoto List");

        var loEx = new R_Exception();
        var loCls = new PMT03500UtilityUsageCls();
        var loDbParams = new PMT03500ParameterDb();
        var loReturn = new PMT03500SingleDTO<PMT03500UtilityUsageDetailDTO>();

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
            loDbParams.CCHARGES_TYPE = poParam.CCHARGES_TYPE;
            loDbParams.CCHARGES_ID = poParam.CCHARGES_ID;
            loDbParams.CSEQ_NO = poParam.CSEQ_NO;
            loDbParams.CINV_PRD = poParam.CINV_PRD;
            // loDbParams.CSTART_PHOTO1_STORAGE_ID = poParam.CSTART_PHOTO1_STORAGE_ID;
            // loDbParams.CSTART_PHOTO2_STORAGE_ID = poParam.CSTART_PHOTO2_STORAGE_ID;
            // loDbParams.CSTART_PHOTO3_STORAGE_ID = poParam.CSTART_PHOTO3_STORAGE_ID;
            // loDbParams.CEND_PHOTO1_STORAGE_ID = poParam.CEND_PHOTO1_STORAGE_ID;
            // loDbParams.CEND_PHOTO2_STORAGE_ID = poParam.CEND_PHOTO2_STORAGE_ID;
            // loDbParams.CEND_PHOTO3_STORAGE_ID = poParam.CEND_PHOTO3_STORAGE_ID;

            _logger.LogInfo("Get UtilityUsageDetailPhoto List");
            loReturn.Data = loCls.GetUtilityUsageDetailPhoto(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
    
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get UtilityUsageDetailPhoto List");
        return loReturn;
        
    }

    [HttpPost]
    public PMT03500ListDTO<PMT03500FunctDTO> PMT03500GetUtilityTypeList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT03500GetUtilityTypeList));
        _logger.LogInfo("Start - Get Utility Type List");

        var loEx = new R_Exception();
        var loCls = new PMT03500UtilityUsageCls();
        var loDbParams = new PMT03500ParameterDb();
        var loReturn = new PMT03500ListDTO<PMT03500FunctDTO>();

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
    public PMT03500ListDTO<PMT03500FloorDTO> PMT03500GetFloorList(PMT03500FloorParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT03500GetFloorList));
        _logger.LogInfo("Start - Get Floor List");

        var loEx = new R_Exception();
        var loCls = new PMT03500UtilityUsageCls();
        var loDbParams = new PMT03500ParameterDb();
        var loReturn = new PMT03500ListDTO<PMT03500FloorDTO>();

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
    public PMT03500ListDTO<PMT03500YearDTO> PMT03500GetYearList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT03500GetYearList));
        _logger.LogInfo("Start - Get Period List");

        var loEx = new R_Exception();
        var loCls = new PMT03500UtilityUsageCls();
        var loDbParams = new PMT03500ParameterDb();
        var loReturn = new PMT03500ListDTO<PMT03500YearDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

            _logger.LogInfo("Get Period List");
            loReturn.Data = loCls.GetYearList(loDbParams);
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

    [HttpPost]
    public PMT03500ListDTO<PMT03500PeriodDTO> PMT03500GetPeriodList(PMT03500PeriodParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT03500GetPeriodList));
        _logger.LogInfo("Start - Get Period List");

        var loEx = new R_Exception();
        var loCls = new PMT03500UtilityUsageCls();
        var loDbParams = new PMT03500ParameterDb();
        var loReturn = new PMT03500ListDTO<PMT03500PeriodDTO>();

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
    
    [HttpPost]
    public PMT03500SingleDTO<PMT03500PeriodDTO> PMT03500GetPeriod(PMT03500PeriodParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT03500GetPeriod));
        _logger.LogInfo("Start - Get Period Detail");

        var loEx = new R_Exception();
        var loCls = new PMT03500UtilityUsageCls();
        var loDbParams = new PMT03500ParameterDb();
        var loReturn = new PMT03500SingleDTO<PMT03500PeriodDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CYEAR = poParam.CYEAR;
            loDbParams.CPERIOD_NO = poParam.CPERIOD_NO;

            _logger.LogInfo("Get Period Detail");
            loReturn.Data = loCls.GetPeriod(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Period Detail");
        return loReturn;
    }
    
    [HttpPost]
    public PMT03500ListDTO<PMT03500RateWGListDTO> PMT03500GetRateWGList(PMT03500RateParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT03500GetPeriodList));
        _logger.LogInfo("Start - Get Rate WG List");

        var loEx = new R_Exception();
        var loCls = new PMT03500UtilityUsageCls();
        var loDbParams = new PMT03500ParameterDb();
        var loReturn = new PMT03500ListDTO<PMT03500RateWGListDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            
            loDbParams.CPROPERTY_ID = poParam.CPROPERTY_ID;
            loDbParams.CCHARGES_TYPE = poParam.CCHARGE_TYPE_ID;
            loDbParams.CCHARGES_ID = poParam.CCHARGES_ID;

            _logger.LogInfo("Get Rate WG List");
            loReturn.Data = loCls.GetRateWGList(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Rate WG List");
        return loReturn;
    }
    
    [HttpPost]
    public PMT03500ExcelDTO PMT03500DownloadTemplateFile()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT03500DownloadTemplateFile));
        _logger.LogInfo("Start - Download Template File");
        var loEx = new R_Exception();
        var loRtn = new PMT03500ExcelDTO();

        try
        {
            _logger.LogInfo("Get Template File");
            var loAsm = Assembly.Load("BIMASAKTI_PM_API");
            
            _logger.LogInfo("Set Resource File");
            var lcResourceFile = "BIMASAKTI_PM_API.Template.UtilityUsage.xlsx";

            _logger.LogInfo("Get Resource File");
            using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
            {
                var ms = new MemoryStream();
                resFilestream.CopyTo(ms);
                var bytes = ms.ToArray();

                loRtn.FileBytes = bytes;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Download Template File");
        return loRtn;
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