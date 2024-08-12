using System.Collections;
using System.Diagnostics;
using System.Globalization;
using BaseHeaderReportCOMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR02600Back;
using PMR02600Common;
using PMR02600Common.DTOs.Print;
using PMR02600Common.Params;
using PMR02600Service.DTOs;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;

namespace PMR02600Service;

public class PMR02600PrintController : R_ReportControllerBase
{
    private LoggerPMR02600 _logger;
    private R_ReportFastReportBackClass _ReportCls;
    private PMR02600ReportParam _Parameter;
    private readonly ActivitySource _activitySource;

    public PMR02600PrintController(ILogger<PMR02600PrintController> logger)
    {
        LoggerPMR02600.R_InitializeLogger(logger);
        _logger = LoggerPMR02600.R_GetInstanceLogger();
        _activitySource = PMR02600Activity.R_InitializeAndGetActivitySource(nameof(PMR02600PrintController));

        _ReportCls = new R_ReportFastReportBackClass();
        _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
        _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
    }

    private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcfiletemplate)
    {
        pcfiletemplate = Path.Combine("Reports", "PMR02600Occupancy.frx");
    }

    private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
    {
        poData.Add(GeneratePrint(_Parameter));
        pcDataSourceName = "ResponseDataModel";
    }


    [HttpPost]
    public R_DownloadFileResultDTO ActivityReportPost(PMR02600ReportParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ActivityReportPost));
        _logger.LogInfo("Start - Post Occupancy Report");
        R_Exception loException = new();
        PMR02600ReportLogKeyDTO loCache = null;
        R_DownloadFileResultDTO loRtn = null;
        try
        {
            loRtn = new R_DownloadFileResultDTO();
            loCache = new PMR02600ReportLogKeyDTO
            {
                poParam = poParam,
                poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY)
            };


            _logger.LogInfo("Set GUID Param - Post Occupancy Report");
            R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte(loCache));
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(loException);
        }

        loException.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Post Occupancy Report");
        return loRtn;
    }

    [HttpGet, AllowAnonymous]
    public FileStreamResult ActivityReportGet(string pcGuid)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ActivityReportGet));
        _logger.LogInfo("Start - Get Occupancy Report");
        R_Exception loException = new();
        FileStreamResult loRtn = null;
        PMR02600ReportLogKeyDTO loResultGUID = null;
        try
        {
            //Get Parameter
            loResultGUID =
                R_NetCoreUtility.R_DeserializeObjectFromByte<PMR02600ReportLogKeyDTO>(
                    R_DistributedCache.Cache.Get(pcGuid));

            //Get Data and Set Log Key
            R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);

            _Parameter = loResultGUID.poParam;
            loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(loException);
        }

        loException.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Occupancy Report");
        return loRtn;
    }

    private PMR02600ReportWithBaseHeaderDTO GeneratePrint(PMR02600ReportParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GeneratePrint));
        var loEx = new R_Exception();
        var loRtn = new PMR02600ReportWithBaseHeaderDTO();
        var loCultureInfo = new CultureInfo(poParam.CREPORT_CULTURE);

        try
        {
            loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),
                "Page", loCultureInfo);
            loRtn.BaseHeaderColumn.Of =
                R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
            loRtn.BaseHeaderColumn.Print_Date =
                R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
            loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),
                "Print_By", loCultureInfo);

            var loLabelObject = new PMR02600ReportLabelDTO();
            var loLabel = AssignValuesWithMessages(typeof(PMR02600BackResources.Resources_Dummy_Class),
                loCultureInfo, loLabelObject);

            _logger.LogInfo("Set Base Header Data");

            var loCls = new PMR02600Cls();
            var loLogo = loCls.GetBaseHeaderLogoCompany(poParam.CCOMPANY_ID);
            loRtn.BaseHeaderData = new BaseHeaderDTO
            {
                BLOGO_COMPANY = loLogo.BLOGO,
                CCOMPANY_NAME = "PT Realta Chakradarma",
                CPRINT_CODE = "PMR02600",
                CPRINT_NAME = "Occupancy Report",
                CUSER_ID = poParam.CUSER_ID,
            };

            var loData = new PMR02600ReportResultDTO()
            {
                Title = "Occupancy Report",
                Label = (PMR02600ReportLabelDTO)loLabel,
                Header = null,
                Data = new List<PMR02600DataResultDTO>(),
            };

            _logger.LogInfo("Set Parameter");
            var loDbParam = new PMR02600ParameterDb
            {
                CCOMPANY_ID = poParam.CCOMPANY_ID,
                CUSER_ID = poParam.CUSER_ID,
                CLANG_ID = poParam.CLANG_ID,
                CPROPERTY_ID = poParam.CPROPERTY,
                CFROM_BUILDING = poParam.CFROM_BUILDING,
                CTO_BUILDING = poParam.CTO_BUILDING,
                CCUT_OFF_DATE = poParam.CPERIOD,
            };

            _logger.LogInfo("Get Detail Activity Report");

            loData.Data = loCls.GetReportData(loDbParam);
            loData.Header = new PMR02600ReportHeaderDTO
            {
                CPROPERTY = poParam.CPROPERTY_NAME + $"({poParam.CPROPERTY})",
                CFROM_BUILDING = poParam.CFROM_BUILDING_NAME + $"({poParam.CFROM_BUILDING})",
                CTO_BUILDING = poParam.CTO_BUILDING_NAME + $"({poParam.CTO_BUILDING})",
                CPERIOD_DISPLAY = DateTime.TryParseExact(poParam.CPERIOD, "yyyyMMdd",
                    CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var refDate)
                    ? refDate.ToString("dd MMM yyyy")
                    : null
            };

            loRtn.Data = loData;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    private object AssignValuesWithMessages(Type poResourceType, CultureInfo poCultureInfo, object poObject)
    {
        object loObj = Activator.CreateInstance(poObject.GetType());
        var loGetPropertyObject = poObject.GetType().GetProperties();

        foreach (var property in loGetPropertyObject)
        {
            string propertyName = property.Name;
            string message = R_Utility.R_GetMessage(poResourceType, propertyName, poCultureInfo);
            property.SetValue(loObj, message);
        }

        return loObj;
    }
}