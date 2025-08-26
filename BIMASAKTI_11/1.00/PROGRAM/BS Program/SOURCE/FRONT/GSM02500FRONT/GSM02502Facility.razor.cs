using BlazorClientHelper;
using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON.DTOs.GSM02502Facility;
using GSM02500MODEL.View_Model;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500FRONT
{
    public partial class GSM02502Facility : R_Page, R_ITabPage
    {
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private GSM02502FacilityViewModel loFacilityViewModel = new GSM02502FacilityViewModel();

        private R_ConductorGrid _conductorFacilityRef;

        private R_Grid<GSM02502FacilityDTO> _gridFacilityRef;

        private UtilityTabParameterDTO loFacilityTabParameter = new UtilityTabParameterDTO();


        [Inject] IClientHelper _clientHelper { get; set; }


        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_MODULE_NAME = "GS";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                GSM02502FacilityDTO loData = (GSM02502FacilityDTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "GSM02500",
                        Table_Name = "GSM_PROPERTY_UNIT_TYPE_CATEGORY_FACILITY",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loFacilityTabParameter.CSELECTED_PROPERTY_ID, loFacilityTabParameter.CSELECTED_UNIT_TYPE_CATEGORY_ID, loData.CFACILITY_TYPE)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "GSM02500",
                        Table_Name = "GSM_PROPERTY_UNIT_TYPE_CATEGORY_FACILITY",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loFacilityTabParameter.CSELECTED_PROPERTY_ID, loFacilityTabParameter.CSELECTED_UNIT_TYPE_CATEGORY_ID, loData.CFACILITY_TYPE)
                    };

                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }

                llRtn = loLockResult.IsSuccess;
                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                    throw loLockResult.Exception;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return llRtn;
        }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loFacilityTabParameter = (UtilityTabParameterDTO)poParameter;

                await loFacilityViewModel.GetFacilityTypeListStreamAsync();

                loFacilityViewModel.SelectedProperty = loFacilityTabParameter.CSELECTED_PROPERTY_ID;
                loFacilityViewModel.SelectedUnitTypeCategory = loFacilityTabParameter.CSELECTED_UNIT_TYPE_CATEGORY_ID;
                await _gridFacilityRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task RefreshTabPageAsync(object poParam)
        {
            loFacilityTabParameter = (UtilityTabParameterDTO)poParam;
            loFacilityViewModel.SelectedProperty = loFacilityTabParameter.CSELECTED_PROPERTY_ID;
            loFacilityViewModel.SelectedUnitTypeCategory = loFacilityTabParameter.CSELECTED_UNIT_TYPE_CATEGORY_ID;
            await _gridFacilityRef.R_RefreshGrid(null);
        }

        private async Task Grid_R_SetOther(R_SetEventArgs eventArgs)
        {
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }

        #region Facility
        private async Task Grid_DisplayFacility(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02502FacilityDTO)eventArgs.Data;
                loFacilityViewModel.loFacility = loParam;
            }
        }

        private async Task Grid_R_ServiceGetFacilityListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loFacilityViewModel.GetFacilityListStreamAsync();
                eventArgs.ListEntityResult = loFacilityViewModel.loFacilityList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetRecordFacility(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM02502FacilityDTO loParam = new GSM02502FacilityDTO();

                loParam = R_FrontUtility.ConvertObjectToObject<GSM02502FacilityDTO>(eventArgs.Data);
                await loFacilityViewModel.GetFacilityAsync(loParam);

                eventArgs.Result = loFacilityViewModel.loFacility;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceSaveFacility(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loFacilityViewModel.SaveFacilityAsync(
                    (GSM02502FacilityDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loFacilityViewModel.loFacility;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceDeleteFacility(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSM02502FacilityDTO loData = (GSM02502FacilityDTO)eventArgs.Data;
                await loFacilityViewModel.DeleteFacilityAsync(loData);
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
