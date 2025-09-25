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
    public class PMM01030ViewModel : R_ViewModel<PMM01030DTO>
    {
        private PMM01000Model _PMM01000Model = new PMM01000Model();
        private PMM01030Model _PMM01030Model = new PMM01030Model();
        private PMM01000UniversalModel _PMM01000ModelUniversal = new PMM01000UniversalModel();

        public PMM01030DTO RatePG = new PMM01030DTO();
        public ObservableCollection<PMM01030DTO> RatePGList = new ObservableCollection<PMM01030DTO>();

        public PMM01000DTOPropety Property = new PMM01000DTOPropety();
        public List<PMM01002DTO> CADMIN_CHARGE_ID_LIST = new List<PMM01002DTO>();

        public async Task GetProperty(PMM01030DTO poParam)
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

        public async Task GetRatePGList(PMM01030DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01030Model.GetRatePGDateListAsync(poParam);
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
                RatePGList = new ObservableCollection<PMM01030DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<PMM01030DTO> GetRatePGCheckData(PMM01030DTO poParam)
        {
            var loEx = new R_Exception();
            PMM01030DTO loResult = null;
            try
            {
                loResult = await _PMM01030Model.R_ServiceGetRecordAsync(poParam);
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

        public async Task GetRatePG(PMM01030DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                RatePG = await GetRatePGCheckData(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveRatePC(PMM01030DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01030Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
                if (DateTime.TryParseExact(loResult.CCHARGES_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldChargesDate))
                {
                    loResult.DCHARGES_DATE = ldChargesDate;
                }

                RatePG = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteRatePC(PMM01030DTO poNewEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _PMM01030Model.R_ServiceDeleteAsync(poNewEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
