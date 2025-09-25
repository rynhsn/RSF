using BlazorClientHelper;
using HDM00400Common.DTOs.Upload;
using HDM00400Model.ViewModel;
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

namespace HDM00400Front;

public partial class HDM00400PopupUpload : R_Page
{
    private HDM00400UploadViewModel _viewModel = new();
    private R_Grid<HDM00400UploadForSystemDTO> _gridRef = new();
    
    private R_eFileSelectAccept[] _accepts = { R_eFileSelectAccept.Excel };

    [Inject] IClientHelper ClientHelper { get; set; }
    [Inject] R_IExcel ExcelInject { get; set; }
    [Inject] IJSRuntime JSRuntime { get; set; }
    
    
    public void StateChangeInvoke()
    {
        StateHasChanged();
    }

    #region HandleError

    private void DisplayErrorInvoke(R_Exception poException)
    {
        R_DisplayException(poException);
    }

    #endregion

    private async Task ShowSuccessInvoke()
    {
        var loValidate = await R_MessageBox.Show("", _localizer["UploadSuccessfully"], R_eMessageBoxButtonType.OK);
        if (loValidate == R_eMessageBoxResult.OK)
        {
            await Close(true, true);
        }
    }
    
    private async Task ActionFuncDataSetExcel()
    {
        var lcTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var loByte = ExcelInject.R_WriteToExcel(_viewModel.ExcelDataSet);
        var lcName = $"PublicLocation_{_viewModel.UploadParam.CPROPERTY_ID}_{lcTime}" + ".xlsx";

        await JSRuntime.downloadFileFromStreamHandler(lcName, loByte);
    }

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            //var Param = (GSM00710UploadErrorValidateParamDTO)poParameter;

            ////Assign Company, User and others value for Parameters
            _viewModel.Init(poParameter);
            
            _viewModel.CompanyId = ClientHelper.CompanyId;
            _viewModel.UserId = ClientHelper.UserId;

            _viewModel.StateChangeAction = StateChangeInvoke;
            _viewModel.DisplayErrorAction = DisplayErrorInvoke;
            _viewModel.ActionDataSetExcel = ActionFuncDataSetExcel;
            _viewModel.ShowSuccessAction = async () => { await ShowSuccessInvoke(); };
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        await R_DisplayExceptionAsync(loEx);
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

            var loDataSet = loExcel.R_ReadFromExcel(fileByte, new[] { "PublicLocation" });
            var loResult = R_FrontUtility.R_ConvertTo<HDM00400UploadFromFileDTO>(loDataSet.Tables[0]);

            _viewModel.FileHasData = loResult.Count > 0;

            await _gridRef.R_RefreshGrid(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }
    
    private async Task GetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loData = (List<HDM00400UploadFromFileDTO>)eventArgs.Parameter;

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
                await R_MessageBox.Show("", _localizer["AreYouSureWantToImportData"], R_eMessageBoxButtonType.YesNo);

            if (loValidate == R_eMessageBoxResult.Yes)
            {
                await _viewModel.SaveBulkFile(_viewModel.UploadParam, _viewModel.GridListUpload.ToList());

                // if (!_viewModel.IsError)
                // {
                //     var loResult = await R_MessageBox.Show("", _localizer["PublicLocationUploadedSuccessfully"], R_eMessageBoxButtonType.OK);
                //     if (loResult == R_eMessageBoxResult.OK)
                //     {
                //         await Close(true, true);
                //     }
                // }
            }
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

    public async Task OnClickCancelAsync()
    {
        await Close(false, false);
    }
    
    private void RowRender(R_GridRowRenderEventArgs eventArgs)
    {
        var loData = (HDM00400UploadForSystemDTO)eventArgs.Data;

        if (loData.ValidFlag == "N")
        {
            eventArgs.RowClass = "errorDataLValidIsFalse";
        }
    }

}