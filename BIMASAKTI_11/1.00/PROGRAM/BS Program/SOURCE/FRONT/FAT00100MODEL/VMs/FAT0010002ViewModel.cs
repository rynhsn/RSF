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
using System.Globalization;

namespace FAT00100Model.VMs
{
    /// <summary>
    /// ViewModel for FAT0010002 - Fixed Asset Acquisition Detail
    /// Handles form operations, validation, and data retrieval
    /// </summary>
    public class FAT0010002ViewModel : R_ViewModel<FAT0010002DTO>
    {
        private readonly FAT0010002Model _model = new FAT0010002Model();

        public string DEFAULT_TRANSACTION_CODE = "200010";
        public string DEFAULT_PJ_TRANSACTION_CODE = "420010";
        public string DEFAULT_SOURCE_MODULE_FA = "FA";
        public string DEFAULT_SOURCE_MODULE_PJ = "PJ";
        public string DEFAULT_STATUS_DRAFT = "00";
        public string DEFAULT_GL_TRF_STATUS = "0";
        public string STATUS_FLAG_DISABLED = "0";
        public string STATUS_FLAG_ENABLED = "1";

        // Current form data
        public FAT0010002DTO CurrentRecord { get; set; } = new FAT0010002DTO();

        // Header data
        public FAT0010002GetFAAcquisitionDetailHeaderResultDTO HeaderData { get; set; } = new FAT0010002GetFAAcquisitionDetailHeaderResultDTO();

        // Transaction detail data
        public FAT0010002GetTransDetailResultDTO TransDetailData { get; set; } = new FAT0010002GetTransDetailResultDTO();

        // Lists
        public ObservableCollection<FAT00100GetStatusListResultDTO> ComboDepreciationMethodList { get; set; } = new ObservableCollection<FAT00100GetStatusListResultDTO>();
        public FAT00100GetStatusListResultDTO ComboDepreciationMethodFirstItem { get; set; } = new FAT00100GetStatusListResultDTO();
        public ObservableCollection<FAT0010002GetFAAcquisitionDetailAssetListResultDTO> AssetList { get; set; } = new ObservableCollection<FAT0010002GetFAAcquisitionDetailAssetListResultDTO>();
        public ObservableCollection<FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO> AllocExpenPageList { get; set; } = new ObservableCollection<FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO>();
        public ObservableCollection<FAT00100GetTransExpAllocListResultDTO> TransExpAllocList { get; set; } = new ObservableCollection<FAT00100GetTransExpAllocListResultDTO>();

