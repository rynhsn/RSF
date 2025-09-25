using BlazorClientHelper;
using PMM01000COMMON;
using PMM01000COMMON.Print;
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
using System;
using System.Globalization;
using R_BlazorFrontEnd.Extensions;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel.ViewModel;
using Lookup_GSFRONT;

namespace PMM01000FRONT
{
    public partial class PMM01010 : R_Page, R_ITabPage
    {
        #region ViewModel
        private PMM01010ViewModel _viewModel = new PMM01010ViewModel();
        private PMM01010SaveBatchViewModel _viewModelSave = new PMM01010SaveBatchViewModel();
        private PMM01000UniversalViewModel _Universal_viewModel = new PMM01000UniversalViewModel();
        #endregion

        #region Condutor & Grid
        private R_Conductor _RateUC_conductorRef;
        private R_ConductorGrid _RateUCDetail_conductorRef;
        private R_Grid<PMM01010DTO> _RateUC_gridRef;
        private R_Grid<PMM01011DTO> _RateUCDetail_gridRef;
        #endregion

        #region Private Property
        private R_RadioGroup<PMM01000UniversalDTO, string> UsageRateMode_RadioGrp;
        private bool _IsHMMode;
        private bool HasError = false;
        private bool SuccesSaveBulk = false;
        private PMM01010DTO _HeaderData = new PMM01010DTO();
        private bool EnableNormalMode = true;
        private bool EnableHasData = true;
        #endregion

        #region Inject
        [Inject] private R_IReport _reportService { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }
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
            SuccesSaveBulk = false;
        }
        // Create Method Action if proses is Complete Success
        private async Task ActionFuncIsCompleteSuccess()
        {
            var loEx = new R_Exception();

            try
            {
                SuccesSaveBulk = true;
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            this.R_DisplayException(loEx);
        }
        #endregion

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            PMM01010DTO loParam = null;

            try
            {
                //Assign Action
                _viewModelSave.StateChangeAction = StateChangeInvoke;
                _viewModelSave.ShowErrorAction = ShowErrorInvoke;
                _viewModelSave.ActionIsCompleteSuccess = ActionFuncIsCompleteSuccess;

                //Load Radio Button
                await _Universal_viewModel.GetUsageRateModelList();
                await _Universal_viewModel.GetRateTypeList();
                await _Universal_viewModel.GetAdminFeeTypeList();

                loParam = R_FrontUtility.ConvertObjectToObject<PMM01010DTO>(poParameter);
                _HeaderData = loParam;
                await _viewModel.GetProperty(loParam);

                await _RateUC_gridRef.R_RefreshGrid(loParam);
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
                var loData = (PMM01010DTO)eventArgs.Data;

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
                        Program_Id = "PMM01010",
                        Table_Name = "PMM_UTILITY_RATE_EC_HD",
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
                        Program_Id = "PMM01010",
                        Table_Name = "PMM_UTILITY_RATE_EC_HD",
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
        private async Task RateUCDate_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM01010DTO>(eventArgs.Parameter);
                await _viewModel.GetRateUCList(loParam);

                eventArgs.ListEntityResult = _viewModel.RateUCList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RateUC_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetRateEC((PMM01010DTO)eventArgs.Data);

                eventArgs.Result = _viewModel.RateEC;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RateUC_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01010DTO)eventArgs.Data;
                if (loData != null)
                {
                    if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
                    {
                        await UsageRateMode_RadioGrp.FocusAsync();
                        _IsHMMode = loData.CUSAGE_RATE_MODE == "HM";
                    }

                    if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Normal)
                    {
                        if (string.IsNullOrWhiteSpace(loData.CCHARGES_DATE) == false)
                        {
                            await _RateUCDetail_gridRef.R_RefreshGrid(loData);
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
        private async Task RateUC_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01010DTO)eventArgs.Data;

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

                if (loData.CUSAGE_RATE_MODE == "HM" && _RateUCDetail_gridRef.DataSource.Count <= 0)
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

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RateUC_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01010DTO)eventArgs.Data;
                loData.CCHARGES_DATE = loData.DCHARGES_DATE.Value.ToString("yyyyMMdd");
                loData.CRATE_EC_LIST = new List<PMM01011DTO>();
                if (_RateUCDetail_gridRef.DataSource.Count > 0)
                {
                    loData.CRATE_EC_LIST = _RateUCDetail_gridRef.DataSource.ToList();
                }
                await _viewModelSave.SaveDeleteRateEC(loData, (eCRUDMode)eventArgs.ConductorMode, clientHelper.CompanyId, clientHelper.UserId);

                eventArgs.Result = loData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RateUC_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01010DTO)eventArgs.Data;
                loData.CRATE_EC_LIST = _RateUCDetail_gridRef.DataSource.ToList();
                await _viewModelSave.SaveDeleteRateEC(loData, eCRUDMode.DeleteMode, clientHelper.CompanyId, clientHelper.UserId);
                if (_RateUCDetail_gridRef.DataSource.Count > 0)
                {
                    _RateUCDetail_gridRef.DataSource.Clear();
                }
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
        private void RateUC_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loData = (PMM01010DTO)eventArgs.Data;
            loData.CUSAGE_RATE_MODE = _Universal_viewModel.UsageRateModeList.FirstOrDefault().CCODE;
            _IsHMMode = loData.CUSAGE_RATE_MODE == "HM";
            loData.CRATE_TYPE = _Universal_viewModel.RateTypeList.FirstOrDefault().CCODE;
            loData.CADMIN_FEE = _Universal_viewModel.AdminFeeTypeList.FirstOrDefault().CCODE;
            loData.CCHARGES_ID = _HeaderData.CCHARGES_ID;
            loData.CCHARGES_NAME = _HeaderData.CCHARGES_NAME;   
            loData.CPROPERTY_ID = _HeaderData.CPROPERTY_ID;
            loData.CCHARGES_TYPE = _HeaderData.CCHARGES_TYPE;
            loData.CADMIN_CHARGE_ID = "";
            loData.DCHARGES_DATE = DateTime.Now;

