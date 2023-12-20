using GSM04500Common.DTOs;
using GSM04500Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM04500Front;

public partial class GSM04500AccSetting
{
    private GSM04500AccountSettingViewModel _viewModel = new();
    private R_ConductorGrid _conductorRef;
    private R_Grid<GSM04500AccountSettingDTO> _gridRef;

    private GSM04500AccountSettingDeptViewModel _viewModelDept = new();
    private R_ConductorGrid _conductorRefDept;
    private R_Grid<GSM04500AccountSettingDeptDTO> _gridRefDept;
    private bool _flagGrid;
    private bool _flagGridDept;

    // private R_TabStripTab _tabAccSetting;
    // private R_TabStrip _tab;
    // private R_TabPage _tabPageAccSetting;
    // private bool _flagCombo;

    #region Account Setting

    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.Init(poParam);
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Display(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loData = (GSM04500AccountSettingDTO)eventArgs.Data;
            _flagGridDept = loData.LDEPARTMENT_MODE;
            
            await _viewModelDept.Init(loData);
            await _gridRefDept.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetGridList(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetGridList();
            eventArgs.ListEntityResult = _viewModel.GridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM04500AccountSettingDTO>(eventArgs.Data);

            await _viewModel.GetEntity(loParam);
            eventArgs.Result = _viewModel.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Save(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM04500AccountSettingDTO>(eventArgs.Data);
            loParam.CGLACCOUNT_NO ??= string.Empty;
            await _viewModel.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);
            eventArgs.Result = _viewModel.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void BeforeLookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
    {
        var loParam = new GSL00520ParameterDTO();
        loParam.CGOA_CODE = _viewModel.Entity.CGOA_CODE;
        eventArgs.Parameter = loParam;
        eventArgs.TargetPageType = typeof(GSL00520);
    }

    private void AfterLookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            if (eventArgs.Result == null) return;

            var loResult = (GSL00520DTO)eventArgs.Result;
            var loGetData = (GSM04500AccountSettingDTO)eventArgs.ColumnData;
            loGetData.CGLACCOUNT_NO = loResult.CGLACCOUNT_NO;
            loGetData.CGLACCOUNT_NAME = loResult.CGLACCOUNT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void SetOther(R_SetEventArgs eventArgs)
    {
        _flagGridDept = eventArgs.Enable;
    }

    #endregion

    #region Account Setting Dept

    private async Task GetGridListDept(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModelDept.GetGridList();
            eventArgs.ListEntityResult = _viewModelDept.GridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetRecordDept(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM04500AccountSettingDeptDTO>(eventArgs.Data);
            await _viewModelDept.GetEntity(loParam);
            eventArgs.Result = _viewModelDept.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task SaveDept(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM04500AccountSettingDeptDTO>(eventArgs.Data);
            loParam.CGLACCOUNT_NO ??= string.Empty;
            await _viewModelDept.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);
            eventArgs.Result = _viewModelDept.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    //delete
    private async Task DeleteDept(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM04500AccountSettingDeptDTO>(eventArgs.Data);
            await _viewModelDept.DeleteEntity(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void BeforeLookupDept(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
    {
        if (eventArgs.ColumnName == nameof(GSM04500AccountSettingDeptDTO.CDEPT_CODE))
        {
            var loParam = new GSL00700ParameterDTO();
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00700);
        }
        else if (eventArgs.ColumnName == nameof(GSM04500AccountSettingDeptDTO.CGLACCOUNT_NO))
        {
            var loParam = new GSL00520ParameterDTO();
            loParam.CGOA_CODE = _viewModelDept.ParentEntity.CGOA_CODE;
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00520);
        }
    }
    
    private void AfterLookupDept(R_AfterOpenGridLookupColumnEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            if (eventArgs.Result == null) return;

            if (eventArgs.ColumnName == nameof(GSM04500AccountSettingDeptDTO.CDEPT_CODE))
            {
                var loResult = (GSL00700DTO)eventArgs.Result;
                var loGetData = (GSM04500AccountSettingDeptDTO)eventArgs.ColumnData;
                loGetData.CDEPT_CODE = loResult.CDEPT_CODE;
                loGetData.CDEPT_NAME = loResult.CDEPT_NAME;
            }
            else if (eventArgs.ColumnName == nameof(GSM04500AccountSettingDeptDTO.CGLACCOUNT_NO))
            {
                var loResult = (GSL00520DTO)eventArgs.Result;
                var loGetData = (GSM04500AccountSettingDeptDTO)eventArgs.ColumnData;
                loGetData.CGLACCOUNT_NO = loResult.CGLACCOUNT_NO;
                loGetData.CGLACCOUNT_NAME = loResult.CGLACCOUNT_NAME;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private void SetOtherDept(R_SetEventArgs eventArgs)
    {
        _flagGrid = eventArgs.Enable;
    }

    #endregion
}