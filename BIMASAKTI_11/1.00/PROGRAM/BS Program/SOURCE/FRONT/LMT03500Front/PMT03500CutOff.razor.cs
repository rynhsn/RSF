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

public partial class PMT03500CutOff : R_ITabPage
{
    
    private PMT03500ViewModel _viewModel = new();
    private PMT03500UploadCutOffViewModel _viewModelUpload = new();
    // private PMT03500UploadCutOffViewModel _viewModelUploadCutOff = new();
    private PMT03500UtilityUsageViewModel _viewModelUtility = new();
    
    private R_Conductor _conductorRef;
    private R_ConductorGrid _conductorRefUtility;
    private R_Grid<PMT03500BuildingDTO> _gridRefBuilding = new();
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
        await R_MessageBox.Show("", "Update Successfully", R_eMessageBoxButtonType.OK);
        await _gridRefUtility.R_RefreshGrid(null);
        
    }

    
    protected override async Task R_Init_From_Master(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            // await _viewModel.Init();
            await _viewModelUtility.Init(eventArgs);
            await _gridRefBuilding.R_RefreshGrid(null);
            // _viewModelUtility.ActionDataSetExcel = ActionFuncDataSetExcel;

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
            await _viewModelUtility.GetUtilityCutOffList();
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
                case eParamType.UtilityYear:
                    _viewModelUtility.UtilityPeriodYear = (string)value;
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
        await _gridRefBuilding.R_RefreshGrid(null);
        _display = "d-none";
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
    
    private void OnClickCancel()
    {
        _viewModelUtility.GridUtilityUsageList = new ObservableCollection<PMT03500UtilityUsageDTO>(_viewModelUtility.GridUtilityUsageListTemp);
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
            });
            
            var loDataList = R_FrontUtility.ConvertCollectionToCollection<PMT03500UploadCutOffErrorValidateDTO>(loTempDataList);
            // var loDataList = R_FrontUtility.ConvertCollectionToCollection<PMT03500UploadUtilityErrorValidateDTO>(loTempDataList);

            var loUtilityType = loTempDataList.FirstOrDefault()?.CUTILITY_TYPE;
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
        if(eventArgs.Success)
        {
            await _gridRefUtility.R_RefreshGrid(null);
        }
    }
    
    private void OnChangedAllFloor(object obj)
    {
        _viewModelUtility.CheckFloor((bool)obj);
    }
}