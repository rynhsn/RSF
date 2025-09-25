using PMM00200COMMON.DTO_s;
using PMM00200COMMON;
using PMM00200MODEL;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Interfaces;
using BlazorClientHelper;
using R_BlazorFrontEnd.Controls.Enums;
using R_LockingFront;

namespace PMM00200FRONT
{
    public partial class PMM00200 :R_Page
    {
        private PMM00200ViewModel _userParamViewModel = new();
        private R_Conductor _userParamConductorRef;
        private R_Grid<PMM00200GridDTO> _userParamGridRef;
        private string _labelActiveInactive = "ACTIVE/INACTIVE"; //button label
        private R_NumericTextBox<int> _numTextBoxUserLevel; //ref for Value textbox
        [Inject] private R_ILocalizer<PMM00200FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IClientHelper _clientHelper { get; set; }
        private int _pageSize = 10;
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (PMM00200DTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: ContextConstant.DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: ContextConstant.DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = ContextConstant.PROGRAM_ID,
                        Table_Name = ContextConstant.TABLE_NAME,
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CCODE)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = ContextConstant.PROGRAM_ID,
                        Table_Name = ContextConstant.TABLE_NAME,
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CCODE)
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
                await _userParamGridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task UserParam_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _userParamViewModel.GetUserParamList();
                eventArgs.ListEntityResult = _userParamViewModel._UserParamList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task UserParam_ServiceGetRecordAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM00200DTO>(eventArgs.Data);
                await _userParamViewModel.GetUserParamRecord(loParam);//getrecord
                _labelActiveInactive = _userParamViewModel._UserParam.LACTIVE ? _localizer["btn_Inactive"]: _localizer["btn_Active"]; //set label to button
                _userParamViewModel._Active = !_userParamViewModel._UserParam.LACTIVE; //set active 
                _userParamViewModel._Action = _labelActiveInactive.ToUpper(); //set action for context
                _userParamViewModel._CUserOperatorSign = _userParamViewModel._UserParam.CUSER_LEVEL_OPERATOR_SIGN; //for user operator sign
                eventArgs.Result = _userParamViewModel._UserParam;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void UserParam_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (PMM00200DTO)eventArgs.Data;
                if (loData.IUSER_LEVEL < 0)
                    loEx.Add("", "User level start from 0.");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.HasError)
                eventArgs.Cancel = true;
            loEx.ThrowExceptionIfErrors();
        }
        private void UserParam_Saving(R_SavingEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = (PMM00200DTO)eventArgs.Data;
                if (string.IsNullOrEmpty(loData.CVALUE)) //make sure cvalue not null while sending to back
                {
                    loData.CVALUE = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task UserParam_ServiceSaveAsync(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (PMM00200DTO)eventArgs.Data;
                loParam.CUSER_LEVEL_OPERATOR_SIGN = _userParamViewModel._CUserOperatorSign;
                await _userParamViewModel.SaveUserParam(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _userParamViewModel._UserParam;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void UserParam_ConvertToGridEntity(R_ConvertToGridEntityEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                eventArgs.GridData = R_FrontUtility.ConvertObjectToObject<PMM00200DTO>(eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task UserParam_SetEditAsync(R_SetEventArgs eventArgs)
        {
            //await _numTextBoxUserLevel.FocusAsync(); //make focus to textboxvalue
            //eventArgs.Cancel = true;
        }
        private async Task BtnActiveInactive()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loData = _userParamConductorRef.R_GetCurrentData() as PMM00200DTO; //get current data
                var loParam = R_FrontUtility.ConvertObjectToObject<ActiveInactiveParam>(loData);
                await _userParamViewModel.ActiveInactiveProcessAsync(loParam);//do activeinactive
                await _userParamViewModel.GetUserParamRecord(loParam);
                await _userParamConductorRef.R_SetCurrentData(_userParamViewModel._UserParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private bool _userParamGridEnabled = true;
        private void UserParam_SetOther(R_SetEventArgs eventArgs)
        {
            _userParamGridEnabled = eventArgs.Enable;
        }
        private async Task UserParam_DisplayAsync(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<PMM00200DTO>(eventArgs.Data);
                if (eventArgs.ConductorMode == R_eConductorMode.Edit)
                {
                    await _numTextBoxUserLevel.FocusAsync(); //make focus to textboxvalue
                }
                if (eventArgs.ConductorMode==R_eConductorMode.Normal)
                {
                    _labelActiveInactive = loData.LACTIVE ? _localizer["btn_Inactive"] : _localizer["btn_Active"];
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
