using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using GSM12000COMMON;
using GSM12000MODEL;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;

namespace GSM12000FRONT;

public partial class GSM12000 : R_Page
{
    private GSM12000ViewModel _viewModel = new();
    private R_Grid<GSM12000DTO> _gridRef;
    private R_Conductor _conductorRef;
    private R_TextBox _messageNoRef;
    private R_TextBox _messageDescRef;
    private string loLabel;
    [Inject] private IClientHelper _clientHelper { get; set; }
    [Inject] private R_IFileConverter _fileConverter { get; set; }

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetInitialProcess();
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }


    private async Task OnChanged(object poParam)
    {
        var loEx = new R_Exception();
        string lsmessageTypeValue = (string)poParam ?? ""; // Set default value to an empty string
        try
        {
            _viewModel.messageTypeValue = lsmessageTypeValue;
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }


        R_DisplayException(loEx);
    }

    private void R_ConvertToGridEntity(R_ConvertToGridEntityEventArgs eventArgs)
    {
        eventArgs.GridData = R_FrontUtility.ConvertObjectToObject<GSM12000DTO>(eventArgs.Data);
    }

    private async Task Display(R_DisplayEventArgs eventArgs)
    {
        switch (eventArgs.ConductorMode)
        {
            case R_eConductorMode.Add:
                await _messageNoRef.FocusAsync();
                break;
            case R_eConductorMode.Edit:
                await _messageNoRef.FocusAsync();
                break;
        }
    }

    private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetListMessage();
            eventArgs.ListEntityResult = _viewModel.MessageGrid;
            //await _gridRef.AutoFitAllColumnsAsync();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task Conductor_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM12000DTO>(eventArgs.Data);

            await _viewModel.GetMessage(loParam);
            eventArgs.Result = _viewModel.Message;

            if (loParam.LACTIVE)
            {
                loLabel = _localizer["Inactive"];
                _viewModel.Data.LACTIVE = false;
            }
            else
            {
                loLabel = _localizer["Active"];
                _viewModel.Data.LACTIVE = true;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task Conductor_ServiceSave(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM12000DTO>(eventArgs.Data);
            //loParam.TMESSAGE_DESCR_RTF = _fileConverter.R_GetRtfStringFromHtmlString(loParam.TMESSAGE_DESCRIPTION);
            //loParam.TADDITIONAL_DESCR_RTF = _fileConverter.R_GetRtfStringFromHtmlString(loParam.CADDITIONAL_DESCRIPTION);
            loParam.TMESSAGE_DESCR_RTF = loParam.TMESSAGE_DESCRIPTION;
            loParam.TADDITIONAL_DESCR_RTF = loParam.CADDITIONAL_DESCRIPTION;
            await _viewModel.SaveMessage(loParam, (eCRUDMode)eventArgs.ConductorMode);
            eventArgs.Result = _viewModel.Message;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Conductor_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (GSM12000DTO)eventArgs.Data;
            await _viewModel.DeleteMessage(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void R_Before_Open_Popup_Description(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.Parameter = _viewModel.Data.TMESSAGE_DESCRIPTION;
        eventArgs.TargetPageType = typeof(GSM12000PopUpTextEditor);
    }

    private void R_After_Open_Popup_Description(R_AfterOpenPopupEventArgs eventArgs)
    {
        R_Exception loEx = new R_Exception();

        try
        {
            if (eventArgs.Success)
            {
                _viewModel.Data.TMESSAGE_DESCRIPTION = (string)eventArgs.Result;
                _viewModel.Message.TMESSAGE_DESCRIPTION = (string)eventArgs.Result;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void R_Before_Open_Popup_Additional(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.Parameter = _viewModel.Data.CADDITIONAL_DESCRIPTION;
        eventArgs.TargetPageType = typeof(GSM12000PopUpTextEditor);
    }

    private void R_After_Open_Popup_Additional(R_AfterOpenPopupEventArgs eventArgs)
    {
        R_Exception loEx = new R_Exception();

        try
        {
            if (eventArgs.Success)
            {
                _viewModel.Data.CADDITIONAL_DESCRIPTION = (string)eventArgs.Result;
                _viewModel.Message.CADDITIONAL_DESCRIPTION = (string)eventArgs.Result;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    private void R_BeforeOpenPrint(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(GSM12000Print);
        var param = new GSM12000PrintParamDTO()
        {
            CCOMPANY_ID = _clientHelper.CompanyId,
            CMESSAGE_TYPE = _viewModel.messageTypeValue,
            CUSER_LOGIN_ID = _clientHelper.UserId,
        };
        eventArgs.Parameter = param;
    }

    private async Task R_AfterOpenPrint(R_AfterOpenPopupEventArgs eventArgs)
    {
        R_Exception loException = new R_Exception();
        try
        {
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();
    }
    [Inject] public R_PopupService PopupService { get; set; }
    public async Task ActiveInactiveSaving(R_ValidationEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        GFF00900ParameterDTO loParam = null;
        R_PopupResult loResult = null;
        GSM12000DTO loData = null;
        bool Validated = false;
        try
        {


            loData = (GSM12000DTO)eventArgs.Data;
            if (loData.LACTIVE == true && _conductorRef.R_ConductorMode == R_eConductorMode.Add)
            {
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE =
                    "GSM12001"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel
                    .RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" &&
                    loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    eventArgs.Cancel = false;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    loParam = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "GSM12001" //Uabh Approval Code sesuai Spec masing masing
                    };
                    loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loParam);

                    // eventArgs.Cancel = !(bool)loResult.Result;

                    if (loResult.Result != null)
                    {
                        if ((bool)loResult.Result == false)
                        {
                            Validated = true;
                        }
                    }
                    else
                    {
                        Validated = true;
                    }

                    if ((bool)loResult.Success == false)
                    {
                        Validated = true;
                    }

                    eventArgs.Cancel = Validated;
                }
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task R_Before_Open_Popup_ActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
    {
        R_Exception loException = new R_Exception();
        try
        {
            await lockingButton(true);
            var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
            loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE =
                "GSM12001"; //Uabh Approval Code sesuai Spec masing masing
            await loValidateViewModel
                .RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

            //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
            if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" &&
                loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
            {
                await _viewModel.ChangeStatusActive(); //Ganti jadi method ActiveInactive masing masing
                await _gridRef.R_RefreshGrid(null);
                return;
            }
            else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
            {
                eventArgs.Parameter = new GFF00900ParameterDTO()
                {
                    Data = loValidateViewModel.loRspActivityValidityList,
                    IAPPROVAL_CODE = "GSM12001" //Uabh Approval Code sesuai Spec masing masing
                };
                eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
            }
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            await lockingButton(false);
        }

        loException.ThrowExceptionIfErrors();
    }

    private async Task R_After_Open_Popup_ActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
    {
        R_Exception loException = new R_Exception();
        try
        {

            await lockingButton(false);
            if (eventArgs.Success == false)
            {
                return;
            }

            bool result = (bool)eventArgs.Result;
            if (result == true)
            {
                await _viewModel.ChangeStatusActive();
                await _gridRef.R_RefreshGrid(null);
            }
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();
    }


    [Inject] private IClientHelper clientHelper { get; set; }
    private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
    private const string DEFAULT_MODULE_NAME = "GS";

    protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        var llRtn = false;
        R_LockingFrontResult loLockResult = null;

        try
        {
            var loData = (GSM12000DTO)eventArgs.Data;

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
                    Program_Id = "GSM12000",
                    Table_Name = "GSM_MESSAGE",
                    Key_Value = string.Join("|", clientHelper.CompanyId, loData.CMESSAGE_TYPE, loData.CMESSAGE_NO)
                };

                loLockResult = await loCls.R_Lock(loLockPar);
            }
            else
            {
                var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                {
                    Company_Id = clientHelper.CompanyId,
                    User_Id = clientHelper.UserId,
                    Program_Id = "GSM12000",
                    Table_Name = "GSM_MESSAGE",
                    Key_Value = string.Join("|", clientHelper.CompanyId, loData.CMESSAGE_TYPE, loData.CMESSAGE_NO)
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
    private async Task lockingButton(bool param)
    {
        var loEx = new R_Exception();
        R_LockingFrontResult loLockResult = null;
        try
        {
            var loData = _viewModel.Message;

            var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                plSendWithContext: true,
                plSendWithToken: true,
                pcHttpClientName: DEFAULT_HTTP_NAME);

            if (param)
            {
                var loLockPar = new R_ServiceLockingLockParameterDTO
                {
                    Company_Id = clientHelper.CompanyId,
                    User_Id = clientHelper.UserId,
                    Program_Id = "GSM12000",
                    Table_Name = "GSM_MESSAGE",
                    Key_Value = string.Join("|", clientHelper.CompanyId, loData.CMESSAGE_TYPE, loData.CMESSAGE_NO)
                };

                loLockResult = await loCls.R_Lock(loLockPar);
            }
            else
            {
                var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                {
                    Company_Id = clientHelper.CompanyId,
                    User_Id = clientHelper.UserId,
                    Program_Id = "GSM12000",
                    Table_Name = "GSM_MESSAGE",
                    Key_Value = string.Join("|", clientHelper.CompanyId, loData.CMESSAGE_TYPE, loData.CMESSAGE_NO)
                };

                loLockResult = await loCls.R_UnLock(loUnlockPar);
            }

            if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                throw loLockResult.Exception;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    private void AfterAdd(R_AfterAddEventArgs obj)
    {
        _viewModel.Message.LACTIVE = true;
    }
}