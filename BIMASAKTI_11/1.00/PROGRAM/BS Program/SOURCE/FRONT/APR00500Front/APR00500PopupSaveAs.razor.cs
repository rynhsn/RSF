using APR00500Common.DTOs.Print;
using APR00500Model.ViewModel;
using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;

namespace APR00500Front;

public partial class APR00500PopupSaveAs : R_Page
{
    private APR00500ViewModel _viewModel { get; set; } = new();
    [Inject] private IClientHelper _clientHelper { get; set; }
    [Inject] private R_IReport _reportService { get; set; }

    protected override async Task R_Init_From_Master(object poParameter)
    {
        R_Exception loEx = new R_Exception();
        try
        {
            _viewModel.ReportParam = (APR00500ReportParam)poParameter;
            _viewModel.ReportParam.CREPORT_FILETYPE = "";
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
                "R_DefaultServiceUrlAP",
                "AP",
                "rpt/APR00500Print/ActivityReportPost",
                "rpt/APR00500Print/ActivityReportGet",
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