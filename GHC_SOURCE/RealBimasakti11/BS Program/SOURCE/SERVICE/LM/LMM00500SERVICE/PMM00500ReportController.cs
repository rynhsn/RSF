using BaseHeaderReportCOMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMM00500Back;
using PMM00500BackResources;
using PMM00500Common.DTOs;
using PMM00500Common.Logs;
using PMM00500Common.Report;
using PMM00500Service.DTOs;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;
using System.Collections;
using System.Diagnostics;
using System.Globalization;

namespace PMM00500Service
{
    public class PMM00500ReportController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private PrintParamDTO _poParam;
        private LoggerPMM00500? _loggerPMM00500;
        private readonly ActivitySource _activitySource;

        #region Instantiate

        public PMM00500ReportController(ILogger<LoggerPMM00500> logger)
        {
            LoggerPMM00500.R_InitializeLogger(logger);
            _loggerPMM00500 = LoggerPMM00500.R_GetInstanceLogger();
            _activitySource = PMM00500Activity.R_InitializeAndGetActivitySource(nameof(PMM00500ReportController));

            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
        }

        #endregion Instantiate

        #region EventHandler
        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "PMM00500UnitCharges.frx");
        }
        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            R_Exception loException = new R_Exception();
            try
            {
                poData.Add(GenerateDataPrint(_poParam));
                pcDataSourceName = "ResponseDataModel";
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        #endregion EventHandler

        [HttpPost]
        public R_DownloadFileResultDTO UnitChargesReportPost(PrintParamDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            _loggerPMM00500.LogInfo("Start UnitChargesReportPost PMM00500");
            R_DownloadFileResultDTO loRtn = null;
            PMM00500PrintLogKeyDTO loCache = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new PMM00500PrintLogKeyDTO()
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY)
                };
                _loggerPMM00500.LogInfo("Set GUID Param UnitChargesReportPost");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte(loCache));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            if (loEx.Haserror)
            {
                _loggerPMM00500.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerPMM00500.LogInfo("End UnitChargesReportPost PMM00500");
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult UnitChargesReportGet(string pcGuid)
        {
            R_Exception loEx = new R_Exception();
            _loggerPMM00500.LogInfo("Start UnitChargesReportGet PMM00500");
            PMM00500PrintLogKeyDTO loResultGUID = null;
            FileStreamResult loRtn = null;
            try
            {
                //Get Parameter
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<PMM00500PrintLogKeyDTO>(R_DistributedCache.Cache.Get(pcGuid));

                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                _poParam = loResultGUID.poParam;

                _loggerPMM00500.LogInfo("Read File Report UnitChargesReportGet PMM00500");

                R_FileType loFileType = new();


                //print pdf biasa
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: loFileType), R_ReportUtility.GetMimeType(loFileType));

                //print nama save as
                //loRtn = File(_ReportCls.R_GetStreamReport(peExport: loFileType), R_ReportUtility.GetMimeType(loFileType),$"{loResultGUID.poParam.CPROPERTY_NAME}.{loResultGUID.poParam.CPRINT_EXT_TYPE}");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMM00500.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        #region Helper

        private PMM00500UnitChargesResultWithBaseHeaderDTO GenerateDataPrint(PrintParamDTO poParam)
        {
            var loEx = new R_Exception();
            PMM00500UnitChargesResultWithBaseHeaderDTO loRtn = new PMM00500UnitChargesResultWithBaseHeaderDTO();
            System.Globalization.CultureInfo loCultureInfo = new System.Globalization.CultureInfo(R_BackGlobalVar.REPORT_CULTURE);
            try
            {
                _loggerPMM00500.LogInfo("Set Base Header GenerateDataPrint");
                var loParam = new BaseHeaderDTO()
                {
                    CCOMPANY_NAME = "PT Realta Chackradarma",
                    CPRINT_CODE = "001",
                    CPRINT_NAME = "Unit Charges",
                    CUSER_ID = poParam.CUSER_LOGIN_ID,
                };

                #region ResourceBaseHeader
                //Add Resources
                loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Page", loCultureInfo);
                loRtn.BaseHeaderColumn.Of = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_Date = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_By", loCultureInfo);
                #endregion
                var loColumn = AssignValuesWithMessages(typeof(PMM00500BackResources.Resources_PMM00500), loCultureInfo, new UnitChargesColumnDTO());
                UnitChargesReportResultDTO loData = new UnitChargesReportResultDTO()
                {
                    title = "Unit Charges",
                    Header = new UnitChargesHeaderDTO()
                    {
                        CPROPERTY_ID = poParam.CPROPERTY_ID,
                        CPROPERTY_NAME = poParam.CPROPERTY_NAME
                    },
                    Column =  (UnitChargesColumnDTO)loColumn,
                    DataUnitCharges = new List<UnitChargersDetail1DTO>()
                };

                var loCls = new PMM00510Cls();
                _loggerPMM00500.LogInfo("Call Method GenerateDataPrint");
                var loCollection = loCls.GetPrintParam(poParam);

                #region GroupBy
                var loTempData = loCollection
                    .GroupBy(data1a => new
                    {
                        data1a.CCHARGES_TYPE_DESCR
                    })
                    .Select(data1b => new UnitChargersDetail1DTO()
                    {
                        CCHARGES_TYPE_DESCR = data1b.Key.CCHARGES_TYPE_DESCR,
                        UnitChargesDetail2 = data1b.GroupBy(data2a => new
                        {
                            data2a.CCHARGES_ID,
                            data2a.CCHARGES_NAME,
                            data2a.LACTIVE,
                            data2a.LACCRUAL,
                            data2a.CSERVICE_JRNGRP_CODE,
                            data2a.CSERVICE_JRNGRP_NAME,
                            data2a.CTAX_EXEMPTION_CODE,
                            data2a.NTAX_EXEMPTION_PCT,
                            data2a.COTHER_TAX_ID,
                            data2a.CWITHHOLDING_TAX_TYPE,
                            data2a.CWITHHOLDING_TAX_ID,
                        }).Select(data2b => new UnitChargesDetail2DTO()
                        {
                            CCHARGES_ID = data2b.Key.CCHARGES_ID,
                            CCHARGES_NAME = data2b.Key.CCHARGES_NAME,
                            LACTIVE = data2b.Key.LACTIVE,
                            LACCRUAL = data2b.Key.LACCRUAL,
                            CSERVICE_JRNGRP_CODE = data2b.Key.CSERVICE_JRNGRP_CODE,
                            CSERVICE_JRNGRP_NAME = data2b.Key.CSERVICE_JRNGRP_NAME,
                            CTAX_EXEMPTION_CODE = data2b.Key.CTAX_EXEMPTION_CODE,
                            NTAX_EXEMPTION_PCT = data2b.Key.NTAX_EXEMPTION_PCT,
                            COTHER_TAX_ID = data2b.Key.COTHER_TAX_ID,
                            CWITHHOLDING_TAX_TYPE = data2b.Key.CWITHHOLDING_TAX_TYPE,
                            CWITHHOLDING_TAX_ID = data2b.Key.CWITHHOLDING_TAX_ID,
                            UnitChargesDetail3 = data2b.GroupBy(data3a => new
                            {
                                data3a.CGOA_CODE,
                                data3a.CGOA_NAME,
                                data3a.LDEPARTMENT_MODE,
                                data3a.CGLACCOUNT_NO,
                                data3a.CGLACCOUNT_NAME,
                            }).Select(data3b => new UnitChargesDetail3DTO()
                            {
                                CGOA_CODE = data3b.Key.CGOA_CODE,
                                CGOA_NAME = data3b.Key.CGOA_NAME,
                                LDEPARTMENT_MODE = data3b.Key.LDEPARTMENT_MODE,
                                CGLACCOUNT_NO = data3b.Key.CGLACCOUNT_NO,
                                CGLACCOUNT_NAME = data3b.Key.CGLACCOUNT_NAME,
                            }).ToList()
                        }).ToList()
                    }).ToList();
                #endregion

                _loggerPMM00500.LogInfo("Set Data Report");
                loData.DataUnitCharges = loTempData;
                loRtn.PMM00500PrintData = loData;
                loRtn.BaseHeaderData = loParam;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
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
        #endregion Helper
    }
}