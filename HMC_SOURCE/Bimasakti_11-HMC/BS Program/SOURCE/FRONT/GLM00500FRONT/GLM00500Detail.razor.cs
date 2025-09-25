﻿using GLM00500Common.DTOs;
using GLM00500Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GLM00500Front;

public partial class GLM00500Detail
{
    private GLM00500DetailViewModel _viewModel = new();
    private R_Conductor _conductorRef;
    private R_Grid<GLM00500BudgetDTGridDTO> _gridRef = new();

    private R_TextBox _fieldAcc { get; set; }
    private R_Lookup _lookupCenterCode { get; set; }
    private R_TextBox _fieldCenterCode { get; set; }
    private R_NumericTextBox<decimal> _fieldBudget { get; set; }
    private R_ComboBox<GLM00500FunctionDTO, string> _fieldRoundingMethod { get; set; }
    private R_RadioGroup<KeyValuePair<string, string>, string> _fieldDistributionMethod { get; set; }

    private R_ComboBox<GLM00500BudgetWeightingDTO, string> _fieldWeightingCode { get; set; }

    // private R_Button BtnRefresh { get; set; }
    private R_Button _btnCalculate { get; set; }
    private R_Popup _btnGenerate { get; set; }

    private R_NumericTextBox<decimal> _fieldPeriod1 { get; set; }
    private R_NumericTextBox<decimal> _fieldPeriod2 { get; set; }
    private R_NumericTextBox<decimal> _fieldPeriod3 { get; set; }
    private R_NumericTextBox<decimal> _fieldPeriod4 { get; set; }
    private R_NumericTextBox<decimal> _fieldPeriod5 { get; set; }
    private R_NumericTextBox<decimal> _fieldPeriod6 { get; set; }
    private R_NumericTextBox<decimal> _fieldPeriod7 { get; set; }
    private R_NumericTextBox<decimal> _fieldPeriod8 { get; set; }
    private R_NumericTextBox<decimal> _fieldPeriod9 { get; set; }
    private R_NumericTextBox<decimal> _fieldPeriod10 { get; set; }
    private R_NumericTextBox<decimal> FieldPeriod11 { get; set; }
    private R_NumericTextBox<decimal> FieldPeriod12 { get; set; }
    private R_NumericTextBox<decimal> FieldPeriod13 { get; set; }
    private R_NumericTextBox<decimal> FieldPeriod14 { get; set; }
    private R_NumericTextBox<decimal> FieldPeriod15 { get; set; }

