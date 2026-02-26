using BlazorClientHelper;
using FAM00100FrontResources;
using FAM00100Model;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;

namespace FAM00100Front
{
    public partial class FAM00110 : R_Page
    {
        #region Inject
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<FAM00100FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] private IClientHelper _clientHelper { get; set; }
        #endregion

        private FAM00110ViewModel _viewModel = new FAM00110ViewModel();
        private R_TextBox _CurrencyRateType;

        protected async override Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.GetInitialProcess();
                await _CurrencyRateType.FocusAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task BtnCreate_OnClick()
        {
            var loEx = new R_Exception();
            bool llValidate = false;

            try
            {
                int loSoftPeriod = int.Parse(_viewModel.SystemParameterFA.CSOFT_PERIOD_YY_INT.ToString() + _viewModel.SystemParameterFA.CSOFT_PERIOD_MM);
                int loCurrentPeriod = int.Parse(_viewModel.SystemParameterFA.CCURRENT_PERIOD_YY_INT.ToString() + _viewModel.SystemParameterFA.CCURRENT_PERIOD_MM);

                // V01: Currency Rate Type is required
                if (string.IsNullOrEmpty(_viewModel.SystemParameterFA.CRATETYPE_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V01"));
                    llValidate = true;
                }

                // V09: Default Trans. Department is required
                if (string.IsNullOrEmpty(_viewModel.SystemParameterFA.CTRANS_DEPT_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V09"));
                    llValidate = true;
                }

                // V10: Default Asset Department is required
                if (string.IsNullOrEmpty(_viewModel.SystemParameterFA.CASSET_DEPT_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V10"));
                    llValidate = true;
                }

                // V04: Soft Close Period may not be later than IC Link Date Period (when LICLINK=true)
                if (_viewModel.SystemParameterFA.LICLINK)
                {
                    if (_viewModel.ICLinkDate == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "V02"));
                        llValidate = true;
                    }
                    else
                    {
                        int loICLinkPeriod = int.Parse(_viewModel.ICLinkDate.Value.ToString("yyyyMM"));
                        if (loSoftPeriod > loICLinkPeriod)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Resources_Dummy_Class),
                                "V04"));
                            llValidate = true;
                        }
                    }
                }

                // V05: Soft Close Period may not be later than PJ Link Date Period (when LPJLINK=true)
                if (_viewModel.SystemParameterFA.LPJLINK)
                {
                    if (_viewModel.PJLinkDate == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "V03"));
                        llValidate = true;
                    }
                    else
                    {
                        int loPJLinkPeriod = int.Parse(_viewModel.PJLinkDate.Value.ToString("yyyyMM"));
                        if (loSoftPeriod > loPJLinkPeriod)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Resources_Dummy_Class),
                                "V05"));
                            llValidate = true;
                        }
                    }
                }

                // V06: Soft Close Period may not be later than GL Link Date Period (when LGLLINK=true)
                if (_viewModel.SystemParameterFA.LGLLINK)
                {
                    if (_viewModel.GLLinkDate == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "V08"));
                        llValidate = true;
                    }
                    else
                    {
                        int loGLLinkPeriod = int.Parse(_viewModel.GLLinkDate.Value.ToString("yyyyMM"));
                        if (loSoftPeriod > loGLLinkPeriod)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Resources_Dummy_Class),
                                "V06"));
                            llValidate = true;
                        }
                    }
                }

                // V07: Current Period may not be later than Soft Close Period
                if (loCurrentPeriod > loSoftPeriod)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V07"));
                    llValidate = true;
                }

                if (llValidate == false)
                {
                    var loData = await _viewModel.CreateSystemParamFA();
                    await R_MessageBox.Show("", _localizer["_N02"], R_eMessageBoxButtonType.OK);
                    await this.Close(true, loData);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task BtnCancel_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_N01"], R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await this.Close(false, null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region CurrencyRateType Lookup
        private async Task CurrencyRateType_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.SystemParameterFA.CRATETYPE_CODE) == false)
                {
                    GSL00800ParameterDTO loParam = new GSL00800ParameterDTO() { CSEARCH_TEXT = _viewModel.SystemParameterFA.CRATETYPE_CODE };

                    LookupGSL00800ViewModel loLookupViewModel = new LookupGSL00800ViewModel();

                    var loResult = await loLookupViewModel.GetCurrencyRateType(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.SystemParameterFA.CRATETYPE_DESCRIPTION = "";
                        goto EndBlock;
                    }
                    _viewModel.SystemParameterFA.CRATETYPE_CODE = loResult.CRATETYPE_CODE;
                    _viewModel.SystemParameterFA.CRATETYPE_DESCRIPTION = loResult.CRATETYPE_DESCRIPTION;
                }
                else
                {
                    _viewModel.SystemParameterFA.CRATETYPE_DESCRIPTION = "";
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
            _viewModel.SystemParameterFA.CRATETYPE_CODE = loTempResult.CRATETYPE_CODE;
            _viewModel.SystemParameterFA.CRATETYPE_DESCRIPTION = loTempResult.CRATETYPE_DESCRIPTION;
        }
        #endregion

        #region TransDept Lookup
        private async Task TransDept_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.SystemParameterFA.CTRANS_DEPT_CODE) == false)
                {
                    GSL00700ParameterDTO loParam = new GSL00700ParameterDTO()
                    {
                        CSEARCH_TEXT = _viewModel.SystemParameterFA.CTRANS_DEPT_CODE,
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                    };

                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel();

                    var loResult = await loLookupViewModel.GetDepartment(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.SystemParameterFA.CTRANS_DEPT_NAME = "";
                        goto EndBlock;
                    }
                    _viewModel.SystemParameterFA.CTRANS_DEPT_CODE = loResult.CDEPT_CODE;
                    _viewModel.SystemParameterFA.CTRANS_DEPT_NAME = loResult.CDEPT_NAME;
                }
                else
                {
                    _viewModel.SystemParameterFA.CTRANS_DEPT_NAME = "";
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
                CUSER_ID = _clientHelper.UserId,
                CCOMPANY_ID = _clientHelper.CompanyId
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
            _viewModel.SystemParameterFA.CTRANS_DEPT_CODE = loTempResult.CDEPT_CODE;
            _viewModel.SystemParameterFA.CTRANS_DEPT_NAME = loTempResult.CDEPT_NAME;
        }
        #endregion

        #region DefaultAsset Lookup
        private async Task DefaultAsset_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.SystemParameterFA.CASSET_DEPT_CODE) == false)
                {
                    GSL00700ParameterDTO loParam = new GSL00700ParameterDTO()
                    {
                        CSEARCH_TEXT = _viewModel.SystemParameterFA.CASSET_DEPT_CODE,
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                    };

                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel();

                    var loResult = await loLookupViewModel.GetDepartment(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.SystemParameterFA.CASSET_DEPT_NAME = "";
                        goto EndBlock;
                    }
                    _viewModel.SystemParameterFA.CASSET_DEPT_CODE = loResult.CDEPT_CODE;
                    _viewModel.SystemParameterFA.CASSET_DEPT_NAME = loResult.CDEPT_NAME;
                }
                else
                {
                    _viewModel.SystemParameterFA.CASSET_DEPT_NAME = "";
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
                CUSER_ID = _clientHelper.UserId,
                CCOMPANY_ID = _clientHelper.CompanyId
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
            _viewModel.SystemParameterFA.CASSET_DEPT_CODE = loTempResult.CDEPT_CODE;
            _viewModel.SystemParameterFA.CASSET_DEPT_NAME = loTempResult.CDEPT_NAME;
        }
        #endregion
    }
}
