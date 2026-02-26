using BlazorClientHelper;
using FAM00100Common.DTOs.FAM00100;
using FAM00100FrontResources;
using FAM00100Model.ViewModels;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_LockingFront;

namespace FAM00100Front
{
    public partial class FAM00100 : R_Page
    {
        #region Inject
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<FAM00100FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] private R_PopupService PopupService { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }
        #endregion

        private FAM00100ViewModel _viewModel = new FAM00100ViewModel();
        private R_Conductor _conductorRef;
        private R_TextBox _CurrencyRateType;

        protected async override Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            R_PopupResult loResult = null;

            try
            {
                var loValidate = await _viewModel.GetInitialValidate();
                if (loValidate == null)
                {
                    loResult = await PopupService.Show(typeof(FAM00110), null);
                    if (loResult.Success == false)
                    {
                        await this.CloseProgramAsync();
                    }
                }
                else
                {
                    await _viewModel.GetInitialProcess();
                    await _conductorRef.R_GetEntity(null);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region Locking
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlFA";
        private const string DEFAULT_MODULE_NAME = "FA";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (FAM00100DTO)eventArgs.Data;

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
                        Program_Id = "FAM00100",
                        Table_Name = "FAM_SYSTEM_PARAM",
                        Key_Value = string.Join("|", clientHelper.CompanyId)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "FAM00100",
                        Table_Name = "FAM_SYSTEM_PARAM",
                        Key_Value = string.Join("|", clientHelper.CompanyId)
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

        #region Form
        private async Task SystemParamCB_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetSystemParamCB();

                // Set INT values on FASystemParam before setting as result
                if (!string.IsNullOrEmpty(_viewModel.FASystemParam.CCURRENT_PERIOD_YY))
                {
                    _viewModel.FASystemParam.CCURRENT_PERIOD_YY_INT = int.Parse(_viewModel.FASystemParam.CCURRENT_PERIOD_YY);
                }
                if (!string.IsNullOrEmpty(_viewModel.FASystemParam.CSOFT_PERIOD_YY))
                {
                    _viewModel.FASystemParam.CSOFT_PERIOD_YY_INT = int.Parse(_viewModel.FASystemParam.CSOFT_PERIOD_YY);
                }

                // Conductor will automatically populate Data from Result
                eventArgs.Result = _viewModel.FASystemParam;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task SystemParamCB_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
            {
                //if (true)
                //{
                //    await _CurrencyRateType.FocusAsync();
                //}
            }
        }
        private void SystemParamCB_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;
                var loData = (FAM00100DTO)eventArgs.Data;

                lCancel = string.IsNullOrEmpty(loData.CRATETYPE_CODE);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V01"));
                }
                if (string.IsNullOrEmpty(loData.CTRANS_DEPT_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V11"));
                }
                //if (string.IsNullOrEmpty(loData.CASSET_DEPT_CODE))
                //{
                //    loEx.Add(R_FrontUtility.R_GetError(
                //        typeof(Resources_Dummy_Class),
                //        "V12"));
                //}

                bool softPeriodLater = CompareSoftPeriod(_viewModel.ICLinkDate, loData.CSOFT_PERIOD_YY_INT, loData.CSOFT_PERIOD_MM);

                if (_viewModel.Data.LICLINK && softPeriodLater)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V04"));
                }

                softPeriodLater = CompareSoftPeriod(_viewModel.PJLinkDate, loData.CSOFT_PERIOD_YY_INT, loData.CSOFT_PERIOD_MM);

