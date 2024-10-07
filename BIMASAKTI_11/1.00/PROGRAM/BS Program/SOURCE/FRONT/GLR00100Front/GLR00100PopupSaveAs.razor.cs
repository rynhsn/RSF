using BlazorClientHelper;
using GLR00100Common.Params;
using GLR00100Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;

namespace GLR00100Front;

public partial class GLR00100PopupSaveAs : R_Page
{
    private GLR00100ViewModel _viewModel { get; set; } = new();
    [Inject] private IClientHelper _clientHelper { get; set; }
    [Inject] private R_IReport _reportService { get; set; }

    protected override async Task R_Init_From_Master(object poParameter)
    {
        R_Exception loEx = new R_Exception();
        try
        {
            _viewModel.ReportParam = (GLR00100ReportParam)poParameter;
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
            loParam.CLANGUAGE_ID = _clientHelper.CultureUI.TwoLetterISOLanguageName;
            loParam.CREPORT_CULTURE = _clientHelper.ReportCulture;

            if (loParam.CREPORT_TYPE == _localizer["BASED_ON_TRANS_CODE"])
            {
                await _reportService.GetReport(
                    "R_DefaultServiceUrlGL",
                    "GL",
                    "rpt/GLR00100PrintBasedOnTransCode/ActivityReportBasedOnTransCodePost",
                    "rpt/GLR00100PrintBasedOnTransCode/ActivityReportBasedOnTransCodeGet",
                    loParam
                );
            }
            else if (loParam.CREPORT_TYPE == _localizer["BASED_ON_DATE"])
            {
                await _reportService.GetReport(
                    "R_DefaultServiceUrlGL",
                    "GL",
                    "rpt/GLR00100PrintBasedOnDate/ActivityReportBasedOnDatePost",
                    "rpt/GLR00100PrintBasedOnDate/ActivityReportBasedOnDateGet",
                    loParam
                );
            }
            else if (loParam.CREPORT_TYPE == _localizer["BASED_ON_REF_NO"])
            {
                await _reportService.GetReport(
                    "R_DefaultServiceUrlGL",
                    "GL",
                    "rpt/GLR00100PrintBasedOnRefNo/ActivityReportBasedOnRefNoPost",
                    "rpt/GLR00100PrintBasedOnRefNo/ActivityReportBasedOnRefNoGet",
                    loParam
                );
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