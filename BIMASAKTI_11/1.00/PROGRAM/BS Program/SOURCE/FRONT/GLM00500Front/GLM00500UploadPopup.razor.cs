using BlazorClientHelper;
using GLM00500Common.DTOs;
using GLM00500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GLM00500Front;

public partial class GLM00500UploadPopup : R_Page
{
    private GLM00500UploadViewModel _viewModel = new();
    private R_Grid<GLM00500UploadForSystemDTO> _gridRef = new();

    [Inject] IClientHelper ClientHelper { get; set; }
    [Inject] R_IExcel ExcelInject { get; set; }
    [Inject] IJSRuntime JSRuntime { get; set; }

    // Create Method Action StateHasChange
    private void StateChangeInvoke()
    {
        StateHasChanged();
    }

    // Create Method Action For Download Excel if Has Error
    private async Task ActionFuncDataSetExcel()
    {
        var lcTime = DateTime.Now.ToString("yyyyMMdd_HHmm");
        var loByte = ExcelInject.R_WriteToExcel(_viewModel.ExcelDataSet);
        var lcName = $"ACCOUNT_BUDGET_UPLOAD_{lcTime}" + ".xlsx";

        await JSRuntime.downloadFileFromStreamHandler(lcName, loByte);
    }

    // Create Method Action For Error Unhandle
    private void ShowErrorInvoke(R_Exception poEx)
    {
        this.R_DisplayException(poEx);
    }

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.CompanyId = ClientHelper.CompanyId;
            _viewModel.UserId = ClientHelper.UserId;
            
            //Assign Action
            _viewModel.StateChangeAction = StateChangeInvoke;
            _viewModel.ShowErrorAction = ShowErrorInvoke;
            _viewModel.ActionDataSetExcel = ActionFuncDataSetExcel;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetUploadedList(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            // await _viewModel.GetUploadBudgetList(_viewModel.keyGuid); 
            eventArgs.ListEntityResult = _viewModel.UploadedList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void R_RowRender(R_GridRowRenderEventArgs eventArgs)
    {
        var loData = (GLM00500UploadForSystemDTO)eventArgs.Data;

        if (loData.VALID=="Y")
        {
            eventArgs.RowStyle = new R_GridRowRenderStyle
            {
                FontColor = "red"
            };
        }
    }

    private async Task ChooseFile(InputFileChangeEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            // _viewModel.SourceFileName = eventArgs.File.Name;
            //import from excel
            var loMemoryStream = new MemoryStream();
            await eventArgs.File.OpenReadStream().CopyToAsync(loMemoryStream);
            var loFileByte = loMemoryStream.ToArray();

            //add filebyte to DTO
            // var loExcel = ExcelInject;
            var loDataSet =
                ExcelInject.R_ReadFromExcel(loFileByte, new[] { "Budget" }); //ambil data di sheet Budget
            var loResult = R_FrontUtility.R_ConvertTo<GLM00500UploadFromFileDTO>(loDataSet.Tables[0]);

            await _viewModel.AssignData(loResult);
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task CloseEvent(MouseEventArgs eventArgs)
    {
        await this.Close(true, null);
    }

    private async Task OnClickProcess()
    {
        var loEx = new R_Exception();

        try
        {
            var loValidate =
                await R_MessageBox.Show("", _localizer["ConfirmImport"], R_eMessageBoxButtonType.YesNo);

            if (loValidate == R_eMessageBoxResult.Yes)
            {
                await _viewModel.UploadFile(_viewModel.UploadedList.ToList());

                if (_viewModel.IsError)
                {
                    await R_MessageBox.Show(_localizer["SuccessLabel"], _localizer["SuccessUpload"], R_eMessageBoxButtonType.OK);
                }
                else
                {
                    await _gridRef.R_RefreshGrid(null);
                }
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnClickSave()
    {
        // var loValidate = await R_MessageBox.Show("", "Are you sure want to save to excel?", R_eMessageBoxButtonType.YesNo);
        //
        // if (loValidate == R_eMessageBoxResult.Yes)
        // {
            await ActionFuncDataSetExcel();
        // }
    }
}