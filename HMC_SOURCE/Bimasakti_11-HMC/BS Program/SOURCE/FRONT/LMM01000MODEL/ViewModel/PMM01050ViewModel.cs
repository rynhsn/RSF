using PMM01000COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PMM01000MODEL
{
    public class PMM01050ViewModel : R_ViewModel<PMM01050DTO>
    {
        private PMM01000Model _PMM01000Model = new PMM01000Model();
        private PMM01050Model _PMM01050Model = new PMM01050Model();
        private PMM01000UniversalModel _PMM01000ModelUniversal = new PMM01000UniversalModel();

        public PMM01050DTO RateOT = new PMM01050DTO();

        public ObservableCollection<PMM01051DTO> RateOTWDDetailList = new ObservableCollection<PMM01051DTO>();
        public ObservableCollection<PMM01051DTO> RateOTWKDetailList = new ObservableCollection<PMM01051DTO>();
        public ObservableCollection<PMM01050DTO> RateOTList = new ObservableCollection<PMM01050DTO>();

        public PMM01000DTOPropety Property = new PMM01000DTOPropety();
        public List<PMM01002DTO> CADMIN_CHARGE_ID_LIST = new List<PMM01002DTO>();

        public async Task GetProperty(PMM01050DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loListPropery = await _PMM01000ModelUniversal.GetPropertyAsync();
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM01002DTO>(poParam);
                loParam.CCHARGES_TYPE = "09";
                CADMIN_CHARGE_ID_LIST = await _PMM01000Model.GetChargesUtilityListAsync(loParam);

                Property = loListPropery.Where(k => k.CPROPERTY_ID == poParam.CPROPERTY_ID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetRateOTList(PMM01050DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01050Model.GetRateOTDateListAsync(poParam);
                loResult.Data.ForEach(x =>
                {
                    if (DateTime.TryParseExact(x.CCHARGES_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldChargesDate))
                    {
                        x.DCHARGES_DATE = ldChargesDate;
                    }
                    else
                    {
                        x.DCHARGES_DATE = null;

                    }
                });
                RateOTList = new ObservableCollection<PMM01050DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetRateOTWDDetailList(PMM01051DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {

                poParam.CDAY_TYPE = "WD";

                var loResult = await _PMM01050Model.GetRateOTListAsync(poParam);

                RateOTWDDetailList = new ObservableCollection<PMM01051DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetRateOTWKDetailList(PMM01051DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                poParam.CDAY_TYPE = "WK";

                var loResult = await _PMM01050Model.GetRateOTListAsync(poParam);

                RateOTWKDetailList = new ObservableCollection<PMM01051DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<PMM01050DTO> GetRateOTCheckData(PMM01050DTO poParam)
        {
            var loEx = new R_Exception();
            PMM01050DTO loResult = null;
            try
            {
                loResult = await _PMM01050Model.R_ServiceGetRecordAsync(poParam);
                if (DateTime.TryParseExact(loResult.CCHARGES_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldChargesDate))
                {
                    loResult.DCHARGES_DATE = ldChargesDate;
                }
                else
                {
                    loResult.DCHARGES_DATE = null;

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task GetRateOT(PMM01050DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                RateOT = await GetRateOTCheckData(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveRateOT(PMM01050DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                //poNewEntity.CRATE_OT_LIST = new List<PMM01051DTO>();
                //poNewEntity.CRATE_OT_LIST.AddRange(RateOTWDDetailListData);
                //poNewEntity.CRATE_OT_LIST.AddRange(RateOTWKDetailListData);
                //var loResult = await _PMM01050Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                //RateOT = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
