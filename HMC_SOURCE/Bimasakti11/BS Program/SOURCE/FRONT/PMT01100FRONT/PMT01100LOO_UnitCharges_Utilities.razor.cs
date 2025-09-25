using BlazorClientHelper;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00400;
using Microsoft.AspNetCore.Components;
using PMT01100Common.DTO._2._LOO._3._LOO___Unit___Charges._2._LOO___Unit___Charges___Utilities;
using PMT01100Common.Utilities;
using PMT01100Common.Utilities.Db;
using PMT01100FrontResources;
using PMT01100Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using System;

namespace PMT01100Front
{
    public partial class PMT01100LOO_UnitCharges_Utilities : R_Page
    {
        #region Master Page

        readonly PMT01100LOO_UnitCharges_UtilitiesViewModel _viewModel = new();
        R_ConductorGrid? _conductorUtilities;
        R_Grid<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO>? _gridUtilities;

        PMT01100EventCallBackDTO _oEventCallBack = new PMT01100EventCallBackDTO();
        [Inject] IClientHelper? _clientHelper { get; set; }
        [Inject] R_ILocalizer<Resources_PMT01100_Class>? _localizer { get; set; }

        #endregion


        #region Conductor Function

        string? tempCCHARGES_TYPE = "";

        private async Task R_CellValueChangedAsync(R_CellValueChangedEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                var loData = eventArgs.CurrentRow as PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO;
                switch (eventArgs.ColumnName)
                {
                    case nameof(PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO.CCHARGES_TYPE):
                        loData.CCHARGES_TYPE = eventArgs.Value as string;
                        tempCCHARGES_TYPE = eventArgs.Value as string;
                        loData.CCHARGES_ID = "";
                        loData.CCHARGES_NAME = "";

                        var loParam = new PMT01100Common.Utilities.Request.PMT01100RequestUtilitiesCMeterNoParameterDTO()
                        {
                            CPROPERTY_ID = _viewModel.oParameterUtilitiesList.CPROPERTY_ID,
                            CBUILDING_ID = _viewModel.oParameterUtilitiesList.CBUILDING_ID,
                            CFLOOR_ID = _viewModel.oParameterUtilitiesList.CFLOOR_ID,
                            CUNIT_ID = _viewModel.oParameterUtilitiesList.CUNIT_ID,
                            CUTILITY_TYPE = eventArgs.Value as string,
                        };

                        bool llResult = await _viewModel.GetComboBoxDataCMETER_NO(loParam);

                        var loContactNameColumn = eventArgs.Columns
                            .FirstOrDefault(x => x.FieldName == nameof(PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO.CMETER_NO));

                        loData.CMETER_NO = llResult ? _viewModel.oComboBoxDataCMETER_NO.FirstOrDefault().CMETER_NO : null;
                        loData.NCAPACITY = llResult ? _viewModel.oComboBoxDataCMETER_NO.FirstOrDefault().NCAPACITY : 0;
                        loData.NCALCULATION_FACTOR = llResult ? _viewModel.oComboBoxDataCMETER_NO.FirstOrDefault().NCALCULATION_FACTOR : 0;
                        loContactNameColumn.Enabled = llResult;
                        break;

                    case nameof(PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO.CMETER_NO):
                        loData.CMETER_NO = eventArgs.Value as string;
                        loData.NCAPACITY = _viewModel.oComboBoxDataCMETER_NO
                            .FirstOrDefault(x => x.CMETER_NO == loData.CMETER_NO)
                            .NCAPACITY;

                        loData.NCALCULATION_FACTOR = _viewModel.oComboBoxDataCMETER_NO
                            .FirstOrDefault(x => x.CMETER_NO == loData.CMETER_NO)
                            .NCALCULATION_FACTOR;
                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);


        }

        public async Task AfterDelete()
        {
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }

        private async Task R_SetOther(R_SetEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                //_viewModel.lControlCRUDMode = eventArgs.Enable;
                _oEventCallBack.LCRUD_MODE = eventArgs.Enable;
                await InvokeTabEventCallbackAsync(_oEventCallBack);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private void ServiceR_Display(R_DisplayEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                var loData = (PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO)eventArgs.Data;
                switch (eventArgs.ConductorMode)
                {

                    case R_eConductorMode.Edit:
                        tempCCHARGES_TYPE = loData.CCHARGES_TYPE;
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private void R_Validation(R_ValidationEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                var loData = (PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO)eventArgs.Data;
                //await LostFocusCUNIT_ID();


                if (string.IsNullOrWhiteSpace(loData.CCHARGES_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01100_Class), "ValidationCharge");
                    loException.Add(loErr);
                }

                if (string.IsNullOrWhiteSpace(loData.CMETER_NO))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01100_Class), "ValidationMeterNo");
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

        private async Task AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                var loData = (PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO)eventArgs.Data;
                loData.CCHARGES_TYPE = tempCCHARGES_TYPE = _viewModel.oComboBoxDataCCHARGES_TYPE.FirstOrDefault().CCODE;

                var loParam = new PMT01100Common.Utilities.Request.PMT01100RequestUtilitiesCMeterNoParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.oParameterUtilitiesList.CPROPERTY_ID,
                    CBUILDING_ID = _viewModel.oParameterUtilitiesList.CBUILDING_ID,
                    CFLOOR_ID = _viewModel.oParameterUtilitiesList.CFLOOR_ID,
                    CUNIT_ID = _viewModel.oParameterUtilitiesList.CUNIT_ID,
                    CUTILITY_TYPE = loData.CCHARGES_TYPE,
                };
                bool llResult = await _viewModel.GetComboBoxDataCMETER_NO(loParam);
                loData.CMETER_NO = llResult ? _viewModel.oComboBoxDataCMETER_NO.FirstOrDefault().CMETER_NO : "";
                loData.NCAPACITY = llResult ? _viewModel.oComboBoxDataCMETER_NO.FirstOrDefault().NCAPACITY : 0;
                loData.NCALCULATION_FACTOR = llResult ? _viewModel.oComboBoxDataCMETER_NO.FirstOrDefault().NCALCULATION_FACTOR : 0;
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

