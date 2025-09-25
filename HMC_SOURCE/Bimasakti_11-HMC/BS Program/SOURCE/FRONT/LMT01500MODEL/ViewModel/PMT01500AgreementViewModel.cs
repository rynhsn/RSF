using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using PMT01500Common.DTO._2._Agreement;
using PMT01500Common.Utilities;
using PMT01500Common.Utilities.Front;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace PMT01500Model.ViewModel
{
    public class PMT01500AgreementViewModel : R_ViewModel<PMT01500FrontAgreementDetailDTO>
    {
        #region From Back
        private readonly PMT01500AgreementModel _modelPMT01500AgreementModel = new PMT01500AgreementModel();
        public PMT01500FrontAgreementDetailDTO loEntityPMT01500AgreementDetail = new PMT01500FrontAgreementDetailDTO();

        public List<PMT01500ComboBoxDTO> loComboBoxDataCLeaseMode { get; set; } = new List<PMT01500ComboBoxDTO>();
        public List<PMT01500ComboBoxDTO> loComboBoxDataCChargesMode { get; set; } = new List<PMT01500ComboBoxDTO>();
        #endregion

        #region For Front
        public PMT01500GetHeaderParameterDTO loParameter = new PMT01500GetHeaderParameterDTO();
        public PMT01500ControlYMD _oControlYMD = new PMT01500ControlYMD();
        #endregion

        #region Agreement
        public async Task GetEntity(PMT01500FrontAgreementDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _modelPMT01500AgreementModel.R_ServiceGetRecordAsync(poEntity: ConvertToEntityBack(poEntity));
                loEntityPMT01500AgreementDetail = ConvertToEntityFront(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT01500FrontAgreementDetailDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                if (eCRUDMode.AddMode == peCRUDMode)
                {

                }

                var loResult = await _modelPMT01500AgreementModel.R_ServiceSaveAsync(ConvertToEntityBack(poNewEntity), peCRUDMode);

                loEntityPMT01500AgreementDetail = ConvertToEntityFront(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(PMT01500FrontAgreementDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Validation Before Delete
                await _modelPMT01500AgreementModel.R_ServiceDeleteAsync(ConvertToEntityBack(poEntity));
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
                var loResult = await _modelPMT01500AgreementModel.GetComboBoxDataCLeaseModeAsync();
                loComboBoxDataCLeaseMode = new List<PMT01500ComboBoxDTO>(loResult);
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
                var loResult = await _modelPMT01500AgreementModel.GetComboBoxDataCChargesModeAsync();
                loComboBoxDataCChargesMode = new List<PMT01500ComboBoxDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Utilities

        private PMT01500FrontAgreementDetailDTO ConvertToEntityFront(PMT01500AgreementDetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            PMT01500FrontAgreementDetailDTO? loReturn = null;

            try
            {
                if (poEntity != null)
                {
                    loReturn = R_FrontUtility.ConvertObjectToObject<PMT01500FrontAgreementDetailDTO>(poEntity);
                    loReturn.DREF_DATE = ConvertStringToDateTimeFormat(poEntity.CREF_DATE!);
                    loReturn.DDOC_DATE = ConvertStringToDateTimeFormat(poEntity.CDOC_DATE!);
                    loReturn.DSTART_DATE = ConvertStringToDateTimeFormat(poEntity.CSTART_DATE!);
                    loReturn.DEND_DATE = ConvertStringToDateTimeFormat(poEntity.CEND_DATE!);
                    loReturn.IYEAR = string.IsNullOrEmpty(poEntity.CYEAR) ? 0 : (int.TryParse(poEntity.CYEAR, out int parsedYear) ? parsedYear : 0);
                    loReturn.IMONTH = string.IsNullOrEmpty(poEntity.CMONTH) ? 0 : (int.TryParse(poEntity.CMONTH, out int parsedMonth) ? parsedMonth : 0);
                    loReturn.IDAY = string.IsNullOrEmpty(poEntity.CDAY) ? 0 : (int.TryParse(poEntity.CDAY, out int parsedDay) ? parsedDay : 0);
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

        private PMT01500AgreementDetailDTO ConvertToEntityBack(PMT01500FrontAgreementDetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            PMT01500AgreementDetailDTO? loReturn = null;

            try
            {
                if (poEntity != null)
                {
                    loReturn = R_FrontUtility.ConvertObjectToObject<PMT01500AgreementDetailDTO>(poEntity);
                    loReturn.CREF_DATE = ConvertDateTimeToStringFormat(poEntity.DREF_DATE!);
                    loReturn.CDOC_DATE = ConvertDateTimeToStringFormat(poEntity.DDOC_DATE!);
                    loReturn.CSTART_DATE = ConvertDateTimeToStringFormat(poEntity.DSTART_DATE!);
                    loReturn.CEND_DATE = ConvertDateTimeToStringFormat(poEntity.DEND_DATE!);
                    loReturn.CYEAR = poEntity.IYEAR.ToString();
                    loReturn.CMONTH = poEntity.IMONTH.ToString();
                    loReturn.CDAY = poEntity.IDAY.ToString();
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