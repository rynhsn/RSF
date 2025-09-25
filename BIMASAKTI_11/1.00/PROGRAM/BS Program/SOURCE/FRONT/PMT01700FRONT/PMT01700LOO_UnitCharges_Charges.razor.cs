using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT01700MODEL.ViewModel;
using PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Charges;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System.Reflection.Emit;
using PMT01700COMMON.DTO.Utilities.Front;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Unit___Charges;
using System.Xml.Linq;
using R_BlazorFrontEnd.Controls.Popup;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using PMT01700COMMON.DTO._2._LOO._2._LOO___Offer;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00200;
using R_CommonFrontBackAPI;
using R_BlazorFrontEnd.Controls.MessageBox;
using System.Collections.ObjectModel;
using System.Globalization;
using PMT01700FrontResources;
using PMT01700COMMON.DTO.Utilities;
using R_BlazorFrontEnd;
using System.Diagnostics.Tracing;

namespace PMT01700FRONT
{
    public partial class PMT01700LOO_UnitCharges_Charges
    {
        private PMT01700LOO_UnitCharges_ChargesViewModel _viewModel = new();

        private R_Conductor? _conductorCharges;
        private R_Grid<PMT01700LOO_UnitCharges_ChargesListDTO>? _gridCharges;

        private R_ConductorGrid? _conductorDetailItem;
        //for detail item
        private R_Grid<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO>? _gridItemCharges;
        public bool _lControlCRUD;
        private bool _lControlChargesItem;
        private bool _lControlButton = true;
        private bool EnabledFeeAmount;
        PMT01700EventCallBackDTO _oEventCallBack = new PMT01700EventCallBackDTO();

        [Inject] IClientHelper? _clientHelper { get; set; }
        private int _pageSizeCharges = 15;
        public bool lFeeAmount;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                _viewModel.oParameterChargeTab = R_FrontUtility.ConvertObjectToObject<PMT01700ParameterChargesTab>(poParameter);

                await _viewModel.GetFeeMethodList();
                await _viewModel.GetPeriodModeList();

