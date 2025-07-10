using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PMT02100COMMON.DTOs.PMT02100;
using PMT02100MODEL.FrontDTOs.PMT02100;
using PMT02100MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT02100FRONT
{
    public partial class PMT02100 : R_Page
    {
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<PMT02100FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private PMT02100ViewModel loViewModel = new PMT02100ViewModel();

        public GetPropertyListDTO loSelectedProperty = new GetPropertyListDTO();

        //private PMT02100TabParameterDTO loTabParameter = new PMT02100TabParameterDTO();
        
        private R_TabStrip _TabStripRef;

        private R_TabPage _tabOpenRef;
        
        private R_TabPage _tabConfirmedRef;

        private R_TabPage _tabScheduledRef;

        private R_TabPage _tabHistoryRef;

        private R_ConductorGrid _conductorHandoverBuildingRef;

        private R_ConductorGrid _conductorHandoverRef;

        private R_Grid<PMT02100HandoverBuildingDTO> _gridHandoverBuildingRef;

        private R_Grid<PMT02100HandoverDTO> _gridHandoverRef;

        private bool IsTenantListExist = false;

        private bool _comboboxEnabled = true;

        private bool _pageTenantOnCRUDmode = false;

        private string lcSelectedBuilding = "";

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loViewModel.lcStatusCode = "01";
                await loViewModel.GetPropertyListStreamAsync();
                loViewModel.loSelectedType = loViewModel.loTypeList.FirstOrDefault();
                loViewModel.loTabParameter.CTYPE = loViewModel.loSelectedType.CCODE;

                if (loViewModel.loPropertyList.Count() > 0)
                {
                    loViewModel.loProperty.CPROPERTY_ID = loViewModel.loPropertyList.FirstOrDefault().CPROPERTY_ID;
                    //loViewModel.loTabParameter.CPROPERTY_ID = loViewModel.loProperty.CPROPERTY_ID;
                    await PropertyDropdown_ValueChanged(loViewModel.loProperty.CPROPERTY_ID);
                    await _gridHandoverBuildingRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_TabEventCallback(object poValue)
        {
            var loEx = new R_Exception();

            try
            {
                //PMT02100EventCallBackParameterDTO poParam = (PMT02100EventCallBackParameterDTO)poValue;
                //_comboboxEnabled = !poParam.CRUDMode;
                //_pageTenantOnCRUDmode = poParam.CRUDMode;
                //if (poParam.IsTenantChanging)
                //{
                //    await _gridTenantRef.R_RefreshGrid(null);
                //    if (!string.IsNullOrEmpty(poParam.CTENANT_ID))
                //    {
                //        PMM03501DTO loSelected = _gridTenantRef.DataSource.Where(x => x.CTENANT_ID == poParam.CTENANT_ID).FirstOrDefault();
                //        if (loSelected != null)
                //        {
                //            await _gridTenantRef.R_SetCurrentData(loSelected);
                //        }
                //    }

                //}
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

            //_pageSupplierOnCRUDmode = !(bool)poValue;
        }

        private async Task OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            if (eventArgs.Id == "List")
            {
                //await _gridTenantRef.R_RefreshGrid(null);
            }
        }

        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            eventArgs.Cancel = _pageTenantOnCRUDmode;
        }

        private async Task PropertyDropdown_ValueChanged(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                //loTabParameter.CPROPERTY_ID = poParam;

                //loTabParameter.CSELECTED_PROPERTY_NAME = loPropertyList.Where(x => loTabParameter.CSELECTED_PROPERTY_ID == x.CPROPERTY_ID).FirstOrDefault().CPROPERTY_NAME;

                //loTenantViewModel.loProperty.CPROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID;
                //loTenantViewModel.loProperty.CPROPERTY_NAME = loTabParameter.CSELECTED_PROPERTY_NAME;

                //loTabParameter.CSELECTED_PROPERTY_NAME = loTenantViewModel.loProperty.CPROPERTY_NAME;

                loViewModel.loProperty.CPROPERTY_ID = string.IsNullOrEmpty(poParam) ? "": poParam;
                loViewModel.loTabParameter.CPROPERTY_ID = string.IsNullOrEmpty(poParam) ? "" : poParam;

                if (_TabStripRef.ActiveTab.Id == "Open")
                {
                    await _gridHandoverBuildingRef.R_RefreshGrid(null);
                }
                else if (_TabStripRef.ActiveTab.Id == "Scheduled")
                {
                    await _tabScheduledRef.InvokeRefreshTabPageAsync(loViewModel.loTabParameter);
                }
                else if (_TabStripRef.ActiveTab.Id == "Confirmed")
                {
                    await _tabConfirmedRef.InvokeRefreshTabPageAsync(loViewModel.loTabParameter);
                }
                else if (_TabStripRef.ActiveTab.Id == "History")
                {
                    await _tabHistoryRef.InvokeRefreshTabPageAsync(loViewModel.loTabParameter);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task TypeFilterRadioButton_ValueChanged(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                loViewModel.loSelectedType.CCODE = poParam;
                loViewModel.loTabParameter.CTYPE = poParam;

                if (_TabStripRef.ActiveTab.Id == "Open")
                {
                    await _gridHandoverBuildingRef.R_RefreshGrid(null);
                }
                else if (_TabStripRef.ActiveTab.Id == "Scheduled")
                {
                    await _tabScheduledRef.InvokeRefreshTabPageAsync(loViewModel.loTabParameter);
                }
                else if (_TabStripRef.ActiveTab.Id == "Confirmed")
                {
                    await _tabConfirmedRef.InvokeRefreshTabPageAsync(loViewModel.loTabParameter);
                }
                else if (_TabStripRef.ActiveTab.Id == "History")
                {
                    await _tabHistoryRef.InvokeRefreshTabPageAsync(loViewModel.loTabParameter);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickRefreshButton()
        {
            if (_TabStripRef.ActiveTab.Id == "Open")
            {
                await _gridHandoverBuildingRef.R_RefreshGrid(null);
            }
            else if (_TabStripRef.ActiveTab.Id == "Scheduled")
            {
                await _tabScheduledRef.InvokeRefreshTabPageAsync(loViewModel.loTabParameter);
            }
            else if (_TabStripRef.ActiveTab.Id == "Confirmed")
            {
                await _tabConfirmedRef.InvokeRefreshTabPageAsync(loViewModel.loTabParameter);
            }
            else if (_TabStripRef.ActiveTab.Id == "History")
            {
                await _tabHistoryRef.InvokeRefreshTabPageAsync(loViewModel.loTabParameter);
            }
        }

        private void R_CheckBoxSelectValueChanged(R_CheckBoxSelectValueChangedEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                PMT02100HandoverDTO loSelectedData = (PMT02100HandoverDTO)eventArgs.CurrentRow;
                eventArgs.Enabled = true;
                loSelectedData.LSELECTED = eventArgs.Value;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            R_DisplayException(loException);
        }

        #region Handover Building

        private async Task Grid_Handover_Building_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.GetHandoverBuildingListStreamAsync();
                if (loViewModel.loHandoverBuildingList.Count == 0)
                {
                    _gridHandoverRef.DataSource.Clear();
                    loViewModel.loHandoverBuilding = new PMT02100HandoverBuildingDTO();
                    loViewModel.loHandover = new PMT02100HandoverDTO();
                }
                eventArgs.ListEntityResult = loViewModel.loHandoverBuildingList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_Handover_Building_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                loViewModel.loHandoverBuilding = (PMT02100HandoverBuildingDTO)eventArgs.Data;
                await _gridHandoverRef.R_RefreshGrid(null);
                lcSelectedBuilding = loViewModel.loHandoverBuilding.CBUILDING_ID;
            }
        }

        #endregion

        #region Handover

        private async Task Grid_Handover_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.GetHandoverListStreamAsync();
                eventArgs.ListEntityResult = loViewModel.loHandoverList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_Handover_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                loViewModel.loHandover = (PMT02100HandoverDTO)eventArgs.Data;
                //await _gridHandoverRef.R_RefreshGrid(null);
            }
        }

        #endregion

        #region Schedule Process
        private void Before_Open_Schedule_Process_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Parameter = new ScheduleProcessParameterDTO()
                {
                    loSelectedHandover = loViewModel.loHandoverList.Where(x => x.LSELECTED == true).ToList(),
                    CPROPERTY_ID = loViewModel.loProperty.CPROPERTY_ID
                };
                eventArgs.TargetPageType = typeof(PMT02101);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task After_Open_Schedule_Process_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if ((bool)eventArgs.Success && (bool)eventArgs.Result)
                {
                    await _gridHandoverBuildingRef.R_RefreshGrid(null);
                    if (loViewModel.loHandoverBuildingList.Count > 0)
                    {
                        PMT02100HandoverBuildingDTO loTemp = loViewModel.loHandoverBuildingList.Where(x => x.CBUILDING_ID == lcSelectedBuilding).FirstOrDefault();
                        if (loTemp != null)
                        {
                            await _gridHandoverBuildingRef.R_SetCurrentData(loTemp);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Tab
        private void Before_Open_Scheduled_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = loViewModel.loTabParameter;
            eventArgs.TargetPageType = typeof(PMT02110);
        }
        private void Before_Open_Confirmed_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = loViewModel.loTabParameter;
            eventArgs.TargetPageType = typeof(PMT02120);
        }
        private void Before_Open_History_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = loViewModel.loTabParameter;
            eventArgs.TargetPageType = typeof(PMT02130);
        }
        private async Task _General_After_Open_TabPage(R_AfterOpenTabPageEventArgs eventArgs)
        {
            if (eventArgs.Result == null)
            {
                return;
            }
            if ((bool)eventArgs.Result)
            {
                //await _gridTenantRef.R_RefreshGrid(null);
            }
        }
        #endregion
    }
}
