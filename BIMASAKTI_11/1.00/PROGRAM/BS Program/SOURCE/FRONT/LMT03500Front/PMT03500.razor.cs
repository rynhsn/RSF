using System.Collections.ObjectModel;
using BlazorClientHelper;
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
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace PMT03500Front;

public partial class PMT03500 : R_Page
{
    private PMT03500ViewModel _viewModel = new();
    private PMT03500UploadUtilityViewModel _viewModelUpload = new();
    private PMT03500UndoUtilityViewModel _viewModelUndo = new();

    private PMT03500UtilityUsageViewModel _viewModelUtility = new();
    private R_Conductor _conductorRefBuilding;
    private R_ConductorGrid _conductorRefUtility;
    private R_Grid<PMT03500BuildingDTO> _gridRefBuilding = new();
    private R_Grid<PMT03500UtilityUsageDTO> _gridRefUtility = new();

    [Inject] private IJSRuntime JS { get; set; }
    [Inject] private R_IExcel ExcelInject { get; set; }
    [Inject] IClientHelper ClientHelper { get; set; }

    private R_TabStrip _tabStripRef;
    private R_TabStrip _tabStripUtilityRef;

    private string _dataLabel = "";
    private string _display = "d-none";
    private bool _visibleColumnEC;
    private bool _visibleColumnWG;

    private bool _hasDetail;
    private bool _enabledBtn = true;
    
    private string _flagPropertyUtility;

    private R_TabPage _pageCO { get; set; }
    public R_TabPage _pageMN { get; set; }

    private void StateChangeInvoke()
    {
        StateHasChanged();
    }

    private void StateChangeUndoInvoke()
    {
        StateHasChanged();
    }

    #region HandleError

    private void DisplayErrorInvoke(R_Exception poException)
    {
        R_DisplayException(poException);
    }

    private void DisplayErrorUndoInvoke(R_Exception poException)
    {
        R_DisplayException(poException);
    }

    #endregion

    private async Task ActionFuncDataSetExcel()
    {
        await Task.CompletedTask;
    }

    public async Task ShowSuccessUpdateInvoke()
    {
        _enabledBtn = true;
        await R_MessageBox.Show("", "Update Successfully", R_eMessageBoxButtonType.OK);
        await _gridRefUtility.R_RefreshGrid(null);
    }

    public async Task ShowSuccessUndoInvoke()
    {
        _enabledBtn = true;
        await R_MessageBox.Show("", "Undo Successfully", R_eMessageBoxButtonType.OK);
        await _gridRefUtility.R_RefreshGrid(null);
    }

    protected override async Task R_Init_From_Master(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.Init();
            await _viewModelUtility.Init(_viewModel.Property);
            await _gridRefBuilding.R_RefreshGrid(null);


            _viewModelUpload.StateChangeAction = StateChangeInvoke;
            _viewModelUpload.DisplayErrorAction = DisplayErrorInvoke;
            _viewModelUpload.ActionDataSetExcel = ActionFuncDataSetExcel;
            _viewModelUpload.ShowSuccessAction = async () => { await ShowSuccessUpdateInvoke(); };

            _viewModelUndo.StateChangeAction = StateChangeUndoInvoke;
            _viewModelUndo.DisplayErrorAction = DisplayErrorUndoInvoke;
            _viewModelUndo.ShowSuccessAction = async () => { await ShowSuccessUndoInvoke(); };
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

    private void GetUtilityRecord(R_ServiceGetRecordEventArgs eventArgs)
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
                            await _pageMN.InvokeRefreshTabPageAsync(_viewModelUtility.Property?.CPROPERTY_ID);
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
                case eParamType.InvPeriod:
                    _viewModelUtility.InvPeriodNo = (string)value;
                    break;
                case eParamType.UtilityYear:
                    _viewModelUtility.UtilityPeriodYear = (string)value;
                    await _viewModelUtility.GetPeriod(_viewModelUtility.UtilityPeriodYear,
                        _viewModelUtility.UtilityPeriodNo);
                    break;
                case eParamType.UtilityPeriod:
                    _viewModelUtility.UtilityPeriodNo = (string)value;
                    await _viewModelUtility.GetPeriod(_viewModelUtility.UtilityPeriodYear,
                        _viewModelUtility.UtilityPeriodNo);
                    break;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        await R_DisplayExceptionAsync(loEx);
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

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }

    private void BeforeOpenPhoto(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(PMT03500PhotoPopup);
        var loParam = new PMT03500DetailParam
        {
            OUTILITY_HEADER = _viewModelUtility.EntityUtility,
            EUTILITY_TYPE = _viewModelUtility.UtilityType
        };
        eventArgs.Parameter = loParam;
    }

    private void BeforeTabCutOff(R_BeforeOpenTabPageEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(PMT03500CutOff);
        // eventArgs.Parameter = _viewModelUtility.PropertyId;
        eventArgs.Parameter = _viewModelUtility.Property;
    }

    private void BeforeTabUpdateMeter(R_BeforeOpenTabPageEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(PMT03500UpdateMeter);
        eventArgs.Parameter = _viewModel.PropertyId;
        // eventArgs.Parameter = _viewModelUtility.Property;
    }

