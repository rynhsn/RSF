using BaseHeaderReportCOMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR02000BACK;
using PMR02000COMMON;
using PMR02000COMMON.DTO_s.Print;
using PMR02000COMMON.DTO_s.Print.Grouping;
using PMR02000SERVICE;
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

namespace PMR02001SERVICE
{
    public class PMR02001PrintController : R_ReportControllerBase
    {
        private PMR02000PrintLogger _logger;
        private R_ReportFastReportBackClass _ReportCls;
        private ReportParamDTO _Parameter;
        private readonly ActivitySource _activitySource;
        private PMR02000BackResources.Resources_Dummy_Class _backRes = new();

        public PMR02001PrintController(ILogger<PMR02000PrintLogger> logger)
        {
            PMR02000PrintLogger.R_InitializeLogger(logger);
            _logger = PMR02000PrintLogger.R_GetInstanceLogger();
            _activitySource = PMR02000Activity.R_InitializeAndGetActivitySource(nameof(PMR02001PrintController));
            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }

        #region reporthelper

        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "PMR02000Detail.frx");
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
        public FileStreamResult UserActivityDetail_ReportListGet(string pcGuid)
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

        private ReportPrintDetailDTO GenerateDataPrint(ReportParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            R_Exception loEx = new();
            ReportPrintDetailDTO loRtn = new();
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
                ReportLabelDTO loColumnObject = new ReportLabelDTO();
                var loColumn = AssignValuesWithMessages(typeof(PMR02000BackResources.Resources_Dummy_Class), loCultureInfo, loColumnObject);

                // Set base header data
                var loCls = new PMR02000Cls();
                var loBaseHeader = loCls.GetCompanyName(R_BackGlobalVar.COMPANY_ID);

                _logger.LogDebug("Deserialized Print Parameters: {@PrintParameters}");
                loParam.CPRINT_CODE = "02001";
                loParam.CPRINT_NAME = PMR02000ContextConstant.CPROGRAM_NAME;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParam.BLOGO_COMPANY = loCls.GetCompanyLogo(poParam).CLOGO;
                loParam.CCOMPANY_NAME = loBaseHeader.CCOMPANY_NAME;
                loParam.CPRINT_DATE_COMPANY = DateTime.ParseExact(loBaseHeader.CDATETIME_NOW, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture).ToString(R_BackGlobalVar.REPORT_FORMAT_SHORT_DATE + " " + R_BackGlobalVar.REPORT_FORMAT_SHORT_TIME);

                // Create an instance
                ReportDetailDataDTO loData = new()
                {
                    Title = PMR02000ContextConstant.CPROGRAM_NAME,
                    Header = PMR02000ContextConstant.CPROGRAM_NAME,
                    Label = (ReportLabelDTO)loColumn,
                    Param = poParam,
                    Data = new List<DeptDTO>(),
                    GrandTotal = new List<SubtotalCurrenciesDTO>()
                };

                //set custom display param
                loData.Param.DDATE_CUTOFF = DateTime.ParseExact(loData.Param.CCUT_OFF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                loData.Param.CPERIOD_DISPLAY = string.Concat(loData.Param.CPERIOD.AsSpan(0, 4), "-", loData.Param.CPERIOD.AsSpan(4, 2));
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                var loCollData = loCls.GetDetailData(poParam);
                _logger.LogInfo("Data processed successfully. Generating print output.");

                //grouping
                loData.Data = loCollData
                .GroupBy(x => new { x.CDEPT_CODE, x.CDEPT_NAME }) // Group by Department
                .Select(deptGroup => new DeptDTO
                {
                    CDEPT_CODE = deptGroup.Key.CDEPT_CODE,
                    CDEPT_NAME = deptGroup.Key.CDEPT_NAME,
                    Customers = deptGroup
                        .GroupBy(c => new { c.CCUSTOMER_ID, c.CCUSTOMER_NAME }) // Group by Customer
                        .Select(customerGroup => new CustomerDTO
                        {
                            CCUSTOMER_ID = customerGroup.Key.CCUSTOMER_ID,
                            CCUSTOMER_NAME = customerGroup.Key.CCUSTOMER_NAME,

                            Transactions = customerGroup
                                .GroupBy(t => new { t.CTRX_TYPE_NAME, t.CREF_NO, t.CREF_DATE, t.DREF_DATE }) // Group by Transaction
                                .Select(trxGroup => new TrxTypeDTO
                                {
                                    CTRX_TYPE_NAME = trxGroup.Key.CTRX_TYPE_NAME,
                                    CREF_NO = trxGroup.Key.CREF_NO,
                                    CREF_DATE = trxGroup.Key.CREF_DATE,
                                    DREF_DATE = DateTime.ParseExact(trxGroup.Key.CREF_DATE, "yyyyMMdd", loCultureInfo),
                                    CTENANT_TYPE_ID = trxGroup.First().CTENANT_TYPE_ID,
                                    CCUSTOMER_TYPE_NAME = trxGroup.First().CCUSTOMER_TYPE_NAME,
                                    CLOI_AGRMT_NO = trxGroup.First().CLOI_AGRMT_NO,
                                    CCURRENCY_CODE = trxGroup.First().CCURRENCY_CODE,
                                    NBEGINNING_APPLY_AMOUNT = trxGroup.Sum(t => t.NBEGINNING_APPLY_AMOUNT),
                                    NREMAINING_AMOUNT = trxGroup.Sum(t => t.NREMAINING_AMOUNT),
                                    NTAX_AMOUNT = trxGroup.Sum(t => t.NTAX_AMOUNT),
                                    NGAINLOSS_AMOUNT = trxGroup.Sum(t => t.NGAINLOSS_AMOUNT),
                                    NCASHBANK_AMOUNT = trxGroup.Sum(t => t.NCASHBANK_AMOUNT),
                                }).ToList(),
                            ProductList = customerGroup
                                        .Select(p => new ProductDTO
                                        {
                                            CPRODUCT_OR_CHARGE_ID = p.CPRODUCT_OR_CHARGE_ID,
                                            CPRODUCT_OR_CHARGE_NAME = p.CPRODUCT_OR_CHARGE_NAME,
                                            CPRODUCT_DEPARTMENT_CODE = p.CPRODUCT_DEPARTMENT_CODE,
                                            CPRODUCT_DEPARTMENT_NAME = p.CPRODUCT_DEPARTMENT_NAME,
                                            NPRODUCT_QUANTITY = p.NPRODUCT_QUANTITY,
                                            CPRODUCT_MEASUREMENT_NAME = p.CPRODUCT_MEASUREMENT_NAME,
                                            NPRODUCT_PRICE_AMOUNT = p.NPRODUCT_PRICE_AMOUNT,
                                            NPRODUCT_LINE_AMOUNT = p.NPRODUCT_LINE_AMOUNT,
                                            NPRODUCT_DISCOUNT_AMOUNT = p.NPRODUCT_DISCOUNT_AMOUNT,
                                            NOTHER_TAX_AMOUNT = p.NOTHER_TAX_AMOUNT,
                                            NPRODUCT_LINE_TOTAL_AMOUNT = p.NPRODUCT_LINE_TOTAL_AMOUNT,
                                        }).ToList(),
                            CustSubtotalCurr = customerGroup
                                .GroupBy(c => c.CCURRENCY_CODE) // Group by Currency for Subtotal
                                .Select(currencyGroup => new SubtotalCurrenciesDTO
                                {
                                    CCURRENCY_CODE = currencyGroup.Key,
                                    NBEGINNING_APPLY_AMOUNT = currencyGroup.Sum(c => c.NBEGINNING_APPLY_AMOUNT),
                                    NREMAINING_AMOUNT = currencyGroup.Sum(c => c.NREMAINING_AMOUNT),
                                    NTAX_AMOUNT = currencyGroup.Sum(c => c.NTAX_AMOUNT),
                                    NGAINLOSS_AMOUNT = currencyGroup.Sum(c => c.NGAINLOSS_AMOUNT),
                                    NCASHBANK_AMOUNT = currencyGroup.Sum(c => c.NCASHBANK_AMOUNT)
                                }).ToList()
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
                _logger.LogInfo("Data generation successful. Processing data for printing.");

                //set report data
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