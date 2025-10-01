using APB00200COMMON.DTO_s;
using APB00200MODEL.View_Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using APB00200FrontResources;


namespace APB00200FRONT
{
    public partial class APB00201 : R_Page
    {
        //variables
        private APB00200ViewModel _viewModel = new();
        private R_ConductorGrid _conAPProcessError;
        private R_Grid<ErrorCloseAPProcessDTO> _gridAPProcessError;
        [Inject] R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IJSRuntime _jsRuntime { get; set; }
        [Inject] R_IExcel _excel { get; set; }
        private R_eFileSelectAccept[] _accepts = { R_eFileSelectAccept.Excel };

        //methods
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel.ClosePeriod = R_FrontUtility.ConvertObjectToObject<ClosePeriodDTO>(poParameter);
                await _gridAPProcessError.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task GridAPCloseError_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetListErrorProcessCloseAPPrdAsync();
                eventArgs.ListEntityResult = _viewModel.ListErrorProcess;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task SaveToExcel_OnclickAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                List<ErrorCloseAPProcessExcelDTO> loExcelList = new(_viewModel.ListErrorProcess.Select(e => new ErrorCloseAPProcessExcelDTO
                {
                    No = e.ISEQ_NO,
                    Description = e.CDESCRIPTION,
                    Solution = e.CSOLUTION_DESCR
                }).ToList());

                var loDataTable = R_FrontUtility.R_ConvertTo(loExcelList);
                loDataTable.TableName = "ErrorList";

                //export to excel
                var loByteFile = _excel.R_WriteToExcel(loDataTable);
                var saveFileName = $"CLOSE_AP_CurrentPeriod_ERROR_LIST.xlsx";
                await _jsRuntime.downloadFileFromStreamHandler(saveFileName, loByteFile);
                await Close(true, null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
