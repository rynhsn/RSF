﻿using BlazorClientHelper;
using GLR00100Common.DTOs;
using GLR00100Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;

namespace GLR00100Front
{
    public partial class GLR00100 : R_Page
    {
        private GLR00100ViewModel _viewModel = new GLR00100ViewModel();
        private R_ComboBox<GLR00100TransCodeDTO, string> ComboTransCode { get; set; }
        private R_TextBox TextFromDept { get; set; }
        private R_TextBox TextToDept { get; set; }

        [Inject] private R_IReport _reportService { get; set; }
        [Inject] private IClientHelper _clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.Init();
                await ComboTransCode.FocusAsync();
                await _viewModel.GetTransCodeList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        private async Task OnLostFocusLookupFromDept()
        {
            var loEx = new R_Exception();

            var loLookupViewModel = new LookupGSL00700ViewModel();
            try
            {
                if (_viewModel.ReportParam.CFROM_DEPT_CODE == null ||
                    _viewModel.ReportParam.CFROM_DEPT_CODE.Trim().Length <= 0)
                {
                    _viewModel.ReportParam.CFROM_DEPT_NAME = "";
                    return;
                }

                var param = new GSL00700ParameterDTO
                {
                    CSEARCH_TEXT = _viewModel.ReportParam.CFROM_DEPT_CODE
                };

                GSL00700DTO loResult = null;

                loResult = await loLookupViewModel.GetDepartment(param);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                        "_ErrLookup01"));
                    _viewModel.ReportParam.CFROM_DEPT_CODE = "";
                    _viewModel.ReportParam.CFROM_DEPT_NAME = "";
                    goto EndBlock;
                }

