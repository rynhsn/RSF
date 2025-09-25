using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Interfaces;
using PMT01100FrontResources;
using BlazorClientHelper;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls.DataControls;
using PMT01100Model.ViewModel;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Controls.Tab;
using PMT01100Common.DTO._2._LOO._1._LOO___Offer_List;
using R_BlazorFrontEnd.Controls.MessageBox;
using PMT01100Common.Utilities.Front;
using PMT01100Common.Utilities;

namespace PMT01100Front
{
    public partial class PMT01100LOO_OfferList : R_Page, R_ITabPage
    {
        #region Master Page

        readonly PMT01100LOO_OfferListViewModel _viewModel = new();
        R_ConductorGrid? _conductorOfferList;
        R_ConductorGrid? _conductorUnitList;
        R_Grid<PMT01100LOO_OfferList_OfferListDTO>? _gridOfferList;
        R_Grid<PMT01100LOO_OfferList_UnitListDTO>? _gridUnitList;

        [Inject] IJSRuntime? JS { get; set; }
        [Inject] IClientHelper? _clientHelper { get; set; }
        [Inject] R_ILocalizer<Resources_PMT01100_Class>? _localizer { get; set; }
        PMT01100EventCallBackDTO _oEventCallBack = new PMT01100EventCallBackDTO();

        #endregion


        #region Front Control


