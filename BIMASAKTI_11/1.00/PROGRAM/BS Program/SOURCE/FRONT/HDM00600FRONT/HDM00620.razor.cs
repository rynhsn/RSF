using BlazorClientHelper;
using HDM00600COMMON.DTO;
using HDM00600MODEL.View_Model_s;
using HDM00600FrontResources;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using R_BlazorFrontEnd.Helpers;
using HDM00600COMMON.DTO_s.Helper;
using R_BlazorFrontEnd.Controls.Tab;

namespace HDM00600FRONT
{
    public partial class HDM00620 : R_Page, R_ITabPage
    {
        private HDM00600ViewModel _viewModel = new();
        private R_Grid<PricelistDTO> _gridPricelist;
        private R_ConductorGrid _conPricelist;
        private int _pageSize_Pricelist = 20;

        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IClientHelper _clientHelper { get; set; }

        //method - tab
        public async Task RefreshTabPageAsync(object poParam)
        {
            R_Exception loEx = new();
            try
            {
                if (poParam != null)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<PropertyDTO>(poParam);
                    _viewModel._propertyId = loParam.CPROPERTY_ID as string ?? "";
                    _viewModel._propertyName = loParam.CPROPERTY_NAME as string ?? "";
                    _viewModel._propertyCurr = loParam.CCURRENCY as string ?? "";
                    await _gridPricelist.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        //method - override
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (poParameter != null)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<PropertyDTO>(poParameter);
                    _viewModel._propertyId = loParam.CPROPERTY_ID as string ?? "";
                    _viewModel._propertyName = loParam.CPROPERTY_NAME as string ?? "";
                    _viewModel._propertyCurr = loParam.CCURRENCY as string ?? "";
                    await _gridPricelist.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        //method - event
        private async Task CurrentPricelist_GetList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.GetPricingList(HDM00600ViewModel.eListPricingParamType.GetHistory);
                eventArgs.ListEntityResult = _viewModel._pricelist_List;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void CurrentPricelsit_Display(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = eventArgs.Data as PricelistDTO;
                _viewModel._pricelistId = loData.CPRICELIST_ID;
                _viewModel._validId = loData.CVALID_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
