using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using PMM00100COMMON;
using PMM00100COMMON.DTO_s;
using PMM00100FrontResources;
using PMM00100MODEL.View_Model;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_LockingFront;

namespace PMM00100FRONT
{
    public partial class PMM00102 : R_Page, R_ITabPage
    {
        //var
        private PMM00102ViewModel _viewModel = new();
        private R_ConductorGrid _conPropertyRef;
        private R_Conductor _conBillingRef;
        private R_Grid<PropertyDTO> _gridRef;
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }
        [Inject] private IClientHelper _clientHelper { get; set; }
        private bool _enableGrid = true;
        private int _pageSizeProperty = 25;

        //abstract
        public async Task RefreshTabPageAsync(object poParam)
        {
            R_Exception loEx = new();
            try
            {
                await _viewModel.GetGSBCodeInfoListAsync();
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //override
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new();
            try
            {
                await _viewModel.GetGSBCodeInfoListAsync();
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;
            SystemParamDTO loData = null;
            try
            {
                loData = R_FrontUtility.ConvertObjectToObject<SystemParamDTO>(eventArgs.Data);
                if (loData != null)
                {

                    var loCls = new R_LockingServiceClient(pcModuleName: PMM00100ContextConstant.DEFAULT_MODULE,
                        plSendWithContext: true,
                        plSendWithToken: true,
                        pcHttpClientName: PMM00100ContextConstant.DEFAULT_HTTP_NAME);

                    if (eventArgs.Mode == R_eLockUnlock.Lock)
                    {
                        var loLockPar = new R_ServiceLockingLockParameterDTO
                        {
                            Company_Id = _clientHelper.CompanyId,
                            User_Id = _clientHelper.UserId,
                            Program_Id = PMM00100ContextConstant.PROGRAM_ID,
                            Table_Name = PMM00100ContextConstant.TABLE_NAME_SYSPARAM,
                            Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID)
                        };

                        loLockResult = await loCls.R_Lock(loLockPar);
                    }
                    else
                    {
                        var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                        {
                            Company_Id = _clientHelper.CompanyId,
                            User_Id = _clientHelper.UserId,
                            Program_Id = PMM00100ContextConstant.PROGRAM_ID,
                            Table_Name = PMM00100ContextConstant.TABLE_NAME_SYSPARAM,
                            Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID)
                        };

                        loLockResult = await loCls.R_UnLock(loUnlockPar);
                    }

                    llRtn = loLockResult.IsSuccess;
                    if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                        throw loLockResult.Exception;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return llRtn;
        }

        //events - grid
        private async Task BillingParamGrid_GetListAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.GetList_PropertyAsync();
                eventArgs.ListEntityResult = _viewModel.Properties;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task BillingParamGrid_DisplayAsync(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loCurrentRow = R_FrontUtility.ConvertObjectToObject<SystemParamBillingDTO>(eventArgs.Data);
                _viewModel._propertyId = loCurrentRow.CPROPERTY_ID;
                await _conBillingRef.R_GetEntity(loCurrentRow);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //events - con
        private async Task BillingParamForm_GetRecordAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<SystemParamBillingDTO>(eventArgs.Data);
                await _viewModel.GetRecord_BillingParamAsync(loData);
                eventArgs.Result = _viewModel.SystemParamBilling;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task BillingParamForm_SetOtherAsync(R_SetEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await Task.Delay(1);
                await InvokeTabEventCallbackAsync(eventArgs.Enable);
                _enableGrid = eventArgs.Enable;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void BillingParamForm_Validation(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<SystemParamBillingDTO>(eventArgs.Data);
                if (!_viewModel.Data.DOL_PAY_START_DATE.HasValue)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val19"));
                }
                if (string.IsNullOrWhiteSpace(_viewModel.Data.COL_PAY_SUBMIT_BY))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val20"));
                }
                if (string.IsNullOrWhiteSpace(_viewModel.Data.COL_PAY_CURRENCY))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val21"));
                }
                if ((_viewModel.Data.IBILLING_STATEMENT_DATE < 1 || _viewModel.Data.IBILLING_STATEMENT_DATE > 31))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val22"));
                }
                if (string.IsNullOrWhiteSpace(_viewModel.Data.CBILLING_STATEMENT_TOP_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val23"));
                }
                eventArgs.Cancel = loEx.HasError;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void BillingParamForm_Saving(R_SavingEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Data is SystemParamBillingDTO loData)
                {
                    loData.CPROPERTY_ID = loData.CPROPERTY_ID ?? _viewModel._propertyId;
                    loData.CBILLING_STATEMENT_DATE = loData.IBILLING_STATEMENT_DATE.ToString();
                    loData.COL_PAY_START_DATE = loData.DOL_PAY_START_DATE.Value.ToString("yyyyMMdd") ?? "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task BillingParamForm_SaveAsync(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = eventArgs.Data as SystemParamBillingDTO;
                await _viewModel.SaveRecord_BillingParamAsync(loData, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel.SystemParamBilling;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void BillingParam_CheckAdd(R_CheckAddEventArgs eventArgs) { eventArgs.Allow = !_viewModel._isRecordFound; }
        private void BillingParam_CheckEdit(R_CheckEditEventArgs eventArgs) { eventArgs.Allow = _viewModel._isRecordFound; }

        //events - lookup
        private void BeforeOpenPopup_Currency(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                eventArgs.Parameter = new GSL00300ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                };
                eventArgs.TargetPageType = typeof(GSL00300);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void AfterOpenPopup_CurrencyAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                if (eventArgs.Result != null)
                {
                    var loTempResult = eventArgs.Result as GSL00300DTO;
                    _viewModel.Data.COL_PAY_CURRENCY = loTempResult.CCURRENCY_CODE ?? "";
                    _viewModel.Data.COL_PAY_CURRENCY_DESCRIPTION = loTempResult.CCURRENCY_NAME ?? "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void BeforeOpenPopup_TOP(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                eventArgs.Parameter = new GSL02100ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                };
                eventArgs.TargetPageType = typeof(GSL02100);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void AfterOpenPopup_TOP(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                if (eventArgs.Result != null)
                {
                    var loTempResult = eventArgs.Result as GSL02100DTO;
                    _viewModel.Data.CBILLING_STATEMENT_TOP_CODE = loTempResult.CPAY_TERM_CODE ?? "";
                    _viewModel.Data.CBILLING_STATEMENT_TOP_NAME = loTempResult.CPAY_TERM_NAME ?? "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //lostfocus
        private async Task OnLostFocus_CurrencyAsync()
        {
            var loEx = new R_Exception();
            LookupGSL00300ViewModel loLookupViewModel;
            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.Data.COL_PAY_CURRENCY))
                {

                    loLookupViewModel = new LookupGSL00300ViewModel(); //use GSL's model
                    GSL00300DTO loResult = await loLookupViewModel.GetCurrency(new GSL00300ParameterDTO()
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel.Data.COL_PAY_CURRENCY
                    });

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.Data.COL_PAY_CURRENCY = "";
                        _viewModel.Data.COL_PAY_CURRENCY_DESCRIPTION = "";
                    }
                    else
                    {
                        _viewModel.Data.COL_PAY_CURRENCY = loResult.CCURRENCY_CODE ?? "";
                        _viewModel.Data.COL_PAY_CURRENCY_DESCRIPTION = loResult.CCURRENCY_NAME ?? "";
                    }
                }
                else
                {
                    _viewModel.Data.COL_PAY_CURRENCY = "";
                    _viewModel.Data.COL_PAY_CURRENCY_DESCRIPTION = "";
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
            await Task.CompletedTask;
        }
        private async Task OnLostFocus_TermOfPaymentAsync()
        {
            var loEx = new R_Exception();
            LookupGSL02100ViewModel loLookupViewModel = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.Data.CBILLING_STATEMENT_TOP_CODE))
                {

                    loLookupViewModel = new LookupGSL02100ViewModel(); //use GSL's model
                    GSL02100DTO loResult = await loLookupViewModel.GetPaymentTerm(new GSL02100ParameterDTO()
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel.Data.CBILLING_STATEMENT_TOP_CODE
                    });

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.Data.CBILLING_STATEMENT_TOP_CODE = "";
                        _viewModel.Data.CBILLING_STATEMENT_TOP_NAME = "";
                    }
                    else
                    {
                        _viewModel.Data.CBILLING_STATEMENT_TOP_CODE = loResult.CPAY_TERM_CODE ?? "";
                        _viewModel.Data.CBILLING_STATEMENT_TOP_NAME = loResult.CPAY_TERM_NAME ?? "";
                    }
                }
                else
                {
                    _viewModel.Data.CBILLING_STATEMENT_TOP_CODE = "";
                    _viewModel.Data.CBILLING_STATEMENT_TOP_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        Endblock:
            R_DisplayException(loEx);
            await Task.CompletedTask;
        }
    }
}
