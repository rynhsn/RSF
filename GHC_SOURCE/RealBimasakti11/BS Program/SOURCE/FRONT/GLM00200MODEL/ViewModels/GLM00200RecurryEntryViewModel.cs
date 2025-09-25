
using GLM00200COMMON;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GLM00200MODEL
{
    public class GLM00200RecurryEntryViewModel : R_ViewModel<JournalDTO>
    {
        private GLM00200MODEL _model = new GLM00200MODEL();
        private GLM00200InitModel _modelInit = new GLM00200InitModel();

        private PublicLookupModel _lookupGSModel = new PublicLookupModel();
        public RecurringJournalListParamDTO Parameter { get; set; } = new RecurringJournalListParamDTO();
        public JournalDTO Journal { get; set; } = new JournalDTO();
        public ObservableCollection<JournalDetailGridDTO> JournaDetailGrid { get; set; } = new ObservableCollection<JournalDetailGridDTO>();
        public ObservableCollection<JournalDetailGridDTO> JournaDetailGridTemp { get; set; } = new ObservableCollection<JournalDetailGridDTO>();
        public CompanyDTO GSM_COMPANY { get; set; } = new CompanyDTO();
        public TransCodeDTO GSM_TRANSACTION_CODE { get; set; } = new TransCodeDTO();
        public IUndoCommitJrnDTO IUNDO_COMMIT_JRN { get; set; } = new IUndoCommitJrnDTO();
        public GLSysParamDTO GL_SYSTEM_PARAM { get; set; } = new GLSysParamDTO();
        public PeriodDTInfoDTO CURRENT_PERIOD_START_DATE { get; set; } = new PeriodDTInfoDTO();
        public PeriodDTInfoDTO CSOFT_PERIOD_START_DATE { get; set; } = new PeriodDTInfoDTO();

        public List<CurrencyDTO> CURRENCY_LIST { get; set; } = new List<CurrencyDTO>();
        public List<GSL00900DTO> CENTER_LIST { get; set; } = new List<GSL00900DTO>();

        public TodayDTO VAR_TODAY { get; set; } = new TodayDTO() { DTODAY = DateTime.Now };


        #region Property ViewModel
        public DateTime? RefDate { get; set; } = DateTime.Now;
        public DateTime? DocDate { get; set; }
        public DateTime? StartDate { get; set; } = DateTime.Now;
        public DateTime? NextDate { get; set; }
        public DateTime? LastDate { get; set; }

        public bool _IsCopyMode = false;

        #endregion

        public async Task GetInitData()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _modelInit.GetAllInitRecordAsync();
                var loCenterResult = await _lookupGSModel.GSL00900GetCenterListAsync();
                CURRENCY_LIST = await _modelInit.GetListCurrencyAsync();
                GSM_COMPANY = loResult.COMPANY_INFO;
                CENTER_LIST = loCenterResult;
                CENTER_LIST.Add(new GSL00900DTO()
                {
                    CCENTER_CODE = "",
                    CCENTER_NAME = ""
                }); //prevent error while center value reset
                GSM_TRANSACTION_CODE = loResult.GSM_TRANSACTION_CODE;
                IUNDO_COMMIT_JRN = loResult.IUNDO_COMMIT_JRN;
                GL_SYSTEM_PARAM = loResult.GL_SYSTEM_PARAM;
                CURRENT_PERIOD_START_DATE = loResult.CURRENT_PERIOD_START_DATE;
                //VAR_TODAY.DTODAY = loResult.DTODAY;//
                CSOFT_PERIOD_START_DATE = loResult.SOFT_PERIOD_START_DATE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        #region Journal
        public async Task GetJournal(JournalDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetJournalRecordAsync(poEntity);
                Journal = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task ShowAllJournalDetail(JournalDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetRecurringJrnDtListAsync(poParam);

                JournaDetailGrid = new ObservableCollection<JournalDetailGridDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveJournal(JournalParamDTO poEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poEntity.CREF_DATE = RefDate.HasValue == true ? RefDate.Value.ToString("yyyyMMdd") : "";
                poEntity.CDOC_DATE = DocDate.HasValue == true ? DocDate.Value.ToString("yyyyMMdd") : "";
                poEntity.CSTART_DATE = StartDate.HasValue == true ? StartDate.Value.ToString("yyyyMMdd") : "";
                poEntity.CNEXT_DATE = NextDate.HasValue == true ? NextDate.Value.ToString("yyyyMMdd") : "";

                if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poEntity.CJRN_ID = poEntity.CREC_ID;
                }

                var loParam = new ParemeterRecordWithCRUDModeResultDTO<JournalParamDTO>();
                loParam.data = poEntity;
                loParam.eCRUDMode = poCRUDMode;
                var loResult = await _model.SaveJournalAsync(loParam);

                Journal = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region RefreshCurrencyRate
        public async Task RefreshCurrencyRate()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = (JournalDTO)R_GetCurrentData();
                var _CURRENCY_RATE_RESULT = await _modelInit.GetCurrencyRateRecordAsync( new CurrencyRateParamDTO()
                {
                    CCURRENCY_CODE=loData.CCURRENCY_CODE,
                    CRATE_DATE= StartDate.Value.ToString("yyyyMMdd"),
                    CRATETYPE_CODE = GL_SYSTEM_PARAM.CRATETYPE_CODE
                });

                if (_CURRENCY_RATE_RESULT != null)
                {
                    loData.NLBASE_RATE = _CURRENCY_RATE_RESULT.NLBASE_RATE_AMOUNT;
                    loData.NLCURRENCY_RATE = _CURRENCY_RATE_RESULT.NLCURRENCY_RATE_AMOUNT;
                    loData.NBBASE_RATE = _CURRENCY_RATE_RESULT.NBBASE_RATE_AMOUNT;
                    loData.NBCURRENCY_RATE = _CURRENCY_RATE_RESULT.NBCURRENCY_RATE_AMOUNT;
                }
                else
                {
                    loData.NLBASE_RATE = 1;
                    loData.NLCURRENCY_RATE = 1;
                    loData.NBBASE_RATE = 1;
                    loData.NBCURRENCY_RATE = 1;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Commit/Approval
        public async Task UpdateJournalStatusAsync(GLM00200UpdateStatusDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _model.UpdateJrnStatusAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        
        #region template
        public async Task<UploadByte> DownloadTemplate()
        {
            var loEx = new R_Exception();
            UploadByte loResult = null;

            try
            {
                loResult = await _model.DownloadTemplateAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion
    }
}
