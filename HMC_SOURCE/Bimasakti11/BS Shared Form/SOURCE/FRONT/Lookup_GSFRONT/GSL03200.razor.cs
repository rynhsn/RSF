using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace Lookup_GSFRONT
{
    public partial class GSL03200 : R_Page
    {
        private LookupGSL03200ViewModel _viewModel = new LookupGSL03200ViewModel();
        private R_Grid<GSL03200DTO> GridRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.ProductAllocationParameter = (GSL03200ParameterDTO)poParameter;
                await GridRef.R_RefreshGrid(null);
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
                await _viewModel.GetProductAllocationList();

                eventArgs.ListEntityResult = _viewModel.ProductAllocationGrid;

                if (_viewModel.ProductAllocationGrid.Count <= 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                           typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                           "N03"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task OnClickRefresh()
        {
            var loEx = new R_Exception();

            try
            {
                await GridRef.R_RefreshGrid(null);
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
