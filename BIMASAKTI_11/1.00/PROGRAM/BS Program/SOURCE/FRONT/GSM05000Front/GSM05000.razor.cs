using GSM05000Common;
using GSM05000Common.DTOs;
using GSM05000Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM05000Front;

public partial class GSM05000 : R_Page
{
    private GSM05000ViewModel _GSM05000ViewModel = new();
    private R_Conductor _conductorRef;
    private R_Grid<GSM05000GridDTO> _gridRef;
    public bool NumberingDock { get; set; }
    public bool ApprovalDock { get; set; }

    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            await _GSM05000ViewModel.GetDelimiterList();

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
            await _GSM05000ViewModel.GetGridList();
            eventArgs.ListEntityResult = _GSM05000ViewModel.GridList;
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
            await _GSM05000ViewModel.GetEntity(loParam);
            EnTab();

            eventArgs.Result = _GSM05000ViewModel.Entity;
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
            var llCondition2 = loData.LINCREMENT_FLAG != _GSM05000ViewModel.TempEntity.LINCREMENT_FLAG;
            var llCondition3 = loData.LDEPT_MODE != _GSM05000ViewModel.TempEntity.LDEPT_MODE;
            var llCondition4 = loData.LTRANSACTION_MODE != _GSM05000ViewModel.TempEntity.LTRANSACTION_MODE;
            var llCondition5 = loData.CPERIOD_MODE != _GSM05000ViewModel.TempEntity.CPERIOD_MODE;
            var llCondition6 = loData.LAPPROVAL_FLAG != _GSM05000ViewModel.TempEntity.LAPPROVAL_FLAG;
            var llCondition7 = loData.LAPPROVAL_DEPT != _GSM05000ViewModel.TempEntity.LAPPROVAL_DEPT;
            var llCondition8 = loData.LUSE_THIRD_PARTY != _GSM05000ViewModel.TempEntity.LUSE_THIRD_PARTY;

            var llIsExistNumbering = await _GSM05000ViewModel.CheckExistData(loData, GSM05000eTabName.Numbering);
            
