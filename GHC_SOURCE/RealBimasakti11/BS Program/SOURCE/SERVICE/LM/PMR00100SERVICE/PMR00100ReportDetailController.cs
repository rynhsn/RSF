using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI.Log;
using R_CommonFrontBackAPI;
using R_ReportFastReportBack;
using System.Collections;
using System.Diagnostics;
using PMR00100Service.DTOs;
using R_Cache;
using BaseHeaderReportCOMMON;
using System.Globalization;
using PMR00100Common.DTOs;
using PMR00100Common.Logs;
using PMR00100Common.Report;
using PMR00100Back;

namespace PMR00100Service
{
    public class PMR00100ReportDetailController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private PrintParamDTO _poParam;
        private readonly LoggerPMR00100? _loggerPMR00100;
        private readonly ActivitySource _activitySource;

        #region Instantiate
        public PMR00100ReportDetailController(ILogger<LoggerPMR00100> logger)
        {
            LoggerPMR00100.R_InitializeLogger(logger);
            _loggerPMR00100 = LoggerPMR00100.R_GetInstanceLogger();
            _activitySource = PMR00100Activity.R_InitializeAndGetActivitySource(nameof(PMR00100ReportDetailController));

            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }
        #endregion

        #region EventHandler
        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "PMR00100LOOStatus.frx");
        }
        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            R_Exception loException = new R_Exception();
            try
            {
                poData.Add(GenerateDataPrint(_poParam));
                pcDataSourceName = "ResponseDataModel";
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
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

        [HttpPost]
        public R_DownloadFileResultDTO LOOStatusReportPost(PrintParamDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            _loggerPMR00100.LogInfo("Start LOOStatusReportPost PMR00100");
            R_DownloadFileResultDTO loRtn = null;
            PMR00100PrintLogKeyDTO loCache = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new PMR00100PrintLogKeyDTO()
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY)
                };
                _loggerPMR00100.LogInfo("Set GUID Param LOOStatusReportPost");
                var loObject = R_NetCoreUtility.R_SerializeObjectToByte(loCache);
                _loggerPMR00100.LogDebug("log Data Length = {0}", loObject.Length);
                _loggerPMR00100.LogDebug("log GUID = {0}", loRtn.GuidResult);
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte(loCache));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            if (loEx.Haserror)
            {
                _loggerPMR00100.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerPMR00100.LogInfo("End LOOStatusReportPost PMR00100");
            return loRtn;
        }
        [HttpGet, AllowAnonymous]
        public FileStreamResult LOOStatusReportGet(string pcGuid)
        {
            R_Exception loEx = new R_Exception();
            _loggerPMR00100.LogInfo("Start LOOStatusReportGet PMR00100");
            PMR00100PrintLogKeyDTO loResultGUID = null;
            FileStreamResult loRtn = null;
            try
            {
                //Get Parameter
                var test = R_DistributedCache.Cache.Get(pcGuid);
                if (test != null)
                {
                    _loggerPMR00100.LogInfo("get data, GUID = {0}", pcGuid);
                    _loggerPMR00100.LogInfo("get length = {0}", test.Length);
                }
                else
                {
                    _loggerPMR00100.LogInfo("report not found");
                }
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<PMR00100PrintLogKeyDTO>(R_DistributedCache.Cache.Get(pcGuid));

                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                _poParam = loResultGUID.poParam;

                _loggerPMR00100.LogInfo("Read File Report LOOStatusReportGet PMR00100");
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMR00100.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        #region Helper

        public static string ConvertDateToMonthYear(string dateString, string inputFormat, string outputFormat)
        {
            if (DateTime.TryParseExact(dateString, inputFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                return date.ToString(outputFormat, CultureInfo.InvariantCulture);
            }
            else
            {
                // Return null or an empty string if parsing fails
                return null;
            }
        }
        private PMR00100LOOStatusResultWithBaseHeaderDTO GenerateDataPrint(PrintParamDTO poParam)
        {
            var loEx = new R_Exception();
            PMR00100LOOStatusResultWithBaseHeaderDTO loRtn = new PMR00100LOOStatusResultWithBaseHeaderDTO();
            System.Globalization.CultureInfo loCultureInfo = new System.Globalization.CultureInfo(R_BackGlobalVar.REPORT_CULTURE);
            try
            {
                //Add Resources
                loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Page", loCultureInfo);
                loRtn.BaseHeaderColumn.Of = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_Date = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_By", loCultureInfo);

                LOOStatusReportResultDTO loData = new LOOStatusReportResultDTO()
                {
                    title = "LOO Status",
                    Header = new LOOStatusHeaderDTO()
                    {
                        CPROPERTY = $"{poParam.CPROPERTY_ID} - {poParam.CPROPERTY_NAME}" ?? "",
                        CDEPT = $"{poParam.CFROM_DEPARTMENT_NAME}({poParam.CFROM_DEPARTMENT_ID}) - {poParam.CTO_DEPARTMENT_NAME}({poParam.CTO_DEPARTMENT_ID})" ?? "",
                        CSALESMAN = $"{poParam.CFROM_SALESMAN_NAME}({poParam.CFROM_SALESMAN_ID}) - {poParam.CTO_SALESMAN_NAME}({poParam.CTO_SALESMAN_ID})" ?? "",
                        CPERIOD = $"{poParam.CFROM_PERIOD_NAME} - {poParam.CTO_PERIOD_NAME} " ?? "",
                    },
                    Column = new LOOStatusColumnDTO()
                    {
                        //COL_DEPARTMENT = R_Utility.R_GetMessage(typeof(Resources_PMR00100), "_labelDepartment", loCultureInfo),

                    },
                    DataLOOStatus = new List<LOOStatusDetail1DTO>()
                };

                var loCls = new PMR00100Cls();
                _loggerPMR00100.LogInfo("Call Method GenerateDataPrint");
                PrintParamDTO loParamaterDb = new PrintParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROPERTY_ID = poParam.CPROPERTY_ID,
                    @CFROM_DEPARTMENT_ID = poParam.CFROM_DEPARTMENT_ID,
                    @CTO_DEPARTMENT_ID = poParam.CTO_DEPARTMENT_ID,
                    @CFROM_SALESMAN_ID = poParam.CFROM_SALESMAN_ID,
                    @CTO_SALESMAN_ID = poParam.CTO_SALESMAN_ID,
                    @CFROM_PERIOD = poParam.CFROM_PERIOD,
                    @CTO_PERIOD = poParam.CTO_PERIOD,
                    @CLANG_ID = R_BackGlobalVar.CULTURE,
                };

                var loCollection = loCls.GetPrintLOOList(loParamaterDb);
                var loTempData = loCollection
               .GroupBy(data1a => new
               {
                   data1a.CTRANS_NAME, // First level of grouping
               })
               .Select(data1b => new LOOStatusDetail1DTO()
               {
                   CTRANS_NAME = data1b.Key.CTRANS_NAME,
                   LOOStatusDetail2 = data1b.GroupBy(data2a => new
                   {
                       data2a.CSALESMAN_ID, // Second level of grouping
                       data2a.CSALESMAN_NAME,
                   }).Select(data2b => new LOOStatusDetail2DTO()
                   {
                       CSALESMAN_ID = data2b.Key.CSALESMAN_ID,
                       CSALESMAN_NAME = data2b.Key.CSALESMAN_NAME,
                       LOOStatusDetail3 = data2b.GroupBy(data3a => new
                       {
                           data3a.CREF_NO, // Third level of grouping
                           data3a.CREF_DATE,
                           data3a.CTENURE,
                           data3a.CGLACCOUNT_NAME,
                           data3a.CAGREEMENT_STATUS_NAME,
                           data3a.CTRANS_STATUS_NAME,
                           data3a.NREVISION_COUNT,
                           data3a.NTOTAL_PRICE,
                           data3a.CTAX,
                           data3a.CTENANT_ID,
                           data3a.CTENANT_NAME,
                           data3a.CTC_MESSAGE,
                       }).Select(data3b => new LOOStatusDetail3DTO()
                       {
                           CREF_NO = data3b.Key.CREF_NO,
                           CREF_DATE = ConvertDateToMonthYear(data3b.Key.CREF_DATE, "yyyyMMdd", "dd MMMM yyyy"),
                           CTENURE = data3b.Key.CTENURE,
                           CGLACCOUNT_NAME = data3b.Key.CGLACCOUNT_NAME,
                           CAGREEMENT_STATUS_NAME = data3b.Key.CAGREEMENT_STATUS_NAME,
                           CTRANS_STATUS_NAME = data3b.Key.CTRANS_STATUS_NAME,
                           NREVISION_COUNT = data3b.Key.NREVISION_COUNT,
                           NTOTAL_PRICE = data3b.Key.NTOTAL_PRICE,
                           CTAX = data3b.Key.CTAX,
                           CTENANT_ID = data3b.Key.CTENANT_ID,
                           CTENANT_NAME = data3b.Key.CTENANT_NAME,
                           CTC_MESSAGE = data3b.Key.CTC_MESSAGE,
                           LOOStatusDetailUnit = data3b.GroupBy(dataUnit => new
                           {
                               dataUnit.CUNIT_DETAIL_ID,
                               dataUnit.CUNIT_DETAIL_NAME,
                               dataUnit.NUNIT_DETAIL_GROSS_AREA_SIZE,
                               dataUnit.NUNIT_DETAIL_NET_AREA_SIZE,
                               dataUnit.NUNIT_DETAIL_COMMON_AREA_SIZE,
                               dataUnit.NUNIT_DETAIL_PRICE,
                               dataUnit.NUNIT_TOTAL_GROSS_AREA_SIZE,
                               dataUnit.NUNIT_TOTAL_NET_AREA_SIZE,
                               dataUnit.NUNIT_TOTAL_COMMON_AREA_SIZE,
                               dataUnit.NUNIT_TOTAL_PRICE,

                           }).Select(selectUnit => new LOOStatusDetailUnitDTO()
                           {
                               CUNIT_DETAIL_ID = selectUnit.Key.CUNIT_DETAIL_ID,
                               CUNIT_DETAIL_NAME = selectUnit.Key.CUNIT_DETAIL_NAME,
                               NUNIT_DETAIL_GROSS_AREA_SIZE = selectUnit.Key.NUNIT_DETAIL_GROSS_AREA_SIZE,
                               NUNIT_DETAIL_NET_AREA_SIZE = selectUnit.Key.NUNIT_DETAIL_NET_AREA_SIZE,
                               NUNIT_DETAIL_COMMON_AREA_SIZE = selectUnit.Key.NUNIT_DETAIL_COMMON_AREA_SIZE,
                               NUNIT_DETAIL_PRICE = selectUnit.Key.NUNIT_DETAIL_PRICE,
                               NUNIT_TOTAL_GROSS_AREA_SIZE = selectUnit.Key.NUNIT_TOTAL_GROSS_AREA_SIZE,
                               NUNIT_TOTAL_NET_AREA_SIZE = selectUnit.Key.NUNIT_TOTAL_NET_AREA_SIZE,
                               NUNIT_TOTAL_COMMON_AREA_SIZE = selectUnit.Key.NUNIT_TOTAL_COMMON_AREA_SIZE,
                               NUNIT_TOTAL_PRICE = selectUnit.Key.NUNIT_TOTAL_PRICE,

                           }).ToList(),
                           LOOStatusDetailCharge = data3b
                           .GroupBy(dataCharge => new
                           {
                               dataCharge.CCHARGE_DETAIL_TYPE_NAME,
                               dataCharge.CCHARGE_DETAIL_UNIT_NAME,
                               dataCharge.CCHARGE_DETAIL_CHARGE_NAME,
                               dataCharge.CCHARGE_DETAIL_TAX_NAME,
                               dataCharge.CCHARGE_DETAIL_START_DATE,
                               dataCharge.CCHARGE_DETAIL_END_DATE,
                               dataCharge.CCHARGE_DETAIL_TENURE,
                               dataCharge.CCHARGE_DETAIL_FEE_METHOD,
                               dataCharge.NCHARGE_DETAIL_FEE_AMOUNT,
                               dataCharge.NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT,
                               dataCharge.NCHARGE_DETAIL_SUBTOTAL_CALCULATED_FEE_AMOUNT,
                               dataCharge.CDEPOSIT_DETAIL_ID,
                               dataCharge.CDEPOSIT_DETAIL_DATE,
                               dataCharge.NDEPOSIT_DETAIL_AMOUNT,
                               dataCharge.CDEPOSIT_DETAIL_DESCRIPTION,
                           })
                           .Select(selectCharge => new LOOStatusDetailChargeDTO()
                           {
                               CCHARGE_DETAIL_TYPE_NAME = selectCharge.Key.CCHARGE_DETAIL_TYPE_NAME,
                               LOOStatusDetailChargeUnit = selectCharge.GroupBy(chargeUnit => new
                               {
                                   chargeUnit.CCHARGE_DETAIL_UNIT_NAME,
                               }).Select(selectChargeUnit => new LOOStatusDetailChargeTypeUnitDTO()
                               {
                                   CCHARGE_DETAIL_UNIT_NAME = selectChargeUnit.Key.CCHARGE_DETAIL_UNIT_NAME,
                                   LOOStatusDetailChargeTypeUnitCharge = selectChargeUnit.Select(charge => new LOOStatusDetailChargeTypeUnitChargeDTO()
                                   {
                                       CCHARGE_DETAIL_CHARGE_NAME = charge.CCHARGE_DETAIL_CHARGE_NAME,
                                       CCHARGE_DETAIL_TAX_NAME = charge.CCHARGE_DETAIL_TAX_NAME,
                                       CCHARGE_DETAIL_START_DATE = ConvertDateToMonthYear(charge.CCHARGE_DETAIL_START_DATE, "yyyyMMdd", "dd MMMM yyyy"),
                                       CCHARGE_DETAIL_END_DATE = ConvertDateToMonthYear(charge.CCHARGE_DETAIL_END_DATE, "yyyyMMdd", "dd MMMM yyyy"),
                                       CCHARGE_DETAIL_TENURE = charge.CCHARGE_DETAIL_TENURE,
                                       CCHARGE_DETAIL_FEE_METHOD = charge.CCHARGE_DETAIL_FEE_METHOD,
                                       NCHARGE_DETAIL_FEE_AMOUNT = charge.NCHARGE_DETAIL_FEE_AMOUNT,
                                       NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT = charge.NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT,
                                       NCHARGE_DETAIL_SUBTOTAL_CALCULATED_FEE_AMOUNT = charge.NCHARGE_DETAIL_SUBTOTAL_CALCULATED_FEE_AMOUNT,
                                       CDEPOSIT_DETAIL_ID = charge.CDEPOSIT_DETAIL_ID,
                                       CDEPOSIT_DETAIL_DATE = charge.CDEPOSIT_DETAIL_DATE,
                                       NDEPOSIT_DETAIL_AMOUNT = charge.NDEPOSIT_DETAIL_AMOUNT,
                                       CDEPOSIT_DETAIL_DESCRIPTION = charge.CDEPOSIT_DETAIL_DESCRIPTION,
                                   }).ToList(),
                               }).ToList(),
                           }).ToList(),
                           LOOStatusDetailDeposit = data3b
                           .GroupBy(dataDeposit => new
                           {
                               dataDeposit.CDEPOSIT_DETAIL_ID,
                               dataDeposit.CDEPOSIT_DETAIL_DATE,
                               dataDeposit.NDEPOSIT_DETAIL_AMOUNT,
                               dataDeposit.CDEPOSIT_DETAIL_DESCRIPTION,

                           }).Select(selectDeposit => new LOOStatusDetailDepositDTO()
                           {
                               CDEPOSIT_DETAIL_ID = selectDeposit.Key.CDEPOSIT_DETAIL_ID,
                               CDEPOSIT_DETAIL_DATE = ConvertDateToMonthYear(selectDeposit.Key.CDEPOSIT_DETAIL_DATE, "yyyyMMdd", "dd MMMM yyyy"),
                               NDEPOSIT_DETAIL_AMOUNT = selectDeposit.Key.NDEPOSIT_DETAIL_AMOUNT,
                               CDEPOSIT_DETAIL_DESCRIPTION = selectDeposit.Key.CDEPOSIT_DETAIL_DESCRIPTION,

                           }).ToList(),
                       }).ToList()
                   }).ToList()
               }).ToList();

                var loTempDataTotalTrans = loCollection
                      .GroupBy(data => data.CTRANS_NAME)
                      .Select(group => new LOOStatusTotalTransDTO()
                      {
                          CTRANS_NAME = group.Key,
                          COL_TOTAL_TRANS = group.Select(data => data.CTRANS_CODE).Distinct().Count(),
                      })
                      .ToList();

                var loTempDataTotalSales = loCollection
             .GroupBy(data => data.CSALESMAN_ID)
             .Select(group => new LOOStatusTotalSalesDTO()
             {
                 CSALESMAN_ID = group.Key,
                 COL_TOTAL_SALESMAN = group.Select(data => data.CSALESMAN_ID).Distinct().Count()
             })
             .ToList();

                loData.LOOStatusTotalSales = loTempDataTotalSales;


                _loggerPMR00100.LogInfo("Set Base Header GenerateDataPrint");
                var loParam = new BaseHeaderDTO()
                {
                    CCOMPANY_NAME = "PT Realta Chackradarma",
                    CPRINT_CODE = "001",
                    CPRINT_NAME = "LOO Status",
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                };
                _loggerPMR00100.LogInfo("Set Data Report");
                loData.DataLOOStatus = loTempData;
                loData.LOOStatusTotalSales = loTempDataTotalSales;
                loData.LOOStatusTotalTrans = loTempDataTotalTrans;
                loRtn.PMR00100PrintData = loData;
                loRtn.BaseHeaderData = loParam;
            }
            catch (Exception ex)
            {
                _loggerPMR00100.LogInfo(ex.Message);
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        #endregion

    }
}