    protected override async Task R_Init_From_Master(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.Init(eventArgs);
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private bool _gridEnabled;
    private void SetOther(R_SetEventArgs eventArgs)
    {
        _gridEnabled = eventArgs.Enable;
    }
    
    //GetList
    private async Task GetBudgetDTList(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var lcBudgetId = _viewModel.BudgetHDEntity.CREC_ID;
            var lcAccountType = _viewModel.SelectedAccountType;
            await _viewModel.GetBudgetDTList(lcBudgetId, lcAccountType);
            eventArgs.ListEntityResult = _viewModel.BudgetDTList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    //Get
    private async Task GetBudgetDT(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var lcParam = R_FrontUtility.ConvertObjectToObject<GLM00500BudgetDTDTO>(eventArgs.Data);
            await _viewModel.GetBudgetDT(lcParam);
            eventArgs.Result = _viewModel.BudgetDTEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task RefreshFormProcess()
    {
        await _gridRef.R_RefreshGrid(null);
    }

    private void BeforeOpenLookupAccount(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loParameter = new GSL00510ParameterDTO()
        {
            CGLACCOUNT_TYPE = _viewModel.SelectedAccountType
        };

        eventArgs.Parameter = loParameter;
        eventArgs.TargetPageType = typeof(GSL00510);
    }

    private void AfterOpenLookupAccount(R_AfterOpenLookupEventArgs eventArgs)
    {
        if (eventArgs.Result == null) return;

        var loTempResult = (GSL00510DTO)eventArgs.Result;

        var loGetData = (GLM00500BudgetDTDTO)_conductorRef.R_GetCurrentData();
        loGetData.CGLACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
        loGetData.CGLACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;
        loGetData.CBSIS = R_FrontUtility.ConvertObjectToObject<string>(loTempResult.CBSIS);
        if ((loTempResult.CBSIS == "I" && _viewModel.Company.LENABLE_CENTER_IS == false) ||
            (loTempResult.CBSIS == "B" && _viewModel.Company.LENABLE_CENTER_BS == false))
        {
            loGetData.CCENTER_CODE = "";
            loGetData.CCENTER_NAME = "";

            EnableCenter(loGetData.CBSIS);
        }
    }

    private void BeforeOpenLookupCenter(R_BeforeOpenLookupEventArgs eventArgs)
    {
        eventArgs.Parameter = new GSL00900ParameterDTO();
        eventArgs.TargetPageType = typeof(GSL00900);
    }

    private void AfterOpenLookupCenter(R_AfterOpenLookupEventArgs eventArgs)
    {
        if (eventArgs.Result == null) return;
        var loTempResult = (GSL00900DTO)eventArgs.Result;

        var loGetData = (GLM00500BudgetDTDTO)_conductorRef.R_GetCurrentData();
        loGetData.CCENTER_CODE = loTempResult.CCENTER_CODE;
        loGetData.CCENTER_NAME = loTempResult.CCENTER_NAME;
    }

    private async Task Display(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _btnGenerate.Enabled = _viewModel.BudgetHDEntity.LFINAL == false;
            _btnCalculate.Enabled = false;
            if (eventArgs.ConductorMode != R_eConductorMode.Normal)
            {
                _btnCalculate.Enabled = _viewModel.BudgetHDEntity.LFINAL == false;
                _btnGenerate.Enabled = false;
                EnablePeriod();
            }

            switch (eventArgs.ConductorMode)
            {
                case R_eConductorMode.Edit:
                    EnableCenter(_viewModel.BudgetDTEntity.CBSIS);
                    ChangeInputMethod(_viewModel.BudgetDTEntity.CINPUT_METHOD);
                    await _fieldCenterCode.FocusAsync();
                    break;
                case R_eConductorMode.Add:
                    ChangeInputMethod((string)"MN");
                    await _fieldAcc.FocusAsync();
                    break;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void EnableCenter(string pcCBSIS)
    {
        _lookupCenterCode.Enabled = false;
        _fieldCenterCode.Enabled = false;
        if ((pcCBSIS == "B" && _viewModel.Company.LENABLE_CENTER_BS) ||
            (pcCBSIS == "I" && _viewModel.Company.LENABLE_CENTER_IS))
        {
            _lookupCenterCode.Enabled = true;
            _fieldCenterCode.Enabled = true;
        }
    }

    private void ChangeInputMethod(object eventArgs)
    {
        var lcValue = eventArgs.ToString();
        if (lcValue != "MN")
        {
            _viewModel.Data.CROUNDING_METHOD = "00";
            _viewModel.Data.CDIST_METHOD = "EV";

            _fieldBudget.Enabled = true;
            _fieldRoundingMethod.Enabled = true;
            _fieldDistributionMethod.Enabled = true;
        }
        else
        {
            _viewModel.Data.NBUDGET = 0;
            _viewModel.Data.CROUNDING_METHOD = "";
            _viewModel.Data.CDIST_METHOD = "";
            _viewModel.Data.CBW_CODE = "";

            _fieldBudget.Enabled = false;
            _fieldRoundingMethod.Enabled = false;
            _fieldDistributionMethod.Enabled = false;
            _fieldWeightingCode.Enabled = false;
        }

        ChangeDistMethod();
        EnablePeriod();
    }

    private void AfterAdd(R_AfterAddEventArgs eventArgs)
    {
        var loData = (GLM00500BudgetDTDTO)eventArgs.Data;
        loData.DCREATE_DATE = DateTime.Now;
        loData.DUPDATE_DATE = DateTime.Now;
        loData.CBUDGET_ID = _viewModel.BudgetHDEntity.CREC_ID;
        loData.CBUDGET_NO = _viewModel.BudgetHDEntity.CBUDGET_NO;
        loData.CGLACCOUNT_TYPE = _viewModel.SelectedAccountType;
        loData.CINPUT_METHOD = "MN";
        loData.CROUNDING_METHOD = "00";
        loData.CDIST_METHOD = "EV";
    }

    private void ChangeDistMethod()
    {
        _fieldWeightingCode.Enabled = false;
        if (_viewModel.Data.CDIST_METHOD == "BW" && _viewModel.Data.CINPUT_METHOD != "MN")
        {
            _fieldWeightingCode.Enabled = true;
        }
    }

    private void EnablePeriod()
    {
        _fieldPeriod1.Value = 0;
        _fieldPeriod1.Enabled = false;
        _fieldPeriod2.Value = 0;
        _fieldPeriod2.Enabled = false;
        _fieldPeriod3.Value = 0;
        _fieldPeriod3.Enabled = false;
        _fieldPeriod4.Value = 0;
        _fieldPeriod4.Enabled = false;
        _fieldPeriod5.Value = 0;
        _fieldPeriod5.Enabled = false;
        _fieldPeriod6.Value = 0;
        _fieldPeriod6.Enabled = false;
        _fieldPeriod7.Value = 0;
        _fieldPeriod7.Enabled = false;
        _fieldPeriod8.Value = 0;
        _fieldPeriod8.Enabled = false;
        _fieldPeriod9.Value = 0;
        _fieldPeriod9.Enabled = false;
        _fieldPeriod10.Value = 0;
        _fieldPeriod10.Enabled = false;
        FieldPeriod11.Value = 0;
        FieldPeriod11.Enabled = false;
        FieldPeriod12.Value = 0;
        FieldPeriod12.Enabled = false;
        FieldPeriod13.Value = 0;
        FieldPeriod13.Enabled = false;
        FieldPeriod14.Value = 0;
        FieldPeriod14.Enabled = false;
        FieldPeriod15.Value = 0;
        FieldPeriod15.Enabled = false;

        if (_viewModel.Data.CINPUT_METHOD == "MN")
        {
            if (_viewModel.PeriodCount.INO_PERIOD >= 1) _fieldPeriod1.Enabled = true;
            if (_viewModel.PeriodCount.INO_PERIOD >= 2) _fieldPeriod2.Enabled = true;
            if (_viewModel.PeriodCount.INO_PERIOD >= 3) _fieldPeriod3.Enabled = true;
            if (_viewModel.PeriodCount.INO_PERIOD >= 4) _fieldPeriod4.Enabled = true;
            if (_viewModel.PeriodCount.INO_PERIOD >= 5) _fieldPeriod5.Enabled = true;
            if (_viewModel.PeriodCount.INO_PERIOD >= 6) _fieldPeriod6.Enabled = true;
            if (_viewModel.PeriodCount.INO_PERIOD >= 7) _fieldPeriod7.Enabled = true;
            if (_viewModel.PeriodCount.INO_PERIOD >= 8) _fieldPeriod8.Enabled = true;
            if (_viewModel.PeriodCount.INO_PERIOD >= 9) _fieldPeriod9.Enabled = true;
            if (_viewModel.PeriodCount.INO_PERIOD >= 10) _fieldPeriod10.Enabled = true;
            if (_viewModel.PeriodCount.INO_PERIOD >= 11) FieldPeriod11.Enabled = true;
            if (_viewModel.PeriodCount.INO_PERIOD >= 12) FieldPeriod12.Enabled = true;
            if (_viewModel.PeriodCount.INO_PERIOD >= 13) FieldPeriod13.Enabled = true;
            if (_viewModel.PeriodCount.INO_PERIOD >= 14) FieldPeriod14.Enabled = true;
            if (_viewModel.PeriodCount.INO_PERIOD >= 15) FieldPeriod15.Enabled = true;
        }
    }

    private async Task Delete(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loEntity = (GLM00500BudgetDTDTO)eventArgs.Data;
            await _viewModel.DeleteBudgetDT(loEntity);
            await Task.Delay(500);
            await R_MessageBox.Show(_localizer["SuccessLabel"], _localizer["SuccessDeleteAcc"], R_eMessageBoxButtonType.OK);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void Validation(R_ValidationEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loEntity = (GLM00500BudgetDTDTO)eventArgs.Data;
            if (string.IsNullOrEmpty(loEntity.CGLACCOUNT_NO))
            {
                loEx.Add(new Exception(_localizer["LABEL_PERIOD0"]));
            }

            if (string.IsNullOrEmpty(loEntity.CCENTER_CODE) &&
                ((loEntity.CBSIS == "B" && _viewModel.Company.LENABLE_CENTER_BS) ||
                 (loEntity.CBSIS == "I" && _viewModel.Company.LENABLE_CENTER_IS)))
            {
                loEx.Add(new Exception($"{_localizer["Exception05"]} {loEntity.CGLACCOUNT_NO}!"));
            }

            if (loEntity.CINPUT_METHOD != "MN" && loEntity.NBUDGET <= 0)
            {
                loEx.Add(new Exception(_localizer["Exception06"]));
            }

            if (loEntity.CDIST_METHOD == "BW" && loEntity.CBW_CODE == "")
            {
                loEx.Add(new Exception(_localizer["Exception07"]));
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Save(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loEntity = (GLM00500BudgetDTDTO)eventArgs.Data;
            await _viewModel.SaveBudgetDT(loEntity, (eCRUDMode)eventArgs.ConductorMode);
            eventArgs.Result = _viewModel.BudgetDTEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Calcuate()
    {
        switch (_viewModel.Data.CINPUT_METHOD)
        {
            case "MO":
            {
                if (_viewModel.PeriodCount.INO_PERIOD >= 1) _viewModel.Data.NPERIOD1 = _viewModel.Data.NBUDGET;
                if (_viewModel.PeriodCount.INO_PERIOD >= 2) _viewModel.Data.NPERIOD2 = _viewModel.Data.NBUDGET;
                if (_viewModel.PeriodCount.INO_PERIOD >= 3) _viewModel.Data.NPERIOD3 = _viewModel.Data.NBUDGET;
                if (_viewModel.PeriodCount.INO_PERIOD >= 4) _viewModel.Data.NPERIOD4 = _viewModel.Data.NBUDGET;
                if (_viewModel.PeriodCount.INO_PERIOD >= 5) _viewModel.Data.NPERIOD5 = _viewModel.Data.NBUDGET;
                if (_viewModel.PeriodCount.INO_PERIOD >= 6) _viewModel.Data.NPERIOD6 = _viewModel.Data.NBUDGET;
                if (_viewModel.PeriodCount.INO_PERIOD >= 7) _viewModel.Data.NPERIOD7 = _viewModel.Data.NBUDGET;
                if (_viewModel.PeriodCount.INO_PERIOD >= 8) _viewModel.Data.NPERIOD8 = _viewModel.Data.NBUDGET;
                if (_viewModel.PeriodCount.INO_PERIOD >= 9) _viewModel.Data.NPERIOD9 = _viewModel.Data.NBUDGET;
                if (_viewModel.PeriodCount.INO_PERIOD >= 10) _viewModel.Data.NPERIOD10 = _viewModel.Data.NBUDGET;
                if (_viewModel.PeriodCount.INO_PERIOD >= 11) _viewModel.Data.NPERIOD11 = _viewModel.Data.NBUDGET;
                if (_viewModel.PeriodCount.INO_PERIOD >= 12) _viewModel.Data.NPERIOD12 = _viewModel.Data.NBUDGET;
                if (_viewModel.PeriodCount.INO_PERIOD >= 13) _viewModel.Data.NPERIOD13 = _viewModel.Data.NBUDGET;
                if (_viewModel.PeriodCount.INO_PERIOD >= 14) _viewModel.Data.NPERIOD14 = _viewModel.Data.NBUDGET;
                if (_viewModel.PeriodCount.INO_PERIOD >= 15) _viewModel.Data.NPERIOD15 = _viewModel.Data.NBUDGET;
                break;
            }
            case "AN":
            {
                _viewModel.Data.NBUDGET = _fieldBudget.Value; // sementara, karena perubahan tidak langsung bind value
                var loResult = await _viewModel.CalculateBudget(_viewModel.BudgetHDEntity, _viewModel.Data);
                if (_viewModel.PeriodCount.INO_PERIOD >= 1) _viewModel.Data.NPERIOD1 = loResult.NPERIOD1;
                if (_viewModel.PeriodCount.INO_PERIOD >= 2) _viewModel.Data.NPERIOD2 = loResult.NPERIOD2;
                if (_viewModel.PeriodCount.INO_PERIOD >= 3) _viewModel.Data.NPERIOD3 = loResult.NPERIOD3;
                if (_viewModel.PeriodCount.INO_PERIOD >= 4) _viewModel.Data.NPERIOD4 = loResult.NPERIOD4;
                if (_viewModel.PeriodCount.INO_PERIOD >= 5) _viewModel.Data.NPERIOD5 = loResult.NPERIOD5;
                if (_viewModel.PeriodCount.INO_PERIOD >= 6) _viewModel.Data.NPERIOD6 = loResult.NPERIOD6;
                if (_viewModel.PeriodCount.INO_PERIOD >= 7) _viewModel.Data.NPERIOD7 = loResult.NPERIOD7;
                if (_viewModel.PeriodCount.INO_PERIOD >= 8) _viewModel.Data.NPERIOD8 = loResult.NPERIOD8;
                if (_viewModel.PeriodCount.INO_PERIOD >= 9) _viewModel.Data.NPERIOD9 = loResult.NPERIOD9;
                if (_viewModel.PeriodCount.INO_PERIOD >= 10) _viewModel.Data.NPERIOD10 = loResult.NPERIOD10;
                if (_viewModel.PeriodCount.INO_PERIOD >= 11) _viewModel.Data.NPERIOD11 = loResult.NPERIOD11;
                if (_viewModel.PeriodCount.INO_PERIOD >= 12) _viewModel.Data.NPERIOD12 = loResult.NPERIOD12;
                if (_viewModel.PeriodCount.INO_PERIOD >= 13) _viewModel.Data.NPERIOD13 = loResult.NPERIOD13;
                if (_viewModel.PeriodCount.INO_PERIOD >= 14) _viewModel.Data.NPERIOD14 = loResult.NPERIOD14;
                if (_viewModel.PeriodCount.INO_PERIOD >= 15) _viewModel.Data.NPERIOD15 = loResult.NPERIOD15;
                break;
            }
        }
    }

    private void CheckAdd(R_CheckAddEventArgs eventArgs)
    {
        _btnGenerate.Enabled = _viewModel.BudgetHDEntity.LFINAL == false;
        eventArgs.Allow = _viewModel.BudgetHDEntity.LFINAL == false;
    }

    private void CheckEdit(R_CheckEditEventArgs eventArgs)
    {
        eventArgs.Allow = _viewModel.BudgetHDEntity.LFINAL == false;
    }

    private void CheckDelete(R_CheckDeleteEventArgs eventArgs)
    {
        eventArgs.Allow = _viewModel.BudgetHDEntity.LFINAL == false && _viewModel.BudgetDTList.Count > 0;
    }

    private Task BeforeGeneratePopup(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.Parameter = new GLM00500ParameterGenerateBudget
        {
            BudgetHD = _viewModel.BudgetHDEntity,
            CGLACCOUNT_TYPE = _viewModel.SelectedAccountType
        };
        eventArgs.TargetPageType = typeof(GLM00500DetailGenerate);
        return Task.CompletedTask;
    }

    private async Task AfterGeneratePopup(R_AfterOpenPopupEventArgs eventArgs)
    {
        var loException = new R_Exception();
        try
        {
            if (eventArgs.Success == false)
                return;

            var loResult = (GLM00500GenerateAccountBudgetDTO)eventArgs.Result;
            await _viewModel.GenerateBudget(loResult);
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();
        
        
    }
}