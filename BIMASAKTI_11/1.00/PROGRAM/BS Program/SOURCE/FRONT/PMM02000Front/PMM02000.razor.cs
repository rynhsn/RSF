using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PMM02000Common;
using PMM02000Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace PMM02000Front;

public partial class PMM02000 : R_Page
{
    private PMM02000ViewModel _viewModel = new PMM02000ViewModel();   
    [Inject] private IJSRuntime JS { get; set; }
    
    private async Task DownloadTemplate()
    {
        var loEx = new R_Exception();

        try
        {
            var loByteFile = await _viewModel.DownloadTemplate();
            var saveFileName = $"Salesman.xlsx";
            await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void BeforeOpen(R_BeforeOpenPopupEventArgs obj)
    {
        // obj.Parameter = "ASHMD";
        obj.Parameter = "ASHMD";
        obj.TargetPageType = typeof(PMM02000Upload);
    }
}