                if (!string.IsNullOrEmpty(_viewModel.oParameterChargeTab.CPROPERTY_ID))
                {
                    await _gridCharges.R_RefreshGrid(null);

                    //if (!string.IsNullOrEmpty(_viewModel.oParameter.CREF_NO))
                    //{
                    //    await _gridDetailItem!.R_RefreshGrid(null);
                    //}
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region CHARGES
        bool _hasDataUnit = false;
        private async Task GetListCharges(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetChargesList();
                eventArgs.ListEntityResult = _viewModel.oListCharges;
                _lControlButton = _viewModel.oListCharges.Any();

                if (_viewModel.oListCharges.Any())
                {
                    _lControlButton = _hasDataUnit = true;
                }
                else
                {
                    _hasDataUnit = false;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ServiceGetRecordCharges(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.LTAXABLE = false;
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01700LOO_UnitCharges_ChargesDetailDTO>(eventArgs.Data);
                var loTempParam = R_FrontUtility.ConvertObjectToObject<PMT01700LOO_UnitUtilities_ParameterDTO>(loParam);

                loParam.CDEPT_CODE = _viewModel.oParameterChargeTab.CDEPT_CODE;
                loParam.CTRANS_CODE = _viewModel.oParameterChargeTab.CTRANS_CODE;
                await _viewModel.GetEntityCharges(loParam);

                //if (_viewModel.oEntityCharges != null)
                //    _viewModel._cCurrencyCode = "";
                //else
                //    _viewModel._cCurrencyCode = "";

                if (_viewModel.oEntityCharges!.LCAL_UNIT)
                {
                    if (!string.IsNullOrEmpty(loParam.CSEQ_NO))
                    {
                        await _gridItemCharges!.R_RefreshGrid(loTempParam);
                    }
                    else
                    {
                        loEx.Add("ErrDev", "SeqNo It's Not Supplied");
                    }
                }
                eventArgs.Result = _viewModel.oEntityCharges;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void ServiceAfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var loData = (PMT01700LOO_UnitCharges_ChargesDetailDTO)eventArgs.Data;
            try
            {
                _viewModel._oControlYMD.LYEAR = true;
                _viewModel._oControlYMD.LMONTH = true;
                _viewModel._oControlYMD.LMONTH = true;
                _viewModel.loTempListChargesItem = new ObservableCollection<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO>();
                _viewModel.oListChargesItem = new ObservableCollection<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO>();

                EnabledFeeAmount = true;

                loData.DSTART_DATE = _viewModel.ConvertStringToDateTimeFormat(_viewModel.oParameterChargeTab.CSTART_DATE);
                loData.DEND_DATE = _viewModel.ConvertStringToDateTimeFormat(_viewModel.oParameterChargeTab.CEND_DATE);
                loData.IYEARS = _viewModel.oParameterChargeTab.IYEARS;
                loData.IMONTHS = _viewModel.oParameterChargeTab.IMONTHS;
                loData.IDAYS = _viewModel.oParameterChargeTab.IDAYS;

                loData.CCURRENCY_CODE = _viewModel.oParameterChargeTab.CCURRENCY_CODE ?? "";
                _viewModel._cCurrencyCode = _viewModel.oParameterChargeTab.CCURRENCY_CODE ?? "";
                loData.IDAYS = _viewModel.oParameterChargeTab.IDAYS;
                //loData.LACTIVE = false;
                loData.CBILLING_MODE = _viewModel.loRadioGroupDataCBILLING_MODE.First().CCODE;
                loData.CFEE_METHOD = _viewModel.oComboBoxFeeMethod!.First().CCODE;
                //loData.CFEE_METHOD = _viewModel.oComboBoxFeeMethod!.First().CCODE;
                loData.CINVOICE_PERIOD = _viewModel.oComboBoxPeriodMode!.First().CCODE;
                _viewModel.loTempListChargesItem = _viewModel.oListChargesItem;
                _viewModel.oListChargesItem = new ObservableCollection<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO>();
                _lControlCRUD = true;

                if (_viewModel.Data.CFEE_METHOD == "04")
                {
                    _viewModel.Data.CINVOICE_PERIOD = _viewModel.Data.CFEE_METHOD;
                    _viewModel.EnabledFeeMethod = false;
                }
                else
                {
                    _viewModel.EnabledFeeMethod = true;
                }
                _gridItemCharges!.R_RefreshGrid(null);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ServiceR_Display(R_DisplayEventArgs eventArgs)
        {
            var loException = new R_Exception();
            var loData = (PMT01700LOO_UnitCharges_ChargesDetailDTO)eventArgs.Data;

            try
            {

                if (loData != null)
                {
                    if (!string.IsNullOrEmpty(loData.CSEQ_NO))
                    {
                        var loTempParam = R_FrontUtility.ConvertObjectToObject<PMT01700LOO_UnitUtilities_ParameterDTO>(loData);
                        if (loData.LCAL_UNIT)
                        {
                            await _gridItemCharges!.R_RefreshGrid(loTempParam);
                        }
                        else
                        {
                            _viewModel.oListChargesItem = new();
                        }
                        // var loTempParam = _viewModel.oParameterGetItemCharges;
                        //_viewModel.oParameterChargesList.CTRANS_CODE = _viewModel.oParameterUtilitiesPage.CTRANS_CODE = _viewModel.oParameter.CTRANS_CODE;
                        //_viewModel.oParameterChargesList.CDEPT_CODE = _viewModel.oParameterUtilitiesPage.CDEPT_CODE = _viewModel.oParameter.CDEPT_CODE;
                    }

                }

                if (eventArgs.ConductorMode == R_eConductorMode.Edit)
                {
                    if (_viewModel.Data.CFEE_METHOD == "04")
                    {
                        _viewModel.Data.CINVOICE_PERIOD = _viewModel.Data.CFEE_METHOD;
                        _viewModel.EnabledFeeMethod = false;
                    }
                    else
                    {
                        _viewModel.EnabledFeeMethod = true;
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
        private async Task ServiceSaveCharges(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (PMT01700LOO_UnitCharges_ChargesDetailDTO)eventArgs.Data;
                if (!loParam.LCAL_UNIT)
                {
                    loParam.NTOTAL_AMT = 0;
                }

                await _viewModel.ServiceSaveCharges(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel.oEntityCharges;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (PMT01700LOO_UnitCharges_ChargesDetailDTO)eventArgs.Data;
                await _viewModel.ServiceDeleteCharges(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task Charges_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            //PMT01700LOO_UnitCharges_ChargesDetailDTO? loData = null;
            try
            {
                var loData = (PMT01700LOO_UnitCharges_ChargesDetailDTO)eventArgs.Data;
  
                if (_lControlChargesItem)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationChargesItem");
                    loEx.Add(loErr);
                    goto EndBlock;
                }

                if (eventArgs.ConductorMode == R_eConductorMode.Edit)
                {
                    if (_viewModel.oListChargesItem.Any() && !loData.LCAL_UNIT)
                    {
                        var llFalse = await R_MessageBox.Show("", "All Fee Calculation Data will be deleted, Are you sure!",
                R_eMessageBoxButtonType.OKCancel);
                        switch (llFalse)
                        {
                            case R_eMessageBoxResult.Cancel:
                                eventArgs.Cancel = true;
                                break;
                            case R_eMessageBoxResult.OK:
                                _viewModel.Data.ChargeItemList = null;
                                //dibuat null, soalnya kalo NFEE nya pertama mati, bakal keluar ini lagi
                                break;
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(loData.CCHARGES_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationCharge");
                    loEx.Add(loErr);
                }
                if (loData.IYEARS <= 0 && loData.IMONTHS <= 0 && loData.IDAYS <= 0)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationTenure");
                    loEx.Add(loErr);
                }
                if (!loData.LCAL_UNIT)
                {
                    if (loData.NFEE_AMT <= 0)
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationFeeAmount");
                        loEx.Add(loErr);
                    }
                }
                else
                {
                    if (!_viewModel.oListChargesItem.Any() && loData.NFEE_AMT <= 0)
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationFeeCalculation");
                        loEx.Add(loErr);
                    }
                }

                if (loData.IYEARS <= 0 && loData.IMONTHS <= 0 && loData.IDAYS < 30)
                {
                    if (loData.CFEE_METHOD != "03" && loData.CFEE_METHOD != "04")
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationFeeMethod");
                        loEx.Add(loErr);
                    }
                }
                var DStartDateHeader = ConvertStringToDateTimeFormat(_viewModel.oParameterChargeTab.CSTART_DATE);
                var DEndDateHeader = ConvertStringToDateTimeFormat(_viewModel.oParameterChargeTab.CEND_DATE);

                if (loData.CBILLING_MODE != "01")
                {
                    if (loData.DSTART_DATE < DStartDateHeader || loData.DSTART_DATE > DEndDateHeader)
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationLimitStartDate");
                        loEx.Add(loErr);
                    }
                }
                if (loData.DEND_DATE > DEndDateHeader || loData.DEND_DATE < loData.DSTART_DATE)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationLimitEndDate");
                    loEx.Add(loErr);
                }
                if (loData.NINVOICE_AMT <= 0)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationInvoiceAmount");
                    loEx.Add(loErr);
                }

               // var lCancel = DStartDateHeader > DEndDateHeader;
                if (loData.CFEE_METHOD == "03" || loData.CFEE_METHOD == "04")
                {
                   var  lCancel = (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS != 0);
                    if (lCancel == false)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_PMT01700_Class),
                            "ValidationTenureDaily"));
                    }
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            EnabledFeeAmount = _viewModel.oListChargesItem.Any() ? false : loEx.HasError;
            eventArgs.Cancel = _lControlCRUD = loEx.HasError;
            R_DisplayException(loEx);
        }
        public async Task AfterDelete()
        {
            _hasDataUnit = _lControlCRUD = _lControlButton = _viewModel.oListCharges.Any();
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }
        private async Task SetOther(R_SetEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                //var x = _lControlCRUD;
                //var y = _viewModel.Data.LCAL_UNIT;
                EnabledFeeAmount = _oEventCallBack.LCRUD_MODE = eventArgs.Enable;
                _hasDataUnit = _viewModel.oListCharges.Any();
                _lControlCRUD = !eventArgs.Enable;
                _oEventCallBack.LCRUD_MODE = eventArgs.Enable;
                await InvokeTabEventCallbackAsync(_oEventCallBack);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }
        public async Task BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var res = await R_MessageBox.Show("", @_localizer["ValidationBeforeCancel"], R_eMessageBoxButtonType.YesNo);
                if (res == R_eMessageBoxResult.No)
                {
                    eventArgs.Cancel = true;
                }
                else
                {
                    await Close(false, false);
                    switch (eventArgs.ConductorMode)
                    {
                        case R_eConductorMode.Edit:
                            if (_viewModel.Data.LCAL_UNIT)
                            {
                                await _gridItemCharges.R_RefreshGrid(null);
                            }
                            break;
                        case R_eConductorMode.Add:
                            _viewModel.oListChargesItem = _viewModel.loTempListChargesItem.Any() ? _viewModel.loTempListChargesItem : new ObservableCollection<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO>();
                            _viewModel.loTempListChargesItem = new ObservableCollection<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO>();
                            break;
                        default:
                            break;
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
        #region CR new
        private async Task InvoiceAmountCalculate()
        {
            var loData = _viewModel.Data;
            decimal lnSummary = 0;
            decimal lnGrossAmt = loData.LCAL_UNIT ? 1 : _viewModel.oParameterChargeTab.NTOTAL_GROSS_AREA;

            decimal lnDays = ((loData.IDAYS * loData.NFEE_AMT * lnGrossAmt) / 30);
            decimal lnTenurePeriod = ((loData.IYEARS * 12) + loData.IMONTHS);
            decimal lnPeriodMode = loData.CINVOICE_PERIOD == "01" ? 1 :
                                  loData.CINVOICE_PERIOD == "02" ? 2 :
                                  loData.CINVOICE_PERIOD == "03" ? 3 :
                                  loData.CINVOICE_PERIOD == "04" ? 6 :
                                  loData.CINVOICE_PERIOD == "05" ? 12 :
                                  loData.CINVOICE_PERIOD == "06" ? lnTenurePeriod : 0;


            if (loData.NFEE_AMT > 0)
            {
                if (loData.CFEE_METHOD == "01")
                {
                    lnSummary = (loData.NFEE_AMT * lnGrossAmt * lnPeriodMode) / 1;
                    loData.NINVOICE_AMT = loData.CINVOICE_PERIOD == "06" ? lnSummary + lnDays : lnSummary;
                }
                else if (loData.CFEE_METHOD == "02")
                {
                    lnSummary = (loData.NFEE_AMT * lnGrossAmt * lnPeriodMode) / 12;
                    loData.NINVOICE_AMT = loData.CINVOICE_PERIOD == "06" ? lnSummary + lnDays : lnSummary;
                }
                else if (loData.CFEE_METHOD == "03")
                {
                    loData.NINVOICE_AMT = loData.NFEE_AMT * loData.IDAYS * lnGrossAmt;
                }
                else
                {
                    loData.NINVOICE_AMT = ((loData.NFEE_AMT * lnGrossAmt * 1));
                }
            }
            else
            {
                loData.NINVOICE_AMT = 0;
            }
            await Task.CompletedTask;
        }
        private async Task PeriodMethodComboBox_ValueChange(string poParam)
        {
            _viewModel.Data.CINVOICE_PERIOD = poParam;

            await InvoiceAmountCalculate();
        }


        #endregion

        #region CHARGES_ITEM
        private async Task GetListCharges_Item(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var poParameter = (PMT01700LOO_UnitUtilities_ParameterDTO)eventArgs.Parameter;
                await _viewModel.GetChargesItemList(poParameter);
                eventArgs.ListEntityResult = _viewModel.oListChargesItem;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void R_ValidationItems(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var data = (PMT01700LOO_UnitCharges_Charges_ChargesItemDTO)eventArgs.Data;

                //if (_viewModel.Data.LCAL_UNIT)
                //{
                //    if (string.IsNullOrWhiteSpace(data.CUNIT_ID))
                //    {
                //        loEx.Add("", "Unit ID is required!");
                //    }
                //}

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            eventArgs.Cancel = loEx.HasError;
            loEx.ThrowExceptionIfErrors();
        }
        private void ServiceGetRecordCharges_Item(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (PMT01700LOO_UnitCharges_Charges_ChargesItemDTO)(eventArgs.Data);
                _viewModel.GetChargesItem(loParam);
                eventArgs.Result = _viewModel.oEntityChargesItem;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void R_DisplayChargesItem(R_DisplayEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                var loData = (PMT01700LOO_UnitCharges_Charges_ChargesItemDTO)eventArgs.Data;
                var PMT01700LOO_UnitCharges_ChargesDetailDTO = (PMT01700LOO_UnitCharges_ChargesDetailDTO)_conductorCharges!.R_GetCurrentData();
                PMT01700LOO_UnitCharges_ChargesDetailDTO.NINVOICE_AMT = _viewModel.NTotalItemDetil;

                // _viewModel.oListChargesItem.Add(_viewModel.oEntityChargesItem);

                var loHeaderData = _viewModel.Data;
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    if (_gridItemCharges!.DataSource.Count > 0)
                    {
                        loHeaderData.NINVOICE_AMT = ((_gridItemCharges.DataSource.Sum(x => x.NTOTAL_PRICE)));
                        loHeaderData.NFEE_AMT = 0;
                        EnabledFeeAmount = false;
                        //*  (_gridItemCharges.DataSource.Sum(x => x.)));
                    }
                    else
                    {
                        EnabledFeeAmount = true;
                    }
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            //EndBlocks:
            loException.ThrowExceptionIfErrors();
        }
        private void R_ServiceAfterSave()
        {
            var loEx = new R_Exception();

            try
            {
                PMT01700LOO_UnitCharges_ChargesDetailDTO PMT01700LOO_UnitCharges_ChargesDetailDTO = (PMT01700LOO_UnitCharges_ChargesDetailDTO)_conductorCharges!.R_GetCurrentData();
                // PMT01700LOO_UnitCharges_ChargesDetailDTO.NINVOICE_AMT = _viewModel.NTotalItemDetil;

                // _viewModel.oListChargesItem.Add(_viewModel.oEntityChargesItem);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void AfterDeleteItems()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loData = _viewModel.Data;
                if (_gridItemCharges!.DataSource.Count > 0)
                {
                    //loHeaderData.NINVOICE_AMT = ((_gridItemCharges.DataSource.Sum(x => x.NTOTAL_PRICE)));
                    //loHeaderData.NFEE_AMT = 0;
                    EnabledFeeAmount = false;
                    //*  (_gridItemCharges.DataSource.Sum(x => x.)));
                }
                else
                {
                    loData.NINVOICE_AMT = 0;
                    EnabledFeeAmount = true;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            R_DisplayException(loException);
        }
        private void SetOtherChargesItems(R_SetEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                _lControlChargesItem = !eventArgs.Enable;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            R_DisplayException(loException);
        }

        private void R_CellLostFocused(R_CellValueChangedEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {

                var lnTotalPrice = (PMT01700LOO_UnitCharges_Charges_ChargesItemDTO)eventArgs.CurrentRow;
                //.FirstOrDefault(x => x.Name == "Total Price");
                var liQuantity = (PMT01700LOO_UnitCharges_Charges_ChargesItemDTO)eventArgs.CurrentRow;
                //.FirstOrDefault(x => x.Name == "Qty");
                var lnUnitPrice = (PMT01700LOO_UnitCharges_Charges_ChargesItemDTO)eventArgs.CurrentRow;
                //.FirstOrDefault(x => x.Name == "Unit Price");
                var lnDiscount = (PMT01700LOO_UnitCharges_Charges_ChargesItemDTO)eventArgs.CurrentRow;
                //.FirstOrDefault(x => x.Name == "Discount");

                if (eventArgs.ColumnName != "CITEM_NAME")
                {

                    double qty = Convert.ToDouble(liQuantity.IQTY);
                    double unitPrice = Convert.ToDouble(lnUnitPrice.NUNIT_PRICE);
                    double discount = Convert.ToDouble(lnDiscount.NDISCOUNT) / 100;

                    var loData = (PMT01700LOO_UnitCharges_Charges_ChargesItemDTO)_conductorDetailItem!.R_GetCurrentData();
                    // Menghitung Harga Total dengan Diskon
                    lnTotalPrice.NTOTAL_PRICE = (decimal)(qty * unitPrice * (1 - discount));
                    _viewModel.NTotalItemDetil = (decimal)(qty * unitPrice * (1 - discount));
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }
        #endregion

        #region Lookup Button UTILITTY Charges Lookup
        private R_Lookup _BtnLookupCharges;

        private void BeforeOpenLookUpChargesLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00200ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.oParameterChargeTab.CPROPERTY_ID))
            {
                param = new LML00200ParameterDTO
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CPROPERTY_ID = _viewModel.oParameterChargeTab.CPROPERTY_ID,
                    CCHARGE_TYPE_ID = "01,02,05"
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00200);
        }

        private void AfterOpenLookUpChargesLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            LML00200DTO? loTempResult = null;
            //PMT01100LOO_Offer_SelectedOfferDTO? loGetData = null;

            try
            {
                loTempResult = (LML00200DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;
                _viewModel.Data.CCHARGES_ID = loTempResult.CCHARGES_ID;
                _viewModel.Data.CCHARGES_NAME = loTempResult.CCHARGES_NAME;
                _viewModel.Data.CCHARGES_TYPE = loTempResult.CCHARGES_TYPE;
                _viewModel.Data.CCHARGES_TYPE_DESCR = loTempResult.CCHARGES_TYPE_DESCR;
                _viewModel.LTAXABLE = loTempResult.LTAXABLE;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusCharges()
        {
            R_Exception loEx = new();

            try
            {
                PMT01700LOO_UnitCharges_ChargesDetailDTO loGetData = _viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CCHARGES_ID))
                {
                    loGetData.CCHARGES_ID = "";
                    loGetData.CCHARGES_NAME = "";
                    loGetData.CCHARGES_TYPE = "";
                    loGetData.CCHARGES_TYPE_DESCR = "";
                    _viewModel.LTAXABLE = false;
                    return;
                }

                LookupLML00200ViewModel loLookupViewModel = new LookupLML00200ViewModel();
                LML00200ParameterDTO loParam = new LML00200ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper!.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CPROPERTY_ID = _viewModel.oParameterChargeTab.CPROPERTY_ID!,
                    CCHARGE_TYPE_ID = "01,02,05",
                    CSEARCH_TEXT = loGetData.CCHARGES_ID ?? "",
                };

                var loResult = await loLookupViewModel.GetUnitCharges(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                            "_ErrLookup01"));
                    loGetData.CCHARGES_ID = "";
                    loGetData.CCHARGES_NAME = "";
                    loGetData.CCHARGES_TYPE = "";
                    loGetData.CCHARGES_TYPE_DESCR = "";
                    _viewModel.LTAXABLE = false;
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CCHARGES_ID = loResult.CCHARGES_ID;
                    loGetData.CCHARGES_NAME = loResult.CCHARGES_NAME;
                    loGetData.CCHARGES_TYPE = loResult.CCHARGES_TYPE;
                    loGetData.CCHARGES_TYPE_DESCR = loResult.CCHARGES_TYPE_DESCR;
                    _viewModel.LTAXABLE = loResult.LTAXABLE;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region Currency
        private R_Lookup R_LookupCurrencyLookup;
        private void BeforeOpenLookUpCurrencyLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00300ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.oParameterChargeTab.CPROPERTY_ID))
            {
                param = new GSL00300ParameterDTO
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId
                };
            }

            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00300);
        }

        private void AfterOpenLookUpCurrencyLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL00300DTO? loTempResult = null;
            //PMT02500FrontAgreementDetailDTO? loGetData = null;

            try
            {
                loTempResult = (GSL00300DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //loGetData = (PMT02500FrontAgreementDetailDTO)_conductorFullPMT02500Agreement.R_GetCurrentData();

                _viewModel.Data.CCURRENCY_CODE = loTempResult.CCURRENCY_CODE;
                _viewModel._cCurrencyCode = loTempResult.CCURRENCY_CODE;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }
        private async Task OnLostFocusCurrency()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loGetData = (PMT01700LOO_UnitCharges_ChargesDetailDTO)_viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CCURRENCY_CODE))
                {
                    loGetData.CCURRENCY_CODE = "";
                    return;
                }

                LookupGSL00300ViewModel loLookupViewModel = new LookupGSL00300ViewModel();
                GSL00300ParameterDTO loParam = new GSL00300ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CSEARCH_TEXT = loGetData.CCURRENCY_CODE ?? "",
                };

                var loResult = await loLookupViewModel.GetCurrency(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                        "_ErrLookup01"));
                    loGetData.CCURRENCY_CODE = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CCURRENCY_CODE = loResult.CCURRENCY_CODE;
                    _viewModel._cCurrencyCode = loResult.CCURRENCY_CODE;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion
        #region Lookup Button Tax Code Lookup

        private R_Lookup? R_LookupTaxCodeLookup;
        private void BeforeOpenLookUpTaxCodeLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00110ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.oParameterChargeTab.CPROPERTY_ID))
            {
                param = new GSL00110ParameterDTO
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CTAX_DATE = DateTime.Now.ToString("yyyyMMdd")
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00110);
        }

        private void AfterOpenLookUpTaxCodeLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL00110DTO? loTempResult = null;
            //LMM01500AgreementDetailDTO? loGetData = null;


            try
            {
                loTempResult = (GSL00110DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //loGetData = (LMM01500AgreementDetailDTO)_conductorFullPMT02500Agreement.R_GetCurrentData();

                _viewModel.Data.CTAX_ID = loTempResult.CTAX_ID;
                _viewModel.Data.CTAX_NAME = loTempResult.CTAX_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusTaxCode()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loGetData = _viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CTAX_ID))
                {
                    loGetData.CTAX_ID = "";
                    loGetData.CTAX_NAME = "";
                    return;
                }

                LookupGSL00110ViewModel loLookupViewModel = new LookupGSL00110ViewModel();
                GSL00110ParameterDTO loParam = new GSL00110ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CTAX_DATE = DateTime.Now.ToString("yyyyMMdd"),
                    CSEARCH_TEXT = loGetData.CTAX_ID ?? "",
                };

                var loResult = await loLookupViewModel.GetTaxByDate(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CTAX_ID = "";
                    loGetData.CTAX_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CTAX_ID = loResult.CTAX_ID;
                    loGetData.CTAX_NAME = loResult.CTAX_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region External Function


        private async Task OnChangedFeeMethodAsync(string pcValue)
        {
            _viewModel.Data.CFEE_METHOD = pcValue;
            if (pcValue == "04" || pcValue == "03")
            {
                _viewModel.Data.CINVOICE_PERIOD = "06";
                _viewModel.EnabledFeeMethod = false;
            }
            else
            {
                await InvoiceAmountCalculate();
                _viewModel.EnabledFeeMethod = true;
            }
        }

        private void OnChangedLCAL_UNIT(bool plParam)
        {
            _viewModel.Data.LCAL_UNIT = plParam;
            _viewModel._nTempFEE_AMT = plParam ? _viewModel.Data.NFEE_AMT : _viewModel._nTempFEE_AMT;
            _viewModel.Data.NFEE_AMT = plParam ? 0 : _viewModel._nTempFEE_AMT;

            if (plParam)
            {
                if (_viewModel.loTempListChargesItem.Any())
                {
                    EnabledFeeAmount = false;
                    _viewModel.oListChargesItem = _viewModel.loTempListChargesItem;
                }
                else
                {
                    EnabledFeeAmount = true;
                    _viewModel.oListChargesItem = new ObservableCollection<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO>();
                }
            }
            else
            {
                EnabledFeeAmount = true;
                if (_viewModel.loTempListChargesItem.Any())
                {
                    _viewModel.oListChargesItem = new ObservableCollection<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO>();
                }
                else
                {
                    _viewModel.loTempListChargesItem = _viewModel.oListChargesItem;
                    _viewModel.oListChargesItem = new ObservableCollection<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO>();
                }

            }
            _viewModel.Data.NINVOICE_AMT = !plParam ? _viewModel.Data.NFEE_AMT * _viewModel.oParameterChargeTab.NTOTAL_GROSS_AREA : ((_viewModel.oListChargesItem.Sum(x => x.NTOTAL_PRICE)));
        }

        private async Task OnChangedCYEAR(Int32 poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01700LOO_UnitCharges_ChargesDetailDTO)_viewModel.Data;
                PMT01700ControlYMD llControl = _viewModel._oControlYMD;
                loData.IYEARS = poParam;

                if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
                {
                    if (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                    }
                }
                else
                {
                    llControl.LYEAR = true;
                    loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddDays(-1);
                }
                await InvoiceAmountCalculate();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task OnChangedCMONTH(Int32 poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = _viewModel.Data;
                var llControl = _viewModel._oControlYMD;
                loData.IMONTHS = poParam;
                if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
                {
                    if (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                    }
                }
                else
                {
                    llControl.LMONTH = true;
                    loData.DEND_DATE = loData.DSTART_DATE!.Value.AddMonths(loData.IMONTHS).AddDays(-1);
                }
                await InvoiceAmountCalculate();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task OnChangedCDAY(Int32 poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = _viewModel.Data;
                var llControl = _viewModel._oControlYMD;
                loData.IDAYS = poParam;

                if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
                {
                    if (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                    }
                }
                else
                {
                    llControl.LYEAR = true;
                    loData.DEND_DATE = loData.DSTART_DATE!.Value.AddDays(loData.IDAYS).AddDays(-1);
                }
                await InvoiceAmountCalculate();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private void OnChangedIHOUR(Int32 poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = _viewModel.Data;
                var llControl = _viewModel._oControlYMD;

                // Mengatur nilai IHOUR
                //loData.IHOURS = poParam;

                if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
                {
                    if (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0)//&& )loData.IHOURS == 0)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value
                            .AddYears(loData.IYEARS)
                            .AddMonths(loData.IMONTHS)
                            .AddDays(loData.IDAYS)
                            //.AddHours(loData.IHOURS)
                            .AddDays(-1);
                    }
                }
                else
                {
                    llControl.LYEAR = true;
                    llControl.LMONTH = true;
                    llControl.LDAY = true;
                    loData.DEND_DATE = loData.DSTART_DATE!.Value
                        .AddDays(loData.IDAYS)
                        //   .AddHours(loData.IHOURS)
                        .AddDays(-1);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        DateTime tStartDate = DateTime.Now.AddDays(-1);
        /*
        private void OnChangedDSTART_DATE(DateTime? poValue)
        {
            R_Exception loException = new R_Exception();

            try
            {
                PMT01700LOO_UnitCharges_ChargesDetailDTO loData = _viewModel.Data;
                loData.DSTART_DATE = poValue;

                if (loData.DEND_DATE == null)
                {
                    loData.DEND_DATE = loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0
                        ? loData.DSTART_DATE
                        : loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                }
                else
                {
                    CalculateYMD(poStartDate: loData.DSTART_DATE, poEndDate: loData.DEND_DATE);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        private void OnChangedDEND_DATE(DateTime? poValue)
        {
            R_Exception loException = new R_Exception();

            try
            {
                PMT01700LOO_UnitCharges_ChargesDetailDTO loData = _viewModel.Data;
                loData.DEND_DATE = poValue;

                if (loData.DSTART_DATE == null)
                {
                    loData.DSTART_DATE = loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0
                        ? loData.DEND_DATE
                        : loData.DEND_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                }
                else
                {
                    CalculateYMD(poStartDate: loData.DSTART_DATE, poEndDate: loData.DEND_DATE);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }
        */
        private async Task OnChangedDSTART_DATE(DateTime? poValue)
        {
            R_Exception loException = new R_Exception();
            var loData = _viewModel.Data;
            try
            {
                if (poValue.HasValue)
                {
                    DateTime adjustedValue = new DateTime(poValue.Value.Year, poValue.Value.Month, poValue.Value.Day, poValue.Value.Hour, 0, 0);
                    loData.DSTART_DATE = poValue;
                }

                tStartDate = poValue ?? DateTime.Now;

                if (loData.DEND_DATE == null)
                {
                    loData.DEND_DATE = loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0
                        ? loData.DSTART_DATE
                        : loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                }
                else
                {
                    CalculateYMD(poStartDate: loData.DSTART_DATE, poEndDate: loData.DEND_DATE, pcStart: "S");
                }
                await InvoiceAmountCalculate();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        private async Task OnChangedDEND_DATE(DateTime? poValue)
        {
            R_Exception loException = new R_Exception();

            try
            {
                PMT01700LOO_UnitCharges_ChargesDetailDTO loData = _viewModel.Data;
                loData.DEND_DATE = poValue;

                if (loData.DSTART_DATE == null)
                {
                    loData.DSTART_DATE = loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0
                        ? loData.DEND_DATE
                        : loData.DEND_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                }
                else
                {
                    CalculateYMD(poStartDate: loData.DSTART_DATE, poEndDate: loData.DEND_DATE);
                }
                await InvoiceAmountCalculate();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        private void CalculateYMD(DateTime? poStartDate, DateTime? poEndDate, string pcStart = "")
        {
            R_Exception loException = new R_Exception();
            PMT01700LOO_UnitCharges_ChargesDetailDTO loData = _viewModel.Data;

            try
            {
                if (poEndDate != null && poStartDate != null)
                {
                    if (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0)
                    {
                        loData.IDAYS = 1;
                        loData.IMONTHS = loData.IYEARS = 0;
                        //loData.IHOURS = 24; // Set to 24 hours for 1 day difference
                        if (string.IsNullOrEmpty(pcStart))
                            loData.DSTART_DATE = loData.DEND_DATE;
                        else
                            loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        if (pcStart == "S")
                        {
                            loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                        }
                        else
                        {
                            loData.DSTART_DATE = loData.DEND_DATE!.Value.AddYears(-loData.IYEARS).AddMonths(-loData.IMONTHS).AddDays(-loData.IDAYS).AddDays(1);
                        }
                        //    loData.IDAYS = dValueEndDate.Day - poStartDate!.Value.Day;
                        //    if (loData.IDAYS < 0)
                        //    {
                        //        DateTime dValueEndDateForHandleDay = dValueEndDate.AddMonths(-1);
                        //        int liTempDayinMonth = DateTime.DaysInMonth(dValueEndDateForHandleDay.Year, dValueEndDateForHandleDay.Month);
                        //        loData.IDAYS = liTempDayinMonth + loData.IDAYS;
                        //        if (loData.IDAYS < 0) { throw new Exception("ERROR HARINYA MINUS"); }
                        //        loData.IMONTHS = dValueEndDateForHandleDay.Month - poStartDate!.Value.Month;
                        //        if (loData.IMONTHS < 0)
                        //        {
                        //            loData.IMONTHS = 12 + loData.IMONTHS;
                        //            DateTime dValueEndDateForHandleMonth = dValueEndDate.AddYears(-1);
                        //            loData.IYEARS = dValueEndDateForHandleMonth.Year - poStartDate!.Value.Year;
                        //            if (loData.IYEARS < 0)
                        //            {
                        //                loData.IYEARS = 0;
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        loData.IMONTHS = dValueEndDate.Month - poStartDate!.Value.Month;
                        //        if (loData.IMONTHS < 0)
                        //        {
                        //            loData.IMONTHS = 12 + loData.IMONTHS;
                        //            DateTime dValueEndDateForHandleMonth = dValueEndDate.AddYears(-1);
                        //            loData.IYEARS = dValueEndDateForHandleMonth.Year - poStartDate!.Value.Year;
                        //            if (loData.IYEARS < 0)
                        //            {
                        //                loData.IYEARS = 0;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            loData.IYEARS = dValueEndDate.Year - poStartDate!.Value.Year;
                        //        }
                        //    }
                        //    // Calculate hours
                        //    //loData.IHOURS = (int)(poEndDate.Value - poStartDate.Value).TotalHours % 24;
                        //}
                    }
                }
                else
                {
                    if (poStartDate != null)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddDays(1);
                        loData.IYEARS = loData.IMONTHS = 0;
                        loData.IDAYS = 2;
                        //loData.IHOURS = 24; // Set to 24 hours for 1 day difference
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        private async Task OnChangedNFEE_AMT(decimal poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = _viewModel.Data;

                loData.NFEE_AMT = poParam;
                if (!_viewModel.oListChargesItem.Any())
                {
                    await InvoiceAmountCalculate();
                    //loData.NINVOICE_AMT = poParam * _viewModel.oParameterChargeTab.NTOTAL_GROSS_AREA;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion
        #region utilities
        private DateTime? ConvertStringToDateTimeFormat(string? pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                // Jika string kosong atau null, kembalikan DateTime.MinValue atau nilai default yang sesuai
                //return DateTime.MinValue; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
                return null;
            }
            else
            {
                // Parse string ke DateTime
                DateTime result;
                if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    return result;
                }
                else
                {
                    // Jika parsing gagal, kembalikan DateTime.MinValue atau nilai default yang sesuai
                    //return DateTime.MinValue; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
                    return null;
                }
            }
        }

        private string? ConvertDateTimeToStringFormat(DateTime? ptEntity)
        {
            if (!ptEntity.HasValue || ptEntity.Value == null)
            {
                // Jika ptEntity adalah null atau DateTime.MinValue, kembalikan null
                return null;
            }
            else
            {
                // Format DateTime ke string "yyyyMMdd"
                return ptEntity.Value.ToString("yyyyMMdd");
            }
        }
        #endregion
    }
}
