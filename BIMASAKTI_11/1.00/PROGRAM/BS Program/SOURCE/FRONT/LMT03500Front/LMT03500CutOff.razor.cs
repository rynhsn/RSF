using PMT03500Common.DTOs;
using PMT03500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;

namespace LMT03500Front;

public partial class LMT03500CutOff : R_ITabPage
{
    
    private PMT03500ViewModel _viewModel = new();
    private PMT03500UtilityUsageViewModel _viewModelUtility = new();
    
    private R_Conductor _conductorRef;
    private R_ConductorGrid _conductorRefUtility;
    private R_Grid<PMT03500BuildingDTO> _gridRefBuilding = new();
    private R_Grid<PMT03500UtilityUsageDTO> _gridRefUtility = new();
    
    [Inject] private IJSRuntime JS { get; set; }
    [Inject] private R_IExcel ExcelInject { get; set; }
    
    private string _dataLabel = "";
    private string _display = "d-none";
    private bool _visibleColumnEC;
    private bool _visibleColumnWG;
    
    protected override async Task R_Init_From_Master(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            // await _viewModel.Init();
            await _viewModelUtility.Init(eventArgs);
            await _gridRefBuilding.R_RefreshGrid(null);
            // _viewModelUtility.ActionDataSetExcel = ActionFuncDataSetExcel;
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
    
    private async Task GetUtilityRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loData = (PMT03500UtilityUsageDTO)eventArgs.Data;
            _viewModelUtility.EntityUtility = loData;
            eventArgs.Result = _viewModelUtility.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task GetBuildingRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loData = (PMT03500BuildingDTO)eventArgs.Data;
        await _viewModelUtility.GetBuildingRecord(loData);
        eventArgs.Result = _viewModelUtility.Entity;
    }
    
    private async Task OnChangeParam(object value, eParamType type)
    {
        var loEx = new R_Exception();
        try
        {
            switch (type)
            {
                case eParamType.UtilityType:
                    _viewModelUtility.UtilityTypeId = (string)value;
                    break;
                case eParamType.Floor:
                    _viewModelUtility.FloorId = (string)value;
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
    
    private async Task OnClickRefresh()
    {
        var loEx = new R_Exception();

        try
        {
            switch (_viewModelUtility.UtilityTypeId)
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
        await _viewModelUtility.Init(poParam);
        await _gridRefBuilding.R_RefreshGrid(null);
    }
    
    
    private async Task DownloadTemplate()
    {
        var loEx = new R_Exception();

        try
        {
            var loByte = ExcelInject.R_WriteToExcel(_viewModelUtility.ExcelDataSetCutOff);
            var saveFileName = $"UtilityUsage.xlsx";
            await JS.downloadFileFromStreamHandler(saveFileName, loByte);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private void BeforeOpenUpload(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(PMT03500UploadCutOffPopup);
        var loParam = new PMT03500UploadParam
        {
            EUTILITY_TYPE = _viewModelUtility.UtilityType,
            CPROPERTY_ID = _viewModelUtility.Property.CPROPERTY_ID,
            //ambil property name berdasarkan property id dari property list yang sudah di load
            CPROPERTY_NAME = _viewModelUtility.Property.CPROPERTY_NAME
        };
        eventArgs.Parameter = loParam;
    }
    
    private async Task AfterOpenUpload(R_AfterOpenPopupEventArgs eventArgs)
    {
        if(eventArgs.Success)
        {
            await _gridRefUtility.R_RefreshGrid(null);
        }
    }
}