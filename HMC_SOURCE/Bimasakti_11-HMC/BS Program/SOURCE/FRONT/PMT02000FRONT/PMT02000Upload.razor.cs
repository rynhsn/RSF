using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using PMT02000COMMON.Upload;
using PMT02000COMMON.Upload.Agreement;
using PMT02000COMMON.Upload.Unit;
using PMT02000COMMON.Upload.Utility;
using PMT02000MODEL.ViewModel;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT02000FRONT
{
    public partial class PMT02000Upload : R_Page
    {
        private PMT02000ViewModel_Upload _viewModel = new();
        private R_Grid<AgreementUploadDTO>? _gridAgreementRef;
        [Inject] public R_IExcel? ExcelInject { get; set; }
        [Inject] private IClientHelper? _ClientHelper { get; set; }
        [Inject] IJSRuntime? JSRuntime { get; set; }
        private R_Grid<AgreementUploadExcelDTO>? _gridUploadAgreementRef;
        private R_Grid<UnitUploadExcelDTO>? _gridUploadUnitRef;
        private R_Grid<UtilityUploadExcelDTO>? _gridUploadUtilityRef;

        #region Private Property 
        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Excel };
        private R_TabStrip? _tabStripRef;
        private bool FileHasData = false;
        #endregion
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
                PMT02000ParameterUploadDTO _parameter = R_FrontUtility.ConvertObjectToObject<PMT02000ParameterUploadDTO>(poParameter);

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

        #region List
        private async Task Grid_R_ServiceGetAgreementListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            eventArgs.ListEntityResult = _viewModel.AgreementGrid;
            await Task.CompletedTask;
        }
        private async Task Grid_R_ServiceGetUnitListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            eventArgs.ListEntityResult = _viewModel.UnitGrid;
            await Task.CompletedTask;
        }
        private async Task Grid_R_ServiceGetUtilityListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            eventArgs.ListEntityResult = _viewModel.UtilityGrid;
            await Task.CompletedTask;
        }
        #endregion
        // Create Method Action For Download Excel if Has Error

        #region Process Input File
        private async Task OnChangeInputFile(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            R_Exception loTempException = new R_Exception();
            DataSet loDataSet;
            List<AgreementUploadExcelDTO> loAgreementResult = null;
            List<UnitUploadExcelDTO> loUnitResult = null;
            List<UtilityUploadExcelDTO> loUtilityResult = null;
            try
            {
                _viewModel.SumDataAgreementExcel = 0;
                _viewModel.SumDataUnitExcel = 0;
                _viewModel.SumDataUtilityExcel = 0;
                _viewModel.SumValidDataAgreementExcel = 0;
                _viewModel.SumValidDataUnitExcel = 0;
                _viewModel.SumValidDataUtilityExcel = 0;
                _viewModel.SumInvalidDataAgreementExcel = 0;
                _viewModel.SumInvalidDataUnitExcel = 0;
                _viewModel.SumInvalidDataUtilityExcel = 0;
                _viewModel._lError = false;

                if (eventArgs.File.Name.Contains(".xlsx") == false)
                {
                    await R_MessageBox.Show("", _localizer["ValidationFormat"], R_eMessageBoxButtonType.OK);
                    return;
                }

                var loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                var lofileByte = loMS.ToArray();

                #region Agreement
                try
                {
                    loDataSet = ExcelInject.R_ReadFromExcel(lofileByte, new string[] { "Agreement" });

                }
                catch (Exception ex)
                {
                    loDataSet = null;
                    loTempException.Add(ex);
                    if (loTempException.ErrorList.FirstOrDefault().ErrDescp == "SkipNumberOfRowsStart was out of range: 0")
                    {
                        loTempException = new R_Exception();
                    }
                    else
                    {
                        loEx.Add(ex);
                    }
                }
                if (loDataSet == null)
                {
                    loAgreementResult = new List<AgreementUploadExcelDTO>();
                    FileHasData = false;
                }
                else
                {
                    loAgreementResult = R_FrontUtility.R_ConvertTo<AgreementUploadExcelDTO>(loDataSet.Tables[0]).ToList();
                    FileHasData = loAgreementResult.Count > 0;
                }
                await _viewModel.GetAgreementList(loAgreementResult);

                #endregion

                #region Unit
                try
                {
                    loDataSet = ExcelInject.R_ReadFromExcel(lofileByte, new string[] { "Unit" });

                }
                catch (Exception ex)
                {
                    loDataSet = null;
                    loTempException.Add(ex);
                    if (loTempException.ErrorList.FirstOrDefault().ErrDescp == "SkipNumberOfRowsStart was out of range: 0")
                    {
                        loTempException = new R_Exception();
                    }
                    else
                    {
                        loEx.Add(ex);
                    }
                }
                if (loDataSet == null)
                {
                    loUnitResult = new List<UnitUploadExcelDTO>();
                    FileHasData = false;
                }
                else
                {
                    loUnitResult = R_FrontUtility.R_ConvertTo<UnitUploadExcelDTO>(loDataSet.Tables[0]).ToList();
                    FileHasData = loUnitResult.Count > 0;
                }
                await _viewModel.GetUnitList(loUnitResult, loAgreementResult.Count);
                #endregion

                #region Utility
                try
                {
                    loDataSet = ExcelInject.R_ReadFromExcel(lofileByte, new string[] { "Utility" });

                }
                catch (Exception ex)
                {
                    loDataSet = null;
                    loTempException.Add(ex);
                    if (loTempException.ErrorList.FirstOrDefault().ErrDescp == "SkipNumberOfRowsStart was out of range: 0")
                    {
                        loTempException = new R_Exception();
                    }
                    else
                    {
                        loEx.Add(ex);
                    }
                }
                if (loDataSet == null)
                {
                    loUtilityResult = new List<UtilityUploadExcelDTO>();
                    FileHasData = false;
                }
                else
                {
                    loUtilityResult = R_FrontUtility.R_ConvertTo<UtilityUploadExcelDTO>(loDataSet.Tables[0]).ToList();
                    FileHasData = loUtilityResult.Count > 0;
                }
                await _viewModel.GetUtilityList(loUtilityResult, loUnitResult.Count + loAgreementResult.Count);
                #endregion       

                await _gridUploadAgreementRef.R_RefreshGrid(null);
                await _gridUploadUnitRef.R_RefreshGrid(null);
                await _gridUploadUtilityRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            await R_DisplayExceptionAsync(loEx);
        }
        #endregion
        #region Btn Process
        public async Task Button_OnClickOkAsync()
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.CheckingDataWithEmptyDocNo();
                if (!string.IsNullOrWhiteSpace(_viewModel.lcFilterResult))
                {
                    _viewModel.lcFilterResult = _viewModel.lcFilterResult + _localizer["Q001"];
                    var loValidate = await R_MessageBox.Show("", _viewModel.lcFilterResult, R_eMessageBoxButtonType.YesNo);
                    if (loValidate == R_eMessageBoxResult.Yes)
                    {
                        await _viewModel.SaveBulkFile();
                    }
                    _viewModel.lcFilterResult = "";
                }
                else
                {
                    await _viewModel.SaveBulkFile();
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
            await ActionFuncDataSetExcel();
        }
        private async Task ActionFuncDataSetExcel()
        {
            byte[] loByte = ExcelInject.R_WriteToExcel(_viewModel.ExcelDataSetError);
            var lcName = $"HandOver Error" + ".xlsx";

            await JSRuntime.downloadFileFromStreamHandler(lcName, loByte);
        }
        #endregion

    }
}
