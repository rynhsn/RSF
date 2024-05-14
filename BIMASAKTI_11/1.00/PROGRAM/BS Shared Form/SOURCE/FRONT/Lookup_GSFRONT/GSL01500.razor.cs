using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace Lookup_GSFRONT
{
    public partial class GSL01500 : R_Page
    {
        private LookupGSL01500ViewModel _viewModel = new LookupGSL01500ViewModel();
        private R_Grid<GSL01500ResultDetailDTO> GridRef;

        private R_ComboBox<GSL01500ResultGroupDTO, string> CashFlowGrp_ComboBox;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await CashFlowGrpComboBox_ServiceGetListRecord(poParameter);
                if (_viewModel.CashFlowGropList.Count > 0)
                {
                    var loParam = _viewModel.CashFlowGropList.FirstOrDefault();
                    await PropertyDropdown_OnChange(loParam.CCASH_FLOW_GROUP_CODE);
                }

                await CashFlowGrp_ComboBox.FocusAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task PropertyDropdown_OnChange(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.CashFlowCode = poParam;

                await GridRef.R_RefreshGrid(null);
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
                await _viewModel.GetCashFlowDetailList();

                eventArgs.ListEntityResult = _viewModel.CashFlowDetailGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Button_OnClickOkAsync()
        {
            var loTempData = GridRef.CurrentSelectedData;
            var loData = R_FrontUtility.ConvertObjectToObject<GSL01500DTO>(loTempData);
                
            await this.Close(true, loData);
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }

    }
}
