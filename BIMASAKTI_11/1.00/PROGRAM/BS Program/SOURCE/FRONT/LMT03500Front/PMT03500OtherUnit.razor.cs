using System.Collections.ObjectModel;
using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using PMT03500Common;
using PMT03500Common.DTOs;
using PMT03500Model.ViewModel;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace PMT03500Front;

public partial class PMT03500OtherUnit
{
    private PMT03500ViewModel _viewModel = new();
    private PMT03500UploadUtilityViewModel _viewModelSave = new();
    private PMT03500UndoUtilityViewModel _viewModelUndo = new();

    private PMT03500UtilityUsageViewModel _viewModelUtility = new();

    // private R_Conductor _conductorRefBuilding;
    private R_ConductorGrid _conductorRefUtility;

    // private R_Grid<PMT03500BuildingDTO> _gridRefBuilding = new();
    private R_Grid<PMT03500UtilityUsageDTO> _gridRefUtility = new();

    [Inject] private IJSRuntime JS { get; set; }
    [Inject] private R_IExcel ExcelInject { get; set; }
    [Inject] IClientHelper ClientHelper { get; set; }

    private eBatchType _batchType;

    private R_TabStrip _tabStripRef;
    private R_TabStrip _tabStripUtilityRef;
    private R_ComboBox<PMT03500YearDTO, string> _comboUtilityPrdRef;
    private R_ComboBox<PMT03500PropertyDTO, string> _comboPropertyRef;

    private string _dataLabel = "";
    private string _display = "d-none";
    private bool _visibleColumnEC;
    private bool _visibleColumnWG;

    private bool _hasDetail;
    private bool _enabledBtn = true;

    private string _flagPropertyUtility;

    private R_TabPage _pageOtherCO { get; set; }
    public R_TabPage _pageOtherMN { get; set; }

    private void StateChangeInvoke()
    {
        StateHasChanged();
    }

    private void StateChangeUndoInvoke()
    {
        StateHasChanged();
    }

    #region HandleError

    private void DisplayErrorInvoke(R_APIException poException)
    {
        var loEx = R_FrontUtility.R_ConvertFromAPIException(poException);
        this.R_DisplayException(loEx);
    }

    private void DisplayErrorUndoInvoke(R_APIException poException)
    {
        var loEx = new R_Exception(_viewModelUndo.ErrorList);
        R_DisplayException(loEx);
    }

    #endregion

    private async Task ActionFuncDataSetExcel()
    {
        await Task.CompletedTask;
    }

    public async Task ShowSuccessUpdateInvoke()
    {
        _enabledBtn = true;
        await R_MessageBox.Show("", _localizer["UPDATE_SUCCESSFULLY"], R_eMessageBoxButtonType.OK);
        await _gridRefUtility.R_RefreshGrid(null);
    }

    public async Task ShowSuccessUndoInvoke()
    {
        _enabledBtn = true;
        await R_MessageBox.Show("", _localizer["UNDO_SUCCESSFULLY"], R_eMessageBoxButtonType.OK);
        await _gridRefUtility.R_RefreshGrid(null);
    }

