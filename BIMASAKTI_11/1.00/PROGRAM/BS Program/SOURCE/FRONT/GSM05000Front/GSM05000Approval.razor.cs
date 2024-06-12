using BlazorClientHelper;
using GSM05000Common.DTOs;
using GSM05000Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;

namespace GSM05000Front
{
    public partial class GSM05000Approval : R_Page
    {
        private GSM05000ApprovalUserViewModel _viewModelUser = new();
        private R_ConductorGrid _conductorRefDept;
        private R_ConductorGrid _conductorRefUser;
        private R_Grid<GSM05000ApprovalDepartmentDTO> _gridRefDept = new();
        private R_Grid<GSM05000ApprovalUserDTO> _gridRefUser = new();

        private GSM05000ApprovalReplacementViewModel _viewModelReplacement = new();
        private R_ConductorGrid _conductorRefReplacement;
        private R_Grid<GSM05000ApprovalReplacementDTO> _gridRefReplacement = new();

        [Inject] public IClientHelper _clientHelper { get; set; }

        private bool _gridDeptEnabled;
        private bool _gridUserEnabled;
        private bool _gridReplacementEnabled;


        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                // _viewModelUser.HeaderEntity = (GSM05000ApprovalHeaderDTO)poParameter;
                _viewModelUser.TransactionCode =
                    ((GSM05000ApprovalHeaderDTO)poParameter).CTRANS_CODE;
                await _viewModelUser.GetApprovalHeader();

                if (_viewModelUser.HeaderEntity.LAPPROVAL_DEPT)
                {
                    await _gridRefDept.R_RefreshGrid(null);
                }

                await Task.Delay(100);

                _gridUserEnabled = _viewModelUser.HeaderEntity.LAPPROVAL_FLAG;

                await _gridRefUser.R_RefreshGrid(null);

                // await _gridRefUser.AutoFitAllColumnsAsync();
                //await _gridRefReplacement.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Approval User

