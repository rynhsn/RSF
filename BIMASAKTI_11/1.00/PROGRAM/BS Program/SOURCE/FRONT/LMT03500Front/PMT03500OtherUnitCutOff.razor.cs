using System.Collections.ObjectModel;
using BlazorClientHelper;
using PMT03500Front;
using PMT03500Common.DTOs;
using PMT03500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PMT03500Common;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace PMT03500Front;

public partial class PMT03500OtherUnitCutOff : R_Page, R_ITabPage
{
    private PMT03500ViewModel _viewModel = new();

    private PMT03500UploadCutOffViewModel _viewModelUpload = new();

    // private PMT03500UploadCutOffViewModel _viewModelUploadCutOff = new();
    private PMT03500UtilityUsageViewModel _viewModelUtility = new();

    private R_Conductor _conductorRef;
    private R_ConductorGrid _conductorRefUtility;
    private R_Grid<PMT03500UtilityUsageDTO> _gridRefUtility = new();

    [Inject] private IJSRuntime JS { get; set; }
    [Inject] private R_IExcel ExcelInject { get; set; }
    [Inject] IClientHelper ClientHelper { get; set; }

    private string _dataLabel = "";
    private string _display = "d-none";
    private bool _visibleColumnEC;
    private bool _visibleColumnWG;

    private bool _enabledBtn = true;


    private void StateChangeInvoke()
    {
        StateHasChanged();
    }

    #region HandleError

    private void DisplayErrorInvoke(R_Exception poException)
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
        await R_MessageBox.Show("", _localizer["UPDATE_SUCCESSFULLY"], R_eMessageBoxButtonType.OK);
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

            _viewModelUpload.StateChangeAction = StateChangeInvoke;
            _viewModelUpload.DisplayErrorAction = DisplayErrorInvoke;
            _viewModelUpload.ActionDataSetExcel = ActionFuncDataSetExcel;
            _viewModelUpload.ShowSuccessAction = async () => { await ShowSuccessUpdateInvoke(); };
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
            await _viewModelUtility.GetUtilityCutOffList();
            eventArgs.ListEntityResult = _viewModelUtility.GridUtilityUsageList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public int _maxValueStart = 999999;

    private void GetUtilityRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loData = (PMT03500UtilityUsageDTO)eventArgs.Data;
            _viewModelUtility.EntityUtility = loData;
            _maxValueStart = loData.IMETER_MAX_RESET;
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
                case eParamType.UtilityType:
                    _viewModelUtility.UtilityTypeId = (string)value;
                    _viewModelUtility.SetParameterHeader();
                    break;
                case eParamType.Floor:
                    _viewModelUtility.FloorId = (string)value;
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
                    _viewModelUtility.UtilityType = EPMT03500UtilityUsageType.EC;
                    _dataLabel = _localizer["LBL_EC"];
                    _display = "d-block";
                    _visibleColumnEC = true;
                    _visibleColumnWG = false;
                    break;
                case "03" or "04":
                    _viewModelUtility.UtilityType = EPMT03500UtilityUsageType.WG;
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

