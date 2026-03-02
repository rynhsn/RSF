using FAT00700Common.DTOs;
using FAT00700FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using Model = FAT00700Model.FAT00700Model;

namespace FAT00700Model.VMs
{
    /// <summary>
    /// ViewModel for FAT00700 - FA Transaction operations
    /// Handles all data operations, validation, and state management
    /// 
    /// Known Issues/Bugs Preserved from VB.NET:
    /// 1. Line 1941 in VB.NET: loParam._CTRANSACTION_CODE = txtTransNumCode.Text - Should be VAR_CTRANS_CODE or txtTransType.Text
    /// 2. Line 1511 in VB.NET: getBaseCalculate() uses uninitialized loPar._NBCURRENCY_RATE and loPar._NBBASE_RATE
    /// 3. GetGridAllocData: Controller returns only first item from list (should return full list for grid)
    /// </summary>
    public class FAT00700ViewModel : R_ViewModel<FAT00700DTO>
    {
        private readonly Model _fat00700Model = new Model();
        public FAT00700CompanyInfoResultDTO CompanyInfo { get; set; } = new FAT00700CompanyInfoResultDTO();
        public FAT00700SystemParamResultDTO SystemParam { get; set; } = new FAT00700SystemParamResultDTO();
        public FAT00700PeriodInfoResultDTO PeriodInfo { get; set; } = new FAT00700PeriodInfoResultDTO();
        public FAT00700TransCodeInfoResultDTO TransCodeInfo { get; set; } = new FAT00700TransCodeInfoResultDTO();
        public FAT00700PeriodRangeResultDTO PeriodRange { get; set; } = new FAT00700PeriodRangeResultDTO();

        // Main entity
        public FAT00700DTO CurrentRecord { get; set; } = new FAT00700DTO();

        // Grid and list data
        public ObservableCollection<GetGridAllocDataResultDTO> GridAllocList { get; set; } = new ObservableCollection<GetGridAllocDataResultDTO>();
        public ObservableCollection<GetTransactionListResultDTO> TransactionList { get; set; } = new ObservableCollection<GetTransactionListResultDTO>();

        public List<FAT00700GetDeptListResultDTO> loListDept = new List<FAT00700GetDeptListResultDTO>();
        //Parameter DTOs
        public GetTransactionListParameterDTO TransactionListParameterDTO { get; set; } = new();

        // Result DTOs
        public GetPeriodResultDTO PeriodResult { get; set; } = new GetPeriodResultDTO();
        public GetCurrencyResultDTO CurrencyResult { get; set; } = new GetCurrencyResultDTO();
        public GetFATransactionDataResultDTO TransactionDataResult { get; set; } = new GetFATransactionDataResultDTO();
        public GetAssetInformationResultDTO AssetInformationResult { get; set; } = new GetAssetInformationResultDTO();
        public GetAssetInfoDataResultDTO AssetInfoDataResult { get; set; } = new GetAssetInfoDataResultDTO();
        public GetUserRightApprovalResultDTO UserRightApprovalResult { get; set; } = new GetUserRightApprovalResultDTO();
        public GetUserActivityRightsResultDTO UserActivityRightsResult { get; set; } = new GetUserActivityRightsResultDTO();
        public CheckOutstandingTransResultDTO OutstandingTransResult { get; set; } = new CheckOutstandingTransResultDTO();
        public ValidateVoidResultDTO ValidateVoidResult { get; set; } = new ValidateVoidResultDTO();
        public GetApprovalPrecheckResultDTO ApprovalPrecheckResult { get; set; } = new GetApprovalPrecheckResultDTO();
        public ValidateFoundDeptResultDTO ValidateFoundDeptResult { get; set; } = new ValidateFoundDeptResultDTO();
        public GetTransDateValidationResultDTO TransDateValidationResult { get; set; } = new GetTransDateValidationResultDTO();
        public GetDateStatusResultDTO DateStatusResult { get; set; } = new GetDateStatusResultDTO();

        #region For Front

