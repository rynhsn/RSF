using BlazorClientHelper;
using GSM12000COMMON;
using GSM12000MODEL;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;

namespace GSM12000FRONT;

public partial class GSM12000Print : R_Page
{
    private GSM12000ViewModel  _viewModel = new();
    private R_Grid<GSM12000DTO> _gridRef;
    private R_Conductor _conductorRef;
    private R_TextBox _messageNoRef;
    private R_TextBox _messageDescRef;
    private string messageTypeValue = "";
    
    
    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetListMessage();
            var loParam = (GSM12000PrintParamDTO)poParameter;
            messageTypeValue = loParam.CMESSAGE_TYPE;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }
    
    private async Task OnChangedFrom(object poParam)
    {
        var loEx = new R_Exception();
        string lsmessageTypeValue = (string)poParam ?? ""; // Set default value to an empty string
        try
        {
            _viewModel.messageNoFromValue = lsmessageTypeValue;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }


        R_DisplayException(loEx);
    }
    
    private async Task OnChangedTO(object poParam)
    {
        var loEx = new R_Exception();
        string lsmessageTypeValue = (string)poParam ?? ""; // Set default value to an empty string
        try
        {
            _viewModel.messageNoToValue = lsmessageTypeValue;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }


        R_DisplayException(loEx);
    }
    
    [Inject] private IClientHelper _clientHelper { get; set; }
    [Inject] private R_IReport _reportService { get; set; }

    private async Task Button_OnClickProcessAsync()
    {
        var loEx = new R_Exception();

        try
        {
            if (string.IsNullOrEmpty(_viewModel.messageNoToValue.ToString()))
            {
                loEx.Add(_localizer["ERROR"], _localizer["PLEASE_SELECT_YOUR_MESSAGE_NO_TO"]);
            }
            if (string.IsNullOrEmpty(_viewModel.messageNoFromValue.ToString()))
            {
                loEx.Add(_localizer["ERROR"], _localizer["PLEASE_SELECT_YOUR_MESSAGE_FROM_TO"]);
            }
            if (loEx.HasError) goto EndBlock;

            var loParam = new GSM12000PrintParamDTO()
            {
                LIS_PRINT = true,
                CREPORT_CULTURE = _clientHelper.ReportCulture,
                CCOMPANY_ID = _clientHelper.CompanyId,
                CMESSAGE_TYPE =  messageTypeValue,
                CMESSAGE_NO_FROM = _viewModel.messageNoFromValue,
                CMESSAGE_NO_TO = _viewModel.messageNoToValue,
                LPRINT_INACTIVE = _viewModel.activeInactiveMessage,
                CUSER_LOGIN_ID = _clientHelper.UserId,
                
            };

                await _reportService.GetReport(
                    "R_DefaultServiceUrl",
                    "GS",
                    "rpt/GSM12000Print/AllMessagePost",
                    "rpt/GSM12000Print/AllStreamMessageGet",
                    loParam);

           
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }
    
    private void BeforeOpen_PopupSaveAsAsync(R_BeforeOpenPopupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        string periodCombo = "";
        string periodName = "";
        GSM12000PrintParamDTO loParam;
        try
        {
            loParam = new GSM12000PrintParamDTO()
            {
                LIS_PRINT = true,
                CCOMPANY_ID = _clientHelper.CompanyId,
                CMESSAGE_TYPE = _viewModel.messageTypeValue,
                CMESSAGE_NO_FROM = _viewModel.messageNoFromValue,
                CMESSAGE_NO_TO = _viewModel.messageNoToValue,
                LPRINT_INACTIVE = _viewModel.activeInactiveMessage,
                CUSER_LOGIN_ID = _clientHelper.UserId,
            };
            eventArgs.Parameter = loParam;
            eventArgs.PageTitle = _localizer["SAVE_AS"];
            eventArgs.TargetPageType = typeof(GSM12000PopUpSaveAs);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }

    
}