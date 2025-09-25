using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using PMM01000BACK;
using PMM01000COMMON;
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
using System.Reflection;
using System.Globalization;

namespace PMM01000SERVICE
{
    public class PMM01000PrintController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private PMM01000PrintParamDTO _AllUtilityParameter;
        private LoggerPMM01000Print _LoggerPrint;
        private readonly ActivitySource _activitySource;

        #region instantiate
        public PMM01000PrintController(ILogger<LoggerPMM01000Print> logger)
        {
            //Initial and Get Logger
            LoggerPMM01000Print.R_InitializeLogger(logger);
            _LoggerPrint = LoggerPMM01000Print.R_GetInstanceLogger();
            _activitySource = PMM01000PrintActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMM01000PrintController));

            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }
        #endregion

        #region Event Handler
        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "LMM01000UtilityCharges.frx");
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            poData.Add(GenerateDataPrint(_AllUtilityParameter));
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
        public R_DownloadFileResultDTO AllUtilityChargesPost(PMM01000PrintParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("AllUtilityChargesPost");
            R_Exception loException = new R_Exception();
            _LoggerPrint.LogInfo("Start AllUtilityChargesPost");
            PMM01000PrintLogKeyDTO<PMM01000PrintParamDTO> loCache = null;

            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new PMM01000PrintLogKeyDTO<PMM01000PrintParamDTO>
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY)
                };

                // Set Guid Param 
                _LoggerPrint.LogInfo("Set GUID Param AllUtilityChargesPost");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<PMM01000PrintLogKeyDTO<PMM01000PrintParamDTO>>(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _LoggerPrint.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _LoggerPrint.LogInfo("End AllUtilityChargesPost");

            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult AllStreamUtilityChargesGet(string pcGuid)
        {
            using Activity activity = _activitySource.StartActivity("AllStreamUtilityChargesGet");
            R_Exception loException = new R_Exception();
            FileStreamResult loRtn = null;
            PMM01000PrintLogKeyDTO<PMM01000PrintParamDTO> loResultGUID = null;
            _LoggerPrint.LogInfo("Start AllStreamOtherChargesGet");

            try
            {
                //Get Parameter
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<PMM01000PrintLogKeyDTO<PMM01000PrintParamDTO>>(R_DistributedCache.Cache.Get(pcGuid));

                //Get Data and Set Log Key
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                _AllUtilityParameter = loResultGUID.poParam;

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
            _LoggerPrint.LogInfo("End AllStreamOtherChargesGet");

            return loRtn;
        }

        #region Helper
        private PMM01000ResultWithBaseHeaderPrintDTO GenerateDataPrint(PMM01000PrintParamDTO poParam)
        {
            System.Globalization.CultureInfo loCultureInfo = new System.Globalization.CultureInfo(poParam.CREPORT_CULTURE);
            using Activity activity = _activitySource.StartActivity("GenerateDataPrint");
            var loEx = new R_Exception();
            PMM01000ResultWithBaseHeaderPrintDTO loRtn = new PMM01000ResultWithBaseHeaderPrintDTO();

            try
            {
                //Add Resources
                loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Page", loCultureInfo);
                loRtn.BaseHeaderColumn.Of = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_Date = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_By", loCultureInfo);

                //Assign Column
                PMM01000ColumnPrintDTO loColumnObject = new PMM01000ColumnPrintDTO();
                var loColumn = AssignValuesWithMessages(typeof(PMM01000BackResources.Resources_Dummy_Class), loCultureInfo, loColumnObject);

                PMM01000ResultPrintDTO loData = new PMM01000ResultPrintDTO()
                {
                    Title = "Utility Charges",
                    Header = string.Format("{0} - {1}", poParam.CPROPERTY_ID, poParam.CPROPERTY_NAME),
                    Column = (PMM01000ColumnPrintDTO)loColumn
                };

                var loCls = new PMM01000Cls(_LoggerPrint);

                _LoggerPrint.LogInfo("Call Method GetListDataPrint Report");
                var loCollection = loCls.GetListDataPrint(poParam);

                
                _LoggerPrint.LogInfo("Grouping Report");
                var loTempData = loCollection
                .GroupBy(item => new { item.CCHARGES_TYPE_DESCR, item.CCHARGES_TYPE })
                .Select(group => new PMM01000TopPrintDTO
                {
                    CCHARGES_TYPE_DESCR = group.Key.CCHARGES_TYPE_DESCR,
                    DataCharges = group
                        .GroupBy(headerGroup => new
                        {
                            headerGroup.CCHARGES_TYPE,
                            headerGroup.CCHARGES_ID,
                            headerGroup.CCHARGES_NAME,
                            headerGroup.LACTIVE,
                            headerGroup.LACCRUAL,
                            headerGroup.CUTILITY_JRNGRP_CODE,
                            headerGroup.CUTILITY_JRNGRP_NAME,
                            headerGroup.LTAX_EXEMPTION,
                            headerGroup.LOTHER_TAX,
                            headerGroup.CTAX_EXEMPTION_CODE,
                            headerGroup.NTAX_EXEMPTION_PCT,
                            headerGroup.COTHER_TAX_ID,
                            headerGroup.LWITHHOLDING_TAX,
                            headerGroup.CWITHHOLDING_TAX_TYPE,
                            headerGroup.CWITHHOLDING_TAX_ID
                        })
                        .Select(headerGroup => new PMM01000HeaderPrintDTO
                        {
                            CCHARGES_TYPE = headerGroup.Key.CCHARGES_TYPE,
                            CCHARGES_ID = headerGroup.Key.CCHARGES_ID,
                            CCHARGES_NAME = headerGroup.Key.CCHARGES_NAME,
                            LACTIVE = headerGroup.Key.LACTIVE,
                            LACCRUAL = headerGroup.Key.LACCRUAL,
                            CUTILITY_JRNGRP_CODE = headerGroup.Key.CUTILITY_JRNGRP_CODE,
                            CUTILITY_JRNGRP_NAME = headerGroup.Key.CUTILITY_JRNGRP_NAME,
                            LTAX_EXEMPTION = headerGroup.Key.LTAX_EXEMPTION,
                            LOTHER_TAX = headerGroup.Key.LOTHER_TAX,
                            NTAX_EXEMPTION_PCT = headerGroup.Key.NTAX_EXEMPTION_PCT,
                            LWITHHOLDING_TAX = headerGroup.Key.LWITHHOLDING_TAX,
                            CTAX_EXEMPTION_CODE = headerGroup.Key.CTAX_EXEMPTION_CODE,
                            COTHER_TAX_ID = headerGroup.Key.COTHER_TAX_ID,
                            CWITHHOLDING_TAX_TYPE = headerGroup.Key.CWITHHOLDING_TAX_TYPE,
                            CWITHHOLDING_TAX_ID = headerGroup.Key.CWITHHOLDING_TAX_ID,
                            DetailCharges = headerGroup
                                .Select(detail => new PMM01000DetailPrintDTO
                                {
                                    CGOA_CODE = detail.CGOA_CODE,
                                    CGOA_NAME = detail.CGOA_NAME,
                                    LDEPARTMENT_MODE = detail.LDEPARTMENT_MODE,
                                    CGLACCOUNT_NO = detail.CGLACCOUNT_NO,
                                    CGLACCOUNT_NAME = detail.CGLACCOUNT_NAME
                                })
                                .ToList()
                        })
                        .ToList()
                })
                .ToList();

                loData.Data = loTempData;

                _LoggerPrint.LogInfo("Set BaseHeader Report");
                var loParam = new BaseHeaderDTO()
                {
                    CCOMPANY_NAME = "PT Realta Chackradarma",
                    CPRINT_CODE = poParam.CCOMPANY_ID.ToUpper(),
                    CPRINT_NAME = "Utility Charges",
                    CUSER_ID = poParam.CUSER_ID.ToUpper(),
                };
                var loBaseHeader = loCls.GetBaseHeaderLogoCompany(poParam);
                loParam.BLOGO_COMPANY = loBaseHeader.CLOGO;

                _LoggerPrint.LogInfo("Set Data Report");
                loRtn.BaseHeaderData = loParam;
                loRtn.UtilitiesData = loData;
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