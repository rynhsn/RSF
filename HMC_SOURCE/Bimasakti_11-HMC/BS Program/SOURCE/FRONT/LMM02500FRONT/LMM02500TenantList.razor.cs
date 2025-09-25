using LMM02500Common.DTO;
using LMM02500Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace LMM02500Front;

public partial class LMM02500TenantList : R_Page
{
    private R_ConductorGrid? _conductorLMM02520;
    private R_Grid<LMM02520GridDTO>? _gridRefLMM02520;
    private readonly LMM02520ViewModel _viewModelLMM02520 = new();

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModelLMM02520.loTabParameter = (LMM02500TabParameterDTO)poParameter;

            await _gridRefLMM02520.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task R_ServiceGetListRecordLMM02520(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            await _viewModelLMM02520.GetAllTenantGroupList();
            
            eventArgs.ListEntityResult = _viewModelLMM02520.loGridListLMM02520;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    

    private async Task R_DisplayLMM02520(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (_viewModelLMM02520.loGridListLMM02520.Count != 0)
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM02520DTO>(eventArgs.Data);
                await _viewModelLMM02520.GetEntity(loParam);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #region MoveTenant

    private R_ConductorGrid? _conductorTenantRef;

    private void R_Before_Open_Popup_Tenant_Move(R_BeforeOpenPopupEventArgs eventArgs)
    {
        var loParam = new TenantGroupForMoveParameterFrontDTO();
        loParam.CPROPERTY_ID = _viewModelLMM02520.loTabParameter.CPROPERTY_ID;
        loParam.CFROM_TENANT_GROUP = _viewModelLMM02520.loTabParameter.CTENANT_GROUP_ID; 
        eventArgs.Parameter = loParam;
        eventArgs.TargetPageType = typeof(LMM02500MoveTenantGroup);
    }

    private async Task R_After_Open_Popup_Tenant_Move(R_AfterOpenPopupEventArgs eventArgs)
    {
        var loException = new R_Exception();
        try
        {
            var result = eventArgs.Result;
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();
        await _gridRefLMM02520.R_RefreshGrid(null);
    }

    #endregion

}