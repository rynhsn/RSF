using GLB00700MODEL.View_Model_s;
using Microsoft.AspNetCore.Components;
using R_APICommonDTO;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLB00700FrontResources;
using BlazorClientHelper;
using R_BlazorFrontEnd.Exceptions;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Popup;

namespace GLB00700FRONT
{
    public partial class GLB00700 : R_Page
    {
        //variables
        private GLB00700ViewModel _viewModel = new();
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IClientHelper _clientHelper { get; set; }
        [Inject] R_PopupService PopupService { get; set; }
        private bool _isProcessing = false;



        //methods
        private void StateChangeInvoke()
        {
            StateHasChanged();
        }
        private void DisplayErrorInvoke(R_APIException poEx)
        {
            var loEx = R_FrontUtility.R_ConvertFromAPIException(poEx);
            R_DisplayException(loEx);
        }
        private async Task ShowSuccessInvoke()
        {
            var loValidate = await R_MessageBox.Show("", _localizer["_msg_batchComplete"], R_eMessageBoxButtonType.OK);
        }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.InitProcessAsync(_localizer);
                _viewModel.StateChangeAction = StateChangeInvoke;
                _viewModel.DisplayErrorAction = DisplayErrorInvoke;
                _viewModel.ShowSuccessAction = async () =>
                {
                    await ShowSuccessInvoke();
                };
                _viewModel.UserId = _clientHelper.UserId;
                _viewModel.CompanyId = _clientHelper.CompanyId;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private void Before_Open_lookupDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00700ParameterDTO
            {
                CUSER_ID = _clientHelper.UserId,
                CCOMPANY_ID = _clientHelper.CompanyId
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00700);
        }
        private async Task After_Open_lookupDept(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {

                var loTempResult = (GSL00700DTO)eventArgs.Result;
                if (loTempResult != null)
                {
                    _viewModel.Param.CDEPT_CODE = loTempResult.CDEPT_CODE;
                    _viewModel.Param.CDEPT_NAME = loTempResult.CDEPT_NAME;
                    await _viewModel.GetRateRevaluatoionRecordAsync();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task OnLostFocus_LookupDept()
        {
            R_Exception loEx = new();
            try
            {
                if (!String.IsNullOrWhiteSpace(_viewModel.Param.CDEPT_CODE))
                {
                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel();
                    var loParam = new GSL00700ParameterDTO { CSEARCH_TEXT = _viewModel.Param.CDEPT_CODE, };
                    var loResult = await loLookupViewModel.GetDepartment(loParam);
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.Param.CDEPT_NAME = "";
                        goto EndBlock;
                    }
                    _viewModel.Param.CDEPT_CODE = loResult.CDEPT_CODE;
                    _viewModel.Param.CDEPT_NAME = loResult.CDEPT_NAME;
                    await _viewModel.GetRateRevaluatoionRecordAsync();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void Validation()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.Param.CDEPT_CODE))
                {
                    loEx.Add("", _localizer["_val_dept"]);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task Onclick_BtnProcessAsync()
        {
            R_Exception loEx = new();
            R_PopupResult loResult = null;
            try
            {
                Validation();
                loResult = await PopupService.Show(typeof(GLB00701), null);
                if (loResult.Success)
                {
                    _isProcessing = true;
                    await _viewModel.ProcessBatchAsync();
                    await _viewModel.GetRateRevaluatoionRecordAsync();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
