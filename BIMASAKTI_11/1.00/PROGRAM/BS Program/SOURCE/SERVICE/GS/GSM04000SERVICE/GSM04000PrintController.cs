using System.Collections;
using System.Diagnostics;
using System.Globalization;
using BaseHeaderReportCOMMON;
using GSM04000Back;
using GSM04000Common;
using GSM04000Common.DTO_s;
using GSM04000Common.DTO_s.Print;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;

namespace GSM04000Service;

public class GSM04000PrintController : R_ReportControllerBase
{
    private LoggerGSM04000 _logger;
    private R_ReportFastReportBackClass _ReportCls;
    private readonly ActivitySource _activitySource;

    public GSM04000PrintController(ILogger<GSM04000PrintController> logger)
    {
        LoggerGSM04000.R_InitializeLogger(logger);
        _logger = LoggerGSM04000.R_GetInstanceLogger();
        _activitySource = GSM04000Activity.R_InitializeAndGetActivitySource(nameof(GSM04000PrintController));

        _ReportCls = new R_ReportFastReportBackClass();
        _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
        _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
        _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
    }

    private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcfiletemplate)
    {
        pcfiletemplate = Path.Combine("Reports", "GSM04000.frx");
    }

    private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
    {
        poData.Add(GeneratePrint());
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
    public R_DownloadFileResultDTO DocumentReportPost()
    {
        using var loActivity = _activitySource.StartActivity(nameof(DocumentReportPost));
        _logger.LogInfo("Start - Post Supplier Statement");
        R_Exception loException = new();
        GSM04000ReportLogKeyDTO loCache = null;
        R_DownloadFileResultDTO loRtn = null;
        try
        {
            loRtn = new R_DownloadFileResultDTO();
            loCache = new GSM04000ReportLogKeyDTO
            {
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
    public FileStreamResult DocumentReportGet(string pcGuid)
    {
        using var loActivity = _activitySource.StartActivity(nameof(DocumentReportGet));
        _logger.LogInfo("Start - Get Supplier Statement");
        R_Exception loException = new();
        FileStreamResult loRtn = null;
        GSM04000ReportLogKeyDTO loResultGUID = null;
        try
        {
            //Get Parameter
            loResultGUID =
                R_NetCoreUtility.R_DeserializeObjectFromByte<GSM04000ReportLogKeyDTO>(
                    R_DistributedCache.Cache.Get(pcGuid));

            //Get Data and Set Log Key
            R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
            R_ReportGlobalVar.R_SetFromReportDTO(loResultGUID.poGlobalVar);

            _logger.LogInfo("Print Supplier Statement");
            loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF),
                R_ReportUtility.GetMimeType(R_FileType.PDF));

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

    private GSM04000ReportWithBaseHeaderDTO GeneratePrint()
    {
        using var loActivity = _activitySource.StartActivity(nameof(GeneratePrint));
        var loEx = new R_Exception();
        var loRtn = new GSM04000ReportWithBaseHeaderDTO();
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

            var loLabelObject = new GSM04000ReportLabelDTO();
            var loLabel = AssignValuesWithMessages(typeof(GSM04000BackResources.Resources_Dummy_Class),
                loCultureInfo, loLabelObject);

            _logger.LogInfo("Set Base Header Data");

            var lcCompany = R_BackGlobalVar.COMPANY_ID;
            var lcUser = R_BackGlobalVar.USER_ID;
            var lcLang = R_BackGlobalVar.CULTURE;

            var loCls = new GSM04000Cls();
            var loHeader = loCls.GetBaseHeaderLogoCompany(lcCompany);
            loRtn.BaseHeaderData = new BaseHeaderDTO
            {
                BLOGO_COMPANY = loHeader.BLOGO,
                CCOMPANY_NAME = loHeader.CCOMPANY_NAME ?? string.Empty,
                DPRINT_DATE_COMPANY = DateTime.ParseExact(loHeader.CDATETIME_NOW, "yyyyMMdd HH:mm:ss",
                    CultureInfo.InvariantCulture),
                CPRINT_CODE = "GSM04000",
                CPRINT_NAME = "Department",
                CUSER_ID = lcUser
            };

            var loData = new GSM04000ReportResultDTO()
            {
                Title = "Department",
                Label = (GSM04000ReportLabelDTO)loLabel,
                Data = new List<GSM04000ReportDataDTO>(),
            };

            _logger.LogInfo("Set Parameter");
            var loDbParam = new GeneralParamDTO
            {
                CCOMPANY_ID = lcCompany,
                CUSER_ID = lcUser,
                CLANG_ID = lcLang
            };

            _logger.LogInfo("Get Department List Report");

            var loDepartments = loCls.GetDepartmentList(loDbParam);

            if (loDepartments != null)
            {
                var loUserCls = new GSM04100Cls();
                foreach (var department in loDepartments)
                {
                    var loDataResult = new GSM04000ReportDataDTO
                    {
                        Department = department,
                        Users = loUserCls.GetUserDeptList(department)
                    };

                    loData.Data.Add(loDataResult);
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