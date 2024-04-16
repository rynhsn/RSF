using BlazorClientHelper;
using PMT03500Common;
using PMT03500Common.DTOs;
using PMT03500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace LMT03500Front;

public partial class PMT03500UploadCutOffPopup : R_Page
{
    private PMT03500UploadCutOffViewModel _viewModel = new();
    private R_Grid<PMT03500UploadCutOffErrorValidateDTO> _gridRef = new();

    private R_eFileSelectAccept[] _accepts = { R_eFileSelectAccept.Excel };

    [Inject] IClientHelper ClientHelper { get; set; }
    [Inject] R_IExcel ExcelInject { get; set; }
    [Inject] IJSRuntime JSRuntime { get; set; }

    private bool _visibleColumnEC { get; set; } = false;
    private bool _visibleColumnWG { get; set; } = false;

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

    public async Task ShowSuccessInvoke()
    {
        var loValidate = await R_MessageBox.Show("", "Upload Successfully", R_eMessageBoxButtonType.OK);
        if (loValidate == R_eMessageBoxResult.OK)
        {
            await Close(true, true);
        }
    }

    // Create Method Action For Download Excel if Has Error
    private async Task ActionFuncDataSetExcel()
    {
        var lcTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var loByte = ExcelInject.R_WriteToExcel(_viewModel.ExcelDataSet);
        var lcName = $"UtilityUsage_{_viewModel.UploadParam.CPROPERTY_ID}_{lcTime}" + ".xlsx";

        await JSRuntime.downloadFileFromStreamHandler(lcName, loByte);
    }

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.Init(poParameter);
            
            _checkUtilityType();
            
            _viewModel.CompanyId = ClientHelper.CompanyId;
            _viewModel.UserId = ClientHelper.UserId;

            _viewModel.StateChangeAction = StateChangeInvoke;
            _viewModel.ActionDataSetExcel = ActionFuncDataSetExcel;
            _viewModel.DisplayErrorAction = DisplayErrorInvoke;
            _viewModel.ShowSuccessAction = async () => { await ShowSuccessInvoke(); };
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        await R_DisplayExceptionAsync(loEx);
    }


    private async Task GetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loData = (List<PMT03500UploadCutOffExcelDTO>)eventArgs.Parameter;

            await _viewModel.ConvertGrid(loData);
            eventArgs.ListEntityResult = _viewModel.GridListUpload;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        await R_DisplayExceptionAsync(loEx);
    }

    public async Task OnClickProcessAsync()
    {
        var loEx = new R_Exception();
        try
        {
            var loValidate =
                await R_MessageBox.Show("", "Are you sure want to import data?", R_eMessageBoxButtonType.YesNo);

            if (loValidate == R_eMessageBoxResult.Yes)
            {
                await _viewModel.SaveBulkFile();

                if (_viewModel.IsError)
                {
                    var loResult = await R_MessageBox.Show("", "Utility Usage uploaded successfully!", R_eMessageBoxButtonType.OK);
                    if (loResult == R_eMessageBoxResult.OK)
                    {
                        await Close(true, true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnChangeUploadSrc(InputFileChangeEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            //get file name
            // _viewModel.SourceFileName = eventArgs.File.Name;

            //import excel from user
            var loMS = new MemoryStream();
            await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
            var fileByte = loMS.ToArray();

            //READ EXCEL
            var loExcel = ExcelInject;

            var loDataSet = loExcel.R_ReadFromExcel(fileByte, new[] { "UtilityUsage" });
            var loResult = R_FrontUtility.R_ConvertTo<PMT03500UploadCutOffExcelDTO>(loDataSet.Tables[0]);

            _viewModel.FileHasData = loResult.Count > 0 ? true : false;

            await _gridRef.R_RefreshGrid(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task OnClickSaveAsync()
    {
        var loEx = new R_Exception();
        try
        {
            var loValidate = await R_MessageBox.Show("", "Are you sure want to save to excel again?",
                R_eMessageBoxButtonType.YesNo);

            if (loValidate == R_eMessageBoxResult.Yes)
            {
                await ActionFuncDataSetExcel();
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task OnClickCloseAsync()
    {
        await Close(false, false);
    }
    
    private void _checkUtilityType()
    {
        switch (_viewModel.UploadParam.EUTILITY_TYPE)
        {
            case EPMT03500UtilityUsageType.EC:
                _visibleColumnEC = true;
                break;
            case EPMT03500UtilityUsageType.WG:
                _visibleColumnWG = true;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}