                _viewModel.ReportParam.CFROM_DEPT_CODE = loResult.CDEPT_CODE;
                _viewModel.ReportParam.CFROM_DEPT_NAME = loResult.CDEPT_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            R_DisplayException(loEx);
        }

        private void BeforeLookupFromDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(GSL00700);
            eventArgs.Parameter = new GSL00700ParameterDTO();
        }

        private void AfterLookupFromDept(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loTempResult = (GSL00700DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                _viewModel.ReportParam.CFROM_DEPT_CODE = loTempResult.CDEPT_CODE;
                _viewModel.ReportParam.CFROM_DEPT_NAME = loTempResult.CDEPT_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task<bool> _validateDataBeforePrint()
        {
            var loEx = new R_Exception();
            var loReturn = true;
            try
            {
                if (_viewModel.ReportParam.CTRANS_CODE == null ||
                    _viewModel.ReportParam.CTRANS_CODE.Trim().Length <= 0)
                {
                    var leMsg = await R_MessageBox.Show("Warning", "Please choose Transaction Code",
                        R_eMessageBoxButtonType.OK);
                    await ComboTransCode.FocusAsync();
                    loReturn = false;
                }
                
                _viewModel.ReportParam.CREPORT_TYPE = _localizer["BASED_ON_TRANS_CODE"];
                _viewModel.ReportParam.CCURRENCY_TYPE_NAME = _viewModel.RadioCurrencyType.Find(x => x.Key == _viewModel.ReportParam.CCURRENCY_TYPE).Value;
                _viewModel.ReportParam.CTRANSACTION_NAME = _viewModel.TransCodeList?.Find(x => x.CTRANS_CODE == _viewModel.ReportParam.CTRANS_CODE).CTRANSACTION_NAME;
                if (_viewModel.ReportParam.CPERIOD_TYPE == "P")
                {
                    _viewModel.ReportParam.CFROM_PERIOD = _viewModel.YearPeriod + _viewModel.FromPeriod + _viewModel.SuffixPeriod;
                    _viewModel.ReportParam.CTO_PERIOD = _viewModel.YearPeriod + _viewModel.ToPeriod + _viewModel.SuffixPeriod;
                }
                else
                {
                    _viewModel.ReportParam.CFROM_PERIOD = _viewModel.DateFrom?.ToString("yyyyMMdd");
                    _viewModel.ReportParam.CTO_PERIOD = _viewModel.DateTo?.ToString("yyyyMMdd");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }

        private async Task OnLostFocusLookupToDept()
        {
            var loEx = new R_Exception();

            var loLookupViewModel = new LookupGSL00700ViewModel();
            try
            {
                if (_viewModel.ReportParam.CTO_DEPT_CODE == null ||
                    _viewModel.ReportParam.CTO_DEPT_CODE.Trim().Length <= 0)
                {
                    _viewModel.ReportParam.CTO_DEPT_NAME = "";
                    return;
                }

                var param = new GSL00700ParameterDTO
                {
                    CSEARCH_TEXT = _viewModel.ReportParam.CTO_DEPT_CODE
                };

                GSL00700DTO loResult = null;

                loResult = await loLookupViewModel.GetDepartment(param);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                        "_ErrLookup01"));
                    _viewModel.ReportParam.CTO_DEPT_CODE = "";
                    _viewModel.ReportParam.CTO_DEPT_NAME = "";
                    goto EndBlock;
                }

                _viewModel.ReportParam.CTO_DEPT_CODE = loResult.CDEPT_CODE;
                _viewModel.ReportParam.CTO_DEPT_NAME = loResult.CDEPT_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            R_DisplayException(loEx);
        }

        private void BeforeLookupToDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(GSL00700);
            eventArgs.Parameter = new GSL00700ParameterDTO();
        }

        private void AfterLookupToDept(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loTempResult = (GSL00700DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                _viewModel.ReportParam.CTO_DEPT_CODE = loTempResult.CDEPT_CODE;
                _viewModel.ReportParam.CTO_DEPT_NAME = loTempResult.CDEPT_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnChangeYear(object eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetPeriodDTList(eventArgs.ToString());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        private void OnChangeByType(object eventArgs)
        {
            _viewModel.ChangeByType((string)eventArgs);
        }

        private void CheckPeriodFrom(object obj)
        {
            var lcData = (string)obj;
            if (_viewModel.FromPeriod == null) return;
            if (int.Parse(lcData) > int.Parse(_viewModel.ToPeriod))
            {
                _viewModel.ToPeriod = lcData;
            }
        }

        private void CheckPeriodTo(object obj)
        {
            var lcData = (string)obj;
            if (_viewModel.ToPeriod == null) return;
            if (int.Parse(lcData) < int.Parse(_viewModel.FromPeriod))
            {
                _viewModel.FromPeriod = lcData;
            }
        }
        
        private async Task OnClickPrint()
        {
            var loEx = new R_Exception();
            try
            {
                // if (_viewModel.ReportParam.CFROM_DEPT_CODE == null ||
                //     _viewModel.ReportParam.CFROM_DEPT_CODE.Trim().Length <= 0)
                // {
                //     var loMsg = await R_MessageBox.Show("Warning", "Please fill From Department",
                //         R_eMessageBoxButtonType.OK);
                //     await TextFromDept.FocusAsync();
                //     return;
                // }
                //
                // if (_viewModel.ReportParam.CTO_DEPT_CODE == null ||
                //     _viewModel.ReportParam.CTO_DEPT_CODE.Trim().Length <= 0)
                // {
                //     var loMsg = await R_MessageBox.Show("Warning", "Please fill To Department",
                //         R_eMessageBoxButtonType.OK);
                //     await TextToDept.FocusAsync();
                //     return;
                // }

                if (!await _validateDataBeforePrint()) return;

                var loParam = _viewModel.ReportParam;
                loParam.CCOMPANY_ID = _clientHelper.CompanyId;
                loParam.CUSER_ID = _clientHelper.UserId;
                loParam.CLANGUAGE_ID = _clientHelper.Culture.TwoLetterISOLanguageName;
                loParam.CREPORT_CULTURE = _clientHelper.ReportCulture;
                loParam.LIS_PRINT = true;
                loParam.CREPORT_FILENAME = "";
                loParam.CREPORT_FILETYPE = "";
                await _reportService.GetReport(
                    "R_DefaultServiceUrlGL",
                    "GL",
                    "rpt/GLR00100PrintBasedOnTransCode/ActivityReportBasedOnTransCodePost",
                    "rpt/GLR00100PrintBasedOnTransCode/ActivityReportBasedOnTransCodeGet",
                    loParam
                );
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        private async Task BeforeOpenPopupSaveAs(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                
                if (!await _validateDataBeforePrint()) return;

                var loParam = _viewModel.ReportParam;
                eventArgs.Parameter = loParam;
                eventArgs.PageTitle = _localizer["SAVE_AS"];
                eventArgs.TargetPageType = typeof(GLR00100PopupSaveAs);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        private void InstanceRefNoTab(R_InstantiateDockEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(GLR00100RefNo);
        }

        private void InstanceDateTab(R_InstantiateDockEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(GLR00100Date);
        }
    }
}