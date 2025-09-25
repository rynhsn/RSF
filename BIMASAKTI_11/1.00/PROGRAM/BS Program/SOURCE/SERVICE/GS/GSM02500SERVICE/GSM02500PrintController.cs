using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using GSM02500BACK;
using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.Loggers;
using GSM02500BackResources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace GSM02500SERVICE
{
    public class GSM02500PrintController : R_ReportControllerBase
    {
        private LoggerGSM02500 _logger;
        private R_ReportFastReportBackClass _ReportCls;
        private GSM02500PrintParamDTO _PropertyProfileParameter;
        private readonly ActivitySource _activitySource;

        #region instantiate
        public GSM02500PrintController(ILogger<LoggerGSM02500> logger)
        {
            //Initial and Get Logger
            LoggerGSM02500.R_InitializeLogger(logger);
            _logger = LoggerGSM02500.R_GetInstanceLogger();
            _activitySource = GSM02500ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GSM02500PrintController));

            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }
        #endregion

        #region Event Handler
        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports","GSM02500.frx");
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            poData.Add(GenerateDataPrint(_PropertyProfileParameter));
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
        public R_DownloadFileResultDTO PropertyProfilePost(GSM02500PrintParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("PropertyProfilePost");
            R_Exception loException = new R_Exception();
            _logger.LogInfo("Start PropertyProfilePost");
            GSM02500PrintLogKeyDTO loCache =null;
            R_DownloadFileResultDTO loRtn = null;

            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new GSM02500PrintLogKeyDTO
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY),
                    poReportGlobal = R_ReportGlobalVar.R_GetReportDTO()
                };

                // Set Guid Param 
                _logger.LogInfo("Set GUID Param PropertyProfilePost");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<GSM02500PrintLogKeyDTO>(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End PropertyProfilePost");

            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult AllStreamPropertyProfileGet(string pcGuid)
        {
            using Activity activity = _activitySource.StartActivity("AllStreamPropertyProfileGet");
            R_Exception loException = new R_Exception();
            GSM02500PrintLogKeyDTO loResultGUID = null;
            FileStreamResult loRtn = null;
            _logger.LogInfo("Start AllStreamPropertyProfileGet");

            try
            {
                //Get Parameter
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<GSM02500PrintLogKeyDTO>(R_DistributedCache.Cache.Get(pcGuid));

                //Get Data and Set Log Key
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                R_ReportGlobalVar.R_SetFromReportDTO(loResultGUID.poReportGlobal);
                _PropertyProfileParameter = loResultGUID.poParam;

                R_FileType loFileType = new();
                if (loResultGUID.poParam.LIS_PRINT)
                {
                    loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF), R_ReportUtility.GetMimeType(R_FileType.PDF));
                }
                else
                {
                    switch (loResultGUID.poParam.CREPORT_FILETYPE)
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
                    loRtn = File(_ReportCls.R_GetStreamReport(peExport: loFileType), R_ReportUtility.GetMimeType(loFileType), $"{loResultGUID.poParam.CREPORT_FILENAME}.{loResultGUID.poParam.CREPORT_FILETYPE}");
                }
                _logger.LogInfo("Report generated successfully.");
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End AllStreamPropertyProfileGet");

            return loRtn;
        }

        #region Helper
        private GSM02500ResultWithBaseHeaderPrintDTO GenerateDataPrint(GSM02500PrintParamDTO poParam)
        {
            System.Globalization.CultureInfo loCultureInfo = new System.Globalization.CultureInfo(poParam.CREPORT_CULTURE);
            using Activity activity = _activitySource.StartActivity("GenerateDataPrint");
            var loEx = new R_Exception();
            GSM02500ResultWithBaseHeaderPrintDTO loRtn = new GSM02500ResultWithBaseHeaderPrintDTO();

            try
            {
                //Add Resources
                loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Page", loCultureInfo);
                loRtn.BaseHeaderColumn.Of = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_Date = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_By", loCultureInfo);

                //Assign Column
                GSM02500ColumnDTO loColumnObject = new GSM02500ColumnDTO();
                var loColumn = AssignValuesWithMessages(typeof(GSM02500BackResources.Resources_Dummy_Class), loCultureInfo, loColumnObject);
                loRtn.Column = (GSM02500ColumnDTO)loColumn;

                var loCls = new GSM02500Cls();

                _logger.LogInfo("Call Method GetPrintDataResult Report");
                var loCollection = loCls.GetPrintDataResult(poParam);

                // Set Base Header Data
                _logger.LogInfo("Set BaseHeader Report");
                var loParam = new BaseHeaderDTO()
                {
                    CPRINT_CODE = poParam.CCOMPANY_ID.ToUpper(),
                    CPRINT_NAME = "Property List",
                    CUSER_ID = poParam.CUSER_LOGIN_ID.ToUpper(),
                };
                var loBaseHeader = loCls.GetBaseHeaderLogoCompany();
                loParam.BLOGO_COMPANY = loBaseHeader.CLOGO;
                loParam.CCOMPANY_NAME = loBaseHeader.CCOMPANY_NAME;
                loParam.DPRINT_DATE_COMPANY = DateTime.ParseExact(loBaseHeader.CDATETIME_NOW, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture);

                var loData = new GSM02500ResultPrintDTO();

                loData.Header = string.Format("{0} - {1}", poParam.CPROPERTY_ID, poParam.CPROPERTY_NAME);
                loData.Data = loCollection;

                _logger.LogInfo("Set Data Report");
                loRtn.BaseHeaderData = loParam;
                loRtn.PropertyProfile = loData;
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
        #endregion
    }
}