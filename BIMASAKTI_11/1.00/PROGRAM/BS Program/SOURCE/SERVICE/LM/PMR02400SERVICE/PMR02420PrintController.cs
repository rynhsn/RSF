using BaseHeaderReportCOMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR02400BACK;
using PMR02400COMMON;
using PMR02400COMMON.DTO_s;
using PMR02400COMMON.DTO_s.Print;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace PMR02400SERVICE
{
    public class PMR02420PrintController : R_ReportControllerBase
    {
        private PMR02400PrintLogger _logger;
        private R_ReportFastReportBackClass _ReportCls;
        private PMR02400ParamDTO _Parameter;
        private readonly ActivitySource _activitySource;
        private PMR02400BackResources.Resources_Dummy_Class _backRes = new();

        public PMR02420PrintController(ILogger<PMR02400PrintLogger> logger)
        {
            PMR02400PrintLogger.R_InitializeLogger(logger);
            _logger = PMR02400PrintLogger.R_GetInstanceLogger();
            _activitySource = PMR02400Activity.R_InitializeAndGetActivitySource(nameof(PMR02420PrintController));

            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }

        #region reporthelper

        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "PMR02400Detail.frx");
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

        #endregion reporthelper

        [HttpPost]
        public R_DownloadFileResultDTO DownloadResultPrintPost(PMR02400ParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            _logger.LogInfo("Start - Post DownloadResultPrintPost Status");
            R_Exception loException = new R_Exception();
            PMR02400PrintLogKey loCache = null;
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new PMR02400PrintLogKey
                {
                    poParamSummary = poParameter,
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
            _logger.LogInfo("End - Print Overtime");
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult PenaltyDetail_ReportListGet(string pcGuid)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            R_Exception loException = new R_Exception();
            FileStreamResult loRtn = null;
            PMR02400PrintLogKey loResultGUID = null;
            try
            {
                // Deserialize the GUID from the cache
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<PMR02400PrintLogKey>(R_DistributedCache.Cache.Get(pcGuid));
                _logger.LogDebug("Deserialized GUID: {pcGuid}", pcGuid);

                // Get Parameter
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                R_ReportGlobalVar.R_SetFromReportDTO(loResultGUID.poReportGlobal);

                _Parameter = loResultGUID.poParamSummary;

                _logger.LogDebug("Deserialized GUID: {pcGuid}", pcGuid);
                _logger.LogDebug("Deserialized Parameters: {@Parameters}", _Parameter);

                _logger.LogInfo("Data retrieval successful. Generating report.");
                R_FileType loFileType = new();
                if (loResultGUID.poParamSummary.LIS_PRINT)
                {
                    loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF), R_ReportUtility.GetMimeType(R_FileType.PDF));
                }
                else
                {
                    loFileType = loResultGUID.poParamSummary.CREPORT_FILETYPE switch
                    {
                        "XLSX" => R_FileType.XLSX,
                        "PDF" => R_FileType.PDF,
                        "XLS" => R_FileType.XLS,
                        "CSV" => R_FileType.CSV,
                        _ => R_FileType.PDF,
                    };

                    //print nama save as
                    loRtn = File(_ReportCls.R_GetStreamReport(peExport: loFileType), R_ReportUtility.GetMimeType(loFileType), $"{loResultGUID.poParamSummary.CREPORT_FILENAME}.{loResultGUID.poParamSummary.CREPORT_FILETYPE}");
                }

                _logger.LogInfo("Report generated successfully.");
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End - Overtime Report Generation");
            return loRtn;
        }

        #region Helper

        private PMR02410PrintDisplayDTO GenerateDataPrint(PMR02400ParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            R_Exception loEx = new();
            PMR02410PrintDisplayDTO loRtn = new();
            BaseHeaderDTO loParam = new();
            CultureInfo loCultureInfo = new(R_BackGlobalVar.REPORT_CULTURE);

            try
            {
                _logger.LogInfo("_logger.LogInfo(\"Start - Generating data for Print\");\n data for Overtime report.");

                //Add Resources
                loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),
                    "Page", loCultureInfo);
                loRtn.BaseHeaderColumn.Of =
                    R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_Date =
                    R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),
                    "Print_By", loCultureInfo);

                PMR02400LabelDTO loColumnObject = new PMR02400LabelDTO();
                var loColumn = AssignValuesWithMessages(typeof(PMR02400BackResources.Resources_Dummy_Class), loCultureInfo, loColumnObject);

                // Set base header data
                _logger.LogDebug("Deserialized Print Parameters: {@PrintParameters}");

                loParam.CPRINT_CODE = "PMR002400";
                loParam.CPRINT_NAME = PMR02400ContextConstant.CPROGRAM_NAME;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                var loCls = new PMR02400Cls();
                loParam.BLOGO_COMPANY = loCls.GetCompanyLogo(R_BackGlobalVar.COMPANY_ID).CLOGO;
                loParam.CCOMPANY_NAME = loCls.GetCompanyName(R_BackGlobalVar.COMPANY_ID).CCOMPANY_NAME;

                // Create an instance
                PMR02410ReportDataDTO loData = new()
                {
                    Title = PMR02400ContextConstant.CPROGRAM_NAME,
                    Header = PMR02400ContextConstant.CPROGRAM_NAME,
                    Label = (PMR02400LabelDTO)loColumn,
                    Param = poParam,
                    Data = new List<DetailDTO>(),
                    GrandTotal = new List<SubtotalCurrencyDTO>()
                };

                // Get print data for Group Of Account report
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                var loCollData = loCls.GetDetailData(poParam);
                _logger.LogInfo("Data generation successful. Processing data for printing.");

                // Set the generated data in loRtn
                _logger.LogInfo("Data processed successfully. Generating print output.");
                loData.Data = loCollData.Any() ? GroupingData(loCollData) : new List<DetailDTO>();
                loData.GrandTotal = loCollData.Any() ? GroupingGrandTotalData(loCollData) : new List<SubtotalCurrencyDTO>();
                loRtn.BaseHeaderData = loParam;
                loRtn.ReportDataDTO = loData;

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

        #endregion Helper

        private List<DetailDTO> GroupingData(List<PMR02401SPResultDTO> poCollectionDataRaw)
        {
            var loException = new R_Exception();
            string lcMethodName = nameof(GroupingData);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START method {0} ", lcMethodName));
            List<DetailDTO> loReturn = new List<DetailDTO>();

            try
            {
                loReturn = poCollectionDataRaw
                .GroupBy(a => new { a.CTENANT_ID, a.CTENANT_NAME })
                .Select(loTenantGroup => new DetailDTO
                {
                    CTENANT_ID = loTenantGroup.Key.CTENANT_ID,
                    CTENANT_NAME = loTenantGroup.Key.CTENANT_NAME,

                    // Full list of agreements for the tenant
                    Agreements = loTenantGroup.Select(a => new AgreementDtDTO
                    {
                        CAGREEMENT_NO = a.CAGREEMENT_NO,
                        CUNIT_DESCRIPTION = a.CUNIT_DESCRIPTION,
                        CBUILDING_ID = a.CBUILDING_ID,
                        CBUILDING_NAME = a.CBUILDING_NAME,
                        CINVOICE_NO = a.CINVOICE_NO,
                        CINVOICE_DESCRIPTION = a.CINVOICE_DESCRIPTION,
                        CDUE_DATE = a.CDUE_DATE,
                        ILATE_DAYS = a.ILATE_DAYS,
                        DDUE_DATE = DateTime.ParseExact(a.CDUE_DATE, "yyyyMMdd", new CultureInfo(R_BackGlobalVar.REPORT_CULTURE)),
                        CCURRENCY_CODE = a.CCURRENCY_CODE,
                        CCURRENCY_NAME = a.CCURRENCY_NAME,
                        NINVOICE_AMOUNT = a.NINVOICE_AMOUNT,
                        NREDEEMED_AMOUNT = a.NREDEEMED_AMOUNT,
                        NPAID_AMOUNT = a.NPAID_AMOUNT,
                        NOUTSTANDING_AMOUNT = a.NOUTSTANDING_AMOUNT
                    }).ToList(),

                    // Subtotal for each currency within a tenant
                    SubtotalCurrencies = loTenantGroup
                        .GroupBy(a => new { a.CCURRENCY_CODE, a.CCURRENCY_NAME })
                        .Select(loCurrGroup => new SubtotalCurrencyDTO
                        {
                            CCURRENCY_CODE = loCurrGroup.Key.CCURRENCY_CODE,
                            CCURRENCY_NAME = loCurrGroup.Key.CCURRENCY_NAME,
                            NINVOICE_AMOUNT = loCurrGroup.Sum(a => a.NINVOICE_AMOUNT),
                            NREDEEMED_AMOUNT = loCurrGroup.Sum(a => a.NREDEEMED_AMOUNT),
                            NPAID_AMOUNT = loCurrGroup.Sum(a => a.NPAID_AMOUNT),
                            NOUTSTANDING_AMOUNT = loCurrGroup.Sum(a => a.NOUTSTANDING_AMOUNT)
                        }).ToList()
                }).ToList();
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

        private List<SubtotalCurrencyDTO> GroupingGrandTotalData(List<PMR02401SPResultDTO> poCollectionDataRaw)
        {
            var loException = new R_Exception();
            string lcMethodName = nameof(GroupingGrandTotalData);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START method {0} ", lcMethodName));
            List<SubtotalCurrencyDTO> loReturn = new List<SubtotalCurrencyDTO>();

            try
            {
                loReturn = poCollectionDataRaw
                    .GroupBy(a => new { a.CCURRENCY_CODE, a.CCURRENCY_NAME })
                    .Select(loCurrGroup => new SubtotalCurrencyDTO
                    {
                        CCURRENCY_CODE = loCurrGroup.Key.CCURRENCY_CODE,
                        CCURRENCY_NAME = loCurrGroup.Key.CCURRENCY_NAME,
                        NINVOICE_AMOUNT = loCurrGroup.Sum(a => a.NINVOICE_AMOUNT),
                        NREDEEMED_AMOUNT = loCurrGroup.Sum(a => a.NREDEEMED_AMOUNT),
                        NPAID_AMOUNT = loCurrGroup.Sum(a => a.NPAID_AMOUNT),
                        NOUTSTANDING_AMOUNT = loCurrGroup.Sum(a => a.NOUTSTANDING_AMOUNT)
                    }).ToList();
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