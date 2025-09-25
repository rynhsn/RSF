using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using PMT02600COMMON.DTOs.PMT02600;
using PMT02600COMMON.DTOs;
using PMT02600COMMON.DTOs.Helper;
using System.Globalization;

namespace PMT02600MODEL.ViewModel
{
    public class PMT02600ViewModel : R_ViewModel<PMT02600DTO>
    {
        private PMT02600Model loModel = new PMT02600Model();

        public PMT02600DTO loAgreement = new PMT02600DTO();

        public ObservableCollection<PMT02600DTO> loAgreementList = new ObservableCollection<PMT02600DTO>();

        public ObservableCollection<PMT02600DetailDTO> loAgreementDetailList = new ObservableCollection<PMT02600DetailDTO>();

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public List<GetPropertyListDTO> loPropertyList = new List<GetPropertyListDTO>();

        public GetTransCodeInfoDTO loTransCodeInfo = new GetTransCodeInfoDTO();

        public TabParameterDTO loTabChangeParameter = new TabParameterDTO();

        public FilterStatusDTO loFilterStatus = new FilterStatusDTO();

        public async Task GetAgreementListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            PMT02600ParameterDTO loParam = null;
            List<PMT02600DTO> loTemp = null;
            PMT02600ResultDTO loResult = null;

            try
            {
                loParam = new PMT02600ParameterDTO()
                {
                    CPROPERTY_ID = loProperty.CPROPERTY_ID,
                    CTRANS_CODE = loTransCodeInfo.CTRANS_CODE,
                    CPAR_TRANS_STS = loFilterStatus.CPAR_TRANS_STS
                };

                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT20600_GET_AGREEMENT_LIST_STREAMING_CONTEXT, loParam);
                loResult = await loModel.GetAgreementListStreamAsync();
                loResult.Data.ForEach(x =>
                {
                    x.DREF_DATE = ConvertStringToDateTimeFormat(x.CREF_DATE);
                    x.DHO_ACTUAL_DATE = ConvertStringToDateTimeFormat(x.CHO_ACTUAL_DATE);
                    x.DOPEN_DATE = ConvertStringToDateTimeFormat(x.COPEN_DATE);
                    x.DSTART_DATE = ConvertStringToDateTimeFormat(x.CSTART_DATE);
                    x.DEND_DATE = ConvertStringToDateTimeFormat(x.CEND_DATE);
                });
                loAgreementList = new ObservableCollection<PMT02600DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetAgreementDetailListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            PMT02600DetailParameterDTO loParam = null;
            List<PMT02600DetailDTO> loTemp = null;
            PMT02600DetailResultDTO loResult = null;

            try
            {
                loParam = new PMT02600DetailParameterDTO()
                {
                    CPROPERTY_ID = loProperty.CPROPERTY_ID,
                    CDEPT_CODE = loAgreement.CDEPT_CODE,
                    CREF_NO = loAgreement.CREF_NO,
                    CTRANS_CODE = loTransCodeInfo.CTRANS_CODE
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT20600_GET_AGREEMENT_DETAIL_LIST_STREAMING_CONTEXT, loParam);
                loResult = await loModel.GetAgreementDetailListStreamAsync();
                loAgreementDetailList = new ObservableCollection<PMT02600DetailDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetPropertyListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            GetPropertyListResultDTO loResult = null;
            try
            {
                loResult = await loModel.GetPropertyListStreamAsync();
                loPropertyList = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task UpdateAgreementTransStatusAsync(string pcNewStatus)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await loModel.UpdateAgreementTransStatusAsync(new UpdateStatusDTO()
                {
                    CPROPERTY_ID = loAgreement.CPROPERTY_ID,
                    CDEPT_CODE = loAgreement.CDEPT_CODE,
                    CTRANS_CODE = ConstantVariable.VAR_TRANS_CODE,
                    CREF_NO = loAgreement.CREF_NO,
                    CNEW_STATUS = pcNewStatus
                });
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<GetTransCodeInfoDTO> GetTransCodeInfoAsync()
        {
            R_Exception loEx = new R_Exception();
            GetTransCodeInfoResultDTO loResult = null;

            try
            {
                loResult = await loModel.GetTransCodeInfoAsync();
                loTransCodeInfo = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loTransCodeInfo;
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
                return "";
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
