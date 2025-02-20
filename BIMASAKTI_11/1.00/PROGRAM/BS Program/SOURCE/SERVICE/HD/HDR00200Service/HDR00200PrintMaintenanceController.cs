using System.Collections;
using System.Diagnostics;
using System.Globalization;
using BaseHeaderReportCOMMON;
using BaseHeaderResources;
using HDR00200Back;
using HDR00200Common;
using HDR00200Common.DTOs.Print;
using HDR00200Service.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;

namespace HDR00200Service;

public class HDR00200PrintMaintenanceController : R_ReportControllerBase
{
    private LoggerHDR00200 _logger;
    private R_ReportFastReportBackClass _ReportCls;
    private HDR00200ReportParam _Parameter;
    private readonly ActivitySource _activitySource;

    public HDR00200PrintMaintenanceController(ILogger<HDR00200PrintMaintenanceController> logger)
    {
        LoggerHDR00200.R_InitializeLogger(logger);
        _logger = LoggerHDR00200.R_GetInstanceLogger();
        _activitySource = HDR00200Activity.R_InitializeAndGetActivitySource(nameof(HDR00200PrintMaintenanceController));

        _ReportCls = new R_ReportFastReportBackClass();
        _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
        _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
        _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
    }

    private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcfiletemplate)
    {
        pcfiletemplate = Path.Combine("Reports", "HDR00200CareReportMaintenance.frx");
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
    public R_DownloadFileResultDTO ActivityReportPost(HDR00200ReportParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ActivityReportPost));
        _logger.LogInfo("Start - Post Care Report Maintenance");
        R_Exception loException = new();
        HDR00200ReportLogKeyDTO loCache = null;
        R_DownloadFileResultDTO loRtn = null;
        try
        {
            loRtn = new R_DownloadFileResultDTO();
            loCache = new HDR00200ReportLogKeyDTO
            {
                poParam = poParam,
                poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY),
                poGlobalVar = R_ReportGlobalVar.R_GetReportDTO()
            };


            _logger.LogInfo("Set GUID Param - Post Care Report Maintenance");
            R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte(loCache));
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(loException);
        }

        loException.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Post Care Report Maintenance");
        return loRtn;
    }

    [HttpGet, AllowAnonymous]
    public FileStreamResult ActivityReportGet(string pcGuid)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ActivityReportGet));
        _logger.LogInfo("Start - Get Care Report Maintenance");
        R_Exception loException = new();
        FileStreamResult loRtn = null;
        HDR00200ReportLogKeyDTO loResultGUID = null;
        try
        {
            //Get Parameter
            loResultGUID =
                R_NetCoreUtility.R_DeserializeObjectFromByte<HDR00200ReportLogKeyDTO>(
                    R_DistributedCache.Cache.Get(pcGuid));

            //Get Data and Set Log Key
            R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
            R_ReportGlobalVar.R_SetFromReportDTO(loResultGUID.poGlobalVar);

            _Parameter = loResultGUID.poParam;

            _logger.LogDebug("Report Parameter", _Parameter);

            // loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
            if (_Parameter.LIS_PRINT)
            {
                _logger.LogInfo("Print Care Report Maintenance");
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF),
                    R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            else
            {
                _logger.LogInfo("Save As Care Report Maintenance");
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
        _logger.LogInfo("End - Get Care Report Maintenance");
        return loRtn;
    }

    private HDR00200ReportWithBaseHeaderDTO GeneratePrint(HDR00200ReportParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GeneratePrint));
        var loEx = new R_Exception();
        var loRtn = new HDR00200ReportWithBaseHeaderDTO();
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

            var loLabelObject = new HDR00200ReportLabelDTO();
            var loLabel = AssignValuesWithMessages(typeof(HDR00200BackResources.Resources_Dummy_Class),
                loCultureInfo, loLabelObject);

            _logger.LogInfo("Set Base Header Data");

            var lcCompany = R_BackGlobalVar.COMPANY_ID;
            var lcUser = R_BackGlobalVar.USER_ID;
            var lcLang = R_BackGlobalVar.CULTURE;

            var loCls = new HDR00200Cls();
            var loLogo = loCls.GetBaseHeaderLogoCompany(lcCompany);
            loRtn.BaseHeaderData = new BaseHeaderDTO
            {
                BLOGO_COMPANY = loLogo.BLOGO,
                CCOMPANY_NAME = "PT Realta Chakradarma",
                CPRINT_CODE = "HDR00200",
                CPRINT_NAME = "CARE Report",
                CUSER_ID = lcUser
            };

            var loData = new HDR00200ReportResultDTO()
            {
                Title = "CARE Report",
                Label = (HDR00200ReportLabelDTO)loLabel,
                Header = null,
                DataMaintenance = new List<HDR00200GroupCareMaintenanceDTO>(),
            };

            _logger.LogInfo("Set Parameter");
            var loDbParam = new HDR00200ParameterDb
            {
                CCOMPANY_ID = lcCompany,
                CLANG_ID = lcLang,
                CUSER_ID = lcUser,
                CPROPERTY_ID = poParam.CPROPERTY_ID,
                CREPORT_TYPE = poParam.CREPORT_TYPE,
                CAREA = poParam.CAREA,
                CFROM_BUILDING_ID = poParam.CFROM_BUILDING_ID,
                CTO_BUILDING_ID = poParam.CTO_BUILDING_ID,
                CFROM_DEPT_CODE = poParam.CFROM_DEPT_CODE,
                CTO_DEPT_CODE = poParam.CTO_DEPT_CODE,
                CFROM_PERIOD = poParam.CFROM_PERIOD,
                CTO_PERIOD = poParam.CTO_PERIOD,
                CCATEGORY = poParam.CCATEGORY,
                CSTATUS = poParam.CSTATUS,
            };

            _logger.LogInfo("Get Detail Invoice List Report");

            var loCollection = loCls.GetReportData(loDbParam);
            
            //jika ada data null atau kosong maka akan diisi dengan -
            
            foreach (var item in loCollection)
            {
                if (string.IsNullOrEmpty(item.CBUILDING_ID)){ item.CBUILDING_ID = "-"; }
                if (string.IsNullOrEmpty(item.CBUILDING_NAME)){ item.CBUILDING_NAME = "-"; }
                if (string.IsNullOrEmpty(item.CFLOOR_ID)){ item.CFLOOR_ID = "-"; }
                if (string.IsNullOrEmpty(item.CFLOOR_NAME)){ item.CFLOOR_NAME = "-"; }
                if (string.IsNullOrEmpty(item.CUNIT_LOCATION_ID)){ item.CUNIT_LOCATION_ID = "-"; }
                if (string.IsNullOrEmpty(item.CUNIT_LOCATION_NAME)){ item.CUNIT_LOCATION_NAME = "-"; }
                if (string.IsNullOrEmpty(item.CASSET_CODE)){ item.CASSET_CODE = "-"; }
                if (string.IsNullOrEmpty(item.CCARE_TICKET_NO)){ item.CCARE_TICKET_NO = "-"; }
                if (string.IsNullOrEmpty(item.CCARE_DESCRIPTION)){ item.CCARE_DESCRIPTION = "-"; }
                if (string.IsNullOrEmpty(item.CSTATUS)){ item.CSTATUS = "-"; }
                if (string.IsNullOrEmpty(item.CSTATUS_DESCRIPTION)){ item.CSTATUS_DESCRIPTION = "-"; }
                // if (string.IsNullOrEmpty(item.CSUBSTATUS)){ item.CSUBSTATUS = "-"; }
                // if (string.IsNullOrEmpty(item.CSUBSTATUS_DESCRIPTION)){ item.CSUBSTATUS_DESCRIPTION = "-"; }
                if (string.IsNullOrEmpty(item.CTENANT_OWNER_ID)){ item.CTENANT_OWNER_ID = "-"; }
                if (string.IsNullOrEmpty(item.CTENANT_OWNER_NAME)){ item.CTENANT_OWNER_NAME = "-"; }
                if (string.IsNullOrEmpty(item.CCALLER_NAME)){ item.CCALLER_NAME = "-"; }
                if (string.IsNullOrEmpty(item.CCALLER_PHONE)){ item.CCALLER_PHONE = "-"; }
                if (string.IsNullOrEmpty(item.CCALLER_CATEGORY)){ item.CCALLER_CATEGORY = "-"; }
                if (string.IsNullOrEmpty(item.CCALLER_CATEGORY_DESCRIPTION)){ item.CCALLER_CATEGORY_DESCRIPTION = "-"; }
                if (string.IsNullOrEmpty(item.CTENANT_OR_OWNER)){ item.CTENANT_OR_OWNER = "-"; }
                if (string.IsNullOrEmpty(item.CSOLUTION)){ item.CSOLUTION = "-"; }
                if (string.IsNullOrEmpty(item.CINVOICE_REF_NO)){ item.CINVOICE_REF_NO = "-"; }
                if (string.IsNullOrEmpty(item.CCURRENCY_SYMBOL)){ item.CCURRENCY_SYMBOL = "-"; }
                if (string.IsNullOrEmpty(item.CTASK_CTG_CODE)){ item.CTASK_CTG_CODE = "-"; }
                if (string.IsNullOrEmpty(item.CTASK_CTG_NAME)){ item.CTASK_CTG_NAME = "-"; }
                if (string.IsNullOrEmpty(item.CTASK_CODE)){ item.CTASK_CODE = "-"; }
                if (string.IsNullOrEmpty(item.CTASK_NAME)){ item.CTASK_NAME = "-"; }
                if (string.IsNullOrEmpty(item.CDURATION_TYPE)){ item.CDURATION_TYPE = "-"; }
                if (string.IsNullOrEmpty(item.CDURATION_TYPE_DESCRIPTION)){ item.CDURATION_TYPE_DESCRIPTION = "-"; }
                if (string.IsNullOrEmpty(item.CCHECKLIST_STATUS)){ item.CCHECKLIST_STATUS = "-"; }
                if (string.IsNullOrEmpty(item.CCHECKLIST_NOTES)){ item.CCHECKLIST_NOTES = "-"; }
            }

            var loTempDataMaintenance = loCollection.GroupBy(data1a => new
            {
                data1a.CTASK_CTG_CODE,
                data1a.CTASK_CTG_NAME
            }).Select(data1b => new HDR00200GroupCareMaintenanceDTO()
            {
                CTASK_CTG_CODE = data1b.Key.CTASK_CTG_CODE,
                CTASK_CTG_NAME = data1b.Key.CTASK_CTG_NAME,
                SUM_ESCALATED = data1b.Count(x => x.LESCALATED),
                SUM_ALL = data1b.Count(),

                Task = data1b.GroupBy(data2a => new
                {
                    data2a.CTASK_CODE,
                    data2a.CTASK_NAME,
                    data2a.IDURATION_TIME,
                    data2a.CDURATION_TYPE_DESCRIPTION
                }).Select(data2b => new HDR00200GroupTaskMaintenanceDTO()
                {
                    CTASK_CODE = data2b.Key.CTASK_CODE,
                    CTASK_NAME = data2b.Key.CTASK_NAME,
                    IDURATION_TIME = data2b.Key.IDURATION_TIME,
                    CDURATION_TYPE_DESCRIPTION = data2b.Key.CDURATION_TYPE_DESCRIPTION,
                    SUM_ESCALATED = data2b.Count(x => x.LESCALATED),
                    SUM_ALL = data2b.Count(),

                    Asset = data2b.GroupBy(data3a => new
                    {
                        data3a.CASSET_CODE,
                    }).Select(data3b => new HDR00200GroupAssetMaintenanceDTO()
                    {
                        CASSET_CODE = data3b.Key.CASSET_CODE,
                        SUM_ESCALATED = data3b.Count(x => x.LESCALATED),
                        SUM_ALL = data3b.Count(),
                        Data = data3b.ToList()
                    }).ToList()
                }).ToList()
            }).ToList();

            loData.DataMaintenance = loTempDataMaintenance;
            loData.Header = new HDR00200ReportHeaderDTO
            {
                PropertyDisplay = poParam.CPROPERTY_NAME,
                PeriodFrom = poParam.DFROM_PERIOD,
                PeriodTo = poParam.DTO_PERIOD,
                AreaDisplay = poParam.CAREA_NAME,
                CategoryDisplay = poParam.CREPORT_TYPE_NAME,
                BuildingDisplay = poParam.CFROM_BUILDING_NAME == poParam.CTO_BUILDING_NAME
                    ? poParam.CFROM_BUILDING_NAME
                    : $"{poParam.CFROM_BUILDING_NAME} {R_Utility.R_GetMessage(typeof(HDR00200BackResources.Resources_Dummy_Class), "To", loCultureInfo)} {poParam.CTO_BUILDING_NAME}",
                DepartmentDisplay = poParam.CFROM_DEPT_NAME == poParam.CTO_DEPT_NAME
                    ? poParam.CFROM_DEPT_NAME
                    : $"{poParam.CFROM_DEPT_NAME} {R_Utility.R_GetMessage(typeof(HDR00200BackResources.Resources_Dummy_Class), "To", loCultureInfo)} {poParam.CTO_DEPT_NAME}",
                CheckHandover = poParam.LHANDOVER,
                CheckComplaint = poParam.LCOMPLAINT,
                CheckRequest = poParam.LREQUEST,
                CheckInquiry = poParam.LINQUIRY,
                CheckOpen = poParam.LOPEN,
                CheckSubmitted = poParam.LSUBMITTED,
                CheckAssigned = poParam.LASSIGNED,
                CheckOnProgress = poParam.LON_PROGRESS,
                CheckSolved = poParam.LSOLVED,
                CheckCompleted = poParam.LCOMPLETED,
                CheckConfirmed = poParam.LCONFIRMED,
                CheckClosed = poParam.LCLOSED,
                CheckCancelled = poParam.LCANCELLED,
                CheckTerminated = poParam.LTERMINATED
            };
            
            if (loData.Header.PeriodFrom == loData.Header.PeriodTo)
            {
                loData.Header.PeriodDisplay = loData.Header.PeriodFrom?.ToString("dd-MMM-yyyy", loCultureInfo);
            }
            else
            {
                loData.Header.PeriodDisplay = $"{loData.Header.PeriodFrom?.ToString("dd-MMM-yyyy", loCultureInfo)} {R_Utility.R_GetMessage(typeof(HDR00200BackResources.Resources_Dummy_Class), "To", loCultureInfo)} {loData.Header.PeriodTo?.ToString("dd-MMM-yyyy", loCultureInfo)}";
            }
            
            loData.SUM_WITH_INVOICE = loCollection.Count(x => x.CINVOICE_REF_NO != "-");
            loData.SUM_ESCALATED = loCollection.Count(x => x.LESCALATED);
            loData.SUM_ALL = loCollection.Count();

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