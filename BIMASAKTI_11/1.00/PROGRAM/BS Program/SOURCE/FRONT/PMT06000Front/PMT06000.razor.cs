using Microsoft.AspNetCore.Components.Web;
using PMT06000Common.DTOs;
using PMT06000Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace PMT06000Front;

public partial class PMT06000 : R_Page
{
    private PMT06000ViewModel _viewModel = new();
    private R_Conductor _conductorRefOvertime;
    private R_Grid<PMT06000OvtGridDTO> _gridRefOvertime;

    private R_ConductorGrid _conductorRefService;
    private R_Grid<PMT06000OvtServiceGridDTO> _gridRefService;

    private R_ConductorGrid _conductorRefUnit;
    private R_Grid<PMT06000OvtUnitDTO> _gridRefUnit;

    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetPeriodList();
            await _viewModel.GetPropertyList();
            await _viewModel.GetYearRange();

            await _gridRefOvertime.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void OnChangedCombo(string value, string formName)
    {
        _viewModel.OnChangedComboOnList(value, formName);
    }

    private async Task GetOvertimeListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetOvertimeGridList();
            eventArgs.ListEntityResult = _viewModel.OvertimeGridList;
            if (_viewModel.OvertimeGridList.Count == 0)
            {
                _viewModel.Entity = new PMT06000OvtDTO();
                _viewModel.EntityService = new PMT06000OvtServiceDTO();
                await _gridRefService.R_RefreshGrid(null);
                await _gridRefOvertime.R_RefreshGrid(null);
            }
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
            _viewModel.Entity = R_FrontUtility.ConvertObjectToObject<PMT06000OvtDTO>(eventArgs.Data);
            eventArgs.Result = _viewModel.Entity;
            await _gridRefService.R_RefreshGrid(null);  
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetOvertimeServiceListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetOvertimeServiceGridList();
            eventArgs.ListEntityResult = _viewModel.OvertimeServiceGridList;
            if (_viewModel.OvertimeServiceGridList.Count == 0)
            {
                _viewModel.OvertimeUnitGridList.Clear();
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetOvertimeUnitListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetOvertimeUnitGridList();
            eventArgs.ListEntityResult = _viewModel.OvertimeUnitGridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void InstatiateTabInfo(R_InstantiateDockEventArgs eventArgs)
    {
        var loParam = new PMT06000ParameterDTO
        {
            CREC_ID = _viewModel.Entity.CREC_ID,
            isCaller = false
        };
        eventArgs.Parameter = loParam;
        eventArgs.TargetPageType = typeof(PMT06000Info);
    }

    private async Task DisplayService(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.EntityService = R_FrontUtility.ConvertObjectToObject<PMT06000OvtServiceDTO>(eventArgs.Data);
            await _gridRefUnit.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnClickRefresh(MouseEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _gridRefOvertime.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task AfterOpenUnit(R_AfterOpenPredefinedDockEventArgs eventArgs)
    {var loEx = new R_Exception();

        try
        {
            await _gridRefOvertime.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}