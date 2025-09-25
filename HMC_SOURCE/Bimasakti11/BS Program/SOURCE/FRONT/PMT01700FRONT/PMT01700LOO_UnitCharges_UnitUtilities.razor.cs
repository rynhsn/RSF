using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT01700COMMON.DTO.Utilities;
using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using PMT01700MODEL.ViewModel;
using R_BlazorFrontEnd.Controls.DataControls;
using PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Unit___Charges;
using PMT01700COMMON.DTO.Utilities.Front;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Events;
using PMT01700FrontResources;
using R_BlazorFrontEnd.Enums;
using R_CommonFrontBackAPI;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00400;
using R_BlazorFrontEnd;

namespace PMT01700FRONT
{
    public partial class PMT01700LOO_UnitCharges_UnitUtilities : R_Page, R_ITabPage
    {
        #region Master Page

        readonly PMT01700LOO_UnitCharges_UnitUtilitiesViewModel _viewModel = new();
        R_ConductorGrid? _conductorUnitInfo;
        R_Grid<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO>? _gridUnitInfo;
        private int _pageSizeUnitList = 10;
        private int _pageSizeUtilitiesList = 10;

        #region Utilities 
        readonly PMT01700LOO_UnitCharges_UtilitiesViewModel _viewModelUtilities = new();
        R_Conductor? _conductorUtilities;
        R_Grid<PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO>? _gridUtilities;

        #endregion

        [Inject] IClientHelper? _clientHelper { get; set; }


        PMT01700EventCallBackDTO _oEventCallBack = new PMT01700EventCallBackDTO();
        #region Front Control

        bool _hasDataUnit = false;
        bool _CRUDModeUnit = false;
        bool _CRUDModeUtility = false;
        bool _IsTransactionStatusDraft = false;


        #endregion

