using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorClientHelper;
using GLR00300Common;
using GLR00300Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_PMCOMMON.DTOs.PopUpSaveAs;
using Lookup_PMFRONT;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;

namespace GLR00300Front
{
    public partial class GLR00300
    {
        private GLR00300ViewModel _viewModelGLR00300 = new();
        private R_Conductor _conducterRef;

        [Inject] private IClientHelper _clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }

        private void StateChangeInvoke()
        {
            StateHasChanged();
        }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await ServiceGetTrialBalance();
                await ServiceGetInitialProcess();
                await ServiceGetPeriod();
                await ServiceGetPrintMethod();
                await ServiceGetBudgetNo();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceGetTrialBalance()
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModelGLR00300.GetTrialBalance();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task ServiceGetInitialProcess()
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelGLR00300.GetInitialProcess();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task ServiceGetPrintMethod()
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModelGLR00300.GetPrintMethod();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task OnChangeYear(object poParam)
        {
            var loEx = new R_Exception();
            int lsParam = (int)poParam;
            try
            {
                _viewModelGLR00300.PeriodYear = lsParam;
                await ServiceGetBudgetNo();
                await ServiceGetPeriod();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            await R_DisplayExceptionAsync(loEx);
        }
        private async Task OnChangeCurrencyType(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                await ServiceGetBudgetNo();
                await ServiceGetPeriod();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task ServiceGetBudgetNo()
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModelGLR00300.GetBudgetNo();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task ServiceGetPeriod()
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModelGLR00300.GetPeriod();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private void IsTrialBalanceTypeNormal(object poParam)
        {
            var loEx = new R_Exception();
            string TrialBalanceValue = (string)poParam;
            try
            {
                _viewModelGLR00300.TrialBalanceTypeValue = TrialBalanceValue;

                if (_viewModelGLR00300.TrialBalanceTypeValue != "N")
                {
                    _viewModelGLR00300._lIsTrialBalanceTypeIsNormal = false;
                }
                else
                {
                    _viewModelGLR00300._lIsTrialBalanceTypeIsNormal = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private Task OnChangedCheckBoxPrintByCenter()
        {
            var loEx = new R_Exception();
            try
            {
                if (!_viewModelGLR00300._lPrintByCenter)
                {
                    _viewModelGLR00300.FromCenter.CCENTER_CODE = "";
                    _viewModelGLR00300.FromCenter.CCENTER_NAME = "";
                    _viewModelGLR00300.ToCenter.CCENTER_CODE = "";
                    _viewModelGLR00300.ToCenter.CCENTER_NAME = "";
                }
                else if (_viewModelGLR00300._lPrintByCenter && string.IsNullOrEmpty(_viewModelGLR00300.FromCenter.CCENTER_CODE) && string.IsNullOrEmpty(_viewModelGLR00300.ToCenter.CCENTER_CODE))
                {
                    _viewModelGLR00300.FromCenter.CCENTER_NAME = "Please Select Center Name.!";
                    _viewModelGLR00300.ToCenter.CCENTER_NAME = "Please Select Center Name.!";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
            return Task.CompletedTask;
        }
        private Task OnChangedCheckBoxPrintBudget()
        {
            var loEx = new R_Exception();
            try
            {
                if (!_viewModelGLR00300._lPrintBudget)
                {
                    _viewModelGLR00300.BudgetNoValue = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
            return Task.CompletedTask;
        }

        #region LookUp_FROM_Account
        private void BeforeOpenLookUpFromAccount(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GSL00510ParameterDTO();
                loParam.CCOMPANY_ID = _clientHelper.CompanyId;
                loParam.CGLACCOUNT_TYPE = "N";
                loParam.CUSER_ID = _clientHelper.UserId;
                eventArgs.Parameter = loParam;

                eventArgs.TargetPageType = typeof(GSL00510);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }
        private void AfterOpenLookUpFromAccount(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loTempResult = (GSL00510DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                var loGetData = _viewModelGLR00300.InitialProcess;
                loGetData.CMIN_GLACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
                loGetData.CMIN_GLACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task LostFocusLookupFromAccountAsync()
        {
            var loEx = new R_Exception();

            try
            {
                var loGetData = _viewModelGLR00300.InitialProcess;
                if (!string.IsNullOrWhiteSpace(loGetData.CMIN_GLACCOUNT_NO))
                {
                    LookupGSL00510ViewModel loLookupViewModel = new LookupGSL00510ViewModel();
                    var param = new GSL00510ParameterDTO
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                        CGLACCOUNT_TYPE = "N",
                        CSEARCH_TEXT = _viewModelGLR00300.InitialProcess.CMIN_GLACCOUNT_NO!,
                    };
                    var loResult = await loLookupViewModel.GetCOA(param);
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loGetData.CMIN_GLACCOUNT_NO = "";
                        loGetData.CMIN_GLACCOUNT_NAME = "";
                    }
                    else
                    {
                        loGetData.CMIN_GLACCOUNT_NO = loResult.CGLACCOUNT_NO;
                        loGetData.CMIN_GLACCOUNT_NAME = loResult.CGLACCOUNT_NAME;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #region LookUp_TO_Account
        private void BeforeOpenLookUpToAccount(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GSL00510ParameterDTO();
                loParam.CCOMPANY_ID = _clientHelper.CompanyId;
                loParam.CGLACCOUNT_TYPE = "N";
                loParam.CUSER_ID = _clientHelper.UserId;
                eventArgs.Parameter = loParam;

                eventArgs.TargetPageType = typeof(GSL00510);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }
        private void AfterOpenLookUpToAccount(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loTempResult = (GSL00510DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                var loGetData = _viewModelGLR00300.InitialProcess;
                loGetData.CMAX_GLACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
                loGetData.CMAX_GLACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task LostFocusLookupToAccount()
        {
            var loEx = new R_Exception();

            try
            {
                var loGetData = _viewModelGLR00300.InitialProcess;
                if (!string.IsNullOrWhiteSpace(loGetData.CMAX_GLACCOUNT_NO))
                {
                    LookupGSL00510ViewModel loLookupViewModel = new LookupGSL00510ViewModel();
                    var param = new GSL00510ParameterDTO
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                        CGLACCOUNT_TYPE = "N",
                        CSEARCH_TEXT = _viewModelGLR00300.InitialProcess.CMAX_GLACCOUNT_NO!,
                    };
                    var loResult = await loLookupViewModel.GetCOA(param);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loGetData.CMAX_GLACCOUNT_NO = "";
                        loGetData.CMAX_GLACCOUNT_NAME = "";
                    }
                    else
                    {
                        loGetData.CMAX_GLACCOUNT_NO = loResult.CGLACCOUNT_NO;
                        loGetData.CMAX_GLACCOUNT_NAME = loResult.CGLACCOUNT_NAME;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region LookUp_FROM_PrintByCenter
        private void BeforeOpenLookupFromPrintByCenter(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GSL00900ParameterDTO();
                loParam.CCOMPANY_ID = _clientHelper.CompanyId;
                loParam.CUSER_ID = _clientHelper.UserId;
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GSL00900);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }
        private void AfterOpenLookUpFromPrintByCenter(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempResult = (GSL00900DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                var loGetData = _viewModelGLR00300.FromCenter;
                loGetData.CCENTER_CODE = loTempResult.CCENTER_CODE;
                loGetData.CCENTER_NAME = loTempResult.CCENTER_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task LostFocusLookupFromCenter()
        {
            var loEx = new R_Exception();

            try
            {
                var loGetData = _viewModelGLR00300.FromCenter;
                if (!string.IsNullOrWhiteSpace(loGetData.CCENTER_CODE))
                {
                    LookupGSL00900ViewModel loLookupViewModel = new LookupGSL00900ViewModel();
                    var param = new GSL00900ParameterDTO
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModelGLR00300.FromCenter.CCENTER_CODE!,
                    };
                    var loResult = await loLookupViewModel.GetCenter(param);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loGetData.CCENTER_CODE = "";
                        loGetData.CCENTER_NAME = "";
                    }
                    else
                    {
                        loGetData.CCENTER_CODE = loResult.CCENTER_CODE;
                        loGetData.CCENTER_NAME = loResult.CCENTER_NAME;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region LookUp_TO_PrintByCenter
        private void BeforeOpenLookupToPrintByCenter(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GSL00900ParameterDTO();
                loParam.CCOMPANY_ID = _clientHelper.CompanyId;
                loParam.CUSER_ID = _clientHelper.UserId;
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GSL00900);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private void AfterOpenLookUpToPrintByCenter(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempResult = (GSL00900DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                var loGetData = _viewModelGLR00300.ToCenter;
                loGetData.CCENTER_CODE = loTempResult.CCENTER_CODE;
                loGetData.CCENTER_NAME = loTempResult.CCENTER_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task LostFocusLookupToCenter()
        {
            var loEx = new R_Exception();

            try
            {
                var loGetData = _viewModelGLR00300.ToCenter;
                if (!string.IsNullOrWhiteSpace(loGetData.CCENTER_CODE))
                {
                    LookupGSL00900ViewModel loLookupViewModel = new LookupGSL00900ViewModel();
                    var param = new GSL00900ParameterDTO
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModelGLR00300.ToCenter.CCENTER_CODE!,
                    };
                    var loResult = await loLookupViewModel.GetCenter(param);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loGetData.CCENTER_CODE = "";
                        loGetData.CCENTER_NAME = "";
                    }
                    else
                    {
                        loGetData.CCENTER_CODE = loResult.CCENTER_CODE;
                        loGetData.CCENTER_NAME = loResult.CCENTER_NAME;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region ButtonReport
        private async Task GenerateReport()
        {
            var loEx = new R_Exception();
            bool loFlag = false;
            try
            {
                _viewModelGLR00300.ValidationFieldEmpty();

                var lcPeriodYear = _viewModelGLR00300.PeriodYear.ToString();
                var loParam = new GLR00300ParamDBToGetReportDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CFROM_PERIOD_NO = _viewModelGLR00300.PeriodNo, //based on spec the value Period_No FROM and TO is same
                    CTO_PERIOD_NO = _viewModelGLR00300.PeriodNo,
                    CTB_TYPE_CODE = _viewModelGLR00300.TrialBalanceTypeValue,
                    CCURRENCY_TYPE_CODE = _viewModelGLR00300.CurrencyTypeValue,
                    CJOURNAL_ADJ_MODE_CODE = _viewModelGLR00300.JournalAdjustModeValue,
                    CYEAR = lcPeriodYear,
                    CFROM_ACCOUNT_NO = _viewModelGLR00300.InitialProcess.CMIN_GLACCOUNT_NO,
                    CTO_ACCOUNT_NO = _viewModelGLR00300.InitialProcess.CMAX_GLACCOUNT_NO,
                    CFROM_CENTER_CODE = _viewModelGLR00300.FromCenter.CCENTER_CODE,
                    CTO_CENTER_CODE = _viewModelGLR00300.ToCenter.CCENTER_CODE,
                    CPRINT_METHOD_CODE = _viewModelGLR00300.PrintMethodValue,
                    CBUDGET_NO = _viewModelGLR00300.BudgetNoValue,
                    LIS_PRINT = true,
                    CREPORT_FILENAME = "",
                    CREPORT_FILETYPE = "",
                };

                var loValidate = await R_MessageBox.Show("", "Are you sure print this?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    if (loParam.CTB_TYPE_CODE == "N")
                    {
                        if (loParam.CJOURNAL_ADJ_MODE_CODE == "S")
                        {
                            if ((!_viewModelGLR00300._lPrintByCenter) && (!_viewModelGLR00300._lPrintBudget)) // (N,S,false,false)
                            {
                                loFlag = true;
                                //FORMAT A
                                await _reportService.GetReport(
                                    "R_DefaultServiceUrlGL",
                                    "GL",
                                    "rpt/GLR00300ReportFormatA/AllTrialBalanceReportPost",
                                    "rpt/GLR00300ReportFormatA/AllTrialBalanceReportGet",
                                    loParam);
                            }
                            else if (!_viewModelGLR00300._lPrintByCenter && _viewModelGLR00300._lPrintBudget) // (N,S,false,true)
                            {
                                loFlag = true;
                                //FORMAT B
                                await _reportService.GetReport(
                                    "R_DefaultServiceUrlGL",
                                    "GL",
                                    "rpt/GLR00300ReportFormatB/AllTrialBalanceReportPost",
                                    "rpt/GLR00300ReportFormatB/AllTrialBalanceReportGet",
                                    loParam);
                            }
                        }
                        else if (loParam.CJOURNAL_ADJ_MODE_CODE == "M")
                        {
                            if ((!_viewModelGLR00300._lPrintByCenter) && (!_viewModelGLR00300._lPrintBudget)) // (N,M,false,false)
                            {
                                loFlag = true;
                                //FORMAT C
                                await _reportService.GetReport(
                                    "R_DefaultServiceUrlGL",
                                    "GL",
                                    "rpt/GLR00300ReportFormatC/AllTrialBalanceReportPost",
                                    "rpt/GLR00300ReportFormatC/AllTrialBalanceReportGet",
                                    loParam);
                            }
                            else if ((!_viewModelGLR00300._lPrintByCenter) && (_viewModelGLR00300._lPrintBudget)) // (N,M,false,true)
                            {
                                loFlag = true;
                                //FORMAT D
                                await _reportService.GetReport(
                                    "R_DefaultServiceUrlGL",
                                    "GL",
                                    "rpt/GLR00300ReportFormatD/AllTrialBalanceReportPost",
                                    "rpt/GLR00300ReportFormatD/AllTrialBalanceReportGet",
                                    loParam);
                            }
                        }
                    }
                    else if (loParam.CTB_TYPE_CODE == "A")
                    {
                        if (loParam.CJOURNAL_ADJ_MODE_CODE == "S")
                        {
                            if ((_viewModelGLR00300._lPrintByCenter) && !(_viewModelGLR00300._lPrintBudget)) // (A,S,True,false)
                            {
                                loFlag = true;
                                //FORMAT E
                                await _reportService.GetReport(
                                    "R_DefaultServiceUrlGL",
                                    "GL",
                                    "rpt/GLR00300ReportFormatE/AllTrialBalanceReportPost",
                                    "rpt/GLR00300ReportFormatE/AllTrialBalanceReportGet",
                                    loParam);
                            }
                            else if (_viewModelGLR00300._lPrintByCenter && _viewModelGLR00300._lPrintBudget) // (A,S,true,true)
                            {
                                loFlag = true;
                                //FORMAT F
                                await _reportService.GetReport(
                                    "R_DefaultServiceUrlGL",
                                    "GL",
                                    "rpt/GLR00300ReportFormatF/AllTrialBalanceReportPost",
                                    "rpt/GLR00300ReportFormatF/AllTrialBalanceReportGet",
                                    loParam);
                            }
                        }
                        else if (loParam.CJOURNAL_ADJ_MODE_CODE == "M")
                        {
                            if ((_viewModelGLR00300._lPrintByCenter) && (!_viewModelGLR00300._lPrintBudget)) // (A,M,true,false)
                            {
                                loFlag = true;
                                //FORMAT G
                                await _reportService.GetReport(
                                    "R_DefaultServiceUrlGL",
                                    "GL",
                                    "rpt/GLR00300ReportFormatG/AllTrialBalanceReportPost",
                                    "rpt/GLR00300ReportFormatG/AllTrialBalanceReportGet",
                                    loParam);
                            }
                            else if ((_viewModelGLR00300._lPrintByCenter) && (_viewModelGLR00300._lPrintBudget)) // (A,M,true,true)
                            {
                                loFlag = true;
                                //FORMAT H
                                await _reportService.GetReport(
                                    "R_DefaultServiceUrlGL",
                                    "GL",
                                    "rpt/GLR00300ReportFormatH/AllTrialBalanceReportPost",
                                    "rpt/GLR00300ReportFormatH/AllTrialBalanceReportGet",
                                    loParam);
                            }
                        }
                    }
                    if (!loFlag)
                    {
                        await R_MessageBox.Show("", "Can not process this command!", R_eMessageBoxButtonType.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            R_DisplayException(loEx);
        }

        #endregion
        #region SaveAs

        private void BeforeOpen_PopupSaveAsAsync(R_BeforeOpenPopupEventArgs eventArgs)
        {
            //InitializePrintParam();
            _viewModelGLR00300.ValidationFieldEmpty();
            eventArgs.PageTitle = _localizer["_titleSaveAs"];
            eventArgs.TargetPageType = typeof(PopUpSaveAs);
        }
        private async Task AfterOpen_PopupSaveAs(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            GLR00300ParamDBToGetReportDTO loParam;
            bool loFlag = false;
            try
            {
                if (eventArgs.Success)
                {
                    var loReturn = R_FrontUtility.ConvertObjectToObject<SaveAsDTO>(eventArgs.Result);
                    _viewModelGLR00300.ValidationFieldEmpty();
                    var lcPeriodYear = _viewModelGLR00300.PeriodYear.ToString();
                    loParam = new GLR00300ParamDBToGetReportDTO()
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                        CFROM_PERIOD_NO = _viewModelGLR00300.PeriodNo, //based on spec the value Period_No FROM and TO is same
                        CTO_PERIOD_NO = _viewModelGLR00300.PeriodNo,
                        CTB_TYPE_CODE = _viewModelGLR00300.TrialBalanceTypeValue,
                        CCURRENCY_TYPE_CODE = _viewModelGLR00300.CurrencyTypeValue,
                        CJOURNAL_ADJ_MODE_CODE = _viewModelGLR00300.JournalAdjustModeValue,
                        CYEAR = lcPeriodYear,
                        CFROM_ACCOUNT_NO = _viewModelGLR00300.InitialProcess.CMIN_GLACCOUNT_NO,
                        CTO_ACCOUNT_NO = _viewModelGLR00300.InitialProcess.CMAX_GLACCOUNT_NO,
                        CFROM_CENTER_CODE = _viewModelGLR00300.FromCenter.CCENTER_CODE,
                        CTO_CENTER_CODE = _viewModelGLR00300.ToCenter.CCENTER_CODE,
                        CPRINT_METHOD_CODE = _viewModelGLR00300.PrintMethodValue,
                        CBUDGET_NO = _viewModelGLR00300.BudgetNoValue,
                        LIS_PRINT = false,
                        CREPORT_FILENAME = loReturn.CREPORT_FILENAME,
                        CREPORT_FILETYPE = loReturn.CREPORT_FILETYPE,
                    };


                    if (loParam.CTB_TYPE_CODE == "N")
                    {
                        if (loParam.CJOURNAL_ADJ_MODE_CODE == "S")
                        {
                            if ((!_viewModelGLR00300._lPrintByCenter) && (!_viewModelGLR00300._lPrintBudget)) // (N,S,false,false)
                            {
                                loFlag = true;
                                //FORMAT A
                                await _reportService.GetReport(
                                    "R_DefaultServiceUrlGL",
                                    "GL",
                                    "rpt/GLR00300ReportFormatA/AllTrialBalanceReportPost",
                                    "rpt/GLR00300ReportFormatA/AllTrialBalanceReportGet",
                                    loParam);
                            }
                            else if (!_viewModelGLR00300._lPrintByCenter && _viewModelGLR00300._lPrintBudget) // (N,S,false,true)
                            {
                                loFlag = true;
                                //FORMAT B
                                await _reportService.GetReport(
                                    "R_DefaultServiceUrlGL",
                                    "GL",
                                    "rpt/GLR00300ReportFormatB/AllTrialBalanceReportPost",
                                    "rpt/GLR00300ReportFormatB/AllTrialBalanceReportGet",
                                    loParam);
                            }
                        }
                        else if (loParam.CJOURNAL_ADJ_MODE_CODE == "M")
                        {
                            if ((!_viewModelGLR00300._lPrintByCenter) && (!_viewModelGLR00300._lPrintBudget)) // (N,M,false,false)
                            {
                                loFlag = true;
                                //FORMAT C
                                await _reportService.GetReport(
                                    "R_DefaultServiceUrlGL",
                                    "GL",
                                    "rpt/GLR00300ReportFormatC/AllTrialBalanceReportPost",
                                    "rpt/GLR00300ReportFormatC/AllTrialBalanceReportGet",
                                    loParam);
                            }
                            else if ((!_viewModelGLR00300._lPrintByCenter) && (_viewModelGLR00300._lPrintBudget)) // (N,M,false,true)
                            {
                                loFlag = true;
                                //FORMAT D
                                await _reportService.GetReport(
                                    "R_DefaultServiceUrlGL",
                                    "GL",
                                    "rpt/GLR00300ReportFormatD/AllTrialBalanceReportPost",
                                    "rpt/GLR00300ReportFormatD/AllTrialBalanceReportGet",
                                    loParam);
                            }
                        }
                    }
                    else if (loParam.CTB_TYPE_CODE == "A")
                    {
                        if (loParam.CJOURNAL_ADJ_MODE_CODE == "S")
                        {
                            if ((_viewModelGLR00300._lPrintByCenter) && !(_viewModelGLR00300._lPrintBudget)) // (A,S,True,false)
                            {
                                loFlag = true;
                                //FORMAT E
                                await _reportService.GetReport(
                                    "R_DefaultServiceUrlGL",
                                    "GL",
                                    "rpt/GLR00300ReportFormatE/AllTrialBalanceReportPost",
                                    "rpt/GLR00300ReportFormatE/AllTrialBalanceReportGet",
                                    loParam);
                            }
                            else if (_viewModelGLR00300._lPrintByCenter && _viewModelGLR00300._lPrintBudget) // (A,S,true,true)
                            {
                                loFlag = true;
                                //FORMAT F
                                await _reportService.GetReport(
                                    "R_DefaultServiceUrlGL",
                                    "GL",
                                    "rpt/GLR00300ReportFormatF/AllTrialBalanceReportPost",
                                    "rpt/GLR00300ReportFormatF/AllTrialBalanceReportGet",
                                    loParam);
                            }
                        }
                        else if (loParam.CJOURNAL_ADJ_MODE_CODE == "M")
                        {
                            if ((_viewModelGLR00300._lPrintByCenter) && (!_viewModelGLR00300._lPrintBudget)) // (A,M,true,false)
                            {
                                loFlag = true;
                                //FORMAT G
                                await _reportService.GetReport(
                                    "R_DefaultServiceUrlGL",
                                    "GL",
                                    "rpt/GLR00300ReportFormatG/AllTrialBalanceReportPost",
                                    "rpt/GLR00300ReportFormatG/AllTrialBalanceReportGet",
                                    loParam);
                            }
                            else if ((_viewModelGLR00300._lPrintByCenter) && (_viewModelGLR00300._lPrintBudget)) // (A,M,true,true)
                            {
                                loFlag = true;
                                //FORMAT H
                                await _reportService.GetReport(
                                    "R_DefaultServiceUrlGL",
                                    "GL",
                                    "rpt/GLR00300ReportFormatH/AllTrialBalanceReportPost",
                                    "rpt/GLR00300ReportFormatH/AllTrialBalanceReportGet",
                                    loParam);
                            }
                        }
                    }
                    if (!loFlag)
                    {
                        await R_MessageBox.Show("", "Can not process this command!", R_eMessageBoxButtonType.OK);
                    }

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }
        #endregion

    }
}
