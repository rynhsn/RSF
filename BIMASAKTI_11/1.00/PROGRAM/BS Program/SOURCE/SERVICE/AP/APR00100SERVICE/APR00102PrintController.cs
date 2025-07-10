using BaseHeaderReportCOMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI.Log;
using R_CommonFrontBackAPI;
using R_ReportFastReportBack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using APR00100COMMON;
using APR00100COMMON.DTO_s.Print;
using APR00100BACK;
using BaseHeaderReportCOMMON;
using APR00100COMMON.DTO_s;


namespace APR00101SERVICE
{
    public class APR00102PrintController : R_ReportControllerBase
    {
        private APR00100PrintLogger _logger;
        private R_ReportFastReportBackClass _ReportCls;
        private ReportParamDTO _Parameter;
        private readonly ActivitySource _activitySource;
        APR00100BackResources.Resources_Dummy_Class _backRes = new();

        public APR00102PrintController(ILogger<APR00100PrintLogger> logger)
        {
            APR00100PrintLogger.R_InitializeLogger(logger);
            _logger = APR00100PrintLogger.R_GetInstanceLogger();
            _activitySource = APR00100Activity.R_InitializeAndGetActivitySource(nameof(APR00102PrintController));
            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }

        #region reporthelper

        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "APR00100DetailBySupp.frx");
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
        public R_DownloadFileResultDTO DownloadResultPrintPost(ReportParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            _logger.LogInfo("Start - Post DownloadResultPrintPost Status");
            R_Exception loException = new R_Exception();
            APR00100PrintLogKey loCache = null;
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new APR00100PrintLogKey
                {
                    poParamSummary = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY)
                };
                _logger.LogInfo("Set GUID Param - Post DownloadResultPrintPost Status");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End - Print UserActivity");
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult UserActivityDetail_ReportListGet(string pcGuid)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            R_Exception loException = new R_Exception();
            FileStreamResult loRtn = null;
            APR00100PrintLogKey loResultGUID = null;
            try
            {
                // Deserialize the GUID from the cache
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<APR00100PrintLogKey>(R_DistributedCache.Cache.Get(pcGuid));
                _logger.LogDebug("Deserialized GUID: {pcGuid}", pcGuid);

                // Get Parameter
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);

                _Parameter = loResultGUID.poParamSummary;

