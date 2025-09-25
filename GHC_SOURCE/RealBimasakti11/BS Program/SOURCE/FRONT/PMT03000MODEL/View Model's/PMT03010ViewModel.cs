using PMT03000COMMON;
using PMT03000COMMON.DTO_s;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System.Globalization;

namespace PMT03000MODEL.View_Model_s
{
    public class PMT03010ViewModel : R_ViewModel<TenantUnitFacilityDTO>
    {
        //variables
        private PMT03001Model _model = new PMT03001Model();
        public ObservableCollection<UnitTypeCtgFacilityDTO> UnitTypeCtgFacilities { get; set; } = new ObservableCollection<UnitTypeCtgFacilityDTO>();
        public ObservableCollection<TenantUnitFacilityDTO> TenantUnitFacilites { get; set; } = new ObservableCollection<TenantUnitFacilityDTO>();
        public TransByUnitDTO TransByUnit { get; set; } = new TransByUnitDTO();
        public UnitTypeCtgFacilityDTO UnitTypeCtgFacility { get; set; } = new UnitTypeCtgFacilityDTO();
        public TenantUnitFacilityDTO TenantUnitFacility { get; set; } = new TenantUnitFacilityDTO();
        public FacilityQtyDTO FacilityQty { get; set; } = new FacilityQtyDTO();
        public string UnitFacilityTypeId { get; set; } = string.Empty;
        public string UnitFacilityTypeName { get; set; } = string.Empty;

