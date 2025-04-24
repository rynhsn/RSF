
using GLM00200COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

namespace GLM00200MODEL
{
    public class GLM00203ViewModel : R_ViewModel<JournalDTO>
    {
        private GLM00200MODEL _model = new GLM00200MODEL();
        public JournalDTO Journal { get; set; } = new JournalDTO();
        public ObservableCollection<JournalDetailActualGridDTO> JournaDetailActualGrid { get; set; } = new ObservableCollection<JournalDetailActualGridDTO>();
        public CompanyDTO GSM_COMPANY { get; set; } = new CompanyDTO();
        public TransCodeDTO GSM_TRANSACTION_CODE { get; set; } = new TransCodeDTO();
        public IUndoCommitJrnDTO IUNDO_COMMIT_JRN { get; set; } = new IUndoCommitJrnDTO();
        public GLSysParamDTO GL_SYSTEM_PARAM { get; set; } = new GLSysParamDTO();
        public PeriodDTInfoDTO CURRENT_PERIOD_START_DATE { get; set; } = new PeriodDTInfoDTO();


        #region Property ViewModel
        public DateTime? RefDate { get; set; } = DateTime.Now;
        public DateTime? DocDate { get; set; }
        public DateTime? StartDate { get; set; } = DateTime.Now;
        public DateTime? NextDate { get; set; }
        public DateTime? LastDate { get; set; }
        #endregion

        #region Init
        public async Task GetInitData()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetInitDataAsync();

                GSM_COMPANY = loResult.COMPANY_INFO;
                GSM_TRANSACTION_CODE = loResult.GSM_TRANSACTION_CODE;
                IUNDO_COMMIT_JRN = loResult.IUNDO_COMMIT_JRN;
                GL_SYSTEM_PARAM = loResult.GL_SYSTEM_PARAM;
                CURRENT_PERIOD_START_DATE = loResult.CURRENT_PERIOD_START_DATE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Journal
        public async Task GetlJournal(JournalDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetJournalRecordAsync(poEntity);
                if (!string.IsNullOrWhiteSpace(loResult.CREF_DATE))
                    RefDate = DateTime.ParseExact(loResult.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                if (!string.IsNullOrWhiteSpace(loResult.CDOC_DATE))
                    DocDate = DateTime.ParseExact(loResult.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                if (!string.IsNullOrWhiteSpace(loResult.CSTART_DATE))
                    StartDate = DateTime.ParseExact(loResult.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                if (!string.IsNullOrWhiteSpace(loResult.CNEXT_DATE))
                    StartDate = DateTime.ParseExact(loResult.CNEXT_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                if (!string.IsNullOrWhiteSpace(loResult.CLAST_DATE))
                    StartDate = DateTime.ParseExact(loResult.CLAST_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);

                Journal = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task ShowAllJournalDetail(RecurringJournalListParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetRecurringJrnActualDetailListAsync(poParam);
                foreach (var loItem in loResult)
                {
                    if (DateTime.TryParseExact(loItem.CREF_PRD+"01", "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime pdParsedRefPrdDate))
                    {
                        loItem.DREF_PRD = pdParsedRefPrdDate;
                    }

                    if (DateTime.TryParseExact(loItem.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime pdParsedRefDate))
                    {
                        loItem.DREF_DATE = pdParsedRefDate;
                    }

                }

                JournaDetailActualGrid = new ObservableCollection<JournalDetailActualGridDTO>(loResult);
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
