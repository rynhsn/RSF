using PMT01700COMMON.Context._1._Other_Untit_List;
using PMT01700COMMON.DTO._2._LOO._4._LOO___Deposit;
using PMT01700COMMON.DTO.Utilities.Front;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using PMT01700COMMON.DTO.Utilities.Response;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT01700MODEL.ViewModel
{
    public class PMT01700LOO_DepositViewModel : R_ViewModel<PMT01700LOO_Deposit_DepositDetailDTO>
    {
        #region From Back

        private readonly PMT01700LOO_DepositModel _model = new PMT01700LOO_DepositModel();
        public PMT01700LOO_Deposit_DepositDetailDTO oEntity = new PMT01700LOO_Deposit_DepositDetailDTO();
        public ObservableCollection<PMT01700LOO_Deposit_DepositListDTO> oListDeposit = new ObservableCollection<PMT01700LOO_Deposit_DepositListDTO>();
        public List<PMT01700ResponseCurrencyParameterDTO> oComboBoxDataCurrency = new List<PMT01700ResponseCurrencyParameterDTO>();
        public PMT01700LOO_Deposit_DepositHeaderDTO oHeaderEntity = new PMT01700LOO_Deposit_DepositHeaderDTO();
        public PMT01700ParameterFrontChangePageDTO oParameter = new PMT01700ParameterFrontChangePageDTO();
        #endregion

        #region Program


        public async Task GetDepositHeader()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(oParameter.CPROPERTY_ID))
                {
                    PMT01700LOO_UnitUtilities_ParameterDTO loParameter = R_FrontUtility.ConvertObjectToObject<PMT01700LOO_UnitUtilities_ParameterDTO>(oParameter);

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
                oComboBoxDataCurrency = new List<PMT01700ResponseCurrencyParameterDTO>(loResult.Data);
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
                 //   PMT01700LOO_UnitUtilities_ParameterDTO loParameter = R_FrontUtility.ConvertObjectToObject<PMT01700LOO_UnitUtilities_ParameterDTO>(oParameter);
                    R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CPROPERTY_ID, oParameter.CPROPERTY_ID);
                    R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CDEPT_CODE, oParameter.CDEPT_CODE);
                    R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CTRANS_CODE, oParameter.CTRANS_CODE);
                    R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CREF_NO, oParameter.CREF_NO);
                    R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CBUILDING_ID, oParameter.CBUILDING_ID);

                    var loResult = await _model.GetDepositListAsync();
                   
                    if (loResult.Data.Any())
                    {
                        foreach (var item in loResult.Data)
                            item.DDEPOSIT_DATE = ConvertStringToDateTimeFormat(item.CDEPOSIT_DATE);
                    }
                    oListDeposit = new ObservableCollection<PMT01700LOO_Deposit_DepositListDTO>(loResult.Data);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        #region Core

        public async Task GetEntity(PMT01700LOO_Deposit_DepositDetailDTO poEntity)
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

        public async Task ServiceSave(PMT01700LOO_Deposit_DepositDetailDTO poNewEntity, eCRUDMode peCRUDMode)
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
                        poNewEntity.CSEQ_NO = "";
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

        public async Task ServiceDelete(PMT01700LOO_Deposit_DepositDetailDTO poEntity)
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
