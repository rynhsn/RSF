using PMT01100Common.DTO._2._LOO._4._LOO___Deposit;
using PMT01100Common.Utilities.Db;
using PMT01100Common.Utilities.Front;
using PMT01100Common.Utilities.Request;
using PMT01100Common.Utilities.Response;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Helpers;
using System.Globalization;
using System.Linq;

namespace PMT01100Model.ViewModel
{
    public class PMT01100LOO_DepositViewModel : R_ViewModel<PMT01100LOO_Deposit_DepositDetailDTO>
    {
        #region From Back

        private readonly PMT01100LOO_DepositModel _model = new PMT01100LOO_DepositModel();
        public PMT01100LOO_Deposit_DepositDetailDTO oEntity = new PMT01100LOO_Deposit_DepositDetailDTO();
        public ObservableCollection<PMT01100LOO_Deposit_DepositListDTO> oListDeposit = new ObservableCollection<PMT01100LOO_Deposit_DepositListDTO>();
        public List<PMT01100ResponseUtilitiesCurrencyParameterDTO> oComboBoxDataCurrency = new List<PMT01100ResponseUtilitiesCurrencyParameterDTO>();
        public PMT01100LOO_Deposit_DepositHeaderDTO oHeaderEntity = new PMT01100LOO_Deposit_DepositHeaderDTO();

        #endregion

        #region For Front

        public PMT01100ParameterFrontChangePageDTO oParameter = new PMT01100ParameterFrontChangePageDTO();

        #endregion

        #region Program


        public async Task GetDepositHeader()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(oParameter.CPROPERTY_ID))
                {
                    PMT01100LOO_Deposit_RequestDepositHeaderDTO loParameter = R_FrontUtility.ConvertObjectToObject<PMT01100LOO_Deposit_RequestDepositHeaderDTO>(oParameter);

                    var loResult = await _model.GetDepositHeaderAsync(poParameter: loParameter);
                    oHeaderEntity = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetComboBoxDataCurrency()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetComboBoxDataCurrencyAsync();
                oComboBoxDataCurrency = new List<PMT01100ResponseUtilitiesCurrencyParameterDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetDepositList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(oParameter.CREF_NO))
                {
                    PMT01100UtilitiesParameterDepositListDTO loParameter = R_FrontUtility.ConvertObjectToObject<PMT01100UtilitiesParameterDepositListDTO>(oParameter);
                    var loResult = await _model.GetDepositListAsync(poParameter: loParameter);
                    if (loResult.Data.Any())
                    {
                        foreach (var item in loResult.Data)
                            item.DDEPOSIT_DATE = ConvertStringToDateTimeFormat(item.CDEPOSIT_DATE);
                    }
                    oListDeposit = new ObservableCollection<PMT01100LOO_Deposit_DepositListDTO>(loResult.Data);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        #region Core

        public async Task GetEntity(PMT01100LOO_Deposit_DepositDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                if (poEntity != null)
                {
                    var loResult = await _model.R_ServiceGetRecordAsync(poEntity);
                    loResult.DDEPOSIT_DATE = ConvertStringToDateTimeFormat(loResult.CDEPOSIT_DATE);
                    oEntity = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT01100LOO_Deposit_DepositDetailDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();
            try
            {

                switch (peCRUDMode)
                {
                    case eCRUDMode.NormalMode:
                        break;
                    case eCRUDMode.AddMode:
                        poNewEntity.CPROPERTY_ID = oParameter.CPROPERTY_ID;
                        poNewEntity.CBUILDING_ID = oParameter.CBUILDING_ID;
                        poNewEntity.CTRANS_CODE = oParameter.CTRANS_CODE;
                        poNewEntity.CDEPT_CODE = oParameter.CDEPT_CODE;
                        poNewEntity.CREF_NO = oParameter.CREF_NO;
                        break;
                    case eCRUDMode.EditMode:
                        break;
                    case eCRUDMode.DeleteMode:
                        break;
                    default:
                        break;
                }

                poNewEntity.CDEPOSIT_DATE = ConvertDateTimeToStringFormat(poNewEntity.DDEPOSIT_DATE);

                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                loResult.DDEPOSIT_DATE = ConvertStringToDateTimeFormat(loResult.CDEPOSIT_DATE);

                oEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(PMT01100LOO_Deposit_DepositDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CDEPOSIT_DATE = ConvertDateTimeToStringFormat(poEntity.DDEPOSIT_DATE);
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
