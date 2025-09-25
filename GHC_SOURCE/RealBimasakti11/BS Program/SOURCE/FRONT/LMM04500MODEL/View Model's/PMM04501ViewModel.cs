using PMM04500COMMON;
using PMM04500COMMON.DTO_s;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

namespace PMM04500MODEL
{
    public class PMM04501ViewModel : R_ViewModel<PricingRateDTO>
    {
        private PMM04501Model _modelPricing = new PMM04501Model();
        private string _unitTypeCategoryId { get; set; } = "";

        private const string PRICE_TYPE = "02"; //lease pricing code

        private const string UNITCTGID = "02"; //unit category id 

        public ObservableCollection<PricingRateDTO> _pricingRateList { get; set; } = new ObservableCollection<PricingRateDTO>();

        public ObservableCollection<PricingRateDTO> _pricingRateDateList { get; set; } = new ObservableCollection<PricingRateDTO>();

        public ObservableCollection<PricingRateBulkSaveDTO> _pricingSaveList { get; set; } = new ObservableCollection<PricingRateBulkSaveDTO>();

        public string _propertyId { get; set; } = "";

        public string _pricingRateDate { get; set; } = "";

        public DateTime? _pricingRateDateDisplay { get; set; }

        public async Task GetPricingRateDateList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPMM04500.CPROPERTY_ID, _propertyId);
                R_FrontContext.R_SetStreamingContext(ContextConstantPMM04500.CPRICE_TYPE, PRICE_TYPE);
                var loResult = await _modelPricing.GetPricingRateDateListAsync();
                foreach (var loData in loResult)
                {
                    loData.DRATE_DATE=ParseDateFromString(loData.CRATE_DATE);
                }

                var loMapping =
                _pricingRateDateList = new ObservableCollection<PricingRateDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPricingRateList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPMM04500.CPROPERTY_ID, _propertyId);
                R_FrontContext.R_SetStreamingContext(ContextConstantPMM04500.CUNIT_TYPE_CATEGORY_ID, UNITCTGID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPMM04500.CPRICE_TYPE, PRICE_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPMM04500.CRATE_DATE, _pricingRateDate);
                var loResult = await _modelPricing.GetPricingRateListAsync();
                foreach (var loData in loResult)
                {
                    loData.DRATE_DATE = ParseDateFromString(loData.CRATE_DATE);
                }
                _pricingRateList = new ObservableCollection<PricingRateDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPricingRateAddList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPMM04500.CPROPERTY_ID, _propertyId);
                R_FrontContext.R_SetStreamingContext(ContextConstantPMM04500.CUNIT_TYPE_CATEGORY_ID, UNITCTGID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPMM04500.CPRICE_TYPE, PRICE_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPMM04500.CRATE_DATE, _pricingRateDate);
                var loResult = await _modelPricing.GetPricingRateListAsync();
                foreach (var loData in loResult)
                {
                    loData.DRATE_DATE = ParseDateFromString(loData.CRATE_DATE);
                }
                var loMappingResult = R_FrontUtility.ConvertCollectionToCollection<PricingRateBulkSaveDTO>(loResult);
                _pricingSaveList = new ObservableCollection<PricingRateBulkSaveDTO>(loMappingResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task SavePricing()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                string lcAction = "ADD";
                var loParam = new PricingRateSaveParamDTO()
                {

                    CPROPERTY_ID = _propertyId,
                    CUNIT_TYPE_CATEGORY_ID = UNITCTGID,
                    CPRICE_TYPE = PRICE_TYPE,
                    CACTION = lcAction,
                    CRATE_DATE = _pricingRateDate,
                };

                loParam.PRICING_RATE_LIST = new List<PricingRateBulkSaveDTO>(_pricingSaveList);
                await _modelPricing.SavePricingRateAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private DateTime? ParseDateFromString(string lcDateStr)
        {
            var loEx = new R_Exception();
            try
            {
                if (lcDateStr != null && DateTime.TryParseExact(lcDateStr, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ldDate))
                    return ldDate;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return null;
        }

    }
}
