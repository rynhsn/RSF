using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public class FAT00800EntryViewModel : R_ViewModel<FAT00800DTO>
    {
        private readonly FAT00800EntryModel _model = new FAT00800EntryModel();

        public const string DEFAULT_TRANSACTION_CODE = "270010";

        // Main entity for R_Conductor synchronization (keep in sync with base Data via R_SetCurrentData)
        public FAT00800DTO Entity { get; set; } = new FAT00800DTO();

        public FAT00800EntryViewModel()
        {
            R_SetCurrentData(new FAT00800DTO());
        }

        // Initial process result data (from Model)
        public FAT00800GetCompanyInfoResultDTO CompanyInfoData { get; set; } = new FAT00800GetCompanyInfoResultDTO();
        public FAT00800GetGetSystemParamResultDTO SystemParamData { get; set; } = new FAT00800GetGetSystemParamResultDTO();
        public FAT00800GetTransCodeInfoResultDTO TransCodeInfoData { get; set; } = new FAT00800GetTransCodeInfoResultDTO();
        public FAT00800GetYearRangeResultDTO YearRangeData { get; set; } = new FAT00800GetYearRangeResultDTO();
        public FAT00800GetLastCurrencyRateResultDTO LastCurrencyRateData { get; set; } = new FAT00800GetLastCurrencyRateResultDTO();

        // Streaming list bindings
        public ObservableCollection<FAT00800GetCurrencyListResultDTO> CurrencyList { get; set; } = new ObservableCollection<FAT00800GetCurrencyListResultDTO>();
        public ObservableCollection<FAT00800GetDeptLookupListResultDTO> DeptLookupList { get; set; } = new ObservableCollection<FAT00800GetDeptLookupListResultDTO>();

        // Initial process properties
        public FAT00800GetPeriodResultDTO Period { get; set; } = new FAT00800GetPeriodResultDTO();
        public FAT00800GetLocalBaseCurrResultDTO Currency { get; set; } = new FAT00800GetLocalBaseCurrResultDTO();
        public FAT00800GetTransTypeDescResultDTO TransTypeDesc { get; set; } = new FAT00800GetTransTypeDescResultDTO();
        public ObservableCollection<FAT00800GetTransListResultDTO> TransList { get; set; } = new ObservableCollection<FAT00800GetTransListResultDTO>();
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


        public string CASSET_DEPT_CODE { get; set; } = string.Empty;
        public string CASSET_DEPT_NAME { get; set; } = string.Empty;
        
        public decimal NLBOOK_VALUE { get; set; }
        public decimal NBOOK_VALUE { get; set; }

        public string CEXPENSE_ALLOC_NAME { get; set; } = string.Empty;


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
                var loParam = new R_ServiceGetRecordParameterDTO<FAT00800DTO>
                {
                    Entity = poEntity
                };

                var loResult = await _model.R_ServiceGetRecord(loParam);
                Entity = loResult.data ?? new FAT00800DTO();
                R_SetCurrentData(Entity);

                if (!string.IsNullOrWhiteSpace(Entity.CTRANSACTION_DATE))
                {
                    Entity.DTRANSACTION_DATE = DateTime.ParseExact(Entity.CTRANSACTION_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    Entity.DTRANSACTION_DATE = DateTime.Now;
                }



                //Entity.CCREATE_DATE = CurrentRecord.DCREATE_DATE.ToString("dd-MMM-yyyy HH:mm");
                //Entity.CUPDATE_DATE = CurrentRecord.DUPDATE_DATE.ToString("dd-MMM-yyyy HH:mm");
                

                loCurrencyTemp = Entity.CCURRENCY_CODE;
                LenableEdit = R_IsStatusEditable(Entity.CSTATUS);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
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
                var loParam = new R_ServiceSaveParameterDTO<FAT00800DTO>
                {
                    Entity = poEntity,
                    CRUDMode = (eCRUDMode)peCRUDMode
                };

                var loResult = await _model.R_ServiceSave(loParam);
                Entity = loResult.data ?? new FAT00800DTO();
                R_SetCurrentData(Entity);
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
                var loParam = new R_ServiceDeleteParameterDTO<FAT00800DTO>
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

        #region Initial Process

        /// <summary>
        /// Initial process: load company info, system param, trans code info, year range and set global params
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcUserId">User ID</param>
        /// <param name="pcLangId">Language/Culture ID</param>
        public async Task GetInitialProcessAsync(string pcCompanyId, string pcUserId, string pcLangId)
        {
            var loEx = new R_Exception();
            try
            {
                var loCompanyParam = new FAT00800GetCompanyInfoParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CLANG_ID = pcLangId,
                    CUSER_ID = pcUserId
                };
                var loCompanyResult = await _model.FAT00800GetCompanyInfoAsync(loCompanyParam);
                CompanyInfoData = loCompanyResult.Data ?? new FAT00800GetCompanyInfoResultDTO();
                CLOCAL_CURRENCY_CODE = CompanyInfoData.CLOCAL_CURRENCY_CODE ?? string.Empty;
                CBASE_CURRENCY_CODE = CompanyInfoData.CBASE_CURRENCY_CODE ?? string.Empty;

                var loSystemParam = new FAT00800GetGetSystemParamParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CLANG_ID = pcLangId,
                    CUSER_ID = pcUserId,
                    CLANGUAGE_ID = pcLangId
                };
                var loSystemResult = await _model.FAT00800GetGetSystemParamAsync(loSystemParam);
                SystemParamData = loSystemResult.Data ?? new FAT00800GetGetSystemParamResultDTO();
                CSOFT_PERIOD = SystemParamData.CSOFT_PERIOD ?? string.Empty;
                CRATETYPE_CODE = SystemParamData.CRATETYPE_CODE ?? string.Empty;
                CGLLINK_DATE = SystemParamData.CGLLINK_DATE ?? string.Empty;
                CDEFAULT_TRX_DEPT_CODE = SystemParamData.CTRANS_DEPT_CODE ?? string.Empty;
                CCURRENT_PERIOD = SystemParamData.CCURRENT_PERIOD ?? string.Empty;

                var loTransCodeParam = new FAT00800GetTransCodeInfoParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CLANG_ID = pcLangId,
                    CUSER_ID = pcUserId,
                    CTRANS_CODE = VAR_CTRANS_CODE
                };
                var loTransCodeResult = await _model.FAT00800GetTransCodeInfoAsync(loTransCodeParam);
                TransCodeInfoData = loTransCodeResult.Data ?? new FAT00800GetTransCodeInfoResultDTO();
                LINCREMENT_FLAG = TransCodeInfoData.LINCREMENT_FLAG;
                LTRANS_APPROVAL = TransCodeInfoData.LAPPROVAL_FLAG;

                var loYearRangeParam = new FAT00800GetYearRangeParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CLANG_ID = string.Empty,
                    CUSER_ID = string.Empty,
                    CCYEAR = string.Empty,
                    CMODE = string.Empty
                };
                var loYearRangeResult = await _model.FAT00800GetYearRangeAsync(loYearRangeParam);
                YearRangeData = loYearRangeResult.Data ?? new FAT00800GetYearRangeResultDTO();
                PeriodFromYear = YearRangeData.IMIN_YEAR;
                PeriodToYear = YearRangeData.IMAX_YEAR;
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
        /// Get currency list and assign to CurrencyList (streaming; context from backend).
        /// </summary>
        public async Task GetCurrencyListAsync()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetCurrencyListAsync();
                CurrencyList = new ObservableCollection<FAT00800GetCurrencyListResultDTO>(loResult.Data ?? new List<FAT00800GetCurrencyListResultDTO>());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get department lookup list; sets streaming context CPROGRAM_ID then calls model.
        /// </summary>
        /// <param name="pcProgramId">Program ID for department filter</param>
        public async Task GetDeptLookupListAsync(string pcProgramId)
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstants.CPROGRAM_ID, pcProgramId ?? string.Empty);
                var loResult = await _model.FAT00800GetDeptLookupListAsync();
                DeptLookupList = new ObservableCollection<FAT00800GetDeptLookupListResultDTO>(loResult.Data ?? new List<FAT00800GetDeptLookupListResultDTO>());
                var foundDept = DeptLookupList?.ToList().Find(x => x.CDEPT_CODE == SystemParamData.CTRANS_DEPT_CODE);
                if (foundDept != null)
                {
                    Entity.CDEPT_CODE = foundDept.CDEPT_CODE;
                    Entity.CDEPT_NAME = foundDept.CDEPT_NAME;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get last currency rate for the given currency, rate type and date (RSP_GS_GET_LAST_CURRENCY_RATE).
        /// </summary>
        /// <param name="pcCurrencyCode">Currency code (e.g. IDR)</param>
        /// <param name="pcRateTypeCode">Rate type code (use ViewModel CRATETYPE_CODE or pass empty)</param>
        /// <param name="pcRateDate">Rate date in yyyyMMdd format</param>
        /// <returns>Task</returns>
        public async Task GetLastCurrencyRateAsync(string pcCurrencyCode, string pcRateTypeCode, string pcRateDate)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new FAT00800GetLastCurrencyRateParameterDTO
                {
                    CCURRENCY_CODE = pcCurrencyCode ?? string.Empty,
                    CRATETYPE_CODE = pcRateTypeCode ?? string.Empty,
                    CRATE_DATE = pcRateDate ?? string.Empty
                };

                var loResult = await _model.FAT00800GetLastCurrencyRateAsync(loParam);
                LastCurrencyRateData = loResult.Data ?? new FAT00800GetLastCurrencyRateResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Update transaction header status via FAT00800UpdateTransHdStatus.
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcUserId">User ID</param>
        /// <param name="pcRecId">Record ID (CREC_ID)</param>
        /// <param name="pcNewStatus">New status code (CNEW_STATUS)</param>
        /// <returns>Task</returns>
        public async Task UpdateTransHdStatusAsync(string pcCompanyId, string pcUserId, string pcRecId, string pcNewStatus)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new FAT00800UpdateTransHdStatusParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId ?? string.Empty,
                    CUSER_ID = pcUserId ?? string.Empty,
                    CREC_ID = pcRecId ?? string.Empty,
                    CNEW_STATUS = pcNewStatus ?? string.Empty
                };

                await _model.FAT00800UpdateTransHdStatus(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Submit transaction via FAT00800SubmitTrans (RSP_FAT00800_SUBMIT_TRANS).
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcUserId">User ID</param>
        /// <param name="pcRecId">Record ID (CREC_ID)</param>
        /// <returns>Task</returns>
        public async Task SubmitTransAsync(string pcCompanyId, string pcUserId, string pcRecId)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new FAT00800SubmitTransParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId ?? string.Empty,
                    CUSER_ID = pcUserId ?? string.Empty,
                    CREC_ID = pcRecId ?? string.Empty
                };

                await _model.FAT00800SubmitTrans(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Returns true when transaction status allows edit (e.g. draft).
        /// </summary>
        private static bool R_IsStatusEditable(string pcStatus)
        {
            return string.IsNullOrEmpty(pcStatus) || pcStatus == "00";
        }

        #endregion

        #region Validation Methods

        /// <summary>
        /// Validate record before save (returns R_Exception with validation errors)
        /// </summary>
        /// <param name="poEntity">Transaction entity to validate</param>
        /// <param name="peMode">Conductor mode</param>
        /// <returns>R_Exception with validation errors if any</returns>
        public R_Exception ValidateRecord(FAT00800DTO poEntity, R_eConductorMode peMode)
        {
            var loEx = new R_Exception();

            // // Date validations
            // if (ValDate1)
            // {
            //     loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS005"));
            // }
            
            

            return loEx;
        }

        #endregion

    }

    /// <summary>
    /// DTO for Period Month ComboBox
    /// </summary>
    public class PeriodMonthDTO
    {
        public string CPERIOD_NO { get; set; } = string.Empty;
    }




}