        private async Task GetListDept(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelUser.GetDepartmentList();
                // tambahkan dummy data ke _viewModelUser.DepartmentList * 50

                eventArgs.ListEntityResult = _viewModelUser.DepartmentList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task DisplayDept(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loParam = (GSM05000ApprovalDepartmentDTO)eventArgs.Data;
                    _viewModelUser.GetDepartmentEntity(loParam);
                    await _gridRefUser.R_RefreshGrid(null);
                    // await _gridRefReplacement.R_RefreshGrid(_viewModelUser.ApproverEntity.CUSER_ID);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task GetListApprover(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelUser.GetApproverList();
                eventArgs.ListEntityResult = _viewModelUser.ApproverList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task GetRecordApprover(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000ApprovalUserDTO>(eventArgs.Data);
                await _viewModelUser.GetApproverEntity(loParam);
                eventArgs.Result = _viewModelUser.ApproverEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void AfterAddApprover(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM05000ApprovalUserDTO)eventArgs.Data;
                _viewModelUser.GenerateSequence(loParam);
                loParam.DCREATE_DATE = DateTime.Now;
                loParam.DUPDATE_DATE = DateTime.Now;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task SaveApprover(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000ApprovalUserDTO>(eventArgs.Data);

                // ubah CSEQUENCE jadi integer dan assign ke loParam.ISEQUENCE
                // loParam.ISEQUENCE = Convert.ToInt32(loParam.CSEQUENCE);

                await _viewModelUser.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModelUser.ApproverEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task DisplayApprover(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await Task.Delay(100);
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loParam = (GSM05000ApprovalUserDTO)eventArgs.Data;
                    // loParam.CSEQUENCE = loParam.ISEQUENCE.ToString("D3");
                    var pcSelectedUserId = loParam.CUSER_ID;
                    _viewModelReplacement.SelectedUserId = pcSelectedUserId;
                    await _gridRefReplacement.R_RefreshGrid(pcSelectedUserId);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task DeleteApprover(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000ApprovalUserDTO>(eventArgs.Data);
                await _viewModelUser.DeleteEntity(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task AfterDeleteApprover(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _gridRefUser.R_RefreshGrid(null);
                await _gridRefReplacement.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void BeforeLookupApprover(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Parameter = new GSL01100ParameterDTO()
                {
                    // CTRANSACTION_CODE = _viewModelUser.HeaderEntity.CTRANS_CODE
                    CPARAMETER_ID = _viewModelUser.HeaderEntity.CTRANS_CODE,
                    CPROGRAM_ID = "GST00500"
                };
                eventArgs.TargetPageType = typeof(GSL01100);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void AfterLookupApprover(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loTempResult = (GSL01100DTO)eventArgs.Result;
                var loGetData = (GSM05000ApprovalUserDTO)eventArgs.ColumnData;
                if (loTempResult == null)
                    return;

                loGetData.CUSER_ID = loTempResult.CUSER_ID;
                loGetData.CUSER_NAME = loTempResult.CUSER_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Replacement

        private async Task GetListReplacement(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var lcDeptCode = _viewModelUser.DepartmentEntity.CDEPT_CODE == null
                    ? ""
                    : _viewModelUser.DepartmentEntity.CDEPT_CODE;
                var lcTransactionCode = _viewModelUser.HeaderEntity.CTRANS_CODE;
                var lcSelectedUserId = eventArgs.Parameter.ToString();

                await _viewModelReplacement.GetReplacementList(lcTransactionCode, lcDeptCode,
                    lcSelectedUserId);
                eventArgs.ListEntityResult = _viewModelReplacement.ReplacementList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task GetRecordReplacement(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM05000ApprovalReplacementDTO)eventArgs.Data;

                var lcDeptCode = loParam.CDEPT_CODE;
                var lcTransactionCode = loParam.CTRANSACTION_CODE;
                var lcSelectedUserId = loParam.CUSER_ID;
                await _viewModelReplacement.GetReplacementEntity(loParam, lcTransactionCode, lcDeptCode,
                    lcSelectedUserId);

                eventArgs.Result = _viewModelReplacement.ReplacementEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task DisplayReplacement(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loParam = (GSM05000ApprovalReplacementDTO)eventArgs.Data;

                    var lcDeptCode = loParam.CDEPT_CODE;
                    var lcTransactionCode = loParam.CTRANSACTION_CODE;
                    var lcSelectedUserId = loParam.CUSER_ID;
                    await _viewModelReplacement.GetReplacementEntity(loParam, lcTransactionCode,
                        lcDeptCode, lcSelectedUserId);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task DeleteReplacement(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000ApprovalReplacementDTO>(eventArgs.Data);
                await _viewModelReplacement.DeleteEntity(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void BeforeLookupReplacement(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Parameter = new GSL01100ParameterDTO()
                {
                    // CTRANSACTION_CODE = _viewModelUser.HeaderEntity.CTRANS_CODE
                    CPARAMETER_ID = _viewModelUser.HeaderEntity.CTRANS_CODE,
                    CPROGRAM_ID = "GST00500"
                };
                eventArgs.TargetPageType = typeof(GSL01100);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void AfterLookupReplacement(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loTempResult = (GSL01100DTO)eventArgs.Result;
                var loGetData = (GSM05000ApprovalReplacementDTO)eventArgs.ColumnData;
                if (loTempResult == null)
                    return;

                loGetData.CUSER_REPLACEMENT = loTempResult.CUSER_ID;
                loGetData.CUSER_REPLACEMENT_NAME = loTempResult.CUSER_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void AfterAddReplacement(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM05000ApprovalReplacementDTO)eventArgs.Data;
                var lcCurrentDate = (DateTime.Now);
                loParam.DVALID_TO = lcCurrentDate;
                loParam.DVALID_FROM = lcCurrentDate;
                loParam.DCREATE_DATE = DateTime.Now;
                loParam.DUPDATE_DATE = DateTime.Now;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void SavingReplacement(R_SavingEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM05000ApprovalReplacementDTO)eventArgs.Data;
                loParam.CDEPT_CODE = _viewModelUser.DepartmentEntity.CDEPT_CODE == null
                    ? ""
                    : _viewModelUser.DepartmentEntity.CDEPT_CODE;
                loParam.CTRANSACTION_CODE = _viewModelUser.HeaderEntity.CTRANS_CODE;
                loParam.CUSER_ID = _viewModelUser.ApproverEntity.CUSER_ID;
                loParam.CVALID_TO = loParam.DVALID_TO.ToString("yyyyMMdd");
                loParam.CVALID_FROM = loParam.DVALID_FROM.ToString("yyyyMMdd");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        private async Task SaveReplacement(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000ApprovalReplacementDTO>(eventArgs.Data);
                loParam.CUSER_ID = _viewModelReplacement.SelectedUserId;
                await _viewModelReplacement.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModelReplacement.ReplacementEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        private Task CopyToBeforePopup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSM05000ApprovalCopyDTO()
            {
                CTRANSACTION_CODE = _viewModelUser.HeaderEntity.CTRANS_CODE,
                CDEPT_CODE = _viewModelUser.DepartmentEntity.CDEPT_CODE,
                CDEPT_NAME = _viewModelUser.DepartmentEntity.CDEPT_NAME,
            };
            eventArgs.TargetPageType = typeof(GSM05000ApprovalCopyTo);
            return Task.CompletedTask;
        }

        private async Task CopyToAfterPopup(R_AfterOpenPopupEventArgs eventArgs)
        {
            await _gridRefUser.R_RefreshGrid(null);
        }

        private Task CopyFromBeforePopup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSM05000ApprovalCopyDTO()
            {
                CTRANSACTION_CODE = _viewModelUser.HeaderEntity.CTRANS_CODE,
                CDEPT_CODE = _viewModelUser.DepartmentEntity.CDEPT_CODE,
                CDEPT_NAME = _viewModelUser.DepartmentEntity.CDEPT_NAME,
            };
            eventArgs.TargetPageType = typeof(GSM05000ApprovalCopyFrom);
            return Task.CompletedTask;
        }

        private async Task CopyFromAfterPopup(R_AfterOpenPopupEventArgs eventArgs)
        {
            await _gridRefUser.R_RefreshGrid(null);
        }

        private Task ChangeSequenceBeforePopup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loParam = _viewModelUser.HeaderEntity.CTRANS_CODE;
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSM05000ApprovalChangeSequence);
            return Task.CompletedTask;
        }

        private async Task ChangeSequenceAfterPopup(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loException = new R_Exception();
            try
            {
                if (eventArgs.Success == false)
                    return;

                var result = (bool)eventArgs.Result;

                if (result)
                {
                    await _gridRefUser.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task Validation(R_ValidationEventArgs eventArgs)
        {
            var loException = new R_Exception();
            var loChoice = new R_eMessageBoxResult();

            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Edit)
                {
                    var loParam = (GSM05000ApprovalUserDTO)eventArgs.Data;
                    if (_viewModelUser.ReplacementFlagTemp && loParam.LREPLACEMENT == false)
                    {
                        loChoice = await R_MessageBox.Show(_localizer["ConfirmLabel"], _localizer["Confirm03"],
                            R_eMessageBoxButtonType.YesNo);
                    }

                    if (loChoice == R_eMessageBoxResult.No)
                    {
                        eventArgs.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private void SetOtherUser(R_SetEventArgs eventArgs)
        {
            // if (_viewModelUser.HeaderEntity.LAPPROVAL_FLAG)
            // {
            if (_viewModelUser.HeaderEntity.LAPPROVAL_DEPT)
            {
                _gridDeptEnabled = eventArgs.Enable;
            }

            _gridReplacementEnabled = eventArgs.Enable && _viewModelUser.ApproverList.Count > 0;
            // }
        }

        private void SetOtherReplacement(R_SetEventArgs eventArgs)
        {
            // if (_viewModelUser.HeaderEntity.LAPPROVAL_FLAG)
            // {
            if (_viewModelUser.HeaderEntity.LAPPROVAL_DEPT)
            {
                _gridDeptEnabled = eventArgs.Enable;
            }

            _gridUserEnabled = eventArgs.Enable;
            // }
        }

        /*
        #region Locking

        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_MODULE_NAME = "GS";

        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult;

            try
            {
                var loData = (GSM05000ApprovalUserDTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: DEFAULT_HTTP_NAME);

                var Company_Id = _clientHelper.CompanyId;
                var User_Id = _clientHelper.UserId;
                var Program_Id = "GSM05000";
                var Table_Name = "GSM_TRANSACTION_APPROVAL";
                var Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CTRANS_CODE, loData.CDEPT_CODE,
                    loData.CUSER_ID);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = Company_Id,
                        User_Id = User_Id,
                        Program_Id = Program_Id,
                        Table_Name = Table_Name,
                        Key_Value = Key_Value
                    };
                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = Company_Id,
                        User_Id = User_Id,
                        Program_Id = Program_Id,
                        Table_Name = Table_Name,
                        Key_Value = Key_Value
                    };
                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }

                llRtn = loLockResult.IsSuccess;
                if (loLockResult is { IsSuccess: false, Exception: not null })
                    throw loLockResult.Exception;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return llRtn;
        }

        #endregion
        */
    }
}