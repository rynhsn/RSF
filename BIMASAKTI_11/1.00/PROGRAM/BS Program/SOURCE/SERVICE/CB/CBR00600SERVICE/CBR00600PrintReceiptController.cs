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
    public class CBR00600PrintReceiptController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private CBR00600DTO _AllCBR00600Parameter;
        private LoggerCBR00600 _LoggerPrint;
        private readonly ActivitySource _activitySource;

        #region instantiate
        public CBR00600PrintReceiptController(ILogger<CBR00600DTO> logger)
        {
            //Initial and Get Logger
            LoggerCBR00600.R_InitializeLogger(logger);
            _LoggerPrint = LoggerCBR00600.R_GetInstanceLogger();
            _activitySource = CBR00600ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(CBR00600PrintReceiptController));

            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }
        #endregion

        #region Event Handler
        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "CBR00600Receipt.frx");
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
        public R_DownloadFileResultDTO AllPrintReceiptCBTransactionPost(CBR00600DTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("AllPrintReceiptCBTransactionPost");
            _LoggerPrint.LogInfo("Start AllPrintReceiptCBTransactionPost");
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
                _LoggerPrint.LogInfo("Set GUID Param AllPrintReceiptCBTransactionPost");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<CBR00600PrintLogKeyDTO<CBR00600DTO>>(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _LoggerPrint.LogError(loException);
            }
            _LoggerPrint.LogInfo("End AllPrintReceiptCBTransactionPost");
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult AllStreamPrintReceiptCBTransactionsGet(string pcGuid)
        {
            using Activity activity = _activitySource.StartActivity("AllStreamPrintReceiptCBTransactionsGet");
            R_Exception loException = new R_Exception();
            CBR00600PrintLogKeyDTO<CBR00600DTO> loResultGUID = null;
            _LoggerPrint.LogInfo("Start AllStreamPrintReceiptCBTransactionsGet");
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
            _LoggerPrint.LogInfo("End AllStreamPrintReceiptCBTransactionsGet");

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
                //Assign Receipt Column
                loRtn.Column = new CBR00600ColoumnDTO();
                CBR00600OfficialReceiptColumnDTO loColumnReceiptObject = new CBR00600OfficialReceiptColumnDTO();
                var loColumnReceipt = AssignValuesWithMessages(typeof(CBR00600BackResources.Resources_Dummy_Class), loCultureInfo, loColumnReceiptObject);
                loRtn.Column.OfficialReceiptColoumn = (CBR00600OfficialReceiptColumnDTO)loColumnReceipt;

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
                loRtn.BaseHeaderData = loParam;

                _LoggerPrint.LogInfo("Get Data Company Info");
                var loCompanyInfo = loCls.GetCompanyInfo();

                if (poParam.LRECEIPT)
                {
                    _LoggerPrint.LogInfo("Call Method Official Report");
                    poParam.CREPORT_TYPE = "RECEIPT";
                    var loOfficialReceiptData = loCls.GetDataReport(poParam);
                    if (loOfficialReceiptData.Count > 0)
                    {
                        var loConvertData = R_Utility.R_ConvertCollectionToCollection<CBR00600SPResultDTO, CBR00600OfficialReceiptHeaderDTO>(loOfficialReceiptData).ToList();
                        loConvertData.ForEach(x =>
                        {
                            x.DREF_DATE = DateTime.TryParseExact(x.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldRefDate) ? (DateTime?)ldRefDate : null;
                            x.CREPORT_TITLE = string.IsNullOrWhiteSpace(x.CREPORT_TITLE) ? R_Utility.R_GetMessage(typeof(CBR00600BackResources.Resources_Dummy_Class), "OFFICIAL_RECEIPT", loCultureInfo) : x.CREPORT_TITLE;
                        });
                        loRtn.Data.OfficialReceipt = loConvertData;
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