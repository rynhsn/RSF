using LMT03500Common.DTOs;
using LMT03500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;

namespace LMT03500Front;

public partial class LMT03500CutOff : R_ITabPage
{
    
    private LMT03500UtilityUsageViewModel _viewModel = new();
    
    private R_Conductor _conductorRef;
    private R_ConductorGrid _conductorRefUtility;
    private R_Grid<LMT03500BuildingDTO> _gridRefBuilding = new();
    private R_Grid<LMT03500UtilityUsageDTO> _gridRefUtility = new();
    
    [Inject] private IJSRuntime JS { get; set; }
    
    private string _dataLabel = "";
    private string _display = "d-none";
    private bool _visibleColumnEC;
    private bool _visibleColumnWG;
    
    protected override async Task R_Init_From_Master(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.Init(eventArgs);
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
            await _viewModel.GetBuildingList();
            eventArgs.ListEntityResult = _viewModel.GridBuildingList;
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
            await _viewModel.GetUtilityUsageList();
            eventArgs.ListEntityResult = _viewModel.GridUtilityUsageList;
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
            _viewModel.EntityUtility = loData;
            eventArgs.Result = _viewModel.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task GetBuildingRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loData = (LMT03500BuildingDTO)eventArgs.Data;
        await _viewModel.GetBuildingRecord(loData);
        eventArgs.Result = _viewModel.Entity;
    }
    
    private async Task OnChangeParam(object value, eParamType type)
    {
        var loEx = new R_Exception();
        try
        {
            switch (type)
            {
                case eParamType.UtilityType:
                    _viewModel.UtilityTypeId = (string)value;
                    break;
                case eParamType.Floor:
                    _viewModel.FloorId = (string)value;
                    break;
                case eParamType.UtilityPeriod:
                    _viewModel.UtilityPeriodYear = (string)value;
                    _viewModel.UtilityPeriodNo = value.ToString()?.Substring(value.ToString()!.Length - 2);
                    break;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }
    
    
    private async Task OnClickRefresh()
    {
        var loEx = new R_Exception();

        try
        {
            switch (_viewModel.UtilityTypeId)
            {
                case "01" or "02":
                    _dataLabel = _localizer["LBL_EC"];
                    _display = "d-block";
                    _visibleColumnEC = true;
                    _visibleColumnWG = false;
                    break;
                case "03" or "04":
                    _dataLabel = _localizer["LBL_WG"];
                    _display = "d-block";
                    _visibleColumnEC = false;
                    _visibleColumnWG = true;
                    break;
                default:
                    _display = "d-none";
                    break;
            }


            await _gridRefUtility.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    public async Task RefreshTabPageAsync(object poParam)
    {
        await _viewModel.Init(poParam);
        await _gridRefBuilding.R_RefreshGrid(null);
    }
    
    
    private async Task DownloadTemplate()
    {
        var loEx = new R_Exception();

        try
        {
            //buat lcDate dengan format yyyyMMdd_HHmm
            // var lcDate = DateTime.Now.ToString("yyyyMMdd_HHmm");
            var loByteFile = await _viewModel.DownloadTemplate();
            var saveFileName = $"UtilityUsage.xlsx";
            await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}