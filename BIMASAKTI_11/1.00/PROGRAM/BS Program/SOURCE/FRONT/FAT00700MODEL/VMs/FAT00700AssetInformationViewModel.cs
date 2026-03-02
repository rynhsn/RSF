using FAT00700Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Model = FAT00700Model.FAT00700Model;

namespace FAT00700Model.VMs
{
    /// <summary>
    /// ViewModel for FAT00700 Asset Information page
    /// Handles asset information display and expense allocation grid
    /// </summary>
    public class FAT00700AssetInformationViewModel : R_ViewModel<FAT00700DTO>
    {
        private readonly Model _fat00700Model = new Model();

        // Main entity (read-only for asset information display)
        public FAT00700DTO CurrentRecord { get; set; } = new FAT00700DTO();

        // Grid data for expense allocation
        public ObservableCollection<GetGridAllocDataResultDTO> GridAllocList { get; set; } = new ObservableCollection<GetGridAllocDataResultDTO>();

        // Result DTOs needed for asset information
        public GetCurrencyResultDTO CurrencyResult { get; set; } = new GetCurrencyResultDTO();
        public GetAssetInformationResultDTO AssetInformationResult { get; set; } = new GetAssetInformationResultDTO();
        public GetAssetInfoDataResultDTO AssetInfoDataResult { get; set; } = new GetAssetInfoDataResultDTO();

        #region UI Display Properties

        // Asset information display properties
        public string AssetCode => CurrentRecord?.CASSET_CODE ?? string.Empty;
        public string AssetName => CurrentRecord?.CASSET_NAME ?? string.Empty;
        public string AssetDepartmentCode => CurrentRecord?.CASSET_DEPT_CODE ?? string.Empty;
        public string AssetDepartmentName => CurrentRecord?.CASSET_DEPT_NAME ?? string.Empty;

        // Currency codes
        public string LocalCurrencyCode => CurrencyResult?.CLOCAL_CURRENCY_CODE ?? string.Empty;
        public string BaseCurrencyCode => CurrencyResult?.CBASE_CURRENCY_CODE ?? string.Empty;

        // Numeric properties for display
        public int Quantity => CurrentRecord?.IQTY ?? 0;
        public string Unit => CurrentRecord?.CUNIT ?? string.Empty;

        // Depreciation amounts
        public decimal LocalBookValue => CurrentRecord?.NLBOOK_VALUE ?? 0;
        public decimal BaseBookValue => CurrentRecord?.NBBOOK_VALUE ?? 0;

        #endregion

        #region Business Methods

        /// <summary>
        /// Initialize asset information data
        /// </summary>
        public async Task InitializeAssetInformationAsync(FAT00700DTO transactionData)
        {
            var loEx = new R_Exception();

            try
            {
                // Copy data from main transaction
                CurrentRecord = transactionData ?? new FAT00700DTO();

                // Load additional asset information
                await LoadAssetInformationAsync();
                await LoadCurrencyDataAsync();
                await LoadExpenseAllocationAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /// <summary>
        /// Load detailed asset information
        /// </summary>
        private async Task LoadAssetInformationAsync()
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GetAssetInformationParameterDTO
                {
                    CCOMPANY_ID = CurrentRecord.CCOMPANY_ID,
                    CLANGID = CurrentRecord.CLANG_ID,
                    CUSER_ID = CurrentRecord.CUSER_ID,
                    CASSET_CODE = CurrentRecord.CASSET_CODE
                };

                var loResult = await _fat00700Model.GetAssetInformation(loParam);
                AssetInformationResult = loResult.Data;

                // Also load asset info data
                var loAssetInfoParam = new GetAssetInfoDataParameterDTO
                {
                    CCOMPANY_ID = CurrentRecord.CCOMPANY_ID,
                    CLANGID = CurrentRecord.CLANG_ID,
                    CUSER_ID = CurrentRecord.CUSER_ID,
                    CASSET_CODE = CurrentRecord.CASSET_CODE
                };

                var loAssetInfoResult = await _fat00700Model.GetAssetInfoData(loAssetInfoParam);
                AssetInfoDataResult = loAssetInfoResult.Data;

                // Update CurrentRecord with detailed asset information
                if (AssetInfoDataResult != null)
                {
                    CurrentRecord.CASSET_NAME = AssetInfoDataResult.CASSET_NAME;
                    CurrentRecord.CASSET_DEPT_CODE = AssetInfoDataResult.CASSET_DEPT_CODE;
                    CurrentRecord.CASSET_DEPT_NAME = AssetInfoDataResult.CASSET_DEPT_NAME;
                    CurrentRecord.NLBOOK_VALUE = AssetInfoDataResult.NLBOOK_VALUE;
                    CurrentRecord.NBBOOK_VALUE = AssetInfoDataResult.NBBOOK_VALUE;
                    CurrentRecord.IQTY = AssetInfoDataResult.IQTY;
                    CurrentRecord.CUNIT = AssetInfoDataResult.CUNIT;
                }
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
                    CCOMPANY_ID = CurrentRecord.CCOMPANY_ID,
                    CLANGID = CurrentRecord.CLANG_ID,
                    CUSER_ID = CurrentRecord.CUSER_ID
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

        /// <summary>
        /// Load expense allocation grid data
        /// </summary>
        private async Task LoadExpenseAllocationAsync()
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GetGridAllocDataParameterDTO
                {
                    CCOMPANY_ID = CurrentRecord.CCOMPANY_ID,
                    CLANGID = CurrentRecord.CLANG_ID,
                    CUSER_ID = CurrentRecord.CUSER_ID,
                    CASSET_CODE = CurrentRecord.CASSET_CODE
                };

                var loResult = await _fat00700Model.GetGridAllocData(loParam);

                GridAllocList.Clear();
                if (loResult.Data != null)
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

        /// <summary>
        /// Refresh asset information data
        /// </summary>
        public async Task RefreshAssetInformationAsync()
        {
            await InitializeAssetInformationAsync(CurrentRecord);
        }

        #endregion
    }
}
