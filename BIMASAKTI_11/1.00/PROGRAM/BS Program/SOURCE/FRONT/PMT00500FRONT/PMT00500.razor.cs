 using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using PMT00500MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using PMT00500COMMON;
using R_BlazorFrontEnd.Helpers;
using PMT00500FrontResources;
using Lookup_GSModel.ViewModel;
using Lookup_PMModel.ViewModel.LML00600;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.PML01200;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Enums;
using System;
using System.Diagnostics.Tracing;
using System.Xml.Linq;
using PMT01300FRONT;
using PMT02500Front;

namespace PMT00500FRONT
{
    public partial class PMT00500 : R_Page
    {
        [Inject] private R_ILocalizer<PMT00500FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }

        private PMT00500ViewModel _viewModel = new PMT00500ViewModel();
        private R_Grid<PMT00500BuildingUnitDTO> _gridBuildingUnitListRef;
        private R_Grid<PMT00500BuildingUnitDTO> _gridSelectedBuildingUnitListRef;
        private R_ConductorGrid _conductorGridRef;


        #region Private 
        private R_TabStrip _TabUnitList;
        private R_TabStripTab _TabUnitListStrip;
        private R_TabStripTab _TabAgreementStrip;

        private R_TabPage _TabPageAgreement;
        private bool _pageCRUDmode = false;
        private bool _ViewTransaction = false;
        private PMT00500BuildingUnitDTO _LoBuildingUnitData = new PMT00500BuildingUnitDTO();
        private int _NotYetHO;
        private int _ConfirmedHO;
        private int _DoneHO;
        #endregion
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.GetInitialVar();
                if (_viewModel.PropertyList.Count > 0)
                {
                    var loPropertyId = _viewModel.PropertyList.FirstOrDefault().CPROPERTY_ID;
                    await PropertyDropdown_ValueChanged(loPropertyId);
                }
                _TabAgreementStrip.Enabled = _viewModel.PropertyList.Count > 0;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region Value Change
        private async Task PropertyDropdown_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel.PROPERTY_ID = string.IsNullOrWhiteSpace(poParam) ? "" : poParam;

