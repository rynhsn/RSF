using BlazorClientHelper;
using HDM00600COMMON.DTO;
using HDM00600COMMON.DTO_s.Helper;
using HDM00600MODEL.View_Model_s;
using HDM00600FrontResources;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Interfaces;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.MessageBox;

namespace HDM00600FRONT
{
    public partial class HDM00600 : R_Page
    {
        private HDM00600ViewModel _viewModel = new();
        private R_Grid<PricelistDTO> _gridPricelist;
        private R_ConductorGrid _conPricelist;
        private R_TabPage _tabNextPricelist;
        private R_TabPage _tabHistoryPricelist;
        private R_TabStrip _tabStrip;
        private bool _anotherTabOnCrudMode = false;
        private bool _comboboxPropertyEnabled = true;
        private R_Button _btnActiveinactive;
        private string _lcActiveInactiveLabel = "";
        private int _pageBinding_PricelistGrid = 20;
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IClientHelper _clientHelper { get; set; }

        //method
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.GetPropertyList();
                if (!string.IsNullOrWhiteSpace(_viewModel._propertyId))
                {
                    await _gridPricelist.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task ComboboxPropertyValueChanged(string poParam)
        {
            R_Exception loEx = new();
            try
            {
                _viewModel._propertyId = string.IsNullOrWhiteSpace(poParam) ? "" : poParam;
                _viewModel._propertyName = _viewModel._propertyList.FirstOrDefault(x => x.CPROPERTY_ID == _viewModel._propertyId).CPROPERTY_NAME;
                _viewModel._propertyCurr = _viewModel._propertyList.FirstOrDefault(x => x.CPROPERTY_ID == _viewModel._propertyId).CCURRENCY;
                PropertyDTO loParam = new PropertyDTO()
                {
                    CPROPERTY_ID = _viewModel._propertyId,
                    CPROPERTY_NAME = _viewModel._propertyName,
                    CCURRENCY = _viewModel._propertyCurr
                };
                await _gridPricelist.R_RefreshGrid(null);
                switch (_tabStrip.ActiveTab.Id)
                {
                    case nameof(HDM00610):
                        await _tabNextPricelist.InvokeRefreshTabPageAsync(loParam);
                        break;
                    case nameof(HDM00620):
                        await _tabHistoryPricelist.InvokeRefreshTabPageAsync(loParam);
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task Btn_Activeinactive_OnClickAsync()
        {
            R_Exception loEx = new();
            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel._validId))
                {
                    var loMsg = await R_MessageBox.Show("", _localizer["_msg_nodataselected"], R_eMessageBoxButtonType.OK);
                }
                else
                {
                    await _viewModel.ActiveInactivePricingListAsync();
                    if (!loEx.HasError)
                    {
                        await _gridPricelist.R_RefreshGrid(null);
                        var loMsg = await R_MessageBox.Show("", _localizer["_msg_inactivesuccess"], R_eMessageBoxButtonType.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task Btn_Template_OnClickAsync()
        {

            var loEx = new R_Exception();
            try
            {
                var loByteFile = await _viewModel.DownloadTemplate();

                var saveFileName = $"PricelistTemplate.xlsx";

                await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //event
        private async Task CurrentPricelist_GetList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.GetPricingList(HDM00600ViewModel.eListPricingParamType.GetCurrent);
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
                _lcActiveInactiveLabel = _localizer["_btn_active"];
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //popup
        private void BeforeOpenPopup_PricelistAdd(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                eventArgs.Parameter = new PropertyDTO()
                {
                    CPROPERTY_ID = _viewModel._propertyId,
                    CPROPERTY_NAME = _viewModel._propertyName,
                    CCURRENCY = _viewModel._propertyCurr
                };
                eventArgs.PageTitle = _localizer["_title_add"];
                eventArgs.TargetPageType = typeof(HDM00601);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task AfterOpenPopup_PricelistAdd(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                await _gridPricelist.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void BeforeOpenPopup_PricelistUpload(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                eventArgs.Parameter = new PropertyDTO()
                {
                    CPROPERTY_ID = _viewModel._propertyId,
                    CPROPERTY_NAME = _viewModel._propertyName,
                    CCURRENCY = _viewModel._propertyCurr
                };
                eventArgs.PageTitle = _localizer["_title_upload"];
                eventArgs.TargetPageType = typeof(HDM00602);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task AfterOpenPopup_PricelistUpload(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                await _gridPricelist.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //tab
        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                eventArgs.Cancel = _anotherTabOnCrudMode;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void BeforeOpenTabPage_NextPricelist(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = new PropertyDTO() { CPROPERTY_ID = _viewModel._propertyId, CPROPERTY_NAME = _viewModel._propertyName, CCURRENCY = _viewModel._propertyCurr };
            eventArgs.TargetPageType = typeof(HDM00610);
        }
        private void TabEventCallback_NextPricelist(object poValue)
        {
            _comboboxPropertyEnabled = (bool)poValue;
            _anotherTabOnCrudMode = !(bool)poValue;
        }
        private void BeforeOpenTabPage_HistoryPricelist(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = new PropertyDTO() { CPROPERTY_ID = _viewModel._propertyId, CPROPERTY_NAME = _viewModel._propertyName, CCURRENCY = _viewModel._propertyCurr };
            eventArgs.TargetPageType = typeof(HDM00620);
        }


    }
}
