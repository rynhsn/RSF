using PMT00500COMMON;
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

namespace PMT00500MODEL
{
    public class PMT00530ViewModel : R_ViewModel<PMT00530DTO>
    {
        private PMT00530Model _PMT00530Model = new PMT00530Model();
        private PMT00500InitModel _PMT00500InitModel = new PMT00500InitModel();

        #region Property Class
        public PMT00500TenureConvertDTO oControlYMD { get; set; } = new PMT00500TenureConvertDTO();
        public PMT00510DTO LOI_Unit { get; set; } = new PMT00510DTO();
        public PMT00530DTO LOI_Charge { get; set; } = new PMT00530DTO();
        public ObservableCollection<PMT00530DTO> LOIChargeGrid { get; set; } = new  ObservableCollection<PMT00530DTO>();
        public ObservableCollection<PMT00500AgreementChargeCalUnitDTO> ChargeCalUnitGrid { get; set; } = new  ObservableCollection<PMT00500AgreementChargeCalUnitDTO>();
        public ObservableCollection<PMT00500AgreementChargeCalUnitDTO> ChargeCalUnitDisplayGrid { get; set; } = new  ObservableCollection<PMT00500AgreementChargeCalUnitDTO>();
        public List<PMT00500UniversalDTO> VAR_FEE_METHOD { get; set; } = new List<PMT00500UniversalDTO>();
        public List<PMT00500UniversalDTO> VAR_PERIOD_MODE { get; set; } = new List<PMT00500UniversalDTO>();
        private LinkedList<PMT00530DTO> LOIChargesList { get; set; } = new LinkedList<PMT00530DTO>();
        public bool StatusChange { get; set; }
        #endregion

        #region ComboBox ViewModel
        public List<KeyValuePair<string, string>> BillingModeList { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("01", R_FrontUtility.R_GetMessage(typeof(PMT00500FrontResources.Resources_Dummy_Class), "_Dp")),
            new KeyValuePair<string, string>("02", R_FrontUtility.R_GetMessage(typeof(PMT00500FrontResources.Resources_Dummy_Class), "_ByPeriod")),
        };
        #endregion
        public async Task GetInitialVar()
        {
            var loEx = new R_Exception();
            try
            {
                var loFeeMethodListResult = await _PMT00500InitModel.GetAllUniversalListAsync("_BS_FEE_METHOD");
                VAR_FEE_METHOD = loFeeMethodListResult;

                var loPeriodModeListResult = await _PMT00500InitModel.GetAllUniversalListAsync("_BS_INVOICE_PERIOD");
                VAR_PERIOD_MODE = loPeriodModeListResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetLOIChargeList(PMT00530DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT00530Model.GetLOIChargeStreamAsync(poEntity);
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

                LOIChargeGrid = new ObservableCollection<PMT00530DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetChargeCallUnitList(PMT00500ParameterAgreementChargeCalUnitDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CDEPT_CODE = LOI_Unit.CDEPT_CODE;
                poEntity.CPROPERTY_ID = LOI_Unit.CPROPERTY_ID;
                var loResult = await _PMT00500InitModel.GetAllAgreementChargeCallUnitListAsync(poEntity);

                ChargeCalUnitGrid = new ObservableCollection<PMT00500AgreementChargeCalUnitDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetChargeCallUnitListDisplay(PMT00500ParameterAgreementChargeCalUnitDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CDEPT_CODE = LOI_Unit.CDEPT_CODE;
                poEntity.CPROPERTY_ID = LOI_Unit.CPROPERTY_ID;
                var loResult = await _PMT00500InitModel.GetAllAgreementChargeCallUnitListAsync(poEntity);

                ChargeCalUnitDisplayGrid = new ObservableCollection<PMT00500AgreementChargeCalUnitDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetLOICharge(PMT00530DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CDEPT_CODE = LOI_Unit.CDEPT_CODE;
                var loResult = await _PMT00530Model.R_ServiceGetRecordAsync(poEntity);

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
        public async Task SaveLOICharge(PMT00530DTO poEntity, eCRUDMode poCRUDMode)
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

                var loResult = await _PMT00530Model.R_ServiceSaveAsync(poEntity, poCRUDMode);
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
        public async Task DeleteLOICharge(PMT00530DTO poEntity)
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
                await _PMT00530Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<PMT00530DTO> ActiveInactiveProcessAsync(object poParameter)
        {
            R_Exception loException = new R_Exception();
            PMT00530DTO loRtn = null;

            try
            {
                // set Status
                var loParamActive = R_FrontUtility.ConvertObjectToObject<PMT00530ActiveInactiveDTO>(poParameter);
                loParamActive.LACTIVE = StatusChange;

                await _PMT00530Model.ChangeStatusLOIChargesAsync(loParamActive);

                var loData = (PMT00530DTO)poParameter;
                var loResult = await _PMT00530Model.R_ServiceGetRecordAsync(loData);
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

        #region  Charges List
        public async Task GetLOIChargeListGrid(PMT00530DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT00530Model.GetLOIChargeStreamAsync(poEntity);

                foreach (var item in loResult)
                {
                    item.CUNIT_ID = poEntity.CUNIT_ID;
                    item.CBUILDING_ID = poEntity.CBUILDING_ID;
                    item.CFLOOR_ID = poEntity.CFLOOR_ID;
                    item.CCHARGES_ID_NAME = item.CCHARGES_NAME + " (" + item.CCHARGES_ID + ")";
                    item.DSTART_DATE = poEntity.DSTART_DATE;
                    item.DEND_DATE = poEntity.DEND_DATE;

                    LOIChargeGrid.Add(item);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task RemoveLOIChargeListGrid(PMT00530DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                if (poEntity.CCHARGE_MODE == "02")
                {
                    var loListData = LOIChargeGrid.Where(x => x.CUNIT_ID == poEntity.CUNIT_ID).ToList();

                    foreach (PMT00530DTO Item in loListData)
                    {
                        LOIChargeGrid.Remove(Item);
                    }
                }

                await Task.CompletedTask;   
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion
    }
}
