using BlazorClientHelper;
using Lookup_PMCOMMON.DTOs.PopUpSaveAs;
using Lookup_PMFRONT;
using Microsoft.AspNetCore.Components;
using PMB04000COMMON.DTO.DTOs;
using PMB04000COMMON.DTO.Utilities;
using PMB04000COMMONPrintBatch.ParamDTO;
using PMB04000FrontResources;
using PMB04000MODEL.ViewModel;
using R_APICommonDTO;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB04000FRONT
{
    public partial class PMB04000Receipt : R_Page, R_ITabPage
    {
        readonly PMB04000ReceiptViewModel _viewModelReceipt = new();
        readonly PMB04000ViewModel _viewModelInvoice = new();
        private R_ConductorGrid? _conductorRef;
        private R_Grid<PMB04000DTO>? _grid;
        [Inject] IClientHelper? _clientHelper { get; set; }
        [Inject] private R_IReport? _reportService { get; set; }
        private string _lcPeriod = null;

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
            var loValidate = await R_MessageBox.Show("", "Data Successfully Distribute", R_eMessageBoxButtonType.OK);
            if (loValidate == R_eMessageBoxResult.OK)
            {
                await this.Close(true, true);
            }
            await _grid!.R_RefreshGrid(null);
        }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                var TempParam = R_FrontUtility.ConvertObjectToObject<PMB04000ParamDTO>(poParameter);
                _viewModelReceipt.oParameterFromInvoice = TempParam;

                _viewModelReceipt.oPeriodYearRange.IMAX_YEAR = TempParam.IMAX_YEAR;
                _viewModelReceipt.oPeriodYearRange.IMIN_YEAR = TempParam.IMIN_YEAR;

                var oMonthList = _viewModelInvoice.GetMonth();
                _viewModelReceipt.GetMonthList = oMonthList;
                await _viewModelReceipt.GetTemplateList();

                _viewModelInvoice.StateChangeAction = StateChangeInvoke;
                _viewModelInvoice.DisplayErrorAction = DisplayErrorInvoke;
                _viewModelInvoice.ShowSuccessAction = async () =>
                {
                    await ShowSuccessInvoke();
                };
                await Task.CompletedTask;

                await _grid!.R_RefreshGrid(null)!;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void OnChangeAllPeriod()
        {
            var loException = new R_Exception();
            try
            {
                _viewModelReceipt.loOfficialReceipt.Clear();
                if (_viewModelReceipt.oParameterReceipt.LALL_PERIOD)
                {
                    _viewModelReceipt.oParameterReceipt.IPERIOD_YEAR = 0;
                    _viewModelReceipt.oParameterReceipt.CPERIOD_MONTH = "";
                }
                else
                {
                    _viewModelReceipt.oParameterReceipt.IPERIOD_YEAR = DateTime.Now.Year;
                    _viewModelReceipt.oParameterReceipt.CPERIOD_MONTH = DateTime.Now.Month.ToString("D2");
                }
                //_lcPeriod = (_viewModel.oParameterReceiptInfoTAB2.IPERIOD_YEAR.ToString()) + _viewModel.oParameterReceiptInfoTAB2.CPERIOD_MONTH;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

        }
        private void OnChangeTemplateList(object? poParam)
        {
            var loEx = new R_Exception();
            string lsValueTemplate = (string)poParam!;
            try
            {
                _viewModelInvoice.pcValueTemplate = lsValueTemplate!;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region Master Tab
        private async Task R_ServiceGetList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                _lcPeriod = (_viewModelReceipt.oParameterReceipt.IPERIOD_YEAR.ToString()) + _viewModelReceipt.oParameterReceipt.CPERIOD_MONTH;

                await _viewModelReceipt.GetOfficialReceiptList(paramPeriod: _lcPeriod);
                if (!_viewModelReceipt.loOfficialReceipt.Any())
                {
                    _viewModelReceipt.loOfficialReceipt.Clear();
                }
                eventArgs.ListEntityResult = _viewModelReceipt.loOfficialReceipt;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void R_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            //PMT01700OtherUnitList_OtherUnitListDTO loData = (PMT01700OtherUnitList_OtherUnitListDTO)eventArgs.Data;

            //try
            //{
            //    //ON DEVELOPEMENT ON RND

            //    if (!string.IsNullOrEmpty(loData.CBUILDING_ID))
            //    {

            //        _viewModel.oProperty_oDataOtherUnit.CBUILDING_ID = loData.CBUILDING_ID;
            //        _viewModel.oProperty_oDataOtherUnit.CBUILDING_NAME = loData.CBUILDING_NAME;
            //        _viewModel.oProperty_oDataOtherUnit.COTHER_UNIT_ID = loData.COTHER_UNIT_ID;
            //        //await _gridRefPMT01100Unit.R_RefreshGrid(null);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    loEx.Add(ex);
            //}

            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        #region Button
        private async Task BtnCancel()
        {
            var loEx = new R_Exception();
            try
            {
                _viewModelInvoice.pcTYPE_PROCESS = "CANCEL_RECEIPT";
                await _grid.R_SaveBatch();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task BtnPrint()
        {
            var loEx = new R_Exception();
            try
            {
                _viewModelInvoice.pcTYPE_PROCESS = "PRINT";
                await _grid.R_SaveBatch();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task BtnDistribute()
        {
            var loEx = new R_Exception();
            try
            {
                _viewModelInvoice.pcTYPE_PROCESS = "DISTRIBUTE";
                await _grid.R_SaveBatch();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        public async Task RefreshTabPageAsync(object poParam)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loTempParam = R_FrontUtility.ConvertObjectToObject<PMB04000ParamDTO>(poParam);
                _viewModelReceipt.ValidationTabReceipt();
                _viewModelReceipt.oParameterFromInvoice = loTempParam;
                await _viewModelReceipt.GetTemplateList();
                await _grid!.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            R_DisplayException(loException);
        }
        #endregion
        #region Save Batch
        private async Task ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loList = (List<PMB04000DTO>)eventArgs.Data;

                List<PMB04000DTO> poDataSelected = _viewModelInvoice.ValidationProcessData(loList);

                _viewModelInvoice._mergeRefNoParamater = string.Join(",", poDataSelected.Select(item => item.CREF_NO));

                if (_viewModelInvoice.pcTYPE_PROCESS == "CANCEL_RECEIPT")
                {
                    if (await R_MessageBox.Show("Confirmation",
                        $"Are you sure want to cancel receipt selected Data?",
                         R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.Yes)
                    {
                        var loParam = new PMB04000ParamDTO
                        {
                            CCOMPANY_ID = _clientHelper!.CompanyId,
                            CUSER_ID = _clientHelper!.UserId,
                            CTYPE_PROCESS = _viewModelInvoice.pcTYPE_PROCESS
                        };
                        await _viewModelInvoice.CreateCancelReceipt(poParam: loParam, poListData: poDataSelected);
                        //CLEAR OLD DATA
                        _grid!.DataSource.Clear();
                        //_viewModel.BankInChequeInfo = new();
                    }
                }
                else if (_viewModelInvoice.pcTYPE_PROCESS == "PRINT")
                {
                    if (poDataSelected.Any(data => data.IPRINT > 0) && _viewModelInvoice.oParameterPrintReceipt.LPRINT_ONE_TIME)
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMB0400_Class), "ValidationPrintOneTimePrint");
                        loEx.Add(loErr);
                        goto EndBlock;
                    }

                    var loParam = new PMB04000ParamReportDTO
                    {
                        CCOMPANY_ID = _clientHelper!.CompanyId,
                        CPROPERTY_ID = _viewModelReceipt.oParameterFromInvoice.CPROPERTY_ID,
                        CDEPT_CODE = _viewModelReceipt.oParameterFromInvoice.CDEPT_CODE,
                        CREF_NO = _viewModelInvoice._mergeRefNoParamater,
                        CUSER_ID = _clientHelper!.UserId,
                        CLANG_ID = _clientHelper.ReportCulture,
                        LPRINT = true,
                        CTEMPLATE_ID = _viewModelInvoice.pcValueTemplate,
                        LIS_PRINT = true,
                        CREPORT_FILENAME = "",
                        CREPORT_FILETYPE = "",
                    };
                    
                    var storageIds = new[] { nameof(TemplateDTO.CSTORAGE_ID01), nameof(TemplateDTO.CSTORAGE_ID02), nameof(TemplateDTO.CSTORAGE_ID03), nameof(TemplateDTO.CSTORAGE_ID04), nameof(TemplateDTO.CSTORAGE_ID05), nameof(TemplateDTO.CSTORAGE_ID06) };
                    var signNames = new[] { nameof(TemplateDTO.CSIGN_NAME01), nameof(TemplateDTO.CSIGN_NAME02), nameof(TemplateDTO.CSIGN_NAME03), nameof(TemplateDTO.CSIGN_NAME04), nameof(TemplateDTO.CSIGN_NAME05), nameof(TemplateDTO.CSIGN_NAME06) };
                    var signPositions = new[] { nameof(TemplateDTO.CSIGN_POSITION01), nameof(TemplateDTO.CSIGN_POSITION02), nameof(TemplateDTO.CSIGN_POSITION03), nameof(TemplateDTO.CSIGN_POSITION04), nameof(TemplateDTO.CSIGN_POSITION05), nameof(TemplateDTO.CSIGN_POSITION06) };

                    for (var i = 0; i < storageIds.Length; i++)
                    {
                        loParam.GetType().GetProperty(storageIds[i])!.SetValue(loParam, _viewModelReceipt.loTemplateList.Where(x => x.CTEMPLATE_ID == _viewModelInvoice.pcValueTemplate).Select(x => x.GetType().GetProperty(storageIds[i])!.GetValue(x)).FirstOrDefault());
                        loParam.GetType().GetProperty(signNames[i])!.SetValue(loParam, _viewModelReceipt.loTemplateList.Where(x => x.CTEMPLATE_ID == _viewModelInvoice.pcValueTemplate).Select(x => x.GetType().GetProperty(signNames[i])!.GetValue(x)).FirstOrDefault());
                        loParam.GetType().GetProperty(signPositions[i])!.SetValue(loParam, _viewModelReceipt.loTemplateList.Where(x => x.CTEMPLATE_ID == _viewModelInvoice.pcValueTemplate).Select(x => x.GetType().GetProperty(signPositions[i])!.GetValue(x)).FirstOrDefault());
                    }
                    
                    var abc = loParam;
                    await _reportService!.GetReport(
                                "R_DefaultServiceUrlPM",
                                "PM",
                                "rpt/PMB04000PrintReportInvoice/PMB04000ReportPost",
                                "rpt/PMB04000PrintReportInvoice/PMB04000ReportGet",
                                loParam);
                }
                else if (_viewModelInvoice.pcTYPE_PROCESS == "DISTRIBUTE")
                {
                    if (poDataSelected.Any(data => data.IDISTRIBUTE > 0) && _viewModelInvoice.oParameterPrintReceipt.LPRINT_ONE_TIME)
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMB0400_Class), "ValidationPrintOneTimeDistribute");
                        loEx.Add(loErr);
                        goto EndBlock;
                    }
                    var loParam = new PMB04000ParamReportDTO
                    {
                        CCOMPANY_ID = _clientHelper!.CompanyId,
                        CPROPERTY_ID = _viewModelReceipt.oParameterFromInvoice.CPROPERTY_ID,
                        CDEPT_CODE = _viewModelReceipt.oParameterFromInvoice.CDEPT_CODE,
                        CREF_NO = _viewModelInvoice._mergeRefNoParamater,
                        CUSER_ID = _clientHelper!.UserId,
                        CLANG_ID = _clientHelper.ReportCulture,
                        LPRINT = true
                    };
                    
                    var storageIds = new[] { nameof(TemplateDTO.CSTORAGE_ID01), nameof(TemplateDTO.CSTORAGE_ID02), nameof(TemplateDTO.CSTORAGE_ID03), nameof(TemplateDTO.CSTORAGE_ID04), nameof(TemplateDTO.CSTORAGE_ID05), nameof(TemplateDTO.CSTORAGE_ID06) };
                    var signNames = new[] { nameof(TemplateDTO.CSIGN_NAME01), nameof(TemplateDTO.CSIGN_NAME02), nameof(TemplateDTO.CSIGN_NAME03), nameof(TemplateDTO.CSIGN_NAME04), nameof(TemplateDTO.CSIGN_NAME05), nameof(TemplateDTO.CSIGN_NAME06) };
                    var signPositions = new[] { nameof(TemplateDTO.CSIGN_POSITION01), nameof(TemplateDTO.CSIGN_POSITION02), nameof(TemplateDTO.CSIGN_POSITION03), nameof(TemplateDTO.CSIGN_POSITION04), nameof(TemplateDTO.CSIGN_POSITION05), nameof(TemplateDTO.CSIGN_POSITION06) };

                    for (var i = 0; i < storageIds.Length; i++)
                    {
                        loParam.GetType().GetProperty(storageIds[i])!.SetValue(loParam, _viewModelReceipt.loTemplateList.Where(x => x.CTEMPLATE_ID == _viewModelInvoice.pcValueTemplate).Select(x => x.GetType().GetProperty(storageIds[i])!.GetValue(x)).FirstOrDefault());
                        loParam.GetType().GetProperty(signNames[i])!.SetValue(loParam, _viewModelReceipt.loTemplateList.Where(x => x.CTEMPLATE_ID == _viewModelInvoice.pcValueTemplate).Select(x => x.GetType().GetProperty(signNames[i])!.GetValue(x)).FirstOrDefault());
                        loParam.GetType().GetProperty(signPositions[i])!.SetValue(loParam, _viewModelReceipt.loTemplateList.Where(x => x.CTEMPLATE_ID == _viewModelInvoice.pcValueTemplate).Select(x => x.GetType().GetProperty(signPositions[i])!.GetValue(x)).FirstOrDefault());
                    }

                    await _viewModelInvoice.DistributeProcess(poParam: loParam);

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        private async Task AfterSaveBatch(R_AfterSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                //GET LIST DATA
                await _grid!.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        #region SaveAs
        private async Task BeforeOpen_PopupSaveAsAsync(R_BeforeOpenPopupEventArgs eventArgs)
        {
            //InitializePrintParam();
            _viewModelInvoice.pcTYPE_PROCESS = "SAVE_AS";
            await _grid.R_SaveBatch();

            if (_viewModelInvoice._lDataselectedExist)
            {
                eventArgs.PageTitle = _localizer["_titleSaveAs"];
                eventArgs.TargetPageType = typeof(PopUpSaveAs);
            }

        }
        private async Task AfterOpen_PopupSaveAs(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Success)
                {
                    var loReturn = R_FrontUtility.ConvertObjectToObject<SaveAsDTO>(eventArgs.Result);
                    string RefnoMergerd = _viewModelInvoice._mergeRefNoParamater;
                    PMB04000ParamReportDTO loParam = new PMB04000ParamReportDTO
                    {
                        CCOMPANY_ID = _clientHelper!.CompanyId,
                        CPROPERTY_ID = _viewModelReceipt.oParameterFromInvoice.CPROPERTY_ID,
                        CDEPT_CODE = _viewModelReceipt.oParameterFromInvoice.CDEPT_CODE,
                        CREF_NO = RefnoMergerd,
                        CUSER_ID = _clientHelper!.UserId,
                        CLANG_ID = _clientHelper.ReportCulture,
                        LPRINT = true,
                        CTEMPLATE_ID = _viewModelInvoice.pcValueTemplate,
                        LIS_PRINT = false,
                        CREPORT_FILENAME = loReturn.CREPORT_FILENAME,
                        CREPORT_FILETYPE = loReturn.CREPORT_FILETYPE,
                    };
                    await _reportService!.GetReport(
                                "R_DefaultServiceUrlPM",
                                "PM",
                                "rpt/PMB04000Report/ReceiptReportPost",
                                "rpt/PMB04000Report/ReceiptReportGet",
                                loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }
        #endregion
        #region implement library for checkbox select
        private void R_CheckBoxSelectValueChanged(R_CheckBoxSelectValueChangedEventArgs eventArgs)
        {
        }
        #endregion

    }
}
