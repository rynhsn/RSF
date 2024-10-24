using Microsoft.AspNetCore.Components.Web;
using PMT06000Common.DTOs;
using PMT06000Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
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
    private int _pageSizeOvt = 9;
    private int _pageSizeService = 8;
    private int _pageSizeUnit = 8;
    private R_TabStripTab _tabList = new();
    private R_TabStripTab _tabDetail = new();

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
                // _viewModel.Entity = new PMT06000OvtDTO();
                // _viewModel.EntityService = new PMT06000OvtServiceDTO();
                _viewModel.OvertimeServiceGridList.Clear();
                _viewModel.OvertimeUnitGridList.Clear();
            }else
            {
                await _conductorRefOvertime.R_GetEntity(_viewModel.Entity);
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
                // _viewModel.EntityService = new PMT06000OvtServiceDTO();
                _viewModel.OvertimeUnitGridList.Clear();
            }
            else
            {
                await DisplayService(new R_DisplayEventArgs(_viewModel.EntityService, R_eConductorMode.Normal));
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
        // var loParam = new PMT06000ParameterDTO
        // {
        //     CREC_ID = _viewModel.Entity.CREC_ID,
        //     isCaller = false
        // };
        var loParam = R_FrontUtility.ConvertObjectToObject<PMT06000ParameterDTO>(_viewModel.Entity);
        loParam.isCaller = false;
        eventArgs.Parameter = loParam;
        eventArgs.TargetPageType = typeof(PMT06000Info);
    }

    private async Task AfterOpenInfo(R_AfterOpenTabPageEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.OvertimeGridList.Clear();
            _viewModel.OvertimeServiceGridList.Clear();
            _viewModel.OvertimeUnitGridList.Clear();

            // var lolo = _viewModel.Entity;
            // await _conductorRefOvertime.R_SetCurrentData(null);

            await _gridRefOvertime.R_RefreshGrid(null);
            // await _conductorRefOvertime.R_GetEntity(_viewModel.Data);
            // //
            // await _gridRefService.R_RefreshGrid(null);
            // await _conductorRefService.R_GetEntity(_viewModel.EntityService);
            //
            // await _gridRefUnit.R_RefreshGrid(null);

            // _gridRefOvertime.GetCurrentData();
            // await _gridRefOvertime.R_SelectCurrentDataAsync(_viewModel.OvertimeGridList[0]);
            // await _gridRefOvertime.R_SelectCurrentDataAsync(_viewModel.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
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
            // await _gridRefOvertime.R_RefreshGrid(null);
            // await _gridRefService.R_RefreshGrid(null);
            // await _gridRefUnit.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task OnClickSubmit()
    {
        var loEx = new R_Exception();

        try
        {
            var leMsg = await R_MessageBox.Show("",
                _viewModel.Data.CTRANS_STATUS switch
                {
                    "00" => _localizer["MSG_BEFORE_SUBMIT"],
                    "10" => _localizer["MSG_BEFORE_REDRAFT"],
                    _ => ""
                }, R_eMessageBoxButtonType.YesNo);

            if (leMsg == R_eMessageBoxResult.No)
            {
                return;
            }

            await _viewModel.ProcessSubmit();

            var leMsgAfter = await R_MessageBox.Show("",
                _viewModel.Data.CTRANS_STATUS switch
                {
                    "00" => _localizer["MSG_AFTER_SUBMIT"],
                    "10" => _localizer["MSG_AFTER_REDRAFT"],
                    _ => ""
                });

            await _gridRefOvertime.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    private void BeforeOpenInfo(R_BeforeOpenTabPageEventArgs eventArgs)
    {
        var loParam = R_FrontUtility.ConvertObjectToObject<PMT06000ParameterDTO>(_viewModel.Entity);
        loParam.isCaller = false;
        eventArgs.Parameter = loParam;
        eventArgs.TargetPageType = typeof(PMT06000Info);
    }

    private void InfoTabEventCallBack(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            var loParamEvent = (EventCallBackParam)poParam;
            if (loParamEvent.LIS_SETOTHER)
            {
                _tabList.Enabled = loParamEvent.LSET_OTHER_STATE;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}