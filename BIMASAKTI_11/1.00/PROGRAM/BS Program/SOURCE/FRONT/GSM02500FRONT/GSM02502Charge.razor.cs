using BlazorClientHelper;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON.DTOs.GSM02502Charge;
using GSM02500MODEL.View_Model;
using Lookup_PMCOMMON.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Popup;
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
    public partial class GSM02502Charge : R_Page
    {
        //Unit Type Category
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_PopupService PopupService { get; set; }
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private GSM02502ChargeViewModel loChargeViewModel = new GSM02502ChargeViewModel();

        private R_ConductorGrid _conductorChargeRef;

        private R_Grid<GSM02502ChargeDTO> _gridChargeRef;

        private string loUnitTypeCategoryLabel = "";

        private bool llSingleUnit = false;

        private bool IsTabUtilityOnCRUDMode = false;

        private bool IsGridUnitTypeCategoryEnabled = true;

        private bool _gridEnabled = true;

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
                GSM02502ChargeDTO loData = (GSM02502ChargeDTO)eventArgs.Data;

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
                        Table_Name = "GSM_PROPERTY_UNIT_TYPE_CATEGORY_CHARGE",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loChargeViewModel.loTabParameter.CPROPERTY_ID, loChargeViewModel.loTabParameter.CUNIT_TYPE_CATEGORY_ID, loData.CCHARGES_TYPE, loData.CCHARGES_ID)
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
                        Table_Name = "GSM_PROPERTY_UNIT_TYPE_CATEGORY_CHARGE",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loChargeViewModel.loTabParameter.CPROPERTY_ID, loChargeViewModel.loTabParameter.CUNIT_TYPE_CATEGORY_ID, loData.CCHARGES_TYPE, loData.CCHARGES_ID)
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
                //loUnitTypeCategoryLabel = _localizer["Activate"];
                loChargeViewModel.loTabParameter = (ChargeTabParameterDTO)poParameter;
                await loChargeViewModel.GetChargeComboBoxListStreamAsync();
                await _gridChargeRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Charge_SetOther(R_SetEventArgs eventArgs)
        {
            //_gridEnabled = (bool)eventArgs.Enable;
            //IsTabUtilityOnCRUDMode = !(bool)eventArgs.Enable;
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }


        private void Grid_R_Before_Open_ChargeId_Lookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            LML00400ParameterDTO loParam = new LML00400ParameterDTO()
            {
                CPROPERTY_ID = loChargeViewModel.loTabParameter.CPROPERTY_ID,
                CCHARGE_TYPE_ID = loChargeViewModel.Data.CCHARGES_TYPE,
                CCOMPANY_ID = _clientHelper.CompanyId,
                CUSER_ID = _clientHelper.UserId
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(Lookup_PMFRONT.LML00400);
        }

        private void Grid_R_After_Open_ChargeId_Lookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loTempResult = (LML00400DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loGetData = (GSM02502ChargeDTO)eventArgs.ColumnData;
            loGetData.CCHARGES_ID = loTempResult.CCHARGES_ID;
        }

        #region Charge
        private async Task Grid_DisplayCharge(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02502ChargeDTO)eventArgs.Data;
                loChargeViewModel.loCharge = loParam;
            }
        }

        private async Task Grid_AfterAddCharge(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                string loNewSequence = "";
                if (loChargeViewModel.loChargeList.Count() == 0)
                {
                    loNewSequence = "001";
                }
                else
                {
                    GSM02502ChargeDTO loLastData = loChargeViewModel.loChargeList.OrderBy(s => int.Parse(s.CSEQUENCE)).Last();
                    loNewSequence = (int.Parse(loLastData.CSEQUENCE) + 1).ToString("D3");
                }

                GSM02502ChargeDTO loData = (GSM02502ChargeDTO)eventArgs.Data;
                loData.CSEQUENCE = loNewSequence;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_R_ServiceGetChargeListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loChargeViewModel.GetChargeListStreamAsync();
                eventArgs.ListEntityResult = loChargeViewModel.loChargeList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetRecordCharge(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM02502ChargeDTO loParam = new GSM02502ChargeDTO();

                loParam = R_FrontUtility.ConvertObjectToObject<GSM02502ChargeDTO>(eventArgs.Data);
                await loChargeViewModel.GetChargeAsync(loParam);

                eventArgs.Result = loChargeViewModel.loCharge;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceSaveCharge(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loChargeViewModel.SaveChargeAsync(
                    (GSM02502ChargeDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loChargeViewModel.loCharge;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceDeleteCharge(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSM02502ChargeDTO loData = (GSM02502ChargeDTO)eventArgs.Data;
                await loChargeViewModel.DeleteChargeAsync(loData);
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
