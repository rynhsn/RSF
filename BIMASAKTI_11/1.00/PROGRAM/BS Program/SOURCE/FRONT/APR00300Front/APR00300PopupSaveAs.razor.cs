using APR00300Common.DTOs.Print;
using APR00300Model.ViewModel;
using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;

namespace APR00300Front;

public partial class APR00300PopupSaveAs
{
    private APR00300ViewModel _viewModel { get; set; } = new();
    [Inject] private IClientHelper _clientHelper { get; set; }
    [Inject] private R_IReport _reportService { get; set; }

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();
        try
        {
            _viewModel.ReportParam = (APR00300ReportParam)poParameter;
            _viewModel.ReportParam.LIS_PRINT = false;
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
            await Close(false, null);
            var loParam = _viewModel.ReportParam;
            loParam.CCOMPANY_ID = _clientHelper.CompanyId;
            loParam.CUSER_ID = _clientHelper.UserId;
            loParam.CLANG_ID = _clientHelper.CultureUI.TwoLetterISOLanguageName;
            await _reportService.GetReport(
                "R_DefaultServiceUrlAP",
                "AP",
                "rpt/APR00300Print/ActivityReportPost",
                "rpt/APR00300Print/ActivityReportGet",
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