using BlazorClientHelper;
using GSM06000Common;
using GSM06000Model.ViewModel;
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

namespace GSM06000Front;

public partial class GSM06000UploadCenter : R_Page
{

    private UploadCenterViewModel _viewModel = new UploadCenterViewModel();
    private R_Grid<GSM06000UploadErrorValidateDTO>? JournalGroup_gridRef;
    private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Excel };
    [Inject] IClientHelper? clientHelper { get; set; }
    [Inject] R_IExcel? ExcelInject { get; set; }
    [Inject] IJSRuntime? JSRuntime { get; set; }

    private bool FileHasData = false;

    public byte[]? fileByte = null;

    private void StateChangeInvoke()
    {
        StateHasChanged();
    }
    #region HandleError
    private void DisplayErrorInvoke(R_Exception poException)
    {
        this.R_DisplayException(poException);
    }
    #endregion
    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.CurrentObjectParam = (GSM06000ParamDTO)poParameter;
            _viewModel.CurrentObjectParam.CUSER_ID = clientHelper.UserId;

            _viewModel.CompanyId = clientHelper.CompanyId;
            _viewModel.UserId = clientHelper.UserId;

            await _viewModel.GetTypeHeaderParameter();

            _viewModel.StateChangeAction = StateChangeInvoke;
            _viewModel.ActionDataSetExcel = ActionFuncDataSetExcel;
            _viewModel.DisplayErrorAction = DisplayErrorInvoke;
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);

    }
    private async Task SourceUpload_OnChange(InputFileChangeEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            //get file name
            _viewModel.SourceFileName = eventArgs.File.Name;

            //import excel from user
            var loMS = new MemoryStream();
            await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
            var fileByte = loMS.ToArray();

            //READ EXCEL
            var loExcel = ExcelInject;

            var loDataSet = loExcel.R_ReadFromExcel(fileByte, new[] { "CashBank" });
            var loResult = R_FrontUtility.R_ConvertTo<GSM06000UploadFromExcelDTO>(loDataSet.Tables[0]);

            FileHasData = loResult.Count > 0 ? true : false;

            await JournalGroup_gridRef.R_RefreshGrid(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    private async Task Upload_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        R_Exception loEx = new R_Exception();
        try
        {
            List<GSM06000UploadFromExcelDTO> loData = (List<GSM06000UploadFromExcelDTO>)eventArgs.Parameter;

            await _viewModel.ConvertGrid(loData);
            eventArgs.ListEntityResult = _viewModel.JournalGroupValidateUploadError;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        R_DisplayException(loEx);
    }

    public async Task Button_OnClickOkAsync()
    {
        var loEx = new R_Exception();
        try
        {
            var loValidate = await R_MessageBox.Show("", "Are you sure want to import data?", R_eMessageBoxButtonType.YesNo);

            if (loValidate == R_eMessageBoxResult.Yes)
            {
                await _viewModel.SaveFileBulkFile();

                if (_viewModel.VisibleError)
                    await R_MessageBox.Show("", "Journal Group uploaded successfully!", R_eMessageBoxButtonType.OK);

                if (_viewModel._statusFinal)
                    await Close(true, true);

            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task Button_OnClickSaveAsync()
    {
        var loEx = new R_Exception();
        try
        {
            var loValidate = await R_MessageBox.Show("", "Are you sure want to save to excel?", R_eMessageBoxButtonType.YesNo);

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

    // Create Method Action For Download Excel if Has Error
    private async Task ActionFuncDataSetExcel()
    {
        var loByte = ExcelInject.R_WriteToExcel(_viewModel.ExcelDataSet);
        var lcName = $"Cash and Bank {_viewModel.PropertyValue}" + ".xlsx";

        await JSRuntime.downloadFileFromStreamHandler(lcName, loByte);
    }

    public async Task Button_OnClickCloseAsync()
    {
        await this.Close(true, false);
    }



}