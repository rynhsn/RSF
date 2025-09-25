using CBT00200COMMON;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CBT00200MODEL
{
    public class CBT00210ViewModel : R_ViewModel<CBT00200DTO>
    {
        #region Model
        private CBT00200InitialProcessModel _CBT00200InitialProcessModel = new CBT00200InitialProcessModel();
        private PublicLookupModel _PublicLookupModel = new PublicLookupModel();
        private CBT00200Model _CBT00200Model = new CBT00200Model();
        private CBT00210Model _CBT00210Model = new CBT00210Model();
        #endregion

        #region Initial Data
        public CBT00200CBSystemParamDTO VAR_CB_SYSTEM_PARAM { get; set; } = new CBT00200CBSystemParamDTO();
        public CBT00200TodayDateDTO VAR_TODAY { get; set; } = new CBT00200TodayDateDTO();
        public CBT00200GSCompanyInfoDTO VAR_GSM_COMPANY { get; set; } = new CBT00200GSCompanyInfoDTO();
        public List<CBT00200GSCenterDTO> VAR_CENTER_LIST { get; set; } = new List<CBT00200GSCenterDTO>();
        public CBT00200GSTransInfoDTO VAR_GSM_TRANSACTION_CODE { get; set; } = new CBT00200GSTransInfoDTO();
        public CBT00200GLSystemParamDTO VAR_GL_SYSTEM_PARAM { get; set; } = new CBT00200GLSystemParamDTO();
        public CBT00200GSPeriodDTInfoDTO VAR_SOFT_PERIOD_START_DATE { get; set; } = new CBT00200GSPeriodDTInfoDTO();
        public List<CBT00200LastCurrencyRateDTO> VAR_LAST_CURRENCY_RATE_LIST { get; set; } = new List<CBT00200LastCurrencyRateDTO>();
        public List<GSL00700DTO> VAR_DEPT_CODE_LIST { get; set; } = new List<GSL00700DTO>();
        #endregion

        #region Public Property ViewModel
        public DateTime? RefDate { get; set; }
        public DateTime? DocDate { get; set; }
        public CBT00200DTO Journal { get; set; } = new CBT00200DTO();
        public CBT00210DTO JournalDetail { get; set; } = new CBT00210DTO();
        public bool FlagIsCopy { get; set; } = false;
        #endregion

        public async Task GetAllUniversalData()
        {
            var loEx = new R_Exception();

            try
            {
                // Get Universal Data
                var loResult = await _CBT00200InitialProcessModel.GetTabJournalEntryInitialProcessAsync();

                //Set Universal Data
                VAR_CB_SYSTEM_PARAM = loResult.VAR_CB_SYSTEM_PARAM;
                VAR_TODAY = loResult.VAR_TODAY;
                VAR_GSM_COMPANY = loResult.VAR_GSM_COMPANY;
                VAR_GL_SYSTEM_PARAM = loResult.VAR_GL_SYSTEM_PARAM;
                VAR_GSM_TRANSACTION_CODE = loResult.VAR_GSM_TRANSACTION_CODE;
                VAR_SOFT_PERIOD_START_DATE = loResult.VAR_SOFT_PERIOD_START_DATE;

                loResult.VAR_CENTER_LIST.Add(new CBT00200GSCenterDTO { CCENTER_CODE = "", CCENTER_NAME = "" });
                VAR_CENTER_LIST = loResult.VAR_CENTER_LIST;


                //Get And Set List Dept Code
                VAR_DEPT_CODE_LIST = await _PublicLookupModel.GSL00700GetDepartmentListAsync(new GSL00700ParameterDTO());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetJournal(CBT00200DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _CBT00200Model.GetJournalRecordAsync(poEntity);

                Journal = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveJournal(CBT00200DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CREF_NO = string.IsNullOrWhiteSpace(poEntity.CREF_NO) ? "" : poEntity.CREF_NO;
                poEntity.CREF_DATE = RefDate.Value.ToString("yyyyMMdd");
                poEntity.CDOC_DATE = DocDate.Value.ToString("yyyyMMdd");

                var loParam = new CBT00200SaveParamDTO { Data = poEntity, CRUDMode = poCRUDMode  };
                var loResult = await _CBT00200Model.SaveJournalRecordAsync(loParam);

                Journal = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<CBT00200LastCurrencyRateDTO> GetLastCurrencyRate()
        {
            var loEx = new R_Exception();
            CBT00200LastCurrencyRateDTO loRtn = null;
            try
            {
                var loData = (CBT00200DTO)R_GetCurrentData();
                var loParam = R_FrontUtility.ConvertObjectToObject<CBT00200LastCurrencyRateDTO>(loData);
                loParam.CRATETYPE_CODE = VAR_CB_SYSTEM_PARAM.CRATETYPE_CODE;
                loParam.CRATE_DATE = RefDate.Value.ToString("yyyyMMdd");
                var loResult = await _CBT00200InitialProcessModel.GetLastCurrencyRateAsync(loParam);

                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public async Task GetJournalDetail(CBT00210DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _CBT00210Model.R_ServiceGetRecordAsync(poEntity);

                JournalDetail = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveJournalDetail(CBT00210DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _CBT00210Model.R_ServiceSaveAsync(poEntity, poCRUDMode);

                JournalDetail = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteJournalDetail(CBT00210DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _CBT00210Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
