using BaseAOC_BS11Common.DTO.Response.GridList;
using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using PMT01900Common.DTO.Front;
using PMT01900FrontResources;
using PMT01900Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;

namespace PMT01900Front
{
    public partial class PMT01900 : R_Page
    {
        #region Master Page

        readonly PMT01900LOIListViewModel _viewModel = new();
        R_ConductorGrid? _conductorLOIList;
        R_ConductorGrid? _conductorUnitList;
        R_Grid<BaseAOCResponseAgreementListDTO>? _gridLOIList;
        R_Grid<BaseAOCResponseAgreementUnitInfoListDTO>? _gridUnitList;

        //[Inject] IJSRuntime? JS { get; set; }
        [Inject] IClientHelper? _clientHelper { get; set; }
        [Inject] R_ILocalizer<Resources_PMT01900_Class>? _localizer { get; set; }
        PMT01900EventCallBackDTO _oEventCallBack = new PMT01900EventCallBackDTO();

        #endregion

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.lControlButton = false;
                await _viewModel.GetPropertyList();
                _viewModel.oParameter.CTRANS_CODE = "802063";
                if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
                {
                    _viewModel.lControlButton = true;
                    await _gridLOIList.R_RefreshGrid(null);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void BlankFunction()
        {
        }


        private async Task ProsesSubmit()
        {
            var loEx = new R_Exception();

            try
            {
                //SUBMIT CODE == "10"
                //REDRAFT CODE == "00"
                bool llConfirmation = await R_MessageBox.Show("Confirmation",
                    R_FrontUtility.R_GetMessage(typeof(Resources_PMT01900_Class), "_ConfirmationSubmit"),
                    R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.Yes;

                if (llConfirmation)
                {
                    _viewModel.loEntity =
                        R_FrontUtility.ConvertObjectToObject<BaseAOCResponseAgreementListDTO>(
                            _gridLOIList.GetCurrentData());
                    var loResult = await _viewModel.ProsesUpdateAgreementStatus(pcStatus: "10");
                    if ((bool)loResult.LSUCCESS!)
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMT01900_Class),
                            "_SuccessMessageOfferSubmit"));
                        await _gridLOIList!.R_RefreshGrid(null);
                    }
                    else
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMT01900_Class),
                            "_FailedUpdate"));
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task ProsesRedraft()
        {
            var loEx = new R_Exception();

            try
            {
                //SUBMIT CODE == "10"
                //REDRAFT CODE == "00"
                bool llConfirmation = await R_MessageBox.Show("Confirmation",
                    R_FrontUtility.R_GetMessage(typeof(Resources_PMT01900_Class), "_ConfirmationSubmit"),
                    R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.Yes;

                if (llConfirmation)
                {
                    _viewModel.loEntity =
                        R_FrontUtility.ConvertObjectToObject<BaseAOCResponseAgreementListDTO>(
                            _gridLOIList.GetCurrentData());
                    var loResult = await _viewModel.ProsesUpdateAgreementStatus(pcStatus: "00");
                    if ((bool)loResult.LSUCCESS!)
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMT01900_Class),
                            "_SuccessMessageOfferRedraft"));
                        await _gridLOIList!.R_RefreshGrid(null);
                    }
                    else
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMT01900_Class),
                            "_FailedUpdate"));
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }


        #region Front Control Property

        private async Task PropertyDropdown_OnChange(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.oPropertyId.CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID = poParam;
                await _gridLOIList.R_RefreshGrid(null);

                //switch (_tabStripRef.ActiveTab.Id)
                //{
                //    case "LOO":
                //        await _tabLOO.InvokeRefreshTabPageAsync(_viewModel.oParameterForGetUnitList);
                //        break;
                //    case "LOI":
                //        await _tabLOI.InvokeRefreshTabPageAsync(_viewModel.oParameterForGetUnitList);
                //        break;
                //    default:
                //        break;
                //}
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #region Filtering LOI LIST
        private async Task FilteringLOIList_OnChange(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.cFilterStatusList = poParam;
                switch (poParam)
                {
                    case "00":
                        _viewModel.cTempFilterToBack = "00,10,20";
                        break;
                    case "30":
                        _viewModel.cTempFilterToBack = "30";
                        break;
                    case "80":
                        _viewModel.cTempFilterToBack = "80";
                        break;
                    case "90":
                        _viewModel.cTempFilterToBack = "90,98";
                        break;
                    default:
                        _viewModel.cTempFilterToBack = "30";
                        break;
                }
                await _gridLOIList.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region List LOI List

        private async Task R_ServiceGetListLOIListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetLOIList();
                if (!_viewModel.oLOIList.Any())
                {
                    _viewModel.oUnitList.Clear();
                    _viewModel.cBuildingSelectedUnit = "";
                    _viewModel.lControlButtonSubmit = _viewModel.lControlButtonRedraft = false;
                }
                _viewModel.lControlTabUnitandCharges = _viewModel.oLOIList.Any();
                _viewModel.lControlTabDeposit = _viewModel.oLOIList.Any();

                eventArgs.ListEntityResult = _viewModel.oLOIList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_DisplayLOIListGetRecordAsync(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var loData = (BaseAOCResponseAgreementListDTO)eventArgs.Data;

            try
            {
                if (!string.IsNullOrEmpty(loData.CREF_NO))
                {
                    _viewModel.oParameter.CREF_NO = loData.CREF_NO;
                    _viewModel.oParameter.CDEPT_CODE = loData.CDEPT_CODE;
                    _viewModel.oParameter.CBUILDING_ID = loData.CBUILDING_ID;
                    _viewModel.cBuildingSelectedUnit = _viewModel.oParameter.CBUILDING_NAME = loData.CBUILDING_NAME;


                    switch (loData.CTRANS_STATUS)
                    {
                        case "00":
                            _viewModel.lControlButtonRedraft = false;
                            _viewModel.lControlButtonSubmit = true;
                            break;
                        case "30":
                            _viewModel.lControlButtonRedraft = _viewModel.lControlButtonSubmit = false;
                            break;
                        case "10":
                            _viewModel.lControlButtonSubmit = false;
                            _viewModel.lControlButtonRedraft = true;
                            break;
                    }



                    await _gridUnitList.R_RefreshGrid(null);
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

        private async Task R_ServiceGetListUnitListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetUnitList();
                eventArgs.ListEntityResult = _viewModel.oUnitList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Master Tab

        private R_TabStrip? _tabStripRef;
        private R_TabPage? _tabLOI;
        private R_TabPage? _tabUnit;
        private R_TabPage? _tabDeposit;

        #region Tab OfferList

        private async Task OnActiveTabIndexChangedAsync(R_TabStripTab eventArgs)
        {
            switch (eventArgs.Id)
            {
                case "LOIList":
                    await _gridLOIList.R_RefreshGrid(null);
                    break;
                default:
                    break;
            }

        }

        private async Task OnActiveTabIndexChangingAsync(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            switch (eventArgs.TabStripTab.Id)
            {
                case "LOIList":
                    _oEventCallBack.LUSING_PROPERTY_ID = _oEventCallBack.LCRUD_MODE = true;
                    await InvokeTabEventCallbackAsync(_oEventCallBack);
                    _viewModel.lControlButton = true;
                    break;
                case "LOI":
                    _oEventCallBack.LUSING_PROPERTY_ID = false;
                    _oEventCallBack.LCRUD_MODE = true;
                    await InvokeTabEventCallbackAsync(_oEventCallBack);
                    _viewModel.lControlButton = false;
                    break;
                case "Unit":
                    _oEventCallBack.LUSING_PROPERTY_ID = false;
                    _oEventCallBack.LCRUD_MODE = true;
                    await InvokeTabEventCallbackAsync(_oEventCallBack);
                    _viewModel.lControlButton = false;
                    break;
                case "Deposit":
                    _oEventCallBack.LUSING_PROPERTY_ID = false;
                    _oEventCallBack.LCRUD_MODE = true;
                    await InvokeTabEventCallbackAsync(_oEventCallBack);
                    _viewModel.lControlButton = false;
                    break;
                default:
                    break;
            }

        }

        #endregion


        #region Utulities Master Tab

        private async Task R_TabEventCallbackAsync(object poValue)
        {
            var loEx = new R_Exception();

            try
            {

                var loValue = R_FrontUtility.ConvertObjectToObject<PMT01900EventCallBackDTO>(poValue);

                if (string.IsNullOrEmpty(loValue.CCRUD_MODE))
                {
                    _viewModel.lControlTabLOIList = _viewModel.lControlTabLOI = loValue.LCRUD_MODE;
                    if (_viewModel.oLOIList.Any())
                    {
                        _viewModel.lControlTabUnitandCharges = _viewModel.lControlTabDeposit = loValue.LCRUD_MODE;
                    }
                }
                else
                {

                    switch (loValue.CCRUD_MODE)
                    {
                        case "A_ADD":
                            _viewModel.lControlTabLOIList = _viewModel.lControlTabLOI = true;
                            break;

                        case "A_DELETE":
                            _viewModel.oParameter.CDEPT_CODE = "";
                            _viewModel.oParameter.CREF_NO = "";
                            _viewModel.oParameter.CBUILDING_ID = "";

                            _viewModel.lControlTabLOIList = _viewModel.lControlTabLOI = false;
                            break;

                        case "A_CANCEL":

                            _viewModel.lControlTabLOIList = _viewModel.lControlTabLOI = loValue.LCRUD_MODE;
                            if (_viewModel.oLOIList.Any())
                            {
                                _viewModel.lControlTabUnitandCharges = _viewModel.lControlTabDeposit = !string.IsNullOrEmpty(_viewModel.oParameter.CREF_NO) ? loValue.LCRUD_MODE : false;
                            }
                            else
                            {
                                _viewModel.lControlTabUnitandCharges = _viewModel.lControlTabDeposit = false;
                            }
                            break;

                        default:
                            break;
                    }

                }

                await Task.Delay(100);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();


        }


        #endregion

        #region Tab LOI

        private void General_Before_Open_LOI_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModel.oParameter;
            eventArgs.TargetPageType = typeof(PMT01900LOI);
        }

        #endregion

        #region Tab Unit

        private void General_Before_Open_Unit_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModel.oParameter;
            eventArgs.TargetPageType = typeof(PMT01900UnitCharges);
        }

        #endregion

        #region Tab Deposit

        private void General_Before_Open_Deposit_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModel.oParameter;
            eventArgs.TargetPageType = typeof(PMT01900Deposit);
        }

        #endregion

        #endregion

    }
}
