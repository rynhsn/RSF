using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System.Xml.Linq;
using R_BlazorFrontEnd.Interfaces;
using PMT01300COMMON;
using PMT01300MODEL;
using Lookup_PMModel.ViewModel.LML00600;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00500;
using Lookup_PMModel.ViewModel.LML01000;
using Lookup_PMModel.ViewModel.LML01100;
using R_BlazorFrontEnd.Enums;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMModel.ViewModel.LML00400;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls.MessageBox;
using System.Globalization;
using GFF00900COMMON.DTOs;

namespace PMT01300FRONT
{
    public partial class PMT01320 : R_Page, R_ITabPage
    {
        #region ViewModel
        private PMT01300ViewModel _viewModelList = new PMT01300ViewModel();
        private PMT01320ViewModel _viewModelHeader = new PMT01320ViewModel();
        private PMT01321ViewModel _viewModel = new PMT01321ViewModel();
        private PMT01310ViewModel _viewModelDetail = new PMT01310ViewModel();
        #endregion

        #region Conductor
        private R_Conductor _conductorRef;
        private R_ConductorGrid _conductorGridRef;
        #endregion

        #region Grid
        private R_Grid<PMT01310DTO> _gridLOIUnitListRef;
        private R_Grid<PMT01320DTO> _gridLOIUtilitiesListRef;
        #endregion

        #region Inject
        [Inject] private R_ILocalizer<PMT01300FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }
        #endregion

        #region Private Property
        private R_ComboBox<PMT01300UniversalDTO, string> UtilityType_ComboBox;
        private R_ComboBox<PMT01300AgreementBuildingUtilitiesDTO, string> BuildingUtilities_ComboBox;
        private bool EnableNormalMode = false;
        private bool EnableNormalDetailMode = false;
        private bool EnableHasHeaderData = false;
        private bool EnableGreaterClosesSts = true;
        private bool HiddenCapacityAndCF = false;
        private string LabelActiveInactive = "Active";
        private bool IsAddDataLOI = false;

