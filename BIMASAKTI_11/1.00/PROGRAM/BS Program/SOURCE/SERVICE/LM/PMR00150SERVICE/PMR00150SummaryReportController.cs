using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR00150BACK;
using PMR00150COMMON;
using PMR00150COMMON.Utility_Report;
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
using System.Text;
using System.Threading.Tasks;
using PMR00150SERVICE.DTOLogs;
using BaseHeaderReportCOMMON;
using System.Globalization;

namespace PMR00150SERVICE
{
    public class PMR00150SummaryReportController : R_ReportControllerBase
    {
        #region instantiate

        private R_ReportFastReportBackClass _ReportCls;
        private PMR00150DBParamDTO _Parameter;
        private LoggerPMR00150 _logger;
        private readonly ActivitySource _activitySource;


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public PMR00150SummaryReportController(ILogger<PMR00150SummaryReportController> logger)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            //Initial and Get instance
            LoggerPMR00150.R_InitializeLogger(logger);
            _logger = LoggerPMR00150.R_GetInstanceLogger();
            _activitySource = PMR00150Activity.R_InitializeAndGetActivitySource(nameof(PMR00150SummaryReportController));

            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }

        #endregion

        #region Event Handler

        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "PMR00150Summary.frx");
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

        #region Method Get Post
        [HttpPost]
        public R_DownloadFileResultDTO SummaryReportPost(PMR00150DBParamDTO poParameter)
        {
            string lcMethodName = nameof(SummaryReportPost);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START method {0} ", lcMethodName));

            PMR00150ReportLogKeyDTO<PMR00150DBParamDTO> loCache = null;
            R_Exception loException = new R_Exception();
            R_DownloadFileResultDTO? loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new PMR00150ReportLogKeyDTO<PMR00150DBParamDTO>
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY),
                    poReportGlobalDTO = R_ReportGlobalVar.R_GetReportDTO()
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

            PMR00150ReportLogKeyDTO<PMR00150DBParamDTO> loDataFromRedis = null;
            R_Exception loException = new R_Exception();
            FileStreamResult? loRtn = null;
            try
            {
                //Get Parameter
                loDataFromRedis = R_NetCoreUtility.R_DeserializeObjectFromByte<PMR00150ReportLogKeyDTO<PMR00150DBParamDTO>>(R_DistributedCache.Cache.Get(pcGuid));

                //Get Data and Set Log Key
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loDataFromRedis.poLogKey);
                R_ReportGlobalVar.R_SetFromReportDTO(loDataFromRedis.poReportGlobalDTO);
                _Parameter = loDataFromRedis.poParam!;


                _logger.LogInfo(string.Format("READ file report method {0}", lcMethodName));
                _logger.LogDebug("READ poParameter {@objectQuery}", _Parameter);
                R_FileType loFileType = new();
                if (loDataFromRedis.poParam.LIS_PRINT)
                {
                    _logger.LogInfo(string.Format("Normal Print"));
                    //  loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
                    loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF), R_ReportUtility.GetMimeType(R_FileType.PDF));

                }
                else
                {
                    _logger.LogInfo(string.Format("Save As"));
                    switch (loDataFromRedis.poParam.CREPORT_FILETYPE)
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
                    loRtn = File(_ReportCls.R_GetStreamReport(peExport: loFileType), R_ReportUtility.GetMimeType(loFileType), $"{loDataFromRedis.poParam.CREPORT_FILENAME}.{loDataFromRedis.poParam.CREPORT_FILETYPE}");

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
        #endregion

        #region Helper
        private PMR00150SummaryResultWithHeaderDTO GenerateDataPrint(PMR00150DBParamDTO poParam)
        {
            string lcMethodName = nameof(GenerateDataPrint);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START method {0}", lcMethodName));

            var loException = new R_Exception();
            PMR00150SummaryResultWithHeaderDTO loRtn = new PMR00150SummaryResultWithHeaderDTO();
            PMR00150Cls? loCls = null;
            PMR00150SummaryResultDTO? loData = null;
            CultureInfo loCultureInfo = new CultureInfo(R_BackGlobalVar.REPORT_CULTURE);
            try
            {
                //Add Resources
                loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Page", loCultureInfo);
                loRtn.BaseHeaderColumn.Of = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_Date = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_By", loCultureInfo);

                loCls = new PMR00150Cls();
                var loCollectionFromDb = loCls.GetLOCStatusSummaryReportData(poParam);

                _logger.LogInfo("Set BaseHeader Report");
                //GETLOGO
                var loBaseHeader = loCls.GetPropertyLogo(poParam);
                var loParamHeader = new BaseHeaderDTO()
                {
                    CCOMPANY_NAME = loBaseHeader.CCOMPANY_NAME!,
                    CPRINT_CODE = R_BackGlobalVar.PROGRAM_ID.ToUpper(),
                    CPRINT_NAME = "LOC List",
                    CUSER_ID = !string.IsNullOrEmpty(poParam.CUSER_ID) ? poParam.CUSER_ID : "",
                    BLOGO_COMPANY = loBaseHeader.CLOGO!,
                    DPRINT_DATE_COMPANY = DateTime.ParseExact(loBaseHeader.CDATETIME_NOW!, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture)
                };
                _logger.LogInfo("Set Header Report");
                var loHeader = new PMR00150DataHeaderDTO()
                {
                    PROPERTY = poParam.CPROPERTY_NAME,
                    FROM_DEPARTMENT = poParam.CFROM_DEPARTMENT_NAME + $"({poParam.CFROM_DEPARTMENT_ID})",
                    TO_DEPARTMENT = poParam.CTO_DEPARTMENT_NAME + $"({poParam.CTO_DEPARTMENT_ID})",
                    FROM_SALESMEN = poParam.CFROM_SALESMAN_NAME + $"({poParam.CFROM_SALESMAN_ID})",
                    TO_SALESMEN = poParam.CTO_SALESMAN_NAME + $"({poParam.CTO_SALESMAN_ID})",
                    FROM_PERIOD = poParam.CFROM_PERIOD_NAME,
                    TO_PERIOD = poParam.CTO_PERIOD_NAME,
                    CREPORT_NAME = poParam.CREPORT_NAME
                };
                var loColumn = AssignValuesWithMessages(typeof(PMR00150BackResources.Resources_PMR00150), loCultureInfo, new PMR00150ColumnSummaryDTO());
                var loLabel = AssignValuesWithMessages(typeof(PMR00150BackResources.Resources_PMR00150), loCultureInfo, new PMR00150LabelDTO());

                _logger.LogInfo("Convert data to Format Print");
                //ASSIGN VALUE
                loData = new PMR00150SummaryResultDTO();
                loData.Title = "LOC List";
                loData.ColumnSummary = (PMR00150ColumnSummaryDTO)loColumn;
                loData.Header = loHeader;
                loData.Label = (PMR00150LabelDTO)loLabel;

                //CONVERT DATA TO DISPLAY IF DATA EXIST
                loData.Data = loCollectionFromDb.Any() ? ConvertResultToFormatPrint(loCollectionFromDb) : new List<PMR00150DataTransactionDTO>();

                _logger.LogInfo("Set Data Report");
                loRtn.BaseHeaderData = loParamHeader;
                loRtn.PMR00150SummaryResulDataFormatDTO = loData;
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
        #endregion

        #region Utilities
        private static DateTime? ConvertStringToDateTimeFormat(string? pcEntity)
        {
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

                if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }
        public static string ConvertStringToDate(string? dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString))
                return ""; // Handle jika null atau kosong

            // Coba parsing dengan format yyyyMMdd
            if (DateTime.TryParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                return parsedDate.ToString("dd-MMM-yyyy", new CultureInfo("en-US"));
            }

            return ""; // Handle jika parsing gagal
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

        #endregion
        private List<PMR00150DataTransactionDTO> ConvertResultToFormatPrint(List<PMR00150DataSummaryDbDTO> poCollectionDataRaw)
        {
            var loException = new R_Exception();
            string lcMethodName = nameof(ConvertResultToFormatPrint);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START method {0} ", lcMethodName));
            List<PMR00150DataTransactionDTO> loReturn = new List<PMR00150DataTransactionDTO>();

            try
            {

                var loTempData = poCollectionDataRaw
               .GroupBy(x => new
               {
                   x.CTRANS_CODE,
                   x.CTRANS_NAME,
               }).Select(itemData1 => new PMR00150DataTransactionDTO
               {
                   CTRANS_CODE = itemData1.Key.CTRANS_CODE,
                   CTRANS_NAME = itemData1.Key.CTRANS_NAME,
                   TransactionDetail = itemData1
                   .GroupBy(x => new
                   {
                       x.CSALESMAN_ID,
                       x.CSALESMAN_NAME,
                   }).Select(itemData2 => new PMR00150DataTransactionDetailDTO
                   {
                       CSALESMAN_ID = itemData2.Key.CSALESMAN_ID,
                       CSALESMAN_NAME = itemData2.Key.CSALESMAN_NAME,
                       SalesmanDetail = itemData2
                       .Select(x => new PMR00150DataSalesmanDetailDTO
                       {
                           CREF_NO = x.CREF_NO,
                           CREF_DATE = ConvertStringToDate(x.CREF_DATE),
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
