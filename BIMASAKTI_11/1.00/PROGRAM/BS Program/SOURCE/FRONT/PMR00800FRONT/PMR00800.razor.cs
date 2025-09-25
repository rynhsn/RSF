using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using PMR00800COMMON.DTO_s.Print;
using PMR00800FrontResources;
using PMR00800MODEL.View_Models;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System.Globalization;

namespace PMR00800FRONT
{
    public partial class PMR00800 : R_Page
    {
        private PMR00800ViewModel _viewModel = new PMR00800ViewModel();

        [Inject] IClientHelper _clientHelper { get; set; }

        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }

        [Inject] private R_IReport _reportService { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.InitialProcess();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        public void ComboboxPropertyValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel.ReportParam.CPROPERTY_ID = string.IsNullOrWhiteSpace(poParam) ? "" : poParam;
                _viewModel.ReportParam.CPROPERTY_NAME = _viewModel.PropertyList.Where(x => x.CPROPERTY_ID == poParam).FirstOrDefault().CPROPERTY_NAME;
                // reset building param
                _viewModel.ReportParam.CTO_BUILDING = "";
                _viewModel.ReportParam.CTO_BUILDING_NAME = "";
                _viewModel.ReportParam.CFROM_BUILDING = "";
                _viewModel.ReportParam.CFROM_BUILDING_NAME = "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        public async void OnChanged_NumericTextBoxPeriodYear()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.GetMonthDTListAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void BeforeOpen_lookupFromBuilding(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = new GSL02200ParameterDTO()
                { CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID };
                eventArgs.TargetPageType = typeof(GSL02200);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void BeforeOpen_lookupToBuilding(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = new GSL02200ParameterDTO()
                { CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID };
                eventArgs.TargetPageType = typeof(GSL02200);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void AfterOpen_lookupFromBuildingAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loTempResult = (GSL02200DTO)eventArgs.Result;
                _viewModel.ReportParam.CFROM_BUILDING = loTempResult.CBUILDING_ID ?? "";
                _viewModel.ReportParam.CFROM_BUILDING_NAME = loTempResult.CBUILDING_NAME ?? "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }

        private void AfterOpen_lookupToBuildingAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loTempResult = (GSL02200DTO)eventArgs.Result;
                _viewModel.ReportParam.CTO_BUILDING = loTempResult.CBUILDING_ID ?? "";
                _viewModel.ReportParam.CTO_BUILDING_NAME = loTempResult.CBUILDING_NAME ?? "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }

        private async Task OnLostFocus_LookupFromBuilding()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.ReportParam.CFROM_BUILDING))
                {

                    LookupGSL02200ViewModel loLookupViewModel = new LookupGSL02200ViewModel(); //use GSL's model

                    var loResult = await loLookupViewModel.GetBuilding(new GSL02200ParameterDTO()
                    {
                        CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
                        CSEARCH_TEXT = _viewModel.ReportParam.CFROM_BUILDING,
                    });

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                    }
                    _viewModel.ReportParam.CFROM_BUILDING = loResult?.CBUILDING_ID ?? "";
                    _viewModel.ReportParam.CFROM_BUILDING_NAME = loResult?.CBUILDING_NAME ?? "";
                }
                else
                {
                    _viewModel.ReportParam.CFROM_BUILDING_NAME = "";
                    _viewModel.ReportParam.CFROM_BUILDING = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }

        private async Task OnLostFocus_LookupToBuilding()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.ReportParam.CTO_BUILDING))
                {

                    LookupGSL02200ViewModel loLookupViewModel = new LookupGSL02200ViewModel(); //use GSL's model

                    var loResult = await loLookupViewModel.GetBuilding(new GSL02200ParameterDTO()
                    {
                        CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
                        CSEARCH_TEXT = _viewModel.ReportParam.CTO_BUILDING,
                    });

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                    }
                    _viewModel.ReportParam.CTO_BUILDING = loResult?.CBUILDING_ID ?? "";
                    _viewModel.ReportParam.CTO_BUILDING_NAME = loResult?.CBUILDING_NAME ?? "";
                }
                else
                {
                    _viewModel.ReportParam.CTO_BUILDING = "";
                    _viewModel.ReportParam.CTO_BUILDING_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }

        private async Task Btn_Print()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                Validation();
                await Generate_Param();
                await Generate_Report();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void Validation()
        {
            R_Exception loEx = new R_Exception();

            if (string.IsNullOrWhiteSpace(_viewModel.ReportParam.CPROPERTY_ID))
            {
                loEx.Add("", _localizer["_val_process1"]);
            }
            if (string.IsNullOrWhiteSpace(_viewModel.ReportParam.CTO_BUILDING) || string.IsNullOrEmpty(_viewModel.ReportParam.CFROM_BUILDING))
            {
                loEx.Add("", _localizer["_val_process2"]);
            }
            if (string.IsNullOrEmpty(_viewModel.SelectedPeriodMonth) || _viewModel.PickedPeriodYear == null)
            {
                loEx.Add("", _localizer["_val_process2"]);
            }

            if (loEx.HasError)
            {
                loEx.ThrowExceptionIfErrors();
            }

            loEx.ThrowExceptionIfErrors();

            //await Task.CompletedTask;
        }

        private async Task Generate_Param(PMR00800SaveAsDTO poParam = null)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel.ReportParam.CPERIOD = _viewModel.PickedPeriodYear.ToString() + _viewModel.SelectedPeriodMonth;
                _viewModel.ReportParam.CPERIOD_YYYY = _viewModel.PickedPeriodYear.ToString();
                _viewModel.ReportParam.CPERIOD_MM = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(_viewModel.SelectedPeriodMonth));
                _viewModel.ReportParam.CCOMPANY_ID = _clientHelper.CompanyId;
                _viewModel.ReportParam.CUSER_ID = _clientHelper.UserId;
                _viewModel.ReportParam.CREPORT_CULTURE = _clientHelper.ReportCulture.ToString();
                _viewModel.ReportParam.CLANG_ID = _clientHelper.Culture.TwoLetterISOLanguageName;
                if (poParam != null)
                {
                    _viewModel.ReportParam.LIS_PRINT = poParam.LIS_PRINT;
                    _viewModel.ReportParam.CREPORT_FILENAME = poParam.CREPORT_FILENAME;
                    _viewModel.ReportParam.CREPORT_FILETYPE = poParam.CREPORT_FILETYPE;
                }
                else
                {
                    _viewModel.ReportParam.LIS_PRINT = true;
                    _viewModel.ReportParam.CREPORT_FILENAME = "";
                    _viewModel.ReportParam.CREPORT_FILETYPE = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }

        private async Task Generate_Report()
        {

            R_Exception loEx = new R_Exception();
            try
            {
                await _reportService.GetReport(
                    "R_DefaultServiceUrlPM",
                    "PM",
                    "rpt/PMR00801Print/DownloadResultPrintPost",
                    "rpt/PMR00801Print/LeaseRevenueAnalysis_ReportListGet",
                    _viewModel.ReportParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //popup save as
        private void BeforeOpen_PopupSaveAsAsync(R_BeforeOpenPopupEventArgs eventArgs)
        {
            Validation();
            eventArgs.PageTitle = _localizer["_title_saveas"];
            eventArgs.TargetPageType = typeof(PMR00801);
        }

        private async Task AfterOpen_PopupSaveAs(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Success)
                {
                    var loReturn = R_FrontUtility.ConvertObjectToObject<PMR00800SaveAsDTO>(eventArgs.Result);
                    Validation();
                    await Generate_Param(loReturn);
                    await Generate_Report();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }

    }
}