        private R_TabStrip _TabUnitCharges;
        private R_TabPage _TabCharge;
        #endregion

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01300DTO)poParameter;
                IsAddDataLOI = loData.LIS_ADD_DATA_LOI;
                var loHeaderData = await _viewModelDetail.GetLOIWithResult(loData);
                _viewModelHeader.LOI = loHeaderData;
                EnableGreaterClosesSts = int.Parse(loHeaderData.CTRANS_STATUS) >= 80 == false;

                await _viewModel.GetInitialVar();
                await _gridLOIUnitListRef.R_RefreshGrid(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Locking
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_MODULE_NAME = "PM";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (PMT01310DTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                   plSendWithContext: true,
                   plSendWithToken: true,
                   pcHttpClientName: DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMT01310",
                        Table_Name = "GSM_PROPERTY_UNIT",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CBUILDING_ID, loData.CFLOOR_ID, loData.CUNIT_ID)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMT01310",
                        Table_Name = "GSM_PROPERTY_UNIT",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CBUILDING_ID, loData.CFLOOR_ID, loData.CUNIT_ID)
                    };

                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }

                llRtn = loLockResult.IsSuccess;
                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                    throw loLockResult.Exception;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return llRtn;
        }
        #endregion

        #region Tab Refresh
        public async Task RefreshTabPageAsync(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01300DTO)poParam;
                IsAddDataLOI = false;
                if (string.IsNullOrWhiteSpace(loData.CREF_NO) ==  false)
                {
                    var loHeaderData = await _viewModelDetail.GetLOIWithResult(loData);
                    _viewModelHeader.LOI = loHeaderData;
                    EnableGreaterClosesSts = int.Parse(loHeaderData.CTRANS_STATUS) >= 80 == false;

                    await _gridLOIUnitListRef.R_RefreshGrid(loData);

                    if (_TabUnitCharges.ActiveTab.Id == "Charges")
                    {
                        await Task.CompletedTask;

                        var loParam = _viewModelList.LOIUNITGrid.Count > 0 ? _viewModelList.LOIUNITGrid.FirstOrDefault() : null;
                        if (loParam != null)
                        {
                            loParam.CDEPT_CODE = _viewModelHeader.LOI.CDEPT_CODE;
                            loParam.NTOTAL_NET_AREA = _viewModelHeader.LOI.NTOTAL_NET_AREA;
                            loParam.NTOTAL_GROSS_AREA = _viewModelHeader.LOI.NTOTAL_GROSS_AREA;
                            loParam.CTRANS_STATUS_LOI = _viewModelHeader.LOI.CTRANS_STATUS;
                            loParam.CLEASE_MODE = _viewModelHeader.LOI.CLEASE_MODE;
                            loParam.DSTART_DATE = _viewModelHeader.LOI.DSTART_DATE.Value;
                            loParam.CSTART_DATE = _viewModelHeader.LOI.CSTART_DATE;
                            loParam.CEND_DATE = _viewModelHeader.LOI.CEND_DATE;
                            loParam.CCURRENCY_CODE = _viewModelHeader.LOI.CCURRENCY_CODE;
                        }

                        await _TabCharge.InvokeRefreshTabPageAsync(loParam);
                    }
                }
                else
                {
                    _viewModelHeader.LOI = new PMT01300DTO();
                    if (_gridLOIUnitListRef.DataSource.Count > 0)
                    {
                        _gridLOIUnitListRef.DataSource.Clear();
                    }
                    if (_gridLOIUtilitiesListRef.DataSource.Count > 0)
                    {
                        _viewModel.R_SetCurrentData(null);
                        _gridLOIUtilitiesListRef.DataSource.Clear();
                    }

                    if (_TabUnitCharges.ActiveTab.Id == "Charges")
                    {
                        await _TabCharge.InvokeRefreshTabPageAsync(null);
                    }
                }

                EnableHasHeaderData = string.IsNullOrWhiteSpace(loData.CREF_NO) == false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region Unit Grid
        private async Task Unit_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParameter = R_FrontUtility.ConvertObjectToObject<PMT01310DTO>(eventArgs.Parameter);
                await _viewModelHeader.GetLOIUnitList(loParameter);

                eventArgs.ListEntityResult = _viewModelHeader.LOIUNITGrid;
                if (_viewModelHeader.LOIUNITGrid.Count <= 0)
                {
                    if (_gridLOIUtilitiesListRef.DataSource.Count > 0)
                    {
                        _viewModel.R_SetCurrentData(null);
                        _gridLOIUtilitiesListRef.DataSource.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Unit_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelHeader.GetLOIUnit((PMT01310DTO)eventArgs.Data);

                eventArgs.Result = _viewModelHeader.LOI_Unit;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Unit_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Normal)
                {
                    var loData = (PMT01310DTO)eventArgs.Data;
                    if (loData != null)
                    {
                        if (string.IsNullOrWhiteSpace(loData.CUNIT_ID) == false)
                        {
                            await _gridLOIUtilitiesListRef.R_RefreshGrid(loData);
                        }
                        else
                        {
                            if (_gridLOIUtilitiesListRef.DataSource.Count > 0)
                            {
                                _viewModel.R_SetCurrentData(null);
                                _gridLOIUtilitiesListRef.DataSource.Clear();
                            }
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
        private async Task Unit_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelHeader.SaveLOIUnit(
                    (PMT01310DTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModelHeader.LOI_Unit;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Unit_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelHeader.DeleteLOIUnit((PMT01310DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Grid_R_SetOther(R_SetEventArgs eventArgs)
        {
            PMT01310DTO loUnitData = null;
            bool loHasData = false;
            if (_gridLOIUnitListRef != null)
            {
                loUnitData = _gridLOIUnitListRef.CurrentSelectedData;
                loHasData = _gridLOIUnitListRef.DataSource.Count > 0;
            }
            EnableNormalMode = eventArgs.Enable;
            PMT01300LOICallBackParameterDTO loData = new PMT01300LOICallBackParameterDTO { CRUD_MODE = eventArgs.Enable, LIS_ADD_DATA_LOI = IsAddDataLOI, SELECTED_DATA_TAB_LOI = _viewModelHeader.LOI, SELECTED_DATA_TAB_UNIT = loUnitData, LIS_LOI_UNIT_HAS_DATA = loHasData };
            await InvokeTabEventCallbackAsync(loData);
        }
        private void Unit_Before_Open_Grid_Lookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSL02300ParameterDTO>(_viewModelHeader.LOI);
            loParam.CUNIT_CATEGORY_LIST = "02,03";
            loParam.CLEASE_STATUS_LIST = "01,02";
            if (_gridLOIUnitListRef.DataSource.Count > 0)
            {
                loParam.CUNIT_TYPE_ID = _gridLOIUnitListRef.DataSource[0].CUNIT_TYPE_ID;
            }
            loParam.CPROGRAM_ID = "";
            var loListDataSeparator = _gridLOIUnitListRef.DataSource.Select(x => x.CUNIT_ID);
            loParam.CREMOVE_DATA_UNIT_ID_SEPARATOR = string.Join(",", loListDataSeparator);

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL02300);
        }
        private void Unit_After_Open_Grid_Lookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loTempResult = (GSL02300DTO)eventArgs.Result;
            var loData = (PMT01310DTO)eventArgs.ColumnData;
            if (loTempResult is null)
            {
                return;
            }

            loData.CUNIT_ID = loTempResult.CUNIT_ID;
            loData.CUNIT_NAME = loTempResult.CUNIT_NAME;
            loData.CFLOOR_ID = loTempResult.CFLOOR_ID;
            loData.CBUILDING_ID = loTempResult.CBUILDING_ID;
            loData.CUNIT_TYPE_ID = loTempResult.CUNIT_TYPE_ID;
            loData.NACTUAL_AREA_SIZE = loTempResult.NACTUAL_AREA_SIZE;
            loData.NCOMMON_AREA_SIZE = loTempResult.NCOMMON_AREA_SIZE;
            loData.NGROSS_AREA_SIZE = loTempResult.NGROSS_AREA_SIZE;
            loData.NNET_AREA_SIZE = loTempResult.NNET_AREA_SIZE;
        }
        private async Task Unit_CellLostFocus(R_CellLostFocusedEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.ColumnName == "CUNIT_ID")
                {
                    var loData = (PMT01310DTO)eventArgs.CurrentRow;
                    string loIdValue = (string)eventArgs.Value;

                    if (eventArgs.Value.ToString().Length > 0)
                    {
                        var loParam = R_FrontUtility.ConvertObjectToObject<GSL02300ParameterDTO>(_viewModelHeader.LOI);
                        loParam.CUNIT_CATEGORY_LIST = "02,03";
                        var loListDataSeparator = _gridLOIUnitListRef.DataSource.Select(x => x.CUNIT_ID);
                        loParam.CREMOVE_DATA_UNIT_ID_SEPARATOR = string.Join(",", loListDataSeparator);
                        //loParam.CPROGRAM_ID =  "PMT01300";
                        loParam.CLEASE_STATUS_LIST = "01,02";
                        loParam.CSEARCH_TEXT = loIdValue;

                        LookupGSL02300ViewModel loLookupViewModel = new LookupGSL02300ViewModel();

                        var loResult = await loLookupViewModel.GetBuildingUnit(loParam);

                        if (loResult == null)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                    "_ErrLookup01"));

                            loData.CUNIT_NAME = "";
                            loData.CFLOOR_ID = "";
                            loData.CUNIT_TYPE_ID = "";
                            loData.CBUILDING_ID = "";
                            loData.NACTUAL_AREA_SIZE = 0;
                            loData.NCOMMON_AREA_SIZE = 0;
                            loData.NGROSS_AREA_SIZE = 0;
                            loData.NNET_AREA_SIZE = 0;
                            goto EndBlock;
                        }

                        loData.CUNIT_ID = loResult.CUNIT_ID;
                        loData.CUNIT_NAME = loResult.CUNIT_NAME;
                        loData.CFLOOR_ID = loResult.CFLOOR_ID;
                        loData.CBUILDING_ID = loResult.CBUILDING_ID;
                        loData.CUNIT_TYPE_ID = loResult.CUNIT_TYPE_ID;
                        loData.NACTUAL_AREA_SIZE = loResult.NACTUAL_AREA_SIZE;
                        loData.NCOMMON_AREA_SIZE = loResult.NCOMMON_AREA_SIZE;
                        loData.NGROSS_AREA_SIZE = loResult.NGROSS_AREA_SIZE;
                        loData.NNET_AREA_SIZE = loResult.NNET_AREA_SIZE;
                    }
                    else
                    {
                        loData.CUNIT_NAME = "";
                        loData.CFLOOR_ID = "";
                        loData.CUNIT_TYPE_ID = "";
                        loData.CBUILDING_ID = "";
                        loData.NACTUAL_AREA_SIZE = 0;
                        loData.NCOMMON_AREA_SIZE = 0;
                        loData.NGROSS_AREA_SIZE = 0;
                        loData.NNET_AREA_SIZE = 0;
                    }
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        private void Unit_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loData = (PMT01310DTO)eventArgs.Data;
            loData.CREF_NO = _viewModelHeader.LOI.CREF_NO;
        }
        private void Unit_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;
                var loData = (PMT01310DTO)eventArgs.Data;

                lCancel = string.IsNullOrWhiteSpace(loData.CFLOOR_ID);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT01300FrontResources.Resources_Dummy_Class),
                        "V0V01619"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            eventArgs.Cancel = loEx.HasError;
        }
        private void Unit_CheckGridAdd(R_CheckGridEventArgs eventArgs) 
        {
            eventArgs.Allow = EnableGreaterClosesSts;
        }
        private void Unit_CheckGridDelete(R_CheckGridEventArgs eventArgs) 
        {
            eventArgs.Allow = EnableGreaterClosesSts;
        }
        private void Unit_BeforeAdd(R_BeforeAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (_viewModelHeader.LIS_SINGLE_UNIT)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT01300FrontResources.Resources_Dummy_Class),
                        "N07"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            eventArgs.Cancel = loEx.HasError;
        }
        private async Task Unit_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loHeaderData = await _viewModelDetail.GetLOIWithResult(_viewModelHeader.LOI);
                _viewModelHeader.LOI = loHeaderData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        private async Task Unit_AfterDelete()
        {
            var loEx = new R_Exception();

            try
            {
                var loHeaderData = await _viewModelDetail.GetLOIWithResult(_viewModelHeader.LOI);
                _viewModelHeader.LOI = loHeaderData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Utilities Form
        private async Task Utilities_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParameter = R_FrontUtility.ConvertObjectToObject<PMT01320DTO>(eventArgs.Parameter);
                loParameter.CDEPT_CODE = _viewModelHeader.LOI.CDEPT_CODE;

                await _viewModel.GetLOIUtilitiesList(loParameter);

                eventArgs.ListEntityResult = _viewModel.LOIUtiliesGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Utilities_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetLOIUtilities((PMT01320DTO)eventArgs.Data);

                eventArgs.Result = _viewModel.LOI_Utilities;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Utilities_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01320DTO)eventArgs.Data;
                if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
                {
                    await BuildingUtilities_ComboBox.FocusAsync();
                }
                else if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Normal)
                {
                    if (string.IsNullOrWhiteSpace(loData.CCHARGES_TYPE) == false)
                    {
                        if (loData.CSTATUS == "80")
                        {
                            LabelActiveInactive = _localizer["_Inactive"];
                            _viewModel.StatusChange = false;
                        }
                        else if (loData.CSTATUS == "00")
                        {
                            LabelActiveInactive = _localizer["_Active"];
                            _viewModel.StatusChange = true;
                        }
                        else
                        {
                            LabelActiveInactive = _localizer["_Active"];
                            _viewModel.StatusChange = true;
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
        private void Utilities_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;
                var loData = (PMT01320DTO)eventArgs.Data;

                lCancel = string.IsNullOrWhiteSpace(loData.CCHARGES_TYPE);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT01300FrontResources.Resources_Dummy_Class),
                        "V019"));
                }

                lCancel = string.IsNullOrWhiteSpace(loData.CMETER_NO);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT01300FrontResources.Resources_Dummy_Class),
                        "V020"));
                }

                lCancel = string.IsNullOrWhiteSpace(loData.CCHARGES_ID);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT01300FrontResources.Resources_Dummy_Class),
                        "V021"));
                }

                lCancel = string.IsNullOrWhiteSpace(loData.CTAX_ID) && loData.LTAXABLE;
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT01300FrontResources.Resources_Dummy_Class),
                        "V029"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        private async Task Utilities_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01320DTO)eventArgs.Data;
                loData.CDEPT_CODE = _viewModelHeader.LOI.CDEPT_CODE;
                await _viewModel.SaveLOIUtilities(
                    loData,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.LOI_Utilities;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Utilities_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.DeleteLOIUtilities((PMT01320DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Utilities_SetOther(R_SetEventArgs eventArgs)
        {
            PMT01310DTO loUnitData = null;
            bool loHasData = false;
            if (_gridLOIUnitListRef != null)
            {
                loUnitData = _gridLOIUnitListRef.CurrentSelectedData;
                loHasData = _gridLOIUnitListRef.DataSource.Count > 0;
            }
            EnableNormalDetailMode = eventArgs.Enable;
            PMT01300LOICallBackParameterDTO loData = new PMT01300LOICallBackParameterDTO { CRUD_MODE = eventArgs.Enable, LIS_ADD_DATA_LOI = IsAddDataLOI, SELECTED_DATA_TAB_LOI = _viewModelHeader.LOI, SELECTED_DATA_TAB_UNIT = loUnitData,LIS_LOI_UNIT_HAS_DATA = loHasData };
            await InvokeTabEventCallbackAsync(loData);
        }
        private async Task Utilities_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            eventArgs.Data = R_FrontUtility.ConvertObjectToObject<PMT01320DTO>(_gridLOIUnitListRef.CurrentSelectedData);
            var loData = (PMT01320DTO)eventArgs.Data;

            if (_viewModel.VAR_UTILITY_TYPE.Count > 0)
            {
                var loFirstChargesTypeData = _viewModel.VAR_UTILITY_TYPE.FirstOrDefault();
                loData.CCHARGES_TYPE = loFirstChargesTypeData.CCODE;
            }
            else
            {
                loData.CCHARGES_TYPE = "";
            }
            await _viewModel.GetBuildingUtilitiesList(loData);
            if (_viewModel.VAR_METER_NO.Count > 0)
            {
                var loFirstMeterNoData = _viewModel.VAR_METER_NO.FirstOrDefault();
                loData.CMETER_NO = loFirstMeterNoData.CMETER_NO;
                loData.NCAPACITY = loFirstMeterNoData.NCAPACITY;
                loData.NCALCULATION_FACTOR = loFirstMeterNoData.NCALCULATION_FACTOR;
            }

            await UtilityType_ComboBox.FocusAsync();
        }
        private async Task Utilities_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            var res = await R_MessageBox.Show("", _localizer["Q04"],
                R_eMessageBoxButtonType.YesNo);

            eventArgs.Cancel = res == R_eMessageBoxResult.No;
        }
        #endregion

        #region Value Change
        private async Task UtilityTypeComboBox_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();
            bool llCondition;
            try
            {
                _viewModel.Data.CCHARGES_TYPE = poParam;
                await _viewModel.GetBuildingUtilitiesList(_viewModel.Data);
                _viewModel.Data.CCHARGES_ID = "";
                _viewModel.Data.CMETER_NO = "";
                _viewModel.Data.CCHARGES_NAME = "";
                _viewModel.Data.NCAPACITY = 0;
                _viewModel.Data.NCALCULATION_FACTOR = 0;

                llCondition = poParam == "05" || poParam == "06" || poParam == "07";
                HiddenCapacityAndCF = llCondition;
                if (llCondition == false)
                {
                    if (_viewModel.VAR_METER_NO.Count > 0)
                    {
                        var loFirstMeterNoData = _viewModel.VAR_METER_NO.FirstOrDefault();
                        _viewModel.Data.CMETER_NO = loFirstMeterNoData.CMETER_NO;
                        _viewModel.Data.NCAPACITY = loFirstMeterNoData.NCAPACITY;
                        _viewModel.Data.NCALCULATION_FACTOR = loFirstMeterNoData.NCALCULATION_FACTOR;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task MeterNoComboBox_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel.Data.CMETER_NO = poParam;
                if (_viewModel.VAR_METER_NO.Count > 0)
                {
                    var loData = _viewModel.VAR_METER_NO.FirstOrDefault(x => x.CMETER_NO == poParam);
                    _viewModel.Data.NCAPACITY = loData.NCAPACITY;
                    _viewModel.Data.NCALCULATION_FACTOR = loData.NCALCULATION_FACTOR;
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region Charges Lookup
        private async Task Charges_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.Data.CCHARGES_ID) == false)
                {
                    LML00400ParameterDTO loParam = new LML00400ParameterDTO()
                    {
                        CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                        CSEARCH_TEXT = _viewModel.Data.CCHARGES_ID,
                        CACTIVE_TYPE = "1",
                        CCHARGE_TYPE_ID = _viewModel.Data.CCHARGES_TYPE
                    };

                    LookupLML00400ViewModel loLookupViewModel = new LookupLML00400ViewModel();

                    var loResult = await loLookupViewModel.GetUtitlityCharges(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.Data.CCHARGES_NAME = "";
                        _viewModel.Data.CTAX_NAME = "";
                        _viewModel.Data.CTAX_ID = "";
                        _viewModel.Data.LTAXABLE = false;
                        _viewModel.Data.LADMIN_FEE_TAX = false;
                        goto EndBlock;
                    }

                    _viewModel.Data.CCHARGES_ID = loResult.CCHARGES_ID;
                    _viewModel.Data.CCHARGES_NAME = loResult.CCHARGES_NAME;
                    _viewModel.Data.LTAXABLE = loResult.LTAXABLE;
                    _viewModel.Data.LADMIN_FEE_TAX = loResult.LADMIN_FEE_TAX;
                    _viewModel.Data.CTAX_NAME = "";
                    _viewModel.Data.CTAX_ID = "";
                }
                else
                {
                    _viewModel.Data.CCHARGES_NAME = "";
                    _viewModel.Data.CTAX_NAME = "";
                    _viewModel.Data.CTAX_ID = "";
                    _viewModel.Data.LTAXABLE = false;
                    _viewModel.Data.LADMIN_FEE_TAX = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void R_Before_Open_LookupCharges(R_BeforeOpenLookupEventArgs eventArgs)
        {
            if (string.IsNullOrWhiteSpace(_viewModel.Data.CPROPERTY_ID))
            {
                return;
            }
            LML00400ParameterDTO loParam = new LML00400ParameterDTO()
            {
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                CCHARGE_TYPE_ID = _viewModel.Data.CCHARGES_TYPE,
                CACTIVE_TYPE = "1"
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(LML00400);
        }
        private void R_After_Open_LookupCharges(R_AfterOpenLookupEventArgs eventArgs)
        {
            LML00400DTO loTempResult = (LML00400DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _viewModel.Data.CCHARGES_ID = loTempResult.CCHARGES_ID;
            _viewModel.Data.CCHARGES_NAME = loTempResult.CCHARGES_NAME;
            _viewModel.Data.LTAXABLE = loTempResult.LTAXABLE;
            _viewModel.Data.LADMIN_FEE_TAX = loTempResult.LADMIN_FEE_TAX;
            _viewModel.Data.CTAX_NAME = "";
            _viewModel.Data.CTAX_ID = "";
        }
        #endregion

        #region Tax Lookup
        private async Task Tax_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.Data.CTAX_ID) == false)
                {
                    GSL00110ParameterDTO loParam = new GSL00110ParameterDTO()
                    {
                        CSEARCH_TEXT = _viewModel.Data.CTAX_ID,
                        CTAX_DATE = DateTime.Now.ToString("yyyyMMdd"),
                    };

                    LookupGSL00110ViewModel loLookupViewModel = new LookupGSL00110ViewModel();

                    var loResult = await loLookupViewModel.GetTaxByDate(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.Data.CTAX_NAME = "";
                        goto EndBlock;
                    }

                    _viewModel.Data.CTAX_ID = loResult.CTAX_ID;
                    _viewModel.Data.CTAX_NAME = loResult.CTAX_NAME;
                }
                else
                {
                    _viewModel.Data.CTAX_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void R_Before_Open_LookupTax(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00110ParameterDTO loParam = new GSL00110ParameterDTO()
            {
                CTAX_DATE = DateTime.Now.ToString("yyyyMMdd"),
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00110);
        }
        private void R_After_Open_LookupTax(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00110DTO loTempResult = (GSL00110DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _viewModel.Data.CTAX_ID = loTempResult.CTAX_ID;
            _viewModel.Data.CTAX_NAME = loTempResult.CTAX_NAME;
        }
        #endregion

        #region TabPage
        private async Task ChargeTab_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                await Task.CompletedTask;
                var loData = _gridLOIUnitListRef.CurrentSelectedData;
                
                loData.CDEPT_CODE = _viewModelHeader.LOI.CDEPT_CODE;
                loData.NTOTAL_NET_AREA = _viewModelHeader.LOI.NTOTAL_NET_AREA;
                loData.NTOTAL_GROSS_AREA = _viewModelHeader.LOI.NTOTAL_GROSS_AREA;
                loData.CTRANS_STATUS_LOI = _viewModelHeader.LOI.CTRANS_STATUS;
                loData.CLEASE_MODE = _viewModelHeader.LOI.CLEASE_MODE;
                loData.DSTART_DATE = _viewModelHeader.LOI.DSTART_DATE.Value;
                loData.CSTART_DATE = _viewModelHeader.LOI.CSTART_DATE;
                loData.CEND_DATE = _viewModelHeader.LOI.CEND_DATE;
                loData.CCURRENCY_CODE = _viewModelHeader.LOI.CCURRENCY_CODE;

                eventArgs.Parameter = loData;
                eventArgs.TargetPageType = typeof(PMT01330);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private bool _pageCRUDmode = false;
        private async Task R_TabEventCallback(object poValue)
        {
            var loValueCallBack = (PMT01300LOICallBackParameterDTO)poValue;
            
            _pageCRUDmode = loValueCallBack.CRUD_MODE == false;
            loValueCallBack.SELECTED_DATA_TAB_LOI = _viewModelHeader.LOI;
            loValueCallBack.SELECTED_DATA_TAB_UNIT = _gridLOIUnitListRef.CurrentSelectedData;
            loValueCallBack.LIS_ADD_DATA_LOI = IsAddDataLOI;
            await InvokeTabEventCallbackAsync(loValueCallBack);
        }
        #endregion

        #region Utility Active / Inactive
        private async Task Utility_Activate_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loGetData = (PMT01320DTO)_conductorRef.R_GetCurrentData();
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "PMT01301"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<PMT01320ActiveInactiveDTO>(loGetData);
                    await _viewModel.ActiveInactiveProcessAsync(loParam);
                    await _conductorRef.R_SetCurrentData(loGetData);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "PMT01301" //Uabh Approval Code sesuai Spec masing masing
                    };
                    eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        private async Task Utility_Activate_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {

            R_Exception loException = new R_Exception();
            try
            {
                var loGetData = (PMT01320DTO)_conductorRef.R_GetCurrentData();

                if (eventArgs.Success == false)
                {
                    return;
                }

                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    var loActiveData = await _viewModel.ActiveInactiveProcessAsync(loGetData);
                    await _conductorRef.R_SetCurrentData(loActiveData);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        #endregion
    }
}