                if (_TabUnitList.ActiveTab.Id == "UnitList")
                {
                    _viewModel.BuildingUnit.CBUILDING_ID = "";
                    _gridSelectedBuildingUnitListRef.DataSource.Clear();
                    _gridBuildingUnitListRef.DataSource.Clear();
                    _NotYetHO = 0;
                    _ConfirmedHO = 0;
                    _DoneHO = 0;
                }
                else if (_TabUnitList.ActiveTab.Id == "Agreement")
                {
                    var loPropertyData = _viewModel.PropertyList.FirstOrDefault(x => x.CPROPERTY_ID == _viewModel.PROPERTY_ID);
                    await _TabPageAgreement.InvokeRefreshTabPageAsync(loPropertyData);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region Building Unit
        private async Task Grid_Building_Unit_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.GetBuildingUnitList();
                _NotYetHO = _viewModel.BuildingUnitGrid.Where(x => x.CSTRATA_HO_STATUS == "01,02").Count();
                _ConfirmedHO = _viewModel.BuildingUnitGrid.Where(x => x.CSTRATA_HO_STATUS == "03").Count();
                _DoneHO = _viewModel.BuildingUnitGrid.Where(x => x.CSTRATA_HO_STATUS == "04").Count();

                eventArgs.ListEntityResult = _viewModel.BuildingUnitGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void Grid_Building_Unit_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }
        private void Grid_Building_Unit_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.Data != null)
            {
                _LoBuildingUnitData = (PMT00500BuildingUnitDTO)eventArgs.Data;
            }
        }
        private async Task Grid_Building_Unit_CheckBoxSelectValueChanged(R_CheckBoxSelectValueChangedEventArgs eventArgs)
        {
            var loCurrentRow = (PMT00500BuildingUnitDTO)eventArgs.CurrentRow;
            
            if (eventArgs.Value)
            {
                if (_viewModel.SelectedBuildingUnitGrid.Any(x => x.CUNIT_ID == loCurrentRow.CUNIT_ID) == false)
                    _viewModel.SelectedBuildingUnitGrid.Add(loCurrentRow);
            }
            else
            {
                var loData = _viewModel.SelectedBuildingUnitGrid.FirstOrDefault(x => x.CUNIT_ID == loCurrentRow.CUNIT_ID);
                _viewModel.SelectedBuildingUnitGrid.Remove(loData);
            }

            await _gridSelectedBuildingUnitListRef.R_RefreshGrid(null);
            eventArgs.Enabled = true;
        }
        #endregion

        #region  Selected Building Unit
        private async Task Grid_Selected_Building_Unit_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                eventArgs.ListEntityResult = _viewModel.SelectedBuildingUnitGrid;
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region TabPage
        private void BuildingTab_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loPropertyData = _viewModel.PropertyList.FirstOrDefault(x => x.CPROPERTY_ID == _viewModel.PROPERTY_ID);

                var loParam = R_FrontUtility.ConvertObjectToObject<PMT00500UnitAgreement>(loPropertyData);
                if (_ViewTransaction)
                {
                    loParam.LVIEW_TRANSACTION = _ViewTransaction;
                    loParam.CREC_ID = _gridBuildingUnitListRef.CurrentSelectedData.CSTRATA_REC_ID;
                    loParam.CREF_NO = _gridBuildingUnitListRef.CurrentSelectedData.CSTRATA_REF_NO;
                    loParam.CTRANS_CODE = _gridBuildingUnitListRef.CurrentSelectedData.CSTRATA_TRANS_CODE;
                    loParam.CDEPT_CODE = _gridBuildingUnitListRef.CurrentSelectedData.CSTRATA_DEPT_CODE;
                }

                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(PMT00502);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        private void BuildingTab_After_Open_TabPage(R_AfterOpenTabPageEventArgs eventArgs)
        {
            _ViewTransaction = false;
        }
        private void R_TabEventCallback(object poValue)
        {
            _pageCRUDmode = (bool)poValue;
            _TabUnitListStrip.Enabled = !_pageCRUDmode;
        }
        #endregion

        #region NewAgreement
        private void NewAgreement_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            bool llValidate = false;
            
            try
            {
                if (_viewModel.SelectedBuildingUnitGrid.Count <= 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "N13"));
                    llValidate = true;
                }
                else
                {
                    if (_viewModel.SelectedBuildingUnitGrid.Any(x => x.CSTRATA_STATUS == ContextConstant.VAR_SOLD_STS))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(PMT00500FrontResources.Resources_Dummy_Class),
                            "N09"));
                        llValidate = true;
                    }

                    if (_viewModel.SelectedBuildingUnitGrid.Count > 10)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(PMT00500FrontResources.Resources_Dummy_Class),
                            "N12"));
                        llValidate = true;
                    }
                }
                
                
                if (llValidate == false)
                {
                    var loData = R_FrontUtility.ConvertObjectToObject<PMT00500LOICallParameterDTO>(_viewModel.BuildingUnit);
                    var loPropertyData = _viewModel.PropertyList.FirstOrDefault(x => x.CPROPERTY_ID == _viewModel.PROPERTY_ID);
                    loData.CPROPERTY_ID = _viewModel.PROPERTY_ID;
                    loData.CCURRENCY_CODE = loPropertyData.CCURRENCY;
                    loData.CPROPERTY_ID = _viewModel.PROPERTY_ID;
                    loData.VAR_TRANS_MODE = "N";
                    loData.VAR_SELECTED_UNIT_LIST_DATA = _viewModel.SelectedBuildingUnitGrid.ToList();
                    loData.LPOP_UP_MODE = true;
                    loData.CALLER_ACTION = "ADD";
                    loData.BUTTON_FROM_TAB_UNIT_LIST = "NEW_OFFER";
                    
                    eventArgs.Parameter = loData;
                    eventArgs.TargetPageType = typeof(PMT00510);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);   
        }
        private async Task NewAgreement_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                _gridSelectedBuildingUnitListRef.DataSource.Clear();
                await _TabUnitList.SetActiveTabAsync("Agreement");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        
        #region MoverBuyer
        private void MoverBuyer_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            //var loPropertyData = _viewModel.PropertyList.FirstOrDefault(x => x.CPROPERTY_ID == _viewModel.PROPERTY_ID);
            //eventArgs.Parameter = loPropertyData;
            //eventArgs.TargetPageType = typeof(PMT00501);
        }
        private async Task MoverBuyer_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                _gridSelectedBuildingUnitListRef.DataSource.Clear();
                await _gridBuildingUnitListRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        
        #region ChangeOwner
        private void ChangeOwner_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            bool llValidate = false;
            
            try
            {
                if (_gridBuildingUnitListRef.CurrentSelectedData.CSTRATA_TRX_STATUS != "30")
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "N19"));
                    llValidate = true;
                }
                if (_gridBuildingUnitListRef.CurrentSelectedData.CSTRATA_TRANS_CODE != ContextConstant.VAR_TRANS_CODE && _gridBuildingUnitListRef.CurrentSelectedData.CSTRATA_STATUS == ContextConstant.VAR_SOLD_STS)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "N20"));
                    llValidate = true;
                }
                
                if (llValidate == false)
                {
                    var loData = R_FrontUtility.ConvertObjectToObject<PMT00500LOICallParameterDTO>(_gridBuildingUnitListRef.CurrentSelectedData);
                    var loPropertyData = _viewModel.PropertyList.FirstOrDefault(x => x.CPROPERTY_ID == _viewModel.PROPERTY_ID);
                    loData.CCURRENCY_CODE = loPropertyData.CCURRENCY;
                    loData.VAR_TRANS_MODE = "N";
                    loData.LPOP_UP_MODE = true;
                    loData.CALLER_ACTION = "ADD";
                    loData.BUTTON_FROM_TAB_UNIT_LIST = "CHANGE_OWNER";
                    loData.VAR_LINK_REF_NO = _gridBuildingUnitListRef.CurrentSelectedData.CSTRATA_REF_NO;
                    loData.VAR_LINK_DEPT_CODE = _gridBuildingUnitListRef.CurrentSelectedData.CSTRATA_DEPT_CODE;
                    loData.VAR_LINK_TRANS_CODE = _gridBuildingUnitListRef.CurrentSelectedData.CSTRATA_TRANS_CODE;
                    loData.VAR_AGRMT_ID = _gridBuildingUnitListRef.CurrentSelectedData.CSTRATA_REC_ID;

                    eventArgs.Parameter = loData;
                    eventArgs.TargetPageType = typeof(PMT00510);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);  
        }
        private async Task ChangeOwner_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                _gridSelectedBuildingUnitListRef.DataSource.Clear();
                await _gridBuildingUnitListRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        
        #region Lease
        private void Lease_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            bool llValidate = false;
            
            try
            {
                if (_gridBuildingUnitListRef.CurrentSelectedData.CLEASE_STATUS == ContextConstant.VAR_LEASE_STS && _gridBuildingUnitListRef.CurrentSelectedData.CLEASE_TRX_STATUS.Contains("30,80"))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "N14"));
                    llValidate = true;
                }

                if (_gridBuildingUnitListRef.CurrentSelectedData.CSTRATA_STATUS != ContextConstant.VAR_SOLD_STS || _gridBuildingUnitListRef.CurrentSelectedData.CUNIT_CATEGORY_ID == "01")
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "N15"));
                    llValidate = true;
                }

                if (llValidate == false)
                {
                    var loData = R_FrontUtility.ConvertObjectToObject<PMT00500LeaseAgreement>(_gridBuildingUnitListRef.CurrentSelectedData);
                    loData.CREF_NO = _gridBuildingUnitListRef.CurrentSelectedData.CSTRATA_REF_NO;
                    loData.CDEPT_CODE = _gridBuildingUnitListRef.CurrentSelectedData.CSTRATA_DEPT_CODE;
                    loData.CTRANS_CODE = _gridBuildingUnitListRef.CurrentSelectedData.CSTRATA_TRANS_CODE;
                    loData.CREC_ID = _gridBuildingUnitListRef.CurrentSelectedData.CSTRATA_REC_ID;

                    eventArgs.Parameter = loData;
                    eventArgs.TargetPageType = typeof(PMT00500Lease);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);   
        }
        private async Task Lease_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                _gridSelectedBuildingUnitListRef.DataSource.Clear();
                await _gridBuildingUnitListRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        
        #region CloseLease
        private void CloseLease_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            bool llValidate = false;
            
            try
            {
                if (_gridBuildingUnitListRef.CurrentSelectedData.CLEASE_STATUS != ContextConstant.VAR_LEASE_STS || int.Parse(_gridBuildingUnitListRef.CurrentSelectedData.CLEASE_TRX_STATUS) < 30)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "N16"));
                    llValidate = true;
                }
                if (_gridBuildingUnitListRef.CurrentSelectedData.CSTRATA_STATUS != ContextConstant.VAR_SOLD_STS && (_gridBuildingUnitListRef.CurrentSelectedData.CLEASE_STATUS == ContextConstant.VAR_LEASE_STS || int.Parse(_gridBuildingUnitListRef.CurrentSelectedData.CLEASE_TRX_STATUS) < 30))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "N17"));
                    llValidate = true;
                }
                if (_gridBuildingUnitListRef.CurrentSelectedData.CSTRATA_STATUS != ContextConstant.VAR_SOLD_STS || _gridBuildingUnitListRef.CurrentSelectedData.CUNIT_CATEGORY_ID == "01")
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "N17"));
                    llValidate = true;
                }
                
                if (llValidate == false)
                {
                    var loData = R_FrontUtility.ConvertObjectToObject<PMT00500LOICallParameterDTO>(_gridBuildingUnitListRef.CurrentSelectedData);
                    loData.CPROPERTY_ID = _viewModel.PROPERTY_ID;
                    loData.CREF_NO = _gridBuildingUnitListRef.CurrentSelectedData.CLEASE_REF_NO;
                    loData.CDEPT_CODE = _gridBuildingUnitListRef.CurrentSelectedData.CLEASE_DEPT_CODE;
                    loData.CTRANS_CODE = _gridBuildingUnitListRef.CurrentSelectedData.CLEASE_TRANS_CODE;
                    loData.CREC_ID = _gridBuildingUnitListRef.CurrentSelectedData.CLEASE_REC_ID;
                    loData.CALLER_ACTION = "VIEW";
                    loData.LPOP_UP_MODE = true;
                    loData.LCLOSE_ONLY = true;

                    eventArgs.Parameter = loData;
                    if (loData.CTRANS_CODE == "802030" || loData.CTRANS_CODE == "802031")
                    {
                        eventArgs.TargetPageType = typeof(PMT02500Agreement);
                    }
                    else if (loData.CTRANS_CODE == "802061")
                    {
                        eventArgs.TargetPageType = typeof(PMT01310);
                    } 
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);  
        }
        private async Task CloseLease_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                _gridSelectedBuildingUnitListRef.DataSource.Clear();
                await _gridBuildingUnitListRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region lookupBuilding
        private void BeforeOpen_lookupBuilding(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var param = new GSL02200ParameterDTO
                {
                    CPROPERTY_ID = _viewModel.PROPERTY_ID
                };
                eventArgs.Parameter = param;
                eventArgs.TargetPageType = typeof(GSL02200);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private async Task AfterOpen_lookupBuildingAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL02200DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                _NotYetHO = 0;
                _ConfirmedHO = 0;
                _DoneHO = 0;
                return;
            }

            _viewModel.BuildingUnit = R_FrontUtility.ConvertObjectToObject<PMT00500BuildingDTO>(loTempResult);
            await _gridBuildingUnitListRef.R_RefreshGrid(null);
        }
        private async Task OnLostFocus_LookupBuilding()
        {
            var loEx = new R_Exception();

            try
            {
                LookupGSL02200ViewModel loLookupViewModel = new LookupGSL02200ViewModel(); //use GSL's model
                if (string.IsNullOrWhiteSpace(_viewModel.BuildingUnit.CBUILDING_ID) == false)
                {
                    var loParam = new GSL02200ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CPROPERTY_ID = _viewModel.PROPERTY_ID,
                        CSEARCH_TEXT = _viewModel.BuildingUnit.CBUILDING_ID // property that bindded to search textbox
                    };

                    var loResult = await loLookupViewModel.GetBuilding(loParam); //retrive single record

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.BuildingUnit.CBUILDING_NAME = ""; //kosongin bind textbox name kalo gaada
                        _gridBuildingUnitListRef.DataSource.Clear();
                        _NotYetHO = 0;
                        _ConfirmedHO = 0;
                        _DoneHO = 0;
                    }
                    else
                    {
                        _viewModel.BuildingUnit = R_FrontUtility.ConvertObjectToObject<PMT00500BuildingDTO>(loResult);
                        await _gridBuildingUnitListRef.R_RefreshGrid(null);
                    }
                }
                else
                {
                    _viewModel.BuildingUnit.CBUILDING_NAME = ""; //kosongin bind textbox name kalo gaada
                    _gridBuildingUnitListRef.DataSource.Clear();
                    _NotYetHO = 0;
                    _ConfirmedHO = 0;
                    _DoneHO = 0;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region View Transaction
        private async Task ViewTransaction_OnClick()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _ViewTransaction = true;
                await _TabUnitList.SetActiveTabAsync("Agreement");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion
    }
}
