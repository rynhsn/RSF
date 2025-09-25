using Lookup_PMCOMMON.DTOs;
using Lookup_PMCOMMON.DTOs.LML01000;
using Lookup_PMModel.ViewModel.LML00800;
using Lookup_PMModel.ViewModel.LML01000;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_PMFRONT
{
    public partial class LML01000 : R_Page
    {
        private LookupLML01000ViewModel _viewModel = new LookupLML01000ViewModel();
        private R_Grid<LML01000DTO>? GridRef;
        private int _pageSize = 12;
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
                var loParam = (LML01000ParameterDTO)eventArgs.Parameter;
                await _viewModel.GetBillingRuleList(loParam);
                eventArgs.ListEntityResult = _viewModel.BillingRuleList;
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
