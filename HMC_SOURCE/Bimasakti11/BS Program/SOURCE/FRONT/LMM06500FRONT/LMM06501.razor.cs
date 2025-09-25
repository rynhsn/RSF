using BlazorClientHelper;
using LMM06500COMMON;
using LMM06500MODEL;
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
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System.Data;

namespace LMM06500FRONT
{
    public partial class LMM06501 : R_Page
    {
        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Excel };

        private LMM06501ViewModel _viewModel = new LMM06501ViewModel();
        private R_Grid<LMM06501ErrorValidateDTO> _StaffMoveDetail_gridRef;

        private bool FileHasData = false;

        [Inject] IClientHelper clientHelper { get; set; }
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
            var loByte = ExcelInject.R_WriteToExcel(_viewModel.ExcelDataSet);
            var lcName = $"Staff {_viewModel.PropertyValue}" + ".xlsx";

            await JSRuntime.downloadFileFromStreamHandler(lcName, loByte);
        }

        // Create Method Action For Error Unhandle
        private void ShowErrorInvoke(R_APIException poEx)
        {
            var loEx = new R_Exception(poEx.ErrorList.Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.ErrNo, x.ErrDescp)).ToList());
            this.R_DisplayException(loEx);
        }

        protected override Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var Param = (LMM06500DTOInitial)poParameter;
                _viewModel.PropertyValue = Param.CPROPERTY_ID;
                _viewModel.PropertyName = Param.CPROPERTY_NAME;

                //Assign Action
                _viewModel.StateChangeAction = StateChangeInvoke;
                _viewModel.ShowErrorAction = ShowErrorInvoke;
                _viewModel.ActionDataSetExcel = ActionFuncDataSetExcel;

                //Asign Company and User id
                _viewModel.CompanyID = clientHelper.CompanyId;
                _viewModel.UserId = clientHelper.UserId;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

            return Task.CompletedTask;
        }

        private async Task _Staff_SourceUpload_OnChange(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.SourceFileName = eventArgs.File.Name;
                //import from excel
                var loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                var loFileByte = loMS.ToArray();

                //add filebyte to DTO
                var loExcel = ExcelInject;

                var loDataSet = loExcel.R_ReadFromExcel(loFileByte, new string[] { "Staff"});

                var loResult = R_FrontUtility.R_ConvertTo<LMM06501DTO>(loDataSet.Tables[0]);

                FileHasData = loResult.Count > 0 ? true : false;

                await _StaffMoveDetail_gridRef.R_RefreshGrid(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task _Staff_Upload_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (List<LMM06501DTO>)eventArgs.Parameter;

                await _viewModel.ConvertGrid(loData);

                eventArgs.ListEntityResult = _viewModel.StaffValidateUploadError;
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
                    await _viewModel.SaveBulkFile();

                    if (_viewModel.VisibleError)
                    {
                        await R_MessageBox.Show("", "Staff uploaded successfully!", R_eMessageBoxButtonType.OK);
                    }
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
            await this.Close(true, false);
        }

        public async Task Button_OnClickSaveExcelAsync()
        {
            var loValidate = await R_MessageBox.Show("", "Are you sure want to save to excel again?", R_eMessageBoxButtonType.YesNo);

            if (loValidate == R_eMessageBoxResult.Yes)
            {
                await ActionFuncDataSetExcel();
            }
        }
    }
}
