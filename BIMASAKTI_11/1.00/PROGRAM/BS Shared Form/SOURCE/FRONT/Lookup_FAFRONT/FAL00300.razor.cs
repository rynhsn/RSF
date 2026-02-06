using Lookup_FACommon.DTOs;
using Lookup_FAModel.ViewModel.FAL00200;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace Lookup_FAFront
{
    public partial class FAL00300 : R_Page
    {
        private LookupFAL00300ViewModel _viewModel = new LookupFAL00300ViewModel();
        private R_Grid<FAL00300DTO> GridRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await GridRef.R_RefreshGrid(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var param = R_FrontUtility.ConvertObjectToObject<FAL00300ParameterDTO>(eventArgs.Parameter);
                await _viewModel.GetTaxCategoryList(param);

                eventArgs.ListEntityResult = _viewModel.AssetGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Button_OnClickOkAsync()
        {
            var loData = GridRef.GetCurrentData();
            await this.Close(true, loData);
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }

    }
}
