using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PMT01100Common.DTO._2._LOO._1._LOO___Offer_List;
using PMT01100Common.DTO._2._LOO._2._LOO___Offer;
using PMT01100Common.DTO._2._LOO._3._LOO___Unit___Charges._1._LOO___Unit___Charges___Unit___Charges;
using PMT01100Common.Utilities;
using PMT01100Common.Utilities.Db;
using PMT01100Common.Utilities.Front;
using PMT01100FrontResources;
using PMT01100Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PMT01100Front
{
    public partial class PMT01100LOO_UnitCharges_UnitCharges : R_Page, R_ITabPage
    {
        #region Master Page

        readonly PMT01100LOO_UnitCharges_UnitChargesViewModel _viewModel = new();
        R_ConductorGrid? _conductorUnitInfo;
        R_ConductorGrid? _conductorCharges;
        R_Grid<PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO>? _gridUnitInfo;
        R_Grid<PMT01100LOO_UnitCharges_UnitCharges_ChargesListDTO>? _gridCharges;

        [Inject] IClientHelper? _clientHelper { get; set; }
        [Inject] R_ILocalizer<Resources_PMT01100_Class>? _localizer { get; set; }


        PMT01100EventCallBackDTO _oEventCallBack = new PMT01100EventCallBackDTO();

        #endregion


        #region Front Control



        #endregion


        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.oParameter = R_FrontUtility.ConvertObjectToObject<PMT01100ParameterFrontChangePageDTO>(poParameter);
                if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
                {
                    await _viewModel.GetUnitChargesHeader();

                    //if (!string.IsNullOrEmpty(_viewModel.oParameter.CREF_NO))
                    if (!string.IsNullOrEmpty(_viewModel.oParameter.CREF_NO))
                    {
                        await _gridUnitInfo.R_RefreshGrid(null);
                    }
                }
                /*
                if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
                {
                    await _gridUnitInfo.R_RefreshGrid(null);
                }

                */

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

        #region Event Call Back

        public Task RefreshTabPageAsync(object poParam)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Conductor Function

        private void R_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO)eventArgs.Data;

                if (string.IsNullOrEmpty(loData.CFLOOR_ID))
                {
                    if (!string.IsNullOrEmpty(loData.CUNIT_ID))
                    {

                    }

                }


                if (string.IsNullOrWhiteSpace(loData.CUNIT_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01100_Class), "ValidationUnit");
                    loEx.Add(loErr);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            eventArgs.Cancel = loEx.HasError;
            loEx.ThrowExceptionIfErrors();
        }


        public async Task AfterDelete()
        {
            _viewModel.lControlTabUtilities = _viewModel.oListUnitInfo.Any();
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }



        private async Task R_SetOther(R_SetEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                //_viewModel.lControlCRUDMode = eventArgs.Enable;
                _oEventCallBack.LCRUD_MODE = eventArgs.Enable;

                _viewModel.lControlTabUtilities = _viewModel.oListUnitInfo.Any() ? eventArgs.Enable : false;
                //_oEventCallBack.CREF_NO = _viewModel.loEntityPMT01500AgreementDetail.CREF_NO!;
                await InvokeTabEventCallbackAsync(_oEventCallBack);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }


        #endregion


        #region Unit List

        private async Task R_ServiceGetListUnitListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetUnitInfoList();
                if (!_viewModel.oListUnitInfo.Any())
                {
                    _viewModel.oListCharges.Clear();
                    //_viewModel.cBuildingSelectedUnit = "";
                }
                _viewModel.lControlTabUtilities = _viewModel.oListUnitInfo.Any();

                eventArgs.ListEntityResult = _viewModel.oListUnitInfo;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_DisplayUnitListGetRecord(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO loData = (PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO)eventArgs.Data;

            try
            {
                if (eventArgs.Data != null)
                {
                    if (!string.IsNullOrEmpty(loData.CUNIT_ID))
                    {
                        _viewModel.oParameterChargesList = R_FrontUtility.ConvertObjectToObject<PMT01100UtilitiesParameterChargesListDTO>(loData);
                        _viewModel.oParameterUtilitiesPage = R_FrontUtility.ConvertObjectToObject<PMT01100UtilitiesParameterUtilitiesListDTO>(loData);
                        _viewModel.oParameterChargesList.CTRANS_CODE = _viewModel.oParameterUtilitiesPage.CTRANS_CODE = _viewModel.oParameter.CTRANS_CODE;
                        _viewModel.oParameterChargesList.CDEPT_CODE = _viewModel.oParameterUtilitiesPage.CDEPT_CODE = _viewModel.oParameter.CDEPT_CODE;
                        await _gridCharges.R_RefreshGrid(null);
                    }

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        #region Master CRUD


        private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO loParam;

            try
            {
                loParam = new PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO();
                if (eventArgs.Data != null)
                {
                    loParam = R_FrontUtility.ConvertObjectToObject<PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO>(eventArgs.Data);
                    loParam.CDEPT_CODE = _viewModel.oParameter.CDEPT_CODE;

                }
                else
                {
                    loParam.CREF_NO = _viewModel.oParameter.CREF_NO;
                    loParam.CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID;
                    loParam.CDEPT_CODE = _viewModel.oParameter.CDEPT_CODE;
                    loParam.CTRANS_CODE = _viewModel.oParameter.CTRANS_CODE;
                    loParam.CCOMPANY_ID = _clientHelper.CompanyId;
                    loParam.CUSER_ID = _clientHelper.UserId;
                };
                await _viewModel.GetEntity(loParam);
                /*
                if (_viewModelPMT01500Agreement.loEntityPMT01500AgreementDetail.DEND_DATE != null)
                {
                    OnChangedDEND_DATE(_viewModelPMT01500Agreement.loEntityPMT01500AgreementDetail.DEND_DATE);
                }
                */

                eventArgs.Result = _viewModel.oEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO>(eventArgs.Data);

                switch (eventArgs.ConductorMode)
                {
                    case R_eConductorMode.Normal:
                        break;
                    case R_eConductorMode.Add:
                        loParam.CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID;
                        loParam.CBUILDING_ID = _viewModel.oParameter.CBUILDING_ID;
                        loParam.CTRANS_CODE = _viewModel.oParameter.CTRANS_CODE;
                        loParam.CDEPT_CODE = _viewModel.oParameter.CDEPT_CODE;
                        loParam.CREF_NO = _viewModel.oParameter.CREF_NO;
                        break;
                    case R_eConductorMode.Edit:
                        loParam.CTRANS_CODE = _viewModel.oParameter.CTRANS_CODE;
                        loParam.CDEPT_CODE = _viewModel.oParameter.CDEPT_CODE;
                        break;
                    default:
                        break;
                }

                await _viewModel.ServiceSave(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel.oEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO)eventArgs.Data;

                await _viewModel.GetEntity(loData);

                if (_viewModel.oEntity != null)
                    await _viewModel.ServiceDelete(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion


        #endregion


        #region Charges List

        private async Task R_ServiceGetListChargesListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetChargesList();
                //_viewModel.lControlTabUtilities = _viewModel.oListCharges.Any();
                eventArgs.ListEntityResult = _viewModel.oListCharges;
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
        private R_TabPage? _tabUtilities;


        #region Tab OfferList

        private async Task OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            switch (eventArgs.Id)
            {
                case "OfferList":
                    await _gridUnitInfo.R_RefreshGrid(null);
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
                case "UnitAndCharges":
                    //_viewModel.lControlData = true;
                    break;
                case "Utilities":
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
                    _viewModel.lControlTabUnitAndCharges = _viewModel.lControlTabUtilities = loValue.LCRUD_MODE;
                    await InvokeTabEventCallbackAsync(loValue);
                }
                //var loValue = R_FrontUtility.ConvertObjectToObject<PMT01500EventCallBackDTO>(poValue);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }

        #endregion


        #region Tab Utilities

        private void General_Before_Open_Utilities_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModel.oParameterUtilitiesPage;
            eventArgs.TargetPageType = typeof(PMT01100LOO_UnitCharges_Utilities);
        }

        #endregion


        #endregion


        #region Master LookUp

        private void Before_Open_LookupUnitId(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var param = new GSL02300ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID!,
                    CBUILDING_ID = _viewModel.oParameter.CBUILDING_ID!,
                    CFLOOR_ID = "",
                };
                eventArgs.Parameter = param;
                eventArgs.TargetPageType = typeof(GSL02300);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }

        private void After_Open_LookupUnitId(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loTempResult = (GSL02300DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;
                var loGetData = (PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO)eventArgs.ColumnData;
                loGetData.CUNIT_ID = loTempResult.CUNIT_ID;
                loGetData.CUNIT_NAME = loTempResult.CUNIT_NAME;
                loGetData.CFLOOR_ID = loTempResult.CFLOOR_ID;
                //loGetData.CFLOOR_NAME = loTempResult.CFLOOR_NAME;
                //loGetData.NACTUAL_AREA_SIZE = loTempResult.NACTUAL_AREA_SIZE;
                loGetData.CUNIT_TYPE_NAME = loTempResult.CUNIT_TYPE_NAME;
                loGetData.NNET_AREA_SIZE = loTempResult.NNET_AREA_SIZE;
                loGetData.NGROSS_AREA_SIZE = loTempResult.NGROSS_AREA_SIZE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_CellLostFocusUnitId(R_CellLostFocusedEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO loGetData = (PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO)eventArgs.CurrentRow;

                if (eventArgs.ColumnName == nameof(PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO.CUNIT_ID))
                {
                    if (!string.IsNullOrWhiteSpace((string)eventArgs.Value))
                    {
                        LookupGSL02300ViewModel loLookupViewModel = new LookupGSL02300ViewModel();
                        var param = new GSL02300ParameterDTO
                        {
                            CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID!,
                            CBUILDING_ID = _viewModel.oParameter.CBUILDING_ID!,
                            CFLOOR_ID = "",
                            CSEARCH_TEXT = (string)eventArgs.Value,
                        };
                        var loResult = await loLookupViewModel.GetBuildingUnit(param);

                        if (loResult == null)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                    "_ErrLookup01"));
                            loGetData.CUNIT_ID = "";
                            loGetData.CUNIT_NAME = "";
                            loGetData.CFLOOR_ID = "";
                            //loGetData.CFLOOR_NAME = "";
                            //loGetData.NACTUAL_AREA_SIZE = 0;
                            loGetData.CUNIT_TYPE_NAME = "";
                            loGetData.NNET_AREA_SIZE = 0;
                            loGetData.NGROSS_AREA_SIZE = 0;
                        }
                        else
                        {
                            loGetData.CUNIT_ID = loResult.CUNIT_ID;
                            loGetData.CUNIT_NAME = loResult.CUNIT_NAME;
                            loGetData.CFLOOR_ID = loResult.CFLOOR_ID;
                            loGetData.CUNIT_TYPE_NAME = loResult.CUNIT_TYPE_NAME;
                            //loGetData.CFLOOR_NAME = loResult.CFLOOR_NAME;
                            loGetData.NNET_AREA_SIZE = loResult.NNET_AREA_SIZE;
                            loGetData.NGROSS_AREA_SIZE = loResult.NGROSS_AREA_SIZE;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

    }
}
