using BlazorClientHelper;
using PMT01500Common.DTO._1._AgreementList.Upload;
using PMT01500Common.Utilities.Front;
using PMT01500Model.ViewModel;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd;


namespace PMT01500Front;
public partial class PMT01500AgreementList_Upload
{

    private PMT01500UploadViewModel _viewModel = new PMT01500UploadViewModel();
    private R_Grid<PMT01500UploadErrorValidateDTO>? _gridRefLeaseManagerUpload;
    private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Excel };
    [Inject] IClientHelper? clientHelper { get; set; }
    [Inject] R_IExcel? ExcelInject { get; set; }
    [Inject] IJSRuntime? JSRuntime { get; set; }

    private bool FileHasData;

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
    public async Task ShowSuccessInvoke()
    {
        var loValidate = await R_MessageBox.Show("", "Upload Data Successfully", (R_eMessageBoxButtonType)1);
        if (loValidate == R_eMessageBoxResult.OK)
            await this.Close(true, true);

    }
    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            PMT01500ParameterUploadChangePageDTO loTempParameterPropertyId = (PMT01500ParameterUploadChangePageDTO)poParameter;
            _viewModel.CurrentObjectParam = new PMT01500UploadParameterContext()
            {
                CPROPERTY_ID = loTempParameterPropertyId.CPROPERTY_ID,
                CPROPERTY_NAME = loTempParameterPropertyId.CPROPERTY_NAME
            };
            _viewModel.CompanyId = clientHelper.CompanyId;
            _viewModel.UserId = clientHelper.UserId;

            await _viewModel.GetPropertyList();

            _viewModel.StateChangeAction = StateChangeInvoke;
            _viewModel.ActionDataSetExcel = ActionFuncDataSetExcel;
            _viewModel.DisplayErrorAction = DisplayErrorInvoke;
            _viewModel.ShowSuccessAction = async () =>
            {
                await ShowSuccessInvoke();
            };
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

            var loDataSet = loExcel.R_ReadFromExcel(fileByte, new[] { "LeaseManager" });
            var loResult = R_FrontUtility.R_ConvertTo<PMT01500UploadFromExcelDTO>(loDataSet.Tables[0]);

            FileHasData = loResult.Count > 0;

            await _gridRefLeaseManagerUpload.R_RefreshGrid(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        R_DisplayException(loEx);
    }

    private async Task Upload_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        R_Exception loEx = new R_Exception();
        try
        {
            List<PMT01500UploadFromExcelDTO> loData = (List<PMT01500UploadFromExcelDTO>)eventArgs.Parameter;

            await _viewModel.ConvertGrid(loData);
            eventArgs.ListEntityResult = _viewModel.LeaseManagerValidateUploadError;
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

                /*
                if (_viewModel.VisibleError)
                    await R_MessageBox.Show("", "Cash Bank uploaded successfully!", R_eMessageBoxButtonType.OK);

                if (_viewModel._statusFinal)
                    await Close(true, true);
                */

            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
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
        var lcName = $"Lease Manager {_viewModel.PropertyValue}" + ".xlsx";

        await JSRuntime.downloadFileFromStreamHandler(lcName, loByte);
    }

    public async Task Button_OnClickCloseAsync()
    {
        await this.Close(true, false);
    }

}
