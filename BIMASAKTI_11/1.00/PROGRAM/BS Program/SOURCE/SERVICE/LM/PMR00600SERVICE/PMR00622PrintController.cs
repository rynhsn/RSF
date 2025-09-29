using BaseHeaderReportCOMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR00600BACK;
using PMR00600COMMON;
using PMR00600COMMON.DTO_s;
using PMR00600COMMON.DTO_s.PrintDetail;
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

namespace PMR00600SERVICE
{
    public class PMR00622PrintController : R_ReportControllerBase
    {
        private PMR00600PrintLogger _logger;
        private R_ReportFastReportBackClass _ReportCls;
        private PMR00601ParamDTO _Parameter;
        private readonly ActivitySource _activitySource;
        PMR00600BackResources.Resources_Dummy_Class _backRes = new();

        public PMR00622PrintController(ILogger<PMR00600PrintLogger> logger)
        {
            PMR00600PrintLogger.R_InitializeLogger(logger);
            _logger = PMR00600PrintLogger.R_GetInstanceLogger();
            _activitySource = PMR00600Activity.R_InitializeAndGetActivitySource(nameof(PMR00622PrintController));


            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }

        #region reporthelper

        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "PMR00600DetailByCharge.frx");
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
        public R_DownloadFileResultDTO DownloadResultPrintPost(PMR00601ParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            _logger.LogInfo("Start - Post DownloadResultPrintPost Status");
            R_Exception loException = new R_Exception();
            PMR00600PrintLogKey loCache = null;
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new PMR00600PrintLogKey
                {
                    poParamDetail = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY),
                    poReportGlobal = R_ReportGlobalVar.R_GetReportDTO()
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
            _logger.LogInfo("End - Print Overtime");
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult OvertimeDetailByCharge_ReportListGet(string pcGuid)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            R_Exception loException = new R_Exception();
            FileStreamResult loRtn = null;
            PMR00600PrintLogKey loResultGUID = null;
            try
            {
                // Deserialize the GUID from the cache
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<PMR00600PrintLogKey>(R_DistributedCache.Cache.Get(pcGuid));
                _logger.LogDebug("Deserialized GUID: {pcGuid}", pcGuid);

                // Get Parameter
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                R_ReportGlobalVar.R_SetFromReportDTO(loResultGUID.poReportGlobal);

                _Parameter = loResultGUID.poParamDetail;

                _logger.LogDebug("Deserialized GUID: {pcGuid}", pcGuid);
                _logger.LogDebug("Deserialized Parameters: {@Parameters}", _Parameter);

                R_FileType loFileType = new();
                if (string.IsNullOrWhiteSpace(loResultGUID.poParamDetail.CREPORT_FILEEXT) || string.IsNullOrWhiteSpace(loResultGUID.poParamDetail.CREPORT_FILENAME))
                {
                    loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF), R_ReportUtility.GetMimeType(R_FileType.PDF));
                }
                else
                {
                    loFileType = loResultGUID.poParamDetail.CREPORT_FILEEXT switch
                    {
                        "XLSX" => R_FileType.XLSX,
                        "PDF" => R_FileType.PDF,
                        "XLS" => R_FileType.XLS,
                        "CSV" => R_FileType.CSV,
                        _ => R_FileType.PDF,
                    };

                    //print nama save as
                    loRtn = File(_ReportCls.R_GetStreamReport(peExport: loFileType), R_ReportUtility.GetMimeType(loFileType), $"{loResultGUID.poParamDetail.CREPORT_FILENAME}.{loResultGUID.poParamDetail.CREPORT_FILEEXT}");
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
            _logger.LogInfo("End - Overtime Report Generation");
            return loRtn;
        }

        #region Helper

