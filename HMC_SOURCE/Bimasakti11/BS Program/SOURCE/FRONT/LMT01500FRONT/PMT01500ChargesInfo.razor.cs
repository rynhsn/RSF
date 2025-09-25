using BlazorClientHelper;
using System.Collections.ObjectModel;
using GFF00900COMMON.DTOs;
using PMT01500Common.DTO._4._Charges_Info;
using PMT01500Common.Utilities;
using PMT01500Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using Lookup_GSModel.ViewModel;
using Lookup_PMModel.ViewModel.LML00200;
using PMT01500Common.Utilities.Front;
using PMT01500FrontResources;
using R_BlazorFrontEnd.Controls.Enums;
using R_LockingFront;

namespace PMT01500Front
{
    public partial class PMT01500ChargesInfo
    {
        private PMT01500ChargesInfoViewModel _viewModel = new();
        private R_Conductor? _conductorRef;
        private R_Grid<PMT01500ChargesInfoListDTO>? _gridListRef;
        private R_ConductorGrid? _conductorCalUnitRef;
        private R_Grid<PMT01500FrontChargesInfo_FeeCalculationDetailDTO>? _gridListCalUnitRef;
        [Inject] private IClientHelper? _clientHelper { get; set; }
        [Inject] public R_PopupService? PopupService { get; set; }
        PMT01500EventCallBackDTO _oEventCallBack = new PMT01500EventCallBackDTO();

        private bool _lControlButton;
        private bool _lIsDataExist;

        private bool _lControlUsingForTotalArea;

        private async Task BlankFunction()
        {
            var loException = new R_Exception();

            try
            {
                await R_MessageBox.Show("", "This Function still on Development Process", R_eMessageBoxButtonType.OK);
                //var llTrue = await R_MessageBox.Show("", "You Clicked the Button!", R_eMessageBoxButtonType.OK);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.loParameterList = (PMT01500GetHeaderParameterDTO)poParameter;

                await _viewModel.GetChargesInfoHeader();
                await _viewModel.GetComboBoxDataCFEE_METHOD();
                await _viewModel.GetComboBoxDataCINVOICE_PERIOD();
                if (!string.IsNullOrEmpty(_viewModel.loParameterList.CREF_NO))
                {
                    await _gridListRef.R_RefreshGrid(null);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task GridChargesList_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetChargesInfoList();
                eventArgs.ListEntityResult = _viewModel.loListChargesInfo;
                _lIsDataExist = _viewModel.loListChargesInfo.Any();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Locking

        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_MODULE_NAME = "PM";

        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult? loLockResult = null;

            try
            {
                var loData = (PMT01500FrontChargesInfoDetailDTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "PMT01500",
                        Table_Name = "PMT_AGREEMENT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CREF_NO)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "PMT01500",
                        Table_Name = "PMT_AGREEMENT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CREF_NO)
                    };

                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }

                llRtn = loLockResult.IsSuccess;
                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                    throw loLockResult.Exception;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return llRtn;
        }

        #endregion

        #region External Function

        private void OnChangedLCAL_UNIT(bool plParam)
        {
            _viewModel.Data.LCAL_UNIT = plParam;
            _viewModel._nTempFEE_AMT = plParam ? _viewModel.Data.NFEE_AMT : _viewModel._nTempFEE_AMT;
            _viewModel.Data.NFEE_AMT = plParam ? 0 : _viewModel._nTempFEE_AMT;
        }

        private void OnChangedCYEAR(Int32 poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01500FrontChargesInfoDetailDTO)_viewModel.Data;
                var llControl = _viewModel._oControlYMD;
                loData.IYEAR = poParam;

                if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
                {
                    if (loData.IYEAR == 0 && loData.IMONTH == 0 && loData.IDAY == 0)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEAR).AddMonths(loData.IMONTH).AddDays(loData.IDAY).AddDays(-1);
                    }
                }
                else
                {
                    llControl.LYEAR = true;
                    loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEAR).AddDays(-1);
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
                var loData = (PMT01500FrontChargesInfoDetailDTO)_viewModel.Data;
                var llControl = _viewModel._oControlYMD;
                loData.IMONTH = poParam;

