using BlazorClientHelper;
using ICB00300Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace ICB00300Front;

public partial class ICB00300 : R_Page
{
    private ICB00300ViewModel _viewModel = new();
    [Inject] private IClientHelper _clientHelper { get; set; }

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.Param.CCOMPANY_ID = _clientHelper.CompanyId;
            _viewModel.Param.CUSER_ID = _clientHelper.UserId;
            await _viewModel.Init();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void BeforeOpenProcess(R_BeforeOpenPopupEventArgs eventArgs)
    {
        //Property, Recalculate Type, Update Balance, Fail Facility
        eventArgs.Parameter = _viewModel.Param;
        eventArgs.PageTitle = _localizer["Product"];
        eventArgs.TargetPageType = typeof(ICB00300Process);
    }
}