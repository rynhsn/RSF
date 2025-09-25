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
using APT00100COMMON.DTOs.APT00110Print;
using APT00100SERVICE.DTOs;
using APT00100COMMON.Loggers;
using APT00100BACK;
using BaseHeaderReportCOMMON;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using APT00100BACK.OpenTelemetry;

namespace APT00100SERVICE
{
    public class APT00110PrintController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private APT00110PrintReportParameterDTO _AllReportParameter;
        private LoggerAPT00110Print _logger;
        private readonly ActivitySource _activitySource;

        #region instantiate
        public APT00110PrintController(ILogger<APT00110PrintController> logger)
        {
            LoggerAPT00110Print.R_InitializeLogger(logger);
            _logger = LoggerAPT00110Print.R_GetInstanceLogger();
            _activitySource = APT00110PrintActivitySourceBase.R_InitializeAndGetActivitySource(nameof(APT00110PrintController));
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
            pcFileTemplate = System.IO.Path.Combine("Reports", "APT00110PrintReport.frx");
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
        public R_DownloadFileResultDTO PrintReportPost(APT00110PrintReportParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("PrintReportPost");
            _logger.LogInfo("Start || PrintReportPost(Controller)");
            R_Exception loException = new R_Exception();
            R_DownloadFileResultDTO loRtn = null;
            APT00110PrintLogKeyDTO loCache = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();

                loCache = new APT00110PrintLogKeyDTO
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY)
                };

                _logger.LogInfo("Set GUID Param || PrintReportPost(Controller)");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<APT00110PrintLogKeyDTO>(loCache));
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
            APT00110PrintLogKeyDTO loResultGUID = null;
            FileStreamResult loRtn = null;
            try
            {
                //Get Parameter
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<APT00110PrintLogKeyDTO>(R_DistributedCache.Cache.Get(pcGuid));

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
        private APT00110PrintReportResultWithBaseHeaderPrintDTO GenerateDataPrint(APT00110PrintReportParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GenerateDataPrint");
            _logger.LogInfo("Start || GenerateDataPrint(Controller)");
            R_Exception loException = new R_Exception();
            APT00110PrintReportResultWithBaseHeaderPrintDTO loRtn = new APT00110PrintReportResultWithBaseHeaderPrintDTO();
            List<APT00110PrintReportSubDetailDTO> loSubDetail = new List<APT00110PrintReportSubDetailDTO>();
            try
            {
                APT00110Cls loCls = new APT00110Cls(_logger);

                List<APT00110PrintReportDTO> loCollection = loCls.GetPrintReportList(poParam);

                _logger.LogInfo("Group Data || GenerateDataPrint(Controller)");
                APT00110PrintReportHeaderDTO loTempData = loCollection
                .GroupBy(item => new {
                    item.CREF_NO,
                    item.CREF_DATE,
                    item.CDOC_NO,
                    item.CDOC_DATE,
                    item.CSUPPLIER_NAME,
                    item.CSUPPLIER_ADDRESS,
                    item.CCURRENCY_CODE,
                    item.CPAY_TERM_NAME,
                    item.CSUPPLIER_PHONE1,
                    item.CSUPPLIER_FAX1,
                    item.CSUPPLIER_EMAIL1,
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
                .Select(header => new APT00110PrintReportHeaderDTO
                {
                    CREF_NO = header.Key.CREF_NO,
                    CREF_DATE = header.Key.CREF_DATE,
                    DREF_DATE = DateTime.ParseExact(header.Key.CREF_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                    CDOC_NO = header.Key.CDOC_NO,
                    CDOC_DATE = header.Key.CDOC_DATE,
                    DDOC_DATE = DateTime.ParseExact(header.Key.CDOC_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                    CSUPPLIER_NAME = header.Key.CSUPPLIER_NAME,
                    CSUPPLIER_ADDRESS = header.Key.CSUPPLIER_ADDRESS,
                    CCURRENCY_CODE = header.Key.CCURRENCY_CODE,
                    CPAY_TERM_NAME = header.Key.CPAY_TERM_NAME,
                    CSUPPLIER_PHONE1 = header.Key.CSUPPLIER_PHONE1,
                    CSUPPLIER_FAX1 = header.Key.CSUPPLIER_FAX1,
                    CSUPPLIER_EMAIL1 = header.Key.CSUPPLIER_EMAIL1,
                    CJRN_ID = header.Key.CJRN_ID,
                    FooterData = new APT00110PrintReportFooterDTO()
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
                        .Select(detail => new APT00110PrintReportDetailDTO
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
                            NDIST_ADD_ON = detail.NDIST_ADD_ON,
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
                        APT00110PrintReportSubDetailParameterDTO loSubParameter = new APT00110PrintReportSubDetailParameterDTO()
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
                    CPRINT_NAME = "PURCHASE INVOICE",
                    CUSER_ID = loBaseHeader.USER_ID,
                };
                APT00110PrintReportDTO loGetLogo = loCls.GetBaseHeaderLogoCompany(poParam);
                loParam.BLOGO_COMPANY = loGetLogo.OLOGO;

                _logger.LogInfo("Set Parameter || GenerateDataPrint(Controller)");
                APT00110PrintReportResultDTO loData = new APT00110PrintReportResultDTO()
                {
                    Column = new APT00110PrintReportColumnDTO(),
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
        #endregion
    }
}