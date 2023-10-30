using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace Lookup_GSFRONT
{
    public partial class GSL01700 : R_Page
    {
        private LookupGSL01700ViewModel _viewModel = new LookupGSL01700ViewModel();
        private R_Grid<GSL01700DTO> GridRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await RateType_ServiceGetListRecordAsync(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task RateType_ServiceGetListRecordAsync(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetRateTypeList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task RateType_OnChange(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.Data.CRATETYPE_CODE = poParam;

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
                var loParam = R_FrontUtility.ConvertObjectToObject<GSL01700DTOParameter>(_viewModel.Data);
                await _viewModel.GetCurrencyRateList(loParam);

                eventArgs.ListEntityResult = _viewModel.CurrencyRateGrid;
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
