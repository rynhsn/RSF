using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Helpers;
using PMT01300COMMON;
using PMT01300MODEL;
using R_BlazorFrontEnd;
using Microsoft.JSInterop;
using R_APICommonDTO;
using Microsoft.AspNetCore.Components.Forms;
using R_BlazorFrontEnd.Controls.Events;
using System.Data;
using Microsoft.VisualBasic;
using R_BlazorFrontEnd.Controls.DataControls;

namespace PMT01300FRONT
{
    public partial class PMT01301 : R_Page
    {
        private PMT01301ViewModel _viewModel = new PMT01301ViewModel();
        #region Inject
        [Inject] private R_ILocalizer<PMT01300FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] public R_IExcel ExcelInject { get; set; }
        [Inject] private IClientHelper _ClientHelper { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }
        #endregion

        #region Grid
        private R_Grid<PMT01300UploadAgreementExcelDTO>? _gridUploadAgreementRef;
        private R_Grid<PMT01300UploadUnitExcelDTO>? _gridUploadUnitRef;
        private R_Grid<PMT01300UploadUtilityExcelDTO>? _gridUploadUtilityRef;
        private R_Grid<PMT01300UploadChargesExcelDTO>? _gridUploadChargesRef;
        private R_Grid<PMT01300UploadDepositExcelDTO>? _gridUploadDepositRef;
        #endregion

