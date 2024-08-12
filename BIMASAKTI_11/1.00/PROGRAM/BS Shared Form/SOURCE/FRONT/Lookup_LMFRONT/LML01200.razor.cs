using Lookup_PMCOMMON.DTOs;
using Lookup_PMModel.ViewModel.LML00800;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lookup_PMModel.ViewModel.PML01200;

namespace Lookup_PMFRONT
{
    public partial  class LML01200 :R_Page
    {
        private LookupLML01200ViewModel _viewModel = new LookupLML01200ViewModel();
        private R_Grid<LML01200DTO>? GridRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await GridRef!.R_RefreshGrid(poParameter);
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
                var loParam = (LML01200ParameterDTO)eventArgs.Parameter;
                await _viewModel.GetInvoiceGroupList(loParam);
                eventArgs.ListEntityResult = _viewModel.InvoiceGroupList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task Button_OnClickOkAsync()
        {
            var loData = GridRef!.GetCurrentData();
            await Close(true, loData);
        }
        public async Task Button_OnClickCloseAsync()
        {
            await Close(true, null);
        }
    }
}
