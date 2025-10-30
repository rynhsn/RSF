using BaseHeaderReportCOMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI.Log;
using R_CommonFrontBackAPI;
using R_ReportFastReportBack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using APR00600COMMON;
using APR00600COMMON.Params;
using APR00600BACK;
using APR00600COMMON.DTOs.Print;

namespace APR00600SERVICE
{
    public class APR00600PrintSupplierCategoryController : R_ReportControllerBase
    {
        private LoggerAPR00600Print _logger;
        private R_ReportFastReportBackClass _ReportCls;
        private APR00600ReportParamDTO _Parameter;
        private readonly ActivitySource _activitySource;
        APR00600BackResources.Resources_Dummy_Class _backRes = new();

        public APR00600PrintSupplierCategoryController(ILogger<LoggerAPR00600Print> logger)
        {
            LoggerAPR00600Print.R_InitializeLogger(logger);
            _logger = LoggerAPR00600Print.R_GetInstanceLogger();
            _activitySource = APR00600Activity.R_InitializeAndGetActivitySource(nameof(APR00600PrintSupplierCategoryController));
            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }

        #region reporthelper

        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "APR00600ReportSupplierCategory.frx");
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

        #endregion

        [HttpPost]
        public R_DownloadFileResultDTO DownloadResultPrintPost(APR00600ReportParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            _logger.LogInfo("Start - Post DownloadResultPrintPost Status");
            R_Exception loException = new R_Exception();
            APR00600PrintLogKey loCache = null;
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new APR00600PrintLogKey
                {
                    poParamSummary = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY)
                };
                _logger.LogInfo("Set GUID Param - Post DownloadResultPrintPost Status");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End - Print UserActivity");
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult SupplierCategory_ReportListGet(string pcGuid)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            R_Exception loException = new R_Exception();
            FileStreamResult loRtn = null;
            APR00600PrintLogKey loResultGUID = null;
            try
            {
                // Deserialize the GUID from the cache
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<APR00600PrintLogKey>(R_DistributedCache.Cache.Get(pcGuid));
                _logger.LogDebug("Deserialized GUID: {pcGuid}", pcGuid);

                // Get Parameter
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);

                _Parameter = loResultGUID.poParamSummary;

                _logger.LogDebug("Deserialized GUID: {pcGuid}", pcGuid);
                _logger.LogDebug("Deserialized Parameters: {@Parameters}", _Parameter);

                R_FileType loFileType = new();
                if (loResultGUID.poParamSummary.LIS_PRINT)
                {
                    loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF), R_ReportUtility.GetMimeType(R_FileType.PDF));

                }
                else
                {
                    switch (loResultGUID.poParamSummary.CREPORT_FILETYPE)
                    {
                        case "XLSX":
                            loFileType = R_FileType.XLSX;
                            break;
                        case "PDF":
                            loFileType = R_FileType.PDF;
                            break;
                        case "XLS":
                            loFileType = R_FileType.XLS;
                            break;
                        case "CSV":
                            loFileType = R_FileType.CSV;
                            break;
                        default:
                            loFileType = R_FileType.PDF;
                            break;
                    }

                    //print nama save as
                    loRtn = File(_ReportCls.R_GetStreamReport(peExport: loFileType), R_ReportUtility.GetMimeType(loFileType), $"{loResultGUID.poParamSummary.CREPORT_FILENAME}.{loResultGUID.poParamSummary.CREPORT_FILETYPE}");
                }
                _logger.LogInfo("Data retrieval successful. Generating report.");

                _logger.LogInfo("Report generated successfully.");
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End - UserActivity Report Generation");
            return loRtn;
        }

        private APR00600ReportWithBaseHeaderDTO GeneratePrint(APR00600ReportParamDTO poParam)
        {
            using var loActivity = _activitySource.StartActivity(nameof(GeneratePrint));
            var loEx = new R_Exception();
            var loRtn = new APR00600ReportWithBaseHeaderDTO();
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

                var loLabelObject = new APR00600ReportLabelDTO();
                var loLabel = AssignValuesWithMessages(typeof(APR00600BackResources.Resources_Dummy_Class),
                    loCultureInfo, loLabelObject);

                _logger.LogInfo("Set Base Header Data");

                var loCls = new APR00600Cls();
                var loHeader = loCls.GetBaseHeaderLogoCompany(poParam);
                loRtn.BaseHeaderData = new BaseHeaderDTO
                {
                    BLOGO_COMPANY = loHeader.BLOGO,
                    CCOMPANY_NAME = loHeader.CCOMPANY_NAME!,
                    //DPRINT_DATE_COMPANY = DateTime.ParseExact(loHeader.CDATETIME_NOW, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture),
                    CPRINT_DATE_COMPANY = DateTime.ParseExact(loHeader.CDATETIME_NOW, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture).ToString(R_BackGlobalVar.REPORT_FORMAT_SHORT_DATE + " " + R_BackGlobalVar.REPORT_FORMAT_SHORT_TIME),
                    CPRINT_CODE = "APR00600",
                    CPRINT_NAME = "AP SUPPLIER ANALYSIS",
                    CUSER_ID = poParam.CUSER_ID,
                };

                var loData = new APR00600ReportResultDTO()
                {
                    Title = "AP SUPPLIER ANALYSIS",
                    Label = (APR00600ReportLabelDTO)loLabel,
                    Header = null,
                    Data = new List<APR00600DataResultDTO>(),
                };

                loData.Data = loCls.GetReportData(poParam);
                loData.Header = new APR00600ReportHeaderDTO
                {
                    CPROPERTY = poParam.CPROPERTY_NAME + $"({poParam.CPROPERTY_ID})",
                    CSUPPLIER_DISPLAY = poParam.CFR_CODE + "-" + $"{poParam.CFR_CODE_NAME}",
                    CPERIOD_DISPLAY = poParam.CFR_PERIOD_DISPLAY + " - " + $"{poParam.CTO_PERIOD_DISPLAY}",
                    CCURRENCY = poParam.CCURRENCY_TYPE_NAME,
                    CFILTER_BY = poParam.CFILTER_BY_NAME
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
}
