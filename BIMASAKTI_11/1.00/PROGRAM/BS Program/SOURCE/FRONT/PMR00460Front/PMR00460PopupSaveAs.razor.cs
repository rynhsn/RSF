using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using PMR00460Common.DTOs.Print;
using PMR00460Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;

namespace PMR00460Front;

public partial class PMR00460PopupSaveAs : R_Page
{
    private R_TextBox _textFileName;
    private PMR00460ViewModel _viewModel { get; set; } = new();
    [Inject] private IClientHelper _clientHelper { get; set; }
    [Inject] private R_IReport _reportService { get; set; }

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();
        try
        {
            _viewModel.ReportParam = (PMR00460ReportParam)poParameter;
            _viewModel.ReportParam.CREPORT_FILETYPE = _viewModel.FileType[0];
            _viewModel.ReportParam.CREPORT_FILENAME = "";
            await _textFileName.FocusAsync();
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
                "R_DefaultServiceUrlPM",
                "PM",
                "rpt/PMR00460Print/ActivityReportPost",
                "rpt/PMR00460Print/ActivityReportGet",
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