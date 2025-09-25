using Microsoft.Extensions.Logging;
using PMR00150BACK;
using PMR00150COMMON.Utility_Report;
using PMR00150COMMON;
using R_ReportFastReportBack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_BackEnd;
using System.Collections;
using BaseHeaderReportCOMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMR00150SERVICE.DTOLogs;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI.Log;
using R_CommonFrontBackAPI;
using System.Globalization;
using PMR00150COMMON.DTO_Report_Detail.Detail;
using PMR00150COMMON.DTO_Report_Detail;

namespace PMR00150SERVICE
{
    public class PMR00150DetailReportController : R_ReportControllerBase
    {
        #region instantiate

        private R_ReportFastReportBackClass _ReportCls;
        private PMR00150DBParamDTO _Parameter;
        private LoggerPMR00150 _logger;
        private readonly ActivitySource _activitySource;


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public PMR00150DetailReportController(ILogger<PMR00150DetailReportController> logger)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            //Initial and Get instance
            LoggerPMR00150.R_InitializeLogger(logger);
            _logger = LoggerPMR00150.R_GetInstanceLogger();
            _activitySource = PMR00150Activity.R_InitializeAndGetActivitySource(nameof(PMR00150DetailReportController));

            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }

        #endregion
        #region Event Handler

        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "PMR00150Detail.frx");
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
        #region Method Get Post
        [HttpPost]
        public R_DownloadFileResultDTO DetailReportPost(PMR00150DBParamDTO poParameter)
        {
            string lcMethodName = nameof(DetailReportPost);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START method {0} ", lcMethodName));

            PMR00150ReportLogKeyDTO<PMR00150DBParamDTO> loCache = null;
            R_Exception loException = new R_Exception();
            R_DownloadFileResultDTO? loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new PMR00150ReportLogKeyDTO<PMR00150DBParamDTO>
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY),
                    poReportGlobalDTO = R_ReportGlobalVar.R_GetReportDTO()
                };

                // Set Guid Param 
                _logger.LogInfo("Set GUID Param on method post");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END method {0}", lcMethodName));

            return loRtn!;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult DetailReportGet(string pcGuid)
        {
            string lcMethodName = nameof(DetailReportGet);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START method {0}", lcMethodName));

            PMR00150ReportLogKeyDTO<PMR00150DBParamDTO> loDataFromRedis = null;
            R_Exception loException = new R_Exception();
            FileStreamResult? loRtn = null;
            try
            {
                //Get Parameter
                loDataFromRedis = R_NetCoreUtility.R_DeserializeObjectFromByte<PMR00150ReportLogKeyDTO<PMR00150DBParamDTO>>(R_DistributedCache.Cache.Get(pcGuid));

#pragma warning disable CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loDataFromRedis.poLogKey);
#pragma warning restore CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.
                R_ReportGlobalVar.R_SetFromReportDTO(loDataFromRedis.poReportGlobalDTO); 
                _Parameter = loDataFromRedis.poParam!;


                _logger.LogInfo(string.Format("READ file report method {0}", lcMethodName));
                R_FileType loFileType = new();
                if (loDataFromRedis.poParam.LIS_PRINT)
                {
                    _logger.LogInfo(string.Format("Normal Print"));
                    loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF), R_ReportUtility.GetMimeType(R_FileType.PDF));

                }
                else
                {
                    _logger.LogInfo(string.Format("Save As"));
                    switch (loDataFromRedis.poParam.CREPORT_FILETYPE)
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
                    loRtn = File(_ReportCls.R_GetStreamReport(peExport: loFileType), R_ReportUtility.GetMimeType(loFileType), $"{loDataFromRedis.poParam.CREPORT_FILENAME}.{loDataFromRedis.poParam.CREPORT_FILETYPE}");

                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END method {0}", lcMethodName));

            return loRtn!;
        }
        #endregion

        #region Helper
        private PMR00150DetailResultWithHeaderDTO GenerateDataPrint(PMR00150DBParamDTO poParam)
        {
            string lcMethodName = nameof(GenerateDataPrint);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START method {0}", lcMethodName));

            var loException = new R_Exception();
            PMR00150DetailResultWithHeaderDTO loRtn = new PMR00150DetailResultWithHeaderDTO();
            PMR00150Cls? loCls = null;
            PMR00150DetailResultDTO? loData = null;
            CultureInfo loCultureInfo = new CultureInfo(R_BackGlobalVar.REPORT_CULTURE);
            try
            {
                //Add Resources
                loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Page", loCultureInfo);
                loRtn.BaseHeaderColumn.Of = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_Date = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_By", loCultureInfo);

                loCls = new PMR00150Cls();
                //poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                //poParam.CLANG_ID = R_BackGlobalVar.CULTURE;

                var loCollectionFromDb = loCls.GetLOCStatusDetailReportData(poParam);

                _logger.LogInfo("Set BaseHeader Report");
                var loBaseHeader = loCls.GetBaseHeaderLogoCompany(poParam);
                var loParamHeader = new BaseHeaderDTO()
                {
                    CCOMPANY_NAME = loBaseHeader.CCOMPANY_NAME!,
                    CPRINT_CODE = R_BackGlobalVar.PROGRAM_ID.ToUpper(),
                    CPRINT_NAME = "LOC List",
                    CUSER_ID = !string.IsNullOrEmpty(poParam.CUSER_ID) ? poParam.CUSER_ID : "",
                    BLOGO_COMPANY = loBaseHeader.CLOGO!,
                    DPRINT_DATE_COMPANY = DateTime.ParseExact(loBaseHeader.CDATETIME_NOW, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture)
            };

                _logger.LogInfo("Set Header Report");
                var loHeader = new PMR00150DataHeaderDTO()
                {
                    PROPERTY = poParam.CPROPERTY_NAME,
                    FROM_DEPARTMENT = poParam.CFROM_DEPARTMENT_NAME + $"({poParam.CFROM_DEPARTMENT_ID})",
                    TO_DEPARTMENT = poParam.CTO_DEPARTMENT_NAME + $"({poParam.CTO_DEPARTMENT_ID})",
                    FROM_SALESMEN = poParam.CFROM_SALESMAN_NAME + $"({poParam.CFROM_SALESMAN_ID})",
                    TO_SALESMEN = poParam.CTO_SALESMAN_NAME + $"({poParam.CTO_SALESMAN_ID})",
                    FROM_PERIOD = poParam.CFROM_PERIOD_NAME,
                    TO_PERIOD = poParam.CTO_PERIOD_NAME,
                    CREPORT_NAME = poParam.CREPORT_NAME
                };
                var loColumn = AssignValuesWithMessages(typeof(PMR00150BackResources.Resources_PMR00150), loCultureInfo, new PMR00150ColumnDetailDTO());
                var loLabel = AssignValuesWithMessages(typeof(PMR00150BackResources.Resources_PMR00150), loCultureInfo, new PMR00150LabelDTO());

                _logger.LogInfo("Convert data  to Format Print");

                //ASSIGN VALUE
                loData = new PMR00150DetailResultDTO();
                loData.Title = "LOC List";
                loData.Header = loHeader;
                loData.ColumnDetail = (PMR00150ColumnDetailDTO)loColumn;
                loData.Label = (PMR00150LabelDTO)loLabel;

                //CONVERT DATA TO DISPLAY IF DATA EXIST
                //
                if (loCollectionFromDb.Any())
                {
                    var loTempData = ConvertResultToFormatPrint(loCollectionFromDb);
                    loData.Data = loTempData;
                }
                else
                {
                    loData.Data = new List<PMR00150DataDetailTransactionDTO>();
                }

                _logger.LogInfo("Set Data Report");
                loRtn.BaseHeaderData = loParamHeader;
                loRtn.PMR00150DetailResultDataFormatDTO = loData;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("END Method GenerateDataPrint on Controller");

            return loRtn;
        }
        #endregion

        #region Utilities
        private static DateTime? ConvertStringToDateTimeFormat(string? pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                return null;
            }
            else
            {
                DateTime result;
                if (pcEntity.Length == 6)
                {
                    pcEntity += "01";
                }

                if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }
        //Helper Assign Object
        private object AssignValuesWithMessages(Type poResourceType, CultureInfo poCultureInfo, object poObject)
        {
            object loObj = Activator.CreateInstance(poObject.GetType())!;
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
        private List<PMR00150DataDetailTransactionDTO> ConvertResultToFormatPrint(List<PMR00150DataDetailDbDTO> poCollectionDataRaw)
        {
            var loException = new R_Exception();
            string lcMethodName = nameof(ConvertResultToFormatPrint);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START method {0} ", lcMethodName));
            List<PMR00150DataDetailTransactionDTO> loReturn = new List<PMR00150DataDetailTransactionDTO>();

            try
            {

                var loTempData = poCollectionDataRaw
.GroupBy(Data => new
{
    Data.CTRANS_CODE,
    Data.CTRANS_NAME,
}).Select(Data1 => new PMR00150DataDetailTransactionDTO
{
    CTRANS_CODE = Data1.Key.CTRANS_CODE,
    CTRANS_NAME = Data1.Key.CTRANS_NAME,
    TransactionDetail = Data1
   .GroupBy(Data => new
   {
       Data.CSALESMAN_ID,
       Data.CSALESMAN_NAME,
   }).Select(Data2 => new PMR00150DataDetailSalesmanDTO
   {
       CSALESMAN_ID = Data2.Key.CSALESMAN_ID,
       CSALESMAN_NAME = Data2.Key.CSALESMAN_NAME,
       SalesmanDetail = Data2
       .GroupBy(Data3 => new
       {
           Data3.CREF_NO,
           Data3.CREF_DATE,
           Data3.CTENURE,
           Data3.CAGREEMENT_STATUS_NAME,
           Data3.CTRANS_STATUS_NAME,
           Data3.NTOTAL_PRICE,
           Data3.CTAX,
           Data3.CTENANT_ID,
           Data3.CTENANT_NAME,
           Data3.CTC_MESSAGE,
       }).Select(Data4 => new PMR00150DataDetailLocNoDTO
       {
           CREF_NO = Data4.Key.CREF_NO,
           CREF_DATE = Data4.Key.CREF_DATE,
           DREF_DATE = ConvertStringToDateTimeFormat(Data4.Key.CREF_DATE),
           CTENURE = Data4.Key.CTENURE,
           CAGREEMENT_STATUS_NAME = Data4.Key.CAGREEMENT_STATUS_NAME,
           CTRANS_STATUS_NAME = Data4.Key.CTRANS_STATUS_NAME,
           NTOTAL_PRICE = Data4.Key.NTOTAL_PRICE,
           CTAX = Data4.Key.CTAX,
           CTENANT_ID = Data4.Key.CTENANT_ID,
           CTENANT_NAME = Data4.Key.CTENANT_NAME,
           CTC_MESSAGE = Data4.Key.CTC_MESSAGE,

           UnitDetail = Data4
           .GroupBy(Data5 => new
           {
               Data5.CUNIT_DETAIL_ID,
               Data5.NUNIT_DETAIL_GROSS_AREA_SIZE,
               Data5.NUNIT_DETAIL_NET_AREA_SIZE,
               Data5.NUNIT_DETAIL_COMMON_AREA_SIZE,
               Data5.NUNIT_DETAIL_PRICE,
               Data5.NUNIT_TOTAL_GROSS_AREA_SIZE,
               Data5.NUNIT_TOTAL_NET_AREA_SIZE,
               Data5.NUNIT_TOTAL_COMMON_AREA_SIZE,
               Data5.NUNIT_TOTAL_PRICE
           }).Select(Data6 => new PMR00150DataDetailUnitDTO
           {
               CUNIT_DETAIL_ID = Data6.Key.CUNIT_DETAIL_ID,
               NUNIT_DETAIL_GROSS_AREA_SIZE = Data6.Key.NUNIT_DETAIL_GROSS_AREA_SIZE,
               NUNIT_DETAIL_NET_AREA_SIZE = Data6.Key.NUNIT_DETAIL_NET_AREA_SIZE,
               NUNIT_DETAIL_COMMON_AREA_SIZE = Data6.Key.NUNIT_DETAIL_COMMON_AREA_SIZE,
               NUNIT_DETAIL_PRICE = Data6.Key.NUNIT_DETAIL_PRICE,
               NUNIT_TOTAL_GROSS_AREA_SIZE = Data6.Key.NUNIT_TOTAL_GROSS_AREA_SIZE,
               NUNIT_TOTAL_NET_AREA_SIZE = Data6.Key.NUNIT_TOTAL_NET_AREA_SIZE,
               NUNIT_TOTAL_COMMON_AREA_SIZE = Data6.Key.NUNIT_TOTAL_COMMON_AREA_SIZE,
               NUNIT_TOTAL_PRICE = Data6.Key.NUNIT_TOTAL_PRICE
           }).ToList(),

           ChargeDetail =
           Data4.GroupBy(data => new
           {
               data.CCHARGE_DETAIL_TYPE_NAME
           }) // Group by charge type
                .Select(group1 => new PMR00150DataDetailChargeDTO
                {
                    CCHARGE_DETAIL_TYPE_NAME = group1.Key.CCHARGE_DETAIL_TYPE_NAME,
                    ChargeTypeDetail = group1
                        .GroupBy(data => new { data.CCHARGE_DETAIL_UNIT_NAME }) // Group by charge unit

                        .Select(selectChargeTypeUnit => new PMR00150DataDetailChargeTypeDTO
                        {
                            CCHARGE_DETAIL_UNIT_NAME = selectChargeTypeUnit.Key.CCHARGE_DETAIL_UNIT_NAME,
                            ChargeTypeUnitDetail = selectChargeTypeUnit
                            .GroupBy(Data => new
                            {
                                Data.CCHARGE_DETAIL_CHARGE_NAME,
                                Data.CCHARGE_DETAIL_TAX_NAME,
                                Data.CCHARGE_DETAIL_START_DATE,
                                Data.CCHARGE_DETAIL_END_DATE,
                                Data.CCHARGE_DETAIL_TENURE,
                                Data.CCHARGE_DETAIL_FEE_METHOD,
                                Data.NCHARGE_DETAIL_FEE_AMOUNT,
                            })
                            .Select(DetailChargeTypeUnitDTO => new PMR00150DataDetailChargeTypeUnitDTO
                            {
                                CCHARGE_DETAIL_CHARGE_NAME = DetailChargeTypeUnitDTO.Key.CCHARGE_DETAIL_CHARGE_NAME,
                                CCHARGE_DETAIL_TAX_NAME = DetailChargeTypeUnitDTO.Key.CCHARGE_DETAIL_TAX_NAME,
                                CCHARGE_DETAIL_START_DATE = DetailChargeTypeUnitDTO.Key.CCHARGE_DETAIL_START_DATE,
                                CCHARGE_DETAIL_END_DATE = DetailChargeTypeUnitDTO.Key.CCHARGE_DETAIL_END_DATE,
                                DCHARGE_DETAIL_START_DATE = ConvertStringToDateTimeFormat(DetailChargeTypeUnitDTO.Key.CCHARGE_DETAIL_START_DATE),
                                DCHARGE_DETAIL_END_DATE = ConvertStringToDateTimeFormat(DetailChargeTypeUnitDTO.Key.CCHARGE_DETAIL_END_DATE),
                                CCHARGE_DETAIL_TENURE = DetailChargeTypeUnitDTO.Key.CCHARGE_DETAIL_TENURE,
                                CCHARGE_DETAIL_FEE_METHOD = DetailChargeTypeUnitDTO.Key.CCHARGE_DETAIL_FEE_METHOD,
                                NCHARGE_DETAIL_FEE_AMOUNT = DetailChargeTypeUnitDTO.Key.NCHARGE_DETAIL_FEE_AMOUNT
                            }).ToList()

                        }).ToList()
                }).ToList(),


           /*
           ChargeDetail = Data4
           .GroupBy(Data5 => new
           {
               Data5.CCHARGE_DETAIL_TYPE_NAME
           }).Select(Data6 => new PMR00150DataDetailChargeDTO
           {
               CCHARGE_DETAIL_TYPE_NAME = Data6.Key.CCHARGE_DETAIL_TYPE_NAME,
               ChargeTypeDetail = Data6
               .GroupBy(Data7 => new
               {
                   Data7.CCHARGE_DETAIL_UNIT_NAME
               }).Select(Data8 => new PMR00150DataDetailChargeTypeDTO
               {
                   CCHARGE_DETAIL_UNIT_NAME = Data8.Key.CCHARGE_DETAIL_UNIT_NAME,
                   ChargeTypeUnitDetail = Data8
                   .GroupBy(Data9 => new
                   {
                       Data9.CCHARGE_DETAIL_CHARGE_NAME,
                       Data9.CCHARGE_DETAIL_TAX_NAME,
                       Data9.CCHARGE_DETAIL_START_DATE,
                       Data9.CCHARGE_DETAIL_END_DATE,
                       Data9.CCHARGE_DETAIL_TENURE,
                       Data9.CCHARGE_DETAIL_FEE_METHOD,
                       Data9.NCHARGE_DETAIL_FEE_AMOUNT,
                       Data9.NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT
                   })
                   .Select(Data10 => new PMR00150DataDetailChargeTypeUnitDTO
                   {
                       CCHARGE_DETAIL_CHARGE_NAME = Data10.Key.CCHARGE_DETAIL_CHARGE_NAME,
                       CCHARGE_DETAIL_TAX_NAME = Data10.Key.CCHARGE_DETAIL_TAX_NAME,
                       CCHARGE_DETAIL_START_DATE = Data10.Key.CCHARGE_DETAIL_START_DATE,
                       CCHARGE_DETAIL_END_DATE = Data10.Key.CCHARGE_DETAIL_END_DATE,
                       DCHARGE_DETAIL_START_DATE = ConvertStringToDateTimeFormat(Data10.Key.CCHARGE_DETAIL_START_DATE)!,
                       DCHARGE_DETAIL_END_DATE = ConvertStringToDateTimeFormat(Data10.Key.CCHARGE_DETAIL_END_DATE)!,
                       CCHARGE_DETAIL_TENURE = Data10.Key.CCHARGE_DETAIL_TENURE,
                       CCHARGE_DETAIL_FEE_METHOD = Data10.Key.CCHARGE_DETAIL_FEE_METHOD,
                       NCHARGE_DETAIL_FEE_AMOUNT = Data10.Key.NCHARGE_DETAIL_FEE_AMOUNT,
                       NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT = Data10.Key.NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT
                   }).ToList(),
               }).ToList(),
           }).ToList(),

           */
           DepositDetail = Data4
              .GroupBy(Data5 => new
              {
                  Data5.CDEPOSIT_DETAIL_ID,
                  Data5.CDEPOSIT_DETAIL_DATE,
                  Data5.NDEPOSIT_DETAIL_AMOUNT,
                  Data5.CDEPOSIT_DETAIL_DESCRIPTION
              }).Select(Data6 => new PMR00150DataDetailDepositDTO
              {
                  CDEPOSIT_DETAIL_ID = Data6.Key.CDEPOSIT_DETAIL_ID,
                  CDEPOSIT_DETAIL_DATE = Data6.Key.CDEPOSIT_DETAIL_DATE,
                  DDEPOSIT_DETAIL_DATE = ConvertStringToDateTimeFormat(Data6.Key.CDEPOSIT_DETAIL_DATE),
                  NDEPOSIT_DETAIL_AMOUNT = Data6.Key.NDEPOSIT_DETAIL_AMOUNT,
                  CDEPOSIT_DETAIL_DESCRIPTION = Data6.Key.CDEPOSIT_DETAIL_DESCRIPTION
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

    }
}
