using PMT01100Common.DTO._1._Unit_List;
using PMT01100Common.DTO._2._LOO._2._LOO___Offer;
using PMT01100Common.Utilities;
using PMT01100Common.Utilities.Front;
using PMT01100Common.Utilities.Request;
using PMT01100Common.Utilities.Response;
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

namespace PMT01100Model.ViewModel
{
    public class PMT01100LOO_OfferViewModel : R_ViewModel<PMT01100LOO_Offer_SelectedOfferDTO>
    {
        #region From Back
        private readonly PMT01100LOO_OfferModel _model = new PMT01100LOO_OfferModel();
        public PMT01100LOO_Offer_SelectedOfferDTO oEntity = new PMT01100LOO_Offer_SelectedOfferDTO();
        public PMT01100TempExistingDataOfferDTO oTempExistingEntity = new PMT01100TempExistingDataOfferDTO();
        public List<PMT01100ResponseTenantCategoryDTO> oComboBoxTenantCategory = new List<PMT01100ResponseTenantCategoryDTO>();
        public List<PMT01100ComboBoxDTO> oComboBoxTaxType = new List<PMT01100ComboBoxDTO>();
        public List<PMT01100ComboBoxDTO> oComboBoxIdType = new List<PMT01100ComboBoxDTO>();
        public PMT01100VarGsmTransactionCodeDTO oVarGSMTransactionCode = new PMT01100VarGsmTransactionCodeDTO();
        #endregion

        #region For Front
        public List<PMT01100LOO_Offer_SelectedDataUnitListDTO>? TempDataUnitList = new List<PMT01100LOO_Offer_SelectedDataUnitListDTO>();
        public R_ILocalizer<PMT01100FrontResources.Resources_PMT01100_Class>? _localizer { get; set; }
        public PMT01100ParameterFrontChangePageDTO oParameter = new PMT01100ParameterFrontChangePageDTO();
        public PMT01100UtilitiesTempFirstDataOfferDTO oTempFirstDataDefault = new PMT01100UtilitiesTempFirstDataOfferDTO();
        public bool lControlCRUDMode = true;
        public bool lControlExistingTenant = true;
        public PMT01100ControlYMD oControlYMD = new PMT01100ControlYMD();

        public List<PMT01100ComboBoxDTO> oDataChoose { get; set; } =
            new List<PMT01100ComboBoxDTO>
            {
                new PMT01100ComboBoxDTO
                { CCODE = "1", CDESCRIPTION = R_FrontUtility.R_GetMessage(typeof(PMT01100FrontResources.Resources_PMT01100_Class), "Existing Tenant*")  },
                new PMT01100ComboBoxDTO
                { CCODE = "2", CDESCRIPTION = R_FrontUtility.R_GetMessage(typeof(PMT01100FrontResources.Resources_PMT01100_Class), "New Prospect") },
            };

        public string cDataChoosen = "1";

        #endregion

        #region LOO - Offer

        #region Core

