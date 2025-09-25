using BlazorClientHelper;
using HDR00200Common.DTOs.Print;
using HDR00200Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;

namespace HDR00200Front;

public partial class HDR00200PopupSaveAs : R_Page
{
    private R_TextBox _textFileName;
    private HDR00200ViewModel _viewModel { get; set; } = new();
    [Inject] private IClientHelper _clientHelper { get; set; }
    [Inject] private R_IReport _reportService { get; set; }

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();
        try
        {
            _viewModel.ReportParam = (HDR00200ReportParam)poParameter;
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
            if (_viewModel.ReportParam.CREPORT_TYPE == "M")
            {
                await _reportService.GetReport(
                    "R_DefaultServiceUrlHD",
                    "HD",
                    "rpt/HDR00200PrintMaintenance/ActivityReportPost",
                    "rpt/HDR00200PrintMaintenance/ActivityReportGet",
                    loParam
                );
            }
            else if (_viewModel.ReportParam.CREPORT_TYPE == "C")
            {
                if (_viewModel.ReportParam.CAREA == "01")
                {
                    await _reportService.GetReport(
                        "R_DefaultServiceUrlHD",
                        "HD",
                        "rpt/HDR00200PrintPrivate/ActivityReportPost",
                        "rpt/HDR00200PrintPrivate/ActivityReportGet",
                        loParam
                    );
                }else if (_viewModel.ReportParam.CAREA == "02")
                {
                    await _reportService.GetReport(
                        "R_DefaultServiceUrlHD",
                        "HD",
                        "rpt/HDR00200PrintPublic/ActivityReportPost",
                        "rpt/HDR00200PrintPublic/ActivityReportGet",
                        loParam
                    );
                }
            }
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