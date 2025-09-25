
using GSM04500Common;
using GSM04500Model;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM04500Model;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Controls.Grid.Columns;
using BlazorClientHelper;
using R_BlazorFrontEnd.Controls.Enums;
using R_CommonFrontBackAPI;
using R_LockingFront;
using R_BlazorFrontEnd.Controls.Grid.Columns.ColumnInfo;
using Lookup_GSModel.ViewModel;

namespace GSM04500Front
{
    public partial class GSM04500AccountSetting
    {
        private GSM04501ViewModel JournalGOAViewModel = new();
        private R_Grid<GSM04510GOADTO> _gridRef;
        private R_ConductorGrid? _conJournalGOARef;

        private GSM04502ViewModel GOADeptViewModel = new();
        private R_ConductorGrid _conGOADeptRef;
        private R_Grid<GSM04510GOADeptDTO> _gridGOADeptRef;
        private bool _EnabledAccountNo = true;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await _gridRef.R_RefreshGrid((GSM04500DTO)poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                GSM04500DTO loParam = new();
                if ((GSM04500DTO)eventArgs.Parameter != null)
                {
                    JournalGOAViewModel.CurrentJournalGroup = (GSM04500DTO)eventArgs.Parameter;
                    loParam = JournalGOAViewModel.CurrentJournalGroup;
                    await JournalGOAViewModel.GetAllJournalGrupGOAAsync(loParam.CJRNGRP_TYPE, loParam.CJRNGRP_CODE);
                    eventArgs.ListEntityResult = JournalGOAViewModel.GOAList;

                    // await _gridGOADeptRef.R_RefreshGrid(loParam);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task R_ServiceGetRecordAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM04510GOADTO)eventArgs.Data;

                _EnabledAccountNo = !(loParam.LDEPARTMENT_MODE);
                eventArgs.Result = await JournalGOAViewModel.GetGOAOneRecord(loParam);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceSaveGOA(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM04510GOADTO)eventArgs.Data;
                if (loParam != null)
                {
                    // JournalGOAViewModel.ValidationEmptyField(loParam);  

                    if (loParam.LDEPARTMENT_MODE)
                    {
                        loParam.CGLACCOUNT_NO = "";
                    }
                }
                await JournalGOAViewModel.SaveGOA(loParam, eventArgs.ConductorMode);
                eventArgs.Result = JournalGOAViewModel.GOA;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                GSM04510GOADTO loParam = (GSM04510GOADTO)eventArgs.Data;
                JournalGOAViewModel.CurrentGOA = loParam;

                //Checking By Dept to enable disable add, delete edit
                _EnableGridGOADept = GOADeptViewModel._lGOADeptAllowAdd = loParam.LDEPARTMENT_MODE;
                await _gridGOADeptRef.R_RefreshGrid(loParam);
            }
        }

        #region CELL Value Change
        private void R_CellValueChanged(R_CellValueChangedEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.ColumnName == "LDEPARTMENT_MODE")
                {
                    var loData = (GSM04510GOADTO)eventArgs.CurrentRow;

                    eventArgs.Columns[2].Enabled = !(bool)eventArgs.Value;

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region EnableGrid
        private bool _EnableGridGOADept = false;

        #endregion

        #region GOALOOKUP
        //  Button LookUp GOA
        private void BeforeOpenLookUPGOA(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var loParam = new GSL00520ParameterDTO();
            loParam.CGOA_CODE = JournalGOAViewModel.CurrentGOA.CGOA_CODE!;
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00520);

        }

        private void AfterOpenLookGOA(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            //mengambil result dari popup dan set ke data row
            if (eventArgs.Result == null)
            {
                return;
            }
            if (eventArgs.ColumnName == "GLAccount_No")
            {
                var loTempResult2 = R_FrontUtility.ConvertObjectToObject<GSL00520DTO>(eventArgs.Result);
                ((GSM04510GOADTO)eventArgs.ColumnData).CGLACCOUNT_NO = loTempResult2.CGLACCOUNT_NO;
                ((GSM04510GOADTO)eventArgs.ColumnData).CGLACCOUNT_NAME = loTempResult2.CGLACCOUNT_NAME;
            }
        }

