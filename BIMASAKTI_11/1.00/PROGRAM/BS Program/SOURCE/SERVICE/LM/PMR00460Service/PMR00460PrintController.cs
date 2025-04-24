using System.Collections;
using System.Diagnostics;
using System.Globalization;
using BaseHeaderReportCOMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR00460Back;
using PMR00460Common;
using PMR00460Common.DTOs.Print;
using PMR00460Service.DTOs;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;

namespace PMR00460Service;

public class PMR00460PrintController : R_ReportControllerBase
{
    private LoggerPMR00460 _logger;
    private R_ReportFastReportBackClass _ReportCls;
    private PMR00460ReportParam _Parameter;
    private readonly ActivitySource _activitySource;

    public PMR00460PrintController(ILogger<PMR00460PrintController> logger)
    {
        LoggerPMR00460.R_InitializeLogger(logger);
        _logger = LoggerPMR00460.R_GetInstanceLogger();
        _activitySource = PMR00460Activity.R_InitializeAndGetActivitySource(nameof(PMR00460PrintController));

        _ReportCls = new R_ReportFastReportBackClass();
        _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
        _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
        _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
    }

    private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcfiletemplate)
    {
        pcfiletemplate = Path.Combine("Reports", "PMR00460Handover.frx");
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
    public R_DownloadFileResultDTO ActivityReportPost(PMR00460ReportParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ActivityReportPost));
        _logger.LogInfo("Start - Post Handover Report");
        R_Exception loException = new();
        PMR00460ReportLogKeyDTO loCache = null;
        R_DownloadFileResultDTO loRtn = null;
        try
        {
            loRtn = new R_DownloadFileResultDTO();
            loCache = new PMR00460ReportLogKeyDTO
            {
                poParam = poParam,
                poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY),
                poGlobalVar = R_ReportGlobalVar.R_GetReportDTO()
            };


            _logger.LogInfo("Set GUID Param - Post Handover Report");
            R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte(loCache));
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(loException);
        }

        loException.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Post Handover Report");
        return loRtn;
    }

    [HttpGet, AllowAnonymous]
    public FileStreamResult ActivityReportGet(string pcGuid)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ActivityReportGet));
        _logger.LogInfo("Start - Get Handover Report");
        R_Exception loException = new();
        FileStreamResult loRtn = null;
        PMR00460ReportLogKeyDTO loResultGUID = null;
        try
        {
            //Get Parameter
            loResultGUID =
                R_NetCoreUtility.R_DeserializeObjectFromByte<PMR00460ReportLogKeyDTO>(
                    R_DistributedCache.Cache.Get(pcGuid));

            //Get Data and Set Log Key
            R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
            R_ReportGlobalVar.R_SetFromReportDTO(loResultGUID.poGlobalVar);

            _Parameter = loResultGUID.poParam;

            _logger.LogDebug("Report Parameter", _Parameter);

            // loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
            if (_Parameter.LIS_PRINT)
            {
                _logger.LogInfo("Print Handover Report");
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF), R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            else
            {
                _logger.LogInfo("Save As Handover Report");
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
        _logger.LogInfo("End - Get Handover Report");
        return loRtn;
    }

    private PMR00460ReportWithBaseHeaderDTO GeneratePrint(PMR00460ReportParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GeneratePrint));
        var loEx = new R_Exception();
        var loRtn = new PMR00460ReportWithBaseHeaderDTO();
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
    
            var loLabelObject = new PMR00460ReportLabelDTO();
            var loLabel = AssignValuesWithMessages(typeof(PMR00460BackResources.Resources_Dummy_Class),
                loCultureInfo, loLabelObject);
    
            _logger.LogInfo("Set Base Header Data");
            
            var lcCompany = R_BackGlobalVar.COMPANY_ID;
            var lcUser = R_BackGlobalVar.USER_ID;
            var lcLang = R_BackGlobalVar.CULTURE;
    
            var loCls = new PMR00460Cls();
            var loHeader = loCls.GetBaseHeaderLogoCompany(lcCompany);
            loRtn.BaseHeaderData = new BaseHeaderDTO
            {
                BLOGO_COMPANY = loHeader.BLOGO,
                CCOMPANY_NAME = loHeader.CCOMPANY_NAME!,
                CPRINT_CODE = "PMR00460",
                CPRINT_NAME = "Handover Report",
                CUSER_ID = lcUser
            };
    
            var loData = new PMR00460ReportResultDTO()
            {
                Title = "Handover Report",
                Label = (PMR00460ReportLabelDTO)loLabel,
                Header = null,
                Data = new List<PMR00460ReportDataDTO>(),
            };
    
            _logger.LogInfo("Set Parameter");
            var loDbParam = new PMR00460ParameterDb
            {
                CCOMPANY_ID = lcCompany,
                CPROPERTY_ID = poParam.CPROPERTY_ID,
                CFROM_BUILDING_ID = poParam.CFROM_BUILDING_ID,
                CTO_BUILDING_ID = poParam.CTO_BUILDING_ID,
                CFROM_PERIOD = poParam.CFROM_PERIOD,
                CTO_PERIOD = poParam.CTO_PERIOD,
                CREPORT_TYPE = poParam.CREPORT_TYPE,
                LOPEN = poParam.LOPEN,
                LSCHEDULED = poParam.LSCHEDULED,
                LCONFIRMED = poParam.LCONFIRMED,
                LCLOSED = poParam.LCLOSED,
                CLANG_ID = lcLang,
                CTYPE = poParam.CTYPE,
            };
    
            _logger.LogInfo("Get Detail Invoice List Report");
    
            var loCollection = loCls.GetReportData(loDbParam);
            
            var loResult = new List<PMR00460ReportDataDTO>();
            var loGroupBuilding = loCollection.GroupBy(x => x.CBUILDING_ID).ToList();

            foreach (var item in loGroupBuilding)
            {
                //group by ref no dan LCHECKLIST = false
                // var loGroupLoi = item.GroupBy(x => x.CREF_NO).Where(x => x.All(y => y.LCHECKLIST == false)).ToList();
                // var loGroupLoi = item.GroupBy(x => x.CREF_NO).ToList();
                // groupby ref no dan unit id
                var loGroupLoi = item.GroupBy(x => new { x.CREF_NO, x.CUNIT_ID }).ToList();
                var loGroup = new PMR00460ReportDataDTO()
                {
                    CBUILDING_ID =item.Key,
                    CBUILDING_NAME = item.First().CBUILDING_NAME,
                    LOI = new List<PMR00460GroupBuildingDTO>()
                };
                
                foreach (var item1 in loGroupLoi)
                {
                    var loGroupLoiSummary = item1.Where(x => x.LCHECKLIST == false).ToList();
                    var loGroupLoiDetail = item1.Where(x => x.LCHECKLIST == true).ToList();
                    var loGroupByLoi = new PMR00460GroupBuildingDTO()
                    {
                        CREF_NO = item1.Key.CREF_NO,
                        Summary = new List<PMR00460GroupByLoiSummaryDTO>(),
                        Detail = new List<PMR00460GroupByLoiDetailDTO>()
                    };
                    
                    foreach (var item2 in loGroupLoiSummary)
                    {
                        loGroupByLoi.Summary.Add(new PMR00460GroupByLoiSummaryDTO()
                        {
                            CREF_NO = item2.CREF_NO,
                            LCHECKLIST = item2.LCHECKLIST,
                            CSCHEDULE_DATE = item2.CSCHEDULE_DATE,
                            DSCHEDULE_DATE = DateTime.TryParseExact(item2.CSCHEDULE_DATE, "yyyyMMdd",
                                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var refDate)
                                ? refDate : (DateTime?)null,
                            CUNIT_ID = item2.CUNIT_ID,
                            CUNIT_TYPE_CATEGORY = item2.CUNIT_TYPE_CATEGORY,
                            CTENANT_ID = item2.CTENANT_ID,
                            CTENANT_NAME = item2.CTENANT_NAME,
                            CTENANT_EMAIL = item2.CTENANT_EMAIL,
                            CSTAFF_ID = item2.CSTAFF_ID,
                            CSTAFF_NAME = item2.CSTAFF_NAME,
                            CHANDOVER_STATUS = item2.CHANDOVER_STATUS
                        });
                    }
                    
                    foreach (var item2 in loGroupLoiDetail)
                    {
                        if (!string.IsNullOrEmpty(item2.CCHECKLIST_DESCRIPTION))
                        {
                            loGroupByLoi.Detail.Add(new PMR00460GroupByLoiDetailDTO()
                            {
                                CCHECKLIST_DESCRIPTION = item2.CCHECKLIST_DESCRIPTION,
                                CNOTES = item2.CNOTES,
                                CCHECKLIST_STATUS = item2.LCHECKLIST_STATUS ? R_Utility.R_GetMessage(typeof(PMR00460BackResources.Resources_Dummy_Class), "OK", loCultureInfo) : R_Utility.R_GetMessage(typeof(PMR00460BackResources.Resources_Dummy_Class), "NotOK", loCultureInfo),
                                CCARE_REF_NO = item2.CCARE_REF_NO,
                                IDEFAULT_QUANTITY = item2.IDEFAULT_QUANTITY,
                                IACTUAL_QUANTITY = item2.IACTUAL_QUANTITY,
                                CQUANTITY_DISPLAY = item2.IDEFAULT_QUANTITY == 0 ? "-" : item2.IACTUAL_QUANTITY + "/" + item2.IDEFAULT_QUANTITY + " " + item2.CUNIT,
                                CUNIT = item2.CUNIT
                            });
                        }
                    }
                    
                    loGroup.LOI.Add(loGroupByLoi);
                }
                
                loResult.Add(loGroup);
            }
            
            loData.Data = loResult;
            loData.Header = new PMR00460ReportHeaderDTO
            {
                CPROPERTY_NAME = poParam.CPROPERTY_NAME,
                CBUILDING = !string.IsNullOrEmpty(poParam.CTO_BUILDING_ID) &&
                            poParam.CFROM_BUILDING_ID != poParam.CTO_BUILDING_ID ? (poParam.CFROM_BUILDING_NAME + " to " + poParam.CTO_BUILDING_NAME) : poParam.CFROM_BUILDING_NAME,
                CPERIOD = poParam.CTO_PERIOD != poParam.CFROM_PERIOD ? (poParam.CFROM_PERIOD_MONTH_NAME + " " + poParam.IFROM_PERIOD_YEAR + " To " + poParam.CTO_PERIOD_MONTH_NAME + " " + poParam.ITO_PERIOD_YEAR) : poParam.CFROM_PERIOD_MONTH_NAME + " " + poParam.IFROM_PERIOD_YEAR,
                CREPORT_TYPE = poParam.CREPORT_TYPE,
                CREPORT_TYPE_NAME = poParam.CREPORT_TYPE_NAME,
                CTYPE = poParam.CTYPE,
                CTYPE_NAME = poParam.CTYPE_NAME,
                LOPEN = poParam.LOPEN,
                LSCHEDULED = poParam.LSCHEDULED,
                LCONFIRMED = poParam.LCONFIRMED,
                LCLOSED = poParam.LCLOSED,
            };
    
            foreach (var item in loData.Data)
            {
                item.LOI.ForEach(x => x.Summary.ForEach(y =>
                {
                    y.DSCHEDULE_DATE = DateTime.TryParseExact(y.CSCHEDULE_DATE, "yyyyMMdd",
                        CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var scheduleDate) ? scheduleDate : (DateTime?)null;
                }));
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