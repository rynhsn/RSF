using LMT03500Common.DTOs;
using LMT03500Model.ViewModel;
using Microsoft.AspNetCore.Components.Web;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Layouts;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;

namespace LMT03500Front;

public partial class LMT03500 : R_Page
{
    private LMT03500ViewModel _viewModel = new();

    private LMT03500UtilityUsageViewModel _viewModelUtility = new();
    private R_Conductor _conductorRefBuilding;
    private R_ConductorGrid _conductorRefUtility;
    private R_Grid<LMT03500BuildingDTO> _gridRefBuilding = new();
    private R_Grid<LMT03500UtilityUsageDTO> _gridRefUtility = new();

    private R_TabStrip _tabStripRef;
    private R_TabStrip _tabStripUtilityRef;

    private string _dataLabel = "";
    private string _display = "d-none";
    private bool _visibleColumnEC;
    private bool _visibleColumnWG;

    private R_TabPage _pageCO { get; set; }
    public R_TabPage _pageMN { get; set; }

    protected override async Task R_Init_From_Master(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.Init();
            await _viewModelUtility.Init(_viewModel.PropertyId);
            await _gridRefBuilding.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetBuildingListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModelUtility.GetBuildingList();
            eventArgs.ListEntityResult = _viewModelUtility.GridBuildingList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetUtilityListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModelUtility.GetUtilityUsageList();
            eventArgs.ListEntityResult = _viewModelUtility.GridUtilityUsageList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetBuildingRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loData = (LMT03500BuildingDTO)eventArgs.Data;
            await _viewModelUtility.GetBuildingRecord(loData);
            eventArgs.Result = _viewModelUtility.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task GetUtilityRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loData = (LMT03500UtilityUsageDTO)eventArgs.Data;
            _viewModelUtility.EntityUtility = loData;
            eventArgs.Result = _viewModelUtility.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnChangeParam(object value, eParamType type)
    {
        var loEx = new R_Exception();
        try
        {
            switch (type)
            {
                case eParamType.Property:
                    _viewModel.PropertyId = (string)value;
                    _viewModelUtility.PropertyId = (string)value;

                    switch (_tabStripRef.ActiveTab.Id)
                    {
                        case "Utility" when _tabStripUtilityRef.ActiveTab.Id == "GI":
                            await _gridRefBuilding.R_RefreshGrid(null);
                            break;
                        case "CutOff":
                            await _pageCO.InvokeRefreshTabPageAsync(_viewModel.PropertyId);
                            break;
                        case "UpdateMeter":
                            await _pageMN.InvokeRefreshTabPageAsync(_viewModel.PropertyId);
                            break;
                    }

                    break;
                case eParamType.UtilityType:
                    _viewModelUtility.UtilityTypeId = (string)value;
                    break;
                case eParamType.Floor:
                    _viewModelUtility.FloorId = (string)value;
                    break;
                case eParamType.InvPeriod:
                    _viewModelUtility.InvPeriodYear = (string)value;
                    _viewModelUtility.InvPeriodNo = value.ToString()?.Substring(value.ToString()!.Length - 2);
                    break;
                case eParamType.UtilityPeriod:
                    _viewModelUtility.UtilityPeriodYear = (string)value;
                    _viewModelUtility.UtilityPeriodNo = value.ToString()?.Substring(value.ToString()!.Length - 2);
                    break;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private void ValidateDate(object obj)
    {
        //cek apakah tanggal to lebih kecil dari tanggal from
        if (_viewModelUtility.UtilityPeriodToDtDt < _viewModelUtility.UtilityPeriodFromDtDt)
        {
            _viewModelUtility.UtilityPeriodToDtDt = _viewModelUtility.UtilityPeriodFromDtDt;
        }
    }

    private async Task OnClickRefresh()
    {
        var loEx = new R_Exception();

        try
        {
            if (_viewModelUtility.UtilityTypeId is "01" or "02")
            {
                _dataLabel = _localizer["LBL_EC"];
                _display = "d-block";
                _visibleColumnEC = true;
                _visibleColumnWG = false;
            }
            else if (_viewModelUtility.UtilityTypeId is "03" or "04")
            {
                _dataLabel = _localizer["LBL_WG"];
                _display = "d-block";
                _visibleColumnEC = false;
                _visibleColumnWG = true;
            }
            else
            {
                _display = "d-none";
            }


            await _gridRefUtility.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private void BeforeOpenPhoto(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(LMT03500PhotoPopup);
        eventArgs.Parameter = _viewModelUtility.EntityUtility;
    }

    private void BeforeTabCutOff(R_BeforeOpenTabPageEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(LMT03500CutOff);
        eventArgs.Parameter = _viewModel.PropertyId;
    }

    private void BeforeTabUpdateMeter(R_BeforeOpenTabPageEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(LMT03500UpdateMeter);
        eventArgs.Parameter = _viewModel.PropertyId;
    }

    private void BeforeTabDetail(R_BeforeOpenTabPageEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(LMT03500Detail);
        eventArgs.Parameter = _viewModelUtility.EntityUtility;
    }
}