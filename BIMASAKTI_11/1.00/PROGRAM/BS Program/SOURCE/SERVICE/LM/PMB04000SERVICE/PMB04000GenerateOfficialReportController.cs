using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMB04000BACK;
using PMB04000BackResources;
using PMB04000COMMON.Logs;
using PMB04000COMMONPrintBatch;
using PMB04000COMMONPrintBatch.ParamDTO;
using PMB04000SERVICE.DTOLogs;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;
using R_ReportServerClient;
using R_ReportServerCommon;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Xml.Linq;
using R_FileType = R_ReportServerCommon.R_FileType;

namespace PMB04000Service
{
    public class PMB04000PrintReportInvoiceController : R_ReportControllerBase
    {
        //private R_ReportFastReportBackClass _ReportCls;
        //private PMB04000ParamReportDTO _poParam;
        private readonly LoggerPMB04000? _loggerPMB04000;
        private readonly ActivitySource _activitySource;

        #region Instantiate

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public PMB04000PrintReportInvoiceController(ILogger<LoggerPMB04000> logger)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            LoggerPMB04000.R_InitializeLogger(logger);
            _loggerPMB04000 = LoggerPMB04000.R_GetInstanceLogger();
            _activitySource =
                PMB04000Activity.R_InitializeAndGetActivitySource(nameof(PMB04000PrintReportInvoiceController));
        }

        private ReportFormatDTO GetReportFormat()
        {
            return new ReportFormatDTO()
            {
                _DecimalPlaces = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_PLACES,
                _DecimalSeparator = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_SEPARATOR,
                _GroupSeparator = R_BackGlobalVar.REPORT_FORMAT_GROUP_SEPARATOR,
                _ShortDate = R_BackGlobalVar.REPORT_FORMAT_SHORT_DATE,
                _ShortTime = R_BackGlobalVar.REPORT_FORMAT_SHORT_TIME,
                _LongDate = R_BackGlobalVar.REPORT_FORMAT_LONG_DATE,
                _LongTime = R_BackGlobalVar.REPORT_FORMAT_LONG_TIME,
            };
        }

        #endregion

        [HttpPost]
        public R_DownloadFileResultDTO PMB04000ReportPost(PMB04000ParamReportDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            _loggerPMB04000.LogInfo("Start PMB04000ReportPost with report parameters.");

            R_DownloadFileResultDTO loRtn = new R_DownloadFileResultDTO();
            try
            {
                // Create cache object
                var loCache = new PMB04000ReportLogKeyDTO<PMB04000ParamReportDTO>
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY),
                    poReportGlobalDTO = R_ReportGlobalVar.R_GetReportDTO()
                };

                // Serialize cache data to byte array
                var loSerializedCache = R_NetCoreUtility.R_SerializeObjectToByte(loCache);
                if (loSerializedCache == null || loSerializedCache.Length == 0)
                {
                    throw new InvalidOperationException("Failed to serialize cache data.");
                }

                // Logging cache details
                _loggerPMB04000.LogDebug("Serialized data length: {0} bytes.", loSerializedCache.Length);

                // Set cache and log GUID
                R_DistributedCache.R_Set(loRtn.GuidResult, loSerializedCache);
                _loggerPMB04000.LogInfo("Cache set successfully with GUID: {0}", loRtn.GuidResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerPMB04000.LogError("Error in PMB04000ReportPost", ex);
            }

