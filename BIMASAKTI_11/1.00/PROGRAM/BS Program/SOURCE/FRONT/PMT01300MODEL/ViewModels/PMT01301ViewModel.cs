using PMT01300COMMON;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PMT01300MODEL
{
    public class PMT01301ViewModel : R_IProcessProgressStatus
    {

        #region Property Class
        public PMT01300PropertyDTO Property { get; set; } = new PMT01300PropertyDTO();
        public ObservableCollection<PMT01300UploadAgreementExcelDTO> AgreementGrid { get; set; } = new  ObservableCollection<PMT01300UploadAgreementExcelDTO>();
        public ObservableCollection<PMT01300UploadUnitExcelDTO> UnitGrid { get; set; } = new  ObservableCollection<PMT01300UploadUnitExcelDTO>();
        public ObservableCollection<PMT01300UploadUtilityExcelDTO> UtilityGrid { get; set; } = new  ObservableCollection<PMT01300UploadUtilityExcelDTO>();
        public ObservableCollection<PMT01300UploadChargesExcelDTO> ChargesGrid { get; set; } = new  ObservableCollection<PMT01300UploadChargesExcelDTO>();
        public ObservableCollection<PMT01300UploadDepositExcelDTO> DepositGrid { get; set; } = new  ObservableCollection<PMT01300UploadDepositExcelDTO>();
        #endregion

        #region Public Upload ViewModel
        // Action StateHasChanged
        public Action StateChangeAction { get; set; }
        // Action Get Error
        public Action<R_APIException> ShowErrorAction { get; set; }
        // Action Get DataSet
        public Func<Task> ActionDataSetExcel { get; set; }
        // Func Proses is Success
        public Func<Task> ActionIsCompleteSuccess { get; set; }
        // DataSet Excel 
        public DataSet ExcelDataSet { get; set; }
        public string SourceFileName { get; set; } = "";
        public string Message { get; set; } = "";
        public int Percentage { get; set; } = 0;
        public bool BtnSave { get; set; } = true;
        public bool VisibleError { get; set; } = false;
        public string CompanyID { get; set; }
        public string UserId { get; set; }
        public List<R_BlazorFrontEnd.Exceptions.R_Error> ErrorList { get; set; } = new List<R_BlazorFrontEnd.Exceptions.R_Error>();
        #endregion

        #region Public Summary Upload
        public int SumDataAgreementExcel { get; set; }
        public int SumDataUnitExcel { get; set; }
        public int SumDataUtilityExcel { get; set; }
        public int SumDataChargesExcel { get; set; }
        public int SumDataDepositExcel { get; set; }
        public int SumValidDataAgreementExcel { get; set; }
        public int SumInvalidDataAgreementExcel { get; set; }
        public int SumValidDataUnitExcel { get; set; }
        public int SumInvalidDataUnitExcel { get; set; }
        public int SumValidDataUtilityExcel { get; set; }
        public int SumInvalidDataUtilityExcel { get; set; }
        public int SumValidDataChargesExcel { get; set; }
        public int SumInvalidDataChargesExcel { get; set; }
        public int SumValidDataDepositExcel { get; set; }
        public int SumInvalidDataDepositExcel { get; set; }
        public string lcFilterResult { get; set; } = "";
        #endregion

        #region Get List Convert
        public async Task GetAgreementList(List<PMT01300UploadAgreementExcelDTO> poEntity)
        {
            var loEx = new R_Exception();

            try
            {

                // Convert Excel DTO and add SeqNo
                var loData = await Task.WhenAll(poEntity.Select(async (loTemp, i) =>
                {
                    PMT01300UploadAgreementExcelDTO loData = new PMT01300UploadAgreementExcelDTO
                    {
                        NO = i + 1,
                        Building = loTemp.Building,
                        Department = loTemp.Department,
                        AgreementNo = loTemp.AgreementNo,
                        AgreementDate = loTemp.AgreementDate,
                        DocumentNo = loTemp.DocumentNo,
                        DocumentDate = loTemp.DocumentDate,
                        StartDate = loTemp.StartDate,
                        EndDate = loTemp.EndDate,
                        Year = loTemp.Year,
                        Month = loTemp.Month,
                        Day = loTemp.Day,
                        Currency = loTemp.Currency,
                        LeaseMode = loTemp.LeaseMode,
                        ChargeMode = loTemp.ChargeMode,
                        Salesman = loTemp.Salesman,
                        Tenant = loTemp.Tenant,
                        UnitDescription = loTemp.UnitDescription,
                        PlanHODate = loTemp.PlanHODate,
                        Notes = loTemp.Notes,
                        Valid = "",
                        BillingRule = loTemp.BillingRule,
                        BookingFee = loTemp.BookingFee,
                        TCCode = loTemp.TCCode,
                        ActualHODate = loTemp.ActualHODate,
                        NotesError = ""
                    };

                    if (DateTime.TryParseExact(loData.AgreementDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldAgreementDate))
                    {
                        loData.AgreementDateDisplay = ldAgreementDate;
                    }
                    else
                    {
                        loData.AgreementDateDisplay = null;
                    }
                    if (DateTime.TryParseExact(loData.DocumentDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldDocumentDate))
                    {
                        loData.DocumentDateDisplay = ldDocumentDate;
                    }
                    else
                    {
                        loData.DocumentDateDisplay = null;
                    }
                    if (DateTime.TryParseExact(loData.StartDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldStartDate))
                    {
                        loData.StartDateDisplay = ldStartDate;
                    }
                    else
                    {
                        loData.StartDateDisplay = null;
                    }
                    if (DateTime.TryParseExact(loData.EndDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldEndDate))
                    {
                        loData.EndDateDisplay = ldEndDate;
                    }
                    else
                    {
                        loData.EndDateDisplay = null;
                    }
                    if (DateTime.TryParseExact(loData.PlanHODate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldPlanHODate))
                    {
                        loData.PlanHODateDisplay = ldPlanHODate;
                    }
                    else
                    {
                        loData.PlanHODateDisplay = null;
                    }
                    if (DateTime.TryParseExact(loData.ActualHODate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldActualHODate))
                    {
                        loData.ActualHODateDisplay = ldActualHODate;
                    }
                    else
                    {
                        loData.ActualHODateDisplay = null;
                    }
                    return loData;
                }
                ));

                SumDataAgreementExcel = loData.Count();
                AgreementGrid = new ObservableCollection<PMT01300UploadAgreementExcelDTO>(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetUnitList(List<PMT01300UploadUnitExcelDTO> poEntity, int liCountGrid)
        {
            var loEx = new R_Exception();

            try
            {
                // Convert Excel DTO and add SeqNo
                var loData = await Task.WhenAll(poEntity.Select(async (loTemp, i) =>
                {
                    PMT01300UploadUnitExcelDTO loData = new PMT01300UploadUnitExcelDTO
                    {
                        SEQ_ERROR = i + 1 + liCountGrid,
                        NO = i + 1,
                        BuildingId = loTemp.BuildingId,
                        FloorId = loTemp.FloorId,
                        UnitId = loTemp.UnitId,
                        DocumentNo = loTemp.DocumentNo,
                        Notes = "",
                        Valid = ""
                    };

                    return loData;
                }
                ));

                SumDataUnitExcel = loData.Count();
                UnitGrid = new ObservableCollection<PMT01300UploadUnitExcelDTO>(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetUtilityList(List<PMT01300UploadUtilityExcelDTO> poEntity, int liCountGrid)
        {
            var loEx = new R_Exception();

            try
            {
                // Convert Excel DTO and add SeqNo
                var loData = await Task.WhenAll(poEntity.Select(async (loTemp, i) =>
                {
                    PMT01300UploadUtilityExcelDTO loData = new PMT01300UploadUtilityExcelDTO
                    {
                        SEQ_ERROR = i + 1 + liCountGrid,
                        NO = i + 1,
                        UnitId = loTemp.UnitId,
                        MeterNo = loTemp.MeterNo,
                        ChargesId = loTemp.ChargesId,
                        TaxId = loTemp.TaxId,
                        UtilityType = loTemp.UtilityType,
                        DocumentNo = loTemp.DocumentNo,
                        MeterStart = loTemp.MeterStart,
                        Block1Start = loTemp.Block1Start,
                        Block2Start = loTemp.Block2Start,
                        Notes = "",
                        Valid = ""
                    };

                    return loData;
                }
                ));

                SumDataUtilityExcel = loData.Count();
                UtilityGrid = new ObservableCollection<PMT01300UploadUtilityExcelDTO>(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetChargesList(List<PMT01300UploadChargesExcelDTO> poEntity, int liCountGrid)
        {
            var loEx = new R_Exception();

            try
            {
                // Convert Excel DTO and add SeqNo
                var loData = await Task.WhenAll(poEntity.Select(async (loTemp, i) =>
                {
                    PMT01300UploadChargesExcelDTO loData = new PMT01300UploadChargesExcelDTO
                    {
                        SEQ_ERROR = i + 1 + liCountGrid,
                        NO = i + 1,
                        ChargesId = loTemp.ChargesId,
                        TaxId = loTemp.TaxId,
                        TenureYear = loTemp.TenureYear,
                        TenureMonth = loTemp.TenureMonth,
                        TenureDays = loTemp.TenureDays,
                        BaseonOpeningDate = loTemp.BaseonOpeningDate,
                        BaseonOpeningDateDisplay = loTemp.BaseonOpeningDate == "0" ? false : true,
                        StartDate = loTemp.StartDate,
                        EndDate = loTemp.EndDate,
                        BillingMode = loTemp.BillingMode,
                        Currency = loTemp.Currency,
                        FeeMethod = loTemp.FeeMethod,
                        FeeAmount = loTemp.FeeAmount,
                        PeriodMode = loTemp.PeriodMode,
                        Prorate = loTemp.Prorate,
                        ProrateDisplay = loTemp.Prorate == "0" ? false : true,
                        Description = loTemp.Description,
                        DocumentNo = loTemp.DocumentNo,
                        FloorId = loTemp.FloorId,
                        UnitId = loTemp.UnitId,
                        Notes = "",
                        Valid = ""
                    };

                    if (DateTime.TryParseExact(loData.StartDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldStartDate))
                    {
                        loData.StartDateDisplay = ldStartDate;
                    }
                    else
                    {
                        loData.StartDateDisplay = null;
                    }
                    if (DateTime.TryParseExact(loData.EndDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldEndDate))
                    {
                        loData.EndDateDisplay = ldEndDate;
                    }
                    else
                    {
                        loData.EndDateDisplay = null;
                    }

                    return loData;
                }
                ));

                SumDataChargesExcel = loData.Count();
                ChargesGrid = new ObservableCollection<PMT01300UploadChargesExcelDTO>(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetDepositList(List<PMT01300UploadDepositExcelDTO> poEntity, int liCountGrid)
        {
            var loEx = new R_Exception();

            try
            {
                // Convert Excel DTO and add SeqNo
                var loData = await Task.WhenAll(poEntity.Select(async (loTemp, i) =>
                {
                    PMT01300UploadDepositExcelDTO loData = new PMT01300UploadDepositExcelDTO
                    {
                        SEQ_ERROR = i + 1 + liCountGrid,
                        NO = i + 1,
                        FlagContractor = loTemp.FlagContractor,
                        FlagContractorDisplay = loTemp.FlagContractor == "0" ? false : true,
                        ContractorId = loTemp.ContractorId,
                        DepositId = loTemp.DepositId,
                        DepositDate = loTemp.DepositDate,
                        Currency = loTemp.Currency,
                        DepositAmount = loTemp.DepositAmount,
                        FlagPaid = loTemp.FlagPaid,
                        FlagPaidDisplay = loTemp.FlagPaid == "0" ? false : true,
                        Description = loTemp.Description,
                        DocumentNo = loTemp.DocumentNo,
                        Notes = "",
                        Valid = ""
                    };

                    if (DateTime.TryParseExact(loData.DepositDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldDepositDate))
                    {
                        loData.DepositDateDisplay = ldDepositDate;
                    }
                    else
                    {
                        loData.DepositDateDisplay = null;
                    }

                    return loData;
                }
                ));

                SumDataDepositExcel = loData.Count();
                DepositGrid = new ObservableCollection<PMT01300UploadDepositExcelDTO>(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Validate 
        public void CheckingDataWithEmptyDocNo()
        {
            R_Exception loEx = new R_Exception();
            List<PMT01300UploadAgreementExcelDTO>? loAgreementFilterResult = null;
            List<PMT01300UploadUnitExcelDTO>? loUnitFilterResult = null;
            List<PMT01300UploadUtilityExcelDTO>? loUtilityFilterResult = null;
            List<PMT01300UploadChargesExcelDTO>? loChargesFilterResult = null;
            List<PMT01300UploadDepositExcelDTO>? loDepositFilterResult = null;
            try
            {
                if (AgreementGrid.Count() > 0)
                {
                    loAgreementFilterResult = AgreementGrid.Where(item => string.IsNullOrWhiteSpace(item.DocumentNo)).ToList();
                    if (loAgreementFilterResult.Count() > 0)
                    {
                        lcFilterResult = lcFilterResult + "Agreement : ";
                        foreach (PMT01300UploadAgreementExcelDTO item in loAgreementFilterResult)
                        {
                            lcFilterResult = lcFilterResult + item.NO.ToString() + ", ";
                        }
                        lcFilterResult = lcFilterResult.Substring(0, lcFilterResult.Length - 2);
                        lcFilterResult = lcFilterResult + ". ";
                    }
                }
                if (UnitGrid.Count() > 0)
                {
                    loUnitFilterResult = UnitGrid.Where(item => string.IsNullOrWhiteSpace(item.DocumentNo)).ToList();
                    if (loUnitFilterResult.Count() > 0)
                    {
                        lcFilterResult = lcFilterResult + "Unit : ";
                        foreach (PMT01300UploadUnitExcelDTO item in loUnitFilterResult)
                        {
                            lcFilterResult = lcFilterResult + item.NO.ToString() + ", ";
                        }
                        lcFilterResult = lcFilterResult.Substring(0, lcFilterResult.Length - 2);
                        lcFilterResult = lcFilterResult + ". ";
                    }
                }
                if (UtilityGrid.Count() > 0)
                {
                    loUtilityFilterResult = UtilityGrid.Where(item => string.IsNullOrWhiteSpace(item.DocumentNo)).ToList();
                    if (loUtilityFilterResult.Count() > 0)
                    {
                        lcFilterResult = lcFilterResult + "Utility : ";
                        foreach (PMT01300UploadUtilityExcelDTO item in loUtilityFilterResult)
                        {
                            lcFilterResult = lcFilterResult + item.NO.ToString() + ", ";
                        }
                        lcFilterResult = lcFilterResult.Substring(0, lcFilterResult.Length - 2);
                        lcFilterResult = lcFilterResult + ". ";
                    }
                }
                if (ChargesGrid.Count() > 0)
                {
                    loChargesFilterResult = ChargesGrid.Where(item => string.IsNullOrWhiteSpace(item.DocumentNo)).ToList();
                    if (loChargesFilterResult.Count() > 0)
                    {
                        lcFilterResult = lcFilterResult + "Charges : ";
                        foreach (PMT01300UploadChargesExcelDTO item in loChargesFilterResult)
                        {
                            lcFilterResult = lcFilterResult + item.NO.ToString() + ", ";
                        }
                        lcFilterResult = lcFilterResult.Substring(0, lcFilterResult.Length - 2);
                        lcFilterResult = lcFilterResult + ". ";
                    }
                }
                if (DepositGrid.Count() > 0)
                {
                    loDepositFilterResult = DepositGrid.Where(item => string.IsNullOrWhiteSpace(item.DocumentNo)).ToList();
                    if (loDepositFilterResult.Count() > 0)
                    {
                        lcFilterResult = lcFilterResult + "Deposit : ";
                        foreach (PMT01300UploadDepositExcelDTO item in loDepositFilterResult)
                        {
                            lcFilterResult = lcFilterResult + item.NO.ToString() + ", ";
                        }
                        lcFilterResult = lcFilterResult.Substring(0, lcFilterResult.Length - 2);
                        lcFilterResult = lcFilterResult + ". ";
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        public async Task SaveBulkFile()
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            PMT01300UploadBigObjectParameterDTO loBigObject;
            List<R_KeyValue> loUserParameneters;

            try
            {
                loUserParameneters = new List<R_KeyValue>();
                loUserParameneters.Add(new R_KeyValue() { Key = ContextConstant.CPROPERTY_ID, Value = Property.CPROPERTY_ID! });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM",
                    poProcessProgressStatus: this);

                loBigObject = new PMT01300UploadBigObjectParameterDTO();

                loBigObject.AgreementList = new List<PMT01300UploadAgreementDTO>();
                loBigObject.UnitList = new List<PMT01300UploadUnitDTO>();
                loBigObject.UtilityList = new List<PMT01300UploadUtilitiesDTO>();
                loBigObject.ChargesList = new List<PMT01300UploadChargesDTO>();
                loBigObject.DepositList = new List<PMT01300UploadDepositDTO>();

                //Check Data
                if (AgreementGrid.Count > 0)
                {
                    loBigObject.AgreementList = AgreementGrid
                        .Select(x => new PMT01300UploadAgreementDTO
                        {
                            NO = x.NO,
                            CCOMPANY_ID = CompanyID,
                            CPROPERTY_ID = Property.CPROPERTY_ID,
                            CDEPT_CODE = x.Department,
                            CREF_NO = x.AgreementNo,
                            CREF_DATE = x.AgreementDate,
                            CBUILDING_ID = x.Building,
                            CDOC_NO = x.DocumentNo,
                            CDOC_DATE = x.DocumentDate,
                            CHO_PLAN_DATE = x.PlanHODate,
                            CSTART_DATE = x.StartDate,
                            CEND_DATE = x.EndDate,
                            CMONTH = x.Month,
                            CYEAR = x.Year,
                            CDAY = x.Day,
                            CSALESMAN_ID = x.Salesman,
                            CTENANT_ID = x.Tenant,
                            CUNIT_DESCRIPTION = x.UnitDescription,
                            CNOTES = x.Notes,
                            CCURRENCY_CODE = x.Currency,
                            CLEASE_MODE = x.LeaseMode,
                            CCHARGE_MODE = x.ChargeMode,
                            CBILLING_RULE_CODE = x.BillingRule,
                            NBOOKING_FEE = x.BookingFee,
                            CTC_CODE = x.TCCode,
                            ISEQ_NO_ERROR = x.NO,
                            CHO_ACTUAL_DATE = x.ActualHODate,
                        })
                        .ToList();
                }
                if (UnitGrid.Count > 0)
                {
                    loBigObject.UnitList = UnitGrid
                        .Select(x => new PMT01300UploadUnitDTO
                        {
                            NO = x.NO,
                            CDOC_NO = x.DocumentNo,
                            CUNIT_ID = x.UnitId,
                            CBUILDING_ID = x.BuildingId,
                            CFLOOR_ID = x.FloorId,
                            ISEQ_NO_ERROR = x.SEQ_ERROR,
                        })
                        .ToList();
                }
                if (UtilityGrid.Count > 0)
                {
                    loBigObject.UtilityList = UtilityGrid
                       .Select(x => new PMT01300UploadUtilitiesDTO
                       {
                           NO = x.NO,
                           CDOC_NO = x.DocumentNo,
                           CUTILITY_TYPE = x.UtilityType,
                           CUNIT_ID = x.UnitId,
                           CMETER_NO = x.MeterNo,
                           CCHARGES_ID = x.ChargesId,
                           CTAX_ID = x.TaxId,
                           NMETER_START = x.MeterStart,
                           NBLOCK1_START = x.Block1Start,
                           NBLOCK2_START = x.Block2Start,
                           ISEQ_NO_ERROR = x.SEQ_ERROR,
                       })
                       .ToList();
                }
                if (ChargesGrid.Count > 0)
                {
                    loBigObject.ChargesList = ChargesGrid
                        .Select(x =>
                        {
                            int tenureYear = 0;
                            int tenureMonth = 0;
                            int tenureDays = 0;

                            int.TryParse(x.TenureYear, out tenureYear);
                            int.TryParse(x.TenureMonth, out tenureMonth);
                            int.TryParse(x.TenureDays, out tenureDays);
                            return new PMT01300UploadChargesDTO
                            {
                                NO = x.NO,
                                CDOC_NO = x.DocumentNo,
                                CUNIT_ID = x.UnitId,
                                CFLOOR_ID = x.FloorId,
                                CCHARGES_ID = x.ChargesId,
                                CTAX_ID = x.TaxId,
                                IYEARS = tenureYear,
                                IMONTHS = tenureMonth,
                                IDAYS = tenureDays,
                                LBASED_OPEN_DATE = x.BaseonOpeningDateDisplay,
                                CSTART_DATE = x.StartDate,
                                CEND_DATE = x.EndDate,
                                CBILLING_MODE = x.BillingMode,
                                CCURENCY_CODE = x.Currency,
                                CFEE_METHOD = x.FeeMethod,
                                NFEE_AMT = x.FeeAmount,
                                CPERIOD_MODE = x.PeriodMode,
                                LPRORATE = x.ProrateDisplay,
                                CDESCRIPTION = x.Description,
                                ISEQ_NO_ERROR = x.SEQ_ERROR,
                            };
                        })
                        .ToList();
                }
                if (DepositGrid.Count > 0)
                {
                    loBigObject.DepositList = DepositGrid
                        .Select(x => new PMT01300UploadDepositDTO
                        {
                            NO = x.NO,
                            CDOC_NO = x.DocumentNo,
                            LCONTRACTOR = x.FlagContractorDisplay,
                            CCONTRACTOR_ID = x.ContractorId,
                            CDEPOSIT_ID = x.DepositId,
                            CDEPOSIT_DATE = x.DepositDate,
                            CCURRENCY_CODE = x.Currency,
                            NDEPOSIT_AMT = x.DepositAmount,
                            LPAID = x.FlagPaidDisplay,
                            CDESCRIPTION = x.Description,
                            ISEQ_NO_ERROR = x.SEQ_ERROR,
                        })
                        .ToList();
                }

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();
                loBatchPar.COMPANY_ID = CompanyID;
                loBatchPar.USER_ID = UserId;
                loBatchPar.ClassName = "PMT01300BACK.PMT01301Cls";
                loBatchPar.UserParameters = loUserParameneters;
                loBatchPar.BigObject = loBigObject;

                var lcGuid = await loCls.R_BatchProcess<PMT01300UploadBigObjectParameterDTO>(loBatchPar, 60);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:

            loEx.ThrowExceptionIfErrors();
        }

        #region Status
        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            var loEx = new R_Exception();

            try
            {
                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    Message = string.Format("Process Complete and success with GUID {0}", pcKeyGuid);
                    VisibleError = false;
                    await ActionIsCompleteSuccess();
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    Message = string.Format("Process Complete but fail with GUID {0}", pcKeyGuid);
                    await ServiceGetError(pcKeyGuid);
                    VisibleError = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            // Call Method Action StateHasChange
            StateChangeAction();

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            Message = string.Format("Process Error with GUID {0}", pcKeyGuid);

            // Call Method Action Error Unhandle
            ShowErrorAction(ex);

            // Call Method Action StateHasChange
            StateChangeAction();

            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            Percentage = pnProgress;
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            // Call Method Action StateHasChange
            StateChangeAction();

            await Task.CompletedTask;
        }

        private async Task ServiceGetError(string pcKeyGuid)
        {
            R_Exception loException = new R_Exception();

            List<R_ErrorStatusReturn> loResultData;
            R_GetErrorWithMultiLanguageParameter loParameterData;
            R_ProcessAndUploadClient loCls;
            DataTable loDataTable;
            try
            {
                // Add Parameter
                loParameterData = new R_GetErrorWithMultiLanguageParameter()
                {
                    COMPANY_ID = CompanyID,
                    USER_ID = UserId,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "RSP_PM_UPLOAD_LEASE_AGREEMENTResources"
                };

                loCls = new R_ProcessAndUploadClient(pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM");

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);

                // check error if unhandle
                if (loResultData.Any(y => y.SeqNo <= 0))
                {
                    var loUnhandleEx = loResultData.Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                    ErrorList = new List<R_BlazorFrontEnd.Exceptions.R_Error>(loUnhandleEx);
                    loUnhandleEx.ForEach(x => loException.Add(x));
                }

                if (loResultData.Any(y => y.SeqNo > 0))
                {
                    // Display Error Handle if get seq Agreement
                    AgreementGrid.ToList().ForEach(x =>
                    {
                        //Assign ErrorMessage, Valid and Set Valid And Invalid Data
                        if (loResultData.Any(y => y.SeqNo == x.NO))
                        {
                            x.NotesError = loResultData.Where(y => y.SeqNo == x.NO).FirstOrDefault().ErrorMessage;
                            x.Valid = "N";
                            SumInvalidDataAgreementExcel++;
                        }
                        else
                        {
                            x.Valid = "Y";
                            SumValidDataAgreementExcel++;
                        }
                    });

                    // Display Error Handle if get seq Unit
                    UnitGrid.ToList().ForEach(x =>
                    {
                        //Assign ErrorMessage, Valid and Set Valid And Invalid Data
                        if (loResultData.Any(y => y.SeqNo == x.SEQ_ERROR))
                        {
                            x.Notes = loResultData.Where(y => y.SeqNo == x.SEQ_ERROR).FirstOrDefault().ErrorMessage;
                            x.Valid = "N";
                            SumInvalidDataUnitExcel++;
                        }
                        else
                        {
                            x.Valid = "Y";
                            SumValidDataUnitExcel++;
                        }
                    });

                    // Display Error Handle if get seq Utility
                    UtilityGrid.ToList().ForEach(x =>
                    {
                        //Assign ErrorMessage, Valid and Set Valid And Invalid Data
                        if (loResultData.Any(y => y.SeqNo == x.SEQ_ERROR))
                        {
                            x.Notes = loResultData.Where(y => y.SeqNo == x.SEQ_ERROR).FirstOrDefault().ErrorMessage;
                            x.Valid = "N";
                            SumInvalidDataUtilityExcel++;
                        }
                        else
                        {
                            x.Valid = "Y";
                            SumValidDataUtilityExcel++;
                        }
                    });

                    // Display Error Handle if get seq Charges
                    ChargesGrid.ToList().ForEach(x =>
                    {
                        //Assign ErrorMessage, Valid and Set Valid And Invalid Data
                        if (loResultData.Any(y => y.SeqNo == x.SEQ_ERROR))
                        {
                            x.Notes = loResultData.Where(y => y.SeqNo == x.SEQ_ERROR).FirstOrDefault().ErrorMessage;
                            x.Valid = "N";
                            SumInvalidDataChargesExcel++;
                        }
                        else
                        {
                            x.Valid = "Y";
                            SumValidDataChargesExcel++;
                        }
                    });

                    // Display Error Handle if get seq Deposit
                    DepositGrid.ToList().ForEach(x =>
                    {
                        //Assign ErrorMessage, Valid and Set Valid And Invalid Data
                        if (loResultData.Any(y => y.SeqNo == x.SEQ_ERROR))
                        {
                            x.Notes = loResultData.Where(y => y.SeqNo == x.SEQ_ERROR).FirstOrDefault().ErrorMessage;
                            x.Valid = "N";
                            SumInvalidDataDepositExcel++;
                        }
                        else
                        {
                            x.Valid = "Y";
                            SumValidDataDepositExcel++;
                        }
                    });

                    //Set DataSetTable
                    var loDataSet = new DataSet();

                    #region Agreement
                    var loAgreementExcelData = R_FrontUtility.ConvertCollectionToCollection<PMT01300UploadAgreementSaveExcelDTO>(AgreementGrid);

                    loDataTable = R_FrontUtility.R_ConvertTo<PMT01300UploadAgreementSaveExcelDTO>(loAgreementExcelData);
                    loDataTable.TableName = "Agreement";

                    loDataSet.Tables.Add(loDataTable);
                    #endregion

                    #region Unit
                    var loUnitExcelData = R_FrontUtility.ConvertCollectionToCollection<PMT01300UploadUnitSaveExcelDTO>(UnitGrid);

                    loDataTable = R_FrontUtility.R_ConvertTo<PMT01300UploadUnitSaveExcelDTO>(loUnitExcelData);
                    loDataTable.TableName = "Unit";

                    loDataSet.Tables.Add(loDataTable);
                    #endregion

                    #region Utility
                    var loUtilityExcelData = R_FrontUtility.ConvertCollectionToCollection<PMT01300UploadUtilitySaveExcelDTO>(UtilityGrid);

                    loDataTable = R_FrontUtility.R_ConvertTo<PMT01300UploadUtilitySaveExcelDTO>(loUtilityExcelData);
                    loDataTable.TableName = "Utility";

                    loDataSet.Tables.Add(loDataTable);
                    #endregion

                    #region Charges
                    var loChargesExcelData = R_FrontUtility.ConvertCollectionToCollection<PMT01300UploadChargesSaveExcelDTO>(ChargesGrid);

                    loDataTable = R_FrontUtility.R_ConvertTo<PMT01300UploadChargesSaveExcelDTO>(loChargesExcelData);
                    loDataTable.TableName = "Charges";

                    loDataSet.Tables.Add(loDataTable);
                    #endregion

                    #region Deposit
                    var loDepositExcelData = R_FrontUtility.ConvertCollectionToCollection<PMT01300UploadDepositSaveExcelDTO>(DepositGrid);

                    loDataTable = R_FrontUtility.R_ConvertTo<PMT01300UploadDepositSaveExcelDTO>(loDepositExcelData);
                    loDataTable.TableName = "Deposit";

                    loDataSet.Tables.Add(loDataTable);
                    #endregion

                    // Asign Dataset
                    ExcelDataSet = loDataSet;

                    // Dowload if get Error
                    //await ActionDataSetExcel.Invoke();
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
        #endregion
    }
}
