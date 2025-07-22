using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using GSM04000Common;
using GSM04000Model;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_TXCOMMON.DTOs.TXL00100;
using Lookup_TXFRONT;
using Lookup_TXModel.ViewModel.TXL00100;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System;
using System.Xml.Linq;
using Microsoft.AspNetCore.Components.Web;

namespace GSM04000Front
{
    public partial class GSM04000 : R_Page
    {
        private GSM04000ViewModel _deptViewModel = new GSM04000ViewModel();
        private R_Grid<DepartmentDTO> _gridDeptRef;

        private GSM04100ViewModel _deptUserViewModel = new GSM04100ViewModel();
        private R_ConductorGrid _conGridDeptRef;

        private R_Grid<UserDepartmentDTO> _gridDeptUserRef;
        private R_ConductorGrid _conGridDeptUserRef;

        private R_Conductor _conductorForPrint;

        [Inject] R_PopupService PopupService { get; set; }
        [Inject] IClientHelper _clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }
        [Inject] private R_ILocalizer<GSM04000FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private string _labelActiveInactive = "";

        private bool _enableBtnAssignUser;

        private bool _enableBtnActiveInactive = true;

        private bool _enableGridDeptUser = true;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await _gridDeptRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";

        private const string DEFAULT_MODULE_NAME = "GS";

        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (DepartmentDTO)eventArgs.Data;

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
                        Program_Id = "GSM04000",
                        Table_Name = "GSM_DEPARTMENT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CDEPT_CODE)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "GSM04000",
                        Table_Name = "GSM_DEPARTMENT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CDEPT_CODE)
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
            await Task.CompletedTask;
        }

        private async Task Dept_CustomLock(bool plLock)
        {
            var loEx = new R_Exception();
            R_LockingFrontResult loLockResult = null;
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<DepartmentDTO>(_conGridDeptRef.R_GetCurrentData());
                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: DEFAULT_HTTP_NAME);
                if (plLock) // Lock
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "GSM04000",
                        Table_Name = "GSM_DEPARTMENT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CDEPT_CODE)
                    };
                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else // Unlock
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "GSM04000",
                        Table_Name = "GSM_DEPARTMENT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CDEPT_CODE)
                    };
                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }

                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                {
                    loEx.Add(loLockResult.Exception);
                    //throw loLockResult.Exception;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region GridDept

        private async Task DeptGrid_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _deptViewModel.GetDepartmentList();
                eventArgs.ListEntityResult = _deptViewModel._DepartmentList;
                if (_deptViewModel._DepartmentList.Count == 0)
                {
                    _enableBtnActiveInactive = false;
                    _enableBtnAssignUser = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task DeptGrid_ServiceGetRecordAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (DepartmentDTO)eventArgs.Data;
                await _deptViewModel.GetDepartment(loParam);

                eventArgs.Result = _deptViewModel._Department;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task DeptGrid_ValidationAsync(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            GFF00900ParameterDTO loParam = null;
            R_PopupResult loResult = null;
            try
            {
                //get data
                var loData = (DepartmentDTO)eventArgs.Data;

                switch (_conGridDeptRef.R_ConductorMode)
                {
                    case R_eConductorMode.Add:

                        //Approval before saving
                        if (loData.LACTIVE == true)
                        {
                            var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                            loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM04001";
                            await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync();

                            if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER ==
                                "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault()
                                    .IAPPROVAL_MODE == 1)
                            {
                                eventArgs.Cancel = false;
                            }
                            else
                            {
                                loParam = new GFF00900ParameterDTO()
                                {
                                    Data = loValidateViewModel.loRspActivityValidityList,
                                    IAPPROVAL_CODE = "GSM04001"
                                };
                                loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loParam);
                                if (loResult.Success == false || (bool)loResult.Result == false)
                                {
                                    eventArgs.Cancel = true;
                                }
                            }
                        }

                        //~End approval
                        break;

                    //case validation when editmode
                    case R_eConductorMode.Edit:

                        //check when changing everyone then delete user
                        if (_deptViewModel._Department.LEVERYONE == false && loData.LEVERYONE == true)
                        {
                            await _deptViewModel.CheckIsUserDeptExistAsync();
                            if (_deptViewModel._isUserDeptExist)
                            {
                                var loConfirm = await R_MessageBox.Show("", _localizer["_msgConfirmation1"],
                                    R_eMessageBoxButtonType.OKCancel);
                                if (loConfirm == R_eMessageBoxResult.Cancel)
                                {
                                    eventArgs.Cancel = true;
                                }

                                await _deptViewModel.DeleteAssignedUserWhenChangeEveryone();
                            }
                        }
                        //

                        if (string.IsNullOrWhiteSpace(loData.CDEPT_CODE))
                        {
                            loEx.Add("", _localizer["_valSave1"]);
                        }

                        if (string.IsNullOrWhiteSpace(loData.CDEPT_NAME))
                        {
                            loEx.Add("", _localizer["_valSave2"]);
                        }

                        if (string.IsNullOrWhiteSpace(loData.CCENTER_CODE))
                        {
                            loEx.Add("", _localizer["_valSave3"]);
                        }

                        if (string.IsNullOrWhiteSpace(loData.CBRANCH_NAME))
                        {
                            loEx.Add("", _localizer["_valSave4"]);
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            if (loEx.HasError)
                eventArgs.Cancel = true;
            loEx.ThrowExceptionIfErrors();
        }

        private void DeptGrid_Saving(R_SavingEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = (DepartmentDTO)eventArgs.Data;
                loData.CMANAGER_NAME = loData.CMANAGER_CODE;
                loData.CDEPT_CODE = string.IsNullOrWhiteSpace(loData.CDEPT_CODE) ? "" : loData.CDEPT_CODE;
                loData.CDEPT_NAME = string.IsNullOrWhiteSpace(loData.CDEPT_NAME) ? "" : loData.CDEPT_NAME;
                loData.CCENTER_CODE = string.IsNullOrWhiteSpace(loData.CCENTER_CODE) ? "" : loData.CCENTER_CODE;
                loData.CMANAGER_NAME = string.IsNullOrWhiteSpace(loData.CMANAGER_NAME) ? "" : loData.CMANAGER_NAME;
                loData.CEMAIL1 = string.IsNullOrWhiteSpace(loData.CEMAIL1) ? "" : loData.CEMAIL1;
                loData.CEMAIL2 = string.IsNullOrWhiteSpace(loData.CEMAIL2) ? "" : loData.CEMAIL2;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void DeptGrid_BeforeDelete(R_BeforeDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = (DepartmentDTO)eventArgs.Data;
                loData.CMANAGER_NAME = loData.CMANAGER_CODE;
                loData.CMANAGER_NAME = string.IsNullOrWhiteSpace(loData.CMANAGER_NAME) ? "" : loData.CMANAGER_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task DeptGrid_ServiceSaveAsync(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _deptViewModel.SaveDepartment((DepartmentDTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _deptViewModel._Department;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task DeptGrid_ServiceDeleteAsync(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (DepartmentDTO)eventArgs.Data;
                await _deptViewModel.DeleteDepartment(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task DeptGrid_DisplayAsync(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<DepartmentDTO>(eventArgs.Data);
                switch (eventArgs.ConductorMode)
                {
                    case R_eConductorMode.Normal:
                        //set to viewmodel parent
                        _deptViewModel._departmentCode = loParam.CDEPT_CODE;
                        _deptViewModel._activeDept = loParam.LACTIVE;

                        if (loParam.LACTIVE)
                        {
                            _labelActiveInactive = _localizer["_lblInactive"];
                            _deptViewModel._activeDept = !loParam.LACTIVE;
                        }
                        else
                        {
                            _labelActiveInactive = _localizer["_lblActive"];
                            _deptViewModel._activeDept = !loParam.LACTIVE;
                        }

                        if (loParam.LEVERYONE == false && loParam.LACTIVE == true)
                        {
                            _enableBtnAssignUser = true;
                        }
                        else
                        {
                            _enableBtnAssignUser = false;
                        }

                        //set deptcode to view model child
                        _deptUserViewModel._DepartmentCode = loParam.CDEPT_CODE;
                        _deptUserViewModel._DepartmentName = loParam.CDEPT_NAME;

                        //refresh grid child
                        if (!string.IsNullOrWhiteSpace(_deptUserViewModel._DepartmentCode))
                        {
                            await _gridDeptUserRef.R_RefreshGrid(loParam);
                        }

                        break;

                    case R_eConductorMode.Edit:
                        if (loParam.LEVERYONE != true)
                        {
                            await R_MessageBox.Show("", _localizer["_msgConfirmation2"],
                                R_eMessageBoxButtonType.OKCancel);
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void DeptGrid_SetOther(R_SetEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _enableBtnActiveInactive = eventArgs.Enable;
                _enableBtnAssignUser = eventArgs.Enable;
                _enableGridDeptUser = eventArgs.Enable;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void DeptGrid_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = (DepartmentDTO)eventArgs.Data;
                loData.LACTIVE = true; //set active=true as default
                loData.DCREATE_DATE = DateTime.Now; //set now date when adding data
                loData.DUPDATE_DATE = DateTime.Now; //set now date when adding data
                //_enableBtnActiveInactive = false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task DeptGrid_R_CellLostFocused(R_CellLostFocusedEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = eventArgs.CurrentRow as DepartmentDTO;
                switch (eventArgs.ColumnName)
                {
                    case nameof(DepartmentDTO.CCENTER_CODE):
                        if (!string.IsNullOrWhiteSpace(eventArgs.Value.ToString()))
                        {
                            var loCenterCode = eventArgs.Value.ToString();
                            LookupGSL00900ViewModel loLookupGSL00900ViewModel = new LookupGSL00900ViewModel();
                            var loResult = await loLookupGSL00900ViewModel.GetCenter(new GSL00900ParameterDTO
                            {
                                CCOMPANY_ID = _clientHelper.CompanyId,
                                CUSER_ID = _clientHelper.UserId,
                                CSEARCH_TEXT = loCenterCode,
                            });

                            if (loResult == null)
                            {
                                loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class), "_ErrLookup01"));
                                loData.CCENTER_NAME = "";
                                loData.CCENTER_CODE = "";
                            }
                            else
                            {
                                loData.CCENTER_NAME = loResult.CCENTER_NAME;
                                loData.CCENTER_CODE = loResult.CCENTER_CODE;
                            }
                        }
                        else
                        {
                            loData.CCENTER_NAME = "";
                            loData.CCENTER_CODE = "";
                        }

                        await Task.CompletedTask;

                        break;

                    case nameof(DepartmentDTO.CMANAGER_NAME):
                        if (!string.IsNullOrWhiteSpace(eventArgs.Value.ToString()))
                        {
                            var loManagerCode = eventArgs.Value.ToString();
                            LookupGSL01000ViewModel loLookupGSL01000ViewModel = new LookupGSL01000ViewModel();

                            var loResult = await loLookupGSL01000ViewModel.GetUser(new GSL01000ParameterDTO
                            {
                                CSEARCH_TEXT = loManagerCode,
                            });

                            if (loResult == null)
                            {
                                loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class), "_ErrLookup01"));
                                loData.CMANAGER_CODE = "";
                                loData.CMANAGER_NAME = "";
                            }
                            else
                            {
                                loData.CMANAGER_CODE = loResult.CUSER_ID;
                                loData.CMANAGER_NAME = loResult.CUSER_NAME;
                            }
                        }
                        else
                        {
                            loData.CMANAGER_CODE = "";
                            loData.CMANAGER_NAME = "";
                        }

                        await Task.CompletedTask;

                        break;

                    case nameof(DepartmentDTO.CBRANCH_NAME):
                        if (!string.IsNullOrWhiteSpace(eventArgs.Value.ToString()))
                        {
                            var loBranchCode = eventArgs.Value.ToString();
                            LookupTXL00100ViewModel loLookupTXL00100ViewModel = new LookupTXL00100ViewModel();
                            var loResult = await loLookupTXL00100ViewModel.TXL00100BranchLookUp(
                                new TXL00100ParameterGetRecordDTO
                                {
                                    CSEARCH_TEXT = loBranchCode,
                                });
                            if (loResult == null)
                            {
                                loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(Lookup_TXFrontResources.Resources_TXLookup_Class), "_ErrLookup01"));
                                loData.CBRANCH_CODE = "";
                                loData.CBRANCH_NAME = "";
                            }
                            else
                            {
                                loData.CBRANCH_CODE = loResult.CBRANCH_CODE;
                                loData.CBRANCH_NAME = loResult.CBRANCH_NAME;
                            }
                        }
                        else
                        {
                            loData.CBRANCH_CODE = loData.CBRANCH_NAME = "";
                        }

                        await Task.CompletedTask;
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }

        #region GridLookup

        private async Task Dept_Before_Open_LookupAsync(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                switch (eventArgs.ColumnName)
                {
                    case nameof(DepartmentDTO.CCENTER_CODE):
                        eventArgs.Parameter = new GSL00900ParameterDTO();
                        eventArgs.TargetPageType = typeof(GSL00900);
                        await Task.CompletedTask;
                        break;
                    case nameof(DepartmentDTO.CMANAGER_NAME):
                        eventArgs.TargetPageType = typeof(GSL01000);
                        await Task.CompletedTask;
                        break;
                    case nameof(DepartmentDTO.CBRANCH_NAME): //CR09
                        eventArgs.TargetPageType = typeof(TXL00100);
                        await Task.CompletedTask;
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }

        private void Dept_After_Open_Lookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                if (eventArgs.Result == null)
                {
                    return;
                }

                //mengambil result dari popup dan set ke data row
                switch (eventArgs.ColumnName)
                {
                    case nameof(DepartmentDTO.CCENTER_CODE):
                        var loCenterLookupresult = R_FrontUtility.ConvertObjectToObject<GSL00900DTO>(eventArgs.Result);
                        ((DepartmentDTO)eventArgs.ColumnData).CCENTER_CODE = loCenterLookupresult.CCENTER_CODE;
                        ((DepartmentDTO)eventArgs.ColumnData).CCENTER_NAME = loCenterLookupresult.CCENTER_NAME;
                        break;
                    case nameof(DepartmentDTO.CMANAGER_NAME):
                        var loManagerLookupresult = R_FrontUtility.ConvertObjectToObject<GSL01000DTO>(eventArgs.Result);
                        ((DepartmentDTO)eventArgs.ColumnData).CMANAGER_CODE = loManagerLookupresult.CUSER_ID;
                        ((DepartmentDTO)eventArgs.ColumnData).CMANAGER_NAME = loManagerLookupresult.CUSER_NAME;
                        break;
                    case nameof(DepartmentDTO.CBRANCH_NAME): //CR09
                        var loBranchLookupresult = R_FrontUtility.ConvertObjectToObject<TXL00100DTO>(eventArgs.Result);
                        ((DepartmentDTO)eventArgs.ColumnData).CBRANCH_CODE = loBranchLookupresult.CBRANCH_CODE;
                        ((DepartmentDTO)eventArgs.ColumnData).CBRANCH_NAME = loBranchLookupresult.CBRANCH_NAME;
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion //GridLookup

        #endregion //GridDept

        #region Template

        private async Task DownloadTemplateAsync()
        {
            var loEx = new R_Exception();
            try
            {
                var loValidate = await R_MessageBox.Show("", "Are you sure download this template?",
                    R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await _deptViewModel.DownloadTemplate();

                    var saveFileName = $"Department - {_clientHelper.CompanyId}.xlsx";

                    await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            R_DisplayException(loEx);
        }

        #endregion //Template

        #region Upload

        private void R_Before_Open_PopupUpload(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(GSM04000PopupUpload);
            eventArgs.PageTitle = _localizer["_pageTitleUpload"];
            ;
        }

        public async Task R_After_Open_PopupUpload(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _gridDeptRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion //Upload

        #region Assign User

        private async Task R_Before_Open_PopupAssignUserAsync(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                await Dept_CustomLock(true);
                eventArgs.Parameter = _deptViewModel._departmentCode;
                eventArgs.TargetPageType = typeof(GSM04000PopupAssignUser);
                eventArgs.PageTitle = _localizer["_pageTitleAssignUser"];
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_After_Open_PopupAssignUser(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loTempResult = R_FrontUtility.ConvertObjectToObject<UserDepartmentDTO>(eventArgs.Result);
                if (loTempResult == null) //if there is no user select on popup
                {
                    await R_MessageBox.Show("", _localizer["_msgWarning1"], R_eMessageBoxButtonType.OK);
                    goto EndBlock;
                }

                await _deptUserViewModel.AssignUserToDept(new UserDepartmentDTO()
                    {
                        CDEPT_CODE = _deptViewModel._departmentCode,
                        CUSER_ID = loTempResult.CUSER_ID
                    },
                    eCRUDMode.AddMode);
                await _gridDeptUserRef.R_RefreshGrid(null);
                await Dept_CustomLock(false);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        #endregion //Assign User

        #region Active/Inactive

        private async Task R_Before_Open_Popup_ActivateInactiveAsync(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new();
            try
            {
                await Dept_CustomLock(true);
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel
                {
                    ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM04001" //Uabh Approval Code sesuai Spec masing masing
                };
                await loValidateViewModel
                    .RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" &&
                    loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    await _deptViewModel.ActiveInactiveProcessAsync(); //Ganti jadi method ActiveInactive masing masing
                    await _gridDeptRef.R_RefreshGrid(null);
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "GSM04001" //Uabh Approval Code sesuai Spec masing masing
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
            var loData = (DepartmentDTO)_conGridDeptRef.R_GetCurrentData();
            try
            {
                if (eventArgs.Success == false)
                {
                    return;
                }

                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    await _deptViewModel.ActiveInactiveProcessAsync();
                    await _deptViewModel.GetDepartment(loData);
                    await _gridDeptRef.R_SelectCurrentDataAsync(_deptViewModel._Department);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            await Dept_CustomLock(true);
        }

        #endregion //Active/Inactive

        #region DepartmentUser(CHILD)

        private async Task DeptUserGrid_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _deptUserViewModel._DepartmentCode = _deptViewModel._departmentCode;
                await _deptUserViewModel.GetDeptUserListByDeptCode();
                eventArgs.ListEntityResult = _deptUserViewModel._DepartmentUserList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task DeptUserGrid_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<UserDepartmentDTO>(eventArgs.Data);
                loParam.CDEPT_CODE = _deptUserViewModel._DepartmentCode;
                await _deptUserViewModel.GetDepartmentUser(loParam);
                eventArgs.Result = _deptUserViewModel._DepartmentUser;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task DeptUserGrid_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (DepartmentDTO)eventArgs.Data;
                await _deptUserViewModel.DeleteUserDepartment(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion //DepartmentUser(CHILD)

        private async Task OnClickPrint(MouseEventArgs arg)
        {
            var loEx = new R_Exception();

            try
            {
                if (!loEx.HasError)
                {
                    await _reportService.GetReport(
                        "R_DefaultServiceUrl",
                        "GS",
                        "rpt/GSM04000Print/DocumentReportPost",
                        "rpt/GSM04000Print/DocumentReportGet"
                    );
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
    }
}