using PMM01000COMMON;
using PMM01000MODEL;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace PMM01000FRONT
{
    public partial class PMM01003 : R_Page
    {
        private PMM01003ViewModel _viewModel = new PMM01003ViewModel();
        private R_Conductor _UtilityCharges_conductorRef;

        private R_TextBox NewChargesId;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _UtilityCharges_conductorRef.R_GetEntity(poParameter);

                await NewChargesId.FocusAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void UtilityCharges_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Result = eventArgs.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Button_OnClickOkAsync()
        {
            var loData = (PMM01003DTO)_viewModel.R_GetCurrentData();
            var loEx = new R_Exception();

            try
            {
                await _viewModel.CopyNewCharges(loData);
                await this.Close(true, loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }

    }
}
