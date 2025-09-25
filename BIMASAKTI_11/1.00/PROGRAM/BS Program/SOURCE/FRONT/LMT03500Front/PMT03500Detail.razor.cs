using PMT03500Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;

namespace PMT03500Front;

public partial class PMT03500Detail : R_Page
{
    private PMT03500UtilityDetailViewModel _viewModel = new();

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.Init(poParameter);
            await _viewModel.GetRecord(_viewModel.Param.OUTILITY_HEADER);
            
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}