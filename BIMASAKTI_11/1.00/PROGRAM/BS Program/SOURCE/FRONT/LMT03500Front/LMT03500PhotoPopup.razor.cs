using PMT03500Model.ViewModel;
using R_BlazorFrontEnd.Exceptions;

namespace LMT03500Front;

public partial class LMT03500PhotoPopup
{
    public PMT03500UtilityDetailViewModel _viewModel = new();

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.Init(poParameter);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}