    private void BeforeTabDetail(R_BeforeOpenTabPageEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(PMT03500Detail);
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

            // var test = await _viewModelUtility.DownloadTemplate(ExcelInject);
            // var saveFileName = $"UtilityUsage.xlsx";
            // var loByte = ExcelInject.R_WriteToExcel(test);
            // await JS.downloadFileFromStreamHandler(saveFileName, loByte);

            // var loByteFile = await _viewModelUtility.DownloadTemplate();
            // var saveFileName = $"UtilityUsage.xlsx";
            // await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);

            // var loResult = await _viewModelUtility.DownloadTemplate(ExcelInject);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void BeforeOpenUpload(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(PMT03500UploadUtilityPopup);
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
            if (_flagPropertyUtility != _viewModel.PropertyId)
            {
                await _gridRefBuilding.R_RefreshGrid(null);
                _display = "d-none";
            }

            _comboProperty = _tabStripUtilityRef.ActiveTab.Id != "DI";
        }
        else
        {
            _flagPropertyUtility = _viewModel.PropertyId;
        }
    }

    private async Task DisplayUtility(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loData = (PMT03500UtilityUsageDTO)eventArgs.Data;
            // _tabDetail = loData.CUTILITY_TYPE is "03" or "04" && loData.CSTATUS.Length > 0;

            _hasDetail = loData.CSTATUS.Length > 0 && _conductorRefUtility.R_ConductorMode != R_eConductorMode.Edit;


            _viewModelUtility.EntityUtility = loData;
            _viewModelUtility.EntityUtility.CPROPERTY_ID = _viewModel.PropertyId;

            await Task.Delay(100);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnClickSave()
    {
        var loEx = new R_Exception();

        try
        {
            _enabledBtn = false;
            await _conductorRefUtility.R_SaveBatch();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void Validation(R_ValidationEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loData = (PMT03500UtilityUsageDTO)eventArgs.Data;
            if (loData.DSTART_DATE > loData.DEND_DATE)
            {
                loEx.Add("Invalid Date", "Start Date must be less than End Date");
                eventArgs.Cancel = loEx.HasError;
            }

            loData.CSTART_DATE = loData.DSTART_DATE?.ToString("yyyyMMdd");
            loData.CEND_DATE = loData.DEND_DATE?.ToString("yyyyMMdd");
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        // completed task
        loEx.ThrowExceptionIfErrors();
    }

    private async Task SaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            // await _viewModelUtility.SaveBatch((List<PMT03500UtilityUsageDTO>)eventArgs.Data, ClientHelper.CompanyId, ClientHelper.UserId);
            var loTempDataList = (List<PMT03500UtilityUsageDTO>)eventArgs.Data;
            var loDataList =
                R_FrontUtility.ConvertCollectionToCollection<PMT03500UploadUtilityErrorValidateDTO>(loTempDataList);

            var loUtilityType = loTempDataList.FirstOrDefault().CUTILITY_TYPE;
            _viewModelUpload.CompanyId = ClientHelper.CompanyId;
            _viewModelUpload.UserId = ClientHelper.UserId;
            _viewModelUpload.UploadParam.CPROPERTY_ID = _viewModelUtility.Property.CPROPERTY_ID;
            _viewModelUpload.UploadParam.EUTILITY_TYPE =
                loUtilityType is "01" or "02"
                    ? EPMT03500UtilityUsageType.EC
                    : EPMT03500UtilityUsageType.WG;

            await _viewModelUpload.SaveBulkFile(poUploadParam: _viewModelUpload.UploadParam,
                poDataList: loDataList.ToList());

            // await _gridRefUtility.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnClickUndo(MouseEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loData = _viewModelUtility.GridUtilityUsageList.Where(x => x.LSELECTED).ToList();
            var loParam = new PMT03500UndoParam
            {
                CCOMPANY_ID = ClientHelper.CompanyId,
                CUSER_ID = ClientHelper.UserId,
                CPROPERTY_ID = _viewModelUtility.Property.CPROPERTY_ID,
                CBUILDING_ID = _viewModelUtility.Entity.CBUILDING_ID,
                CCHARGES_TYPE = _viewModelUtility.UtilityTypeId,
                CINV_PRD = _viewModelUtility.InvPeriodYear + _viewModelUtility.InvPeriodNo
            };
            await _viewModelUndo.SaveBulk(loParam, loData.ToList());
        }
        catch (Exception Ex)
        {
            loEx.Add(Ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void OnClickCancel()
    {
        _viewModelUtility.GridUtilityUsageList =
            new ObservableCollection<PMT03500UtilityUsageDTO>(_viewModelUtility.GridUtilityUsageListTemp);
    }

    private void OnChangedAllFloor(object obj)
    {
        _viewModelUtility.CheckFloor((bool)obj);
    }

    private void R_RowRender(R_GridRowRenderEventArgs eventArgs)
    {
        var loData = (PMT03500UtilityUsageDTO)eventArgs.Data;

        if (loData.LOVER_USAGE)
        {
            //eventArgs.RowStyle = new R_GridRowRenderStyle
            //{
            //    FontColor = "red"
            //};

            eventArgs.RowClass = "myCustomRowFormatting";
        }
    }
}