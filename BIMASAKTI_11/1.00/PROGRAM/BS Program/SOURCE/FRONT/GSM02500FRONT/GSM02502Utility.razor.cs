using BlazorClientHelper;
using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON.DTOs.GSM02502Utility;
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
    public partial class GSM02502Utility : R_Page, R_ITabPage
    {
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private GSM02502UtilityViewModel loUtilityViewModel = new GSM02502UtilityViewModel();

        private R_ConductorGrid _conductorUtilityRef;

        private R_Grid<GSM02502UtilityDTO> _gridUtilityRef;

        private UtilityTabParameterDTO loUtilityTabParameter = new UtilityTabParameterDTO();


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
                GSM02502UtilityDTO loData = (GSM02502UtilityDTO)eventArgs.Data;

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
                        Table_Name = "GSM_PROPERTY_UNIT_TYPE_CATEGORY_UTILITY",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loUtilityTabParameter.CSELECTED_PROPERTY_ID, loUtilityTabParameter.CSELECTED_UNIT_TYPE_CATEGORY_ID, loData.CUTILITY_TYPE)   
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
                        Table_Name = "GSM_PROPERTY_UNIT_TYPE_CATEGORY_UTILITY",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loUtilityTabParameter.CSELECTED_PROPERTY_ID, loUtilityTabParameter.CSELECTED_UNIT_TYPE_CATEGORY_ID, loData.CUTILITY_TYPE)
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
                loUtilityTabParameter = (UtilityTabParameterDTO)poParameter;
                loUtilityViewModel.SelectedProperty = loUtilityTabParameter.CSELECTED_PROPERTY_ID;
                loUtilityViewModel.SelectedUnitTypeCategory = loUtilityTabParameter.CSELECTED_UNIT_TYPE_CATEGORY_ID;
                await _gridUtilityRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task RefreshTabPageAsync(object poParam)
        {
            loUtilityTabParameter = (UtilityTabParameterDTO)poParam;
            loUtilityViewModel.SelectedProperty = loUtilityTabParameter.CSELECTED_PROPERTY_ID;
            loUtilityViewModel.SelectedUnitTypeCategory = loUtilityTabParameter.CSELECTED_UNIT_TYPE_CATEGORY_ID;
            await _gridUtilityRef.R_RefreshGrid(null);
        }

        private async Task Grid_R_SetOther(R_SetEventArgs eventArgs)
        {
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }

        #region Utility
        private async Task Grid_DisplayUtility(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02502UtilityDTO)eventArgs.Data;
                loUtilityViewModel.loUtility = loParam;
            }
        }

        private async Task Grid_R_ServiceGetUtilityListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loUtilityViewModel.GetUtilityListStreamAsync();
                eventArgs.ListEntityResult = loUtilityViewModel.loUtilityList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetRecordUtility(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM02502UtilityDTO loParam = new GSM02502UtilityDTO();

                loParam = R_FrontUtility.ConvertObjectToObject<GSM02502UtilityDTO>(eventArgs.Data);
                await loUtilityViewModel.GetUtilityAsync(loParam);

                eventArgs.Result = loUtilityViewModel.loUtility;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceSaveUtility(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loUtilityViewModel.SaveUtilityAsync(
                    (GSM02502UtilityDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loUtilityViewModel.loUtility;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
/*
        private async Task Grid_ServiceDeleteUtility(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSM02502UtilityDTO loData = (GSM02502UtilityDTO)eventArgs.Data;
                await loUtilityViewModel.DeleteUtilityAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }*/
        #endregion
    }
}
