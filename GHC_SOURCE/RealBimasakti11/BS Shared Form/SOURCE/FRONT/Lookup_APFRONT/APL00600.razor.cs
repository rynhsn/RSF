using Lookup_APCOMMON.DTOs.APL00600;
using Lookup_APModel.ViewModel.APL00600;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace Lookup_APFRONT;

public partial class APL00600 :  R_Page
    {
        private LookupAPL00600ViewModel _viewModel = new LookupAPL00600ViewModel();
        private R_Grid<APL00600DTO> GridRef;
        private R_Grid<APL00600DTO> GridRefInvoice;
        private R_ConductorGrid _conductorGridRef;
        private R_ConductorGrid _conductorGridRefInvoice;
        private R_Conductor _conductorRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.ParameterLookup = (APL00600ParameterDTO)poParameter;

              await  GridRef.R_RefreshGrid(null);
              if (_viewModel.SchedulePaymentGrid.Count == 0)
              {
                  await R_MessageBox.Show("Error", @_localizer["DATA_NOT_FOUND"], R_eMessageBoxButtonType.OK);
                  return;
              }
          
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }

        public async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                await _viewModel.GetSchedulePaymentList();
                eventArgs.ListEntityResult = _viewModel.SchedulePaymentGrid;


            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        public async Task R_ServiceGetListRecorInvoiceAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                await _viewModel.GetInvoiceGridLIst();
                eventArgs.ListEntityResult = _viewModel.InvoiceListGrid;


            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        public async Task Button_OnClickOkAsync()
        {
            if (_viewModel.SchedulePaymentGrid.Count == 0)
            {
                await R_MessageBox.Show("Error", @_localizer["DATA_NOT_FOUND"], R_eMessageBoxButtonType.OK);
                return;
            }
            else
            {
                var loData = GridRef.GetCurrentData();
                await this.Close(true, loData);
            }
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }
        public async Task Refresh_Button()
        {
            var loEx = new R_Exception();

            try
            {
             await  GridRef.R_RefreshGrid(null);
                
                if (_viewModel.SchedulePaymentGrid.Count == 0)
                {
                    await R_MessageBox.Show("Error", @_localizer["DATA_NOT_FOUND"], R_eMessageBoxButtonType.OK);
                    return;
                }
                if (_viewModel.InvoiceListGrid.Count == 0)
                {
                    await R_MessageBox.Show("Error", @_localizer["DATA_INVOICE_NOT_FOUND"], R_eMessageBoxButtonType.OK);
                    return;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task R_DisplayAPL00600(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<APL00600DTO>(eventArgs.Data);

                    _viewModel.ParameterLookup.CREC_ID = loParam.CREC_ID;
                    await  GridRefInvoice.R_RefreshGrid(null);

                }
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
    