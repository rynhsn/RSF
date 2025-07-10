using BaseHeaderReportCOMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT02100BACK;
using PMT02100BACK.OpenTelemetry;
using PMT02100COMMON.DTOs.PMT02120Print;
using PMT02100COMMON.Loggers;
using PMT02100SERVICE.DTOs;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT02100SERVICE
{
    public class PMT02120PrintController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private PMT02120PrintReportParameterDTO _AllReportParameter;
        private LoggerPMT02120Print _logger;
        private readonly ActivitySource _activitySource;

        #region instantiate
        public PMT02120PrintController(ILogger<PMT02120PrintController> logger)
        {
            LoggerPMT02120Print.R_InitializeLogger(logger);
            _logger = LoggerPMT02120Print.R_GetInstanceLogger();
            _activitySource = PMT02120PrintActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT02120PrintController));
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
            pcFileTemplate = System.IO.Path.Combine("Reports", "PMT02100PrintReport.frx");
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
        public R_DownloadFileResultDTO PrintReportPost(PMT02120PrintReportParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("PrintReportPost");
            _logger.LogInfo("Start || PrintReportPost(Controller)");
            R_Exception loException = new R_Exception();
            R_DownloadFileResultDTO loRtn = null;
            PMT02120PrintLogKeyDTO loCache = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();

                loCache = new PMT02120PrintLogKeyDTO
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY),
                    poReportGlobal = R_ReportGlobalVar.R_GetReportDTO()
                };

                _logger.LogInfo("Set GUID Param || PrintReportPost(Controller)");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<PMT02120PrintLogKeyDTO>(loCache));
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
            PMT02120PrintLogKeyDTO loResultGUID = null;
            FileStreamResult loRtn = null;
            try
            {
                //Get Parameter
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<PMT02120PrintLogKeyDTO>(R_DistributedCache.Cache.Get(pcGuid));

                //Get Data and Set Log Key
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                R_ReportGlobalVar.R_SetFromReportDTO(loResultGUID.poReportGlobal);
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
        private PMT02120PrintReportResultWithBaseHeaderPrintDTO GenerateDataPrint(PMT02120PrintReportParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GenerateDataPrint");
            _logger.LogInfo("Start || GenerateDataPrint(Controller)");
            R_Exception loException = new R_Exception();
            PMT02120PrintReportResultWithBaseHeaderPrintDTO loRtn = new PMT02120PrintReportResultWithBaseHeaderPrintDTO();
            System.Globalization.CultureInfo loCultureInfo = new System.Globalization.CultureInfo(R_BackGlobalVar.REPORT_CULTURE);
            //System.Globalization.CultureInfo loCultureInfo = new System.Globalization.CultureInfo("id");
            try
            {
                PMT02120PrintCls loCls = new PMT02120PrintCls();
                loRtn.ReportData = new PMT02120PrintReportResultDTO();

                //Add Resources
                loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Page", loCultureInfo);
                loRtn.BaseHeaderColumn.Of = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_Date = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_By", loCultureInfo);

                PMT02120PrintReportColumnDTO loColumnObject = new PMT02120PrintReportColumnDTO();
                var loColumn = AssignValuesWithMessages(typeof(PMT02100BackResources.Resources_Dummy_Class), loCultureInfo, loColumnObject);


                List<PMT02120PrintReportDTO> loCollection = loCls.GetPrintReportList(poParam);

                _logger.LogInfo("Group Data || GenerateDataPrint(Controller)");

                List<PMT02120PrintReportGroupingDTO> loTempData = loCollection
                .GroupBy(item => new {
                    item.CDEPT_CODE,
                    item.CREF_NO,
                    item.CREF_DATE,
                    item.CCONFIRMED_HO_DATE,
                    item.CCONFIRMED_HO_TIME,
                    item.CCONFIRMED_HO_BY,
                    item.CSCHEDULED_HO_DATE,
                    item.CSCHEDULED_HO_TIME,
                    item.IRESCHEDULE_COUNT,
                    item.IPRINT_COUNT,
                    item.CPROPERTY_NAME,
                    item.CBUILDING_NAME,
                    item.CFLOOR_NAME,
                    item.CUNIT_NAME,
                    item.CUNIT_TYPE_CATEGORY_NAME,
                    item.CTENANT_NAME,
                    item.CTENANT_PHONE_NO,
                    item.CTENANT_EMAIL
                })
                .Select(header => new PMT02120PrintReportGroupingDTO
                {
                    CDEPT_CODE = !string.IsNullOrWhiteSpace(header.Key.CDEPT_CODE) ? header.Key.CDEPT_CODE : "-",
                    CREF_NO = !string.IsNullOrWhiteSpace(header.Key.CREF_NO) ? header.Key.CREF_NO : "-",
                    CREF_DATE = header.Key.CREF_DATE,
                    DREF_DATE = DateTime.ParseExact(header.Key.CREF_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                    DCONFIRMED_DATE = !string.IsNullOrWhiteSpace(header.Key.CCONFIRMED_HO_DATE) && !string.IsNullOrWhiteSpace(header.Key.CCONFIRMED_HO_TIME) ? DateTime.ParseExact($"{header.Key.CCONFIRMED_HO_DATE} {header.Key.CCONFIRMED_HO_TIME}", "yyyyMMdd HH:mm", CultureInfo.InvariantCulture) : (DateTime?)null,
                    DCONFIRMED_DATE_ONLY = !string.IsNullOrWhiteSpace(header.Key.CCONFIRMED_HO_DATE) ? DateTime.ParseExact(header.Key.CCONFIRMED_HO_DATE, "yyyyMMdd", CultureInfo.InvariantCulture) : (DateTime?)null,
                    CCONFIRMED_BY = !string.IsNullOrWhiteSpace(header.Key.CCONFIRMED_HO_BY) ? header.Key.CCONFIRMED_HO_BY : "-",
                    DSCHEDULED_DATE = !string.IsNullOrWhiteSpace(header.Key.CSCHEDULED_HO_DATE) && !string.IsNullOrWhiteSpace(header.Key.CSCHEDULED_HO_TIME) ? DateTime.ParseExact($"{header.Key.CSCHEDULED_HO_DATE} {header.Key.CSCHEDULED_HO_TIME}", "yyyyMMdd HH:mm", CultureInfo.InvariantCulture) : (DateTime?)null,
                    DSCHEDULED_DATE_ONLY = !string.IsNullOrWhiteSpace(header.Key.CSCHEDULED_HO_DATE) ? DateTime.ParseExact(header.Key.CSCHEDULED_HO_DATE, "yyyyMMdd", CultureInfo.InvariantCulture) : (DateTime?)null,
                    IRESCHEDULE_COUNT = header.Key.IRESCHEDULE_COUNT,
                    IPRINT_COUNT = header.Key.IPRINT_COUNT,
                    CPROPERTY_NAME = !string.IsNullOrWhiteSpace(header.Key.CPROPERTY_NAME) ? header.Key.CPROPERTY_NAME : "-",
                    CBUILDING_NAME = !string.IsNullOrWhiteSpace(header.Key.CBUILDING_NAME) ? header.Key.CBUILDING_NAME : "-",
                    CFLOOR_NAME = !string.IsNullOrWhiteSpace(header.Key.CFLOOR_NAME) ? header.Key.CFLOOR_NAME : "-",
                    CUNIT_NAME = !string.IsNullOrWhiteSpace(header.Key.CUNIT_NAME) ? header.Key.CUNIT_NAME : "-",
                    CUNIT_TYPE_CATEGORY_NAME = !string.IsNullOrWhiteSpace(header.Key.CUNIT_TYPE_CATEGORY_NAME) ? header.Key.CUNIT_TYPE_CATEGORY_NAME : "-",
                    CTENANT_NAME = !string.IsNullOrWhiteSpace(header.Key.CTENANT_NAME) ? header.Key.CTENANT_NAME : "-",
                    CTENANT_PHONE_NO = !string.IsNullOrWhiteSpace(header.Key.CTENANT_PHONE_NO) ? header.Key.CTENANT_PHONE_NO : "-",
                    CTENANT_EMAIL = !string.IsNullOrWhiteSpace(header.Key.CTENANT_EMAIL) ? header.Key.CTENANT_EMAIL : "-",
                    FooterData = new PMT02120PrintReportFooterDTO()
                    {
                        CMESSAGE_CONFIRMATION = "",
                        CMESSAGE_DUTIES = ""
                    },
                    EmployeeData = header.Where(x => x.LCHECKLIST == false)
                        .GroupBy(item2 => new
                        {
                            item2.CEMPLOYEE_ID,
                            item2.CEMPLOYEE_NAME,
                            item2.CEMPLOYEE_TYPE,
                            item2.CEMPLOYEE_PHONE_NO
                        })
                        .Select(Employee => new PMT02120PrintReportEmployeeDTO
                        {
                            CEMPLOYEE = Employee.Key.CEMPLOYEE_ID + ((!string.IsNullOrWhiteSpace(Employee.Key.CEMPLOYEE_ID) && !string.IsNullOrWhiteSpace(Employee.Key.CEMPLOYEE_NAME)) ? " - " : "") + Employee.Key.CEMPLOYEE_NAME,
                            CEMPLOYEE_TYPE = Employee.Key.CEMPLOYEE_TYPE,
                            CEMPLOYEE_PHONE_NO = Employee.Key.CEMPLOYEE_PHONE_NO
                        })
                        .ToList(),
                    ChecklistData = header.Where(x => x.LCHECKLIST == true)
                        .GroupBy(item2 => new
                        {
                            item2.CCATEGORY,
                            item2.CITEM,
                            item2.IDEFAULT_QUANTITY,
                            item2.CUNIT
                        })
                        .Select(Checklist => new PMT02120PrintReportChecklistDTO
                        {
                            CCATEGORY = Checklist.Key.CCATEGORY,
                            CITEM = Checklist.Key.CITEM,
                            CSTATUS = "",
                            CNOTES = "",
                            CQUANTITY = Checklist.Key.IDEFAULT_QUANTITY != 0 ? "/" + Checklist.Key.IDEFAULT_QUANTITY.ToString() + " " + Checklist.Key.CUNIT : "-",
                            CUNIT = Checklist.Key.CUNIT,
                            //CQUANTITY = Checklist.Key.IDEFAULT_QUANTITY != 0 ? Checklist.Key.IDEFAULT_QUANTITY.ToString() : "-",
                            //CACTUAL_QUANTITY = Checklist.Key.IDEFAULT_QUANTITY != 0 ? "" : "-",
                            IDEFAULT_QUANTITY = Checklist.Key.IDEFAULT_QUANTITY
                        })
                        .ToList()
                })
                .ToList();

                //PMT02120PrintReportHeaderDTO loTempData = loCollection
                //.GroupBy(item => new {
                //    item.CDEPT_CODE,
                //    item.CREF_NO,
                //    item.CREF_DATE,
                //    item.CCONFIRMED_HO_DATE, 
                //    item.CCONFIRMED_HO_TIME,
                //    item.CCONFIRMED_HO_BY,
                //    item.CSCHEDULED_HO_DATE,
                //    item.CSCHEDULED_HO_TIME,
                //    item.IRESCHEDULE_COUNT,
                //    item.IPRINT_COUNT,
                //    item.CPROPERTY_NAME,
                //    item.CBUILDING_NAME,
                //    item.CFLOOR_NAME,
                //    item.CUNIT_NAME,
                //    item.CUNIT_TYPE_CATEGORY_NAME,
                //    item.CTENANT_NAME,
                //    item.CTENANT_PHONE_NO,
                //    item.CTENANT_EMAIL
                //})
                //.Select(header => new PMT02120PrintReportHeaderDTO
                //{
                //    CDEPT_CODE = !string.IsNullOrWhiteSpace(header.Key.CDEPT_CODE) ? header.Key.CDEPT_CODE : "-",
                //    CREF_NO = !string.IsNullOrWhiteSpace(header.Key.CREF_NO) ? header.Key.CREF_NO : "-",
                //    CREF_DATE = header.Key.CREF_DATE,
                //    DREF_DATE = DateTime.ParseExact(header.Key.CREF_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                //    DCONFIRMED_DATE = !string.IsNullOrWhiteSpace(header.Key.CCONFIRMED_HO_DATE) && !string.IsNullOrWhiteSpace(header.Key.CCONFIRMED_HO_TIME) ? DateTime.ParseExact($"{header.Key.CCONFIRMED_HO_DATE} {header.Key.CCONFIRMED_HO_TIME}", "yyyyMMdd HH:mm", CultureInfo.InvariantCulture) : (DateTime?)null,
                //    DCONFIRMED_DATE_ONLY = !string.IsNullOrWhiteSpace(header.Key.CCONFIRMED_HO_DATE) ? DateTime.ParseExact(header.Key.CCONFIRMED_HO_DATE, "yyyyMMdd", CultureInfo.InvariantCulture) : (DateTime?)null,
                //    CCONFIRMED_BY = !string.IsNullOrWhiteSpace(header.Key.CCONFIRMED_HO_BY) ? header.Key.CCONFIRMED_HO_BY : "-",
                //    DSCHEDULED_DATE = !string.IsNullOrWhiteSpace(header.Key.CSCHEDULED_HO_DATE) && !string.IsNullOrWhiteSpace(header.Key.CSCHEDULED_HO_TIME) ? DateTime.ParseExact($"{header.Key.CSCHEDULED_HO_DATE} {header.Key.CSCHEDULED_HO_TIME}", "yyyyMMdd HH:mm", CultureInfo.InvariantCulture) : (DateTime?)null,
                //    DSCHEDULED_DATE_ONLY = !string.IsNullOrWhiteSpace(header.Key.CSCHEDULED_HO_DATE) ? DateTime.ParseExact(header.Key.CSCHEDULED_HO_DATE, "yyyyMMdd", CultureInfo.InvariantCulture) : (DateTime?)null,
                //    IRESCHEDULE_COUNT = header.Key.IRESCHEDULE_COUNT,
                //    IPRINT_COUNT = header.Key.IPRINT_COUNT,
                //    CPROPERTY_NAME = !string.IsNullOrWhiteSpace(header.Key.CPROPERTY_NAME) ? header.Key.CPROPERTY_NAME : "-",
                //    CBUILDING_NAME = !string.IsNullOrWhiteSpace(header.Key.CBUILDING_NAME) ? header.Key.CBUILDING_NAME : "-",
                //    CUNIT_NAME = !string.IsNullOrWhiteSpace(header.Key.CUNIT_NAME) ? header.Key.CUNIT_NAME : "-",
                //    CUNIT_TYPE_CATEGORY_NAME = !string.IsNullOrWhiteSpace(header.Key.CUNIT_TYPE_CATEGORY_NAME) ? header.Key.CUNIT_TYPE_CATEGORY_NAME : "-",
                //    CTENANT_NAME = !string.IsNullOrWhiteSpace(header.Key.CTENANT_NAME) ? header.Key.CTENANT_NAME : "-",
                //    CTENANT_PHONE_NO = !string.IsNullOrWhiteSpace(header.Key.CTENANT_PHONE_NO) ? header.Key.CTENANT_PHONE_NO : "-",
                //    CTENANT_EMAIL = !string.IsNullOrWhiteSpace(header.Key.CTENANT_EMAIL) ? header.Key.CTENANT_EMAIL : "-",
                //    FooterData = new PMT02120PrintReportFooterDTO()
                //    {
                //        CMESSAGE_CONFIRMATION = "",
                //        CMESSAGE_DUTIES = ""
                //    },
                //    EmployeeData = header.Where(x => x.LCHECKLIST == false)
                //        .GroupBy(item2 => new
                //        {
                //            item2.CEMPLOYEE_ID,
                //            item2.CEMPLOYEE_NAME,
                //            item2.CEMPLOYEE_TYPE,
                //            item2.CEMPLOYEE_PHONE_NO
                //        })
                //        .Select(Employee => new PMT02120PrintReportEmployeeDTO
                //        {
                //            CEMPLOYEE = Employee.Key.CEMPLOYEE_ID + ((!string.IsNullOrWhiteSpace(Employee.Key.CEMPLOYEE_ID) && !string.IsNullOrWhiteSpace(Employee.Key.CEMPLOYEE_NAME)) ? " - " : "") + Employee.Key.CEMPLOYEE_NAME,
                //            CEMPLOYEE_TYPE = Employee.Key.CEMPLOYEE_TYPE,
                //            CEMPLOYEE_PHONE_NO = Employee.Key.CEMPLOYEE_PHONE_NO
                //        })
                //        .ToList(),
                //    ChecklistData = header.Where(x => x.LCHECKLIST == true)
                //        .GroupBy(item2 => new
                //        {
                //            item2.CCATEGORY,
                //            item2.CITEM,
                //            item2.IDEFAULT_QUANTITY
                //        })
                //        .Select(Checklist => new PMT02120PrintReportChecklistDTO
                //        {
                //            CCATEGORY = Checklist.Key.CCATEGORY,
                //            CITEM = Checklist.Key.CITEM,
                //            CSTATUS = "",
                //            CNOTES = "",
                //            CQUANTITY = Checklist.Key.IDEFAULT_QUANTITY != 0 ? Checklist.Key.IDEFAULT_QUANTITY.ToString() : "-",
                //            CACTUAL_QUANTITY = Checklist.Key.IDEFAULT_QUANTITY != 0 ? "" : "-",
                //            IDEFAULT_QUANTITY = Checklist.Key.IDEFAULT_QUANTITY
                //        })
                //        .ToList()
                //})
                //.FirstOrDefault();

                //if (loTempData != null)
                //{
                //    if (!string.IsNullOrWhiteSpace(loTempData.CJRN_ID))
                //    {
                //        _logger.LogInfo("Run  || GenerateDataPrint(Controller)");
                //        PMT02120PrintReportSubDetailParameterDTO loSubParameter = new PMT02120PrintReportSubDetailParameterDTO()
                //        {
                //            CJRN_ID = loTempData.CJRN_ID,
                //            CLANGUAGE_ID = poParam.CLANGUAGE_ID
                //        };
                //        loSubDetail = loCls.GetPrintSubDetailReportList(loSubParameter);
                //    }
                //}

                // Set Base Header Data
                var loBaseHeader = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY);
                _logger.LogInfo("Set Base Header || GenerateDataPrint(Controller)");
                BaseHeaderDTO loParam = new BaseHeaderDTO()
                {
                    CPRINT_CODE = poParam.CCOMPANY_ID,
                    CPRINT_NAME = "HANDOVER",
                    CUSER_ID = loBaseHeader.USER_ID,
                };
                PMT02120PrintReportDTO loGetLogo = loCls.GetBaseHeaderLogoCompany(poParam);
                loParam.BLOGO_COMPANY = loGetLogo.OLOGO;
                loParam.CCOMPANY_NAME = loGetLogo.CCOMPANY_NAME;
                loParam.DPRINT_DATE_COMPANY = DateTime.ParseExact(loGetLogo.CDATETIME_NOW, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture);

                _logger.LogInfo("Set Parameter || GenerateDataPrint(Controller)");
                PMT02120PrintReportResultDTO loData = new PMT02120PrintReportResultDTO()
                {
                    Column = (PMT02120PrintReportColumnDTO)loColumn,
                    Data = new PMT02120PrintReportHeaderDTO()
                    {
                        ReportData = loTempData
                    },
                    //Data = loCollection,
                    LASSIGNMENT = poParam.LASSIGNMENT,
                    LCHECKLIST = poParam.LCHECKLIST
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