            if (_RateUCDetail_gridRef.DataSource.Count > 0)
            {
                _RateUCDetail_gridRef.DataSource.Clear();
            }

        }
        #endregion

        #region Currency Lookup
        private async Task Currency_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01010DTO)_RateUC_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loData.CACTION) == false)
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
                var loData = (PMM01010DTO)_RateUC_conductorRef.R_GetCurrentData();
                loData.CCURRENCY_CODE = loTempResult.CCURRENCY_CODE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region On Change
        private void RateUc_Admin_OnChange(string poParam)
        {
            _viewModel.Data.CADMIN_FEE = poParam;
            if ((string)poParam == "01")
                _viewModel.Data.NADMIN_FEE_AMT = 0;

            if ((string)poParam == "02")
                _viewModel.Data.NADMIN_FEE_PCT = 0;
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
        private void Rate_SplitAdmin_OnChange(bool poParam)
        {
            _viewModel.Data.LSPLIT_ADMIN = poParam;
            if (poParam == false)
            {
                _viewModel.Data.CADMIN_CHARGE_ID = "";
                _viewModel.Data.LADMIN_FEE_TAX = false;
            }
        }
        #endregion

        #region Detail Grid
        private async Task RateUC_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM01011DTO>(eventArgs.Parameter);
                await _viewModel.GetRateUCDetailList(loParam);

                eventArgs.ListEntityResult = _viewModel.RateUCDetailList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RateUCDetail_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            bool lCancel;
            try
            {
                var loData = (PMM01011DTO)eventArgs.Data;
                var loListInt = _RateUCDetail_gridRef.DataSource.Where(x => x.SEQ_NO != loData.SEQ_NO).Select(x => x.IUP_TO_USAGE).ToList();

                if (loListInt.Count > 0)
                {
                    lCancel = loData.IUP_TO_USAGE <= loListInt.Max();
                    if (lCancel)
                    {
                        loEx.Add("10026", string.Format(_localizer["10026"], _localizer["_UsageKwh"], loListInt.Max()));
                    }
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
        private void RateUCDetail_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }
        private void RateUCDetail_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loParentData = (PMM01010DTO)_RateUC_conductorRef.R_GetCurrentData();
            var loData = (PMM01011DTO)eventArgs.Data;
            loData.SEQ_NO = _RateUCDetail_gridRef.DataSource.Count + 1;
            loData.CCOMPANY_ID = clientHelper.CompanyId;
            loData.CPROPERTY_ID = loParentData.CPROPERTY_ID;
            loData.CCHARGES_TYPE = loParentData.CCHARGES_TYPE;
            loData.CCHARGES_ID = loParentData.CCHARGES_ID;
            loData.CCHARGES_DATE = loParentData.DCHARGES_DATE.Value.ToString("yyyyMMdd");

            loData.CUSAGE_DESC = string.IsNullOrWhiteSpace(loData.CUSAGE_DESC) ? "" : loData.CUSAGE_DESC;
        }
        #endregion

        #region Refresh Tab Property
        public async Task RefreshTabPageAsync(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM01010DTO>(poParam);
                _HeaderData = loParam;
                EnableHasData = string.IsNullOrWhiteSpace(loParam.CCHARGES_ID) == false;
                if (string.IsNullOrWhiteSpace(loParam.CCHARGES_ID))
                {
                    _HeaderData.CCHARGES_TYPE_DESCR = "";
                    _viewModel.R_SetCurrentData(new PMM01010DTO());
                    if (_RateUC_gridRef.DataSource.Count > 0)
                    {
                        _RateUC_gridRef.DataSource.Clear();
                    }
                    if (_RateUCDetail_gridRef.DataSource.Count > 0)
                    {
                        _RateUCDetail_gridRef.DataSource.Clear();
                    }
                }
                else
                {
                    await _viewModel.GetProperty(loParam);

                    await _RateUC_gridRef.R_RefreshGrid(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Method View
        public async Task Button_OnClickPrintAsync()
        {
            var loEx = new R_Exception();

            try
            {
                // set Param
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM01010PrintParamDTO>(_viewModel.RateEC);
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
                     "rpt/PMM01010Print/AllRateECReportPost",
                     "rpt/PMM01010Print/AllStreamRateECReportGet",
                     loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public void UsageRateMode_OnChange(string poParam)
        {
            _viewModel.Data.CUSAGE_RATE_MODE = poParam;
            _IsHMMode = poParam == "HM";
            if (poParam == "SM")
            {
                _RateUCDetail_gridRef.DataSource.Clear();
            }
        }
        #endregion
    }
}
