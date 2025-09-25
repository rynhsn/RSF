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
    public class PMM01040ViewModel : R_ViewModel<PMM01040DTO>
    {
        private PMM01000Model _PMM01000Model = new PMM01000Model();
        private PMM01040Model _PMM01040Model = new PMM01040Model();
        private PMM01000UniversalModel _PMM01000ModelUniversal = new PMM01000UniversalModel();

        public PMM01040DTO RateGU = new PMM01040DTO();
        public ObservableCollection<PMM01040DTO> RateGUList = new ObservableCollection<PMM01040DTO>();

        public PMM01000DTOPropety Property = new PMM01000DTOPropety();
        public List<PMM01002DTO> CADMIN_CHARGE_ID_LIST = new List<PMM01002DTO>();

        public async Task GetProperty(PMM01040DTO poParam)
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
        public async Task GetRateGUList(PMM01040DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01040Model.GetRateGUDateListAsync(poParam);
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
                RateGUList = new ObservableCollection<PMM01040DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<PMM01040DTO> GetRateGUCheckData(PMM01040DTO poParam)
        {
            var loEx = new R_Exception();
            PMM01040DTO loResult = null;
            try
            {
                loResult = await _PMM01040Model.R_ServiceGetRecordAsync(poParam);
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

        public async Task GetRateUG(PMM01040DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                RateGU = await GetRateGUCheckData(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveRateGU(PMM01040DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01040Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
                if (DateTime.TryParseExact(loResult.CCHARGES_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldChargesDate))
                {
                    loResult.DCHARGES_DATE = ldChargesDate;
                }

                RateGU = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteRateGU(PMM01040DTO poNewEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _PMM01040Model.R_ServiceDeleteAsync(poNewEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
