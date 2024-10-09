using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00400;
using Lookup_PMModel.ViewModel.LML00600;
using Lookup_PMModel.ViewModel.LML00800;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using PMT06000Common.DTOs;
using PMT06000Common.Params;
using PMT06000Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;

namespace PMT06000Front;

public partial class PMT06000Info : R_Page
{
    private PMT06000ViewModel _viewModel = new();
    private R_Conductor _conductorRef;

    private PMT06000ServiceViewModel _viewModelService = new();
    private R_Conductor _conductorRefService;
    private R_Grid<PMT06000OvtServiceGridDTO> _gridRefService;

    #region Locking

    [Inject] private IClientHelper _clientHelper { get; set; }
    [Inject] private IJSRuntime JS { get; set; }

    private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
    private const string DEFAULT_MODULE_NAME = "PM";

    protected override async Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        var llRtn = false;
        R_LockingFrontResult loLockResult;

        try
        {
            var loData = (PMT06000OvtDTO)eventArgs.Data;

            var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                plSendWithContext: true,
                plSendWithToken: true,
                pcHttpClientName: DEFAULT_HTTP_NAME);

            var Company_Id = _clientHelper.CompanyId;
            var User_Id = _clientHelper.UserId;
            var Program_Id = "PMT06000";
            var Table_Name = "PMT_OVERTIME";
            var Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE,
                loData.CTRANS_CODE, loData.CREF_NO);

            if (eventArgs.Mode == R_eLockUnlock.Lock)
            {
                var loLockPar = new R_ServiceLockingLockParameterDTO
                {
                    Company_Id = Company_Id,
                    User_Id = User_Id,
                    Program_Id = Program_Id,
                    Table_Name = Table_Name,
                    Key_Value = Key_Value
                };
                loLockResult = await loCls.R_Lock(loLockPar);
            }
            else
            {
                var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                {
                    Company_Id = Company_Id,
                    User_Id = User_Id,
                    Program_Id = Program_Id,
                    Table_Name = Table_Name,
                    Key_Value = Key_Value
                };
                loLockResult = await loCls.R_UnLock(loUnlockPar);
            }

            llRtn = loLockResult.IsSuccess;
            if (loLockResult is { IsSuccess: false, Exception: not null })
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

    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetPeriodList();
            await _viewModel.GetPropertyList();
            await _viewModel.GetYearRange();

            _viewModel.Caller = (PMT06000ParameterDTO)poParam;
            var loParam = R_FrontUtility.ConvertObjectToObject<PMT06000OvtDTO>(_viewModel.Caller);
            await _conductorRef.R_GetEntity(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #region Lookup Detail

    private async Task OnLostFocusDept()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL00710ViewModel();
        try
        {
            if (_viewModel.Data.CDEPT_CODE == null || _viewModel.Data.CDEPT_CODE.Trim().Length <= 0)
            {
                _viewModel.Data.CDEPT_NAME = "";
                return;
            }

            var param = new GSL00710ParameterDTO
            {
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                CSEARCH_TEXT = _viewModel.Data.CDEPT_CODE
            };

            var loResult = await loLookupViewModel.GetDepartmentProperty(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.Data.CDEPT_CODE = "";
                _viewModel.Data.CDEPT_NAME = "";
                goto EndBlock;
            }

            _viewModel.Data.CDEPT_CODE = loResult.CDEPT_CODE;
            _viewModel.Data.CDEPT_NAME = loResult.CDEPT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        await R_DisplayExceptionAsync(loEx);
    }

    private void BeforeOpenLookupDept(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParameter = new GSL00710ParameterDTO
            {
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID
            };

            eventArgs.Parameter = loParameter;
            eventArgs.TargetPageType = typeof(GSL00710);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void AfterOpenLookupDept(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.Result == null) return;

            var loTempResult = (GSL00710DTO)eventArgs.Result;
            _viewModel.Data.CDEPT_CODE = loTempResult.CDEPT_CODE;
            _viewModel.Data.CDEPT_NAME = loTempResult.CDEPT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnLostFocusTenant()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupLML00600ViewModel();
        try
        {
            if (_viewModel.Data.CTENANT_ID == null || _viewModel.Data.CTENANT_ID.Trim().Length <= 0)
            {
                _viewModel.Data.CTENANT_NAME = "";

                _viewModel.Data.CAGREEMENT_NO = "";
                _viewModel.Data.CUNIT_DESCRIPTION = "";
                return;
            }

            var param = new LML00600ParameterDTO
            {
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                CCUSTOMER_TYPE = "",
                CSEARCH_TEXT = _viewModel.Data.CTENANT_ID
            };

            var loResult = await loLookupViewModel.GetTenant(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.Data.CTENANT_ID = "";
                _viewModel.Data.CTENANT_NAME = "";

                _viewModel.Data.CAGREEMENT_NO = "";
                _viewModel.Data.CUNIT_DESCRIPTION = "";
                goto EndBlock;
            }

            _viewModel.Data.CTENANT_ID = loResult.CTENANT_ID;
            _viewModel.Data.CTENANT_NAME = loResult.CTENANT_NAME;

            _viewModel.Data.CAGREEMENT_NO = "";
            _viewModel.Data.CUNIT_DESCRIPTION = "";
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        await R_DisplayExceptionAsync(loEx);
    }

    private void BeforeOpenLookupTenant(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParameter = new LML00600ParameterDTO
            {
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                CCUSTOMER_TYPE = ""
            };

            eventArgs.Parameter = loParameter;
            eventArgs.TargetPageType = typeof(LML00600);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void AfterOpenLookupTenant(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.Result == null) return;

            var loTempResult = (LML00600DTO)eventArgs.Result;

            if (loTempResult.CTENANT_ID == _viewModel.Data.CTENANT_ID) return;

            _viewModel.Data.CTENANT_ID = loTempResult.CTENANT_ID;
            _viewModel.Data.CTENANT_NAME = loTempResult.CTENANT_NAME;
            _viewModel.Data.CAGREEMENT_NO = "";
            _viewModel.Data.CUNIT_DESCRIPTION = "";
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnLostFocusBuilding()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL02200ViewModel();
        try
        {
            if (_viewModel.Data.CBUILDING_ID == null || _viewModel.Data.CBUILDING_ID.Trim().Length <= 0)
            {
                _viewModel.Data.CBUILDING_NAME = "";

                _viewModel.Data.CAGREEMENT_NO = "";
                _viewModel.Data.CUNIT_DESCRIPTION = "";
                return;
            }

            var param = new GSL02200ParameterDTO
            {
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                CSEARCH_TEXT = _viewModel.Data.CBUILDING_ID
            };

            var loResult = await loLookupViewModel.GetBuilding(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.Data.CBUILDING_ID = "";
                _viewModel.Data.CBUILDING_NAME = "";

                _viewModel.Data.CAGREEMENT_NO = "";
                _viewModel.Data.CUNIT_DESCRIPTION = "";
                goto EndBlock;
            }

            _viewModel.Data.CBUILDING_ID = loResult.CBUILDING_ID;
            _viewModel.Data.CBUILDING_NAME = loResult.CBUILDING_NAME;

            _viewModel.Data.CAGREEMENT_NO = "";
            _viewModel.Data.CUNIT_DESCRIPTION = "";
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        await R_DisplayExceptionAsync(loEx);
    }

    private void BeforeOpenLookupBuilding(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParameter = new GSL02200ParameterDTO
            {
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID
            };

            eventArgs.Parameter = loParameter;
            eventArgs.TargetPageType = typeof(GSL02200);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void AfterOpenLookupBuilding(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.Result == null) return;

            var loTempResult = (GSL02200DTO)eventArgs.Result;

            if (loTempResult.CBUILDING_ID == _viewModel.Data.CBUILDING_ID) return;

            _viewModel.Data.CBUILDING_ID = loTempResult.CBUILDING_ID;
            _viewModel.Data.CBUILDING_NAME = loTempResult.CBUILDING_NAME;
            _viewModel.Data.CAGREEMENT_NO = "";
            _viewModel.Data.CUNIT_DESCRIPTION = "";
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnLostFocusAgreement()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupLML00800ViewModel();
        try
        {
            if (_viewModel.Data.CAGREEMENT_NO == null || _viewModel.Data.CAGREEMENT_NO.Trim().Length <= 0)
            {
                _viewModel.Data.CUNIT_DESCRIPTION = "";
                _viewModel.Data.CLINK_DEPT_CODE = "";
                _viewModel.Data.CLINK_TRANS_CODE = "";
                return;
            }

            var param = new LML00800ParameterDTO
            {
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                CDEPT_CODE = _viewModel.Data.CDEPT_CODE,
                CTRANS_CODE = _viewModel.TRANS_CODE,
                CTENANT_ID = _viewModel.Data.CTENANT_ID,
                CBUILDING_ID = _viewModel.Data.CBUILDING_ID,
                CTRANS_STATUS = "30,80",
                CAGGR_STTS = "",
                CSEARCH_TEXT = _viewModel.Data.CAGREEMENT_NO
            };

            var loResult = await loLookupViewModel.GetAgreement(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.Data.CAGREEMENT_NO = "";
                _viewModel.Data.CUNIT_DESCRIPTION = "";
                _viewModel.Data.CLINK_DEPT_CODE = "";
                _viewModel.Data.CLINK_TRANS_CODE = "";
                goto EndBlock;
            }

            _viewModel.Data.CAGREEMENT_NO = loResult.CREF_NO;
            _viewModel.Data.CUNIT_DESCRIPTION = loResult.CUNIT_DESCRIPTION;
            _viewModel.Data.CLINK_DEPT_CODE = loResult.CDEPT_CODE;
            _viewModel.Data.CLINK_TRANS_CODE = loResult.CTRANS_CODE;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        await R_DisplayExceptionAsync(loEx);
    }

    private void BeforeOpenLookupRef(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loParameter = new LML00800ParameterDTO
            {
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                CDEPT_CODE = _viewModel.Data.CDEPT_CODE,
                CTRANS_CODE = _viewModel.TRANS_CODE,
                CTENANT_ID = _viewModel.Data.CTENANT_ID,
                CBUILDING_ID = _viewModel.Data.CBUILDING_ID,
                CTRANS_STATUS = "30,80",
                CAGGR_STTS = ""
            };

            eventArgs.Parameter = loParameter;
            eventArgs.TargetPageType = typeof(LML00800);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void AfterOpenLookupRef(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.Result == null) return;

            var loTempResult = (LML00800DTO)eventArgs.Result;
            _viewModel.Data.CAGREEMENT_NO = loTempResult.CREF_NO;
            _viewModel.Data.CUNIT_DESCRIPTION = loTempResult.CUNIT_DESCRIPTION;
            _viewModel.Data.CLINK_DEPT_CODE = loTempResult.CDEPT_CODE;
            _viewModel.Data.CLINK_TRANS_CODE = loTempResult.CTRANS_CODE;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    #region CRUD Detail

    private R_TabPage _pageUnit { get; set; } = new();

    private async Task ServiceGetRecordOvt(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<PMT06000OvtDTO>(eventArgs.Data);
            await _viewModel.GetEntity(loParam);
            eventArgs.Result = _viewModel.Entity;
            if (!string.IsNullOrEmpty(_viewModel.Entity.CREC_ID))
            {
                await _gridRefService.R_RefreshGrid(null);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void ValidationOvt(R_ValidationEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loEntity = (PMT06000OvtDTO)eventArgs.Data;
            if (string.IsNullOrEmpty(loEntity.CPROPERTY_ID))
            {
                loEx.Add(new Exception(_localizer["PLEASE_SELECT_PROPERTY"]));
            }

            if (string.IsNullOrEmpty(loEntity.CDEPT_CODE))
            {
                loEx.Add(new Exception(_localizer["PLEASE_SELECT_DEPARTMENT"]));
            }

            if (string.IsNullOrEmpty(loEntity.CTENANT_ID))
            {
                loEx.Add(new Exception(_localizer["PLEASE_SELECT_TENANT"]));
            }

            if (string.IsNullOrEmpty(loEntity.CBUILDING_ID))
            {
                loEx.Add(new Exception(_localizer["PLEASE_SELECT_BUILDING"]));
            }

            if (string.IsNullOrEmpty(loEntity.DREF_DATE?.ToString("yyyyMMdd")))
            {
                loEx.Add(new Exception(_localizer["REF_DATE_REQ"]));
            }

            if (string.IsNullOrEmpty(loEntity.CREQUEST_NAME))
            {
                loEx.Add(new Exception(_localizer["REQ_NAME_REQ"]));
            }

            if (string.IsNullOrEmpty(loEntity.IINV_YEAR.ToString()))
            {
                loEx.Add(new Exception(_localizer["PLEASE_SELECT_INV_YEAR"]));
            }

            if (string.IsNullOrEmpty(loEntity.CINV_MONTH))
            {
                loEx.Add(new Exception(_localizer["PLEASE_SELECT_INV_MONTH"]));
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void SavingOvt(R_SavingEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loEntity = (PMT06000OvtDTO)eventArgs.Data;
            loEntity.CREF_DATE = loEntity.DREF_DATE?.ToString("yyyyMMdd");
            loEntity.CINV_YEAR = loEntity.IINV_YEAR.ToString();
            loEntity.CINV_PRD = loEntity.CINV_YEAR + loEntity.CINV_MONTH;
            loEntity.CREC_ID = eventArgs.ConductorMode == R_eConductorMode.Add ? "" : loEntity.CREC_ID;
            loEntity.CREF_NO = eventArgs.ConductorMode == R_eConductorMode.Add ? "" : loEntity.CREF_NO;
            loEntity.CTRANS_CODE = _viewModel.TRANS_CODE;
            loEntity.CTRANS_STATUS = "00";
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task ServiceSaveOvt(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<PMT06000OvtDTO>(eventArgs.Data);
            await _viewModel.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);

            eventArgs.Result = _viewModel.Entity;
            await _pageUnit.InvokeRefreshTabPageAsync(_viewModel.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task BeforeDeleteOvt(R_BeforeDeleteEventArgs eventArgs)
    {
        var leMsg = await R_MessageBox.Show("", _localizer["MSG_BEFORE_DELETE_OVT"], R_eMessageBoxButtonType.YesNo);
        eventArgs.Cancel = leMsg != R_eMessageBoxResult.Yes;
    }

    private async Task ServiceDeleteOvt(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (PMT06000OvtDTO)eventArgs.Data;
            await _viewModel.DeleteEntity(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task AfterDeleteOvt()
    {
        var loEx = new R_Exception();

        try
        {
            var leMsg = await R_MessageBox.Show("", _localizer["MSG_AFTER_DELETE_OVT"]);
            await _gridRefService.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void AfterAddOvt(R_AfterAddEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loEntity = (PMT06000OvtDTO)eventArgs.Data;

            loEntity.CPROPERTY_ID = _viewModel.SelectedPropertyId;
            loEntity.CINV_MONTH = _viewModel.SelectedPeriodNo;
            loEntity.IINV_YEAR = _viewModel.SelectedYear;
            loEntity.DREF_DATE = DateTime.Today;

            // await _gridRefService.R_RefreshGrid(null);
            if (_viewModel.OvertimeServiceGridList.Count > 0)
            {
                _viewModel.OvertimeServiceGridList.Clear();
                _viewModelService.Data.CSERVICE_ID = "";
                _viewModelService.Data.CSERVICE_NAME = "";
                _viewModelService.Data.DDATE_IN = null;
                _viewModelService.Data.DDATE_OUT = null;
                _viewModelService.Data.CSERVICE_DESCR = "";
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private bool _detailIsNormal = true;

    private async Task AfterSaveOvt(R_AfterSaveEventArgs eventArgs)
    {
        // _detailIsNormal = true;
        await _gridRefService.R_RefreshGrid(null);
    }

    private void SetEditOvt(R_SetEventArgs eventArgs)
    {
        _detailIsNormal = false;
    }

    private async Task BeforeCancelOvt(R_BeforeCancelEventArgs eventArgs)
    {
        var leMsg = await R_MessageBox.Show("", _localizer["MSG_BEFORE_CANCEL"], R_eMessageBoxButtonType.YesNo);
        eventArgs.Cancel = leMsg != R_eMessageBoxResult.Yes;
        _detailIsNormal = leMsg != R_eMessageBoxResult.Yes;

        if (leMsg == R_eMessageBoxResult.Yes)
        {
            await _gridRefService.R_RefreshGrid(null);
            if (_viewModel.OvertimeServiceGridList.Count > 0)
            {
                await _conductorRefService.R_SetCurrentData(_viewModelService.Entity);
            }
        }
    }

    private void CheckAddOvt(R_CheckAddEventArgs eventArgs)
    {
        eventArgs.Allow = !_viewModel.Caller.isCaller;
    }

    private void CheckEditOvt(R_CheckEditEventArgs eventArgs)
    {
        eventArgs.Allow = _viewModel.Data.CTRANS_STATUS == "00";
    }

    private void CheckDeleteOvt(R_CheckDeleteEventArgs eventArgs)
    {
        eventArgs.Allow = _viewModel.Data.CTRANS_STATUS == "00";
    }

    private R_TabStrip _tabMain = new();
    private R_TabStripTab _tabUnit = new();
    private R_TabStripTab _tabService = new();

    private void SetOtherOvt(R_SetEventArgs eventArgs)
    {
        // await Task.Delay(1);
        _tabUnit.Enabled = eventArgs.Enable;
        _tabService.Enabled = eventArgs.Enable;
    }

    #endregion

    private async Task OnClickSubmit()
    {
        var loEx = new R_Exception();

        try
        {
            var leMsg = await R_MessageBox.Show("",
                _viewModel.Data.CTRANS_STATUS switch
                {
                    "00" => _localizer["MSG_BEFORE_SUBMIT"],
                    "10" => _localizer["MSG_BEFORE_REDRAFT"],
                    _ => ""
                }, R_eMessageBoxButtonType.YesNo);

            if (leMsg == R_eMessageBoxResult.No)
            {
                return;
            }

            await _viewModel.ProcessSubmit();

            var leMsgAfter = await R_MessageBox.Show("",
                _viewModel.Data.CTRANS_STATUS switch
                {
                    "00" => _localizer["MSG_AFTER_SUBMIT"],
                    "10" => _localizer["MSG_AFTER_REDRAFT"],
                    _ => ""
                });

            await _conductorRef.R_GetEntity(_viewModel.Entity);
            await _gridRefService.R_RefreshGrid(null);
            await _pageUnit.InvokeRefreshTabPageAsync(_viewModel.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    #region Tab

    private void BeforeOpenUnitTab(R_BeforeOpenTabPageEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = new PMT06000UnitParam();
            loParam.OVT = _viewModel.Entity;
            loParam.SVC = _viewModelService.Entity;
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(PMT06000Unit);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private bool accessInfo = true;
    private int _pageSizeService = 6;

    private void OnChangeTab(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
    {
        accessInfo = eventArgs.TabStripTab.Id == nameof(PMT06000Info);
    }

    #endregion

    #region Lookup Service

    private async Task OnLostFocusUtility()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupLML00400ViewModel();
        try
        {
            if (_viewModelService.Data.CSERVICE_ID == null || _viewModelService.Data.CSERVICE_ID.Trim().Length <= 0)
            {
                _viewModelService.Data.CSERVICE_NAME = "";
                return;
            }

            var param = new LML00400ParameterDTO
            {
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                CCHARGE_TYPE_ID = "08",
                CTAXABLE_TYPE = "1",
                CACTIVE_TYPE = "1",
                CTAX_DATE = _viewModel.Data.DREF_DATE?.ToString("yyyyMMdd"),
                CSEARCH_TEXT = _viewModelService.Data.CSERVICE_ID
            };

            var loResult = await loLookupViewModel.GetUtitlityCharges(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModelService.Data.CSERVICE_ID = "";
                _viewModelService.Data.CSERVICE_NAME = "";
                goto EndBlock;
            }

            _viewModelService.Data.CSERVICE_ID = loResult.CCHARGES_ID;
            _viewModelService.Data.CSERVICE_NAME = loResult.CCHARGES_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        await R_DisplayExceptionAsync(loEx);
    }

    private void BeforeOpenLookupUtility(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParameter = new LML00400ParameterDTO
            {
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                CCHARGE_TYPE_ID = "08",
                CTAXABLE_TYPE = "1",
                CACTIVE_TYPE = "1",
                CTAX_DATE = _viewModel.Data.DREF_DATE?.ToString("yyyyMMdd"),
            };

            eventArgs.Parameter = loParameter;
            eventArgs.TargetPageType = typeof(LML00400);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void AfterOpenLookupUtility(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.Result == null) return;

            var loTempResult = (LML00400DTO)eventArgs.Result;
            _viewModelService.Data.CSERVICE_ID = loTempResult.CCHARGES_ID;
            _viewModelService.Data.CSERVICE_NAME = loTempResult.CCHARGES_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    #region CRUD Service

    private void CheckAddService(R_CheckAddEventArgs eventArgs)
    {
        eventArgs.Allow = _viewModel.Entity.CTRANS_STATUS == "00";
    }

    private void CheckEditService(R_CheckEditEventArgs eventArgs)
    {
        eventArgs.Allow = _viewModel.Entity.CTRANS_STATUS == "00";
    }

    private void CheckDeleteService(R_CheckDeleteEventArgs eventArgs)
    {
        eventArgs.Allow = _viewModel.Entity.CTRANS_STATUS == "00";
    }


    private void DisplayService(R_DisplayEventArgs eventArgs)
    {
        _viewModelService.Entity = (PMT06000OvtServiceDTO)eventArgs.Data;
    }

    private async Task ServiceGetListService(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetOvertimeServiceGridList();
            eventArgs.ListEntityResult = _viewModel.OvertimeServiceGridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task ServiceGetRecordService(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<PMT06000OvtServiceDTO>(eventArgs.Data);
            await _viewModelService.GetEntity(loParam);
            eventArgs.Result = _viewModelService.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void ValidationService(R_ValidationEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loEntity = (PMT06000OvtServiceDTO)eventArgs.Data;
            if (string.IsNullOrEmpty(loEntity.CSERVICE_ID))
            {
                loEx.Add(new Exception(_localizer["PLEASE_SELECT_SERVICE"]));
            }

            loEntity.CDATE_IN = loEntity.DDATE_IN?.ToString("yyyyMMdd");
            if (string.IsNullOrEmpty(loEntity.CDATE_IN))
            {
                loEx.Add(new Exception(_localizer["PLEASE_SELECT_DATE_IN"]));
            }

            loEntity.CDATE_OUT = loEntity.DDATE_OUT?.ToString("yyyyMMdd");
            if (string.IsNullOrEmpty(loEntity.CDATE_OUT))
            {
                loEx.Add(new Exception(_localizer["PLEASE_SELECT_DATE_OUT"]));
            }

            if (loEntity.DDATE_IN > loEntity.DDATE_OUT)
            {
                loEx.Add(new Exception(_localizer["DATE_IN_GREATER_THAN_DATE_OUT"]));
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void SavingService(R_SavingEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loEntity = (PMT06000OvtServiceDTO)eventArgs.Data;
            loEntity.CPROPERTY_ID = _viewModel.Entity.CPROPERTY_ID;
            loEntity.CPARENT_ID = _viewModel.Entity.CREC_ID;
            loEntity.CREC_ID = eventArgs.ConductorMode == R_eConductorMode.Add ? "" : loEntity.CREC_ID;
            loEntity.CREF_NO = _viewModel.Entity.CREF_NO;
            loEntity.CDEPT_CODE = _viewModel.Entity.CDEPT_CODE;
            loEntity.CTRANS_CODE = _viewModel.TRANS_CODE;
            loEntity.CSERVICE_ID = loEntity.CSERVICE_ID;
            loEntity.CDATE_IN = loEntity.DDATE_IN?.ToString("yyyyMMdd");
            loEntity.CTIME_IN = loEntity.DDATE_IN?.ToString("HH:mm");
            loEntity.CDATE_OUT = loEntity.DDATE_OUT?.ToString("yyyyMMdd");
            loEntity.CTIME_OUT = loEntity.DDATE_OUT?.ToString("HH:mm");
            loEntity.CDESCRIPTION = loEntity.CSERVICE_DESCR;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task ServiceSaveService(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<PMT06000OvtServiceDTO>(eventArgs.Data);
            await _viewModelService.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);

            eventArgs.Result = _viewModelService.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task BeforeCancelService(R_BeforeCancelEventArgs eventArgs)
    {
        var leMsg = await R_MessageBox.Show("", _localizer["MSG_BEFORE_CANCEL"], R_eMessageBoxButtonType.YesNo);
        eventArgs.Cancel = leMsg != R_eMessageBoxResult.Yes;
        _detailIsNormal = leMsg != R_eMessageBoxResult.Yes;
    }

    private async Task BeforeDeleteService(R_BeforeDeleteEventArgs eventArgs)
    {
        var leMsg = await R_MessageBox.Show("", _localizer["MSG_BEFORE_DELETE_SVC"], R_eMessageBoxButtonType.YesNo);
        eventArgs.Cancel = leMsg != R_eMessageBoxResult.Yes;
    }

    private async Task ServiceDeleteService(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<PMT06000OvtServiceDTO>(eventArgs.Data);
            await _viewModelService.DeleteEntity(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task AfterDeleteService()
    {
        var loEx = new R_Exception();

        try
        {
            var leMsg = await R_MessageBox.Show("", _localizer["MSG_AFTER_DELETE_SVC"]);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    private void AfterAddService(R_AfterAddEventArgs eventArgs)
    {
        var loEntity = (PMT06000OvtServiceDTO)eventArgs.Data;
        //ddate_in = DateTime.Now;
        loEntity.DDATE_IN = DateTime.Now;
        loEntity.DDATE_OUT = DateTime.Now;
    }

    private async Task R_TabEventCallbackAsync(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<PMT06000OvtDTO>(_viewModel.Entity);
            await _conductorRef.R_GetEntity(loParam);
            if (_tabMain.ActiveTab.Id == nameof(PMT06000Unit))
            {
                await _pageUnit.InvokeRefreshTabPageAsync(_viewModel.Entity);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void OnChangeProperty(string? value)
    {
        if (_viewModel.Data.CPROPERTY_ID == value) return;
        _viewModel.Data.CPROPERTY_ID = value;
        _viewModel.Data.CDEPT_CODE = "";
        _viewModel.Data.CDEPT_NAME = "";

        _viewModel.Data.CTENANT_ID = "";
        _viewModel.Data.CTENANT_NAME = "";

        _viewModel.Data.CBUILDING_ID = "";
        _viewModel.Data.CBUILDING_NAME = "";

        _viewModel.Data.CAGREEMENT_NO = "";
        _viewModel.Data.CUNIT_DESCRIPTION = "";
    }
}