        // Period
        public int IYEAR_FROM = DateTime.Now.Year;
        public int IYEAR_TO = DateTime.Now.Year;
        public string CMONTH_FROM = DateTime.Now.Month.ToString("00");
        public string CMONTH_TO = DateTime.Now.Month.ToString("00");

        public List<string> MonthList = new List<string>
        {
            "01", "02", "03", "04", "05", "06",
            "07", "08", "09", "10", "11", "12"
        };

        // UI Display Properties (only for lookup results and formatted data)
        public string DepartmentDescription { get; set; } = string.Empty;
        public string AllocationDescription { get; set; } = string.Empty;

        //Process Period
        public void SetPeriodFromTo()
        {
            string periodFrom = $"{IYEAR_FROM}{CMONTH_FROM}";
            string periodTo = $"{IYEAR_TO}{CMONTH_TO}";

            R_FrontContext.R_SetStreamingContext(ContextConstantDTO.CFROM_PERIOD, periodFrom);
            R_FrontContext.R_SetStreamingContext(ContextConstantDTO.CTO_PERIOD, periodTo);
        }

        // Date display properties (formatted from DateTime?)
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

        // Transaction Date property for DatePicker binding (converts between string and DateTime?)
        public DateTime? TransactionDate
        {
            get
            {
                if (CurrentRecord == null || string.IsNullOrWhiteSpace(CurrentRecord.CREF_DATE))
                    return null;
                return R_FrontUtility.R_ConvertToDateTime(CurrentRecord.CREF_DATE);
            }
            set
            {
                if (CurrentRecord != null)
                {
                    if (value.HasValue)
                    {
                        CurrentRecord.CREF_DATE = value.Value.ToString("yyyyMMdd");
                    }
                    else
                    {
                        CurrentRecord.CREF_DATE = string.Empty;
                    }
                }
            }
        }

        #endregion

        #region CRUD Methods

        /// <summary>
        /// Get record for display/edit
        /// </summary>
        /// <param name="poEntity">Entity containing search parameters (CDEPT_CODE, CREFERENCE_NO)</param>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcLangId">Language ID</param>
        /// <param name="pcTransactionCode">Transaction code (default: "260010")</param>
        public async Task GetRecordAsync(FAT00700DTO poEntity, string pcCompanyId, string pcLangId, string pcTransactionCode)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new FAT00700DTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CLANG_ID = pcLangId,
                    CREC_ID = pcTransactionCode,
                    CDEPT_CODE = poEntity.CDEPT_CODE,
                    CREF_NO = poEntity.CREF_NO
                };

                var loResult = await _fat00700Model.R_ServiceGetRecordAsync(loParam);
                CurrentRecord = loResult ?? new FAT00700DTO();

