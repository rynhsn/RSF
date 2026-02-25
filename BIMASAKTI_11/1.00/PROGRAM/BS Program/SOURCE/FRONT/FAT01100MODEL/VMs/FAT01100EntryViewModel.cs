using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using FAT01100Common;
using FAT01100Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace FAT01100Model.VMs
{
    /// <summary>
    /// ViewModel for FAT01100 Entry - Change Asset Data Transaction (CRUD + init/lookup)
    /// Handles UI state and data binding for transaction management
    /// </summary>
    public class FAT01100EntryViewModel : R_ViewModel<FAT01100DTO>
    {
        private readonly FAT01100EntryModel _model = new FAT01100EntryModel();

        /// <summary>
        /// Main entity for R_Conductor synchronization (keep in sync with base Data via R_SetCurrentData)
        /// </summary>
        public FAT01100DTO Entity { get; set; } = new FAT01100DTO();


        public FAT01100EntryViewModel()
        {
            R_SetCurrentData(new FAT01100DTO());
        }

        // Initial process result data (from Model)
        public FAT01100GetCompanyInfoResultDTO CompanyInfoData { get; set; } = new FAT01100GetCompanyInfoResultDTO();
        public FAT01100GetGetSystemParamResultDTO SystemParamData { get; set; } = new FAT01100GetGetSystemParamResultDTO();
        public FAT01100GetPeriodeDtInfoResultDTO PeriodeDtInfoData { get; set; } = new FAT01100GetPeriodeDtInfoResultDTO();
        public FAT01100GetTransCodeInfoResultDTO TransCodeInfoData { get; set; } = new FAT01100GetTransCodeInfoResultDTO();
        public FAT01100GetYearRangeResultDTO YearRangeData { get; set; } = new FAT01100GetYearRangeResultDTO();
        public FAT01100GetLastCurrencyRateResultDTO LastCurrencyRateData { get; set; } = new FAT01100GetLastCurrencyRateResultDTO();
        public FAT01100GetAssetResultDTO GetAssetData { get; set; } = new FAT01100GetAssetResultDTO();

        // List bindings
        public ObservableCollection<FAT01100GetCurrencyListResultDTO> CurrencyList { get; set; } = new ObservableCollection<FAT01100GetCurrencyListResultDTO>();
        public ObservableCollection<FAT01100GetDeptLookupListResultDTO> DeptLookupList { get; set; } = new ObservableCollection<FAT01100GetDeptLookupListResultDTO>();

        public ObservableCollection<FAT01100GetGsbCodeListResultDTO> GsbCodeList { get; set; } = new ObservableCollection<FAT01100GetGsbCodeListResultDTO>();


        // Mode / flags
        public bool LenableEdit { get; set; }

        #region CRUD Methods

        /// <summary>
        /// Get single transaction record
        /// </summary>
        /// <param name="poEntity">Transaction entity to retrieve</param>
        public async Task GetRecordAsync(FAT01100DTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new R_ServiceGetRecordParameterDTO<FAT01100DTO>
                {
                    Entity = poEntity
                };

                var loResult = await _model.R_ServiceGetRecord(loParam);
                Entity = loResult.data ?? new FAT01100DTO();
                //Entity.DINSERVICE_DATE = DateTime.ParseExact(Entity.CINSERVICE_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                //Entity.DREF_DATE = DateTime.ParseExact(Entity.CREF_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

                if (!string.IsNullOrWhiteSpace(Entity.CINSERVICE_DATE) &&
                    DateTime.TryParseExact( 
                    Entity.CINSERVICE_DATE,
                   "yyyyMMdd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime inServiceDate))
                {
                    Entity.DINSERVICE_DATE = inServiceDate;
                }
                else
                {
                    Entity.DINSERVICE_DATE = null; // or throw your custom exception
                }

                if (!string.IsNullOrWhiteSpace(Entity.CREF_DATE) &&
                    DateTime.TryParseExact(
                        Entity.CREF_DATE,
                        "yyyyMMdd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime refDate))
                {
                    Entity.DREF_DATE = refDate;
                }
                else
                {
                    Entity.DREF_DATE = null;
                }

                if (!string.IsNullOrWhiteSpace(Entity.CSTART_DATE) &&
                    DateTime.TryParseExact(
                        Entity.CSTART_DATE,
                        "yyyyMMdd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime startDate))
                {
                    Entity.DSTART_DATE = startDate;
                }
                else
                {
                    Entity.DSTART_DATE = null;
                }

                if (!string.IsNullOrWhiteSpace(Entity.CSTART_DATE_OLD) &&
                    DateTime.TryParseExact(
                        Entity.CSTART_DATE_OLD,
                        "yyyyMMdd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime startDateOld))
                {
                    Entity.DSTART_DATE_OLD = startDateOld;
                }
                else
                {
                    Entity.DSTART_DATE_OLD = null;
                }






                R_SetCurrentData(Entity);
                LenableEdit = R_IsStatusEditable(Entity.CTRANS_STATUS);
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
        public async Task SaveRecordAsync(FAT01100DTO poEntity, R_eConductorMode peCRUDMode)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new R_ServiceSaveParameterDTO<FAT01100DTO>
                {
                    Entity = poEntity,
                    CRUDMode = (eCRUDMode)peCRUDMode
                };

                var loResult = await _model.R_ServiceSave(loParam);
                Entity = loResult.data ?? new FAT01100DTO();
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
        public async Task DeleteRecordAsync(FAT01100DTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new R_ServiceDeleteParameterDTO<FAT01100DTO>
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

        public async Task FAT01100GetAsset(string pcCompanyId, string pcAssetCode, string pcLangId)
        {
            var loEx = new R_Exception();
            try
            {
                var loGetAssetParam = new FAT01100GetAssetParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CASSET_CODE=pcAssetCode,
                    CLANGUAGE_ID=pcLangId
                };
                var loGetAssetResult = await _model.FAT01100GetAsset(loGetAssetParam);
                GetAssetData = loGetAssetResult.Data ?? new FAT01100GetAssetResultDTO();
                //Entity = R_FrontUtility.ConvertObjectToObject<FAT01100DTO>(GetAssetData);
                

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Initial Process

        /// <summary>
        /// Initial process: load company info, system param, periode dt info, trans code info, year range
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcUserId">User ID</param>
        /// <param name="pcLangId">Language/Culture ID</param>
        public async Task GetInitialProcessAsync(string pcCompanyId, string pcUserId, string pcLangId)
        {
            var loEx = new R_Exception();
            try
            {
                var loCompanyParam = new FAT01100GetCompanyInfoParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId
                };
                var loCompanyResult = await _model.FAT01100GetCompanyInfo(loCompanyParam);
                CompanyInfoData = loCompanyResult.Data ?? new FAT01100GetCompanyInfoResultDTO();

                var loSystemParam = new FAT01100GetGetSystemParamParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CLANGUAGE_ID = pcLangId
                };
                var loSystemResult = await _model.FAT01100GetGetSystemParam(loSystemParam);
                SystemParamData = loSystemResult.Data ?? new FAT01100GetGetSystemParamResultDTO();

                var loPeriodeParam = new FAT01100GetPeriodeDtInfoParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CYEAR = string.Empty,
                    CPERIOD_NO = string.Empty
                };
                var loPeriodeResult = await _model.FAT01100GetPeriodeDtInfo(loPeriodeParam);
                PeriodeDtInfoData = loPeriodeResult.Data ?? new FAT01100GetPeriodeDtInfoResultDTO();

                var loTransCodeParam = new FAT01100GetTransCodeInfoParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CTRANS_CODE = string.Empty
                };
                var loTransCodeResult = await _model.FAT01100GetTransCodeInfo(loTransCodeParam);
                TransCodeInfoData = loTransCodeResult.Data ?? new FAT01100GetTransCodeInfoResultDTO();

                var loYearRangeParam = new FAT01100GetYearRangeParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CCYEAR = string.Empty,
                    CMODE = string.Empty
                };
                var loYearRangeResult = await _model.FAT01100GetYearRange(loYearRangeParam);
                YearRangeData = loYearRangeResult.Data ?? new FAT01100GetYearRangeResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Lookup / Helper Methods

        /// <summary>
        /// Get currency list and assign to CurrencyList
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcUserId">User ID</param>
        /// <param name="pcLangId">Language ID</param>
        public async Task GetCurrencyListAsync(string pcCompanyId, string pcUserId, string pcLangId)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new FAT01100GetCurrencyListParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CUSER_ID = pcUserId,
                    CLANG_ID = pcLangId
                };
                var loResult = await _model.GetCurrencyList(loParam);
                CurrencyList = new ObservableCollection<FAT01100GetCurrencyListResultDTO>(loResult.Data ?? new List<FAT01100GetCurrencyListResultDTO>());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get department lookup list and assign to DeptLookupList
        /// </summary>
        /// <param name="pcCompanyId">Company ID</param>
        /// <param name="pcUserId">User ID</param>
        public async Task GetDeptLookupListAsync(string pcCompanyId, string pcUserId)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new FAT01100GetDeptLookupListParameterDTO
                {
                    CCOMPANY_ID = pcCompanyId,
                    CUSER_ID = pcUserId
                };
                var loResult = await _model.FAT01100GetDeptLookupList(loParam);
                DeptLookupList = new ObservableCollection<FAT01100GetDeptLookupListResultDTO>(loResult.Data ?? new List<FAT01100GetDeptLookupListResultDTO>());
                var loFoundDept = DeptLookupList != null ? System.Linq.Enumerable.ToList(DeptLookupList).Find(x => x.CDEPT_CODE == SystemParamData.CTRANS_DEPT_CODE) : null;
                if (loFoundDept != null)
                {
                    Entity.CDEPT_CODE = loFoundDept.CDEPT_CODE;
                    Entity.CDEPT_NAME = loFoundDept.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Get last currency rate for the given currency, rate type and date
        /// </summary>
        /// <param name="poParam">Parameter (CCOMPANY_ID, CCURRENCY_CODE, CRATETYPE_CODE, CRATE_DATE, etc.)</param>
        public async Task GetLastCurrencyRateAsync(FAT01100GetLastCurrencyRateParameterDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _model.FAT01100GetLastCurrencyRate(poParam);
                LastCurrencyRateData = loResult.Data ?? new FAT01100GetLastCurrencyRateResultDTO();
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
        /// <param name="poParam">Parameter (CCOMPANY_ID, CUSER_ID, CREC_ID, CNEW_STATUS)</param>
        public async Task UpdateTransHdStatusAsync(FAT01100UpdateTransHdStatusParameterDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                await _model.FAT01100UpdateTransHdStatus(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Submit transaction
        /// </summary>
        /// <param name="poParam">Parameter (CCOMPANY_ID, CUSER_ID, CREC_ID)</param>
        public async Task SubmitTransAsync(FAT01100SubmitTransParameterDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                await _model.FAT01100SubmitTrans(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        // call FAT01100GetGsbCodeListAsync from model
        public async Task GetGsbCodeListAsync()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _model.FAT01100GetGsbCodeListAsync();
                GsbCodeList = new ObservableCollection<FAT01100GetGsbCodeListResultDTO>(loResult.Data ?? new List<FAT01100GetGsbCodeListResultDTO>());
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
    }
}
