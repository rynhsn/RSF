using GSM02500COMMON.DTOs.GSM02560;
using GSM02500MODEL.View_Model;
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
    public partial class GSM02560 : R_Page
    {
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private GSM02560ViewModel loViewModel = new GSM02560ViewModel();

        private R_ConductorGrid _conductorRef;

        private R_Grid<GSM02560DTO> _gridRef;

        private R_GridTextColumn DepartmentIdRef;

        private R_GridTextColumn DepartmentNameRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

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
                var loParam = (GSM02560DTO)eventArgs.Data;
                loViewModel.loDepartmentDetail = loParam;
            }
        }

        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.GetDepartmentListStreamAsync();
                eventArgs.ListEntityResult = loViewModel.loDepartmentList;
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
                GSM02560DTO loParam = new GSM02560DTO();

                loParam = R_FrontUtility.ConvertObjectToObject<GSM02560DTO>(eventArgs.Data);
                await loViewModel.GetDepartmentAsync(loParam);

                eventArgs.Result = loViewModel.loDepartmentDetail;
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
                DepartmentIdRef.FocusAsync();*/
            }
            else if (eventArgs.ConductorMode == R_eConductorMode.Edit)
            {/*
                DepartmentNameRef.FocusAsync();*/
            }
        }

        private void Grid_SavingDepartments(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                loViewModel.DepartmentValidation((GSM02560DTO)eventArgs.Data);
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
                await loViewModel.SaveDepartmentAsync(
                    (GSM02560DTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loViewModel.loDepartmentDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSM02560DTO loData = (GSM02560DTO)eventArgs.Data;
                await loViewModel.DeleteDepartmentAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_BeforeLookupDepartment(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            eventArgs.Parameter = loViewModel.SelectedProperty.CPROPERTY_ID;
            eventArgs.TargetPageType = typeof(LookupDepartment);
        }

        private void Grid_AfterLookupDepartment(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loTempResult = (GetDepartmentLookupListDTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loGetData = (GSM02560DTO)eventArgs.ColumnData;
            loGetData.CDEPT_CODE = loTempResult.CDEPT_CODE;
            loGetData.CDEPT_NAME = loTempResult.CDEPT_NAME;
        }
    }
}
