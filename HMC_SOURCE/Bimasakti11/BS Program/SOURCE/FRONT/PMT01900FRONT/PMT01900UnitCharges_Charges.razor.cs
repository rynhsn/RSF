using BaseAOC_BS11Common.DTO.Response.GridList;
using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00200;
using Microsoft.AspNetCore.Components;
using PMT01900Common.DTO.CRUDBase;
using PMT01900Common.DTO.Front;
using PMT01900FrontResources;
using PMT01900Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System.Collections.ObjectModel;

namespace PMT01900Front
{
    public partial class PMT01900UnitCharges_Charges : R_Page
    {
        private PMT01900Charges_ChargesViewModel _viewModel = new();
        private R_Conductor? _conductorRef;
        private R_Grid<BaseAOCResponseAgreementChargesListDTO>? _gridListRef;
        private R_ConductorGrid? _conductorCalUnitRef;
        private R_Grid<BaseAOCResponseAgreementChargesItemsListDTO>? _gridListCalUnitRef;
        [Inject] private IClientHelper? _clientHelper { get; set; }
        [Inject] public R_PopupService? PopupService { get; set; }
        [Inject] private R_ILocalizer<Resources_PMT01900_Class>? _localizer { get; set; }

        PMT01900EventCallBackDTO _oEventCallBack = new PMT01900EventCallBackDTO();

        private bool _lControlButton = true;
        private bool _lIsDataExist;
        private bool _lControlChargesItem;
        private bool _lControlUsingForTotalArea;

        /*
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

        */

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.loParameterList = R_FrontUtility.ConvertObjectToObject<PMT01900ParameterFrontChangePageToChargesDTO>(poParameter);

                await _viewModel.GetComboBoxDataCFEE_METHOD();
                //await _viewModel.GetComboBoxDataCINVOICE_PERIOD();
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
                var loData = (PMT01900Charges_ChagesInfoDetailDTO)eventArgs.Data;

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
                        Program_Id = "PMT01900",
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
                        Program_Id = "PMT01900",
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
                var loData = (PMT01900Charges_ChagesInfoDetailDTO)_viewModel.Data;
                var llControl = _viewModel._oControlYMD;
                loData.IYEAR = poParam;

                if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
                {
                    if (loData.IYEAR == 0 && loData.IMONTH == 0 && loData.IDAYS == 0)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEAR).AddMonths(loData.IMONTH).AddDays(loData.IDAYS).AddDays(-1);
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
                var loData = (PMT01900Charges_ChagesInfoDetailDTO)_viewModel.Data;
                var llControl = _viewModel._oControlYMD;
                loData.IMONTH = poParam;

                if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
                {
                    if (loData.IYEAR == 0 && loData.IMONTH == 0 && loData.IDAYS == 0)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEAR).AddMonths(loData.IMONTH).AddDays(loData.IDAYS).AddDays(-1);
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
                var loData = (PMT01900Charges_ChagesInfoDetailDTO)_viewModel.Data;
                var llControl = _viewModel._oControlYMD;
                loData.IDAYS = poParam;

                if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
                {
                    if (loData.IYEAR == 0 && loData.IMONTH == 0 && loData.IDAYS == 0)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEAR).AddMonths(loData.IMONTH).AddDays(loData.IDAYS).AddDays(-1);
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

