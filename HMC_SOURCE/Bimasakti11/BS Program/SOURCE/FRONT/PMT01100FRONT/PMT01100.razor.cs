using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Interfaces;
using PMT01100FrontResources;
using BlazorClientHelper;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls.DataControls;
using PMT01100Model.ViewModel;
using PMT01100Common.DTO._1._Unit_List;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Helpers;
using PMT01100Common.Utilities;
using System.Xml.Linq;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace PMT01100Front
{
    public partial class PMT01100 : R_Page
    {
        #region Master Page 

        readonly PMT01100UnitListViewModel _viewModel = new();
        R_ConductorGrid? _conductorPMT01100Building;
        R_ConductorGrid? _conductorPMT01100Unit;
        R_ConductorGrid? _conductorPMT01100SelectedUnit;
        R_Grid<PMT01100UnitList_BuildingDTO>? _gridRefPMT01100Building;
        R_Grid<PMT01100UnitList_UnitListDTO>? _gridRefPMT01100Unit;
        R_Grid<PMT01100UnitList_SelectedUnitDTO>? _gridRefPMT01100SelectedUnit;

        [Inject] IJSRuntime? JS { get; set; }
        [Inject] IClientHelper? _clientHelper { get; set; }
        [Inject] R_ILocalizer<Resources_PMT01100_Class>? _localizer { get; set; }
        #endregion


        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.lControlData = false;
                _viewModel.oParameterForGetUnitList.ODataUnitList = null;
                await _viewModel.GetPropertyList();
                if (!string.IsNullOrEmpty(_viewModel.oPropertyId.CPROPERTY_ID))
                {
                    _viewModel.lControlData = true;
                    await _gridRefPMT01100Building.R_RefreshGrid(null);

                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task BlankFunction()
        {
            var loException = new R_Exception();
            //APM00500ProductDetailDTO? poEntityAPM00500Detail = (APM00500ProductDetailDTO)_conductorAPM00500ProductDetail.R_GetCurrentData();

            try
            {
                var llTrue = await R_MessageBox.Show("", "This function still on Development Process!", R_eMessageBoxButtonType.OK);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        #region Button Control


        private async Task NewOfferFunctionAsync()
        {
            var loException = new R_Exception();
            //APM00500ProductDetailDTO? poEntityAPM00500Detail = (APM00500ProductDetailDTO)_conductorAPM00500ProductDetail.R_GetCurrentData();
            List<PMT01100UnitList_UnitListDTO> loData;

            try
            {
                var loDataUnitList = _viewModel.loListUnitList;

                loData = loDataUnitList
                    .Where(x => x.LSELECTED_UNIT == true)
                    .ToList();

                if (!loData.Any())
                {
                    var loValidate = await R_MessageBox.Show("", _localizer["ValidationNoRecord"], R_eMessageBoxButtonType.OK);
                    goto EndBlock;
                }

                loData.ForEach(x => x.LSELECTED_UNIT = false);
                _viewModel.oParameterForGetUnitList.ODataUnitList = JsonSerializer.Serialize(loData);
                await _tabStripRef?.SetActiveTabAsync("LOO")!;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

        EndBlock:

            loException.ThrowExceptionIfErrors();
        }


        #endregion



        #region Front Control Property

        private async Task PropertyDropdown_OnChange(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.oPropertyId.CPROPERTY_ID = _viewModel.oParameterForGetUnitList.CPROPERTY_ID = poParam;
                await _gridRefPMT01100Building.R_RefreshGrid(null);

                if (_tabStripRef.ActiveTab.Id == "LOO")
                {
                    await _tabLOO.InvokeRefreshTabPageAsync(_viewModel.oParameterForGetUnitList);
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


        #region List Building

        private async Task R_ServiceGetListBuildingRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetBuildingList();
                if (!_viewModel.loListBuilding.Any())
                {
                    _viewModel.loListUnitList.Clear();
                }
                eventArgs.ListEntityResult = _viewModel.loListBuilding;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_DisplayBuildingGetRecord(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            PMT01100UnitList_BuildingDTO loData = (PMT01100UnitList_BuildingDTO)eventArgs.Data;

            try
            {
                if (!string.IsNullOrEmpty(loData.CBUILDING_ID))
                {
                    _viewModel.oParameterForGetUnitList.CBUILDING_ID = loData.CBUILDING_ID;
                    _viewModel.oParameterForGetUnitList.CBUILDING_NAME = loData.CBUILDING_NAME;
                    await _gridRefPMT01100Unit.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion


        #region List Unit

        private async Task R_ServiceGetListUnitRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetUnitList();
                eventArgs.ListEntityResult = _viewModel.loListUnitList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_DisplayUnitGetRecord(R_DisplayEventArgs eventArgs)
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

        #endregion


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


        #region Master Tab

        private R_TabStrip? _tabStripRef;
        private R_TabPage? _tabLOO;
        private R_TabPage? _tabLOC;

        #region Tab Unit List

        private async Task OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            switch (eventArgs.Id)
            {
                case "UnitList":
                    await _gridRefPMT01100Building.R_RefreshGrid(null);
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


        #region Utilities Master Tab

        private async Task R_TabEventCallbackAsync(object poValue)
        {
            var loEx = new R_Exception();

            try
            {
                var loValue = R_FrontUtility.ConvertObjectToObject<PMT01100EventCallBackDTO>(poValue);
                _viewModel.lControlData = loValue.LUSING_PROPERTY_ID;
                _viewModel.lControlTabLOC = _viewModel.lControlTabLOO = _viewModel.lControlTabUnitList = loValue.LCRUD_MODE;
                if (!string.IsNullOrEmpty(loValue.CCRUD_MODE))
                {
                    switch (loValue.CCRUD_MODE)
                    {
                        case "A_ADD":
                            _viewModel.oParameterForGetUnitList.ODataUnitList = null;
                            break;

                        case "A_DELETE":
                            _viewModel.oParameterForGetUnitList.ODataUnitList = null;
                            break;

                        case "A_CANCEL":
                            _viewModel.oParameterForGetUnitList.ODataUnitList = null;
                            break;
                        default:
                            _viewModel.oParameterForGetUnitList.ODataUnitList = null;
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
            eventArgs.Parameter = _viewModel.oParameterForGetUnitList;
            eventArgs.TargetPageType = typeof(PMT01100LOO_OfferList);
        }

        #endregion


        #region Tab LOC

        private void General_Before_Open_LOC_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModel.oParameterForGetUnitList;
            eventArgs.TargetPageType = typeof(PMT01100LOC);
        }

        #endregion

        #endregion

    }
}
