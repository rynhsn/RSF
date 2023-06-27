﻿using GSM05000Common.DTOs;
using GSM05000Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM05000Front
{
    public partial class GSM05000Approval : R_Page
    {
        private GSM05000ApprovalUserViewModel _GSM05000ApprovalUserViewModel = new();
        private R_ConductorGrid _conductorRefDept;
        private R_ConductorGrid _conductorRefApprover;
        private R_Grid<GSM05000ApprovalDepartmentDTO> _gridRefDept;
        private R_Grid<GSM05000ApprovalUserDTO> _gridRefApprover;

        private GSM05000ApprovalReplacementViewModel _GSM05000ApprovalReplacementViewModel = new();
        private R_ConductorGrid _conductorRefReplacement;
        private R_Grid<GSM05000ApprovalReplacementDTO> _gridRefReplacement;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _GSM05000ApprovalUserViewModel.HeaderEntity = (GSM05000ApprovalHeaderDTO)poParameter;
                _GSM05000ApprovalUserViewModel.TransactionCode = ((GSM05000ApprovalHeaderDTO)poParameter).CTRANSACTION_CODE;

                await _gridRefDept.R_RefreshGrid(null);
                await _gridRefApprover.R_RefreshGrid(null);
                await _gridRefApprover.AutoFitAllColumnsAsync();
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
                await _GSM05000ApprovalUserViewModel.GetDepartmentList();
                eventArgs.ListEntityResult = _GSM05000ApprovalUserViewModel.DepartmentList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void DisplayDept(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loParam = (GSM05000ApprovalDepartmentDTO)eventArgs.Data;
                    _GSM05000ApprovalUserViewModel.GetDepartmentEntity(loParam);
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
                await _GSM05000ApprovalUserViewModel.GetApproverList();
                eventArgs.ListEntityResult = _GSM05000ApprovalUserViewModel.ApproverList;
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
                await _GSM05000ApprovalUserViewModel.GetApproverEntity(loParam);
                eventArgs.Result = _GSM05000ApprovalUserViewModel.ApproverEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }

        private void AfterAddApprover(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM05000ApprovalUserDTO)eventArgs.Data;
                _GSM05000ApprovalUserViewModel.GenerateSequence(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }

        private async Task SaveApprover(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000ApprovalUserDTO>(eventArgs.Data);
                await _GSM05000ApprovalUserViewModel.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _GSM05000ApprovalUserViewModel.ApproverEntity;
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
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var pcSelectedUserId = ((GSM05000ApprovalUserDTO)eventArgs.Data).CUSER_ID;
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
                await _GSM05000ApprovalUserViewModel.DeleteEntity(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }

        private async Task AfterDeleteApprover(object arg)
        {
            var loEx = new R_Exception();

            try
            {
                await _gridRefApprover.R_RefreshGrid(null);
                await _gridRefReplacement.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }

        private void BeforeLookupApprover(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Parameter = new GSL01100ParameterDTO()
                {
                    CTRANSACTION_CODE = _GSM05000ApprovalUserViewModel.HeaderEntity.CTRANSACTION_CODE
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
                var lcDeptCode = _GSM05000ApprovalUserViewModel.DepartmentEntity.CDEPT_CODE;
                var lcTransactionCode = _GSM05000ApprovalUserViewModel.HeaderEntity.CTRANSACTION_CODE;
                var lcSelectedUserId = eventArgs.Parameter.ToString();

                await _GSM05000ApprovalReplacementViewModel.GetReplacementList(lcTransactionCode, lcDeptCode, lcSelectedUserId);
                eventArgs.ListEntityResult = _GSM05000ApprovalReplacementViewModel.ReplacementList;
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
                    await _GSM05000ApprovalReplacementViewModel.GetReplacementEntity(loParam, lcTransactionCode, lcDeptCode, lcSelectedUserId);
                }
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
                await _GSM05000ApprovalReplacementViewModel.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _GSM05000ApprovalReplacementViewModel.ReplacementEntity;
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
                await _GSM05000ApprovalReplacementViewModel.DeleteEntity(loParam);
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
                    CTRANSACTION_CODE = _GSM05000ApprovalUserViewModel.HeaderEntity.CTRANSACTION_CODE
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
                loParam.CDEPT_CODE = _GSM05000ApprovalUserViewModel.DepartmentEntity.CDEPT_CODE;
                loParam.CTRANSACTION_CODE = _GSM05000ApprovalUserViewModel.HeaderEntity.CTRANSACTION_CODE;
                loParam.CUSER_ID = _GSM05000ApprovalUserViewModel.ApproverEntity.CUSER_ID;
                loParam.CVALID_TO = loParam.DVALID_TO.ToString("yyyyMMdd");
                loParam.CVALID_FROM = loParam.DVALID_FROM.ToString("yyyyMMdd");
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