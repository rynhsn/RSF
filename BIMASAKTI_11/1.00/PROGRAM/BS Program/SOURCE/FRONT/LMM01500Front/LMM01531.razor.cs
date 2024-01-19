using LMM01500COMMON;
using LMM01500MODEL;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace LMM01500FRONT
{
    public partial class LMM01531 : R_Page
    {
        private LMM01531ViewModel _viewModel = new LMM01531ViewModel();
        private R_Grid<LMM01531DTO> _gridRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _gridRef.R_RefreshGrid(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task Lookup_R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (LMM01531DTO)eventArgs.Parameter;
                await _viewModel.GetLookupOtherCharges(loParam);

                eventArgs.ListEntityResult = _viewModel.LookupOtherCharges;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Button_OnClickOkAsync()
        {
            var loData = _gridRef.GetCurrentData();
            await this.Close(true, loData);
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }

    }
}
