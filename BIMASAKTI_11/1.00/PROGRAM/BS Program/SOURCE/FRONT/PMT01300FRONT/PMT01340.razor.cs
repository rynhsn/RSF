using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System.Xml.Linq;
using R_BlazorFrontEnd.Interfaces;
using PMT01300COMMON;
using PMT01300MODEL;
using Lookup_PMModel.ViewModel.LML00600;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00500;
using Lookup_PMModel.ViewModel.LML01000;
using Lookup_PMModel.ViewModel.LML01100;
using R_BlazorFrontEnd.Enums;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMModel.ViewModel.LML00200;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls.MessageBox;
using System;

namespace PMT01300FRONT
{
    public partial class PMT01340 : R_Page, R_ITabPage
    {
        #region ViewModel
        private PMT01340ViewModel _viewModel = new PMT01340ViewModel();
        #endregion

        #region Conductor
        private R_Conductor _conductorRef;
        #endregion

        #region Grid
        private R_Grid<PMT01340DTO> _gridLOIDepositListRef;
        #endregion

        #region Inject
        [Inject] private R_ILocalizer<PMT01300FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }
        #endregion

        #region Private Property
        private R_TextBox DepositId_TextBox;
        private R_DatePicker<DateTime?> DepositDate_DatePicker;
        private bool EnableNormalMode = false;
        private bool EnableHasHeaderData = true;
        private bool EnableGreaterClosesSts = true;
        private bool IsAddDataLOI = false;

