using GLM00500Common.DTOs;
using GLM00500Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GLM00500Front;

public partial class GLM00500Detail
{
    private GLM00500DetailViewModel _viewModel = new();
    private R_Conductor _conductorRef;
    private R_Grid<GLM00500BudgetDTGridDTO> _gridRef = new();

    protected override async Task R_Init_From_Master(object eventArgs)
    {
        var loEx = new R_Exception();
        
        try
        {
            await _viewModel.Init(eventArgs);
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }
    
    //GetList
    private async Task GetBudgetDTList(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var lcBudgetId = _viewModel.BudgetHDEntity.CREC_ID;
            var lcAccountType = _viewModel.SelectedAccountType;
            await _viewModel.GetBudgetDTList(lcBudgetId, lcAccountType);
            eventArgs.ListEntityResult = _viewModel.BudgetDTList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }
    
    //Get
    private async Task GetBudgetDT(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var lcParam = R_FrontUtility.ConvertObjectToObject<GLM00500BudgetDTDTO>(eventArgs.Data);
            await _viewModel.GetBudgetDT(lcParam);
            eventArgs.Result = _viewModel.BudgetDTEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }
    
    //Add
}