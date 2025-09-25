using Microsoft.AspNetCore.Components;
using PMM04500COMMON.DTO_s;
using PMM04500MODEL;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using PMM04500FrontResources;

namespace PMM04500FRONT
{
    public partial class PMM04502 : R_Page, R_ITabPage
    {
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }

        private PMM04500ViewModel _viewModelPricing = new();

        private R_Conductor _conUnitTypeCTG;
        private R_Grid<UnitTypeCategoryDTO> _gridUnitTypeCTG;

        private R_Conductor _conHistoryPricing;
        private R_Grid<PricingDTO> _gridHistoryPricing;

        private R_Conductor _conPricingDate;
        private R_Grid<PricingDTO> _gridPricingDate;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<string>(poParameter);
                _viewModelPricing._propertyId = loParam;
                await Task.Delay(300);
                await (_viewModelPricing._propertyId != "" ? _gridUnitTypeCTG.R_RefreshGrid(null) : Task.CompletedTask);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        public async Task RefreshTabPageAsync(object poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModelPricing._propertyId = (string)poParam;
                await Task.Delay(300);
                await _gridUnitTypeCTG.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
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
                var loParam = R_FrontUtility.ConvertObjectToObject<UnitTypeCategoryDTO>(eventArgs.Data);
                await InvokeTabEventCallbackAsync(loParam);
                _viewModelPricing._unitTypeCategoryId = loParam.CUNIT_TYPE_CATEGORY_ID;
                await _gridPricingDate.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region PricingDate

        private async Task PricingDate_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelPricing.GetPricingList(PMM04500ViewModel.eListPricingParamType.GetHistory, true);

                eventArgs.ListEntityResult = _viewModelPricing._pricingList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void PricingDate_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
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

        private async Task PricingDate_Display(R_DisplayEventArgs eventArgs)
        {
            var loData = R_FrontUtility.ConvertObjectToObject<PricingDTO>(eventArgs.Data);
            _viewModelPricing._validDate = loData.CVALID_DATE;
            _viewModelPricing._validId = loData.CVALID_INTERNAL_ID;
            await _gridHistoryPricing.R_RefreshGrid(null);
        }

        #endregion

        #region Pricing

        private async Task HistoryPricing_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelPricing.GetPricingList(PMM04500ViewModel.eListPricingParamType.GetHistory, false);
                eventArgs.ListEntityResult = _viewModelPricing._pricingList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void PricingHistory_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
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

        //private void PricingHistory_Display(R_DisplayEventArgs eventArgs)
        //{
        //    var loData = R_FrontUtility.ConvertObjectToObject<PricingDTO>(eventArgs.Data);
        //}

        #endregion
    }
}
