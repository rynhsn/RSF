using BlazorClientHelper;
using GLM00200COMMON.DTO_s;
using GLM00200MODEL.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Controls.Events;
using Microsoft.AspNetCore.Components.Forms;
using R_APICommonDTO;
using System.Collections.ObjectModel;
using R_BlazorFrontEnd.Controls.Enums;

namespace GLM00200FRONT
{
    public partial class GLM00202 : R_Page //upload
    {
        //var
        private GLM00202ViewModel _viewModel = new();
        private R_Grid<RecurringUploadErrorDTO> _gridRecurringUpload;
        private R_ConductorGrid _conRecurringUpload;
        [Inject] private IClientHelper _clientHelper { get; set; }
        [Inject] private R_IExcel _excelProvider { get; set; }
        [Inject] private IJSRuntime JS { get; set; }
        private int _pageSize_RecurringUpload = 10;
        private R_Button _btnProcess, _btnSaveToExcel;
        private bool _isFileHasData { get; set; }
        private R_eFileSelectAccept[] _accepts = { R_eFileSelectAccept.Excel };

        //method - invoke
        private void StateChangeInvoke()
        {
            StateHasChanged();
        }
        private void DisplayErrorInvoke(R_APIException poEx)
        {
            var loEx = R_FrontUtility.R_ConvertFromAPIException(poEx);
            R_DisplayException(loEx);
        }
        private async Task ShowSuccessInvoke()
        {
            R_Exception loEx = new();
            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_msg_batchComplete"], R_eMessageBoxButtonType.OK);
                if (loValidate == R_eMessageBoxResult.OK)
                {
                    await Close(true, null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void SetPercentageAndMessageInvoke(string pcMessage, int pnPercentage)
        {
            _viewModel._progressBarMessage = pcMessage;
            _viewModel._progressBarPercentage = pnPercentage;
        }

        //mathod - override
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new();
            try
            {
                _viewModel._stateChangeAction = StateChangeInvoke;
                _viewModel._showErrorAction = DisplayErrorInvoke;
                _viewModel._setPercentageAndMessageAction = SetPercentageAndMessageInvoke;
                _viewModel._showSuccessAction = async () =>
                {
                    await ShowSuccessInvoke();
                };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        //method - event
        private async Task RecurringUpload_GetlistAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = new List<RecurringUploadErrorDTO>(
                    R_FrontUtility.ConvertCollectionToCollection<RecurringUploadErrorDTO>((List<RecurringUploadDTO>)eventArgs.Parameter)
                    .Select((loItem, lnIndex) =>
                    {
                        loItem.SEQ_NO = lnIndex + 1; // Assign row number starting from 1
                        return loItem;
                    }));
                _viewModel.RecurringUploadErrors = new ObservableCollection<RecurringUploadErrorDTO>(loData);
                _viewModel._sumList_RecurringExcel = loData.Count;
                if (_viewModel._visibleError)
                {
                    await _btnProcess.FocusAsync();
                }
                else
                {
                    await _btnSaveToExcel.FocusAsync();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void RecurringUpload_RowRender(R_GridRowRenderEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<RecurringUploadErrorDTO>(eventArgs.Data);
                if (loData.Valid == "N")
                {
                    eventArgs.RowClass = "rowErrorData";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }

        //method - inputfile
        private async Task Onchange_InputFIleRecurringUpload(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                //import from excel
                _viewModel._sourceFileName = eventArgs.File.Name;
                var loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                var loFileByte = loMS.ToArray();

                //add filebyte to DTO
                var loExcel = _excelProvider;
                var loDataSet = loExcel.R_ReadFromExcel(loFileByte, new string[] { "Recurring" });
                var loResult = R_FrontUtility.R_ConvertTo<RecurringUploadDTO>(loDataSet.Tables[0]);
                _isFileHasData = loResult.Count > 0;
                await _gridRecurringUpload.R_RefreshGrid(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        //method - button
        private async Task OnClick_ButtonOK()
        {
            var loEx = new R_Exception();

            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_msg_confirmupload"], R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await _viewModel.Savebatch_RecurringAsync(_clientHelper.CompanyId, _clientHelper.UserId);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task OnClick_ButtonSaveExcelAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                List<RecurringUploadErrorDTO> loExcelList = new();
                var loDataTable = R_FrontUtility.R_ConvertTo(loExcelList);
                loDataTable.TableName = "RecurringUpload";

                //export to excel
                var loByteFile = _excelProvider.R_WriteToExcel(loDataTable);
                var saveFileName = $"Upload Error RecurringUpload.xlsx";
                var loValidate = await R_MessageBox.Show("", _localizer["_msg_confirmasavetoexcel"], R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await JS.downloadFileFromStreamHandler(saveFileName, loByteFile);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }
        private async Task OnClick_ButtonClose()
        {
            await this.Close(true, false);
        }
    }
}