            // Throw if there are any exceptions
            loEx.ThrowExceptionIfErrors();
            _loggerPMB04000.LogInfo("End PMB04000ReportPost successfully.");
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public async Task<FileContentResult> PMB04000ReportGet(string pcGuid)
        {
            R_Exception loEx = new R_Exception();
            string lcMethodName = nameof(PMB04000ReportGet);
            _loggerPMB04000.LogInfo("Start PMB04000ReportGet process with GUID: {0}", pcGuid);

            FileContentResult? loRtn = null;
            PMB04000ResultDataDTO? loData = null;
            R_ReportServerCommon.R_FileType leReportOutputType;
            List<PMB04000DataReportDTO>? loPrintResults = null;
            string lcExtension;

            try
            {
                // Fetching Parameter from Cache
                _loggerPMB04000.LogInfo("Attempting to fetch report data from cache using GUID: {0}", pcGuid);
                var cacheData = R_DistributedCache.Cache.Get(pcGuid);

                if (cacheData == null)
                {
                    _loggerPMB04000.LogInfo("Report data not found in cache for GUID: {0}", pcGuid);
                    return null!;
                }

                _loggerPMB04000.LogInfo("Successfully retrieved data from cache, GUID: {0}", pcGuid);

                // Deserialize cache data
                var loResultGUID =
                    R_NetCoreUtility.R_DeserializeObjectFromByte<PMB04000ReportLogKeyDTO<PMB04000ParamReportDTO>>(
                        cacheData);
                if (loResultGUID?.poParam == null)
                {
                    _loggerPMB04000.LogError("Failed to deserialize cache data or missing parameters.");
                    return null!;
                }

                _loggerPMB04000.LogInfo("Successfully deserialized cache data");

                // Set logging context
#pragma warning disable CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
#pragma warning restore CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.
                R_ReportGlobalVar.R_SetFromReportDTO(loResultGUID.poReportGlobalDTO);
                _loggerPMB04000.LogInfo("Logging context and global variables set");


                //DISINI TITIK PERBEDAANNYA
                var loParam = loResultGUID.poParam;
                // Prepare Data
                //var loCls = new PMB04000Cls();

                var loParameterGetReportReceiptDataToDb = new PMB04000ParamReportDTO
                {
                    CCOMPANY_ID = loParam.CCOMPANY_ID,
                    CPROPERTY_ID = loParam.CPROPERTY_ID,
                    CDEPT_CODE = loParam.CDEPT_CODE,
                    CREF_NO = loParam.CREF_NO,
                    CUSER_ID = loParam.CUSER_ID,
                    CLANG_ID = loParam.CLANG_ID,
                    LPRINT = loParam.LPRINT,
                };

                var loCls = new PMB04000PrintCls();
                _loggerPMB04000.LogInfo("Before to  method GetReportReceiptDataPrintBatch");
                loPrintResults = loCls.GetReportReceiptData(loParameterGetReportReceiptDataToDb);
                _loggerPMB04000!.LogInfo("Start Single process");

                _loggerPMB04000.LogInfo("panggil program GenerateDataPrint", lcMethodName);
                loData = GenerateDataPrint(loParameterGetReportReceiptDataToDb); //
                //Set Report Rule and Name
                var loReportRule = new R_ReportServerRule(R_BackGlobalVar.TENANT_ID.ToLower(),
                    R_BackGlobalVar.COMPANY_ID.ToLower());
                _loggerPMB04000.LogDebug("Report Server Rule initialized", loReportRule);

                string lcReportFileName = "PMB04000.frx";
                _loggerPMB04000.LogInfo("Get file Frx");
                _loggerPMB04000.LogDebug("Get file Frx", lcReportFileName);

                //Prepare Parameter
                _loggerPMB04000!.LogInfo("Get Parameter");
                leReportOutputType = R_ReportServerCommon.R_FileType.PDF;
                lcExtension = Enum.GetName(typeof(R_ReportServerCommon.R_FileType), leReportOutputType)!;
                _loggerPMB04000.LogInfo("Create object Report Parameter");
                var loReportFortmat = GetReportFormat();

                var loParameter = new R_GenerateReportParameter()
                {
                    ReportRule = loReportRule,
                    ReportFileName = lcReportFileName,
                    ReportData = JsonSerializer.Serialize<PMB04000ResultDataDTO>(loData),
                    ReportDataSourceName = "ResponseDataModel",
                    ReportFormat =
                        R_Utility.R_ConvertObjectToObject<ReportFormatDTO, R_ReportServerCommon.R_ReportFormatDTO>(
                            loReportFortmat),
                    ReportDataType = typeof(PMB04000ResultDataDTO).ToString(),
                    ReportOutputType = leReportOutputType,
                    ReportAssemblyName = "PMB04000COMMONPrintBatch.dll",
                    ReportParameter = null
                };
                _loggerPMB04000.LogDebug("{@Parameter}", loParameter);
                _loggerPMB04000!.LogInfo("Succes get Parameter");

                //GENERATE REPORT
                _loggerPMB04000!.LogInfo("Call method [R_GenerateReportByte] to Genearate Report");
                byte[] loRtnInByte = await R_ReportServerUtility.R_GenerateReportByte(
                    R_ReportServerClientService.R_GetHttpClient(),
                    "api/ReportServer/GetReport", loParameter); //menghubungkan ke API dari engine yang sudah ada
                _loggerPMB04000.LogDebug("Report yang telah menjadi byte:", loRtnInByte);

                // Determine file type and format for response
                if (loParam.LIS_PRINT)
                {
                    loRtn = File(loRtnInByte,
                        R_ReportUtility.GetMimeType((R_ReportFastReportBack.R_FileType)R_FileType.PDF));
                }
                else
                {
                    _loggerPMB04000.LogDebug("Determined file type for response: {0}", leReportOutputType);

                    loRtn = File(
                        loRtnInByte,
                        R_ReportUtility.GetMimeType((R_ReportFastReportBack.R_FileType)leReportOutputType),
                        $"{loParam.CREPORT_FILENAME}.{lcExtension}"
                    );
                }

                _loggerPMB04000.LogInfo("Report file successfully generated for PMB04000ReportGet");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerPMB04000.LogError("An error occurred in PMB04000ReportGet", ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn!;
        }

        #region Helper

        private PMB04000ResultDataDTO GenerateDataPrint(PMB04000ParamReportDTO poParam)
        {
            string lcMethodName = nameof(GenerateDataPrint);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerPMB04000.LogInfo(string.Format("START method {0}", lcMethodName));

            var loException = new R_Exception();
            PMB04000ResultDataDTO loRtn = new PMB04000ResultDataDTO();
            PMB04000PrintCls? loCls = null;
            //PMR00150SummaryResultDTO? loData = null;
            CultureInfo loCultureInfo = new CultureInfo(R_BackGlobalVar.REPORT_CULTURE);
            try
            {
                loCls = new PMB04000PrintCls();
                var loCollectionFromDb = loCls.GetReportReceiptData(poParam);
                _loggerPMB04000.LogInfo("Set BaseHeader Report");

                PMB04000DataReportDTO? loDataFirst = loCollectionFromDb.Any() == true
                    ? loCollectionFromDb.FirstOrDefault()
                    : new PMB04000DataReportDTO()!;

                PMB04000BaseHeaderDTO loHeader = new();
                var loGetLogo = loCls.GetLogoCompany(poParam);
                //SET LOGO DAN HEADER
                _loggerPMB04000.LogInfo("Set LOGO AND HEADER Report");

                loHeader.KWITANSI = R_Utility.R_GetMessage(typeof(Resources_PMB04000), "Kwitansi", loCultureInfo);
                loHeader.CLOGO = loGetLogo.CLOGO!;

                _loggerPMB04000.LogInfo("Set Column and Label Report");
                var loColumn =
                    AssignValuesWithMessages(typeof(Resources_PMB04000), loCultureInfo, new PMB04000ColumnDTO());
                var loLabel =
                    AssignValuesWithMessages(typeof(Resources_PMB04000), loCultureInfo, new PMB04000LabelDTO());


                //CONVERT DATA TO DISPLAY IF DATA EXIST
                var oListData = loCollectionFromDb.Any()
                    ? ConvertResultToFormatPrint(loCollectionFromDb)
                    : new List<PMB04000DataReportDTO>();

                #region GetOsign Byte Array
                

                var osign01 = poParam.CSTORAGE_ID01 != null || poParam.CSIGN_ID01 != null ? loCls.GetSignStorage(poParam.CSTORAGE_ID01).Data : null;
                var osign02 = poParam.CSTORAGE_ID02 != null || poParam.CSIGN_ID01 != null ? loCls.GetSignStorage(poParam.CSTORAGE_ID02).Data : null;
                var osign03 = poParam.CSTORAGE_ID03 != null || poParam.CSIGN_ID01 != null ? loCls.GetSignStorage(poParam.CSTORAGE_ID03).Data : null;
                var osign04 = poParam.CSTORAGE_ID04 != null || poParam.CSIGN_ID01 != null ? loCls.GetSignStorage(poParam.CSTORAGE_ID04).Data : null;
                var osign05 = poParam.CSTORAGE_ID05 != null || poParam.CSIGN_ID01 != null ? loCls.GetSignStorage(poParam.CSTORAGE_ID05).Data : null;
                var osign06 = poParam.CSTORAGE_ID06 != null || poParam.CSIGN_ID01 != null ? loCls.GetSignStorage(poParam.CSTORAGE_ID06).Data : null;
                

                oListData.ForEach(x =>
                {
                    x.CNAME01 = poParam.CSIGN_NAME01;
                    x.CNAME02 = poParam.CSIGN_NAME02;
                    x.CNAME03 = poParam.CSIGN_NAME03;
                    x.CNAME04 = poParam.CSIGN_NAME04;
                    x.CNAME05 = poParam.CSIGN_NAME05;
                    x.CNAME06 = poParam.CSIGN_NAME06;
                    x.CPOSITION01 = poParam.CSIGN_POSITION01;
                    x.CPOSITION02 = poParam.CSIGN_POSITION02;
                    x.CPOSITION03 = poParam.CSIGN_POSITION03;
                    x.CPOSITION04 = poParam.CSIGN_POSITION04;
                    x.CPOSITION05 = poParam.CSIGN_POSITION05;
                    x.CPOSITION06 = poParam.CSIGN_POSITION06;
                    x.OSIGN01 = osign01;
                    x.OSIGN02 = osign02;
                    x.OSIGN03 = osign03;
                    x.OSIGN04 = osign04;
                    x.OSIGN05 = osign05;
                    x.OSIGN06 = osign06;
                });

                #endregion

                //Assign DATA
                _loggerPMB04000.LogInfo("Set Data Report");
                loRtn.Header = loHeader;
                loRtn.Column = (PMB04000ColumnDTO)loColumn;
                loRtn.Label = (PMB04000LabelDTO)loLabel;
                loRtn.Data = oListData;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerPMB04000.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _loggerPMB04000.LogInfo("END Method GenerateDataPrint on Controller");

            return loRtn;
        }

        #endregion

        private List<PMB04000DataReportDTO> ConvertResultToFormatPrint(List<PMB04000DataReportDTO> poCollectionDataRaw)
        {
            var loException = new R_Exception();
            string lcMethodName = nameof(ConvertResultToFormatPrint);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerPMB04000.LogInfo(string.Format("START method {0} ", lcMethodName));
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
                _loggerPMB04000.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _loggerPMB04000.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
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

                if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                        out result))
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