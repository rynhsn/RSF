using BaseHeaderReportCOMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR02000BACK;
using PMR02000COMMON;
using PMR02000COMMON.DTO_s.Print;
using PMR02000COMMON.DTO_s.Print.Grouping;
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

namespace PMR02000SERVICE
{
    public class PMR02000PrintController : R_ReportControllerBase
    {
        private PMR02000PrintLogger _logger;
        private R_ReportFastReportBackClass _ReportCls;
        private ReportParamDTO _Parameter;
        private readonly ActivitySource _activitySource;
        private PMR02000BackResources.Resources_Dummy_Class _backRes = new();

        public PMR02000PrintController(ILogger<PMR02000PrintLogger> logger)
        {
            PMR02000PrintLogger.R_InitializeLogger(logger);
            _logger = PMR02000PrintLogger.R_GetInstanceLogger();
            _activitySource = PMR02000Activity.R_InitializeAndGetActivitySource(nameof(PMR02000PrintController));
            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }

        #region reporthelper

        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "PMR02000Summary.frx");
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
        public R_DownloadFileResultDTO DownloadResultPrintPost(ReportParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            _logger.LogInfo("Start - Post DownloadResultPrintPost Status");
            R_Exception loException = new R_Exception();
            PMR02000PrintLogKey loCache = null;
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new PMR02000PrintLogKey
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
            _logger.LogInfo("End - Print UserActivity");
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult UserActivitySummary_ReportListGet(string pcGuid)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            R_Exception loException = new R_Exception();
            FileStreamResult loRtn = null;
            PMR02000PrintLogKey loResultGUID = null;
            try
            {
                // Deserialize the GUID from the cache
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<PMR02000PrintLogKey>(R_DistributedCache.Cache.Get(pcGuid));
                _logger.LogDebug("Deserialized GUID: {pcGuid}", pcGuid);

                // Get Parameter
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                R_ReportGlobalVar.R_SetFromReportDTO(loResultGUID.poReportGlobal);

                _Parameter = loResultGUID.poParamSummary;

                _logger.LogDebug("Deserialized GUID: {pcGuid}", pcGuid);
                _logger.LogDebug("Deserialized Parameters: {@Parameters}", _Parameter);

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
                _logger.LogInfo("Data retrieval successful. Generating report.");

                _logger.LogInfo("Report generated successfully.");
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End - UserActivity Report Generation");
            return loRtn;
        }

        #region Helper

        private ReportPrintSummaryDTO GenerateDataPrint(ReportParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            R_Exception loEx = new();
            ReportPrintSummaryDTO loRtn = new();
            BaseHeaderDTO loParam = new();
            CultureInfo loCultureInfo = new(R_BackGlobalVar.REPORT_CULTURE);
            try
            {
                _logger.LogInfo("_logger.LogInfo(\"Start - Generating data for Print\");\n data for UserActivity report.");

                //Add Resources
                loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),
                    "Page", loCultureInfo);
                loRtn.BaseHeaderColumn.Of =
                    R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_Date =
                    R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),
                    "Print_By", loCultureInfo);

                ReportLabelDTO loColumnObject = new();
                var loColumn = AssignValuesWithMessages(typeof(PMR02000BackResources.Resources_Dummy_Class), loCultureInfo, loColumnObject);

                // Set base header data
                _logger.LogDebug("Deserialized Print Parameters: {@PrintParameters}");

                loParam.CPRINT_CODE = "02000";
                loParam.CPRINT_NAME = PMR02000ContextConstant.CPROGRAM_NAME;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID.ToUpper();
                var loCls = new PMR02000Cls();
                loParam.BLOGO_COMPANY = loCls.GetCompanyLogo(R_BackGlobalVar.COMPANY_ID).CLOGO;
                loParam.CCOMPANY_NAME = loCls.GetCompanyName(R_BackGlobalVar.COMPANY_ID).CCOMPANY_NAME;

                // Create an instance
                ReportSummaryDataDTO loData = new()
                {
                    Title = PMR02000ContextConstant.CPROGRAM_NAME,
                    Header = PMR02000ContextConstant.CPROGRAM_NAME,
                    Label = (ReportLabelDTO)loColumn,
                    Param = poParam,
                    Data = new List<DeptDTO>(),
                    GrandTotal = new List<SubtotalCurrenciesDTO>()
                };

                //set custom display param
                if (!string.IsNullOrWhiteSpace(loData.Param.CCUT_OFF_DATE))
                {
                    loData.Param.DDATE_CUTOFF = DateTime.ParseExact(loData.Param.CCUT_OFF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                }
                loData.Param.CPERIOD_DISPLAY = string.Concat(loData.Param.CPERIOD.AsSpan(0, 4), "-", loData.Param.CPERIOD.AsSpan(4, 2));
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                // Get print data for Group Of Account report
                var loCollData = loCls.GetSummaryData(poParam);
                _logger.LogInfo("Data generation successful. Processing data for printing.");

                // Set the generated data in loRtn
                _logger.LogInfo("Data processed successfully. Generating print output.");
                //grouping data
                loData.Data = loCollData
                    .GroupBy(x => new { x.CDEPT_CODE, x.CDEPT_NAME }) // Group by Department
                    .Select(deptGroup => new DeptDTO
                    {
                        CDEPT_CODE = deptGroup.Key.CDEPT_CODE,
                        CDEPT_NAME = deptGroup.Key.CDEPT_NAME,
                        Customers = deptGroup
                            .GroupBy(c => new
                            {
                                c.CCUSTOMER_ID,
                                c.CCUSTOMER_NAME,
                                c.CTRX_TYPE_NAME,
                                c.CREF_NO,
                                c.CREF_DATE,
                                c.CCUSTOMER_TYPE_NAME,
                                c.CLOI_AGRMT_NO,
                                c.CCURRENCY_CODE,
                                c.NBEGINNING_APPLY_AMOUNT,
                                c.NREMAINING_AMOUNT,
                                c.NTAX_AMOUNT,
                                c.NGAINLOSS_AMOUNT,
                                c.NCASHBANK_AMOUNT,
                            }) // Group by Customer
                            .Select(customerGroup => new CustomerDTO
                            {
                                CDEPT_CODE = deptGroup.Key.CDEPT_CODE,
                                CDEPT_NAME = deptGroup.Key.CDEPT_NAME,
                                CCUSTOMER_ID = customerGroup.Key.CCUSTOMER_ID,
                                CCUSTOMER_NAME = customerGroup.Key.CCUSTOMER_NAME,
                                CTRX_TYPE_NAME = customerGroup.Key.CTRX_TYPE_NAME,
                                CREF_NO = customerGroup.Key.CREF_NO,
                                CREF_DATE = customerGroup.Key.CREF_DATE,
                                DREF_DATE = DateTime.ParseExact(customerGroup.Key.CREF_DATE, "yyyyMMdd", loCultureInfo),
                                CCUSTOMER_TYPE_NAME = customerGroup.Key.CCUSTOMER_TYPE_NAME,
                                CLOI_AGRMT_NO = customerGroup.Key.CLOI_AGRMT_NO,
                                CCURRENCY_CODE = customerGroup.Key.CCURRENCY_CODE,
                                NBEGINNING_APPLY_AMOUNT = customerGroup.Key.NBEGINNING_APPLY_AMOUNT,
                                NREMAINING_AMOUNT = customerGroup.Key.NREMAINING_AMOUNT,
                                NTAX_AMOUNT = customerGroup.Key.NTAX_AMOUNT,
                                NGAINLOSS_AMOUNT = customerGroup.Key.NGAINLOSS_AMOUNT,
                                NCASHBANK_AMOUNT = customerGroup.Key.NCASHBANK_AMOUNT
                            }).ToList(),

                        DeptSubtotalCurrencies = deptGroup
                            .GroupBy(d => d.CCURRENCY_CODE) // Group by Currency for Department Subtotal
                            .Select(currencyGroup => new SubtotalCurrenciesDTO
                            {
                                CCURRENCY_CODE = currencyGroup.Key,
                                NBEGINNING_APPLY_AMOUNT = currencyGroup.Sum(d => d.NBEGINNING_APPLY_AMOUNT),
                                NREMAINING_AMOUNT = currencyGroup.Sum(d => d.NREMAINING_AMOUNT),
                                NTAX_AMOUNT = currencyGroup.Sum(d => d.NTAX_AMOUNT),
                                NGAINLOSS_AMOUNT = currencyGroup.Sum(d => d.NGAINLOSS_AMOUNT),
                                NCASHBANK_AMOUNT = currencyGroup.Sum(d => d.NCASHBANK_AMOUNT)
                            }).ToList()
                    }).ToList();

                //grouping currency
                loData.GrandTotal = loCollData.GroupBy(x => x.CCURRENCY_CODE)
                    .Select(currencyGroup => new SubtotalCurrenciesDTO
                    {
                        CCURRENCY_CODE = currencyGroup.Key,
                        NBEGINNING_APPLY_AMOUNT = currencyGroup.Sum(c => c.NBEGINNING_APPLY_AMOUNT),
                        NREMAINING_AMOUNT = currencyGroup.Sum(c => c.NREMAINING_AMOUNT),
                        NTAX_AMOUNT = currencyGroup.Sum(c => c.NTAX_AMOUNT),
                        NGAINLOSS_AMOUNT = currencyGroup.Sum(c => c.NGAINLOSS_AMOUNT),
                        NCASHBANK_AMOUNT = currencyGroup.Sum(c => c.NCASHBANK_AMOUNT)
                    }).ToList();

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

        #endregion Helper
    }
}