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
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT02100FRONT
{
    public partial class PMT02110 : R_Page, R_ITabPage
    {
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<PMT02100FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private PMT02100ViewModel loViewModel = new PMT02100ViewModel();

        public GetPropertyListDTO loSelectedProperty = new GetPropertyListDTO();

        private PMT02100TabParameterDTO loTabParameter = new PMT02100TabParameterDTO();

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
                loViewModel.lcStatusCode = "02";
                loViewModel.loTabParameter = (PMT02100TabParameterDTO)poParameter;

                loViewModel.loProperty.CPROPERTY_ID = loViewModel.loTabParameter.CPROPERTY_ID;
                loViewModel.loSelectedType.CCODE = loViewModel.loTabParameter.CTYPE;

                await _gridHandoverBuildingRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task RefreshTabPageAsync(object poParam)
        {
            R_Exception loException = new R_Exception();

            try
            {
                loViewModel.loTabParameter = (PMT02100TabParameterDTO)poParam;

                loViewModel.loProperty.CPROPERTY_ID = loViewModel.loTabParameter.CPROPERTY_ID;
                loViewModel.loSelectedType.CCODE = loViewModel.loTabParameter.CTYPE;

                await _gridHandoverBuildingRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }


            R_DisplayException(loException);
        }

        //private void R_CheckBoxSelectValueChanged(R_CheckBoxSelectValueChangedEventArgs eventArgs)
        //{
        //    R_Exception loException = new R_Exception();
        //    try
        //    {
        //        PMT02100HandoverDTO loSelectedData = (PMT02100HandoverDTO)eventArgs.CurrentRow;
        //        eventArgs.Enabled = true;
        //        loSelectedData.LSELECTED = eventArgs.Value;
        //    }
        //    catch (Exception ex)
        //    {
        //        loException.Add(ex);
        //    }
        //    R_DisplayException(loException);
        //}

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
            }
        }

        #endregion

        #region Schedule Process
        private void Before_Open_Confirm_Schedule_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = loViewModel.loHandover;
            eventArgs.TargetPageType = typeof(PMT02111);
        }

        private async Task After_Open_Confirm_Schedule_Popup(R_AfterOpenPopupEventArgs eventArgs)
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
    }
}