                _viewModel.oParameterUtilitiesList = R_FrontUtility.ConvertObjectToObject<PMT01100UtilitiesParameterUtilitiesListDTO>(poParameter);
                if (!string.IsNullOrEmpty(_viewModel.oParameterUtilitiesList.CPROPERTY_ID))
                {
                    _viewModel.oHeaderUtilitiesEntity = R_FrontUtility.ConvertObjectToObject<PMT01100LOO_UnitCharges_Utilities_UnitInfoDTO>(poParameter);
                    await _viewModel.GetComboBoxDataCCHARGES_TYPE();
                    var loParam = new PMT01100Common.Utilities.Request.PMT01100RequestUtilitiesCMeterNoParameterDTO()
                    {
                        CPROPERTY_ID = _viewModel.oParameterUtilitiesList.CPROPERTY_ID,
                        CBUILDING_ID = _viewModel.oParameterUtilitiesList.CBUILDING_ID,
                        CFLOOR_ID = _viewModel.oParameterUtilitiesList.CFLOOR_ID,
                        CUNIT_ID = _viewModel.oParameterUtilitiesList.CUNIT_ID,
                        CUTILITY_TYPE = _viewModel.oComboBoxDataCCHARGES_TYPE.First().CCODE,
                    };

                    bool llResult = await _viewModel.GetComboBoxDataCMETER_NO(loParam);

                    //if (!string.IsNullOrEmpty(_viewModel.oParameterUtilitiesList.CREF_NO))
                    if (!string.IsNullOrEmpty(_viewModel.oParameterUtilitiesList.CREF_NO))
                    {
                        await _gridUtilities.R_RefreshGrid(null);

                    }
                }
                /*
                _viewModel.oParameterUtilitiesList = R_FrontUtility.ConvertObjectToObject<PMT01100UtilitiesParameterUtilitiesListDTO>(poParameter);
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

        #region Utilities List

        private async Task R_ServiceGetListUtilitiesListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetUtilitiesList();
                eventArgs.ListEntityResult = _viewModel.oListUtilities;
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
            PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO loParam;

            try
            {
                loParam = new PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO();
                if (eventArgs.Data != null)
                {
                    loParam = R_FrontUtility.ConvertObjectToObject<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO>(eventArgs.Data);
                    tempCCHARGES_TYPE = loParam.CCHARGES_TYPE;
                }
                else
                {
                    loParam.CREF_NO = _viewModel.oParameterUtilitiesList.CREF_NO;
                    loParam.CPROPERTY_ID = _viewModel.oParameterUtilitiesList.CPROPERTY_ID;
                    loParam.CDEPT_CODE = _viewModel.oParameterUtilitiesList.CDEPT_CODE;
                    loParam.CTRANS_CODE = "802041";
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
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO>(eventArgs.Data);

                await _viewModel.ServiceSave(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel.oEntity;
                tempCCHARGES_TYPE = "";
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
                var loData = (PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO)eventArgs.Data;

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

        #region Master LookUp

        private void Before_Open_Lookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                tempCCHARGES_TYPE = !string.IsNullOrEmpty(tempCCHARGES_TYPE) ? tempCCHARGES_TYPE : _viewModel.oEntity.CCHARGES_TYPE;
                if (!string.IsNullOrEmpty(tempCCHARGES_TYPE))
                {
                    var param = new LML00400ParameterDTO()
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                        CPROPERTY_ID = _viewModel.oParameterUtilitiesList.CPROPERTY_ID,
                        CCHARGE_TYPE_ID = tempCCHARGES_TYPE,
                    };
                    eventArgs.Parameter = param;
                    eventArgs.TargetPageType = typeof(LML00400);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }

        private void After_Open_Lookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loTempResult = (LML00400DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;
                var loGetData = (PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO)eventArgs.ColumnData;
                loGetData.CCHARGES_ID = loTempResult.CCHARGES_ID;
                loGetData.CCHARGES_NAME = loTempResult.CCHARGES_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_CellLostFocus(R_CellLostFocusedEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO loGetData = (PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO)eventArgs.CurrentRow;

                if (eventArgs.ColumnName == nameof(PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO.CCHARGES_ID))
                {
                    if (!string.IsNullOrWhiteSpace(loGetData.CCHARGES_ID))
                    {
                        LookupLML00400ViewModel loLookupViewModel = new LookupLML00400ViewModel();
                        var param = new LML00400ParameterDTO
                        {
                            CCOMPANY_ID = _clientHelper.CompanyId,
                            CUSER_ID = _clientHelper.UserId,
                            CPROPERTY_ID = _viewModel.oParameterUtilitiesList.CPROPERTY_ID,
                            CCHARGE_TYPE_ID = tempCCHARGES_TYPE,
                            CSEARCH_TEXT = (string)eventArgs.Value,
                        };
                        var loResult = await loLookupViewModel.GetUtitlityCharges(param);

                        if (loResult == null)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                    "_ErrLookup01"));
                            loGetData.CCHARGES_ID = "";
                            loGetData.CCHARGES_NAME = "";
                        }
                        else
                        {
                            loGetData.CCHARGES_ID = loResult.CCHARGES_ID;
                            loGetData.CCHARGES_NAME = loResult.CCHARGES_NAME;
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
