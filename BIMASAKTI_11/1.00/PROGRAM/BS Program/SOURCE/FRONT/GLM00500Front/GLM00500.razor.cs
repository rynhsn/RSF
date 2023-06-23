using GLM00500Common.DTOs;
using GLM00500Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GLM00500Front;

public partial class GLM00500 : R_Page
{
    private GLM00500HeaderViewModel _viewModel = new();
    private R_Conductor _conductorRef;
    private R_Grid<GLM00500BudgetHDDTO> _gridRef;
    
    
    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.Init();
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    R_eMessageBoxResult _messageBoxResult;
     private async Task RefreshList()
     {
         await _gridRef.R_RefreshGrid(null);
     }
    
    //GetList
    private async Task GetBudgetHDList(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetBudgetHDList(_viewModel.SelectedYear);
            eventArgs.ListEntityResult = _viewModel.BudgetHDList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    //Get
    private async Task GetBudgetHD(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GLM00500BudgetHDDTO>(eventArgs.Data);
            await _viewModel.GetBudgetHDEntity(loParam);
            eventArgs.Result = _viewModel.BudgetHDEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}