using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT02600COMMON.DTOs.PMT02610;
using PMT02600COMMON.DTOs.PMT02600;
using PMT02600COMMON.DTOs.Helper;
using PMT02600COMMON.DTOs;

namespace PMT02600MODEL.ViewModel
{
    public class PMT02610ViewModel : R_ViewModel<PMT02610DTO>
    {
        #region From Back
        //private readonly PMT01410Model loAgreementListModel = new PMT01410Model();
        private readonly PMT02600Model loAgreementModel = new PMT02600Model();
        
        private readonly PMT02610Model loModel = new PMT02610Model();
        
        public PMT02610DTO loAgreement = new PMT02610DTO();
        
        public GetTransCodeInfoDTO loTransCodeInfo = new GetTransCodeInfoDTO();

        public TabParameterDTO loTabParameter = new TabParameterDTO();
        #endregion

        #region Core


        public async Task<PMT02610DTO> GetAgreementAsync(PMT02610DTO poParam)
        {
            R_Exception loEx = new R_Exception();
            PMT02610DTO loRtn = null;

            try
            {
                var loResult = await loModel.R_ServiceGetRecordAsync(new PMT02610ParameterDTO()
                {
                    Data = poParam
                });

                //loResult.Data.DDOC_DATE = ConvertStringToDateTimeFormat(loResult.Data.CDOC_DATE);
                //loResult.Data.DFOLLOW_UP_DATE = ConvertStringToDateTimeFormat(loResult.Data.CFOLLOW_UP_DATE);
                //loResult.Data.DHO_PLAN_DATE = ConvertStringToDateTimeFormat(loResult.Data.CHO_PLAN_DATE);

                loResult.Data.DSTART_DATE = ConvertStringToDateTimeFormat(loResult.Data.CSTART_DATE);
                loResult.Data.DEND_DATE = ConvertStringToDateTimeFormat(loResult.Data.CEND_DATE);
                loResult.Data.DREF_DATE = ConvertStringToDateTimeFormat(loResult.Data.CREF_DATE);

                loRtn = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public async Task GetEntity(PMT02610DTO poEntity)
        {
            var loEx = new R_Exception();
            PMT02610ParameterDTO loParam = null;
            try
            {
                var loResult = await loModel.R_ServiceGetRecordAsync(new PMT02610ParameterDTO()
                {
                    Data = poEntity
                });

                //loResult.Data.DDOC_DATE = ConvertStringToDateTimeFormat(loResult.Data.CDOC_DATE);
                //loResult.Data.DFOLLOW_UP_DATE = ConvertStringToDateTimeFormat(loResult.Data.CFOLLOW_UP_DATE);
                //loResult.Data.DHO_PLAN_DATE = ConvertStringToDateTimeFormat(loResult.Data.CHO_PLAN_DATE);

                loResult.Data.DSTART_DATE = ConvertStringToDateTimeFormat(loResult.Data.CSTART_DATE);
                loResult.Data.DEND_DATE = ConvertStringToDateTimeFormat(loResult.Data.CEND_DATE);
                loResult.Data.DREF_DATE = ConvertStringToDateTimeFormat(loResult.Data.CREF_DATE);

                loAgreement = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ServiceDelete(PMT02610DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Validation Before Delete
                await loModel.R_ServiceDeleteAsync(new PMT02610ParameterDTO()
                {
                    Data = poEntity
                });
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ServiceSave(PMT02610DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                //poNewEntity.CDOC_DATE = ConvertDateTimeToStringFormat(poNewEntity.DDOC_DATE);
                //poNewEntity.CFOLLOW_UP_DATE = ConvertDateTimeToStringFormat(poNewEntity.DFOLLOW_UP_DATE);
                //poNewEntity.CHO_PLAN_DATE = ConvertDateTimeToStringFormat(poNewEntity.DHO_PLAN_DATE);

                poNewEntity.CSTART_DATE = ConvertDateTimeToStringFormat(poNewEntity.DSTART_DATE);
                poNewEntity.CEND_DATE = ConvertDateTimeToStringFormat(poNewEntity.DEND_DATE);
                poNewEntity.CREF_DATE = ConvertDateTimeToStringFormat(poNewEntity.DREF_DATE);

                var loResult = await loModel.R_ServiceSaveAsync(new PMT02610ParameterDTO()
                {
                    Data = poNewEntity,
                    CPROPERTY_ID = loTabParameter.CPROPERTY_ID,
                }, peCRUDMode);

                //loResult.Data.DDOC_DATE = ConvertStringToDateTimeFormat(loResult.Data.CDOC_DATE);
                //loResult.Data.DFOLLOW_UP_DATE = ConvertStringToDateTimeFormat(loResult.Data.CFOLLOW_UP_DATE);
                //loResult.Data.DHO_PLAN_DATE = ConvertStringToDateTimeFormat(loResult.Data.CHO_PLAN_DATE);

                loResult.Data.DSTART_DATE = ConvertStringToDateTimeFormat(loResult.Data.CSTART_DATE);
                loResult.Data.DEND_DATE = ConvertStringToDateTimeFormat(loResult.Data.CEND_DATE);
                loResult.Data.DREF_DATE = ConvertStringToDateTimeFormat(loResult.Data.CREF_DATE);

                loAgreement = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        public async Task UpdateAgreementTransStatusAsync(string pcNewStatus)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await loAgreementModel.UpdateAgreementTransStatusAsync(new UpdateStatusDTO()
                {
                    CPROPERTY_ID = loTabParameter.CPROPERTY_ID,
                    CDEPT_CODE = loTabParameter.CDEPT_CODE,
                    CTRANS_CODE = ConstantVariable.VAR_TRANS_CODE,
                    CREF_NO = loTabParameter.CREF_NO,
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
                loResult = await loAgreementModel.GetTransCodeInfoAsync();
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

