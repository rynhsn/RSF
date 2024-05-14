using GSM02500COMMON.DTOs.GSM02540;
using GSM02500COMMON.DTOs.GSM02541;
using GSM02500COMMON.DTOs.GSM02550;
using GSM02500MODEL.View_Model;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid.Columns;
using R_BlazorFrontEnd.Controls.Tab;
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
    public partial class GSM02550 : R_Page
    {
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private GSM02550ViewModel loViewModel = new GSM02550ViewModel();
        
        private R_ConductorGrid _conductorRef;

        private R_Grid<GSM02550DTO> _gridRef;

        private R_GridTextColumn UserPropertyIdRef;

        private R_GridTextColumn UserPropertyNameRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loViewModel.SelectedProperty.CPROPERTY_ID = (string)poParameter;

                await loViewModel.GetSelectedPropertyAsync();

                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02550DTO)eventArgs.Data;
                loViewModel.loUserPropertyDetail = loParam;
            }
        }

        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.GetUserPropertyTypeListStreamAsync();
                eventArgs.ListEntityResult = loViewModel.loUserPropertyList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM02550DTO loParam = new GSM02550DTO();

                loParam = R_FrontUtility.ConvertObjectToObject<GSM02550DTO>(eventArgs.Data);
                await loViewModel.GetUserPropertyAsync(loParam);

                eventArgs.Result = loViewModel.loUserPropertyDetail;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private void Grid_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Add)
            {/*
                UserPropertyIdRef.FocusAsync();*/
            }
            else if (eventArgs.ConductorMode == R_eConductorMode.Edit)
            {/*
                UserPropertyNameRef.FocusAsync();*/
            }
        }

        private void Grid_SavingUsers(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                loViewModel.UserValidation((GSM02550DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.SaveUserPropertyAsync(
                    (GSM02550DTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loViewModel.loUserPropertyDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                GSM02550DTO loData = (GSM02550DTO)eventArgs.Data;
                await loViewModel.DeleteUserPropertyAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_BeforelookUpUser(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            eventArgs.Parameter = loViewModel.SelectedProperty.CPROPERTY_ID;
            eventArgs.TargetPageType = typeof(LookUpUser);
        }

        private void Grid_AfterlookUpUser(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loTempResult = (GetUserIdNameDTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loGetData = (GSM02550DTO)eventArgs.ColumnData;
            loGetData.CUSER_ID = loTempResult.CUSER_ID;
            loGetData.CUSER_NAME = loTempResult.CUSER_NAME;
        }
    }
}