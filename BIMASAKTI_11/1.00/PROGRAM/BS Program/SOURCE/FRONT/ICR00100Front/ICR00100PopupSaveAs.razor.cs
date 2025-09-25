using BlazorClientHelper;
using ICR00100Common.DTOs.Print;
using ICR00100Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;

namespace ICR00100Front;

public partial class ICR00100PopupSaveAs : R_Page
{
    private ICR00100ViewModel _viewModel { get; set; } = new();
    [Inject] private IClientHelper _clientHelper { get; set; }
    [Inject] private R_IReport _reportService { get; set; }

    protected override async Task R_Init_From_Master(object poParameter)
    {
        R_Exception loEx = new R_Exception();
        try
        {
            _viewModel.ReportParam = (ICR00100ReportParam)poParameter;
            _viewModel.ReportParam.CREPORT_FILETYPE = _viewModel.FileType[0];
            _viewModel.ReportParam.CREPORT_FILENAME = "";
            _viewModel.ReportParam.LIS_PRINT = false;
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
            await Close(false, null);

            var loParam = _viewModel.ReportParam;
            loParam.CCOMPANY_ID = _clientHelper.CompanyId;
            loParam.CUSER_ID = _clientHelper.UserId;
            loParam.CLANG_ID = _clientHelper.CultureUI.TwoLetterISOLanguageName;
            await _reportService.GetReport(
                "R_DefaultServiceUrlIC",
                "IC",
                "rpt/ICR00100Print/ActivityReportPost",
                "rpt/ICR00100Print/ActivityReportGet",
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