using ICI00200Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;

namespace ICI00200Front;

public partial class ICI00200PopupLastInfo : R_Page
{
    private ICI00200PopupViewModel _viewModel = new();
    
    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            var lcParam = (string) poParam;
            await _viewModel.GetLastInfoDetail(lcParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}