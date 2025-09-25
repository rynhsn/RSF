using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using BlazorClientHelper;
using GSM04500Common;
using GSM04500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Excel;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GSM04500Front
{
    public partial class GSM04500Upload : R_Page
    {
        private GSM04500ViewModel_Upload _viewModel = new();
        private R_Grid<GSM04500UploadErrorValidateDTO> JournalGroup_gridRef;

        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Excel };

        [Inject] R_IExcel ExcelInject { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }

        private bool FileHasData = false;

        private void StateChangeInvoke()
        {
            StateHasChanged();
        }
        private void DisplayErrorInvoke(R_APIException poException)
        {
            var loEx = R_FrontUtility.R_ConvertFromAPIException(poException);
            this.R_DisplayException(loEx);
        }
        public async Task ShowSuccessInvoke()
        {
            var loValidate = await R_MessageBox.Show("", "Upload Data Successfully", R_eMessageBoxButtonType.OK);
            if (loValidate == R_eMessageBoxResult.OK)
            {
                await this.Close(true, true);
            }
        }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var Param = (GSM004500ParamDTO)poParameter;

                //Assign Company, User and others value for Parameters
                _viewModel.CompanyId = Param.CCOMPANY_ID;
                _viewModel.UserId = Param.CUSER_ID;
                _viewModel.PropertyValue = Param.CPROPERTY_ID;
                _viewModel.PropertyName = Param.CPROPERTY_NAME;
                _viewModel.JournalGroupTypeValue = Param.CJRNGRP_TYPE;
                //CAll Ethod VisibleColumnAccrual
                _viewModel.ColumnAccrual_Visible();

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
                var loDataSet = loExcel.R_ReadFromExcel(fileByte, new[] { "JournalGroup" });

                var loResult = R_FrontUtility.R_ConvertTo<GSM04500UploadFromExcelDTO>(loDataSet.Tables[0]);

                FileHasData = loResult.Count > 0 ? true : false;

                await JournalGroup_gridRef.R_RefreshGrid(loResult);
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

                List<GSM04500UploadFromExcelDTO> loData = (List<GSM04500UploadFromExcelDTO>)eventArgs.Parameter;

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
                    await _viewModel.SaveFileBulk();
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

            R_DisplayException(loEx);
        }

        // Create Method Action For Download Excel if Has Error
        private async Task ActionFuncDataSetExcel()
        {
            var loByte = ExcelInject.R_WriteToExcel(_viewModel.ExcelDataSet);
            var lcName = $"Journal Group {_viewModel.PropertyName}" + ".xlsx";

            await JSRuntime.downloadFileFromStreamHandler(lcName, loByte);
        }

        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, false);
        }

    }
}
