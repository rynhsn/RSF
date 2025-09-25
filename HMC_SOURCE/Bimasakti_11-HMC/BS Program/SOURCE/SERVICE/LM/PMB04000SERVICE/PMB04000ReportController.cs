using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMB04000BACK;
using PMB04000COMMON.DTO.Utilities;
using PMB04000COMMON.Logs;
using R_BackEnd;
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
using PMB04000SERVICE.DTOLogs;
using R_Cache;
using System.Globalization;
using PMB04000COMMON.Print;
using Microsoft.AspNetCore.Http;
using PMB04000BackResources;
using PMB04000COMMONPrintBatch.ParamDTO;
using PMB04000COMMONPrintBatch;
using System.Data.Common;
using System.Data;

namespace PMB04000SERVICE
{
    public class PMB04000ReportController : R_ReportControllerBase
    {
        #region instantiate

        private R_ReportFastReportBackClass _ReportCls;
        private PMB04000ParamReportDTO _Parameter;
        private LoggerPMB04000 _logger;
        private readonly ActivitySource _activitySource;


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public PMB04000ReportController(ILogger<PMB04000ReportController> logger)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            //Initial and Get instance
            LoggerPMB04000.R_InitializeLogger(logger);
            _logger = LoggerPMB04000.R_GetInstanceLogger();
            _activitySource = PMB04000Activity.R_InitializeAndGetActivitySource(nameof(PMB04000ReportController));
            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }

        #endregion
        #region Event Handler
        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "PMB04000.frx");
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
        public R_DownloadFileResultDTO ReceiptReportPost(PMB04000ParamReportDTO poParameter)
        {
            string lcMethodName = nameof(ReceiptReportPost);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START method {0} ", lcMethodName));

            PMB04000ReportLogKeyDTO<PMB04000ParamReportDTO> loCache = null;
            R_Exception loException = new R_Exception();
            R_DownloadFileResultDTO? loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new PMB04000ReportLogKeyDTO<PMB04000ParamReportDTO>
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
        public FileStreamResult ReceiptReportGet(string pcGuid)
        {
            string lcMethodName = nameof(ReceiptReportGet);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START method {0}", lcMethodName));

            PMB04000ReportLogKeyDTO<PMB04000ParamReportDTO> loDataFromRedis = null;
            R_Exception loException = new R_Exception();
            FileStreamResult? loRtn = null;
            try
            {
                //Get Parameter
                loDataFromRedis = R_NetCoreUtility.R_DeserializeObjectFromByte<PMB04000ReportLogKeyDTO<PMB04000ParamReportDTO>>(R_DistributedCache.Cache.Get(pcGuid));

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
        private PMB04000ResultDataDTO GenerateDataPrint(PMB04000ParamReportDTO poParam)
        {
            string lcMethodName = nameof(GenerateDataPrint);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START method {0}", lcMethodName));

            var loException = new R_Exception();
            PMB04000ResultDataDTO loRtn = new PMB04000ResultDataDTO();
            PMB04000PrintCls? loCls = null;
            //PMR00150SummaryResultDTO? loData = null;
            CultureInfo loCultureInfo = new CultureInfo(R_BackGlobalVar.REPORT_CULTURE);
            try
            {
                loCls = new PMB04000PrintCls();
                var loCollectionFromDb = loCls.GetReportReceiptData(poParam);
                _logger.LogInfo("Set BaseHeader Report");

                PMB04000DataReportDTO? loDataFirst = loCollectionFromDb.Any() == true ? loCollectionFromDb.FirstOrDefault() : new PMB04000DataReportDTO()!;

                PMB04000BaseHeaderDTO loHeader = new();
                var loGetLogo = loCls.GetLogoCompany(poParam);
                //SET LOGO DAN HEADER
                _logger.LogInfo("Set LOGO AND HEADER Report");

                loHeader.KWITANSI = R_Utility.R_GetMessage(typeof(Resources_PMB04000), "Kwitansi", loCultureInfo);
                loHeader.CLOGO = loGetLogo.CLOGO!;

                _logger.LogInfo("Set Column and Label Report");
                var loColumn = AssignValuesWithMessages(typeof(Resources_PMB04000), loCultureInfo, new PMB04000ColumnDTO());
                var loLabel = AssignValuesWithMessages(typeof(Resources_PMB04000), loCultureInfo, new PMB04000LabelDTO());


                //CONVERT DATA TO DISPLAY IF DATA EXIST
                var oListData = loCollectionFromDb.Any() ? ConvertResultToFormatPrint(loCollectionFromDb) : new List<PMB04000DataReportDTO>();

                //Assign DATA
                _logger.LogInfo("Set Data Report");
                loRtn.Header = loHeader;
                loRtn.Column = (PMB04000ColumnDTO)loColumn;
                loRtn.Label = (PMB04000LabelDTO)loLabel;
                loRtn.Data = oListData;
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
        private List<PMB04000DataReportDTO> ConvertResultToFormatPrint(List<PMB04000DataReportDTO> poCollectionDataRaw)
        {
            var loException = new R_Exception();
            string lcMethodName = nameof(ConvertResultToFormatPrint);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START method {0} ", lcMethodName));
            List<PMB04000DataReportDTO> loReturn = poCollectionDataRaw;

            try
            {
                foreach (var item in loReturn)
                {
                    item.DINVOICE_DATE = ConvertStringToDateTimeFormat(item.CINVOICE_DATE);
                    item.DTODAY_DATE = ConvertStringToDateTimeFormat(item.CTODAY_DATE);
                }
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

       
    }


}
