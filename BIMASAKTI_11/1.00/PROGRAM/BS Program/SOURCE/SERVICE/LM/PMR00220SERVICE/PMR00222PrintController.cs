using BaseHeaderReportCOMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR00220BACK;
using PMR00220COMMON;
using PMR00220COMMON.DTO_s;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI.Log;
using R_CommonFrontBackAPI;
using R_ReportFastReportBack;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using PMR00220COMMON.Print_DTO;
using PMR00220COMMON.Print_DTO.Detail;
using PMR00220COMMON.Print_DTO.Detail.SubDetail;


namespace PMR00220SERVICE
{
    public class PMR00222PrintController : R_ReportControllerBase
    {
        private PMR00220PrintLogger _logger;
        private R_ReportFastReportBackClass _ReportCls;
        private PMR00220ParamDTO _Parameter;
        private readonly ActivitySource _activitySource;
        PMR00220BackResources.Resources_Dummy_Class _backRes = new();

        public PMR00222PrintController(ILogger<PMR00220PrintLogger> logger)
        {
            PMR00220PrintLogger.R_InitializeLogger(logger);
            _logger = PMR00220PrintLogger.R_GetInstanceLogger();
            _activitySource = PMR00220Activity.R_InitializeAndGetActivitySource(nameof(PMR00221PrintController));


            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }

