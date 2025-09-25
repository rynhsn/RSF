using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using GLTR00100BACK;
using GLTR00100COMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
using System.Xml.Linq;

namespace GLTR00100SERVICE
{
    public class GLTR00100PrintController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private GLTR00100PrintParamDTO _AllGLTR00100Parameter;
        private LoggerGLTR00100Print _LoggerPrint;
        private readonly ActivitySource _activitySource;

        #region instantiate
        public GLTR00100PrintController(ILogger<LoggerGLTR00100Print> logger)
        {
            //Initial and Get Logger
            LoggerGLTR00100Print.R_InitializeLogger(logger);
            _LoggerPrint = LoggerGLTR00100Print.R_GetInstanceLogger();
            _activitySource = GLTR00100PrintActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GLTR00100PrintController));

            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }
        #endregion

        #region Event Handler
        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "GLTR00100.frx"); 
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            poData.Add(GenerateDataPrint(_AllGLTR00100Parameter));
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
        public R_DownloadFileResultDTO AllJournalTransactionPost(GLTR00100PrintParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("AllJournalTransactionPost");
            R_Exception loException = new R_Exception();
            _LoggerPrint.LogInfo("Start AllJournalTransactionPost");
            GLTR00100PrintLogKeyDTO<GLTR00100PrintParamDTO> loCache = null;
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new GLTR00100PrintLogKeyDTO<GLTR00100PrintParamDTO>
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY)
                };

                // Set Guid Param 
                _LoggerPrint.LogInfo("Set GUID Param AllJournalTransactionPost");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<GLTR00100PrintLogKeyDTO<GLTR00100PrintParamDTO>>(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _LoggerPrint.LogError(loException);
            }
            _LoggerPrint.LogInfo("End AllJournalTransactionPost");
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult AllStreamJournalTransactionsGet(string pcGuid)
        {
            using Activity activity = _activitySource.StartActivity("AllStreamJournalTransactionsGet");
            R_Exception loException = new R_Exception();
            GLTR00100PrintLogKeyDTO<GLTR00100PrintParamDTO> loResultGUID = null;
            FileStreamResult loRtn = null;
            _LoggerPrint.LogInfo("Start AllStreamJournalTransactionsGet");
            try
            {
                //Get Parameter
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<GLTR00100PrintLogKeyDTO<GLTR00100PrintParamDTO>>(R_DistributedCache.Cache.Get(pcGuid));

                //Get Data and Set Log Key
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                _AllGLTR00100Parameter = loResultGUID.poParam;

                _LoggerPrint.LogInfo("Read File Report AllStreamJournalTransactionsGet");
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _LoggerPrint.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _LoggerPrint.LogInfo("End AllStreamJournalTransactionsGet");

            return loRtn;
        }

        #region Helper
        private GLTR00100ResultWithBaseHeaderPrintDTO GenerateDataPrint(GLTR00100PrintParamDTO poParam)
        {
            System.Globalization.CultureInfo loCultureInfo = new System.Globalization.CultureInfo(poParam.CREPORT_CULTURE);
            using Activity activity = _activitySource.StartActivity("GenerateDataPrint");
            var loEx = new R_Exception();
            GLTR00100ResultWithBaseHeaderPrintDTO loRtn = new GLTR00100ResultWithBaseHeaderPrintDTO();

            try
            {
                //Add Resources
                loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Page", loCultureInfo);
                loRtn.BaseHeaderColumn.Of = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_Date = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_By", loCultureInfo);

                //Assign Column
                GLTR00100ColumnDTO loColumnObject = new GLTR00100ColumnDTO();
                var loColumn = AssignValuesWithMessages(typeof(GLTR00100BackResources.Resources_Dummy_Class), loCultureInfo, loColumnObject);
                loRtn.ColumnData = (GLTR00100ColumnDTO)loColumn;

                var loCls = new GLTR00100Cls(_LoggerPrint);

                _LoggerPrint.LogInfo("Call Method GetReportJournalTransaction Report");
                var loDetailData = loCls.GetReportJournalTransaction(poParam);

                GLTR00100ResultPrintDTO loData = new GLTR00100ResultPrintDTO();

                // Set Header Data
                var loHeaderData = R_Utility.R_ConvertObjectToObject<GLTR00100PrintDTO, GLTR00101DTO>(loDetailData.FirstOrDefault());

                //Convert Header Data
                loHeaderData.DDOC_DATE = !string.IsNullOrWhiteSpace(loHeaderData.CDOC_DATE)
                                        ? DateTime.ParseExact(loHeaderData.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture)
                                        : default;
                loHeaderData.DREF_DATE = !string.IsNullOrWhiteSpace(loHeaderData.CREF_DATE)
                                        ? DateTime.ParseExact(loHeaderData.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture)
                                        : default;
                loHeaderData.DREVERSE_DATE = !string.IsNullOrWhiteSpace(loHeaderData.CREVERSE_DATE)
                                        ? DateTime.ParseExact(loHeaderData.CREVERSE_DATE, "yyyyMMdd", CultureInfo.InvariantCulture)
                                        : default;

                // Set Detail Data
                _LoggerPrint.LogInfo("Grouping Report");
                List<GLTR00102DTO> loDetail = new List<GLTR00102DTO>();
                foreach (var item in loDetailData)
                {
                    //Convert Detail Data
                    var itemDetail = new GLTR00102DTO
                    {
                        CGLACCOUNT_NO = item.CGLACCOUNT_NO,
                        CGLACCOUNT_NAME = item.CGLACCOUNT_NAME,
                        CCENTER_CODE = item.CCENTER_CODE,
                        CCENTER_NAME = item.CCENTER_NAME,
                        CDBCR = item.CDBCR,
                        NTRANS_AMOUNT = item.NTRANS_AMOUNT, // Example decimal value
                        CDETAIL_DESC = item.CDETAIL_DESC,
                        CDOCUMENT_NO = item.CDOCUMENT_NO,
                        CDOCUMENT_DATE = item.CDOCUMENT_DATE,
                        DDOCUMENT_DATE = !string.IsNullOrWhiteSpace(item.CDOCUMENT_DATE)
                                        ? DateTime.ParseExact(item.CDOCUMENT_DATE, "yyyyMMdd", CultureInfo.InvariantCulture)
                                        : default
                    };

                    loDetail.Add(itemDetail);
                }

                // set Base Header Common
                _LoggerPrint.LogInfo("Set BaseHeader Report");
                var loParam = new BaseHeaderDTO()
                {
                    CCOMPANY_NAME = "PT Realta Chackradarma",
                    CPRINT_CODE = poParam.CCOMPANY_ID.ToUpper(),
                    CPRINT_NAME = "JOURNAL TRANSACTION",
                    CUSER_ID = poParam.CUSER_ID.ToUpper(),
                };
                var loBaseHeader = loCls.GetBaseHeaderLogoCompany(poParam);
                loParam.BLOGO_COMPANY = loBaseHeader.CLOGO;

                // Assign Data
                loData.HeaderData = loHeaderData;
                loData.ListDetail = loDetail;

                // Assign Data with Base Header Data
                _LoggerPrint.LogInfo("Set Data Report");
                loRtn.BaseHeaderData = loParam;
                loRtn.GLTR = loData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _LoggerPrint.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        //Helper Assign Object
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