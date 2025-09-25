using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace Lookup_GSFRONT
{
    public partial class GSL02900 : R_Page
    {
        private LookupGSL02900ViewModel _viewModel = new LookupGSL02900ViewModel();
        private R_Grid<GSL02900DTO> GridRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.SupplierParameter = (GSL02900ParameterDTO)poParameter;
                await GridRef.R_RefreshGrid(null);
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
                await _viewModel.GetSupplierList();

                eventArgs.ListEntityResult = _viewModel.SupplierGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task OnClickSearch()
        {
            var loEx = new R_Exception();
            bool loValidate = false;

            try
            {
                if (string.IsNullOrEmpty(_viewModel.SearchText))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                        "N01"));
                    loValidate = true;
                }
                else
                {
                    if (_viewModel.SearchText.Length < 3)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                        "N02"));
                        loValidate = true;
                    }
                }

                if (loValidate == false)
                {
                    _viewModel.SupplierParameter.CFILTER_SP = _viewModel.SearchText;
                    await GridRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task OnClickShowAll()
        {
            var loEx = new R_Exception();
            bool loValidate = false;

            try
            {
                _viewModel.SearchText = "";
                _viewModel.SupplierParameter.CFILTER_SP = "";
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
