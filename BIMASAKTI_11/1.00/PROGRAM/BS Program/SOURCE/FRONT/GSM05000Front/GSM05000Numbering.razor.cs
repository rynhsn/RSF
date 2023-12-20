using GFF00900COMMON.DTOs;
using GSM05000Common.DTOs;
using GSM05000Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM05000Front
{
    public partial class GSM05000Numbering : R_Page
    {
        private GSM05000NumberingViewModel _GSM05000NumberingViewModel = new();
        private R_ConductorGrid _conductorRefNumbering;
        private R_Grid<GSM05000NumberingGridDTO> _gridRefNumbering;
        [Inject] public R_PopupService PopupService { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                // _GSM05000NumberingViewModel.HeaderEntity = (GSM05000NumberingHeaderDTO)poParameter;
                _GSM05000NumberingViewModel.TransactionCode = ((GSM05000NumberingHeaderDTO)poParameter).CTRANS_CODE;
                await _GSM05000NumberingViewModel.GetNumberingHeader();

                await _gridRefNumbering.R_RefreshGrid(null);
                // await _gridRefNumbering.AutoFitAllColumnsAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_GetListNumbering(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _GSM05000NumberingViewModel.GetNumberingList();
                eventArgs.ListEntityResult = _GSM05000NumberingViewModel.GridList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_GetRecordNumbering(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000NumberingGridDTO>(eventArgs.Data);
                await _GSM05000NumberingViewModel.GetEntityNumbering(loParam);
                eventArgs.Result = _GSM05000NumberingViewModel.Entity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_AfterAddNumbering(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM05000NumberingGridDTO)eventArgs.Data;
                _GSM05000NumberingViewModel.GeneratePeriod(loParam);
                loParam.DCREATE_DATE = DateTime.Now;
                loParam.DUPDATE_DATE = DateTime.Now;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_BeforeEditNumbering(R_BeforeEditEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            GFF00900ParameterDTO loParam = null;
            R_PopupResult loResult = null;

            try
            {
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE =
                    "GSM05001"; //Ubah Approval Code sesuai Spec masing masing
                await loValidateViewModel
                    .RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" &&
                    loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    eventArgs.Cancel = false;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    loParam = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "GSM05001" //Uabh Approval Code sesuai Spec masing masing
                    };
                    loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loParam);
                    
                    if (loResult.Success == false)
                    {
                        eventArgs.Cancel = true;
                        return;
                    }

                    eventArgs.Cancel = !(bool)loResult.Result;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceSaveNumbering(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000NumberingGridDTO>(eventArgs.Data);
                await _GSM05000NumberingViewModel.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _GSM05000NumberingViewModel.Entity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceAfterSaveNumbering(R_AfterSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _gridRefNumbering.R_RefreshGrid((GSM05000NumberingGridDTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceDeleteNumbering(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000NumberingGridDTO>(eventArgs.Data);
                await _GSM05000NumberingViewModel.DeleteEntity(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void BeforeLookupNumbering(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Parameter = new GSL00700ParameterDTO();
                eventArgs.TargetPageType = typeof(GSL00700);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void AfterLookupNumbering(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempResult = (GSL00700DTO)eventArgs.Result;
                var loGetData = (GSM05000NumberingGridDTO)eventArgs.ColumnData;
                if (loTempResult == null)
                    return;

                loGetData.CDEPT_CODE = loTempResult.CDEPT_CODE;
                loGetData.CDEPT_NAME = loTempResult.CDEPT_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}