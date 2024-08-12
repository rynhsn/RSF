using Lookup_PMCOMMON.DTOs;
using Lookup_PMCOMMON.DTOs.LML01300;
using Lookup_PMModel.ViewModel.LML01300;
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
    public partial class LML01300 : R_Page
    {
        private LookupLML01300ViewModel _viewModel = new LookupLML01300ViewModel();
        private R_Grid<LML01300DTO>? GridRef;

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
                var loParam = (LML01300ParameterDTO)eventArgs.Parameter;
                await _viewModel.GetLOIAgreementList(loParam);
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
            var loData = GridRef!.GetCurrentData();
            await Close(true, loData);
        }
        public async Task Button_OnClickCloseAsync()
        {
            await Close(true, null);
        }
    }
}
