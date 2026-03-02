using FAT00700Common.DTOs;
using FAT00700Model.VMs;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;

namespace FAT00700Front
{
    public partial class FAT00700_AssetInformation : R_Page
    {
        private FAT00700AssetInformationViewModel _viewModel = new();
        private R_Grid<GetGridAllocDataResultDTO>? _gridAllocRef;

        protected override async Task R_Init_From_Master(object? poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                // Initialize from parent page parameter (transaction data)
                if (poParameter is FAT00700DTO transactionData)
                {
                    await _viewModel.InitializeAssetInformationAsync(transactionData);
                }
                else
                {
                    // Initialize with empty data
                    await _viewModel.InitializeAssetInformationAsync(new FAT00700DTO());
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
        public async Task RefreshAssetInformation()
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.RefreshAssetInformationAsync();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