        private PMR00601PrintDisplayDTO GenerateDataPrint(PMR00601ParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            var loEx = new R_Exception();
            PMR00601PrintDisplayDTO loRtn = new PMR00601PrintDisplayDTO();
            var loParam = new BaseHeaderDTO();

            CultureInfo loCultureInfo = new(R_BackGlobalVar.REPORT_CULTURE);

            try
            {
                _logger.LogInfo("_logger.LogInfo(\"Start - Generating data for Print\");\n data for Overtime report.");

                //Add Resources
                loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),
                    "Page", loCultureInfo);
                loRtn.BaseHeaderColumn.Of =
                    R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_Date =
                    R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),
                    "Print_By", loCultureInfo);

                PMR00601LabelDTO loColumnObject = new PMR00601LabelDTO();
                var loColumn = AssignValuesWithMessages(typeof(PMR00600BackResources.Resources_Dummy_Class), loCultureInfo, loColumnObject);

                // Set base header data
                _logger.LogDebug("Deserialized Print Parameters: {@PrintParameters}");

                //loParam.CPRINT_CODE = "622";
                //loParam.CPRINT_NAME = PMR00600ContextConstant.CPROGRAM_NAME;
                //loParam.CUSER_ID = R_BackGlobalVar.USER_ID.ToUpper();
                //var loCls = new PMR00600Cls();
                //loParam.BLOGO_COMPANY = loCls.GetCompanyLogo(R_BackGlobalVar.COMPANY_ID).CLOGO;
                //loParam.CCOMPANY_NAME = loCls.GetCompanyName(R_BackGlobalVar.COMPANY_ID).CCOMPANY_NAME;

                var loCls = new PMR00600Cls();
                var lcCompany = R_BackGlobalVar.COMPANY_ID;
                var lcUser = R_BackGlobalVar.USER_ID;
                var lcLang = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Set Parameter");
                var loDbParam = new PMR00600SPParamDTO
                {
                    CCOMPANY_ID = lcCompany,
                    CLANG_ID = lcLang,
                    CPROPERTY_ID = poParam.CPROPERTY_ID,
                    CFROM_BUILDING_ID = poParam.CFROM_BUILDING_ID,
                    CTO_BUILDING_ID = poParam.CTO_BUILDING_ID

                };
                var loHeader = loCls.GetBaseHeaderLogoCompany(loDbParam);
                loRtn.BaseHeaderData = new BaseHeaderDTO
                {
                    BLOGO_COMPANY = loHeader.BLOGO,
                    CCOMPANY_NAME = loHeader.CCOMPANY_NAME!,
                    //DPRINT_DATE_COMPANY = DateTime.ParseExact(loHeader.CDATETIME_NOW, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture),
                    CPRINT_DATE_COMPANY = DateTime.ParseExact(loHeader.CDATETIME_NOW, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture).ToString(R_BackGlobalVar.REPORT_FORMAT_SHORT_DATE + " " + R_BackGlobalVar.REPORT_FORMAT_SHORT_TIME),
                    CPRINT_CODE = "622",
                    CPRINT_NAME = PMR00600ContextConstant.CPROGRAM_NAME,
                    CUSER_ID = poParam.CUSER_ID.ToUpper()
                };

                // Create an instance
                PMR00601ReportDataDTO loData = new PMR00601ReportDataDTO()
                {
                    Title = PMR00600ContextConstant.CPROGRAM_NAME,
                    Header = PMR00600ContextConstant.CPROGRAM_NAME,
                    Label = (PMR00601LabelDTO)loColumn,
                    Param = poParam,
                    Data = new List<PMR00601DataDTO>(),
                };

                // Get print data for Group Of Account report
                var loCollData = loCls.GetDetailData(poParam);
                _logger.LogInfo("Data generation successful. Processing data for printing.");

                // Set the generated data in loRtn
                _logger.LogInfo("Data processed successfully. Generating print output.");
                var loMappingData = R_Utility.R_ConvertCollectionToCollection<PMR00601SPResultDTO, PMR00601DataDTO>(loCollData);
                foreach (var loItem in loMappingData)
                {
                    loItem.DAGREEMENT_DATE = DateTime.TryParseExact(loItem.CAGREEMENT_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loResultAgrr) ? loResultAgrr : null;
                    loItem.DINVOICE_DATE = DateTime.TryParseExact(loItem.CINVOICE_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loResultInv) ? loResultInv : null;
                    loItem.DOVERTIME_DATE = DateTime.TryParseExact(loItem.COVERTIME_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loResultOvt) ? loResultOvt : null;
                }
                loData.Data = new List<PMR00601DataDTO>(loMappingData);
                //loRtn.BaseHeaderData = loParam;
                loRtn.ReportDataDTO = loData;
                _logger.LogInfo("Print output generated successfully. Saving print file.");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End - Data Generation for Print");
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

        private static DateTime? ConvertStringToDateTimeFormat(string? pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                return null;
            }
            else
            {
                if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime loResult))
                {
                    return loResult;
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

    }
}
