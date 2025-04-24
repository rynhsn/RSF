using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_GSFRONT
{
    public partial class GSL03500 : R_Page
    {
        private LookupGSL03500ViewModel _viewModel = new LookupGSL03500ViewModel();
        private R_Grid<GSL03500DTO> GridRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSL03500ParameterDTO)poParameter;
                _viewModel.WarehouseParameter = loParam;

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
                await _viewModel.GetWarehouseList();

                eventArgs.ListEntityResult = _viewModel.WarehouseGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnChangeInactiveWarehouse(bool poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.LACTIVE_CHECKBOX = poParam;
                if (poParam)
                {
                    _viewModel.WarehouseParameter.CACTIVE_TYPE = "ALL";
                }
                else
                {
                    _viewModel.WarehouseParameter.CACTIVE_TYPE = "ACTIVE";
                }

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
