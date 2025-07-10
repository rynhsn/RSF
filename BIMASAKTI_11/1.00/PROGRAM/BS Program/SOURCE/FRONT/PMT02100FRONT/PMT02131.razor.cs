using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PMT02100COMMON.DTOs.PMT02100;
using PMT02100COMMON.DTOs.PMT02130;
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
    public partial class PMT02131 : R_Page
    {
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<PMT02100FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private PMT02131ViewModel loViewModel = new PMT02131ViewModel();

        public GetPropertyListDTO loSelectedProperty = new GetPropertyListDTO();

        //private PMT02100TabParameterDTO loTabParameter = new PMT02100TabParameterDTO();

        private R_ConductorGrid _conductorHandoverUnitRef;

        private R_ConductorGrid _conductorHandoverUnitChecklistRef;

        private R_Grid<PMT02130HandoverUnitDTO> _gridHandoverUnitRef;

        private R_Grid<PMT02130HandoverUnitChecklistDTO> _gridHandoverUnitChecklistRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loViewModel.loHeader = (PMT02100HandoverDTO)poParameter;
                if (loViewModel.loHeader != null)
                {
                    await _gridHandoverUnitRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //private void R_CheckBoxSelectValueChanged(R_CheckBoxSelectValueChangedEventArgs eventArgs)
        //{
        //    R_Exception loException = new R_Exception();
        //    try
        //    {
        //        PMT02130HandoverUnitChecklistDTO loSelectedData = (PMT02130HandoverUnitChecklistDTO)eventArgs.CurrentRow;
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

        private async Task Grid_Handover_Unit_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.GetHandoverUnitListStreamAsync();
                if (loViewModel.loHandoverUnitList.Count == 0)
                {
                    _gridHandoverUnitChecklistRef.DataSource.Clear();
                    loViewModel.loHandoverUnit = new PMT02130HandoverUnitDTO();
                    loViewModel.loHandoverUnitChecklist = new PMT02130HandoverUnitChecklistDTO();
                }
                eventArgs.ListEntityResult = loViewModel.loHandoverUnitList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_Handover_Unit_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                loViewModel.loHandoverUnit = (PMT02130HandoverUnitDTO)eventArgs.Data;
                await _gridHandoverUnitChecklistRef.R_RefreshGrid(null);
            }
        }

        #endregion

        #region Handover

        private async Task Grid_Handover_Unit_Checklist_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.GetHandoverUnitChecklistListStreamAsync();
                eventArgs.ListEntityResult = loViewModel.loHandoverUnitChecklistList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_Handover_Unit_Checklist_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                loViewModel.loHandoverUnitChecklist = (PMT02130HandoverUnitChecklistDTO)eventArgs.Data;
            }
        }
        #endregion

        private async Task OnClose()
        {
            await this.Close(false, false);
        }

    }
}
