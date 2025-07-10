using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using BaseHeaderResources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR03000Back;
using PMR03000Common;
using PMR03000Common.DTOs.Print;
using PMR03000Service.DTOs;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;
using R_ReportServerClient;
using R_ReportServerCommon;
using R_FileType = R_ReportFastReportBack.R_FileType;

namespace PMR03000Service;

public class PMR03000PrintController : R_ReportControllerBase
{
    private PMR03000ReportParamDTO _poParam;
    private readonly LoggerPMR03000 _logger;
    private readonly ActivitySource _activitySource;

    public PMR03000PrintController(ILogger<PMR03000PrintController> logger)
    {
        LoggerPMR03000.R_InitializeLogger(logger);
        _logger = LoggerPMR03000.R_GetInstanceLogger();
        _activitySource = PMR03000Activity.R_InitializeAndGetActivitySource(nameof(PMR03000PrintController));
    }

    private PMR03000PrintReportFormatDTO _getReportFormat()
    {
        var loReportFormat = new PMR03000PrintReportFormatDTO();
        loReportFormat._DecimalSeparator = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_SEPARATOR;
        loReportFormat._GroupSeparator = R_BackGlobalVar.REPORT_FORMAT_GROUP_SEPARATOR;
        loReportFormat._DecimalPlaces = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_PLACES;
        loReportFormat._ShortDate = R_BackGlobalVar.REPORT_FORMAT_SHORT_DATE;
        loReportFormat._ShortTime = R_BackGlobalVar.REPORT_FORMAT_SHORT_TIME;
        return loReportFormat;
    }

