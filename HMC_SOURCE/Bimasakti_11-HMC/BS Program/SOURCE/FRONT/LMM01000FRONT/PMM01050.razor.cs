using BlazorClientHelper;
using PMM01000COMMON;
using PMM01000COMMON.Print;
using PMM01000FrontResources;
using PMM01000MODEL;
using Microsoft.AspNetCore.Components;
using R_APICommonDTO;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System.Globalization;
using System.Xml.Linq;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;

namespace PMM01000FRONT
{
    public partial class PMM01050 : R_Page, R_ITabPage
    {
        #region ViewModel
        private PMM01050ViewModel _viewModel = new PMM01050ViewModel();
        private PMM01050SaveBatchViewModel _viewModelSave = new PMM01050SaveBatchViewModel();
        private PMM01000UniversalViewModel _Universal_viewModel = new PMM01000UniversalViewModel();
        #endregion

        #region Conductor
        private R_Conductor _RateOT_conductorRef;
        private R_ConductorGrid _RateOTDetailWD_conductorRef;
        private R_ConductorGrid _RateOTDetailWK_conductorRef;
        #endregion

        #region Grid
        private R_Grid<PMM01051DTO> _RateOTDetailWD_gridRef;
        private R_Grid<PMM01051DTO> _RateOTDetailWK_gridRef;
        private R_Grid<PMM01050DTO> _RateOT_gridRef;
        #endregion

        #region Inject
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }
        [Inject] R_ILocalizer<PMM01000FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        #endregion