        loEx.ThrowExceptionIfErrors();
    }

    public async Task RefreshTabPageAsync(object poParam)
    {
        await _viewModelUtility.Init(poParam);
        await _viewModelUtility.GetBuildingList();
        _display = "d-none";
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

    private void OnClickCancel()
    {
        _viewModelUtility.GridUtilityUsageList =
            new ObservableCollection<PMT03500UtilityUsageDTO>(_viewModelUtility.GridUtilityUsageListTemp);
    }

    private void OnClickProcess()
    {
        var loData = _gridRefUtility.DataSource;
        foreach (var loItem in loData)
        {
            loItem.DSTART_DATE = _viewModelUtility.UtilityPeriodFromDtDt;
        }
    }

    private void CheckBoxSelectValueChanged(R_CheckBoxSelectValueChangedEventArgs eventArgs)
    {
        eventArgs.Enabled = true;
    }

    private async Task SaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            // await _viewModelUtility.SaveBatch((List<PMT03500UtilityUsageDTO>)eventArgs.Data, ClientHelper.CompanyId, ClientHelper.UserId);
            var loTempDataList = (List<PMT03500UtilityUsageDTO>)eventArgs.Data;

            //ubah DSTART_DATE ke CSTART_DATE 
            loTempDataList.ForEach(x =>
            {
                x.CSTART_DATE = x.DSTART_DATE?.ToString("yyyyMMdd");
                x.CINV_PRD = string.IsNullOrEmpty(x.CINV_PRD)
                    ? _viewModelUtility.InvPeriodYear + _viewModelUtility.InvPeriodNo
                    : x.CINV_PRD_YEAR + x.CINV_PRD_MONTH;
            });

            var loDataList =
                R_FrontUtility.ConvertCollectionToCollection<PMT03500UploadCutOffErrorValidateDTO>(loTempDataList
                    .Where(x => x.LSELECTED).ToList());
            // var loDataList = R_FrontUtility.ConvertCollectionToCollection<PMT03500UploadUtilityErrorValidateDTO>(loTempDataList);

            var loUtilityType = loTempDataList.FirstOrDefault()?.CUTILITY_TYPE;
            _viewModelUpload.CompanyId = ClientHelper.CompanyId;
            _viewModelUpload.UserId = ClientHelper.UserId;
            _viewModelUpload.UploadParam.CPROPERTY_ID = _viewModelUtility.Property.CPROPERTY_ID;
            _viewModelUpload.UploadParam.EUTILITY_TYPE =
                loUtilityType is "01" or "02"
                    ? EPMT03500UtilityUsageType.EC
                    : EPMT03500UtilityUsageType.WG;

            _viewModelUpload.IsUpload = false;

            await _viewModelUpload.SaveBulkFile(poUploadParam: _viewModelUpload.UploadParam,
                poDataList: loDataList.ToList());

            if (_viewModelUpload.IsError)
            {
                loEx.Add("Error", "Cut Off saved is not successfully!");
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

    private async Task DownloadTemplate()
    {
        var loEx = new R_Exception();

        try
        {
            // var loByte = ExcelInject.R_WriteToExcel(_viewModelUtility.ExcelDataSetCutOff);
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

    private void DisplayUtility(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loData = (PMT03500UtilityUsageDTO)eventArgs.Data;
            // _tabDetail = loData.CUTILITY_TYPE is "03" or "04" && loData.CSTATUS.Length > 0;

            _viewModelUtility.EntityUtility = loData;
            // _viewModelUtility.EntityUtility.CPROPERTY_ID = _viewModel.PropertyId;

            // await Task.Delay(100);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task AfterOpenUpload(R_AfterOpenPopupEventArgs eventArgs)
    {
        if (eventArgs.Success)
        {
            await _gridRefUtility.R_RefreshGrid(null);
        }
    }

    private void ValidationUtility(R_ValidationEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loData = (PMT03500UtilityUsageDTO)eventArgs.Data;
            if (loData.NBLOCK1_START > loData.IMETER_MAX_RESET)
            {
                loEx.Add(_localizer["INVALID_MAX_RESET"], _localizer["BLOCK1_START_GREATER_THAN_MAX_RESET"]);
                eventArgs.Cancel = loEx.HasError;
            }

            if (loData.NBLOCK2_START > loData.IMETER_MAX_RESET)
            {
                loEx.Add(_localizer["INVALID_MAX_RESET"], _localizer["BLOCK2_START_GREATER_THAN_MAX_RESET"]);
                eventArgs.Cancel = loEx.HasError;
            }

            if (loData.NMETER_START > loData.IMETER_MAX_RESET)
            {
                loEx.Add(_localizer["INVALID_MAX_RESET"], _localizer["METER_START_GREATER_THAN_MAX_RESET"]);
                eventArgs.Cancel = loEx.HasError;
            }

            //validari start date
            if (loData.DSTART_DATE == null)
            {
                loEx.Add(_localizer["INVALID_START_DATE"], _localizer["START_DATE_MUST_BE_FILLED"]);
            }

            // if (string.IsNullOrEmpty(loData.CINV_PRD_YEAR) || string.IsNullOrEmpty(loData.CINV_PRD_MONTH))
            // {
            //     loEx.Add(_localizer["INVALID_INV_PERIOD"], _localizer["INV_PERIOD_MUST_BE_FILLED"]);
            // }

            eventArgs.Cancel = loEx.HasError;

            var llStartDate = loData.DSTART_DATE != _viewModelUtility.EntityUtility.DSTART_DATE;
            // var llInvPrd = (loData.CINV_PRD_YEAR + loData.CINV_PRD_MONTH) != _viewModelUtility.EntityUtility.CINV_PRD;
            var llBlock1Start = loData.NBLOCK1_START != _viewModelUtility.EntityUtility.NBLOCK1_START;
            var llBlock2Start = loData.NBLOCK2_START != _viewModelUtility.EntityUtility.NBLOCK2_START;
            var llMeterStart = loData.NMETER_START != _viewModelUtility.EntityUtility.NMETER_START;
            if (llStartDate ||
                // llInvPrd || 
                llBlock1Start ||
                llBlock2Start ||
                llMeterStart)
            {
                loData.LSELECTED = true;
                loData.CINV_PRD_YEAR = _viewModelUtility.InvPeriodYear;
                loData.CINV_PRD_MONTH = _viewModelUtility.InvPeriodNo;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
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
}