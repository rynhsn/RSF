using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace Lookup_GSFRONT
{
    public partial class GSL03400 : R_Page
    {
        private LookupGSL03400ViewModel _viewModel = new LookupGSL03400ViewModel();
        private R_Grid<GSL03400DTO> GridRef;
        private R_Conductor _conductorRef;
        protected override async Task R_Init_From_Master(object poParameter)
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

        public async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetDigitalSignList();

                eventArgs.ListEntityResult = _viewModel.DigitalSignGrid;

                if (_viewModel.DigitalSignGrid.Count <= 0)
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
        public async Task R_ServiceGetRecordAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<GSL03400ParameterDTO>(eventArgs.Data);
                var loResult = await _viewModel.GetDigitalSign(loData);

                eventArgs.Result = loResult;
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
            var loData = _conductorRef.R_GetCurrentData();
            await this.Close(true, loData);
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }
    }
}