        //methods
        public async Task GetList_UnitTypeCtgFacilityAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMT03000ContextConstant.CPROPERTY_ID, TransByUnit.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMT03000ContextConstant.CUNIT_TYPE_CATEGORY_ID, TransByUnit.CUNIT_TYPE_CATEGORY_ID);
                var loResult = await _model.GetList_UnitTypeCtgFacilityAsync();
                UnitTypeCtgFacilities = new ObservableCollection<UnitTypeCtgFacilityDTO>(loResult) ?? new ObservableCollection<UnitTypeCtgFacilityDTO>();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetList_TenantUnitFacilityAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMT03000ContextConstant.CPROPERTY_ID, TransByUnit.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMT03000ContextConstant.CTENANT_ID, TransByUnit.CTENANT_ID);
                R_FrontContext.R_SetStreamingContext(PMT03000ContextConstant.CBUILDING_ID, TransByUnit.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(PMT03000ContextConstant.CFLOOR_ID, TransByUnit.CFLOOR_ID);
                R_FrontContext.R_SetStreamingContext(PMT03000ContextConstant.CUNIT_ID, TransByUnit.CUNIT_ID);
                R_FrontContext.R_SetStreamingContext(PMT03000ContextConstant.CFACILITY_TYPE, UnitFacilityTypeId);
                var loResult = await _model.GetList_TenantUnitFacilityAsync();
                foreach (var loRowData in loResult)
                {
                    loRowData.DSTART_DATE = ParseStringToDate(loRowData.CSTART_DATE);
                    loRowData.DEND_DATE = ParseStringToDate(loRowData.CEND_DATE);
                }
                TenantUnitFacilites = new ObservableCollection<TenantUnitFacilityDTO>(loResult) ?? new ObservableCollection<TenantUnitFacilityDTO>();
                await GetRecord_FacilityQtyAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetRecord_TenantUnitFacilityAsync(TenantUnitFacilityDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poParam.CPROPERTY_ID = TransByUnit.CPROPERTY_ID;
                poParam.CTENANT_ID = TransByUnit.CTENANT_ID;
                poParam.CBUILDING_ID = TransByUnit.CBUILDING_ID;
                poParam.CFLOOR_ID = TransByUnit.CFLOOR_ID;
                poParam.CUNIT_ID = TransByUnit.CUNIT_ID;
                poParam.CUNIT_TYPE_CATEGORY_ID = TransByUnit.CUNIT_TYPE_CATEGORY_ID;
                poParam.CSEQUENCE = poParam.CSEQUENCE ?? "";
                var loResult = await _model.R_ServiceGetRecordAsync(poParam);
                loResult.DSTART_DATE = ParseStringToDate(loResult.CSTART_DATE);
                loResult.DEND_DATE = ParseStringToDate(loResult.CEND_DATE);
                TenantUnitFacility = R_FrontUtility.ConvertObjectToObject<TenantUnitFacilityDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveRecord_TenantUnitFacilityAsync(TenantUnitFacilityDTO poParam, eCRUDMode poCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poParam.CPROPERTY_ID = TransByUnit.CPROPERTY_ID;
                poParam.CTENANT_ID = TransByUnit.CTENANT_ID;
                poParam.CBUILDING_ID = TransByUnit.CBUILDING_ID;
                poParam.CFLOOR_ID = TransByUnit.CFLOOR_ID;
                poParam.CUNIT_ID = TransByUnit.CUNIT_ID;
                poParam.CUNIT_TYPE_CATEGORY_ID = TransByUnit.CUNIT_TYPE_CATEGORY_ID;
                poParam.CFACILITY_TYPE = UnitFacilityTypeId;
                switch (poCRUDMode)
                {
                    case eCRUDMode.AddMode:
                        poParam.CACTION = "NEW";
                        break;
                    case eCRUDMode.EditMode:
                        poParam.CACTION = "EDIT";
                        break;
                }
                var loResult = await _model.R_ServiceSaveAsync(poParam, poCRUDMode);
                loResult.DSTART_DATE = ParseStringToDate(loResult.CSTART_DATE);
                loResult.DEND_DATE = ParseStringToDate(loResult.CEND_DATE);
                TenantUnitFacility = R_FrontUtility.ConvertObjectToObject<TenantUnitFacilityDTO>(loResult);
                await GetRecord_FacilityQtyAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteRecord_TenantUnitFacilityAsync(TenantUnitFacilityDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                poParam.CPROPERTY_ID = TransByUnit.CPROPERTY_ID;
                poParam.CTENANT_ID = TransByUnit.CTENANT_ID;
                poParam.CBUILDING_ID = TransByUnit.CBUILDING_ID;
                poParam.CFLOOR_ID = TransByUnit.CFLOOR_ID;
                poParam.CUNIT_ID = TransByUnit.CUNIT_ID;
                poParam.CUNIT_TYPE_CATEGORY_ID = TransByUnit.CUNIT_TYPE_CATEGORY_ID;
                await _model.R_ServiceDeleteAsync(poParam);
                await GetRecord_FacilityQtyAsync();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ActiveInactive_TenantUnitFacility(TenantUnitFacilityDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poParam.CPROPERTY_ID = TransByUnit.CPROPERTY_ID;
                poParam.CTENANT_ID = TransByUnit.CTENANT_ID;
                poParam.CBUILDING_ID = TransByUnit.CBUILDING_ID;
                poParam.CFLOOR_ID = TransByUnit.CFLOOR_ID;
                poParam.CUNIT_ID = TransByUnit.CUNIT_ID;
                poParam.CFACILITY_TYPE = UnitFacilityTypeId;
                poParam.LACTIVE = !poParam.LACTIVE;
                await _model.ActiveInactive_TenantUnitFacilityAsync(poParam);
                await GetRecord_FacilityQtyAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task GetRecord_FacilityQtyAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var poParam = new TenantUnitFacilityDTO();
                poParam = R_FrontUtility.ConvertObjectToObject<TenantUnitFacilityDTO>(TransByUnit);
                poParam.CFACILITY_TYPE = UnitFacilityTypeId;
                var loResult = await _model.GetRecord_FacilityQtyAsync(poParam);
                FacilityQty = R_FrontUtility.ConvertObjectToObject<FacilityQtyDTO>(loResult) ?? new FacilityQtyDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //helpers
        private DateTime? ParseStringToDate(string pcStringDate)
        {
            if (DateTime.TryParseExact(pcStringDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var loResult))
            {
                return loResult;
            }
            return null;
        }
    }
}
