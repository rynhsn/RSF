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

        // Current form data
        public FAT00100DTO CurrentRecord { get; set; } = new FAT00100DTO();

        // Initialization data
        public FAT00100GetInitialProcessResultDTO InitialProcessData { get; set; } = new FAT00100GetInitialProcessResultDTO();
        public FAT00100GetPeriodYearResultDTO PeriodYearData { get; set; } = new FAT00100GetPeriodYearResultDTO();

        // Lists
        public ObservableCollection<FAT00100GetComboPeriodMonthResultDTO> ComboPeriodMonthList { get; set; } = new ObservableCollection<FAT00100GetComboPeriodMonthResultDTO>();
        public ObservableCollection<FAT00100GetDataGridResultDTO> DataGridList { get; set; } = new ObservableCollection<FAT00100GetDataGridResultDTO>();
        public ObservableCollection<FAT00100GetAssetListResultDTO> AssetList { get; set; } = new ObservableCollection<FAT00100GetAssetListResultDTO>();
        public ObservableCollection<FAT00100GetGSM_SUPPLIER_CONTACTResultDTO> SupplierContactList { get; set; } = new ObservableCollection<FAT00100GetGSM_SUPPLIER_CONTACTResultDTO>();
        public ObservableCollection<FAT00100CPDTO> ContactPersonList { get; set; } = new ObservableCollection<FAT00100CPDTO>();

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

        // Additional state
        public string GLTransferStatus { get; set; } = string.Empty;
        public bool GLLink { get; set; }

        #region Initialization Methods

        /// <summary>
        /// Get initial process data for form initialization
        /// </summary>
        public async Task GetInitialProcessAsync(string pcCompanyId, string pcUserId, string pcReferenceNo, string pcDeptCode, string pcPJTransCode, string pcTransactionCode)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new FAT00100GetInitialProcessParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CUSER_ID = pcUserId,
                    CREFERENCE_NO = pcReferenceNo,
                    CDEPT_CODE = pcDeptCode,
                    CPJ_TRANS_CODE = pcPJTransCode,
                    CTRANSACTION_CODE = pcTransactionCode
                };

                var loResult = await _model.GetInitialProcess(loParam);
                InitialProcessData = loResult.Data;

                // Store properties from result
                if (InitialProcessData != null)
                {
                    DefaultTrxDeptCode = InitialProcessData.CTRANS_DEPT_CODE;
                    DefaultAssetDeptCode = InitialProcessData.CASSET_DEPT_CODE;
                    AssetIncrementFlag = InitialProcessData.LASSET_INCREMENT_FLAG;
                    JrngrpMode = InitialProcessData.LJRNGRP_MODE;
                    DeptMode = InitialProcessData.LDEPT_MODE;
                    PeriodMode = InitialProcessData.CPERIOD_MODE;
                    CurrentPeriod = InitialProcessData.CCURRENT_PERIOD;
                    SoftPeriod = InitialProcessData.CSOFT_PERIOD;
                    RateTypeCode = InitialProcessData.CRATETYPE_CODE;
                    GlinkDate = InitialProcessData.CGLLINK_DATE;
                    PJlinkDate = InitialProcessData.CPJLINK_DATE;
                    FilterSupplierId = InitialProcessData.CSUPPLIER_ID;
                    TransactionPrd = InitialProcessData.CTRANSACTION_PRD;
                    LocalCurrencyCode = InitialProcessData.CLOCAL_CURRENCY_CODE;
                    BaseCurrencyCode = InitialProcessData.CBASE_CURRENCY_CODE;
                    CustPeriodFlag = InitialProcessData.LCUST_PERIOD_FLAG;
                    FilterTransDesc = InitialProcessData.CFILTER_TRANS_DESC;
                    ApprovalFlag = InitialProcessData.LAPPROVAL_FLAG;
                    IncrementFlag = InitialProcessData.LINCREMENT_FLAG;
                    PJTransDesc = InitialProcessData.CPJ_TRANS_DESC;
                    CanApprove = true; // Set to true as per VB.NET code
                    CanClose = true; // Set to true as per VB.NET code
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

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

        #endregion

        #region Streaming Methods

        /// <summary>
        /// Get combo period month - streaming method
        /// </summary>
        public async Task GetComboPeriodMonthAsync(string pcCompanyId, string pcReferenceNo, string pcSoftPeriod)
        {
            var loEx = new R_Exception();

            try
            {
                // Set streaming context for custom parameters (NOT CCOMPANY_ID, CUSER_ID)
                R_FrontContext.R_SetStreamingContext(ContextConstants.CSOFT_PERIOD, pcSoftPeriod);

                var loResult = await _model.GetComboPeriodMonthAsync();
                ComboPeriodMonthList = new ObservableCollection<FAT00100GetComboPeriodMonthResultDTO>(loResult.Data ?? new List<FAT00100GetComboPeriodMonthResultDTO>());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get data grid - streaming method for main grid
        /// </summary>
        public async Task GetDataGridAsync(string pcCompanyId, string pcLangId, string pcDeptCode, string pcTransactionCode, string pcReferenceNo, string pcSupplierId, string pcPeriodFrom, string pcPeriodTo, string pcStatusDraft, string pcStatusOpen, string pcStatusApproved, string pcStatusClosed)
        {
            var loEx = new R_Exception();

            try
            {
                // Set streaming context for all custom parameters (NOT CCOMPANY_ID, CFOREIGN_LANGUAGE)
                R_FrontContext.R_SetStreamingContext(ContextConstants.CDEPT_CODE, pcDeptCode);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CTRANSACTION_CODE, pcTransactionCode);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CREFERENCE_NO, pcReferenceNo);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CSUPPLIER_ID, pcSupplierId);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CPERIODFROM, pcPeriodFrom);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CPERIODTO, pcPeriodTo);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CSTATUSDRAFT, pcStatusDraft);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CSTATUSOPEN, pcStatusOpen);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CSTATUSAPPROVED, pcStatusApproved);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CSTATUSCLOSED, pcStatusClosed);

                var loResult = await _model.GetDataGridAsync();
                DataGridList = new ObservableCollection<FAT00100GetDataGridResultDTO>(loResult.Data ?? new List<FAT00100GetDataGridResultDTO>());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get asset list - streaming method for asset grid
        /// </summary>
        public async Task GetAssetListAsync(string pcCompanyId, string pcLangId, string pcDeptCode, string pcTransactionCode, string pcReferenceNo, string pcStatus, DateTime pdUpdateDate)
        {
            var loEx = new R_Exception();

            try
            {
                // Set streaming context for custom parameters (NOT CCOMPANY_ID, CFOREIGN_LANGUAGE)
                R_FrontContext.R_SetStreamingContext(ContextConstants.CDEPT_CODE, pcDeptCode);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CTRANSACTION_CODE, pcTransactionCode);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CREFERENCE_NO, pcReferenceNo);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CSTATUS, pcStatus);
                R_FrontContext.R_SetStreamingContext(ContextConstants.DUPDATE_DATE, pdUpdateDate);

                var loResult = await _model.GetAssetListAsync();
                AssetList = new ObservableCollection<FAT00100GetAssetListResultDTO>(loResult.Data ?? new List<FAT00100GetAssetListResultDTO>());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get GSM supplier info - streaming method
        /// </summary>
        public async Task GetGSM_SUPPLIER_INFOAsync(string pcCompanyId, string pcSupplierId, string pcInfoSeqNo)
        {
            var loEx = new R_Exception();

            try
            {
                // Set streaming context for custom parameters (NOT CCOMPANY_ID)
                R_FrontContext.R_SetStreamingContext(ContextConstants.CINFO_SEQNO, pcInfoSeqNo);

                var loResult = await _model.GetGSM_SUPPLIER_INFOAsync();
                if (loResult.Data != null && loResult.Data.Count > 0)
                {
                    SupplierInfo = loResult.Data[0];
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get GSM supplier contact - streaming method
        /// </summary>
        public async Task GetGSM_SUPPLIER_CONTACTAsync(string pcCompanyId, string pcSupplierId, string pcInfoSeqNo)
        {
            var loEx = new R_Exception();

            try
            {
                // Set streaming context for custom parameters (NOT CCOMPANY_ID)
                R_FrontContext.R_SetStreamingContext(ContextConstants.CINFO_SEQNO, pcInfoSeqNo);

                var loResult = await _model.GetGSM_SUPPLIER_CONTACTAsync();
                SupplierContactList = new ObservableCollection<FAT00100GetGSM_SUPPLIER_CONTACTResultDTO>(loResult.Data ?? new List<FAT00100GetGSM_SUPPLIER_CONTACTResultDTO>());
                
                // Also populate ContactPersonList from result
                ContactPersonList = new ObservableCollection<FAT00100CPDTO>();
                if (loResult.Data != null)
                {
                    foreach (var loItem in loResult.Data)
                    {
                        ContactPersonList.Add(new FAT00100CPDTO
                        {
                            CCOMPANY_ID = loItem.CCOMPANY_ID,
                            CSUPPLIER_ID = loItem.CSUPPLIER_ID,
                            CINFO_SEQNO = loItem.CINFO_SEQNO,
                            CCONTACT_SEQNO = loItem.CCONTACT_SEQNO,
                            CFIRST_NAME = loItem.CFIRST_NAME,
                            CLAST_NAME = loItem.CLAST_NAME,
                            CTITLE = loItem.CTITLE,
                            COCCUP_CODE = loItem.COCCUP_CODE,
                            LDEFAULT = loItem.LDEFAULT,
                            CCREATE_BY = loItem.CCREATE_BY,
                            DCREATE_DATE = loItem.DCREATE_DATE,
                            CUPDATE_BY = loItem.CUPDATE_BY,
                            DUPDATE_DATE = loItem.DUPDATE_DATE
                        });
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

                // Handle nested DTOs
                if (CurrentRecord != null)
                {
                    if (CurrentRecord.oCP != null)
                    {
                        ContactPersonList = new ObservableCollection<FAT00100CPDTO>(CurrentRecord.oCP);
                    }
                    if (CurrentRecord.oSupp != null)
                    {
                        // Map supplier info
                        SupplierInfo = new FAT00100GetGSM_SUPPLIER_INFOResultDTO
                        {
                            CCOMPANY_ID = CurrentRecord.oSupp.CCOMPANY_ID,
                            CSUPPLIER_ID = CurrentRecord.oSupp.CSUPPLIER_ID,
                            CINFO_SEQNO = CurrentRecord.oSupp.CINFO_SEQNO,
                            CSUPPLIER_NAME = CurrentRecord.oSupp.CSUPPLIER_NAME,
                            CADDRESS = CurrentRecord.oSupp.CADDRESS,
                            CPOSTAL_CODE = CurrentRecord.oSupp.CPOSTAL_CODE,
                            CCITY = CurrentRecord.oSupp.CCITY,
                            CCOUNTRY_CODE = CurrentRecord.oSupp.CCOUNTRY_CODE,
                            CSTATE_CODE = CurrentRecord.oSupp.CSTATE_CODE,
                            CPHONE_1 = CurrentRecord.oSupp.CPHONE_1,
                            CPHONE_2 = CurrentRecord.oSupp.CPHONE_2,
                            CPHONE_3 = CurrentRecord.oSupp.CPHONE_3,
                            CFAX_NO1 = CurrentRecord.oSupp.CFAX_NO1,
                            CFAX_NO2 = CurrentRecord.oSupp.CFAX_NO2,
                            CFAX_NO3 = CurrentRecord.oSupp.CFAX_NO3,
                            CEMAIL_1 = CurrentRecord.oSupp.CEMAIL_1,
                            CEMAIL_2 = CurrentRecord.oSupp.CEMAIL_2,
                            CEMAIL_3 = CurrentRecord.oSupp.CEMAIL_3,
                            CTAX_REG_TP = CurrentRecord.oSupp.CTAX_REG_TP,
                            CTAX_NAME = CurrentRecord.oSupp.CTAX_NAME,
                            CTAX_REGISTER_ID = CurrentRecord.oSupp.CTAX_REGISTER_ID,
                            DTAX_REGISTER_DATE = CurrentRecord.oSupp.DTAX_REGISTER_DATE,
                            CTAX_BUSINESS_TYPE = CurrentRecord.oSupp.CTAX_BUSINESS_TYPE,
                            CTAX_BUSINESS_NAME = CurrentRecord.oSupp.CTAX_BUSINESS_NAME,
                            CNPWP = CurrentRecord.oSupp.CNPWP,
                            CNOTES = CurrentRecord.oSupp.CNOTES
                        };
                    }
                    GLTransferStatus = CurrentRecord.CGL_TRF_STATUS;
                    GLLink = CurrentRecord.LGLLINK;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

                loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get record for conductor (legacy method - kept for backward compatibility)
        /// </summary>
        public async Task GetRecordAsync(string pcCompanyId, string pcLangId, string pcDeptCode, string pcFilterTransCode, string pcReferenceNo)
        {
            var loEx = new R_Exception();

            try
            {
                var loEntity = new FAT00100DTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CLANG_ID = pcLangId,
                    CDEPT_CODE = pcDeptCode,
                    CFILTER_TRANS_CODE = pcFilterTransCode,
                    CREFERENCE_NO = pcReferenceNo
                };

                await GetEntity(loEntity);
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

                // Handle nested DTOs
                if (CurrentRecord != null)
                {
                    if (CurrentRecord.oCP != null && CurrentRecord.oCP.Count > 0)
                    {
                        ContactPersonList = new ObservableCollection<FAT00100CPDTO>(CurrentRecord.oCP);
                    }
                    else
                    {
                        CurrentRecord.oCP = new List<FAT00100CPDTO>();
                    }

                    if (CurrentRecord.oSupp != null)
                    {
                        // Map supplier info back
                        SupplierInfo = new FAT00100GetGSM_SUPPLIER_INFOResultDTO
                        {
                            CCOMPANY_ID = CurrentRecord.oSupp.CCOMPANY_ID,
                            CSUPPLIER_ID = CurrentRecord.oSupp.CSUPPLIER_ID,
                            CINFO_SEQNO = CurrentRecord.oSupp.CINFO_SEQNO,
                            CSUPPLIER_NAME = CurrentRecord.oSupp.CSUPPLIER_NAME,
                            CADDRESS = CurrentRecord.oSupp.CADDRESS,
                            CPOSTAL_CODE = CurrentRecord.oSupp.CPOSTAL_CODE,
                            CCITY = CurrentRecord.oSupp.CCITY,
                            CCOUNTRY_CODE = CurrentRecord.oSupp.CCOUNTRY_CODE,
                            CSTATE_CODE = CurrentRecord.oSupp.CSTATE_CODE,
                            CPHONE_1 = CurrentRecord.oSupp.CPHONE_1,
                            CPHONE_2 = CurrentRecord.oSupp.CPHONE_2,
                            CPHONE_3 = CurrentRecord.oSupp.CPHONE_3,
                            CFAX_NO1 = CurrentRecord.oSupp.CFAX_NO1,
                            CFAX_NO2 = CurrentRecord.oSupp.CFAX_NO2,
                            CFAX_NO3 = CurrentRecord.oSupp.CFAX_NO3,
                            CEMAIL_1 = CurrentRecord.oSupp.CEMAIL_1,
                            CEMAIL_2 = CurrentRecord.oSupp.CEMAIL_2,
                            CEMAIL_3 = CurrentRecord.oSupp.CEMAIL_3,
                            CTAX_REG_TP = CurrentRecord.oSupp.CTAX_REG_TP,
                            CTAX_NAME = CurrentRecord.oSupp.CTAX_NAME,
                            CTAX_REGISTER_ID = CurrentRecord.oSupp.CTAX_REGISTER_ID,
                            DTAX_REGISTER_DATE = CurrentRecord.oSupp.DTAX_REGISTER_DATE,
                            CTAX_BUSINESS_TYPE = CurrentRecord.oSupp.CTAX_BUSINESS_TYPE,
                            CTAX_BUSINESS_NAME = CurrentRecord.oSupp.CTAX_BUSINESS_NAME,
                            CNPWP = CurrentRecord.oSupp.CNPWP,
                            CNOTES = CurrentRecord.oSupp.CNOTES
                        };
                    }
                    else
                    {
                        CurrentRecord.oSupp = new FAT00100SuppDTO();
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
                        CTRANSACTION_CODE = "200010",
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
        public async Task<int> ValidateDeptCodeAsync(string pcCompanyId, string pcDeptCode, string pcUserId)
        {
            var loEx = new R_Exception();
            int liResult = 0;

            try
            {
                var loParam = new FAT00100ValidateDeptCodeParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CDEPT_CODE = pcDeptCode,
                    CUSER_ID = pcUserId
                };

                var loRtn = await _model.ValidateDeptCode(loParam);
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
        /// Submit process
        /// </summary>
        public async Task<FAT00100SubmitProcessResultDTO> SubmitProcessAsync(string pcCompanyId, string pcUserId, string pcReferenceNo)
        {
            var loEx = new R_Exception();
            FAT00100SubmitProcessResultDTO loResult = new FAT00100SubmitProcessResultDTO();

            try
            {
                var loParam = new FAT00100SubmitProcessParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CUSER_ID = pcUserId,
                    CREFERENCE_NO = pcReferenceNo
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
        public async Task<FAT00100ValidationBeforeSubmitResultDTO> ValidationBeforeSubmitAsync(string pcCompanyId, string pcReferenceNo)
        {
            var loEx = new R_Exception();
            FAT00100ValidationBeforeSubmitResultDTO loResult = new FAT00100ValidationBeforeSubmitResultDTO();

            try
            {
                var loParam = new FAT00100ValidationBeforeSubmitParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
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

        #endregion
    }
}

