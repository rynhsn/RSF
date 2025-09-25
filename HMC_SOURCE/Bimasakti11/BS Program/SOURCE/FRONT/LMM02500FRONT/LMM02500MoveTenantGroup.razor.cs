using LMM02500Common.DTO;
using LMM02500Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace LMM02500Front;

public partial class LMM02500MoveTenantGroup : R_Page
{
    private readonly LMM02520ViewModeLMM02520MoveTenantViewModel _viewModelMoveTenant = new();
    private R_ConductorGrid? _conductorMoveTenantRef;
    private R_Conductor? _conductorToTenantRef;
    private R_Grid<TenantListForMoveProcessDTO>? _gridMoveTenantRef;

    private readonly R_Grid<LMM02520GridDTO>? _gridTenantCategoryRef;

    private bool? IsMoveTenantModalHidden = true;
    private bool? IsTenantCategoryHidden = true;

    protected override async Task R_Init_From_Master(object poParameter)
    {
        //poParameter has data CPROPERTY ID,
        //you can GOT CFROM if you parse data from LMM02500.razor.cs
        var loEx = new R_Exception();
        var loParam = (TenantGroupForMoveParameterFrontDTO)poParameter;

        try
        {
            await _viewModelMoveTenant.GetEntity(loParam, "FROM");
            await _viewModelMoveTenant.GetAllTenantGroupList(loParam.CPROPERTY_ID);

            var loParamForList = R_FrontUtility.ConvertObjectToObject<R_ServiceGetListRecordEventArgs>(loParam);

            await R_ServiceGetListRecordAsync(loParamForList);

            SwitchModal(false);

            await _gridMoveTenantRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }


    private async Task PropertyDropdown_OnChange(object poParam)
    {
        var loEx = new R_Exception();
        var loToParam = new TenantGroupForMoveParameterFrontDTO();


        try
        {
            loToParam.CTO_TENANT_GROUP = (string)poParam;
            loToParam.CPROPERTY_ID = _viewModelMoveTenant.lcPropertyId;

            //_viewModelMoveTenant.loToTenantCategory.CTENANT_GROUP_NAME
            await _viewModelMoveTenant.GetEntity(loToParam, "TO");
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModelMoveTenant.GetTenantListForMoveProcess();
            eventArgs.ListEntityResult = _viewModelMoveTenant.loTenantList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnClickProcessButton()
    {
        var loEx = new R_Exception();

        try
        {
            await _conductorMoveTenantRef.R_SaveBatch();
            await Close(true, true);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnClickCancelButton()
    {
        await Close(true, false);
    }

    public async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModelMoveTenant.GetAllTenantGroupList();

            eventArgs.ListEntityResult = _viewModelMoveTenant.loToTenantListLMM02500;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void SwitchModal(bool plParam)
    {
        IsMoveTenantModalHidden = plParam;
        IsTenantCategoryHidden = !IsMoveTenantModalHidden;
    }

    private void Button_OnClickLookUp()
    {
        SwitchModal(true);
        _gridTenantCategoryRef.R_RefreshGrid(null);
    }

    public void Button_OnClickOk()
    {
        var loData = _gridTenantCategoryRef.GetCurrentData();
        _viewModelMoveTenant.loToTenantCategory = (LMM02500ProfileDTO)loData;
        SwitchModal(false);
    }

    public void Button_OnClickClose()
    {
        SwitchModal(false);
    }

    private void R_BeforeSaveBatch(R_BeforeSaveBatchEventArgs events)
    {
        var loData = (List<TenantListForMoveProcessDTO>)events.Data;

        events.Cancel = loData.Count == 0;
    }

    private async Task R_ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModelMoveTenant.loGetTenantBatchList = (List<TenantListForMoveProcessDTO>)eventArgs.Data;
            await _viewModelMoveTenant.SaveMoveTenantGroupCategory();
            
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void R_AfterSaveBatch(R_AfterSaveBatchEventArgs eventArgs)
    {
    }
}