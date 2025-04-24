using Microsoft.AspNetCore.Components;
using PMM04500COMMON.DTO_s;
using PMM04500MODEL;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System.Globalization;
using PMM04500FrontResources;

namespace PMM04500FRONT
{
    public partial class PMM04500 : R_Page
    {
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }

        private PMM04500ViewModel _viewModelPricing = new();

        private R_Conductor _conUnitTypeCTG;

        private R_Grid<UnitTypeCategoryDTO> _gridUnitTypeCTG;

        private R_Conductor _conPricing;

        private R_Grid<PricingDTO> _gridPricing;

        private R_TabStrip _tabStripPricing; //ref Tabstrip

        private R_TabPage _tabNextPricing; //tabpageNextPricing

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
                await (_viewModelPricing._propertyId != "" ? _gridUnitTypeCTG.R_RefreshGrid(null) : Task.CompletedTask);
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
                if (_conUnitTypeCTG.R_ConductorMode == R_eConductorMode.Normal && _viewModelPricing._propertyId != "")
                {
                    await _gridUnitTypeCTG.R_RefreshGrid(null);

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

        private async Task UnitTypeCTG_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelPricing.GetUnitCategoryList();
                eventArgs.ListEntityResult = _viewModelPricing._unitTypeCategoryList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }

        private void UnitTypeCTG_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                eventArgs.Result = R_FrontUtility.ConvertObjectToObject<UnitTypeCategoryDTO>(eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task UnitTypeCTG_ServiceDisplay(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                //get record data
                var loParam = R_FrontUtility.ConvertObjectToObject<UnitTypeCategoryDTO>(eventArgs.Data);

                // class variable for pricing refreshgrid param
                _viewModelPricing._unitTypeCategoryId = loParam.CUNIT_TYPE_CATEGORY_ID;

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
                await _viewModelPricing.GetPricingList(PMM04500ViewModel.eListPricingParamType.GetAll, false);
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

        private async Task TabEventCallback_NextPricingAsync(object poValue)
        {
            var loEx = new R_Exception();

            try
            {
                if (poValue != null)
                {
                    var loParamCallback = poValue as UnitTypeCategoryDTO;
                    await _gridUnitTypeCTG.R_SelectCurrentDataAsync(loParamCallback);
                    await _gridPricing.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task TabEventCallback_HistoryPricingAsync(object poValue)
        {
            var loEx = new R_Exception();

            try
            {
                if (poValue != null)
                {
                    var loParamCallback = poValue as UnitTypeCategoryDTO;
                    await _gridUnitTypeCTG.R_SelectCurrentDataAsync(loParamCallback);
                    await _gridPricing.R_RefreshGrid(null);
                }
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
            eventArgs.TargetPageType = typeof(PMM04501);
        }

        private void BeforeOpenTabPage_HistoryPricing(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModelPricing._propertyId;
            eventArgs.TargetPageType = typeof(PMM04502);
        }

        private void BeforeOpenTabPage_PricingRate(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModelPricing._propertyId;
            eventArgs.TargetPageType = typeof(PMM04503);
        }

        #endregion
    }
}