                _logger.LogDebug("Deserialized GUID: {pcGuid}", pcGuid);
                _logger.LogDebug("Deserialized Parameters: {@Parameters}", _Parameter);
                R_FileType loFileType = new();
                if (loResultGUID.poParamSummary.LIS_PRINT)
                {
                    loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF), R_ReportUtility.GetMimeType(R_FileType.PDF));
                }
                else
                {
                    switch (loResultGUID.poParamSummary.CREPORT_FILETYPE)
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
                    loRtn = File(_ReportCls.R_GetStreamReport(peExport: loFileType), R_ReportUtility.GetMimeType(loFileType), $"{loResultGUID.poParamSummary.CREPORT_FILENAME}.{loResultGUID.poParamSummary.CREPORT_FILETYPE}");
                }
                _logger.LogInfo("Data retrieval successful. Generating report.");

                _logger.LogInfo("Report generated successfully.");
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End - UserActivity Report Generation");
            return loRtn;
        }

        #region Helper

        private ReportPrintSummaryDTO GenerateDataPrint(ReportParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            var loEx = new R_Exception();
            ReportPrintSummaryDTO loRtn = new ReportPrintSummaryDTO();
            var loParam = new BaseHeaderDTO();

            System.Globalization.CultureInfo loCultureInfo =
                new System.Globalization.CultureInfo(poParam.CREPORT_CULTURE);

            try
            {
                _logger.LogInfo("_logger.LogInfo(\"Start - Generating data for Print\");\n data for UserActivity report.");

                //Add Resources
                loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),
                    "Page", loCultureInfo);
                loRtn.BaseHeaderColumn.Of =
                    R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_Date =
                    R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),
                    "Print_By", loCultureInfo);

                ReportLabelDTO loColumnObject = new ReportLabelDTO();
                var loColumn = AssignValuesWithMessages(typeof(APR00100BackResources.Resources_Dummy_Class), loCultureInfo, loColumnObject);

                // Set base header data
                _logger.LogDebug("Deserialized Print Parameters: {@PrintParameters}");

                loParam.CCOMPANY_NAME = poParam.CCOMPANY_ID.ToUpper();
                loParam.CPRINT_CODE = "1003";
                loParam.CPRINT_NAME = APR00100ContextConstant.CPROGRAM_NAME;
                loParam.CUSER_ID = poParam.CUSER_ID.ToUpper();

                // Create an instance of PMR01000Cls
                var loCls = new APR00100Cls();

                _logger.LogInfo("Set BaseHeader Report");
                // loParam.BLOGO_COMPANY = loCls.GetBaseHeaderLogoCompany(poParam.CCOMPANY_ID).CLOGO;
                var loBaseHeader = loCls.GetBaseHeaderLogoCompany();
                loParam.BLOGO_COMPANY = loBaseHeader.CLOGO;
                loParam.CCOMPANY_NAME = loBaseHeader.CCOMPANY_NAME;
                loParam.DPRINT_DATE_COMPANY = DateTime.ParseExact(loBaseHeader.CDATETIME_NOW, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture);

                // Create an instance 
                ReportSummaryDataDTO loData = new ReportSummaryDataDTO()
                {
                    Title = APR00100ContextConstant.CPROGRAM_NAME,
                    Header = APR00100ContextConstant.CPROGRAM_NAME,
                    Label = (ReportLabelDTO)loColumn,
                    Param = poParam,
                    SummaryDate = new List<APR00100SummaryByDateDTO>(),
                };

                // Get print data for Group Of Account report
                var loCollection = loCls.GetSummaryData(poParam);
                
                loCollection = loCollection.Where(x => 
                         x.NBEGINNING_APPLY_AMOUNT != 0 && 
                         x.NTOTAL_REMAINING != 0 && 
                         x.NTAXABLE_AMOUNT != 0 && 
                         x.NGAIN_LOSS_AMOUNT != 0 && 
                         x.NCASH_BANK_AMOUNT != 0).ToList();
                
                loCollection.ForEach(x =>
                {
                    if (x.CTRANS_CODE is "120010" or "110040")
                    {
                        x.NBEGINNING_APPLY_AMOUNT = -x.NBEGINNING_APPLY_AMOUNT;
                        x.NLBEGINNING_APPLY_AMOUNT = -x.NLBEGINNING_APPLY_AMOUNT;
                        x.NBBEGINNING_APPLY_AMOUNT = -x.NBBEGINNING_APPLY_AMOUNT;
                        x.NTOTAL_REMAINING = -x.NTOTAL_REMAINING;
                        x.NLTOTAL_REMAINING = -x.NLTOTAL_REMAINING;
                        x.NBTOTAL_REMAINING = -x.NBTOTAL_REMAINING;
                    }
                });
                
                loCollection.ForEach(x => x.DREF_DATE = DateTime.TryParseExact(x.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldInvoiceDate) ? ldInvoiceDate : DateTime.MinValue);



                var loTempData = loCollection
                    .GroupBy(data1a => new
                        {
                            data1a.CSUPPLIER_ID,
                            data1a.CSUPPLIER_NAME,
                        }).Select(data1b => new APR00100DataResultDTO()
                        {
                            CSUPPLIER_ID = data1b.Key.CSUPPLIER_ID,
                            CSUPPLIER_NAME = data1b.Key.CSUPPLIER_NAME,
                            Detail1 = data1b.GroupBy(data2a => new
                            {
                               data2a.CREF_DATE,
                               data2a.DREF_DATE,
                               data2a.CDEPT_CODE,
                               data2a.CDEPT_NAME,
                               data2a.CREF_PRD,
                               data2a.CREF_NO,
                               data2a.CTRANS_CODE,
                               data2a.CTRANS_NAME,
                            }).Select(data2b => new APR00100SummaryBySupp1DTO()
                            {
                                CREF_DATE = data2b.Key.CREF_DATE,
                                DREF_DATE = data2b.Key.DREF_DATE,
                                CDEPT_CODE = data2b.Key.CDEPT_CODE,
                                CDEPT_NAME = data2b.Key.CDEPT_NAME,
                                CREF_PRD = data2b.Key.CREF_PRD,
                                CREF_NO = data2b.Key.CREF_NO,
                                CTRANS_CODE = data2b.Key.CTRANS_CODE,
                                CTRANS_NAME = data2b.Key.CTRANS_NAME,
                                Detail2 = data2b.GroupBy(data2b => new
                                {
                                    data2b.CCURRENCY_CODE,
                                    data2b.CBASE_CURRENCY_CODE,
                                    data2b.CLOCAL_CURRENCY_CODE,
                                    data2b.NBEGINNING_APPLY_AMOUNT,
                                    data2b.NLBEGINNING_APPLY_AMOUNT,
                                    data2b.NBBEGINNING_APPLY_AMOUNT,
                                    data2b.NTOTAL_REMAINING,
                                    data2b.NLTOTAL_REMAINING,
                                    data2b.NBTOTAL_REMAINING,
                                    data2b.NTAXABLE_AMOUNT,
                                    data2b.NBTAXABLE_AMOUNT,
                                    data2b.NLTAXABLE_AMOUNT,
                                    data2b.NGAIN_LOSS_AMOUNT,
                                    data2b.NLGAIN_LOSS_AMOUNT,
                                    data2b.NBGAIN_LOSS_AMOUNT,
                                    data2b.NCASH_BANK_AMOUNT,
                                    data2b.NLCASH_BANK_AMOUNT,
                                    data2b.NBCASH_BANK_AMOUNT,

                                }).Select(data3b => new APR00100SummaryBySupp2DTO()
                                {
                                    CCURRENCY_CODE = data3b.Key.CCURRENCY_CODE,
                                    CBASE_CURRENCY_CODE = data3b.Key.CBASE_CURRENCY_CODE,
                                    CLOCAL_CURRENCY_CODE = data3b.Key.CLOCAL_CURRENCY_CODE,
                                    NBEGINNING_APPLY_AMOUNT = data3b.Key.NBEGINNING_APPLY_AMOUNT,
                                    NLBEGINNING_APPLY_AMOUNT = data3b.Key.NLBEGINNING_APPLY_AMOUNT,
                                    NBBEGINNING_APPLY_AMOUNT = data3b.Key.NBBEGINNING_APPLY_AMOUNT,
                                    NTOTAL_REMAINING = data3b.Key.NTOTAL_REMAINING,
                                    NLTOTAL_REMAINING = data3b.Key.NLTOTAL_REMAINING,
                                    NBTOTAL_REMAINING = data3b.Key.NBTOTAL_REMAINING,
                                    NTAXABLE_AMOUNT = data3b.Key.NTAXABLE_AMOUNT,
                                    NBTAXABLE_AMOUNT = data3b.Key.NBTAXABLE_AMOUNT,
                                    NLTAXABLE_AMOUNT = data3b.Key.NLTAXABLE_AMOUNT,
                                    NGAIN_LOSS_AMOUNT = data3b.Key.NGAIN_LOSS_AMOUNT,
                                    NLGAIN_LOSS_AMOUNT = data3b.Key.NLGAIN_LOSS_AMOUNT,
                                    NBGAIN_LOSS_AMOUNT = data3b.Key.NBGAIN_LOSS_AMOUNT,
                                    NCASH_BANK_AMOUNT = data3b.Key.NCASH_BANK_AMOUNT,
                                    NLCASH_BANK_AMOUNT = data3b.Key.NLCASH_BANK_AMOUNT,
                                    NBCASH_BANK_AMOUNT = data3b.Key.NBCASH_BANK_AMOUNT,
                                }).ToList()
                            }).ToList()
                        }).ToList();

                _logger.LogInfo("Data generation successful. Processing data for printing.");

                // Set the generated data in loRtn
                _logger.LogInfo("Data processed successfully. Generating print output.");

                // Set the generated data in loRt
                loData.Data = loTempData;
                /*
                loRtn.ReportData.Total = loTempDataTotal;
                */
                loRtn.BaseHeaderData = loParam;
                loRtn.ReportData = loData;

                _logger.LogInfo("Print output generated successfully. Saving print file.");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
            _logger.LogInfo("End - Data Generation for Print");
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
