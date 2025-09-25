using System.Collections;
using System.Diagnostics;
using System.Globalization;
using APR00500Back;
using APR00500Common;
using APR00500Common.DTOs.Print;
using BaseHeaderReportCOMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;

namespace APR00500Service;

public class APR00500PrintController : R_ReportControllerBase
{
    private LoggerAPR00500 _logger;
    private R_ReportFastReportBackClass _ReportCls;
    private APR00500ReportParam _Parameter;
    private readonly ActivitySource _activitySource;

    public APR00500PrintController(ILogger<APR00500PrintController> logger)
    {
        LoggerAPR00500.R_InitializeLogger(logger);
        _logger = LoggerAPR00500.R_GetInstanceLogger();
        _activitySource = APR00500Activity.R_InitializeAndGetActivitySource(nameof(APR00500PrintController));

        _ReportCls = new R_ReportFastReportBackClass();
        _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
        _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
        _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
    }

    private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcfiletemplate)
    {
        pcfiletemplate = Path.Combine("Reports", "APR00500InvoiceList.frx");
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
    public R_DownloadFileResultDTO ActivityReportPost(APR00500ReportParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ActivityReportPost));
        _logger.LogInfo("Start - Post AP Invoice List");
        R_Exception loException = new();
        APR00500ReportLogKeyDTO loCache = null;
        R_DownloadFileResultDTO loRtn = null;
        try
        {
            loRtn = new R_DownloadFileResultDTO();
            loCache = new APR00500ReportLogKeyDTO
            {
                poParam = poParam,
                poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY),
                poGlobalVar = R_ReportGlobalVar.R_GetReportDTO()
            };


            _logger.LogInfo("Set GUID Param - Post AP Invoice List");
            R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte(loCache));
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(loException);
        }

        loException.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Post AP Invoice List");
        return loRtn;
    }

    [HttpGet, AllowAnonymous]
    public FileStreamResult ActivityReportGet(string pcGuid)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ActivityReportGet));
        _logger.LogInfo("Start - Get AP Invoice List");
        R_Exception loException = new();
        FileStreamResult loRtn = null;
        APR00500ReportLogKeyDTO loResultGUID = null;
        try
        {
            //Get Parameter
            loResultGUID =
                R_NetCoreUtility.R_DeserializeObjectFromByte<APR00500ReportLogKeyDTO>(
                    R_DistributedCache.Cache.Get(pcGuid));

            //Get Data and Set Log Key
            R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
            R_ReportGlobalVar.R_SetFromReportDTO(loResultGUID.poGlobalVar);

            _Parameter = loResultGUID.poParam;

            _logger.LogDebug("Report Parameter", _Parameter);

            // loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
            if (_Parameter.LIS_PRINT)
            {
                _logger.LogInfo("Print AP Invoice List");
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF),
                    R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            else
            {
                _logger.LogInfo("Save As AP Invoice List");
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
        _logger.LogInfo("End - Get AP Invoice List");
        return loRtn;
    }

    private APR00500ReportWithBaseHeaderDTO GeneratePrint(APR00500ReportParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GeneratePrint));
        var loEx = new R_Exception();
        var loRtn = new APR00500ReportWithBaseHeaderDTO();
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

            var loLabelObject = new APR00500ReportLabelDTO();
            var loLabel = AssignValuesWithMessages(typeof(APR00500BackResources.Resources_Dummy_Class),
                loCultureInfo, loLabelObject);

            _logger.LogInfo("Set Base Header Data");

            var lcCompany = R_BackGlobalVar.COMPANY_ID;
            var lcUser = R_BackGlobalVar.USER_ID;
            var lcLang = R_BackGlobalVar.CULTURE;

            var loCls = new APR00500Cls();
            var loHeader = loCls.GetBaseHeaderLogoCompany(lcCompany);
            loRtn.BaseHeaderData = new BaseHeaderDTO
            {
                BLOGO_COMPANY = loHeader.BLOGO,
                CCOMPANY_NAME = loHeader.CCOMPANY_NAME ?? string.Empty,
                DPRINT_DATE_COMPANY = DateTime.ParseExact(loHeader.CDATETIME_NOW, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture),
                CPRINT_CODE = "APR00500",
                CPRINT_NAME = "AP Invoice List",
                CUSER_ID = lcUser,
            };

            var loData = new APR00500ReportResultDTO()
            {
                Title = "AP Invoice List",
                Label = (APR00500ReportLabelDTO)loLabel,
                Header = null,
                Data = new List<APR00500DataResultDTO>(),
            };

            _logger.LogInfo("Set Parameter");
            var loDbParam = new APR00500ParameterDb
            {
                CCOMPANY_ID = lcCompany,
                CUSER_ID = lcUser,
                CPROPERTY_ID = poParam.CPROPERTY_ID,
                CPROPERTY_NAME = poParam.CPROPERTY_NAME,
                CCUT_OFF_DATE = poParam.CCUT_OFF_DATE,
                CDEPT_CODE = poParam.CDEPT_CODE,
                CFROM_PERIOD = poParam.CFROM_PERIOD,
                CTO_PERIOD = poParam.CTO_PERIOD,
                CFROM_REFERENCE_DATE = poParam.CFROM_REFERENCE_DATE,
                CTO_REFERENCE_DATE = poParam.CTO_REFERENCE_DATE,
                CFROM_DUE_DATE = poParam.CFROM_DUE_DATE,
                CTO_DUE_DATE = poParam.CTO_DUE_DATE,
                CSUPPLIER_ID = poParam.CSUPPLIER_ID,
                CFROM_REFERENCE_NO = poParam.CFROM_REFERENCE_NO,
                CTO_REFERENCE_NO = poParam.CTO_REFERENCE_NO,
                CCURRENCY = poParam.CCURRENCY,
                NFROM_TOTAL_AMOUNT = poParam.NFROM_TOTAL_AMOUNT,
                NTO_TOTAL_AMOUNT = poParam.NTO_TOTAL_AMOUNT,
                NFROM_REMAINING_AMOUNT = poParam.NFROM_REMAINING_AMOUNT,
                NTO_REMAINING_AMOUNT = poParam.NTO_REMAINING_AMOUNT,
                IFROM_DAYS_LATE = poParam.IFROM_DAYS_LATE,
                ITO_DAYS_LATE = poParam.ITO_DAYS_LATE,
                CLANG_ID = lcLang,
            };

            _logger.LogInfo("Get Detail Invoice List Report");

            loData.Data = loCls.GetReportData(loDbParam);
            loData.Header = new APR00500ReportHeaderDTO
            {
                CPROPERTY_ID = poParam.CPROPERTY_ID,
                CPROPERTY_NAME = poParam.CPROPERTY_NAME,
                DCUT_OFF_DATE = DateTime.TryParseExact(poParam.CCUT_OFF_DATE, "yyyyMMdd",
                    CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var cutOfDate)
                    ? cutOfDate
                    : (DateTime?)null
            };

            foreach (var item in loData.Data)
            {
                item.DREFERENCE_DATE = DateTime.TryParseExact(item.CREFERENCE_DATE, "yyyyMMdd",
                    CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var refDate)
                    ? refDate
                    : (DateTime?)null;
                item.DDUE_DATE = DateTime.TryParseExact(item.CDUE_DATE, "yyyyMMdd",
                    CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var dueDate)
                    ? dueDate
                    : (DateTime?)null;
                if (item.CTRANS_CODE is "120010" or "110040")
                {
                    item.NTOTAL_AMOUNT = -item.NTOTAL_AMOUNT;
                    item.NINVOICE_AMOUNT = -item.NINVOICE_AMOUNT;
                    item.NREMAINING = -item.NREMAINING;
                }
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