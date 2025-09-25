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
using Lookup_PMModel.ViewModel.LML01100;
using Lookup_PMCOMMON.DTOs.LML01100;

namespace Lookup_PMFRONT
{
    public partial class LML01100 : R_Page
    {
        private LookupLML01100ViewModel _viewModel = new LookupLML01100ViewModel();
        private R_Grid<LML01100DTO>? GridRef;
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
                var loParam = (LML01100ParameterDTO)eventArgs.Parameter;
                await _viewModel.GetTermNConditionList(loParam);
                eventArgs.ListEntityResult = _viewModel.TermNConditionList;
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
