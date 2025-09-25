using BaseAOC_BS11Common.DTO.Request.Context;
using BaseAOC_BS11Common.DTO.Request.Request.GridList;
using BaseAOC_BS11Common.DTO.Request.Request.List;
using BaseAOC_BS11Common.DTO.Response.GridList;
using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00400;
using Microsoft.AspNetCore.Components;
using PMT01900Common.DTO.CRUDBase;
using PMT01900Common.DTO.Front;
using PMT01900FrontResources;
using PMT01900Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
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

namespace PMT01900Front
{
    public partial class PMT01900UnitCharges : R_Page
    {
        #region Master Page

        #region Unit List

        readonly PMT01900UnitCharges_UnitViewModel _viewModel = new();
        R_ConductorGrid? _conductorUnitInfo;
        R_Grid<BaseAOCResponseAgreementUnitInfoListDTO>? _gridUnitInfo;

        #endregion

        #region Utilities 

        readonly PMT01900Unit_UtilitiesViewModel _viewModelUtilities = new();
        R_Conductor? _conductorUtilities;
        R_Grid<BaseAOCResponseAgreementUtilitiesListDTO>? _gridUtilities;

        #endregion

        [Inject] IClientHelper? _clientHelper { get; set; }
        [Inject] R_ILocalizer<Resources_PMT01900_Class>? _localizer { get; set; }


        PMT01900EventCallBackDTO _oEventCallBack = new PMT01900EventCallBackDTO();

        #endregion

        #region Front Control

        bool _hasDataUnit = false;


        #endregion

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.oParameter = R_FrontUtility.ConvertObjectToObject<PMT01900ParameterFrontChangePageDTO>(poParameter);
                if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
                {
                    await _viewModel.GetUnitChargesHeader();
                    await _viewModelUtilities.GetComboBoxDataCCHARGES_TYPE();

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

        #region Event Call Back

        public Task RefreshTabPageAsync(object poParam)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Unit List

        #region Conductor Function

        private void R_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                _hasDataUnit = _viewModel.oListUnitInfo.Any();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (BaseAOCResponseAgreementUnitInfoListDTO)eventArgs.Data;

                if (string.IsNullOrEmpty(loData.CFLOOR_ID))
                {
                    if (!string.IsNullOrEmpty(loData.COTHER_UNIT_ID))
                    {

                    }

                }


                if (string.IsNullOrWhiteSpace(loData.COTHER_UNIT_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01900_Class), "ValidationOtherUnit");
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
            _viewModel.lControlTabCharges = _hasDataUnit = _viewModel.oListUnitInfo.Any();
            await _gridUnitInfo.R_RefreshGrid(null);
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }

