using BlazorClientHelper;
using PMT04100COMMON;
using PMT04100FrontResources;
using PMT04100MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMModel.ViewModel.LML00600;
using Lookup_PMFRONT;

namespace PMT04100FRONT
{
    public partial class PMT04100 : R_Page
    {
        private PMT04100ViewModel _viewModel = new();
        private R_Conductor _conductorRef;
        private R_Grid<PMT04100DTO> _gridRef;

        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_ILocalizer<PMT04100FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetAllUniversalData();

                //Set Dept Code
                if (_viewModel.VAR_GS_PROPERTY_LIST.Count > 0)
                {
                    var lcPropertyId = _viewModel.VAR_GS_PROPERTY_LIST.FirstOrDefault().CPROPERTY_ID;
                    await PropertyCombobox_ValueChange(lcPropertyId);
                }

                //Set Journal Period
                if (!string.IsNullOrWhiteSpace(_viewModel.VAR_PM_SYSTEM_PARAM.CSOFT_PERIOD_YY))
                    _viewModel.JournalPeriodYear = int.Parse(_viewModel.VAR_PM_SYSTEM_PARAM.CSOFT_PERIOD_YY);

                _viewModel.JournalPeriodMonth = _viewModel.VAR_PM_SYSTEM_PARAM.CSOFT_PERIOD_MM;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        public async Task OnclickSearch()
        {
            var loEx = new R_Exception();
            bool loValidate = false;

            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.JornalParam.CPROPERTY_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT04100FrontResources.Resources_Dummy_Class),
                        "V01"));
                    loValidate = true;
                }

                if (string.IsNullOrEmpty(_viewModel.SearchText))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT04100FrontResources.Resources_Dummy_Class),
                        "N02"));
                    loValidate = true;
                }
                else
                {
                    if (_viewModel.SearchText.Length < 3)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT04100FrontResources.Resources_Dummy_Class),
                        "N03"));
                        loValidate = true;
                    }
                }

                if (loValidate == false)
                {
                    _viewModel.JornalParam.CSEARCH_TEXT = _viewModel.SearchText;
                    await _gridRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task OnClickShowAll()
        {
            var loEx = new R_Exception();
            bool loValidate = false;

            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.JornalParam.CPROPERTY_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT04100FrontResources.Resources_Dummy_Class),
                        "V01"));
                    loValidate = true;
                }

                if (loValidate == false)
                {
                    _viewModel.SearchText = "";
                    _viewModel.JornalParam.CSEARCH_TEXT = "";
                    await _gridRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task PropertyCombobox_ValueChange(string pcPropertyId)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.JornalParam.CPROPERTY_ID = pcPropertyId;
                await _viewModel.GetPMSystemParam(pcPropertyId);
                if (_gridRef.DataSource.Count > 0)
                {
                    _gridRef.DataSource.Clear();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }

        #region JournalGrid
        private async Task JournalGrid_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetJournalList();
                eventArgs.ListEntityResult = _viewModel.JournalGrid;
                if (_viewModel.JournalGrid.Count <= 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT04100FrontResources.Resources_Dummy_Class),
                        "N01"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private void JournalGrid_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }
        private async Task JournalGrid_Display(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = (PMT04100DTO)eventArgs.Data;
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    await Task.CompletedTask;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region lookup Dept
        private async Task DeptCode_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.JornalParam.CDEPT_CODE) == false)
                {
                    GSL00710ParameterDTO loParam = new GSL00710ParameterDTO() { CSEARCH_TEXT = _viewModel.JornalParam.CDEPT_CODE, CPROPERTY_ID = _viewModel.JornalParam.CPROPERTY_ID };

                    LookupGSL00710ViewModel loLookupViewModel = new LookupGSL00710ViewModel();

                    var loResult = await loLookupViewModel.GetDepartmentProperty(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.JornalParam.CDEPT_NAME = "";
                        goto EndBlock;
                    }
                    _viewModel.JornalParam.CDEPT_CODE = loResult.CDEPT_CODE;
                    _viewModel.JornalParam.CDEPT_NAME = loResult.CDEPT_NAME;
                }
                else
                {
                    _viewModel.JornalParam.CDEPT_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void Before_Open_lookupDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00710ParameterDTO
            {
                CPROPERTY_ID = _viewModel.JornalParam.CPROPERTY_ID
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00710);
        }
        private void After_Open_lookupDept(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00710DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _viewModel.JornalParam.CDEPT_CODE = loTempResult.CDEPT_CODE;
            _viewModel.JornalParam.CDEPT_NAME = loTempResult.CDEPT_NAME;
        }
        #endregion

        #region lookup Customer
        private async Task CustomerCode_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.JornalParam.CCUSTOMER_ID) == false)
                {
                    LML00600ParameterDTO loParam = new LML00600ParameterDTO() { CSEARCH_TEXT = _viewModel.JornalParam.CCUSTOMER_ID, CPROPERTY_ID = _viewModel.JornalParam.CPROPERTY_ID };

                    LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel();

                    var loResult = await loLookupViewModel.GetTenant(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.JornalParam.CCUSTOMER_NAME = "";
                        _viewModel.JornalParam.CCUSTOMER_TYPE_NAME = "";
                        goto EndBlock;
                    }
                    _viewModel.JornalParam.CCUSTOMER_ID = loResult.CTENANT_ID;
                    _viewModel.JornalParam.CCUSTOMER_NAME = loResult.CTENANT_NAME;
                    _viewModel.JornalParam.CCUSTOMER_TYPE_NAME = loResult.CCUSTOMER_TYPE_NAME;
                }
                else
                {
                    _viewModel.JornalParam.CCUSTOMER_TYPE_NAME = "";
                    _viewModel.JornalParam.CCUSTOMER_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void Before_Open_lookupCustomer(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new LML00600ParameterDTO
            {
                CPROPERTY_ID = _viewModel.JornalParam.CPROPERTY_ID
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00600);
        }
        private void After_Open_lookupCustomer(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00600DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _viewModel.JornalParam.CCUSTOMER_ID = loTempResult.CTENANT_ID;
            _viewModel.JornalParam.CCUSTOMER_NAME = loTempResult.CTENANT_NAME;
            _viewModel.JornalParam.CCUSTOMER_TYPE_NAME = loTempResult.CCUSTOMER_TYPE_NAME;
        }
        #endregion

        #region Predefine Journal Entry
        private void Predef_JournalEntry(R_InstantiateDockEventArgs eventArgs)
        {
            var loData = (PMT04100DTO)_conductorRef.R_GetCurrentData();
            loData.CPROPERTY_ID = _viewModel.JornalParam.CPROPERTY_ID;

            eventArgs.TargetPageType = typeof(PMT04110);
            eventArgs.Parameter = loData;
        }
        private async Task AfterPredef_JournalEntry(R_AfterOpenPredefinedDockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                //await _gridRef.R_RefreshGrid(null);
                await _viewModel.GetAllUniversalData();
                await _viewModel.GetPMSystemParam(_viewModel.JornalParam.CPROPERTY_ID);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
    }
}
