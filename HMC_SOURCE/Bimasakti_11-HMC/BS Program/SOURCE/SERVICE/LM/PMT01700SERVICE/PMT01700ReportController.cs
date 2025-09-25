using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT01700BACK;
using PMT01700COMMON.DTO.Utilities.Print;
using PMT01700COMMON.Logs;
using PMT01700CommonReport;
using PMT01700SERVICE.DTOLogs;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;
using R_ReportServerClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using R_ReportServerCommon;
using System.Globalization;
using R_ReportFormatDTO = R_ReportServerCommon.R_ReportFormatDTO;

namespace PMT01700SERVICE
{
    public class PMT01700ReportController : R_ReportControllerBase
    {
        private ParameterPrintDTO _poParam;
        private readonly LoggerPMT01700? _logger;
        private readonly ActivitySource _activitySource;

        #region Instantiate
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public PMT01700ReportController(ILogger<PMT01700ReportController> logger)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            LoggerPMT01700.R_InitializeLogger(logger);
            _logger = LoggerPMT01700.R_GetInstanceLogger();
            _activitySource = PMT01700Activity.R_InitializeAndGetActivitySource(nameof(PMT01700ReportController));
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
        public R_DownloadFileResultDTO ReportPMT01700Post(ParameterPrintDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            _logger.LogInfo("Start ReportPMT01700Post with report parameters.");

            R_DownloadFileResultDTO loRtn = new R_DownloadFileResultDTO();
            try
            {
                // Create cache object
                var loCache = new ReportLogKeyDTO<ParameterPrintDTO>
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
                _logger.LogDebug("Serialized data length: {0} bytes.", loSerializedCache.Length);

                // Set cache and log GUID
                R_DistributedCache.R_Set(loRtn.GuidResult, loSerializedCache);
                _logger.LogInfo("Cache set successfully with GUID: {0}", loRtn.GuidResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError("Error in ReportPMT01700Post", ex);
            }

            // Throw if there are any exceptions
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End ReportPMT01700Post successfully.");
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public async Task<FileContentResult> ReportPMT01700Get(string pcGuid)
        {
            R_Exception loEx = new R_Exception();
            _logger.LogInfo("Start ReportPMT01700Get process with GUID: {0}", pcGuid);

            FileContentResult? loRtn = null;
            R_ReportServerCommon.R_FileType leReportOutputType;
            string lcExtension;

            try
            {
                // Fetching Parameter from Cache
                _logger.LogInfo("Attempting to fetch report data from cache using GUID: {0}", pcGuid);
                var cacheData = R_DistributedCache.Cache.Get(pcGuid);

                if (cacheData == null)
                {
                    _logger.LogInfo("Report data not found in cache for GUID: {0}", pcGuid);
                    return null!;
                }
                _logger.LogInfo("Successfully retrieved data from cache, GUID: {0}", pcGuid);

                // Deserialize cache data
                var loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<ReportLogKeyDTO<ParameterPrintDTO>>(cacheData);
                if (loResultGUID?.poParam == null)
                {
                    _logger.LogError("Failed to deserialize cache data or missing parameters.");
                    return null!;
                }
                _logger.LogInfo("Successfully deserialized cache data");

                // Set logging context
#pragma warning disable CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
#pragma warning restore CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.
                R_ReportGlobalVar.R_SetFromReportDTO(loResultGUID.poReportGlobalDTO);
                _logger.LogInfo("Logging context and global variables set");

                // Prepare Data
                var loCls = new PMT01700PrintCls();
                var loParameterGenerateData = new ParameterPrintDTO
                {
                    CCOMPANY_ID = loResultGUID.poParam.CCOMPANY_ID,
                    CPROPERTY_ID = loResultGUID.poParam.CPROPERTY_ID,
                    CDEPT_CODE = loResultGUID.poParam.CDEPT_CODE,
                    CTRANS_CODE = loResultGUID.poParam.CTRANS_CODE,
                    CREF_NO = loResultGUID.poParam.CREF_NO,
                    CUSER_ID = loResultGUID.poParam.CUSER_ID,
                    CFILE_NAME = loResultGUID.poParam.CFILE_NAME,
                    CTITLE = loResultGUID.poParam.CTITLE,
                    CPROGRAM_ID = loResultGUID.poParam.CPROGRAM_ID,
                    CTEMPLATE_ID = loResultGUID.poParam.CTEMPLATE_ID,
                };

                _logger.LogDebug("Generated parameters for data print", loParameterGenerateData);
                var loReportFormat = GetReportFormat();
                R_ReportFormatDTO loReportFormatConvert = new R_ReportFormatDTO()
                {
                    DecimalSeparator = loReportFormat._DecimalSeparator,
                    DecimalPlaces = loReportFormat._DecimalPlaces,
                    GroupSeparator = loReportFormat._GroupSeparator,
                    ShortDate = loReportFormat._ShortDate,
                    ShortTime = loReportFormat._ShortTime,
                    LongDate = loReportFormat._LongDate,
                    LongTime = loReportFormat._LongTime,
                };


                PMTResultDataReportDTO loData = loCls.GenerateDataPrint(loResultGUID.poParam);
                loData.Data.CREF_DATE = ConvertStringToDate(dateString:loData.Data.CREF_DATE, ParamStringFormat:loReportFormatConvert.ShortDate);
                loData.Data.CSTART_DATE = ConvertStringToDate(dateString:loData.Data.CSTART_DATE, ParamStringFormat:loReportFormatConvert.ShortDate);
                loData.Data.CEND_DATE = ConvertStringToDate(dateString: loData.Data.CEND_DATE, ParamStringFormat: loReportFormatConvert.ShortDate);
                loData.Data.CEXPIRED_DATE = ConvertStringToDate(dateString: loData.Data.CEXPIRED_DATE, ParamStringFormat: loReportFormatConvert.ShortDate);
                loData.Data.CHO_PLAN_DATE = ConvertStringToDate(dateString: loData.Data.CHO_PLAN_DATE, ParamStringFormat: loReportFormatConvert.ShortDate);
                loData.Data.CHO_ACTUAL_DATE = ConvertStringToDate(dateString: loData.Data.CHO_ACTUAL_DATE, ParamStringFormat: loReportFormatConvert.ShortDate);
                loData.Data.CACTUAL_START_DATE = ConvertStringToDate(dateString: loData.Data.CACTUAL_START_DATE, ParamStringFormat: loReportFormatConvert.ShortDate);
                loData.Data.CACTUAL_END_DATE = ConvertStringToDate(dateString: loData.Data.CACTUAL_END_DATE, ParamStringFormat: loReportFormatConvert.ShortDate);
                loData.Data.CSOLD_DATE = ConvertStringToDate(dateString: loData.Data.CSOLD_DATE, ParamStringFormat: loReportFormatConvert.ShortDate);



                //loData.Data.
                _logger.LogInfo("Report data formatted for print");

                // Setup Report Parameters
                if (string.IsNullOrEmpty(loParameterGenerateData.CFILE_NAME))
                {
                    loEx.Add("", "Report Name not Supplied!");
                    goto EndBlock;
                }
                var lcReportFileName = loParameterGenerateData.CFILE_NAME.EndsWith(".frx")
                                    ? loParameterGenerateData.CFILE_NAME
                                    : $"{loParameterGenerateData.CFILE_NAME}.frx";
                _logger.LogDebug("Selected report template file: {0}", lcReportFileName);

                leReportOutputType = R_ReportServerCommon.R_FileType.PDF;
                lcExtension = Enum.GetName(typeof(R_ReportFastReportBack.R_FileType), leReportOutputType)!;

                var loReportRule = new R_ReportServerRule(R_BackGlobalVar.TENANT_ID.ToLower(), R_BackGlobalVar.COMPANY_ID.ToLower());
                _logger.LogDebug("Report Server Rule initialized", loReportRule);

                var loParameter = new R_GenerateReportParameter()
                {
                    ReportRule = loReportRule,
                    ReportFileName = lcReportFileName,
                    ReportData = JsonSerializer.Serialize(loData),
                    ReportDataSourceName = "ResponseDataModel",
                    ReportFormat = loReportFormatConvert,
                    ReportDataType = typeof(PMTResultDataReportDTO).ToString(),
                    ReportOutputType = leReportOutputType,
                    ReportAssemblyName = "PMT01700CommonReport.dll",
                    ReportParameter = null
                };
                //loParameter.ReportFormat.ShortDate = "dd-MMM-yyyy";
                _logger.LogInfo("Report parameters prepared successfully");

                // Generate Report as Byte Array
                _logger.LogInfo("Starting report generation as byte array...");
                byte[] loRtnInByte = await R_ReportServerUtility.R_GenerateReportByte(
                    R_ReportServerClientService.R_GetHttpClient(),
                    "api/ReportServer/GetReport",
                    loParameter
                );

                // Determine file type and format for response
                loRtn = File(loRtnInByte, R_ReportUtility.GetMimeType(R_ReportFastReportBack.R_FileType.PDF));

            EndBlock:
                _logger.LogInfo("Report file successfully generated for ReportPMT01700Get");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError("An error occurred in ReportPMT01700Get", ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn!;
        }
        #region ConvertStringDate
        public static string ConvertStringToDate(string? dateString, string ParamStringFormat)
        {
            if (string.IsNullOrWhiteSpace(dateString))
                return ""; // Handle jika null atau kosong

            // Coba parsing dengan format yyyyMMdd
            if (DateTime.TryParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                return parsedDate.ToString(ParamStringFormat, new CultureInfo("en-US"));
            }
            return ""; // Handle jika parsing gagal
        }
        #endregion

    }
}
