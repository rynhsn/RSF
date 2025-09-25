using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
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
using Lookup_GSFRONT;
using System.Globalization;
using R_BlazorFrontEnd.Enums;

namespace CBT01200FRONT
{
    public partial class CBT01210 : R_Page
    {
      /*
        private CBT01200ViewModel _TransactionListViewModel = new();
        private CBT01210ViewModel _TransactionEntryViewModel = new();
        private R_Conductor _conductorRef;
        private R_ConductorGrid _conductorDetailRef;
        private R_Grid<CBT01201DTO> _gridDetailRef;
        
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_ILocalizer<CBT01200FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private R_Lookup R_LookupBtnPrint;

        private R_Lookup R_LookupBtnDept;

        private R_TextBox _txt_CDEPT_CODE;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _TransactionEntryViewModel.GetAllUniversalData();
                var loParam = R_FrontUtility.ConvertObjectToObject<CBT01200DTO>(poParameter);
                if (!string.IsNullOrWhiteSpace(loParam.CREC_ID))
                {
                    await _conductorRef.R_GetEntity(loParam);
                }
                else
                {
                    _TransactionListViewModel.RefDate = _TransactionEntryViewModel.VAR_TODAY.DTODAY;
                    _TransactionListViewModel.DocDate = _TransactionEntryViewModel.VAR_TODAY.DTODAY;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        #region Locking
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlCB";
        private const string DEFAULT_MODULE_NAME = "CB";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (CBT01200JournalHDParam)eventArgs.Data;

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
                        Program_Id = "CBT01200",
                        Table_Name = "CBT_TRANS_HD",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "CBT01200",
                        Table_Name = "CBT_TRANS_HD",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO)
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

        #region Form
        private async Task JournalForm_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<CBT01200JournalHDParam>(eventArgs.Data);
                await _TransactionListViewModel.GetJournalRecord(loParam);
                eventArgs.Result = _TransactionListViewModel.JournalRecord;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task JournalForm_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<CBT01200JournalHDParam>(eventArgs.Data);
                _TransactionListViewModel.VAR_GSM_TRANSACTION_CODE = _TransactionEntryViewModel.VAR_GSM_TRANSACTION_CODE;
                await _TransactionListViewModel.SaveJournal(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _TransactionListViewModel.JournalRecord;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task BtnDelete_OnClick()
        {
            var loEx = new R_Exception();
            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["Q03"], R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.No)
                    goto EndBlock;

                var loData = (CBT01200JournalHDParam)_conductorRef.R_GetCurrentData();
                var loParam = R_FrontUtility.ConvertObjectToObject<CBT01200UpdateStatusDTO>(loData);
                loParam.LAUTO_COMMIT = false;
                loParam.LUNDO_COMMIT = false;
                loParam.CNEW_STATUS = "99";

                await _TransactionListViewModel.UpdateJournalStatus(loParam);
                await _conductorRef.R_GetEntity(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        private R_TextBox _DeptCode_TextBox;

        private async Task JournalForm_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                _TransactionEntryViewModel.JournalDetailGridTemp = new(_gridDetailRef.DataSource.ToList()); ;//store detail to temp
                await _DeptCode_TextBox.FocusAsync();
                var data = (CBT01200DTO)eventArgs.Data;

                data.CCREATE_BY = clientHelper.UserId;
                data.CUPDATE_BY = clientHelper.UserId;
                data.DUPDATE_DATE = _TransactionEntryViewModel.VAR_TODAY.DTODAY;
                data.DCREATE_DATE = _TransactionEntryViewModel.VAR_TODAY.DTODAY;
                _TransactionListViewModel.RefDate = _TransactionEntryViewModel.VAR_TODAY.DTODAY;
                data.NLBASE_RATE = 1;
                data.NLCURRENCY_RATE = 1;
                data.NBBASE_RATE = 1;
                data.NBCURRENCY_RATE = 1;
                data.CDEPT_CODE = _TransactionEntryViewModel.VAR_GL_SYSTEM_PARAM.CCLOSE_DEPT_CODE;
                data.CDEPT_NAME = _TransactionEntryViewModel.VAR_GL_SYSTEM_PARAM.CCLOSE_DEPT_NAME;

                if (!string.IsNullOrWhiteSpace(data.CDEPT_CODE))
                {
                    _gridDetailRef.DataSource.Clear();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void ValidationFormCBT01200TransactionEntry(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (CBT01200DTO)eventArgs.Data;
                if (string.IsNullOrWhiteSpace(loParam.CDEPT_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V01"));
                }

                if (string.IsNullOrWhiteSpace(loParam.CDEPT_CODE) && _TransactionEntryViewModel.VAR_GSM_TRANSACTION_CODE.LINCREMENT_FLAG == false)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V03"));
                }

                if (_TransactionListViewModel.RefDate == DateTime.MinValue)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V03"));
                }
                else
                {
                    if (_TransactionListViewModel.RefDate > _TransactionEntryViewModel.VAR_TODAY.DTODAY)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "V04"));
                    }

                    if (int.Parse(_TransactionListViewModel.RefDate.Value.ToString("yyyyMMdd")) < int.Parse(_TransactionEntryViewModel.VAR_CB_SYSTEM_PARAM.CCB_LINK_DATE))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "V05"));
                    }

                    if (int.Parse(_TransactionListViewModel.RefDate.Value.ToString("yyyyMMdd")) < int.Parse(_TransactionEntryViewModel.VAR_SOFT_PERIOD_START_DATE.CEND_DATE))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "V06"));
                    }
                }

                if (string.IsNullOrWhiteSpace(loParam.CCB_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V07"));
                }

                if (loParam.NTRANS_AMOUNT <= 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V08"));
                }

                if (string.IsNullOrWhiteSpace(loParam.CDOC_NO))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V09"));
                }

                if (_TransactionListViewModel.DocDate.HasValue == false)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V10"));
                }
                else
                {
                    if (int.Parse(_TransactionListViewModel.DocDate.Value.ToString("yyyyMMdd")) > int.Parse(_TransactionListViewModel.RefDate.Value.ToString("yyyyMMdd")))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "V11"));
                    }

                    if (int.Parse(_TransactionListViewModel.DocDate.Value.ToString("yyyyMMdd")) < int.Parse(_TransactionEntryViewModel.VAR_SOFT_PERIOD_START_DATE.CEND_DATE))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "V12"));
                    }
                }

                if (string.IsNullOrWhiteSpace(loParam.CTRANS_DESC))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V13"));
                }

                if (loParam.NLBASE_RATE <= 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V14"));
                }

                if (loParam.NLCURRENCY_RATE <= 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V15"));
                }

                if (loParam.NBBASE_RATE <= 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V16"));
                }

                if (loParam.NBCURRENCY_RATE <= 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V17"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Private Property
        private string lcLabelSubmit = "Submit";
        private string lcLabelCommit = "Commit";
        private bool EnableEdit = false;
        private bool EnableDelete = false;
        private bool EnableSubmit = false;
        private bool EnableApprove = false;
        private bool EnableCommit = false;
        private bool EnableHaveRecId = false;
        #endregion

        private async Task JournalForm_RDisplay(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var data = (CBT01200DTO)eventArgs.Data;
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    if (!string.IsNullOrWhiteSpace(data.CSTATUS))
                    {
                        lcLabelCommit = data.CSTATUS == "80" ? _localizer["_UndoCommit"] : _localizer["_Commit"];
                        lcLabelSubmit = data.CSTATUS == "10" ? _localizer["_UndoSubmit"] : _localizer["_Submit"];

                        EnableEdit = data.CSTATUS == "00";
                        EnableDelete = data.CSTATUS != "00";
                        EnableSubmit = data.CSTATUS == "00" || data.CSTATUS == "10";
                        EnableApprove = data.CSTATUS == "10" && _TransactionEntryViewModel.VAR_GSM_TRANSACTION_CODE.LAPPROVAL_FLAG == true;
                        EnableCommit = (data.CSTATUS == "20" || (data.CSTATUS == "10" && _TransactionEntryViewModel.VAR_GSM_TRANSACTION_CODE.LAPPROVAL_FLAG == false)) ||
                                       (data.CSTATUS == "80") &&
                                       int.Parse(data.CREF_PRD) >= int.Parse(_TransactionEntryViewModel.VAR_GL_SYSTEM_PARAM.CSOFT_PERIOD);
                        EnableHaveRecId = !string.IsNullOrWhiteSpace(data.CREC_ID);
                        _TransactionEntryViewModel._CREC_ID = data.CREC_ID;
                    }
                    if (!string.IsNullOrWhiteSpace(_TransactionEntryViewModel._CREC_ID))
                    {
                        await _gridDetailRef.R_RefreshGrid(null);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }

        private async Task JournalForm_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            var res = await R_MessageBox.Show("", _localizer["Q04"],
                R_eMessageBoxButtonType.YesNo);
            eventArgs.Cancel = res == R_eMessageBoxResult.No;
            if (res != R_eMessageBoxResult.No)
            {
                _TransactionEntryViewModel.JournalDetailGrid = _TransactionEntryViewModel.JournalDetailGridTemp; //assign detail back from temp
                if (eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    _gridDetailRef.DataSource.Clear();
                }
                await Close(false, false);
            }
        }

        private async Task CopyJournalEntryProcess()
        {
            var loEx = new R_Exception();
            try
            {
                var loOldData = (CBT01200JournalHDParam)_conductorRef.R_GetCurrentData();
                await _conductorRef.Add();
                if (_conductorRef.R_ConductorMode == R_eConductorMode.Add)
                {
                    var loNewData = (CBT01200JournalHDParam)_conductorRef.R_GetCurrentData();

                    loNewData.CREF_PRD = loOldData.CREF_PRD;
                    loNewData.LALLOW_APPROVE = loOldData.LALLOW_APPROVE;
                    loNewData.CINPUT_TYPE = loOldData.CINPUT_TYPE;
                    loNewData.CDEPT_CODE = loOldData.CDEPT_CODE;
                    _TransactionListViewModel.RefDate = DateTime.ParseExact(loOldData.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                    loNewData.CCB_CODE = loOldData.CCB_CODE;
                    loNewData.CCB_NAME = loOldData.CCB_NAME;
                    loNewData.CCB_ACCOUNT_NO = loOldData.CCB_ACCOUNT_NO;
                    loNewData.CCURRENCY_CODE = loOldData.CCURRENCY_CODE;
                    loNewData.NTRANS_AMOUNT = loOldData.NTRANS_AMOUNT;
                    loNewData.NLBASE_RATE = loOldData.NLBASE_RATE;
                    loNewData.CCURRENCY_CODE = loOldData.CCURRENCY_CODE;
                    loNewData.NLCURRENCY_RATE = loOldData.NLCURRENCY_RATE;
                    loNewData.NBBASE_RATE = loOldData.NBBASE_RATE;
                    loNewData.NBCURRENCY_RATE = loOldData.NBCURRENCY_RATE;
                    loNewData.NDEBIT_AMOUNT = loOldData.NDEBIT_AMOUNT;
                    loNewData.NCREDIT_AMOUNT = loOldData.NCREDIT_AMOUNT;
                    loNewData.CDOC_NO = loOldData.CDOC_NO;
                    _TransactionListViewModel.DocDate = DateTime.ParseExact(loOldData.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                    loNewData.CTRANS_DESC = loOldData.CTRANS_DESC;
                    loNewData.CSTATUS_NAME = loOldData.CSTATUS_NAME;
                    loNewData.CSTATUS = loOldData.CSTATUS;
                    loNewData.CUPDATE_BY = loOldData.CUPDATE_BY;
                    loNewData.DUPDATE_DATE = loOldData.DUPDATE_DATE;
                    loNewData.CCREATE_BY = loOldData.CCREATE_BY;
                    loNewData.DCREATE_DATE = loOldData.DCREATE_DATE;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Refresh Currency Method
        public async Task RefreshCurrency()
        {
            var loEx = new R_Exception();
            try
            {
                _TransactionListViewModel.VAR_CB_SYSTEM_PARAM=_TransactionEntryViewModel.VAR_CB_SYSTEM_PARAM;
                var loParam = R_FrontUtility.ConvertObjectToObject<CBT01210LastCurrencyRateDTO>(_conductorRef.R_GetCurrentData());
                var loResult = await _TransactionListViewModel.GetLastCurrency(loParam);
                if (loResult is null)
                {
                    _TransactionListViewModel.Data.NLBASE_RATE = 1;
                    _TransactionListViewModel.Data.NLBASE_RATE = 1;
                    _TransactionListViewModel.Data.NLCURRENCY_RATE = 1;
                    _TransactionListViewModel.Data.NBBASE_RATE = 1;
                    _TransactionListViewModel.Data.NBCURRENCY_RATE = 1;
                }
                else
                {
                    _TransactionListViewModel.Data.NLBASE_RATE = loResult.NLBASE_RATE_AMOUNT;
                    _TransactionListViewModel.Data.NLCURRENCY_RATE = loResult.NLCURRENCY_RATE_AMOUNT;
                    _TransactionListViewModel.Data.NBBASE_RATE = loResult.NBBASE_RATE_AMOUNT;
                    _TransactionListViewModel.Data.NBCURRENCY_RATE = loResult.NBCURRENCY_RATE_AMOUNT;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Ref Date OnLostFocus

        private async Task RefDate_ValueChange(DateTime? poParam)
        {
            var loEx = new R_Exception();
            try
            {
                _TransactionListViewModel.RefDate = poParam;
                if (!string.IsNullOrWhiteSpace(_TransactionListViewModel.Data.CCURRENCY_CODE) &&
                    (_TransactionListViewModel.Data.CCURRENCY_CODE != _TransactionEntryViewModel.VAR_GSM_COMPANY.CLOCAL_CURRENCY_CODE
                    || _TransactionListViewModel.Data.CCURRENCY_CODE != _TransactionEntryViewModel.VAR_GSM_COMPANY.CBASE_CURRENCY_CODE))
                {
                    await RefreshCurrency();
                }
                else
                {
                    _TransactionListViewModel.Data.NLBASE_RATE = 1;
                    _TransactionListViewModel.Data.NLCURRENCY_RATE = 1;
                    _TransactionListViewModel.Data.NBBASE_RATE = 1;
                    _TransactionListViewModel.Data.NBCURRENCY_RATE = 1;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Detail
        private async Task JournalDet_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _TransactionEntryViewModel.GetJournalDetailList();

                eventArgs.ListEntityResult = _TransactionEntryViewModel.JournalDetailGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task JournalDet_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<CBT01210ParamDTO>(eventArgs.Data);
                await _TransactionEntryViewModel.GetJournalDetailRecord(loData);
                eventArgs.Result = _TransactionEntryViewModel.Journal;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private bool EnableCenterList = true;
        private void JournalDet_RDisplay(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var data = R_FrontUtility.ConvertObjectToObject<CBT01201DTO>(eventArgs.Data);
                var loHeaderData = (CBT01200DTO)_conductorRef.R_GetCurrentData();

                if (data != null)
                {
                    EnableCenterList = (data.CBSIS == "B" && _TransactionEntryViewModel.VAR_GSM_COMPANY.LENABLE_CENTER_BS == true) || (data.CBSIS == "I" && _TransactionEntryViewModel.VAR_GSM_COMPANY.LENABLE_CENTER_IS == true);
                }

                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    if (_gridDetailRef.DataSource.Count > 0)
                    {
                        loHeaderData.NDEBIT_AMOUNT = _gridDetailRef.DataSource.Sum(x => x.NDEBIT);
                        loHeaderData.NCREDIT_AMOUNT = _gridDetailRef.DataSource.Sum(x => x.NCREDIT);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void JournalDet_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loHeaderData = (CBT01200DTO)_conductorRef.R_GetCurrentData();
                var loData = (CBT01201DTO)eventArgs.Data;
                var loFirstCenter = _TransactionEntryViewModel.VAR_CENTER_LIST.FirstOrDefault();

                loData.INO = _gridDetailRef.DataSource.Count + 1;
                loData.CREF_NO = loHeaderData.CREF_NO;
                loData.CREF_DATE = loHeaderData.CREF_DATE;
                loData.CDETAIL_DESC = loHeaderData.CTRANS_DESC;
                loData.CCENTER_CODE = loFirstCenter.CCENTER_CODE;
                loData.CCENTER_NAME = loFirstCenter.CCENTER_NAME;
                loData.CDOCUMENT_NO = string.IsNullOrWhiteSpace(loHeaderData.CDOC_NO) ? "" : loHeaderData.CDOC_NO;
                loData.CDOCUMENT_DATE = string.IsNullOrWhiteSpace(loHeaderData.CDOC_DATE) ? "" : loHeaderData.CDOC_DATE ?? ""; // Menangani nilai nullable dengan operator ??
                loData.DDOCUMENT_DATE = _TransactionEntryViewModel.DocDate ?? DateTime.MinValue;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void Before_Open_Lookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            if (eventArgs.ColumnName == nameof(CBT01201DTO.CGLACCOUNT_NO))
            {
                eventArgs.Parameter = new GSL00500ParameterDTO()
                {
                    CPROGRAM_CODE = "GLM00100",
                };
                eventArgs.TargetPageType = typeof(GSL00500);
            }
            else if (eventArgs.ColumnName == nameof(CBT01201DTO.CCASH_FLOW_CODE))
            {
                eventArgs.Parameter = new GSL01500ParameterGroupDTO()
                {
                };
                eventArgs.TargetPageType = typeof(GSL01500);
            }
        }
        private void After_Open_Lookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            CBT01201DTO loGetData = (CBT01201DTO)eventArgs.ColumnData;
            if (eventArgs.ColumnName == nameof(CBT01201DTO.CGLACCOUNT_NO))
            {
                GSL00500DTO loTempResult = (GSL00500DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }

                loGetData.CCASH_FLOW_CODE = loTempResult.CCASH_FLOW_CODE;
                loGetData.CCASH_FLOW_GROUP_CODE = loTempResult.CCASH_FLOW_GROUP_CODE;
                loGetData.CCASH_FLOW_NAME = loTempResult.CCASH_FLOW_NAME;
                loGetData.CGLACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
                loGetData.CGLACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;
                loGetData.CBSIS = loTempResult.CBSIS;
                if (loTempResult.CBSIS == "I" && _TransactionEntryViewModel.VAR_GSM_COMPANY.LENABLE_CENTER_IS == false || loTempResult.CBSIS == "B" && _TransactionEntryViewModel.VAR_GSM_COMPANY.LENABLE_CENTER_BS == false)
                {
                    loGetData.CCENTER_CODE = "";
                    loGetData.CCENTER_NAME = "";
                }
            }
            else if (eventArgs.ColumnName == nameof(CBT01201DTO.CCASH_FLOW_CODE))
            {
                GSL01500DTO loTempResult = R_FrontUtility.ConvertObjectToObject<GSL01500DTO>(eventArgs.Result);
                if (loTempResult == null)
                {
                    return;
                }

                loGetData.CCASH_FLOW_GROUP_CODE = loTempResult.CCASH_FLOW_GROUP_CODE;
                loGetData.CCASH_FLOW_CODE = loTempResult.CCASH_FLOW_CODE;
                loGetData.CCASH_FLOW_NAME = loTempResult.CCASH_FLOW_NAME;
            }

        }
        private async Task JournalDet_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<CBT01210ParamDTO>(eventArgs.Data);
                await _TransactionEntryViewModel.DeleteJournalDetail(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void JournalDet_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (CBT01201DTO)eventArgs.Data;
                if (string.IsNullOrWhiteSpace(loData.CGLACCOUNT_NO))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V22"));
                }


                if (_gridDetailRef.DataSource.Any(x => x.CGLACCOUNT_NO == loData.CGLACCOUNT_NO))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V23"));
                }

                if (string.IsNullOrWhiteSpace(loData.CCENTER_CODE) && (loData.CBSIS == "B" && _TransactionEntryViewModel.VAR_GSM_COMPANY.LENABLE_CENTER_BS) || (loData.CBSIS == "I" && _TransactionEntryViewModel.VAR_GSM_COMPANY.LENABLE_CENTER_IS))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V24"));
                }

                if (string.IsNullOrWhiteSpace(loData.CCASH_FLOW_CODE) && _TransactionEntryViewModel.VAR_GSM_COMPANY.LCASH_FLOW)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V25"));
                }

                if (loData.NDEBIT == 0 && loData.NCREDIT == 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V26"));
                }

                if (loData.NDEBIT > 0 && loData.NCREDIT > 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V27"));
                }

                if (string.IsNullOrWhiteSpace(loData.CDETAIL_DESC))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V28"));
                }

                if (string.IsNullOrWhiteSpace(loData.CDOCUMENT_NO))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V29"));
                }

                if (loData.DDOCUMENT_DATE.HasValue == false)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V30"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void JournalDet_Saving(R_SavingEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task JournalDet_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loHeaderData = (CBT01200DTO)_conductorRef.R_GetCurrentData();
                var loData = R_FrontUtility.ConvertObjectToObject<CBT01210ParamDTO>(eventArgs.Data);
                
                loData.CDOCUMENT_DATE = loData.DDOCUMENT_DATE.Value.ToString("yyyyMMdd");
                loData.CDBCR = loData.NDEBIT > 0 && loData.NCREDIT == 0 ? "D" : loData.NCREDIT > 0 && loData.NDEBIT == 0 ? "C" : "";
                loData.NTRANS_AMOUNT = loData.NDEBIT > 0 ? loData.NDEBIT : loData.NCREDIT;
                loData.CPARENT_ID= loHeaderData.CREC_ID;
                loData.CDEPT_CODE = loHeaderData.CDEPT_CODE;
                loData.CREF_NO = loHeaderData.CREF_NO;
                loData.CREF_DATE = loHeaderData.CREF_DATE;
                loData.CCURRENCY_CODE = loHeaderData.CCURRENCY_CODE;
           
                await _TransactionEntryViewModel.SaveJournalDetail(loData, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _TransactionEntryViewModel.Journal;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task JournalDet_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            bool llInputType = false;
            bool llValidateType = false;

            try
            {
                var loData = (CBT01210ParamDTO)eventArgs.Data;
                if (loData.CINPUT_TYPE == "A")
                {
                    await R_MessageBox.Show("", _localizer["N06"], R_eMessageBoxButtonType.OK);
                    llInputType = true;
                }

                eventArgs.Cancel = llInputType || llValidateType;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task JournalDet_BeforeDelete(R_BeforeDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            bool llInputType = false;
            bool llValidateType = false;

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<CBT01210ParamDTO>(eventArgs.Data);
                if (loData.CINPUT_TYPE == "A")
                {
                    await R_MessageBox.Show("", _localizer["N05"], R_eMessageBoxButtonType.OK);
                    llInputType = true;
                }

                var loValidate = await R_MessageBox.Show("", _localizer["Q07"], R_eMessageBoxButtonType.YesNo);
                llValidateType = loValidate == R_eMessageBoxResult.No;

                eventArgs.Cancel = llInputType || llValidateType;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Process
        private async Task ApproveJournalProcess()
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (CBT01200JournalHDParam)_conductorRef.R_GetCurrentData();
                if (!loData.LALLOW_APPROVE)
                {
                    loEx.Add("", _localizer["N01"]);
                    goto EndBlock;
                }

                var loParam = R_FrontUtility.ConvertObjectToObject<CBT01200UpdateStatusDTO>(loData);
                loParam.LAUTO_COMMIT = false;
                loParam.LUNDO_COMMIT = false;
                loParam.CNEW_STATUS = "20";

                await _TransactionListViewModel.UpdateJournalStatus(loParam);
                await _conductorRef.R_GetEntity(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        private async Task CommitJournalProcess()
        {
            var loEx = new R_Exception();
            R_eMessageBoxResult loValidate;

            try
            {
                var loData = (CBT01200JournalHDParam)_conductorRef.R_GetCurrentData();

                if (loData.CSTATUS == "80")
                {
                    loValidate = await R_MessageBox.Show("", _localizer["Q01"], R_eMessageBoxButtonType.YesNo);
                    if (loValidate == R_eMessageBoxResult.No)
                        goto EndBlock;
                }
                else
                {
                    loValidate = await R_MessageBox.Show("", _localizer["Q02"], R_eMessageBoxButtonType.YesNo);
                    if (loValidate == R_eMessageBoxResult.No)
                        goto EndBlock;
                }

                var loParam = R_FrontUtility.ConvertObjectToObject<CBT01200UpdateStatusDTO>(loData);
                loParam.LAUTO_COMMIT = false;
                loParam.LUNDO_COMMIT = loData.CSTATUS == "80" ? true : false;
                loParam.CNEW_STATUS = loData.CSTATUS == "80" ? "10" : "80";

                await _TransactionListViewModel.UpdateJournalStatus(loParam);
                await _conductorRef.R_GetEntity(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        private async Task SubmitJournalProcess()
        {
            var loEx = new R_Exception();
            R_eMessageBoxResult loResult;
            bool llValidate = false;
            try
            {
                var loData = (CBT01200JournalHDParam)_conductorRef.R_GetCurrentData();

                if (loData.CSTATUS == "00" && int.Parse(loData.CREF_PRD) < int.Parse(_TransactionEntryViewModel.VAR_CB_SYSTEM_PARAM.CSOFT_PERIOD))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V18"));
                    llValidate = true;
                }

                if ((loData.NDEBIT_AMOUNT > 0 || loData.NCREDIT_AMOUNT > 0) && loData.NCREDIT_AMOUNT != loData.NDEBIT_AMOUNT)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V19"));
                    llValidate = true;
                }

                if (loData.NDEBIT_AMOUNT == 0 || loData.NCREDIT_AMOUNT == 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                         typeof(Resources_Dummy_Class),
                         "V20"));
                    llValidate = true;
                }

                if (loData.NDEBIT_AMOUNT > 0 && loData.NDEBIT_AMOUNT != loData.NTRANS_AMOUNT)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                         typeof(Resources_Dummy_Class),
                         "V21"));
                    llValidate = true;
                }

                if (llValidate == true)
                {
                    goto EndBlock;
                }
                else
                {
                    if (loData.CSTATUS == "10")
                    {
                        loResult = await R_MessageBox.Show("", _localizer["Q01"], R_eMessageBoxButtonType.YesNo);
                        if (loResult == R_eMessageBoxResult.No)
                            goto EndBlock;
                    }
                    else
                    {
                        loResult = await R_MessageBox.Show("", _localizer["Q01"], R_eMessageBoxButtonType.YesNo);
                        if (loResult == R_eMessageBoxResult.No)
                            goto EndBlock;
                    }

                    var loParam = R_FrontUtility.ConvertObjectToObject<CBT01200UpdateStatusDTO>(loData);
                    loParam.LAUTO_COMMIT = false;
                    loParam.LUNDO_COMMIT = false;
                    loParam.CNEW_STATUS = loData.CSTATUS == "00" ? "10" : "00";

                    await _TransactionListViewModel.UpdateJournalStatus(loParam);
                    await _conductorRef.R_GetEntity(loData);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Print
        private void Before_Open_lookupPrint(R_BeforeOpenLookupEventArgs eventArgs)
        {
            //var loData = (GLT00110DTO)_conductorRef.R_GetCurrentData();
            //var param = new GLTR00100DTO()
            //{
            //    CREC_ID = loData.CREC_ID
            //};
            //eventArgs.Parameter = param;
            //eventArgs.TargetPageType = typeof(GLTR00100);
        }
        #endregion

        #region lookupDept
        private async Task DeptCode_OnLostFocus(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (CBT01200DTO)_conductorRef.R_GetCurrentData();
                if (loData.CDEPT_CODE.Length > 0)
                {
                    GSL00700ParameterDTO loParam = new GSL00700ParameterDTO()
                    {
                        CSEARCH_TEXT = loData.CDEPT_CODE
                    };

                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel();

                    var loResult = await loLookupViewModel.GetDepartment(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loData.CDEPT_NAME = "";
                        goto EndBlock;
                    }
                    loData.CDEPT_CODE = loResult.CDEPT_CODE;
                    loData.CDEPT_NAME = loResult.CDEPT_NAME;
                }
                else
                {
                    loData.CDEPT_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void Before_Open_lookupDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00700ParameterDTO
            {
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00700);
        }
        private void After_Open_lookupDept(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loData = (CBT01200DTO)_conductorRef.R_GetCurrentData();
            loData.CDEPT_CODE = loTempResult.CDEPT_CODE;
            loData.CDEPT_NAME = loTempResult.CDEPT_NAME;
        }
        #endregion

        #region lookupCashCode
        private async Task CashCode_OnLostFocus(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (CBT01200DTO)_conductorRef.R_GetCurrentData();
                if (loData.CCB_CODE.Length > 0)
                {
                    GSL02500ParameterDTO loParam = new GSL02500ParameterDTO()
                    {
                        CDEPT_CODE = loData.CDEPT_CODE,
                        CCB_TYPE = "B",
                        CBANK_TYPE = "I",
                        CSEARCH_TEXT = loData.CCB_CODE
                    };

                    LookupGSL02500ViewModel loLookupViewModel = new LookupGSL02500ViewModel();

                    var loResult = await loLookupViewModel.GetCB(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loData.CCB_NAME = "";
                        loData.CCURRENCY_CODE = "";
                        loData.CCB_ACCOUNT_NO = "";
                        goto EndBlock;
                    }
                    loData.CCB_CODE = loResult.CCB_CODE;
                    loData.CCB_NAME = loResult.CCB_NAME;
                    loData.CCURRENCY_CODE = loResult.CCURRENCY_CODE;
                    loData.CCB_ACCOUNT_NO = loResult.CCB_ACCOUNT_NO;

                    if (!string.IsNullOrWhiteSpace(loData.CCURRENCY_CODE) &&
                    (loData.CCURRENCY_CODE != _TransactionEntryViewModel.VAR_GSM_COMPANY.CLOCAL_CURRENCY_CODE
                    || loData.CCURRENCY_CODE != _TransactionEntryViewModel.VAR_GSM_COMPANY.CBASE_CURRENCY_CODE))
                    {
                        await RefreshCurrency();
                    }
                    else
                    {
                        loData.NLBASE_RATE = 1;
                        loData.NLCURRENCY_RATE = 1;
                        loData.NBBASE_RATE = 1;
                        loData.NBCURRENCY_RATE = 1;
                    }
                }
                else
                {
                    loData.CCB_NAME = "";
                    loData.CCURRENCY_CODE = "";
                    loData.CCB_ACCOUNT_NO = "";
                    loData.CCB_ACCOUNT_NAME = "";

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void BeforeOpenLookup_BankCode(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loData = (CBT01200DTO)_conductorRef.R_GetCurrentData();
            GSL02500ParameterDTO loParam = new GSL02500ParameterDTO()
            {
                CDEPT_CODE = loData.CDEPT_CODE,
                CCB_TYPE = "B",
                CBANK_TYPE = "I"
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL02500);
        }
        private async Task AfterOpenLookup_BankCode(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL02500DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loData = (CBT01200DTO)_conductorRef.R_GetCurrentData();
            loData.CCB_CODE = loTempResult.CCB_CODE;
            loData.CCB_NAME = loTempResult.CCB_NAME;
            loData.CCURRENCY_CODE = loTempResult.CCURRENCY_CODE;
            loData.CCB_ACCOUNT_NO = loTempResult.CCB_ACCOUNT_NO;
            if (!string.IsNullOrWhiteSpace(loData.CCURRENCY_CODE) &&
                    (loData.CCURRENCY_CODE != _TransactionEntryViewModel.VAR_GSM_COMPANY.CLOCAL_CURRENCY_CODE
                    || loData.CCURRENCY_CODE != _TransactionEntryViewModel.VAR_GSM_COMPANY.CBASE_CURRENCY_CODE))
            {
                await RefreshCurrency();
            }
            else
            {
                loData.NLBASE_RATE = 1;
                loData.NLCURRENCY_RATE = 1;
                loData.NBBASE_RATE = 1;
                loData.NBCURRENCY_RATE = 1;
            }
        }

        private async Task GlAccountNo_OnLostFocus(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (CBT01200DTO)_conductorRef.R_GetCurrentData();
                if (loData.CCB_CODE.Length > 0)
                {
                    GSL02500ParameterDTO loParam = new GSL02500ParameterDTO()
                    {
                        CDEPT_CODE = loData.CDEPT_CODE,
                        CCB_TYPE = "B",
                        CBANK_TYPE = "I",
                        CSEARCH_TEXT = loData.CCB_CODE
                    };

                    LookupGSL02500ViewModel loLookupViewModel = new LookupGSL02500ViewModel();

                    var loResult = await loLookupViewModel.GetCB(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loData.CCB_NAME = "";
                        loData.CCURRENCY_CODE = "";
                        loData.CCB_ACCOUNT_NO = "";
                        goto EndBlock;
                    }
                    loData.CCURRENCY_CODE = loResult.CCURRENCY_CODE;
                    loData.CCB_ACCOUNT_NO = loResult.CCB_ACCOUNT_NO;
                    loData.CCB_ACCOUNT_NAME = loResult.CCB_ACCOUNT_NAME;

                    if (!string.IsNullOrWhiteSpace(loData.CCURRENCY_CODE) &&
                    (loData.CCURRENCY_CODE != _TransactionEntryViewModel.VAR_GSM_COMPANY.CLOCAL_CURRENCY_CODE
                    || loData.CCURRENCY_CODE != _TransactionEntryViewModel.VAR_GSM_COMPANY.CBASE_CURRENCY_CODE))
                    {
                        await RefreshCurrency();
                    }
                    else
                    {
                        loData.NLBASE_RATE = 1;
                        loData.NLCURRENCY_RATE = 1;
                        loData.NBBASE_RATE = 1;
                        loData.NBCURRENCY_RATE = 1;
                    }
                }
                else
                {
                    loData.CCB_NAME = "";
                    loData.CCURRENCY_CODE = "";
                    loData.CCB_ACCOUNT_NO = "";
                    loData.CCB_ACCOUNT_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void BeforeOpenLookup_GLAccountNo(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loData = (CBT01200DTO)_conductorRef.R_GetCurrentData();
            GSL02600ParameterDTO loParam = new GSL02600ParameterDTO()
            {
                CDEPT_CODE = loData.CDEPT_CODE,
                CCB_CODE = loData.CCB_CODE,
                CCB_TYPE = "B",
                CBANK_TYPE = "I",

            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL02600);
        }
        private async Task AfterOpenLookup_GLAccountNo(R_AfterOpenLookupEventArgs eventArgs)
        {
            //IF Currency Code Not Empty AND(Currency Code<> VAR_GSM_COMPANY.CLOCAL_CURRENCY_CODE OR selected Currency Code<> VAR_GSM_COMPANY.CBASE_CURRENCY_CODE)
            var loTempResult = (GSL02600DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loData = (CBT01200DTO)_conductorRef.R_GetCurrentData();
            loData.CCB_ACCOUNT_NO = loTempResult.CCB_ACCOUNT_NO;
            loData.CCB_ACCOUNT_NAME = loTempResult.CCB_ACCOUNT_NAME;
            loData.CCURRENCY_CODE = loTempResult.CCURRENCY_CODE;
            if (!string.IsNullOrWhiteSpace(loData.CCURRENCY_CODE) &&
                    (loData.CCURRENCY_CODE != _TransactionEntryViewModel.VAR_GSM_COMPANY.CLOCAL_CURRENCY_CODE
                    || loData.CCURRENCY_CODE != _TransactionEntryViewModel.VAR_GSM_COMPANY.CBASE_CURRENCY_CODE))
            {
                await RefreshCurrency();
            }
            else
            {
                loData.NLBASE_RATE = 1;
                loData.NLCURRENCY_RATE = 1;
                loData.NBBASE_RATE = 1;
                loData.NBCURRENCY_RATE = 1;
            }
        }
        #endregion

        #region Result Predifiend

        #endregion
      */
    }
}

