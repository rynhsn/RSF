using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_GSFRONT
{
    public partial class GSL03800 : R_Page
    {
        private LookupGSL03800ViewModel _viewModel = new LookupGSL03800ViewModel();
        private R_Grid<GSL03800DTO> GridRef;
        private bool EnableLookupList;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetInitialProcess();
                var loParam = (GSL03800ParameterDTO)poParameter;
                EnableLookupList = string.IsNullOrWhiteSpace(loParam.CPROPERTY_ID);
                _viewModel.LookupParam = loParam;

                if (string.IsNullOrWhiteSpace(loParam.CPROPERTY_ID))
                {
                    if (_viewModel.PropertyList.Count > 0)
                    {
                        await OnChangePropertyComboBox(_viewModel.PropertyList[0].CPROPERTY_ID);
                    }
                }
                else
                {
                    await OnChangePropertyComboBox(loParam.CPROPERTY_ID);
                }
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
                await _viewModel.GetLocationList();

                eventArgs.ListEntityResult = _viewModel.LocationGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnChangePropertyComboBox(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.LookupParam.CPROPERTY_ID = poParam;
                await GridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task OnChangeInactiveLocation(bool poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.LACTIVE_CHECKBOX = poParam;
                if (poParam)
                {
                    _viewModel.LookupParam.CACTIVE_TYPE = "ALL";
                }
                else
                {
                    _viewModel.LookupParam.CACTIVE_TYPE = "ACTIVE";
                }

                await GridRef.R_RefreshGrid(null);
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
