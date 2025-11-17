using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR03400BACK;
using PMR03400COMMON;
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
using PMR03400SERVICE.DTO_s;
using Microsoft.AspNetCore.Authorization;
using BaseHeaderReportCOMMON;
using System.Globalization;
using PMR03400COMMON.Print_DTO;
using PMR03400COMMON.DTO_s;

namespace PMR03400SERVICE
{
    public class PMR03400PrintController : R_ReportControllerBase
    {
        private PMR03400PrintLogger _logger;
        private R_ReportFastReportBackClass _ReportCls;
        private PMR03400ParamDTO _Parameter;
        private readonly ActivitySource _activitySource;
        PMR03400BackResources.Resources_Dummy_Class _backRes = new();

        public PMR03400PrintController(ILogger<PMR03400PrintLogger> logger)
        {
            PMR03400PrintLogger.R_InitializeLogger(logger);
            _logger = PMR03400PrintLogger.R_GetInstanceLogger();
            _activitySource = PMR03400Activity.R_InitializeAndGetActivitySource(nameof(PMR03400PrintController));


            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }

        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "PMR03400.frx");
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
        public R_DownloadFileResultDTO DownloadResultPrintPost(PMR03400ParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            _logger.LogInfo("Start - Post DownloadResultPrintPost Status");
            R_Exception loException = new R_Exception();
            PMR03400PrintLogKey loCache = null;
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new PMR03400PrintLogKey
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
        public FileStreamResult CustomerLedger_ReportListGet(string pcGuid)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            R_Exception loException = new R_Exception();
            FileStreamResult loRtn = null;
            PMR03400PrintLogKey loResultGUID = null;
            try
            {
                // Deserialize the GUID from the cache
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<PMR03400PrintLogKey>(R_DistributedCache.Cache.Get(pcGuid));
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

        private PMR03400PrintDisplayDTO GenerateDataPrint(PMR03400ParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            var loEx = new R_Exception();
            PMR03400PrintDisplayDTO loRtn = new PMR03400PrintDisplayDTO();
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

                PMR03400LabelDTO loColumnObject = new();
                var loColumn = AssignValuesWithMessages(typeof(PMR03400BackResources.Resources_Dummy_Class), loCultureInfo, loColumnObject);

                // Set base header data
                _logger.LogDebug("Deserialized Print Parameters: {@PrintParameters}");

                var loCls = new PMR03400Cls();
                var loBaseHeader = loCls.GetCompanyName(R_BackGlobalVar.COMPANY_ID);


                loParam.CCOMPANY_NAME = R_BackGlobalVar.COMPANY_ID.ToUpper();
                loParam.CPRINT_CODE = "PMR03400";
                loParam.CPRINT_NAME = PMR03400ContextConstant.CPROGRAM_NAME;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID.ToUpper();
                loParam.CPRINT_DATE_COMPANY = DateTime.ParseExact(loBaseHeader.CDATETIME_NOW, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture).ToString(R_BackGlobalVar.REPORT_FORMAT_SHORT_DATE + " " + R_BackGlobalVar.REPORT_FORMAT_SHORT_TIME);

                // Create an instance 
                loParam.BLOGO_COMPANY = loCls.GetPropertyLogo(poParam).CLOGO;
                loParam.CCOMPANY_NAME = loCls.GetCompanyName(R_BackGlobalVar.COMPANY_ID).CCOMPANY_NAME;

                poParam.CPERIOD_DISPLAY = $"{poParam.CFR_PERIOD.Insert(4, "-")} - {poParam.CTO_PERIOD.Insert(4, "-")}";

                // Create an instance 
                PMR03400ReportDataDTO loData = new()
                {
                    Title = PMR03400ContextConstant.CPROGRAM_NAME,
                    Header = PMR03400ContextConstant.CPROGRAM_NAME,
                    Column = (PMR03400LabelDTO)loColumn,
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