        // Form state properties (extracted from VB.NET module variables)
        public string DeptCode { get; set; } = string.Empty;
        public string ReferenceNo { get; set; } = string.Empty;
        public string RecId { get; set; } = string.Empty;
        public string TransactionCode { get; set; } = string.Empty;
        public string Mode { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string LocalCurrencyCode { get; set; } = string.Empty;
        public string BaseCurrencyCode { get; set; } = string.Empty;
        public bool AssetIncrementFlag { get; set; }
        public bool JrngrpCode { get; set; }
        public bool DeptMode { get; set; }
        public decimal LocalRate { get; set; }
        public decimal BaseRate { get; set; }
        public decimal BaseXRate { get; set; }
        public string FrDeptCode { get; set; } = string.Empty;
        public string FrTransactionCode { get; set; } = string.Empty;
        public string FrReferenceNo { get; set; } = string.Empty;
        public string FrModule { get; set; } = string.Empty;
        public string DocumentDate { get; set; } = string.Empty;
        public string SupplierId { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;
        //public decimal YearDeprPct { get; set; }
        //public decimal UsefulYears { get; set; }
        //public decimal UsefulMonths { get; set; }
        public string DefaultAssetDeptCode { get; set; } = string.Empty;
        public bool GLLink { get; set; }
        public string GLLinkDate { get; set; } = string.Empty;
        public decimal NLGLinkVal { get; set; }
        public string SoftPeriod { get; set; } = string.Empty;

        // Button enablement properties (based on PCMODE and PCSTATUS)
        // NET4: btnImportPJ.Enabled = PCMODE = "T" And PCSTATUS = "00" And PCFR_MODULE = "PJ"
        // NET4: btnImportExist.Enabled = PCMODE = "T" And PCSTATUS = "00" And PCFR_MODULE = "FA"
        // NET4: btnEditAllocExpense.Enabled = PCMODE = "T" And PCSTATUS = "00"
        public bool EnableImportPJ => Mode == "T" && Status == "00" && FrModule == "PJ";
        public bool EnableImportExisting => Mode == "T" && Status == "00" && FrModule == "FA";
        public bool EnableEditAllocExpense => Mode == "T" && Status == "00";

        #region Display Properties

        /// <summary>
        /// Format transaction date for display (from yyyyMMdd to dd-MMM-yyyy)
        /// </summary>
        public string RefDateDisplay { get; set; } = string.Empty;

        /// <summary>
        /// Format local currency rate display
        /// </summary>
        public string LocalCurrencyRateDisplay
        {
            get
            {
                try
                {
                    if (TransDetailData == null)
                        return string.Empty;

                    //if (TransDetailData.NLBASE_RATE <= 0 ||
                    //    TransDetailData.NLCURRENCY_RATE <= 0)
                    //    return string.Empty;

                    if (string.IsNullOrWhiteSpace(TransDetailData.CCURRENCY_CODE) ||
                        string.IsNullOrWhiteSpace(LocalCurrencyCode))
                        return string.Empty;

                    return $"{TransDetailData.NLBASE_RATE} {TransDetailData.CCURRENCY_CODE} = {TransDetailData.NLCURRENCY_RATE:N2} {LocalCurrencyCode}";

                    return string.Empty;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Format base currency rate display
        /// </summary>
        public string BaseCurrencyRateDisplay
        {
            get
            {
                try
                {
                    if (TransDetailData == null)
                        return string.Empty;

                    //if (TransDetailData.NBBASE_RATE <= 0 ||
                    //    TransDetailData.NBCURRENCY_RATE <= 0)
                    //    return string.Empty;

                    if (string.IsNullOrWhiteSpace(TransDetailData.CCURRENCY_CODE) ||
                        string.IsNullOrWhiteSpace(BaseCurrencyCode))
                        return string.Empty;

                    return $"{TransDetailData.NBBASE_RATE} {TransDetailData.CCURRENCY_CODE} = {TransDetailData.NBCURRENCY_RATE:N2} {BaseCurrencyCode}";
                    return string.Empty;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        public object R_Utility { get; private set; }

        #endregion

        #region CRUD Methods

        /// <summary>
        /// Get record for conductor
        /// </summary>
        public async Task GetRecordAsync(FAT0010002DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new R_ServiceGetRecordParameterDTO<FAT0010002DTO>
                {
                    Entity = poEntity
                };

                var loResult = await _model.R_ServiceGetRecord(loParam);
                CurrentRecord = loResult.data;

                // Store properties from result
                if (CurrentRecord != null)
                {
                    //YearDeprPct = CurrentRecord.NYEAR_DEPR_PCT;
                    //UsefulYears = CurrentRecord.IUSEFUL_LIVE_YR;
                    //UsefulMonths = CurrentRecord.IUSEFUL_LIVE_MO;
                    if (!string.IsNullOrWhiteSpace(CurrentRecord.CTRANSACTION_DESCR))
                    {
                        CurrentRecord.CDESCRIPTION = CurrentRecord.CTRANSACTION_DESCR;
                    }
                    CurrentRecord.DSTART_DATE = R_FrontUtility.R_ConvertToDateTime(CurrentRecord.CSTART_DATE, "yyyyMMdd");
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
        public async Task SaveRecordAsync(FAT0010002DTO poEntity, eCRUDMode peCRUDMode, string pcCompanyId, string pcLangId)
        {
            var loEx = new R_Exception();

            try
            {
                // Set standard properties
                poEntity.CCOMPANY_ID = pcCompanyId;
                poEntity.CLANG_ID = pcLangId;

                var loParam = new R_ServiceSaveParameterDTO<FAT0010002DTO>
                {
                    Entity = poEntity,
                    CRUDMode = peCRUDMode
                };

                var loResult = await _model.R_ServiceSave(loParam);
                CurrentRecord = loResult.data;
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
        public async Task DeleteRecordAsync(FAT0010002DTO poEntity, string pcCompanyId, string pcLangId)
        {
            var loEx = new R_Exception();

            try
            {
                // Set standard properties
                poEntity.CCOMPANY_ID = pcCompanyId;
                poEntity.CLANG_ID = pcLangId;

                var loParam = new R_ServiceDeleteParameterDTO<FAT0010002DTO>
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

        #region Non-Streaming Methods

        /// <summary>
        /// Get FA acquisition detail header
        /// </summary>
        //public async Task GetFAAcquisitionDetailHeaderAsync(string pcCompanyId, string pcLangId, FAT0010002DTO poParam)
        //{
        //    var loEx = new R_Exception();

        //    try
        //    {
        //        var loParam = new FAT0010002GetFAAcquisitionDetailHeaderParameterDTO
        //        {
        //            CCOMPANY_ID = pcCompanyId,
        //            CLANG_ID = pcLangId,
        //            CDEPT_CODE = poParam.CDEPT_CODE,
        //            CTRANSACTION_CODE = poParam.CTRANSACTION_CODE,
        //            CREFERENCE_NO = poParam.CREFERENCE_NO
        //        };

        //        var loResult = await _model.GetFAAcquisitionDetailHeader(loParam);
        //        HeaderData = loResult.Data;

        //        // Store properties from result
        //        if (HeaderData != null)
        //        {
        //            TransactionCode = HeaderData.CTRANSACTION_CODE;
        //            ReferenceNo = HeaderData.CREFERENCE_NO;
        //            DeptCode = HeaderData.CDEPT_CODE;
        //            LocalRate = HeaderData.NLRATE;
        //            BaseRate = HeaderData.NBRATE;
        //            BaseXRate = HeaderData.NBXRATE;
        //            FrDeptCode = HeaderData.CFR_DEPT_CODE;
        //            FrTransactionCode = HeaderData.CFR_TRANSACTION_CODE;
        //            FrReferenceNo = HeaderData.CFR_REFERENCE_NO;
        //            FrModule = HeaderData.CFR_MODULE;
        //            DocumentDate = HeaderData.CDOCUMENT_DATE;
        //            SupplierId = HeaderData.CSUPPLIER_ID;
        //            SupplierName = HeaderData.CSUPPLIER_NAME;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    loEx.ThrowExceptionIfErrors();
        //}

        /// <summary>
        /// Get declining depreciation amount
        /// </summary>
        public async Task<decimal> GetDecliningDeprAmtAsync(string pcCompanyId, string pcLangId, FAT0010002GetDecliningDeprAmtParameterDTO poParam)
        {
            var loEx = new R_Exception();
            decimal lnResult = 0;

            try
            {
                poParam.CCOMPANY_ID = pcCompanyId;
                poParam.CLANG_ID = pcLangId;

                var loResult = await _model.GetDecliningDeprAmt(poParam);
                lnResult = loResult.Data.DeprAmt;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return lnResult;
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
        //        var loParam = new FAT0010002ValidateDeptCodeParameterDTO
        //        {
        //            CCOMPANY_ID = pcCompanyId,
        //            CDEPT_CODE = pcDeptCode,
        //            CUSER_ID = pcUserId
        //        };

        //        var loRtn = await _model.ValidateDeptCode(loParam);
        //        liResult = loRtn.Data.Result;
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    loEx.ThrowExceptionIfErrors();
        //    return liResult;
        //}

        /// <summary>
        /// Get transaction detail
        /// </summary>
        public async Task GetTransDetailAsync(string pcCompanyId, string pcLangId, string pcRecId, string pcDeptCode, string pcRefNo)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new FAT0010002GetTransDetailParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CLANGUAGE_ID = pcLangId,
                    CREC_ID = pcRecId,
                    CDEPT_CODE = pcDeptCode,
                    CREF_NO = pcRefNo
                };

                var loResult = await _model.FAT0010002GetTransDetail(loParam);
                if (loResult != null)
                {
                    TransDetailData = loResult.Data ?? new FAT0010002GetTransDetailResultDTO();
                    RefDateDisplay = DateTime.ParseExact(TransDetailData.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");

                    
                    ReferenceNo = TransDetailData.CREF_NO;
                    DeptCode = HeaderData.CDEPT_CODE;
                    //LocalRate = HeaderData.NLRATE;
                    //BaseRate = HeaderData.NBRATE;
                    //BaseXRate = HeaderData.NBXRATE;
                    //FrDeptCode = HeaderData.CFR_DEPT_CODE;
                    //FrTransactionCode = HeaderData.CFR_TRANSACTION_CODE;
                    //FrReferenceNo = HeaderData.CFR_REFERENCE_NO;
                    //FrModule = HeaderData.CFR_MODULE;
                    //DocumentDate = HeaderData.CDOCUMENT_DATE;
                    //SupplierId = HeaderData.CSUPPLIER_ID;
                    //SupplierName = HeaderData.CSUPPLIER_NAME;
                }
                
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
        /// Get combo depreciation method - streaming method
        /// </summary>
        public async Task GetComboDepreciationMethodAsync(string pcCompanyId, string pcLangId)
        {
            var loEx = new R_Exception();

            try
            {
                // No custom parameters needed for this streaming method (only CCOMPANY_ID, CLANG_ID which are handled automatically)
                var loResult = await _model.GetComboDepreciationMethodAsync();
                ComboDepreciationMethodList = new ObservableCollection<FAT00100GetStatusListResultDTO>(loResult.Data ?? new List<FAT00100GetStatusListResultDTO>());
                if (ComboDepreciationMethodList.Count > 0)  
                {
                    ComboDepreciationMethodFirstItem = ComboDepreciationMethodList[0];
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get FA acquisition detail asset list - streaming method for asset grid
        /// </summary>
        public async Task GetFAAcquisitionDetailAssetListAsync(string pcCompanyId, string pcLangId, string pcDeptCode, string pcTransactionCode, string pcReferenceNo, string pcRecid, string pcStatus, DateTime pdUpdateDate)
        {
            var loEx = new R_Exception();

            try
            {
                // Set streaming context for custom parameters (NOT CCOMPANY_ID, CLANG_ID)
                // CFOREIGN_LANGUAGE is required by backend controller
                R_FrontContext.R_SetStreamingContext(ContextConstants.CFOREIGN_LANGUAGE, pcLangId);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CDEPT_CODE, pcDeptCode);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CTRANSACTION_CODE, pcTransactionCode);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CREFERENCE_NO, pcReferenceNo);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CSTATUS, pcStatus);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CREC_ID, pcRecid);

                var loResult = await _model.GetFAAcquisitionDetailAssetListAsync();
                AssetList = new ObservableCollection<FAT0010002GetFAAcquisitionDetailAssetListResultDTO>(loResult.Data ?? new List<FAT0010002GetFAAcquisitionDetailAssetListResultDTO>());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get FA acquisition detail alloc expen page list - streaming method for allocation expense grid
        /// </summary>
        public async Task GetFAAcquisitionDetailAllocExpenPageListAsync(string pcCompanyId, string pcLangId, string pcDeptCode, string pcTransactionCode, string pcReferenceNo, string pcAssetCode, string pcAssetTransSeqNo)
        {
            var loEx = new R_Exception();

            try
            {
                // Set streaming context for custom parameters (NOT CCOMPANY_ID, CLANG_ID)
                R_FrontContext.R_SetStreamingContext(ContextConstants.CDEPT_CODE, pcDeptCode);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CTRANSACTION_CODE, pcTransactionCode);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CREFERENCE_NO, pcReferenceNo);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CASSET_CODE, pcAssetCode);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CASSET_TRANS_SEQNO, pcAssetTransSeqNo);

                var loResult = await _model.GetFAAcquisitionDetailAllocExpenPageListAsync();
                AllocExpenPageList = new ObservableCollection<FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO>(loResult.Data ?? new List<FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO>());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get transaction expense allocation list - streaming method
        /// </summary>
        public async Task FAT00100GetTransExpAllocListAsync(string pcCompanyId, string pcLangId, string pcParentId, string pcDeptCode, string pcTransCode, string pcRefNo, string pcAssetCode, string pcAssetTransSeqNo)
        {
            var loEx = new R_Exception();

            try
            {
                // Set streaming context for custom parameters (NOT CCOMPANY_ID, CLANG_ID)
                R_FrontContext.R_SetStreamingContext(ContextConstants.CPARENT_ID, pcParentId);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CDEPT_CODE, pcDeptCode);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CTRANS_CODE, pcTransCode);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CREF_NO, pcRefNo);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CASSET_CODE, pcAssetCode);
                R_FrontContext.R_SetStreamingContext(ContextConstants.CASSET_TRANS_SEQ_NO, pcAssetTransSeqNo);

                var loResult = await _model.FAT00100GetTransExpAllocListAsync();
                TransExpAllocList = new ObservableCollection<FAT00100GetTransExpAllocListResultDTO>(loResult.Data ?? new List<FAT00100GetTransExpAllocListResultDTO>());
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
        public async Task<R_Exception> ValidateRecordAsync(
            FAT0010002DTO poEntity,
            eCRUDMode peCRUDMode,
            string pcCompanyId,
            string pcLangId,
            bool plAssetIncrementFlag,
            string pcAssetName,
            string pcAssetCategoryCode,
            string pcAssetDepartmentCode,
            string pcAssetJournalGroupCode,
            string pcAssetTaxCategoryCode,
            decimal pnQuantity,
            string pcUnit,
            DateTime? pdInServiceDate,
            DateTime? pdTransactionDate,
            string pcDepreciationMethod,
            DateTime? pdStartDate,
            decimal pnInitialCostAmnt,
            decimal pnUserfulLifeYears,
            decimal pnUserfulLifeMonths,
            decimal pnYearlyDepreciation,
            decimal pnLocalBegBookVal,
            decimal pnBookValueLocalAmnt,
            decimal pnRemUsefulLifeYr,
            decimal pnRemUsefulLifeMo,
            bool plNew,
            string pcAssetCode)
        {
            var loEx = new R_Exception();

            try
            {
                // Validate asset name
                if (string.IsNullOrWhiteSpace(pcAssetName))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS020"));
                }

                // Validate asset category code
                if (string.IsNullOrWhiteSpace(pcAssetCategoryCode))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS021"));
                }

                // Validate asset department code
                if (string.IsNullOrWhiteSpace(pcAssetDepartmentCode))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS022"));
                }

                // Validate asset journal group code
                if (string.IsNullOrWhiteSpace(pcAssetJournalGroupCode))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS023"));
                }

                // Validate asset tax category code
                if (string.IsNullOrWhiteSpace(pcAssetTaxCategoryCode))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS024"));
                }

                // Validate quantity
                if (pnQuantity == 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS025"));
                }

                // Validate unit
                if (string.IsNullOrWhiteSpace(pcUnit))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS026"));
                }

                // Validate in service date
                if (pdInServiceDate == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS027"));
                }

                // Validate in service date not less than transaction date
                if (pdInServiceDate != null && pdTransactionDate != null)
                {
                    string lcInServiceDateStr = pdInServiceDate.Value.ToString("yyyyMMdd");
                    string lcTransactionDateStr = pdTransactionDate.Value.ToString("yyyyMMdd");
                    if (!plNew && lcInServiceDateStr.CompareTo(lcTransactionDateStr) < 0)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS028"));
                    }
                }

                // Validate start date if depreciation method is not "0"
                if (pcDepreciationMethod != "0")
                {
                    if (pdStartDate == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS029"));
                    }
                    else if (pdInServiceDate != null && pdTransactionDate != null)
                    {
                        string lcStartDateStr = pdStartDate.Value.ToString("yyyyMMdd");
                        string lcInServiceDateStr = pdInServiceDate.Value.ToString("yyyyMMdd");
                        string lcTransactionDateStr = pdTransactionDate.Value.ToString("yyyyMMdd");
                        if (lcStartDateStr.CompareTo(lcInServiceDateStr) < 0 || lcStartDateStr.CompareTo(lcTransactionDateStr) < 0)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS030"));
                        }
                    }
                }

                // Validate initial cost amount
                if (pnInitialCostAmnt == 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS031"));
                }

                // Validate useful life if depreciation method is not "0"
                if (pcDepreciationMethod != "0" && pnUserfulLifeYears == 0 && pnUserfulLifeMonths == 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS032"));
                }

                // Validate declining balance depreciation method (method "3")
                if (pcDepreciationMethod == "3")
                {
                    if (pnUserfulLifeYears < 2)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS041"));
                    }
                    if (pnYearlyDepreciation < 0 || pnYearlyDepreciation > 100)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS042"));
                    }
                }
                else
                {
                    if (pnYearlyDepreciation < 0 || pnYearlyDepreciation > 1200)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS042"));
                    }
                }

                // Validate beginning book value (CR21)
                if (pcDepreciationMethod != "0" && pnLocalBegBookVal < pnBookValueLocalAmnt)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS043"));
                }

                // Validate remaining useful life not greater than useful life
                if ((pnUserfulLifeYears * 12) + pnUserfulLifeMonths < (pnRemUsefulLifeYr * 12) + pnRemUsefulLifeMo)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS044"));
                }

                // Validate remaining useful life months (0-11)
                if (pnRemUsefulLifeMo < 0 || pnRemUsefulLifeMo > 11)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS045"));
                }

                // Validate asset code in Add mode if increment flag is false
                if (peCRUDMode == eCRUDMode.AddMode)
                {
                    if (!plAssetIncrementFlag)
                    {
                        if (string.IsNullOrWhiteSpace(pcAssetCode))
                        {
                            loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS033"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            return loEx;
        }

        #endregion
    }
}

