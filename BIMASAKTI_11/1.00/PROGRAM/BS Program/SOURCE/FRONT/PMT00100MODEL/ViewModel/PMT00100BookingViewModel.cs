using PMT00100COMMON.Booking;
using PMT00100COMMON.UnitList;
using PMT00100COMMON.UtilityDTO;
using PMT00100FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace PMT00100MODEL.ViewModel
{
    public class PMT00100BookingViewModel : R_ViewModel<PMT00100BookingDTO>
    {
        private PMT00100BookingModel _model = new PMT00100BookingModel();

        public PMT00100BookingDTO BookingParameter = new PMT00100BookingDTO();
        public PMT00100BookingDTO oEntityBooking = new PMT00100BookingDTO();
        public VarGsmTransactionCodeDTO VarTransaction = new VarGsmTransactionCodeDTO();
        public PMT00100AgreementByUnitDTO loEntityAgreementByUnit = new PMT00100AgreementByUnitDTO();
        public bool LEnaledEditDelete;
        public async Task<VarGsmTransactionCodeDTO> GetVarTransactionCode()
        {
            R_Exception loException = new R_Exception();
            VarGsmTransactionCodeDTO loResult = new VarGsmTransactionCodeDTO();
            try
            {
                loResult = await _model.GetVAR_GSM_TRANSACTION_CODEAsync();
                VarTransaction = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<PMT00100BookingDTO> GetEntity(PMT00100BookingDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            PMT00100BookingDTO loResult = new PMT00100BookingDTO();
            try
            {
                loResult = await _model.R_ServiceGetRecordAsync(poEntity);

                if (!string.IsNullOrEmpty(loResult.CREF_NO))
                {
                    loResult.DREF_DATE = ConvertStringToDateTimeFormat(loResult.CREF_DATE);
                    loResult.DHO_PLAN_DATE = ConvertStringToDateTimeFormat(loResult.CHO_PLAN_DATE);
                    oEntityBooking = loResult;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task ServiceSave(PMT00100BookingDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poNewEntity.CREF_DATE = ConvertDateTimeToStringFormat(poNewEntity.DREF_DATE);
                poNewEntity.CHO_PLAN_DATE = ConvertDateTimeToStringFormat(poNewEntity.DHO_PLAN_DATE);
                poNewEntity.CSTART_DATE = ConvertDateTimeToStringFormat(poNewEntity.DHO_PLAN_DATE);
                poNewEntity.CEND_DATE = "";
                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
                oEntityBooking = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ServiceDelete(PMT00100BookingDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<AgreementProcessDTO> ProcessUpdateAgreement(string lcNewStatus)
        {
            R_Exception loEx = new R_Exception();
            AgreementProcessDTO loReturn = new AgreementProcessDTO();
            try
            {
                if (!string.IsNullOrEmpty(loEntityAgreementByUnit.CREF_NO))
                {
                    AgreementProcessDTO currentAgreement = R_FrontUtility.ConvertObjectToObject<AgreementProcessDTO>(loEntityAgreementByUnit);
                    currentAgreement.CNEW_STATUS = lcNewStatus;

                    var loResult = await _model.UpdateAgreementAsync(currentAgreement);
                    loReturn = loResult;

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }
        #region Validation
        public void ValidationFieldEmpty(PMT00100BookingDTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                if (string.IsNullOrWhiteSpace(poEntity.CDEPT_CODE))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT00100_Class), "ValidationDepartment");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(poEntity.CTENANT_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT00100_Class), "ValidationTenant");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(poEntity.CSALESMAN_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT00100_Class), "ValidationSalesman");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(poEntity.CBILLING_RULE_CODE))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT00100_Class), "ValidationBillingRule");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(poEntity.CSTRATA_TAX_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT00100_Class), "ValidationTax");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(poEntity.CCURRENCY_CODE))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT00100_Class), "ValidationCurrency");
                    loEx.Add(loErr);
                }
                if (poEntity.NACTUAL_PRICE < 0)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT00100_Class), "ValidationActualPrice");
                    loEx.Add(loErr);
                }
                if (poEntity.NBOOKING_FEE < 0)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT00100_Class), "ValidationBookingFee");
                    loEx.Add(loErr);
                }
                if (poEntity.DHO_PLAN_DATE == null)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT00100_Class), "ValidationPlanHoDate");
                    loEx.Add(loErr);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        #region utilities
        private DateTime? ConvertStringToDateTimeFormat(string? pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                // Jika string kosong atau null, kembalikan DateTime.MinValue atau nilai default yang sesuai
                //return DateTime.MinValue; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
                return null;
            }
            else
            {
                // Parse string ke DateTime
                DateTime result;
                if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    return result;
                }
                else
                {
                    // Jika parsing gagal, kembalikan DateTime.MinValue atau nilai default yang sesuai
                    //return DateTime.MinValue; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
                    return null;
                }
            }
        }

        private string? ConvertDateTimeToStringFormat(DateTime? ptEntity)
        {
            if (!ptEntity.HasValue || ptEntity.Value == null)
            {
                // Jika ptEntity adalah null atau DateTime.MinValue, kembalikan null
                return null;
            }
            else
            {
                // Format DateTime ke string "yyyyMMdd"
                return ptEntity.Value.ToString("yyyyMMdd");
            }
        }
        #endregion

    }
}
