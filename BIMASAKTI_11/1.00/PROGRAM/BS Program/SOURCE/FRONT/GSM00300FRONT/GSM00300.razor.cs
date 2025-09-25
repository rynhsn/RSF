using GSM00300MODEL.View_Model;
using GSM00300COMMON.DTO_s;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Interfaces;
using BlazorClientHelper;
using GSM00300FrontResources;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Controls.Enums;
using R_LockingFront;
using GSM00300COMMON;
using R_BlazorFrontEnd.Controls.MessageBox;

namespace GSM00300FRONT
{
    public partial class GSM00300 : R_Page
    {
        //variables
        private GSM00300ViewModel _viewModel = new();
        private R_Conductor _conRef;
        private R_TabPage _tabPage_TaxInfo;
        private R_TabStrip _tabStrip; //ref Tabstrip
        private bool _isTaxinfoPageCRUDmode = false;
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IClientHelper _clientHelper { get; set; }

        //abstracts
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.InitProcess(_localizer);
                await _conRef.R_GetEntity(new CompanyParamRecordDTO());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        protected override async Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult? loLockResult = null;

            try
            {
                var loData = (CompanyParamRecordDTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: GSM00300ContextConstant.DEFAULT_MODULE,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: GSM00300ContextConstant.DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = GSM00300ContextConstant.CPROGRAM_ID,
                        Table_Name = GSM00300ContextConstant.TABLE_NAME,
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CCOMPANY_ID)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = GSM00300ContextConstant.CPROGRAM_ID,
                        Table_Name = GSM00300ContextConstant.TABLE_NAME,
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CCOMPANY_ID)
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

        //event
        private async Task CompParam_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {

            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.CompParamGetRecord();
                eventArgs.Result = _viewModel.CompanyParamRecord;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void CompParam_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task CompParam_BeforeEditAsync(R_BeforeEditEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.CheckIsCompanyParamEditableAsync();

                if (!_viewModel.CheckCompanyParamEditableRecord.LALLOW_EDIT)
                {
                    eventArgs.Cancel = true;
                    await R_MessageBox.Show("", _localizer["_msg_validateEditCompany"], R_eMessageBoxButtonType.OK);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task CompParam_SaveAsync(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {

                await _viewModel.CompParamSaveAsync(_viewModel.Data, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel.CompanyParamRecord ?? new CompanyParamRecordDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //tabpage behaviour
        private void BeforeOpenTabPage_TaxInfo(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.TargetPageType = typeof(GSM00301);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void R_TabEventCallback(object poValue)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _isTaxinfoPageCRUDmode = !(bool)poValue;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task OnActiveTabIndexChangingAsync(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                eventArgs.Cancel = _conRef.R_ConductorMode != R_eConductorMode.Normal || _isTaxinfoPageCRUDmode;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //lookups
        private async Task BeforeOpen_LookupBaseCurrencyAsync(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = new GSL00300ParameterDTO();
                eventArgs.TargetPageType = typeof(GSL00300);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }
        private async Task AfterOpen_LookupBaseCurrency(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loTempResult = (GSL00300DTO)eventArgs.Result;
                _viewModel.Data.CBASE_CURRENCY_CODE = loTempResult.CCURRENCY_CODE ?? "";
                _viewModel.Data.CBASE_CURRENCY_NAME = loTempResult.CCURRENCY_NAME ?? "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }
        private async Task OnLostFocus_LookupBaseCurrency()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.Data.CBASE_CURRENCY_CODE))
                {

                    LookupGSL00300ViewModel loLookupViewModel = new LookupGSL00300ViewModel(); //use GSL's model

                    var loResult = await loLookupViewModel.GetCurrency(new GSL00300ParameterDTO()
                    {
                        CSEARCH_TEXT = _viewModel.Data.CBASE_CURRENCY_CODE,
                    });

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                    }
                    _viewModel.Data.CBASE_CURRENCY_CODE = loResult?.CCURRENCY_CODE ?? "";
                    _viewModel.Data.CBASE_CURRENCY_NAME = loResult?.CCURRENCY_NAME ?? "";
                }
                else
                {
                    _viewModel.Data.CBASE_CURRENCY_CODE = "";
                    _viewModel.Data.CBASE_CURRENCY_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
            await Task.CompletedTask;
        }
        private async Task BeforeOpen_LookupLocalCurrencyAsync(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = new GSL00300ParameterDTO();
                eventArgs.TargetPageType = typeof(GSL00300);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }
        private async Task AfterOpen_LookupLocalCurrency(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loTempResult = (GSL00300DTO)eventArgs.Result;
                _viewModel.Data.CLOCAL_CURRENCY_CODE = loTempResult.CCURRENCY_CODE ?? "";
                _viewModel.Data.CLOCAL_CURRENCY_NAME = loTempResult.CCURRENCY_NAME ?? "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }
        private async Task OnLostFocus_LookupLocalCurrency()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.Data.CLOCAL_CURRENCY_CODE))
                {

                    LookupGSL00300ViewModel loLookupViewModel = new LookupGSL00300ViewModel(); //use GSL's model

                    var loResult = await loLookupViewModel.GetCurrency(new GSL00300ParameterDTO()
                    {
                        CSEARCH_TEXT = _viewModel.Data.CLOCAL_CURRENCY_CODE,
                    });

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                    }
                    _viewModel.Data.CLOCAL_CURRENCY_CODE = loResult?.CCURRENCY_CODE ?? "";
                    _viewModel.Data.CLOCAL_CURRENCY_NAME = loResult?.CCURRENCY_NAME ?? "";
                }
                else
                {
                    _viewModel.Data.CLOCAL_CURRENCY_CODE = "";
                    _viewModel.Data.CLOCAL_CURRENCY_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
            await Task.CompletedTask;
        }
    }
}
