using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using PMM10000COMMON.Upload;
using PMM10000COMMON.UtilityDTO;
using PMM10000MODEL.ViewModel;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM10000FRONT
{
    public partial class PMM10000Upload : R_Page
    {
        private PMM10000ViewModel_Upload _viewModel = new();
        private R_Grid<CallTypeErrorDTO>? _gridRef;
        [Inject] public R_IExcel? ExcelInject { get; set; }
        [Inject] private IClientHelper? _ClientHelper { get; set; }
        [Inject] IJSRuntime? JSRuntime { get; set; }
        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Excel };
        private bool FileHasData = false;

        private void StateChangeInvoke()
        {
            StateHasChanged();
        }
        private void DisplayErrorInvoke(R_Exception poException)
        {
            R_DisplayException(poException);
        }
        public async Task ShowSuccessInvoke()
        {
            var loValidate = await R_MessageBox.Show("", "Upload Data Successfully", R_eMessageBoxButtonType.OK);
            if (loValidate == R_eMessageBoxResult.OK)
            {
                await Close(true, true);
            }
        }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                PMM10000DbParameterDTO _parameter = R_FrontUtility.ConvertObjectToObject<PMM10000DbParameterDTO>(poParameter);

                _viewModel._parameterUpload = _parameter;

                _viewModel.CompanyID = _ClientHelper.CompanyId;
                _viewModel.UserId = _ClientHelper.UserId;

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
                //import excel from user
                var loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                var fileByte = loMS.ToArray();
                //READ EXCEL
                var loExcel = ExcelInject;
                var loDataSet = loExcel.R_ReadFromExcel(fileByte, new[] { "SLACallType" });

                var loResult = R_FrontUtility.R_ConvertTo<CallTypeUploadExcelDTO>(loDataSet.Tables[0]);

                FileHasData = loResult.Count > 0 ? true : false;

                await _gridRef.R_RefreshGrid(loResult);
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

                List<CallTypeUploadExcelDTO> loData = (List<CallTypeUploadExcelDTO>)eventArgs.Parameter;

                await _viewModel.ConvertGrid(loData);
                eventArgs.ListEntityResult = _viewModel.CallTypeGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private void R_RowRender(R_GridRowRenderEventArgs eventArgs)
        {
            var loData = (CallTypeErrorDTO)eventArgs.Data;

            if (loData.ErrorFlag == "N")
            {
                eventArgs.RowClass = "errorDataLValidIsFalse";//"errorDataLValidisFalse";
            }
        }

        #region ButtonProcess
        public async Task Button_OnClickOkAsync()
        {
            var loEx = new R_Exception();

            try
            {
                var loValidate = await R_MessageBox.Show("", "Are you sure want to import data?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await _viewModel.SaveFileBulk();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task Button_OnClickCloseAsync()
        {
            await Close(true, false);
        }
        public async Task Button_OnClickSaveExcelAsync()
        {
            var loValidate = await R_MessageBox.Show("", "Are you sure want to save to excel?", R_eMessageBoxButtonType.YesNo);

            if (loValidate == R_eMessageBoxResult.Yes)
            {
                await ActionFuncDataSetExcel();
            }
        }
        private async Task ActionFuncDataSetExcel()
        {
            byte[] loByte = ExcelInject.R_WriteToExcel(_viewModel.ExcelDataSetError);
            var lcName = $"SLA Master Error" + ".xlsx";

            await JSRuntime.downloadFileFromStreamHandler(lcName, loByte);
        }
        #endregion

    }
}
