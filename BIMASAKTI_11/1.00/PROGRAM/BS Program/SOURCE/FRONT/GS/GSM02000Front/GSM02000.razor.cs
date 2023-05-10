using GSM02000Common.DTOs;
using GSM02000Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM02000Front;

public partial class GSM02000 : R_Page
{
    private GSM02000ViewModel _GSM02000ViewModel = new();
    private R_Conductor _conductorRef;

    private R_Grid<GSM02000GridDTO> _gridRef;

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

    private async Task Conductor_Validation(R_ValidationEventArgs arg)
    {
        // var loEx = new R_Exception();
        //
        // try
        // {
        //     var loData = (GSM02000DTO)arg.Data;
        //
        //     //cek apakah data  CTAX_ID yang dimasukkan dengan id yang sama ketika saving
        //     if (arg.ConductorMode == R_eConductorMode.Add)
        //     {
        //         var loParam = new GSM02000DTO();
        //         loParam.CTAX_ID = loData.CTAX_ID;
        //         await _GSM02000ViewModel.GetEntity(loParam);
        //
        //         if (_GSM02000ViewModel.Entity != null)
        //         {
        //             loEx.Add("2001", );
        //         }
        //     }
        //     
        // }
        // catch (Exception ex)
        // {
        //     loEx.Add(ex);
        // }
        //
        // if (loEx.HasError)
        //     arg.Cancel = true;
        //
        // loEx.ThrowExceptionIfErrors();
        
        throw new NotImplementedException();
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

    private Task Conductor_AfterAdd(R_AfterAddEventArgs arg)
    {
        throw new NotImplementedException();
    }
}