        private void OnChangeLCONTRACTOR(bool poParam)
        {
            var loException = new R_Exception();

            try
            {
                _viewModel.lFilterCancelled = poParam;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

        }


        private void OnChangedFromOfferDate(DateTime? poValue)
        {
            R_Exception loException = new R_Exception();

            try
            {
                _viewModel.dFilterFromOfferDate = poValue;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }


        #endregion


        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.lControlButton = false;
                _viewModel.oParameter = R_FrontUtility.ConvertObjectToObject<PMT01100ParameterFrontChangePageDTO>(poParameter);
                _viewModel.oParameter.CTRANS_CODE = "802041";
                if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
                {
                    _viewModel.lControlButton = true;
                    await _gridOfferList.R_RefreshGrid(null);
                    if (!string.IsNullOrEmpty(_viewModel.oParameter.ODataUnitList))
                    {
                        await _tabStripRef?.SetActiveTabAsync("Offer")!;
                    }
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





        #region List Building

        private async Task R_ServiceGetListOfferListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetOfferList();
                if (!_viewModel.loListOfferList.Any())
                {
                    _viewModel.loListUnitList.Clear();
                    _viewModel.cBuildingSelectedUnit = "";
                }
                _viewModel.lControlTabUnit = _viewModel.loListOfferList.Any();
                _viewModel.lControlTabDeposit = _viewModel.loListOfferList.Any();

                eventArgs.ListEntityResult = _viewModel.loListOfferList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_DisplayOfferListGetRecord(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            PMT01100LOO_OfferList_OfferListDTO loData = (PMT01100LOO_OfferList_OfferListDTO)eventArgs.Data;

            try
            {
                if (!string.IsNullOrEmpty(loData.CREF_NO))
                {
                    _viewModel.oParameter.CREF_NO = loData.CREF_NO;
                    _viewModel.oParameter.CDEPT_CODE = loData.CDEPT_CODE;
                    _viewModel.oParameter.CBUILDING_ID = loData.CBUILDING_ID;
                    _viewModel.cBuildingSelectedUnit = _viewModel.oParameter.CBUILDING_NAME = loData.CBUILDING_NAME;

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

        private async Task R_ServiceGetListUnitListRecord(R_ServiceGetListRecordEventArgs eventArgs)
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



        #region Master Tab

        private R_TabStrip? _tabStripRef;
        private R_TabPage? _tabOffer;
        private R_TabPage? _tabUnit;
        private R_TabPage? _tabCharges;
        private R_TabPage? _tabDeposit;

        #region Tab OfferList

        private async Task OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            switch (eventArgs.Id)
            {
                case "OfferList":
                    await _gridOfferList.R_RefreshGrid(null);
                    _viewModel.oParameter.ODataUnitList = null;
                    break;
                default:
                    break;
            }

        }

        private async Task OnActiveTabIndexChangingAsync(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            /*
            _view._lComboBoxProperty = true;
            eventArgs.Cancel = _pageAgreementListOnCRUDmode;
            */

            switch (eventArgs.TabStripTab.Id)
            {
                case "OfferList":
                    _oEventCallBack.LUSING_PROPERTY_ID = _oEventCallBack.LCRUD_MODE = true;
                    await InvokeTabEventCallbackAsync(_oEventCallBack);
                    //_viewModel.lControlData = true;
                    break;
                case "Offer":
                    _oEventCallBack.LUSING_PROPERTY_ID = false;
                    _oEventCallBack.LCRUD_MODE = true;
                    await InvokeTabEventCallbackAsync(_oEventCallBack);
                    //_viewModel.lControlData = false;
                    break;
                case "Unit":
                    _oEventCallBack.LUSING_PROPERTY_ID = false;
                    _oEventCallBack.LCRUD_MODE = true;
                    await InvokeTabEventCallbackAsync(_oEventCallBack);
                    //_viewModel.lControlData = false;
                    break;
                case "Deposit":
                    _oEventCallBack.LUSING_PROPERTY_ID = false;
                    _oEventCallBack.LCRUD_MODE = true;
                    await InvokeTabEventCallbackAsync(_oEventCallBack);
                    //_viewModel.lControlData = false;
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

                var loValue = R_FrontUtility.ConvertObjectToObject<PMT01100EventCallBackDTO>(poValue);
                //var loValue = R_FrontUtility.ConvertObjectToObject<PMT01500EventCallBackDTO>(poValue);
                if (string.IsNullOrEmpty(loValue.CCRUD_MODE))
                {
                    _viewModel.lControlTabOfferList = _viewModel.lControlTabOffer = loValue.LCRUD_MODE;
                    if (_viewModel.loListOfferList.Any())
                    {
                        _viewModel.lControlTabUnit = _viewModel.lControlTabDeposit = loValue.LCRUD_MODE;
                    }
                    //_viewModel.oParameter.ODataUnitList = null;
                    await InvokeTabEventCallbackAsync(loValue);
                }
                else
                {

                    switch (loValue.CCRUD_MODE)
                    {
                        case "A_ADD":
                            _viewModel.oParameter.CDEPT_CODE = loValue.ODATA_PARAMETER.CDEPT_CODE;
                            _viewModel.oParameter.CREF_NO = loValue.ODATA_PARAMETER.CREF_NO;
                            _viewModel.oParameter.CBUILDING_ID = loValue.ODATA_PARAMETER.CBUILDING_ID;

                            _viewModel.lControlTabDeposit = _viewModel.lControlTabOfferList = true;
                            break;

                        case "A_DELETE":
                            _viewModel.oParameter.CDEPT_CODE = "";
                            _viewModel.oParameter.CREF_NO = "";
                            _viewModel.oParameter.CBUILDING_ID = "";

                            _viewModel.lControlTabDeposit = _viewModel.lControlTabOfferList = false;
                            break;

                        case "A_CANCEL":
                            _viewModel.oParameter.ODataUnitList = null;

                            _viewModel.lControlTabOfferList = _viewModel.lControlTabOffer = loValue.LCRUD_MODE;
                            if (_viewModel.loListOfferList.Any())
                            {
                                _viewModel.lControlTabUnit = _viewModel.lControlTabDeposit = !string.IsNullOrEmpty(_viewModel.oParameter.CREF_NO) ? loValue.LCRUD_MODE : false;
                            }
                            else
                            {
                                _viewModel.lControlTabUnit = _viewModel.lControlTabDeposit = false;
                            }
                            break;

                        default:
                            _viewModel.oParameter.ODataUnitList = null;
                            break;
                    }

                    _viewModel.oParameter.ODataUnitList = null;
                    await InvokeTabEventCallbackAsync(loValue);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }


        //ini buat Dapet fungsi dari Page 1
        public async Task RefreshTabPageAsync(object poParam)
        {
            R_Exception loException = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01100ParameterFrontChangePageDTO>(poParam);

                _viewModel.oParameter.CPROPERTY_ID = loParam.CPROPERTY_ID;
                _viewModel.oParameter.CBUILDING_ID = loParam.CBUILDING_ID;
                _viewModel.oParameter.CBUILDING_NAME = loParam.CBUILDING_NAME;

                await _gridOfferList.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }


            R_DisplayException(loException);

        }


        #endregion


        #region Tab Offer

        private void General_Before_Open_Offer_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModel.oParameter;
            eventArgs.TargetPageType = typeof(PMT01100LOO_Offer);
        }

        #endregion


        #region Tab Unit

        private void General_Before_Open_Unit_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModel.oParameter;
            eventArgs.TargetPageType = typeof(PMT01100LOO_UnitCharges_UnitCharges);
        }

        #endregion

        #region Tab Deposit

        private void General_Before_Open_Deposit_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModel.oParameter;
            eventArgs.TargetPageType = typeof(PMT01100LOO_Deposit);
        }

        #endregion

        #endregion


    }
}