    protected override async Task R_Init_From_Master(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModelUtility.LOTHER_UNIT = true;
            await _viewModel.Init();
            await _viewModelUtility.Init(_viewModel.Property);
            if (_viewModel.PropertyList.Count > 0)
            {
                await _viewModelUtility.GetBuildingList();
            }

            _viewModelSave.StateChangeAction = StateChangeInvoke;
            _viewModelSave.DisplayErrorAction = DisplayErrorInvoke;
            _viewModelSave.ActionDataSetExcel = ActionFuncDataSetExcel;
            _viewModelSave.ShowSuccessAction = async () => { await ShowSuccessUpdateInvoke(); };

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

    private void GetUtilityRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loData = (PMT03500UtilityUsageDTO)eventArgs.Data;
            _viewModelUtility.EntityUtility = loData;
            _viewModelUtility.EntityUtility.CPROPERTY_ID = _viewModel.PropertyId;
            eventArgs.Result = _viewModelUtility.EntityUtility;
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
                    if (string.IsNullOrEmpty(_viewModel.PropertyId))
                    {
                        await _comboPropertyRef.FocusAsync();
                        return;
                    }

                    // _viewModelUtility.PropertyId = (string)value;
                    _viewModelUtility.Property =
                        _viewModel.PropertyList.FirstOrDefault(x => x.CPROPERTY_ID == _viewModel.PropertyId);

                    await _viewModelUtility.GetSystemParam(_viewModel.PropertyId);
                    _viewModelUtility.SetParameterHeader();

                    switch (_tabStripRef.ActiveTab.Id)
                    {
                        case "Utility" when _tabStripUtilityRef.ActiveTab.Id == "GI":
                            await _viewModelUtility.GetBuildingList();
                            break;
                        case "CutOff":
                            // await _pageCO.InvokeRefreshTabPageAsync(_viewModel.PropertyId);
                            await _pageOtherCO.InvokeRefreshTabPageAsync(_viewModelUtility.Property);
                            break;
                        case "UpdateMeter":
                            var loMnParam = new PMT03500UpdateMeterParameter
                            {
                                CPROPETY_ID = _viewModelUtility.Property?.CPROPERTY_ID,
                                LOTHER_UNIT = _viewModelUtility.LOTHER_UNIT
                            };
                            await _pageOtherMN.InvokeRefreshTabPageAsync(loMnParam);
                            break;
                    }

                    break;
                case eParamType.UtilityType:
                    _viewModelUtility.UtilityTypeId = (string)value;
                    _viewModelUtility.SetParameterHeader();
                    break;
                case eParamType.InvYear:
                    _viewModelUtility.InvPeriodYear = (string)value;
                    await _viewModelUtility.GetPeriod(_viewModelUtility.InvPeriodYear, _viewModelUtility.InvPeriodNo);
                    _viewModelUtility.SetParameterHeader();
                    break;
                case eParamType.InvPeriod:
                    _viewModelUtility.InvPeriodNo = (string)value;
                    await _viewModelUtility.GetPeriod(_viewModelUtility.InvPeriodYear, _viewModelUtility.InvPeriodNo);
                    _viewModelUtility.SetParameterHeader();
                    break;
                case eParamType.UtilityYear:
                    _viewModelUtility.UtilityPeriodYear = (string)value;
                    // await _viewModelUtility.GetPeriod(_viewModelUtility.UtilityPeriodYear, _viewModelUtility.UtilityPeriodNo);
                    // _viewModelUtility.SetParameterHeader();
                    break;
                case eParamType.UtilityPeriod:
                    _viewModelUtility.UtilityPeriodNo = (string)value;
                    // await _viewModelUtility.GetPeriod(_viewModelUtility.UtilityPeriodYear, _viewModelUtility.UtilityPeriodNo);
                    // _viewModelUtility.SetParameterHeader();
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
            if (int.Parse(_viewModelUtility.InvPeriodYear + _viewModelUtility.InvPeriodNo) <
                int.Parse(_viewModelUtility.UtilityPeriodYear + _viewModelUtility.UtilityPeriodNo))
            {
                await R_MessageBox.Show("", _localizer["UTILITY_PERIOD_GREATER_THAN_INVOICE_PERIOD"]);
                await _comboUtilityPrdRef.FocusAsync();
                return;
            }

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
        eventArgs.TargetPageType = typeof(PMT03500OtherUnitCutOff);
        // eventArgs.Parameter = _viewModelUtility.PropertyId;
        eventArgs.Parameter = _viewModelUtility.Property;
    }