        private void OnChangedDSTART_DATE(DateTime? poValue)
        {
            R_Exception loException = new R_Exception();

            try
            {
                PMT01900Charges_ChagesInfoDetailDTO loData = _viewModel.Data;
                loData.DSTART_DATE = poValue;

                if (loData.DEND_DATE == null)
                {
                    loData.DEND_DATE = loData.IYEAR == 0 && loData.IMONTH == 0 && loData.IDAYS == 0
                        ? loData.DSTART_DATE
                        : loData.DSTART_DATE!.Value.AddYears(loData.IYEAR).AddMonths(loData.IMONTH).AddDays(loData.IDAYS).AddDays(-1);
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
                PMT01900Charges_ChagesInfoDetailDTO loData = _viewModel.Data;
                loData.DEND_DATE = poValue;

                if (loData.DSTART_DATE == null)
                {
                    loData.DSTART_DATE = loData.IYEAR == 0 && loData.IMONTH == 0 && loData.IDAYS == 0
                        ? loData.DEND_DATE
                        : loData.DEND_DATE!.Value.AddYears(loData.IYEAR).AddMonths(loData.IMONTH).AddDays(loData.IDAYS).AddDays(-1);
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
            PMT01900Charges_ChagesInfoDetailDTO loData = _viewModel.Data;

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
                    loData.IDAYS = 1;
                    goto EndBlocks;
                }
                DateTime dValueEndDate = poEndDate!.Value.AddDays(1);

                int liChecker = poEndDate!.Value.Day - poStartDate!.Value.Day;


                loData.IDAYS = dValueEndDate.Day - poStartDate!.Value.Day;
                if (loData.IDAYS < 0)
                {
                    DateTime dValueEndDateForHandleDay = dValueEndDate.AddMonths(-1);
                    int liTempDayinMonth = DateTime.DaysInMonth(dValueEndDateForHandleDay.Year, dValueEndDateForHandleDay.Month);
                    loData.IDAYS = liTempDayinMonth + loData.IDAYS;
                    if (loData.IDAYS < 0) { loException.Add("ErrDev", "Value is negative!"); }
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


        #region Conductor Function

        private async Task Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (PMT01900Charges_ChagesInfoDetailDTO)eventArgs.Data;

                //Validation if user on CRUD MODE in CalUnit Grid
                if (_lControlChargesItem)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01900_Class), "ValidationChargesItem");
                    loEx.Add(loErr);
                    goto EndBlock;
                }


                if (eventArgs.ConductorMode == R_eConductorMode.Edit)
                {
                    if (_viewModel.loListChargesInfoCalUnit.Any() && !loData.LCAL_UNIT)
                    {
                        var llFalse = await R_MessageBox.Show("", "All Detail Items Data will be deleted, Are you sure!",
                R_eMessageBoxButtonType.OKCancel);
                        switch (llFalse)
                        {
                            case R_eMessageBoxResult.Cancel:
                                eventArgs.Cancel = true;
                                break;
                            case R_eMessageBoxResult.OK:
                                _viewModel.Data.ODATA_CHARGES_ITEM = null;
                                //dibuat null, soalnya kalo NFEE nya pertama mati, bakal keluar ini lagi
                                break;
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(loData.CCHARGES_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01900_Class), "ValidationCharge");
                    loEx.Add(loErr);
                }
                if (loData.IYEAR <= 0 && loData.IMONTH <= 0 && loData.IDAYS <= 0)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01900_Class), "ValidationTenure");
                    loEx.Add(loErr);
                }
                if (!loData.LCAL_UNIT)
                {
                    if (loData.NFEE_AMT <= 0)
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01900_Class), "ValidationFeeAmount");
                        loEx.Add(loErr);
                    }
                }
                else
                {
                    if (!_viewModel.loListChargesInfoCalUnit.Any())
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01900_Class), "ValidationFeeCalculation");
                        loEx.Add(loErr);
                    }
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
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
                var loData = (PMT01900Charges_ChagesInfoDetailDTO)eventArgs.Data;

                loData.DSTART_DATE = _viewModel._AOCService.ConvertStringToDateTimeFormat(_viewModel.loParameterList.CSTART_DATE);
                loData.DEND_DATE = _viewModel._AOCService.ConvertStringToDateTimeFormat(_viewModel.loParameterList.CEND_DATE);
                loData.IYEAR = _viewModel.loParameterList.IYEARS;
                loData.IMONTH = _viewModel.loParameterList.IMONTHS;
                loData.IDAYS = _viewModel.loParameterList.IDAYS;
                loData.LACTIVE = false;
                loData.CBILLING_MODE = _viewModel.loRadioGroupDataCBILLING_MODE.First().CCODE;
                loData.CFEE_METHOD = _viewModel.loComboBoxDataCFEE_METHOD.First().CCODE;
                loData.CINVOICE_PERIOD = _viewModel.loComboBoxDataCFEE_METHOD.First().CCODE;
                _viewModel.loTempListChargesInfoCalUnit = _viewModel.loListChargesInfoCalUnit;
                _viewModel.loListChargesInfoCalUnit = new ObservableCollection<BaseAOCResponseAgreementChargesItemsListDTO>();
                _lControlCRUD = true;
                //_viewModel._cCurrencyCode = _viewModel.loParameterList.CCURRENCY_CODE!; --Cari tau ini darimana!
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
                //_viewModel.lControlCRUDMode = eventArgs.Enable;
                _oEventCallBack.LCRUD_MODE = eventArgs.Enable;
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
                var loData = (PMT01900Charges_ChagesInfoDetailDTO)eventArgs.Data;
                switch (eventArgs.ConductorMode)
                {
                    case R_eConductorMode.Normal:
                        //if (loData.DEND_DATE != null)
                        //{
                        //OnChangedDEND_DATE(loData.DEND_DATE);
                        //}
                        //else
                        //{
                        //goto EndBlocks;
                        //}
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

            //EndBlocks:
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
                        _viewModel.loListChargesInfoCalUnit = _viewModel.loTempListChargesInfoCalUnit.Any() ? _viewModel.loTempListChargesInfoCalUnit : new ObservableCollection<BaseAOCResponseAgreementChargesItemsListDTO>();
                        _viewModel.loTempListChargesInfoCalUnit = new ObservableCollection<BaseAOCResponseAgreementChargesItemsListDTO>();
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
                _viewModel.LTAXABLE = false;
                BaseAOCResponseAgreementChargesListDTO loData = R_FrontUtility.ConvertObjectToObject<BaseAOCResponseAgreementChargesListDTO>(eventArgs.Data);
                PMT01900Charges_ChagesInfoDetailDTO loParam = new PMT01900Charges_ChagesInfoDetailDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _viewModel.loParameterList.CPROPERTY_ID,
                    CDEPT_CODE = _viewModel.loParameterList.CDEPT_CODE,
                    CREF_NO = _viewModel.loParameterList.CREF_NO,
                    CTRANS_CODE = _viewModel.loParameterList.CTRANS_CODE,
                    CSEQ_NO = loData.CSEQ_NO!,
                    CUSER_ID = _clientHelper.UserId
                };

                await _viewModel.GetEntity(loParam);
                if (_viewModel.loEntityChargesInfo != null)
                    //_viewModel._cCurrencyCode = _viewModel.loParameterList.CCURRENCY_CODE!; --Caritau ini darimana
                    _viewModel._cCurrencyCode = "";
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
                /*
                if (loData != null)
                    OnChangedDEND_DATE(loData.DEND_DATE);
                */


                eventArgs.Result = _viewModel.loEntityChargesInfo;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceSaveCharges(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01900Charges_ChagesInfoDetailDTO>(eventArgs.Data);

                if (!loParam.LCAL_UNIT)
                {
                    loParam.NINVOICE_AMT = 0;
                }

                //var loDataChargesItems = R_FrontUtility.ConvertCollectionToCollection<PMT01900Charges_ChargesInfoDetail_ChargesItemDTO>(loParam.ODATA_CHARGES_ITEM);
                //loParam.ODATA_CHARGES_ITEM = new List<PMT01900Charges_ChargesInfoDetail_ChargesItemDTO>(loDataChargesItems);
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
                var loData = (PMT01900Charges_ChagesInfoDetailDTO)eventArgs.Data;

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
            R_Exception loException = new R_Exception();


            try
            {
                LML00200ParameterDTO? param = null;
                if (!string.IsNullOrEmpty(_viewModel.loParameterList.CPROPERTY_ID))
                {
                    param = new LML00200ParameterDTO
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                        CPROPERTY_ID = _viewModel.loParameterList.CPROPERTY_ID,
                        CCHARGE_TYPE_ID = "01,02,05"
                    };
                }
                eventArgs.Parameter = param;
                eventArgs.TargetPageType = typeof(LML00200);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
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

                _viewModel.Data.CCHARGES_ID = loTempResult.CCHARGES_ID;
                _viewModel.Data.CCHARGES_NAME = loTempResult.CCHARGES_NAME;
                _viewModel.Data.CCHARGES_TYPE = loTempResult.CCHARGES_TYPE;
                _viewModel.Data.CCHARGES_TYPE_DESCR = loTempResult.CCHARGES_TYPE_DESCR;
                _viewModel.LTAXABLE = loTempResult.LTAXABLE;
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
                PMT01900Charges_ChagesInfoDetailDTO loGetData = (PMT01900Charges_ChagesInfoDetailDTO)_viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CCHARGES_ID))
                {
                    loGetData.CCHARGES_ID = "";
                    loGetData.CCHARGES_NAME = "";
                    loGetData.CCHARGES_TYPE = "";
                    loGetData.CCHARGES_TYPE_DESCR = "";
                    _viewModel.LTAXABLE = false;
                    return;
                }

                LookupLML00200ViewModel loLookupViewModel = new LookupLML00200ViewModel();
                LML00200ParameterDTO loParam = new LML00200ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CPROPERTY_ID = _viewModel.loParameterList.CPROPERTY_ID!,
                    CCHARGE_TYPE_ID = "01,02,05",
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
                    _viewModel.LTAXABLE = false;
                }
                else
                {
                    loGetData.CCHARGES_ID = loResult.CCHARGES_ID;
                    loGetData.CCHARGES_NAME = loResult.CCHARGES_NAME;
                    loGetData.CCHARGES_TYPE = loResult.CCHARGES_TYPE;
                    loGetData.CCHARGES_TYPE_DESCR = loResult.CCHARGES_TYPE_DESCR;
                    _viewModel.LTAXABLE = loResult.LTAXABLE;
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
                    CTAX_DATE = _viewModel._AOCService.ConvertDateTimeToStringFormat(ptEntity: DateTime.Now)!,
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

                //loGetData = (LMM01500AgreementDetailDTO)_conductorFullPMT02500Agreement.R_GetCurrentData();

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
                PMT01900Charges_ChagesInfoDetailDTO loGetData = (PMT01900Charges_ChagesInfoDetailDTO)_viewModel.Data;

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
                    CTAX_DATE = _viewModel._AOCService.ConvertDateTimeToStringFormat(ptEntity: DateTime.Now)!,
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
        #region Currency
        private R_Lookup? R_LookupCurrencyLookup;
        private void BeforeOpenLookUpCurrencyLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00300ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.loParameterList.CPROPERTY_ID))
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
                _viewModel._cCurrencyCode = loTempResult.CCURRENCY_CODE;
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
                var loGetData = _viewModel.Data;

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
                    _viewModel._cCurrencyCode = loResult.CCURRENCY_CODE;
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
                _lControlButton = _viewModel.loListChargesInfo.Any();
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
                var data = (BaseAOCResponseAgreementChargesItemsListDTO)eventArgs.Data;
                var loHeaderData = _viewModel.Data;
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    if (_gridListCalUnitRef.DataSource.Count > 0)
                    {
                        loHeaderData.NINVOICE_AMT = ((_gridListCalUnitRef.DataSource.Sum(x => x.NTOTAL_PRICE)));
                    }
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
                var data = (BaseAOCResponseAgreementChargesItemsListDTO)eventArgs.Data;
                if (_viewModel.Data.LCAL_UNIT)
                {
                    if (string.IsNullOrWhiteSpace(data.CITEM_NAME))
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

        }

        private void R_ServiceSaveCalUnit(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                /*
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01900Charges_ChagesInfoDetailDTO>(eventArgs.Data);
                loParam.ODATA_CHARGES_ITEM = new List<BaseAOCResponseAgreementChargesItemsListDTO>(_viewModel.loListChargesInfoCalUnit);
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
                var loData = (PMT01900Charges_ChagesInfoDetailDTO)eventArgs.Data;

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

        private void R_CellLostFocused(R_CellValueChangedEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                var lnTotalPrice = (BaseAOCResponseAgreementChargesItemsListDTO)eventArgs.CurrentRow;
                //.FirstOrDefault(x => x.Name == "Total Price");
                var liQuantity = (BaseAOCResponseAgreementChargesItemsListDTO)eventArgs.CurrentRow;
                //.FirstOrDefault(x => x.Name == "Qty");
                var lnUnitPrice = (BaseAOCResponseAgreementChargesItemsListDTO)eventArgs.CurrentRow;
                //.FirstOrDefault(x => x.Name == "Unit Price");
                var lnDiscount = (BaseAOCResponseAgreementChargesItemsListDTO)eventArgs.CurrentRow;
                //.FirstOrDefault(x => x.Name == "Discount");

                if (eventArgs.ColumnName != "Item Name")
                {

                    double qty = Convert.ToDouble(liQuantity);
                    double unitPrice = Convert.ToDouble(lnUnitPrice);
                    double discount = Convert.ToDouble(lnDiscount) / 100; // Jika diskon dalam persentase

                    //var loData = (BaseAOCResponseAgreementChargesItemsListDTO)_conductorCalUnitRef.R_GetCurrentData();
                    // Menghitung Harga Total dengan Diskon
                    lnTotalPrice.NTOTAL_PRICE = (decimal)(qty * unitPrice * (1 - discount));
                    //loData.NTOTAL_PRICE = (decimal)(qty * unitPrice * (1 - discount));
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
        private void SetOtherCalUnit(R_SetEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                _lControlChargesItem = !eventArgs.Enable;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            R_DisplayException(loException);
        }

        #endregion
    }
}
