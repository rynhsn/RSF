
using BaseHeaderReportCOMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR00100Service.DTOs;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI.Log;
using R_CommonFrontBackAPI;
using R_ReportFastReportBack;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using PMR00100Common.Logs;
using PMR00100Common.DTOs;
using PMR00100Common.Report;
using PMR00100Back;
using PMR00100BackResources;

namespace PMR00100Service
{
    public class PMR00100ReportSummaryController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private PrintParamDTO _poParam;
        private readonly LoggerPMR00100? _loggerPMR00100;
        private readonly ActivitySource _activitySource;

        #region Instantiate
        public PMR00100ReportSummaryController(ILogger<LoggerPMR00100> logger)
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
            pcFileTemplate = System.IO.Path.Combine("Reports","PMR00100LOOStatusSummary.frx");
        }
        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData,ref string pcDataSourceName)
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
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY),
                    poReportGlobal = R_ReportGlobalVar.R_GetReportDTO()
                };
                _loggerPMR00100.LogInfo("Set GUID Param LOOStatusReportPost");
                var loObject = R_NetCoreUtility.R_SerializeObjectToByte(loCache);
                _loggerPMR00100.LogDebug("log Data Length = {0}",loObject.Length);
                _loggerPMR00100.LogDebug("log GUID = {0}",loRtn.GuidResult);
                R_DistributedCache.R_Set(loRtn.GuidResult,R_NetCoreUtility.R_SerializeObjectToByte(loCache));
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
                    _loggerPMR00100.LogInfo("get data, GUID = {0}",pcGuid);
                    _loggerPMR00100.LogInfo("get length = {0}",test.Length);
                }
                else
                {
                    _loggerPMR00100.LogInfo("report not found");
                }
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<PMR00100PrintLogKeyDTO>(R_DistributedCache.Cache.Get(pcGuid));

                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                R_ReportGlobalVar.R_SetFromReportDTO(loResultGUID.poReportGlobal);
                _poParam = loResultGUID.poParam;

                _loggerPMR00100.LogInfo("Read File Report LOOStatusReportGet PMR00100");
                R_FileType loFileType = new();
                if (loResultGUID.poParam.LIS_PRINT)
                {
                    loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF),R_ReportUtility.GetMimeType(R_FileType.PDF));
                }
                else
                {
                    switch (loResultGUID.poParam.CREPORT_FILETYPE)
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
                    loRtn = File(_ReportCls.R_GetStreamReport(peExport: loFileType),R_ReportUtility.GetMimeType(loFileType),$"{loResultGUID.poParam.CREPORT_FILENAME}.{loResultGUID.poParam.CREPORT_FILETYPE}");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerPMR00100.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        #region Helper
        public static DateTime? ConvertStringToDate(string dateString,string inputFormat)
        {
            if (DateTime.TryParseExact(dateString,inputFormat,CultureInfo.InvariantCulture,DateTimeStyles.None,out var date))
            {
                return date;
            }
            else
            {
                return null; // Jika parsing gagal, kembalikan null
            }
        }
        private PMR00100LOOStatusResultWithBaseHeaderDTO GenerateDataPrint(PrintParamDTO poParam)
        {
            var loEx = new R_Exception();
            PMR00100LOOStatusResultWithBaseHeaderDTO loRtn = new PMR00100LOOStatusResultWithBaseHeaderDTO();
            System.Globalization.CultureInfo loCultureInfo = new System.Globalization.CultureInfo(R_BackGlobalVar.REPORT_CULTURE);
            try
            {
                var formatDate = "dd MMM yyyy";
                _loggerPMR00100.LogInfo("Set Base Header GenerateDataPrint");
                var loParam = new BaseHeaderDTO()
                {
                    CCOMPANY_NAME = "",
                    CPRINT_CODE = "001",
                    CPRINT_NAME = "LOO List",
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                };

                var loCls = new PMR00100Cls();

                var loBaseHeader = loCls.GetBaseHeaderLogoCompany(poParam);
                loParam.BLOGO_COMPANY = loBaseHeader.CLOGO;
                loParam.CCOMPANY_NAME = loBaseHeader.CCOMPANY_NAME;
                loParam.DPRINT_DATE_COMPANY = DateTime.ParseExact(loBaseHeader.CDATETIME_NOW,"yyyyMMdd HH:mm:ss",CultureInfo.InvariantCulture);


                #region ResourcesBaseHeader
                //Add Resources
                loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),"Page",loCultureInfo);
                loRtn.BaseHeaderColumn.Of = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),"Of",loCultureInfo);
                loRtn.BaseHeaderColumn.Print_Date = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),"Print_Date",loCultureInfo);
                loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),"Print_By",loCultureInfo);
                #endregion

                var loColumn = AssignValuesWithMessages(typeof(PMR00100BackResources.Resources_PMR00100),loCultureInfo,new LOOStatusColumnDTO());
                LOOStatusReportResultDTO loData = new LOOStatusReportResultDTO()
                {
                    title = "LOO Status",
                    Header = new LOOStatusHeaderDTO()
                    {
                        CPROPERTY = $"{poParam.CPROPERTY_ID} - {poParam.CPROPERTY_NAME}" ?? "",
                        CDEPT = $"{poParam.CFROM_DEPT_NAME}({poParam.CFROM_DEPT_CODE}) - {poParam.CTO_DEPT_NAME}({poParam.CTO_DEPT_CODE})" ?? "",
                        CSALESMAN = $"{poParam.CFROM_SALESMAN_NAME}({poParam.CFROM_SALESMAN_ID}) - {poParam.CTO_SALESMAN_NAME}({poParam.CTO_SALESMAN_ID})" ?? "",
                        CPERIOD = $"{poParam.CFROM_PERIOD_NAME} - {poParam.CTO_PERIOD_NAME} " ?? "",
                        CREPORT_NAME = poParam.CREPORT_TYPENAME
                    },
                    Column = (LOOStatusColumnDTO)loColumn,
                    DataLOOStatus = new List<LOOStatusDetail1DTO>()
                };

                _loggerPMR00100.LogInfo("Call Method GenerateDataPrint");
                PrintParamDTO loParamaterDb = new PrintParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROPERTY_ID = poParam.CPROPERTY_ID,
                    CFROM_DEPT_CODE = poParam.CFROM_DEPT_CODE,
                    CTO_DEPT_CODE = poParam.CTO_DEPT_CODE,
                    CFROM_SALESMAN_ID = poParam.CFROM_SALESMAN_ID,
                    CTO_SALESMAN_ID = poParam.CTO_SALESMAN_ID,
                    CFROM_PERIOD = poParam.CFROM_PERIOD,
                    CTO_PERIOD = poParam.CTO_PERIOD,
                    CREPORT_TYPE = poParam.CREPORT_TYPE,
                    CLANG_ID = R_BackGlobalVar.CULTURE,
                };

                var loCollection = loCls.GetPrintLOOList(loParamaterDb);
                #region GroupBy
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
                             data3a.CAGREEMENT_STATUS_NAME,
                             data3a.CTRANS_STATUS_NAME,
                             data3a.NREVISION_COUNT,
                             data3a.CTAX,
                             data3a.CTENANT_ID,
                             data3a.CTENANT_NAME,
                             data3a.CTC_MESSAGE,
                             data3a.NTOTAL_PRICE
                         }).Select(data3b => new LOOStatusDetail3DTO()
                         {
                             CREF_NO = data3b.Key.CREF_NO,
                             DREF_DATE = ConvertStringToDate(data3b.Key.CREF_DATE,"yyyyMMdd"),
                             CTENURE = data3b.Key.CTENURE,
                             CAGREEMENT_STATUS_NAME = data3b.Key.CAGREEMENT_STATUS_NAME,
                             CTRANS_STATUS_NAME = data3b.Key.CTRANS_STATUS_NAME,
                             NREVISION_COUNT = data3b.Key.NREVISION_COUNT,
                             CTAX = data3b.Key.CTAX,
                             CTENANT_ID = data3b.Key.CTENANT_ID,
                             CTENANT_NAME = data3b.Key.CTENANT_NAME,
                             CTC_MESSAGE = data3b.Key.CTC_MESSAGE,
                             NTOTAL_PRICE = data3b.Key.NTOTAL_PRICE,
                             LOOStatusDetailUnit = data3b.GroupBy(dataUnit => new
                             {
                                 dataUnit.CUNIT_DETAIL_ID,
                                 dataUnit.CUNIT_DETAIL_NAME,
                                 dataUnit.NUNIT_DETAIL_GROSS_AREA_SIZE,
                                 dataUnit.NUNIT_DETAIL_NET_AREA_SIZE,
                                 dataUnit.NUNIT_DETAIL_COMMON_AREA_SIZE,
                                 dataUnit.NUNIT_DETAIL_PRICE,

                             }).Select(selectUnit => new LOOStatusDetailUnitDTO()
                             {
                                 CUNIT_DETAIL_ID = selectUnit.Key.CUNIT_DETAIL_ID,
                                 CUNIT_DETAIL_NAME = selectUnit.Key.CUNIT_DETAIL_NAME,
                                 NUNIT_DETAIL_GROSS_AREA_SIZE = selectUnit.Key.NUNIT_DETAIL_GROSS_AREA_SIZE,
                                 NUNIT_DETAIL_NET_AREA_SIZE = selectUnit.Key.NUNIT_DETAIL_NET_AREA_SIZE,
                                 NUNIT_DETAIL_COMMON_AREA_SIZE = selectUnit.Key.NUNIT_DETAIL_COMMON_AREA_SIZE,
                                 NUNIT_DETAIL_PRICE = selectUnit.Key.NUNIT_DETAIL_PRICE,

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
                                         DCHARGE_DETAIL_START_DATE = ConvertStringToDate(charge.CCHARGE_DETAIL_START_DATE,"yyyyMMdd"),
                                         DCHARGE_DETAIL_END_DATE = ConvertStringToDate(charge.CCHARGE_DETAIL_END_DATE,"yyyyMMdd"),
                                         CCHARGE_DETAIL_TENURE = charge.CCHARGE_DETAIL_TENURE,
                                         CCHARGE_DETAIL_FEE_METHOD = charge.CCHARGE_DETAIL_FEE_METHOD,
                                         NCHARGE_DETAIL_FEE_AMOUNT = charge.NCHARGE_DETAIL_FEE_AMOUNT,
                                         NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT = charge.NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT,
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
                                 DDEPOSIT_DETAIL_DATE = ConvertStringToDate(selectDeposit.Key.CDEPOSIT_DETAIL_DATE,"yyyyMMdd"),
                                 NDEPOSIT_DETAIL_AMOUNT = selectDeposit.Key.NDEPOSIT_DETAIL_AMOUNT,
                                 CDEPOSIT_DETAIL_DESCRIPTION = selectDeposit.Key.CDEPOSIT_DETAIL_DESCRIPTION,

                             }).ToList(),
                         }).ToList()
                     }).ToList()
                 }).ToList();

                #endregion
                _loggerPMR00100.LogInfo("Set Data Report");
                loData.DataLOOStatus = loTempData;
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
        //Helper Assign Object
        private object AssignValuesWithMessages(Type poResourceType,CultureInfo poCultureInfo,object poObject)
        {
            object loObj = Activator.CreateInstance(poObject.GetType());
            var loGetPropertyObject = poObject.GetType().GetProperties();

            foreach (var property in loGetPropertyObject)
            {
                string propertyName = property.Name;
                string message = R_Utility.R_GetMessage(poResourceType,propertyName,poCultureInfo);
                property.SetValue(loObj,message);
            }

            return loObj;
        }
        #endregion
    }
}
