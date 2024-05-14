using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace Lookup_GSFRONT
{
    public partial class GSL02300 : R_Page
    {
        private LookupGSL02300ViewModel _viewModel = new LookupGSL02300ViewModel();
        private R_Grid<GSL02300DTO> GridRef;
        private GSL02300ParameterDTO _Parameter = new GSL02300ParameterDTO();
        private string _FloorName;
        private R_TextBox _FloorCode;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _Parameter = (GSL02300ParameterDTO)poParameter;

                await _FloorCode.FocusAsync();
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
                await _viewModel.GetBuildingUnitList(_Parameter);

                eventArgs.ListEntityResult = _viewModel.BuildingUnitGrid;
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

        #region Floor

        private void R_Before_Open_LookupFloor(R_BeforeOpenLookupEventArgs eventArgs)
        {

            GSL02400ParameterDTO loParam = R_FrontUtility.ConvertObjectToObject<GSL02400ParameterDTO>(_Parameter);
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL02400);
        }

        private async Task R_After_Open_LookupFloor(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL02400DTO loTempResult = (GSL02400DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _Parameter.CFLOOR_ID = loTempResult.CFLOOR_ID;
            _FloorName = loTempResult.CFLOOR_NAME;

            await GridRef.R_RefreshGrid(null);
        }

        #endregion
    }
}