                if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
                {
                    if (loData.IYEAR == 0 && loData.IMONTH == 0 && loData.IDAY == 0)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEAR).AddMonths(loData.IMONTH).AddDays(loData.IDAY).AddDays(-1);
                    }
                }
                else
                {
                    llControl.LMONTH = true;
                    loData.DEND_DATE = loData.DSTART_DATE!.Value.AddMonths(loData.IMONTH).AddDays(-1);
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
                var loData = (PMT01500FrontChargesInfoDetailDTO)_viewModel.Data;
                var llControl = _viewModel._oControlYMD;
                loData.IDAY = poParam;

                if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
                {
                    if (loData.IYEAR == 0 && loData.IMONTH == 0 && loData.IDAY == 0)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEAR).AddMonths(loData.IMONTH).AddDays(loData.IDAY).AddDays(-1);
                    }
                }
                else
                {
                    llControl.LYEAR = true;
                    loData.DEND_DATE = loData.DSTART_DATE!.Value.AddDays(loData.IDAY).AddDays(-1);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void OnChangedDSTART_DATE(DateTime? poValue)
        {
            R_Exception loException = new R_Exception();

            try
            {
                PMT01500FrontChargesInfoDetailDTO loData = _viewModel.Data;
                loData.DSTART_DATE = poValue;

                if (loData.DEND_DATE == null)
                {
                    loData.DEND_DATE = loData.IYEAR == 0 && loData.IMONTH == 0 && loData.IDAY == 0
                        ? loData.DSTART_DATE
                        : loData.DSTART_DATE!.Value.AddYears(loData.IYEAR).AddMonths(loData.IMONTH).AddDays(loData.IDAY).AddDays(-1);
                }
                else
                {
                    CalculateYMD(poStartDate: loData.DSTART_DATE, poEndDate: loData.DEND_DATE, plStart: true);
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
                PMT01500FrontChargesInfoDetailDTO loData = _viewModel.Data;
                loData.DEND_DATE = poValue;

                if (loData.DSTART_DATE == null)
                {
                    loData.DSTART_DATE = loData.IYEAR == 0 && loData.IMONTH == 0 && loData.IDAY == 0
                        ? loData.DEND_DATE
                        : loData.DEND_DATE!.Value.AddYears(loData.IYEAR).AddMonths(loData.IMONTH).AddDays(loData.IDAY).AddDays(-1);
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

        private void CalculateYMD(DateTime? poStartDate, DateTime? poEndDate, bool plStart = false)
        {
            R_Exception loException = new R_Exception();
            PMT01500FrontChargesInfoDetailDTO loData = _viewModel.Data;

            try
            {
                if (poStartDate >= poEndDate)
                {
                    if (plStart)
                    {
                        loData.DEND_DATE = poStartDate;
                    }
                    else
                    {
                        loData.DSTART_DATE = poEndDate;
                    }
                    loData.IYEAR = loData.IMONTH = 0;
                    loData.IDAY = 1;
                    goto EndBlocks;
                }
                DateTime dValueEndDate = poEndDate!.Value.AddDays(1);

                int liChecker = poEndDate!.Value.Day - poStartDate!.Value.Day;


                loData.IDAY = dValueEndDate.Day - poStartDate!.Value.Day;
                if (loData.IDAY < 0)
                {
                    DateTime dValueEndDateForHandleDay = dValueEndDate.AddMonths(-1);
                    int liTempDayinMonth = DateTime.DaysInMonth(dValueEndDateForHandleDay.Year, dValueEndDateForHandleDay.Month);
                    loData.IDAY = liTempDayinMonth + loData.IDAY;
                    if (loData.IDAY < 0) { loException.Add("ErrDev", "Value is negative!"); }
                    loData.IMONTH = dValueEndDateForHandleDay.Month - poStartDate!.Value.Month;
                    if (loData.IMONTH < 0)
                    {
                        loData.IMONTH = 12 + loData.IMONTH;
                        DateTime dValueEndDateForHandleMonth = dValueEndDate.AddYears(-1);
                        loData.IYEAR = dValueEndDateForHandleMonth.Year - poStartDate!.Value.Year;
                        if (loData.IYEAR < 0)
                        {
                            loData.IYEAR = 0;
                        }
                    }

                }
                else
                {
                    loData.IMONTH = dValueEndDate.Month - poStartDate!.Value.Month;
                    if (loData.IMONTH < 0)
                    {
                        loData.IMONTH = 12 + loData.IMONTH;
                        DateTime dValueEndDateForHandleMonth = dValueEndDate.AddYears(-1);
                        loData.IYEAR = dValueEndDateForHandleMonth.Year - poStartDate!.Value.Year;
                        if (loData.IYEAR < 0)
                        {
                            loData.IYEAR = 0;
                        }
                    }
                    else
                    {
                        loData.IYEAR = dValueEndDate.Year - poStartDate!.Value.Year;
                    }
                }

            }

            //loData.IYEAR = dValueEndDate.Year - poStartDate!.Value.Year;
            //loData.IMONTH = dValueEndDate.Month - poStartDate!.Value.Month;}
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlocks:

            R_DisplayException(loException);
        }

        #endregion

        #region Active/Inactive

        private async Task R_Before_Open_ActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "PMT01501"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    await _viewModel.ProcessChangeStatusChargesInfoActive();
                    await _gridListRef.R_RefreshGrid(null);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "PMT01501" //Uabh Approval Code sesuai Spec masing masing
                    };
                    eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task R_After_Open_ActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Success == false)
                {
                    return;
                }
                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    await _viewModel.ProcessChangeStatusChargesInfoActive();
                    await _gridListRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        #endregion

        #region Button RevenueSharing

        private void R_Before_Open_Popup_RevenueSharing(R_BeforeOpenPopupEventArgs eventArgs)
        {
            PMT01500ParameterForChargesInfo_RevenueSharingDTO loParam = R_FrontUtility.ConvertObjectToObject<PMT01500ParameterForChargesInfo_RevenueSharingDTO>(_viewModel.loParameterList);
            loParam.CCHARGE_SEQ_NO = _viewModel.loEntityChargesInfo.CSEQ_NO;
            loParam.NTOTAL_ACTUAL_AREA = _viewModel.loEntityChargesInfoHeader.NTOTAL_ACTUAL_AREA;
            loParam.CINVOICE_PERIOD_DESCRIPTION = _viewModel.loComboBoxDataCINVOICE_PERIOD
                .FirstOrDefault(x => x.CCODE == _viewModel.Data.CINVOICE_PERIOD)?
                .CDESCRIPTION;

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(PMT01500ChargesInfo_RevenueSharing);
        }

        private async Task R_After_Open_Popup_RevenueSharing(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loException = new R_Exception();
            try
            {
                //var result = eventArgs.Result;
                await _gridListRef.R_SetCurrentData(_viewModel.loListChargesInfo.First());
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        #endregion

        #region Conductor Function

        private async Task Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            GFF00900ParameterDTO? loParamPopup = null;
            R_PopupResult? loResult = null;
            PMT01500FrontChargesInfoDetailDTO? loData = null;
            try
            {
                var loCurrentData = (PMT01500FrontChargesInfoDetailDTO)eventArgs.Data;
                //Validation field from user
                //BillingRuleViewModel.ValidationFieldEmpty(loCurrentData);

                if (!loEx.HasError)
                {
                    try
                    {
                        loData = (PMT01500FrontChargesInfoDetailDTO)eventArgs.Data;
                        if (loData.LACTIVE == true && eventArgs.ConductorMode == R_eConductorMode.Add)
                        {
                            var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                            loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "PMT01501";
                            await loValidateViewModel
                                .RSP_ACTIVITY_VALIDITYMethodAsync();
                            if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" &&
                                  loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                            {
                                eventArgs.Cancel = false;
                            }
                            else
                            {
                                loParamPopup = new GFF00900ParameterDTO()
                                {
                                    Data = loValidateViewModel.loRspActivityValidityList,
                                    IAPPROVAL_CODE = "PMT01501"
                                };
                                loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loParamPopup);

                                if (loResult.Success == false || (bool)loResult.Result == false)
                                {
                                    eventArgs.Cancel = true;
                                    return;
                                };
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        loEx.Add(ex);
                    }
                }

                if (eventArgs.ConductorMode == R_eConductorMode.Edit)
                {
                    if (_viewModel.loListChargesInfoCalUnit.Any() && !loData.LCAL_UNIT)
                    {
                        var llFalse = await R_MessageBox.Show("", "All Fee Calculation Data will be deleted, Are you sure!",
                R_eMessageBoxButtonType.OKCancel);
                        switch (llFalse)
                        {
                            case R_eMessageBoxResult.Cancel:
                                eventArgs.Cancel = true;
                                break;
                            case R_eMessageBoxResult.OK:
                                _viewModel.Data.ODATA_FEE_CALCULATION = null;
                                //dibuat null, soalnya kalo NFEE nya pertama mati, bakal keluar ini lagi
                                break;
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(loData.CCHARGES_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01500_Class), "ValidationCharge");
                    loEx.Add(loErr);
                }
                if (!loData.LCAL_UNIT)
                {
                    if (loData.NFEE_AMT <= 0)
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01500_Class), "ValidationFeeAmount");
                        loEx.Add(loErr);
                    }
                }
                else
                {
                    if (!_viewModel.loListChargesInfoCalUnit.Any())
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01500_Class), "ValidationFeeCalculation");
                        loEx.Add(loErr);
                    }
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            eventArgs.Cancel = _lControlCRUD = loEx.HasError;
            loEx.ThrowExceptionIfErrors();
        }

        public async Task AfterDelete()
        {
            _lControlCRUD = _lControlUsingForTotalArea = _lControlButton = _viewModel.loListChargesInfo.Any();
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }

        private void AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                var loData = (PMT01500FrontChargesInfoDetailDTO)eventArgs.Data;

                loData.DSTART_DATE = DateTime.Now;
                loData.DEND_DATE = DateTime.Now.AddYears(1).AddDays(-1);
                loData.IYEAR = 1;
                loData.LACTIVE = false;
                loData.CBILLING_MODE = _viewModel.loRadioGroupDataCBILLING_MODE.First().CCODE;
                loData.CFEE_METHOD = _viewModel.loComboBoxDataCFEE_METHOD.First().CCODE;
                loData.CINVOICE_PERIOD = _viewModel.loComboBoxDataCINVOICE_PERIOD.First().CCODE;
                _viewModel.loTempListChargesInfoCalUnit = _viewModel.loListChargesInfoCalUnit;
                _viewModel.loListChargesInfoCalUnit = new ObservableCollection<PMT01500FrontChargesInfo_FeeCalculationDetailDTO>();
                _lControlCRUD = true;
                _viewModel._cCurrencyCode = _viewModel.loParameterList.CCURRENCY_CODE!;
                //await _componentCBUILDING_IDTextBox.FocusAsync();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        private async Task SetOther(R_SetEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                _oEventCallBack.LContractorOnCRUDmode = _lControlButton = eventArgs.Enable;
                if (_viewModel.loListChargesInfo.Any())
                {
                    _lControlCRUD = !eventArgs.Enable;
                    if (_lControlButton)
                        _lControlUsingForTotalArea = false;
                    else
                        _lControlUsingForTotalArea = _viewModel.loParameterList.CCHARGE_MODE == "01";
                }
                else
                {
                    _lControlCRUD = _lControlUsingForTotalArea = _lControlButton = false;
                }
                //_oEventCallBack.CREF_NO = _viewModel.loParameterList.CREF_NO!;
                await InvokeTabEventCallbackAsync(_oEventCallBack);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        private void R_Display(R_DisplayEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                var loData = (PMT01500FrontChargesInfoDetailDTO)eventArgs.Data;
                switch (eventArgs.ConductorMode)
                {
                    case R_eConductorMode.Normal:
                        if (loData.DEND_DATE != null)
                        {
                            OnChangedDEND_DATE(loData.DEND_DATE);
                        }
                        //await _gridDeposit.R_RefreshGrid(null);
                        break;
                    case R_eConductorMode.Edit:
                        //Focus Async
                        _viewModel._nTempFEE_AMT = 0;
                        OnChangedDEND_DATE(loData.DEND_DATE);
                        //await _NCOMMON_AREA_SIZENumericTextBox.FocusAsync();
                        break;
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        public async void BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                switch (eventArgs.ConductorMode)
                {
                    case R_eConductorMode.Edit:
                        if (_viewModel.Data.LCAL_UNIT)
                        {
                            await _gridListCalUnitRef.R_RefreshGrid(null);
                        }
                        break;
                    case R_eConductorMode.Add:
                        _viewModel.loListChargesInfoCalUnit = _viewModel.loTempListChargesInfoCalUnit.Any() ? _viewModel.loTempListChargesInfoCalUnit : new ObservableCollection<PMT01500FrontChargesInfo_FeeCalculationDetailDTO>(); ;
                        _viewModel.loTempListChargesInfoCalUnit = new ObservableCollection<PMT01500FrontChargesInfo_FeeCalculationDetailDTO>();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #region Master CRUD

        private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                PMT01500ChargesInfoListDTO loData = R_FrontUtility.ConvertObjectToObject<PMT01500ChargesInfoListDTO>(eventArgs.Data);
                PMT01500FrontChargesInfoDetailDTO loParam = new PMT01500FrontChargesInfoDetailDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _viewModel.loParameterList.CPROPERTY_ID,
                    CDEPT_CODE = _viewModel.loParameterList.CDEPT_CODE,
                    CREF_NO = _viewModel.loParameterList.CREF_NO,
                    CTRANS_CODE = _viewModel.loParameterList.CTRANS_CODE,
                    CSEQ_NO = loData.CSEQ_NO,
                    CUSER_ID = _clientHelper.UserId
                };

                await _viewModel.GetEntity(loParam);
                if (_viewModel.loEntityChargesInfo != null)
                    _viewModel._cCurrencyCode = _viewModel.loParameterList.CCURRENCY_CODE!;
                else
                    _viewModel._cCurrencyCode = "";

                if (_viewModel.loEntityChargesInfo.LCAL_UNIT)
                {
                    if (!string.IsNullOrEmpty(loData.CSEQ_NO))
                    {
                        _viewModel._cTempSeqNo = loData.CSEQ_NO;
                        await _gridListCalUnitRef.R_RefreshGrid(null);
                    }
                    else
                    {
                        loEx.Add("ErrDev", "SeqNo It's Not Supplied");
                    }
                }

                eventArgs.Result = _viewModel.loEntityChargesInfo;
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
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01500FrontChargesInfoDetailDTO>(eventArgs.Data);
                loParam.ODATA_FEE_CALCULATION = new List<PMT01500FrontChargesInfo_FeeCalculationDetailDTO>(_viewModel.loListChargesInfoCalUnit);
                await _viewModel.ServiceSave(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel.loEntityChargesInfo;
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
                var loData = (PMT01500FrontChargesInfoDetailDTO)eventArgs.Data;

                await _viewModel.GetEntity(loData);

                if (_viewModel.loEntityChargesInfo != null)
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

        #region Lookup Button ChargeId Lookup

        private R_Lookup? R_LookupChargeIdLookup;

        private void BeforeOpenLookUpChargeIdLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00200ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.loParameterList.CPROPERTY_ID))
            {
                param = new LML00200ParameterDTO
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CPROPERTY_ID = _viewModel.loParameterList.CPROPERTY_ID,
                    CCHARGE_TYPE_ID = ""
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00200);
        }

        private void AfterOpenLookUpChargeIdLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            LML00200DTO? loTempResult = null;
            //LMM01500AgreementDetailDTO? loGetData = null;


            try
            {
                loTempResult = (LML00200DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //loGetData = (LMM01500AgreementDetailDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _viewModel.Data.CCHARGES_ID = loTempResult.CCHARGES_ID;
                _viewModel.Data.CCHARGES_NAME = loTempResult.CCHARGES_NAME;

                _viewModel.Data.CCHARGES_TYPE = loTempResult.CCHARGES_TYPE;
                _viewModel.Data.CCHARGES_TYPE_DESCR = loTempResult.CCHARGES_TYPE_DESCR;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusChargeId()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT01500FrontChargesInfoDetailDTO loGetData = (PMT01500FrontChargesInfoDetailDTO)_viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CCHARGES_ID))
                {
                    loGetData.CCHARGES_ID = "";
                    loGetData.CCHARGES_NAME = "";
                    loGetData.CCHARGES_TYPE = "";
                    loGetData.CCHARGES_TYPE_DESCR = "";
                    return;
                }

                LookupLML00200ViewModel loLookupViewModel = new LookupLML00200ViewModel();
                LML00200ParameterDTO loParam = new LML00200ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CPROPERTY_ID = _viewModel.loParameterList.CPROPERTY_ID!,
                    CCHARGE_TYPE_ID = "",
                    CSEARCH_TEXT = loGetData.CCHARGES_ID ?? "",
                };

                var loResult = await loLookupViewModel.GetUnitCharges(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CCHARGES_ID = "";
                    loGetData.CCHARGES_NAME = "";
                    loGetData.CCHARGES_TYPE = "";
                    loGetData.CCHARGES_TYPE_DESCR = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CCHARGES_ID = loResult.CCHARGES_ID;
                    loGetData.CCHARGES_NAME = loResult.CCHARGES_NAME;
                    loGetData.CCHARGES_TYPE = loResult.CCHARGES_TYPE;
                    loGetData.CCHARGES_TYPE_DESCR = loResult.CCHARGES_TYPE_DESCR;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #region Lookup Button Tax Code Lookup

        private R_Lookup? R_LookupTaxCodeLookup;

        private void BeforeOpenLookUpTaxCodeLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00110ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.loParameterList.CPROPERTY_ID))
            {
                param = new GSL00110ParameterDTO
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CTAX_DATE = _viewModel.loParameterList.CREF_DATE!,
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00110);
        }

        private void AfterOpenLookUpTaxCodeLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL00110DTO? loTempResult = null;
            //LMM01500AgreementDetailDTO? loGetData = null;


            try
            {
                loTempResult = (GSL00110DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //loGetData = (LMM01500AgreementDetailDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _viewModel.Data.CTAX_ID = loTempResult.CTAX_ID;
                _viewModel.Data.CTAX_NAME = loTempResult.CTAX_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusTaxCode()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT01500FrontChargesInfoDetailDTO loGetData = (PMT01500FrontChargesInfoDetailDTO)_viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CTAX_ID))
                {
                    loGetData.CTAX_ID = "";
                    loGetData.CTAX_NAME = "";
                    return;
                }

                LookupGSL00110ViewModel loLookupViewModel = new LookupGSL00110ViewModel();
                GSL00110ParameterDTO loParam = new GSL00110ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CTAX_DATE = _viewModel.loParameterList.CREF_DATE!,
                    CSEARCH_TEXT = loGetData.CTAX_ID ?? "",
                };

                var loResult = await loLookupViewModel.GetTaxByDate(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CTAX_ID = "";
                    loGetData.CTAX_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CTAX_ID = loResult.CTAX_ID;
                    loGetData.CTAX_NAME = loResult.CTAX_NAME;
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

        #region Cal Unit

        private bool _lControlCRUD;

        private async Task ServiceGetCalUnitListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetChargesInfoCalUnitList();
                eventArgs.ListEntityResult = _viewModel.loListChargesInfoCalUnit;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_DisplayCalUnit(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var data = (PMT01500FrontChargesInfo_FeeCalculationDetailDTO)eventArgs.Data;
                if (data != null)
                {
                    /*
                    if (_viewModel.CenterListData != null)
                    {
                        var firstCenter = _viewModel.CenterListData.FirstOrDefault();
                        data.CCENTER_CODE = firstCenter.CCENTER_CODE;
                    }
                    data.CDBCR = data.NDEBIT > 0 ? "D" : "C";
                    data.NAMOUNT = data.NDEBIT + data.NCREDIT;

                    data.CDOCUMENT_DATE = _viewModel.Ddocdate.ToString("yyyyMMdd");
                    data.CDOCUMENT_NO = _viewModel.Data.CDOC_NO;
                    if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                    {
                        if (_gridDetailRef.DataSource.Count > 0)
                        {
                            loHeaderData.NDEBIT_AMOUNT = _gridDetailRef.DataSource.Sum(x => x.NDEBIT);
                            loHeaderData.NCREDIT_AMOUNT = _gridDetailRef.DataSource.Sum(x => x.NCREDIT);
                        }
                    }
                    loHeaderData.NTRANS_AMOUNT = loHeaderData.NDEBIT_AMOUNT + loHeaderData.NCREDIT_AMOUNT;
                    */
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void ServiceGetCalUnitRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }

        private void R_ValidationCalUnit(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var data = (PMT01500FrontChargesInfo_FeeCalculationDetailDTO)eventArgs.Data;
                if (_viewModel.Data.LCAL_UNIT)
                {
                    if (string.IsNullOrWhiteSpace(data.CUNIT_ID))
                    {
                        loEx.Add("", "Unit ID is required!");
                    }
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            eventArgs.Cancel = loEx.HasError;
            loEx.ThrowExceptionIfErrors();
        }

        private void After_AddCalUnit(R_AfterAddEventArgs eventArgs)
        {
            var data = (PMT01500FrontChargesInfo_FeeCalculationDetailDTO)eventArgs.Data;
            /*
            if (_viewModel.JournaDetailList.Any())
            {
                // Find the maximum INO value in the list and increment it by 1
                int maxINO = _viewModel.JournaDetailList.Max(item => item.INO);
                data.INO = maxINO + 1;
            }
            else
            {
                // If the list is empty, set INO to 1 (or another initial value)
                data.INO = 1;
            }
            */
            data.CBUILDING_ID = _viewModel.loParameterList.CBUILDING_ID;
            eventArgs.Data = data;
        }


        private void R_ServiceSaveCalUnit(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                /*
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01500FrontChargesInfoDetailDTO>(eventArgs.Data);
                loParam.ODATA_FEE_CALCULATION = new List<PMT01500FrontChargesInfo_FeeCalculationDetailDTO>(_viewModel.loListChargesInfoCalUnit);
                await _viewModel.ServiceSave(loParam, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.loEntityChargesInfo;
                */
                eventArgs.Result = eventArgs.Data;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_ServiceDeleteCalUnit(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                /*
                var loData = (PMT01500FrontChargesInfoDetailDTO)eventArgs.Data;

                await _viewModel.GetEntity(loData);

                if (_viewModel.loEntityChargesInfo != null)
                    await _viewModel.ServiceDelete(loData);
                */

                var test = eventArgs.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        #region CalUnit LookUp

        private void Before_Open_LookupCalUnit(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var param = new GSL02300ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.loParameterList.CPROPERTY_ID!,
                    CBUILDING_ID = _viewModel.loParameterList.CBUILDING_ID!,
                    CFLOOR_ID = "",
                };
                eventArgs.Parameter = param;
                eventArgs.TargetPageType = typeof(GSL02300);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }

        private void After_Open_LookupCalUnit(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loTempResult = (GSL02300DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;
                var loGetData = (PMT01500FrontChargesInfo_FeeCalculationDetailDTO)eventArgs.ColumnData;
                loGetData.CUNIT_ID = loTempResult.CUNIT_ID;
                loGetData.CFLOOR_ID = loTempResult.CFLOOR_ID;
                loGetData.CBUILDING_ID = loTempResult.CBUILDING_ID;
                loGetData.NTOTAL_AREA = loTempResult.NACTUAL_AREA_SIZE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #endregion

    }
}
