using BaseAOC_BS11Common.DTO.Request.Request.Single;
using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMCOMMON.DTOs.LML01000;
using Lookup_PMCOMMON.DTOs.LML01100;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00500;
using Lookup_PMModel.ViewModel.LML00600;
using Lookup_PMModel.ViewModel.LML00800;
using Lookup_PMModel.ViewModel.LML01100;
using Microsoft.AspNetCore.Components;
using PMT01900Common.DTO.CRUDBase;
using PMT01900Common.DTO.Front;
using PMT01900FrontResources;
using PMT01900Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace PMT01900Front
{
    public partial class PMT01900LOI : R_Page
    {
        private readonly PMT01900LOIViewModel _viewModel = new();
        private R_Conductor? _conductor;
        //private R_Grid<PMT01900LOI_SelectedLOIDTO>? _gridRef;

        [Inject] IClientHelper? _clientHelper { get; set; }

        PMT01900EventCallBackDTO _oEventCallBack = new PMT01900EventCallBackDTO();
        private bool _isCheckerDataFound = false;
        private bool _lDataCREF_NO = false;

        //Tambahan
        bool _isAllDataReady = false;

        //For New Open Page
        bool _LOpenAsPopUpPage = false;
        private string PageWidth = "width: 1100px;";


        private R_TextBox? _componentCDEPT_CODETextBox;




        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                //lControlChoosenData = true;
                _viewModel.oParameter = R_FrontUtility.ConvertObjectToObject<PMT01900ParameterFrontChangePageDTO>(poParameter);

                _LOpenAsPopUpPage = _viewModel.oParameter.CALLER_ACTION == "VIEW";
                PageWidth = _LOpenAsPopUpPage ? "width: 1100px;" : "width: auto;";

                await _viewModel.GetVAR_GSM_TRANSACTION_CODE();

                if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
                {
                    _isAllDataReady = true;
                    if (!string.IsNullOrEmpty(_viewModel.oParameter.CREF_NO))
                    {
                        _isCheckerDataFound = true;
                        await _conductor.R_GetEntity(null);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Function Component

        #region Master HandleFunction

        private void OnChangedDFOLLOW_UP_DATE(DateTime? poParameter)
        {
            PMT01900LOI_SelectedLOIDTO loData = _viewModel.Data;
            loData.DFOLLOW_UP_DATE = poParameter;
        }

        private void OnChangedDHAND_OVER_DATE(DateTime? poParameter)
        {
            PMT01900LOI_SelectedLOIDTO loData = _viewModel.Data;
            //  loData.DHAND_OVER_DATE = poParameter;
        }


        private void OnChangedDREF_DATE(DateTime? poParameter)
        {
            PMT01900LOI_SelectedLOIDTO loData = _viewModel.Data;
            loData.DREF_DATE = poParameter;
        }

        #region DateTime Function

        #endregion

        private void OnChangedCYEAR(Int32 poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01900LOI_SelectedLOIDTO)_viewModel.Data;
                var llControl = _viewModel.oControlYMD;
                loData.IYEARS = poParam;

                if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
                {
                    if (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                    }
                }
                else
                {
                    llControl.LYEAR = true;
                    loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddDays(-1);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void OnChangedCMONTH(Int32 poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01900LOI_SelectedLOIDTO)_viewModel.Data;
                var llControl = _viewModel.oControlYMD;
                loData.IMONTHS = poParam;

                if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
                {
                    if (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                    }
                }
                else
                {
                    llControl.LMONTH = true;
                    loData.DEND_DATE = loData.DSTART_DATE!.Value.AddMonths(loData.IMONTHS).AddDays(-1);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void OnChangedCDAY(Int32 poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01900LOI_SelectedLOIDTO)_viewModel.Data;
                var llControl = _viewModel.oControlYMD;
                loData.IDAYS = poParam;

                if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
                {
                    if (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                    }
                }
                else
                {
                    llControl.LYEAR = true;
                    loData.DEND_DATE = loData.DSTART_DATE!.Value.AddDays(loData.IDAYS).AddDays(-1);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void OnChangedIHOUR(Int32 poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01900LOI_SelectedLOIDTO)_viewModel.Data;
                var llControl = _viewModel.oControlYMD;

                // Mengatur nilai IHOUR
                loData.IHOURS = poParam;

                if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
                {
                    if (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0 && loData.IHOURS == 0)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value
                            .AddYears(loData.IYEARS)
                            .AddMonths(loData.IMONTHS)
                            .AddDays(loData.IDAYS)
                            .AddHours(loData.IHOURS)
                            .AddDays(-1);
                    }
                }
                else
                {
                    llControl.LYEAR = true;
                    llControl.LMONTH = true;
                    llControl.LDAY = true;
                    loData.DEND_DATE = loData.DSTART_DATE!.Value
                        .AddDays(loData.IDAYS)
                        .AddHours(loData.IHOURS)
                        .AddDays(-1);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }


        DateTime tStartDate = DateTime.Now.AddDays(-1);

        private void OnChangedDSTART_DATE(DateTime? poValue)
        {
            R_Exception loException = new R_Exception();
            var loData = _viewModel.Data;
            try
            {
                if (poValue.HasValue)
                {
                    DateTime adjustedValue = new DateTime(poValue.Value.Year, poValue.Value.Month, poValue.Value.Day, poValue.Value.Hour, 0, 0);
                    loData.DSTART_DATE = poValue;
                }

                tStartDate = poValue ?? DateTime.Now;

                if (loData.DEND_DATE == null)
                {
                    loData.DEND_DATE = loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0
                        ? loData.DSTART_DATE
                        : loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                }
                else
                {
                    CalculateYMD(poStartDate: loData.DSTART_DATE, poEndDate: loData.DEND_DATE, pcStart: "S");
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        private void OnChangedDEND_DATE(DateTime? poValue)
        {
            R_Exception loException = new R_Exception();

            try
            {
                PMT01900LOI_SelectedLOIDTO loData = _viewModel.Data;
                loData.DEND_DATE = poValue;

                if (loData.DSTART_DATE == null)
                {
                    loData.DSTART_DATE = loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0
                        ? loData.DEND_DATE
                        : loData.DEND_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                }
                else
                {
                    CalculateYMD(poStartDate: loData.DSTART_DATE, poEndDate: loData.DEND_DATE);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        private void CalculateYMD(DateTime? poStartDate, DateTime? poEndDate, string pcStart = "")
        {
            R_Exception loException = new R_Exception();
            PMT01900LOI_SelectedLOIDTO loData = _viewModel.Data;

            try
            {
                if (poEndDate != null && poStartDate != null)
                {
                    DateTime dValueEndDate = poEndDate!.Value.AddDays(1);
                    int liChecker = poEndDate!.Value.Day - poStartDate!.Value.Day;

                    if (liChecker < 0)
                    {
                        loData.IDAYS = 1;
                        loData.IMONTHS = loData.IYEARS = 0;
                        loData.IHOURS = 24; // Set to 24 hours for 1 day difference
                        if (string.IsNullOrEmpty(pcStart))
                            loData.DSTART_DATE = loData.DEND_DATE;
                        else
                            loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        loData.IDAYS = dValueEndDate.Day - poStartDate!.Value.Day;
                        if (loData.IDAYS < 0)
                        {
                            DateTime dValueEndDateForHandleDay = dValueEndDate.AddMonths(-1);
                            int liTempDayinMonth = DateTime.DaysInMonth(dValueEndDateForHandleDay.Year, dValueEndDateForHandleDay.Month);
                            loData.IDAYS = liTempDayinMonth + loData.IDAYS;
                            if (loData.IDAYS < 0) { throw new Exception("ERROR HARINYA MINUS"); }
                            loData.IMONTHS = dValueEndDateForHandleDay.Month - poStartDate!.Value.Month;
                            if (loData.IMONTHS < 0)
                            {
                                loData.IMONTHS = 12 + loData.IMONTHS;
                                DateTime dValueEndDateForHandleMonth = dValueEndDate.AddYears(-1);
                                loData.IYEARS = dValueEndDateForHandleMonth.Year - poStartDate!.Value.Year;
                                if (loData.IYEARS < 0)
                                {
                                    loData.IYEARS = 0;
                                }
                            }
                        }
                        else
                        {
                            loData.IMONTHS = dValueEndDate.Month - poStartDate!.Value.Month;
                            if (loData.IMONTHS < 0)
                            {
                                loData.IMONTHS = 12 + loData.IMONTHS;
                                DateTime dValueEndDateForHandleMonth = dValueEndDate.AddYears(-1);
                                loData.IYEARS = dValueEndDateForHandleMonth.Year - poStartDate!.Value.Year;
                                if (loData.IYEARS < 0)
                                {
                                    loData.IYEARS = 0;
                                }
                            }
                            else
                            {
                                loData.IYEARS = dValueEndDate.Year - poStartDate!.Value.Year;
                            }
                        }
                        // Calculate hours
                        loData.IHOURS = (int)(poEndDate.Value - poStartDate.Value).TotalHours % 24;
                    }
                }
                else
                {
                    if (poStartDate != null)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddDays(1);
                        loData.IYEARS = loData.IMONTHS = 0;
                        loData.IDAYS = 2;
                        loData.IHOURS = 24; // Set to 24 hours for 1 day difference
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        /*
        private void OnChangedDSTART_TIME(DateTime? poValue)
        {
            R_Exception loException = new R_Exception();

            try
            {
                PMT01900LOI_SelectedLOIDTO loData = _viewModel.Data;
                loData.DSTART_DATE = poValue;

                if (loData.DSTART_DATE == null)
                {
                    //    loData.DSTART_DATE = loData.IYEAR == 0 && loData.IMONTH == 0 && loData.IDAYS == 0
                    //        ? loData.DEND_DATE
                    //        : loData.DEND_DATE!.Value.AddYears(loData.IYEAR).AddMonths(loData.IMONTH).AddDays(loData.IDAYS).AddDays(-1);
                }
                else
                {

                    // CalculateYMD(poStartDate: loData.DSTART_DATE, poEndDate: loData.DEND_DATE);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }
        private void OnChangedDEND_TIME(DateTime? poValue)
        {
            R_Exception loException = new R_Exception();

            try
            {
                PMT01900LOI_SelectedLOIDTO loData = _viewModel.Data;
                loData.DEND_DATE = poValue;

                if (loData.DEND_DATE == null)
                {
                    //    loData.DSTART_DATE = loData.IYEAR == 0 && loData.IMONTH == 0 && loData.IDAYS == 0
                    //        ? loData.DEND_DATE
                    //        : loData.DEND_DATE!.Value.AddYears(loData.IYEAR).AddMonths(loData.IMONTH).AddDays(loData.IDAYS).AddDays(-1);
                }
                else
                {

                    // CalculateYMD(poStartDate: loData.DSTART_DATE, poEndDate: loData.DEND_DATE);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }
        */

        #endregion

        #endregion

        #region Conductor Event

        private void AfterAddAsync(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            var loData = (PMT01900LOI_SelectedLOIDTO)eventArgs.Data;

            try
            {
                _oEventCallBack.LCRUD_MODE = true;

                DateTime ldNow = DateTime.Now;
                DateTime ldFormatNow = new DateTime(ldNow.Year, ldNow.Month, ldNow.Day, ldNow.Hour, 0, 0);

                loData.DSTART_DATE = ldFormatNow;
                loData.DEND_DATE = ldFormatNow;
                loData.DREF_DATE = ldNow;
                loData.DFOLLOW_UP_DATE = ldNow;

                //await _componentCDEPT_CODETextBox.FocusAsync();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        private async void AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            var loException = new R_Exception();
            //Task loTask;

            try
            {
                //if (!LOpenAsNormalPage)
                //{
                //    await Close(true, _viewModel.Data);
                //}

                _isCheckerDataFound = true;
                _oEventCallBack.LCRUD_MODE = true;
                //_oEventCallBack.LACTIVEUnitInfoHasData = true;
                _oEventCallBack.CCRUD_MODE = "A_ADD";//Meaning of Agreement Add
                // Lakukan pemanggilan async
                await InvokeTabEventCallbackAsync(_oEventCallBack);

                // Setelah pemanggilan selesai, lanjutkan dengan kode selanjutnya
                _oEventCallBack.CCRUD_MODE = "";
            }
            catch (Exception Ex)
            {
                loException.Add(Ex);
            }

            R_DisplayException(loException);
        }

        private void R_SetEdit(R_SetEventArgs eventArgs)
        {

        }

        public void AfterDelete()
        {
        }

        private async Task R_SetOther(R_SetEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                _viewModel.lControlCRUDMode = eventArgs.Enable;
                _oEventCallBack.LCRUD_MODE = _viewModel.lControlCRUDMode = eventArgs.Enable;
                await InvokeTabEventCallbackAsync(_oEventCallBack);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        private void R_Validation(R_ValidationEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                var loData = (PMT01900LOI_SelectedLOIDTO)eventArgs.Data;
                //await LostFocusCUNIT_ID();

                if (_lDataCREF_NO)
                {
                    if (string.IsNullOrWhiteSpace(loData.CREF_NO))
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01900_Class), "ValidationAgreementNo");
                        loException.Add(loErr);
                    }
                }

                if (string.IsNullOrWhiteSpace(loData.CTENANT_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01900_Class), "ValidationTenant");
                    loException.Add(loErr);
                }

                if (string.IsNullOrWhiteSpace(loData.CDEPT_CODE))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01900_Class), "ValidationDepartment");
                    loException.Add(loErr);
                }


                if (string.IsNullOrWhiteSpace(loData.CBUILDING_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01900_Class), "ValidationBuilding");
                    loException.Add(loErr);
                }

                if (string.IsNullOrWhiteSpace(loData.CSALESMAN_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01900_Class), "ValidationSalesman");
                    loException.Add(loErr);
                }

                if (loData.DREF_DATE == null)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01900_Class), "ValidationOfferDate");
                    loException.Add(loErr);
                }

                if (loData.DSTART_DATE > loData.DEND_DATE)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01900_Class), "ValidationDate");
                    loException.Add(loErr);
                }

                if (string.IsNullOrWhiteSpace(loData.CCURRENCY_CODE))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01900_Class), "ValidationCurrency");
                    loException.Add(loErr);
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            eventArgs.Cancel = loException.HasError;


            loException.ThrowExceptionIfErrors();
        }

        #endregion

        #region Master CRUD

        private void R_Display(R_DisplayEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                //switch (eventArgs.ConductorMode)
                //{
                //    case R_eConductorMode.Edit:
                //        //await _componentCDOC_NOTextBox.FocusAsync();
                //        //OnChangedDEND_DATE(_viewModel.Data.DEND_DATE);
                //        break;
                //    default:
                //        break;
                //}

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        public async Task R_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                _oEventCallBack.LCRUD_MODE = true;
                _oEventCallBack.CCRUD_MODE = "A_CANCEL";//Meaning of Agreement Add
                await InvokeTabEventCallbackAsync(_oEventCallBack);
                // Setelah pemanggilan selesai, lanjutkan dengan kode selanjutnya
                _oEventCallBack.CCRUD_MODE = "";

                //if (!string.IsNullOrEmpty(_viewModel.oParameter.CREF_NO))
                //{
                //    _isCheckerDataFound = true;
                //    await _conductor.R_GetEntity(null);
                //}
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            PMT01900LOI_SelectedLOIDTO loParam;

            try
            {
                loParam = new PMT01900LOI_SelectedLOIDTO();
                if (eventArgs.Data != null)
                {
                    loParam = R_FrontUtility.ConvertObjectToObject<PMT01900LOI_SelectedLOIDTO>(eventArgs.Data);
                }
                else
                {
                    loParam.CREF_NO = _viewModel.oParameter.CREF_NO;
                    loParam.CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID;
                    loParam.CDEPT_CODE = _viewModel.oParameter.CDEPT_CODE;
                    loParam.CTRANS_CODE = _viewModel.oParameter.CTRANS_CODE;
                    loParam.CCOMPANY_ID = _clientHelper.CompanyId;
                    loParam.CUSER_ID = _clientHelper.UserId;
                };
                await _viewModel.GetEntity(loParam);

                eventArgs.Result = _viewModel.oEntity;
                switch (_viewModel.oEntity.CTRANS_STATUS)
                {
                    case "00":
                        _viewModel.lControlButtonRedraft = false;
                        _viewModel.lControlButtonSubmit = true;
                        break;
                    case "30":
                        _viewModel.lControlButtonRedraft = _viewModel.lControlButtonSubmit = false;
                        break;
                    case "10":
                        _viewModel.lControlButtonSubmit = false;
                        _viewModel.lControlButtonRedraft = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01900LOI_SelectedLOIDTO>(eventArgs.Data);


                await _viewModel.ServiceSave(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel.oEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01900LOI_SelectedLOIDTO)eventArgs.Data;

                await _viewModel.GetEntity(loData);

                if (_viewModel.oEntity != null)
                    await _viewModel.ServiceDelete(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Master LookUp

        #region Lookup Button RefNo Lookup

        private R_Lookup? R_LookupRefNoLookup;

        private void BeforeOpenLookUpRefNoLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00800ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
            {
                param = new LML00800ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID,
                    CDEPT_CODE = _viewModel.Data.CDEPT_CODE ?? "",
                    CAGGR_STTS = "00,02",
                    CTRANS_CODE = "802053",
                    CTRANS_STATUS = "30",
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00800);
        }

        private async Task AfterOpenLookUpRefNoLookupAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            LML00800DTO? loTempResult = null;
            //PMT01900LOI_SelectedLOIDTO? loGetData = null;

            try
            {
                loTempResult = (LML00800DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;
                //loGetData = (PMT01900LOI_SelectedLOIDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();
                BaseAOCParameterRequestGetAgreementDetailDTO loParamGetAgreementDetail = new BaseAOCParameterRequestGetAgreementDetailDTO()
                {
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID,
                    CDEPT_CODE = loTempResult.CDEPT_CODE,
                    CTRANS_CODE = loTempResult.CTRANS_CODE,
                    CREF_NO = loTempResult.CREF_NO,
                };


                if (!string.IsNullOrEmpty(loParamGetAgreementDetail.CREF_NO))
                {

                    var loAgreementData = await _viewModel.GetAgreementDetail(loParamGetAgreementDetail);

                    _viewModel.Data.CDEPT_CODE = loAgreementData.CDEPT_CODE;
                    _viewModel.Data.CDEPT_NAME = loAgreementData.CDEPT_NAME;
                    _viewModel.Data.CLINK_REF_NO = loAgreementData.CREF_NO;
                    _viewModel.Data.CLINK_TRANS_CODE = loAgreementData.CLINK_TRANS_CODE;

                    _viewModel.Data.DREF_DATE = !string.IsNullOrEmpty(loAgreementData.CREF_DATE) ? _viewModel._AOCService.ConvertStringToDateTimeFormat(loAgreementData.CREF_DATE) : null;
                    _viewModel.Data.DFOLLOW_UP_DATE = !string.IsNullOrEmpty(loAgreementData.CFOLLOW_UP_DATE) ? _viewModel._AOCService.ConvertStringToDateTimeFormat(loAgreementData.CFOLLOW_UP_DATE) : null;

                    _viewModel.Data.CTENANT_ID = loAgreementData.CTENANT_ID;
                    _viewModel.Data.CTENANT_NAME = loAgreementData.CTENANT_NAME;
                    _viewModel.Data.CBUILDING_ID = loAgreementData.CBUILDING_ID;
                    _viewModel.Data.CBUILDING_NAME = loAgreementData.CBUILDING_NAME;

                    _viewModel.Data.CSALESMAN_ID = loAgreementData.CSALESMAN_ID;
                    _viewModel.Data.CSALESMAN_NAME = loAgreementData.CSALESMAN_NAME;
                    //_viewModel.Data.CEVENT_NAME = loAgreementData.CEVENT_NAME;
                    _viewModel.Data.IYEARS = loAgreementData.IYEARS;
                    _viewModel.Data.IMONTHS = loAgreementData.IMONTHS;
                    _viewModel.Data.IDAYS = loAgreementData.IDAYS;
                    _viewModel.Data.IHOURS = loAgreementData.IHOURS;
                    _viewModel.Data.DSTART_DATE = !string.IsNullOrEmpty(loAgreementData.CSTART_DATE) ? _viewModel._AOCService.ConvertStringToDateTimeFormat(loAgreementData.CSTART_DATE) : null;
                    _viewModel.Data.DEND_DATE = !string.IsNullOrEmpty(loAgreementData.CEND_DATE) ? _viewModel._AOCService.ConvertStringToDateTimeFormat(loAgreementData.CEND_DATE) : null;
                    _viewModel.Data.CUNIT_DESCRIPTION = loAgreementData.CUNIT_DESCRIPTION;
                    _viewModel.Data.CNOTES = loAgreementData.CNOTES;
                    _viewModel.Data.CCURRENCY_CODE = loAgreementData.CCURRENCY_CODE;
                    _viewModel.Data.NBOOKING_FEE = loAgreementData.NBOOKING_FEE;
                    _viewModel.Data.CTC_CODE = loAgreementData.CTC_CODE;

                }


            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusLinkRefNo()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT01900LOI_SelectedLOIDTO loGetData = _viewModel.Data;

                if (string.IsNullOrWhiteSpace(loGetData.CLINK_REF_NO))
                {
                    loGetData.CLINK_REF_NO = "";
                    loGetData.CLINK_TRANS_CODE = "";
                    return;
                }

                LookupLML00800ViewModel loLookupViewModel = new LookupLML00800ViewModel();
                LML00800ParameterDTO loParam = new LML00800ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID!,
                    CDEPT_CODE = _viewModel.Data.CDEPT_CODE ?? "",
                    CAGGR_STTS = "00,02",
                    CTRANS_CODE = "802053",
                    CTRANS_STATUS = "30",
                    CSEARCH_TEXT = loGetData.CLINK_REF_NO ?? "",
                };


                var loResult = await loLookupViewModel.GetAgreement(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CLINK_REF_NO = "";
                    loGetData.CLINK_TRANS_CODE = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    BaseAOCParameterRequestGetAgreementDetailDTO loParamGetAgreementDetail = new BaseAOCParameterRequestGetAgreementDetailDTO()
                    {
                        CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID,
                        CDEPT_CODE = loGetData.CDEPT_CODE,
                        CTRANS_CODE = loGetData.CTRANS_CODE,
                        CREF_NO = loGetData.CREF_NO,
                    };


                    if (!string.IsNullOrEmpty(loParamGetAgreementDetail.CREF_NO))
                    {

                        var loAgreementData = await _viewModel.GetAgreementDetail(loParamGetAgreementDetail);

                        loGetData.CDEPT_CODE = loAgreementData.CDEPT_CODE;
                        loGetData.CDEPT_NAME = loAgreementData.CDEPT_NAME;
                        loGetData.CLINK_REF_NO = loAgreementData.CREF_NO;
                        loGetData.CLINK_TRANS_CODE = loAgreementData.CLINK_TRANS_CODE;

                        loGetData.DREF_DATE = !string.IsNullOrEmpty(loAgreementData.CREF_DATE) ? _viewModel._AOCService.ConvertStringToDateTimeFormat(loAgreementData.CREF_DATE) : null;
                        loGetData.DFOLLOW_UP_DATE = !string.IsNullOrEmpty(loAgreementData.CFOLLOW_UP_DATE) ? _viewModel._AOCService.ConvertStringToDateTimeFormat(loAgreementData.CFOLLOW_UP_DATE) : null;

                        loGetData.CTENANT_ID = loAgreementData.CTENANT_ID;
                        loGetData.CTENANT_NAME = loAgreementData.CTENANT_NAME;
                        loGetData.CBUILDING_ID = loAgreementData.CBUILDING_ID;
                        loGetData.CBUILDING_NAME = loAgreementData.CBUILDING_NAME;

                        loGetData.CSALESMAN_ID = loAgreementData.CSALESMAN_ID;
                        loGetData.CSALESMAN_NAME = loAgreementData.CSALESMAN_NAME;
                        //loGetData.CEVENT_NAME = loAgreementData.CEVENT_NAME;
                        loGetData.IYEARS = loAgreementData.IYEARS;
                        loGetData.IMONTHS = loAgreementData.IMONTHS;
                        loGetData.IDAYS = loAgreementData.IDAYS;
                        loGetData.IHOURS = loAgreementData.IHOURS;
                        loGetData.DSTART_DATE = !string.IsNullOrEmpty(loAgreementData.CSTART_DATE) ? _viewModel._AOCService.ConvertStringToDateTimeFormat(loAgreementData.CSTART_DATE) : null;
                        loGetData.DEND_DATE = !string.IsNullOrEmpty(loAgreementData.CEND_DATE) ? _viewModel._AOCService.ConvertStringToDateTimeFormat(loAgreementData.CEND_DATE) : null;
                        loGetData.CUNIT_DESCRIPTION = loAgreementData.CUNIT_DESCRIPTION;
                        loGetData.CNOTES = loAgreementData.CNOTES;
                        loGetData.CCURRENCY_CODE = loAgreementData.CCURRENCY_CODE;
                        loGetData.NBOOKING_FEE = loAgreementData.NBOOKING_FEE;
                        loGetData.CTC_CODE = loAgreementData.CTC_CODE;

                    }

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion


        #region Lookup Button Tenant Lookup

        private R_Lookup? R_LookupTenantLookup;

        private void BeforeOpenLookUpTenantLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00600ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
            {
                param = new LML00600ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID,
                    CCUSTOMER_TYPE = "01",
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00600);
        }

        private void AfterOpenLookUpTenantLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            LML00600DTO? loTempResult = null;
            //PMT01900LOI_SelectedLOIDTO? loGetData = null;

            try
            {
                loTempResult = (LML00600DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;
                //loGetData = (PMT01900LOI_SelectedLOIDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _viewModel.Data.CTENANT_ID = loTempResult.CTENANT_ID;
                _viewModel.Data.CTENANT_NAME = loTempResult.CTENANT_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private R_TextBox? _componentCTENANT_IDTextBox;

        private async Task OnLostFocusTenant()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT01900LOI_SelectedLOIDTO loGetData = _viewModel.Data;

                if (string.IsNullOrWhiteSpace(loGetData.CTENANT_ID))
                {
                    loGetData.CTENANT_ID = "";
                    loGetData.CTENANT_NAME = "";
                    return;
                }

                LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel();
                LML00600ParameterDTO loParam = new LML00600ParameterDTO()
                {

                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID!,
                    CCUSTOMER_TYPE = "01",
                    CSEARCH_TEXT = loGetData.CTENANT_ID ?? "",
                };

                var loResult = await loLookupViewModel.GetTenant(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CTENANT_ID = "";
                    loGetData.CTENANT_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CTENANT_ID = loResult.CTENANT_ID;
                    loGetData.CTENANT_NAME = loResult.CTENANT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #region Lookup Button Building Lookup

        private R_Lookup? R_LookupBuildingLookup;

        private void BeforeOpenLookUpBuildingLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL02200ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
            {
                param = new GSL02200ParameterDTO
                {
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL02200);
        }

        private void AfterOpenLookUpBuildingLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL02200DTO? loTempResult = null;
            //PMT01900LOI_SelectedLOIDTO? loGetData = null;

            try
            {
                loTempResult = (GSL02200DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;
                //loGetData = (PMT01900LOI_SelectedLOIDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _viewModel.Data.CBUILDING_ID = loTempResult.CBUILDING_ID;
                _viewModel.Data.CBUILDING_NAME = loTempResult.CBUILDING_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusBuilding()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT01900LOI_SelectedLOIDTO loGetData = (PMT01900LOI_SelectedLOIDTO)_viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CBUILDING_ID))
                {
                    loGetData.CBUILDING_ID = "";
                    loGetData.CBUILDING_NAME = "";
                    return;
                }

                LookupGSL02200ViewModel loLookupViewModel = new LookupGSL02200ViewModel();
                GSL02200ParameterDTO loParam = new GSL02200ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID!,
                    CSEARCH_TEXT = loGetData.CBUILDING_ID ?? "",
                };

                var loResult = await loLookupViewModel.GetBuilding(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CBUILDING_ID = "";
                    loGetData.CBUILDING_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CBUILDING_ID = loResult.CBUILDING_ID;
                    loGetData.CBUILDING_NAME = loResult.CBUILDING_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #region Lookup Button Department Lookup

        private R_Lookup? R_LookupDepartmentLookup;

        private void BeforeOpenLookUpDepartmentLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00710ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
            {
                param = new GSL00710ParameterDTO
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_LOGIN_ID = _clientHelper.UserId,
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID,
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00710);
        }

        private void AfterOpenLookUpDepartmentLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL00710DTO? loTempResult = null;
            //PMT01900LOI_SelectedLOIDTO? loGetData = null;


            try
            {
                loTempResult = (GSL00710DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //loGetData = (PMT01900LOI_SelectedLOIDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _viewModel.Data.CDEPT_CODE = loTempResult.CDEPT_CODE;
                _viewModel.Data.CDEPT_NAME = loTempResult.CDEPT_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusDepartment()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT01900LOI_SelectedLOIDTO loGetData = (PMT01900LOI_SelectedLOIDTO)_viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CDEPT_CODE))
                {
                    loGetData.CDEPT_CODE = "";
                    loGetData.CDEPT_NAME = "";
                    return;
                }

                LookupGSL00710ViewModel loLookupViewModel = new LookupGSL00710ViewModel();
                GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_LOGIN_ID = _clientHelper.UserId,
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID!,
                    CSEARCH_TEXT = loGetData.CDEPT_CODE ?? "",
                };

                var loResult = await loLookupViewModel.GetDepartmentProperty(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CDEPT_CODE = "";
                    loGetData.CDEPT_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CDEPT_CODE = loResult.CDEPT_CODE;
                    loGetData.CDEPT_NAME = loResult.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #region Lookup Button Salesman Lookup

        private R_Lookup? R_LookupSalesmanLookup;

        private void BeforeOpenLookUpSalesmanLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00500ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
            {
                param = new LML00500ParameterDTO
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID,
                    CUSER_ID = _clientHelper.UserId
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00500);
        }

        private void AfterOpenLookUpSalesmanLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            LML00500DTO? loTempResult = null;
            //PMT01900LOI_SelectedLOIDTO? loGetData = null;


            try
            {
                loTempResult = (LML00500DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //loGetData = (PMT01900LOI_SelectedLOIDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _viewModel.Data.CSALESMAN_ID = loTempResult.CSALESMAN_ID;
                _viewModel.Data.CSALESMAN_NAME = loTempResult.CSALESMAN_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusSalesman()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT01900LOI_SelectedLOIDTO loGetData = (PMT01900LOI_SelectedLOIDTO)_viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CSALESMAN_ID))
                {
                    loGetData.CSALESMAN_ID = "";
                    loGetData.CSALESMAN_NAME = "";
                    return;
                }

                LookupLML00500ViewModel loLookupViewModel = new LookupLML00500ViewModel();
                LML00500ParameterDTO loParam = new LML00500ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID!,
                    CUSER_ID = _clientHelper.UserId,
                    CSEARCH_TEXT = loGetData.CSALESMAN_ID ?? "",
                };

                var loResult = await loLookupViewModel.GetSalesman(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CSALESMAN_ID = "";
                    loGetData.CSALESMAN_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CSALESMAN_ID = loResult.CSALESMAN_ID;
                    loGetData.CSALESMAN_NAME = loResult.CSALESMAN_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #region Lookup Button Currency Lookup

        private R_Lookup? R_LookupCurrencyLookup;

        private void BeforeOpenLookUpCurrencyLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00300ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
            {
                param = new GSL00300ParameterDTO
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00300);
        }

        private void AfterOpenLookUpCurrencyLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL00300DTO? loTempResult = null;
            //PMT02500FrontAgreementDetailDTO? loGetData = null;

            try
            {
                loTempResult = (GSL00300DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //loGetData = (PMT02500FrontAgreementDetailDTO)_conductorFullPMT02500Agreement.R_GetCurrentData();

                _viewModel.Data.CCURRENCY_CODE = loTempResult.CCURRENCY_CODE;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusCurrency()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loGetData = (PMT01900LOI_SelectedLOIDTO)_viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CCURRENCY_CODE))
                {
                    loGetData.CCURRENCY_CODE = "";
                    return;
                }

                LookupGSL00300ViewModel loLookupViewModel = new LookupGSL00300ViewModel();
                GSL00300ParameterDTO loParam = new GSL00300ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CSEARCH_TEXT = loGetData.CCURRENCY_CODE ?? "",
                };

                var loResult = await loLookupViewModel.GetCurrency(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CCURRENCY_CODE = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CCURRENCY_CODE = loResult.CCURRENCY_CODE;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion


        #region Lookup Button TandC Lookup

        private R_Lookup? R_LookupTandCLookup;


        private void BeforeOpenLookUpTandCLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML01100ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
            {
                param = new LML01100ParameterDTO
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML01100);
        }

        private void AfterOpenLookUpTandCLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            LML01100DTO? loTempResult = null;

            try
            {
                loTempResult = (LML01100DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                _viewModel.Data.CTC_CODE = loTempResult.CTC_CODE;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusTandC()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT01900LOI_SelectedLOIDTO loGetData = _viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CTC_CODE))
                {
                    loGetData.CTC_CODE = "";
                    return;
                }

                LookupLML01100ViewModel loLookupViewModel = new LookupLML01100ViewModel();
                LML01100ParameterDTO loParam = new LML01100ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID!,
                    CSEARCH_TEXT = loGetData.CTC_CODE ?? "",
                };

                var loResult = await loLookupViewModel.GetTermNCondition(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CTC_CODE = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CTC_CODE = loResult.CTC_CODE;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #endregion

        #region Helper

        private void BlankFunctionButton() { }

        #endregion


        #region Proses

        private async Task ProsesSubmit()
        {
            var loEx = new R_Exception();

            try
            {
                //SUBMIT CODE == "10"
                //REDRAFT CODE == "00"
                bool llConfirmation = await R_MessageBox.Show("Confirmation",
                    R_FrontUtility.R_GetMessage(typeof(Resources_PMT01900_Class), "_ConfirmationSubmit"),
                    R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.Yes;


                if (llConfirmation)
                {
                    _viewModel.oEntity =
                        R_FrontUtility.ConvertObjectToObject<PMT01900LOI_SelectedLOIDTO>(
                            _conductor.R_GetCurrentData());
                    var loResult = await _viewModel.ProsesUpdateAgreementStatus(pcStatus: "10");
                    if ((bool)loResult.LSUCCESS!)
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMT01900_Class),
                            "_SuccessMessageOfferSubmit"));
                        await _conductor.R_GetEntity(null); ;
                    }
                    else
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMT01900_Class),
                            "_FailedUpdate"));
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task ProsesRedraft()
        {
            var loEx = new R_Exception();

            try
            {
                //SUBMIT CODE == "10"
                //REDRAFT CODE == "00"
                bool llConfirmation = await R_MessageBox.Show("Confirmation",
                    R_FrontUtility.R_GetMessage(typeof(Resources_PMT01900_Class), "_ConfirmationSubmit"),
                    R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.Yes;

                if (llConfirmation)
                {
                    _viewModel.oEntity =
                        R_FrontUtility.ConvertObjectToObject<PMT01900LOI_SelectedLOIDTO>(
                            _conductor.R_GetCurrentData());
                    var loResult = await _viewModel.ProsesUpdateAgreementStatus(pcStatus: "00");
                    if ((bool)loResult.LSUCCESS!)
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMT01900_Class),
                            "_SuccessMessageOfferRedraft"));
                        await _conductor.R_GetEntity(null); ;
                    }
                    else
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMT01900_Class),
                            "_FailedUpdate"));
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion
    }
}
