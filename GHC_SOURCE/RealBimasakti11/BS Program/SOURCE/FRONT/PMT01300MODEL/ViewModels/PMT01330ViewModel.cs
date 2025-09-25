using PMT01300COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PMT01300MODEL
{
    public class PMT01330ViewModel : R_ViewModel<PMT01330DTO>
    {
        private PMT01330Model _PMT01330Model = new PMT01330Model();
        private PMT01300InitModel _PMT01300InitModel = new PMT01300InitModel();

        #region Property Class
        public PMT01300TenureConvertDTO oControlYMD { get; set; } = new PMT01300TenureConvertDTO();
        public PMT01310DTO LOI_Unit { get; set; } = new PMT01310DTO();
        public PMT01330DTO LOI_Charge { get; set; } = new PMT01330DTO();
        public ObservableCollection<PMT01330DTO> LOIChargeGrid { get; set; } = new  ObservableCollection<PMT01330DTO>();
        public ObservableCollection<PMT01300AgreementChargeCalUnitDTO> ChargeCalUnitGrid { get; set; } = new  ObservableCollection<PMT01300AgreementChargeCalUnitDTO>();
        public ObservableCollection<PMT01300AgreementChargeCalUnitDTO> ChargeCalUnitDisplayGrid { get; set; } = new  ObservableCollection<PMT01300AgreementChargeCalUnitDTO>();
        public List<PMT01300UniversalDTO> VAR_FEE_METHOD { get; set; } = new List<PMT01300UniversalDTO>();
        public List<PMT01300UniversalDTO> VAR_PERIOD_MODE { get; set; } = new List<PMT01300UniversalDTO>();
        public bool StatusChange { get; set; }
        #endregion

        #region ComboBox ViewModel
        public List<KeyValuePair<string, string>> BillingModeList { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("01", R_FrontUtility.R_GetMessage(typeof(PMT01300FrontResources.Resources_Dummy_Class), "_Dp")),
            new KeyValuePair<string, string>("02", R_FrontUtility.R_GetMessage(typeof(PMT01300FrontResources.Resources_Dummy_Class), "_ByPeriod")),
        };
        #endregion
        public async Task GetInitialVar()
        {
            var loEx = new R_Exception();
            try
            {
                var loFeeMethodListResult = await _PMT01300InitModel.GetAllUniversalListAsync("_BS_FEE_METHOD");
                VAR_FEE_METHOD = loFeeMethodListResult;

                var loPeriodModeListResult = await _PMT01300InitModel.GetAllUniversalListAsync("_BS_INVOICE_PERIOD");
                VAR_PERIOD_MODE = loPeriodModeListResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetLOIChargeList(PMT01330DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT01330Model.GetLOIChargeStreamAsync(poEntity);
                loResult.ForEach(x => 
                { 
                    x.CCHARGES_ID_NAME = x.CCHARGES_NAME + " (" + x.CCHARGES_ID + ")";
                    if (DateTime.TryParseExact(x.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldStartDate))
                    {
                        x.DSTART_DATE = ldStartDate;
                    }
                    else
                    {
                        x.DSTART_DATE = null;
                    }
                    if (DateTime.TryParseExact(x.CEND_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldEndDate))
                    {
                        x.DEND_DATE = ldEndDate;
                    }
                    else
                    {
                        x.DEND_DATE = null;
                    }
                }); 

                LOIChargeGrid = new ObservableCollection<PMT01330DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetChargeCallUnitList(PMT01300ParameterAgreementChargeCalUnitDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CDEPT_CODE = LOI_Unit.CDEPT_CODE;
                poEntity.CPROPERTY_ID = LOI_Unit.CPROPERTY_ID;
                var loResult = await _PMT01300InitModel.GetAllAgreementChargeCallUnitListAsync(poEntity);

                ChargeCalUnitGrid = new ObservableCollection<PMT01300AgreementChargeCalUnitDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetChargeCallUnitListDisplay(PMT01300ParameterAgreementChargeCalUnitDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CDEPT_CODE = LOI_Unit.CDEPT_CODE;
                poEntity.CPROPERTY_ID = LOI_Unit.CPROPERTY_ID;
                var loResult = await _PMT01300InitModel.GetAllAgreementChargeCallUnitListAsync(poEntity);

                ChargeCalUnitDisplayGrid = new ObservableCollection<PMT01300AgreementChargeCalUnitDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetLOICharge(PMT01330DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CDEPT_CODE = LOI_Unit.CDEPT_CODE;
                var loResult = await _PMT01330Model.R_ServiceGetRecordAsync(poEntity);

                if (DateTime.TryParseExact(loResult.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldStartDate))
                {
                    loResult.DSTART_DATE = ldStartDate;
                }
                else
                {
                    loResult.DSTART_DATE = null;
                }
                if (DateTime.TryParseExact(loResult.CEND_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldEndDate))
                {
                    loResult.DEND_DATE = ldEndDate;
                }
                else
                {
                    loResult.DEND_DATE = null;
                }

                if (VAR_PERIOD_MODE.Count > 0 && !string.IsNullOrWhiteSpace(loResult.CINVOICE_PERIOD))
                    loResult.CINVOICE_PERIOD_DESCR = VAR_PERIOD_MODE.FirstOrDefault(x => x.CCODE == loResult.CINVOICE_PERIOD).CDESCRIPTION;
                loResult.CCHARGES_ID_NAME = loResult.CCHARGES_NAME + " (" + loResult.CCHARGES_ID + ")";

                LOI_Charge = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveLOICharge(PMT01330DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CPROPERTY_ID = LOI_Unit.CPROPERTY_ID;
                poEntity.CDEPT_CODE = LOI_Unit.CDEPT_CODE;
                poEntity.CREF_NO = LOI_Unit.CREF_NO;
                poEntity.CCHARGE_MODE = LOI_Unit.CCHARGE_MODE;
                poEntity.CBUILDING_ID = LOI_Unit.CBUILDING_ID;
                poEntity.CFLOOR_ID = LOI_Unit.CFLOOR_ID;
                poEntity.CUNIT_ID = LOI_Unit.CUNIT_ID;

                poEntity.CSTART_DATE = poEntity.DSTART_DATE != null ? poEntity.DSTART_DATE.Value.ToString("yyyyMMdd") : "";
                poEntity.CEND_DATE = poEntity.DEND_DATE != null ? poEntity.DEND_DATE.Value.ToString("yyyyMMdd") : "";

                var loResult = await _PMT01330Model.R_ServiceSaveAsync(poEntity, poCRUDMode);
                if (VAR_PERIOD_MODE.Count > 0 && !string.IsNullOrWhiteSpace(loResult.CINVOICE_PERIOD))
                    loResult.CINVOICE_PERIOD_DESCR = VAR_PERIOD_MODE.FirstOrDefault(x => x.CCODE == loResult.CINVOICE_PERIOD).CDESCRIPTION;

                loResult.CCHARGES_ID_NAME = loResult.CCHARGES_NAME + " (" + loResult.CCHARGES_ID + ")";
                if (DateTime.TryParseExact(loResult.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldStartDate))
                {
                    loResult.DSTART_DATE = ldStartDate;
                }
                else
                {
                    loResult.DSTART_DATE = null;
                }
                if (DateTime.TryParseExact(loResult.CEND_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldEndDate))
                {
                    loResult.DEND_DATE = ldEndDate;
                }
                else
                {
                    loResult.DEND_DATE = null;
                }

                LOI_Charge = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteLOICharge(PMT01330DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CPROPERTY_ID = LOI_Unit.CPROPERTY_ID;
                poEntity.CDEPT_CODE = LOI_Unit.CDEPT_CODE;
                poEntity.CREF_NO = LOI_Unit.CREF_NO;
                poEntity.CCHARGE_MODE = LOI_Unit.CCHARGE_MODE;
                poEntity.CBUILDING_ID = LOI_Unit.CBUILDING_ID;
                poEntity.CFLOOR_ID = LOI_Unit.CFLOOR_ID;
                poEntity.CUNIT_ID = LOI_Unit.CUNIT_ID;
                poEntity.CCHARGE_MODE = LOI_Unit.CCHARGE_MODE;
                await _PMT01330Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<PMT01330DTO> ActiveInactiveProcessAsync(object poParameter)
        {
            R_Exception loException = new R_Exception();
            PMT01330DTO loRtn = null;

            try
            {
                // set Status
                var loParamActive = R_FrontUtility.ConvertObjectToObject<PMT01330ActiveInactiveDTO>(poParameter);
                loParamActive.LACTIVE = StatusChange;

                await _PMT01330Model.ChangeStatusLOIChargesAsync(loParamActive);

                var loData = (PMT01330DTO)poParameter;
                var loResult = await _PMT01330Model.R_ServiceGetRecordAsync(loData);
                if (VAR_PERIOD_MODE.Count > 0)
                    loResult.CINVOICE_PERIOD_DESCR = VAR_PERIOD_MODE.FirstOrDefault(x => x.CCODE == loResult.CINVOICE_PERIOD).CDESCRIPTION;

                loResult.CCHARGES_ID_NAME = loResult.CCHARGES_NAME + " (" + loResult.CCHARGES_ID + ")";
                if (DateTime.TryParseExact(loResult.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldStartDate))
                {
                    loResult.DSTART_DATE = ldStartDate;
                }
                else
                {
                    loResult.DSTART_DATE = null;
                }
                if (DateTime.TryParseExact(loResult.CEND_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldEndDate))
                {
                    loResult.DEND_DATE = ldEndDate;
                }
                else
                {
                    loResult.DEND_DATE = null;
                }

                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}