                // Business logic from VB.NET: Process transaction date
                if (loResult != null)
                {
                    if (!string.IsNullOrWhiteSpace(loResult.CREF_DATE))
                    {
                        // Note: In VB.NET, loCTRANSACTION_PRD = Strings.Left(._CTRANSACTION_DATE, 6)
                        // This extracts period (yyyyMM) from date string
                        // The date conversion is handled in Razor.cs
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Save record (Add or Edit mode)
        /// Note: Entity preparation (R_Saving logic) should be done in Razor.cs before calling this method
        /// </summary>
        /// <param name="poEntity">Entity to save (must be prepared with all required fields)</param>
        /// <param name="peCRUDMode">CRUD mode (Add or Edit)</param>
        public async Task SaveRecordAsync(FAT00700DTO poEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _fat00700Model.R_ServiceSaveAsync(poEntity, peCRUDMode);
                CurrentRecord = loResult ?? new FAT00700DTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Delete record
        /// </summary>
        /// <param name="poEntity">Entity to delete</param>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcUserId">User ID</param>
        /// <param name="pcTransactionCode">Transaction code (default: "260010")</param>
        public async Task DeleteRecordAsync(FAT00700DTO poEntity, string pcCompanyId, string pcUserId, string pcTransactionCode)
        {
            var loEx = new R_Exception();

            try
            {
                if (poEntity != null)
                {
                    poEntity.CCOMPANY_ID = pcCompanyId;
                    poEntity.CUSER_ID = pcUserId;
                    //poEntity.CTRANSACTION_CODE = pcTransactionCode;
                    await _fat00700Model.R_ServiceDeleteAsync(poEntity);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Initialization Methods

        public async Task GetCompanyInfoAsync(FAT00700CompanyInfoParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loResult = await _fat00700Model.GetCompanyInfo(poParameter);
                CompanyInfo = loResult.Data ?? new FAT00700CompanyInfoResultDTO();
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetSystemParamAsync(FAT00700SystemParamParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loResult = await _fat00700Model.GetSystemParam(poParameter);
                SystemParam = loResult.Data ?? new FAT00700SystemParamResultDTO();

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPeriodInfoAsync(FAT00700PeriodInfoParamDTO poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                poParameter.CYEAR = SystemParam.CSOFT_PERIOD_YY;
                poParameter.CPERIOD_NO = SystemParam.CSOFT_PERIOD_MM;
                var loResult = await _fat00700Model.GetPeriodInfo(poParameter);
                PeriodInfo = loResult.Data ?? new FAT00700PeriodInfoResultDTO();
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTransCodeInfoAsync(FAT00700TransCodeInfoParamDTO poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loResult = await _fat00700Model.GetTransCodeInfo(poParameter);
                TransCodeInfo = loResult.Data ?? new FAT00700TransCodeInfoResultDTO();
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPeriodRangeAsync(FAT00700PeriodRangeParamDTO poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loResult = await _fat00700Model.GetPeriodRange(poParameter);
                poParameter.CCYEAR = "";
                poParameter.CMODE = "";
                PeriodRange = loResult.Data ?? new FAT00700PeriodRangeResultDTO();
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Initialize form - Get period, currency, transaction data, and user rights
        /// This method calls multiple initialization methods and stores results in properties
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcLangId">Language ID</param>
        /// <param name="pcTransactionCode">Transaction code (default: "260010")</param>
        /// <param name="pcActivityCode">Activity code (default: "FA013001")</param>
        /// <param name="pcUserId">User ID</param>
        public async Task GetInitialProcessAsync(string pcCompanyId, string pcLangId, string pcTransactionCode, string pcActivityCode, string pcUserId)
        {
            var loEx = new R_Exception();

            try
            {
                // Get period information
                await GetPeriodAsync(pcCompanyId);

                // Get currency information
                await GetCurrencyAsync(pcCompanyId);

                // Get FA transaction data
                await GetFATransactionDataAsync(pcCompanyId, pcLangId, pcTransactionCode);

                // Get user right approval
                await GetUserRightApprovalAsync(pcCompanyId, pcTransactionCode, pcUserId);

                // Get user activity rights
                await GetUserActivityRightsAsync(pcCompanyId, pcActivityCode, pcUserId);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get period information
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        public async Task GetPeriodAsync(string pcCompanyId)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GetPeriodParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId
                };

                var loResult = await _fat00700Model.GetPeriod(loParam);
                PeriodResult = loResult.Data ?? new GetPeriodResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get currency information
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        public async Task GetCurrencyAsync(string pcCompanyId)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GetCurrencyParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId
                };

                var loResult = await _fat00700Model.GetCurrency(loParam);
                CurrencyResult = loResult.Data ?? new GetCurrencyResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get FA transaction data
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcLangId">Language ID</param>
        /// <param name="pcTransactionCode">Transaction code</param>
        public async Task GetFATransactionDataAsync(string pcCompanyId, string pcLangId, string pcTransactionCode)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GetFATransactionDataParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CLANGID = pcLangId,
                    CTRANSACTION_CODE = pcTransactionCode
                };

                var loResult = await _fat00700Model.GetFATransactionData(loParam);
                TransactionDataResult = loResult.Data ?? new GetFATransactionDataResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get asset information (rates)
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcAssetCode">Asset code</param>
        public async Task GetAssetInformationAsync(string pcCompanyId, string pcAssetCode)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GetAssetInformationParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CASSET_CODE = pcAssetCode
                };

                var loResult = await _fat00700Model.GetAssetInformation(loParam);
                AssetInformationResult = loResult.Data ?? new GetAssetInformationResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get asset info data for tab display
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcLangId">Language ID</param>
        /// <param name="pcAssetCode">Asset code</param>
        public async Task GetAssetInfoDataAsync(string pcCompanyId, string pcLangId, string pcAssetCode)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GetAssetInfoDataParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CLANGID = pcLangId,
                    CASSET_CODE = pcAssetCode
                };

                var loResult = await _fat00700Model.GetAssetInfoData(loParam);
                AssetInfoDataResult = loResult.Data ?? new GetAssetInfoDataResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get user approval rights
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcTransactionCode">Transaction code</param>
        /// <param name="pcUserId">User ID</param>
        public async Task GetUserRightApprovalAsync(string pcCompanyId, string pcTransactionCode, string pcUserId)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GetUserRightApprovalParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CTRANSACTION_CODE = pcTransactionCode,
                    CUSER_ID = pcUserId
                };

                var loResult = await _fat00700Model.GetUserRightApproval(loParam);
                UserRightApprovalResult = loResult.Data ?? new GetUserRightApprovalResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get user activity rights
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcActivityCode">Activity code</param>
        /// <param name="pcUserId">User ID</param>
        public async Task GetUserActivityRightsAsync(string pcCompanyId, string pcActivityCode, string pcUserId)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GetUserActivityRightsParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CACTIVITY_CODE = pcActivityCode,
                    CUSER_ID = pcUserId
                };

                var loResult = await _fat00700Model.GetUserActivityRights(loParam);
                UserActivityRightsResult = loResult.Data ?? new GetUserActivityRightsResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Validation Methods

        public void ValidationDepartment()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (string.IsNullOrEmpty(Data.CDEPT_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS012"));
                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Process Methods (Button Actions)

        /// <summary>
        /// Submit transaction
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcDeptCode">Department code</param>
        /// <param name="pcTransactionCode">Transaction code</param>
        /// <param name="pcReferenceNo">Reference number</param>
        /// <param name="pcUserId">User ID</param>
        

        /// <summary>
        /// Close transaction (with GL validation)
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcDeptCode">Department code</param>
        /// <param name="pcTransactionCode">Transaction code</param>
        /// <param name="pcReferenceNo">Reference number</param>
        /// <param name="pcUserId">User ID</param>
        public async Task CloseButtonAsync(string pcCompanyId, string pcDeptCode, string pcTransactionCode, string pcReferenceNo, string pcUserId)
        {
            var loEx = new R_Exception();

            try
            {
                // Business logic from VB.NET: Validate GL journal first (CR03 MA - 8/31/2023)
                var loValidationParam = new ValidateGLJournalParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CDEPT_CODE = pcDeptCode,
                    CTRANSACTION_CODE = pcTransactionCode,
                    CREFERENCE_NO = pcReferenceNo,
                    CUSER_ID = pcUserId
                };

                await _fat00700Model.ValidateGLJournal(loValidationParam);

                // Close transaction
                var loParam = new CloseButtonParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CDEPT_CODE = pcDeptCode,
                    CTRANSACTION_CODE = pcTransactionCode,
                    CREFERENCE_NO = pcReferenceNo,
                    CUSER_ID = pcUserId
                };

                await _fat00700Model.CloseButton(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Approve transaction
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcDeptCode">Department code</param>
        /// <param name="pcTransactionCode">Transaction code</param>
        /// <param name="pcReferenceNo">Reference number</param>
        /// <param name="pcUserId">User ID</param>
        public async Task ApproveButtonAsync(string pcCompanyId, string pcDeptCode, string pcTransactionCode, string pcReferenceNo, string pcUserId)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new ApproveButtonParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CDEPT_CODE = pcDeptCode,
                    CTRANSACTION_CODE = pcTransactionCode,
                    CREFERENCE_NO = pcReferenceNo,
                    CUSER_ID = pcUserId
                };

                await _fat00700Model.ApproveButton(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Void transaction
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcDeptCode">Department code</param>
        /// <param name="pcTransactionCode">Transaction code</param>
        /// <param name="pcReferenceNo">Reference number</param>
        /// <param name="pcUserId">User ID</param>
        /// <param name="pcCancelReasonCode">Cancel reason code</param>
        /// <param name="pcCancelApprovedBy">Cancel approved by</param>
        public async Task VoidButtonAsync(string pcCompanyId, string pcDeptCode, string pcTransactionCode, string pcReferenceNo, string pcUserId, string pcCancelReasonCode, string pcCancelApprovedBy)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new VoidButtonParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CDEPT_CODE = pcDeptCode,
                    CTRANSACTION_CODE = pcTransactionCode,
                    CREFERENCE_NO = pcReferenceNo,
                    CUSER_ID = pcUserId,
                    CCANCEL_REASON_CODE = pcCancelReasonCode,
                    CCANCEL_APPROVED_BY = pcCancelApprovedBy
                };

                await _fat00700Model.VoidButton(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Validate GL journal before close
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcDeptCode">Department code</param>
        /// <param name="pcTransactionCode">Transaction code</param>
        /// <param name="pcReferenceNo">Reference number</param>
        /// <param name="pcUserId">User ID</param>
        public async Task ValidateGLJournalAsync(string pcCompanyId, string pcDeptCode, string pcTransactionCode, string pcReferenceNo, string pcUserId)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new ValidateGLJournalParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CDEPT_CODE = pcDeptCode,
                    CTRANSACTION_CODE = pcTransactionCode,
                    CREFERENCE_NO = pcReferenceNo,
                    CUSER_ID = pcUserId
                };

                await _fat00700Model.ValidateGLJournal(loParam);
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
                var loResult = await _fat00700Model.R_ServiceSaveAsync(CurrentRecord, R_CommonFrontBackAPI.eCRUDMode.AddMode);

                CurrentRecord = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Check outstanding transactions
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcAssetCode">Asset code</param>
        public async Task CheckOutstandingTransAsync(string pcCompanyId, string pcAssetCode)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new CheckOutstandingTransParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CASSET_CODE = pcAssetCode
                };

                var loResult = await _fat00700Model.CheckOutstandingTrans(loParam);
                OutstandingTransResult = loResult.Data ?? new CheckOutstandingTransResultDTO();

                // Business logic from VB.NET: If result is not null and has CASSET_CODE, add error PS003
                if (OutstandingTransResult != null && !string.IsNullOrWhiteSpace(OutstandingTransResult.CASSET_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS003"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get transaction date validation (period)
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcTransactionDate">Transaction date (format: yyyyMMdd)</param>
        public async Task GetTransDateValidationAsync(string pcCompanyId, string pcTransactionDate)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GetTransDateValidationParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CTRANSACTION_DATE = pcTransactionDate
                };

                var loResult = await _fat00700Model.GetTransDateValidation(loParam);
                TransDateValidationResult = loResult.Data ?? new GetTransDateValidationResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get date status (last transaction date, next depreciation period, asset status)
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcAssetCode">Asset code</param>
        public async Task GetDateStatusAsync(string pcCompanyId, string pcAssetCode)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GetDateStatusParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CASSET_CODE = pcAssetCode
                };

                var loResult = await _fat00700Model.GetDateStatus(loParam);
                DateStatusResult = loResult.Data ?? new GetDateStatusResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Check if transaction number already exists
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcDeptCode">Department code</param>
        /// <param name="pcTransactionCode">Transaction code</param>
        /// <param name="pcReferenceNo">Reference number (Transaction Number)</param>
        /// <returns>True if exists, False otherwise</returns>
        public async Task<bool> CheckTransactionNumberExistsAsync(string pcCompanyId, string pcDeptCode, string pcTransactionCode, string pcReferenceNo)
        {
            var loEx = new R_Exception();
            bool llResult = false;

            try
            {
                // Check if transaction exists by trying to get it
                var loEntity = new FAT00700DTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CDEPT_CODE = pcDeptCode,
                    //CTRANSACTION_CODE = pcTransactionCode,
                    CREF_NO = pcReferenceNo
                };

                try
                {
                    var loResult = await _fat00700Model.R_ServiceGetRecordAsync(loEntity);
                    if (loResult != null && !string.IsNullOrWhiteSpace(loResult.CREF_NO))
                    {
                        llResult = true;
                    }
                }
                catch
                {
                    // If record not found, exception is thrown, so it doesn't exist
                    llResult = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return llResult;
        }

        /// <summary>
        /// Main validation method for transaction (from R_Validation event handler)
        /// Implements all validation rules from VB.NET R_Validation
        /// </summary>
        /// <param name="poEntity">Entity to validate</param>
        /// <param name="peMode">CRUD mode</param>
        public async Task ValidateTransactionAsync(FAT00700DTO poEntity, eCRUDMode peMode = eCRUDMode.AddMode)
        {
            var loEx = new R_Exception();

            try
            {
                // 1. Department code should not be empty
                if (string.IsNullOrWhiteSpace(poEntity.CDEPT_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS005"));
                }

                // 2. Transaction Date should not be empty
                if (string.IsNullOrWhiteSpace(poEntity.CREF_DATE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS006"));
                }

                // 3. Asset code should not be empty
                if (string.IsNullOrWhiteSpace(poEntity.CASSET_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS007"));
                }

                // 4. Allocation code should not be empty
                if (string.IsNullOrWhiteSpace(poEntity.CEXPENSE_ALLOC_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS008"));
                }

                // If basic validations fail, stop here
                if (loEx.HasError)
                {
                    loEx.ThrowExceptionIfErrors();
                    return;
                }

                // 5. If Add mode, validate outstanding transaction
                if (peMode == eCRUDMode.AddMode)
                {
                    await CheckOutstandingTransAsync(poEntity.CCOMPANY_ID, poEntity.CASSET_CODE);
                    if (OutstandingTransResult != null && !string.IsNullOrWhiteSpace(OutstandingTransResult.CASSET_CODE))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS003"));
                    }
                }

                // 6. Get asset validation info (CLAST_TRANS_DATE, CNEXT_DEPR_PERIOD, CASSET_STATUS)
                await GetDateStatusAsync(poEntity.CCOMPANY_ID, poEntity.CASSET_CODE);

                if (DateStatusResult != null)
                {
                    // 7. Check if asset status allows transaction
                    // Logic: (Edit Mode and (LCHANGE_DESC=1 or LCHANGE_ALLOC=1)) or CASSET_STATUS in ('1','2')
                    bool llStatusNotAllowed = false;
                    //if ((peMode == eCRUDMode.EditMode && (poEntity.LCHANGE_DESC == true || poEntity.LCHANGE_ALLOC == true)) ||
                    //    (DateStatusResult.CASSET_STATUS == "1" || DateStatusResult.CASSET_STATUS == "2"))
                    //{
                    //    llStatusNotAllowed = true;
                    //}

                    if (llStatusNotAllowed)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS002"));
                    }

                    // 8. Transaction Date should not be before the last one of this asset
                    if (!string.IsNullOrWhiteSpace(poEntity.CREF_DATE) && !string.IsNullOrWhiteSpace(DateStatusResult.CLAST_TRANS_DATE))
                    {
                        // Convert dates to DateTime for comparison
                        if (DateTime.TryParseExact(poEntity.CREF_DATE, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime ldTransactionDate) &&
                            DateTime.TryParseExact(DateStatusResult.CLAST_TRANS_DATE, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime ldLastTransDate))
                        {
                            if (ldTransactionDate < ldLastTransDate)
                            {
                                loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS021"));
                            }
                        }
                    }

                    // 9. Transaction Date should not pass the next depreciation period
                    if (!string.IsNullOrWhiteSpace(poEntity.CREF_DATE) && !string.IsNullOrWhiteSpace(DateStatusResult.CNEXT_DEPR_PERIOD))
                    {
                        // Get period from transaction date (first 6 characters = yyyyMM)
                        string lcTransactionPeriod = poEntity.CREF_DATE.Length >= 6 ? poEntity.CREF_DATE.Substring(0, 6) : string.Empty;
                        
                        if (!string.IsNullOrWhiteSpace(lcTransactionPeriod) && !string.IsNullOrWhiteSpace(DateStatusResult.CNEXT_DEPR_PERIOD))
                        {
                            // Compare periods (string comparison works for yyyyMM format)
                            if (string.Compare(lcTransactionPeriod, DateStatusResult.CNEXT_DEPR_PERIOD) > 0)
                            {
                                loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS022"));
                            }
                        }
                    }
                }

                // 10. If Add Mode and LINCREMENT_FLAG=0, validate Transaction Number
                //if (peMode == eCRUDMode.AddMode && poEntity.LINCREMENT_FLAG == false)
                //{
                //    // Transaction Number should not be empty
                //    if (string.IsNullOrWhiteSpace(poEntity.CREFERENCE_NO))
                //    {
                //        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS023"));
                //    }
                //    else
                //    {
                //        // Transaction Number should not already exist
                //        bool llExists = await CheckTransactionNumberExistsAsync(
                //            poEntity.CCOMPANY_ID,
                //            poEntity.CDEPT_CODE,
                //            poEntity.CTRANSACTION_CODE,
                //            poEntity.CREFERENCE_NO);

                //        if (llExists)
                //        {
                //            loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS004"));
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Grid Methods

        /// <summary>
        /// Get allocation grid data
        /// Note: This was originally a streaming method in VB.NET but has been converted to non-streaming in NET6
        /// The Controller currently returns only the first item, which is incorrect for a grid
        /// This should be fixed to return a list, but for now we work with the current implementation
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcLangId">Language ID</param>
        /// <param name="pcAssetCode">Asset code</param>
        public async Task GetGridAllocDataAsync(string pcCompanyId, string pcLangId, string pcAssetCode)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GetGridAllocDataParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CLANGID = pcLangId,
                    CASSET_CODE = string.IsNullOrWhiteSpace(pcAssetCode) ? string.Empty : pcAssetCode
                };

                var loResult = await _fat00700Model.GetGridAllocData(loParam);

                // Note: Current implementation returns single item, but grid needs list
                // This is a known issue in the Controller that should be fixed
                // For now, add single item to collection if available
                GridAllocList.Clear();
                if (loResult?.Data != null)
                {
                    GridAllocList.Add(loResult.Data);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTransactionListAsync()
        {
            var loEx = new R_Exception();

            try
            {
                TransactionListParameterDTO.CDEPT_CODE = Data.CDEPT_CODE;
                TransactionListParameterDTO.CTRANSACTION_CODE = "260010";
                TransactionListParameterDTO.CASSET_CODE = Data.CASSET_CODE;
                var loResult = await _fat00700Model.GetTransactionListAsync(TransactionListParameterDTO);
                TransactionList = new ObservableCollection<GetTransactionListResultDTO>(loResult);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Streaming Methods
        public async Task GetDeptListAsync()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loResult = await _fat00700Model.GetAllDeptListAsync();
                if (loResult != null)
                {
                    loListDept = loResult.Data ?? new List<FAT00700GetDeptListResultDTO>();
                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Default Value
        public void SetDefaultLookUpDepartment()
        {
            R_Exception loEx = new R_Exception();

            try
            {

                if (!string.IsNullOrEmpty(SystemParam.CTRANS_DEPT_CODE))
                {
                    Data.CDEPT_CODE = SystemParam.CTRANS_DEPT_CODE;
                    Data.CDEPT_NAME = SystemParam.CTRANS_DEPT_NAME;
                }
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

