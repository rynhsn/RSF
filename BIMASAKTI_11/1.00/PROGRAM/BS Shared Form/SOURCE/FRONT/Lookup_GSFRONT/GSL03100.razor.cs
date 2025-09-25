using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace Lookup_GSFRONT
{
    public partial class GSL03100 : R_Page
    {
        private LookupGSL03100ViewModel _viewModel = new LookupGSL03100ViewModel();
        private R_Grid<GSL03100DTO> GridRef;

        private string _ExpenditureName = "";
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.ExpenditureParameter = (GSL03100ParameterDTO)poParameter;
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
                await _viewModel.GetExpenditureList();

                eventArgs.ListEntityResult = _viewModel.ExpenditureGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task OnClickRefresh()
        {
            var loEx = new R_Exception();
            bool llValidate = false;
            try
            {
                if (_viewModel.CategoryOption == "S")
                {
                    if (string.IsNullOrEmpty(_viewModel.ExpenditureParameter.CCATEGORY_ID))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "V01"));
                        llValidate = true;
                    }
                }

                if (llValidate == false)
                {
                    await GridRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task OnChanged(string poParam)
        {
            var loEx = new R_Exception();
            try
            {
                _viewModel.CategoryOption = poParam;
                if (poParam == "A")
                {
                    _viewModel.ExpenditureParameter.CCATEGORY_ID = "";
                    _ExpenditureName = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
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

        #region Category
        private async Task Category_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.ExpenditureParameter.CCATEGORY_ID) == false)
                {

                    GSL01800DTOParameter loParam = new GSL01800DTOParameter();
                    loParam.CSEARCH_TEXT = _viewModel.ExpenditureParameter.CCATEGORY_ID;
                    loParam.CCATEGORY_TYPE = "40";

                    LookupGSL01800ViewModel loLookupViewModel = new LookupGSL01800ViewModel();

                    var loResult = await loLookupViewModel.GetCategory(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _ExpenditureName = "";
                        goto EndBlock;
                    }
                    _viewModel.ExpenditureParameter.CCATEGORY_ID = loResult.CCATEGORY_ID;
                    _ExpenditureName = loResult.CCATEGORY_NAME;

                    await GridRef.R_RefreshGrid(null);
                }
                else
                {
                    await GridRef.R_RefreshGrid(null);
                    _ExpenditureName = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void R_Before_Open_LookupCategory(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL01800DTOParameter loParam = new GSL01800DTOParameter();
            loParam.CSEARCH_TEXT = _viewModel.ExpenditureParameter.CCATEGORY_ID;
            loParam.CCATEGORY_TYPE = "40";

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL01800);
        }
        private async Task R_After_Open_LookupCategory(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL01800DTO loTempResult = (GSL01800DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _viewModel.ExpenditureParameter.CCATEGORY_ID = loTempResult.CCATEGORY_ID;
            _ExpenditureName = loTempResult.CCATEGORY_NAME;

            await GridRef.R_RefreshGrid(null);
        }
        #endregion
    }
}
