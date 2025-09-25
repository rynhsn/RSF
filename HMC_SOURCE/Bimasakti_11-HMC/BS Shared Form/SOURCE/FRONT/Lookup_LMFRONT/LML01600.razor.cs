using Lookup_PMCOMMON.DTOs.LML01500;
using Lookup_PMCOMMON.DTOs.LML01600;
using Lookup_PMModel.ViewModel.LML01500;
using Lookup_PMModel.ViewModel.LML01600;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;


namespace Lookup_PMFRONT
{
    public partial class LML01600 : R_Page
    {
        private LookupLML01600ViewModel _viewModel = new LookupLML01600ViewModel();
        private R_Grid<LML01600DTO> GridRef;
        private int _pageSize = 12;

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
                var loParam = (LML01600ParameterDTO)eventArgs.Parameter;
                await _viewModel.GetSLACallTypeList(loParam);
                eventArgs.ListEntityResult = _viewModel.GetList;
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
