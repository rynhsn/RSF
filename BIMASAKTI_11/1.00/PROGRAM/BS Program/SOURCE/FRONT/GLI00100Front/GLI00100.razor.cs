using GLI00100Common.DTOs;
using GLI00100Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GLI00100Front;

public partial class GLI00100 : R_Page
{
    private GLI00100ViewModel _viewModel = new();
    private R_Conductor _conductorRef;
    private R_Grid<GLI00100AccountGridDTO> _gridRef = new();

    protected override async Task R_Init_From_Master(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.Init();
            await _gridRef.R_RefreshGrid(null);
            await RefreshBudgetCombo();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetAccountList();
            eventArgs.ListEntityResult = _viewModel.GLAccountList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetDetailRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GLI00100AccountParameterDTO>(eventArgs.Data);
            await _viewModel.GetAccountDetail(loParam);
            eventArgs.Result = _viewModel.GLAccountDetail;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnLostFocusCenter()
    {
        var loEx = new R_Exception();

        LookupGSL00900ViewModel loLookupViewModel = new LookupGSL00900ViewModel();
        try
        {
            var param = new GSL00900ParameterDTO
            {
                CSEARCH_TEXT = _viewModel.CenterCode
            };
            
            var loResult = await loLookupViewModel.GetCenter(param);

            if (loResult is null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.CenterCode = "";
                _viewModel.CenterName = "";
                goto EndBlock;
            }

            _viewModel.CenterName = loResult.CCENTER_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        await R_DisplayExceptionAsync(loEx);
    }

    private void BeforeOpenLookupCenter(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loParameter = new GSL00900ParameterDTO();
        eventArgs.Parameter = loParameter;
        eventArgs.TargetPageType = typeof(GSL00900);
    }

    private void AfterOpenLookupCenter(R_AfterOpenLookupEventArgs eventArgs)
    {
        if (eventArgs.Result == null) return;
        var loTempResult = (GSL00900DTO)eventArgs.Result;
        _viewModel.CenterCode = loTempResult.CCENTER_CODE;
        _viewModel.CenterName = loTempResult.CCENTER_NAME;
    }

    private async Task RefreshBudgetCombo()
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = new GLI00100BudgetParamsDTO()
            {
                CYEAR = _viewModel.Year.ToString(),
                CCURRENCY_TYPE = _viewModel.CurrencyTypeValue
            };

            await _viewModel.GetBudgetList(loParam);
            await _viewModel.GetPeriodCount();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task RefreshAccountAnalysis()
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = new GLI00100AccountAnalysisParameterDTO()
            {
                CGLACCOUNT_NO = _viewModel.Data.CGLACCOUNT_NO,
                CYEAR = _viewModel.Year.ToString(),
                CCURRENCY_TYPE = _viewModel.CurrencyTypeValue,
                CCENTER_CODE = _viewModel.CenterCode ?? "",
                CBUDGET_NO = _viewModel.BudgetNo
            };
            await _viewModel.GetAccountAnalysisDetail(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private void BeforeOpenPopupPrint(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.Parameter = new GLI00100PopupParamsDTO()
        {
            CGLACCOUNT_NO = _viewModel.Data.CGLACCOUNT_NO,
            CGLACCOUNT_NAME = _viewModel.Data.CGLACCOUNT_NAME,
            CYEAR = _viewModel.Year.ToString(),
            CCURRENCY_TYPE = _viewModel.CurrencyTypeValue,
            CCENTER_CODE = _viewModel.CenterCode ?? "",
            CCENTER_NAME = _viewModel.CenterName,
            CBUDGET_NO = _viewModel.BudgetNo,
            CBUDGET_NAME_DISPLAY = _viewModel.BudgetList.Where(x => x.CBUDGET_NO == _viewModel.BudgetNo)
                .Select(x => x.CBUDGET_NAME_DISPLAY).FirstOrDefault()
        };
        eventArgs.TargetPageType = typeof(GLI00100PrintAccountStatusPopup);
    }

    private void BeforeOpenPopupAccountJournal_01(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.Parameter = new GLI00100PopupParamsDTO()
        {
            CGLACCOUNT_NO = _viewModel.Data.CGLACCOUNT_NO,
            CGLACCOUNT_NAME = _viewModel.Data.CGLACCOUNT_NAME,
            CYEAR = _viewModel.Year.ToString(),
            CMONTH = "01",
            CCURRENCY_TYPE = _viewModel.CurrencyTypeValue,
            CCENTER_CODE = _viewModel.CenterCode ?? "",
            CCENTER_NAME = _viewModel.CenterName
        };
        eventArgs.TargetPageType = typeof(GLI00100AccountJournalPopup);
    }

    private void BeforeOpenPopupAccountJournal_02(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.Parameter = new GLI00100PopupParamsDTO()
        {
            CGLACCOUNT_NO = _viewModel.Data.CGLACCOUNT_NO,
            CGLACCOUNT_NAME = _viewModel.Data.CGLACCOUNT_NAME,
            CYEAR = _viewModel.Year.ToString(),
            CMONTH = "02",
            CCURRENCY_TYPE = _viewModel.CurrencyTypeValue,
            CCENTER_CODE = _viewModel.CenterCode ?? "",
            CCENTER_NAME = _viewModel.CenterName
        };
        eventArgs.TargetPageType = typeof(GLI00100AccountJournalPopup);
    }

    private void BeforeOpenPopupAccountJournal_03(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.Parameter = new GLI00100PopupParamsDTO()
        {
            CGLACCOUNT_NO = _viewModel.Data.CGLACCOUNT_NO,
            CGLACCOUNT_NAME = _viewModel.Data.CGLACCOUNT_NAME,
            CYEAR = _viewModel.Year.ToString(),
            CMONTH = "03",
            CCURRENCY_TYPE = _viewModel.CurrencyTypeValue,
            CCENTER_CODE = _viewModel.CenterCode ?? "",
            CCENTER_NAME = _viewModel.CenterName
        };
        eventArgs.TargetPageType = typeof(GLI00100AccountJournalPopup);
    }

    private void BeforeOpenPopupAccountJournal_04(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.Parameter = new GLI00100PopupParamsDTO()
        {
            CGLACCOUNT_NO = _viewModel.Data.CGLACCOUNT_NO,
            CGLACCOUNT_NAME = _viewModel.Data.CGLACCOUNT_NAME,
            CYEAR = _viewModel.Year.ToString(),
            CMONTH = "04",
            CCURRENCY_TYPE = _viewModel.CurrencyTypeValue,
            CCENTER_CODE = _viewModel.CenterCode ?? "",
            CCENTER_NAME = _viewModel.CenterName
        };
        eventArgs.TargetPageType = typeof(GLI00100AccountJournalPopup);
    }

    private void BeforeOpenPopupAccountJournal_05(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.Parameter = new GLI00100PopupParamsDTO()
        {
            CGLACCOUNT_NO = _viewModel.Data.CGLACCOUNT_NO,
            CGLACCOUNT_NAME = _viewModel.Data.CGLACCOUNT_NAME,
            CYEAR = _viewModel.Year.ToString(),
            CMONTH = "05",
            CCURRENCY_TYPE = _viewModel.CurrencyTypeValue,
            CCENTER_CODE = _viewModel.CenterCode ?? "",
            CCENTER_NAME = _viewModel.CenterName
        };
        eventArgs.TargetPageType = typeof(GLI00100AccountJournalPopup);
    }

    private void BeforeOpenPopupAccountJournal_06(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.Parameter = new GLI00100PopupParamsDTO()
        {
            CGLACCOUNT_NO = _viewModel.Data.CGLACCOUNT_NO,
            CGLACCOUNT_NAME = _viewModel.Data.CGLACCOUNT_NAME,
            CYEAR = _viewModel.Year.ToString(),
            CMONTH = "06",
            CCURRENCY_TYPE = _viewModel.CurrencyTypeValue,
            CCENTER_CODE = _viewModel.CenterCode ?? "",
            CCENTER_NAME = _viewModel.CenterName
        };
        eventArgs.TargetPageType = typeof(GLI00100AccountJournalPopup);
    }

    private void BeforeOpenPopupAccountJournal_07(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.Parameter = new GLI00100PopupParamsDTO()
        {
            CGLACCOUNT_NO = _viewModel.Data.CGLACCOUNT_NO,
            CGLACCOUNT_NAME = _viewModel.Data.CGLACCOUNT_NAME,
            CYEAR = _viewModel.Year.ToString(),
            CMONTH = "07",
            CCURRENCY_TYPE = _viewModel.CurrencyTypeValue,
            CCENTER_CODE = _viewModel.CenterCode ?? "",
            CCENTER_NAME = _viewModel.CenterName
        };
        eventArgs.TargetPageType = typeof(GLI00100AccountJournalPopup);
    }

    private void BeforeOpenPopupAccountJournal_08(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.Parameter = new GLI00100PopupParamsDTO()
        {
            CGLACCOUNT_NO = _viewModel.Data.CGLACCOUNT_NO,
            CGLACCOUNT_NAME = _viewModel.Data.CGLACCOUNT_NAME,
            CYEAR = _viewModel.Year.ToString(),
            CMONTH = "08",
            CCURRENCY_TYPE = _viewModel.CurrencyTypeValue,
            CCENTER_CODE = _viewModel.CenterCode ?? "",
            CCENTER_NAME = _viewModel.CenterName
        };
        eventArgs.TargetPageType = typeof(GLI00100AccountJournalPopup);
    }

    private void BeforeOpenPopupAccountJournal_09(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.Parameter = new GLI00100PopupParamsDTO()
        {
            CGLACCOUNT_NO = _viewModel.Data.CGLACCOUNT_NO,
            CGLACCOUNT_NAME = _viewModel.Data.CGLACCOUNT_NAME,
            CYEAR = _viewModel.Year.ToString(),
            CMONTH = "09",
            CCURRENCY_TYPE = _viewModel.CurrencyTypeValue,
            CCENTER_CODE = _viewModel.CenterCode ?? "",
            CCENTER_NAME = _viewModel.CenterName
        };
        eventArgs.TargetPageType = typeof(GLI00100AccountJournalPopup);
    }

    private void BeforeOpenPopupAccountJournal_10(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.Parameter = new GLI00100PopupParamsDTO()
        {
            CGLACCOUNT_NO = _viewModel.Data.CGLACCOUNT_NO,
            CGLACCOUNT_NAME = _viewModel.Data.CGLACCOUNT_NAME,
            CYEAR = _viewModel.Year.ToString(),
            CMONTH = "10",
            CCURRENCY_TYPE = _viewModel.CurrencyTypeValue,
            CCENTER_CODE = _viewModel.CenterCode ?? "",
            CCENTER_NAME = _viewModel.CenterName
        };
        eventArgs.TargetPageType = typeof(GLI00100AccountJournalPopup);
    }

    private void BeforeOpenPopupAccountJournal_11(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.Parameter = new GLI00100PopupParamsDTO()
        {
            CGLACCOUNT_NO = _viewModel.Data.CGLACCOUNT_NO,
            CGLACCOUNT_NAME = _viewModel.Data.CGLACCOUNT_NAME,
            CYEAR = _viewModel.Year.ToString(),
            CMONTH = "11",
            CCURRENCY_TYPE = _viewModel.CurrencyTypeValue,
            CCENTER_CODE = _viewModel.CenterCode ?? "",
            CCENTER_NAME = _viewModel.CenterName
        };
        eventArgs.TargetPageType = typeof(GLI00100AccountJournalPopup);
    }

    private void BeforeOpenPopupAccountJournal_12(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.Parameter = new GLI00100PopupParamsDTO()
        {
            CGLACCOUNT_NO = _viewModel.Data.CGLACCOUNT_NO,
            CGLACCOUNT_NAME = _viewModel.Data.CGLACCOUNT_NAME,
            CYEAR = _viewModel.Year.ToString(),
            CMONTH = "12",
            CCURRENCY_TYPE = _viewModel.CurrencyTypeValue,
            CCENTER_CODE = _viewModel.CenterCode ?? "",
            CCENTER_NAME = _viewModel.CenterName
        };
        eventArgs.TargetPageType = typeof(GLI00100AccountJournalPopup);
    }

    private void BeforeOpenPopupAccountJournal_13(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.Parameter = new GLI00100PopupParamsDTO()
        {
            CGLACCOUNT_NO = _viewModel.Data.CGLACCOUNT_NO,
            CGLACCOUNT_NAME = _viewModel.Data.CGLACCOUNT_NAME,
            CYEAR = _viewModel.Year.ToString(),
            CMONTH = "13",
            CCURRENCY_TYPE = _viewModel.CurrencyTypeValue,
            CCENTER_CODE = _viewModel.CenterCode ?? "",
            CCENTER_NAME = _viewModel.CenterName
        };
        eventArgs.TargetPageType = typeof(GLI00100AccountJournalPopup);
    }

    private void BeforeOpenPopupAccountJournal_14(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.Parameter = new GLI00100PopupParamsDTO()
        {
            CGLACCOUNT_NO = _viewModel.Data.CGLACCOUNT_NO,
            CGLACCOUNT_NAME = _viewModel.Data.CGLACCOUNT_NAME,
            CYEAR = _viewModel.Year.ToString(),
            CMONTH = "14",
            CCURRENCY_TYPE = _viewModel.CurrencyTypeValue,
            CCENTER_CODE = _viewModel.CenterCode ?? "",
            CCENTER_NAME = _viewModel.CenterName
        };
        eventArgs.TargetPageType = typeof(GLI00100AccountJournalPopup);
    }

    private void BeforeOpenPopupAccountJournal_15(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.Parameter = new GLI00100PopupParamsDTO()
        {
            CGLACCOUNT_NO = _viewModel.Data.CGLACCOUNT_NO,
            CGLACCOUNT_NAME = _viewModel.Data.CGLACCOUNT_NAME,
            CYEAR = _viewModel.Year.ToString(),
            CMONTH = "15",
            CCURRENCY_TYPE = _viewModel.CurrencyTypeValue,
            CCENTER_CODE = _viewModel.CenterCode ?? "",
            CCENTER_NAME = _viewModel.CenterName
        };
        eventArgs.TargetPageType = typeof(GLI00100AccountJournalPopup);
    }
}