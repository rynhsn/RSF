using FAT00700Common.DTOs;
using FAT00700FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Threading.Tasks;
using Model = FAT00700Model.FAT00700Model;

namespace FAT00700Model.VMs
{
    /// <summary>
    /// ViewModel for FAT00700 Transaction Entry page
    /// Handles transaction data entry, validation, and CRUD operations
    /// </summary>
    public class FAT00700TransactionEntryViewModel : R_ViewModel<FAT00700DTO>
    {
        private readonly Model _fat00700Model = new Model();
        public FAT00700DTO paramTransEntry = new FAT00700DTO();
        public FAT00700DTO loTransEntry = new FAT00700DTO();
        public FAT00700DTO loSaveTransEntry = new FAT00700DTO();

        // Main entity for transaction entry
        public FAT00700DTO CurrentRecord { get; set; } = new FAT00700DTO();
        public FAT00700FrontChangePageParameterDTO oParameter { get; set; } = new FAT00700FrontChangePageParameterDTO();

        // Result DTOs needed for transaction entry
        public GetCurrencyResultDTO CurrencyResult { get; set; } = new GetCurrencyResultDTO();
        public GetFATransactionDataResultDTO TransactionDataResult { get; set; } = new GetFATransactionDataResultDTO();
        public GetUserRightApprovalResultDTO UserRightApprovalResult { get; set; } = new GetUserRightApprovalResultDTO();
        public GetUserActivityRightsResultDTO UserActivityRightsResult { get; set; } = new GetUserActivityRightsResultDTO();
        public CheckOutstandingTransResultDTO OutstandingTransResult { get; set; } = new CheckOutstandingTransResultDTO();
        public ValidateVoidResultDTO ValidateVoidResult { get; set; } = new ValidateVoidResultDTO();
        public GetApprovalPrecheckResultDTO ApprovalPrecheckResult { get; set; } = new GetApprovalPrecheckResultDTO();
        public ValidateFoundDeptResultDTO ValidateFoundDeptResult { get; set; } = new ValidateFoundDeptResultDTO();
        public GetTransDateValidationResultDTO TransDateValidationResult { get; set; } = new GetTransDateValidationResultDTO();
        public bool llEnableSubmit { get; set; } = true;
        public bool llEnableEdit { get; set; } = true;
        public bool llEnablePrint { get; set; } = true;
        public bool llEnableJournal { get; set; } = true;
        public bool llEnableRedraft { get; set; } = true;
        public bool llEnableDelete { get; set; } = true;
        public bool llDeletedRecord { get; set; } = false;
        public bool llEnableAdd { get; set; } = true;
        public bool llEnableTabInformation { get; set; } = true;

        #region UI Display Properties

        public string CreateDateDisplay
        {
            get
            {
                if (CurrentRecord?.DCREATE_DATE.HasValue == true)
                {
                    return R_FrontUtility.R_ConvertToDateTimeString(CurrentRecord.DCREATE_DATE.Value, "");
                }
                return string.Empty;
            }
        }

        public string UpdateDateDisplay
        {
            get
            {
                if (CurrentRecord?.DUPDATE_DATE.HasValue == true)
                {
                    return R_FrontUtility.R_ConvertToDateTimeString(CurrentRecord.DUPDATE_DATE.Value, "");
                }
                return string.Empty;
            }
        }

        public DateTime? TransactionDate
        {
            get
            {
                if (!string.IsNullOrEmpty(CurrentRecord?.CREF_DATE))
                {
                    if (DateTime.TryParse(CurrentRecord.CREF_DATE, out DateTime result))
                    {
                        return result;
                    }
                }
                return null;
            }
            set
            {
                if (CurrentRecord != null)
                {
                    CurrentRecord.CREF_DATE = value?.ToString("yyyy-MM-dd") ?? string.Empty;
                }
            }
        }

        #endregion

        #region Business Methods

        public async Task GetRecord(FAT00700DTO param)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _fat00700Model.R_ServiceGetRecordAsync(param);

                CurrentRecord = loResult;
                CurrentRecord.DREF_DATE = R_FrontUtility.R_ConvertToDateTime(CurrentRecord.CREF_DATE, "yyyyMMdd");