        private async Task R_CellLostFocusedGOA(R_CellLostFocusedEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                GSM04510GOADTO loGetData = (GSM04510GOADTO)eventArgs.CurrentRow;

                if (eventArgs.ColumnName == "GLAccount_No")
                {
                    if (!string.IsNullOrWhiteSpace((string)eventArgs.Value))
                    {
                        LookupGSL00520ViewModel loLookupViewModel = new LookupGSL00520ViewModel();
                        var param = new GSL00520ParameterDTO
                        {
                            CCOMPANY_ID = clientHelper.CompanyId,
                            CGOA_CODE = JournalGOAViewModel.CurrentGOA.CGOA_CODE!,
                            CSEARCH_TEXT = (string)eventArgs.Value,
                        };
                        var loResult = await loLookupViewModel.GetGOACOA(param);

                        if (loResult == null)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                    "_ErrLookup01"));
                            loGetData.CGLACCOUNT_NO = "";
                            loGetData.CGLACCOUNT_NAME = "";
                        }
                        else
                        {
                            loGetData.CGLACCOUNT_NO = loResult.CGLACCOUNT_NO;
                            loGetData.CGLACCOUNT_NAME = loResult.CGLACCOUNT_NAME;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task R_CellLostFocusedGOADEPT(R_CellLostFocusedEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                GSM04510GOADeptDTO loGetData = (GSM04510GOADeptDTO)eventArgs.CurrentRow;

                if (eventArgs.ColumnName == "CGLACCOUNT_NO")
                {
                    if (!string.IsNullOrWhiteSpace((string)eventArgs.Value))
                    {
                        LookupGSL00520ViewModel loLookupViewModel = new LookupGSL00520ViewModel();
                        var param = new GSL00520ParameterDTO
                        {
                            CCOMPANY_ID = clientHelper.CompanyId,
                            CGOA_CODE = JournalGOAViewModel.CurrentGOA.CGOA_CODE!,
                            CSEARCH_TEXT = (string)eventArgs.Value,
                        };
                        var loResult = await loLookupViewModel.GetGOACOA(param);

                        if (loResult == null)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                    "_ErrLookup01"));
                            loGetData.CGLACCOUNT_NO = "";
                            loGetData.CGLACCOUNT_NAME = "";
                        }
                        else
                        {
                            loGetData.CGLACCOUNT_NO = loResult.CGLACCOUNT_NO;
                            loGetData.CGLACCOUNT_NAME = loResult.CGLACCOUNT_NAME;
                        }
                    }
                }
                else if (eventArgs.ColumnName == "CDEPT_CODE")
                {
                    if (!string.IsNullOrWhiteSpace((string)eventArgs.Value))
                    {
                        LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel();
                        var param = new GSL00700ParameterDTO
                        {
                            CSEARCH_TEXT = (string)eventArgs.Value,
                        };
                        var loResult = await loLookupViewModel.GetDepartment(param);

                        if (loResult == null)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                    "_ErrLookup01"));
                            loGetData.CDEPT_CODE = "";
                            loGetData.CDEPT_NAME = "";
                        }
                        else
                        {
                            loGetData.CDEPT_CODE = loResult.CDEPT_CODE;
                            loGetData.CDEPT_NAME = loResult.CDEPT_NAME;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region GroupOfAccountDept
        public void CheckGrid(R_CheckGridEventArgs e)
        {
            if (JournalGOAViewModel.GOAList.Count() > 0)
            {
                _EnableGridGOADept = JournalGOAViewModel.CurrentGOA.LDEPARTMENT_MODE; ;
                e.Allow = JournalGOAViewModel.CurrentGOA.LDEPARTMENT_MODE;

            }
            else
            {
                e.Allow = false;
            }
        }
        private async Task GridGOADept_GetList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loDataTemp = (GSM04510GOADTO)eventArgs.Parameter;
                GSM04510GOADTO loParam = R_FrontUtility.ConvertObjectToObject<GSM04510GOADTO>(loDataTemp);
                // var liParam = GOADeptViewModel.CurrentGOADEPT;
                await GOADeptViewModel.GetGOAAllByDept(loParam);
                eventArgs.ListEntityResult = GOADeptViewModel.GOADeptList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task R_ServiceGetRecordGOADeptAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM04510GOADeptDTO)eventArgs.Data;
                eventArgs.Result = await GOADeptViewModel.GetGOADeptOneRecord(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public void ServiceValidationGOADept(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                GOADeptViewModel.ValidationGOADept((GSM04510GOADeptDTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            eventArgs.Cancel = loEx.HasError;
            loEx.ThrowExceptionIfErrors();
        }
        private void AfterAddGOADept(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                var loData = (GSM04510GOADeptDTO)eventArgs.Data;
                if (loData != null)
                {
                    loData.CPROPERTY_ID = JournalGOAViewModel.CurrentGOA.CPROPERTY_ID!;
                    loData.CJRNGRP_TYPE = JournalGOAViewModel.CurrentGOA.CJRNGRP_TYPE!;
                    loData.CJRNGRP_CODE = JournalGOAViewModel.CurrentGOA.CJRNGRP_CODE!;
                    loData.CGOA_CODE = JournalGOAViewModel.CurrentGOA.CGOA_CODE!;

                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }
        private async Task ServiceSaveGOADept(R_ServiceSaveEventArgs eventArgs)

        {
            var loEx = new R_Exception();
            try
            {

                var loParam = (GSM04510GOADeptDTO)eventArgs.Data;
                //loParam.CPROPERTY_ID = JournalGOAViewModel.CurrentGOA.CPROPERTY_ID!;
                //loParam.CJRNGRP_TYPE = JournalGOAViewModel.CurrentGOA.CJRNGRP_TYPE!;
                //loParam.CJRNGRP_CODE = JournalGOAViewModel.CurrentGOA.CJRNGRP_CODE!;
                //loParam.CGOA_CODE = JournalGOAViewModel.CurrentGOA.CGOA_CODE!;
                await GOADeptViewModel.SaveGOADept(loParam, eventArgs.ConductorMode);
                eventArgs.Result = GOADeptViewModel.GOADept;
                // await _gridGOADeptRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region LookUpGOADEPT
        //  Button LookUp DeptCode
        private void BeforeOpenLookUpDeptCode(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            switch (eventArgs.ColumnName)
            {
                case "CDEPT_CODE":
                    eventArgs.Parameter = new GSL00700ParameterDTO();
                    eventArgs.TargetPageType = typeof(GSL00700);
                    break;
                case "CGLACCOUNT_NO":
                    var loParam = new GSL00520ParameterDTO();
                    loParam.CGOA_CODE = JournalGOAViewModel.CurrentGOA.CGOA_CODE!;
                    eventArgs.Parameter = loParam;
                    eventArgs.TargetPageType = typeof(GSL00520);
                    break;
            }
        }

        private void AfterOpenLookUpDeptCode(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            //mengambil result dari popup dan set ke data row
            if (eventArgs.Result == null)
            {
                return;
            }
            switch (eventArgs.ColumnName)
            {
                case "CDEPT_CODE":
                    var loTempResult = R_FrontUtility.ConvertObjectToObject<GSL00700DTO>(eventArgs.Result);
                    ((GSM04510GOADeptDTO)eventArgs.ColumnData).CDEPT_CODE = loTempResult.CDEPT_CODE;
                    ((GSM04510GOADeptDTO)eventArgs.ColumnData).CDEPT_NAME = loTempResult.CDEPT_NAME;
                    break;
                case "CGLACCOUNT_NO":
                    var loTempResult2 = R_FrontUtility.ConvertObjectToObject<GSL00520DTO>(eventArgs.Result);
                    ((GSM04510GOADeptDTO)eventArgs.ColumnData).CGLACCOUNT_NO = loTempResult2.CGLACCOUNT_NO;
                    ((GSM04510GOADeptDTO)eventArgs.ColumnData).CGLACCOUNT_NAME = loTempResult2.CGLACCOUNT_NAME;
                    break;
            }
        }
        #endregion

        #endregion
        #region UserLocking
        [Inject] IClientHelper clientHelper { get; set; }
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_MODULE_NAME = "GS";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (GSM04510GOADTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {

                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "GSM04500",
                        Table_Name = "GSM_JRNGRP",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CJRNGRP_TYPE, loData.CJRNGRP_CODE)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "GSM04500",
                        Table_Name = "GSM_JRNGRP",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CJRNGRP_TYPE, loData.CJRNGRP_CODE)
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
        #endregion
    }
}

