using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using GSM02000Common.DTOs;
using GSM02000Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM02000Front;

public partial class GSM02000 : R_Page
{
    private GSM02000ViewModel _GSM02000ViewModel = new();
    private R_Conductor _conductorRef;
    private R_Grid<GSM02000GridDTO> _gridRef;
    private string loLabel = "Activate";
    
    [Inject] private IClientHelper _clientHelper { get; set; }
    [Inject] private R_PopupService PopupService { get; set;}

    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            await _gridRef.R_RefreshGrid(null);
            // await _gridRef.AutoFitAllColumnsAsync();
            await _GSM02000ViewModel.GetRoundingMode();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Grid_Display(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02000DTO)eventArgs.Data;
                _GSM02000ViewModel.ActiveInactiveEntity.CTAX_ID = loParam.CTAX_ID;
                if (loParam.LACTIVE)
                {
                    loLabel = "Inactive";
                    _GSM02000ViewModel.ActiveInactiveEntity.LACTIVE = false;
                }
                else
                {
                    loLabel = "Activate";
                    _GSM02000ViewModel.ActiveInactiveEntity.LACTIVE = true;
                }
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs arg)
    {
        var loEx = new R_Exception();

        try
        {
            await _GSM02000ViewModel.GetGridList();
            arg.ListEntityResult = _GSM02000ViewModel.GridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    private async Task Conductor_ServiceGetRecord(R_ServiceGetRecordEventArgs arg)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM02000DTO>(arg.Data);

            await _GSM02000ViewModel.GetEntity(loParam);
            arg.Result = _GSM02000ViewModel.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Conductor_ServiceSave(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM02000DTO>(eventArgs.Data);
            await _GSM02000ViewModel.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);

            eventArgs.Result = _GSM02000ViewModel.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Conductor_ServiceDelete(R_ServiceDeleteEventArgs arg)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (GSM02000DTO)arg.Data;
            await _GSM02000ViewModel.DeleteEntity(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Conductor_AfterSave(R_AfterSaveEventArgs arg)
    {
        var loEx = new R_Exception();

        try
        {
            await _gridRef.R_RefreshGrid((GSM02000DTO)arg.Data);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #region Lookup Button

    private R_AddButton R_AddBtn;
    private R_Button R_ActiveInActiveBtn;
    public R_Lookup R_LookupBtnIn;
    private R_Lookup R_LookupBtnOut;

    private void BeforeOpenLookupIn(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var param = new GSL00500ParameterDTO
        {
            CPROPERTY_ID = "",
            CPROGRAM_CODE = "GSM02000",
            CBSIS = "",
            CDBCR = "",
            LCENTER_RESTR = false,
            LUSER_RESTR = false,
            CCENTER_CODE = "",
            CUSER_LANGUAGE = _clientHelper.CultureUI.TwoLetterISOLanguageName
        };
        eventArgs.Parameter = param;
        eventArgs.TargetPageType = typeof(GSL00500);
    }

    private void AfterOpenLookupIn(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loTempResult = (GSL00500DTO)eventArgs.Result;
        if (loTempResult == null)
            return;

        var loGetData = (GSM02000DTO)_conductorRef.R_GetCurrentData();
        loGetData.CTAXIN_GLACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
        loGetData.CTAXIN_GLACCOUNT_NAME = loTempResult?.CGLACCOUNT_NAME;
    }

    private void BeforeOpenLookupOut(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var param = new GSL00500ParameterDTO
        {
            CPROPERTY_ID = "",
            CPROGRAM_CODE = "GSM02000",
            CBSIS = "",
            CDBCR = "",
            LCENTER_RESTR = false,
            LUSER_RESTR = false,
            CCENTER_CODE = "",
            CUSER_LANGUAGE = _clientHelper.CultureUI.TwoLetterISOLanguageName
        };
        eventArgs.Parameter = param;
        eventArgs.TargetPageType = typeof(GSL00500);
    }

    private void AfterOpenLookupOut(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loTempResult = (GSL00500DTO)eventArgs.Result;
        if (loTempResult == null)
            return;

        var loGetData = (GSM02000DTO)_conductorRef.R_GetCurrentData();
        loGetData.CTAXOUT_GLACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
        loGetData.CTAXOUT_GLACCOUNT_NAME = loTempResult?.CGLACCOUNT_NAME;
    }

    private void R_SetAdd(R_SetEventArgs eventArgs)
    {
        if (R_LookupBtnOut != null)
            R_LookupBtnOut.Enabled = eventArgs.Enable;
        if (R_LookupBtnIn != null)
            R_LookupBtnIn.Enabled = eventArgs.Enable;
    }

    private void R_SetEdit(R_SetEventArgs eventArgs)
    {
        if (R_LookupBtnOut != null)
            R_LookupBtnOut.Enabled = eventArgs.Enable;
        if (R_LookupBtnIn != null)
            R_LookupBtnIn.Enabled = eventArgs.Enable;
    }

    #endregion

    private async Task BeforeOpenActiveInactive(R_BeforeOpenPopupEventArgs eventArgs)
    {
        var loException = new R_Exception();
        try
        {
            var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
            loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM02001"; //Uabh Approval Code sesuai Spec masing masing
            await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

            //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
            if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
            {
                await _GSM02000ViewModel.SetActiveInactive();
                await _gridRef.R_RefreshGrid(null);
                return;
            }
            else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
            {
                eventArgs.Parameter = new GFF00900ParameterDTO()
                {
                    Data = loValidateViewModel.loRspActivityValidityList,
                    IAPPROVAL_CODE = "GSM02001" //Uabh Approval Code sesuai Spec masing masing
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

    private async Task AfterOpenActiveInactive(R_AfterOpenPopupEventArgs eventArgs)
    {
        var loException = new R_Exception();
        try
        {
            if (eventArgs.Success == false)
                return;

            var result = (bool)eventArgs.Result;
            if (result)
            {
                await _GSM02000ViewModel.SetActiveInactive();
                await _gridRef.R_RefreshGrid(null);
            }
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();
    }

    private Task InstanceTaxTab(R_InstantiateDockEventArgs eventArgs)
    {
        // eventArgs.Parameter = R_FrontUtility.ConvertObjectToObject<GSM02000DTO>(_GSM02000ViewModel.Entity);
        eventArgs.TargetPageType = typeof(GSM02000Tax);
        return Task.CompletedTask;
    }

    private async Task Validation(R_ValidationEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        
        R_PopupResult loResult = null;
        GFF00900ParameterDTO loParam = null;
        GSM02000DTO loData = null;
        var lsApprovalCode = "GSM02001";
        try
        {
            loData = (GSM02000DTO)eventArgs.Data;
            _GSM02000ViewModel.Validate(loData);
            
            loData.CTAXIN_GLACCOUNT_NO ??= "";
            loData.CTAXOUT_GLACCOUNT_NO ??= "";
            
            if (loData.LACTIVE == true && _conductorRef.R_ConductorMode == R_eConductorMode.Add)
            {
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = lsApprovalCode;
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync();

                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    eventArgs.Cancel = false;
                }
                else
                {
                    loParam = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = lsApprovalCode
                    };
                    loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loParam);
                    if (loResult.Success == false || (bool)loResult.Result == false)
                    {
                        eventArgs.Cancel = true;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }

    private Task Saving(R_SavingEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        
        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM02000DTO>(eventArgs.Data);
            //cek apakah cdescription null maka akan diberikan string kosong
            // if (string.IsNullOrEmpty(loParam.CDESCRIPTION))
            //     loParam.CDESCRIPTION = string.Empty;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
        return Task.CompletedTask;
    }

    private void ChangeRoundingMode(object obj)
    {
        if ((string)obj == "03")
        {
            _GSM02000ViewModel.Data.IROUNDING = 0;
        }
    }

    private void Conductor_AfterAdd(R_AfterAddEventArgs eventArgs)
    {
        var tes = (GSM02000DTO)eventArgs.Data;
        tes.LACTIVE = true;
        // eventArgs.Data = tes;
    }
}