        private async Task R_SetOther(R_SetEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                //_viewModel.lControlCRUDMode = eventArgs.Enable;
                _oEventCallBack.LCRUD_MODE = eventArgs.Enable;

                _viewModel.lControlTabCharges = _viewModel.oListUnitInfo.Any() ? eventArgs.Enable : false;
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
                _hasDataUnit = _viewModel.oListUnitInfo.Any();
                _viewModel.lControlTabCharges = _viewModel.oListUnitInfo.Any();
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
            var loData = (BaseAOCResponseAgreementUnitInfoListDTO)eventArgs.Data;
            loData.CTRANS_CODE = _viewModel.oParameter.CTRANS_CODE!;
            loData.CDEPT_CODE = _viewModel.oParameter.CDEPT_CODE!;
            try
            {
                if (eventArgs.Data != null)
                {
                    if (!string.IsNullOrEmpty(loData.COTHER_UNIT_ID))
                    {
                        _viewModel.oParameterChargesList = R_FrontUtility.ConvertObjectToObject<PMT01900ParameterFrontChangePageToChargesDTO>(loData);
                        //parameter to utiliies
                        _viewModelUtilities.oParameterUtilitiesList = R_FrontUtility.ConvertObjectToObject<BaseAOCParameterRequestGetAgreementUtilitiesListDTO>(loData);
                        _viewModelUtilities.oParameterUtilitiesList.CUNIT_ID = loData.COTHER_UNIT_ID;

                        _viewModel.oParameterChargesList.CTRANS_CODE = _viewModelUtilities.oParameterUtilitiesList.CTRANS_CODE = _viewModel.oParameter.CTRANS_CODE;
                        _viewModel.oParameterChargesList.CDEPT_CODE = _viewModelUtilities.oParameterUtilitiesList.CDEPT_CODE = _viewModel.oParameter.CDEPT_CODE;
                        await _gridUtilities.R_RefreshGrid(null);
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
            PMT01900Unit_UnitInfoDetailDTO loParam;

            try
            {
                loParam = new PMT01900Unit_UnitInfoDetailDTO();
                if (eventArgs.Data != null)
                {
                    loParam = R_FrontUtility.ConvertObjectToObject<PMT01900Unit_UnitInfoDetailDTO>(eventArgs.Data);
                    loParam.CDEPT_CODE = _viewModel.oParameter.CDEPT_CODE!;

                }
                else
                {
                    loParam.CREF_NO = _viewModel.oParameter.CREF_NO!;
                    loParam.CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID!;
                    loParam.CDEPT_CODE = _viewModel.oParameter.CDEPT_CODE!;
                    loParam.CTRANS_CODE = _viewModel.oParameter.CTRANS_CODE!;
                    loParam.CCOMPANY_ID = _clientHelper.CompanyId;
                    loParam.CUSER_ID = _clientHelper.UserId;
                };
                await _viewModel.GetEntity(loParam);

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
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01900Unit_UnitInfoDetailDTO>(eventArgs.Data);

                switch (eventArgs.ConductorMode)
                {
                    case R_eConductorMode.Normal:
                        break;
                    case R_eConductorMode.Add:
                        loParam.CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID!;
                        loParam.CBUILDING_ID = _viewModel.oParameter.CBUILDING_ID!;
                        loParam.CTRANS_CODE = _viewModel.oParameter.CTRANS_CODE!;
                        loParam.CDEPT_CODE = _viewModel.oParameter.CDEPT_CODE!;
                        loParam.CREF_NO = _viewModel.oParameter.CREF_NO!;
                        break;
                    case R_eConductorMode.Edit:
                        loParam.CTRANS_CODE = _viewModel.oParameter.CTRANS_CODE!;
                        loParam.CDEPT_CODE = _viewModel.oParameter.CDEPT_CODE!;
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
                var loData = (BaseAOCResponseAgreementUnitInfoListDTO)eventArgs.Data;
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01900Unit_UnitInfoDetailDTO>(loData);
                if (_viewModel.oEntity != null)
                    await _viewModel.ServiceDelete(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion


        #endregion

        #region Master LookUp

        private void Before_Open_LookupOtherUnitId(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var param = new GSL02700ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID!,
                    CBUILDING_ID = _viewModel.oParameter.CBUILDING_ID!,
                    LEVENT = true
                };
                eventArgs.Parameter = param;
                eventArgs.TargetPageType = typeof(GSL02700);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }

        private void After_Open_LookupOtherUnitId(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loTempResult = (GSL02700DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;
                var loGetData = (BaseAOCResponseAgreementUnitInfoListDTO)eventArgs.ColumnData;
                loGetData.COTHER_UNIT_ID = loTempResult.COTHER_UNIT_ID;
                loGetData.COTHER_UNIT_NAME = loTempResult.COTHER_UNIT_NAME;
                loGetData.CFLOOR_ID = loTempResult.CFLOOR_ID;
                loGetData.CLOCATION = loTempResult.CLOCATION;
                loGetData.CUNIT_TYPE_ID = loTempResult.COTHER_UNIT_TYPE_ID;
                loGetData.COTHER_UNIT_TYPE_NAME = loTempResult.COTHER_UNIT_TYPE_NAME;
                loGetData.NNET_AREA_SIZE = loTempResult.NNET_AREA_SIZE;
                loGetData.NGROSS_AREA_SIZE = loTempResult.NGROSS_AREA_SIZE;
                //   loGetData.NACTUAL_AREA_SIZE = loTempResult.N;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_CellLostFocusOtherUnitId(R_CellLostFocusedEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                BaseAOCResponseAgreementUnitInfoListDTO loGetData = (BaseAOCResponseAgreementUnitInfoListDTO)eventArgs.CurrentRow;

                if (eventArgs.ColumnName == nameof(BaseAOCResponseAgreementUnitInfoListDTO.CUNIT_ID))
                {
                    if (!string.IsNullOrWhiteSpace((string)eventArgs.Value))
                    {
                        LookupGSL02700ViewModel loLookupViewModel = new();
                        var param = new GSL02700ParameterDTO
                        {
                            CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID!,
                            CBUILDING_ID = _viewModel.oParameter.CBUILDING_ID!,
                            LEVENT = true,
                            CSEARCH_TEXT = (string)eventArgs.Value,
                        };
                        var loResult = await loLookupViewModel.GetOtherUnit(param);

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
                            loGetData.CUNIT_TYPE_ID = "";
                            loGetData.COTHER_UNIT_TYPE_NAME = "";
                            loGetData.NNET_AREA_SIZE = 0;
                            loGetData.NGROSS_AREA_SIZE = 0;
                        }
                        else
                        {
                            loGetData.COTHER_UNIT_ID = loResult.COTHER_UNIT_ID;
                            loGetData.COTHER_UNIT_NAME = loResult.COTHER_UNIT_NAME;
                            loGetData.CFLOOR_ID = loResult.CFLOOR_ID;
                            loGetData.CLOCATION = loResult.CLOCATION;
                            loGetData.CUNIT_TYPE_ID = loResult.COTHER_UNIT_TYPE_ID;
                            loGetData.COTHER_UNIT_TYPE_NAME = loResult.COTHER_UNIT_TYPE_NAME;
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

        #endregion

        #region Utilities

        #region Conductor Function

        public async Task AfterDeleteUtilities()
        {
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }

        private async Task R_SetOtherUtilities(R_SetEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                //_viewModelUtilities.lControlCRUDMode = eventArgs.Enable;
                _oEventCallBack.LCRUD_MODE = _viewModel.lControlTabCharges = eventArgs.Enable;
                await InvokeTabEventCallbackAsync(_oEventCallBack);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private void R_ValidationUtilities(R_ValidationEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                var loData = (PMT01900Unit_UtilitiesDetailDTO)eventArgs.Data;

                if (string.IsNullOrWhiteSpace(loData.CCHARGES_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01900_Class), "ValidationCharge");
                    loException.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(loData.CMETER_NO))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01900_Class), "ValidationMeterNo");
                    loException.Add(loErr);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            eventArgs.Cancel = loException.HasError;


            loException.ThrowExceptionIfErrors();
        }

        private async Task AfterAddUtilities(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                var loData = (PMT01900Unit_UtilitiesDetailDTO)eventArgs.Data;
                loData.CCHARGES_TYPE = _viewModelUtilities.oComboBoxDataCCHARGES_TYPE.FirstOrDefault().CCODE;

                var loParam = new BaseAOCParameterRequestUtilitiesCMeterNoDTO()
                {
                    CPROPERTY_ID = _viewModelUtilities.oParameterUtilitiesList.CPROPERTY_ID,
                    CBUILDING_ID = _viewModelUtilities.oParameterUtilitiesList.CBUILDING_ID,
                    CFLOOR_ID = _viewModelUtilities.oParameterUtilitiesList.CFLOOR_ID,
                    CUNIT_ID = _viewModelUtilities.oParameterUtilitiesList.CUNIT_ID,
                    CUTILITY_TYPE = loData.CCHARGES_TYPE,
                };
                bool llResult = await _viewModelUtilities.GetComboBoxDataCMETER_NO(loParam);
                loData.CMETER_NO = llResult ? _viewModelUtilities.oComboBoxDataCMETER_NO.FirstOrDefault().CMETER_NO : "";
                loData.NCAPACITY = llResult ? _viewModelUtilities.oComboBoxDataCMETER_NO.FirstOrDefault().NCAPACITY : 0;
                loData.NCALCULATION_FACTOR = llResult ? _viewModelUtilities.oComboBoxDataCMETER_NO.FirstOrDefault().NCALCULATION_FACTOR : 0;
                // loData.CTA = llResult ? _viewModelUtilities.oComboBoxDataCMETER_NO.FirstOrDefault().NCALCULATION_FACTOR : 0;

                _lControlCMeterNo = true;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        #endregion

        #region External Handle Function

        bool _lControlCMeterNo;

        private async Task OnChangedCCHARGES_TYPE(string pcParam, string pcMode = "")
        {
            R_Exception loException = new R_Exception();
            BaseAOCParameterRequestUtilitiesCMeterNoDTO? loParam;

            try
            {
                var loDataParameter = _viewModelUtilities.oParameterUtilitiesList;
                var loData = _viewModelUtilities.Data;
                loData.CCHARGES_TYPE = pcParam;

                loData.CCHARGES_ID = "";
                loData.CCHARGES_NAME = "";

                loParam = new()
                {
                    CPROPERTY_ID = loDataParameter.CPROPERTY_ID,
                    CBUILDING_ID = loDataParameter.CBUILDING_ID,
                    CFLOOR_ID = loDataParameter.CFLOOR_ID,
                    CUNIT_ID = loDataParameter.CUNIT_ID,
                    CUTILITY_TYPE = loData.CCHARGES_TYPE
                };
                var llResult = await _viewModelUtilities.GetComboBoxDataCMETER_NO(loParam);

                if (llResult)
                {
                    _lControlCMeterNo = true;
                    if (string.IsNullOrEmpty(pcMode))
                    {
                        loData.CMETER_NO = _viewModelUtilities.oComboBoxDataCMETER_NO.FirstOrDefault().CMETER_NO;
                        loData.NCALCULATION_FACTOR = _viewModelUtilities.oComboBoxDataCMETER_NO.FirstOrDefault().NCALCULATION_FACTOR;
                        loData.NCAPACITY = _viewModelUtilities.oComboBoxDataCMETER_NO.FirstOrDefault().NCAPACITY;
                    }
                    else if (pcMode == "DISPLAY")
                    {
                        _lControlCMeterNo = false;
                    }
                    else if (pcMode == "EDIT")
                    {
                        _lControlCMeterNo = true;
                    }
                }
                else
                {
                    loData.CMETER_NO = "";
                    loData.NCALCULATION_FACTOR = 0;
                    loData.NCAPACITY = 0;
                    _lControlCMeterNo = false;

                    if (string.IsNullOrEmpty(pcMode))
                    {
                        var loValidate = await R_MessageBox.Show("", _localizer["ValidationDataMeterNo"], R_eMessageBoxButtonType.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            R_DisplayException(loException);
        }

        private void OnChangedCMETER_NO(string pcParam)
        {
            R_Exception loException = new R_Exception();

            try
            {

                var loData = _viewModelUtilities.Data;

                loData.CMETER_NO = pcParam;
                loData.NCAPACITY = _viewModelUtilities.oComboBoxDataCMETER_NO
                    .FirstOrDefault(x => x.CMETER_NO == loData.CMETER_NO)
                .NCAPACITY;

                loData.NCALCULATION_FACTOR = _viewModelUtilities.oComboBoxDataCMETER_NO
                    .FirstOrDefault(x => x.CMETER_NO == loData.CMETER_NO)
                    .NCALCULATION_FACTOR;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            R_DisplayException(loException);

        }

        #endregion

        #region Utilities List

        private async Task R_ServiceGetListUtilitiesListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelUtilities.GetUtilitiesList();
                eventArgs.ListEntityResult = _viewModelUtilities.oListUtilities;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Master CRUD

        private async Task ServiceGetUtilitiesRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            PMT01900Unit_UtilitiesDetailDTO loParam;

            try
            {
                loParam = new PMT01900Unit_UtilitiesDetailDTO();
                _viewModelUtilities.LTAXABLE = false;
                if (eventArgs.Data != null)
                {
                    loParam = R_FrontUtility.ConvertObjectToObject<PMT01900Unit_UtilitiesDetailDTO>(eventArgs.Data);
                }
                else
                {
                    loParam.CREF_NO = _viewModelUtilities.oParameterUtilitiesList.CREF_NO;
                    loParam.CPROPERTY_ID = _viewModelUtilities.oParameterUtilitiesList.CPROPERTY_ID;
                    loParam.CDEPT_CODE = _viewModelUtilities.oParameterUtilitiesList.CDEPT_CODE;
                    loParam.CTRANS_CODE = _viewModelUtilities.oParameterUtilitiesList.CTRANS_CODE;
                    loParam.CCOMPANY_ID = _clientHelper.CompanyId;
                    loParam.CUSER_ID = _clientHelper.UserId;
                };
                await _viewModelUtilities.GetEntity(loParam);

                if (_viewModelUtilities.oComboBoxDataCCHARGES_TYPE.Any())
                {
                    var loParamMeterNo = new BaseAOCParameterRequestUtilitiesCMeterNoDTO()
                    {
                        CPROPERTY_ID = _viewModelUtilities.oParameterUtilitiesList.CPROPERTY_ID,
                        CBUILDING_ID = _viewModelUtilities.oParameterUtilitiesList.CBUILDING_ID,
                        CFLOOR_ID = _viewModelUtilities.oParameterUtilitiesList.CFLOOR_ID,
                        CUNIT_ID = _viewModelUtilities.oParameterUtilitiesList.CUNIT_ID,
                        CUTILITY_TYPE = _viewModelUtilities.oEntity.CCHARGES_TYPE,
                    };
                    bool llResult = await _viewModelUtilities.GetComboBoxDataCMETER_NO(loParamMeterNo);
                }

                eventArgs.Result = _viewModelUtilities.oEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceSaveUtilities(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01900Unit_UtilitiesDetailDTO>(eventArgs.Data);

                if ((eCRUDMode)eventArgs.ConductorMode == eCRUDMode.AddMode)
                {
                    var loDataUnit = (BaseAOCResponseAgreementUnitInfoListDTO)_conductorUnitInfo.R_GetCurrentData();
                    loParam.CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID;
                    loParam.CDEPT_CODE = _viewModel.oParameter.CDEPT_CODE;
                    loParam.CTRANS_CODE = _viewModel.oParameter.CTRANS_CODE;
                    loParam.CREF_NO = _viewModel.oParameter.CREF_NO;
                    loParam.CUNIT_ID = _viewModelUtilities.oParameterUtilitiesList.CUNIT_ID; //OTHER UNIT
                    loParam.CFLOOR_ID = _viewModelUtilities.oParameterUtilitiesList.CFLOOR_ID;
                    loParam.CBUILDING_ID = _viewModel.oParameter.CBUILDING_ID;
                }

                await _viewModelUtilities.ServiceSave(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModelUtilities.oEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceDeleteUtilities(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01900Unit_UtilitiesDetailDTO)eventArgs.Data;

                await _viewModelUtilities.GetEntity(loData);

                if (_viewModelUtilities.oEntity != null)
                    await _viewModelUtilities.ServiceDelete(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #endregion

        #region LookupCharge

        private R_Lookup? R_Lookup_Charge;

        private void BeforeOpenLookUp_ChargeID(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var param = new LML00400ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _viewModelUtilities.oParameterUtilitiesList.CPROPERTY_ID!,
                    CCHARGE_TYPE_ID = _viewModelUtilities.Data.CCHARGES_TYPE!,
                    CUSER_ID = _clientHelper.UserId
                };
                eventArgs.Parameter = param;
                eventArgs.TargetPageType = typeof(LML00400);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }

        private void AfterOpenLookUp_ChargeID(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00400DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            var loGetData = (PMT01900Unit_UtilitiesDetailDTO)_conductorUtilities.R_GetCurrentData();

            loGetData.CCHARGES_ID = loTempResult.CCHARGES_ID;
            loGetData.CCHARGES_NAME = loTempResult.CCHARGES_NAME;
            _viewModelUtilities.LTAXABLE = loTempResult.LTAXABLE;
        }

        private async Task OnLostFocusChargeID()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loGetData = _viewModelUtilities.Data;

                if (string.IsNullOrWhiteSpace(_viewModelUtilities.Data.CCHARGES_ID))
                {
                    loGetData.CCHARGES_ID = "";
                    loGetData.CCHARGES_NAME = "";
                    return;
                }

                LookupLML00400ViewModel loLookupViewModel = new LookupLML00400ViewModel();
                LML00400ParameterDTO loParam = new LML00400ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _viewModelUtilities.oParameterUtilitiesList.CPROPERTY_ID!,
                    CCHARGE_TYPE_ID = _viewModelUtilities.Data.CCHARGES_TYPE!,
                    CUSER_ID = _clientHelper.UserId,
                    CSEARCH_TEXT = loGetData.CCHARGES_ID ?? "",
                };

                var loResult = await loLookupViewModel.GetUtitlityCharges(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CCHARGES_ID = "";
                    loGetData.CCHARGES_NAME = "";
                    _viewModelUtilities.LTAXABLE = false;
                }
                else
                {
                    loGetData.CCHARGES_ID = loResult.CCHARGES_ID;
                    loGetData.CCHARGES_NAME = loResult.CCHARGES_NAME;
                    _viewModelUtilities.LTAXABLE = loResult.LTAXABLE;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion
        #region LookupCharge

        private R_Lookup? R_Lookup_Tax;

        private void BeforeOpenLookUp_Tax(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var param = new GSL00110ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CTAX_DATE = DateTime.Now.ToString("yyyyMMdd")
                };
                eventArgs.Parameter = param;
                eventArgs.TargetPageType = typeof(GSL00110);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }

        private void AfterOpenLookUp_Tax(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00110DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            var loGetData = (PMT01900Unit_UtilitiesDetailDTO)_conductorUtilities.R_GetCurrentData();

            loGetData.CTAX_ID = loTempResult.CTAX_ID;
            loGetData.CTAX_NAME = loTempResult.CTAX_NAME;

        }

        private async Task OnLostFocusTax()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loGetData = _viewModelUtilities.Data;

                if (string.IsNullOrWhiteSpace(_viewModelUtilities.Data.CTAX_ID))
                {
                    loGetData.CTAX_ID = "";
                    loGetData.CTAX_NAME = "";
                    return;
                }

                LookupGSL00110ViewModel loLookupViewModel = new LookupGSL00110ViewModel();
                var loParam = new GSL00110ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CTAX_DATE = DateTime.Now.ToString("yyyyMMdd"),
                    CSEARCH_TEXT = loGetData.CTAX_ID ?? ""
                };

                var loResult = await loLookupViewModel.GetTaxByDate(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CTAX_ID = "";
                    loGetData.CTAX_NAME = "";
                }
                else
                {
                    loGetData.CTAX_ID = loResult.CTAX_ID;
                    loGetData.CTAX_NAME = loResult.CTAX_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #endregion

        #region Master Tab

        private R_TabStrip? _tabStripRef;
        private R_TabPage? _tabCharges;


        #region Tab OfferList

        private async Task OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            switch (eventArgs.Id)
            {
                case "UnitAndUtilities":
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
                case "UnitAndUtilities":
                    //_viewModel.lControlData = true;
                    break;
                case "Charges":
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

                var loValue = R_FrontUtility.ConvertObjectToObject<PMT01900EventCallBackDTO>(poValue);
                //var loValue = R_FrontUtility.ConvertObjectToObject<PMT01500EventCallBackDTO>(poValue);
                if (string.IsNullOrEmpty(loValue.CCRUD_MODE))
                {
                    _viewModel.lControlTabUnitAndUtilities = _viewModel.lControlTabCharges = loValue.LCRUD_MODE;
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


        #region Tab Charges

        private void General_Before_Open_Charges_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            _viewModel.oParameterChargesList.CSTART_DATE = _viewModel.oHeaderEntity.CSTART_DATE;
            _viewModel.oParameterChargesList.CEND_DATE = _viewModel.oHeaderEntity.CEND_DATE;
            _viewModel.oParameterChargesList.CREF_DATE = _viewModel.oHeaderEntity.CREF_DATE;
            _viewModel.oParameterChargesList.NTOTAL_ACTUAL_AREA = _viewModel.oHeaderEntity.NTOTAL_ACTUAL_AREA;
            _viewModel.oParameterChargesList.CUNIT_ID = _viewModelUtilities.oParameterUtilitiesList.CUNIT_ID;
            _viewModel.oParameterChargesList.IYEARS = _viewModel.oHeaderEntity.IYEARS;
            _viewModel.oParameterChargesList.IMONTHS = _viewModel.oHeaderEntity.IMONTHS;
            _viewModel.oParameterChargesList.IDAYS = _viewModel.oHeaderEntity.IDAYS;
            eventArgs.Parameter = _viewModel.oParameterChargesList;
            eventArgs.TargetPageType = typeof(PMT01900UnitCharges_Charges);
        }

        #endregion


        #endregion

    }
}
