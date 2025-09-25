using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00600;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PMB04000COMMON.DTO.DTOs;
using PMB04000COMMON.DTO.Utilities;
using PMB04000MODEL.ViewModel;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB04000FRONT
{
    public partial class PMB04000 : R_Page
    {
        readonly PMB04000ViewModel _viewModel = new();
        readonly R_Conductor? _conductorHeader;
        private R_ConductorGrid? _conductorRef;
        private R_Grid<PMB04000DTO>? _grid;

        private R_TabStrip? _tabStripInvoice;
        private R_TabPage? _tabPageSubReceipt;
        [Inject] IClientHelper? _clientHelper { get; set; }
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
            var loValidate = await R_MessageBox.Show("", "Data Successfully Created", R_eMessageBoxButtonType.OK);
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
                await _viewModel.GetPropertyList();
                if (_viewModel._lPropertyExist)
                {
                    await _viewModel.GetInvoiceTypeList();
                    await _viewModel.GetPeriodRangeYear();
                    var oMonthList = _viewModel.GetMonth();
                    _viewModel.GetMonthList = oMonthList;

                    _viewModel.StateChangeAction = StateChangeInvoke;
                    _viewModel.DisplayErrorAction = DisplayErrorInvoke;
                    _viewModel.ShowSuccessAction = async () =>
                    {
                        await ShowSuccessInvoke();
                    };
                    await Task.CompletedTask;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void OnChangedProperty(object? poParam)
        {
            var loEx = new R_Exception();
            string lsProperty = (string)poParam!;
            try
            {
                if (_viewModel._lPropertyExist)
                {
                    _viewModel.oParameterInvoice.CPROPERTY_ID = lsProperty!;

                    _viewModel.oParameterInvoice.CTENANT_ID = "";
                    _viewModel.oParameterInvoice.CTENANT_NAME = "";
                    _viewModel.oParameterInvoice.CDEPT_CODE = "";
                    _viewModel.oParameterInvoice.CDEPT_NAME = "";
                    _viewModel.loInvoiceList.Clear();
                };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void OnChangedInvoiceType(object? poParam)
        {
            var loEx = new R_Exception();
            string lsInvoiceType = (string)poParam!;
            try
            {
                _viewModel.oParameterInvoice.CINVOICE_TYPE = lsInvoiceType!;
                _viewModel.loInvoiceList.Clear();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void OnChangeAllTenant()
        {
            var loException = new R_Exception();
            try
            {
                _viewModel.loInvoiceList.Clear();
                if (_viewModel.oParameterInvoice.LALL_TENANT)
                {
                    _viewModel.oParameterInvoice.CTENANT_ID = "";
                    _viewModel.oParameterInvoice.CTENANT_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

        }
        private void OnChangeAllPeriod()
        {
            var loException = new R_Exception();
            try
            {
                _viewModel.loInvoiceList.Clear();
                if (_viewModel.oParameterInvoice.LALL_PERIOD)
                {
                    _viewModel.oParameterInvoice.IPERIOD_YEAR = 0;
                    _viewModel.oParameterInvoice.CPERIOD_MONTH = "";
                }
                else
                {
                    _viewModel.oParameterInvoice.IPERIOD_YEAR = DateTime.Now.Year;
                    _viewModel.oParameterInvoice.CPERIOD_MONTH = DateTime.Now.Month.ToString("D2");
                }
                //  _lcPeriod = (_viewModel.oParameterInvoiceReceiptTAB1.IPERIOD_YEAR.ToString()) + _viewModel.oParameterInvoiceReceiptTAB1.CPERIOD_MONTH;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

        }

        #region Master Tab
        private async Task BtnRefresh()
        {
            var loEx = new R_Exception();
            try
            {
                if (_viewModel._lPropertyExist)
                {
                    _viewModel.ValidationParamToGetList();
                    await _grid!.R_RefreshGrid(null)!;

                    if (_tabStripInvoice!.ActiveTab.Id == "TabReceiptInfo")
                    {
                        await _tabPageSubReceipt!.InvokeRefreshTabPageAsync(_viewModel.oParameterInvoice);
                    }
                }
                else
                {
                    await R_MessageBox.Show("", _localizer["ValidationProperty"], R_eMessageBoxButtonType.OK);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task R_ServiceGetList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                _lcPeriod = (_viewModel.oParameterInvoice.IPERIOD_YEAR.ToString()) + _viewModel.oParameterInvoice.CPERIOD_MONTH;

                await _viewModel.GetOfficialInvoiceList(paramPeriod: _lcPeriod);
                if (!_viewModel.loInvoiceList.Any())
                {
                    _viewModel.loInvoiceList.Clear();
                }
                eventArgs.ListEntityResult = _viewModel.loInvoiceList;
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
        private async Task BtnCreate()
        {
            var loEx = new R_Exception();
            try
            {
                await _grid!.R_SaveBatch();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion
        #region Save Batch
        private async Task ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loList = (List<PMB04000DTO>)eventArgs.Data;
                List<PMB04000DTO> poDataSelected = _viewModel.ValidationProcessData(loList);
                _viewModel.pcTYPE_PROCESS = "CREATE_RECEIPT";

                if (await R_MessageBox.Show("Confirmation",
                    $"Are you sure want to create receipt selected Data?",
                     R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.Yes)
                {
                    var loParam = new PMB04000ParamDTO
                    {
                        CCOMPANY_ID = _clientHelper!.CompanyId,
                        CUSER_ID = _clientHelper!.UserId,
                        CTYPE_PROCESS = _viewModel.pcTYPE_PROCESS
                    };
                    await _viewModel.CreateCancelReceipt(poParam: loParam, poListData: poDataSelected);

                    //CLEAR OLD DATA
                    await _grid!.R_RefreshGrid(null);
                    //_viewModel.BankInChequeInfo = new();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
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
        #region Control Tab
        private void Before_Open_ReceiptInfo(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = _viewModel.oParameterInvoice;
                eventArgs.TargetPageType = typeof(PMB04000Receipt);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }
        private async Task OnActiveTabDepositIndexChangingAsync(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            //that used, when user on crud mode and changing the tab. then the command  will be cancelled
            if (eventArgs.TabStripTab.Id == "TabInvoiceList")
            {
                await _grid!.R_RefreshGrid(null);
            }
            //else if (!_pageDepositOnCRUDmode)
            //{
            //    eventArgs.Cancel = true;
            //}
        }
        #endregion

        #region Lookup Button Department Lookup
        private readonly R_Lookup? R_LookupDepartmentLookup;
        private void BeforeOpenLookUpDepartmentLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00710ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.oParameterInvoice.CPROPERTY_ID))
            {
                param = new GSL00710ParameterDTO
                {
                    CPROPERTY_ID = _viewModel.oParameterInvoice.CPROPERTY_ID,
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00710);
        }
        private void AfterOpenLookUpDepartmentLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new ();
            try
            {
                var loTempResult = (GSL00710DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;
                _viewModel.oParameterInvoice.CDEPT_CODE = loTempResult.CDEPT_CODE;
                _viewModel.oParameterInvoice.CDEPT_NAME = loTempResult.CDEPT_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            R_DisplayException(loException);
        }
        private async Task OnLostFocusDepartment()
        {
            R_Exception loEx = new ();
            try
            {
                var loGetData = _viewModel.oParameterInvoice;
                if (string.IsNullOrWhiteSpace(loGetData.CDEPT_CODE))
                {
                    loGetData.CDEPT_CODE = "";
                    loGetData.CDEPT_NAME = "";
                    return;
                }

                LookupGSL00710ViewModel loLookupViewModel = new ();
                GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
                {
                    CPROPERTY_ID = loGetData.CPROPERTY_ID!,
                    CSEARCH_TEXT = loGetData.CDEPT_CODE ?? "",
                };
                var loResult = await loLookupViewModel.GetDepartmentProperty(loParam);
                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CDEPT_CODE = "";
                    loGetData.CDEPT_NAME = "";
                }
                else
                {
                    loGetData.CDEPT_CODE = loResult.CDEPT_CODE;
                    loGetData.CDEPT_NAME = loResult.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region Lookup Tenant
        private R_Lookup? R_LookupTenantLookup;
        private void BeforeOpenLookUpTenantLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00600ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.oParameterInvoice.CPROPERTY_ID))
            {
                param = new LML00600ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.oParameterInvoice.CPROPERTY_ID,
                    CCUSTOMER_TYPE = "",
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00600);
        }
        private void AfterOpenLookUpTenantLookupAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            LML00600DTO? loTempResult = null;
            try
            {
                loTempResult = (LML00600DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;
                _viewModel.oParameterInvoice.CTENANT_ID = loTempResult.CTENANT_ID;
                _viewModel.oParameterInvoice.CTENANT_NAME = loTempResult.CTENANT_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }
        private async Task OnLostFocusTenant()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loGetData = _viewModel.oParameterInvoice;
                if (string.IsNullOrWhiteSpace(loGetData.CTENANT_ID))
                {
                    loGetData.CTENANT_ID = "";
                    loGetData.CTENANT_NAME = "";
                    return;
                }
                LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel();
                LML00600ParameterDTO loParam = new LML00600ParameterDTO()
                {
                    CPROPERTY_ID = loGetData.CPROPERTY_ID!,
                    CCUSTOMER_TYPE = "",
                    CSEARCH_TEXT = loGetData.CTENANT_ID ?? "",
                };
                var loResult = await loLookupViewModel.GetTenant(loParam);
                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CTENANT_ID = "";
                    loGetData.CTENANT_NAME = "";
                }
                else
                {
                    loGetData.CTENANT_ID = loResult.CTENANT_ID;
                    loGetData.CTENANT_NAME = loResult.CTENANT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion
        #region implement library for checkbox select
        private void R_CheckBoxSelectValueChanged(R_CheckBoxSelectValueChangedEventArgs eventArgs)
        {
        }
        #endregion




    }
}
