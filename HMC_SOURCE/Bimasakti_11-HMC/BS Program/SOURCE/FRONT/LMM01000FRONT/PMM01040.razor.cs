using BlazorClientHelper;
using PMM01000COMMON;
using PMM01000COMMON.Print;
using PMM01000MODEL;
using Microsoft.AspNetCore.Components;
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
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;

namespace PMM01000FRONT
{
    public partial class PMM01040 : R_Page, R_ITabPage
    {
        #region ViewModel
        private PMM01040ViewModel _viewModel = new PMM01040ViewModel();
        private PMM01000UniversalViewModel _Universal_viewModel = new PMM01000UniversalViewModel();
        #endregion

        #region Conductor & Grid
        private R_Conductor _RateGU_conductorRef;
        private R_Grid<PMM01040DTO> _RateGU_gridRef;
        #endregion

        #region Inject
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }
        [Inject] R_ILocalizer<PMM01000FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        #endregion

        #region Private Property
        private PMM01040DTO _HeaderData = new PMM01040DTO();
        private bool EnableNormalMode = true;
        private bool EnableHasData = true;
        private R_NumericTextBox<decimal> StandingCharges_NumericTextBox;
        #endregion

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                PMM01040DTO loParam;
                loParam = R_FrontUtility.ConvertObjectToObject<PMM01040DTO>(poParameter);
                _HeaderData = loParam;

                await _viewModel.GetProperty(loParam);
                await _Universal_viewModel.GetAdminFeeTypeList();
                await _RateGU_gridRef.R_RefreshGrid(loParam);

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
                var loData = (PMM01040DTO)eventArgs.Data;

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
                        Program_Id = "PMM01040",
                        Table_Name = "PMM_UTILITY_RATE_GU",
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
                        Program_Id = "PMM01040",
                        Table_Name = "PMM_UTILITY_RATE_GU",
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
        private async Task RateGUDate_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM01040DTO>(eventArgs.Parameter);
                await _viewModel.GetRateGUList(loParam);

                eventArgs.ListEntityResult = _viewModel.RateGUList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RateWG_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetRateUG((PMM01040DTO)eventArgs.Data);

                eventArgs.Result = _viewModel.RateGU;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RateGU_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
            {
                await StandingCharges_NumericTextBox.FocusAsync();
            }
        }
        private void RateGU_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01040DTO)eventArgs.Data;
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
        private async Task RateGU_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01040DTO)eventArgs.Data;
                loData.CCHARGES_DATE = loData.DCHARGES_DATE.Value.ToString("yyyyMMdd");

                await _viewModel.SaveRateGU(loData, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.RateGU;
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
        private async Task RateGU_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01040DTO)eventArgs.Data;
                await _viewModel.DeleteRateGU(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void RateGU_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loData = (PMM01040DTO)eventArgs.Data;
            loData.CADMIN_FEE = _Universal_viewModel.AdminFeeTypeList.FirstOrDefault().CCODE;
            loData.CCHARGES_ID = _HeaderData.CCHARGES_ID;
            loData.CCHARGES_NAME = _HeaderData.CCHARGES_NAME;
            loData.CPROPERTY_ID = _HeaderData.CPROPERTY_ID;
            loData.CCHARGES_TYPE = _HeaderData.CCHARGES_TYPE;
            loData.CADMIN_CHARGE_ID = "";
            loData.DCHARGES_DATE = DateTime.Now;
        }
        #endregion

        #region Currency Lookup
        private async Task Currency_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM01040DTO)_RateGU_conductorRef.R_GetCurrentData();
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
                var loData = (PMM01040DTO)_RateGU_conductorRef.R_GetCurrentData();
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
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM01040DTO>(poParam);
                EnableHasData = string.IsNullOrWhiteSpace(loParam.CCHARGES_ID) == false;
                if (string.IsNullOrWhiteSpace(loParam.CCHARGES_ID))
                {
                    _HeaderData.CCHARGES_TYPE_DESCR = "";
                    _viewModel.R_SetCurrentData(new PMM01040DTO());
                    if (_RateGU_gridRef.DataSource.Count > 0)
                    {
                        _RateGU_gridRef.DataSource.Clear();
                    }
                }
                else
                {
                    _HeaderData = loParam;
                    await _viewModel.GetProperty(loParam);

                    await _RateGU_gridRef.R_RefreshGrid(loParam);
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
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM01040PrintParamDTO>(_viewModel.RateGU);

                loParam.CCOMPANY_ID = clientHelper.CompanyId;
                loParam.CUSER_ID = clientHelper.UserId;
                loParam.CREPORT_CULTURE = clientHelper.ReportCulture;
                loParam.CPROPERTY_NAME = _viewModel.Property.CPROPERTY_NAME;

                var loValidate = await R_MessageBox.Show("", _localizer["_NotifPrint"], R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await _reportService.GetReport(
                     "R_DefaultServiceUrlPM",
                     "PM",
                     "rpt/PMM01040Print/AllRateGUReportPost",
                     "rpt/PMM01040Print/AllStreamRateGUReportGet",
                     loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Value Change
        private void RateGU_Admin_OnChange(string poParam)
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
        #endregion
    }
}
