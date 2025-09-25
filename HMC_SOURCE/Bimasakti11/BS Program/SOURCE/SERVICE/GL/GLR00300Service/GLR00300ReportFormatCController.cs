
using GLR00300Back;
using GLR00300Common.GLR00300Print;
using GLR00300Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_ReportFastReportBack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseHeaderReportCOMMON;
using GLR00300Common.Logs;
using Microsoft.Extensions.Logging;
using GLR00300Service.DTOLogs;
using R_CommonFrontBackAPI.Log;
using System.Diagnostics;
using GLR00300BackResources;
using System.Globalization;

namespace GLR00300Service
{
    public class GLR00300ReportFormatCController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private GLR00300ParamDBToGetReportDTO _Parameter;
        private LoggerGLR00300 _loggerGLR00300Report;
        private readonly ActivitySource _activitySource;
        #region instantiate

        public GLR00300ReportFormatCController(ILogger<GLR00300ReportFormatCController> logger)
        {
            LoggerGLR00300.R_InitializeLogger(logger);
            _loggerGLR00300Report = LoggerGLR00300.R_GetInstanceLogger();
            _activitySource = GLR00300Activity.R_InitializeAndGetActivitySource(nameof(GLR00300ReportFormatCController));

            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }

        #endregion

        #region Event Handler

        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "GLR00300AccountTrialBalanceC.frx");
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

        #endregion

        [HttpPost]
        public R_DownloadFileResultDTO AllTrialBalanceReportPost(GLR00300ParamDBToGetReportDTO poParameter)
        {
            string lcMethodName = nameof(AllTrialBalanceReportPost);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerGLR00300Report.LogInfo(string.Format("START method {0} on Format C", lcMethodName));

            R_Exception loException = new R_Exception();
            R_DownloadFileResultDTO loRtn = null;
            GLR00300ReportLogKeyDTO<GLR00300ParamDBToGetReportDTO> loCache = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new GLR00300ReportLogKeyDTO<GLR00300ParamDBToGetReportDTO>
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY),
                    poReportGlobalDTO = R_ReportGlobalVar.R_GetReportDTO()
                };

                // Set Guid Param 
                _loggerGLR00300Report.LogInfo("Set GUID Param on method post");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<GLR00300ReportLogKeyDTO<GLR00300ParamDBToGetReportDTO>>(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGLR00300Report.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _loggerGLR00300Report.LogInfo(string.Format("END method {0} on Format C", lcMethodName));

            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult AllTrialBalanceReportGet(string pcGuid)
        {
            string lcMethodName = nameof(AllTrialBalanceReportGet);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGLR00300Report.LogInfo(string.Format("START method {0} on Format C", lcMethodName));

            R_Exception loException = new R_Exception();
            FileStreamResult loRtn = null;
            GLR00300ReportLogKeyDTO<GLR00300ParamDBToGetReportDTO> loDataFromRedis = null;
            try
            {
                //Get Parameter
                loDataFromRedis = R_NetCoreUtility.R_DeserializeObjectFromByte<GLR00300ReportLogKeyDTO<GLR00300ParamDBToGetReportDTO>>(R_DistributedCache.Cache.Get(pcGuid));

                //Get Data and Set Log Key
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loDataFromRedis.poLogKey);
                R_ReportGlobalVar.R_SetFromReportDTO(loDataFromRedis.poReportGlobalDTO);
                _Parameter = loDataFromRedis.poParam;

                _loggerGLR00300Report.LogInfo(string.Format("READ file report method {0}", lcMethodName));
                _loggerGLR00300Report.LogDebug("READ poParameter {@objectQuery}", _Parameter);
                R_FileType loFileType = new();
                if (loDataFromRedis.poParam.LIS_PRINT)
                {
                    _loggerGLR00300Report.LogInfo(string.Format("Normal Print"));
                    loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF), R_ReportUtility.GetMimeType(R_FileType.PDF));

                }
                else
                {
                    _loggerGLR00300Report.LogInfo(string.Format("Save As"));
                    switch (loDataFromRedis.poParam.CREPORT_FILETYPE)
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
                    loRtn = File(_ReportCls.R_GetStreamReport(peExport: loFileType), R_ReportUtility.GetMimeType(loFileType), $"{loDataFromRedis.poParam.CREPORT_FILENAME}.{loDataFromRedis.poParam.CREPORT_FILETYPE}");

                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGLR00300Report.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _loggerGLR00300Report.LogInfo(string.Format("END method {0} on Format C", lcMethodName));

            return loRtn;
        }

        #region Helper

        private GLR00300AccountTrialBalanceResult_FormatAtoD_WithBaseHeaderDTO GenerateDataPrint(
            GLR00300ParamDBToGetReportDTO poParam)
        {
            string lcMethodName = nameof(GenerateDataPrint);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGLR00300Report.LogInfo(string.Format("START method {0} on Format C", lcMethodName));

            var loException = new R_Exception();
            GLR00300AccountTrialBalanceResult_FormatAtoD_WithBaseHeaderDTO loRtn =
                new GLR00300AccountTrialBalanceResult_FormatAtoD_WithBaseHeaderDTO();

            GLR00300Cls loCls = null;
            GLR00300AccountTrialBalanceResultFormat_AtoD_DTO loData = null;
            List<GLR00300DataAccountTrialBalanceAD> loConvertData = null;
            string lcPeriod;

            System.Globalization.CultureInfo loCultureInfo = new System.Globalization.CultureInfo(R_BackGlobalVar.REPORT_CULTURE);
            try
            {
                //Add Resources
                loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Page", loCultureInfo);
                loRtn.BaseHeaderColumn.Of = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_Date = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_By", loCultureInfo);

                loCls = new GLR00300Cls();
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _loggerGLR00300Report.LogInfo("Call Method GetAllTrialBalanceReportData");
                var loCollectionFromDb = loCls.GetAllTrialBalanceReportData(poParam);
                loConvertData = FromRaw_To_Display(loCollectionFromDb);

                //CONDITIONAL IF DATA FROM USER TO DB NO RESULT (NULL)
                if (loCollectionFromDb.Count > 0)
                {
                    GLR00300_DataDetail_AccountTrialBalance getFirstDataForHeader = loCollectionFromDb.FirstOrDefault();
                    loData = new GLR00300AccountTrialBalanceResultFormat_AtoD_DTO()
                    {
                        Header = new GLR00300HeaderAccountTrialBalanceDTO()
                        {
                            CPERIOD = getFirstDataForHeader.CPERIOD_NAME,
                            CFROM_ACCOUNT_NO = getFirstDataForHeader.CFROM_ACCOUNT_NO,
                            CTO_ACCOUNT_NO = getFirstDataForHeader.CTO_ACCOUNT_NO,
                            CFROM_CENTER_CODE = getFirstDataForHeader.CFROM_CENTER_CODE,
                            CTO_CENTER_CODE = getFirstDataForHeader.CTO_CENTER_CODE,
                            CTB_TYPE_NAME = getFirstDataForHeader.CTB_TYPE_NAME,
                            CCURRENCY = getFirstDataForHeader.CCURRENCY,
                            CJOURNAL_ADJ_MODE_NAME = getFirstDataForHeader.CJOURNAL_ADJ_MODE_NAME,
                            CPRINT_METHOD_NAME = getFirstDataForHeader.CPRINT_METHOD_NAME,
                            CBUDGET_NO = getFirstDataForHeader.CBUDGET_NO,
                        },

                    };
                }
                else
                {
                    lcPeriod = poParam.CYEAR + "-" + poParam.CTO_PERIOD_NO;
                    loData = new GLR00300AccountTrialBalanceResultFormat_AtoD_DTO()
                    {
                        Header = new GLR00300HeaderAccountTrialBalanceDTO()
                        {
                            CPERIOD = lcPeriod,
                            CFROM_ACCOUNT_NO = poParam.CFROM_ACCOUNT_NO,
                            CTO_ACCOUNT_NO = poParam.CTO_ACCOUNT_NO,
                            CFROM_CENTER_CODE = poParam.CFROM_CENTER_CODE,
                            CTO_CENTER_CODE = poParam.CTO_CENTER_CODE,
                            CTB_TYPE_NAME = "Normal",
                            CCURRENCY = "",
                            CJOURNAL_ADJ_MODE_NAME = "Split",
                            CPRINT_METHOD_NAME = "",
                            CBUDGET_NO = poParam.CBUDGET_NO,
                        }
                    };
                }
                loData.Title = "Account Trial Balance";
                loData.Column = new AccountTrialBalanceColumnDTO()
                {
                    Col_ACCOUNT_NO = R_Utility.R_GetMessage(typeof(Resources_GLR00300), "_accountNo", loCultureInfo),
                    Col_ACCOUNT_NAME = R_Utility.R_GetMessage(typeof(Resources_GLR00300), "_accountName", loCultureInfo),
                    Col_D_C = R_Utility.R_GetMessage(typeof(Resources_GLR00300), "_D/C", loCultureInfo),
                    Col_BS_IS = R_Utility.R_GetMessage(typeof(Resources_GLR00300), "_BS/IS", loCultureInfo),
                    Col_BEG_BALANCE = R_Utility.R_GetMessage(typeof(Resources_GLR00300), "_begBalance", loCultureInfo),
                    Col_Center = R_Utility.R_GetMessage(typeof(Resources_GLR00300), "_center", loCultureInfo),
                    Col_DEBIT = R_Utility.R_GetMessage(typeof(Resources_GLR00300), "_debit", loCultureInfo),
                    Col_CREDIT = R_Utility.R_GetMessage(typeof(Resources_GLR00300), "_credit", loCultureInfo),
                    Col_DEBIT_ADJ = R_Utility.R_GetMessage(typeof(Resources_GLR00300), "_debitAdj", loCultureInfo),
                    Col_CREDIT_ADJ = R_Utility.R_GetMessage(typeof(Resources_GLR00300), "_creditAdj", loCultureInfo),
                    Col_END_BALANCE = R_Utility.R_GetMessage(typeof(Resources_GLR00300), "_endBalance", loCultureInfo),
                    Col_NBUDGET = R_Utility.R_GetMessage(typeof(Resources_GLR00300), "_mtdBudget", loCultureInfo)
                };
                loData.Label = new GLR00300LabelDTO()
                {
                    Label_Period = R_Utility.R_GetMessage(typeof(Resources_GLR00300), "_labelPeriod", loCultureInfo),
                    Label_AccountNo = R_Utility.R_GetMessage(typeof(Resources_GLR00300), "_accountNo", loCultureInfo),
                    Label_Center = R_Utility.R_GetMessage(typeof(Resources_GLR00300), "_center", loCultureInfo),
                    Label_To = R_Utility.R_GetMessage(typeof(Resources_GLR00300), "_to", loCultureInfo),
                    Label_TrialBalanceType = R_Utility.R_GetMessage(typeof(Resources_GLR00300), "_labelTrialBalanceType", loCultureInfo),
                    Label_Currency = R_Utility.R_GetMessage(typeof(Resources_GLR00300), "_labelCurrency", loCultureInfo),
                    Label_JournalAdjMode = R_Utility.R_GetMessage(typeof(Resources_GLR00300), "_labelJournalAdjMode", loCultureInfo),
                    Label_PrintMethod = R_Utility.R_GetMessage(typeof(Resources_GLR00300), "_labelPrintMode", loCultureInfo),
                    Label_BudgetNo = R_Utility.R_GetMessage(typeof(Resources_GLR00300), "_labelBudgetNo", loCultureInfo),
                    Label_GrandTotal = R_Utility.R_GetMessage(typeof(Resources_GLR00300), "_labelGrandTotal", loCultureInfo),
                    Label_Difference = R_Utility.R_GetMessage(typeof(Resources_GLR00300), "_labelDifference", loCultureInfo),
                    Label_Note = R_Utility.R_GetMessage(typeof(Resources_GLR00300), "_note", loCultureInfo)
                };
                _loggerGLR00300Report.LogInfo("Set BaseHeader Report");
                //Assign raw data to Data list display
                loData.Data = loConvertData;
                var loParam = new BaseHeaderDTO()
                {
                    CPRINT_CODE = R_BackGlobalVar.PROGRAM_ID.ToUpper(),
                    CPRINT_NAME = "Account Trial Balance",
                    CUSER_ID = poParam.CUSER_ID,
                };
                var loBaseHeader = loCls.GetBaseHeaderLogoCompany(poParam);
                loParam.BLOGO_COMPANY = loBaseHeader.CLOGO!;
                loParam.CCOMPANY_NAME = loBaseHeader.CCOMPANY_NAME!;
                loParam.DPRINT_DATE_COMPANY = DateTime.ParseExact(loBaseHeader.CDATETIME_NOW, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture);


                _loggerGLR00300Report.LogInfo("Set Data Report");
                loRtn.BaseHeaderData = loParam;
                loRtn.GLR00300AccountTrialBalanceResult_FormatAtoD_DataFormat = loData;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGLR00300Report.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _loggerGLR00300Report.LogInfo("END Method GenerateDataPrint on Controller");

            return loRtn;
        }

        private List<GLR00300DataAccountTrialBalanceAD> FromRaw_To_Display(
            List<GLR00300_DataDetail_AccountTrialBalance> poCollectionDataRaw)
        {
            string lcMethodName = "Convert data to display";
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGLR00300Report.LogInfo(string.Format("START method {0} on Format C", lcMethodName));

            var loException = new R_Exception();
            List<GLR00300DataAccountTrialBalanceAD> loReturn = null;
            try
            {
                loReturn = new List<GLR00300DataAccountTrialBalanceAD>();

                loReturn = poCollectionDataRaw.Select(item
                    => new GLR00300DataAccountTrialBalanceAD
                    {
                        CGLACCOUNT_NO = item.CGLACCOUNT_NO,
                        CGLACCOUNT_NAME = item.CGLACCOUNT_NAME,
                        CDBCR = item.CDBCR,
                        CBSIS = item.CBSIS,
                        NBEGIN_BALANCE = item.NBEGIN_BALANCE,
                        NDEBIT = item.NDEBIT,
                        NCREDIT = item.NCREDIT,
                        NDEBIT_ADJ = item.NDEBIT_ADJ,
                        NCREDIT_ADJ = item.NCREDIT_ADJ,
                        NEND_BALANCE = item.NCREDIT_ADJ,
                        NBUDGET = item.NBUDGET
                    }).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGLR00300Report.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _loggerGLR00300Report.LogInfo("END method for convert data to display");

            return loReturn;

            #endregion

        }
    }
}
