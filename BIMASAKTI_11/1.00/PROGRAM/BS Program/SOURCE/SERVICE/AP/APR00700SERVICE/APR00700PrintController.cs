using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using APR00700BACK;
using APR00700COMMON;
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
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using APR00700SERVICE.DTO_s;
using Microsoft.AspNetCore.Authorization;
using BaseHeaderReportCOMMON;
using System.Globalization;
using APR00700COMMON.Print_DTO;
using APR00700COMMON.DTO_s;
using APR00700OMMON;

namespace APR00700SERVICE
{
    public class APR00700PrintController : R_ReportControllerBase
    {
        private APR00700PrintLogger _logger;
        private R_ReportFastReportBackClass _ReportCls;
        private APR00700ParamDTO _Parameter;
        private readonly ActivitySource _activitySource;
        APR00700BackResources.Resources_Dummy_Class _backRes = new();

        public APR00700PrintController(ILogger<APR00700PrintLogger> logger)
        {
            APR00700PrintLogger.R_InitializeLogger(logger);
            _logger = APR00700PrintLogger.R_GetInstanceLogger();
            _activitySource = APR00700Activity.R_InitializeAndGetActivitySource(nameof(APR00700PrintController));


            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }

        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "APR00700.frx");
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            poData.Add(GenerateDataPrint(_Parameter));
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
        public R_DownloadFileResultDTO DownloadResultPrintPost(APR00700ParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            _logger.LogInfo("Start - Post DownloadResultPrintPost Status");
            R_Exception loException = new R_Exception();
            APR00700PrintLogKey loCache = null;
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new APR00700PrintLogKey
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY),
                    poReportGlobal = R_ReportGlobalVar.R_GetReportDTO()
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
            _logger.LogInfo("End - Print LOI");
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult SupplierLedger_ReportListGet(string pcGuid)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            R_Exception loException = new R_Exception();
            FileStreamResult loRtn = null;
            APR00700PrintLogKey loResultGUID = null;
            try
            {
                // Deserialize the GUID from the cache
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<APR00700PrintLogKey>(R_DistributedCache.Cache.Get(pcGuid));
                _logger.LogDebug("Deserialized GUID: {pcGuid}", pcGuid);

                // Get Parameter
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                R_ReportGlobalVar.R_SetFromReportDTO(loResultGUID.poReportGlobal);


                _Parameter = loResultGUID.poParam;

                _logger.LogDebug("Deserialized GUID: {pcGuid}", pcGuid);
                _logger.LogDebug("Deserialized Parameters: {@Parameters}", _Parameter);

                R_FileType loFileType = new();
                if (string.IsNullOrWhiteSpace(loResultGUID.poParam.CREPORT_FILEEXT) || string.IsNullOrWhiteSpace(loResultGUID.poParam.CREPORT_FILENAME))
                {
                    loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF), R_ReportUtility.GetMimeType(R_FileType.PDF));
                }
                else
                {
                    loFileType = loResultGUID.poParam.CREPORT_FILEEXT switch
                    {
                        "XLSX" => R_FileType.XLSX,
                        "PDF" => R_FileType.PDF,
                        "XLS" => R_FileType.XLS,
                        "CSV" => R_FileType.CSV,
                        _ => R_FileType.PDF,
                    };

                    //print nama save as
                    loRtn = File(_ReportCls.R_GetStreamReport(peExport: loFileType), R_ReportUtility.GetMimeType(loFileType), $"{loResultGUID.poParam.CREPORT_FILENAME}.{loResultGUID.poParam.CREPORT_FILEEXT}");
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
            _logger.LogInfo("End - LOI Report Generation");
            return loRtn;
        }

        #region Helper

        private APR00700PrintDisplayDTO GenerateDataPrint(APR00700ParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            var loEx = new R_Exception();
            APR00700PrintDisplayDTO loRtn = new APR00700PrintDisplayDTO();
            var loParam = new BaseHeaderDTO();

            CultureInfo loCultureInfo = new(R_BackGlobalVar.REPORT_CULTURE);

            try
            {
                _logger.LogInfo("_logger.LogInfo(\"Start - Generating data for Print\");\n data for LOI report.");

                //Add Resources
                loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),
                    "Page", loCultureInfo);
                loRtn.BaseHeaderColumn.Of =
                    R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_Date =
                    R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),
                    "Print_By", loCultureInfo);

                APR00700LabelDTO loColumnObject = new();
                var loColumn = AssignValuesWithMessages(typeof(APR00700BackResources.Resources_Dummy_Class), loCultureInfo, loColumnObject);

                // Set base header data
                _logger.LogDebug("Deserialized Print Parameters: {@PrintParameters}");

                var loCls = new APR00700Cls();
                var loBaseHeader = loCls.GetCompanyName(R_BackGlobalVar.COMPANY_ID);


                loParam.CCOMPANY_NAME = R_BackGlobalVar.COMPANY_ID.ToUpper();
                loParam.CPRINT_CODE = "APR00700";
                loParam.CPRINT_NAME = APR00700ContextConstant.CPROGRAM_NAME;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID.ToUpper();
                loParam.CPRINT_DATE_COMPANY = DateTime.ParseExact(loBaseHeader.CDATETIME_NOW, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture).ToString(R_BackGlobalVar.REPORT_FORMAT_SHORT_DATE + " " + R_BackGlobalVar.REPORT_FORMAT_SHORT_TIME);

                // Create an instance 
                loParam.BLOGO_COMPANY = loCls.GetPropertyLogo(poParam).CLOGO;
                loParam.CCOMPANY_NAME = loCls.GetCompanyName(R_BackGlobalVar.COMPANY_ID).CCOMPANY_NAME;

                poParam.CPERIOD_DISPLAY = $"{poParam.CFR_PERIOD.Insert(4, "-")} - {poParam.CTO_PERIOD.Insert(4, "-")}";

                // Create an instance 
                APR00700ReportDataDTO loData = new()
                {
                    Title = APR00700ContextConstant.CPROGRAM_NAME,
                    Header = APR00700ContextConstant.CPROGRAM_NAME,
                    Column = (APR00700LabelDTO)loColumn,
                    HeaderParam = poParam,
                };

                // Get print data for Group Of Account report
                var loCollData = loCls.GetReportData(poParam);
                _logger.LogInfo("Data generation successful. Processing data for printing.");

                loCollData.ForEach(x =>
                {
                    x.DREF_DATE = DateTime.TryParseExact(x.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loResultInv) ? loResultInv : null;
                });

                // Set the generated data in loRtn
                _logger.LogInfo("Data processed successfully. Generating print output.");

                loData.Data = loCollData;
                loData.HeaderData = loCollData.FirstOrDefault();
                loRtn.BaseHeaderData = loParam;
                loRtn.ReportData = loData;

                _logger.LogInfo("Print output generated successfully. Saving print file.");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End - Data Generation for Print");
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