        #region Batch Proses
        // Create Method Action StateHasChange
        private void StateChangeInvoke()
        {
            StateHasChanged();
        }
        // Create Method Action For Error Unhandle
        private void ShowErrorInvoke(R_APIException poEx)
        {
            //var loEx = new R_Exception(poEx.ErrorList.Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.ErrNo, x.ErrDescp)).ToList());
            var loEx = new R_Exception(_viewModelSave.ErrorList);
            this.R_DisplayException(loEx);
        }
        // Create Method Action if proses is Complete Success
        private async Task ActionFuncIsCompleteSuccess()
        {
            var loEx = new R_Exception();

            try
            {
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            this.R_DisplayException(loEx);
        }
        #endregion

        #region Private Property Partial
        private bool EnableNormalMode = true;
        private PMM01050DTO _HeaderData = new PMM01050DTO();
        private R_RadioGroup<PMM01000UniversalDTO, string> AdminPerMonth_RadioGrp;
        private bool EnableHasData = true;
        #endregion

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                //Assign Action
                _viewModelSave.StateChangeAction = StateChangeInvoke;
                _viewModelSave.ShowErrorAction = ShowErrorInvoke;
                _viewModelSave.ActionIsCompleteSuccess = ActionFuncIsCompleteSuccess;


                PMM01050DTO loParam;
                loParam = R_FrontUtility.ConvertObjectToObject<PMM01050DTO>(poParameter);
                _HeaderData = loParam;

                await _viewModel.GetProperty(loParam);
                await _Universal_viewModel.GetAdminFeeTypeList();
                await _RateOT_gridRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
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
                var loData = (PMM01050DTO)eventArgs.Data;

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
                        Program_Id = "PMM01050",
                        Table_Name = "PMM_UTILITY_RATE_OT_HD",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CCHARGES_TYPE, loData.CCHARGES_ID, loData.CCHARGES_DATE)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMM01050",
                        Table_Name = "PMM_UTILITY_RATE_OT_HD",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CCHARGES_TYPE, loData.CCHARGES_ID, loData.CCHARGES_DATE)
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

        #region Form
        private async Task RateOTDate_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM01050DTO>(eventArgs.Parameter);
                await _viewModel.GetRateOTList(loParam);

                eventArgs.ListEntityResult = _viewModel.RateOTList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RateOT_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetRateOT((PMM01050DTO)eventArgs.Data);

                eventArgs.Result = _viewModel.RateOT;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RateOT_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
            {
                await AdminPerMonth_RadioGrp.FocusAsync();
            }
            if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Normal)
            {
                if (eventArgs.Data != null)
                {
                    var loData = (PMM01050DTO)eventArgs.Data;
                    if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Normal)
                    {
                        if (string.IsNullOrWhiteSpace(loData.CCHARGES_DATE) == false)
                        {
                            await _RateOTDetailWD_gridRef.R_RefreshGrid(loData);
                            await _RateOTDetailWK_gridRef.R_RefreshGrid(loData);
                        }
                    }
                }
            }
        }
        private async Task RateOT_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _RateOTDetailWD_gridRef.R_SaveBatch();
                await _RateOTDetailWK_gridRef.R_SaveBatch();
                var loData = (PMM01050DTO)eventArgs.Data;

                if (loData.DCHARGES_DATE == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMM01000FrontResources.Resources_Dummy_Class),
                        "10024"));
                }

                if (string.IsNullOrWhiteSpace(loData.CCURRENCY_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMM01000FrontResources.Resources_Dummy_Class),
                        "10025"));
                }

                if (_RateOTDetailWD_gridRef.DataSource.Count <= 0 && _RateOTDetailWK_gridRef.DataSource.Count <= 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMM01000FrontResources.Resources_Dummy_Class),
                        "10021"));
                }
                if (loData.LSPLIT_ADMIN && string.IsNullOrWhiteSpace(loData.CADMIN_CHARGE_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMM01000FrontResources.Resources_Dummy_Class),
                        "10022"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RateOT_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01050DTO)eventArgs.Data;
                loData.CRATE_OT_LIST = new List<PMM01051DTO>();
                loData.CRATE_OT_LIST.AddRange(_RateOTDetailWD_gridRef.DataSource);
                loData.CRATE_OT_LIST.AddRange(_RateOTDetailWK_gridRef.DataSource);
                loData.CCHARGES_DATE = loData.DCHARGES_DATE.Value.ToString("yyyyMMdd");

                await _viewModelSave.SaveDeleteRateOT(loData, (eCRUDMode)eventArgs.ConductorMode, clientHelper.CompanyId, clientHelper.UserId);

                eventArgs.Result = loData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void RateOT_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM01050DTO>(eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void RateOT_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM01050DTO>(eventArgs.Data);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Grid_R_SetOther(R_SetEventArgs eventArgs)
        {
            EnableNormalMode = eventArgs.Enable;
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }
        private async Task RateOT_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01050DTO)eventArgs.Data;
                loData.CRATE_OT_LIST = new List<PMM01051DTO>();
                loData.CRATE_OT_LIST.AddRange(_RateOTDetailWD_gridRef.DataSource);
                loData.CRATE_OT_LIST.AddRange(_RateOTDetailWK_gridRef.DataSource);

                await _viewModelSave.SaveDeleteRateOT(loData, eCRUDMode.DeleteMode, clientHelper.CompanyId, clientHelper.UserId);
                if (_RateOTDetailWD_gridRef.DataSource.Count > 0)
                {
                    _RateOTDetailWD_gridRef.DataSource.Clear();
                }
                if (_RateOTDetailWK_gridRef.DataSource.Count > 0)
                {
                    _RateOTDetailWK_gridRef.DataSource.Clear();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void RateOT_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loData = (PMM01050DTO)eventArgs.Data;
            loData.CADMIN_FEE = _Universal_viewModel.AdminFeeTypeList.FirstOrDefault().CCODE;
            loData.CCHARGES_ID = _HeaderData.CCHARGES_ID;
            loData.CCHARGES_NAME = _HeaderData.CCHARGES_NAME;
            loData.CPROPERTY_ID = _HeaderData.CPROPERTY_ID;
            loData.CCHARGES_TYPE = _HeaderData.CCHARGES_TYPE;
            loData.CADMIN_CHARGE_ID = "";
            loData.DCHARGES_DATE = DateTime.Now;

            if (_RateOTDetailWD_gridRef.DataSource.Count > 0)
            {
                _RateOTDetailWD_gridRef.DataSource.Clear();
            }
            if (_RateOTDetailWK_gridRef.DataSource.Count > 0)
            {
                _RateOTDetailWK_gridRef.DataSource.Clear();
            }
        }
        #endregion

        #region Value Change
        private void RateOT_Admin_OnChange(string poParam)
        {
            _viewModel.Data.CADMIN_FEE = poParam;

            if ((string)poParam == "01")
                _viewModel.Data.NADMIN_FEE_AMT = 0;

            if ((string)poParam == "02")
                _viewModel.Data.NADMIN_FEE_PCT = 0;
        }
        private void Rate_SplitAdmin_OnChange(bool poParam)
        {
            _viewModel.Data.LSPLIT_ADMIN = poParam;
            if (poParam == false)
            {
                _viewModel.Data.CADMIN_CHARGE_ID = "";
                _viewModel.Data.LADMIN_FEE_TAX = false;
            }
        }
        private void Rate_ChargeAdmin_OnChange(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.Data.CADMIN_CHARGE_ID = string.IsNullOrWhiteSpace(poParam) ? "" : poParam;
                if (string.IsNullOrWhiteSpace(poParam) == false)
                {
                    var loData = _viewModel.CADMIN_CHARGE_ID_LIST.FirstOrDefault(x => x.CCHARGES_ID == poParam);
                    _viewModel.Data.LADMIN_FEE_TAX = loData.LTAXABLE;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private void Rate_Holiday_OnChange(bool poParam)
        {
            _viewModel.Data.LHOLIDAY = poParam;
            if (!_viewModel.Data.LSATURDAY && !_viewModel.Data.LSUNDAY && !poParam)
            {
                if (_RateOTDetailWK_gridRef.DataSource.Count > 0)
                {
                    _RateOTDetailWK_gridRef.DataSource.Clear();
                }
            }
        }
        private void Rate_Saturday_OnChange(bool poParam)
        {
            _viewModel.Data.LSATURDAY = poParam;
            if (!_viewModel.Data.LHOLIDAY && !_viewModel.Data.LSUNDAY && !poParam)
            {
                if (_RateOTDetailWK_gridRef.DataSource.Count > 0)
                {
                    _RateOTDetailWK_gridRef.DataSource.Clear();
                }
            }
        }
        private void Rate_Sunday_OnChange(bool poParam)
        {
            _viewModel.Data.LSUNDAY = poParam;
            if (!_viewModel.Data.LSATURDAY && !_viewModel.Data.LHOLIDAY && !poParam)
            {
                if (_RateOTDetailWK_gridRef.DataSource.Count > 0)
                {
                    _RateOTDetailWK_gridRef.DataSource.Clear();
                }
            }
        }
        #endregion

        #region Detail Weekend
        private async Task RateOTDetailWK_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loEventParam = (PMM01050DTO)eventArgs.Parameter;
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM01051DTO>(loEventParam);

                await _viewModel.GetRateOTWKDetailList(loParam);

                eventArgs.ListEntityResult = _viewModel.RateOTWKDetailList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void RateWGDetailWK_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }
        private void RateOTWKDetail_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loParentData = (PMM01050DTO)_RateOT_conductorRef.R_GetCurrentData();
            var loData = (PMM01051DTO)eventArgs.Data;
            var loListInt = _RateOTDetailWK_gridRef.DataSource.Select(x => x.ILEVEL).ToList();
            if (loListInt.Count > 0)
            {
                loData.ILEVEL = loListInt.Max() + 1;
            }
            else
            {
                loData.ILEVEL = _RateOTDetailWD_gridRef.DataSource.Count + 1;
            }

            loData.SEQ_NO = _RateOTDetailWK_gridRef.DataSource.Count + 1;
            loData.CCOMPANY_ID = clientHelper.CompanyId;
            loData.CPROPERTY_ID = loParentData.CPROPERTY_ID;
            loData.CCHARGES_TYPE = loParentData.CCHARGES_TYPE;
            loData.CCHARGES_ID = loParentData.CCHARGES_ID;
            loData.CDAY_TYPE = "WK";
            loData.CCHARGES_DATE = loParentData.DCHARGES_DATE.Value.ToString("yyyyMMdd");
            loData.CLEVEL_DESC = string.IsNullOrWhiteSpace(loData.CLEVEL_DESC) ? "" : loData.CLEVEL_DESC;
        }
        private async Task RateOTWKDetail_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            bool lCancel;
            try
            {
                var loData = (PMM01051DTO)eventArgs.Data;
                var loListIntLevel = _RateOTDetailWK_gridRef.DataSource.Where(x => x.SEQ_NO != loData.SEQ_NO).Select(x => x.ILEVEL).ToList();

                if (loListIntLevel.Count > 0)
                {
                    lCancel = loData.ILEVEL <= loListIntLevel.Max();
                    if (lCancel)
                    {
                        loEx.Add("10026", string.Format(_localizer["10026"], _localizer["_Level"], loListIntLevel.Max()));
                    }
                }

                var loListIntTo = _RateOTDetailWK_gridRef.DataSource.Where(x => x.SEQ_NO != loData.SEQ_NO).Select(x => x.IHOURS_TO).ToList();

                if (loListIntTo.Count > 0)
                {
                    lCancel = loData.IHOURS_FROM != loListIntTo.Max() + 1;
                    if (lCancel)
                    {
                        loEx.Add("10028", string.Format(_localizer["10028"], _localizer["_WeekendOvertimeRate"], loListIntTo.Max() + 1));
                    }
                }

                lCancel = loData.IHOURS_FROM >= loData.IHOURS_TO;
                if (lCancel)
                {
                    loEx.Add("10027", string.Format(_localizer["10027"], _localizer["_WeekendOvertimeRate"], _localizer["_WeekendOvertimeRate"]));
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            eventArgs.Cancel = loEx.HasError;
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Detail Weekday
        private async Task RateOTDetailWD_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loEventParam = (PMM01050DTO)eventArgs.Parameter;
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM01051DTO>(loEventParam);

                await _viewModel.GetRateOTWDDetailList(loParam);

                eventArgs.ListEntityResult = _viewModel.RateOTWDDetailList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void RateOTDetailWD_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }
        private void RateOTWDDetail_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loParentData = (PMM01050DTO)_RateOT_conductorRef.R_GetCurrentData();
            var loData = (PMM01051DTO)eventArgs.Data;
            var loListInt = _RateOTDetailWD_gridRef.DataSource.Select(x => x.ILEVEL).ToList();
            if (loListInt.Count > 0)
            {
                loData.ILEVEL = loListInt.Max() + 1;
            }
            else
            {
                loData.ILEVEL = _RateOTDetailWD_gridRef.DataSource.Count + 1;
            }
            loData.SEQ_NO = _RateOTDetailWD_gridRef.DataSource.Count + 1;
            loData.CCOMPANY_ID = clientHelper.CompanyId;
            loData.CPROPERTY_ID = loParentData.CPROPERTY_ID;
            loData.CCHARGES_TYPE = loParentData.CCHARGES_TYPE;
            loData.CCHARGES_ID = loParentData.CCHARGES_ID;
            loData.CDAY_TYPE = "WD";
            loData.CCHARGES_DATE = loParentData.DCHARGES_DATE.Value.ToString("yyyyMMdd");
            loData.CLEVEL_DESC = string.IsNullOrWhiteSpace(loData.CLEVEL_DESC) ? "" : loData.CLEVEL_DESC;

        }
        private async Task RateOTWDDetail_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            bool lCancel;
            try
            {
                var loData = (PMM01051DTO)eventArgs.Data;
                var loListIntLevel = _RateOTDetailWD_gridRef.DataSource.Where(x => x.SEQ_NO != loData.SEQ_NO).Select(x => x.ILEVEL).ToList();

                if (loListIntLevel.Count > 0)
                {
                    lCancel = loData.ILEVEL <= loListIntLevel.Max();
                    if (lCancel)
                    {
                        loEx.Add("10026", string.Format(_localizer["10026"], _localizer["_Level"], loListIntLevel.Max()));
                    }
                }

                var loListIntTo = _RateOTDetailWD_gridRef.DataSource.Where(x => x.SEQ_NO != loData.SEQ_NO).Select(x => x.IHOURS_TO).ToList();

                if (loListIntTo.Count > 0)
                {
                    lCancel = loData.IHOURS_FROM != loListIntTo.Max() + 1;
                    if (lCancel)
                    {
                        loEx.Add("10028", string.Format(_localizer["10028"], _localizer["_WeekdayOvertimeRate"], loListIntTo.Max() + 1));
                    }
                }

                lCancel = loData.IHOURS_FROM >= loData.IHOURS_TO;
                if (lCancel)
                {
                    loEx.Add("10027", string.Format(_localizer["10027"], _localizer["_WeekdayOvertimeRate"], _localizer["_WeekdayOvertimeRate"]));
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            eventArgs.Cancel = loEx.HasError;
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Currency Lookup
        private async Task Currency_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01050DTO)_RateOT_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loData.CCURRENCY_CODE) == false)
                {
                    GSL00300ParameterDTO loParam = new GSL00300ParameterDTO()
                    {
                        CSEARCH_TEXT = loData.CCURRENCY_CODE
                    };

                    LookupGSL00300ViewModel loLookupViewModel = new LookupGSL00300ViewModel();

                    var loResult = await loLookupViewModel.GetCurrency(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        goto EndBlock;
                    }
                    loData.CCURRENCY_CODE = loResult.CCURRENCY_CODE;
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void Currency_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00300ParameterDTO loParam = new GSL00300ParameterDTO()
            {
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00300);
        }
        private void Currency_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSL00300DTO loTempResult = (GSL00300DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }
                var loData = (PMM01050DTO)_RateOT_conductorRef.R_GetCurrentData();
                loData.CCURRENCY_CODE = loTempResult.CCURRENCY_CODE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        public async Task RefreshTabPageAsync(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM01050DTO>(poParam);
                EnableHasData = string.IsNullOrWhiteSpace(loParam.CCHARGES_ID) == false;
                if (string.IsNullOrWhiteSpace(loParam.CCHARGES_ID))
                {
                    _HeaderData.CCHARGES_TYPE_DESCR = "";
                    _viewModel.R_SetCurrentData(new PMM01050DTO());
                    if (_RateOT_gridRef.DataSource.Count > 0)
                    {
                        _RateOT_gridRef.DataSource.Clear();
                    }
                    if (_RateOTDetailWD_gridRef.DataSource.Count > 0)
                    {
                        _RateOTDetailWD_gridRef.DataSource.Clear();
                    }
                    if (_RateOTDetailWK_gridRef.DataSource.Count > 0)
                    {
                        _RateOTDetailWK_gridRef.DataSource.Clear();
                    }
                }
                else
                {
                    _HeaderData = loParam;
                    await _viewModel.GetProperty(loParam);

                    await _RateOT_gridRef.R_RefreshGrid(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task Button_OnClickPrintAsync()
        {
            var loEx = new R_Exception();

            try
            {
                // set Param
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM01050PrintParamDTO>(_viewModel.RateOT);
                
                loParam.CUSER_ID = clientHelper.UserId;
                loParam.CCOMPANY_ID = clientHelper.CompanyId;
                loParam.CREPORT_CULTURE = clientHelper.ReportCulture;
                loParam.CPROPERTY_NAME = _viewModel.Property.CPROPERTY_NAME;

                var loValidate = await R_MessageBox.Show("", _localizer["_NotifPrint"], R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await _reportService.GetReport(
                     "R_DefaultServiceUrlPM",
                     "PM",
                     "rpt/PMM01050Print/AllRateOTReportPost",
                     "rpt/PMM01050Print/AllStreamRateOTReportGet",
                     loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
