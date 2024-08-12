using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using PMM02000Common;
using PMM02000Model.ViewModel;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace PMM02000Front;

public partial class PMM02000Upload : R_Page
{
    private PMM02000UploadViewModel _viewModel = new();
    private R_Grid<PMM02000FromExcelDTO> _grid = new();
    private R_Conductor _conductor;


    [Inject] IClientHelper ClientHelper { get; set; }
    [Inject] R_IExcel ExcelInject { get; set; }
    [Inject] IJSRuntime JSRuntime { get; set; }

    private R_eFileSelectAccept[] _accepts = { R_eFileSelectAccept.Excel };

    protected override async Task R_Init_From_Master(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (string)eventArgs;
            _viewModel.PropertyId = loParam;
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
            // var loExcel = ExcelInject;

            var loDataSet = ExcelInject.R_ReadFromExcel(fileByte, new[] { "Salesman" });
            var loResult = R_FrontUtility.R_ConvertTo<PMM02000FromExcelDTO>(loDataSet.Tables[0]);

            // _viewModel.FileHasData = loResult.Count > 0 ? true : false;

            await _grid.R_RefreshGrid(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void GetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loData = (List<PMM02000FromExcelDTO>)eventArgs.Parameter;
            _viewModel.ConvertGrid(loData);
            eventArgs.ListEntityResult = _viewModel.GridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}