using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Threading.Tasks;
using BaseAOC_BS11Common.DTO.Request.Request.Single;
using BaseAOC_BS11Common.DTO.Response.Single;
using BaseAOC_BS11Common.Service;
using BaseAOC_BS11Model;
using PMT02500Common.DTO._2._Agreement;
using PMT02500Common.Utilities;
using PMT02500Common.Utilities.Front;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace PMT02500Model.ViewModel
{
    public class PMT02500AgreementViewModel : R_ViewModel<PMT02500FrontAgreementDetailDTO>
    {
        #region From Back
        private readonly BaseAOCGetSingleDataModel _baseSingleDataModel = new BaseAOCGetSingleDataModel();
        private readonly PMT02500AgreementModel _modelPMT02500AgreementModel = new PMT02500AgreementModel();
        public PMT02500FrontAgreementDetailDTO loEntityPMT02500AgreementDetail = new PMT02500FrontAgreementDetailDTO();

        public List<PMT02500ComboBoxDTO> loComboBoxDataCLeaseMode { get; set; } = new List<PMT02500ComboBoxDTO>();
        public List<PMT02500ComboBoxDTO> loComboBoxDataCChargesMode { get; set; } = new List<PMT02500ComboBoxDTO>();

        #endregion

        #region For Front
        public PMT02500GetHeaderParameterDTO loParameter = new PMT02500GetHeaderParameterDTO();
        public BaseAOCFunctionUtility _AOCService = new BaseAOCFunctionUtility();
        public PMT02500ControlYMD _oControlYMD = new PMT02500ControlYMD();

        public bool lControlButtonSubmit = false;
        public bool lControlButtonRedraft = false;

        public bool lControlCRUDMode = true;
        #endregion

        #region Agreement
        public async Task GetEntity(PMT02500FrontAgreementDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _modelPMT02500AgreementModel.R_ServiceGetRecordAsync(poEntity: ConvertToEntityBack(poEntity));
                loEntityPMT02500AgreementDetail = ConvertToEntityFront(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT02500FrontAgreementDetailDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                if (eCRUDMode.AddMode == peCRUDMode)
                {

                }

                var loResult = await _modelPMT02500AgreementModel.R_ServiceSaveAsync(ConvertToEntityBack(poNewEntity), peCRUDMode);

                loEntityPMT02500AgreementDetail = ConvertToEntityFront(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(PMT02500FrontAgreementDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Validation Before Delete
                await _modelPMT02500AgreementModel.R_ServiceDeleteAsync(ConvertToEntityBack(poEntity));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetComboBoxDataCLeaseMode()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _modelPMT02500AgreementModel.GetComboBoxDataCLeaseModeAsync();
                loComboBoxDataCLeaseMode = new List<PMT02500ComboBoxDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetComboBoxDataCChargesMode()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _modelPMT02500AgreementModel.GetComboBoxDataCChargesModeAsync();
                loComboBoxDataCChargesMode = new List<PMT02500ComboBoxDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion


        #region Proses Update Status

        public async Task<BaseAOCResponseUpdateAgreementStatusDTO> ProsesUpdateAgreementStatus(string pcStatus)
        {
            BaseAOCResponseUpdateAgreementStatusDTO loReturn = new BaseAOCResponseUpdateAgreementStatusDTO();
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loEntityPMT02500AgreementDetail.CREF_NO))
                {
                    var loParam = new BaseAOCParameterRequestUpdateAgreementStatusDTO()
                    {
                        CPROPERTY_ID = loParameter.CPROPERTY_ID!,
                        CDEPT_CODE = loParameter.CDEPT_CODE!,
                        CTRANS_CODE = loParameter.CTRANS_CODE!,
                        CREF_NO = loEntityPMT02500AgreementDetail.CREF_NO,
                        CNEW_STATUS = pcStatus,

                    };

                    var loResult = await _baseSingleDataModel.ProsesUpdateAgreementStatusAsync(loParam);
                    loReturn = loResult;
                }
                else
                {
                    loReturn.LSUCCESS = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }


        public async Task<BaseAOCResponseGetAgreementDetailDTO> GetAgreementDetail(BaseAOCParameterRequestGetAgreementDetailDTO poParameter)
        {
            var loException = new R_Exception();
            BaseAOCResponseGetAgreementDetailDTO loReturn = new BaseAOCResponseGetAgreementDetailDTO();

            try
            {
                if (!string.IsNullOrEmpty(poParameter.CREF_NO))
                {
                    loReturn = await _baseSingleDataModel.GetAgreementDetailAsync(poParameter);
                }
                else
                {
                    loReturn = new BaseAOCResponseGetAgreementDetailDTO();
                }
            }
            catch (Exception e)
            {
                loException.Add(e);
            }

            loException.ThrowExceptionIfErrors();

            return loReturn;
        }


        #endregion

        #region Utilities

        private PMT02500FrontAgreementDetailDTO ConvertToEntityFront(PMT02500AgreementDetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            PMT02500FrontAgreementDetailDTO? loReturn = null;

            try
            {
                if (poEntity != null)
                {
                    loReturn = R_FrontUtility.ConvertObjectToObject<PMT02500FrontAgreementDetailDTO>(poEntity);
                    loReturn.DREF_DATE = ConvertStringToDateTimeFormat(poEntity.CREF_DATE!);
                    loReturn.DDOC_DATE = ConvertStringToDateTimeFormat(poEntity.CDOC_DATE!);
                    loReturn.DSTART_DATE = ConvertStringToDateTimeFormat(poEntity.CSTART_DATE!);
                    loReturn.DEND_DATE = ConvertStringToDateTimeFormat(poEntity.CEND_DATE!);
                    loReturn.DFOLLOW_UP_DATE = ConvertStringToDateTimeFormat(poEntity.CFOLLOW_UP_DATE!);
                    loReturn.DHO_PLAN_DATE = ConvertStringToDateTimeFormat(poEntity.CHO_PLAN_DATE!);
                    loReturn.DHO_ACTUAL_DATE = ConvertStringToDateTimeFormat(poEntity.CHO_ACTUAL_DATE!);
                    loReturn.DHAND_OVER_DATE = ConvertStringToDateTimeFormat(poEntity.CHAND_OVER_DATE!);
                    loReturn.CREF_NO = string.IsNullOrEmpty(poEntity.CREF_NO) ? "" : poEntity.CREF_NO;
                    loReturn.CDOC_NO = string.IsNullOrEmpty(poEntity.CDOC_NO) ? "" : poEntity.CDOC_NO;
                    loReturn.CNOTES = string.IsNullOrEmpty(poEntity.CNOTES) ? "" : poEntity.CNOTES;
                }
            }

            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loReturn!;
        }

        private PMT02500AgreementDetailDTO ConvertToEntityBack(PMT02500FrontAgreementDetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            PMT02500AgreementDetailDTO? loReturn = null;

            try
            {
                if (poEntity != null)
                {
                    loReturn = R_FrontUtility.ConvertObjectToObject<PMT02500AgreementDetailDTO>(poEntity);
                    loReturn.CREF_DATE = ConvertDateTimeToStringFormat(poEntity.DREF_DATE!);
                    loReturn.CDOC_DATE = ConvertDateTimeToStringFormat(poEntity.DDOC_DATE!);
                    loReturn.CSTART_DATE = ConvertDateTimeToStringFormat(poEntity.DSTART_DATE!);
                    loReturn.CEND_DATE = ConvertDateTimeToStringFormat(poEntity.DEND_DATE!);
                    loReturn.CFOLLOW_UP_DATE = ConvertDateTimeToStringFormat(poEntity.DFOLLOW_UP_DATE!);
                    loReturn.CHO_PLAN_DATE = ConvertDateTimeToStringFormat(poEntity.DHO_PLAN_DATE!);
                    loReturn.CHO_ACTUAL_DATE = ConvertDateTimeToStringFormat(poEntity.DHO_ACTUAL_DATE!);
                    loReturn.CEXPIRED_DATE = ConvertDateTimeToStringFormat(poEntity.DEXPIRED_DATE!);
                    loReturn.CHAND_OVER_DATE = ConvertDateTimeToStringFormat(poEntity.DHAND_OVER_DATE!);
                    loReturn.CREF_NO = string.IsNullOrEmpty(poEntity.CREF_NO) ? "" : poEntity.CREF_NO;
                    loReturn.CDOC_NO = string.IsNullOrEmpty(poEntity.CDOC_NO) ? "" : poEntity.CDOC_NO;
                    loReturn.CNOTES = string.IsNullOrEmpty(poEntity.CNOTES) ? "" : poEntity.CNOTES;
                }

            }

            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loReturn!;
        }

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