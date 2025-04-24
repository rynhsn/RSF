using System.Collections;
using System.Diagnostics;
using System.Globalization;
using APR00300Back;
using APR00300Common;
using APR00300Common.DTOs.Print;
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

namespace APR00300Service;

public class APR00300PrintController : R_ReportControllerBase
{
    private LoggerAPR00300 _logger;
    private R_ReportFastReportBackClass _ReportCls;
    private APR00300ReportParam _Parameter;
    private readonly ActivitySource _activitySource;

    public APR00300PrintController(ILogger<APR00300PrintController> logger)
    {
        LoggerAPR00300.R_InitializeLogger(logger);
        _logger = LoggerAPR00300.R_GetInstanceLogger();
        _activitySource = APR00300Activity.R_InitializeAndGetActivitySource(nameof(APR00300PrintController));

        _ReportCls = new R_ReportFastReportBackClass();
        _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
        _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
        _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
    }

    private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcfiletemplate)
    {
        pcfiletemplate = Path.Combine("Reports", "APR00300SupplierStatement.frx");
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
    public R_DownloadFileResultDTO ActivityReportPost(APR00300ReportParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ActivityReportPost));
        _logger.LogInfo("Start - Post Supplier Statement");
        R_Exception loException = new();
        APR00300ReportLogKeyDTO loCache = null;
        R_DownloadFileResultDTO loRtn = null;
        try
        {
            loRtn = new R_DownloadFileResultDTO();
            loCache = new APR00300ReportLogKeyDTO
            {
                poParam = poParam,
                poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY),
                poGlobalVar = R_ReportGlobalVar.R_GetReportDTO()
            };


            _logger.LogInfo("Set GUID Param - Post Supplier Statement");
            R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte(loCache));
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(loException);
        }

        loException.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Post Supplier Statement");
        return loRtn;
    }

    [HttpGet, AllowAnonymous]
    public FileStreamResult ActivityReportGet(string pcGuid)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ActivityReportGet));
        _logger.LogInfo("Start - Get Supplier Statement");
        R_Exception loException = new();
        FileStreamResult loRtn = null;
        APR00300ReportLogKeyDTO loResultGUID = null;
        try
        {
            //Get Parameter
            loResultGUID =
                R_NetCoreUtility.R_DeserializeObjectFromByte<APR00300ReportLogKeyDTO>(
                    R_DistributedCache.Cache.Get(pcGuid));

            //Get Data and Set Log Key
            R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
            R_ReportGlobalVar.R_SetFromReportDTO(loResultGUID.poGlobalVar);

            _Parameter = loResultGUID.poParam;

            _logger.LogDebug("Report Parameter", _Parameter);

            // loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
            if (_Parameter.LIS_PRINT)
            {
                _logger.LogInfo("Print Supplier Statement");
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF), R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            else
            {
                _logger.LogInfo("Save As Supplier Statement");
                var loFileType = (R_FileType)Enum.Parse(typeof(R_FileType), _Parameter.CREPORT_FILETYPE);
                loRtn = File(_ReportCls.R_GetStreamReport(peExport: loFileType), R_ReportUtility.GetMimeType(loFileType), $"{_Parameter.CREPORT_FILENAME}.{_Parameter.CREPORT_FILETYPE}");
            }

            _logger.LogInfo("Finish get file");
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(loException);
        }

        loException.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Supplier Statement");
        return loRtn;
    }

    private APR00300ReportWithBaseHeaderDTO GeneratePrint(APR00300ReportParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GeneratePrint));
        var loEx = new R_Exception();
        var loRtn = new APR00300ReportWithBaseHeaderDTO();
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

            var loLabelObject = new APR00300ReportLabelDTO();
            var loLabel = AssignValuesWithMessages(typeof(APR00300BackResources.Resources_Dummy_Class),
                loCultureInfo, loLabelObject);

            _logger.LogInfo("Set Base Header Data");
            
            var lcCompany = R_BackGlobalVar.COMPANY_ID;
            var lcUser = R_BackGlobalVar.USER_ID;
            var lcLang = R_BackGlobalVar.CULTURE;

            var loCls = new APR00300Cls();
            var loHeader = loCls.GetBaseHeaderLogoCompany(lcCompany);
            loRtn.BaseHeaderData = new BaseHeaderDTO
            {
                BLOGO_COMPANY = loHeader.BLOGO,
                CCOMPANY_NAME = loHeader.CCOMPANY_NAME ?? string.Empty,
                CPRINT_CODE = "APR00300",
                CPRINT_NAME = "Supplier Statement",
                CUSER_ID = lcUser
            };

            var loData = new APR00300ReportResultDTO()
            {
                Title = "Supplier Statement",
                Label = (APR00300ReportLabelDTO)loLabel,
                Header = null,
                Data = new List<APR00300ReportDataDTO>(),
            };

            _logger.LogInfo("Set Parameter");
            var loDbParam = new APR00300ParameterDb
            {
                CCOMPANY_ID = lcCompany,
                CPROPERTY_ID = poParam.CPROPERTY_ID,
                CFROM_SUPPLIER_ID = poParam.CFROM_SUPPLIER_ID,
                CTO_SUPPLIER_ID = poParam.CTO_SUPPLIER_ID,
                CCUT_OFF_DATE = poParam.CCUT_OFF_DATE,
                CFROM_PERIOD = poParam.CFROM_PERIOD,
                CTO_PERIOD = poParam.CTO_PERIOD,
                LINCLUDE_ZERO_BALANCE = poParam.LINCLUDE_ZERO_BALANCE,
                LSHOW_AGE_TOTAL = poParam.LSHOW_AGE_TOTAL,
                CLANG_ID = lcLang
            };

            _logger.LogInfo("Get Detail Invoice List Report");

            var loCollection = loCls.GetReportData(loDbParam);
            //buat original dan outstanding menjadi minus jika trand code = 120010 dan 120040
            loCollection.ForEach(x =>
            {
                if (x.CTRANS_CODE is "120010" or "110040")
                {
                    x.NORIGINAL_AMOUNT = -x.NORIGINAL_AMOUNT;
                    x.NOUTSTANDING_AMOUNT = -x.NOUTSTANDING_AMOUNT;
                }
            });
            
            var loResult = new List<APR00300ReportDataDTO>();
            var loGroupSupplier = loCollection.GroupBy(x => x.CSUPPLIER_ID).ToList();
            foreach (var item in loGroupSupplier)
            {
                //untuk list period
                var loGroupPeriod = item.GroupBy(x => x.CREF_PRD).ToList();
                var loGroup = new APR00300ReportDataDTO
                {
                    CSUPPLIER_ID = item.Key,
                    CSUPPLIER_NAME = item.First().CSUPPLIER_NAME,
                    PERIODS = new List<APR00300GroupPeriodDTO>(),
                    NGRAND_TOTAL = item.Sum(x => x.NOUTSTANDING_AMOUNT)
                };
                
                foreach (var item2 in loGroupPeriod)
                {
                    var loGroupCurrencyPeriod = item2.GroupBy(x => x.CCURRENCY_CODE).ToList();
                    var loPeriod = new APR00300GroupPeriodDTO
                    {
                        CREF_PRD = item2.Key,
                        INVOICES = new List<APR00300DataResultDTO>(),
                        CURRENCIES = new List<APR00300GroupCurrencyDTO>(),
                        NSUB_TOTAL = item2.Sum(x => x.NOUTSTANDING_AMOUNT)
                    };
                    foreach (var item3 in loGroupCurrencyPeriod)
                    {
                        var loCurrency = new APR00300GroupCurrencyDTO
                        {
                            CCURRENCY_CODE = item3.Key,
                            NSUB_TOTAL = item3.Sum(x => x.NOUTSTANDING_AMOUNT)
                        };
                        loPeriod.CURRENCIES.Add(loCurrency);
                        loPeriod.INVOICES.AddRange(item3);
                    }
                    loGroup.PERIODS.Add(loPeriod);
                }
                
                // untuk list invoice
                loGroup.INVOICES = item.ToList();
                // untuk list currency
                var loGroupCurrency = item.GroupBy(x => x.CCURRENCY_CODE).ToList();
                loGroup.CURRENCIES = new List<APR00300GroupCurrencyDTO>();
                foreach (var item2 in loGroupCurrency)
                {
                    var loCurrency = new APR00300GroupCurrencyDTO
                    {
                        CCURRENCY_CODE = item2.Key,
                        NSUB_TOTAL = item2.Sum(x => x.NOUTSTANDING_AMOUNT)
                    };
                    loGroup.CURRENCIES.Add(loCurrency);
                }
                
                loResult.Add(loGroup);
            }
            
            loData.Data = loResult;
            loData.Header = new APR00300ReportHeaderDTO
            {
                CPROPERTY_ID = poParam.CPROPERTY_ID,
                CPROPERTY_NAME = poParam.CPROPERTY_NAME,
                DCUT_OFF_DATE = poParam.DCUT_OFF_DATE,
                CFROM_PERIOD = poParam.CFROM_PERIOD,
                CTO_PERIOD = poParam.CTO_PERIOD,
                DSTATEMENT_DATE = poParam.DSTATEMENT_DATE,
                LINCLUDE_ZERO_BALANCE = poParam.LINCLUDE_ZERO_BALANCE,
                LSHOW_AGE_TOTAL = poParam.LSHOW_AGE_TOTAL,
                CDATE_BASED_ON = poParam.CDATE_BASED_ON
            };

            foreach (var item in loData.Data)
            {
                item.PERIODS.ForEach(x => x.INVOICES.ForEach(y =>
                {
                    y.DREF_DATE = DateTime.TryParseExact(y.CREF_DATE, "yyyyMMdd",
                        CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var refDate) ? refDate : (DateTime?)null;
                }));
                
                item.INVOICES.ForEach(x =>
                {
                    x.DREF_DATE = DateTime.TryParseExact(x.CREF_DATE, "yyyyMMdd",
                        CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var refDate) ? refDate : (DateTime?)null;
                });
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