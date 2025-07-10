using BaseHeaderReportCOMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMF00200BACK;
using PMF00200COMMON;
using PMF00200ReportCommon;
using R_BackEnd;
using R_Common;
using R_ReportServerClient;
using R_ReportServerCommon;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Xml.Linq;

namespace PMF00200SERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMF00200Controller : ControllerBase, PMF00200BACK.IPMF00200
    {
        private LoggerPMF00200 _Logger;
        private readonly ActivitySource _activitySource;
        public PMF00200Controller(ILogger<PMF00200Controller> logger)
        {
            LoggerPMF00200.R_InitializeLogger(logger);
            _Logger = LoggerPMF00200.R_GetInstanceLogger();
            _activitySource = PMF00200ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMF00200Controller));
        }

        [HttpPost]
        public async Task<PMF00200Record<PMF00200AllInitialProcessDTO>> GetAllInitialProcess(PMF00200InputParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetAllInitialProcess");
            var loEx = new R_Exception();
            PMF00200Record<PMF00200AllInitialProcessDTO> loRtn = new PMF00200Record<PMF00200AllInitialProcessDTO>();
            _Logger.LogInfo("Start GetAllInitialProcess");

            try
            {
                var loCls = new PMF00200Cls();
                _Logger.LogInfo("Call All Back Method GetAllInitialProcess");
                var loTempResult = new PMF00200AllInitialProcessDTO
                {
                    VAR_GSM_COMPANY = await loCls.GetCompanyInfoRecord(),
                    VAR_REPORT_TEMPLATE_LIST = await loCls.GetReportTempateList(poEntity),
                };

                loRtn.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllInitialProcess");

            return loRtn;
        }

        [HttpPost]
        public async Task<PMF00200Record<PMF00200DTO>> GetJournalRecord(PMF00200InputParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetJournalRecord");
            var loEx = new R_Exception();
            PMF00200Record<PMF00200DTO> loRtn = new PMF00200Record<PMF00200DTO>();
            _Logger.LogInfo("Start GetJournalRecord");

            try
            {
                var loCls = new PMF00200Cls();

                _Logger.LogInfo("Call Back Method GetJournalDisplay");
                loRtn.Data = await loCls.GetJournalDisplay(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetJournalRecord");

            return loRtn;
        }

        [HttpPost]
        public async Task<PMF00200Record<string>> SendEmail(PMF00200InputParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("SendEmail");
            var loEx = new R_Exception();
            PMF00200Record<string> loRtn = new PMF00200Record<string>();
            PMF00200ResultWithBaseHeaderPrintDTO loData = null;
            R_GenerateReportParameter loParameter;
            R_ReportServerRule loReportRule;
            R_FileType leReportOutputType;
            byte[] loRtnInByte;
            string lcReportFileName = "";
            _Logger.LogInfo("Start SendEmail");

            try
            {
                //Set Parameter
                loData = GenerateDataPrint(poEntity);
                loReportRule = new R_ReportServerRule(R_BackGlobalVar.TENANT_ID.ToLower(), R_BackGlobalVar.COMPANY_ID.ToLower());
                if (poEntity.RECEIPT_TEMPLATE == "TRC02")
                {
                    lcReportFileName = "CRC02" + ".frx";
                }
                else
                {
                    lcReportFileName = "CustomerReceipt" + ".frx";
                }
                leReportOutputType = R_FileType.PDF;
                loParameter = new R_GenerateReportParameter()
                {
                    ReportRule = loReportRule,
                    ReportFileName = lcReportFileName,
                    ReportData = JsonSerializer.Serialize<PMF00200ResultWithBaseHeaderPrintDTO>(loData),
                    ReportDataSourceName = "ResponseDataModel",
                    ReportFormat = R_Utility.R_ConvertObjectToObject<ReportFormatDTO, R_ReportFormatDTO>(GetReportFormat()),
                    ReportDataType = typeof(PMF00200ResultWithBaseHeaderPrintDTO).ToString(),
                    ReportOutputType = leReportOutputType,
                    ReportAssemblyName = "PMF00200ReportCommon.dll",
                    ReportParameter = null
                };

                // Generate byte Report
                loRtnInByte = await R_ReportServerUtility.R_GenerateReportByte(R_ReportServerClientService.R_GetHttpClient(),
                    "api/ReportServer/GetReport", loParameter);

                PMF00200Cls loCls = new PMF00200Cls();
                string lcRtn = loCls.SendEmailProcess(loRtnInByte, poEntity);
                loRtn.Data = lcRtn;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End SendEmail");

            return loRtn;
        }

        #region Set Report Format
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

                _Logger.LogInfo("Set BaseHeader Report");
                var loParam = new BaseHeaderDTO()
                {
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

                var loBaseHeader = loCls.GetBaseHeaderLogoCompany();
                loParam.BLOGO_COMPANY = loBaseHeader.CLOGO;
                loParam.CCOMPANY_NAME = loBaseHeader.CCOMPANY_NAME;
                loParam.DPRINT_DATE_COMPANY = DateTime.ParseExact(loBaseHeader.CDATETIME_NOW, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture);

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

                _Logger.LogInfo("Set Data Report");
                loRtn.BaseHeaderData = loParam;
                loRtn.Data = loData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
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
