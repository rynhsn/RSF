using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using CBR00600BACK;
using CBR00600COMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Xml.Linq;

namespace CBR00600SERVICE
{
    public class CBR00600PrintAllocationPaymentToSupplierController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private CBR00600DTO _AllCBR00600Parameter;
        private LoggerCBR00600 _LoggerPrint;
        private readonly ActivitySource _activitySource;

        #region instantiate
        public CBR00600PrintAllocationPaymentToSupplierController(ILogger<CBR00600DTO> logger)
        {
            //Initial and Get Logger
            LoggerCBR00600.R_InitializeLogger(logger);
            _LoggerPrint = LoggerCBR00600.R_GetInstanceLogger();
            _activitySource = CBR00600ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(CBR00600PrintAllocationPaymentToSupplierController));

            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }
        #endregion

        #region Event Handler
        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "CBR00600AllocationPaymentToSupplier.frx");
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            poData.Add(GenerateDataPrint(_AllCBR00600Parameter));
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
        public R_DownloadFileResultDTO AllPrintAllocationPaymentToSupplierCBTransactionPost(CBR00600DTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("AllPrintAllocationPaymentToSupplierCBTransactionPost");
            _LoggerPrint.LogInfo("Start AllPrintAllocationPaymentToSupplierCBTransactionPost");
            R_Exception loException = new R_Exception();
            CBR00600PrintLogKeyDTO<CBR00600DTO> loCache = null;
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new CBR00600PrintLogKeyDTO<CBR00600DTO>
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY),
                    poReportGlobal = R_ReportGlobalVar.R_GetReportDTO()
                };

                // Set Guid Param 
                _LoggerPrint.LogInfo("Set GUID Param AllPrintAllocationPaymentToSupplierCBTransactionPost");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<CBR00600PrintLogKeyDTO<CBR00600DTO>>(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _LoggerPrint.LogError(loException);
            }
            _LoggerPrint.LogInfo("End AllPrintAllocationPaymentToSupplierCBTransactionPost");
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult AllStreamPrintAllocationPaymentToSupplierCBTransactionsGet(string pcGuid)
        {
            using Activity activity = _activitySource.StartActivity("AllStreamPrintAllocationPaymentToSupplierCBTransactionsGet");
            R_Exception loException = new R_Exception();
            CBR00600PrintLogKeyDTO<CBR00600DTO> loResultGUID = null;
            _LoggerPrint.LogInfo("Start AllStreamPrintAllocationPaymentToSupplierCBTransactionsGet");
            FileStreamResult loRtn = null;
            try
            {
                //Get Parameter
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<CBR00600PrintLogKeyDTO<CBR00600DTO>>(R_DistributedCache.Cache.Get(pcGuid));

                //Get Data and Set Log Key
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                R_ReportGlobalVar.R_SetFromReportDTO(loResultGUID.poReportGlobal);
                _AllCBR00600Parameter = loResultGUID.poParam;

                R_FileType loFileType = new();
                if (loResultGUID.poParam.LIS_PRINT)
                {
                    loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF), R_ReportUtility.GetMimeType(R_FileType.PDF));
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
                    loRtn = File(_ReportCls.R_GetStreamReport(peExport: loFileType), R_ReportUtility.GetMimeType(loFileType), $"{loResultGUID.poParam.CREPORT_FILENAME}.{loResultGUID.poParam.CREPORT_FILETYPE}");
                }
                _LoggerPrint.LogInfo("Report generated successfully.");
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _LoggerPrint.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _LoggerPrint.LogInfo("End AllStreamPrintAllocationPaymentToSupplierCBTransactionsGet");

            return loRtn;
        }

        #region Helper
        private CBR00600ResultPrintDTO GenerateDataPrint(CBR00600DTO poParam)
        {
            System.Globalization.CultureInfo loCultureInfo = new System.Globalization.CultureInfo(poParam.CREPORT_CULTURE);
            using Activity activity = _activitySource.StartActivity("GenerateDataPrint");
            var loEx = new R_Exception();
            CBR00600ResultPrintDTO loRtn = new CBR00600ResultPrintDTO();

            try
            {
                //Set Param
                loRtn.Parameter = R_Utility.R_ConvertObjectToObject<CBR00600DTO, CBR00600ParameterReportDTO>(poParam);
                loRtn.Parameter.LIS_PAYMENT_TO_SUPPLIER = poParam.CTRANS_CODE == "191000";
                loRtn.Parameter.LIS_RECEIPT_FROM_CUSTOMER = poParam.CTRANS_CODE == "991000";
                loRtn.Parameter.LIS_CBT_INTERNAL = poParam.CTRANS_CODE == "190040";
                loRtn.Parameter.LIS_PAYMENT_JOURNAL = poParam.CTRANS_CODE != "191000" && poParam.CTRANS_CODE != "991000" && poParam.CTRANS_CODE != "190040";


                _LoggerPrint.LogInfo("Add Resources Column");
                //Add Resources
                var loBaseHeaderColumn = AssignValuesWithMessages(typeof(BaseHeaderResources.Resources_Dummy_Class), loCultureInfo, loRtn.BaseHeaderColumn);
                loRtn.BaseHeaderColumn = (BaseHeaderColumnDTO)loBaseHeaderColumn;

                //Assign Allocation Column
                loRtn.Column = new CBR00600ColoumnDTO();
                CBR00600AllocationColumnDTO loColumnAllocationObject = new CBR00600AllocationColumnDTO();
                var loColumnAllocation = AssignValuesWithMessages(typeof(CBR00600BackResources.Resources_Dummy_Class), loCultureInfo, loColumnAllocationObject);
                loRtn.Column.AllocationColoumn = (CBR00600AllocationColumnDTO)loColumnAllocation;

                var loCls = new CBR00600Cls();
                loRtn.Data = new CBR00600PrintDataDTO();

                // Set Base Header Data
                _LoggerPrint.LogInfo("Set BaseHeader Report");
                var loParam = new BaseHeaderDTO()
                {
                    CPRINT_CODE = R_BackGlobalVar.COMPANY_ID.ToUpper(),
                    CUSER_ID = R_BackGlobalVar.USER_ID.ToUpper(),
                };
                var loBaseHeader = loCls.GetBaseHeaderLogoCompany();
                loParam.BLOGO_COMPANY = loBaseHeader.CLOGO;
                loParam.CCOMPANY_NAME = loBaseHeader.CCOMPANY_NAME;
                loParam.DPRINT_DATE_COMPANY = DateTime.ParseExact(loBaseHeader.CDATETIME_NOW, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture);
                loParam.CPRINT_DATE_COMPANY = loParam.DPRINT_DATE_COMPANY.ToString(R_BackGlobalVar.REPORT_FORMAT_SHORT_DATE + " " + R_BackGlobalVar.REPORT_FORMAT_SHORT_TIME);
                loRtn.BaseHeaderData = loParam;

                _LoggerPrint.LogInfo("Get Data Company Info");
                var loCompanyInfo = loCls.GetCompanyInfo();
                if (poParam.LALLOCATION)
                {
                    _LoggerPrint.LogInfo("Call Method Allocation Report");
                    poParam.CREPORT_TYPE = "ALLOCATION";
                    var loAllocationData = loCls.GetDataReport(poParam);
                    if (loAllocationData.Count > 0)
                    {
                        var loGroupingAllocation = loAllocationData
                        .GroupBy(x => x.CCB_REF_NO) // Group by header key
                        .Select(g => new CBR00600AllocationHeaderDTO
                        {
                            // HEADER FIELDS
                            CREPORT_TITLE = g.First().CREPORT_TITLE,
                            CCB_REF_NO = g.Key,
                            CCB_REF_DATE = g.First().CCB_REF_DATE,
                            NCB_AMOUNT = g.First().NCB_AMOUNT,
                            CCUST_SUPP_ID = g.First().CCUST_SUPP_ID,
                            CCUST_SUPP_ID_NAME = g.First().CCUST_SUPP_ID_NAME,
                            CCB_CODE = g.First().CCB_CODE,
                            CCB_NAME = g.First().CCB_NAME,
                            CCB_ACCOUNT_NO = g.First().CCB_ACCOUNT_NO,
                            CCB_DEPT_CODE = g.First().CCB_DEPT_CODE,
                            CCB_DEPT_NAME = g.First().CCB_DEPT_NAME,
                            CCB_DOC_NO = g.First().CCB_DOC_NO,
                            CCB_DOC_DATE = g.First().CCB_DOC_DATE,
                            CCB_CHEQUE_NO = g.First().CCB_CHEQUE_NO,
                            CCB_CHEQUE_DATE = g.First().CCB_CHEQUE_DATE,
                            CCB_CURRENCY_CODE = g.First().CCB_CURRENCY_CODE,
                            CCB_AMOUNT_WORDS = g.First().CCB_AMOUNT_WORDS,
                            NCBLBASE_RATE = g.First().NCBLBASE_RATE,
                            NCBLCURRENCY_RATE = g.First().NCBLCURRENCY_RATE,
                            NCBBBASE_RATE = g.First().NCBBBASE_RATE,
                            NCBBCURRENCY_RATE = g.First().NCBBCURRENCY_RATE,
                            CCB_PAYMENT_TYPE = g.First().CCB_PAYMENT_TYPE,
                            CCB_TRANS_DESC = g.First().CCB_TRANS_DESC,
                            DCB_REF_DATE = DateTime.TryParseExact(g.First().CCB_REF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldCbRefDate) ? (DateTime?)ldCbRefDate : null,
                            DCB_CHEQUE_DATE = DateTime.TryParseExact(g.First().CCB_CHEQUE_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldCbChequeDate) ? (DateTime?)ldCbChequeDate : null,
                            DCB_DOC_DATE = DateTime.TryParseExact(g.First().CCB_DOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldCbDocDate) ? (DateTime?)ldCbDocDate : null,
                            CCOMPANY_BASE_CURRENCY_CODE = loCompanyInfo.CBASE_CURRENCY_CODE,
                            CCOMPANY_LOCAL_CURRENCY_CODE = loCompanyInfo.CLOCAL_CURRENCY_CODE,
                            
                            // DETAILS
                            DetailAllocation = g.Select(d => new CBR00600AllocationDetailDTO
                            {
                                CCB_REF_NO = d.CCB_REF_NO,
                                INO = d.INO,
                                CALLOC_NO = d.CALLOC_NO,
                                CALLOC_DATE = d.CALLOC_DATE,
                                CCURRENCY_CODE = d.CCURRENCY_CODE,
                                CINVOICE_NO = d.CINVOICE_NO,
                                CINVOICE_DESC = d.CINVOICE_DESC,
                                NTRANS_AMOUNT = d.NTRANS_AMOUNT,
                                DALLOC_DATE = DateTime.TryParseExact(d.CALLOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldAllocDate) ? (DateTime?)ldAllocDate : null,
                                DINVOICE_DATE = DateTime.TryParseExact(d.CINVOICE_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldInvoiceDate) ? (DateTime?)ldInvoiceDate : null,
                                CGLACCOUNT_NO_NAME = d.CGLACCOUNT_NO_NAME,
                                CINVOICE_DATE = d.CINVOICE_DATE,
                                NCREDIT_AMOUNT = d.NCREDIT_AMOUNT,
                                NDEBIT_AMOUNT = d.NDEBIT_AMOUNT,
                                CREF_NO = d.CREF_NO,
                            }).ToList()
                        })
                        .ToList();
                        loRtn.Data.Allocation = loGroupingAllocation;
                    }
                }
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