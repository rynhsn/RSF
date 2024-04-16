using PMT03500Common;
using PMT03500Common.DTOs;
using PMT03500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Layouts;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;

namespace LMT03500Front;

public partial class LMT03500 : R_Page
{
    private PMT03500ViewModel _viewModel = new();

    private PMT03500UtilityUsageViewModel _viewModelUtility = new();
    private R_Conductor _conductorRefBuilding;
    private R_ConductorGrid _conductorRefUtility;
    private R_Grid<PMT03500BuildingDTO> _gridRefBuilding = new();
    private R_Grid<PMT03500UtilityUsageDTO> _gridRefUtility = new();

    [Inject] private IJSRuntime JS { get; set; }
    [Inject] private R_IExcel ExcelInject { get; set; }

    private R_TabStrip _tabStripRef;
    private R_TabStrip _tabStripUtilityRef;

    private string _dataLabel = "";
    private string _display = "d-none";
    private bool _visibleColumnEC;
    private bool _visibleColumnWG;

    private bool _tabDetail;

    private R_TabPage _pageCO { get; set; }
    public R_TabPage _pageMN { get; set; }

    protected override async Task R_Init_From_Master(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.Init();
            await _viewModelUtility.Init(_viewModel.Property);
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
            _tabDetail = _viewModelUtility.GridUtilityUsageList.Count > 0;
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
            var loData = (PMT03500BuildingDTO)eventArgs.Data;
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
            var loData = (PMT03500UtilityUsageDTO)eventArgs.Data;
            _viewModelUtility.EntityUtility = loData;
            _viewModelUtility.EntityUtility.CPROPERTY_ID = _viewModel.PropertyId;
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
                    // _viewModelUtility.PropertyId = (string)value;
                    _viewModelUtility.Property =
                        _viewModel.PropertyList.FirstOrDefault(x => x.CPROPERTY_ID == _viewModel.PropertyId);

                    switch (_tabStripRef.ActiveTab.Id)
                    {
                        case "Utility" when _tabStripUtilityRef.ActiveTab.Id == "GI":
                            await _gridRefBuilding.R_RefreshGrid(null);
                            break;
                        case "CutOff":
                            // await _pageCO.InvokeRefreshTabPageAsync(_viewModel.PropertyId);
                            await _pageCO.InvokeRefreshTabPageAsync(_viewModelUtility.Property);
                            break;
                        case "UpdateMeter":
                            await _pageMN.InvokeRefreshTabPageAsync(_viewModelUtility.Property.CPROPERTY_ID);
                            // await _pageMN.InvokeRefreshTabPageAsync(_viewModelUtility.Property);
                            break;
                    }

                    break;
                case eParamType.UtilityType:
                    _viewModelUtility.UtilityTypeId = (string)value;
                    break;
                case eParamType.Floor:
                    _viewModelUtility.FloorId = (string)value;
                    break;
                case eParamType.InvYear:
                    _viewModelUtility.InvPeriodYear = (string)value;
                    break;
                case eParamType.UtilityYear:
                    _viewModelUtility.UtilityPeriodYear = (string)value;
                    break;
                case eParamType.InvPeriod:
                    _viewModelUtility.InvPeriodNo = (string)value;
                    break;
                case eParamType.UtilityPeriod:
                    _viewModelUtility.UtilityPeriodNo = (string)value;
                    break;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        await R_DisplayExceptionAsync(loEx);
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
                _viewModelUtility.UtilityType = EPMT03500UtilityUsageType.EC;
                _dataLabel = _localizer["LBL_EC"];
                _display = "d-block";
                _visibleColumnEC = true;
                _visibleColumnWG = false;
            }
            else if (_viewModelUtility.UtilityTypeId is "03" or "04")
            {
                _viewModelUtility.UtilityType = EPMT03500UtilityUsageType.WG;
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
        // eventArgs.Parameter = _viewModelUtility.PropertyId;
        eventArgs.Parameter = _viewModelUtility.Property;
    }

    private void BeforeTabUpdateMeter(R_BeforeOpenTabPageEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(LMT03500UpdateMeter);
        eventArgs.Parameter = _viewModel.PropertyId;
        // eventArgs.Parameter = _viewModelUtility.Property;
    }

    private void BeforeTabDetail(R_BeforeOpenTabPageEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(LMT03500Detail);
        var loParam = new PMT03500DetailParam
        {
            OUTILITY_HEADER = _viewModelUtility.EntityUtility,
            EUTILITY_TYPE = _viewModelUtility.UtilityType
        };
        eventArgs.Parameter = loParam;
    }

    private async Task DownloadTemplate()
    {
        var loEx = new R_Exception();

        try
        {
            var loByte = ExcelInject.R_WriteToExcel(_viewModelUtility.ExcelDataSetUtility);
            var saveFileName = $"UtilityUsage.xlsx";
            await JS.downloadFileFromStreamHandler(saveFileName, loByte);
            // var loByteFile = await _viewModelUtility.DownloadTemplate();
            // var saveFileName = $"UtilityUsage.xlsx";
            // await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void BeforeOpenUpload(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(LMT03500UploadUtilityPopup);
        var loParam = new PMT03500UploadParam
        {
            EUTILITY_TYPE = _viewModelUtility.UtilityType,
            CPROPERTY_ID = _viewModel.PropertyId,
            //ambil property name berdasarkan property id dari property list yang sudah di load
            CPROPERTY_NAME = _viewModel.PropertyList.FirstOrDefault(x => x.CPROPERTY_ID == _viewModel.PropertyId)
                ?.CPROPERTY_NAME
        };
        eventArgs.Parameter = loParam;
    }

    private async Task AfterOpenUpload(R_AfterOpenPopupEventArgs eventArgs)
    {
        if (eventArgs.Success)
        {
            await _gridRefUtility.R_RefreshGrid(null);
        }
    }

    private bool _comboProperty = true;

    private async Task OnActiveSubTab(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
    {
        if (_tabStripRef.ActiveTab.Id == "Utility")
        {
            _comboProperty = eventArgs.TabStripTab.Id != "DI";
        }
    }

    private async Task OnActiveTab(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
    {
        _comboProperty = true;
        if (eventArgs.TabStripTab.Id == "Utility")
        {
            _comboProperty = _tabStripUtilityRef.ActiveTab.Id != "DI";
        }
    }

    private void DisplayUtility(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loData = (PMT03500UtilityUsageDTO)eventArgs.Data;
            _viewModelUtility.EntityUtility = loData;
            _viewModelUtility.EntityUtility.CPROPERTY_ID = _viewModel.PropertyId;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}