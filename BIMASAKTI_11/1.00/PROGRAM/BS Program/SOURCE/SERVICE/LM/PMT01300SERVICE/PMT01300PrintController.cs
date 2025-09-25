using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT01300BACK;
using PMT01300COMMON;
using PMT01300ReportCommon;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;
using R_ReportServerClient;
using R_ReportServerCommon;
using R_FileType = R_ReportFastReportBack.R_FileType;

namespace PMT01300SERVICE;

public class PMT01300PrintController : R_ReportControllerBase
{
    private PMT01300ReportParamDTO _poParam;
    private readonly LoggerPMT01300Print? _logger;
    private readonly ActivitySource _activitySource;

    public PMT01300PrintController(ILogger<LoggerPMT01300Print> logger)
    {
        LoggerPMT01300Print.R_InitializeLogger(logger);
        _logger = LoggerPMT01300Print.R_GetInstanceLogger();
        _activitySource =
            PMT01300PrintActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT01300PrintController));
    }

    private PMT01300PrintReportFormatDTO _getReportFormat()
    {
        PMT01300PrintReportFormatDTO loReportFormat = new PMT01300PrintReportFormatDTO();
        loReportFormat._DecimalSeparator = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_SEPARATOR;
        loReportFormat._GroupSeparator = R_BackGlobalVar.REPORT_FORMAT_GROUP_SEPARATOR;
        loReportFormat._DecimalPlaces = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_PLACES;
        loReportFormat._ShortDate = R_BackGlobalVar.REPORT_FORMAT_SHORT_DATE;
        loReportFormat._ShortTime = R_BackGlobalVar.REPORT_FORMAT_SHORT_TIME;
        return loReportFormat;
    }

    [HttpPost]
    public R_DownloadFileResultDTO PrintReportPost(PMT01300ReportParamDTO poParameter)
    {
        R_Exception loEx = new R_Exception();
        _logger.LogInfo("Start PrintReportPost with report parameters.");

        R_DownloadFileResultDTO loRtn = new R_DownloadFileResultDTO();
        try
        {
            // Create cache object
            var loCache = new PMT01300PrintLogKeyDTO<PMT01300ReportParamDTO>
            {
                poParam = poParameter,
                poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY),
                poReportGlobal = R_ReportGlobalVar.R_GetReportDTO()
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
            _logger.LogError("Error in PrintReportPost", ex);
        }

        // Throw if there are any exceptions
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End PrintReportPost successfully.");
        return loRtn;
    }

    [HttpGet, AllowAnonymous]
    public async Task<FileContentResult> PrintReportGet(string pcGuid)
    {
        R_Exception loEx = new R_Exception();
        _logger.LogInfo("Start PrintReportGet process with GUID: {0}", pcGuid);

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
            var loResultGUID =
                R_NetCoreUtility.R_DeserializeObjectFromByte<PMT01300PrintLogKeyDTO<PMT01300ReportParamDTO>>(cacheData);
            if (loResultGUID?.poParam == null)
            {
                _logger.LogError("Failed to deserialize cache data or missing parameters.");
                return null!;
            }

            _logger.LogInfo("Successfully deserialized cache data");

            // Set logging context
            R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
            R_ReportGlobalVar.R_SetFromReportDTO(loResultGUID.poReportGlobal);
            _logger.LogInfo("Logging context and global variables set");

            // Prepare Data
            var loCls = new PMT01300Cls();
            var loParameterGenerateData = new PMT01300ReportParamDTO()
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                CPROPERTY_ID = loResultGUID.poParam.CPROPERTY_ID,
                CDEPT_CODE = loResultGUID.poParam.CDEPT_CODE,
                CTRANS_CODE = loResultGUID.poParam.CTRANS_CODE,
                CREF_NO = loResultGUID.poParam.CREF_NO,
                CUSER_ID = R_BackGlobalVar.USER_ID,
                CFILE_NAME = loResultGUID.poParam.CFILE_NAME,
                CTITLE = loResultGUID.poParam.CTITLE,
            };

            _logger.LogDebug("Generated parameters for data print", loParameterGenerateData);

            // Generate and Format Report Data
            //_logger.LogInfo("Fetching report data for print...");
            //var loPrintResults = loCls.GetDataPrintPMTWorldDb(loParameterGenerateData);
            //_logger.LogInfo("Successfully fetched report data for print");

            var loData = loCls.GenerateDataPrint(loResultGUID.poParam);
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
            //loResultGUID.ParameterPrint.CINVOICE_TYPE switch
            //{
            //    "01" => "PrintInvoiceFormat01_Charge_and_Utility.frx",
            //    "02" or "08" => "PrintInvoiceFormat02_08_Deposit_Penalty.frx",
            //    "09" => "PrintInvoiceFormat09_Others.frx",
            //    _ => "PrintInvoiceFormat01_Charge_and_Utility.frx"
            //};
            _logger.LogDebug("Selected report template file: {0}", lcReportFileName);

            //leReportOutputType = loResultGUID.ParameterPrint.CREPORT_FILETYPE switch
            //{
            //    "XLSX" => R_FileType.XLSX,
            //    "PDF" => R_FileType.PDF,
            //    "XLS" => R_FileType.XLS,
            //    "CSV" => R_FileType.CSV,
            //    _ => R_FileType.PDF
            //};
            leReportOutputType = R_ReportServerCommon.R_FileType.PDF;
            lcExtension = Enum.GetName(typeof(R_FileType), leReportOutputType)!;

            var loReportFormat = _getReportFormat();

            var loReportRule = new R_ReportServerRule(R_BackGlobalVar.TENANT_ID.ToLower(),
                R_BackGlobalVar.COMPANY_ID.ToLower());
            _logger.LogDebug("Report Server Rule initialized", loReportRule);

            var loParameter = new R_GenerateReportParameter()
            {
                ReportRule = loReportRule,
                ReportFileName = lcReportFileName,
                ReportData = JsonSerializer.Serialize(loData),
                ReportDataSourceName = "ResponseDataModel",
                ReportFormat =
                    R_Utility.R_ConvertObjectToObject<PMT01300PrintReportFormatDTO, R_ReportServerCommon.R_ReportFormatDTO>(
                        loReportFormat),
                ReportDataType = typeof(PMT01300ReportResultDataDTO).ToString(),
                ReportOutputType = leReportOutputType,
                ReportAssemblyName = "PMT01300ReportCommon.dll",
                ReportParameter = null
            };

            _logger.LogInfo("Report parameters prepared successfully");

            // Generate Report as Byte Array
            _logger.LogInfo("Starting report generation as byte array...");
            byte[] loRtnInByte = await R_ReportServerUtility.R_GenerateReportByte(
                R_ReportServerClientService.R_GetHttpClient(),
                "api/ReportServer/GetReport",
                loParameter
            );

            // Determine file type and format for response
            loRtn = File(loRtnInByte, R_ReportUtility.GetMimeType((R_ReportFastReportBack.R_FileType)R_FileType.PDF));
            //if (loResultGUID.ParameterPrint.LIS_PRINT)
            //{
            //}
            //else
            //{
            //    _logger.LogDebug("Determined file type for response: {0}", leReportOutputType);

            //    loRtn = File(
            //        loRtnInByte,
            //        R_ReportUtility.GetMimeType((R_ReportFastReportBack.R_FileType)leReportOutputType),
            //        $"{loResultGUID.ParameterPrint.CREPORT_FILENAME}.{lcExtension}"
            //    );
            //}
            EndBlock:
            _logger.LogInfo("Report file successfully generated for PrintReportGet");
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError("An error occurred in PrintReportGet", ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn!;
    }
}

public class PMT01300PrintReportFormatDTO
{
    public int _DecimalPlaces { get; set; }
    public string? _DecimalSeparator { get; set; }
    public string? _GroupSeparator { get; set; }
    public string? _ShortDate { get; set; }
    public string? _ShortTime { get; set; }
    public string? _LongDate { get; set; }
    public string? _LongTime { get; set; }
}