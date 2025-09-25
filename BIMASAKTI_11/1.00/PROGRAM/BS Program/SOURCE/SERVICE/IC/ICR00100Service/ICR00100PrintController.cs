using System.Collections;
using System.Diagnostics;
using System.Globalization;
using BaseHeaderReportCOMMON;
using BaseHeaderResources;
using ICR00100Back;
using ICR00100Common;
using ICR00100Common.DTOs.Print;
using ICR00100Service.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;

namespace ICR00100Service;

public class ICR00100PrintController : R_ReportControllerBase
{
    private LoggerICR00100 _logger;
    private R_ReportFastReportBackClass _ReportCls;
    private ICR00100ReportParam _Parameter;
    private readonly ActivitySource _activitySource;

    public ICR00100PrintController(ILogger<ICR00100PrintController> logger)
    {
        LoggerICR00100.R_InitializeLogger(logger);
        _logger = LoggerICR00100.R_GetInstanceLogger();
        _activitySource = ICR00100Activity.R_InitializeAndGetActivitySource(nameof(ICR00100PrintController));

        _ReportCls = new R_ReportFastReportBackClass();
        _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
        _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
        _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
    }

    private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcfiletemplate)
    {
        pcfiletemplate = Path.Combine("Reports", "ICR00100ProductCard.frx");
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
    public R_DownloadFileResultDTO ActivityReportPost(ICR00100ReportParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ActivityReportPost));
        _logger.LogInfo("Start - Post Product Card Activity");
        R_Exception loException = new();
        ICR00100ReportLogKeyDTO loCache = null;
        R_DownloadFileResultDTO loRtn = null;
        try
        {
            loRtn = new R_DownloadFileResultDTO();
            loCache = new ICR00100ReportLogKeyDTO
            {
                poParam = poParam,
                poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY),
                poGlobalVar = R_ReportGlobalVar.R_GetReportDTO()
            };


            _logger.LogInfo("Set GUID Param - Post Product Card Activity");
            R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte(loCache));
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(loException);
        }

        loException.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Post Product Card Activity");
        return loRtn;
    }

    [HttpGet, AllowAnonymous]
    public FileStreamResult ActivityReportGet(string pcGuid)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ActivityReportGet));
        _logger.LogInfo("Start - Get Product Card Activity");
        R_Exception loException = new();
        FileStreamResult loRtn = null;
        ICR00100ReportLogKeyDTO loResultGUID = null;
        try
        {
            //Get Parameter
            loResultGUID =
                R_NetCoreUtility.R_DeserializeObjectFromByte<ICR00100ReportLogKeyDTO>(
                    R_DistributedCache.Cache.Get(pcGuid));

            //Get Data and Set Log Key
            R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
            R_ReportGlobalVar.R_SetFromReportDTO(loResultGUID.poGlobalVar);

            _Parameter = loResultGUID.poParam;

            _logger.LogDebug("Report Parameter", _Parameter);

            // loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
            if (_Parameter.LIS_PRINT)
            {
                _logger.LogInfo("Print Product Card Activity");
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF),
                    R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            else
            {
                _logger.LogInfo("Save As Product Card Activity");
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
        _logger.LogInfo("End - Get Product Card Activity");
        return loRtn;
    }

    private ICR00100ReportWithBaseHeaderDTO GeneratePrint(ICR00100ReportParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GeneratePrint));
        var loEx = new R_Exception();
        var loRtn = new ICR00100ReportWithBaseHeaderDTO();
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

            var loLabelObject = new ICR00100ReportLabelDTO();
            var loLabel = AssignValuesWithMessages(typeof(ICR00100BackResources.Resources_Dummy_Class),
                loCultureInfo, loLabelObject);

            _logger.LogInfo("Set Base Header Data");

            var lcCompany = R_BackGlobalVar.COMPANY_ID;
            var lcUser = R_BackGlobalVar.USER_ID;
            var lcLang = R_BackGlobalVar.CULTURE;

            var loCls = new ICR00100Cls();
            var loHeader = loCls.GetBaseHeaderLogoCompany(lcCompany);
            loRtn.BaseHeaderData = new BaseHeaderDTO
            {
                BLOGO_COMPANY = loHeader.BLOGO,
                CCOMPANY_NAME = loHeader.CCOMPANY_NAME ?? string.Empty,
                DPRINT_DATE_COMPANY = DateTime.ParseExact(loHeader.CDATETIME_NOW, "yyyyMMdd HH:mm:ss",
                    CultureInfo.InvariantCulture),
                CPRINT_CODE = "ICR00100",
                CPRINT_NAME = "Product Card",
                CUSER_ID = lcUser,
            };

            var loData = new ICR00100ReportResultDTO()
            {
                Title = "Product Card Activity",
                Label = (ICR00100ReportLabelDTO)loLabel,
                Header = null,
                Data = new List<ICR00100DataResultDTO>(),
            };

            _logger.LogInfo("Set Parameter");
            var loDbParam = new ICR00100ParameterDb
            {
                CCOMPANY_ID = lcCompany,
                CPROPERTY_ID = poParam.CPROPERTY_ID,
                CDATE_FILTER = poParam.CDATE_FILTER,
                CPERIOD = poParam.CPERIOD,
                CFROM_DATE = poParam.CFROM_DATE,
                CTO_DATE = poParam.CTO_DATE,
                CWAREHOUSE_CODE = poParam.CWAREHOUSE_CODE,
                CDEPT_CODE = poParam.CDEPT_CODE,
                LINC_FUTURE_TRANSACTION = poParam.LINC_FUTURE_TRANSACTION,
                CFILTER_BY = poParam.CFILTER_BY,
                CFROM_PROD_ID = poParam.CFROM_PROD_ID,
                CTO_PROD_ID = poParam.CTO_PROD_ID,
                CFILTER_DATA = poParam.CFILTER_DATA,
                COPTION_PRINT = poParam.COPTION_PRINT,
                CLANG_ID = lcLang,
            };

            _logger.LogInfo("Get StockTakeActivity Report");

            loData.Data = loCls.GetReportData(loDbParam);

            foreach (var item in loData.Data)
            {
                item.DREF_DATE = DateTime.TryParseExact(item.CREF_DATE, "yyyyMMdd",
                    CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var refDate)
                    ? refDate
                    : (DateTime?)null;
                item.DDOC_DATE = DateTime.TryParseExact(item.CDOC_DATE, "yyyyMMdd",
                    CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var docDate)
                    ? docDate
                    : (DateTime?)null;
            }
            
            // var FromDate = DateTime.TryParseExact(poParam.CFROM_DATE, "yyyyMMdd",
            //     CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var fromDate) 
            //     ? fromDate
            //     : (DateTime?)null;
            
            poParam.DFROM_DATE = DateTime.TryParseExact(poParam.CFROM_DATE, "yyyyMMdd",
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var fromDate)
                ? fromDate
                : (DateTime?)null;
            poParam.DTO_DATE = DateTime.TryParseExact(poParam.CTO_DATE, "yyyyMMdd",
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var toDate)
                ? toDate
                : (DateTime?)null;

            loData.Header = new ICR00100ReportHeaderDTO
            {
                CPROPERTY = poParam.CPROPERTY_ID + " - " + poParam.CPROPERTY_NAME,
                CDEPARTMENT = !string.IsNullOrEmpty(poParam.CDEPT_CODE) ? poParam.CDEPT_CODE + " - " + poParam.CDEPT_NAME : "All",
                CWAREHOUSE = poParam.CWAREHOUSE_CODE + " - " + poParam.CWAREHOUSE_NAME,
                CDATE_FILTER = poParam.CDATE_FILTER,
                CPERIOD = poParam.IPERIOD_YEAR + " - " + poParam.CPERIOD_MONTH,
                CDATE = poParam.CDATE_FILTER == "Date" ? poParam.DFROM_DATE + " To " + poParam.DTO_DATE : "",
                DDATE_FROM = poParam.DFROM_DATE,
                DDATE_TO = poParam.DTO_DATE,
                LINC_FUTURE_TRANSACTION = poParam.LINC_FUTURE_TRANSACTION,
                CFILTER_BY = poParam.CFILTER_BY,
                CFILTER_BY_DESC = poParam.CFILTER_BY_DESC,
                CFILTER_DATA = poParam.CFILTER_BY == "PROD"
                    ? poParam.CFROM_PROD_ID + (!string.IsNullOrEmpty(poParam.CTO_PROD_ID) ? " To " + poParam.CTO_PROD_ID : "")
                    : poParam.CFILTER_DATA + (!string.IsNullOrEmpty(poParam.CFILTER_DATA_NAME) ? " - " + poParam.CFILTER_DATA_NAME : ""),
                COPTION_PRINT = poParam.COPTION_PRINT,
                COPTION_PRINT_DESC = poParam.COPTION_PRINT_DESC
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