using PMT01700COMMON.DTO._2._LOO._2._LOO___Offer;
using PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Unit___Charges;
using PMT01700COMMON.DTO.Utilities;
using PMT01700COMMON.DTO.Utilities.Front;
using PMT01700COMMON.DTO.Utilities.ParamDb;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using PMT01700COMMON.DTO.Utilities.Response;
using PMT01700FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace PMT01700MODEL.ViewModel
{
    public class PMT01700LOO_OfferViewModel : R_ViewModel<PMT01700LOO_Offer_SelectedOfferDTO>
    {
        #region From Back
        private readonly PMT01700LOO_OfferModel _model = new PMT01700LOO_OfferModel();
        public PMT01700LOO_Offer_SelectedOfferDTO oEntity = new PMT01700LOO_Offer_SelectedOfferDTO();
        public List<PMT01700ComboBoxDTO> oComboBoxTaxType = new List<PMT01700ComboBoxDTO>();
        public List<PMT01700ComboBoxDTO> oComboBoxIdType = new List<PMT01700ComboBoxDTO>();
        public List<PMT01700ResponseTenantCategoryDTO> oComboBoxTenantCategory = new List<PMT01700ResponseTenantCategoryDTO>();
        public PMT01700VarGsmTransactionCodeDTO oVarGSMTransactionCode = new PMT01700VarGsmTransactionCodeDTO();

        public PMT01700LOO_Offer_ExistingDataDTO oTempExistingEntity = new PMT01700LOO_Offer_ExistingDataDTO();
        public PMT01700LOO_Offer_ExistingDataDTO oTempExistingEntityBackUp = new PMT01700LOO_Offer_ExistingDataDTO();
        #endregion
        #region For Front
        public List<PMT01700LOO_Offer_SelectedOtherDataUnitListDTO>? TempDataUnitList = new List<PMT01700LOO_Offer_SelectedOtherDataUnitListDTO>();

        public PMT01700ParameterFrontChangePageDTO oParameter = new PMT01700ParameterFrontChangePageDTO();
        //  public PMT01100UtilitiesTempFirstDataOfferDTO oTempFirstDataDefault = new PMT01100UtilitiesTempFirstDataOfferDTO();
        public PMT01700ControlYMD oControlYMD = new PMT01700ControlYMD();

        public bool lControlCRUDMode = true;
        public bool lControlExistingTenant = true;
        public bool lControlExistingTenantOriginal = false;
        public bool lControlButtonRedraft;
        public bool lControlButtonSubmit;
        public bool lControlButtonPrint;

        public PMT01700LOO_Offer_TenantDetailDTO TenantDetail = new PMT01700LOO_Offer_TenantDetailDTO();
        public List<PMT01700ComboBoxDTO> oDataChoose { get; set; } =
            new List<PMT01700ComboBoxDTO>
            {
                new PMT01700ComboBoxDTO
                { CCODE = "1", CDESCRIPTION = R_FrontUtility.R_GetMessage(typeof(Resources_PMT01700_Class), "Existing Tenant*")  },
                new PMT01700ComboBoxDTO
                { CCODE = "2", CDESCRIPTION = R_FrontUtility.R_GetMessage(typeof(Resources_PMT01700_Class), "New Prospect") },
            };

        public string cDataChoosen = "1";

        #endregion
        #region Core
        public async Task GetEntity(PMT01700LOO_Offer_SelectedOfferDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                PMT01700LOO_Offer_SelectedOfferDTO loResult = await _model.R_ServiceGetRecordAsync(poEntity);

                if (!string.IsNullOrEmpty(loResult.CREF_NO))
                {
                    loResult.DEXPIRED_DATE = ConvertStringToDateTimeFormat(loResult.CEXPIRED_DATE);
                    loResult.DHAND_OVER_DATE = ConvertStringToDateTimeFormat(loResult.CHAND_OVER_DATE);
                    loResult.DID_EXPIRED_DATE = ConvertStringToDateTimeFormat(loResult.CID_EXPIRED_DATE);
                    loResult.DREF_DATE = ConvertStringToDateTimeFormat(loResult.CREF_DATE);
                    
                    loResult.DSTART_DATE = MergeDate(loResult.CSTART_DATE, loResult.CSTART_TIME);
                    loResult.DEND_DATE = MergeDate(loResult.CEND_DATE, loResult.CEND_TIME);

                    oEntity = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ServiceDelete(PMT01700LOO_Offer_SelectedOfferDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Validation Before Delete
                await _model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ServiceSave(PMT01700LOO_Offer_SelectedOfferDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                if (eCRUDMode.AddMode == peCRUDMode)
                {
                    poNewEntity.CPROPERTY_ID = oParameter.CPROPERTY_ID;
                    poNewEntity.CTRANS_CODE = "802043";
                }


                poNewEntity.CMODE_CRUD = cDataChoosen;
                var iDataChoosen = int.Parse(cDataChoosen);
                ////For Checking is needed safe UnitList or not
                poNewEntity.CMODE_CRUD = TempDataUnitList.Count() > 0 ? (iDataChoosen + 2).ToString() : iDataChoosen.ToString();

                if (poNewEntity.CMODE_CRUD == "1" || poNewEntity.CMODE_CRUD == "3")
                {
                    poNewEntity.CTENANT_ID = oTempExistingEntity.CTENANT_ID;
                    poNewEntity.CTENANT_NAME = oTempExistingEntity.CTENANT_NAME;

                }
                if (TempDataUnitList.Any())
                {
                    TempDataUnitList.ForEach(unit =>
                    {
                        unit.CREF_NO = poNewEntity.CREF_NO;
                        unit.CDEPT_CODE = poNewEntity.CDEPT_CODE;
                        unit.CBUILDING_ID = poNewEntity.CBUILDING_ID;
                    });
                    poNewEntity.ODATA_UNIT_LIST = TempDataUnitList;
                }

                poNewEntity.CFOLLOW_UP_DATE = ConvertDateTimeToStringFormat(poNewEntity.DFOLLOW_UP_DATE);
                poNewEntity.CEXPIRED_DATE = ConvertDateTimeToStringFormat(poNewEntity.DEXPIRED_DATE);
                poNewEntity.CHAND_OVER_DATE = ConvertDateTimeToStringFormat(poNewEntity.DHAND_OVER_DATE);
                poNewEntity.CID_EXPIRED_DATE = ConvertDateTimeToStringFormat(poNewEntity.DID_EXPIRED_DATE);
                //poNewEntity.CDOC_DATE = ConvertDateTimeToStringFormat(poNewEntity.DDOC_DATE);
                poNewEntity.CSTART_DATE = ConvertDateTimeToStringFormat(poNewEntity.DSTART_DATE);
                poNewEntity.CEND_DATE = ConvertDateTimeToStringFormat(poNewEntity.DEND_DATE);
                poNewEntity.CREF_DATE = ConvertDateTimeToStringFormat(poNewEntity.DREF_DATE);

                poNewEntity.CSTART_TIME = ConvertTimeToStringFormat(poNewEntity.DSTART_DATE);
                poNewEntity.CEND_TIME = ConvertTimeToStringFormat(poNewEntity.DEND_DATE);

                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                loResult.DFOLLOW_UP_DATE = ConvertStringToDateTimeFormat(loResult.CFOLLOW_UP_DATE);
                loResult.DEXPIRED_DATE = ConvertStringToDateTimeFormat(loResult.CEXPIRED_DATE);
                loResult.DHAND_OVER_DATE = ConvertStringToDateTimeFormat(loResult.CHAND_OVER_DATE);
                loResult.DID_EXPIRED_DATE = ConvertStringToDateTimeFormat(loResult.CID_EXPIRED_DATE);
                loResult.DREF_DATE = ConvertStringToDateTimeFormat(loResult.CREF_DATE);

                loResult.DSTART_DATE = MergeDate(loResult.CSTART_DATE, loResult.CSTART_TIME);
                loResult.DEND_DATE = MergeDate(loResult.CEND_DATE, loResult.CEND_TIME);

                oEntity = loResult;

                switch (oEntity.CTRANS_STATUS)
                {
                    case "00":
                        lControlButtonRedraft = false;
                        lControlButtonSubmit = true;
                        break;
                    case "30":
                        lControlButtonRedraft = lControlButtonSubmit = false;
                        break;
                    case "10":
                        lControlButtonSubmit = false;
                        lControlButtonRedraft = true;
                        break;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion
        public async Task GetTenantDetail(PMT01700LOO_Offer_TenantParamDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetTenantDetailAsync(poParam: poParameter);
                TenantDetail = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task<PMT01700LOO_Offer_SelectedOfferDTO> GetAgreementDetail(PMT01700LOO_Offer_SelectedOfferDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            PMT01700LOO_Offer_SelectedOfferDTO? loReturn = null;
            try
            {
                var loResult = await _model.GetAgreementDetailAsync(poParam: poParameter);
                loReturn = (!string.IsNullOrEmpty(loResult.CREF_NO)) ? loResult : new PMT01700LOO_Offer_SelectedOfferDTO();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loReturn!;
        }
        public async Task GetVAR_GSM_TRANSACTION_CODE()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetVAR_GSM_TRANSACTION_CODEAsync();
                oVarGSMTransactionCode = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetComboBoxDataTenantCategory()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(oParameter.CPROPERTY_ID))
                {
                    PMT01700BaseParameterDTO poParameter = new PMT01700BaseParameterDTO()
                    {
                        CPROPERTY_ID = oParameter.CPROPERTY_ID,
                    };
                    var loResult = await _model.GetComboBoxDataTenantCategoryAsync(poParam: poParameter);

                    oComboBoxTenantCategory = new List<PMT01700ResponseTenantCategoryDTO>(loResult.Data);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetComboBoxDataTaxType()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetComboBoxDataTaxTypeAsync();
                oComboBoxTaxType = new List<PMT01700ComboBoxDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetComboBoxDataIDTypeAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetComboBoxDataIDTypeAsync();
                oComboBoxIdType = new List<PMT01700ComboBoxDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region Utilities

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
        private TimeSpan? ConvertStringToTimeSpanFormat(string? pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                // Jika string kosong atau null, kembalikan null
                return null;
            }
            else
            {
                // Parse string ke TimeSpan
                TimeSpan result;
                if (TimeSpan.TryParseExact(pcEntity, "hh\\:mm", CultureInfo.InvariantCulture, out result))
                {
                    // Kembalikan TimeSpan jika parsing berhasil
                    return result;
                }
                else
                {
                    // Jika parsing gagal, kembalikan null
                    return null;
                }
            }
        }

        private string? ConvertTimeToStringFormat(DateTime? ptEntity)
        {
            if (!ptEntity.HasValue)
            {
                // Jika ptEntity null, kembalikan null
                return null;
            }
            else
            {
                // Format DateTime ke string "HH:mm" (jam dan menit)
                return ptEntity.Value.ToString("HH:mm");
            }
        }

        private DateTime? MergeDate (string? ParamDate, string? ParamTime)
        {
            DateTime? loTempDate = ConvertStringToDateTimeFormat(ParamDate);
            TimeSpan? loTempTime = ConvertStringToTimeSpanFormat(ParamTime);
            DateTime? lomergetDate = null;

            if (loTempTime.HasValue && loTempDate.HasValue)
            {
                lomergetDate = loTempDate.Value.Add(loTempTime.Value);
            }
            else if (loTempDate.HasValue)
            {
                lomergetDate = loTempDate.Value;
            }
            return lomergetDate;
        }
        #endregion
    }
}
