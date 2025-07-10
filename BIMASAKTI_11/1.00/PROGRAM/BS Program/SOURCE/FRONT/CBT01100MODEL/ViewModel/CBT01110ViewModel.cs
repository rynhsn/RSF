using CBT01100COMMON;
using CBT01100COMMON.DTO_s.CBT01110;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

namespace CBT01100MODEL
{
    public class CBT01110ViewModel : R_ViewModel<CBT01110ParamDTO>
    {
        #region Model

        private CBT01100InitModel _CBT01100InitModel = new CBT01100InitModel();
        private CBT01100Model _CBT01100Model = new CBT01100Model();
        private CBT01110Model _CBT01110Model = new CBT01110Model();

        #endregion Model

        #region Initial Data

        public CBT01100GSTransInfoDTO VAR_GSM_TRANSACTION_CODE { get; set; } = new CBT01100GSTransInfoDTO();
        public CBT01100GLSystemParamDTO VAR_GL_SYSTEM_PARAM { get; set; } = new CBT01100GLSystemParamDTO();
        public CBT01100CBSystemParamDTO VAR_CB_SYSTEM_PARAM { get; set; } = new CBT01100CBSystemParamDTO();
        public CBT01100GLSystemEnableOptionInfoDTO VAR_IUNDO_COMMIT_JRN { get; set; } = new CBT01100GLSystemEnableOptionInfoDTO();
        public CBT01100TodayDateDTO VAR_TODAY { get; set; } = new CBT01100TodayDateDTO();
        public CBT01100GSCompanyInfoDTO VAR_GSM_COMPANY { get; set; } = new CBT01100GSCompanyInfoDTO();
        public List<CBT01100GSCurrencyDTO> VAR_CURRENCY_LIST { get; set; } = new List<CBT01100GSCurrencyDTO>();
        public List<CBT01100GSCenterDTO> VAR_CENTER_LIST { get; set; } = new List<CBT01100GSCenterDTO>();
        public CBT01100GSPeriodDTInfoDTO VAR_SOFT_PERIOD_START_DATE { get; set; } = new CBT01100GSPeriodDTInfoDTO();
        public CBT01100GSPeriodYearRangeDTO VAR_GSM_PERIOD { get; set; } = new CBT01100GSPeriodYearRangeDTO();
        public List<CBT01100GSGSBCodeDTO> VAR_GSB_CODE_LIST { get; set; } = new List<CBT01100GSGSBCodeDTO>();

        #endregion Initial Data

        #region Public Property ViewModel

        public string _CREC_ID { get; set; } = "";
        public DateTime? RefDate { get; set; }
        public DateTime? DocDate { get; set; }
        public CBT01110ParamDTO Journal { get; set; } = new CBT01110ParamDTO();
        public CBT01110JournalDetailDTO JournalDetail { get; set; } = new CBT01110JournalDetailDTO();
        public ObservableCollection<CBT01101DTO> JournalDetailGrid { get; set; } = new ObservableCollection<CBT01101DTO>();
        public ObservableCollection<CBT01101DTO> JournalDetailGridTemp { get; set; } = new ObservableCollection<CBT01101DTO>();

        #endregion Public Property ViewModel

        public async Task GetAllUniversalData()
        {
            var loEx = new R_Exception();

            try
            {
                // Get Universal Data
                //var loResult = await _CBT01100InitModel.GetTabJournalEntryUniversalVarAsync();

                //Set Universal Data
                VAR_GSM_COMPANY = await _CBT01100InitModel.GetGSCompanyInfoAsync();
                VAR_CB_SYSTEM_PARAM = await _CBT01100InitModel.GetCBSystemParamAsync();
                VAR_GL_SYSTEM_PARAM = await _CBT01100InitModel.GetGLSystemParamAsync();
                VAR_GSM_TRANSACTION_CODE = await _CBT01100InitModel.GetGSTransCodeInfoAsync();
                VAR_TODAY = await _CBT01100InitModel.GetTodayDateAsync();
                VAR_CURRENCY_LIST = await _CBT01100InitModel.GetCurrencyListAsync();
                VAR_IUNDO_COMMIT_JRN = await _CBT01100InitModel.GetGSSystemEnableOptionInfoAsync();
                VAR_GSM_PERIOD = await _CBT01100InitModel.GetGSPeriodYearRangeAsync();
                VAR_GSB_CODE_LIST = await _CBT01100InitModel.GetGSBCodeListAsync();
                VAR_CENTER_LIST = await _CBT01100InitModel.GetCenterListAsync();
                VAR_CENTER_LIST.Add(new CBT01100GSCenterDTO { CCENTER_CODE = "", CCENTER_NAME = "" });
                var loParam = new CBT01100ParamGSPeriodDTInfoDTO() { CCYEAR = VAR_CB_SYSTEM_PARAM.CCURRENT_PERIOD_YY, CPERIOD_NO = VAR_CB_SYSTEM_PARAM.CCURRENT_PERIOD_MM };
                VAR_SOFT_PERIOD_START_DATE = await _CBT01100InitModel.GetGSPeriodDTInfoAsync(loParam);
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
                R_FrontContext.R_SetContext(ContextConstantCBT01100.CREC_ID, _CREC_ID);
                var loResult = await _CBT01110Model.GetJournalDetailListAsync();
                foreach (var item in loResult)
                {
                    if (!string.IsNullOrWhiteSpace(item.CDOCUMENT_DATE))
                        item.DDOCUMENT_DATE = DateTime.ParseExact(item.CDOCUMENT_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                }

                JournalDetailGrid = new ObservableCollection<CBT01101DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetJournalDetailRecord(CBT01110ParamDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _CBT01110Model.R_ServiceGetRecordAsync(poEntity);
                Journal = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task UpdateJournalStatus(CBT01100UpdateStatusDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _CBT01100Model.UpdateJournalStatusAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteJournalDetail(CBT01110ParamDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _CBT01110Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveJournalDetail(CBT01110ParamDTO poEntity, eCRUDMode poCRUDMode)
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
                var loResult = await _CBT01110Model.R_ServiceSaveAsync(poEntity, poCRUDMode);
                Journal = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion Journal
    }
}