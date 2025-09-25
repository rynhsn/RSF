using BlazorClientHelper;
using ICR00600Common.DTOs.Print;
using ICR00600Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;

namespace ICR00600Front;

public partial class ICR00600PopupSaveAs : R_Page
{
    private ICR00600ViewModel _viewModel { get; set; } = new();
    [Inject] private IClientHelper _clientHelper { get; set; }
    [Inject] private R_IReport _reportService { get; set; }

    protected override async Task R_Init_From_Master(object poParameter)
    {
        R_Exception loEx = new R_Exception();
        try
        {
            _viewModel.ReportParam = (ICR00600ReportParam)poParameter;
            _viewModel.ReportParam.CREPORT_FILETYPE = _viewModel.FileType[0];
            _viewModel.ReportParam.CREPORT_FILENAME = "";
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task OnClickSave()
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.ReportParam.LIS_PRINT = false;
            await Close(false, null);

            var loParam = _viewModel.ReportParam;
            loParam.CCOMPANY_ID = _clientHelper.CompanyId;
            loParam.CUSER_ID = _clientHelper.UserId;
            loParam.CLANG_ID = _clientHelper.CultureUI.TwoLetterISOLanguageName;
            await _reportService.GetReport(
                "R_DefaultServiceUrlIC",
                "IC",
                "rpt/ICR00600Print/StockTakeActivityReportPost",
                "rpt/ICR00600Print/StockTakeActivityReportGet",
                loParam
            );
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnClickCancel()
    {
        R_Exception loEx = new R_Exception();
        try
        {
            await Close(false, null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}