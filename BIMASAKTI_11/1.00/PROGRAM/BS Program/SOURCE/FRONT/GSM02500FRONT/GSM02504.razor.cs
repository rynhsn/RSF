using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid.Columns;
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
using GSM02500COMMON.DTOs.GSM02504;
using GSM02500COMMON.DTOs.GSM02503;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Interfaces;
using BlazorClientHelper;
using R_BlazorFrontEnd.Controls.Enums;
using R_LockingFront;

namespace GSM02500FRONT
{
    public partial class GSM02504 : R_Page
    {
        //Unit View
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        public GSM02504ViewModel loUnitViewViewModel = new();

        private R_ConductorGrid _conGridUnitViewRef;

        private R_Grid<GSM02504DTO> _gridUnitViewRef;

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
                GSM02504DTO loData = (GSM02504DTO)eventArgs.Data;

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
                        Table_Name = "GSM_PROPERTY_UNIT_VIEW",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loUnitViewViewModel.SelectedProperty.CPROPERTY_ID, loData.CUNIT_VIEW_ID)
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
                        Table_Name = "GSM_PROPERTY_UNIT_VIEW",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loUnitViewViewModel.SelectedProperty.CPROPERTY_ID, loData.CUNIT_VIEW_ID)
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
                loUnitViewViewModel.SelectedProperty.CPROPERTY_ID = (string)poParameter;
                await loUnitViewViewModel.GetSelectedPropertyAsync();
                await _gridUnitViewRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task UnitView_SetOther(R_SetEventArgs eventArgs)
        {
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }
        #region Unit View

        private async Task Grid_DisplayUnitView(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02504DTO)eventArgs.Data;
                //await loUnitViewViewModel.GetSelectedPropertyAsync();
            }
        }

        private async Task Grid_R_ServiceGetUnitViewListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loUnitViewViewModel.GetUnitViewListStreamAsync();
                eventArgs.ListEntityResult = loUnitViewViewModel.loUnitViewList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetUnitViewRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM02504DTO loParam = (GSM02504DTO)eventArgs.Data;
                await loUnitViewViewModel.GetUnitViewAsync(loParam);

                eventArgs.Result = loUnitViewViewModel.loUnitView;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceSaveUnitView(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loUnitViewViewModel.SaveUnitViewAsync(
                    (GSM02504DTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loUnitViewViewModel.loUnitView;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_SavingUnitView(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                loUnitViewViewModel.UnitViewValidation((GSM02504DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceDeleteUnitView(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSM02504DTO loData = (GSM02504DTO)eventArgs.Data;
                await loUnitViewViewModel.DeleteUnitViewAsync(loData);
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
