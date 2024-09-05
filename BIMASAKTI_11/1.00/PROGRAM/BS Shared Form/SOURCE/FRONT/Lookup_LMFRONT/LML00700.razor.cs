using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMModel.ViewModel.LML00700;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_PMFRONT
{
    public partial class LML00700 : R_Page
    {

        private LookupLML00700ViewModel _viewModelLML00700 = new LookupLML00700ViewModel();
        private R_Grid<LML00700DTO> GridRef;
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
                var loParam = (LML00700ParameterDTO)eventArgs.Parameter;
                await _viewModelLML00700.GetDiscountList(loParam);
                eventArgs.ListEntityResult = _viewModelLML00700.DiscountList;
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