    [HttpPost]
    public R_DownloadFileResultDTO PrintReportPost(PMR03000ReportParamDTO poParameter)
    {
        R_Exception loEx = new R_Exception();
        _logger.LogInfo("Start PrintReportPost with report parameters.");

        R_DownloadFileResultDTO loRtn = new R_DownloadFileResultDTO();
        try
        {
            // Create cache object
            var loCache = new PMR03000PrintLogKeyDTO<PMR03000ReportParamDTO>
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
        var loCultureInfo = new CultureInfo(R_BackGlobalVar.REPORT_CULTURE);
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
                R_NetCoreUtility.R_DeserializeObjectFromByte<PMR03000PrintLogKeyDTO<PMR03000ReportParamDTO>>(cacheData);
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
            var loCls = new PMR03000PrintCls();
            var loParameterGenerateData = new PMR03000ReportParamDTO()
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                CPROPERTY_ID = loResultGUID.poParam.CPROPERTY_ID,
                CPERIOD = loResultGUID.poParam.CPERIOD,
                CFROM_TENANT = loResultGUID.poParam.CFROM_TENANT,
                CTO_TENANT = loResultGUID.poParam.CTO_TENANT,
                CUSER_ID = R_BackGlobalVar.USER_ID,
                CFILE_NAME = loResultGUID.poParam.CFILE_NAME,
                CREPORT_TYPE = "S",
                LPRINT = true
                // CTITLE = loResultGUID.poParam.CTITLE,
            };

            // prepare report server parameters
            _logger.LogDebug("Preparing report server parameters");
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
            lcExtension = Enum.GetName(typeof(R_FileType), leReportOutputType)!;

            var loReportFormat = _getReportFormat();

            var loReportRule = new R_ReportServerRule(R_BackGlobalVar.TENANT_ID.ToLower(),
                R_BackGlobalVar.COMPANY_ID.ToLower());
            _logger.LogDebug("Report Server Rule initialized", loReportRule);
            // end prepare report server parameters

            _logger.LogDebug("Generated parameters for data print", loParameterGenerateData);
            _logger.LogInfo("Get Summary Data Billing for Report");
            loParameterGenerateData.CREPORT_TYPE = "S";
            loParameterGenerateData.CSERVICE_TYPE = "";
            var listDataBilling = await loCls.GetDataBilling(poParameter: loParameterGenerateData);
            _logger.LogInfo("Data Billing retrieved successfully for Report");

            _logger.LogInfo("Get Data Detail Unit");
            loParameterGenerateData.CREPORT_TYPE = "D";
            loParameterGenerateData.CSERVICE_TYPE = "UN";
            var listDataDetailUnit = await loCls.GeDataDetailUnitList(poParameter: loParameterGenerateData);
            _logger.LogInfo("Data Detail Unit Retrieved successfully for Report");

            _logger.LogInfo("Get Data Detail Utility");
            loParameterGenerateData.CREPORT_TYPE = "D";
            loParameterGenerateData.CSERVICE_TYPE = "UT";
            var listDataDetailUtility = await loCls.GeDataDetailUtilityList(poParameter: loParameterGenerateData);
            _logger.LogInfo("Data Detail Utility Retrieved successfully for Report");

            _logger.LogInfo("Get Data Logo");
            var oLogo = await loCls.GetLogoCompany(loParameterGenerateData);
            _logger.LogInfo("Company Logo Retrieved successfully for Report");

            _logger.LogInfo("Get Label");
            var loLabel = loCls.AssignValuesWithMessages(typeof(PMR03000BackResources.Resources_Dummy_Class),
                loCultureInfo,
                new PMR03000ReportLabelDTO());

            _logger.LogInfo("Report data formatted for print");

            // var loDataPrint = new PMR03000ResultDataPrintDTO();
            var loData = new PMR03000ResultDataDTO();
            var Header = new PMR03000BaseHeaderDTO()
            {
                PROPERTY_NAME = loResultGUID.poParam.CPROPERTY_NAME,
                CLOGO = oLogo.CLOGO
            };

            // loData.Label = (PMR03000ReportLabelDTO)loLabel;
            // loData.Header = Header;
            // loData.Label = loData.Label;
            // loData.Header = Header;


            // if (listDataBilling != null)
            // {

            var resultData = new PMR03000ResultDataDTO()
            {
                Header = Header,
                Label = (PMR03000ReportLabelDTO)loLabel,
                Datas = new List<PMR03000DataReportDTO>(),
            };

            foreach (var item in listDataBilling)
            {
                var itemIndexes = 0;
                // _logger.LogInfo("Assign Tenant id for param");
                // item.CTENANT_ID = item.CTENANT_ID;
                // Setup Report Parameters


                _logger.LogInfo("Set Param for VA");
                var loVAParam = new PMR03000ParameterDb()
                {
                    CCOMPANY_ID = item.CCOMPANY_ID,
                    CPROPERTY_ID = item.CPROPERTY_ID,
                    CTENANT_ID = item.CTENANT_ID,
                    CLANG_ID = R_BackGlobalVar.REPORT_CULTURE
                };

                _logger.LogInfo("Get data VA");
                var dataVirtualAccount = await loCls.GetVAList(loVAParam);

                _logger.LogInfo("Get data Unit");
                var FilteredUnitList = listDataDetailUnit.Where(x => x.CREF_NO == item.CREF_NO).ToList();

                _logger.LogInfo("Get data Utility");
                var filteredGroups = listDataDetailUtility
                    .Where(x => x.CREF_NO == item.CREF_NO)
                    .GroupBy(x => x.CCHARGES_TYPE)
                    .ToDictionary(g => g.Key, g => g.ToList());

                _logger.LogInfo("Seprated with different ChargeType");
                var FilteredUtilityList01 =
                    filteredGroups.GetValueOrDefault("01", new List<PMR03000DetailUtilityDTO>());
                var FilteredUtilityList02 =
                    filteredGroups.GetValueOrDefault("02", new List<PMR03000DetailUtilityDTO>());
                var FilteredUtilityList03 =
                    filteredGroups.GetValueOrDefault("03", new List<PMR03000DetailUtilityDTO>());
                var FilteredUtilityList04 =
                    filteredGroups.GetValueOrDefault("04", new List<PMR03000DetailUtilityDTO>());

                // var resultData = new PMR03000ResultDataDTO()
                // {
                //     Header = Header,
                //     Label = (PMR03000ReportLabelDTO)loLabel,
                //     Datas = item,
                //     VirtualAccountData = dataVirtualAccount,
                //     DataUnitList = FilteredUnitList,
                //     DataUtility1 = FilteredUtilityList01,
                //     DataUtility2 = FilteredUtilityList02,
                //     DataUtility3 = FilteredUtilityList03,
                //     DataUtility4 = FilteredUtilityList04,
                // };

                resultData.Datas.Add(item);
                resultData.Datas[itemIndexes].VirtualAccountData = dataVirtualAccount;
                resultData.Datas[itemIndexes].DataUnitList = FilteredUnitList;
                resultData.Datas[itemIndexes].DataUtility1 = FilteredUtilityList01;
                resultData.Datas[itemIndexes].DataUtility2 = FilteredUtilityList02;
                resultData.Datas[itemIndexes].DataUtility3 = FilteredUtilityList03;
                resultData.Datas[itemIndexes].DataUtility4 = FilteredUtilityList04;

                itemIndexes++;
                // loDataPrint.Data.Add(resultData);
            }

            loData = resultData;

            var loParameter = new R_GenerateReportParameter()
            {
                ReportRule = loReportRule,
                ReportFileName = lcReportFileName,
                ReportData = JsonSerializer.Serialize(loData),
                // ReportData = JsonSerializer.Serialize(loData),
                ReportDataSourceName = "ResponseDataModel",
                ReportFormat =
                    R_Utility
                        .R_ConvertObjectToObject<PMR03000PrintReportFormatDTO,
                            R_ReportServerCommon.R_ReportFormatDTO>(loReportFormat),
                ReportDataType = typeof(PMR03000ResultDataDTO).ToString(),
                ReportOutputType = leReportOutputType,
                ReportAssemblyName = "PMR03000Common.dll",
                ReportParameter = null
            };

            _logger.LogInfo("Report parameters prepared successfully");

            // Generate Report as Byte Array
            _logger.LogInfo("Starting report generation as byte array...");
            var loRtnInByte = await R_ReportServerUtility.R_GenerateReportByte(
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
            // }

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

    public class PMR03000PrintReportFormatDTO
    {
        public int _DecimalPlaces { get; set; }
        public string? _DecimalSeparator { get; set; }
        public string? _GroupSeparator { get; set; }
        public string? _ShortDate { get; set; }
        public string? _ShortTime { get; set; }
        public string? _LongDate { get; set; }
        public string? _LongTime { get; set; }
    }
}