    private void BeforeTabUpdateMeter(R_BeforeOpenTabPageEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(PMT03500UpdateMeter);
        // eventArgs.Parameter = _viewModel.PropertyId;
        eventArgs.Parameter = new PMT03500UpdateMeterParameter
        {
            CPROPETY_ID = _viewModel.PropertyId,
            LOTHER_UNIT = _viewModelUtility.LOTHER_UNIT
        };
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

    private void OnActiveSubTab(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
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
                await _viewModelUtility.GetBuildingList();
                _display = "d-none";
            }

            _comboProperty = _tabStripUtilityRef.ActiveTab.Id != "DI";
        }
        else
        {
            _flagPropertyUtility = _viewModel.PropertyId;
        }
    }

    private void DisplayUtility(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loData = (PMT03500UtilityUsageDTO)eventArgs.Data;
            // _tabDetail = loData.CUTILITY_TYPE is "03" or "04" && loData.CSTATUS.Length > 0;

            _hasDetail = loData.CSTATUS.Length > 0 && _conductorRefUtility.R_ConductorMode != R_eConductorMode.Edit;

            _viewModelUtility.EntityUtility = loData;
            _viewModelUtility.EntityUtility.CPROPERTY_ID = _viewModel.PropertyId;

            //await Task.Delay(100);
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
            await _gridRefUtility.R_SaveBatch();
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
                loEx.Add(_localizer["INVALID_DATE"], _localizer["START_DATE_LESS_THAN_END_DATE"]);
                eventArgs.Cancel = loEx.HasError;
            }

            loData.CSTART_DATE = loData.DSTART_DATE?.ToString("yyyyMMdd");
            loData.CEND_DATE = loData.DEND_DATE?.ToString("yyyyMMdd");

            //proses untuk merubah state LSELECTED
            var llStartDate = loData.DSTART_DATE != _viewModelUtility.EntityUtility.DSTART_DATE;
            var llEndDate = loData.DEND_DATE != _viewModelUtility.EntityUtility.DEND_DATE;
            var llBlock1End = loData.NBLOCK1_END != _viewModelUtility.EntityUtility.NBLOCK1_END;
            var llBlock2End = loData.NBLOCK2_END != _viewModelUtility.EntityUtility.NBLOCK2_END;
            var llBebanBersama = loData.NBEBAN_BERSAMA != _viewModelUtility.EntityUtility.NBEBAN_BERSAMA;
            var llMeterEnd = loData.NMETER_END != _viewModelUtility.EntityUtility.NMETER_END;
            if (llStartDate || llEndDate || llBlock1End || llBlock2End || llBebanBersama || llMeterEnd)
            {
                loData.LSELECTED = true;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        // completed task
        loEx.ThrowExceptionIfErrors();
    }


    private void BeforeEditUtility(R_BeforeEditEventArgs eventArgs)
    {
        var loData = (PMT03500UtilityUsageDTO)eventArgs.Data;
        eventArgs.Cancel = loData.CSTATUS.Length > 0;
    }

    private void SetEdit(R_SetEditGridColumnEventArgs eventArgs)
    {
        var loData = (PMT03500UtilityUsageDTO)eventArgs.Data;
        var loColumn =
            eventArgs.Columns.FirstOrDefault(x => x.FieldName == nameof(PMT03500UtilityUsageDTO.DSTART_DATE));
        loColumn.Enabled = !loData.LDISABLED_START_DATE;
    }

    private async Task SaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            // await _viewModelUtility.SaveBatch((List<PMT03500UtilityUsageDTO>)eventArgs.Data, ClientHelper.CompanyId, ClientHelper.UserId);
            var loTempDataList = (List<PMT03500UtilityUsageDTO>)eventArgs.Data;
            var loDataList =
                R_FrontUtility.ConvertCollectionToCollection<PMT03500UploadUtilityErrorValidateDTO>(loTempDataList
                    .Where(x => x.LSELECTED).ToList());

            var loUtilityType = loTempDataList.FirstOrDefault().CUTILITY_TYPE;
            _viewModelSave.CompanyId = ClientHelper.CompanyId;
            _viewModelSave.UserId = ClientHelper.UserId;
            _viewModelSave.UploadParam.CPROPERTY_ID = _viewModelUtility.Property.CPROPERTY_ID;
            _viewModelSave.UploadParam.EUTILITY_TYPE =
                loUtilityType is "01" or "02"
                    ? EPMT03500UtilityUsageType.EC
                    : EPMT03500UtilityUsageType.WG;

            _viewModelSave.IsUpload = false;

            await _viewModelSave.SaveBulkFile(poUploadParam: _viewModelSave.UploadParam,
                poDataList: loDataList.ToList());
            if (_viewModelSave.IsError)
            {
                loEx.Add("Error", "Utility Usage saved is not successfully!");
            }

            _enabledBtn = true;

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


    #region Other Utility dock

    private R_ComboBox<PMT03500BuildingDTO, string> _comboBuildingRef;
    private R_ComboBox<PMT03500FloorDTO, string> _comboFloorRef;

    private async Task OnChangedRadio(bool value, eParamType eType)
    {
        //assign value to property (IsBuildingSelected or IsFloorSelected) based on eType
        if (eType == eParamType.Building) _viewModelUtility.IsBuildingSelected = value;
        else if (eType == eParamType.Floor) _viewModelUtility.IsFloorSelected = value;

        //if value is true, assign value to property (TempBuildingId or TempFloorId) based on eType
        if (value)
        {
            switch (eType)
            {
                case eParamType.Building:
                    _viewModelUtility.Entity.CBUILDING_ID = _viewModelUtility.TempBuildingId;
                    await _viewModelUtility.GetFloorList();
                    break;
                case eParamType.Floor:
                    _viewModelUtility.FloorId = _viewModelUtility.TempFloorId;
                    break;
            }
        }
        else
        {
            switch (eType)
            {
                case eParamType.Building:
                    _viewModelUtility.TempBuildingId ??= _viewModelUtility.Entity.CBUILDING_ID;
                    _viewModelUtility.Entity.CBUILDING_ID = "";
                    /*
                     - Kosongkan FloorId jika BuildingId kosong
                     - jika IsFloorSelected bernilai true, maka set IsFloorSelected menjadi false, dan jika bernilai false, maka tidak perlu di set
                    */
                    _viewModelUtility.FloorId = "";
                    _viewModelUtility.IsFloorSelected = _viewModelUtility.IsFloorSelected && false;
                    break;
                case eParamType.Floor:
                    _viewModelUtility.TempFloorId = _viewModelUtility.FloorId;
                    _viewModelUtility.FloorId = "";
                    break;
            }
        }
    }

    private async Task OnLostFocusedBuilding()
    {
        var lcValue = _viewModelUtility.Entity.CBUILDING_ID;
        if (string.IsNullOrEmpty(lcValue))
        {
            await _comboBuildingRef.FocusAsync();
            _viewModelUtility.FloorId = "";
            _viewModelUtility.IsFloorSelected = _viewModelUtility.IsFloorSelected && false;
        }
        else
        {
            await _viewModelUtility.GetFloorList();
        }
    }

    private async Task OnLostFocusedFloor()
    {
        var lcValue = _viewModelUtility.FloorId;
        if (string.IsNullOrEmpty(lcValue))
        {
            await _comboFloorRef.FocusAsync();
        }

        await Task.CompletedTask;
    }

    #endregion

    private void CheckBoxSelectValueChanged(R_CheckBoxSelectValueChangedEventArgs eventArgs)
    {
        eventArgs.Enabled = true;
    }


    private void OnClickProcess(MouseEventArgs eventArgs)
    {
        var loData = _gridRefUtility.DataSource;
        foreach (var loItem in loData)
        {
            loItem.DEND_DATE = _viewModelUtility.UtilityPeriodToDtDt;
        }
    }
    
    private void CheckEditUtility(R_CheckEditEventArgs eventArgs)
    {
        eventArgs.Allow = !_viewModelUtility.Invoiced;
    }
}