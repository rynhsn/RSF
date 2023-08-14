using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lookup_GSFRONT
{
    public partial class GSL01500 : R_Page
    {
        private LookupGSL01500ViewModel _viewModel = new LookupGSL01500ViewModel();
        private R_Grid<GSL01500ResultDetailDTO> GridRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await CashFlowGrpComboBox_ServiceGetListRecord(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task PropertyDropdown_OnChange(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                await GridRef.R_RefreshGrid(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task CashFlowGrpComboBox_ServiceGetListRecord(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSL01500ParameterGroupDTO)poParameter;
                await _viewModel.GetCashFlowGroupList(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        public async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrEmpty(eventArgs.Parameter.ToString()))
                {
                    var loParam = new GSL01500ParameterDetailDTO()
                    {
                        CCASH_FLOW_GROUP_CODE = eventArgs.Parameter.ToString()
                    };
                    await _viewModel.GetCashFlowDetailList(loParam);

                    eventArgs.ListEntityResult = _viewModel.CashFlowDetailGrid;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Button_OnClickOkAsync()
        {
            var loTempData = (GSL01500ResultDetailDTO)GridRef.GetCurrentData();
            var loData = new GSL01500DTO()
            {
                CCASH_FLOW_GROUP_CODE = loTempData.CCASH_FLOW_GROUP_CODE,
                CCASH_FLOW_CODE = loTempData.CCASH_FLOW_CODE,
                CCASH_FLOW_NAME = loTempData.CCASH_FLOW_NAME
            };
            await this.Close(true, loData);
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }

    }
}
