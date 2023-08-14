using GLM00500Common.DTOs;
using GLM00500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GLM00500Front;

public partial class GLM00500UploadPopup : R_Page
{
    private GLM00500UploadViewModel _viewModel = new();
    // private R_ConductorGrid _conductorRef;
    private R_Grid<GLM00500UploadFromSystemDTO> _gridRef = new();
    private R_ConductorGrid _conductorRef;
    
    // private R_ConductorGrid _conductorErrorRef;
    private R_Grid<GLM00500UploadErrorDTO> _gridErrorRef = new();
    private R_ConductorGrid _conductorErrorRef;

    [Inject] R_IExcel ExcelInject { get; set; }
    
    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();
        
        try
        {
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
            var loParam = (GLM00500UploadCheckErrorDTO)eventArgs.Parameter;
            await _viewModel.GetUploadBudgetList(loParam);
            eventArgs.ListEntityResult = _viewModel.UploadedList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task GetErrorList(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        
        try
        {
            eventArgs.ListEntityResult = _viewModel.ErrorList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }

    private async Task CloseEvent(MouseEventArgs eventArgs)
    {
        await this.Close(true, null);
    }

    private async Task FileHasSelected(InputFileChangeEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.SourceFileName = eventArgs.File.Name;
            //import from excel
            var loMemoryStream = new MemoryStream();
            await eventArgs.File.OpenReadStream().CopyToAsync(loMemoryStream);
            var loFileByte = loMemoryStream.ToArray();

            //add filebyte to DTO
            var loExcel = ExcelInject;
            var loDataSet = loExcel.R_ReadFromExcel(loFileByte, new string[] { "Budget" });
            var loResult = R_FrontUtility.R_ConvertTo<GLM00500UploadFromFileDTO>(loDataSet.Tables[0]);

            var loData = new List<GLM00500UploadFromFileDTO>(loResult);
            
            _viewModel.UploadListResult = _viewModel.ConvertData(loData);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        
    }

    private async Task UploadProcess(MouseEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        
        try
        {
            var loCheckResult = await _viewModel.CheckUpload(_viewModel.UploadListResult);
            
            if (loCheckResult.IERROR_COUNT == 0)
            {
                await _viewModel.UploadBudget(loCheckResult, _viewModel.UploadListResult);
                R_MessageBox.Show("Upload Success", "Account Budget uploaded successfully!", R_eMessageBoxButtonType.OK);
            }
            else
            {
                await _gridRef.R_RefreshGrid(loCheckResult);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }
}