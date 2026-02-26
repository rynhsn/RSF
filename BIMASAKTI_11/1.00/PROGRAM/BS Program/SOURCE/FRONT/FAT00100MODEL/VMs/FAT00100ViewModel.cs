using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using FAT00100Common;
using FAT00100Common.DTOs;
using FAT00100FrontResources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FAT00100Model.VMs
{
    /// <summary>
    /// ViewModel for FAT00100 - Fixed Asset Transaction
    /// Handles form operations, validation, and data retrieval
    /// </summary>
    public class FAT00100ViewModel : R_ViewModel<FAT00100DTO>
    {
        private readonly FAT00100Model _model = new FAT00100Model();

        // Hardcoded constants
        public const string DEFAULT_TRANSACTION_CODE = "200010";
        public const string DEFAULT_PJ_TRANSACTION_CODE = "420010";
        public const string DEFAULT_SOURCE_MODULE_FA = "FA";
        public const string DEFAULT_SOURCE_MODULE_PJ = "PJ";
        public const string DEFAULT_STATUS_DRAFT = "00";
        public const string DEFAULT_GL_TRF_STATUS = "0";
        public const string STATUS_FLAG_DISABLED = "0";
        public const string STATUS_FLAG_ENABLED = "1";

        // Current form data
        public FAT00100DTO CurrentRecord { get; set; } = new FAT00100DTO();

        // Initialization data
        public FAT00100GetPeriodYearResultDTO PeriodYearData { get; set; } = new FAT00100GetPeriodYearResultDTO();

        // Additional data properties
        public FAT00100GetCompanyInfoResultDTO CompanyInfoData { get; set; } = new FAT00100GetCompanyInfoResultDTO();
        public FAT00100GetGetSystemParamResultDTO SystemParamData { get; set; } = new FAT00100GetGetSystemParamResultDTO();
        public FAT00100GetPeriodeDtInfoResultDTO PeriodeDtInfoData { get; set; } = new FAT00100GetPeriodeDtInfoResultDTO();
        public FAT00100GetTransCodeInfoResultDTO TransCodeInfoData { get; set; } = new FAT00100GetTransCodeInfoResultDTO();
        public FAT00100GetYearRangeResultDTO YearRangeData { get; set; } = new FAT00100GetYearRangeResultDTO();

        // Lists
        public ObservableCollection<FAT00100GetComboPeriodMonthResultDTO> ComboPeriodMonthList { get; set; } = new ObservableCollection<FAT00100GetComboPeriodMonthResultDTO>();
        public ObservableCollection<FAT00100GetDataGridResultDTO> DataGridList { get; set; } = new ObservableCollection<FAT00100GetDataGridResultDTO>();
        public ObservableCollection<FAT00100GetGSM_SUPPLIER_CONTACTResultDTO> SupplierContactList { get; set; } = new ObservableCollection<FAT00100GetGSM_SUPPLIER_CONTACTResultDTO>();
        public ObservableCollection<FAT00100CPDTO> ContactPersonList { get; set; } = new ObservableCollection<FAT00100CPDTO>();
        public ObservableCollection<FAT00100GetDeptLookupListResultDTO> DeptLookupList { get; set; } = new ObservableCollection<FAT00100GetDeptLookupListResultDTO>();
        public ObservableCollection<FAT00100GetStatusListResultDTO> StatusList { get; set; } = new ObservableCollection<FAT00100GetStatusListResultDTO>();
        public ObservableCollection<FAT00100GetCurrencyListResultDTO> CurrencyList { get; set; } = new ObservableCollection<FAT00100GetCurrencyListResultDTO>();
        public List<FAT00100GetCurrencyListResultDTO> CurrencyListtemp { get; set; } = new List<FAT00100GetCurrencyListResultDTO>();
        public FAT00100GetLastCurrencyRateResultDTO LastCurrencyRateData { get; set; } = new FAT00100GetLastCurrencyRateResultDTO();

        // Supplier info
        public FAT00100GetGSM_SUPPLIER_INFOResultDTO SupplierInfo { get; set; } = new FAT00100GetGSM_SUPPLIER_INFOResultDTO();

        // Form state properties (from GetInitialProcess)
        
        public string DefaultTrxDeptCode { get; set; } = string.Empty;
        public string DefaultAssetDeptCode { get; set; } = string.Empty;
        public bool AssetIncrementFlag { get; set; }
        public bool JrngrpMode { get; set; }
        public bool DeptMode { get; set; }
        public string PeriodMode { get; set; } = string.Empty;
        public string CurrentPeriod { get; set; } = string.Empty;
        public string SoftPeriod { get; set; } = string.Empty;
        public string RateTypeCode { get; set; } = string.Empty;
        public string GlinkDate { get; set; } = string.Empty;
        public string PJlinkDate { get; set; } = string.Empty;
        public string FilterSupplierId { get; set; } = string.Empty;
        public string TransactionPrd { get; set; } = string.Empty;
        public string LocalCurrencyCode { get; set; } = string.Empty;
        public string BaseCurrencyCode { get; set; } = string.Empty;
        public bool CustPeriodFlag { get; set; }
        public string FilterTransDesc { get; set; } = string.Empty;
        public bool ApprovalFlag { get; set; }
        public bool IncrementFlag { get; set; }
        public string PJTransDesc { get; set; } = string.Empty;
        public bool CanApprove { get; set; }
        public bool CanClose { get; set; }
        public string PoDeptCode { get; set; } = string.Empty;
        public string PoDeptName { get; set; } = string.Empty;
        public string PoSupplierId { get; set; } = string.Empty;
        public string PoSupplierName { get; set; } = string.Empty;
        
        // Period filter properties
        public int PeriodFromYear { get; set; } = DateTime.Now.Year;
        public string PeriodFromMonth { get; set; } = DateTime.Now.Month.ToString("00");
        public int PeriodToYear { get; set; } = DateTime.Now.Year;
        public string PeriodToMonth { get; set; } = DateTime.Now.Month.ToString("00");

        // Status filter property
        public string SelectedStatus { get; set; } = "";

        // Filter properties for GetDataGrid
        public string FilterPeriodFrom { get; set; } = string.Empty;
        public string FilterPeriodTo { get; set; } = string.Empty;
        public string FilterStatusDraft { get; set; } = string.Empty;
        public string FilterStatusOpen { get; set; } = string.Empty;
        public string FilterStatusApproved { get; set; } = string.Empty;
        public string FilterStatusClosed { get; set; } = string.Empty;
        public string FilterReferenceNo { get; set; } = string.Empty;
        
        // Additional state
        public string GLTransferStatus { get; set; } = string.Empty;
        public bool GLLink { get; set; }

        #region Initialization Methods

        /// <summary>
        /// Get period year data
        /// </summary>
        public async Task GetPeriodYearAsync(string pcCompanyId, string pcSoftPeriod, string pcTransactionPrd)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new FAT00100GetPeriodYearParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CSOFT_PERIOD = pcSoftPeriod,
                    CTRANSACTION_PRD = pcTransactionPrd
                };

                var loResult = await _model.GetPeriodYear(loParam);
                PeriodYearData = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get company info
        /// </summary>
        public async Task GetCompanyInfoAsync(string pcCompanyId, string userId, string clang)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new FAT00100GetCompanyInfoParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CUSER_ID = userId,
                    CLANG_ID = clang
                };

                var loResult = await _model.FAT00100GetCompanyInfo(loParam);
                CompanyInfoData = loResult.Data ?? new FAT00100GetCompanyInfoResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get system parameter
        /// </summary>
        public async Task GetGetSystemParamAsync(string pcCompanyId, string pcLanguageId)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new FAT00100GetGetSystemParamParameterDTO
                {
                    CCOMPANY_ID =pcCompanyId,
                    CLANGUAGE_ID = pcLanguageId
                };

                var loResult = await _model.FAT00100GetGetSystemParam(loParam);
                SystemParamData = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndTry:
            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get periode date info
        /// </summary>
        public async Task GetPeriodeDtInfoAsync(string pcCompanyId, string pcYear, string pcPeriodNo)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new FAT00100GetPeriodeDtInfoParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CYEAR = pcYear,
                    CPERIOD_NO = pcPeriodNo
                };

                var loResult = await _model.FAT00100GetPeriodeDtInfo(loParam);
                PeriodeDtInfoData = loResult.Data ?? new FAT00100GetPeriodeDtInfoResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get transaction code info
        /// </summary>
        public async Task GetTransCodeInfoAsync(string pcCompanyId, string pcTransCode)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new FAT00100GetTransCodeInfoParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CTRANS_CODE = pcTransCode
                };

                var loResult = await _model.FAT00100GetTransCodeInfo(loParam);
                TransCodeInfoData = loResult.Data ?? new FAT00100GetTransCodeInfoResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get year range
        /// </summary>
        public async Task GetYearRangeAsync(string pcCompanyId, string pcYear, string pcMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new FAT00100GetYearRangeParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CCYEAR = pcYear,
                    CMODE = pcMode
                };

                var loResult = await _model.FAT00100GetYearRange(loParam);
                YearRangeData = loResult.Data ?? new FAT00100GetYearRangeResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Streaming Methods

        /// <summary>
        /// Initialize combo period month list - hardcoded months from 01 to 12
        /// </summary>
        public void SetComboPeriodMonthList()
        {
            var loEx = new R_Exception();

            try
            {
                // Clear existing list to avoid duplicates
                ComboPeriodMonthList.Clear();
                
                // Add months from 01 to 12
                for (int i = 1; i <= 12; i++)
                {
                    ComboPeriodMonthList.Add(new FAT00100GetComboPeriodMonthResultDTO
                    {
                        CPERIOD_NO = i.ToString("00")
                    });
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get data grid - streaming method for main grid
        /// Reads filter parameters from ViewModel properties
        /// </summary>
        public async Task GetDataGridAsync()
        {
            var loEx = new R_Exception();

            try
            {
                // Validate PoDeptCode is not empty
                if (string.IsNullOrWhiteSpace(PoDeptCode))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS001"));
                    goto EndTry;
                }

                // Set streaming context for all custom parameters from ViewModel properties
                // (NOT CCOMPANY_ID, CFOREIGN_LANGUAGE - handled automatically)
                R_FrontContext.R_SetStreamingContext(ContextConstants.CDEPT_CODE, PoDeptCode ?? string.Empty);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CTRANSACTION_CODE, CurrentRecord?.CTRANSACTION_CODE ?? string.Empty);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CREFERENCE_NO, FilterReferenceNo ?? string.Empty);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CSUPPLIER_ID, PoSupplierId ?? string.Empty);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CPERIODFROM, FilterPeriodFrom ?? string.Empty);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CPERIODTO, FilterPeriodTo ?? string.Empty);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CTRANS_STATUS, SelectedStatus ?? string.Empty);
                var loResult = await _model.GetDataGridAsync();
                DataGridList = new ObservableCollection<FAT00100GetDataGridResultDTO>(loResult.Data ?? new List<FAT00100GetDataGridResultDTO>());
                // Format CREF_DATE for display
                foreach (var item in DataGridList)
                {
                    if (!string.IsNullOrWhiteSpace(item.CREF_DATE) && item.CREF_DATE.Length == 8)
                    {
                        try
                        {
                            item.CREF_DATE_DISPLAY = DateTime.ParseExact(item.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
                            item.DREF_DATE = DateTime.TryParseExact(item.CREF_DATE, "yyyyMMdd",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out var refDate)
                        ? refDate
                        : (DateTime?)null;
                        }
                        catch
                        {
                            item.CREF_DATE_DISPLAY = item.CREF_DATE;
                        }

                    }
                    else
                    {
                        item.CREF_DATE_DISPLAY = item.CREF_DATE;
                    }
                }




            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndTry:
            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get department lookup list - streaming method
        /// </summary>
        public async Task GetDeptLookupListAsync(string pcCompanyId, string pcUserId, string pcProgramId)
        {
            var loEx = new R_Exception();

            try
            {
                // Set streaming context for custom parameters (NOT CCOMPANY_ID, CUSER_ID)
                R_FrontContext.R_SetStreamingContext(ContextConstants.CPROGRAM_ID, pcProgramId);

                var loResult = await _model.FAT00100GetDeptLookupListAsync();
                DeptLookupList = new ObservableCollection<FAT00100GetDeptLookupListResultDTO>(loResult.Data ?? new List<FAT00100GetDeptLookupListResultDTO>());
                var foundDept = DeptLookupList?.ToList().Find(x => x.CDEPT_CODE == SystemParamData.CTRANS_DEPT_CODE);
                if (foundDept != null)
                {
                    PoDeptCode = foundDept.CDEPT_CODE;
                    PoDeptName = foundDept.CDEPT_NAME;
                }   
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region CRUD Methods

        /// <summary>
        /// Get entity record (following GSM02000 pattern)
        /// </summary>
        public async Task GetEntity(FAT00100DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new R_ServiceGetRecordParameterDTO<FAT00100DTO>
                {
                    Entity = poEntity
                };

                var loResult = await _model.R_ServiceGetRecord(loParam);
                CurrentRecord = loResult.data;

                // Set currency codes from CompanyInfoData
                if (CurrentRecord != null && CompanyInfoData != null)
                {
                    CurrentRecord.CLOCAL_CURRENCY_CODE = CompanyInfoData.CLOCAL_CURRENCY_CODE ?? string.Empty;
                    CurrentRecord.CBASE_CURRENCY_CODE = CompanyInfoData.CBASE_CURRENCY_CODE ?? string.Empty;
                    CurrentRecord.CCREATE_DATE= CurrentRecord.DCREATE_DATE.ToString("dd-MMM-yyyy HH:mm");
                    CurrentRecord.CUPDATE_DATE = CurrentRecord.DUPDATE_DATE.ToString("dd-MMM-yyyy HH:mm");
                    CurrentRecord.CREF_DATE_DISPLAY = CurrentRecord.DREF_DATE.ToString("dd-MMMM-yyyy");

                }
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
        public async Task SaveRecordAsync(FAT00100DTO poEntity, eCRUDMode peCRUDMode, string pcCompanyId, string pcLangId)
        {
            var loEx = new R_Exception();

            try
            {
                // Set standard properties
                poEntity.CCOMPANY_ID = pcCompanyId;
                poEntity.CLANG_ID = pcLangId;

                var loParam = new R_ServiceSaveParameterDTO<FAT00100DTO>
                {
                    Entity = poEntity,
                    CRUDMode = peCRUDMode
                };

                var loResult = await _model.R_ServiceSave(loParam);
                CurrentRecord = loResult.data;
                // Note: Data property is read-only and will be updated by the conductor from eventArgs.Result

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //implementation
        /// <summary>
        /// Submit transaction
        /// </summary>
        public async Task FAT00100SubmitTransAsync(string pcCompanyId, string pcUserId, string pcRecId)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new FAT00100SubmitTransParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CUSER_ID = pcUserId,
                    CREC_ID = pcRecId
                };

                await _model.FAT00100SubmitTrans(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Update transaction header status
        /// </summary>
        public async Task FAT00100UpdateTransHdAsync(string pcCompanyId, string pcUserId, string pcRecId, string pcNewStatus)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new FAT00100UpdateTransHdParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CUSER_ID = pcUserId,
                    CREC_ID = pcRecId,
                    CNEW_STATUS = pcNewStatus
                };

                await _model.FAT00100UpdateTransHd(loParam);
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
        public async Task DeleteRecordAsync(FAT00100DTO poEntity, string pcCompanyId, string pcLangId)
        {
            var loEx = new R_Exception();

            try
            {
                // Set standard properties
                poEntity.CCOMPANY_ID = pcCompanyId;
                poEntity.CLANG_ID = pcLangId;

                var loParam = new R_ServiceDeleteParameterDTO<FAT00100DTO>
                {
                    Entity = poEntity
                };

                await _model.R_ServiceDelete(loParam);
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
        /// Validate record before save
        /// </summary>
        public async Task<R_Exception> ValidateRecordAsync(FAT00100DTO poEntity, eCRUDMode peCRUDMode, string pcCompanyId, string pcLangId, bool plIncrementFlag, bool plChangeDesc, bool plPJChecked, string pcTransactionNumber, DateTime? pdTransactionDate, string pcCurrency, string pcDepartmentCode, string pcTransactionNumberForPJ, DateTime? pdDocumentDate)
        {
            var loEx = new R_Exception();

            try
            {
                // Validate transaction number in Add mode if increment flag is false
                if (peCRUDMode == eCRUDMode.AddMode)
                {
                    if (!plIncrementFlag)
                    {
                        if (string.IsNullOrWhiteSpace(pcTransactionNumber))
                        {
                            loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS009"));
                        }
                    }
                }

                // Validate PJ transaction if PJ is checked and change desc is false
                if (plPJChecked && !plChangeDesc)
                {
                    var loPJParam = new FAT00100ValidatePJTransParameterDTO
                    {
                        CCOMPANY_ID = pcCompanyId,
                        CDEPT_CODE = pcDepartmentCode,
                        CTRANSACTION_CODE = DEFAULT_TRANSACTION_CODE,
                        CREFERENCE_NO = pcTransactionNumberForPJ
                    };

                    var loPJResult = await _model.ValidatePJTrans(loPJParam);
                    if (!string.IsNullOrWhiteSpace(loPJResult.Data.CASSET_CODE))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS037"));
                    }
                }

                // Validate transaction date
                if (pdTransactionDate == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS006"));
                }

                // Validate currency
                if (string.IsNullOrWhiteSpace(pcCurrency))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS007"));
                }

                // Validate PJ fields if PJ is checked
                if (plPJChecked && (string.IsNullOrWhiteSpace(pcDepartmentCode) || string.IsNullOrWhiteSpace(pcTransactionNumberForPJ)))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS008"));
                }

                // Validate document date not greater than transaction date
                if (pdDocumentDate != null && pdTransactionDate != null)
                {
                    if (pdDocumentDate.Value > pdTransactionDate.Value)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS013"));
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
        /// Validate transaction date
        /// </summary>
        public async Task<R_Exception> ValidateTransactionDateAsync(string pcCompanyId, DateTime pdTransactionDate, string pcSoftPeriod, bool plCustPeriodFlag)
        {
            var loEx = new R_Exception();

            try
            {
                string lcPRD = string.Empty;
                string lcTransactionDateStr = pdTransactionDate.ToString("yyyyMMdd");
                string lcTodayDateStr = DateTime.Now.ToString("yyyyMMdd");

                // Get period
                if (!plCustPeriodFlag)
                {
                    lcPRD = lcTransactionDateStr.Substring(0, 6);
                }
                else
                {
                    var loParam = new FAT00100GetPeriodDTParameterDTO
                    {
                        CCOMPANY_ID = pcCompanyId,
                        CTRANSACTION_DATE = lcTransactionDateStr
                    };

                    var loResult = await _model.GetPeriodDT(loParam);
                    lcPRD = loResult.Data.CDEFAULTPERIOD;
                }

                // Validate period not less than soft period
                if (!string.IsNullOrWhiteSpace(lcPRD) && !string.IsNullOrWhiteSpace(pcSoftPeriod))
                {
                    if (lcPRD.CompareTo(pcSoftPeriod) < 0)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS004"));
                    }
                }

                // Validate date not greater than today
                if (lcTransactionDateStr.CompareTo(lcTodayDateStr) > 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS005"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            return loEx;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Get department lookup validation
        /// </summary>
        public async Task<int> GetDeptLookUpValidationAsync(string pcCompanyId, string pcDeptCode, string pcUserId)
        {
            var loEx = new R_Exception();
            int liResult = 0;

            try
            {
                var loParam = new FAT00100GetDeptLookUpValidationParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CDEPT_CODE = pcDeptCode,
                    CUSER_ID = pcUserId
                };

                var loRtn = await _model.GetDeptLookUpValidation(loParam);
                liResult = loRtn.Data.IResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return liResult;
        }

        /// <summary>
        /// Validate department code
        /// </summary>
        //public async Task<int> ValidateDeptCodeAsync(string pcCompanyId, string pcDeptCode, string pcUserId)
        //{
        //    var loEx = new R_Exception();
        //    int liResult = 0;

        //    try
        //    {
        //        var loParam = new FAT00100ValidateDeptCodeParameterDTO
        //        {
        //            CCOMPANY_ID = pcCompanyId,
        //            CDEPT_CODE = pcDeptCode,
        //            CUSER_ID = pcUserId
        //        };

        //        var loRtn = await _model.ValidateDeptCode(loParam);
        //        liResult = loRtn.Data.IResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    loEx.ThrowExceptionIfErrors();
        //    return liResult;
        //}

        /// <summary>
        /// Get period DT
        /// </summary>
        public async Task<string> GetPeriodDTAsync(string pcCompanyId, string pcDate)
        {
            var loEx = new R_Exception();
            string lcResult = string.Empty;

            try
            {
                var loParam = new FAT00100GetPeriodDTParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CTRANSACTION_DATE = pcDate
                };

                var loRtn = await _model.GetPeriodDT(loParam);
                lcResult = loRtn.Data.CDEFAULTPERIOD;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return lcResult;
        }

        /// <summary>
        /// Get currency rate
        /// </summary>
        public async Task<FAT00100RSP_GET_CURRENCY_RATEResultDTO> RSP_GET_CURRENCY_RATEAsync(string pcCompanyId, string pcCurrencyCode, string pcTransactionDate, string pcRateTypeCode)
        {
            var loEx = new R_Exception();
            FAT00100RSP_GET_CURRENCY_RATEResultDTO loResult = new FAT00100RSP_GET_CURRENCY_RATEResultDTO();

            try
            {
                var loParam = new FAT00100RSP_GET_CURRENCY_RATEParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CCURRENCY_CODE = pcCurrencyCode,
                    CTRANSACTION_DATE = pcTransactionDate,
                    CRATETYPE_CODE = pcRateTypeCode
                };

                var loRtn = await _model.RSP_GET_CURRENCY_RATE(loParam);
                loResult = loRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        /// <summary>
        /// Get last currency rate
        /// </summary>
        public async Task<FAT00100GetLastCurrencyRateResultDTO> FAT00100GetLastCurrencyRateAsync(string pcCompanyId, string pcCurrencyCode, string pcRateDate)
        {
            var loEx = new R_Exception();
            FAT00100GetLastCurrencyRateResultDTO loResult = new FAT00100GetLastCurrencyRateResultDTO();

            try
            {
                var loParam = new FAT00100GetLastCurrencyRateParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CCURRENCY_CODE = pcCurrencyCode,
                    CRATETYPE_CODE = SystemParamData.CRATETYPE_CODE,
                    CRATE_DATE = pcRateDate
                };

                var loRtn = await _model.FAT00100GetLastCurrencyRate(loParam);
                loResult = loRtn.Data ?? new FAT00100GetLastCurrencyRateResultDTO();
                LastCurrencyRateData = loResult;

            }
            catch (Exception ex)
            {
                LastCurrencyRateData = new FAT00100GetLastCurrencyRateResultDTO();
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        /// <summary>
        /// Submit process
        /// Uses existing CurrentRecord data from ViewModel instead of requiring all parameters
        /// </summary>
        public async Task<FAT00100SubmitProcessResultDTO> SubmitProcessAsync(string pcCompanyId, string pcLangId, string pcUserId)
        {
            var loEx = new R_Exception();
            FAT00100SubmitProcessResultDTO loResult = new FAT00100SubmitProcessResultDTO();

            try
            {
                // Use CurrentRecord which already contains CDEPT_CODE, CTRANSACTION_CODE, CREFERENCE_NO
                var loParam = new FAT00100SubmitProcessParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CLANG_ID = pcLangId,
                    CUSER_ID = pcUserId,
                    CDEPT_CODE = CurrentRecord.CDEPT_CODE,
                    CTRANSACTION_CODE = CurrentRecord.CTRANSACTION_CODE,
                    CREFERENCE_NO = CurrentRecord.CREFERENCE_NO
                };

                var loRtn = await _model.SubmitProcess(loParam);
                loResult = loRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        /// <summary>
        /// Approve process
        /// </summary>
        public async Task<FAT00100ApproveProcessResultDTO> ApproveProcessAsync(string pcCompanyId, string pcUserId, string pcReferenceNo)
        {
            var loEx = new R_Exception();
            FAT00100ApproveProcessResultDTO loResult = new FAT00100ApproveProcessResultDTO();

            try
            {
                var loParam = new FAT00100ApproveProcessParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CUSER_ID = pcUserId,
                    CREFERENCE_NO = pcReferenceNo
                };

                var loRtn = await _model.ApproveProcess(loParam);
                loResult = loRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        /// <summary>
        /// Close process
        /// </summary>
        public async Task<FAT00100CloseProcessResultDTO> CloseProcessAsync(string pcCompanyId, string pcUserId, string pcReferenceNo)
        {
            var loEx = new R_Exception();
            FAT00100CloseProcessResultDTO loResult = new FAT00100CloseProcessResultDTO();

            try
            {
                var loParam = new FAT00100CloseProcessParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CUSER_ID = pcUserId,
                    CREFERENCE_NO = pcReferenceNo
                };

                var loRtn = await _model.CloseProcess(loParam);
                loResult = loRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        /// <summary>
        /// Void process
        /// </summary>
        public async Task<FAT00100VoidProcessResultDTO> VoidProcessAsync(string pcCompanyId, string pcUserId, string pcReferenceNo)
        {
            var loEx = new R_Exception();
            FAT00100VoidProcessResultDTO loResult = new FAT00100VoidProcessResultDTO();

            try
            {
                var loParam = new FAT00100VoidProcessParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CUSER_ID = pcUserId,
                    CREFERENCE_NO = pcReferenceNo
                };

                var loRtn = await _model.VoidProcess(loParam);
                loResult = loRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        /// <summary>
        /// Validation before submit
        /// </summary>
        public async Task<FAT00100ValidationBeforeSubmitResultDTO> ValidationBeforeSubmitAsync(string pcCompanyId, string pcDeptCode, string pcTransactionCode, string pcReferenceNo)
        {
            var loEx = new R_Exception();
            FAT00100ValidationBeforeSubmitResultDTO loResult = new FAT00100ValidationBeforeSubmitResultDTO();

            try
            {
                var loParam = new FAT00100ValidationBeforeSubmitParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CDEPT_CODE = pcDeptCode,
                    CTRANSACTION_CODE = pcTransactionCode,
                    CREFERENCE_NO = pcReferenceNo
                };

                var loRtn = await _model.ValidationBeforeSubmit(loParam);
                loResult = loRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        /// <summary>
        /// Validation before close
        /// </summary>
        public async Task<FAT00100ValidationBeforeCloseResultDTO> ValidationBeforeCloseAsync(string pcCompanyId, string pcReferenceNo)
        {
            var loEx = new R_Exception();
            FAT00100ValidationBeforeCloseResultDTO loResult = new FAT00100ValidationBeforeCloseResultDTO();

            try
            {
                var loParam = new FAT00100ValidationBeforeCloseParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CREFERENCE_NO = pcReferenceNo
                };

                var loRtn = await _model.ValidationBeforeClose(loParam);
                loResult = loRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        /// <summary>
        /// Validation asset code
        /// </summary>
        public async Task<FAT00100ValidationAssetCodeResultDTO> ValidationAssetCodeAsync(string pcCompanyId, string pcAssetCode)
        {
            var loEx = new R_Exception();
            FAT00100ValidationAssetCodeResultDTO loResult = new FAT00100ValidationAssetCodeResultDTO();

            try
            {
                var loParam = new FAT00100ValidationAssetCodeParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CDEPT_CODE = string.Empty,
                    CFILTER_TRANS_CODE = string.Empty,
                    CREFERENCE_NO = pcAssetCode
                };

                var loRtn = await _model.ValidationAssetCode(loParam);
                loResult = loRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        /// <summary>
        /// Run approval precheck
        /// </summary>
        public async Task<FAT00100RunApprovalPrecheckResultDTO> RunApprovalPrecheckAsync(string pcCompanyId, string pcReferenceNo)
        {
            var loEx = new R_Exception();
            FAT00100RunApprovalPrecheckResultDTO loResult = new FAT00100RunApprovalPrecheckResultDTO();

            try
            {
                var loParam = new FAT00100RunApprovalPrecheckParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CAPPROVAL_CODE = pcReferenceNo
                };

                var loRtn = await _model.RunApprovalPrecheck(loParam);
                loResult = loRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        /// <summary>
        /// Get status list (streaming method)
        /// </summary>
        public async Task GetStatusListAsync(string pcCompanyId, string pcLanguageId, string pcApplication = "RHAPSODY", string pcClassId = "_TRX_STATUS", string pcRecIdList = "")
        {
            var loEx = new R_Exception();

            try
            {
                // Set streaming context for custom parameters (NOT CCOMPANY_ID, CLANGUAGE_ID - handled automatically)
                R_FrontContext.R_SetStreamingContext(ContextConstants.CAPPLICATION, pcApplication);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CCLASS_ID, pcClassId);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CREC_ID_LIST, pcRecIdList);

                var loResult = await _model.FAT00100GetStatusListAsync();
                StatusList = new ObservableCollection<FAT00100GetStatusListResultDTO>(loResult.Data ?? new List<FAT00100GetStatusListResultDTO>());
                // Add "All" item at the beginning of the status list
                StatusList.Insert(0, new FAT00100GetStatusListResultDTO { CCODE = "", CNAME = "All" });
                SelectedStatus = "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get currency list (streaming method)
        /// </summary>
        public async Task GetCurrencyListAsync(string pcCompanyId, string pcUserId)
        {
            var loEx = new R_Exception();

            try
            {
                // No streaming context needed - CCOMPANY_ID and CUSER_ID are handled automatically by R_BackGlobalVar in Controller
                var loResult = await _model.FAT00100GetCurrencyListAsync();
                CurrencyList = new ObservableCollection<FAT00100GetCurrencyListResultDTO>(loResult.Data ?? new List<FAT00100GetCurrencyListResultDTO>());
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

