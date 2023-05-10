using GSM02000Common.DTOs;
using GSM02000Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GSM02000Front;

public partial class GSM02000
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
            arg.Result = _GSM02000ViewModel.loEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }

    private Task Conductor_Validation(R_ValidationEventArgs arg)
    {
        throw new NotImplementedException();
    }
}