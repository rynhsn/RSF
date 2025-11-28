using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FAT00800Common;
using FAT00800Common.DTOs;
using FAT00800FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace FAT00800Model.VMs
{
    /// <summary>
    /// ViewModel for FAT00800 - Fixed Asset Transaction operations
    /// Handles UI state and data binding for transaction management
    /// </summary>
    public class FAT00800ViewModel : R_ViewModel<FAT00800DTO>
    {
        private readonly FAT00800Model _model = new FAT00800Model();

        // Main entity for R_Conductor synchronization
        public FAT00800DTO Entity { get; set; } = new FAT00800DTO();


        // Initial process properties
        public FAT00800GetPeriodResultDTO Period { get; set; } = new FAT00800GetPeriodResultDTO();
        public FAT00800GetLocalBaseCurrResultDTO Currency { get; set; } = new FAT00800GetLocalBaseCurrResultDTO();
        public FAT00800GetTransTypeDescResultDTO TransTypeDesc { get; set; } = new FAT00800GetTransTypeDescResultDTO();
        public ObservableCollection<FAT00800TransListResultDTO> TransList { get; set; } = new ObservableCollection<FAT00800TransListResultDTO>();
        public int UserRightApproval { get; set; }
        public int UserActivityRights { get; set; }

        // Validation flags
        public bool ValDate1 { get; set; }
        public bool ValDate2 { get; set; }
        public bool ValDate3 { get; set; }

        // Mode flags
        public bool LCHANGE_DESC { get; set; }
        public bool LCHANGE_ALLOC { get; set; }
        public bool Lfind { get; set; }
        public bool LrefreshLook { get; set; }
        public bool LINCREMENT_FLAG { get; set; }
        public bool LTRANS_APPROVAL { get; set; }
        public bool LGLLINK { get; set; }

        // Global parameters (from initial process)
        public string CLOCAL_CURRENCY_CODE { get; set; } = string.Empty;
        public string CBASE_CURRENCY_CODE { get; set; } = string.Empty;
        public string CSOFT_PERIOD { get; set; } = string.Empty;
        public string CRATETYPE_CODE { get; set; } = string.Empty;
        public string CGLLINK_DATE { get; set; } = string.Empty;
        public string CDEFAULT_TRX_DEPT_CODE { get; set; } = string.Empty;
        public string CCURRENT_PERIOD { get; set; } = string.Empty;
        public int ILCAN_CLOSE { get; set; }
        public int ILCAN_APPROVE { get; set; }

        // Period filter properties
        public int PeriodFromYear { get; set; } = DateTime.Now.Year;
        public string PeriodFromMonth { get; set; } = "01";
        public int PeriodToYear { get; set; } = DateTime.Now.Year;
        public string PeriodToMonth { get; set; } = "12";

        public bool LenableEdit { get; set; }

        // Month List for ComboBox
        public List<PeriodMonthDTO> MonthList { get; set; } = new List<PeriodMonthDTO>
        {
            new PeriodMonthDTO { CPERIOD_NO = "01" },
            new PeriodMonthDTO { CPERIOD_NO = "02" },
            new PeriodMonthDTO { CPERIOD_NO = "03" },
            new PeriodMonthDTO { CPERIOD_NO = "04" },
            new PeriodMonthDTO { CPERIOD_NO = "05" },
            new PeriodMonthDTO { CPERIOD_NO = "06" },
            new PeriodMonthDTO { CPERIOD_NO = "07" },
            new PeriodMonthDTO { CPERIOD_NO = "08" },
            new PeriodMonthDTO { CPERIOD_NO = "09" },
            new PeriodMonthDTO { CPERIOD_NO = "10" },
            new PeriodMonthDTO { CPERIOD_NO = "11" },
            new PeriodMonthDTO { CPERIOD_NO = "12" }
        };

        // Constants
        public const string VAR_CTRANS_CODE = "270010";
        public const string VAR_CACTIVITY_CODE = "FA013001";

        public string loCurrencyTemp { get; set; } = string.Empty;

        // OnChange Sale Amount - Validation flags
        public bool ValAmountPositive { get; set; }
        public bool ValCurrencyRatesValid { get; set; }
        public bool ValCalculationComplete { get; set; }

        // OnChange Sale Amount - Tax and Commission flags (for future use)
        public bool IsTaxApplicable { get; set; }
        public bool IsCommissionApplicable { get; set; }

        #region CRUD Methods

        /// <summary>
        /// Get single transaction record
        /// </summary>
        /// <param name="poEntity">Transaction entity to retrieve</param>
        /// <returns>Task</returns>
        public async Task GetRecordAsync(FAT00800DTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                // Debug: Log the input
                System.Diagnostics.Debug.WriteLine($"ViewModel.GetRecordAsync - Input: CompanyId={poEntity.CCOMPANY_ID}, RefNo={poEntity.CREFERENCE_NO}, TransCode={poEntity.CTRANSACTION_CODE}");

                var loResult = await _model.R_ServiceGetRecordAsync(poEntity);
                
                // Debug: Log the model result
                System.Diagnostics.Debug.WriteLine($"ViewModel.GetRecordAsync - Model Result: RefNo={loResult?.CREFERENCE_NO}, DeptCode={loResult?.CDEPT_CODE}, Status={loResult?.CSTATUS}, TransCode={loResult?.CTRANSACTION_CODE}");

                Entity = loResult; // Update Entity for R_Conductor synchronization
                
                // Parse transaction date only if not empty
                if (!string.IsNullOrWhiteSpace(Entity.CTRANSACTION_DATE))
                {
                    Entity.DTRANSACTION_DATE = DateTime.ParseExact(Entity.CTRANSACTION_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    Entity.DTRANSACTION_DATE = DateTime.Now;
                }
                
                loCurrencyTemp = Entity.CCURRENCY_CODE;
                LenableEdit = CheckEnableEdit(Entity.CSTATUS);

                // Debug: Log the final Entity
                System.Diagnostics.Debug.WriteLine($"ViewModel.GetRecordAsync - Final Entity: RefNo={Entity.CREFERENCE_NO}, DeptCode={Entity.CDEPT_CODE}, Status={Entity.CSTATUS}");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                System.Diagnostics.Debug.WriteLine($"ViewModel.GetRecordAsync - Error: {ex.Message}");
            }
            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Save (add/edit) transaction record
        /// </summary>
        /// <param name="poEntity">Transaction entity to save</param>
        /// <param name="peCRUDMode">CRUD mode (Add/Edit)</param>
        /// <returns>Task</returns>
        public async Task SaveRecordAsync(FAT00800DTO poEntity, R_eConductorMode peCRUDMode)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _model.R_ServiceSaveAsync(poEntity, (eCRUDMode)peCRUDMode);
                Entity = loResult; // Update Entity for R_Conductor synchronization
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Delete transaction record
        /// </summary>
        /// <param name="poEntity">Transaction entity to delete</param>
        /// <returns>Task</returns>
        public async Task DeleteRecordAsync(FAT00800DTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                await _model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Initial Process Methods

        /// <summary>
        /// Get initial process data (Period, Currency, TransTypeDesc, UserRights, ActivityRights)
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcLangId">Language ID</param>
        /// <param name="pcUserId">User ID</param>
        /// <returns>Task</returns>
        public async Task GetInitialProcessAsync(string pcCompanyId, string pcLangId, string pcUserId)
        {
            var loEx = new R_Exception();
            try
            {
                // GetPeriod
                var loPeriodParam = new FAT00800GetPeriodParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId
                };
                var loPeriodResult = await _model.GetPeriod(loPeriodParam);
                Period = loPeriodResult.Data;

                // GetLocalBaseCurr
                var loCurrParam = new FAT00800GetLocalBaseCurrParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId
                };
                var loCurrResult = await _model.GetLocalBaseCurr(loCurrParam);
                Currency = loCurrResult.Data;

                // GetTransTypeDesc
                var loDescParam = new FAT00800GetTransTypeDescParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CLANG_ID = pcLangId,
                    CTRANSACTION_CODE = VAR_CTRANS_CODE
                };
                var loDescResult = await _model.GetTransTypeDesc(loDescParam);
                TransTypeDesc = loDescResult.Data;

                // GetUserRightApproval
                var loUserRightParam = new FAT00800GetUserRightApprovalParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CTRANSACTION_CODE = VAR_CTRANS_CODE,
                    CUSER_ID = pcUserId
                };
                var loUserRightResult = await _model.GetUserRightApproval(loUserRightParam);
                UserRightApproval = loUserRightResult.Data.Result;

                // GetUserActivityRights
                var loActivityParam = new FAT00800GetUserActivityRightsParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CACTIVITY_CODE = VAR_CACTIVITY_CODE,
                    CUSER_ID = pcUserId
                };
                var loActivityResult = await _model.GetUserActivityRights(loActivityParam);
                UserActivityRights = loActivityResult.Data.Result;

                // Set global parameters
                LTRANS_APPROVAL = TransTypeDesc.LTRANS_APPROVAL;
                ILCAN_CLOSE = UserActivityRights;
                ILCAN_APPROVE = UserRightApproval;
                CLOCAL_CURRENCY_CODE = Currency.CLOCAL_CURRENCY_CODE;
                CBASE_CURRENCY_CODE = Currency.CBASE_CURRENCY_CODE;
                CSOFT_PERIOD = Period.CSOFT_PERIOD;
                LINCREMENT_FLAG = TransTypeDesc.LINCREMENT_FLAG;
                CRATETYPE_CODE = Period.CRATETYPE_CODE;
                CGLLINK_DATE = Period.CGLLINK_DATE;
                CDEFAULT_TRX_DEPT_CODE = Period.CDEFAULT_TRX_DEPT_CODE;
                CCURRENT_PERIOD = Period.CCURRENT_PERIOD;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Validation Methods

        /// <summary>
        /// Validate department code
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcDeptCode">Department code to validate</param>
        /// <param name="pcUserId">User ID</param>
        /// <returns>Task with validation result (0 = invalid, 1 = valid)</returns>
        public async Task<int> ValidateDepartmentAsync(string pcCompanyId, string pcDeptCode, string pcUserId)
        {
            var loEx = new R_Exception();
            int liResult = 0;
            try
            {
                var loParam = new FAT00800GetValidateDepartmentParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CDEPT_CODE = pcDeptCode,
                    CUSER_ID = pcUserId
                };
                var loResult = await _model.GetValidateDepartment(loParam);
                liResult = loResult.Data.Result;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return liResult;
        }

        /// <summary>
        /// Validate transaction date
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pdTransDate">Transaction date to validate</param>
        /// <param name="pcLangId">Language ID</param>
        /// <returns>Task with validation result (CPRD)</returns>
        public async Task<string> ValidateTransDateAsync(string pcCompanyId, DateTime pdTransDate, string pcLangId)
        {
            var loEx = new R_Exception();
            string lcCprd = string.Empty;
            try
            {
                var loParam = new FAT00800GetValidateTransDateParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CTRANSACTION_DATE = pdTransDate.ToString("yyyyMMdd"),
                    CLANG_ID = pcLangId
                };
                var loResult = await _model.GetValidateTransDate(loParam);
                lcCprd = loResult.Data.CPRD;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return lcCprd;
        }

        /// <summary>
        /// Validate outstanding transaction
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcAssetCode">Asset code to validate</param>
        /// <returns>Task with validation result (CASSET_CODE if outstanding exists)</returns>
        public async Task<string> ValidateOutstandTransAsync(string pcCompanyId, string pcAssetCode)
        {
            var loEx = new R_Exception();
            string lcAssetCode = string.Empty;
            try
            {
                var loParam = new FAT00800GetValidateOutstandTransParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CASSET_CODE = pcAssetCode
                };
                var loResult = await _model.GetValidateOutstandTrans(loParam);
                lcAssetCode = loResult.Data.CASSET_CODE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return lcAssetCode;
        }

        /// <summary>
        /// Validate void operation
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcAssetCode">Asset code</param>
        /// <param name="pcAssetTransSeqNo">Asset transaction sequence number</param>
        /// <returns>Task with validation result (CASSET_CODE if cannot void)</returns>
        public async Task<string> ValidateVoidAsync(string pcCompanyId, string pcAssetCode, string pcAssetTransSeqNo)
        {
            var loEx = new R_Exception();
            string lcAssetCode = string.Empty;
            try
            {
                var loParam = new FAT00800GetValidateVoidParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CASSET_CODE = pcAssetCode,
                    CASSET_TRANS_SEQNO = pcAssetTransSeqNo
                };
                var loResult = await _model.GetValidateVoid(loParam);
                lcAssetCode = loResult.Data.CASSET_CODE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return lcAssetCode;
        }

        /// <summary>
        /// Validate GL before close
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcDeptCode">Department code</param>
        /// <param name="pcTransactionCode">Transaction code</param>
        /// <param name="pcReferenceNo">Reference number</param>
        /// <returns>Task</returns>
        public async Task ValidateGLAsync(string pcCompanyId, string pcDeptCode, string pcTransactionCode, string pcReferenceNo)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new FAT00800GetValidateGLParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CDEPT_CODE = pcDeptCode,
                    CTRANSACTION_CODE = pcTransactionCode,
                    CREFERENCE_NO = pcReferenceNo
                };
                await _model.GetValidateGL(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Validate record before save (returns R_Exception with validation errors)
        /// </summary>
        /// <param name="poEntity">Transaction entity to validate</param>
        /// <param name="peMode">Conductor mode</param>
        /// <returns>R_Exception with validation errors if any</returns>
        public R_Exception ValidateRecord(FAT00800DTO poEntity, R_eConductorMode peMode)
        {
            var loEx = new R_Exception();

            // Date validations
            if (ValDate1)
            {
                loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS005"));
            }
            if (ValDate2)
            {
                loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS006"));
            }
            if (ValDate3)
            {
                loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS023"));
            }

            // Required field validations
            if (string.IsNullOrWhiteSpace(poEntity.CDEPT_CODE))
            {
                loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS013"));
            }
            if (string.IsNullOrWhiteSpace(poEntity.CTRANSACTION_DATE))
            {
                loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS014"));
            }
            if (string.IsNullOrWhiteSpace(poEntity.CASSET_CODE))
            {
                loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS015"));
            }
            if (string.IsNullOrWhiteSpace(poEntity.CCURRENCY_CODE))
            {
                loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS016"));
            }
            if (string.IsNullOrWhiteSpace(poEntity.CALLOC_EXPENSE_CODE))
            {
                loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS017"));
            }

            // Amount validations
            if (poEntity.NTRANSACTION_AMOUNT1 < 0)
            {
                loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS018"));
            }

            // Currency rate validations (Note: These would need to be passed as parameters or stored in properties)
            // The validation logic checks if both base and currency amounts are zero
            // This is handled in the UI layer (Razor.cs) as it requires access to UI controls

            return loEx;
        }

        #endregion

        #region Display/Helper Methods

        /// <summary>
        /// Get book value for asset
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcAssetCode">Asset code</param>
        /// <returns>Task with book value result (Local and Base)</returns>
        public async Task<(decimal NLBOOKVAL, decimal NBBOOKVAL)> GetBookValueAsync(string pcCompanyId, string pcAssetCode)
        {
            var loEx = new R_Exception();
            decimal lnLocalBookVal = 0;
            decimal lnBaseBookVal = 0;
            try
            {
                var loParam = new FAT00800GetBookValueParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CASSET_CODE = pcAssetCode
                };
                var loResult = await _model.GetBookValue(loParam);
                lnLocalBookVal = loResult.Data.NLBOOKVAL;
                lnBaseBookVal = loResult.Data.NBBOOKVAL;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return (lnLocalBookVal, lnBaseBookVal);
        }

        /// <summary>
        /// Get currency rates
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcCurrencyCode">Currency code</param>
        /// <param name="pcRateTypeCode">Rate type code</param>
        /// <param name="pcTransDate">Transaction date</param>
        /// <returns>Task with currency rate result</returns>
        public async Task<FAT00800GetCurrencyResultDTO> GetCurrencyAsync(string pcCompanyId, string pcCurrencyCode, string pcRateTypeCode, string pcTransDate)
        {
            var loEx = new R_Exception();
            FAT00800GetCurrencyResultDTO loResult = new FAT00800GetCurrencyResultDTO();
            try
            {
                var loParam = new FAT00800GetCurrencyParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CCURRENCY_CODE = pcCurrencyCode,
                    CRATETYPE_CODE = pcRateTypeCode,
                    CTRANSACTION_DATE = pcTransDate
                };
                var loCurrencyResult = await _model.GetCurrency(loParam);
                loResult = loCurrencyResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }



        #endregion

        #region Business Operation Methods

        /// <summary>
        /// Submit transaction (submit or draft)
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcLangId">Language ID</param>
        /// <param name="pcUserId">User ID</param>
        /// <param name="pcDeptCode">Department code</param>
        /// <param name="pcReferenceNo">Reference number</param>
        /// <returns>Task</returns>
        public async Task SubmitAsync(string pcCompanyId, string pcLangId, string pcUserId, string pcDeptCode, string pcReferenceNo)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new FAT00800DoSubmitParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CLANG_ID = pcLangId,
                    CUSER_ID = pcUserId,
                    CDEPT_CODE = pcDeptCode,
                    CTRANSACTION_CODE = VAR_CTRANS_CODE,
                    CREFERENCE_NO = pcReferenceNo
                };
                await _model.DoSubmit(loParam);
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
        /// <param name="pcLangId">Language ID</param>
        /// <param name="pcUserId">User ID</param>
        /// <param name="pcDeptCode">Department code</param>
        /// <param name="pcReferenceNo">Reference number</param>
        /// <returns>Task</returns>
        public async Task ApproveAsync(string pcCompanyId, string pcLangId, string pcUserId, string pcDeptCode, string pcReferenceNo)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new FAT00800DoApproveParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CLANG_ID = pcLangId,
                    CUSER_ID = pcUserId,
                    CDEPT_CODE = pcDeptCode,
                    CTRANSACTION_CODE = VAR_CTRANS_CODE,
                    CREFERENCE_NO = pcReferenceNo
                };
                await _model.DoApprove(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Close transaction
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcLangId">Language ID</param>
        /// <param name="pcUserId">User ID</param>
        /// <param name="pcDeptCode">Department code</param>
        /// <param name="pcReferenceNo">Reference number</param>
        /// <returns>Task</returns>
        public async Task CloseAsync(string pcCompanyId, string pcLangId, string pcUserId, string pcDeptCode, string pcReferenceNo)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new FAT00800DoCloseParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CLANG_ID = pcLangId,
                    CUSER_ID = pcUserId,
                    CDEPT_CODE = pcDeptCode,
                    CTRANSACTION_CODE = VAR_CTRANS_CODE,
                    CREFERENCE_NO = pcReferenceNo
                };
                await _model.DoClose(loParam);
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
        /// <param name="pcLangId">Language ID</param>
        /// <param name="pcUserId">User ID</param>
        /// <param name="pcDeptCode">Department code</param>
        /// <param name="pcReferenceNo">Reference number</param>
        /// <param name="pcCancelReasonCode">Cancel reason code</param>
        /// <param name="pcCancelApprovedBy">Cancel approved by</param>
        /// <returns>Task</returns>
        public async Task VoidAsync(string pcCompanyId, string pcLangId, string pcUserId, string pcDeptCode, string pcReferenceNo, string pcCancelReasonCode, string pcCancelApprovedBy)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new FAT00800DoVoidParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CLANG_ID = pcLangId,
                    CUSER_ID = pcUserId,
                    CDEPT_CODE = pcDeptCode,
                    CTRANSACTION_CODE = VAR_CTRANS_CODE,
                    CREFERENCE_NO = pcReferenceNo,
                    CCANCEL_REASON_CODE = pcCancelReasonCode,
                    CCANCEL_APPROVED_BY = pcCancelApprovedBy
                };
                await _model.DoVoid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get approval precheck (check if approval is required for void)
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <returns>Task with boolean result (true if approval required)</returns>
        public async Task<bool> GetApprovalPrecheckAsync(string pcCompanyId)
        {
            var loEx = new R_Exception();
            bool llResult = false;
            try
            {
                var loParam = new FAT00800GetApprovalPrecheckParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId
                };
                var loResult = await _model.GetApprovalPrecheck(loParam);
                llResult = loResult.Data.Result;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return llResult;
        }

        /// <summary>
        /// Change description mode - sets LCHANGE_DESC flag
        /// </summary>
        public void SetChangeDescriptionMode()
        {
            LCHANGE_DESC = true;
        }

        /// <summary>
        /// Change allocation mode - sets LCHANGE_ALLOC flag
        /// </summary>
        public void SetChangeAllocationMode()
        {
            LCHANGE_ALLOC = true;
        }

        /// <summary>
        /// Reset change flags
        /// </summary>
        public void ResetChangeFlags()
        {
            LCHANGE_DESC = false;
            LCHANGE_ALLOC = false;
        }

        #endregion

        #region Streaming Methods
        /// <summary>
        /// Get transaction header list (streaming method)
        /// </summary>
        /// <param name="pcTransCode">Transaction code</param>
        /// <param name="pcDeptCode">Department code</param>
        /// <param name="pcFromPeriod">From period (YYYYMM)</param>
        /// <param name="pcToPeriod">To period (YYYYMM)</param>
        /// <param name="pcAssetCode">Asset code (optional)</param>
        /// <param name="pcLanguageId">Language ID</param>
        /// <returns>Task</returns>
        public async Task GetTransListAsync(string pcTransCode, string pcDeptCode, string pcFromPeriod, string pcToPeriod, string pcAssetCode, string pcLanguageId)
        {
            var loEx = new R_Exception();
            try
            {
                // Set streaming context for custom parameters (not available in R_BackGlobalVar)
                R_FrontContext.R_SetStreamingContext(ContextConstants.CTRANS_CODE, pcTransCode);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CDEPT_CODE, pcDeptCode);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CFROM_PERIOD, pcFromPeriod);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CTO_PERIOD, pcToPeriod);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CASSET_CODE, pcAssetCode);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CLANGUAGE_ID, pcLanguageId);

                // FAT00800TransList method moved to FAT00800ListModel
                // Use FAT00800ListViewModel for transaction list operations
                var loListModel = new FAT00800ListModel();
                var loResult = await loListModel.FAT00800TransListAsync();
                TransList = new ObservableCollection<FAT00800TransListResultDTO>(loResult.Data ?? new List<FAT00800TransListResultDTO>());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Calculation Methods

        /// <summary>
        /// Calculate sale amount (preserve exact VB.NET business logic)
        /// Formula: SaleAmount = SaleOriginalAmount * CurrencyAmount / BaseAmount
        /// </summary>
        /// <param name="pnSaleOriginalAmount">Sale original amount</param>
        /// <param name="pnLocalCurrCurrencyAmount">Local currency amount</param>
        /// <param name="pnLocalCurrBaseAmount">Local base amount</param>
        /// <param name="pnBaseCurrCurrencyAmount">Base currency amount</param>
        /// <param name="pnBaseCurrBaseAmount">Base base amount</param>
        /// <returns>Tuple (LocalAmount, BaseAmount)</returns>
        public (decimal LocalAmount, decimal BaseAmount) CalculateSaleAmount(
            decimal pnSaleOriginalAmount,
            decimal pnLocalCurrCurrencyAmount,
            decimal pnLocalCurrBaseAmount,
            decimal pnBaseCurrCurrencyAmount,
            decimal pnBaseCurrBaseAmount)
        {
            decimal lnLocalAmount = 0;
            decimal lnBaseAmount = 0;

            try
            {
                // Preserve exact VB.NET calculation logic
                // VB.NET: txtSaleAmountLocalAmount.Value = Math.Round(txtSaleOriginalAmount.Value * txtLocalCurrCurrencyAmount.Value / txtLocalCurrBaseAmount.Value, 2)
                lnLocalAmount = Math.Round(pnSaleOriginalAmount * pnLocalCurrCurrencyAmount / pnLocalCurrBaseAmount, 2);

                // VB.NET: txtSaleAmountBaseAmount.Value = Math.Round(txtSaleOriginalAmount.Value * txtBaseCurrCurrencyAmount.Value / txtBaseCurrBaseAmount.Value, 2)
                lnBaseAmount = Math.Round(pnSaleOriginalAmount * pnBaseCurrCurrencyAmount / pnBaseCurrBaseAmount, 2);
            }
            catch
            {
                // Preserve VB.NET behavior - no exception handling in calculation
            }

            return (lnLocalAmount, lnBaseAmount);
        }

        /// <summary>
        /// Calculate gain/loss (preserve exact VB.NET business logic)
        /// Formula: Gain = SaleAmount - BookValue
        /// </summary>
        /// <param name="pnSaleAmountLocal">Sale amount in local currency</param>
        /// <param name="pnBookValLocal">Book value in local currency</param>
        /// <param name="pnSaleAmountBase">Sale amount in base currency</param>
        /// <param name="pnBookValBase">Book value in base currency</param>
        /// <returns>Tuple (LocalGain, BaseGain)</returns>
        public (decimal LocalGain, decimal BaseGain) CalculateGain(
            decimal pnSaleAmountLocal,
            decimal pnBookValLocal,
            decimal pnSaleAmountBase,
            decimal pnBookValBase)
        {
            decimal lnLocalGain = 0;
            decimal lnBaseGain = 0;

            try
            {
                // Preserve exact VB.NET calculation logic
                // VB.NET: txtGainLocalAmount.Value = txtSaleAmountLocalAmount.Value - txtBookValLocalAmount.Value
                lnLocalGain = pnSaleAmountLocal - pnBookValLocal;

                // VB.NET: txtGainBaseAmount.Value = txtSaleAmountBaseAmount.Value - txtBookValBaseAmount.Value
                lnBaseGain = pnSaleAmountBase - pnBookValBase;
            }
            catch
            {
                // Preserve VB.NET behavior - no exception handling in calculation
            }

            return (lnLocalGain, lnBaseGain);
        }

        #endregion

        #region OnChange Sale Amount Business Process

        /// <summary>
        /// OnChange Sale Amount - Main business process method
        /// Implements the complete business process when sale amount is changed
        /// </summary>
        /// <param name="pnNewSaleAmount">New sale amount entered by user</param>
        /// <returns>Task with calculation results</returns>
        public async Task<OnChangeSaleAmountResult> OnChangeSaleAmountAsync(decimal pnNewSaleAmount)
        {
            var loEx = new R_Exception();
            var loResult = new OnChangeSaleAmountResult();

            try
            {
                // Phase 1: User Input & Field Validation
                var loValidationResult = ValidateSaleAmountInput(pnNewSaleAmount);
                if (!loValidationResult.IsValid)
                {
                    loResult.IsSuccess = false;
                    loResult.ErrorMessage = loValidationResult.ErrorMessage;
                    loResult.ErrorCode = loValidationResult.ErrorCode;
                    return loResult;
                }

                // Phase 2: Currency Rate Validation & Preparation
                var loCurrencyValidation = ValidateCurrencyRates();
                if (!loCurrencyValidation.IsValid)
                {
                    loResult.IsSuccess = false;
                    loResult.ErrorMessage = loCurrencyValidation.ErrorMessage;
                    loResult.ErrorCode = loCurrencyValidation.ErrorCode;
                    return loResult;
                }

                // Phase 3: Sale Amount Currency Conversion
                var loConversionResult = CalculateCurrencyConversion(pnNewSaleAmount);
                loResult.LocalSaleAmount = loConversionResult.LocalAmount;
                loResult.BaseSaleAmount = loConversionResult.BaseAmount;

                // Phase 4: Gain/Loss Calculation
                var loGainLossResult = CalculateGainLoss(loConversionResult.LocalAmount, loConversionResult.BaseAmount);
                loResult.LocalGainLoss = loGainLossResult.LocalGain;
                loResult.BaseGainLoss = loGainLossResult.BaseGain;
                loResult.GainLossStatus = DetermineGainLossStatus(loGainLossResult.LocalGain);

                // Phase 5: Update Entity Values
                UpdateEntityAmounts(pnNewSaleAmount, loConversionResult, loGainLossResult);

                // Phase 6: Validation & Error Handling (Post-Calculation)
                var loPostValidation = ValidateCalculatedResults(loConversionResult, loGainLossResult);
                if (!loPostValidation.IsValid)
                {
                    loResult.IsSuccess = false;
                    loResult.ErrorMessage = loPostValidation.ErrorMessage;
                    loResult.ErrorCode = loPostValidation.ErrorCode;
                    return loResult;
                }

                // Phase 7: Integration & Side Effects
                UpdateValidationFlags(pnNewSaleAmount);
                await TriggerDependentCalculations(pnNewSaleAmount);

                loResult.IsSuccess = true;
                loResult.OriginalSaleAmount = pnNewSaleAmount;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                loResult.IsSuccess = false;
                loResult.ErrorMessage = ex.Message;
                loResult.ErrorCode = "CALC_ERROR";
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        /// <summary>
        /// Phase 1: Validate sale amount input
        /// </summary>
        /// <param name="pnSaleAmount">Sale amount to validate</param>
        /// <returns>Validation result</returns>
        private ValidationResult ValidateSaleAmountInput(decimal pnSaleAmount)
        {
            var loResult = new ValidationResult { IsValid = true };

            // Basic Field Validation
            if (pnSaleAmount < 0)
            {
                loResult.IsValid = false;
                loResult.ErrorMessage = "Sale amount must be positive";
                loResult.ErrorCode = "PS018";
                return loResult;
            }

            // Check decimal precision (2 decimal places max)
            if (Math.Round(pnSaleAmount, 2) != pnSaleAmount)
            {
                // Auto-correct precision but warn
                loResult.WarningMessage = "Sale amount precision adjusted to 2 decimal places";
                loResult.WarningCode = "CALC003";
            }

            // Check for extremely large amounts
            if (pnSaleAmount > 999999999999.99m)
            {
                loResult.IsValid = false;
                loResult.ErrorMessage = "Sale amount exceeds maximum allowed value";
                loResult.ErrorCode = "CALC001";
                return loResult;
            }

            return loResult;
        }

        /// <summary>
        /// Phase 2: Validate currency rates availability
        /// </summary>
        /// <returns>Validation result</returns>
        private ValidationResult ValidateCurrencyRates()
        {
            var loResult = new ValidationResult { IsValid = true };

            // Check local currency rates
            if (Entity.NLBASE_RATE_AMOUNT == 0 || Entity.NLCURRENCY_RATE_AMOUNT == 0)
            {
                loResult.IsValid = false;
                loResult.ErrorMessage = "Currency rates cannot be zero";
                loResult.ErrorCode = "PS019";
                return loResult;
            }

            // Check base currency rates
            if (Entity.NBBASE_RATE_AMOUNT == 0 || Entity.NBCURRENCY_RATE_AMOUNT == 0)
            {
                loResult.IsValid = false;
                loResult.ErrorMessage = "Base currency rates cannot be zero";
                loResult.ErrorCode = "PS019";
                return loResult;
            }

            return loResult;
        }

        /// <summary>
        /// Phase 3: Calculate currency conversion
        /// </summary>
        /// <param name="pnSaleOriginalAmount">Original sale amount</param>
        /// <returns>Conversion result</returns>
        private CurrencyConversionResult CalculateCurrencyConversion(decimal pnSaleOriginalAmount)
        {
            var loResult = new CurrencyConversionResult();

            try
            {
                // Handle same currency scenarios first for optimization
                if (Entity.CCURRENCY_CODE == CLOCAL_CURRENCY_CODE)
                {
                    // No conversion needed for local
                    loResult.LocalAmount = pnSaleOriginalAmount;
                }
                else
                {
                    // Convert to local currency using exact VB.NET formula
                    loResult.LocalAmount = Math.Round(
                        pnSaleOriginalAmount * Entity.NLCURRENCY_RATE_AMOUNT / Entity.NLBASE_RATE_AMOUNT, 2);
                }

                if (Entity.CCURRENCY_CODE == CBASE_CURRENCY_CODE)
                {
                    // No conversion needed for base
                    loResult.BaseAmount = pnSaleOriginalAmount;
                }
                else
                {
                    // Convert to base currency using exact VB.NET formula
                    loResult.BaseAmount = Math.Round(
                        pnSaleOriginalAmount * Entity.NBCURRENCY_RATE_AMOUNT / Entity.NBBASE_RATE_AMOUNT, 2);
                }

                // Handle local same as base currency
                if (CLOCAL_CURRENCY_CODE == CBASE_CURRENCY_CODE)
                {
                    loResult.BaseAmount = loResult.LocalAmount;
                }
            }
            catch (DivideByZeroException)
            {
                throw new Exception("Currency rates cannot be zero");
            }
            catch (OverflowException)
            {
                throw new Exception("Calculation result exceeds maximum value");
            }

            return loResult;
        }

        /// <summary>
        /// Phase 4: Calculate gain/loss amounts
        /// </summary>
        /// <param name="pnLocalSaleAmount">Local currency sale amount</param>
        /// <param name="pnBaseSaleAmount">Base currency sale amount</param>
        /// <returns>Gain/loss calculation result</returns>
        private GainLossResult CalculateGainLoss(decimal pnLocalSaleAmount, decimal pnBaseSaleAmount)
        {
            var loResult = new GainLossResult();

            try
            {
                // Use exact VB.NET formulas
                loResult.LocalGain = pnLocalSaleAmount - Entity.NLBOOKVAL;
                loResult.BaseGain = pnBaseSaleAmount - Entity.NBBOOKVAL;
            }
            catch (Exception)
            {
                // Preserve VB.NET behavior - no exception handling in calculation
                loResult.LocalGain = 0;
                loResult.BaseGain = 0;
            }

            return loResult;
        }

        /// <summary>
        /// Determine gain/loss status classification
        /// </summary>
        /// <param name="pnLocalGainLoss">Local currency gain/loss amount</param>
        /// <returns>Status string</returns>
        private string DetermineGainLossStatus(decimal pnLocalGainLoss)
        {
            if (pnLocalGainLoss > 0)
                return "GAIN";
            else if (pnLocalGainLoss < 0)
                return "LOSS";
            else
                return "BREAK EVEN";
        }

        /// <summary>
        /// Phase 5: Update entity with calculated amounts
        /// </summary>
        /// <param name="pnOriginalAmount">Original sale amount</param>
        /// <param name="poConversionResult">Currency conversion results</param>
        /// <param name="poGainLossResult">Gain/loss calculation results</param>
        private void UpdateEntityAmounts(decimal pnOriginalAmount, CurrencyConversionResult poConversionResult, GainLossResult poGainLossResult)
        {
            // Update transaction amounts
            Entity.NTRANSACTION_AMOUNT1 = pnOriginalAmount;
            Entity.NLTRANSACTION_AMOUNT1 = poConversionResult.LocalAmount;
            Entity.NBTRANSACTION_AMOUNT1 = poConversionResult.BaseAmount;

            // Synchronize main transaction amounts for save operation
            Entity.NTRANSACTION_AMOUNT = pnOriginalAmount;
            Entity.NLTRANSACTION_AMOUNT = poConversionResult.LocalAmount;
            Entity.NBTRANSACTION_AMOUNT = poConversionResult.BaseAmount;

            // Update gain/loss amounts (these would be calculated and stored separately in actual implementation)
            // Note: Gain/loss amounts are typically stored in asset transaction detail table
        }

        /// <summary>
        /// Phase 6: Validate calculated results
        /// </summary>
        /// <param name="poConversionResult">Currency conversion results</param>
        /// <param name="poGainLossResult">Gain/loss results</param>
        /// <returns>Validation result</returns>
        private ValidationResult ValidateCalculatedResults(CurrencyConversionResult poConversionResult, GainLossResult poGainLossResult)
        {
            var loResult = new ValidationResult { IsValid = true };

            // Validate calculated amounts are within acceptable ranges
            if (poConversionResult.LocalAmount > decimal.MaxValue || poConversionResult.LocalAmount < decimal.MinValue)
            {
                loResult.IsValid = false;
                loResult.ErrorMessage = "Calculated local amount exceeds system limits";
                loResult.ErrorCode = "CALC001";
                return loResult;
            }

            if (poConversionResult.BaseAmount > decimal.MaxValue || poConversionResult.BaseAmount < decimal.MinValue)
            {
                loResult.IsValid = false;
                loResult.ErrorMessage = "Calculated base amount exceeds system limits";
                loResult.ErrorCode = "CALC002";
                return loResult;
            }

            // Check for precision issues
            if (Math.Round(poConversionResult.LocalAmount, 2) != poConversionResult.LocalAmount)
            {
                loResult.WarningMessage = "Local amount precision adjusted to 2 decimal places";
                loResult.WarningCode = "CALC003";
            }

            return loResult;
        }

        /// <summary>
        /// Phase 7: Update validation flags based on new amounts
        /// </summary>
        /// <param name="pnSaleAmount">New sale amount</param>
        private void UpdateValidationFlags(decimal pnSaleAmount)
        {
            ValAmountPositive = (pnSaleAmount > 0);
            ValCurrencyRatesValid = (Entity.NLBASE_RATE_AMOUNT != 0 && Entity.NBBASE_RATE_AMOUNT != 0);
            ValCalculationComplete = true;
        }

        /// <summary>
        /// Phase 7: Trigger dependent calculations (tax, commission, etc.)
        /// </summary>
        /// <param name="pnSaleAmount">Sale amount for dependent calculations</param>
        /// <returns>Task</returns>
        private async Task TriggerDependentCalculations(decimal pnSaleAmount)
        {
            try
            {
                // Future implementation: Tax calculations
                if (IsTaxApplicable)
                {
                    await CalculateTaxAmounts(pnSaleAmount);
                }

                // Future implementation: Commission calculations
                if (IsCommissionApplicable)
                {
                    await CalculateCommissionAmounts(pnSaleAmount);
                }
            }
            catch (Exception)
            {
                // Log error but don't fail main calculation
                // These are optional calculations
            }
        }

        /// <summary>
        /// Future implementation: Calculate tax amounts
        /// </summary>
        /// <param name="pnSaleAmount">Sale amount for tax calculation</param>
        /// <returns>Task</returns>
        private async Task CalculateTaxAmounts(decimal pnSaleAmount)
        {
            // Placeholder for future tax calculation implementation
            await Task.CompletedTask;
        }

        /// <summary>
        /// Future implementation: Calculate commission amounts
        /// </summary>
        /// <param name="pnSaleAmount">Sale amount for commission calculation</param>
        /// <returns>Task</returns>
        private async Task CalculateCommissionAmounts(decimal pnSaleAmount)
        {
            // Placeholder for future commission calculation implementation
            await Task.CompletedTask;
        }

        /// <summary>
        /// Reset calculated fields on error
        /// </summary>
        public void ResetCalculatedFields()
        {
            // Reset both Entity and Data to ensure consistency
            Entity.NTRANSACTION_AMOUNT = 0;
            Entity.NTRANSACTION_AMOUNT1 = 0;
            Entity.NLTRANSACTION_AMOUNT = 0;
            Entity.NLTRANSACTION_AMOUNT1 = 0;
            Entity.NBTRANSACTION_AMOUNT = 0;
            Entity.NBTRANSACTION_AMOUNT1 = 0;
            Entity.NLGAIN_LOSS = 0;
            Entity.NBGAIN_LOSS = 0;
            
            Data.NTRANSACTION_AMOUNT = 0;
            Data.NTRANSACTION_AMOUNT1 = 0;
            Data.NLTRANSACTION_AMOUNT = 0;
            Data.NLTRANSACTION_AMOUNT1 = 0;
            Data.NBTRANSACTION_AMOUNT = 0;
            Data.NBTRANSACTION_AMOUNT1 = 0;
            Data.NLGAIN_LOSS = 0;
            Data.NBGAIN_LOSS = 0;
            
            ValAmountPositive = false;
            ValCurrencyRatesValid = false;
            ValCalculationComplete = false;
        }

        /// <summary>
        /// Get formatted calculation summary for display
        /// </summary>
        /// <returns>Formatted summary string</returns>
        public string GetCalculationSummary()
        {
            if (!ValCalculationComplete)
                return "No calculations performed";

            var loGainLoss = Entity.NLTRANSACTION_AMOUNT1 - Entity.NLBOOKVAL;
            var loStatus = DetermineGainLossStatus(loGainLoss);

            return $@"Sale Amount: {Entity.NTRANSACTION_AMOUNT1:N2} {Entity.CCURRENCY_CODE}
Local Amount: {Entity.NLTRANSACTION_AMOUNT1:N2} {CLOCAL_CURRENCY_CODE}
Base Amount: {Entity.NBTRANSACTION_AMOUNT1:N2} {CBASE_CURRENCY_CODE}
Gain/Loss: {loGainLoss:N2} {CLOCAL_CURRENCY_CODE} ({loStatus})";
        }

        /// <summary>
        /// Get exchange rate information for display
        /// </summary>
        /// <returns>Formatted exchange rate string</returns>
        public string GetExchangeRateInfo()
        {
            return $@"Local Rate: {Entity.NLCURRENCY_RATE_AMOUNT:N4} / {Entity.NLBASE_RATE_AMOUNT:N4}
Base Rate: {Entity.NBCURRENCY_RATE_AMOUNT:N4} / {Entity.NBBASE_RATE_AMOUNT:N4}
Rate Date: {Entity.CTRANSACTION_DATE}";
        }

        #endregion

        /// <summary>
        /// Check if editing is enabled based on status code
        /// </summary>
        /// <param name="statusCode">Transaction status code</param>
        /// <returns>True if editing is allowed, false otherwise</returns>
        public bool CheckEnableEdit(string statusCode)
        {
            switch (statusCode)
            {
                case "00": // Draft (VB.NET compatible)
                    return true;  // Allow editing
                case "01": // Submitted
                case "02": // Pending approval
                case "03": // Approved
                case "08": // Closed
                case "09": // Other final state
                case "98": // Other final state
                case "99": // Other final state
                    return false; // No editing
                default:
                    return false; // Default to no editing for unknown statuses
            }
        }

    }

    /// <summary>
    /// DTO for Period Month ComboBox
    /// </summary>
    public class PeriodMonthDTO
    {
        public string CPERIOD_NO { get; set; } = string.Empty;
    }

    /// <summary>
    /// Result class for OnChange Sale Amount business process
    /// </summary>
    public class OnChangeSaleAmountResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public string ErrorCode { get; set; } = string.Empty;
        public string WarningMessage { get; set; } = string.Empty;
        public string WarningCode { get; set; } = string.Empty;
        
        public decimal OriginalSaleAmount { get; set; }
        public decimal LocalSaleAmount { get; set; }
        public decimal BaseSaleAmount { get; set; }
        public decimal LocalGainLoss { get; set; }
        public decimal BaseGainLoss { get; set; }
        public string GainLossStatus { get; set; } = string.Empty;
    }

    /// <summary>
    /// Validation result class
    /// </summary>
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public string ErrorCode { get; set; } = string.Empty;
        public string WarningMessage { get; set; } = string.Empty;
        public string WarningCode { get; set; } = string.Empty;
    }

    /// <summary>
    /// Currency conversion result class
    /// </summary>
    public class CurrencyConversionResult
    {
        public decimal LocalAmount { get; set; }
        public decimal BaseAmount { get; set; }
    }

    /// <summary>
    /// Gain/Loss calculation result class
    /// </summary>
    public class GainLossResult
    {
        public decimal LocalGain { get; set; }
        public decimal BaseGain { get; set; }
    }

}

