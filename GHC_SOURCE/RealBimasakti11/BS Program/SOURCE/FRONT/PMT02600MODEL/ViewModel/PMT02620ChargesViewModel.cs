using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using PMT02600COMMON.DTOs.PMT02620;
using PMT01400COMMON.DTOs.Helper;
using PMT02600COMMON.DTOs.Helper;

namespace PMT02600MODEL.ViewModel
{
    public class PMT02620ChargesViewModel : R_ViewModel<PMT02620ChargesDTO>
    {
        private PMT02620ChargesModel _PMT02620ChargesModel = new PMT02620ChargesModel();
        //private PMT01300InitModel _PMT01300InitModel = new PMT01300InitModel();

        #region Property Class
        //public PMT02620UnitDTO Agreement_Unit { get; set; } = new PMT02620UnitDTO();
        public PMT02620ChargesDTO loCharge { get; set; } = new PMT02620ChargesDTO();
        public ObservableCollection<PMT02620ChargesDTO> AgreementChargeGrid { get; set; } = new ObservableCollection<PMT02620ChargesDTO>();
        public ObservableCollection<PMT02620AgreementChargeCalUnitDTO> ChargeCalUnitGrid { get; set; } = new ObservableCollection<PMT02620AgreementChargeCalUnitDTO>();
        public List<CodeDescDTO> VAR_FEE_METHOD { get; set; } = new List<CodeDescDTO>();
        public PMT02620UnitDTO loUnitParameter = new PMT02620UnitDTO();
        #endregion

        #region ComboBox ViewModel
        public List<KeyValuePair<string, string>> BillingModeList { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("01", R_FrontUtility.R_GetMessage(typeof(PMT02600FrontResources.Resources_Dummy_Class), "_Dp")),
            new KeyValuePair<string, string>("02", R_FrontUtility.R_GetMessage(typeof(PMT02600FrontResources.Resources_Dummy_Class), "_ByPeriod")),
        };
        #endregion
        public async Task GetInitialVar()
        {
            var loEx = new R_Exception();
            try
            {
                VAR_FEE_METHOD = await _PMT02620ChargesModel.GetFeeMethodStreamAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetAgreementChargeList()
        {
            var loEx = new R_Exception();

            try
            {
                PMT02620ChargesDTO loParam = R_FrontUtility.ConvertObjectToObject<PMT02620ChargesDTO>(loUnitParameter);
                var loResult = await _PMT02620ChargesModel.GetAgreementChargeStreamAsync(loParam);
                loResult.ForEach(x =>
                {
                    x.CCHARGES_ID_NAME = x.CCHARGES_NAME + " (" + x.CCHARGES_ID + ")";
                    x.DSTART_DATE = DateTime.ParseExact(x.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                    x.DEND_DATE = DateTime.ParseExact(x.CEND_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                });

                AgreementChargeGrid = new ObservableCollection<PMT02620ChargesDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetChargeCallUnitList(PMT02620ParameterAgreementChargeCalUnitDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CTRANS_CODE = loUnitParameter.CTRANS_CODE;
                poEntity.CDEPT_CODE = loUnitParameter.CDEPT_CODE;
                poEntity.CPROPERTY_ID = loUnitParameter.CPROPERTY_ID;
                poEntity.CREF_NO = loUnitParameter.CREF_NO;
                var loResult = await _PMT02620ChargesModel.GetAllAgreementChargeCallUnitListStreamAsync(poEntity);

                ChargeCalUnitGrid = new ObservableCollection<PMT02620AgreementChargeCalUnitDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetAgreementCharge(PMT02620ChargesDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CDEPT_CODE = loUnitParameter.CDEPT_CODE;
                poEntity.CTRANS_CODE = loUnitParameter.CTRANS_CODE;
                var loResult = await _PMT02620ChargesModel.R_ServiceGetRecordAsync(poEntity);

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

                loCharge = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveAgreementCharge(PMT02620ChargesDTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CTRANS_CODE = loUnitParameter.CTRANS_CODE;
                poEntity.CPROPERTY_ID = loUnitParameter.CPROPERTY_ID;
                poEntity.CDEPT_CODE = loUnitParameter.CDEPT_CODE;
                poEntity.CREF_NO = loUnitParameter.CREF_NO;
                poEntity.CSTART_DATE = poEntity.DSTART_DATE.Value.ToString("yyyyMMdd");
                poEntity.CEND_DATE = poEntity.DEND_DATE.Value.ToString("yyyyMMdd");

                var loResult = await _PMT02620ChargesModel.R_ServiceSaveAsync(poEntity, poCRUDMode);

                loCharge = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteAgreementCharge(PMT02620ChargesDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                //poEntity.CTRANS_CODE = 
                await _PMT02620ChargesModel.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