                if (_viewModel.Data.LPJLINK && softPeriodLater)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V05"));
                }

                softPeriodLater = CompareSoftPeriod(_viewModel.GLLinkDate, loData.CSOFT_PERIOD_YY_INT, loData.CSOFT_PERIOD_MM);

                if (_viewModel.Data.LGLLINK && softPeriodLater)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V06"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task SystemParamCB_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.SaveSystemParamCB((FAM00100DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                // Set INT values on FASystemParam after save
                if (!string.IsNullOrEmpty(_viewModel.FASystemParam.CCURRENT_PERIOD_YY))
                {
                    _viewModel.FASystemParam.CCURRENT_PERIOD_YY_INT = int.Parse(_viewModel.FASystemParam.CCURRENT_PERIOD_YY);
                }
                if (!string.IsNullOrEmpty(_viewModel.FASystemParam.CSOFT_PERIOD_YY))
                {
                    _viewModel.FASystemParam.CSOFT_PERIOD_YY_INT = int.Parse(_viewModel.FASystemParam.CSOFT_PERIOD_YY);
                }

                // Conductor will automatically populate Data from Result
                eventArgs.Result = _viewModel.FASystemParam;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task SystemParamCB_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_N01"], R_eMessageBoxButtonType.YesNo);
                //if (loValidate == R_eMessageBoxResult.Yes)
                //{
                //    // Restore the original entity from FASystemParam
                //    // Set INT values before restoring
                //    if (!string.IsNullOrEmpty(_viewModel.FASystemParam.CCURRENT_PERIOD_YY))
                //    {
                //        _viewModel.FASystemParam.CCURRENT_PERIOD_YY_INT = int.Parse(_viewModel.FASystemParam.CCURRENT_PERIOD_YY);
                //    }
                //    if (!string.IsNullOrEmpty(_viewModel.FASystemParam.CSOFT_PERIOD_YY))
                //    {
                //        _viewModel.FASystemParam.CSOFT_PERIOD_YY_INT = int.Parse(_viewModel.FASystemParam.CSOFT_PERIOD_YY);
                //    }
                //}
                eventArgs.Cancel = loValidate == R_eMessageBoxResult.No;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Currency Rate Type Lookup
        private async Task CurrencyRateType_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (FAM00100DTO)_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loData.CRATETYPE_CODE) == false)
                {
                    GSL00800ParameterDTO loParam = new GSL00800ParameterDTO() { CSEARCH_TEXT = loData.CRATETYPE_CODE };

                    LookupGSL00800ViewModel loLookupViewModel = new LookupGSL00800ViewModel();

                    var loResult = await loLookupViewModel.GetCurrencyRateType(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loData.CRATETYPE_DESCRIPTION = "";
                        goto EndBlock;
                    }
                    loData.CRATETYPE_CODE = loResult.CRATETYPE_CODE;
                    loData.CRATETYPE_DESCRIPTION = loResult.CRATETYPE_DESCRIPTION;
                }
                else
                {
                    loData.CRATETYPE_DESCRIPTION = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void R_Before_Open_LookupCurrRateType(R_BeforeOpenLookupEventArgs eventArgs)
        {

            GSL00800ParameterDTO loParam = new GSL00800ParameterDTO();
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00800);
        }

        private void R_After_Open_LookupCurrRateType(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00800DTO loTempResult = (GSL00800DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            var loData = (FAM00100DTO)_conductorRef.R_GetCurrentData();
            loData.CRATETYPE_CODE = loTempResult.CRATETYPE_CODE;
            loData.CRATETYPE_DESCRIPTION = loTempResult.CRATETYPE_DESCRIPTION;
        }
        #endregion

        #region TransDept Lookup
        private async Task TransDept_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.Data.CTRANS_DEPT_CODE) == false)
                {
                    GSL00700ParameterDTO loParam = new GSL00700ParameterDTO()
                    {
                        CSEARCH_TEXT = _viewModel.Data.CTRANS_DEPT_CODE,
                        CCOMPANY_ID = clientHelper.CompanyId,
                        CUSER_ID = clientHelper.UserId,
                    };

                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel();

                    var loResult = await loLookupViewModel.GetDepartment(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.Data.CTRANS_DEPT_NAME = "";
                        goto EndBlock;
                    }
                    _viewModel.Data.CTRANS_DEPT_CODE = loResult.CDEPT_CODE;
                    _viewModel.Data.CTRANS_DEPT_NAME = loResult.CDEPT_NAME;
                }
                else
                {
                    _viewModel.Data.CTRANS_DEPT_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void R_Before_Open_LookupTransDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00700ParameterDTO loParam = new GSL00700ParameterDTO
            {
                CUSER_ID = clientHelper.UserId,
                CCOMPANY_ID = clientHelper.CompanyId
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00700);
        }

        private void R_After_Open_LookupTransDept(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00700DTO loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _viewModel.Data.CTRANS_DEPT_CODE = loTempResult.CDEPT_CODE;
            _viewModel.Data.CTRANS_DEPT_NAME = loTempResult.CDEPT_NAME;
        }
        #endregion

        #region DefaultAsset Lookup
        private async Task DefaultAsset_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.Data.CASSET_DEPT_CODE) == false)
                {
                    GSL00700ParameterDTO loParam = new GSL00700ParameterDTO()
                    {
                        CSEARCH_TEXT = _viewModel.Data.CASSET_DEPT_CODE,
                        CCOMPANY_ID = clientHelper.CompanyId,
                        CUSER_ID = clientHelper.UserId,
                    };

                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel();

                    var loResult = await loLookupViewModel.GetDepartment(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.Data.CASSET_DEPT_NAME = "";
                        goto EndBlock;
                    }
                    _viewModel.Data.CASSET_DEPT_CODE = loResult.CDEPT_CODE;
                    _viewModel.Data.CASSET_DEPT_NAME = loResult.CDEPT_NAME;
                }
                else
                {
                    _viewModel.Data.CASSET_DEPT_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void R_Before_Open_LookupDefaultAsset(R_BeforeOpenLookupEventArgs eventArgs)
        {

            GSL00700ParameterDTO loParam = new GSL00700ParameterDTO
            {
                CUSER_ID = clientHelper.UserId,
                CCOMPANY_ID = clientHelper.CompanyId
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00700);
        }

        private void R_After_Open_LookupDefaultAsset(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00700DTO loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _viewModel.Data.CASSET_DEPT_CODE = loTempResult.CDEPT_CODE;
            _viewModel.Data.CASSET_DEPT_NAME = loTempResult.CDEPT_NAME;
        }
        #endregion

        #region OnChange

        private void OnChangePeriodYear(int param)
        {
            var loEx = new R_Exception();
            try
            {
                _viewModel.Data.CCURRENT_PERIOD_YY_INT = param;
                _viewModel.Data.CCURRENT_PERIOD_YY = param.ToString();
                // Year then month (YYYYMM)
                var currentYy = _viewModel.Data.CCURRENT_PERIOD_YY_INT != 0 ? _viewModel.Data.CCURRENT_PERIOD_YY_INT.ToString() : (_viewModel.Data.CCURRENT_PERIOD_YY ?? "");
                _viewModel.Data.CCURRENT_PERIOD = currentYy + (_viewModel.Data.CCURRENT_PERIOD_MM ?? "").PadLeft(2, '0');
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return;
        }

        private void OnChangePeriodMonth(string param)
        {
            var loEx = new R_Exception();
            try
            {
                _viewModel.Data.CCURRENT_PERIOD_MM = param;
                // Year then month (YYYYMM)
                var currentYy = _viewModel.Data.CCURRENT_PERIOD_YY_INT != 0 ? _viewModel.Data.CCURRENT_PERIOD_YY_INT.ToString() : (_viewModel.Data.CCURRENT_PERIOD_YY ?? "");
                _viewModel.Data.CCURRENT_PERIOD = currentYy + (_viewModel.Data.CCURRENT_PERIOD_MM ?? "").PadLeft(2, '0');
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return;
        }

        private void OnChangeSoftPeriodYear(int param)
        {
            var loEx = new R_Exception();
            try
            {
                _viewModel.Data.CSOFT_PERIOD_YY_INT = param;
                _viewModel.Data.CSOFT_PERIOD_YY = param.ToString();
                // Year then month (YYYYMM)
                var softYy = _viewModel.Data.CSOFT_PERIOD_YY_INT != 0 ? _viewModel.Data.CSOFT_PERIOD_YY_INT.ToString() : (_viewModel.Data.CSOFT_PERIOD_YY ?? "");
                _viewModel.Data.CSOFT_PERIOD = softYy + (_viewModel.Data.CSOFT_PERIOD_MM ?? "").PadLeft(2, '0');
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return;
        }

        private void OnChangeSoftPeriodMonth(string param)
        {
            var loEx = new R_Exception();
            try
            {
                _viewModel.Data.CSOFT_PERIOD_MM = param;
                // Year then month (YYYYMM)
                var softYy = _viewModel.Data.CSOFT_PERIOD_YY_INT != 0 ? _viewModel.Data.CSOFT_PERIOD_YY_INT.ToString() : (_viewModel.Data.CSOFT_PERIOD_YY ?? "");
                _viewModel.Data.CSOFT_PERIOD = softYy + (_viewModel.Data.CSOFT_PERIOD_MM ?? "").PadLeft(2, '0');
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return;
        }

        private void OnIncrementalChanged(bool val)
        {
            _viewModel.Data.LINCREMENT_FLAG = val;
            if (!_viewModel.Data.LINCREMENT_FLAG)
            {
                _viewModel.Data.LBY_DEPT = false;
                _viewModel.Data.IJRNGRP_LENGTH = 0;
                _viewModel.Data.IBY_DEPT_LENGTH = 0;
            }
        }

        #endregion

        #region helper

        private bool CompareSoftPeriod(DateTime? date, int softPeriodYY, string softPeriodMM)
        {
            var loEx = new R_Exception();
            bool llRtn = false;
            try
            {
                int year = softPeriodYY;
                int month = Convert.ToInt32(softPeriodMM);

                if (date.HasValue)
                {
                    bool isValid =
                        year < date.Value.Year ||
                        (year == date.Value.Year && month <= date.Value.Month);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return llRtn;
        }

        #endregion
    }
}
