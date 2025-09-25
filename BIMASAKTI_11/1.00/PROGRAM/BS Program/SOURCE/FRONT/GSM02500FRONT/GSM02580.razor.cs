using GSM02500COMMON.DTOs.GSM02580;
using GSM02500MODEL.View_Model;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid.Columns;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GSM02500FRONT
{
    public partial class GSM02580 : R_Page
    {
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private GSM02580ViewModel loViewModel = new GSM02580ViewModel();

        private R_Conductor _conductorRef;

        private bool IsButtonAddEnabled = false;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.SelectedProperty.CPROPERTY_ID = (string)poParameter;

                await loViewModel.GetSelectedPropertyAsync();
                await loViewModel.GetOperationalHourAsync();
                if (loViewModel.loOperationalHourDetail != null)
                {
                    await _conductorRef.R_GetEntity(null);
                }
                else
                {
                    IsButtonAddEnabled = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void OperationalHour_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02580DTO)eventArgs.Data;
                loViewModel.loOperationalHourDetail = loParam;
            }
        }

        private async Task OperationalHour_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                await loViewModel.GetOperationalHourAsync();

                IsButtonAddEnabled = loViewModel.loOperationalHourDetail == null;

                eventArgs.Result = loViewModel.loOperationalHourDetail;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private void OperationalHour_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Add)
            {
                IsButtonAddEnabled = false;
            }
        }

        private void OperationalHour_AfterDelete()
        {
            IsButtonAddEnabled = true;
        }

        private void OperationalHour_SavingOperationalHours(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                //loViewModel.OperationalHourValidation((GSM02580DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task OperationalHour_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.SaveOperationalHourAsync(
                    (GSM02580DTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loViewModel.loOperationalHourDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OperationalHour_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSM02580DTO loData = (GSM02580DTO)eventArgs.Data;
                await loViewModel.DeleteOperationalHourAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
