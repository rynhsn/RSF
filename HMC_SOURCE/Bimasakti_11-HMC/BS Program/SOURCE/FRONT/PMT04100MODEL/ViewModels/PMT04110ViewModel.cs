using PMT04100COMMON;
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

namespace PMT04100MODEL
{
    public class PMT04110ViewModel : R_ViewModel<PMT04100DTO>
    {
        #region Model
        private PMT04100InitialProcessModel _PMT04100InitialProcessModel = new PMT04100InitialProcessModel();
        private PMT04100Model _PMT04100Model = new PMT04100Model();
        #endregion

        #region Initial Data
        public PMT04100CBSystemParamDTO VAR_CB_SYSTEM_PARAM { get; set; } = new PMT04100CBSystemParamDTO();
        public PMT04100TodayDateDTO VAR_TODAY { get; set; } = new PMT04100TodayDateDTO();
        public PMT04100GSCompanyInfoDTO VAR_GSM_COMPANY { get; set; } = new PMT04100GSCompanyInfoDTO();
        public PMT04100GSTransInfoDTO VAR_GSM_TRANSACTION_CODE { get; set; } = new PMT04100GSTransInfoDTO();
        public PMT04100PMSystemParamDTO VAR_PM_SYSTEM_PARAM { get; set; } = new PMT04100PMSystemParamDTO();
        public PMT04100GSPeriodDTInfoDTO VAR_SOFT_PERIOD_START_DATE { get; set; } = new PMT04100GSPeriodDTInfoDTO();
        public List<PMT04100PropertyDTO> VAR_PROPERTY_LIST { get; set; } = new List<PMT04100PropertyDTO>();
        public List<PMT04100LastCurrencyRateDTO> VAR_LAST_CURRENCY_RATE_LIST { get; set; } = new List<PMT04100LastCurrencyRateDTO>();
        #endregion

        #region Public Property ViewModel
        public DateTime? RefDate { get; set; }
        public DateTime? DocDate { get; set; }
        public PMT04100DTO Journal { get; set; } = new PMT04100DTO();
        public bool FlagIsCopy { get; set; } = false;
        #endregion

        public async Task GetAllUniversalData(object poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Get Universal Data
                var loParam = R_FrontUtility.ConvertObjectToObject<PMTInitialParamDTO>(poEntity);
                if (string.IsNullOrWhiteSpace(loParam.CPROPERTY_ID) == false)
                {
                    var loResult = await _PMT04100InitialProcessModel.GetTabJournalEntryInitialProcessAsync(loParam);

                    //Set Universal Data
                    VAR_CB_SYSTEM_PARAM = loResult.VAR_CB_SYSTEM_PARAM;
                    VAR_GSM_TRANSACTION_CODE = loResult.VAR_GSM_TRANSACTION_CODE;
                    VAR_TODAY = loResult.VAR_TODAY;
                    VAR_GSM_COMPANY = loResult.VAR_GSM_COMPANY;
                    VAR_PM_SYSTEM_PARAM = loResult.VAR_PM_SYSTEM_PARAM;
                    VAR_SOFT_PERIOD_START_DATE = loResult.VAR_SOFT_PERIOD_START_DATE;
                    VAR_PROPERTY_LIST = loResult.VAR_PROPERTY_LIST;

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetJournal(PMT04100DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT04100Model.GetJournalRecordAsync(poEntity);
                if (DateTime.TryParseExact(loResult.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldRefDate))
                {
                    RefDate = ldRefDate;
                }
                else
                {
                    RefDate = null;
                }
                if (DateTime.TryParseExact(loResult.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldDocDate))
                {
                    DocDate = ldDocDate;
                }
                else
                {
                    DocDate = null;
                }

                Journal = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveJournal(PMT04100DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new PMT04100SaveParamDTO { Data = poEntity, CRUDMode = poCRUDMode  };
                var loResult = await _PMT04100Model.SaveJournalRecordAsync(loParam);

                Journal = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<PMT04100LastCurrencyRateDTO> GetLastCurrencyRate()
        {
            var loEx = new R_Exception();
            PMT04100LastCurrencyRateDTO loRtn = null;
            try
            {
                var loData = (PMT04100DTO)R_GetCurrentData();
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT04100LastCurrencyRateDTO>(loData);
                loParam.CRATETYPE_CODE = VAR_PM_SYSTEM_PARAM.CCUR_RATETYPE_CODE;
                loParam.CRATE_DATE = RefDate.Value.ToString("yyyyMMdd");
                var loResult = await _PMT04100InitialProcessModel.GetLastCurrencyRateAsync(loParam);

                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public async Task UpdateJournalStatus(PMT04100UpdateStatusDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _PMT04100Model.UpdateJournalStatusAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SubmitCashReceipt(PMT04100UpdateStatusDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _PMT04100Model.SubmitCashReceiptAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