        private R_TabStrip _TabDepositCharges;
        private R_TabPage _TabCharge;
        #endregion

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01300DTO)poParameter;
                EnableGreaterClosesSts = int.Parse(loData.CTRANS_STATUS) >= 80 == false;
                _viewModel.LOI = loData;
                IsAddDataLOI = loData.LIS_ADD_DATA_LOI;
                PMT01300LOICallBackParameterDTO loCallBackData = new PMT01300LOICallBackParameterDTO { CRUD_MODE = true, SELECTED_DATA_TAB_LOI = _viewModel.LOI, LIS_ADD_DATA_LOI = IsAddDataLOI };
                await InvokeTabEventCallbackAsync(loCallBackData);

                await _viewModel.GetInitialVar();
                await _gridLOIDepositListRef.R_RefreshGrid(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Locking
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_MODULE_NAME = "PM";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (PMT01340DTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                   plSendWithContext: true,
                   plSendWithToken: true,
                   pcHttpClientName: DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMT01340",
                        Table_Name = "PMT_AGREEMENT_DEPOSIT",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO, loData.CSEQ_NO)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMT01340",
                        Table_Name = "PMT_AGREEMENT_DEPOSIT",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO, loData.CSEQ_NO)
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

        #region Tab Refresh
        public async Task RefreshTabPageAsync(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                IsAddDataLOI = false;
                var loData = (PMT01300DTO)poParam;
                if (string.IsNullOrWhiteSpace(loData.CREF_NO) ==  false && string.IsNullOrWhiteSpace(loData.CUNIT_ID) == false)
                {
                    EnableGreaterClosesSts = int.Parse(loData.CTRANS_STATUS) >= 80 == false;
                    await _gridLOIDepositListRef.R_RefreshGrid(loData);
                    _viewModel.LOI = loData;
                }
                else
                {
                    _viewModel.LOI = new PMT01300DTO();
                    if (_gridLOIDepositListRef.DataSource.Count > 0)
                    {
                        _viewModel.R_SetCurrentData(null);
                        _gridLOIDepositListRef.DataSource.Clear();
                    }
                }

                EnableHasHeaderData = string.IsNullOrWhiteSpace(loData.CREF_NO) == false && string.IsNullOrWhiteSpace(loData.CFLOOR_ID) == false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region Deposit Form
        private async Task Deposit_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParameter = R_FrontUtility.ConvertObjectToObject<PMT01340DTO>(eventArgs.Parameter);
                await _viewModel.GetLOIDepositList(loParameter);

                eventArgs.ListEntityResult = _viewModel.LOIDepositGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Deposit_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetLOIDeposit((PMT01340DTO)eventArgs.Data);

                eventArgs.Result = _viewModel.LOI_Deposit;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Deposit_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
                {
                    await DepositDate_DatePicker.FocusAsync();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Deposit_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.SaveLOIDeposit(
                    (PMT01340DTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.LOI_Deposit;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Deposit_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.DeleteLOIDeposit((PMT01340DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Grid_R_SetOther(R_SetEventArgs eventArgs)
        {
            EnableNormalMode = eventArgs.Enable;
            PMT01300LOICallBackParameterDTO loData = new PMT01300LOICallBackParameterDTO { CRUD_MODE = eventArgs.Enable, SELECTED_DATA_TAB_LOI = _viewModel.LOI, LIS_ADD_DATA_LOI = IsAddDataLOI };
            await InvokeTabEventCallbackAsync(loData);
        }
        private async void Deposit_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loData = (PMT01340DTO)eventArgs.Data;

            loData.CREF_NO = _viewModel.LOI.CREF_NO;
            loData.CCURRENCY_CODE = _viewModel.LOI.CCURRENCY_CODE;
            loData.CCURRENCY_NAME = _viewModel.LOI.CCURRENCY_NAME;
            loData.CPROPERTY_ID = _viewModel.LOI.CPROPERTY_ID;
            loData.CDEPT_CODE = _viewModel.LOI.CDEPT_CODE;
            loData.CCONTRACTOR_ID = "";
            loData.CUNIT_ID = _viewModel.LOI.CUNIT_ID;
            loData.CFLOOR_ID = _viewModel.LOI.CFLOOR_ID;
            loData.CBUILDING_ID = _viewModel.LOI.CBUILDING_ID;
            loData.CCHARGE_MODE = _viewModel.LOI.CCHARGE_MODE;
            loData.CDESCRIPTION = "";

            loData.DDEPOSIT_DATE = DateTime.Now;

            await DepositId_TextBox.FocusAsync();
        }
        private void Deposit_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;
                var loData = (PMT01340DTO)eventArgs.Data;

                lCancel = string.IsNullOrWhiteSpace(loData.CDEPOSIT_ID);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT01300FrontResources.Resources_Dummy_Class),
                        "V022"));
                }

                if (loData.LTAXABLE)
                {
                    lCancel = string.IsNullOrWhiteSpace(loData.CTAX_ID);
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(PMT01300FrontResources.Resources_Dummy_Class),
                            "V029"));
                    }
                }

                lCancel = loData.DDEPOSIT_DATE == null;
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT01300FrontResources.Resources_Dummy_Class),
                        "V023"));
                }

                lCancel = string.IsNullOrWhiteSpace(loData.CCURRENCY_CODE);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT01300FrontResources.Resources_Dummy_Class),
                        "V009"));
                }

                lCancel = loData.NDEPOSIT_AMT <= 0;
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT01300FrontResources.Resources_Dummy_Class),
                        "V024"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            eventArgs.Cancel = loEx.HasError;
        }
        private async Task Deposit_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            var res = await R_MessageBox.Show("", _localizer["Q04"],
                R_eMessageBoxButtonType.YesNo);

            eventArgs.Cancel = res == R_eMessageBoxResult.No;
        }
        #endregion

        #region Value Change
        private void CurrencyCode_ValueChanged(string poParam)
        {
            _viewModel.Data.CCURRENCY_CODE = string.IsNullOrWhiteSpace(poParam) ? "" : poParam;
            var loCurrency = _viewModel.VAR_CURRENCY_LIST.FirstOrDefault(x => x.CCURRENCY_CODE == poParam);
            _viewModel.Data.CCURRENCY_NAME = string.IsNullOrWhiteSpace(poParam) ? "" : loCurrency.CCURRENCY_NAME;
        }
        #endregion

        #region Unit Charges Lookup
        private async Task Charges_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.Data.CDEPOSIT_ID) == false)
                {
                    LML00200ParameterDTO loParam = new LML00200ParameterDTO()
                    {
                        CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                        CSEARCH_TEXT = _viewModel.Data.CDEPOSIT_ID,
                        CCHARGE_TYPE_ID = "03"
                    };

                    LookupLML00200ViewModel loLookupViewModel = new LookupLML00200ViewModel();

                    var loResult = await loLookupViewModel.GetUnitCharges(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.Data.CDEPOSIT_NAME = "";
                        goto EndBlock;
                    }

                    _viewModel.Data.CDEPOSIT_ID = loResult.CCHARGES_ID;
                    _viewModel.Data.CDEPOSIT_NAME = loResult.CCHARGES_NAME;
                    _viewModel.Data.LTAXABLE = loResult.LTAXABLE;
                }
                else
                {
                    _viewModel.Data.CDEPOSIT_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void R_Before_Open_LookupCharges(R_BeforeOpenLookupEventArgs eventArgs)
        {
            if (string.IsNullOrWhiteSpace(_viewModel.Data.CPROPERTY_ID))
            {
                return;
            }
            LML00200ParameterDTO loParam = new LML00200ParameterDTO()
            {
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                CCHARGE_TYPE_ID = "03"
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(LML00200);
        }
        private void R_After_Open_LookupCharges(R_AfterOpenLookupEventArgs eventArgs)
        {
            LML00200DTO loTempResult = (LML00200DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _viewModel.Data.CDEPOSIT_ID = loTempResult.CCHARGES_ID;
            _viewModel.Data.CDEPOSIT_NAME = loTempResult.CCHARGES_NAME;
            _viewModel.Data.LTAXABLE = loTempResult.LTAXABLE;
        }
        #endregion

        #region Tax Lookup
        private async Task Tax_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.Data.CTAX_ID) == false)
                {
                    GSL00110ParameterDTO loParam = new GSL00110ParameterDTO()
                    {
                        CSEARCH_TEXT = _viewModel.Data.CTAX_ID,
                        CTAX_DATE = DateTime.Now.ToString("yyyyMMdd"),
                    };

                    LookupGSL00110ViewModel loLookupViewModel = new LookupGSL00110ViewModel();

                    var loResult = await loLookupViewModel.GetTaxByDate(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.Data.CTAX_NAME = "";
                        goto EndBlock;
                    }

                    _viewModel.Data.CTAX_ID = loResult.CTAX_ID;
                    _viewModel.Data.CTAX_NAME = loResult.CTAX_NAME;
                }
                else
                {
                    _viewModel.Data.CTAX_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void R_Before_Open_LookupTax(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00110ParameterDTO loParam = new GSL00110ParameterDTO()
            {
                CTAX_DATE = DateTime.Now.ToString("yyyyMMdd"),
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00110);
        }
        private void R_After_Open_LookupTax(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00110DTO loTempResult = (GSL00110DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _viewModel.Data.CTAX_ID = loTempResult.CTAX_ID;
            _viewModel.Data.CTAX_NAME = loTempResult.CTAX_NAME;
        }
        #endregion
    }
}
