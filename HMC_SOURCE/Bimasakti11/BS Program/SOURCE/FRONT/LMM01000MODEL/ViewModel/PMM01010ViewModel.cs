using PMM01000COMMON;
using PMM01000FrontResources;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PMM01000MODEL
{
    public class PMM01010ViewModel : R_ViewModel<PMM01010DTO>
    {
        private PMM01000Model _PMM01000Model = new PMM01000Model();
        private PMM01010Model _PMM01010Model = new PMM01010Model();
        private PMM01000UniversalModel _PMM01000ModelUniversal = new PMM01000UniversalModel();

        public PMM01010DTO RateEC = new PMM01010DTO();

        public ObservableCollection<PMM01010DTO> RateUCList = new ObservableCollection<PMM01010DTO>();
        public ObservableCollection<PMM01011DTO> RateUCDetailList = new ObservableCollection<PMM01011DTO>();

        public PMM01000DTOPropety Property = new PMM01000DTOPropety();
        public List<PMM01002DTO> CADMIN_CHARGE_ID_LIST = new List<PMM01002DTO>();

        public async Task GetProperty(PMM01010DTO poParam)
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
        public async Task GetRateUCList(PMM01010DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01010Model.GetRateECDateListAsync(poParam);
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
                RateUCList = new ObservableCollection<PMM01010DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetRateUCDetailList(PMM01011DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01010Model.GetRateECListAsync(poParam);

                RateUCDetailList = new ObservableCollection<PMM01011DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetRateEC(PMM01010DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _PMM01010Model.R_ServiceGetRecordAsync(poParam);

                if (DateTime.TryParseExact(loResult.CCHARGES_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldChargesDate))
                {
                    loResult.DCHARGES_DATE = ldChargesDate;
                }
                else
                {
                    loResult.DCHARGES_DATE = null;

                }

                RateEC = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void ValidationUtiliryEC(PMM01010DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;

                lCancel = poParam.NRETRIBUTION_PCT < 0 || poParam.NRETRIBUTION_PCT > 100;

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Resources_Dummy_Class),
                    "10019"));
                }

                lCancel = poParam.NKWH_USED_MAX < 0 || poParam.NKWH_USED_MAX > 100;

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Resources_Dummy_Class),
                    "10020"));
                }

                if (poParam.CADMIN_FEE == "01")
                {
                    lCancel = poParam.NADMIN_FEE_PCT == 0 || poParam.NADMIN_FEE_PCT < 0 || poParam.NADMIN_FEE_PCT > 100;
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "10014"));
                    }

                }

                if (poParam.CADMIN_FEE == "02")
                {
                    lCancel = poParam.NADMIN_FEE_AMT == 0 || poParam.NADMIN_FEE_AMT < 0;

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "10015"));
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveRateEC(PMM01010DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {

                var loResult = await _PMM01010Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                RateEC = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
