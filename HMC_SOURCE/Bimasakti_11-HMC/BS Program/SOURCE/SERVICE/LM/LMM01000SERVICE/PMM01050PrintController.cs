using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using PMM01000BACK;
using PMM01000COMMON;
using PMM01000COMMON.Print;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Globalization;

namespace PMM01000SERVICE
{
    public class PMM01050PrintController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private PMM01050PrintParamDTO _AllRateOTParameter;
        private LoggerPMM01050Print _LoggerPrint;
        private readonly ActivitySource _activitySource;

        #region instantiate
        public PMM01050PrintController(ILogger<LoggerPMM01050Print> logger)
        {
            //Initial and Get Logger
            LoggerPMM01050Print.R_InitializeLogger(logger);
            _LoggerPrint = LoggerPMM01050Print.R_GetInstanceLogger();
            _activitySource = PMM01050PrintActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMM01020PrintController));

            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }
        #endregion

        #region Event Handler
        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "LMM01050RateOT.frx");
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            poData.Add(GenerateDataPrint(_AllRateOTParameter));
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
        public R_DownloadFileResultDTO AllRateOTReportPost(PMM01050PrintParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("AllRateOTReportPost");
            R_Exception loException = new R_Exception();
            _LoggerPrint.LogInfo("Start AllRateOTReportPost");
            PMM01000PrintLogKeyDTO<PMM01050PrintParamDTO> loCache = null;
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new PMM01000PrintLogKeyDTO<PMM01050PrintParamDTO>
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY)
                };

                // Set Guid Param 
                _LoggerPrint.LogInfo("Set GUID Param AllRateOTReportPost");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<PMM01000PrintLogKeyDTO<PMM01050PrintParamDTO>>(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _LoggerPrint.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _LoggerPrint.LogInfo("End AllRateOTReportPost");

            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult AllStreamRateOTReportGet(string pcGuid)
        {
            using Activity activity = _activitySource.StartActivity("AllStreamRateOTReportGet");
            PMM01000PrintLogKeyDTO<PMM01050PrintParamDTO> loResultGUID = null;
            R_Exception loException = new R_Exception();
            FileStreamResult loRtn = null;
            _LoggerPrint.LogInfo("Start AllStreamRateOTReportGet");
            try
            {
                //Get Parameter
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<PMM01000PrintLogKeyDTO<PMM01050PrintParamDTO>>(R_DistributedCache.Cache.Get(pcGuid));

                //Get Data and Set Log Key
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                _AllRateOTParameter = loResultGUID.poParam;

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
            _LoggerPrint.LogInfo("End AllStreamRateOTReportGet");

            return loRtn;
        }

        #region Helper
        private PMM01050ResultWithBaseHeaderPrintDTO GenerateDataPrint(PMM01050PrintParamDTO poParam)
        {
            System.Globalization.CultureInfo loCultureInfo = new System.Globalization.CultureInfo(poParam.CREPORT_CULTURE);
            using Activity activity = _activitySource.StartActivity("GenerateDataPrint");
            var loEx = new R_Exception();
            PMM01050ResultWithBaseHeaderPrintDTO loRtn = new PMM01050ResultWithBaseHeaderPrintDTO();

            try
            {
                //Add Resources
                loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Page", loCultureInfo);
                loRtn.BaseHeaderColumn.Of = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_Date = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_By", loCultureInfo);

                //Assign Column
                PMM01050ColumnPrintDTO loColumnObject = new PMM01050ColumnPrintDTO();
                var loColumn = AssignValuesWithMessages(typeof(PMM01000BackResources.Resources_Dummy_Class), loCultureInfo, loColumnObject);
                loRtn.Column = (PMM01050ColumnPrintDTO)loColumn;

                PMM01050ResultPrintDTO loData = new PMM01050ResultPrintDTO()
                {
                    Title = "Overtime Rate",
                    Header = string.Format("{0} - {1}", poParam.CPROPERTY_ID, poParam.CPROPERTY_NAME),
                };

                var loCls = new PMM01050Cls(_LoggerPrint);

                var loHeaderParam = R_Utility.R_ConvertObjectToObject<PMM01050PrintParamDTO, PMM01050DTO>(poParam);
                var loDetailParam = R_Utility.R_ConvertObjectToObject<PMM01050PrintParamDTO, PMM01051DTO>(poParam);

                _LoggerPrint.LogInfo("Call Method GetHDReportRateOT And GetDetailReportRateOT Report");
                var loHeaderCollection = loCls.GetHDReportRateOT(loHeaderParam);
                if (DateTime.TryParseExact(loHeaderCollection.CCHARGES_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldChargesDate))
                {
                    loHeaderCollection.DCHARGES_DATE = ldChargesDate;
                }
                else
                {
                    loHeaderCollection.DCHARGES_DATE = null;

                }
                loDetailParam.CDAY_TYPE = "WD";
                var loDetailWDCollection = loCls.GetDetailReportRateOT(loDetailParam);

                loDetailParam.CDAY_TYPE = "WK";
                var loDetailWKCollection = loCls.GetDetailReportRateOT(loDetailParam);

                loData.HeaderData = loHeaderCollection;
                loData.DetailWDData = loDetailWDCollection;
                loData.DetailWKData = loDetailWKCollection;

                _LoggerPrint.LogInfo("Set BaseHeader Report");
                var loParam = new BaseHeaderDTO()
                {
                    CCOMPANY_NAME = "PT Realta Chackradarma",
                    CPRINT_CODE = poParam.CCOMPANY_ID.ToUpper(),
                    CPRINT_NAME = "Overtime Rate",
                    CUSER_ID = poParam.CUSER_ID.ToUpper(),
                };
                var loBaseHeader = loCls.GetBaseHeaderLogoCompany(poParam);
                loParam.BLOGO_COMPANY = loBaseHeader.CLOGO;

                _LoggerPrint.LogInfo("Set Data Report");
                loRtn.BaseHeaderData = loParam;
                loRtn.RateOT = loData;

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