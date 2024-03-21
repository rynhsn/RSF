using System.Reflection.Emit;
using GLM00500Common.DTOs;
using GLM00500Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Extensions;
using R_BlazorFrontEnd.Helpers;

namespace GLM00500Front;

public partial class GLM00500DetailGenerate
{
    private GLM00500DetailViewModel _viewModel = new();


    //init master
    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            var loData = (GLM00500ParameterGenerateBudget)poParameter;
            _viewModel.GenerateAccountBudget.CBUDGET_ID = loData.BudgetHD.CREC_ID;
            _viewModel.GenerateAccountBudget.CBUDGET_NO = loData.BudgetHD.CBUDGET_NO;
            _viewModel.GenerateAccountBudget.CCURRENCY_TYPE = loData.BudgetHD.CCURRENCY_TYPE;
            _viewModel.GenerateAccountBudget.CGLACCOUNT_TYPE = loData.CGLACCOUNT_TYPE;
            _viewModel.GenerateAccountBudget.CBY = "P";
            _viewModel.GenerateAccountBudget.CUPDATE_METHOD = "C";
            _viewModel.GenerateAccountBudget.CBASED_ON = "EB";
            await _viewModel.Init(loData.BudgetHD);
            await _viewModel.GetPeriods();
            await _viewModel.GetSystemParams();
            await GetBudgetHDList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetBudgetHDList()
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetBudgetHDList(_viewModel.SelectedYear);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnLostFocusFromAccount()
    {
        var loEx = new R_Exception();

        LookupGSL00510ViewModel loLookupViewModel = new LookupGSL00510ViewModel();
        try
        {
            var param = new GSL00510ParameterDTO
            {
                CGLACCOUNT_TYPE = _viewModel.SelectedAccountType,
                CSEARCH_TEXT = _viewModel.GenerateAccountBudget.CFROM_GLACCOUNT_NO
            };

            var loResult = await loLookupViewModel.GetCOA(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.GenerateAccountBudget.CFROM_GLACCOUNT_NAME = "";
                goto EndBlock;
            }

            _viewModel.GenerateAccountBudget.CFROM_GLACCOUNT_NAME = loResult.CGLACCOUNT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        await R_DisplayExceptionAsync(loEx);
    }

    private void BeforeOpenLookupFromAccount(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loParameter = new GSL00510ParameterDTO
        {
            CGLACCOUNT_TYPE = _viewModel.SelectedAccountType
        };

        eventArgs.Parameter = loParameter;
        eventArgs.TargetPageType = typeof(GSL00510);
    }

    private void AfterOpenLookupFromAccount(R_AfterOpenLookupEventArgs eventArgs)
    {
        if (eventArgs.Result == null) return;

        var loTempResult = (GSL00510DTO)eventArgs.Result;
        _viewModel.GenerateAccountBudget.CFROM_GLACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
        _viewModel.GenerateAccountBudget.CFROM_GLACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;
    }

    private async Task OnLostFocusToAccount()
    {
        var loEx = new R_Exception();

        LookupGSL00510ViewModel loLookupViewModel = new LookupGSL00510ViewModel();
        try
        {
            var param = new GSL00510ParameterDTO
            {
                CGLACCOUNT_TYPE = _viewModel.SelectedAccountType,
                CSEARCH_TEXT = _viewModel.GenerateAccountBudget.CTO_GLACCOUNT_NO
            };

            var loResult = await loLookupViewModel.GetCOA(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.GenerateAccountBudget.CTO_GLACCOUNT_NAME = "";
                goto EndBlock;
            }

            _viewModel.GenerateAccountBudget.CTO_GLACCOUNT_NAME = loResult.CGLACCOUNT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        await R_DisplayExceptionAsync(loEx);
    }
    
    private void BeforeOpenLookupToAccount(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loParameter = new GSL00510ParameterDTO
        {
            CGLACCOUNT_TYPE = _viewModel.SelectedAccountType
        };

        eventArgs.Parameter = loParameter;
        eventArgs.TargetPageType = typeof(GSL00510);
    }

    private void AfterOpenLookupToAccount(R_AfterOpenLookupEventArgs eventArgs)
    {
        if (eventArgs.Result == null) return;

        var loTempResult = (GSL00510DTO)eventArgs.Result;
        _viewModel.GenerateAccountBudget.CTO_GLACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
        _viewModel.GenerateAccountBudget.CTO_GLACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;
    }

    private async Task OnLostFocusFromCenter()
    {
        var loEx = new R_Exception();

        LookupGSL00900ViewModel loLookupViewModel = new LookupGSL00900ViewModel();
        try
        {
            var param = new GSL00900ParameterDTO
            {
                CSEARCH_TEXT = _viewModel.GenerateAccountBudget.CFROM_CENTER_CODE
            };

            var loResult = await loLookupViewModel.GetCenter(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.GenerateAccountBudget.CFROM_CENTER_NAME = "";
                goto EndBlock;
            }

            _viewModel.GenerateAccountBudget.CFROM_CENTER_NAME = loResult.CCENTER_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        await R_DisplayExceptionAsync(loEx);
    }
    
    private void BeforeOpenLookupFromCenter(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loParameter = new GSL00900ParameterDTO();

        eventArgs.Parameter = loParameter;
        eventArgs.TargetPageType = typeof(GSL00900);
    }

    private void AfterOpenLookupFromCenter(R_AfterOpenLookupEventArgs eventArgs)
    {
        if (eventArgs.Result == null) return;

        var loTempResult = (GSL00900DTO)eventArgs.Result;
        _viewModel.GenerateAccountBudget.CFROM_CENTER_CODE = loTempResult.CCENTER_CODE;
        _viewModel.GenerateAccountBudget.CFROM_CENTER_NAME = loTempResult.CCENTER_NAME;
    }

    private async Task OnLostFocusToCenter()
    {
        var loEx = new R_Exception();

        LookupGSL00900ViewModel loLookupViewModel = new LookupGSL00900ViewModel();
        try
        {
            var param = new GSL00900ParameterDTO
            {
                CSEARCH_TEXT = _viewModel.GenerateAccountBudget.CTO_CENTER_CODE
            };

            var loResult = await loLookupViewModel.GetCenter(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.GenerateAccountBudget.CTO_CENTER_NAME = "";
                goto EndBlock;
            }

            _viewModel.GenerateAccountBudget.CTO_CENTER_NAME = loResult.CCENTER_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        await R_DisplayExceptionAsync(loEx);
    }
    
    private void BeforeOpenLookupToCenter(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loParameter = new GSL00900ParameterDTO();

        eventArgs.Parameter = loParameter;
        eventArgs.TargetPageType = typeof(GSL00900);
    }

    private void AfterOpenLookupToCenter(R_AfterOpenLookupEventArgs eventArgs)
    {
        if (eventArgs.Result == null) return;

        var loTempResult = (GSL00900DTO)eventArgs.Result;
        _viewModel.GenerateAccountBudget.CTO_CENTER_CODE = loTempResult.CCENTER_CODE;
        _viewModel.GenerateAccountBudget.CTO_CENTER_NAME = loTempResult.CCENTER_NAME;
    }

    private void CheckPeriodFrom(object obj)
    {
        var lcData = (string)obj;
        if (_viewModel.GenerateAccountBudget.CTO_PERIOD_NO == null) return;
        if (int.Parse(lcData) > int.Parse(_viewModel.GenerateAccountBudget.CTO_PERIOD_NO))
        {
            _viewModel.GenerateAccountBudget.CTO_PERIOD_NO = lcData;
        }
    }

    private void CheckPeriodTo(object obj)
    {
        var lcData = (string)obj;
        if (_viewModel.GenerateAccountBudget.CFROM_PERIOD_NO == null) return;
        if (int.Parse(lcData) < int.Parse(_viewModel.GenerateAccountBudget.CFROM_PERIOD_NO))
        {
            _viewModel.GenerateAccountBudget.CFROM_PERIOD_NO = lcData;
        }
    }

    private async Task ClickProcess()
    {
        if (_viewModel.GenerateAccountBudget.CSOURCE_BUDGET_NO == null &&
            _viewModel.GenerateAccountBudget.CBASED_ON == "EB")
        {
            await R_MessageBox.Show(_localizer["ErrorLabel"], _localizer["Exception08"], R_eMessageBoxButtonType.OK);
        }
        else
        {
            if (_viewModel.GenerateAccountBudget.CBASED_ON == "AV")
                _viewModel.GenerateAccountBudget.CSOURCE_BUDGET_NO = "";
            _viewModel.GenerateAccountBudget.CYEAR = _viewModel.SelectedYear.ToString();

            await this.Close(true, _viewModel.GenerateAccountBudget);
        }
    }

    private async Task ClickCancel()
    {
        await this.Close(false, null);
    }

    private void ChangeBy(object obj)
    {
        var lcData = (string)obj;
        if (lcData == "P")
        {
            _viewModel.GenerateAccountBudget.NBY_AMOUNT = 0;
        }
        else
        {
            _viewModel.GenerateAccountBudget.NBY_PCT = 0;
        }
    }
}