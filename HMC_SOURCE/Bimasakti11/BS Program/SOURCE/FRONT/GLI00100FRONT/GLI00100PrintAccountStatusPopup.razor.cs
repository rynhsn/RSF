using BlazorClientHelper;
using GLI00100Common.DTOs;
using GLI00100Model.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;

namespace GLI00100Front;

public partial class GLI00100PrintAccountStatusPopup : R_Page
{
    private GLI00100ViewModel _mainViewModel = new();
    private GLI00100PopupParamsDTO _parameter = new();

    [Inject] private R_IReport _reportService { get; set; }
    [Inject] private IClientHelper _clientHelper { get; set; }
    // private GSM05000ApprovalCopyDTO _copyFrom = new();

    protected override Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (GLI00100PopupParamsDTO)poParameter;
            _parameter.CCOMPANY_ID = _clientHelper.CompanyId;
            _parameter.CUSER_ID = _clientHelper.UserId;
            _parameter.CLANGUAGE_ID = _clientHelper.CultureUI.TwoLetterISOLanguageName;
            _parameter.CGLACCOUNT_NO = loParam.CGLACCOUNT_NO;
            _parameter.CGLACCOUNT_NAME = loParam.CGLACCOUNT_NAME;
            _parameter.CYEAR = loParam.CYEAR;
            _parameter.CCURRENCY_TYPE = loParam.CCURRENCY_TYPE;
            _parameter.CCENTER_CODE = loParam.CCENTER_CODE;
            _parameter.CCENTER_NAME = loParam.CCENTER_NAME;
            _parameter.CBUDGET_NO = loParam.CBUDGET_NO;
            _parameter.CBUDGET_NAME_DISPLAY = loParam.CBUDGET_NAME_DISPLAY;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return Task.CompletedTask;
    }

    private async Task OnClickPrint(MouseEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        GLI00100PopupParamsDTO loParam;

        try
        {
            loParam = _parameter;
            await _reportService.GetReport(
                "R_DefaultServiceUrlGL",
                "GL",
                "rpt/GLI00100Print/AccountStatusPost",
                "rpt/GLI00100Print/AccountStatusGet",
                loParam
            );
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }
}