        #region reporthelper

        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "PMR00220Detail.frx");
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
        public R_DownloadFileResultDTO DownloadResultPrintPost(PMR00220ParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            _logger.LogInfo("Start - Post DownloadResultPrintPost LOI");
            R_Exception loException = new R_Exception();
            PMR00220PrintLogKey loCache = null;
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new PMR00220PrintLogKey
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY),
                    poReportGlobal = R_ReportGlobalVar.R_GetReportDTO()

                };
                _logger.LogInfo("Set GUID Param - Post DownloadResultPrintPost LOI");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End - Print LOI");
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult LOIEventDetail_ReportListGet(string pcGuid)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            R_Exception loException = new R_Exception();
            FileStreamResult loRtn = null;
            PMR00220PrintLogKey loResultGUID = null;
            try
            {
                // Deserialize the GUID from the cache
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<PMR00220PrintLogKey>(R_DistributedCache.Cache.Get(pcGuid));
                _logger.LogDebug("Deserialized GUID: {pcGuid}", pcGuid);

                // Get Parameter
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                R_ReportGlobalVar.R_SetFromReportDTO(loResultGUID.poReportGlobal);


                _Parameter = loResultGUID.poParam;

                _logger.LogDebug("Deserialized GUID: {pcGuid}", pcGuid);
                _logger.LogDebug("Deserialized Parameters: {@Parameters}", _Parameter);

                R_FileType loFileType = new();
                if (string.IsNullOrWhiteSpace(loResultGUID.poParam.CREPORT_FILEEXT) || string.IsNullOrWhiteSpace(loResultGUID.poParam.CREPORT_FILENAME))
                {
                    loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF), R_ReportUtility.GetMimeType(R_FileType.PDF));
                }
                else
                {
                    loFileType = loResultGUID.poParam.CREPORT_FILEEXT switch
                    {
                        "XLSX" => R_FileType.XLSX,
                        "PDF" => R_FileType.PDF,
                        "XLS" => R_FileType.XLS,
                        "CSV" => R_FileType.CSV,
                        _ => R_FileType.PDF,
                    };

                    //print nama save as
                    loRtn = File(_ReportCls.R_GetStreamReport(peExport: loFileType), R_ReportUtility.GetMimeType(loFileType), $"{loResultGUID.poParam.CREPORT_FILENAME}.{loResultGUID.poParam.CREPORT_FILEEXT}");
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
            _logger.LogInfo("End - LOI List Report Generation");
            return loRtn;
        }

        #region Helper

        private PMR00221PrintDislpayDTO GenerateDataPrint(PMR00220ParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            var loEx = new R_Exception();
            PMR00221PrintDislpayDTO loRtn = new PMR00221PrintDislpayDTO();
            var loParam = new BaseHeaderDTO();

            CultureInfo loCultureInfo = new(R_BackGlobalVar.REPORT_CULTURE);

            try
            {
                _logger.LogInfo("_logger.LogInfo(\"Start - Generating data for Print\");\n data for LOI report.");

                //Add Resources
                loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),
                    "Page", loCultureInfo);
                loRtn.BaseHeaderColumn.Of =
                    R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_Date =
                    R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),
                    "Print_By", loCultureInfo);

                PMR00220LabelDTO loColumnObject = new PMR00220LabelDTO();
                var loColumn = AssignValuesWithMessages(typeof(PMR00220BackResources.Resources_Dummy_Class), loCultureInfo, loColumnObject);

                // Set base header data
                _logger.LogDebug("Deserialized Print Parameters: {@PrintParameters}");

                loParam.CCOMPANY_NAME = R_BackGlobalVar.COMPANY_ID.ToUpper();
                loParam.CPRINT_CODE = "PMR00220";
                loParam.CPRINT_NAME = PMR00220ContextConstant.CPROGRAM_NAME;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID.ToUpper();

                // Create an instance 
                var loCls = new PMR00220Cls();
                loParam.BLOGO_COMPANY = loCls.GetCompanyLogo(R_BackGlobalVar.COMPANY_ID).CLOGO;
                loParam.CCOMPANY_NAME = loCls.GetCompanyName(R_BackGlobalVar.COMPANY_ID).CCOMPANY_NAME;

                // Create an instance 
                PMR00221ReportDataDTO loData = new PMR00221ReportDataDTO()
                {
                    Title = PMR00220ContextConstant.CPROGRAM_NAME,
                    Header = PMR00220ContextConstant.CPROGRAM_NAME,
                    Column = (PMR00220LabelDTO)loColumn,
                    HeaderParam = poParam,
                    Data = new List<TransactionDTO>(),
                };

                // Get print data for Group Of Account report
                var loCollData = loCls.GetDetailReportData(poParam);

                _logger.LogInfo("Data generation successful. Processing data for printing.");

                // Set the generated data in loRtn
                _logger.LogInfo("Data processed successfully. Generating print output.");
                if (loCollData.Any())
                {
                    var loTempData = ConvertResultToFormatPrint(loCollData);
                    loData.Data = ConvertResultToFormatPrint(loCollData);
                }
                else
                {
                    loData.Data = new();
                }
                loRtn.BaseHeaderData = loParam;
                loRtn.DetailData = loData;

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

        private List<TransactionDTO> ConvertResultToFormatPrint(List<PMR00221SPResultDTO> poCollectionDataRaw)
        {
            var loException = new R_Exception();
            string lcMethodName = nameof(ConvertResultToFormatPrint);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START method {0} ", lcMethodName));
            List<TransactionDTO> loReturn = new();
            try
            {
                var loTempData = poCollectionDataRaw
                .GroupBy(Data => new
                {
                    Data.CTRANS_CODE,
                    Data.CTRANS_NAME,
                }).Select(Data1 => new TransactionDTO
                {
                    CTRANS_CODE = Data1.Key.CTRANS_CODE,
                    CTRANS_NAME = Data1.Key.CTRANS_NAME,
                    TransactionDetail = Data1
                   .GroupBy(Data => new
                   {
                       Data.CSALESMAN_ID,
                       Data.CSALESMAN_NAME,
                   }).Select(Data2 => new SalesmanDTO
                   {
                       CSALESMAN_ID = Data2.Key.CSALESMAN_ID,
                       CSALESMAN_NAME = Data2.Key.CSALESMAN_NAME,
                       SalesmanDetail = Data2
                       .GroupBy(Data3 => new
                       {
                           Data3.CREF_NO,
                           Data3.CREF_DATE,
                           Data3.DREF_DATE,
                           Data3.CTENURE,
                           Data3.CTRANS_STATUS_NAME,
                           Data3.NTOTAL_PRICE,
                           Data3.CTENANT_ID,
                           Data3.CTENANT_NAME,
                           Data3.CTC_MESSAGE,
                       }).Select(Data4 => new LoiNoDTO
                       {
                           CREF_NO = Data4.Key.CREF_NO,
                           CREF_DATE = Data4.Key.CREF_DATE,
                           DREF_DATE = DateTime.TryParseExact(Data4.Key.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loresultRefDate) ? loresultRefDate : null,
                           CTENURE = Data4.Key.CTENURE,
                           CTRANS_STATUS_NAME = Data4.Key.CTRANS_STATUS_NAME,
                           NTOTAL_PRICE = Data4.Key.NTOTAL_PRICE,
                           CTENANT_ID = Data4.Key.CTENANT_ID,
                           CTENANT_NAME = Data4.Key.CTENANT_NAME,
                           CTC_MESSAGE = Data4.Key.CTC_MESSAGE,
                           UnitDetail = Data4
                           .GroupBy(Data5 => new
                           {
                               Data5.CUNIT_DETAIL_ID,
                               Data5.CUNIT_DETAIL_NAME,
                               Data5.NUNIT_DETAIL_GROSS_AREA_SIZE,
                               Data5.NUNIT_DETAIL_NET_AREA_SIZE,
                               Data5.NUNIT_DETAIL_COMMON_AREA_SIZE,
                               Data5.NUNIT_DETAIL_PRICE,
                           }).Select(Data6 => new UnitDTO
                           {
                               CUNIT_DETAIL_ID = Data6.Key.CUNIT_DETAIL_ID,
                               CUNIT_DETAIL_NAME = Data6.Key.CUNIT_DETAIL_NAME,
                               NUNIT_DETAIL_GROSS_AREA_SIZE = Data6.Key.NUNIT_DETAIL_GROSS_AREA_SIZE,
                               NUNIT_DETAIL_NET_AREA_SIZE = Data6.Key.NUNIT_DETAIL_NET_AREA_SIZE,
                               NUNIT_DETAIL_COMMON_AREA_SIZE = Data6.Key.NUNIT_DETAIL_COMMON_AREA_SIZE,
                               NUNIT_DETAIL_PRICE = Data6.Key.NUNIT_DETAIL_PRICE,
                           }).ToList(),
                           UtilityDetail = Data4
                           .GroupBy(Data11 => new
                           {
                               Data11.CUTILITY_DETAIL_TYPE_NAME
                           }).Select(Data12 => new UtilityDTO
                           {
                               CUTILITY_DETAIL_TYPE_NAME = Data12.Key.CUTILITY_DETAIL_TYPE_NAME,
                               UtilityTypeDetail = Data12
                             .GroupBy(Data13 => new
                             {
                                 Data13.CUTILITY_DETAIL_UNIT_NAME
                             }).Select(Data14 => new UtilityTypeDTO
                             {
                                 CUTILITY_DETAIL_UNIT_NAME = Data14.Key.CUTILITY_DETAIL_UNIT_NAME,
                                 UtilityTypeUnitDetail = Data14
                                 .GroupBy(Data15 => new
                                 {
                                     Data15.CUTILITY_DETAIL_CHARGE_NAME,
                                     Data15.CUTILITY_DETAIL_TAX_NAME,
                                     Data15.CUTILITY_DETAIL_METER_NO,
                                     Data15.NUTILITY_DETAIL_METER_START,
                                     Data15.CUTILITY_DETAIL_START_INV_PRD,
                                     Data15.NUTILITY_DETAIL_BLOCK1_START,
                                     Data15.NUTILITY_DETAIL_BLOCK2_START,
                                     Data15.CUTILITY_DETAIL_STATUS_NAME
                                 })
                                    .Select(Data16 => new UtilityTypeUnitDTO
                                    {
                                        CUTILITY_DETAIL_CHARGE_NAME = Data16.Key.CUTILITY_DETAIL_CHARGE_NAME,
                                        CUTILITY_DETAIL_TAX_NAME = Data16.Key.CUTILITY_DETAIL_TAX_NAME,
                                        CUTILITY_DETAIL_METER_NO = Data16.Key.CUTILITY_DETAIL_METER_NO,
                                        NUTILITY_DETAIL_METER_START = Data16.Key.NUTILITY_DETAIL_METER_START,
                                        CUTILITY_DETAIL_START_INV_PRD = DateTime.TryParseExact(Data16.Key.CUTILITY_DETAIL_START_INV_PRD, "yyyyMM", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result) ? result.ToString("MMMM yyyy") : "",
                                        NUTILITY_DETAIL_BLOCK1_START = Data16.Key.NUTILITY_DETAIL_BLOCK1_START,
                                        NUTILITY_DETAIL_BLOCK2_START = Data16.Key.NUTILITY_DETAIL_BLOCK2_START,
                                        CUTILITY_DETAIL_STATUS_NAME = Data16.Key.CUTILITY_DETAIL_STATUS_NAME
                                    }).ToList(),
                             }).ToList(),

                           }).ToList(),
                           ChargeDetail = Data4
                           .GroupBy(Data5 => new
                           {
                               Data5.CCHARGE_DETAIL_TYPE_NAME
                           }).Select(Data6 => new ChargeDTO
                           {
                               CCHARGE_DETAIL_TYPE_NAME = Data6.Key.CCHARGE_DETAIL_TYPE_NAME,
                               ChargeTypeDetail = Data6
                               .GroupBy(Data7 => new
                               {
                                   Data7.CCHARGE_DETAIL_UNIT_NAME
                               }).Select(Data8 => new ChargeTypeDTO
                               {
                                   CCHARGE_DETAIL_UNIT_NAME = Data8.Key.CCHARGE_DETAIL_UNIT_NAME,
                                   ChargeTypeUnitDetail = Data8
                                   .GroupBy(Data9 => new
                                   {
                                       Data9.CCHARGE_DETAIL_CHARGE_NAME,
                                       Data9.CCHARGE_DETAIL_TAX_NAME,
                                       Data9.CCHARGE_DETAIL_START_DATE,
                                       Data9.CCHARGE_DETAIL_END_DATE,
                                       Data9.DCHARGE_DETAIL_START_DATE,
                                       Data9.DCHARGE_DETAIL_END_DATE,
                                       Data9.CCHARGE_DETAIL_TENURE,
                                       Data9.CCHARGE_DETAIL_FEE_METHOD,
                                       Data9.NCHARGE_DETAIL_FEE_AMOUNT,
                                       Data9.NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT
                                   })
                                   .Select(Data10 => new ChargeTypeUnitDTO
                                   {
                                       CCHARGE_DETAIL_CHARGE_NAME = Data10.Key.CCHARGE_DETAIL_CHARGE_NAME,
                                       CCHARGE_DETAIL_TAX_NAME = Data10.Key.CCHARGE_DETAIL_TAX_NAME,
                                       CCHARGE_DETAIL_START_DATE = Data10.Key.CCHARGE_DETAIL_START_DATE,
                                       CCHARGE_DETAIL_END_DATE = Data10.Key.CCHARGE_DETAIL_END_DATE,
                                       DCHARGE_DETAIL_START_DATE = DateTime.TryParseExact(Data10.Key.CCHARGE_DETAIL_START_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loResultStartDate) ? loResultStartDate : null,
                                       DCHARGE_DETAIL_END_DATE = DateTime.TryParseExact(Data10.Key.CCHARGE_DETAIL_END_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loResultEndDate) ? loResultEndDate : null!,
                                       CCHARGE_DETAIL_TENURE = Data10.Key.CCHARGE_DETAIL_TENURE,
                                       CCHARGE_DETAIL_FEE_METHOD = Data10.Key.CCHARGE_DETAIL_FEE_METHOD,
                                       NCHARGE_DETAIL_FEE_AMOUNT = Data10.Key.NCHARGE_DETAIL_FEE_AMOUNT,
                                       NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT = Data10.Key.NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT
                                   }).ToList(),
                               }).ToList(),
                           }).ToList(),
                           DepositDetail = Data4
                              .GroupBy(Data5 => new
                              {
                                  Data5.CDEPOSIT_DETAIL_ID,
                                  Data5.CDEPOSIT_DETAIL_DATE,
                                  Data5.DDEPOSIT_DETAIL_DATE,
                                  Data5.NDEPOSIT_DETAIL_AMOUNT,
                                  Data5.CDEPOSIT_DETAIL_DESCRIPTION
                              }).Select(Data6 => new DepositDTO
                              {
                                  CDEPOSIT_DETAIL_ID = Data6.Key.CDEPOSIT_DETAIL_ID,
                                  CDEPOSIT_DETAIL_DATE = Data6.Key.CDEPOSIT_DETAIL_DATE,
                                  DDEPOSIT_DETAIL_DATE = DateTime.TryParseExact(Data6.Key.CDEPOSIT_DETAIL_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loDepoDTDate) ? loDepoDTDate : null,
                                  NDEPOSIT_DETAIL_AMOUNT = Data6.Key.NDEPOSIT_DETAIL_AMOUNT,
                                  CDEPOSIT_DETAIL_DESCRIPTION = Data6.Key.CDEPOSIT_DETAIL_DESCRIPTION
                              }).ToList(),
                           DocumentDetail = Data4
                           .GroupBy(Data17 => new
                           {
                               Data17.CDOCUMENT_DETAIL_NO,
                               Data17.CDOCUMENT_DETAIL_DATE,
                               Data17.DDOCUMENT_DETAIL_DATE,
                               Data17.CDOCUMENT_DETAIL_EXPIRED_DATE,
                               Data17.DDOCUMENT_DETAIL_EXPIRED_DATE,
                               Data17.CDOCUMENT_DETAIL_FILE,
                               Data17.CDOCUMENT_DETAIL_DESCRIPTION
                           }).Select(Data18 => new DocumentDTO
                           {
                               CDOCUMENT_DETAIL_NO = Data18.Key.CDOCUMENT_DETAIL_NO,
                               CDOCUMENT_DETAIL_DATE = Data18.Key.CDOCUMENT_DETAIL_DATE,
                               DDOCUMENT_DETAIL_DATE = DateTime.TryParseExact(Data18.Key.CDOCUMENT_DETAIL_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loDocDtDate) ? loDocDtDate : null,
                               CDOCUMENT_DETAIL_EXPIRED_DATE = Data18.Key.CDOCUMENT_DETAIL_EXPIRED_DATE,
                               DDOCUMENT_DETAIL_EXPIRED_DATE = DateTime.TryParseExact(Data18.Key.CDOCUMENT_DETAIL_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var loDocDtExpDate) ? loDocDtExpDate : null,
                               CDOCUMENT_DETAIL_FILE = Data18.Key.CDOCUMENT_DETAIL_DATE,
                               CDOCUMENT_DETAIL_DESCRIPTION = Data18.Key.CDOCUMENT_DETAIL_DESCRIPTION,
                           }).ToList(),
                       }).ToList(),
                   }).ToList(),
                }).ToList();

                loReturn = loTempData;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);

            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return loReturn;

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
