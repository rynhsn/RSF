using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM02500MODEL.View_Model;
using GSM02500COMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSCOMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02501;
using R_BlazorFrontEnd.Helpers;
using GFF00900COMMON.DTOs;

namespace GSM02500FRONT
{
    public partial class GSM02501 : R_Page
    {
        private GSM02501ViewModel loViewModel = new();

        private R_Conductor _conductorRef;

        private R_Grid<GSM02501PropertyDTO> _gridRef;

        private string loLabel = "Active";

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02501DetailDTO)eventArgs.Data;
                loViewModel.loPropertyDetail = loParam;
                loViewModel.loPropertyDetail.CPROPERTY_NAME = loParam.CPROPERTY_NAME;
                loViewModel.SelectedActiveInactiveLACTIVE = loParam.LACTIVE;
                if (loParam.LACTIVE)
                {
                    loLabel = "Inactive";
                    loViewModel.SelectedActiveInactiveLACTIVE = false;
                }
                else
                {
                    loLabel = "Active";
                    loViewModel.SelectedActiveInactiveLACTIVE = true;
                }
            }
        }

        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.GetPropertyListStreamAsync();
                eventArgs.ListEntityResult = loViewModel.loPropertyList;
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
                GSM02501DetailDTO loParam = new GSM02501DetailDTO();

                loParam = R_FrontUtility.ConvertObjectToObject<GSM02501DetailDTO>(eventArgs.Data);
                await loViewModel.GetPropertyAsync(loParam);

                eventArgs.Result = loViewModel.loPropertyDetail;
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
                await loViewModel.SavePropertyAsync(
                    (GSM02501DetailDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loViewModel.loPropertyDetail;
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
                GSM02501DetailDTO loData = (GSM02501DetailDTO)eventArgs.Data;
                await loViewModel.DeletePropertyAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_Before_Open_Popup_ActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM02502"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    await loViewModel.ActiveInactiveProcessAsync(); //Ganti jadi method ActiveInactive masing masing
                    await _gridRef.R_RefreshGrid(null);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "GSM02502" //Uabh Approval Code sesuai Spec masing masing
                    };
                    eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task R_After_Open_Popup_ActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Success == false)
                {
                    return;
                }
                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    await loViewModel.ActiveInactiveProcessAsync();
                    await _gridRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void R_After_Open_LookupSalesTaxId(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00100DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loGetData = (GSM02501DetailDTO)_conductorRef.R_GetCurrentData();
            loGetData.CSALES_TAX_ID = loTempResult.CTAX_ID;
            loGetData.CSALES_TAX_NAME = loTempResult.CTAX_NAME;
            loGetData.NTAX_PERCENTAGE = loTempResult.NTAX_PERCENTAGE;
        }

        private void R_Before_Open_LookupSalesTaxId(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00100ParameterDTO()
            {
                CCOMPANY_ID = "",
                CUSER_ID = ""
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00100);
        }

        private void R_After_Open_LookupCurrency(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00300DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loGetData = (GSM02501DetailDTO)_conductorRef.R_GetCurrentData();
            loGetData.CCURRENCY = loTempResult.CCURRENCY_CODE;
            loGetData.CCURRENCY_NAME = loTempResult.CCURRENCY_NAME;
        }

        private void R_Before_Open_LookupCurrency(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(GSL00300);
        }

        private void R_Before_OpenBuilding_Detail(R_BeforeOpenDetailEventArgs eventArgs)
        {
            eventArgs.Parameter = loViewModel.loPropertyDetail.CPROPERTY_ID;
            eventArgs.TargetPageType = typeof(GSM02510);
        }

        private void R_After_OpenBuilding_Detail()
        {

        }
    }
}
