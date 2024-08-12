using BlazorClientHelper;
using GSM05000Common;
using GSM05000Common.DTOs;
using GSM05000Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;

namespace GSM05000Front;

public partial class GSM05000 : R_Page
{
    private GSM05000ViewModel _viewModel = new();
    private R_Conductor _conductorRef;
    private R_Grid<GSM05000GridDTO> _gridRef = new();
    public bool NumberingDock { get; set; }
    public bool ApprovalDock { get; set; }
    [Inject] public IClientHelper _clientHelper { get; set; }

    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetDelimiterList();

            var loGroupDescriptor = new List<R_GridGroupDescriptor>
            {
                new() { FieldName = "MODULE" }
            };
            await _gridRef.R_GroupBy(loGroupDescriptor);
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #region Tab Transaction

    private void Grid_Display(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                GetUpdateSample();
                _radioGroup = false;
            }
            else
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000DTO>(eventArgs.Data);
                if (loParam.CPERIOD_MODE != "N")
                {
                    _radioGroup = true;
                }
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Grid_GetList(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetGridList();
            eventArgs.ListEntityResult = _viewModel.GridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Conductor_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000DTO>(eventArgs.Data);
            await _viewModel.GetEntity(loParam);
            EnTab();

            eventArgs.Result = _viewModel.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Validation(R_ValidationEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loData = (GSM05000DTO)eventArgs.Data;
            loData.CAPPROVAL_MODE ??= "";

            var llCondition1 = loData is { LAPPROVAL_FLAG: true, LUSE_THIRD_PARTY: false, CAPPROVAL_MODE: "" };
            var llCondition2 = loData.LINCREMENT_FLAG != _viewModel.TempEntity.LINCREMENT_FLAG;
            var llCondition3 = loData.LDEPT_MODE != _viewModel.TempEntity.LDEPT_MODE;
            var llCondition4 = loData.LTRANSACTION_MODE != _viewModel.TempEntity.LTRANSACTION_MODE;
            var llCondition5 = loData.CPERIOD_MODE != _viewModel.TempEntity.CPERIOD_MODE;
            var llCondition6 = loData.LAPPROVAL_FLAG != _viewModel.TempEntity.LAPPROVAL_FLAG;
            var llCondition7 = loData.LAPPROVAL_DEPT != _viewModel.TempEntity.LAPPROVAL_DEPT;
            var llCondition8 = loData.LUSE_THIRD_PARTY != _viewModel.TempEntity.LUSE_THIRD_PARTY;

            var llIsExistNumbering = await _viewModel.CheckExistData(loData, GSM05000eTabName.Numbering);

            if (llIsExistNumbering)
            {
                if (llCondition2 || llCondition3 || llCondition4 || llCondition5)
                {
                    var llReturn = await R_MessageBox.Show(_localizer["ConfirmLabel"],
                        _localizer["Confirm01"],
                        R_eMessageBoxButtonType.OKCancel);

                    if (llReturn == R_eMessageBoxResult.Cancel)
                    {
                        eventArgs.Cancel = true;
                        return;
                    }
                }
            }

            var llIsExistApproval = await _viewModel.CheckExistData(loData, GSM05000eTabName.Approval);

            if (llIsExistApproval)
            {
                if (llCondition1)
                {
                    await R_MessageBox.Show("Error", _localizer["Err01"], R_eMessageBoxButtonType.OK);
                    eventArgs.Cancel = true;
                    return;
                }

                if (llCondition6 || llCondition7 || llCondition8)
                {
                    var llReturn = await R_MessageBox.Show(_localizer["ConfirmLabel"],
                        _localizer["Confirm02"],
                        R_eMessageBoxButtonType.OKCancel);

                    if (llReturn == R_eMessageBoxResult.Cancel)
                    {
                        eventArgs.Cancel = true;
                        return;
                    }
                }
            }

            if (_viewModel.Data.CTRANS_SHORT_NAME == String.Empty)
            {
                await R_MessageBox.Show("Error", _localizer["Err09"], R_eMessageBoxButtonType.OK);
                eventArgs.Cancel = true;
                return;
            }

            if (_viewModel.Data.LINCREMENT_FLAG)
            {
                if (_viewModel.DeptSequence == 0 && loData.LDEPT_MODE)
                {
                    await R_MessageBox.Show("Error", _localizer["Err02"], R_eMessageBoxButtonType.OK);
                    eventArgs.Cancel = true;
                    return;
                }

                if (_viewModel.TrxSequence == 0 && loData.LTRANSACTION_MODE)
                {
                    await R_MessageBox.Show("Error", _localizer["Err03"],
                        R_eMessageBoxButtonType.OK);
                    eventArgs.Cancel = true;
                    return;
                }

                if (_viewModel.PeriodSequence == 0 && loData.LINCREMENT_FLAG)
                {
                    await R_MessageBox.Show("Error", _localizer["Err04"], R_eMessageBoxButtonType.OK);
                    eventArgs.Cancel = true;
                    return;
                }

                if (loData.INUMBER_LENGTH == 0 && loData.LINCREMENT_FLAG)
                {
                    await R_MessageBox.Show("Error", _localizer["Err05"],
                        R_eMessageBoxButtonType.OK);
                    eventArgs.Cancel = true;
                    return;
                }

                var llIsDuplicate = _viewModel.IsDuplicateSequence(loData);
                if (llIsDuplicate)
                {
                    await R_MessageBox.Show("Error", _localizer["Err06"], R_eMessageBoxButtonType.OK);
                    eventArgs.Cancel = true;
                    return;
                }

                var llIsValid = _viewModel.IsValidSequence(loData, out var loCount);
                if (!llIsValid)
                {
                    await R_MessageBox.Show("Error", _localizer["Err07"] + $" {loCount}",
                        R_eMessageBoxButtonType.OK);
                    eventArgs.Cancel = true;
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void Saving(R_SavingEventArgs eventArgs)
    {
        var loData = (GSM05000DTO)eventArgs.Data;
        loData.CDEPT_DELIMITER ??= "";
        loData.CTRANSACTION_DELIMITER ??= "";
        loData.CPERIOD_DELIMITER ??= "";
        loData.CNUMBER_DELIMITER ??= "";
        loData.CPREFIX_DELIMITER ??= "";
    }

    private async Task Conductor_ServiceSave(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (GSM05000DTO)eventArgs.Data;
            loParam.DUPDATE_DATE = DateTime.Now;
            await _viewModel.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);
            eventArgs.Result = _viewModel.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    private Task InstanceNumberingTab(R_InstantiateDockEventArgs eventArgs)
    {
        // EnTab();
        eventArgs.TargetPageType = typeof(GSM05000Numbering);
        eventArgs.Parameter =
            R_FrontUtility.ConvertObjectToObject<GSM05000NumberingHeaderDTO>(_viewModel.Entity);
        return Task.CompletedTask;
    }

    private Task InstanceApprovalTab(R_InstantiateDockEventArgs eventArgs)
    {
        // EnTab();
        eventArgs.TargetPageType = typeof(GSM05000Approval);
        eventArgs.Parameter =
            R_FrontUtility.ConvertObjectToObject<GSM05000ApprovalHeaderDTO>(_viewModel.Entity);
        return Task.CompletedTask;
    }

    private Task CheckIncrementFlag(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if ((bool)eventArgs == false)
            {
                _viewModel.Data.LDEPT_MODE = false;
                _viewModel.Data.LTRANSACTION_MODE = false;
                _viewModel.Data.CPERIOD_MODE = "N";
                _viewModel.Data.INUMBER_LENGTH = 0;
                _viewModel.Data.CPREFIX = "";
                _viewModel.Data.CSUFFIX = "";
                _viewModel.Data.CYEAR_FORMAT = "";

                _viewModel.Data.CDEPT_DELIMITER = "";
                _viewModel.Data.CTRANSACTION_DELIMITER = "";
                _viewModel.Data.CPERIOD_DELIMITER = "";
                _viewModel.Data.CNUMBER_DELIMITER = "";
                _viewModel.Data.CPREFIX_DELIMITER = "";

                _viewModel.DeptSequence = 0;
                _viewModel.TrxSequence = 0;
                _viewModel.PeriodSequence = 0;
                _viewModel.LenSequence = 0;
                _viewModel.PrefixSequence = 0;
            }

            GetUpdateSample();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return Task.CompletedTask;
    }

    private Task CheckApprovalFlag(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if ((bool)eventArgs == false)
            {
                _viewModel.Data.LAPPROVAL_FLAG = false;
                _viewModel.Data.CAPPROVAL_MODE = "";
                _viewModel.Data.LAPPROVAL_DEPT = false;
            }
            else
            {
                _viewModel.Data.CAPPROVAL_MODE = "1";
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return Task.CompletedTask;
    }

    private Task CheckDeptMode(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.AutoSequence(eNumericList.DeptMode, (bool)eventArgs);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return Task.CompletedTask;
    }

    private Task CheckTrxMode(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.AutoSequence(eNumericList.TrxMode, (bool)eventArgs);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return Task.CompletedTask;
    }

    private void CheckPeriodMode(object eventArgs)
    {
        var llCondition = (string)eventArgs != "N";
        _radioGroup = llCondition;
        if ((_viewModel.TempEntity.CPERIOD_MODE == "N" && llCondition) ||
            (_viewModel.TempEntity.CPERIOD_MODE != "N" && !llCondition))
        {
            _viewModel.AutoSequence(eNumericList.PeriodMode, llCondition);
        }

        _viewModel.TempEntity.CPERIOD_MODE = (string)eventArgs;
        GetUpdateSample();
    }

    private void CheckLenMode(object eventArgs)
    {
        var llCondition = (int)eventArgs != 0;
        if ((_viewModel.TempEntity.INUMBER_LENGTH == 0 && llCondition)
            || (_viewModel.TempEntity.INUMBER_LENGTH != 0 && !llCondition))
        {
            _viewModel.AutoSequence(eNumericList.LenMode, llCondition);
        }

        _viewModel.TempEntity.INUMBER_LENGTH = (int)eventArgs;
        GetUpdateSample();
    }

    private void GetUpdateSample()
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.getRefNo();

            if (_viewModel.REFNO.Length > 30)
            {
                loEx.Add("Err08", _localizer["Err08"]);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private bool _radioGroup = false;


    private void ConvertGridToEntity(R_ConvertToGridEntityEventArgs eventArgs)
    {
        var loData = (GSM05000DTO)eventArgs.Data;
        eventArgs.GridData = new GSM05000GridDTO()
            { CTRANS_CODE = loData.CTRANS_CODE, CTRANSACTION_NAME = loData.CTRANSACTION_NAME };
    }

    // private bool _gridEnabled;
    private bool _isNormalMode = true;
    
    private void SetOther(R_SetEventArgs eventArgs)
    {
        _isNormalMode = eventArgs.Enable;
        // _gridEnabled = eventArgs.Enable;
    }

    private void BeforeCancel(R_BeforeCancelEventArgs eventArgs)
    {
        _viewModel.GetSequence();
    }

    private void EnTab()
    {
        NumberingDock = _viewModel.Entity.LDEPT_MODE;
        ApprovalDock = _viewModel.Entity.LAPPROVAL_FLAG;
    }


    #region Locking

    private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
    private const string DEFAULT_MODULE_NAME = "GS";

    protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        var llRtn = false;
        R_LockingFrontResult loLockResult;

        try
        {
            var loData = (GSM05000DTO)eventArgs.Data;

            var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                plSendWithContext: true,
                plSendWithToken: true,
                pcHttpClientName: DEFAULT_HTTP_NAME);

            var Company_Id = _clientHelper.CompanyId;
            var User_Id = _clientHelper.UserId;
            var Program_Id = "GSM05000";
            var Table_Name = "GSM_TRANSACTION_CODE";
            var Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CTRANS_CODE);

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
}