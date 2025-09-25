using BaseHeaderReportCOMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR00170BACK;
using PMR00170COMMON;
using PMR00170COMMON.Utility_Report;
using PMR00170SERVICE.DTOLogs;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;
using System.Collections;
using System.Diagnostics;
using System.Globalization;

namespace PMR00170SERVICE
{
    public class PMR00170SummaryReportController : R_ReportControllerBase
    {
        #region instantiate

        private R_ReportFastReportBackClass _ReportCls;
        private PMR00170DBParamDTO _Parameter;
        private LoggerPMR00170 _logger;
        private readonly ActivitySource _activitySource;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public PMR00170SummaryReportController(ILogger<PMR00170SummaryReportController> logger)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            //Initial and Get instance
            LoggerPMR00170.R_InitializeLogger(logger);
            _logger = LoggerPMR00170.R_GetInstanceLogger();
            _activitySource = PMR00170Activity.R_InitializeAndGetActivitySource(nameof(PMR00170SummaryReportController));

            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }

        #endregion instantiate

        #region Event Handler

        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "PMR00170Summary.frx");
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

        #endregion Event Handler

        #region Method Get Post

        [HttpPost]
        public R_DownloadFileResultDTO SummaryReportPost(PMR00170DBParamDTO poParameter)
        {
            string lcMethodName = nameof(SummaryReportPost);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START method {0} ", lcMethodName));

            PMR00170ReportLogKeyDTO<PMR00170DBParamDTO> loCache = null;
            R_Exception loException = new R_Exception();
            R_DownloadFileResultDTO? loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new PMR00170ReportLogKeyDTO<PMR00170DBParamDTO>
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY),
                    poReportGlobal = R_ReportGlobalVar.R_GetReportDTO()
                };

                // Set Guid Param
                _logger.LogInfo("Set GUID Param on method post");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END method {0}", lcMethodName));

            return loRtn!;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult SummaryReportGet(string pcGuid)
        {
            string lcMethodName = nameof(SummaryReportGet);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START method {0}", lcMethodName));

            PMR00170ReportLogKeyDTO<PMR00170DBParamDTO> loResultGUID = null;
            R_Exception loException = new R_Exception();
            FileStreamResult? loRtn = null;
            try
            {
                //Get Parameter
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<PMR00170ReportLogKeyDTO<PMR00170DBParamDTO>>(R_DistributedCache.Cache.Get(pcGuid));

                //Get Data and Set Log Key
#pragma warning disable CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                R_ReportGlobalVar.R_SetFromReportDTO(loResultGUID.poReportGlobal);
#pragma warning restore CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.
                _Parameter = loResultGUID.poParam!;

                _logger.LogInfo(string.Format("READ file report method {0}", lcMethodName));
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
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END method {0}", lcMethodName));

            return loRtn!;
        }

        #endregion Method Get Post

        #region Helper

        private PMR00170SummaryResultWithHeaderDTO GenerateDataPrint(PMR00170DBParamDTO poParam)
        {
            string lcMethodName = nameof(GenerateDataPrint);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START method {0}", lcMethodName));

            var loException = new R_Exception();
            PMR00170SummaryResultWithHeaderDTO loRtn = new PMR00170SummaryResultWithHeaderDTO();
            PMR00170Cls? loCls = null;
            PMR00170SummaryResultDTO? loData = null;
            CultureInfo loCultureInfo = new CultureInfo(R_BackGlobalVar.REPORT_CULTURE);
            try
            {
                //Add Resources
                loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Page", loCultureInfo);
                loRtn.BaseHeaderColumn.Of = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_Date = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_By", loCultureInfo);

                loCls = new PMR00170Cls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParam.CLANG_ID = R_BackGlobalVar.CULTURE;
                var loCollectionFromDb = loCls.GetLOCStatusSummaryReportData(poParam);

                _logger.LogInfo("Set BaseHeader Report");
                var loBaseHeader = new BaseHeaderDTO()
                {
                    CPRINT_CODE = "003",
                    CPRINT_NAME = "LOC Event",
                    CUSER_ID = poParam.CUSER_ID!,
                };
                loBaseHeader.BLOGO_COMPANY = loCls.GetCompanyLogo(R_BackGlobalVar.COMPANY_ID).CLOGO;
                loBaseHeader.CCOMPANY_NAME = loCls.GetCompanyName(R_BackGlobalVar.COMPANY_ID).CCOMPANY_NAME;

                _logger.LogInfo("Set Header Report");

                DateTime ldFromDate = DateTime.ParseExact(poParam.CFROM_PERIOD, "yyyyMM", null), ldToDate = DateTime.ParseExact(poParam.CTO_PERIOD, "yyyyMM", null);
                var loHeader = new PMR00170DataHeaderDTO()
                {
                    PROPERTY = $"{poParam.CPROPERTY_ID} - {poParam.CPROPERTY_NAME}",
                    CDEPT_REPORT_DISPLAY = poParam.CFROM_DEPARTMENT_ID != poParam.CTO_DEPARTMENT_ID
                    ? $"{poParam.CFROM_DEPARTMENT_NAME} ({poParam.CFROM_DEPARTMENT_ID}) - {poParam.CTO_DEPARTMENT_NAME}({poParam.CTO_DEPARTMENT_ID})"
                    : $"{poParam.CFROM_DEPARTMENT_NAME} ({poParam.CFROM_DEPARTMENT_ID})",
                    CSALESMAN_REPORT_DISPLAY = poParam.CFROM_SALESMAN_ID != poParam.CTO_SALESMAN_ID ? $"{poParam.CFROM_SALESMAN_NAME} ({poParam.CFROM_SALESMAN_ID}) - {poParam.CTO_SALESMAN_NAME} ({poParam.CTO_SALESMAN_ID})" : $"{poParam.CFROM_SALESMAN_NAME} ({poParam.CFROM_SALESMAN_ID})",
                    CPERIOD_DISPLAY = ldFromDate != ldToDate ? $"{ldFromDate:MMM yyyy} – {ldToDate:MMM yyyy}" : $"{ldFromDate:MMM yyyy}",
                    CREPORT_NAME = poParam.CREPORT_NAME
                };
                var loColumn = AssignValuesWithMessages(typeof(PMR00170BackResources.Resources_PMR00170), loCultureInfo, new PMR00170ColumnSummaryDTO());
                var loLabel = AssignValuesWithMessages(typeof(PMR00170BackResources.Resources_PMR00170), loCultureInfo, new PMR00170LabelDTO());

                _logger.LogInfo("Convert data to Format Print");
                //ASSIGN VALUE
                loData = new PMR00170SummaryResultDTO
                {
                    Title = "LOC Event",
                    ColumnSummary = (PMR00170ColumnSummaryDTO)loColumn,
                    Header = loHeader,
                    Label = (PMR00170LabelDTO)loLabel,

                    //CONVERT DATA TO DISPLAY IF DATA EXIST
                    Data = loCollectionFromDb.Any() ? ConvertResultToFormatPrint(loCollectionFromDb) : new List<PMR00170DataTransactionDTO>()
                };

                _logger.LogInfo("Set Data Report");
                loRtn.BaseHeaderData = loBaseHeader;
                loRtn.PMR00170SummaryResulDataFormatDTO = loData;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("END Method GenerateDataPrint on Controller");

            return loRtn;
        }

        #endregion Helper

        #region Utilities

        private static DateTime? ConvertStringToDateTimeFormat(string? pcEntity)
        {
            CultureInfo loCultureInfo = new(R_BackGlobalVar.REPORT_CULTURE);

            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                return null;
            }
            else
            {
                DateTime result;
                if (pcEntity.Length == 6)
                {
                    pcEntity += "01";
                }

                if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", loCultureInfo, DateTimeStyles.None, out result))
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        //Helper Assign Object
        private object AssignValuesWithMessages(Type poResourceType, CultureInfo poCultureInfo, object poObject)
        {
            object loObj = Activator.CreateInstance(poObject.GetType())!;
            var loGetPropertyObject = poObject.GetType().GetProperties();

            foreach (var property in loGetPropertyObject)
            {
                string propertyName = property.Name;
                string message = R_Utility.R_GetMessage(poResourceType, propertyName, poCultureInfo);
                property.SetValue(loObj, message);
            }

            return loObj;
        }

        #endregion Utilities

        private List<PMR00170DataTransactionDTO> ConvertResultToFormatPrint(List<PMR00170DataSummaryDbDTO> poCollectionDataRaw)
        {
            var loException = new R_Exception();
            string lcMethodName = nameof(ConvertResultToFormatPrint);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START method {0} ", lcMethodName));
            List<PMR00170DataTransactionDTO> loReturn = new List<PMR00170DataTransactionDTO>();

            try
            {
                var loTempData = poCollectionDataRaw
               .GroupBy(x => new
               {
                   x.CTRANS_CODE,
                   x.CTRANS_NAME,
               }).Select(itemData1 => new PMR00170DataTransactionDTO
               {
                   CTRANS_CODE = itemData1.Key.CTRANS_CODE,
                   CTRANS_NAME = itemData1.Key.CTRANS_NAME,
                   TransactionDetail = itemData1
                   .GroupBy(x => new
                   {
                       x.CSALESMAN_ID,
                       x.CSALESMAN_NAME,
                   }).Select(itemData2 => new PMR00170DataTransactionDetailDTO
                   {
                       CSALESMAN_ID = itemData2.Key.CSALESMAN_ID,
                       CSALESMAN_NAME = itemData2.Key.CSALESMAN_NAME,
                       SalesmanDetail = itemData2
                       .Select(x => new PMR00170DataSalesmanDetailDTO
                       {
                           CREF_NO = x.CREF_NO,
                           CREF_DATE = x.CREF_DATE,
                           DREF_DATE = ConvertStringToDateTimeFormat(x.CREF_DATE!),
                           CTENURE = x.CTENURE,
                           NTOTAL_GROSS_AREA_SIZE = x.NTOTAL_GROSS_AREA_SIZE,
                           NTOTAL_NET_AREA_SIZE = x.NTOTAL_NET_AREA_SIZE,
                           NTOTAL_COMMON_AREA_SIZE = x.NTOTAL_COMMON_AREA_SIZE,
                           CAGREEMENT_STATUS_NAME = x.CAGREEMENT_STATUS_NAME,
                           CTRANS_STATUS_NAME = x.CTRANS_STATUS_NAME,
                           NTOTAL_PRICE = x.NTOTAL_PRICE,
                           CTAX = x.CTAX,
                           CTENANT_ID = x.CTENANT_ID,
                           CTENANT_NAME = x.CTENANT_NAME,
                           CTC_MESSAGE = x.CTC_MESSAGE
                       }).ToList(),
                   }).ToList(),
               }).ToList();

                loReturn = loTempData;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return loReturn;
        }
    }
}