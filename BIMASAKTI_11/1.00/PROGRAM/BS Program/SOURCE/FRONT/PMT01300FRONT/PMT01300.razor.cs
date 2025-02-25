 using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using PMT01300MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using PMT01300COMMON;
using R_BlazorFrontEnd.Helpers;
using PMT01300FrontResources;
using Lookup_GSModel.ViewModel;
using Lookup_PMModel.ViewModel.LML00600;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.PML01200;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Enums;
using System;
using System.Diagnostics.Tracing;
using System.Xml.Linq;
using PMT01300ReportCommon;

namespace PMT01300FRONT
{
    public partial class PMT01300 : R_Page
    {
        [Inject] private R_ILocalizer<PMT01300FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }

        private PMT01300ViewModel _viewModel = new PMT01300ViewModel();
        private R_Grid<PMT01300DTO> _gridLOIListRef;
        private R_Grid<PMT01310DTO> _gridLOIUnitListRef;
        private R_Conductor _conRef;


        #region Private 
        private R_TabStrip _TabLOIList;
        private R_TabStripTab _TabLOIListStrip;
        private R_TabStripTab _TabLOIStrip;
        private R_TabStripTab _TabLOIUnitStrip;
        private R_TabStripTab _TabLOIInvoiceStrip;
        private R_TabStripTab _TabLOIDepositStrip;
        private R_TabPage _TabPageLOI;
        private R_TabPage _TabPageUnitAndCharges;
        private R_TabPage _TabPageDeposit;
        private R_TabPage _TabPageInvoicePlan;
        private bool EnableDraftSts = false;
        private bool EnableOpenSts = false;
        private bool EnableApproveSts = false;
        private PMT01300DTO _SelectDataFromTabLOI = new PMT01300DTO();
        private PMT01310DTO _SelectDataFromTabUNIT = new PMT01310DTO();
        private PMT01330DTO _SelectDataFromTabCharges = new PMT01330DTO();
        private bool IsAddData = false;
        private bool HasUnitData = false;
        private bool HasUnitFromUnitTab = false;
        #endregion
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.GetInitialVar();
                if (_viewModel.PropertyList.Count > 0)
                {
                    var loPropertyId = _viewModel.PropertyList.FirstOrDefault().CPROPERTY_ID;
                    await PropertyDropdown_ValueChanged(loPropertyId);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region Value Change
        private async Task PropertyDropdown_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();
            PMT01300DTO loData = null;
            try
            {
                _viewModel.PROPERTY_ID = string.IsNullOrWhiteSpace(poParam) ? "" : poParam;
                await _gridLOIListRef.R_RefreshGrid(null);

                if (_viewModel.LOIGrid.Count == 0)
                {
                    await R_MessageBox.Show("", _localizer["N01"], R_eMessageBoxButtonType.OK);
                    if (_gridLOIUnitListRef.DataSource.Count > 0)
                    {
                        _gridLOIUnitListRef.DataSource.Clear();
                    }
                }

                if (_conRef.R_ConductorMode == R_eConductorMode.Normal)
                {
                    await Task.CompletedTask;
                    var loPropertyData = _viewModel.PropertyList.FirstOrDefault(x => x.CPROPERTY_ID == poParam);
                    loData = _gridLOIListRef.DataSource.Count > 0 ? _gridLOIListRef.CurrentSelectedData : new PMT01300DTO() { CPROPERTY_ID = poParam, CCURRENCY_CODE = loPropertyData.CCURRENCY, CCURRENCY_NAME = loPropertyData.CCURRENCY_NAME };
                    if (_TabLOIList.ActiveTab.Id == "LOI")
                    {
                        await _TabPageLOI.InvokeRefreshTabPageAsync(loData);
                    }
                    else if (_TabLOIList.ActiveTab.Id == "UnitCharges")
                    {
                        loData.CUNIT_ID = _gridLOIUnitListRef.DataSource.Count > 0 ? _gridLOIUnitListRef.CurrentSelectedData.CUNIT_ID : "";
                        loData.CBUILDING_ID = _gridLOIUnitListRef.DataSource.Count > 0 ? _gridLOIUnitListRef.CurrentSelectedData.CBUILDING_ID : "";
                        loData.CFLOOR_ID = _gridLOIUnitListRef.DataSource.Count > 0 ? _gridLOIUnitListRef.CurrentSelectedData.CFLOOR_ID : "";

                        await _TabPageUnitAndCharges.InvokeRefreshTabPageAsync(loData);
                    }
                    else if (_TabLOIList.ActiveTab.Id == "Deposit")
                    {
                        loData.CUNIT_ID = _gridLOIUnitListRef.DataSource.Count > 0 ? _gridLOIUnitListRef.CurrentSelectedData.CUNIT_ID : "";
                        loData.CBUILDING_ID = _gridLOIUnitListRef.DataSource.Count > 0 ? _gridLOIUnitListRef.CurrentSelectedData.CBUILDING_ID : "";
                        loData.CFLOOR_ID = _gridLOIUnitListRef.DataSource.Count > 0 ? _gridLOIUnitListRef.CurrentSelectedData.CFLOOR_ID : "";

                        await _TabPageDeposit.InvokeRefreshTabPageAsync(loData);
                    }
                    else if (_TabLOIList.ActiveTab.Id == "InvoicePlan")
                    {
                        loData.CUNIT_ID = _gridLOIUnitListRef.DataSource.Count > 0 ? _gridLOIUnitListRef.CurrentSelectedData.CUNIT_ID : "";
                        loData.CBUILDING_ID = _gridLOIUnitListRef.DataSource.Count > 0 ? _gridLOIUnitListRef.CurrentSelectedData.CBUILDING_ID : "";
                        loData.CFLOOR_ID = _gridLOIUnitListRef.DataSource.Count > 0 ? _gridLOIUnitListRef.CurrentSelectedData.CFLOOR_ID : "";

                        var loParamInvoiceTab = R_FrontUtility.ConvertObjectToObject<PMT01300LOIParameterInvoicePlanDTO>(loData);
                        loParamInvoiceTab.SELECTED_DATA_TAB_CHARGES = _SelectDataFromTabCharges;
                        await _TabPageInvoicePlan.InvokeRefreshTabPageAsync(loParamInvoiceTab);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task TransStatusRadioButton_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel.CPAR_TRANS_STS = poParam;
                await _gridLOIListRef.R_RefreshGrid(null);

                if (_viewModel.LOIGrid.Count == 0)
                {
                    await R_MessageBox.Show("", _localizer["N01"], R_eMessageBoxButtonType.OK);
                    if (_gridLOIUnitListRef.DataSource.Count > 0)
                    {
                        _gridLOIUnitListRef.DataSource.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region LOI List
        private async Task Grid_LOI_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.GetLOIList();

                eventArgs.ListEntityResult = _viewModel.LOIGrid;
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Grid_LOI_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
            await Task.CompletedTask;
        }
        private async Task Grid_LOI_Display(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Normal)
                {
                    var loData = (PMT01300DTO)eventArgs.Data;
                    if (string.IsNullOrWhiteSpace(loData.CTRANS_STATUS) == false)
                    {
                        EnableDraftSts = loData.CTRANS_STATUS == "00";
                        EnableOpenSts = loData.CTRANS_STATUS == "10";
                        EnableApproveSts = loData.CTRANS_STATUS == "30";
                    }
                    if (_viewModel.LOIGrid.Count != 0)
                    {
                        await _gridLOIUnitListRef.R_RefreshGrid(loData);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region LOI Unit
        private async Task Grid_LOI_Unit_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loParameter = R_FrontUtility.ConvertObjectToObject<PMT01310DTO>(eventArgs.Parameter);
                await _viewModel.GetLOIUnitList(loParameter);
                HasUnitData = _viewModel.LOIUNITGrid.Count > 0;
                eventArgs.ListEntityResult = _viewModel.LOIUNITGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region TabPage
        private void LOITab_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            PMT01300DTO loLOIData;
            try
            {
                if (IsAddData)
                {
                    loLOIData = _SelectDataFromTabLOI;
                    loLOIData.LIS_ADD_DATA_LOI = true; 
                }
                else
                {
                    loLOIData = _gridLOIListRef.DataSource.Count > 0 ? _gridLOIListRef.CurrentSelectedData : new PMT01300DTO();
                }

                var loPropertyData = _viewModel.PropertyList.FirstOrDefault(x => x.CPROPERTY_ID == _viewModel.PROPERTY_ID);
                loLOIData.CPROPERTY_ID = _viewModel.PROPERTY_ID;
                loLOIData.CCURRENCY_CODE = loPropertyData.CCURRENCY;
                loLOIData.CCURRENCY_NAME = loPropertyData.CCURRENCY_NAME;

                eventArgs.Parameter = loLOIData;
                eventArgs.TargetPageType = typeof(PMT01310);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        private async Task LOITab_After_Open_TabPage(R_AfterOpenTabPageEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (IsAddData)
                {
                    if (_viewModel.CPAR_TRANS_STS != "00,10,20")
                    {
                        _viewModel.CPAR_TRANS_STS = "00,10,20";
                    }
                    await _gridLOIListRef.R_RefreshGrid(null);

                    var loSelectedChargesData = _gridLOIListRef.DataSource.FirstOrDefault(x => x.CREF_NO == _SelectDataFromTabLOI.CREF_NO);
                    await _gridLOIListRef.R_SelectCurrentDataAsync(loSelectedChargesData);
                    IsAddData = false;
                }
                else
                {
                    await _gridLOIListRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        private void UnitUtilitiesTab_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            PMT01300DTO loLOIData;
            try
            {
                if (IsAddData)
                {
                    loLOIData = _SelectDataFromTabLOI;
                    loLOIData.LIS_ADD_DATA_LOI = true;
                }
                else
                {
                    loLOIData = _gridLOIListRef.CurrentSelectedData;
                }
                var loPropertyData = _viewModel.PropertyList.FirstOrDefault(x => x.CPROPERTY_ID == _viewModel.PROPERTY_ID);
                loLOIData.CCURRENCY_CODE = loPropertyData.CCURRENCY;

                eventArgs.Parameter = loLOIData;
                eventArgs.TargetPageType = typeof(PMT01320);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        private void DepositTab_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            PMT01300DTO loLOIData;
            try
            {
                if (IsAddData)
                {
                    loLOIData = _SelectDataFromTabLOI;
                    loLOIData.LIS_ADD_DATA_LOI = true;
                }
                else
                {
                    loLOIData = _gridLOIListRef.CurrentSelectedData;
                    
                }

                if (_TabLOIList.ActiveTab.Id == "UnitCharges")
                {
                    if (!string.IsNullOrWhiteSpace(_SelectDataFromTabUNIT.CUNIT_ID))
                    {
                        loLOIData.CUNIT_ID = _SelectDataFromTabUNIT.CUNIT_ID;
                        loLOIData.CFLOOR_ID = _SelectDataFromTabUNIT.CFLOOR_ID;
                        loLOIData.CBUILDING_ID = _SelectDataFromTabUNIT.CBUILDING_ID;
                    }
                    else
                    {
                        loLOIData.CUNIT_ID = _gridLOIUnitListRef.CurrentSelectedData.CUNIT_ID;
                        loLOIData.CFLOOR_ID = _gridLOIUnitListRef.CurrentSelectedData.CFLOOR_ID;
                        loLOIData.CBUILDING_ID = _gridLOIUnitListRef.CurrentSelectedData.CBUILDING_ID;
                    }
                }
                else
                {
                    loLOIData.CUNIT_ID = _gridLOIUnitListRef.CurrentSelectedData.CUNIT_ID;
                    loLOIData.CFLOOR_ID = _gridLOIUnitListRef.CurrentSelectedData.CFLOOR_ID;
                    loLOIData.CBUILDING_ID = _gridLOIUnitListRef.CurrentSelectedData.CBUILDING_ID;
                }

                var loPropertyData = _viewModel.PropertyList.FirstOrDefault(x => x.CPROPERTY_ID == _viewModel.PROPERTY_ID);
                loLOIData.CCURRENCY_CODE = loPropertyData.CCURRENCY;
                loLOIData.CCURRENCY_NAME = loPropertyData.CCURRENCY_NAME;

                eventArgs.Parameter = loLOIData;
                eventArgs.TargetPageType = typeof(PMT01340);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        private void InvoicePlanTab_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            PMT01300DTO loLOIData;
            try
            {
                if (IsAddData)
                {
                    loLOIData = _SelectDataFromTabLOI;
                    loLOIData.LIS_ADD_DATA_LOI = true;
                }
                else
                {
                    loLOIData = _gridLOIListRef.CurrentSelectedData;
                }

                if (_TabLOIList.ActiveTab.Id == "UnitCharges")
                {
                    if (!string.IsNullOrWhiteSpace(_SelectDataFromTabUNIT.CUNIT_ID))
                    {
                        loLOIData.CUNIT_ID = _SelectDataFromTabUNIT.CUNIT_ID;
                        loLOIData.CUNIT_NAME = _SelectDataFromTabUNIT.CUNIT_NAME;
                        loLOIData.CFLOOR_ID = _SelectDataFromTabUNIT.CFLOOR_ID;
                        loLOIData.CBUILDING_ID = _SelectDataFromTabUNIT.CBUILDING_ID;
                    }
                    else
                    {
                        loLOIData.CUNIT_ID = _gridLOIUnitListRef.CurrentSelectedData.CUNIT_ID;
                        loLOIData.CUNIT_NAME = _gridLOIUnitListRef.CurrentSelectedData.CUNIT_NAME;
                        loLOIData.CFLOOR_ID = _gridLOIUnitListRef.CurrentSelectedData.CFLOOR_ID;
                        loLOIData.CBUILDING_ID = _gridLOIUnitListRef.CurrentSelectedData.CBUILDING_ID;
                    }
                }
                else
                {
                    loLOIData.CUNIT_ID = _gridLOIUnitListRef.CurrentSelectedData.CUNIT_ID;
                    loLOIData.CUNIT_NAME = _gridLOIUnitListRef.CurrentSelectedData.CUNIT_NAME;
                    loLOIData.CFLOOR_ID = _gridLOIUnitListRef.CurrentSelectedData.CFLOOR_ID;
                    loLOIData.CBUILDING_ID = _gridLOIUnitListRef.CurrentSelectedData.CBUILDING_ID;
                }

                var loParamInvoiceTab = R_FrontUtility.ConvertObjectToObject<PMT01300LOIParameterInvoicePlanDTO>(loLOIData);
                loParamInvoiceTab.SELECTED_DATA_TAB_CHARGES = _SelectDataFromTabCharges;

                eventArgs.Parameter = loParamInvoiceTab;
                eventArgs.TargetPageType = typeof(PMT01350);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private bool _pageCRUDmode = false;
        private async Task R_TabEventCallback(object poValue)
        {
            R_Exception loException = new R_Exception();

            try
            {
                var loValueCallBack = (PMT01300LOICallBackParameterDTO)poValue;
                _pageCRUDmode = loValueCallBack.CRUD_MODE == false;
                if (loValueCallBack.SELECTED_DATA_TAB_LOI != null)
                {
                    _SelectDataFromTabLOI = loValueCallBack.SELECTED_DATA_TAB_LOI;
                }
                if (loValueCallBack.SELECTED_DATA_TAB_UNIT != null)
                {
                    _SelectDataFromTabUNIT = loValueCallBack.SELECTED_DATA_TAB_UNIT;
                }

                IsAddData = loValueCallBack.LIS_ADD_DATA_LOI;
                HasUnitFromUnitTab = loValueCallBack.LIS_LOI_UNIT_HAS_DATA;

                if (loValueCallBack.CRUD_MODE == true)
                {
                    if (loValueCallBack.TO_INVOICE_TAB == true)
                    {
                        if (loValueCallBack.SELECTED_DATA_TAB_CHARGES != null)
                        {
                            _SelectDataFromTabCharges = loValueCallBack.SELECTED_DATA_TAB_CHARGES;
                        }
                        await _TabLOIList.SetActiveTabAsync("InvoicePlan");
                    }
                }

                _TabLOIListStrip.Enabled = loValueCallBack.CRUD_MODE;
                _TabLOIStrip.Enabled = loValueCallBack.CRUD_MODE;
                _TabLOIUnitStrip.Enabled = loValueCallBack.CRUD_MODE;

                if (loValueCallBack.LIS_ADD_DATA_LOI)
                {
                    _TabLOIInvoiceStrip.Enabled = loValueCallBack.CRUD_MODE && !string.IsNullOrWhiteSpace(_SelectDataFromTabUNIT.CUNIT_ID);
                    _TabLOIDepositStrip.Enabled = loValueCallBack.CRUD_MODE && !string.IsNullOrWhiteSpace(_SelectDataFromTabUNIT.CUNIT_ID);
                }
                else
                {
                    _TabLOIInvoiceStrip.Enabled = loValueCallBack.CRUD_MODE;
                    _TabLOIDepositStrip.Enabled = loValueCallBack.CRUD_MODE;
                }
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        #endregion

        #region BTN Process
        private async Task SubmitProcess()
        {
            var loEx = new R_Exception();
            R_eMessageBoxResult loResult;
            bool llValidate = false;
            try
            {

                var loData = _gridLOIListRef.CurrentSelectedData;

                if (HasUnitData == false)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT01300FrontResources.Resources_Dummy_Class),
                        "N08"));
                    llValidate = true;
                }

                if (llValidate == false)
                {
                    loResult = await R_MessageBox.Show("", _localizer["Q02"], R_eMessageBoxButtonType.YesNo);
                    if (loResult == R_eMessageBoxResult.No)
                        goto EndBlock;

                    var loParam = R_FrontUtility.ConvertObjectToObject<PMT01300SubmitRedraftDTO>(loData);
                    loParam.CNEW_STATUS = "10";

                    var loUpdateStatusData = await _viewModel.SubmitRedraftLOI(loParam);

                    await R_MessageBox.Show("", _localizer["N02"], R_eMessageBoxButtonType.OK);
                    await _gridLOIListRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        private async Task RedraftProcess()
        {
            var loEx = new R_Exception();
            R_eMessageBoxResult loResult;
            bool llValidate = false;
            try
            {
                var loData = _gridLOIListRef.CurrentSelectedData;

                loResult = await R_MessageBox.Show("", _localizer["Q03"], R_eMessageBoxButtonType.YesNo);
                if (loResult == R_eMessageBoxResult.No)
                    goto EndBlock;

                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01300SubmitRedraftDTO>(loData);
                loParam.CNEW_STATUS = "00";

                var loUpdateStatusData = await _viewModel.SubmitRedraftLOI(loParam);

                await R_MessageBox.Show("", _localizer["N03"], R_eMessageBoxButtonType.OK);
                await _gridLOIListRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        private async Task CancelLOIProcess()
        {
            var loEx = new R_Exception();
            R_eMessageBoxResult loResult;
            bool llValidate = false;
            try
            {
                var loData = _gridLOIListRef.CurrentSelectedData;

                loResult = await R_MessageBox.Show("", _localizer["Q05"], R_eMessageBoxButtonType.YesNo);
                if (loResult == R_eMessageBoxResult.No)
                    goto EndBlock;

                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01300SubmitRedraftDTO>(loData);
                loParam.CNEW_STATUS = "98";

                var loUpdateStatusData = await _viewModel.SubmitRedraftLOI(loParam);

                await R_MessageBox.Show("", _localizer["N04"], R_eMessageBoxButtonType.OK);
                await _gridLOIListRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        private async Task CloseLOIProcess()
        {
            var loEx = new R_Exception();
            R_eMessageBoxResult loResult;
            bool llValidate = false;
            try
            {
                var loData = _gridLOIListRef.CurrentSelectedData;

                loResult = await R_MessageBox.Show("", _localizer["Q06"], R_eMessageBoxButtonType.YesNo);
                if (loResult == R_eMessageBoxResult.No)
                    goto EndBlock;

                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01300SubmitRedraftDTO>(loData);
                loParam.CNEW_STATUS = "80";

                var loUpdateStatusData = await _viewModel.SubmitRedraftLOI(loParam);

                await R_MessageBox.Show("", _localizer["N05"], R_eMessageBoxButtonType.OK);
                await _gridLOIListRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        private async Task TemplateBtn_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_NotifTemplate"], R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await _viewModel.DownloadTemplate();

                    var saveFileName = "LeaseManager.xlsx";

                    await JSRuntime.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region Upload
        private void Upload_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loPropertyData = _viewModel.PropertyList.FirstOrDefault(x => x.CPROPERTY_ID == _viewModel.PROPERTY_ID);
            eventArgs.Parameter = loPropertyData;
            eventArgs.TargetPageType = typeof(PMT01301);
        }
        private async Task Upload_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _gridLOIListRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        private void BeforeOpenPrintPopup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                var loParam = new PMT01300ReportParamDTO
                {
                    CPROPERTY_ID = _viewModel.PROPERTY_ID,
                    CDEPT_CODE = _viewModel.Data.CDEPT_CODE,
                    CREF_NO = _viewModel.Data.CREF_NO,
                    CTRANS_CODE = _viewModel.Data.CTRANS_CODE,
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(PMT01300PrintPopup);
                eventArgs.PageTitle = _localizer["_Print"];
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
