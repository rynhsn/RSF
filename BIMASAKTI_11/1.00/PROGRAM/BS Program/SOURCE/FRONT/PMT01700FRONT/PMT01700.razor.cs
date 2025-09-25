using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PMT01700COMMON.DTO._1._Other_Unit_List;
using PMT01700COMMON.DTO.Utilities;
using PMT01700COMMON.DTO.Utilities.Front;
using PMT01700FrontResources;
using PMT01700MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Base;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Forms;
using R_BlazorFrontEnd.Controls.Helpers;
using R_BlazorFrontEnd.Controls.Interfaces;
using R_BlazorFrontEnd.Controls.Menu;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace PMT01700FRONT
{
    public partial class PMT01700 : R_Page
    {
        #region Master Page 

        readonly PMT01700UnitListViewModel _viewModel = new();
        R_ConductorGrid? _conductorPMT01700otherUnit;
        R_ConductorGrid? _conductorPMT01700SelectedOtherUnit;
        R_Grid<PMT01700OtherUnitList_OtherUnitListDTO>? _gridRefPMT01700OtherUnit;
        R_Grid<PMT01700OtherUnitList_OtherSelectedUnitDTO>? _gridRefPMT01700SelectedOtherUnit;

        [Inject] IJSRuntime? JS { get; set; }
        [Inject] IClientHelper? _clientHelper { get; set; }
        #endregion


        private R_TabStrip? _tabStripRef;
        private R_TabPage? _tabLOO;
        private R_TabPage? _tabLOC;

        private int _pageSizeOtherUnit = 15;
        private int _pageSizeSelectedOtherUnit = 7;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.lControlData = false;
                _viewModel.oProperty_oDataOtherUnit.ODataOtherUnitList = null;
                await _viewModel.GetPropertyList();
                if (!string.IsNullOrEmpty(_viewModel.oProperty_oDataOtherUnit.CPROPERTY_ID))
                {
                    _viewModel.lControlData = true;
                    await _gridRefPMT01700OtherUnit!.R_RefreshGrid(null);

                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region Button Control

        private async Task BeforeOpenPopupOfferAsync(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loOtherDataUnitList = _viewModel.loOtherUnitList;

                List<PMT01700OtherUnitList_OtherUnitListDTO> loData = loOtherDataUnitList
                    .Where(x => x.LSELECTED_UNIT == true)
                    .ToList();

                if (!loData.Any())
                {
                    var loValidate = await R_MessageBox.Show("", _localizer["ValidationNoRecord"], R_eMessageBoxButtonType.OK);
                    goto EndBlock;
                }

                //to validate user just choose 1 type building
                int BuildingCount = loData
                                    .Select(item => item.CBUILDING_ID)
                                    .Distinct()
                                    .Count();

                if (BuildingCount == 1)
                {
                    loData.ForEach(x => x.LSELECTED_UNIT = false);
                    //assign value param to offer page
                    _viewModel.oParameterNewOffer.CPROPERTY_ID = _viewModel.oProperty_oDataOtherUnit.CPROPERTY_ID!;
                    _viewModel.oParameterNewOffer.CBUILDING_ID = loData[0].CBUILDING_ID!;
                    _viewModel.oParameterNewOffer.CBUILDING_NAME = loData[0].CBUILDING_ID!;
                    _viewModel.oParameterNewOffer.ODataUnitList = JsonSerializer.Serialize(loData);
                    _viewModel.oParameterNewOffer.CTRANS_CODE = "802043";

                    eventArgs.Parameter = _viewModel.oParameterNewOffer;
                    eventArgs.TargetPageType = typeof(PMT01700LOO_Offer);

                }
                else
                {
                    var loValidate = await R_MessageBox.Show("", _localizer["ValidationSelectedRecord"], R_eMessageBoxButtonType.OK);
                    goto EndBlock;
                }
                //to unselect data
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);

        }

        private async Task AfterOpenPopupOfferAsync(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.Success)
                {
                    await _tabStripRef?.SetActiveTabAsync("LOO")!;
                }
                else
                {
                    _viewModel.loSelectedUnitList = new ObservableCollection<PMT01700OtherUnitList_OtherSelectedUnitDTO>();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }

        #endregion

        #region Front Control Property

        private async Task PropertyDropdown_OnChange(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.oProperty_oDataOtherUnit.CPROPERTY_ID = _viewModel.oProperty_oDataOtherUnit.CPROPERTY_ID = poParam;
                _viewModel.loSelectedUnitList = new ObservableCollection<PMT01700OtherUnitList_OtherSelectedUnitDTO>();

                await _gridRefPMT01700OtherUnit!.R_RefreshGrid(null);

                if (_tabStripRef!.ActiveTab.Id == "LOO")
                {
                    await _tabLOO!.InvokeRefreshTabPageAsync(_viewModel.oProperty_oDataOtherUnit);
                }
                else if (_tabStripRef.ActiveTab.Id == "LOC")
                {
                    await _tabLOC!.InvokeRefreshTabPageAsync(_viewModel.oProperty_oDataOtherUnit);
                }


                /*

                switch (_tabStripRef.ActiveTab.Id)
                {
                    case "Agreement":
                        await _tabAgreementRef.InvokeRefreshTabPageAsync(null);
                        break;

                    case "UnitInfo":
                        await _tabUnitInfoRef.InvokeRefreshTabPageAsync(null);
                        break;

                    case "ChargesInfo":
                        await _tabChargesInfoRef.InvokeRefreshTabPageAsync(null);
                        break;

                    case "InvoicePlan":
                        await _tabInvoicePlanRef.InvokeRefreshTabPageAsync(null);
                        break;

                    case "Deposit":
                        await _tabDepositRef.InvokeRefreshTabPageAsync(null);
                        break;

                    case "Document":
                        await _tabDocumentRef.InvokeRefreshTabPageAsync(null);
                        break;

                    default:
                        break;
                }
                */

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }


        #endregion

        #region Tab Unit List
        private async Task OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            switch (eventArgs.Id)
            {
                case "UnitList":
                    // await _gridRefPMT01100Building.R_RefreshGrid(null);
                    break;
                case "LOO":

                    break;
                default:
                    break;
            }

        }
        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            /*
            _view._lComboBoxProperty = true;
            eventArgs.Cancel = _pageAgreementListOnCRUDmode;
            */

            switch (eventArgs.TabStripTab.Id)
            {
                case "UnitList":
                    _viewModel.lControlData = true;
                    break;
                case "LOO":
                    _viewModel.lControlData = true;
                    break;
                case "LOC":
                    _viewModel.lControlData = true;
                    break;
                default:
                    break;
            }

        }

        #endregion


        private async Task BlankFunction()
        {
            var loException = new R_Exception();
            //APM00500ProductDetailDTO? poEntityAPM00500Detail = (APM00500ProductDetailDTO)_conductorAPM00500ProductDetail.R_GetCurrentData();

            try
            {
                //   var llTrue = await R_MessageBox.Show("", "This function still on Development Process!", R_eMessageBoxButtonType.OK);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
        #region CellValueChanged
        private void R_CellValueChanged(R_CellValueChangedEventArgs eventArgs)
        {
            var loSelectedData = eventArgs.CurrentRow as PMT01700OtherUnitList_OtherUnitListDTO;

            if (loSelectedData.LSELECTED_UNIT == true)
            {
                if (!_viewModel.loSelectedUnitList.Any(data => data.COTHER_UNIT_ID == loSelectedData.COTHER_UNIT_ID))
                {
                    _viewModel.loSelectedUnitList.Add(new PMT01700OtherUnitList_OtherSelectedUnitDTO()
                    {
                        CFLOOR_ID = loSelectedData.CFLOOR_ID,
                        COTHER_UNIT_ID = loSelectedData.COTHER_UNIT_ID,
                        COTHER_UNIT_TYPE_ID = loSelectedData.COTHER_UNIT_TYPE_ID,
                        NGROSS_AREA_SIZE = loSelectedData.NGROSS_AREA_SIZE,
                        NNET_AREA_SIZE = loSelectedData.NNET_AREA_SIZE,
                        CLOCATION = loSelectedData.CLOCATION,
                    });
                }
            }
            else if (loSelectedData.LSELECTED_UNIT == false)
            {
                // Menghapus item dari loListUnitSelected jika Selected == false
                var itemToRemove = _viewModel.loSelectedUnitList
                    .FirstOrDefault(item => item.COTHER_UNIT_ID == loSelectedData.COTHER_UNIT_ID);
                if (itemToRemove != null)
                {
                    _viewModel.loSelectedUnitList.Remove(itemToRemove);
                }
            }
        }
        #endregion

        #region List Other unit list
        private async Task R_ServiceGetListOtherUnitRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetOtherUnitList();
                if (!_viewModel.loOtherUnitList.Any())
                {
                    _viewModel.loOtherUnitList.Clear();
                }
                eventArgs.ListEntityResult = _viewModel.loOtherUnitList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_DisplayOtherUnitGetRecord(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            PMT01700OtherUnitList_OtherUnitListDTO loData = (PMT01700OtherUnitList_OtherUnitListDTO)eventArgs.Data;

            try
            {
                //ON DEVELOPEMENT ON RND

                if (!string.IsNullOrEmpty(loData.CBUILDING_ID))
                {

                    _viewModel.oProperty_oDataOtherUnit.CBUILDING_ID = loData.CBUILDING_ID;
                    _viewModel.oProperty_oDataOtherUnit.CBUILDING_NAME = loData.CBUILDING_NAME;
                    _viewModel.oProperty_oDataOtherUnit.COTHER_UNIT_ID = loData.COTHER_UNIT_ID;
                    //await _gridRefPMT01100Unit.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        #region List Selected Unit
        private void R_ServiceGetListSelectedUnitRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /*
        private void R_DisplaySelectedUnitGetRecord(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            //PMT01500AgreementListOriginalDTO loData = (PMT01500AgreementListOriginalDTO)eventArgs.Data;

            try
            {
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        */
        #endregion

        #endregion
        #region Utilities Master Tab

        private async Task R_TabEventCallbackAsync(object poValue)
        {
            var loEx = new R_Exception();

            try
            {
                var loValue = R_FrontUtility.ConvertObjectToObject<PMT01700EventCallBackDTO>(poValue);
                _viewModel.lControlData = loValue.LUSING_PROPERTY_ID;
                _viewModel.lControlTabLOC = _viewModel.lControlTabLOO = _viewModel.lControlTabUnitList = loValue.LCRUD_MODE;
                if (!string.IsNullOrEmpty(loValue.CCRUD_MODE))
                {
                    switch (loValue.CCRUD_MODE)
                    {
                        case "A_ADD":
                            _viewModel.oProperty_oDataOtherUnit.ODataOtherUnitList = null;
                            break;

                        case "A_DELETE":
                            _viewModel.oProperty_oDataOtherUnit.ODataOtherUnitList = null;
                            break;

                        case "A_CANCEL":
                            _viewModel.oProperty_oDataOtherUnit.ODataOtherUnitList = null;
                            break;
                        default:
                            _viewModel.oProperty_oDataOtherUnit.ODataOtherUnitList = null;
                            break;
                    }
                }
                //await InvokeTabEventCallbackAsync(null);
                /*
                switch (loValue.CCRUD_MODE)
                {
                    case 'A':
                        break;
                    default:
                        break;
                }
                */
                //INI MASALAH PASTI
                await Task.Delay(100);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }

        #endregion


        #region Tab LOO

        private void General_Before_Open_LOO_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModel.oProperty_oDataOtherUnit;
            eventArgs.TargetPageType = typeof(PMT01700LOO_OfferList);
        }

        #endregion


        #region Tab LOC

        private void General_Before_Open_LOC_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModel.oProperty_oDataOtherUnit;
            eventArgs.TargetPageType = typeof(PMT01700LOC_LOCList);
        }

        #endregion

        #region implement library for checkbox select
        private void R_CheckBoxSelectValueChanged(R_CheckBoxSelectValueChangedEventArgs eventArgs)
        {
            var loSelectedData = eventArgs.CurrentRow as PMT01700OtherUnitList_OtherUnitListDTO;

            if (eventArgs.Value)
            {
                if (!_viewModel.loSelectedUnitList.Any(data => data.COTHER_UNIT_ID == loSelectedData.COTHER_UNIT_ID))
                {
                    _viewModel.loSelectedUnitList.Add(new PMT01700OtherUnitList_OtherSelectedUnitDTO()
                    {
                        CFLOOR_ID = loSelectedData.CFLOOR_ID,
                        COTHER_UNIT_ID = loSelectedData.COTHER_UNIT_ID,
                        COTHER_UNIT_TYPE_ID = loSelectedData.COTHER_UNIT_TYPE_ID,
                        NGROSS_AREA_SIZE = loSelectedData.NGROSS_AREA_SIZE,
                        NNET_AREA_SIZE = loSelectedData.NNET_AREA_SIZE,
                        CLOCATION = loSelectedData.CLOCATION,
                    });

                }
            }
            else 
            {
                // Menghapus item dari loListUnitSelected jika Selected == false
                var itemToRemove = _viewModel.loSelectedUnitList
                    .FirstOrDefault(item => item.COTHER_UNIT_ID == loSelectedData.COTHER_UNIT_ID);
                if (itemToRemove != null)
                {
                    _viewModel.loSelectedUnitList.Remove(itemToRemove);
                }
            }
        }
        #endregion



    }
}
