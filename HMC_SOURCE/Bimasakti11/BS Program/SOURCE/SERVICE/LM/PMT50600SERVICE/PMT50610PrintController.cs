using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI.Log;
using R_CommonFrontBackAPI;
using System;
using System.Collections;
using R_Cache;
using R_ReportFastReportBack;
using PMT50600COMMON.DTOs.PMT50610Print;
using PMT50600SERVICE.DTOs;
using PMT50600COMMON.Loggers;
using PMT50600BACK;
using BaseHeaderReportCOMMON;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using PMT50600BACK.OpenTelemetry;
using System.Globalization;

namespace PMT50600SERVICE
{
    public class PMT50610PrintController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private PMT50610PrintReportParameterDTO _AllReportParameter;
        private LoggerPMT50610Print _logger;
        private readonly ActivitySource _activitySource;

        #region instantiate
        public PMT50610PrintController(ILogger<PMT50610PrintController> logger)
        {
            LoggerPMT50610Print.R_InitializeLogger(logger);
            _logger = LoggerPMT50610Print.R_GetInstanceLogger();
            _activitySource = PMT50610PrintActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT50610PrintController));
            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }
        #endregion

        #region Event Handler
        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            using Activity activity = _activitySource.StartActivity("_ReportCls_R_InstantiateMainReportWithFileName");
            _logger.LogInfo("Start || _ReportCls_R_InstantiateMainReportWithFileName(Controller)");
            pcFileTemplate = System.IO.Path.Combine("Reports", "PMT50600PrintReport.frx");
            _logger.LogInfo("End || _ReportCls_R_InstantiateMainReportWithFileName(Controller)");
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            using Activity activity = _activitySource.StartActivity("_ReportCls_R_GetMainDataAndName");
            _logger.LogInfo("Start || _ReportCls_R_GetMainDataAndName(Controller)");
            poData.Add(GenerateDataPrint(_AllReportParameter));
            pcDataSourceName = "ResponseDataModel";
            _logger.LogInfo("End || _ReportCls_R_InstantiateMainReportWithFileName(Controller)");
        }

        private void _ReportCls_R_SetNumberAndDateFormat(ref R_ReportFormatDTO poReportFormat)
        {
            using Activity activity = _activitySource.StartActivity("_ReportCls_R_SetNumberAndDateFormat");
            _logger.LogInfo("Start || _ReportCls_R_SetNumberAndDateFormat(Controller)");
            poReportFormat.DecimalSeparator = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_SEPARATOR;
            poReportFormat.GroupSeparator = R_BackGlobalVar.REPORT_FORMAT_GROUP_SEPARATOR;
            poReportFormat.DecimalPlaces = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_PLACES;
            poReportFormat.ShortDate = R_BackGlobalVar.REPORT_FORMAT_SHORT_DATE;
            poReportFormat.ShortTime = R_BackGlobalVar.REPORT_FORMAT_SHORT_TIME;
            _logger.LogInfo("End || _ReportCls_R_SetNumberAndDateFormat(Controller)");
        }
        #endregion

        [HttpPost]
        public R_DownloadFileResultDTO PrintReportPost(PMT50610PrintReportParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("PrintReportPost");
            _logger.LogInfo("Start || PrintReportPost(Controller)");
            R_Exception loException = new R_Exception();
            R_DownloadFileResultDTO loRtn = null;
            PMT50610PrintLogKeyDTO loCache = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();

                loCache = new PMT50610PrintLogKeyDTO
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY)
                };

                _logger.LogInfo("Set GUID Param || PrintReportPost(Controller)");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<PMT50610PrintLogKeyDTO>(loCache));
            }
            catch (Exception ex)
            {
                _logger.LogError(loException);
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || PrintReportPost(Controller)");
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult PrintReportGet(string pcGuid)
        {
            using Activity activity = _activitySource.StartActivity("PrintReportGet");
            _logger.LogInfo("Start || PrintReportGet(Controller)");
            R_Exception loException = new R_Exception();
            PMT50610PrintLogKeyDTO loResultGUID = null;
            FileStreamResult loRtn = null;
            try
            {
                //Get Parameter
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<PMT50610PrintLogKeyDTO>(R_DistributedCache.Cache.Get(pcGuid));

                //Get Data and Set Log Key
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                _AllReportParameter = loResultGUID.poParam;

                _logger.LogInfo("Read File Report || PrintReportGet(Controller)");
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            catch (Exception ex)
            {
                _logger.LogError(loException);
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || PrintReportGet(Controller)");
            return loRtn;
        }

        #region Helper
        private PMT50610PrintReportResultWithBaseHeaderPrintDTO GenerateDataPrint(PMT50610PrintReportParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GenerateDataPrint");
            _logger.LogInfo("Start || GenerateDataPrint(Controller)");
            R_Exception loException = new R_Exception();
            PMT50610PrintReportResultWithBaseHeaderPrintDTO loRtn = new PMT50610PrintReportResultWithBaseHeaderPrintDTO();
            List<PMT50610PrintReportSubDetailDTO> loSubDetail = new List<PMT50610PrintReportSubDetailDTO>();
            System.Globalization.CultureInfo loCultureInfo = new System.Globalization.CultureInfo(R_BackGlobalVar.REPORT_CULTURE);
            //System.Globalization.CultureInfo loCultureInfo = new System.Globalization.CultureInfo("id");
            try
            {
                PMT50610Cls loCls = new PMT50610Cls(_logger);
                loRtn.ReportData = new PMT50610PrintReportResultDTO();

                //Add Resources
                //loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Page", loCultureInfo);
                //loRtn.BaseHeaderColumn.Of = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
                //loRtn.BaseHeaderColumn.Print_Date = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
                //loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_By", loCultureInfo);

                PMT50610PrintReportColumnDTO loColumnObject = new PMT50610PrintReportColumnDTO();
                var loColumn = AssignValuesWithMessages(typeof(PMT50600BackResources.Resources_Dummy_Class), loCultureInfo, loColumnObject);


                List<PMT50610PrintReportDTO> loCollection = loCls.GetPrintReportList(poParam);

                _logger.LogInfo("Group Data || GenerateDataPrint(Controller)");
                PMT50610PrintReportHeaderDTO loTempData = loCollection
                .GroupBy(item => new {
                    item.CREF_NO,
                    item.CREF_DATE,
                    item.CDOC_NO,
                    item.CDOC_DATE,
                    item.CTENANT_NAME,
                    item.CCUSTOMER_TYPE_NAME,
                    item.CADDRESS,
                    item.CCURRENCY_CODE,
                    item.CPAY_TERM_NAME,
                    item.CPHONE1,
                    //item.CTENANT_FAX1,
                    item.CEMAIL,
                    item.CJRN_ID,
                    item.CTOTAL_AMOUNT_IN_WORDS,
                    item.CTRANS_DESC,
                    item.NTAXABLE_AMOUNT,
                    item.NTAX,
                    item.NOTHER_TAX,
                    item.NADDITION,
                    item.NDEDUCTION,
                    item.NTRANS_AMOUNT
                })
                .Select(header => new PMT50610PrintReportHeaderDTO
                {
                    CREF_NO = header.Key.CREF_NO,
                    CREF_DATE = header.Key.CREF_DATE,
                    DREF_DATE = DateTime.ParseExact(header.Key.CREF_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                    CDOC_NO = header.Key.CDOC_NO,
                    CDOC_DATE = header.Key.CDOC_DATE,
                    DDOC_DATE = DateTime.ParseExact(header.Key.CDOC_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                    CTENANT_NAME = header.Key.CTENANT_NAME,
                    CCUSTOMER_TYPE_NAME = header.Key.CCUSTOMER_TYPE_NAME,
                    CTENANT_ADDRESS = header.Key.CADDRESS,
                    CCURRENCY_CODE = header.Key.CCURRENCY_CODE,
                    CPAY_TERM_NAME = header.Key.CPAY_TERM_NAME,
                    CTENANT_PHONE1 = header.Key.CPHONE1,
                    //CTENANT_FAX1 = header.Key.CTENANT_FAX1,
                    CTENANT_EMAIL1 = header.Key.CEMAIL,
                    CJRN_ID = header.Key.CJRN_ID,
                    FooterData = new PMT50610PrintReportFooterDTO()
                    {
                        CTOTAL_AMOUNT_IN_WORDS = header.Key.CTOTAL_AMOUNT_IN_WORDS,
                        CTRANS_DESC = header.Key.CTRANS_DESC,
                        NTAXABLE_AMOUNT = header.Key.NTAXABLE_AMOUNT,
                        NTAX = header.Key.NTAX,
                        NOTHER_TAX = header.Key.NOTHER_TAX,
                        NADDITION = header.Key.NADDITION,
                        NDEDUCTION = header.Key.NDEDUCTION,
                        NTRANS_AMOUNT = header.Key.NTRANS_AMOUNT
                    },
                    DetailData = header
                        .Select(detail => new PMT50610PrintReportDetailDTO
                        {
                            INO = detail.INO,
                            CPRODUCT_ID = detail.CPRODUCT_ID,
                            CPRODUCT_NAME = detail.CPRODUCT_NAME,
                            CDETAIL_DESC = detail.CDETAIL_DESC,
                            NTRANS_QTY = detail.NTRANS_QTY,
                            CUNIT = detail.CUNIT,
                            NUNIT_PRICE = detail.NUNIT_PRICE,
                            NLINE_AMOUNT = detail.NLINE_AMOUNT,
                            NTOTAL_DISCOUNT = detail.NTOTAL_DISCOUNT,
                            //NDIST_ADD_ON = detail.NDIST_ADD_ON,
                            NLINE_TAXABLE_AMOUNT = detail.NLINE_TAXABLE_AMOUNT
                        })
                        .ToList()
                })
                .FirstOrDefault();

                if (loTempData != null)
                {
                    if (!string.IsNullOrWhiteSpace(loTempData.CJRN_ID))
                    {
                        _logger.LogInfo("Run  || GenerateDataPrint(Controller)");
                        PMT50610PrintReportSubDetailParameterDTO loSubParameter = new PMT50610PrintReportSubDetailParameterDTO()
                        {
                            CJRN_ID = loTempData.CJRN_ID,
                            CLANGUAGE_ID = poParam.CLANGUAGE_ID
                        };
                        loSubDetail = loCls.GetPrintSubDetailReportList(loSubParameter);
                    }
                }

                // Set Base Header Data
                var loBaseHeader = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY);
                _logger.LogInfo("Set Base Header || GenerateDataPrint(Controller)");
                BaseHeaderDTO loParam = new BaseHeaderDTO()
                {
                    CCOMPANY_NAME = "PT Realta Chackradarma",
                    CPRINT_CODE = poParam.CLOGIN_COMPANY_ID,
                    CPRINT_NAME = "SALES CREDIT NOTE",
                    CUSER_ID = loBaseHeader.USER_ID,
                };
                PMT50610PrintReportDTO loGetLogo = loCls.GetBaseHeaderLogoCompany(poParam);
                loParam.BLOGO_COMPANY = loGetLogo.OLOGO;

                _logger.LogInfo("Set Parameter || GenerateDataPrint(Controller)");
                PMT50610PrintReportResultDTO loData = new PMT50610PrintReportResultDTO()
                {
                    Column = (PMT50610PrintReportColumnDTO) loColumn,
                    Data = loTempData,
                    SubDetail = loSubDetail
                };

                loRtn.BaseHeaderData = loParam;
                loRtn.ReportData = loData;
            }
            catch (Exception ex)
            {
                _logger.LogError(loException);
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GenerateDataPrint(Controller)");
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