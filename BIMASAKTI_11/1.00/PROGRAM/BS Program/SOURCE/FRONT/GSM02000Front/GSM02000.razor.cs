using BlazorClientHelper;
using GSM02000Common.DTOs;
using GSM02000Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Forms;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM02000Front;

public partial class GSM02000 : R_Page
{
    private GSM02000ViewModel _GSM02000ViewModel = new();
    private R_Conductor _conductorRef;
    private R_Grid<GSM02000GridDTO> _gridRef;
    [Inject] IClientHelper clientHelper { get; set; }

    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();
        
        try
        {
            await _gridRef.R_RefreshGrid(null);
            await _GSM02000ViewModel.GetRoundingMode();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        R_DisplayException(loEx);
        
    }
    
    private async Task Conductor_Display(R_DisplayEventArgs arg)
    {
        await _gridRef.R_RefreshGrid((GSM02000DTO)arg.Data);
    }

    private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs arg)
    {
        var loEx = new R_Exception();

        try
        {
            await _GSM02000ViewModel.GetGridList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
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

    private async Task Conductor_ServiceSave(R_ServiceSaveEventArgs arg)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM02000DTO>(arg.Data);
            await _GSM02000ViewModel.SaveEntity(loParam, (eCRUDMode)arg.ConductorMode);

            arg.Result = _GSM02000ViewModel.Entity;
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

    private async Task R_ConvertToGridEntity(R_ConvertToGridEntityEventArgs arg)
    {
        arg.GridData = R_FrontUtility.ConvertObjectToObject<GSM02000GridDTO>(arg.Data);
    }

    private async Task Conductor_AfterSave(R_AfterSaveEventArgs arg)
    {
        await _gridRef.R_RefreshGrid((GSM02000DTO)arg.Data);
    }
    
    #region Lookup Button

    private R_AddButton R_AddBtn;
    private R_Button R_ActiveInActiveBtn;
    private R_Lookup R_LookupBtn;
    
    private void Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var param = new GSL00500ParameterDTO {
            CPROPERTY_ID = "",
            CPROGRAM_CODE = "GSM02000",
            CBSIS = "",
            CDBCR = "",
            LCENTER_RESTR = false,
            LUSER_RESTR = false,
            CCENTER_CODE = "",
            CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName
        };
        eventArgs.Parameter = param ;
        eventArgs.TargetPageType = typeof(GSL00500);
    }

    private void After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loTempResult = (GSL00500DTO)eventArgs.Result;
        if (loTempResult == null)
            return;
    
        var loGetData = (GSM02000DTO)_conductorRef.R_GetCurrentData();
        loGetData.CGLACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
        loGetData.CGLACCOUNT_NAME = loTempResult?.CGLACCOUNT_NAME;
    }
    
    private void R_SetHasData(R_SetEventArgs eventArgs)
    {
        if (R_AddBtn != null)
            R_AddBtn.Enabled = eventArgs.Enable;
    
        if (R_ActiveInActiveBtn != null)
            R_ActiveInActiveBtn.Enabled = eventArgs.Enable;
    }   

    private void R_SetAdd(R_SetEventArgs eventArgs)
    {
        if (R_LookupBtn != null)
            R_LookupBtn.Enabled = eventArgs.Enable;
    }
    
    private void R_SetEdit(R_SetEventArgs eventArgs)
    {
        if (R_LookupBtn != null)
            R_LookupBtn.Enabled = eventArgs.Enable;
    }
    
    #endregion

    private async Task BeforeOpenActiveInactive(R_BeforeOpenPopupEventArgs arg)
    {
        arg.TargetPageType = typeof(GFF00900FRONT.GFF00900);
        //
        // R_Exception loException = new R_Exception();
        // try
        // {
        //     await _GSM02000ViewModel.RSP_ACTIVITY_VALIDITYMethodAsync();
        //     if (_GSM02000ViewModel.loRspActivityValidityResult.Data.IAPPROVAL_MODE == 2)
        //     {
        //         arg.Parameter = _GSM02000ViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE;
        //         arg.TargetPageType = typeof(GFF00900FRONT.GFF00900);
        //         if (loException.HasError == false)
        //         {
        //             await _GSM02000ViewModel.ActiveInactiveProcessAsync();
        //         }
        //     }
        // }
        // catch (Exception ex)
        // {
        //     loException.Add(ex);
        // }
        // loException.ThrowExceptionIfErrors();
    }

    private Task AfterOpenActiveInactive(R_AfterOpenPopupEventArgs arg)
    {
        throw new NotImplementedException();
    }
}