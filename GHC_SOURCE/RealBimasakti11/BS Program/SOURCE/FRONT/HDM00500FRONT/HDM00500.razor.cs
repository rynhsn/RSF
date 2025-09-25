using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using HDM00500COMMON;
using HDM00500COMMON.DTO_s;
using HDM00500FrontResources;
using HDM00500MODEL.View_Model_s;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_LockingFront;

namespace HDM00500FRONT
{
    public partial class HDM00500 : R_Page
    {
        //vars
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }
        [Inject] private IClientHelper _clientHelper { get; set; }
        private HDM00500ViewModel _viewModel_Taskchecklist = new();
        private HDM00501ViewModel _viewModel_Checklist = new();
        private R_ConductorGrid _conTaskchecklist;
        private R_ConductorGrid _conChecklist;
        private R_Grid<TaskchecklistDTO> _gridTaskcheklistDTO;
        private R_Grid<ChecklistDTO> _gridCheklistDTO;
        private int _pageSizeTaskChecklist = 10;
        private int _pageSizeChecklist = 10;
        private bool _enableComboboxProperty = true;
        private bool _enableGridTaskchecklist = true;
        private bool _enableGridChecklist = true;
        private string _labelActiveInactive = "";

        //methods
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new();
            try
            {
                await _viewModel_Taskchecklist.GetList_PropertyAsync();
                if (_viewModel_Taskchecklist._propertyList.Count > 0)
                {
                    _viewModel_Checklist._propertyId = _viewModel_Taskchecklist._propertyId;
                    await _gridTaskcheklistDTO.R_RefreshGrid(null);
                    var test = "ASD";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        protected override async Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (TaskchecklistDTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: Taskchecklist_ContextConstant.DEFAULT_MODULE,
                plSendWithContext: true,
                plSendWithToken: true,
                pcHttpClientName: Taskchecklist_ContextConstant.DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = Taskchecklist_ContextConstant.PROGRAM_ID,
                        Table_Name = Taskchecklist_ContextConstant.TABLE_NAME,
                        Key_Value = string.Join("|", _clientHelper.CompanyId, _viewModel_Taskchecklist._propertyId, loData.CTASK_CHECKLIST_ID)
                    };


                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = Taskchecklist_ContextConstant.PROGRAM_ID,
                        Table_Name = Taskchecklist_ContextConstant.TABLE_NAME,
                        Key_Value = string.Join("|", _clientHelper.CompanyId, _viewModel_Taskchecklist._propertyId, loData.CTASK_CHECKLIST_ID)
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
        private async Task ComboboxProperty_ValueChangedAsync(string poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel_Taskchecklist._propertyId = poParam;
                _viewModel_Checklist._propertyId = poParam;
                _viewModel_Checklist._taskChecklistID = "";
                await _gridTaskcheklistDTO.R_RefreshGrid(null);
                await _gridCheklistDTO.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        //events - TaskChecklist
        private async Task Taskchecklist_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel_Taskchecklist.GetList_TaskchecklistAsync();
                eventArgs.ListEntityResult = _viewModel_Taskchecklist._taskchecklist_list;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }
        private async Task Taskchecklist_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel_Taskchecklist.GetRecord_TaskchecklistAsync(R_FrontUtility.ConvertObjectToObject<TaskchecklistDTO>(eventArgs.Data));
                eventArgs.Result = _viewModel_Taskchecklist._taskchecklist_record;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task Taskchecklist_RDisplay(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Normal)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<TaskchecklistDTO>(eventArgs.Data);
                    _viewModel_Checklist._taskChecklistID = loParam.CTASK_CHECKLIST_ID ?? "";
                    _labelActiveInactive = (bool)loParam.LACTIVE ? _localizer["_btn_inactive"] : _localizer["_btn_inactive"];
                    await Task.Delay(300);
                    if (!string.IsNullOrWhiteSpace(_viewModel_Checklist._taskChecklistID))
                    {
                        await _gridCheklistDTO.R_RefreshGrid(null);
                    }
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task Taskchecklist_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TaskchecklistDTO>(eventArgs.Data);
                await _viewModel_Taskchecklist.DeleteRecord_TaskChecklistAsync(loParam);
                await _gridCheklistDTO.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private async Task Taskchecklist_AfterDeleteAsync()
        {
            R_Exception loEx = new();
            try
            {
                await _gridCheklistDTO.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void Taskchecklist_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = (TaskchecklistDTO)eventArgs.Data;
                loData.LACTIVE = true;
                loData.DUPDATE_DATE = DateTime.Now;
                loData.DCREATE_DATE = DateTime.Now;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task Taskchecklist_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TaskchecklistDTO>(eventArgs.Data);
                await _viewModel_Taskchecklist.SaveRecord_TaskChecklist(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel_Taskchecklist._taskchecklist_record;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }
        private void Taskchecklist_SetOther(R_SetEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                _enableComboboxProperty = eventArgs.Enable;
                _enableGridChecklist = eventArgs.Enable;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task Taskchecklist_BeforeOpenGridLookupAsync(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                switch (eventArgs.ColumnName)
                {
                    
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void Taskchecklist_AfterOpenGridLookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                if (eventArgs.Result == null)
                {
                    return;
                }
                switch (eventArgs.ColumnName)
                {
                    
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private async Task Taskchecklist_CellLostFocused(R_CellLostFocusedEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = eventArgs.CurrentRow as TaskchecklistDTO;
                switch (eventArgs.ColumnName)
                {
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }
        //events - Checklist
        private async Task Checklist_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel_Checklist.GetList_Checklist();
                eventArgs.ListEntityResult = _viewModel_Checklist._checklist_list;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }
        private async Task Checklist_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel_Checklist.GetRecord_ChecklistAsync(R_FrontUtility.ConvertObjectToObject<ChecklistDTO>(eventArgs.Data));
                eventArgs.Result = _viewModel_Checklist._checklist_record;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private void Checklist_SetOther(R_SetEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                _enableComboboxProperty = eventArgs.Enable;
                _enableGridTaskchecklist = eventArgs.Enable;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private void Checklist_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = (ChecklistDTO)eventArgs.Data;
                loData.DUPDATE_DATE = DateTime.Now;
                loData.DCREATE_DATE = DateTime.Now;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private void Checklist_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (ChecklistDTO)eventArgs.Data;
                if (string.IsNullOrWhiteSpace(loData.CCHECKLIST_CODE))
                {
                    loEx.Add("", _localizer["_val_checklist1"]);
                }
                if (string.IsNullOrWhiteSpace(loData.CCHECKLIST_NAME))
                {
                    loEx.Add("", _localizer["_val_checklist2"]);
                }
                if (string.IsNullOrWhiteSpace(loData.CDESCRIPTION))
                {
                    loEx.Add("", _localizer["_val_checklist3"]);
                }
                if (loEx.HasError)
                {
                    eventArgs.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }
        private async Task Checklist_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<ChecklistDTO>(eventArgs.Data);
                await _viewModel_Checklist.SaveRecord_Checklist(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel_Checklist._checklist_record;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }
        private async Task Checklist_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel_Checklist.DeleteRecord_ChecklistAsync(R_FrontUtility.ConvertObjectToObject<ChecklistDTO>(eventArgs.Data));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }
        //activeinactive
        private async Task BtnActiveInactiveTaskchecklist_BeforeOpenPopupAsync(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TaskchecklistDTO>(_conTaskchecklist.R_GetCurrentData());
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "HDM00501"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    await _viewModel_Taskchecklist.ActiveInactive_TaskchecklistAsync(loParam); //Ganti jadi method ActiveInactive masing masing
                    await _gridTaskcheklistDTO.R_RefreshGrid(null);
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "HDM00501" //Uabh Approval Code sesuai Spec masing masing
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
        private async Task BtnActiveInactiveTaskchecklist_AfterOpenPopup(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            var loData = R_FrontUtility.ConvertObjectToObject<TaskchecklistDTO>(_conTaskchecklist.R_GetCurrentData());
            try
            {
                if (eventArgs.Success == false)
                {
                    return;
                }
                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    await _viewModel_Taskchecklist.ActiveInactive_TaskchecklistAsync(loData);
                    await _viewModel_Taskchecklist.GetRecord_TaskchecklistAsync(loData);
                    await _gridTaskcheklistDTO.R_RefreshGrid(null);
                    await _gridTaskcheklistDTO.R_SelectCurrentDataAsync(_viewModel_Taskchecklist._taskchecklist_record);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }

    }
}
