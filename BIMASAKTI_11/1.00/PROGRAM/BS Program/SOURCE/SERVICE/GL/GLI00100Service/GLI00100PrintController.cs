using System.Collections;
using System.Diagnostics;
using System.Globalization;
using BaseHeaderReportCOMMON;
using GLI00100Back;
using GLI00100Common;
using GLI00100Common.DTOs;
using GLI00100Common.DTOs.Print;
using GLI00100Service.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_OpenTelemetry;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;

namespace GLI00100Service;

public class GLI00100PrintController : R_ReportControllerBase
{
    private LoggerGLI00100 _logger;
    private R_ReportFastReportBackClass _ReportCls;
    private GLI00100PopupParamsDTO _Parameter;
    private readonly ActivitySource _activitySource;

    public GLI00100PrintController(ILogger<GLI00100PrintController> logger)
    {
        LoggerGLI00100.R_InitializeLogger(logger);
        _logger = LoggerGLI00100.R_GetInstanceLogger();
        _activitySource = GLI00100Activity.R_InitializeAndGetActivitySource(nameof(GLI00100PrintController));

        _ReportCls = new R_ReportFastReportBackClass();
        _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
        _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
        _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
    }

    private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcfiletemplate)
    {
        pcfiletemplate = System.IO.Path.Combine("Reports", "GLI00100AccountStatusReport.frx");
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
    public R_DownloadFileResultDTO AccountStatusPost(GLI00100PopupParamsDTO poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(AccountStatusPost));
        _logger.LogInfo("Start - Post Account Status");
        R_Exception loException = new();
        GLI00100PrintLogKeyDTO loCache = null;
        R_DownloadFileResultDTO loRtn = null;
        try
        {
            loRtn = new R_DownloadFileResultDTO();
            loCache = new GLI00100PrintLogKeyDTO
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
    public FileStreamResult AccountStatusGet(string pcGuid)
    {
        using var loActivity = _activitySource.StartActivity(nameof(AccountStatusGet));
        _logger.LogInfo("Start - Get Account Status");
        R_Exception loException = new();
        FileStreamResult loRtn = null;
        GLI00100PrintLogKeyDTO loResultGUID = null;
        try
        {
            //Get Parameter
            loResultGUID =
                R_NetCoreUtility.R_DeserializeObjectFromByte<GLI00100PrintLogKeyDTO>(
                    R_DistributedCache.Cache.Get(pcGuid));

            //Get Data and Set Log Key
            R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
            R_ReportGlobalVar.R_SetFromReportDTO(loResultGUID.poGlobalVar);

            _Parameter = loResultGUID.poParam;
            loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
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

    private GLI00100PrintWithBaseHeaderDTO GeneratePrint(GLI00100PopupParamsDTO poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GeneratePrint));
        var loEx = new R_Exception();
        var loRtn = new GLI00100PrintWithBaseHeaderDTO();
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

            var loColumnObject = new GLI00100PrintColumnDTO();
            var loColumn = AssignValuesWithMessages(typeof(GLI00100BackResources.Resources_Dummy_Class),
                loCultureInfo, loColumnObject);
            
            var loHeaderObject = new GLI00100PrintHeaderTitleDTO();
            var loHeader = AssignValuesWithMessages(typeof(GLI00100BackResources.Resources_Dummy_Class),
                loCultureInfo, loHeaderObject);
            
            var loRowObject = new GLI00100PrintRowDTO();
            var loRow = AssignValuesWithMessages(typeof(GLI00100BackResources.Resources_Dummy_Class),
                loCultureInfo, loRowObject);

            _logger.LogInfo("Set Base Header Data");

            var lcCompany = R_BackGlobalVar.COMPANY_ID;
            var lcUser = R_BackGlobalVar.USER_ID;
            var lcLang = R_BackGlobalVar.CULTURE;
            
            var loCls = new GLI00100Cls();
            var loBaseHeader = loCls.GetBaseHeaderLogoCompany(lcCompany);
            var loParam = new BaseHeaderDTO
            {
                BLOGO_COMPANY = loBaseHeader.BLOGO,
                CCOMPANY_NAME = loBaseHeader.CCOMPANY_NAME!,
                DPRINT_DATE_COMPANY = DateTime.ParseExact(loBaseHeader.CDATETIME_NOW, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture),
                CPRINT_CODE = "GLI00100",
                CPRINT_NAME = "Account Status Report",
                CUSER_ID = lcUser,
            };

            var loData = new GLI00100PrintResultDTO()
            {
                Title = "Account Status",
                // HeaderTitle = new GLI00100PrintHeaderTitleDTO(),
                // Column = new GLI00100PrintColumnDTO(),
                HeaderTitle = (GLI00100PrintHeaderTitleDTO) loHeader,
                Column = (GLI00100PrintColumnDTO) loColumn,
                Row = (GLI00100PrintRowDTO) loRow,
                Data = new GLI00100AccountAnalysisDTO()
            };

            var loDbParam = new GLI00100ParameterDb();
            var loDbOptParam = new GLI00100AccountAnalysisParamDb();

            _logger.LogInfo("Set Parameter");
            loDbParam.CCOMPANY_ID = lcCompany;
            loDbParam.CLANGUAGE_ID = lcLang;
            loDbOptParam.CGLACCOUNT_NO = poParam.CGLACCOUNT_NO;
            loDbOptParam.CYEAR = poParam.CYEAR;
            loDbOptParam.CCURRENCY_TYPE = poParam.CCURRENCY_TYPE;
            loDbOptParam.CCENTER_CODE = poParam.CCENTER_CODE;
            loDbOptParam.CBUDGET_NO = poParam.CBUDGET_NO;

            _logger.LogInfo("Get Detail Account Analysis Report");
            var loResult = loCls.GLI00100GetDetailAccountAnalysisReportDb(loDbParam, loDbOptParam);

            loData.Data = loResult;
            loRtn.Data = loData;

            loRtn.BaseHeaderData = loParam;
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