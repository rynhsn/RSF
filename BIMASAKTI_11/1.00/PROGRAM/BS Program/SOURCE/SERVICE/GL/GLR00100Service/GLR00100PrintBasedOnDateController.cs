using System.Collections;
using System.Diagnostics;
using System.Globalization;
using BaseHeaderReportCOMMON;
using GLR00100Back;
using GLR00100Common;
using GLR00100Common.DTOs;
using GLR00100Common.DTOs.Print;
using GLR00100Common.Params;
using GLR00100Service.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;

namespace GLR00100Service;

public class GLR00100PrintBasedOnDateController : R_ReportControllerBase
{
    private LoggerGLR00100 _logger;
    private R_ReportFastReportBackClass _ReportCls;
    private GLR00100ReportParam _Parameter;
    private readonly ActivitySource _activitySource;

    public GLR00100PrintBasedOnDateController(ILogger<GLR00100PrintBasedOnDateController> logger)
    {
        LoggerGLR00100.R_InitializeLogger(logger);
        _logger = LoggerGLR00100.R_GetInstanceLogger();
        _activitySource = GLR00100Activity.R_InitializeAndGetActivitySource(nameof(GLR00100PrintBasedOnDateController));

        _ReportCls = new R_ReportFastReportBackClass();
        _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
        _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
        _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
    }

