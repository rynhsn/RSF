using PMF00200COMMON;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PMF00200Model
{
    public class PMF00200ViewModel 
    {
        #region Model
        private PMF00200Model _PMF00200Model = new PMF00200Model();
        #endregion

        #region Initial Data
        public PMF00200GSCompanyInfoDTO VAR_GSM_COMPANY { get; set; } = new PMF00200GSCompanyInfoDTO();
        public List<PMF00200ReportTemplateDTO> VAR_REPORT_TEMPLATE_LIST { get; set; } = new List<PMF00200ReportTemplateDTO>();
        public PMF00200DTO HeaderDisplay = new PMF00200DTO();
        #endregion

        #region Proses Upload
        public string CompanyID { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; } = "";
        public int Percentage { get; set; } = 0;
        // Action StateHasChanged
        public Action StateChangeAction { get; set; }

        // Action Get Error Unhandle
        public Action<R_APIException> ShowErrorAction { get; set; }

        // Func Proses is Success
        public Func<Task> ActionIsCompleteSuccess { get; set; }
        #endregion

        public async Task GetAllUniversalData(PMF00200InputParameterDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Get Universal Data
                var loResult = await _PMF00200Model.GetAllInitialProcessAsync(poEntity);

                //Set Universal Data
                VAR_GSM_COMPANY = loResult.VAR_GSM_COMPANY;
                VAR_REPORT_TEMPLATE_LIST = loResult.VAR_REPORT_TEMPLATE_LIST;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetJournalRecord(PMF00200InputParameterDTO poEntity, string pcCultureId)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMF00200Model.GetJournalRecordAsync(poEntity);
                if (DateTime.TryParseExact(loResult.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldRefDate))
                {
                    loResult.CREF_DATE_DISPLAY = ldRefDate.ToString(pcCultureId);
                }
                else
                {
                    loResult.CREF_DATE_DISPLAY = "";
                }
                if (DateTime.TryParseExact(loResult.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldDocDate))
                {
                    loResult.CDOC_DATE_DISPLAY = ldDocDate.ToString(pcCultureId);
                }
                else
                {
                    loResult.CDOC_DATE_DISPLAY = "";
                }
                if (DateTime.TryParseExact(loResult.CCHEQUE_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldChequeDate))
                {
                    loResult.CCHEQUE_DATE_DISPLAY = ldChequeDate.ToString(pcCultureId);
                }
                else
                {
                    loResult.CCHEQUE_DATE_DISPLAY = "";
                }

                HeaderDisplay = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ProcessSendEmail(PMF00200InputParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                await _PMF00200Model.SendEmail(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
