using BaseHeaderReportCOMMON;
using PMF00200BACK;
using PMF00200COMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
using R_FileType = R_ReportServerCommon.R_FileType;
using R_ReportFormatDTO = R_ReportServerCommon.R_ReportFormatDTO;
using PMF00200ReportCommon;

namespace PMF00200SERVICE
{
    public class PMF00200PrintController : R_ReportControllerBase
    {
        private PMF00200InputParameterDTO _AllReceiptParameter;
        private LoggerPMF00200 _LoggerPrint;
        private readonly ActivitySource _activitySource;
        private ReportFormatDTO _reportFormat = new ReportFormatDTO();

        #region instantiate
        public PMF00200PrintController(ILogger<LoggerPMF00200> logger)
        {
            //Initial and Get Logger
            LoggerPMF00200.R_InitializeLogger(logger);
            _LoggerPrint = LoggerPMF00200.R_GetInstanceLogger();
            _activitySource = PMF00200ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMF00200PrintController));
        }
        #endregion

        #region Event Handler
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
        public R_DownloadFileResultDTO AllRecepitPrintPost(PMF00200InputParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("AllRecepitPrintPost");
            R_Exception loException = new R_Exception();
            _LoggerPrint.LogInfo("Start AllRecepitPrintPost");
            PMF00200PrintLogKeyDTO<PMF00200InputParameterDTO> loCache = null;

            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new PMF00200PrintLogKeyDTO<PMF00200InputParameterDTO>
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY),
                    poReportGlobal = R_ReportGlobalVar.R_GetReportDTO()
                };

                // Set Guid Param 
                _LoggerPrint.LogInfo("Set GUID Param AllRecepitPrintPost");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<PMF00200PrintLogKeyDTO<PMF00200InputParameterDTO>>(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _LoggerPrint.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _LoggerPrint.LogInfo("End AllRecepitPrintPost");

            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public async Task<FileContentResult> AllReceiptPrintGet(string pcGuid)
        {
            using Activity activity = _activitySource.StartActivity("AllReceiptPrintGet");
            R_Exception loException = new R_Exception();
            FileContentResult loRtn = null;
            PMF00200PrintLogKeyDTO<PMF00200InputParameterDTO> loResultGUID = null;
            _LoggerPrint.LogInfo("Start AllReceiptPrintGet");

            PMF00200ResultWithBaseHeaderPrintDTO loData = null;
            R_GenerateReportParameter loParameter;
            R_ReportServerRule loReportRule;
            R_FileType leReportOutputType;
            byte[] loRtnInByte;
            string lcReportFileName = "";

            try
            {

                //Get Parameter
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<PMF00200PrintLogKeyDTO<PMF00200InputParameterDTO>>(R_DistributedCache.Cache.Get(pcGuid));

                //Get Data and Set Log Key
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                R_ReportGlobalVar.R_SetFromReportDTO(loResultGUID.poReportGlobal);
                _AllReceiptParameter = loResultGUID.poParam;

                loData = GenerateDataPrint(loResultGUID.poParam);
                _reportFormat = GetReportFormat();

                loReportRule = new R_ReportServerRule(R_BackGlobalVar.TENANT_ID.ToLower(), R_BackGlobalVar.COMPANY_ID.ToLower());
                lcReportFileName = "CustomerReceipt.frx";
                leReportOutputType = R_FileType.PDF;
                loParameter = new R_GenerateReportParameter()
                {
                    ReportRule = loReportRule,
                    ReportFileName = lcReportFileName,
                    ReportData = JsonSerializer.Serialize<PMF00200ResultWithBaseHeaderPrintDTO>(loData),
                    ReportDataSourceName = "ResponseDataModel",
                    ReportFormat = R_Utility.R_ConvertObjectToObject<ReportFormatDTO, R_ReportFormatDTO>(_reportFormat),
                    ReportDataType = typeof(PMF00200ResultWithBaseHeaderPrintDTO).ToString(),
                    ReportOutputType = leReportOutputType,
                    ReportAssemblyName = "PMF00200ReportCommon.dll",
                    ReportParameter = null
                };

                // Generate Report
                loRtnInByte = await R_ReportServerUtility.R_GenerateReportByte(R_ReportServerClientService.R_GetHttpClient(),
                    "api/ReportServer/GetReport", loParameter);
                loRtn = File(loRtnInByte, R_ReportUtility.GetMimeType((R_ReportFastReportBack.R_FileType)R_FileType.PDF));
                _LoggerPrint.LogInfo("Report generated successfully.");
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _LoggerPrint.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _LoggerPrint.LogInfo("End AllReceiptPrintGet");

            return loRtn;
        }

        #region Helper
        private PMF00200ResultWithBaseHeaderPrintDTO GenerateDataPrint(PMF00200InputParameterDTO poParam)
        {
            System.Globalization.CultureInfo loCultureInfo = new System.Globalization.CultureInfo(R_BackGlobalVar.REPORT_CULTURE);
            using Activity activity = _activitySource.StartActivity("GenerateDataPrint");
            var loEx = new R_Exception();
            PMF00200ResultWithBaseHeaderPrintDTO loRtn = new PMF00200ResultWithBaseHeaderPrintDTO();

            try
            {
                
                //Add Resources
                loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Page", loCultureInfo);
                loRtn.BaseHeaderColumn.Of = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_Date = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_By", loCultureInfo);

                //Assign Column
                PMF00200ColumnPrintDTO loColumnObject = new PMF00200ColumnPrintDTO();
                var loColumn = AssignValuesWithMessages(typeof(PMF00200BackResources.Resources_Dummy_Class), loCultureInfo, loColumnObject);
                loRtn.Column = (PMF00200ColumnPrintDTO)loColumn;

                //Assign Data
                PMF00200Cls loCls = new PMF00200Cls();
                PMF00200ResultPrintDTO loData = new PMF00200ResultPrintDTO();

                _LoggerPrint.LogInfo("Set BaseHeader Report");
                var loParam = new BaseHeaderDTO()
                {
                    CCOMPANY_NAME = "PT Realta Chackradarma",
                    CPRINT_CODE = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                };
                if (poParam.PARAM_RECEIPT_TYPE == "CA")
                {
                    loParam.CPRINT_NAME = R_Utility.R_GetMessage(typeof(PMF00200BackResources.Resources_Dummy_Class), "CATitleName", loCultureInfo);
                }
                else if (poParam.PARAM_RECEIPT_TYPE == "WT")
                {
                    loParam.CPRINT_NAME = R_Utility.R_GetMessage(typeof(PMF00200BackResources.Resources_Dummy_Class), "WTTitleName", loCultureInfo);
                }
                else if (poParam.PARAM_RECEIPT_TYPE == "CQ")
                {
                    loParam.CPRINT_NAME = R_Utility.R_GetMessage(typeof(PMF00200BackResources.Resources_Dummy_Class), "CQTitleName", loCultureInfo);
                }

                var loBaseHeader = loCls.GetBaseHeaderLogoCompany(R_BackGlobalVar.COMPANY_ID);
                loParam.BLOGO_COMPANY = loBaseHeader.CLOGO;

                var loHeaderData = loCls.GetJournalReport(poParam);
                var loGSMCompanyInfo = loCls.GetCompanyInfoPrint(R_BackGlobalVar.COMPANY_ID);
                loHeaderData.DREF_DATE_DISPLAY = DateTime.TryParseExact(loHeaderData.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldRefDate) ? ldRefDate : null;
                loHeaderData.DDOC_DATE_DISPLAY = DateTime.TryParseExact(loHeaderData.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldDocDate) ? ldDocDate : null;
                loHeaderData.DCHEQUE_DATE_DISPLAY = DateTime.TryParseExact(loHeaderData.CCHEQUE_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldChequeDate) ? ldChequeDate : null;
                loHeaderData.PARAM_RECEIPT_TYPE = poParam.PARAM_RECEIPT_TYPE;
                loHeaderData.PRINT_OPTION = poParam.PRINT_OPTION;
                loHeaderData.BASE_CURRENCY_CODE = loGSMCompanyInfo.CBASE_CURRENCY_CODE;
                loHeaderData.CLOCAL_CURRENCY_CODE = loGSMCompanyInfo.CLOCAL_CURRENCY_CODE;

                loData.Header = loHeaderData;
                var loAllocList = loCls.GetAllocReceiptList(R_BackGlobalVar.COMPANY_ID, R_BackGlobalVar.REPORT_CULTURE, string.IsNullOrWhiteSpace(poParam.PARAM_RECEIPT_ID) ? "" : poParam.PARAM_RECEIPT_ID);
                loAllocList.ForEach(x =>
                {
                    x.DALLOC_DATE = DateTime.TryParseExact(x.CALLOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldAllocDate) ? ldAllocDate : null;
                });
                loData.AllocList = loAllocList;
                
                var loJournalList = loCls.GetJournalReceiptList(R_BackGlobalVar.COMPANY_ID, R_BackGlobalVar.REPORT_CULTURE, string.IsNullOrWhiteSpace(poParam.PARAM_RECEIPT_ID) ? "" : poParam.PARAM_RECEIPT_ID);
                loJournalList.ForEach(x =>
                {
                    x.DREF_DATE = DateTime.TryParseExact(x.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldRefDateJournal) ? ldRefDateJournal : null;
                });
                loData.JournalList = loJournalList;

                _LoggerPrint.LogInfo("Set Data Report");
                loRtn.BaseHeaderData = loParam;
                loRtn.Data = loData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _LoggerPrint.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

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
        #endregion
    }
}