                if (loTransEntry.CTRANS_STATUS == "99" || loTransEntry.CTRANS_STATUS == "98")
                {
                    llDeletedRecord = true;
                }
                else
                {
                    llDeletedRecord = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Initialize transaction entry data
        /// </summary>
        public async Task InitializeTransactionEntryAsync(FAT00700DTO transactionData)
        {
            var loEx = new R_Exception();

            try
            {
                // Copy data from main page
                CurrentRecord = transactionData ?? new FAT00700DTO();

                // Load additional data needed for transaction entry
                await LoadCurrencyDataAsync();
                await LoadTransactionDataAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Load currency information
        /// </summary>
        private async Task LoadCurrencyDataAsync()
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GetCurrencyParameterDTO
                {
                    CCOMPANY_ID = "HGRBH",
                    CLANGID = "en",
                    CUSER_ID = "rtm"
                };

                var loResult = await _fat00700Model.GetCurrency(loParam);
                CurrencyResult = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task LoadTransactionDataAsync()
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GetFATransactionDataParameterDTO
                {
                    CCOMPANY_ID = CurrentRecord.CCOMPANY_ID,
                    CLANGID = CurrentRecord.CLANG_ID,
                    CUSER_ID = CurrentRecord.CUSER_ID,
                    CTRANSACTION_CODE = CurrentRecord.CREF_DATE,
                };

                var loResult = await _fat00700Model.GetFATransactionData(loParam);
                TransactionDataResult = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveTransactionAsync()
        {
            var loEx = new R_Exception();

            try
            {
                // Save transaction

                //Hardcoded date
                Data.DREF_DATE = DateTime.Now;

                var loResult = await _fat00700Model.R_ServiceSaveAsync(Data, R_CommonFrontBackAPI.eCRUDMode.AddMode);

                CurrentRecord = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteTransactionAsync(FAT00700DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                // Delete transaction

                await _fat00700Model.R_ServiceDeleteAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //public async Task GetTransactionList(string pcTransactionCode = "260010")
        //{
        //    var loEx = new R_Exception();

        //    try
        //    {
        //        var loParam = new GetTransactionListParameterDTO
        //        {
        //            CCOMPANY_ID = CurrentRecord.CCOMPANY_ID,
        //            CLANGUAGE_ID = CurrentRecord.CLANG_ID,
        //            CUSER_ID = CurrentRecord.CUSER_ID,
        //            CTRANS_CODE = pcTransactionCode,
        //            CDEPT_CODE = CurrentRecord.CDEPT_CODE,
        //            //BELUM DI APPLY!!!
        //            CFROM_PERIOD = string.Empty,
        //            CTO_PERIOD = string.Empty,
        //            CASSET_CODE = string.Empty
        //        };
        //        var loResult = await _fat00700Model.GetTransactionListAsync(loParam);
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    loEx.ThrowExceptionIfErrors();
        //}

        #endregion

        /// <summary>
        /// Copasan dari FAT00400
        /// </summary>
        /// <returns></returns>
        public async Task GetRecordTransEntry()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loTemp = await _fat00700Model.R_ServiceGetRecordAsync(paramTransEntry);
                loTransEntry = loTemp;
                loTransEntry.DREF_DATE = R_FrontUtility.R_ConvertToDateTime(loTransEntry.CREF_DATE, "yyyyMMdd");

                if (loTransEntry.CTRANS_STATUS == "99" || loTransEntry.CTRANS_STATUS == "98")
                {
                    llDeletedRecord = true;
                }
                else
                {
                    llDeletedRecord = false;
                }
                //cTransDate = loTransEntry.CREF_DATE;
                //cDocDate = loTransEntry.CDOC_DATE;
                //refDateValue = R_FrontUtility.R_ConvertToDateTime(cTransDate, "yyyyMMdd");
                //refDocDate = R_FrontUtility.R_ConvertToDateTime(cDocDate, "yyyyMMdd");

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Validation
        public void ValidationTransactionEntry(FAT00700DTO poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loData = poParam;

                if (string.IsNullOrEmpty(loData.CDEPT_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS002"));
                }

                if (string.IsNullOrEmpty(loData.CREF_DATE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS003"));
                }

                if (loData.CREF_DATE.CompareTo(paramTransEntry.CSOFT_PERIOD) < 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS004"));
                }

                if (string.IsNullOrEmpty(loData.CASSET_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS005"));
                }

                if (string.IsNullOrEmpty(loData.CTRANS_DESC))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS008"));
                }

                if (string.IsNullOrEmpty(loData.CEXPENSE_ALLOC_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS009"));
                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        #endregion

        #region Save & Saving
        public void SavingTransactionEntry(FAT00700DTO poParam)
        {
            loSaveTransEntry = poParam;
        }

        public async Task SaveTransactionEntry(eCRUDMode poCrudMode)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (poCrudMode == eCRUDMode.AddMode)
                {
                    loSaveTransEntry.CACTION = "NEW";
                }
                else
                {
                    loSaveTransEntry.CACTION = "EDIT";
                }

                var loTemp = await _fat00700Model.R_ServiceSaveAsync(loSaveTransEntry, poCrudMode);
                if (loTemp != null)
                {
                    loTransEntry = loTemp;
                    loTransEntry.DREF_DATE = R_FrontUtility.R_ConvertToDateTime(loTransEntry.CREF_DATE, "yyyyMMdd");

                    if (loTransEntry.CTRANS_STATUS == "99" || loTransEntry.CTRANS_STATUS == "98")
                    {
                        llDeletedRecord = true;
                    }
                    else
                    {
                        llDeletedRecord = false;
                    }
                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Submit, Redraft, & Delete
        public async Task SubmitTransaction()
        {
            R_Exception loEx = new R_Exception();

            try
            {

                var loParam = new FAT00700SubmitProcessParameterDTO();
                loParam.CREC_ID = loTransEntry.CREC_ID;

                await _fat00700Model.SubmitButton(loParam);

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task RedraftTransaction()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loParam = new FAT00700SubmitProcessParameterDTO();
                loParam.CNEW_STATUS = "00";
                loParam.CREC_ID = loTransEntry.CREC_ID;
                await _fat00700Model.SubmitButton(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteTransaction()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _fat00700Model.DeleteTransaction(loTransEntry);
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