        #endregion
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.oParameter = R_FrontUtility.ConvertObjectToObject<PMT01700ParameterFrontChangePageDTO>(poParameter);
                if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
                {
                    await _viewModel.GetUnitChargesHeader();
                    await _viewModelUtilities.GetComboBoxDataCCHARGES_TYPE();
                    _IsTransactionStatusDraft = _viewModel.oHeaderEntity.CTRANS_STATUS == "00";
                    if (!string.IsNullOrEmpty(_viewModel.oParameter.CREF_NO))
                    {
                        await _gridUnitInfo!.R_RefreshGrid(null);
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
                // var llTrue = await R_MessageBox.Show("", "This function still on Development Process!", R_eMessageBoxButtonType.OK);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
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
                var loData = (PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO)eventArgs.Data;

                if (string.IsNullOrWhiteSpace(loData.COTHER_UNIT_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationUnit");
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
                //_oEventCallBack.LCRUD_MODE = eventArgs.Enable;
                _oEventCallBack.LCRUD_MODE = _CRUDModeUnit = eventArgs.Enable;

                _viewModel.lControlTabCharges = _viewModel.oListUnitInfo.Any() ? eventArgs.Enable : false;
                _hasDataUnit = _viewModel.oListUnitInfo.Any() ? eventArgs.Enable : false;
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
                if (_viewModel.oParameter.CTRANS_STATUS == "00")
                {
                    _hasDataUnit = _viewModel.oListUnitInfo.Any();
                }
                else
                {
                    _hasDataUnit = false;
                }
                _viewModel.lControlTabCharges = _viewModel.oListUnitInfo.Any();
                eventArgs.ListEntityResult = _viewModel.oListUnitInfo;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_DisplayOtherUnitListGetRecord(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO loData = (PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO)eventArgs.Data;
            //Assign Transcode and dept code cause on display didnt get that value
            var lcTempCTRANS_CODE = _viewModel.oParameter!.CTRANS_CODE!;
            var lcTempCDEPT_CODE = _viewModel.oParameter.CDEPT_CODE!;
            var lcTempTransStatus = _viewModel.oParameter.CTRANS_STATUS!;
            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    if (eventArgs.Data != null)
                    {
                        if (!string.IsNullOrEmpty(loData.COTHER_UNIT_ID))
                        {
                            _viewModel.oParameter = R_FrontUtility.ConvertObjectToObject<PMT01700ParameterFrontChangePageDTO>(loData);
                            _viewModelUtilities.oParameterUtilities = R_FrontUtility.ConvertObjectToObject<PMT01700ParameterFrontChangePageDTO>(loData);

                            _viewModel.oParameter.CTRANS_CODE = _viewModelUtilities.oParameterUtilities.CTRANS_CODE = lcTempCTRANS_CODE;
                            _viewModel.oParameter.CDEPT_CODE = _viewModelUtilities.oParameterUtilities.CDEPT_CODE = lcTempCDEPT_CODE;
                            _viewModel.oParameter.CTRANS_STATUS = _viewModelUtilities.oParameterUtilities.CTRANS_STATUS = lcTempTransStatus;
                            await _gridUtilities.R_RefreshGrid(null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        #region Master CRUD Unit List
        private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO loParam;

            try
            {
                loParam = new PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO();
                if (eventArgs.Data != null)
                {
                    loParam = R_FrontUtility.ConvertObjectToObject<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO>(eventArgs.Data);
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
              
                /*
                if (_viewModelPMT01500Agreement.loEntityPMT01500AgreementDetail.DEND_DATE != null)
                {
                    OnChangedDEND_DATE(_viewModelPMT01500Agreement.loEntityPMT01500AgreementDetail.DEND_DATE);
                }
                */

                eventArgs.Result = await _viewModel.GetEntity(loParam); ; //_viewModel.oEntityUnitInfo;
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
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO>(eventArgs.Data);

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
                eventArgs.Result = _viewModel.oEntityUnitInfo;
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
                var loData = (PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO)eventArgs.Data;

               // await _viewModel.GetEntity(loData);

                if (_viewModel.oEntityUnitInfo != null)
                    await _viewModel.ServiceDelete(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        /*

        #region Charges List

        private async Task R_ServiceGetListUtilitiesListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO>(eventArgs.Parameter);
                await _viewModel.GetutilitiesList(loParam);
                //_viewModel.lControlTabUtilities = _viewModel.oListCharges.Any();
                eventArgs.ListEntityResult = _viewModel.oListUtilities;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        */

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
                _oEventCallBack.LCRUD_MODE = _viewModel.lControlTabCharges = _CRUDModeUtility = eventArgs.Enable;
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
                var loData = (PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO)eventArgs.Data;
                //await LostFocusCUNIT_ID();


                if (string.IsNullOrWhiteSpace(loData.CCHARGES_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationCharge");
                    loException.Add(loErr);
                }

                if (string.IsNullOrWhiteSpace(loData.CMETER_NO))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationMeterNo");
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
                var loData = (PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO)eventArgs.Data;
                loData.CCHARGES_TYPE = _viewModelUtilities.oComboBoxDataCCHARGES_TYPE.FirstOrDefault().CCODE;

                var loParam = new PMT01700LOO_UnitUtilities_ParameterDTO()
                {
                    CPROPERTY_ID = _viewModelUtilities.oParameterUtilities.CPROPERTY_ID,
                    CBUILDING_ID = _viewModelUtilities.oParameterUtilities.CBUILDING_ID,
                    CFLOOR_ID = _viewModelUtilities.oParameterUtilities.CFLOOR_ID,
                    COTHER_UNIT_ID = _viewModelUtilities.oParameterUtilities.COTHER_UNIT_ID,
                    CUTILITY_TYPE = loData.CCHARGES_TYPE,
                };
                bool llResult = await _viewModelUtilities.GetComboBoxDataCMETER_NO(loParam);
                loData.CMETER_NO = llResult ? _viewModelUtilities.oComboBoxDataCMETER_NO.FirstOrDefault().CMETER_NO : "";
                loData.NCAPACITY = llResult ? _viewModelUtilities.oComboBoxDataCMETER_NO.FirstOrDefault().NCAPACITY : 0;
                loData.NCALCULATION_FACTOR = llResult ? _viewModelUtilities.oComboBoxDataCMETER_NO.FirstOrDefault().NCALCULATION_FACTOR : 0;
                _lControlCMeterNo = llResult;
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
            PMT01700LOO_UnitUtilities_ParameterDTO? loParam;

            try
            {
                var loDataParameter = _viewModelUtilities.oParameterUtilities;
                var loData = _viewModelUtilities.Data;
                loData.CCHARGES_TYPE = pcParam;

                loData.CCHARGES_ID = "";
                loData.CCHARGES_NAME = "";

                loParam = new()
                {
                    CPROPERTY_ID = loDataParameter.CPROPERTY_ID,
                    CBUILDING_ID = loDataParameter.CBUILDING_ID,
                    CFLOOR_ID = loDataParameter.CFLOOR_ID,
                    COTHER_UNIT_ID = loDataParameter.COTHER_UNIT_ID,
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
                //  var loParam = (PMT01700LOO_UnitUtilities_ParameterDTO)eventArgs.Parameter;
                await _viewModelUtilities.GetUtilitiesList();
               
                eventArgs.ListEntityResult = _viewModelUtilities.oListUtilities;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Master CRUD Utilities

        private async Task ServiceGetUtilitiesRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO loParam;

            try
            {
                loParam = new PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO();
                if (eventArgs.Data != null)
                {
                    loParam = R_FrontUtility.ConvertObjectToObject<PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO>(eventArgs.Data);
                }
                else
                {
                    loParam.CREF_NO = _viewModelUtilities.oParameterUtilities.CREF_NO;
                    loParam.CPROPERTY_ID = _viewModelUtilities.oParameterUtilities.CPROPERTY_ID;
                    loParam.CDEPT_CODE = _viewModelUtilities.oParameterUtilities.CDEPT_CODE;
                    loParam.CTRANS_CODE = _viewModelUtilities.oParameterUtilities.CTRANS_CODE;
                    loParam.CCOMPANY_ID = _clientHelper.CompanyId;
                    loParam.CUSER_ID = _clientHelper.UserId;
                };
                await _viewModelUtilities.GetEntity(loParam);


                if (_viewModelUtilities.oComboBoxDataCCHARGES_TYPE.Any())
                {
                    var loParamMeterNo = new PMT01700LOO_UnitUtilities_ParameterDTO()
                    {
                        CPROPERTY_ID = _viewModelUtilities.oParameterUtilities.CPROPERTY_ID,
                        CBUILDING_ID = _viewModelUtilities.oParameterUtilities.CBUILDING_ID,
                        CFLOOR_ID = _viewModelUtilities.oParameterUtilities.CFLOOR_ID,
                        COTHER_UNIT_ID = _viewModelUtilities.oParameterUtilities.COTHER_UNIT_ID,
                        CUTILITY_TYPE = _viewModelUtilities.oEntityUtilities.CCHARGES_TYPE,
                    };
                    bool llResult = await _viewModelUtilities.GetComboBoxDataCMETER_NO(loParamMeterNo);
                }
                _viewModelUtilities.LTAXABLE = _viewModelUtilities.oEntityUtilities.LTAXABLE || _viewModelUtilities.oEntityUtilities.LADMIN_FEE_TAX;
                eventArgs.Result = _viewModelUtilities.oEntityUtilities;
                _lControlCMeterNo = true;
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
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO>(eventArgs.Data);


                if ((eCRUDMode)eventArgs.ConductorMode == eCRUDMode.AddMode)
                {
                    var loDataUnit = (PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO)_conductorUnitInfo.R_GetCurrentData();
                    loParam.CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID;
                    loParam.CDEPT_CODE = _viewModel.oParameter.CDEPT_CODE;
                    loParam.CTRANS_CODE = _viewModel.oParameter.CTRANS_CODE;
                    loParam.CREF_NO = _viewModel.oParameter.CREF_NO;
                    loParam.CUNIT_ID = _viewModel.oParameter.COTHER_UNIT_ID;
                    loParam.CFLOOR_ID = _viewModel.oParameter.CFLOOR_ID;
                    loParam.CBUILDING_ID = _viewModel.oParameter.CBUILDING_ID;
                }

                await _viewModelUtilities.ServiceSave(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModelUtilities.oEntityUtilities;
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
                var loData = (PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO)eventArgs.Data;

                await _viewModelUtilities.GetEntity(loData);

                if (_viewModelUtilities.oEntityUtilities != null)
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


        #region Charges Master Tab
        private async Task R_TabEventCallbackAsync(object poValue)
        {
            var loEx = new R_Exception();

            try
            {

                var loValue = R_FrontUtility.ConvertObjectToObject<PMT01700EventCallBackDTO>(poValue);
                //var loValue = R_FrontUtility.ConvertObjectToObject<PMT01500EventCallBackDTO>(poValue);
                if (string.IsNullOrEmpty(loValue.CCRUD_MODE))
                {
                    _viewModel.lControlTabUnitAndCharges = _viewModel.lControlTabCharges = loValue.LCRUD_MODE;
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
            //ASSIGN Param with current data and addition with param when get in agreement detail (header this page)

            var loParamChargeTab = R_FrontUtility.ConvertObjectToObject<PMT01700ParameterChargesTab>(_viewModel.oParameter);
            loParamChargeTab.IYEARS = _viewModel.oParameterChargesTab.IYEARS;
            loParamChargeTab.IMONTHS = _viewModel.oParameterChargesTab.IMONTHS;
            loParamChargeTab.IDAYS = _viewModel.oParameterChargesTab.IDAYS;
            loParamChargeTab.CSTART_DATE = _viewModel.oParameterChargesTab.CSTART_DATE;
            loParamChargeTab.CEND_DATE = _viewModel.oParameterChargesTab.CEND_DATE;
            loParamChargeTab.CCURRENCY_CODE = _viewModel.oParameterChargesTab.CCURRENCY_CODE;
            loParamChargeTab.NTOTAL_GROSS_AREA = _viewModel.oHeaderEntity.NTOTAL_GROSS_AREA;

            eventArgs.Parameter = loParamChargeTab;
            eventArgs.TargetPageType = typeof(PMT01700LOO_UnitCharges_Charges);
        }

        #endregion


        #endregion
        #region Check add edit delete Utilities 
        private void R_CheckAddUtilities(R_CheckAddEventArgs eventArgs)
        {
            var loException = new R_Exception();
            //Task loTask;

            try
            {
                //eventArgs.Allow = _viewModel.oParameter.CTRANS_STATUS == "00";
                if (_viewModel.oListUnitInfo.Any())
                {
                    _hasDataUnit = _viewModel.oParameter.CTRANS_STATUS == "00";
                }
                else
                {
                    _hasDataUnit = false;
                }
            }
            catch (Exception Ex)
            {
                loException.Add(Ex);
            }

            R_DisplayException(loException);
        }

        private void R_CheckEditUtilities(R_CheckEditEventArgs eventArgs)
        {
            var loException = new R_Exception();
            //Task loTask;

            try
            {
                eventArgs.Allow = _viewModel.oParameter.CTRANS_STATUS == "00";
            }
            catch (Exception Ex)
            {
                loException.Add(Ex);
            }

            R_DisplayException(loException);
        }

        private void R_CheckDeleteUtilities(R_CheckDeleteEventArgs eventArgs)
        {
            var loException = new R_Exception();
            //Task loTask;

            try
            {
                eventArgs.Allow = _viewModel.oParameter.CTRANS_STATUS == "00";
            }
            catch (Exception Ex)
            {
                loException.Add(Ex);
            }

            R_DisplayException(loException);
        }

        #endregion
        private async Task R_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                var res = await R_MessageBox.Show("", @_localizer["ValidationBeforeCancel"], R_eMessageBoxButtonType.YesNo);
                if (res == R_eMessageBoxResult.No)
                {
                    eventArgs.Cancel = true;
                }
                //else
                //{
                    //await Close(false, false);
                //}

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Check add edit delete OTHER UNIT
        private void R_CheckGridAdd(R_CheckGridEventArgs eventArgs)
        {
            var loException = new R_Exception();
            //Task loTask;

            try
            {
                eventArgs.Allow = _viewModel.oHeaderEntity.CTRANS_STATUS == "00";
            }
            catch (Exception Ex)
            {
                loException.Add(Ex);
            }

            R_DisplayException(loException);
        }

        private void R_CheckGridEdit(R_CheckGridEventArgs eventArgs)
        {
            var loException = new R_Exception();
            //Task loTask;

            try
            {
                eventArgs.Allow = _viewModel.oHeaderEntity.CTRANS_STATUS == "00";
            }
            catch (Exception Ex)
            {
                loException.Add(Ex);
            }

            R_DisplayException(loException);
        }

        private void R_CheckGridDelete(R_CheckGridEventArgs eventArgs)
        {
            var loException = new R_Exception();
            //Task loTask;

            try
            {
                eventArgs.Allow = _viewModel.oHeaderEntity.CTRANS_STATUS == "00";
            }
            catch (Exception Ex)
            {
                loException.Add(Ex);
            }

            R_DisplayException(loException);
        }
        #endregion


        #region LookUp Other Unit

        private void Before_Open_LookupOtherUnitId(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var param = new GSL02700ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID!,
                    CBUILDING_ID = _viewModel.oParameter.CBUILDING_ID ?? "",
                    LEVENT = true,
                    CLEASE_STATUS_LIST = "01,02",
                    CREMOVE_DATA_OTHER_UNIT_ID = string.Join(",", _viewModel.oListUnitInfo.Select(item => item.COTHER_UNIT_ID)),
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
                var loGetData = (PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO)eventArgs.ColumnData;
                loGetData.COTHER_UNIT_ID = loTempResult.COTHER_UNIT_ID;
                loGetData.COTHER_UNIT_NAME = loTempResult.COTHER_UNIT_NAME;
                loGetData.CFLOOR_ID = loTempResult.CFLOOR_ID;
                loGetData.CLOCATION = loTempResult.CLOCATION;
                loGetData.COTHER_UNIT_TYPE_ID = loTempResult.COTHER_UNIT_TYPE_ID;
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
                PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO loGetData = (PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO)eventArgs.CurrentRow;

                if (eventArgs.ColumnName == nameof(PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO.COTHER_UNIT_ID))
                {
                    if (!string.IsNullOrWhiteSpace((string)eventArgs.Value))
                    {
                        LookupGSL02700ViewModel loLookupViewModel = new LookupGSL02700ViewModel();
                        var param = new GSL02700ParameterDTO
                        {
                            CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID!,
                            CBUILDING_ID = _viewModel.oParameter.CBUILDING_ID!,
                            CLEASE_STATUS_LIST = "01,02",
                            CREMOVE_DATA_OTHER_UNIT_ID = string.Join(",", _viewModel.oListUnitInfo.Select(item => item.COTHER_UNIT_ID)),
                            LEVENT = true,
                            CSEARCH_TEXT = (string)eventArgs.Value,
                        };
                        var loResult = await loLookupViewModel.GetOtherUnit(param);

                        if (loResult == null)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                    "_ErrLookup01"));
                            loGetData.COTHER_UNIT_ID = "";
                            loGetData.COTHER_UNIT_NAME = "";
                            loGetData.CFLOOR_ID = "";
                            //loGetData.CFLOOR_NAME = "";
                            //loGetData.NACTUAL_AREA_SIZE = 0;
                            loGetData.COTHER_UNIT_TYPE_ID = "";
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
                            loGetData.COTHER_UNIT_TYPE_ID = loResult.COTHER_UNIT_TYPE_ID; ;
                            loGetData.COTHER_UNIT_TYPE_NAME = loResult.COTHER_UNIT_TYPE_NAME;
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
                    CPROPERTY_ID = _viewModelUtilities.oParameterUtilities.CPROPERTY_ID!,
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

            var loGetData = (PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO)_conductorUtilities.R_GetCurrentData();

            loGetData.CCHARGES_ID = loTempResult.CCHARGES_ID;
            loGetData.CCHARGES_NAME = loTempResult.CCHARGES_NAME;
            _viewModelUtilities.LTAXABLE = loTempResult.LTAXABLE || loTempResult.LADMIN_FEE_TAX; ;
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
                    CPROPERTY_ID = _viewModelUtilities.oParameterUtilities.CPROPERTY_ID!,
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
                    _viewModelUtilities.LTAXABLE = false ;
                }
                else
                {
                    loGetData.CCHARGES_ID = loResult.CCHARGES_ID;
                    loGetData.CCHARGES_NAME = loResult.CCHARGES_NAME;
                    _viewModelUtilities.LTAXABLE = loResult.LTAXABLE || loResult.LADMIN_FEE_TAX;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion
        #region Lookup Button Tax Code Lookup

        private R_Lookup? R_LookupTaxCodeLookup;
        private void BeforeOpenLookUpTaxCodeLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00110ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModelUtilities.oParameterUtilities.CPROPERTY_ID))
            {
                param = new GSL00110ParameterDTO
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CTAX_DATE = DateTime.Now.ToString("yyyyMMdd")
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00110);
        }

        private void AfterOpenLookUpTaxCodeLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL00110DTO? loTempResult = null;
            //LMM01500AgreementDetailDTO? loGetData = null;


            try
            {
                loTempResult = (GSL00110DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //loGetData = (LMM01500AgreementDetailDTO)_conductorFullPMT02500Agreement.R_GetCurrentData();

                _viewModelUtilities.Data.CTAX_ID = loTempResult.CTAX_ID;
                _viewModelUtilities.Data.CTAX_NAME = loTempResult.CTAX_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusTaxCode()
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
                GSL00110ParameterDTO loParam = new GSL00110ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CTAX_DATE = DateTime.Now.ToString("yyyyMMdd"),
                    CSEARCH_TEXT = loGetData.CTAX_ID ?? "",
                };

                var loResult = await loLookupViewModel.GetTaxByDate(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CTAX_ID = "";
                    loGetData.CTAX_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
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


        public Task RefreshTabPageAsync(object poParam)
        {
            throw new NotImplementedException();
        }
    }
}
