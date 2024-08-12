using BlazorClientHelper;
using GSM04500Common.DTOs;
using GSM04500Model.ViewModel;
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

namespace GSM04500Front;

public partial class GSM04500UploadPopup
{
    private GSM04500UploadViewModel _viewModel = new();
    private R_Grid<GSM04500UploadFromSystemDTO> _gridRef = new();

    [Inject] R_IExcel _excelInject { get; set; }
    [Inject] IJSRuntime JS { get; set; }

    private void StateChangeInvoke()
    {
        StateHasChanged();
    }
    
    private void DisplayErrorInvoke(R_Exception poException)
    {
        this.R_DisplayException(poException);
    }
    
    public async Task ShowSuccessInvoke()
    {
        var loValidate = await R_MessageBox.Show("", "Upload Data Successfully", R_eMessageBoxButtonType.OK);
        if (loValidate == R_eMessageBoxResult.OK)
        {
            await this.Close(true, true);
        }
    }

    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.Init(poParam);
            
            _viewModel.StateChangeAction = StateChangeInvoke;
            _viewModel.ActionDataSetExcel = ActionFuncDataSetExcel;
            _viewModel.ShowErrorAction = DisplayErrorInvoke;
            _viewModel.ShowSuccessAction = async () =>
            {
                await ShowSuccessInvoke();
            };
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetList(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            eventArgs.ListEntityResult = _viewModel.UploadedList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
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
            // var loExcel = _excelInject;
            var loDataSet =
                _excelInject.R_ReadFromExcel(loFileByte, new[] { "JournalGroup" }); //ambil data di sheet Budget
            var loResult = R_FrontUtility.R_ConvertTo<GSM04500UploadFromFileDTO>(loDataSet.Tables[0]);

            _viewModel.AssignData(loResult);

            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    private void R_RowRender(R_GridRowRenderEventArgs eventArgs)
    {
        var loData = (GSM04500UploadFromSystemDTO)eventArgs.Data;

        if (loData.ErrorFlag == "N")
        {
            eventArgs.RowClass = "errorDataNotValid";
        }
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
            var loValidate = await R_MessageBox.Show("", "Are you sure want to import data?", R_eMessageBoxButtonType.YesNo);

            if (loValidate == R_eMessageBoxResult.Yes)
            {
                await _viewModel.UploadFile(_viewModel.UploadedList.ToList());
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
        var loEx = new R_Exception();
        try
        {
            var loValidate =
                await R_MessageBox.Show("", "Are you sure want to save to excel?", R_eMessageBoxButtonType.YesNo);

            if (loValidate == R_eMessageBoxResult.Yes)
            {
                await ActionFuncDataSetExcel();
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task ActionFuncDataSetExcel()
    {
        
        var loByte = _excelInject.R_WriteToExcel(_viewModel.ExcelDataSet);
        var lcName = $"Journal Group {_viewModel.Param.CPROPERTY_NAME}" + ".xlsx";


        await JS.downloadFileFromStreamHandler(lcName, loByte);
    }
}