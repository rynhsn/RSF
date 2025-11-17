



using PMM04700Common.DTOs;
using PMM4700MODEL;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMM4700MODEL.ViewModel;

namespace PMM04700FRONT
{
    public partial class PMM04700 : R_Page
    {
        private PMM04700ViewModel _viewModelPricing = new();

        private R_Conductor _ConductorOtherType;
        private R_Grid<OtherUnitDTO> _gridOtherUnit;

        private R_Conductor _conPricing;
        private R_Grid<PricingDTO> _gridPricing;

        private R_TabStrip _tabStripPricing; //ref Tabstrip
        private R_TabPage _tabNextPricing; //tabpageNextPricing\
        private R_TabPage _tabHistoryPricing; //tabpageNextPricing
        private R_TabPage _tabPricingRate; //tabpageNextPricing

        private bool _pageNextPricingOnCRUDmode = false; //to prevent moving tab while crudmode
        private bool _comboboxPropertyEnabled = true; //to prevent combobox while crudmode

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModelPricing.GetPropertyList();
                await Task.Delay(300);
                await (_viewModelPricing._propertyId != "" ? _gridOtherUnit.R_RefreshGrid(null) : Task.CompletedTask);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        public async Task ComboboxPropertyValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModelPricing._propertyId = string.IsNullOrWhiteSpace(poParam) ? "" : poParam; 
                var loCurrencyData = _viewModelPricing._propertyList.FirstOrDefault(properties => properties.CPROPERTY_ID == poParam);
                _viewModelPricing._currency = loCurrencyData != null ? $"{loCurrencyData.CCURRENCY_NAME}({loCurrencyData.CCURRENCY})" : string.Empty;
                await Task.Delay(300);
                if (_ConductorOtherType.R_ConductorMode == R_eConductorMode.Normal && _viewModelPricing._propertyId != "")
                {
                    await _gridOtherUnit.R_RefreshGrid(null);

                    //sending property and refresh another tab (will be catch at init master)
                    switch (_tabStripPricing.ActiveTab.Id)
                    {
                        case "NP":
                            await _tabNextPricing.InvokeRefreshTabPageAsync(_viewModelPricing._propertyId);
                            break;
                        case "HP":
                            await _tabHistoryPricing.InvokeRefreshTabPageAsync(_viewModelPricing._propertyId);
                            break;
                        case "PR":
                            await _tabPricingRate.InvokeRefreshTabPageAsync(_viewModelPricing._propertyId);
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

        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            eventArgs.Cancel = _pageNextPricingOnCRUDmode;//prevent move to another tab
        }

        #region UnitTypeCategory

        private async Task OtherUnit_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelPricing.GetUnitCategoryList();
                eventArgs.ListEntityResult = _viewModelPricing._OtherUnitList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }

        private void OtherUnit_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                eventArgs.Result = R_FrontUtility.ConvertObjectToObject<OtherUnitDTO>(eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OtherUnit_ServiceDisplay(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                //get record data
                var loParam = R_FrontUtility.ConvertObjectToObject<OtherUnitDTO>(eventArgs.Data);

                // class variable for pricing refreshgrid param
                _viewModelPricing._OtherUnitId = loParam.COTHER_UNIT_TYPE_ID;
                
                //reset valid date & valid id form
                _viewModelPricing._validDateDisplay = null;
                _viewModelPricing._validDate = "";
                _viewModelPricing._validId = "";

                //refresh grid pricing
                await _gridPricing.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Current Pricing

        private async Task CurrentPricing_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelPricing.GetPricingList(PMM04700ViewModel.eListPricingParamType.GetAll, false);
                eventArgs.ListEntityResult = _viewModelPricing._pricingList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }

        private void CurrentPricing_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                eventArgs.Result = R_FrontUtility.ConvertObjectToObject<PricingDTO>(eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void CurrentPricing_Display(R_DisplayEventArgs eventArgs)
        {
            var loData = R_FrontUtility.ConvertObjectToObject<PricingDTO>(eventArgs.Data);

            //parsing date
            if (DateTime.TryParseExact(loData.CVALID_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldStartDate))
            {
            _viewModelPricing._validDateDisplay = ldStartDate;
            }
            _viewModelPricing._validDate = loData.CVALID_DATE;
            _viewModelPricing._validId = loData.CVALID_INTERNAL_ID;
        }

        #endregion

        #region Next pricing TabPage
        private void TabEventCallback(object poValue)
        {
            var loEx = new R_Exception();

            try
            {
                _pageNextPricingOnCRUDmode = !(bool)poValue;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

         private void BeforeOpenTabPage_NextPricing(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModelPricing._propertyId;
            eventArgs.TargetPageType = typeof(PMM04701);
        }


        private void BeforeOpenTabPage_HistoryPricing(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModelPricing._propertyId;
            eventArgs.TargetPageType = typeof(PMM04702);
        }

        private void BeforeOpenTabPage_PricingRate(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModelPricing._propertyId;
            eventArgs.TargetPageType = typeof(PMM04703);
        }

        #endregion
    }
}