    private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcfiletemplate)
    {
        pcfiletemplate = Path.Combine("Reports", "GLR00100ActivityReportBasedOnDate.frx");
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
    public R_DownloadFileResultDTO ActivityReportBasedOnDatePost(GLR00100ReportParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ActivityReportBasedOnDatePost));
        _logger.LogInfo("Start - Post Account Status");
        R_Exception loException = new();
        GLR00100ReportLogKeyDTO loCache = null;
        R_DownloadFileResultDTO loRtn = null;
        try
        {
            loRtn = new R_DownloadFileResultDTO();
            loCache = new GLR00100ReportLogKeyDTO
            {
                poParam = poParam,
                poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY),
                poGlobalVar = R_ReportGlobalVar.R_GetReportDTO()
            };


            _logger.LogInfo("Set GUID Param - Post Account Status");
            R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte(loCache));
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(loException);
        }

        loException.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Post Account Status");
        return loRtn;
    }

    [HttpGet, AllowAnonymous]
    public FileStreamResult ActivityReportBasedOnDateGet(string pcGuid)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ActivityReportBasedOnDateGet));
        _logger.LogInfo("Start - Get Account Status");
        R_Exception loException = new();
        FileStreamResult loRtn = null;
        GLR00100ReportLogKeyDTO loResultGUID = null;
        try
        {
            //Get Parameter
            loResultGUID =
                R_NetCoreUtility.R_DeserializeObjectFromByte<GLR00100ReportLogKeyDTO>(
                    R_DistributedCache.Cache.Get(pcGuid));

            //Get Data and Set Log Key
            R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
            R_ReportGlobalVar.R_SetFromReportDTO(loResultGUID.poGlobalVar);

            _Parameter = loResultGUID.poParam;
            
            if (_Parameter.LIS_PRINT)
            {
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF),
                    R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            else
            {
                var loFileType = (R_FileType)Enum.Parse(typeof(R_FileType), _Parameter.CREPORT_FILETYPE);
                loRtn = File(_ReportCls.R_GetStreamReport(peExport: loFileType), R_ReportUtility.GetMimeType(loFileType), $"{_Parameter.CREPORT_FILENAME}.{_Parameter.CREPORT_FILETYPE}");

            }
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(loException);
        }

        loException.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Account Status");
        return loRtn;
    }

    private GLR00100ReportWithBaseHeaderDTO GeneratePrint(GLR00100ReportParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GeneratePrint));
        var loEx = new R_Exception();
        var loRtn = new GLR00100ReportWithBaseHeaderDTO();
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

            var loLabelObject = new GLR00100ReportLabelDTO();
            var loLabel = AssignValuesWithMessages(typeof(GLR00100BackResources.Resources_Dummy_Class),
                loCultureInfo, loLabelObject);

            _logger.LogInfo("Set Base Header Data");
            
            var lcCompany = R_BackGlobalVar.COMPANY_ID;
            var lcUser = R_BackGlobalVar.USER_ID;
            var lcLang = R_BackGlobalVar.CULTURE;

            var loCls = new GLR00100Cls();
            var loHeader = loCls.GetBaseHeaderLogoCompany(lcCompany);
            loRtn.BaseHeaderData = new BaseHeaderDTO
            {
                BLOGO_COMPANY = loHeader.BLOGO,
                CCOMPANY_NAME = loHeader.CCOMPANY_NAME!,
                DPRINT_DATE_COMPANY = DateTime.ParseExact(loHeader.CDATETIME_NOW, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture),
                CPRINT_CODE = "GLR00100",
                CPRINT_NAME = "Activity Report",
                CUSER_ID = lcUser,
            };

            var loData = new GLR00100ReportResultDTO()
            {
                Title = "Activity Report",
                Label = (GLR00100ReportLabelDTO)loLabel,
                Header = null,
                Data = new List<GLR00100ResultActivityReportDTO>(),
                SubData = new List<GLR00100ResultActivitySubReportDTO>()
            };

            _logger.LogInfo("Set Parameter");
            var loDbParam = new GLR00100ParameterDb
            {
                CCOMPANY_ID = lcCompany,
                CUSER_ID = lcUser,
                CLANGUAGE_ID = lcLang,
                CPERIOD_TYPE = poParam.CPERIOD_TYPE,
                CFROM_DATE = poParam.CFROM_PERIOD,
                CTO_DATE = poParam.CTO_PERIOD,
                CFROM_DEPT_CODE = poParam.CFROM_DEPT_CODE,
                CTO_DEPT_CODE = poParam.CTO_DEPT_CODE,
                CCURRENCY_TYPE = poParam.CCURRENCY_TYPE
            };

            _logger.LogInfo("Get Detail Activity Report");

            var loCollection = loCls.BasedOnDateReportDb(loDbParam);
            
            var loTotalCurr = loCollection.GroupBy(x => x.CCURRENCY_CODE).ToList();
            foreach (var itemCurr in loTotalCurr)
            {
                var loCurrency = new GLR00100TotalByCurrDTO()
                {
                    CCURRENCY_CODE = itemCurr.Key,
                    NTOTAL_CREDIT = itemCurr.Sum(x => x.NCREDIT_AMOUNT),
                    NTOTAL_DEBIT = itemCurr.Sum(x => x.NDEBIT_AMOUNT)
                };
                loData.GrandTotalByCurr.Add(loCurrency);
            }
            
            foreach (var item in loCollection)
            {
                item.DREF_DATE = DateTime.TryParseExact(item.CREF_DATE, "yyyyMMdd",
                    CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var refDate) ? refDate : (DateTime?)null;
                item.DDOC_DATE = DateTime.TryParseExact(item.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var docDate) ? docDate : (DateTime?)null;
            }
            
            //grouping untuk data berdasarkan CREF_DATE
            var loGroupDate = loCollection.GroupBy(x => x.DREF_DATE).ToList();
            foreach (var itemDate in loGroupDate)
            {
                var loDataDate = new GLR00100ReportBasedOnDateSubDTO()
                {
                    DREF_DATE = itemDate.Key,
                    Data = itemDate.ToList(),
                    NTOTAL_DEBIT = itemDate.Sum(x => x.NDEBIT_AMOUNT),
                    NTOTAL_CREDIT = itemDate.Sum(x => x.NCREDIT_AMOUNT)
                };
                
                var loSubTotalCurrDate = itemDate.GroupBy(x => x.CCURRENCY_CODE).ToList();
                foreach (var itemSubCurrDate in loSubTotalCurrDate)
                {
                    var loCurrency = new GLR00100TotalByCurrDTO()
                    {
                        CCURRENCY_CODE = itemSubCurrDate.Key,
                        NTOTAL_CREDIT = itemSubCurrDate.Sum(x => x.NCREDIT_AMOUNT),
                        NTOTAL_DEBIT = itemSubCurrDate.Sum(x => x.NDEBIT_AMOUNT)
                    };
                    loDataDate.SubTotalByCurr.Add(loCurrency);
                }
                // parse ke datetime dulu untuk DREF_DATE pada loDataDate.Data
                
                loData.DataByDate.Add(loDataDate);
                loData.NGRAND_TOTAL_CREDIT += loDataDate.NTOTAL_CREDIT;
                loData.NGRAND_TOTAL_DEBIT += loDataDate.NTOTAL_DEBIT;
            }

            loData.Header = new GLR00100ReportHeaderDTO
            {
                CFROM_DEPT_CODE = poParam.CFROM_DEPT_CODE,
                CTO_DEPT_CODE = poParam.CTO_DEPT_CODE,
                CFROM_PERIOD = loCollection.FirstOrDefault()?.CFROM_PERIOD,
                CTO_PERIOD = loCollection.FirstOrDefault()?.CTO_PERIOD,
                CCURRENCY_TYPE = poParam.CCURRENCY_TYPE,
                CCURRENCY_TYPE_NAME = poParam.CCURRENCY_TYPE_NAME,
                CREPORT_BASED_ON = poParam.CREPORT_TYPE
            };
            
            // if (string.IsNullOrWhiteSpace(loData.Header.CFROM_PERIOD))
            // {
            //     
            //     // loData.Header.CFROM_PERIOD ??= DateTime.ParseExact(poParam.CFROM_PERIOD, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
            //     loData.Header.CFROM_PERIOD = loCollection.FirstOrDefault()?.CFROM_PERIOD;
            // }
            //
            // if (string.IsNullOrWhiteSpace(loData.Header.CTO_PERIOD))
            // {
            //     // loData.Header.CTO_PERIOD ??= DateTime.ParseExact(poParam.CTO_PERIOD, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
            //     loData.Header.CTO_PERIOD = loCollection.FirstOrDefault()?.CTO_PERIOD;
            // }
            
            //looping dan ubah ref date menjadi dd-MM-yyyy
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

            loData.SubData = loCls.BasedOnDateSubReportDb(loDbParam);

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