        #region Private Property 
        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Excel };
        private R_TabStrip? _tabStripRef;
        private bool FileHasData = false;
        private R_ConductorGrid _conductorGridAgreementRef;
        private R_ConductorGrid _conductorGridUnitRef;
        private R_ConductorGrid _conductorGridUtilityRef;
        private R_ConductorGrid _conductorGridChargesRef;
        private R_ConductorGrid _conductorGridDepositRef;
        #endregion

        #region Bacth Method Process
        // Create Method Action StateHasChange
        private void StateChangeInvoke()
        {
            StateHasChanged();
        }

        // Create Method Action For Download Excel if Has Error
        private async Task ActionFuncDataSetExcel()
        {
            var loByte = ExcelInject.R_WriteToExcel(_viewModel.ExcelDataSet);
            var lcName = "LeaseManager_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xlsx";

            await JSRuntime.downloadFileFromStreamHandler(lcName, loByte);
        }

        // Create Method Action if proses is Complete Success
        private async Task ActionFuncIsCompleteSuccess()
        {
            await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(PMT01300FrontResources.Resources_Dummy_Class), "_NotifSuccesUpload"), R_eMessageBoxButtonType.OK);
            await this.Close(true, true);
        }

        // Create Method Action For Error Unhandle
        private void ShowErrorInvoke(R_APIException poEx)
        {
            // var loEx = new R_Exception(poEx.ErrorList.Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.ErrNo, x.ErrDescp)).ToList());
            var loEx = new R_Exception(_viewModel.ErrorList);
            this.R_DisplayException(loEx);
        }
        #endregion
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel.Property = (PMT01300PropertyDTO)poParameter;

                // Set Param Company to viewmodel
                _viewModel.CompanyID = _ClientHelper.CompanyId;
                _viewModel.UserId = _ClientHelper.UserId;

                //Assign Action
                _viewModel.StateChangeAction = StateChangeInvoke;
                _viewModel.ShowErrorAction = ShowErrorInvoke;
                _viewModel.ActionDataSetExcel = ActionFuncDataSetExcel;
                _viewModel.ActionIsCompleteSuccess = ActionFuncIsCompleteSuccess;
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
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
        private async Task Grid_R_ServiceGetChargesListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            eventArgs.ListEntityResult = _viewModel.ChargesGrid;
            await Task.CompletedTask;
        }
        private async Task Grid_R_ServiceGetDepositListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            eventArgs.ListEntityResult = _viewModel.DepositGrid;
            await Task.CompletedTask;
        }
        private void ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }
        private void Agreement_RowRender(R_GridRowRenderEventArgs eventArgs)
        {
            var loData = (PMT01300UploadAgreementExcelDTO)eventArgs.Data;

            if (loData.Valid == "N")
            {
                eventArgs.RowClass = "errorDataLValidIsFalse";//"errorDataLValidisFalse";
            }
        }
        private void Unit_RowRender(R_GridRowRenderEventArgs eventArgs)
        {
            var loData = (PMT01300UploadUnitExcelDTO)eventArgs.Data;

            if (loData.Valid == "N")
            {
                eventArgs.RowClass = "errorDataLValidIsFalse";//"errorDataLValidisFalse";
            }
        }
        private void Utility_RowRender(R_GridRowRenderEventArgs eventArgs)
        {
            var loData = (PMT01300UploadUtilityExcelDTO)eventArgs.Data;

            if (loData.Valid == "N")
            {
                eventArgs.RowClass = "errorDataLValidIsFalse";//"errorDataLValidisFalse";
            }
        }
        private void Charges_RowRender(R_GridRowRenderEventArgs eventArgs)
        {
            var loData = (PMT01300UploadChargesExcelDTO)eventArgs.Data;

            if (loData.Valid == "N")
            {
                eventArgs.RowClass = "errorDataLValidIsFalse";//"errorDataLValidisFalse";
            }
        }
        private void Deposit_RowRender(R_GridRowRenderEventArgs eventArgs)
        {
            var loData = (PMT01300UploadDepositExcelDTO)eventArgs.Data;

            if (loData.Valid == "N")
            {
                eventArgs.RowClass = "errorDataLValidIsFalse";//"errorDataLValidisFalse";
            }
        }
        #endregion

        #region Process Input File
        private async Task OnChangeInputFile(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            R_Exception loTempException = new R_Exception();
            DataSet loDataSet = null;
            List<PMT01300UploadAgreementExcelDTO> loAgreementResult = null;
            List<PMT01300UploadUnitExcelDTO> loUnitResult = null;
            List<PMT01300UploadChargesExcelDTO> loChargesResult = null;
            List<PMT01300UploadUtilityExcelDTO> loUtilityResult = null;
            List<PMT01300UploadDepositExcelDTO> loDepositResult = null;

            try
            {
                _viewModel.SumDataAgreementExcel = 0;
                _viewModel.SumDataUnitExcel = 0;
                _viewModel.SumDataUtilityExcel = 0;
                _viewModel.SumDataChargesExcel = 0;
                _viewModel.SumDataDepositExcel = 0;
                _viewModel.SumValidDataAgreementExcel = 0;
                _viewModel.SumValidDataUnitExcel = 0;
                _viewModel.SumValidDataUtilityExcel = 0;
                _viewModel.SumValidDataChargesExcel = 0;
                _viewModel.SumValidDataDepositExcel = 0;
                _viewModel.SumInvalidDataAgreementExcel = 0;
                _viewModel.SumInvalidDataUnitExcel = 0;
                _viewModel.SumInvalidDataUtilityExcel = 0;
                _viewModel.SumInvalidDataChargesExcel = 0;
                _viewModel.SumInvalidDataDepositExcel = 0;
                _viewModel.VisibleError = false;

                if (eventArgs.File.Name.Contains(".xlsx") == false)
                {
                    await R_MessageBox.Show("", _localizer["N06"], R_eMessageBoxButtonType.OK);
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
                    loAgreementResult = new List<PMT01300UploadAgreementExcelDTO>();
                    FileHasData = false;
                }
                else
                {
                    loAgreementResult = R_FrontUtility.R_ConvertTo<PMT01300UploadAgreementExcelDTO>(loDataSet.Tables[0]).ToList();
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
                    loUnitResult = new List<PMT01300UploadUnitExcelDTO>();
                }
                else
                {
                    loUnitResult = R_FrontUtility.R_ConvertTo<PMT01300UploadUnitExcelDTO>(loDataSet.Tables[0]).ToList();
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
                    loUtilityResult = new List<PMT01300UploadUtilityExcelDTO>();
                }
                else
                {
                    loUtilityResult = R_FrontUtility.R_ConvertTo<PMT01300UploadUtilityExcelDTO>(loDataSet.Tables[0]).ToList();
                }
                await _viewModel.GetUtilityList(loUtilityResult, loUnitResult.Count + loAgreementResult.Count);
                #endregion

                #region Charges
                try
                {
                    loDataSet = ExcelInject.R_ReadFromExcel(lofileByte, new string[] { "Charges" });
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
                    loChargesResult = new List<PMT01300UploadChargesExcelDTO>();
                }
                else
                {
                    loChargesResult = R_FrontUtility.R_ConvertTo<PMT01300UploadChargesExcelDTO>(loDataSet.Tables[0]).ToList();
                }
                await _viewModel.GetChargesList(loChargesResult, loUtilityResult.Count + loUnitResult.Count + loAgreementResult.Count);
                #endregion

                #region Deposit
                try
                {
                    loDataSet = ExcelInject.R_ReadFromExcel(lofileByte, new string[] { "Deposit" });
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
                    loDepositResult = new List<PMT01300UploadDepositExcelDTO>();
                }
                else
                {
                    loDepositResult = R_FrontUtility.R_ConvertTo<PMT01300UploadDepositExcelDTO>(loDataSet.Tables[0]).ToList();
                }
                await _viewModel.GetDepositList(loDepositResult, loChargesResult.Count + loUtilityResult.Count + loUnitResult.Count + loAgreementResult.Count);
                #endregion

                await _gridUploadAgreementRef.R_RefreshGrid(null);
                await _gridUploadUnitRef.R_RefreshGrid(null);
                await _gridUploadUtilityRef.R_RefreshGrid(null);
                await _gridUploadChargesRef.R_RefreshGrid(null);
                await _gridUploadDepositRef.R_RefreshGrid(null);
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
            await this.Close(true, false);
        }
        public async Task Button_OnClickSaveExcelAsync()
        {
            //var loValidate = await R_MessageBox.Show("", "Are you sure want to save to excel?", R_eMessageBoxButtonType.YesNo);

            //if (loValidate == R_eMessageBoxResult.Yes)
            //{
            //    await ActionFuncDataSetExcel();
            //}
            await ActionFuncDataSetExcel();
        }
        #endregion
    }
}
