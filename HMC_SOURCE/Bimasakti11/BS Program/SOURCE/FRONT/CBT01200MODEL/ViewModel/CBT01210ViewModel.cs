using CBT01200Common;
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
using CBT01200Common.DTOs;

namespace CBT01200MODEL
{
    public class CBT01210ViewModel : R_ViewModel<CBT01210ParamDTO>
    {
        #region Model
        private CBT01200InitModel _CBT01200InitModel = new CBT01200InitModel();
        private CBT01200Model _CBT01200Model = new CBT01200Model();
        private CBT01210Model _CBT01210Model = new CBT01210Model();
        #endregion

        #region Initial Data
        public CBT01200GSTransInfoDTO VAR_GSM_TRANSACTION_CODE { get; set; } = new CBT01200GSTransInfoDTO();
        public CBT01200GLSystemParamDTO VAR_GL_SYSTEM_PARAM { get; set; } = new CBT01200GLSystemParamDTO();
        public CBT01200CBSystemParamDTO VAR_CB_SYSTEM_PARAM { get; set; } = new CBT01200CBSystemParamDTO();
        public CBT01200GLSystemEnableOptionInfoDTO VAR_IUNDO_COMMIT_JRN { get; set; } = new CBT01200GLSystemEnableOptionInfoDTO();
        public CBT01200TodayDateDTO VAR_TODAY { get; set; } = new CBT01200TodayDateDTO();
        public CBT01200GSCompanyInfoDTO VAR_GSM_COMPANY { get; set; } = new CBT01200GSCompanyInfoDTO();
        public List<CBT01200GSCurrencyDTO> VAR_CURRENCY_LIST { get; set; } = new List<CBT01200GSCurrencyDTO>();
        public List<CBT01200GSCenterDTO> VAR_CENTER_LIST { get; set; } = new List<CBT01200GSCenterDTO>();
        public CBT01200GSPeriodDTInfoDTO VAR_SOFT_PERIOD_START_DATE { get; set; } = new CBT01200GSPeriodDTInfoDTO();
        public CBT01200GSPeriodYearRangeDTO VAR_GSM_PERIOD { get; set; } = new CBT01200GSPeriodYearRangeDTO();
        public List<CBT01200GSGSBCodeDTO> VAR_GSB_CODE_LIST { get; set; } = new List<CBT01200GSGSBCodeDTO>();

        #endregion

        #region Public Property ViewModel

        public string _CREC_ID { get; set; } = "";
        public DateTime? RefDate { get; set; }
        public DateTime? DocDate { get; set; }
        public CBT01210ParamDTO Journal { get; set; } = new CBT01210ParamDTO();
        public CBT01210JournalDetailDTO JournalDetail { get; set; } = new CBT01210JournalDetailDTO();
        public ObservableCollection<CBT01201DTO> JournalDetailGrid { get; set; } = new ObservableCollection<CBT01201DTO>();
        public ObservableCollection<CBT01201DTO> JournalDetailGridTemp { get; set; } = new ObservableCollection<CBT01201DTO>();
        #endregion

        public async Task GetAllUniversalData()
        {
            var loEx = new R_Exception();

            try
            {
                // Get Universal Data
                //var loResult = await _CBT01200InitModel.GetTabJournalEntryUniversalVarAsync();

                //Set Universal Data
                VAR_GSM_COMPANY = await _CBT01200InitModel.GetGSCompanyInfoAsync();
                VAR_CB_SYSTEM_PARAM = await _CBT01200InitModel.GetCBSystemParamAsync();
                VAR_GL_SYSTEM_PARAM = await _CBT01200InitModel.GetGLSystemParamAsync();
                VAR_GSM_TRANSACTION_CODE = await _CBT01200InitModel.GetGSTransCodeInfoAsync();
                VAR_TODAY = await _CBT01200InitModel.GetTodayDateAsync();
                VAR_CURRENCY_LIST = await _CBT01200InitModel.GetCurrencyListAsync();
                VAR_IUNDO_COMMIT_JRN = await _CBT01200InitModel.GetGSSystemEnableOptionInfoAsync();
                VAR_GSM_PERIOD = await _CBT01200InitModel.GetGSPeriodYearRangeAsync();
                VAR_GSB_CODE_LIST = await _CBT01200InitModel.GetGSBCodeListAsync();
                VAR_CENTER_LIST.Add(new CBT01200GSCenterDTO { CCENTER_CODE = "", CCENTER_NAME = "" });
                VAR_CENTER_LIST= await _CBT01200InitModel.GetCenterListAsync();
                var loParam = new CBT01200ParamGSPeriodDTInfoDTO() { CCYEAR = VAR_CB_SYSTEM_PARAM.CCURRENT_PERIOD_YY, CPERIOD_NO = VAR_CB_SYSTEM_PARAM.CCURRENT_PERIOD_MM };
                VAR_SOFT_PERIOD_START_DATE = await _CBT01200InitModel.GetGSPeriodDTInfoAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Journal
        
        public async Task GetJournalDetailList()
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.CREC_ID, _CREC_ID);
                var loResult = await _CBT01210Model.GetJournalDetailListAsync();

                JournalDetailGrid = new ObservableCollection<CBT01201DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetJournalDetailRecord(CBT01210ParamDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _CBT01210Model.R_ServiceGetRecordAsync(poEntity);

                //RefDate = DateTime.ParseExact(loResult.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                //DocDate = DateTime.ParseExact(loResult.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                Journal = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
       
        public async Task UpdateJournalStatus(CBT01200UpdateStatusDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _CBT01200Model.UpdateJournalStatusAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteJournalDetail(CBT01210ParamDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {           
                await _CBT01210Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveJournalDetail(CBT01210ParamDTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {     
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poEntity.CACTION = "NEW";
                    poEntity.CREC_ID = "";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poEntity.CACTION = "EDIT";
                }
                var loResult = await _CBT01210Model.R_ServiceSaveAsync(poEntity, poCRUDMode);

                Journal = loResult;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion




    }
}