            if (llIsExistNumbering)
            {
                if (llCondition1)
                {
                    await R_MessageBox.Show("Error", _localizer["Err01"], R_eMessageBoxButtonType.OK);
                    eventArgs.Cancel = true;
                    return;
                }

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

            var llIsExistApproval = await _GSM05000ViewModel.CheckExistData(loData, GSM05000eTabName.Approval);

            if (llIsExistApproval)
            {
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


            if (_GSM05000ViewModel.DeptSequence == 0 && loData.LDEPT_MODE)
            {
                await R_MessageBox.Show("Error", _localizer["Err02"], R_eMessageBoxButtonType.OK);
                eventArgs.Cancel = true;
                return;
            }

            if (_GSM05000ViewModel.TrxSequence == 0 && loData.LTRANSACTION_MODE)
            {
                await R_MessageBox.Show("Error", _localizer["Err03"],
                    R_eMessageBoxButtonType.OK);
                eventArgs.Cancel = true;
                return;
            }

            if (_GSM05000ViewModel.PeriodSequence == 0 && loData.LINCREMENT_FLAG)
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

            var llIsDuplicate = _GSM05000ViewModel.IsDuplicateSequence(loData);
            if (llIsDuplicate)
            {
                await R_MessageBox.Show("Error", _localizer["Err06"], R_eMessageBoxButtonType.OK);
                eventArgs.Cancel = true;
                return;
            }

            var llIsValid = _GSM05000ViewModel.IsValidSequence(loData, out var loCount);
            if (!llIsValid)
            {
                await R_MessageBox.Show("Error", _localizer["Err07"] +
                                                 $" {loCount}",
                    R_eMessageBoxButtonType.OK);
                eventArgs.Cancel = true;
                return;
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
            await _GSM05000ViewModel.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);
            eventArgs.Result = _GSM05000ViewModel.Entity;
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
            R_FrontUtility.ConvertObjectToObject<GSM05000NumberingHeaderDTO>(_GSM05000ViewModel.Entity);
        return Task.CompletedTask;
    }

    private Task InstanceApprovalTab(R_InstantiateDockEventArgs eventArgs)
    {
        // EnTab();
        eventArgs.TargetPageType = typeof(GSM05000Approval);
        eventArgs.Parameter =
            R_FrontUtility.ConvertObjectToObject<GSM05000ApprovalHeaderDTO>(_GSM05000ViewModel.Entity);
        return Task.CompletedTask;
    }

    private Task CheckIncrementFlag(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if ((bool)eventArgs == false)
            {
                _GSM05000ViewModel.Data.LDEPT_MODE = false;
                _GSM05000ViewModel.Data.LTRANSACTION_MODE = false;
                _GSM05000ViewModel.Data.CPERIOD_MODE = "";
                _GSM05000ViewModel.Data.INUMBER_LENGTH = 0;
                _GSM05000ViewModel.Data.CPREFIX = "";
                _GSM05000ViewModel.Data.CSUFFIX = "";
                _GSM05000ViewModel.Data.CYEAR_FORMAT = "";

                _GSM05000ViewModel.Data.CDEPT_DELIMITER = "";
                _GSM05000ViewModel.Data.CTRANSACTION_DELIMITER = "";
                _GSM05000ViewModel.Data.CPERIOD_DELIMITER = "";
                _GSM05000ViewModel.Data.CNUMBER_DELIMITER = "";
                _GSM05000ViewModel.Data.CPREFIX_DELIMITER = "";

                _GSM05000ViewModel.DeptSequence = 0;
                _GSM05000ViewModel.TrxSequence = 0;
                _GSM05000ViewModel.PeriodSequence = 0;
                _GSM05000ViewModel.LenSequence = 0;
                _GSM05000ViewModel.PrefixSequence = 0;
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
                _GSM05000ViewModel.Data.LAPPROVAL_FLAG = false;
                _GSM05000ViewModel.Data.CAPPROVAL_MODE = "";
                _GSM05000ViewModel.Data.LAPPROVAL_DEPT = false;
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
            _GSM05000ViewModel.AutoSequence(eNumericList.DeptMode, (bool)eventArgs);
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
            _GSM05000ViewModel.AutoSequence(eNumericList.TrxMode, (bool)eventArgs);
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
        if ((_GSM05000ViewModel.TempEntity.CPERIOD_MODE == "N" && llCondition) ||
            (_GSM05000ViewModel.TempEntity.CPERIOD_MODE != "N" && !llCondition))
        {
            _GSM05000ViewModel.AutoSequence(eNumericList.PeriodMode, llCondition);
        }
        _GSM05000ViewModel.TempEntity.CPERIOD_MODE = (string)eventArgs;
        GetUpdateSample();
    }

    private void CheckLenMode(object eventArgs)
    {
        var llCondition = (int)eventArgs != 0;
        if ((_GSM05000ViewModel.TempEntity.INUMBER_LENGTH == 0 && llCondition)
            || (_GSM05000ViewModel.TempEntity.INUMBER_LENGTH != 0 && !llCondition))
        {
            _GSM05000ViewModel.AutoSequence(eNumericList.LenMode, llCondition);
        }
        _GSM05000ViewModel.TempEntity.INUMBER_LENGTH = (int)eventArgs;
        GetUpdateSample();
    }

    private void GetUpdateSample()
    {
        var loEx = new R_Exception();

        try
        {
            _GSM05000ViewModel.getRefNo();
            
            if (_GSM05000ViewModel.REFNO.Length > 30)
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

    private bool _gridEnabled;

    private void SetOther(R_SetEventArgs eventArgs)
    {
        _gridEnabled = eventArgs.Enable;
    }

    private void BeforeCancel(R_BeforeCancelEventArgs eventArgs)
    {
        _GSM05000ViewModel.GetSequence();
    }

    private void EnTab()
    {
        NumberingDock = _GSM05000ViewModel.Entity.LDEPT_MODE;
        ApprovalDock = _GSM05000ViewModel.Entity.LAPPROVAL_FLAG;
    }
}