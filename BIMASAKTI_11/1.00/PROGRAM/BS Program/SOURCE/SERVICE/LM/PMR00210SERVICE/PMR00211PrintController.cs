using BaseHeaderReportCOMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR00210BACK;
using PMR00210COMMON;
using PMR00210COMMON.DTO_s;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI.Log;
using R_CommonFrontBackAPI;
using R_ReportFastReportBack;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using PMR00210COMMON.Print_DTO;


namespace PMR00210SERVICE
{
    public class PMR00211PrintController : R_ReportControllerBase
    {
        private PMR00210PrintLogger _logger;
        private R_ReportFastReportBackClass _ReportCls;
        private PMR00210ParamDTO _Parameter;
        private readonly ActivitySource _activitySource;
        PMR00210BackResources.Resources_Dummy_Class _backRes = new();

        public PMR00211PrintController(ILogger<PMR00210PrintLogger> logger)
        {
            PMR00210PrintLogger.R_InitializeLogger(logger);
            _logger = PMR00210PrintLogger.R_GetInstanceLogger();
            _activitySource = PMR00210Activity.R_InitializeAndGetActivitySource(nameof(PMR00211PrintController));


            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }

        #region reporthelper

        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "PMR00210Summary.frx");
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
        #endregion

        [HttpPost]
        public R_DownloadFileResultDTO DownloadResultPrintPost(PMR00210ParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            _logger.LogInfo("Start - Post DownloadResultPrintPost Status");
            R_Exception loException = new R_Exception();
            PMR00210PrintLogKey loCache = null;
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new PMR00210PrintLogKey
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
        public FileStreamResult LOICasualLeasingSummary_ReportListGet(string pcGuid)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            R_Exception loException = new R_Exception();
            FileStreamResult loRtn = null;
            PMR00210PrintLogKey loResultGUID = null;
            try
            {
                // Deserialize the GUID from the cache
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<PMR00210PrintLogKey>(R_DistributedCache.Cache.Get(pcGuid));
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

        private PMR00210PrintDislpayDTO GenerateDataPrint(PMR00210ParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            var loEx = new R_Exception();
            PMR00210PrintDislpayDTO loRtn = new PMR00210PrintDislpayDTO();
            var loParam = new BaseHeaderDTO();

            CultureInfo loCultureInfo =new(R_BackGlobalVar.REPORT_CULTURE);

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

                PMR00210LabelDTO loColumnObject = new();
                var loColumn = AssignValuesWithMessages(typeof(PMR00210BackResources.Resources_Dummy_Class), loCultureInfo, loColumnObject);

                // Set base header data
                _logger.LogDebug("Deserialized Print Parameters: {@PrintParameters}");

                var loCls = new PMR00210Cls();
                var loBaseHeader = loCls.GetCompanyName(R_BackGlobalVar.COMPANY_ID);


                loParam.CCOMPANY_NAME = R_BackGlobalVar.COMPANY_ID.ToUpper();
                loParam.CPRINT_CODE = "PMR00210";
                loParam.CPRINT_NAME = PMR00210ContextConstant.CPROGRAM_NAME;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID.ToUpper();
                loParam.CPRINT_DATE_COMPANY = DateTime.ParseExact(loBaseHeader.CDATETIME_NOW, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture).ToString(R_BackGlobalVar.REPORT_FORMAT_SHORT_DATE + " " + R_BackGlobalVar.REPORT_FORMAT_SHORT_TIME);

                // Create an instance 
                loParam.BLOGO_COMPANY = loCls.GetCompanyLogo(poParam).CLOGO;
                loParam.CCOMPANY_NAME = loCls.GetCompanyName(R_BackGlobalVar.COMPANY_ID).CCOMPANY_NAME;

                // Create an instance 
                PMR00210ReportDataDTO loData = new()
                {
                    Title = PMR00210ContextConstant.CPROGRAM_NAME,
                    Header = PMR00210ContextConstant.CPROGRAM_NAME,
                    Column = (PMR00210LabelDTO)loColumn,
                    HeaderParam = poParam,
                    Data = new List<PMR00210DataDTO>(),
                };

                // Get print data for Group Of Account report
                var loCollData = loCls.GetSummaryReportData(poParam);
                _logger.LogInfo("Data generation successful. Processing data for printing.");

                // Set the generated data in loRtn
                _logger.LogInfo("Data processed successfully. Generating print output.");
                var loMappingData = R_Utility.R_ConvertCollectionToCollection<PMR00210SPResultDTO, PMR00210DataDTO>(loCollData);
                //change ref date to date format
                foreach (var loRowData in loMappingData)
                {
                    loRowData.DREF_DATE = DateTime.TryParseExact(loRowData.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loResultInv) ? loResultInv : null;
                    loRowData.DCHARGE_DETAIL_START_DATE = DateTime.TryParseExact(loRowData.CCHARGE_DETAIL_START_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loResultChg) ? loResultChg : null;
                    loRowData.DCHARGE_DETAIL_END_DATE = DateTime.TryParseExact(loRowData.CCHARGE_DETAIL_END_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loResultChgEnd) ? loResultChgEnd : null;
                    loRowData.DDEPOSIT_DETAIL_DATE = DateTime.TryParseExact(loRowData.CDEPOSIT_DETAIL_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loResultDep) ? loResultDep : null;
                    loRowData.DDOCUMENT_DETAIL_DATE = DateTime.TryParseExact(loRowData.CDOCUMENT_DETAIL_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loResultDoc) ? loResultDoc : null;
                    loRowData.DDOCUMENT_DETAIL_EXPIRED_DATE = DateTime.TryParseExact(loRowData.CDOCUMENT_DETAIL_EXPIRED_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loResultDocExp) ? loResultDocExp : null;
                }
                loData.Data = new List<PMR00210DataDTO>(loMappingData);
                loRtn.BaseHeaderData = loParam;
                loRtn.SummaryData = loData;

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
