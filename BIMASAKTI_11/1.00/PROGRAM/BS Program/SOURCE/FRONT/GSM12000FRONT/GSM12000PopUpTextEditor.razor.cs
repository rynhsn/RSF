using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;

namespace GSM12000FRONT;

public partial class GSM12000PopUpTextEditor: R_Page
{
    [Inject] private R_ILocalizer<GSM12000FrontResources.Resources_Dummy_Class> _localizer { get; set; } = null!;

    private string? lcContent;

    protected override async Task R_Init_From_Master(object poParameter)
    {
        R_Exception loEx = new R_Exception();

        try
        {
            lcContent = (string)poParameter;
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #region BUTTON METHODS

    private async Task OnClickOK()
    {
        R_Exception loEx = new R_Exception();

        try
        {
            await this.Close(true, lcContent);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnClickCancel()
    {
        await this.Close(false, null);
    }

    #endregion
}