using System.Collections;
using System.Diagnostics;
using System.Globalization;
using BaseHeaderReportCOMMON;
using ICR00600Back;
using ICR00600BackResources;
using ICR00600Common;
using ICR00600Common.DTOs.Print;
using ICR00600Service.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;

namespace ICR00600Service;

public class ICR00600PrintController: R_ReportControllerBase
{
    private LoggerICR00600 _logger;
    private R_ReportFastReportBackClass _ReportCls;
    private ICR00600ReportParam _Parameter;
    private readonly ActivitySource _activitySource;

    public ICR00600PrintController(ILogger<ICR00600PrintController> logger)
    {
        LoggerICR00600.R_InitializeLogger(logger);
        _logger = LoggerICR00600.R_GetInstanceLogger();
        _activitySource = ICR00600Activity.R_InitializeAndGetActivitySource(nameof(ICR00600PrintController));

        _ReportCls = new R_ReportFastReportBackClass();
        _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
        _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
        _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
    }

    private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcfiletemplate)
    {
        pcfiletemplate = Path.Combine("Reports", "ICR00600StockTakeActivity.frx");
    }

    private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
    {
        poData.Add(GeneratePrint(_Parameter));
        pcDataSourceName = "ResponseDataModel";
    }

    private void _ReportCls_R_SetNumberAndDateFormat(ref R_ReportFormatDTO poReportFormat)
    {
        poReportFormat.DecimalSeparator = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_SEPARATOR;
        poReportFormat.GroupSeparator = R_BackGlobalVar.REPORT_FORMAT_GROUP_SEPARATOR;
        poReportFormat.DecimalPlaces = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_PLACES;
        poReportFormat.ShortDate = R_BackGlobalVar.REPORT_FORMAT_SHORT_DATE;
        poReportFormat.ShortTime = R_BackGlobalVar.REPORT_FORMAT_SHORT_TIME;
    }

    [HttpPost]
    public R_DownloadFileResultDTO StockTakeActivityReportPost(ICR00600ReportParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(StockTakeActivityReportPost));
        _logger.LogInfo("Start - Post Stock Take Activity");
        R_Exception loException = new();
        ICR00600ReportLogKeyDTO loCache = null;
        R_DownloadFileResultDTO loRtn = null;
        try
        {
            loRtn = new R_DownloadFileResultDTO();
            loCache = new ICR00600ReportLogKeyDTO
            {
                poParam = poParam,
                poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY),
                poGlobalVar = R_ReportGlobalVar.R_GetReportDTO()
            };


            _logger.LogInfo("Set GUID Param - Post Stock Take Activity");
            R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte(loCache));
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(loException);
        }

        loException.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Post Stock Take Activity");
        return loRtn;
    }

    [HttpGet, AllowAnonymous]
    public FileStreamResult StockTakeActivityReportGet(string pcGuid)
    {
        using var loActivity = _activitySource.StartActivity(nameof(StockTakeActivityReportGet));
        _logger.LogInfo("Start - Get Stock Take Activity");
        R_Exception loException = new();
        FileStreamResult loRtn = null;
        ICR00600ReportLogKeyDTO loResultGUID = null;
        try
        {
            //Get Parameter
            loResultGUID =
                R_NetCoreUtility.R_DeserializeObjectFromByte<ICR00600ReportLogKeyDTO>(
                    R_DistributedCache.Cache.Get(pcGuid));

            //Get Data and Set Log Key
            R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
            R_ReportGlobalVar.R_SetFromReportDTO(loResultGUID.poGlobalVar);

            _Parameter = loResultGUID.poParam;

            _logger.LogDebug("Report Parameter", _Parameter);

            // loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
            if (_Parameter.LIS_PRINT)
            {
                _logger.LogInfo("Print Stock Take Activity");
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF),
                    R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            else
            {
                _logger.LogInfo("Save As Stock Take Activity");
                var loFileType = (R_FileType)Enum.Parse(typeof(R_FileType), _Parameter.CREPORT_FILETYPE);
                loRtn = File(_ReportCls.R_GetStreamReport(peExport: loFileType),
                    R_ReportUtility.GetMimeType(loFileType),
                    $"{_Parameter.CREPORT_FILENAME}.{_Parameter.CREPORT_FILETYPE}");
            }

            _logger.LogInfo("Finish get file");
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(loException);
        }

        loException.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Stock Take Activity");
        return loRtn;
    }

    private ICR00600ReportWithBaseHeaderDTO GeneratePrint(ICR00600ReportParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GeneratePrint));
        var loEx = new R_Exception();
        var loRtn = new ICR00600ReportWithBaseHeaderDTO();
        var loCultureInfo = new CultureInfo(R_BackGlobalVar.REPORT_CULTURE);

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

            var loLabelObject = new ICR00600ReportLabelDTO();
            var loLabel = AssignValuesWithMessages(typeof(ICR00600BackResources.Resources_Dummy_Class),
                loCultureInfo, loLabelObject);

            _logger.LogInfo("Set Base Header Data");

            var lcCompany = R_BackGlobalVar.COMPANY_ID;
            var lcUser = R_BackGlobalVar.USER_ID;
            var lcLang = R_BackGlobalVar.CULTURE;

            var loCls = new ICR00600Cls();
            var loHeader = loCls.GetBaseHeaderLogoCompany(lcCompany);
            loRtn.BaseHeaderData = new BaseHeaderDTO
            {
                BLOGO_COMPANY = loHeader.BLOGO,
                CCOMPANY_NAME = loHeader.CCOMPANY_NAME ?? string.Empty,
                DPRINT_DATE_COMPANY = DateTime.ParseExact(loHeader.CDATETIME_NOW, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture),
                CPRINT_CODE = "ICR00600",
                CPRINT_NAME = "Stock Take Activity",
                CUSER_ID = lcUser,
            };

            var loData = new ICR00600ReportResultDTO()
            {
                Title = "Stock Take Activity",
                Label = (ICR00600ReportLabelDTO)loLabel,
                Header = null,
                Data = new List<ICR00600DataResultDTO>(),
            };

            _logger.LogInfo("Set Parameter");
            var loDbParam = new ICR00600ParameterDb
            {
                CCOMPANY_ID = lcCompany,
                CPROPERTY_ID = poParam.CPROPERTY_ID,
                CDEPT_CODE = poParam.CDEPT_CODE,
                CPERIOD = string.IsNullOrEmpty(poParam.CPERIOD) ? "" : poParam.IPERIOD_YEAR + poParam.CPERIOD_MONTH,
                COPTION_PRINT = poParam.COPTION_PRINT,
                CFROM_REF_NO = poParam.CFROM_REF_NO,
                CTO_REF_NO = poParam.CTO_REF_NO,
                CLANG_ID = lcLang,
                
            };

            _logger.LogInfo("Get StockTakeActivity Report");

            loData.Data = loCls.GetReportData(loDbParam);
            loData.Header = new ICR00600ReportHeaderDTO
            {
                CPROPERTY = poParam.CPROPERTY_ID + " (" + poParam.CPROPERTY_NAME + ")",
                CDEPARTMENT = !poParam.LDEPT ? R_Utility.R_GetMessage(typeof(Resources_Dummy_Class), "ALL", loCultureInfo)
                : poParam.CDEPT_CODE + " (" + poParam.CDEPT_NAME + ")",
                CPERIOD = poParam.IPERIOD_YEAR + " " + poParam.CPERIOD_MONTH,
                COPTION_PRINT = poParam.COPTION_PRINT,
                COPTION_PRINT_NAME = poParam.COPTION_PRINT_NAME,
            };

            foreach (var item in loData.Data)
            {
                item.DREF_DATE = DateTime.TryParseExact(item.CREF_DATE, "yyyyMMdd",
                    CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var refDate)
                    ? refDate
                    : (DateTime?)null;
            }

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
        var loObj = Activator.CreateInstance(poObject.GetType());
        var loGetPropertyObject = poObject.GetType().GetProperties();

        foreach (var property in loGetPropertyObject)
        {
            var propertyName = property.Name;
            var message = R_Utility.R_GetMessage(poResourceType, propertyName, poCultureInfo);
            property.SetValue(loObj, message);
        }

        return loObj;
    }
}