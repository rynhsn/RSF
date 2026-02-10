using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_ContextFrontEnd;
using R_CommonFrontBackAPI;
using FAT00300Common;
using FAT00300Common.DTOs;
using FAT00300Common.Requests;
using FAT00300FrontResources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FAT00300Model.VMs
{
    public class FAT00300ViewModel : R_ViewModel<FAT00300DTO>
    {
        private readonly FAT00300Model _model = new FAT00300Model();

        #region Properties - CRUD Operations

        /// <summary>
        /// Current record for CRUD operations
        /// </summary>
        public FAT00300DTO CurrentRecord { get; set; } = new FAT00300DTO();

        #endregion

        #region Properties - Initialization

        /// <summary>
        /// Initial process data
        /// </summary>
        public FAT00300GetInitialProcessResultDTO InitialProcess { get; set; } = new FAT00300GetInitialProcessResultDTO();
        public FAT00300GetCompanyInfoResultDTO CompanyInfo { get; set; } = new FAT00300GetCompanyInfoResultDTO();
        public FAT00300GetSystemParamResultDTO SystemParam { get; set; } = new FAT00300GetSystemParamResultDTO();
        public FAT00300GetPeriodInfoResultDTO PeriodInfo { get; set; } = new FAT00300GetPeriodInfoResultDTO();
        public FAT00300GetTransCodeInfoResultDTO TransCodeInfo { get; set; } = new FAT00300GetTransCodeInfoResultDTO();
        public FAT00300GetPeriodRangeResultDTO PeriodRange { get; set; } = new FAT00300GetPeriodRangeResultDTO();   

        /// <summary>
        /// Transaction code constant for Manual Depreciation
        /// </summary>
        public string TransactionCode { get; set; } = "210020";

        #endregion

        #region Properties - Validation Results

        /// <summary>
        /// Validation data result
        /// </summary>
        public FAT00300GetValidationDataResultDTO ValidationData { get; set; } = new FAT00300GetValidationDataResultDTO();

        /// <summary>
        /// Validate outstanding transaction result
        /// </summary>
        public FAT00300GetValidateOutstandTransResultDTO ValidateOutstandTrans { get; set; } = new FAT00300GetValidateOutstandTransResultDTO();

        /// <summary>
        /// Validate transaction date result
        /// </summary>
        public FAT00300GetValidateTransDateResultDTO ValidateTransDate { get; set; } = new FAT00300GetValidateTransDateResultDTO();

        /// <summary>
        /// Validate void result
        /// </summary>
        public FAT00300GetValidateVoidResultDTO ValidateVoid { get; set; } = new FAT00300GetValidateVoidResultDTO();

        /// <summary>
        /// Validate department code result
        /// </summary>
        public FAT00300ValidateDeptCodeResultDTO ValidateDeptCode { get; set; } = new FAT00300ValidateDeptCodeResultDTO();

        /// <summary>
        /// Validate GL journal account result
        /// </summary>
        public FAT00300ValidateGLJournalAccountResultDTO ValidateGLJournalAccount { get; set; } = new FAT00300ValidateGLJournalAccountResultDTO();

        #endregion

        #region Properties - Permission Results

        /// <summary>
        /// User can close result
        /// </summary>
        public FAT00300GetUserCanCloseResultDTO UserCanClose { get; set; } = new FAT00300GetUserCanCloseResultDTO();

        /// <summary>
        /// User can approve result
        /// </summary>
        public FAT00300GetUserCanApproveResultDTO UserCanApprove { get; set; } = new FAT00300GetUserCanApproveResultDTO();

        /// <summary>
        /// Approval precheck result
        /// </summary>
        public FAT00300GetApprovalPrecheckResultDTO ApprovalPrecheck { get; set; } = new FAT00300GetApprovalPrecheckResultDTO();

        #endregion

        #region Properties - Process Results

        /// <summary>
        /// Submit process result
        /// </summary>
        public FAT00300SubmitProcessResultDTO SubmitResult { get; set; } = new FAT00300SubmitProcessResultDTO();

        /// <summary>
        /// Approve process result
        /// </summary>
        public FAT00300ApproveProcessResultDTO ApproveResult { get; set; } = new FAT00300ApproveProcessResultDTO();

        /// <summary>
        /// Close process result
        /// </summary>
        public FAT00300CloseProcessResultDTO CloseResult { get; set; } = new FAT00300CloseProcessResultDTO();

        /// <summary>
        /// Void process result
        /// </summary>
        public FAT00300VoidProcessResultDTO VoidResult { get; set; } = new FAT00300VoidProcessResultDTO();

        #endregion

        #region Properties - Asset Information

        /// <summary>
        /// Asset information TAB result
        /// </summary>
        public FAT00300GetAssetInformationTABResultDTO AssetInformation { get; set; } = new FAT00300GetAssetInformationTABResultDTO();

        #endregion

        #region Properties - Streaming Collections

        /// <summary>
        /// Allocation expense list
        /// </summary>
        public ObservableCollection<FAT00300GetAllocationExpenseListResultDTO> AllocationExpenseList { get; set; } = new ObservableCollection<FAT00300GetAllocationExpenseListResultDTO>();
        public ObservableCollection<FAT00300GetTransListResultDTO> AllTransList { get; set; } = new ObservableCollection<FAT00300GetTransListResultDTO>();
        public FAT00300GetTransListResultDTO TranslistRecord { get; set; } = new FAT00300GetTransListResultDTO();
        public List<FAT00300GetDeptListResultDTO> loListDept = new List<FAT00300GetDeptListResultDTO>();
        public string CTRANS_CODE = "";
        public string CDEPT_CODE = "";
        public string CDEPT_NAME = "";
        public string CFROM_PERIOD = "";
        public string CTO_PERIOD = "";
        public string CASSET_CODE = "";
        public string CASSET_NAME = "";
        public int IPERIOD_FROM = DateTime.Now.Year;
        public int IPERIOD_TO = DateTime.Now.Year;
        public FAT00300FrontResources.Resources_Dummy_Class a = new Resources_Dummy_Class();

        public List<FAT00300PeriodDTO> ListMonth = new List<FAT00300PeriodDTO> {
          new FAT00300PeriodDTO { CCODE = "01", CDESC = "January"},
          new FAT00300PeriodDTO { CCODE = "02", CDESC = "February"},
          new FAT00300PeriodDTO { CCODE = "03", CDESC = "March"},
          new FAT00300PeriodDTO { CCODE = "04", CDESC = "April"},
          new FAT00300PeriodDTO { CCODE = "05", CDESC = "Mei"},
          new FAT00300PeriodDTO { CCODE = "06", CDESC = "June"},
          new FAT00300PeriodDTO { CCODE = "07", CDESC = "July"},
          new FAT00300PeriodDTO { CCODE = "08", CDESC = "August"},
          new FAT00300PeriodDTO { CCODE = "09", CDESC = "September"},
          new FAT00300PeriodDTO { CCODE = "10", CDESC = "October"},
          new FAT00300PeriodDTO { CCODE = "11", CDESC = "November"},
          new FAT00300PeriodDTO { CCODE = "12", CDESC = "December"},
        };
        #endregion

        #region Methods - Initialization

        /// <summary>
        /// Get initial process data
        /// </summary>
        public async Task GetInitialProcessAsync(FAT00300GetInitialProcessParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetInitialProcess(poParameter);
                InitialProcess = loResult.Data ?? new FAT00300GetInitialProcessResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetCompanyInfoAsync(FAT00300GetCompanyInfoParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetCompanyInfo(poParameter);
                CompanyInfo = loResult.Data ?? new FAT00300GetCompanyInfoResultDTO();
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetSystemParamAsync(FAT00300GetSystemParamParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetSystemParam(poParameter);
                SystemParam = loResult.Data ?? new FAT00300GetSystemParamResultDTO();

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPeriodInfoAsync(FAT00300GetPeriodInfoParamDTO poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                poParameter.CYEAR = SystemParam.CSOFT_PERIOD_YY;
                poParameter.CPERIOD_NO = SystemParam.CSOFT_PERIOD_MM;
                var loResult = await _model.GetPeriodInfo(poParameter);
                PeriodInfo = loResult.Data ?? new FAT00300GetPeriodInfoResultDTO();
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTransCodeInfoAsync(FAT00300GetTransCodeInfoParamDTO poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetTransCodeInfo(poParameter);
                TransCodeInfo = loResult.Data ?? new FAT00300GetTransCodeInfoResultDTO();
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPeriodRangeAsync(FAT00300GetPeriodRangeParamDTO poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetPeriodRange(poParameter);
                poParameter.CCYEAR = "";
                poParameter.CMODE = "";
                PeriodRange = loResult.Data ?? new FAT00300GetPeriodRangeResultDTO();
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Methods - CRUD Operations

        /// <summary>
        /// Get record by entity
        /// </summary>
        public async Task GetRecordAsync(FAT00300DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.R_ServiceGetRecordAsync(poEntity);
                CurrentRecord = loResult ?? new FAT00300DTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Save record
        /// </summary>
        public async Task SaveRecordAsync(FAT00300DTO poEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.R_ServiceSaveAsync(poEntity, peCRUDMode);
                CurrentRecord = loResult ?? new FAT00300DTO();
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
        public async Task DeleteRecordAsync(FAT00300DTO poEntity)
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

        #region Methods - Validation

        /// <summary>
        /// Get validation data
        /// </summary>
        public async Task GetValidationDataAsync(FAT00300GetValidationDataParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetValidationData(poParameter);
                ValidationData = loResult.Data ?? new FAT00300GetValidationDataResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get validate outstanding transaction
        /// </summary>
        public async Task GetValidateOutstandTransAsync(FAT00300GetValidateOutstandTransParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetValidateOutstandTrans(poParameter);
                ValidateOutstandTrans = loResult.Data ?? new FAT00300GetValidateOutstandTransResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get validate transaction date
        /// </summary>
        public async Task GetValidateTransDateAsync(FAT00300GetValidateTransDateParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetValidateTransDate(poParameter);
                ValidateTransDate = loResult.Data ?? new FAT00300GetValidateTransDateResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get validate void
        /// </summary>
        public async Task GetValidateVoidAsync(FAT00300GetValidateVoidParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetValidateVoid(poParameter);
                ValidateVoid = loResult.Data ?? new FAT00300GetValidateVoidResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Validate department code
        /// </summary>
        public async Task ValidateDeptCodeAsync(FAT00300ValidateDeptCodeParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.ValidateDeptCode(poParameter);
                ValidateDeptCode = loResult.Data ?? new FAT00300ValidateDeptCodeResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Validate GL journal account
        /// </summary>
        public async Task ValidateGLJournalAccountAsync(FAT00300ValidateGLJournalAccountParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.ValidateGLJournalAccount(poParameter);
                ValidateGLJournalAccount = loResult.Data ?? new FAT00300ValidateGLJournalAccountResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Methods - Permission

        /// <summary>
        /// Get user can close permission
        /// </summary>
        public async Task GetUserCanCloseAsync(FAT00300GetUserCanCloseParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetUserCanClose(poParameter);
                UserCanClose = loResult.Data ?? new FAT00300GetUserCanCloseResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get user can approve permission
        /// </summary>
        public async Task GetUserCanApproveAsync(FAT00300GetUserCanApproveParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetUserCanApprove(poParameter);
                UserCanApprove = loResult.Data ?? new FAT00300GetUserCanApproveResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get approval precheck
        /// </summary>
        public async Task GetApprovalPrecheckAsync(FAT00300GetApprovalPrecheckParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetApprovalPrecheck(poParameter);
                ApprovalPrecheck = loResult.Data ?? new FAT00300GetApprovalPrecheckResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Methods - Process Operations

        /// <summary>
        /// Submit process
        /// </summary>
        public async Task SubmitProcessAsync(FAT00300SubmitProcessParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.SubmitProcess(poParameter);
                SubmitResult = loResult.Data ?? new FAT00300SubmitProcessResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Approve process
        /// </summary>
        public async Task ApproveProcessAsync(FAT00300ApproveProcessParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.ApproveProcess(poParameter);
                ApproveResult = loResult.Data ?? new FAT00300ApproveProcessResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Close process
        /// </summary>
        public async Task CloseProcessAsync(FAT00300CloseProcessParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.CloseProcess(poParameter);
                CloseResult = loResult.Data ?? new FAT00300CloseProcessResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Void process
        /// </summary>
        public async Task VoidProcessAsync(FAT00300VoidProcessParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.VoidProcess(poParameter);
                VoidResult = loResult.Data ?? new FAT00300VoidProcessResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Methods - Asset Information

        /// <summary>
        /// Get asset information TAB
        /// </summary>
        public async Task GetAssetInformationTABAsync(FAT00300GetAssetInformationTABParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetAssetInformationTAB(poParameter);
                AssetInformation = loResult.Data ?? new FAT00300GetAssetInformationTABResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Methods - Streaming

        /// <summary>
        /// Get allocation expense list (streaming)
        /// </summary>
        public async Task GetAllocationExpenseListAsync(string pcAssetCode)
        {
            var loEx = new R_Exception();

            try
            {
                // Set streaming context for custom parameters
                R_FrontContext.R_SetStreamingContext(ContextConstants.ASSET_CODE, pcAssetCode);

                var loResult = await _model.GetAllocationExpenseListAsync();
                AllocationExpenseList = new ObservableCollection<FAT00300GetAllocationExpenseListResultDTO>(loResult.Data ?? new List<FAT00300GetAllocationExpenseListResultDTO>());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTransListAsync()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var FROM_PERIOD = IPERIOD_FROM.ToString() + CFROM_PERIOD;
                var TO_PERIOD = IPERIOD_TO.ToString() + CTO_PERIOD;

                R_FrontContext.R_SetStreamingContext(ContextConstants.TRANS_CODE, TransactionCode);
                R_FrontContext.R_SetStreamingContext(ContextConstants.DEPT_CODE, Data.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstants.FROM_PERIOD, FROM_PERIOD);
                R_FrontContext.R_SetStreamingContext(ContextConstants.TO_PERIOD, TO_PERIOD);
                R_FrontContext.R_SetStreamingContext(ContextConstants.ASSET_CODE, Data.CASSET_CODE);

                var loResult = await _model.GetAllTransListAsync();
                AllTransList = new ObservableCollection<FAT00300GetTransListResultDTO>(loResult.Data ?? new List<FAT00300GetTransListResultDTO>());

                foreach (var transList in AllTransList)
                {
                    transList.DREF_DATE = R_FrontUtility.R_ConvertToDateTime(transList.CREF_DATE, "yyyyMMdd");
                }

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Methods - ViewModel Validation

        /// <summary>
        /// Validation for save operation
        /// Returns R_Exception with validation errors
        /// </summary>
        /// <param name="poEntity">Entity to validate</param>
        /// <param name="plIsAddMode">True if in Add mode</param>
        /// <param name="plIncrementFlag">Increment flag from initial process</param>
        /// <param name="plChangeDesc">Change description flag</param>
        /// <param name="pcDepartmentCode">Department code</param>
        /// <param name="pdTransactionDate">Transaction date</param>
        /// <param name="pcAssetCode">Asset code</param>
        /// <param name="pnDepreciationAmount">Depreciation amount</param>
        /// <param name="piDepreciationQty">Depreciation quantity</param>
        /// <param name="pcTransactionNumber">Transaction number</param>
        /// <param name="poValidationData">Validation data result</param>
        /// <param name="plHasOutstandingTrans">Has outstanding transaction</param>
        /// <param name="pcSoftPeriod">Soft period</param>
        /// <param name="pcCurrentPeriod">Current period</param>
        /// <param name="plCustPeriodFlag">Custom period flag</param>
        /// <param name="plErrorTransDateSoftPeriod">Error flag for transaction date vs soft period</param>
        /// <param name="plErrorTransDateFuture">Error flag for transaction date in future</param>
        /// <param name="plErrorTransDateYear">Error flag for transaction date year</param>
        /// <returns>R_Exception containing validation errors</returns>
        public R_Exception ValidationSave(
            FAT00300DTO poEntity,
            bool plIsAddMode,
            bool plIncrementFlag,
            bool plChangeDesc,
            string pcDepartmentCode,
            DateTime? pdTransactionDate,
            string pcAssetCode,
            decimal pnDepreciationAmount,
            int piDepreciationQty,
            string pcTransactionNumber,
            FAT00300GetValidationDataResultDTO poValidationData,
            bool plHasOutstandingTrans,
            string pcSoftPeriod,
            string pcCurrentPeriod,
            bool plCustPeriodFlag,
            bool plErrorTransDateSoftPeriod,
            bool plErrorTransDateFuture,
            bool plErrorTransDateYear)
        {
            var loEx = new R_Exception();

            try
            {
                // PS013: Department code should not be empty
                if (string.IsNullOrWhiteSpace(pcDepartmentCode))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS013"));
                }

                // PS014: Transaction Date should not be empty
                if (pdTransactionDate == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS014"));
                }

                // PS015: Asset code should not be empty
                if (string.IsNullOrWhiteSpace(pcAssetCode))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS015"));
                }

                // PS016: Please enter a valid Depreciation Amount (must be > 0)
                if (pnDepreciationAmount <= 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS016"));
                }

                // PS017: Please enter a valid Depreciation Quantity (must be >= 0)
                if (piDepreciationQty < 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS017"));
                }

                // PS009: Transaction date should be not less than the soft period
                if (plErrorTransDateSoftPeriod)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS009"));
                }

                // PS010: Transaction date should be not greater than today
                if (plErrorTransDateFuture)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS010"));
                }

                // PS011: Transaction date should be in the year of current period
                if (plErrorTransDateYear)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS011"));
                }

                // Skip remaining validations if basic validations fail
                if (loEx.HasError)
                {
                    return loEx;
                }

                // Add mode specific validations
                if (plIsAddMode)
                {
                    // PS018: This asset has outstanding transaction
                    if (plHasOutstandingTrans)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS018"));
                    }

                    // PS023: Transaction Number should not be empty (when increment flag is false)
                    if (plIncrementFlag == false)
                    {
                        if (string.IsNullOrWhiteSpace(pcTransactionNumber))
                        {
                            loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS023"));
                        }
                    }
                }

                // Validation data checks
                if (poValidationData != null)
                {
                    // PS019: Status of the asset is not allowed for adding this transaction
                    if (!(plChangeDesc == true || poValidationData.CASSET_STATUS == "1"))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS019"));
                    }

                    // PS020: Transaction Date should not be before the last one of this asset
                    if (pdTransactionDate.HasValue && !string.IsNullOrWhiteSpace(poValidationData.CLAST_TRANS_DATE))
                    {
                        string lcTransDate = pdTransactionDate.Value.ToString("yyyyMMdd");
                        if (string.Compare(lcTransDate, poValidationData.CLAST_TRANS_DATE) < 0)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS020"));
                        }
                    }

                    // PS021: Depreciation Amount should not exceed the asset remaining (Book Value - Residual Value)
                    decimal lnMaxDepreciation = poValidationData.NLBOOKVAL - poValidationData.NLRESIDUAL;
                    if (pnDepreciationAmount > lnMaxDepreciation)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS021"));
                    }

                    // PS022: Depreciation quantity should not exceed the asset remaining
                    if (piDepreciationQty >= poValidationData.IQTY)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS022"));
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            return loEx;
        }

        /// <summary>
        /// Validate transaction date against soft period and current period
        /// </summary>
        /// <param name="pdTransactionDate">Transaction date</param>
        /// <param name="pcSoftPeriod">Soft period (yyyyMM format)</param>
        /// <param name="pcCurrentPeriod">Current period (yyyyMM format)</param>
        /// <param name="plCustPeriodFlag">Custom period flag</param>
        /// <param name="pcTransactionPeriod">Transaction period from validation (for custom period)</param>
        /// <param name="plErrorSoftPeriod">Output: Error if transaction date is before soft period</param>
        /// <param name="plErrorFuture">Output: Error if transaction date is in the future</param>
        /// <param name="plErrorYear">Output: Error if transaction date year differs from current period year</param>
        public void ValidateTransactionDate(
            DateTime? pdTransactionDate,
            string pcSoftPeriod,
            string pcCurrentPeriod,
            bool plCustPeriodFlag,
            string pcTransactionPeriod,
            out bool plErrorSoftPeriod,
            out bool plErrorFuture,
            out bool plErrorYear)
        {
            plErrorSoftPeriod = false;
            plErrorFuture = false;
            plErrorYear = false;

            if (pdTransactionDate == null)
            {
                return;
            }

            string lcPeriod;
            if (plCustPeriodFlag == false)
            {
                // Use transaction date's year and month
                lcPeriod = pdTransactionDate.Value.ToString("yyyyMM");
            }
            else
            {
                // Use period from validation result
                lcPeriod = pcTransactionPeriod;
            }

            // Check against soft period
            if (!string.IsNullOrWhiteSpace(pcSoftPeriod))
            {
                if (string.Compare(lcPeriod, pcSoftPeriod) < 0)
                {
                    plErrorSoftPeriod = true;
                }
            }

            // Check if future date
            if (pdTransactionDate.Value > DateTime.Now)
            {
                plErrorFuture = true;
            }

            // Check if same year as current period
            if (!string.IsNullOrWhiteSpace(pcCurrentPeriod) && !string.IsNullOrWhiteSpace(lcPeriod))
            {
                string lcTransYear = lcPeriod.Length >= 4 ? lcPeriod.Substring(0, 4) : string.Empty;
                string lcCurrentYear = pcCurrentPeriod.Length >= 4 ? pcCurrentPeriod.Substring(0, 4) : string.Empty;
                
                if (lcTransYear != lcCurrentYear)
                {
                    plErrorYear = true;
                }
            }
        }

        #endregion

        #region Helper
        public void SetDefaultValue()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                int todayMonth = DateTime.Now.Month;
                string todayMonthString = "";

                if (todayMonth < 10)
                {
                    todayMonthString = "0" + todayMonth.ToString();
                    CFROM_PERIOD = todayMonthString;
                    CTO_PERIOD = todayMonthString;
                }
                else 
                {
                    todayMonthString = todayMonth.ToString();
                    CFROM_PERIOD = todayMonthString;
                    CTO_PERIOD = todayMonthString;
                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

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

        #region Validation
        public void ValidationTransactionList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "NoData"));
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void ValidationGetTransList()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (string.IsNullOrEmpty(Data.CDEPT_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS034"));
                }

                //if (string.IsNullOrEmpty(Data.CASSET_CODE))
                //{
                //    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS035"));
                //}
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Get Dept List
        public async Task GetDeptListAsync()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetAllDeptListAsync();
                if (loResult != null)
                {
                    loListDept = loResult.Data ?? new List<FAT00300GetDeptListResultDTO>();
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