        public async Task GetEntity(PMT01100LOO_Offer_SelectedOfferDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.R_ServiceGetRecordAsync(poEntity);

                loResult.IMONTH = string.IsNullOrEmpty(loResult.CMONTH) ? 0 : int.Parse(loResult.CMONTH);
                loResult.IDAYS = string.IsNullOrEmpty(loResult.CDAYS) ? 0 : int.Parse(loResult.CDAYS);
                loResult.IYEAR = string.IsNullOrEmpty(loResult.CYEAR) ? 0 : int.Parse(loResult.CYEAR);
                loResult.DFOLLOW_UP_DATE = ConvertStringToDateTimeFormat(loResult.CFOLLOW_UP_DATE);
                loResult.DEXPIRED_DATE = ConvertStringToDateTimeFormat(loResult.CEXPIRED_DATE);
                loResult.DHAND_OVER_DATE = ConvertStringToDateTimeFormat(loResult.CHAND_OVER_DATE);
                loResult.DID_EXPIRED_DATE = ConvertStringToDateTimeFormat(loResult.CID_EXPIRED_DATE);
                //loResult.DDOC_DATE = ConvertStringToDateTimeFormat(loResult.CDOC_DATE);
                loResult.DSTART_DATE = ConvertStringToDateTimeFormat(loResult.CSTART_DATE);
                loResult.DEND_DATE = ConvertStringToDateTimeFormat(loResult.CEND_DATE);
                loResult.DREF_DATE = ConvertStringToDateTimeFormat(loResult.CREF_DATE);

                oEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT01100LOO_Offer_SelectedOfferDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                if (eCRUDMode.AddMode == peCRUDMode)
                {
                    poNewEntity.CPROPERTY_ID = oParameter.CPROPERTY_ID;
                }


                poNewEntity.CMODE_CRUD = cDataChoosen;
                var iDataChoosen = int.Parse(cDataChoosen);
                //For Checking is needed safe UnitList or not
                poNewEntity.CMODE_CRUD = TempDataUnitList != null ? (iDataChoosen + 2).ToString() : iDataChoosen.ToString();

                if (poNewEntity.CMODE_CRUD == "1" || poNewEntity.CMODE_CRUD == "3")
                {
                    poNewEntity.CTENANT_ID = oTempExistingEntity.CTENANT_ID;
                    poNewEntity.CTENANT_NAME = oTempExistingEntity.CTENANT_NAME;

                    if (TempDataUnitList.Any())
                    {
                        TempDataUnitList.ForEach(unit =>
                        {
                            unit.CREF_NO = poNewEntity.CREF_NO;
                            unit.CBUILDING_ID = poNewEntity.CBUILDING_ID;
                        });
                        poNewEntity.ODATA_UNIT_LIST = TempDataUnitList;
                    }
                }

                poNewEntity.CDAYS = poNewEntity.IDAYS.ToString();
                poNewEntity.CMONTH = poNewEntity.IMONTH.ToString();
                poNewEntity.CYEAR = poNewEntity.IYEAR.ToString();
                poNewEntity.CFOLLOW_UP_DATE = ConvertDateTimeToStringFormat(poNewEntity.DFOLLOW_UP_DATE);
                poNewEntity.CEXPIRED_DATE = ConvertDateTimeToStringFormat(poNewEntity.DEXPIRED_DATE);
                poNewEntity.CHAND_OVER_DATE = ConvertDateTimeToStringFormat(poNewEntity.DHAND_OVER_DATE);
                poNewEntity.CID_EXPIRED_DATE = ConvertDateTimeToStringFormat(poNewEntity.DID_EXPIRED_DATE);
                //poNewEntity.CDOC_DATE = ConvertDateTimeToStringFormat(poNewEntity.DDOC_DATE);
                poNewEntity.CSTART_DATE = ConvertDateTimeToStringFormat(poNewEntity.DSTART_DATE);
                poNewEntity.CEND_DATE = ConvertDateTimeToStringFormat(poNewEntity.DEND_DATE);
                poNewEntity.CREF_DATE = ConvertDateTimeToStringFormat(poNewEntity.DREF_DATE);

                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);


                loResult.IMONTH = string.IsNullOrEmpty(loResult.CMONTH) ? 0 : int.Parse(loResult.CMONTH);
                loResult.IDAYS = string.IsNullOrEmpty(loResult.CDAYS) ? 0 : int.Parse(loResult.CDAYS);
                loResult.IYEAR = string.IsNullOrEmpty(loResult.CYEAR) ? 0 : int.Parse(loResult.CYEAR);
                loResult.DFOLLOW_UP_DATE = ConvertStringToDateTimeFormat(loResult.CFOLLOW_UP_DATE);
                loResult.DEXPIRED_DATE = ConvertStringToDateTimeFormat(loResult.CEXPIRED_DATE);
                loResult.DHAND_OVER_DATE = ConvertStringToDateTimeFormat(loResult.CHAND_OVER_DATE);
                loResult.DID_EXPIRED_DATE = ConvertStringToDateTimeFormat(loResult.CID_EXPIRED_DATE);
                //loResult.DDOC_DATE = ConvertStringToDateTimeFormat(loResult.CDOC_DATE);
                loResult.DSTART_DATE = ConvertStringToDateTimeFormat(loResult.CSTART_DATE);
                loResult.DEND_DATE = ConvertStringToDateTimeFormat(loResult.CEND_DATE);
                loResult.DREF_DATE = ConvertStringToDateTimeFormat(loResult.CREF_DATE);

                oEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(PMT01100LOO_Offer_SelectedOfferDTO poEntity)
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

        #endregion

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
                    PMT01100RequestTenantCategoryDTO poParameter = new PMT01100RequestTenantCategoryDTO()
                    {
                        CPROPERTY_ID = oParameter.CPROPERTY_ID,
                    };

                    var loResult = await _model.GetComboBoxDataTenantCategoryAsync(poParameter: poParameter);
                    oComboBoxTenantCategory = new List<PMT01100ResponseTenantCategoryDTO>(loResult.Data);
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
                oComboBoxTaxType = new List<PMT01100ComboBoxDTO>(loResult.Data);
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
                oComboBoxIdType = new List<PMT01100ComboBoxDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion


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

        #endregion
    }
}
