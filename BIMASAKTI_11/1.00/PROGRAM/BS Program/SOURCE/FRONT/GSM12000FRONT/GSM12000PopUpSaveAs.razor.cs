using BlazorClientHelper;
using GSM12000COMMON;
using GSM12000FrontResources;
using GSM12000MODEL;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;

namespace GSM12000FRONT;

public partial class GSM12000PopUpSaveAs : R_Page
{
    private GSM12000ViewModel _viewModel { get; set; } = new();
    [Inject] private IClientHelper _clientHelper { get; set; }
    [Inject] private R_IReport _reportService { get; set; }
    public R_Button ProcessButton { get; set; }
    private R_Conductor _conductorRef;
    [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }

    protected override async Task R_Init_From_Master(object poParameter)
    {
        R_Exception loEx = new R_Exception();
        try
        {
            _viewModel.PrintParam = (GSM12000PrintParamDTO)poParameter;
            _viewModel.PrintParam.CREPORT_FILETYPE = "";
            _viewModel.PrintParam.CREPORT_FILENAME = "";
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task OnClickOk()
    {
        var loEx = new R_Exception();
        string periodCombo = "";
        string periodName = "";
        GSM12000PrintParamDTO loParam;

        try
        {
            // periodCombo = _viewModel.yearPeriod.ToString() + _viewModel.getPeriodFrom;
            // periodName = _viewModel.yearPeriod.ToString() +"-"+ _viewModel.getPeriodFrom;
            // string logettransaction =  string.IsNullOrWhiteSpace(_viewModel.getTransaction) ? "":_viewModel.getTransaction;
            // string logwtstatus =  string.IsNullOrWhiteSpace(_viewModel.getStatus) ? "":_viewModel.getStatus;


            loParam = new GSM12000PrintParamDTO();
            _viewModel.PrintParam.LIS_PRINT = false;
            await Close(false, null);

            await _reportService.GetReport(
                "R_DefaultServiceUrl",
                "GS",
                "rpt/GSM12000PrintController/AllMessagePost",
                "rpt/GSM12000PrintController/AllStreamMessageGet",
                loParam);
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

    private async Task OnChanged(object poParam)
    {
        var loEx = new R_Exception();
        string lsProperty = (string)poParam ?? ""; // Set default value to an empty string
        try
        {
            _viewModel.FileTypeValue = lsProperty;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }


        R_DisplayException(loEx);
    }
}
