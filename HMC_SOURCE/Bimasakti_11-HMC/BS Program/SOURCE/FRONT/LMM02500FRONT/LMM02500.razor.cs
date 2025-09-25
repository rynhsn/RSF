using BlazorClientHelper;
using LMM02500Common.DTO;
using LMM02500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace LMM02500Front;

public partial class LMM02500 : R_Page
{
    /* LMM02500 */

    #region Master List

    private readonly LMM02500ViewModel _viewModelLMM02500 = new();
    private R_ConductorGrid? _conductorLMM02500;
    private R_Grid<LMM02500ProfileDTO>? _gridRefLMM02500;


    private bool _isDataExist;

    private bool _pageContractorOnCRUDmode;

    [Inject] private IClientHelper? _clientHelper { get; set; }
    
    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModelLMM02500.GetPropertyList();
            await _gridRefLMM02500.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }


    private async Task PropertyDropdown_OnChange(string poParam)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModelLMM02500.PropertyValueContext = poParam;
            //await R_ServiceGetListRecord(null);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            await _gridRefLMM02500.R_RefreshGrid(null);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                if (_tabStripRef.ActiveTab.Id == "Profile")
                {
                    await _tabProfileRef.InvokeRefreshTabPageAsync(_loTabParameter);
                }
                else if (_tabStripRef.ActiveTab.Id == "TenantList")
                {
                    await _tabTenantRef.InvokeRefreshTabPageAsync(_loTabParameter);
                }

        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            await _viewModelLMM02500.GetAllTenantGroupList();
            eventArgs.ListEntityResult = _viewModelLMM02500.loGridListLMM02500;
            if (!_viewModelLMM02500.loGridListLMM02500.Any())
            {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                _loTabParameter.CTENANT_GROUP_ID = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
                _isDataExist = false;
            }
            else if (_viewModelLMM02500.loGridListLMM02500.Any())
            {
                _loTabParameter.CTENANT_GROUP_ID =
                    _viewModelLMM02500.loGridListLMM02500.FirstOrDefault()!.CTENANT_GROUP_ID;
                _isDataExist = true;

            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task R_DisplayGetRecordLMM02500(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            //Untuk Tab - 1
            var loConvertTabParameter = R_FrontUtility.ConvertObjectToObject<LMM02500TabParameterDTO>(eventArgs.Data);
            _loTabParameter = new LMM02500TabParameterDTO();
            _loTabParameter = loConvertTabParameter;
            if (_clientHelper?.UserId != null) _loTabParameter.CUSER_LOGIN_ID = _clientHelper.UserId;
            await Task.CompletedTask;
            
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion


    #region Master Tab
    private R_TabStrip? _tabStripRef;
    private R_TabPage? _tabProfileRef;
    private R_TabPage? _tabTenantRef;

    private LMM02500TabParameterDTO _loTabParameter = new();

    #region Tab List
    private async Task OnActiveTabIndexChanged(R_TabStripTab eventArgs)
    {
        if (eventArgs.Id == "List")
        {
            await _gridRefLMM02500.R_RefreshGrid(null);
        }
    }
    private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
    {
        _viewModelLMM02500._comboBoxEnabled = true;
        eventArgs.Cancel = _pageContractorOnCRUDmode;
        if (eventArgs.TabStripTab.Id != "List")
        {
            _viewModelLMM02500._comboBoxEnabled = false;
        }
    }
    #endregion

    #region Utulities
    private void R_TabEventCallback(object poValue)
    {
        var loEx = new R_Exception();

        try
        {
            //_viewModelLMM02500._comboBoxEnabled = (bool)poValue;
            _pageContractorOnCRUDmode = !(bool)poValue;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();

    }
    #endregion

    #region Tab Profile
    private void General_Before_Open_Profile_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
    {
        eventArgs.Parameter = _loTabParameter;
        eventArgs.TargetPageType = typeof(LMM02500Profile);
    }
    #endregion

    #region Tab Tenant
    private void General_Before_Open_Tenant_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
    {
        eventArgs.Parameter = _loTabParameter;
        eventArgs.TargetPageType = typeof(LMM02500TenantList);
    }
    #endregion

    #endregion
}