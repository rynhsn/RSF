using BlazorClientHelper;
using PMT02100COMMON.DTOs;
using PMT02100MODEL.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using R_BlazorFrontEnd.Interfaces;
using R_APICommonDTO;
using PMT02100COMMON.DTOs.PMT02100;
using PMT02100MODEL.FrontDTOs.PMT02100;
using PMT02100COMMON.DTOs.PMT02130;
using PMT02100COMMON.DTOs.PMT02120;

namespace PMT02100FRONT
{
    public partial class PMT02125 : R_Page
    {
        [Inject] private IClientHelper loClientHelper { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<PMT02100FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private PMT02125ViewModel loViewModel = new PMT02125ViewModel();

        private R_ConductorGrid _conductorGridUnitRef;

        private R_Grid<PMT02130HandoverUnitDTO> _gridUnitRef;

        private R_ConductorGrid _conductorGridUtilityRef;

        private R_Grid<PMT02120HandoverUtilityDTO> _gridUtilityRef;

        private List<PMT02125MonthDTO> loMonth = new List<PMT02125MonthDTO>();

        public void InitializeMonths()
        {
            loMonth = new List<PMT02125MonthDTO>()
            {
                new PMT02125MonthDTO() { VAR_MONTH_CODE = 1, VAR_MONTH_NAME = _localizer["_January"] },
                new PMT02125MonthDTO() { VAR_MONTH_CODE = 2, VAR_MONTH_NAME = _localizer["_February"] },
                new PMT02125MonthDTO() { VAR_MONTH_CODE = 3, VAR_MONTH_NAME = _localizer["_March"] },
                new PMT02125MonthDTO() { VAR_MONTH_CODE = 4, VAR_MONTH_NAME = _localizer["_April"] },
                new PMT02125MonthDTO() { VAR_MONTH_CODE = 5, VAR_MONTH_NAME = _localizer["_May"] },
                new PMT02125MonthDTO() { VAR_MONTH_CODE = 6, VAR_MONTH_NAME = _localizer["_June"] },
                new PMT02125MonthDTO() { VAR_MONTH_CODE = 7, VAR_MONTH_NAME = _localizer["_July"] },
                new PMT02125MonthDTO() { VAR_MONTH_CODE = 8, VAR_MONTH_NAME = _localizer["_August"] },
                new PMT02125MonthDTO() { VAR_MONTH_CODE = 9, VAR_MONTH_NAME = _localizer["_September"] },
                new PMT02125MonthDTO() { VAR_MONTH_CODE = 10, VAR_MONTH_NAME = _localizer["_October"] },
                new PMT02125MonthDTO() { VAR_MONTH_CODE = 11, VAR_MONTH_NAME = _localizer["_November"] },
                new PMT02125MonthDTO() { VAR_MONTH_CODE = 12, VAR_MONTH_NAME = _localizer["_December"] }
            };
        }
        public void StateChangeInvoke()
        {
            StateHasChanged();
        }

        public void ShowErrorInvoke(R_APIException poException)
        {
            var loEx = R_FrontUtility.R_ConvertFromAPIException(poException);
            this.R_DisplayException(loEx);
        }

        public async Task ShowSuccessInvoke()
        {
            await this.Close(true, true);
        }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.loHeader = (PMT02100HandoverDTO)poParameter;

                InitializeMonths();

                loViewModel.loHeader.DHO_ACTUAL_DATE = DateTime.Now;

                loViewModel.SelectedUserId = loClientHelper.UserId;
                loViewModel.SelectedCompanyId = loClientHelper.CompanyId;

                loViewModel.StateChangeAction = StateChangeInvoke;
                loViewModel.ShowErrorAction = ShowErrorInvoke;
                loViewModel.ShowSuccessAction = async () =>
                {
                    await ShowSuccessInvoke();
                };

                if (loViewModel.loHeader != null)
                {
                    await _gridUnitRef.R_RefreshGrid(null);
                    await _gridUtilityRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Unit
        private async Task Grid_Unit_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.GetHandoverUnitListStreamAsync();
                eventArgs.ListEntityResult = loViewModel.loHandoverUnitList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_Unit_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Result = (PMT02130HandoverUnitDTO)eventArgs.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_Unit_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                eventArgs.Result = (PMT02130HandoverUnitDTO)eventArgs.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
        #endregion

        #region Utility
        private async Task Grid_Utility_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.GetHandoverUtilityListStreamAsync();
                eventArgs.ListEntityResult = loViewModel.loHandoverUtilityList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        //private void Grid_Utility_Set_Edit(R_SetEditGridColumnEventArgs eventArgs)
        //{
        //    PMT02120HandoverUtilityDTO loData = (PMT02120HandoverUtilityDTO)eventArgs.Data;
        //    loData.ISTART_INV_PRD_MONTH = 
        //}
        private void Grid_Utility_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Result = (PMT02120HandoverUtilityDTO)eventArgs.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_Utility_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                eventArgs.Result = (PMT02120HandoverUtilityDTO)eventArgs.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        //private void Grid_Utility_RowRender(R_GridRowRenderEventArgs eventArgs)
        //{
        //    var loData = (PMT02120HandoverUtilityDTO)eventArgs.Data;

        //    if (!loData.N)
        //    {
        //        eventArgs.RowClass = "CustomFormatting";
        //    }
        //}


        //private void R_RowRenderContractor(R_GridRowRenderEventArgs eventArgs)
        //{
        //    var loData = (PMT02120HandoverUtilityDTO)eventArgs.Data;

        //    if (loData.CCHARGES_TYPE == "01" || loData.CCHARGES_TYPE == "02")
        //    {
        //        eventArgs.RowClass = "editableField";//"editableField";
        //    }
        //}

        private void Grid_Utility_SetEditGridColumn(R_SetEditGridColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loColumn1 = eventArgs.Columns.FirstOrDefault(x => x.FieldName == nameof(PMT02120HandoverUtilityDTO.NMETER_START));
                var loColumn2 = eventArgs.Columns.FirstOrDefault(x => x.FieldName == nameof(PMT02120HandoverUtilityDTO.NBLOCK1_START));
                var loColumn3 = eventArgs.Columns.FirstOrDefault(x => x.FieldName == nameof(PMT02120HandoverUtilityDTO.NBLOCK2_START));
                var loData = _gridUtilityRef.CurrentSelectedData;

                if (loColumn1 != null)
                {
                    loColumn1.Enabled = loData.CCHARGES_TYPE == "03" || loData.CCHARGES_TYPE == "04";
                }
                if (loColumn2 != null)
                {
                    loColumn2.Enabled = loData.CCHARGES_TYPE == "01" || loData.CCHARGES_TYPE == "02";
                }
                if (loColumn3 != null)
                {
                    loColumn3.Enabled = loData.CCHARGES_TYPE == "01" || loData.CCHARGES_TYPE == "02";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        private async Task OnCancel()
        {
            await this.Close(false, false);
        }

        private async Task OnHandover()
        {
            bool lCancel;
            R_Exception loEx = new R_Exception();
            try
            {
                lCancel = !loViewModel.loHeader.DHO_ACTUAL_DATE.HasValue;
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT02100FrontResources.Resources_Dummy_Class),
                        "V011"));
                }
                lCancel = loViewModel.loHandoverUnitList.Any(x => x.NACTUAL_AREA_SIZE == 0);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT02100FrontResources.Resources_Dummy_Class),
                        "V012"));
                }
                lCancel = loViewModel.loHandoverUtilityList.Any(x => x.ISTART_INV_PRD_YEAR == 0);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT02100FrontResources.Resources_Dummy_Class),
                        "V013"));
                }
                lCancel = loViewModel.loHandoverUtilityList.Any(x => x.ISTART_INV_PRD_MONTH == 0);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT02100FrontResources.Resources_Dummy_Class),
                        "V014"));
                }
                lCancel = loViewModel.loHandoverUtilityList.Where(y => y.CUTILITY_TYPE == "03" || y.CUTILITY_TYPE == "04").Any(x => x.NMETER_START < 0);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT02100FrontResources.Resources_Dummy_Class),
                        "V015"));
                }
                lCancel = loViewModel.loHandoverUtilityList.Where(y => y.CUTILITY_TYPE == "01" || y.CUTILITY_TYPE == "02").Any(x => x.NBLOCK1_START < 0);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT02100FrontResources.Resources_Dummy_Class),
                        "V016"));
                }
                lCancel = loViewModel.loHandoverUtilityList.Where(y => y.CUTILITY_TYPE == "01" || y.CUTILITY_TYPE == "02").Any(x => x.NBLOCK2_START < 0);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT02100FrontResources.Resources_Dummy_Class),
                        "V017"));
                }

                if (loEx.HasError)
                {
                    goto EndBlock;
                }

                await loViewModel.HandoverProcessAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        #region Row Render

        private void R_RowRenderUnit(R_GridRowRenderEventArgs eventArgs)
        {
            var loData = (PMT02130HandoverUnitDTO)eventArgs.Data;
            eventArgs.RowClass = "rowDefaultColor";
        }

        private void R_RowRenderUtility(R_GridRowRenderEventArgs eventArgs)
        {
            var loData = (PMT02120HandoverUtilityDTO)eventArgs.Data;
            eventArgs.RowClass = "rowDefaultColor";
        }
        
        private void Editable_Meter_CellRender(R_GridCellRenderEventArgs eventArgs)
        {
            var loData = (PMT02120HandoverUtilityDTO)eventArgs.Data;

            if (loData.CCHARGES_TYPE == "03" || loData.CCHARGES_TYPE == "04")
            {
                eventArgs.CellClass = "editableField";//"editableField";
            }
        }

        private void Editable_Block_Start_CellRender(R_GridCellRenderEventArgs eventArgs)
        {
            var loData = (PMT02120HandoverUtilityDTO)eventArgs.Data;

            if (loData.CCHARGES_TYPE == "01" || loData.CCHARGES_TYPE == "02")
            {
                eventArgs.CellClass = "editableField";//"editableField";
            }
        }

        private void Editable_Field_CellRender(R_GridCellRenderEventArgs eventArgs)
        {
            eventArgs.CellClass = "editableField";//"editableField";
        }

        #endregion
    }
}