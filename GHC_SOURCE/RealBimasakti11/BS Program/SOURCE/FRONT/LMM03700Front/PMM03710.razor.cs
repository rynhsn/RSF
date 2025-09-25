using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using PMM03700COMMON;
using PMM03700COMMON.DTO_s;
using PMM03700MODEL;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_LockingFront;

namespace PMM03700FRONT
{
    public partial class PMM03710 : R_ITabPage
    {
        private PMM03710ViewModel _viewModelTenantClass = new();//viewModel TenantClass
        private PMM03700ViewModel _viewModelTenantClassGrp = new();//viewModel TenantClass
        private R_ConductorGrid _conTenantClassRef; //conductor grid TenantClass tab 2
        private R_ConductorGrid _conTenantRef; //conductor grid Tenant tab 2
        private R_Grid<TenantClassificationGroupDTO> _gridTenantClassGrpRef; //gridref TenantClassGrp
        private R_Grid<TenantClassificationDTO> _gridTenantClassRef; //gridref TenantClass 
        private R_Grid<TenantDTO> _gridTenantRef; //gridref Tenant 
        private R_Popup R_PopupCheck;
        [Inject] private R_ILocalizer<PMM03700FRONTResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IClientHelper _clientHelper { get; set; }



        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                _viewModelTenantClass._propertyId = (string)poParameter; //getting parameter
                if (!string.IsNullOrEmpty(_viewModelTenantClass._propertyId) || !string.IsNullOrEmpty(_viewModelTenantClass._propertyId)) //check is property id exist or not,
                {
                    await _gridTenantClassGrpRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task RefreshTabPageAsync(object poParam)
        {
            R_Exception loEx = new();
            try
            {
                _viewModelTenantClass._propertyId = (string)poParam;
                await _gridTenantClassGrpRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        protected override async Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<TenantClassificationDTO>(eventArgs.Data);

                var loCls = new R_LockingServiceClient(pcModuleName: PMM03700ContextConstant.DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: PMM03700ContextConstant.DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = PMM03700ContextConstant.PROGRAM_ID,
                        Table_Name = PMM03700ContextConstant.TABLE_NAME_2,
                        Key_Value = string.Join("|", _clientHelper.CompanyId, _viewModelTenantClass._propertyId, loData.CTENANT_CLASSIFICATION_GROUP_ID, loData.CTENANT_CLASSIFICATION_ID)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = PMM03700ContextConstant.PROGRAM_ID,
                        Table_Name = PMM03700ContextConstant.TABLE_NAME_2,
                        Key_Value = string.Join("|", _clientHelper.CompanyId, _viewModelTenantClass._propertyId, loData.CTENANT_CLASSIFICATION_GROUP_ID, loData.CTENANT_CLASSIFICATION_ID)
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

        #region TenantClassGrp

        private async Task TenantClassGrp_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelTenantClass.GetTenantClassGroupList();
                eventArgs.ListEntityResult = _viewModelTenantClass._TenantClassGrpList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }
        private void TenantClassGrp_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                eventArgs.Result = eventArgs.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task TenantClassGrp_ServiceDisplay(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantClassificationDTO>(eventArgs.Data);
                _viewModelTenantClass._tenantClassificationGroupId = loParam.CTENANT_CLASSIFICATION_GROUP_ID;
                await _gridTenantClassRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                throw;
            }
        }

        #endregion

        #region TenantClass
        private async Task TenantClass_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModelTenantClass.GetTenantClassList();
                eventArgs.ListEntityResult = _viewModelTenantClass.TenantClassList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }
        private async Task TenantClass_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantClassificationDTO>(eventArgs.Data);
                await _viewModelTenantClass.GetTenantClassRecord(loParam);
                eventArgs.Result = _viewModelTenantClass.TenantClass;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task TenantClass_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantClassificationDTO>(eventArgs.Data);
                await _viewModelTenantClass.DeleteTenantClass(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private void TenantClass_Validation(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = eventArgs.Data as TenantClassificationDTO;
                if (string.IsNullOrWhiteSpace(loData.CTENANT_CLASSIFICATION_ID))
                {
                    loEx.Add("", _localizer["_val_tc1"]);
                }
                if (string.IsNullOrWhiteSpace(loData.CTENANT_CLASSIFICATION_NAME))
                {
                    loEx.Add("", _localizer["_val_tc2"]);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            eventArgs.Cancel = loEx.HasError;
            loEx.ThrowExceptionIfErrors();
        }
        private void TenantClass_Saving(R_SavingEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (TenantClassificationDTO)eventArgs.Data;
                loData.CTENANT_CLASSIFICATION_ID = string.IsNullOrWhiteSpace(loData.CTENANT_CLASSIFICATION_ID) ? "" : loData.CTENANT_CLASSIFICATION_ID;
                loData.CTENANT_CLASSIFICATION_NAME = string.IsNullOrWhiteSpace(loData.CTENANT_CLASSIFICATION_NAME) ? "" : loData.CTENANT_CLASSIFICATION_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private async Task TenantClass_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantClassificationDTO>(eventArgs.Data);
                await _viewModelTenantClass.SaveTenantClass(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModelTenantClass.TenantClass;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private async Task TenantClass_ServiceDisplay(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loEntity = R_FrontUtility.ConvertObjectToObject<TenantClassificationDTO>(eventArgs.Data);
                if (loEntity != null)
                {
                    _viewModelTenantClass._tenantClassificationId = loEntity.CTENANT_CLASSIFICATION_ID ?? "";
                    if (!string.IsNullOrWhiteSpace(_viewModelTenantClass._tenantClassificationId))
                    {
                        await _gridTenantRef.R_RefreshGrid(null);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private bool _isTenantClassOnCRUDmode = false;
        private async Task TenantClass_SetOtherAsync(R_SetEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                await Task.Delay(1);
                await InvokeTabEventCallbackAsync(eventArgs.Enable);
                _isTenantClassOnCRUDmode = eventArgs.Enable;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Tab2-TenantList
        private async Task Tenant_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelTenantClass.GetAssignedTenantList();
                eventArgs.ListEntityResult = _viewModelTenantClass.AssignedTenantList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private void Tenant_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Result = R_FrontUtility.ConvertObjectToObject<TenantDTO>(_gridTenantRef.GetCurrentData());
                if (eventArgs.Result == null)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region Tab2-Assign Tenant
        private void R_Before_Open_Popup_AssignTenant(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = (TenantClassificationDTO)_gridTenantClassRef.GetCurrentData();
            eventArgs.TargetPageType = typeof(PopupAssignTenantMover);
            eventArgs.PageTitle = _localizer["_pageTitleAssignTenant"];
        }
        private async Task R_After_Open_Popup_AssignTenantAsync(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                if (eventArgs.Result == null)
                {
                    return;
                }
                await _gridTenantRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Tab2-Move Tenant
        private void R_Before_Open_Popup_MoveTenant(R_BeforeOpenPopupEventArgs eventArgs)
        {

            var loParam = (TenantClassificationDTO)_gridTenantClassRef.GetCurrentData();
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(PopupMoveTenant);
            eventArgs.PageTitle = _localizer["_pageTitleMoveTenant"];
        }
        private async Task R_After_Open_Popup_MoveTenant(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                if (eventArgs.Result == null)
                {
                    return;
                }
                await _gridTenantRef.R_RefreshGrid(null);
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