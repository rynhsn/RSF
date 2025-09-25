using GLM00501Model.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;

namespace GLM00501Front;

public partial class GLM00501 : R_Page
{
    private GLM00501ViewModel _viewModel = new GLM00501ViewModel();   
    [Inject] private IJSRuntime JS { get; set; }
    
    private async Task DownloadTemplate()
    {
        var loEx = new R_Exception();

        try
        {
            var loByteFile = await _viewModel.DownloadTemplate();
            var saveFileName = $"ACCOUNT_BUDGET_UPLOAD_2.xlsx";
            await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}