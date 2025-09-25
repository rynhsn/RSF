using BlazorClientHelper;
using CBT01100COMMON;
using CBT01100MODEL;
using CBT01100FrontResources;
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
using Lookup_GSFRONT;
using System.Globalization;
using R_BlazorFrontEnd.Enums;
using CBT01100COMMON.DTO_s.CBT01100;
using CBT01100COMMON.DTO_s.CBT01110;
using System.Xml.Linq;
using System;

namespace CBT01100FRONT
{
    public partial class CBT01110 : R_Page
    {
        private CBT01100ViewModel _TransactionListViewModel = new();
        private CBT01110ViewModel _TransactionEntryViewModel = new();
        private R_Conductor _conductorRef;
        private R_ConductorGrid _conductorDetailRef;
        private R_Grid<CBT01101DTO> _gridDetailRef;

        [Inject] private IClientHelper _clientHelper { get; set; }
        [Inject] private R_ILocalizer<CBT01100FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private R_Lookup R_LookupBtnPrint;

        private R_Lookup R_LookupBtnDept;

        private R_TextBox _txt_CDEPT_CODE;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _TransactionEntryViewModel.GetAllUniversalData();
                var loParam = R_FrontUtility.ConvertObjectToObject<CBT01100DTO>(poParameter);
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

        protected override async Task<object> R_Set_Result_PredefinedDock()
        {
            R_Exception loEx = new();
            try
            {
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return base.R_Set_Result_PredefinedDock();
        }

        #region Locking

        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlCB";
        private const string DEFAULT_MODULE_NAME = "CB";

        protected override async Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (CBT01100DTO)eventArgs.Data;

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
                        Program_Id = "CBT01100",
                        Table_Name = "CBT_TRANS_HD",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "CBT01100",
                        Table_Name = "CBT_TRANS_HD",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO)
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

        #endregion Locking

        #region Form

        private async Task JournalForm_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<CBT01100JournalHDParam>(eventArgs.Data);
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
                var loParam = R_FrontUtility.ConvertObjectToObject<CBT01100JournalHDParam>(eventArgs.Data);
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

                var loData = (CBT01100JournalHDParam)_conductorRef.R_GetCurrentData();
                var loParam = R_FrontUtility.ConvertObjectToObject<CBT01100UpdateStatusDTO>(loData);
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

        private bool ButtonCopySourceOnClick = false;

        private async Task JournalForm_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                _TransactionEntryViewModel.JournalDetailGridTemp = new(_gridDetailRef.DataSource.ToList()); ;//store detail to temp

                if (ButtonCopySourceOnClick == true)
                {
                    eventArgs.Data = R_FrontUtility.ConvertObjectToObject<CBT01100DTO>(_TransactionListViewModel.JournalRecord);
                    var data = (CBT01100DTO)eventArgs.Data;
                    data.CREF_NO = "";

                    _TransactionEntryViewModel.RefDate = DateTime.ParseExact(data.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                    _TransactionEntryViewModel.DocDate = DateTime.ParseExact(data.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                    ButtonCopySourceOnClick = false;

                    if (_gridDetailRef.DataSource.Count > 0)
                    {
                        _gridDetailRef.DataSource.Clear();
                    }
                }
                else
                {
                    var loData = (CBT01100DTO)eventArgs.Data;
                    loData.CCREATE_BY = _clientHelper.UserId;
                    loData.CUPDATE_BY = _clientHelper.UserId;
                    loData.DUPDATE_DATE = _TransactionEntryViewModel.VAR_TODAY.DTODAY;
                    loData.DCREATE_DATE = _TransactionEntryViewModel.VAR_TODAY.DTODAY;
                    _TransactionListViewModel.RefDate = _TransactionEntryViewModel.VAR_TODAY.DTODAY;
                    _TransactionListViewModel.DocDate = _TransactionEntryViewModel.VAR_TODAY.DTODAY;
                    loData.NLBASE_RATE = 1;
                    loData.NLCURRENCY_RATE = 1;
                    loData.NBBASE_RATE = 1;
                    loData.NBCURRENCY_RATE = 1;
                    loData.CDEPT_CODE = _TransactionEntryViewModel.VAR_GL_SYSTEM_PARAM.CCLOSE_DEPT_CODE;
                    loData.CDEPT_NAME = _TransactionEntryViewModel.VAR_GL_SYSTEM_PARAM.CCLOSE_DEPT_NAME;
                    if (_gridDetailRef.DataSource.Count > 0)
                    {
                        _gridDetailRef.DataSource.Clear();
                    }
                }
                await _DeptCode_TextBox.FocusAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void ValidationFormCBT01100TransactionEntry(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (CBT01100DTO)eventArgs.Data;
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
        private bool EnableAdd = false;
        private bool EnableEdit = false;
        private bool EnableDelete = false;
        private bool EnableSubmit = false;
        private bool EnableApprove = false;
        private bool EnableCommit = false;
        private bool EnableHaveRecId = false;
        private bool _isGridDetailOnCrudMode;
        private bool _isHeaderDataOnCrudMode;
        private bool CheckOpenLookup = false;

        #endregion Private Property

        private async Task JournalForm_RDisplay(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var data = (CBT01100DTO)eventArgs.Data;
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    if (!string.IsNullOrWhiteSpace(data.CSTATUS))
                    {
                        EnabledPrintButton = int.Parse(data.CSTATUS) > 0 && int.Parse(data.CSTATUS) <= 80;
                        lcLabelCommit = data.CSTATUS == "80" ? _localizer["_UndoCommit"] : _localizer["_Commit"];
                        lcLabelSubmit = data.CSTATUS == "10" ? _localizer["_UndoSubmit"] : _localizer["_Submit"];
                        EnableEdit = data.CSTATUS == "00";
                        EnableDelete = data.CSTATUS == "00";
                        EnableSubmit = data.CSTATUS == "00" || data.CSTATUS == "10";
                        EnableApprove = data.CSTATUS == "10" && _TransactionEntryViewModel.VAR_GSM_TRANSACTION_CODE.LAPPROVAL_FLAG == true;
                        EnableCommit = (data.CSTATUS == "30" || data.CSTATUS == "80") && int.Parse(data.CREF_PRD) >= int.Parse(_TransactionEntryViewModel.VAR_GL_SYSTEM_PARAM.CSOFT_PERIOD);

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

        private void JrnForm_ConvertToGridEntity(R_ConvertToGridEntityEventArgs eventArgs)
        {
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

        private async Task JournalForm_CopyProcess()
        {
            var loEx = new R_Exception();
            try
            {
                ButtonCopySourceOnClick = true;
                await _conductorRef.Add();
                //var loOldData = (CBT01100JournalHDParam)_conductorRef.R_GetCurrentData();
                //await _conductorRef.Add();
                //if (_conductorRef.R_ConductorMode == R_eConductorMode.Add)
                //{
                //    var loNewData = (CBT01100JournalHDParam)_conductorRef.R_GetCurrentData();

                //    loNewData.CREF_PRD = loOldData.CREF_PRD;
                //    loNewData.LALLOW_APPROVE = loOldData.LALLOW_APPROVE;
                //    loNewData.CINPUT_TYPE = loOldData.CINPUT_TYPE;
                //    loNewData.CDEPT_CODE = loOldData.CDEPT_CODE;
                //    _TransactionListViewModel.RefDate = DateTime.ParseExact(loOldData.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                //    loNewData.CCB_CODE = loOldData.CCB_CODE;
                //    loNewData.CCB_NAME = loOldData.CCB_NAME;
                //    loNewData.CCB_ACCOUNT_NO = loOldData.CCB_ACCOUNT_NO;
                //    loNewData.CCURRENCY_CODE = loOldData.CCURRENCY_CODE;
                //    loNewData.NTRANS_AMOUNT = loOldData.NTRANS_AMOUNT;
                //    loNewData.NLBASE_RATE = loOldData.NLBASE_RATE;
                //    loNewData.CCURRENCY_CODE = loOldData.CCURRENCY_CODE;
                //    loNewData.NLCURRENCY_RATE = loOldData.NLCURRENCY_RATE;
                //    loNewData.NBBASE_RATE = loOldData.NBBASE_RATE;
                //    loNewData.NBCURRENCY_RATE = loOldData.NBCURRENCY_RATE;
                //    loNewData.NDEBIT_AMOUNT = loOldData.NDEBIT_AMOUNT;
                //    loNewData.NCREDIT_AMOUNT = loOldData.NCREDIT_AMOUNT;
                //    loNewData.CDOC_NO = loOldData.CDOC_NO;
                //    _TransactionListViewModel.DocDate = DateTime.ParseExact(loOldData.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                //    loNewData.CTRANS_DESC = loOldData.CTRANS_DESC;
                //    loNewData.CSTATUS_NAME = loOldData.CSTATUS_NAME;
                //    loNewData.CSTATUS = loOldData.CSTATUS;
                //    loNewData.CUPDATE_BY = loOldData.CUPDATE_BY;
                //    loNewData.DUPDATE_DATE = loOldData.DUPDATE_DATE;
                //    loNewData.CCREATE_BY = loOldData.CCREATE_BY;
                //    loNewData.DCREATE_DATE = loOldData.DCREATE_DATE;
                //}
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task JournalForm_SetOther(R_SetEventArgs eventArgs)
        {
            _isHeaderDataOnCrudMode = eventArgs.Enable;
        }

        #endregion Form

        #region Refresh Currency Method

        public async Task RefreshCurrency()
        {
            var loEx = new R_Exception();
            try
            {
                _TransactionListViewModel.VAR_CB_SYSTEM_PARAM = _TransactionEntryViewModel.VAR_CB_SYSTEM_PARAM;
                var loParam = R_FrontUtility.ConvertObjectToObject<CBT01110LastCurrencyRateDTO>(_conductorRef.R_GetCurrentData());
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

        #endregion Refresh Currency Method

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

        #endregion Ref Date OnLostFocus

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
                var loData = R_FrontUtility.ConvertObjectToObject<CBT01110ParamDTO>(eventArgs.Data);
                await _TransactionEntryViewModel.GetJournalDetailRecord(loData);
                eventArgs.Result = _TransactionEntryViewModel.Journal;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void JournalDet_RDisplay(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loHeaderData = (CBT01100DTO)_conductorRef.R_GetCurrentData();

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
                var loHeaderData = (CBT01100DTO)_conductorRef.R_GetCurrentData();
                var loData = (CBT01101DTO)eventArgs.Data;
                var loFirstCenter = _TransactionEntryViewModel.VAR_CENTER_LIST.FirstOrDefault();

                loData.INO = _gridDetailRef.DataSource.Count + 1;
                loData.CREF_NO = loHeaderData.CREF_NO;
                loData.CREF_DATE = loHeaderData.CREF_DATE;
                loData.CDETAIL_DESC = loHeaderData.CTRANS_DESC;
                loData.CCENTER_CODE = loFirstCenter.CCENTER_CODE;
                loData.CCENTER_NAME = loFirstCenter.CCENTER_NAME;
                loData.CDOCUMENT_NO = string.IsNullOrWhiteSpace(loHeaderData.CDOC_NO) ? "" : loHeaderData.CDOC_NO;
                loData.CDOCUMENT_DATE = string.IsNullOrWhiteSpace(loHeaderData.CDOC_DATE) ? "" : loHeaderData.CDOC_DATE ?? ""; // Menangani nilai nullable dengan operator ??
                loData.DDOCUMENT_DATE = _TransactionListViewModel.DocDate ?? null;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void JournalDet_BeforeOpenGridLookupColumn(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            if (eventArgs.ColumnName == nameof(CBT01101DTO.CGLACCOUNT_NO))
            {
                eventArgs.Parameter = new GSL00500ParameterDTO()
                {
                    CPROGRAM_CODE = "GLM00100",
                };
                eventArgs.TargetPageType = typeof(GSL00500);
            }
            else if (eventArgs.ColumnName == nameof(CBT01101DTO.CCASH_FLOW_CODE))
            {
                eventArgs.Parameter = new GSL01500ParameterGroupDTO()
                {
                };
                eventArgs.TargetPageType = typeof(GSL01500);
            }
        }

        private void JournalDet_AfterOpenGridLookupColumn(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            CBT01101DTO loGetData = (CBT01101DTO)eventArgs.ColumnData;
            switch (eventArgs.ColumnName)
            {
                case nameof(CBT01101DTO.CGLACCOUNT_NO):
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
                        if (loTempResult.CBSIS == "B" && _TransactionEntryViewModel.VAR_GSM_COMPANY.LENABLE_CENTER_IS)
                        {
                            loGetData.CCENTER_CODE = null;
                            loGetData.CCENTER_NAME = null;
                        }
                        CheckOpenLookup = true;
                        break;
                    }

                case nameof(CBT01101DTO.CCASH_FLOW_CODE):
                    {
                        GSL01500DTO loTempResult = R_FrontUtility.ConvertObjectToObject<GSL01500DTO>(eventArgs.Result);
                        if (loTempResult == null)
                        {
                            return;
                        }
                        loGetData.CCASH_FLOW_GROUP_CODE = loTempResult.CCASH_FLOW_GROUP_CODE ?? "";
                        loGetData.CCASH_FLOW_CODE = loTempResult.CCASH_FLOW_CODE ?? "";
                        loGetData.CCASH_FLOW_NAME = loTempResult.CCASH_FLOW_NAME ?? "";
                        break;
                    }
            }
        }

        private async Task JournalDet_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<CBT01110ParamDTO>(eventArgs.Data);
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
                var loData = (CBT01101DTO)eventArgs.Data;
                if (string.IsNullOrWhiteSpace(loData.CGLACCOUNT_NO))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V22"));
                }

                if (eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    if (_gridDetailRef.DataSource.Any(x => x.CGLACCOUNT_NO == loData.CGLACCOUNT_NO))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "V23"));
                    }
                }

                if (string.IsNullOrWhiteSpace(loData.CCENTER_CODE) && !(loData.CBSIS == "B" && _TransactionEntryViewModel.VAR_GSM_COMPANY.LENABLE_CENTER_IS))
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

                if (eventArgs.ConductorMode == R_eConductorMode.Add && string.IsNullOrWhiteSpace(loData.CDOCUMENT_NO))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V29"));
                }

                if (eventArgs.ConductorMode == R_eConductorMode.Add && loData.DDOCUMENT_DATE.HasValue == false)
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
                var loData = (CBT01101DTO)eventArgs.Data;
                loData.CCENTER_CODE = string.IsNullOrEmpty(loData.CCENTER_CODE) ? "" : loData.CCENTER_CODE;
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
                var loHeaderData = (CBT01100DTO)_conductorRef.R_GetCurrentData();
                var loData = R_FrontUtility.ConvertObjectToObject<CBT01110ParamDTO>(eventArgs.Data);

                loData.CDOCUMENT_DATE = _TransactionListViewModel.DocDate.HasValue ? _TransactionListViewModel.DocDate.Value.ToString("yyyyMMdd") : "";
                loData.CDBCR = loData.NDEBIT > 0 && loData.NCREDIT == 0 ? "D" : loData.NCREDIT > 0 && loData.NDEBIT == 0 ? "C" : "";
                loData.NTRANS_AMOUNT = loData.NDEBIT > 0 ? loData.NDEBIT : loData.NCREDIT;
                loData.CPARENT_ID = loHeaderData.CREC_ID;
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

        private async Task JournalDetail_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loHeaderData = (CBT01100DTO)_conductorRef.R_GetCurrentData();
                await _conductorRef.R_GetEntity(loHeaderData);
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
                var loData = R_FrontUtility.ConvertObjectToObject<CBT01110ParamDTO>(eventArgs.Data);
                //var loData = R_FrontUtility.
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
                var loData = R_FrontUtility.ConvertObjectToObject<CBT01110ParamDTO>(eventArgs.Data);
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

        private async Task JournalDet_CellValueChangedAsync(R_CellValueChangedEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loColumn = eventArgs.ColumnName;
                var loData = (CBT01101DTO)eventArgs.CurrentRow;
                switch (loColumn)
                {
                    case nameof(CBT01110ParamDTO.CGLACCOUNT_NO):
                        string lcSearchText = eventArgs.Value as string;
                        var loColCenter = eventArgs.Columns.FirstOrDefault(x => x.FieldName == nameof(CBT01101DTO.CCENTER_CODE));
                        if (CheckOpenLookup)
                        {
                            if (loColCenter != null)
                            {
                                loColCenter.Enabled = !(loData.CBSIS == "B" && _TransactionEntryViewModel.VAR_GSM_COMPANY.LENABLE_CENTER_IS);
                            }
                            CheckOpenLookup = false;
                        }
                        else
                        {
                            if (lcSearchText.Length >= 3)
                            {
                                LookupGSL00500ViewModel loLookupViewModel = new();

                                var loResult = await loLookupViewModel.GetGLAccount(new()
                                {
                                    CPROGRAM_CODE = "GLM00100",
                                    CBSIS = "",
                                    CDBCR = "",
                                    LCENTER_RESTR = false,
                                    LUSER_RESTR = false,
                                    CCENTER_CODE = "",
                                    CGOA_CODE = "",
                                    CSEARCH_TEXT = lcSearchText ?? "",
                                    CCOMPANY_ID = _clientHelper.CompanyId,
                                    CUSER_ID = _clientHelper.UserId,
                                });

                                if (loResult != null)
                                {
                                    loData.CGLACCOUNT_NO = loResult.CGLACCOUNT_NO;
                                    loData.CGLACCOUNT_NAME = loResult.CGLACCOUNT_NAME;
                                    loData.CBSIS = loResult.CBSIS.Trim();

                                    //reset center
                                    if (loResult.CBSIS == "B" && !_TransactionEntryViewModel.VAR_GSM_COMPANY.LENABLE_CENTER_IS)
                                    {
                                        loData.CCENTER_CODE = "";
                                        loData.CCENTER_NAME = "";
                                    }
                                    //enable/disable center based on companysetting.enablebisis & selected glaccount bsis
                                    if (loColCenter != null)
                                    {
                                        loColCenter.Enabled = !(loData.CBSIS == "B" && _TransactionEntryViewModel.VAR_GSM_COMPANY.LENABLE_CENTER_IS);
                                    }
                                }
                                else
                                {
                                    loData.CGLACCOUNT_NO = "";
                                    loData.CGLACCOUNT_NAME = "";
                                    loData.CBSIS = "";
                                    loData.CCENTER_CODE = "";
                                    loData.CCENTER_NAME = "";
                                }
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void JournalDet_CheckGrid(R_CheckGridEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (CBT01100DTO)_conductorRef.R_GetCurrentData();
                if (loData.CSTATUS != "00")
                {
                    eventArgs.Allow = false;
                }
                else
                {
                    eventArgs.Allow = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void JournalDet_SetOther(R_SetEventArgs eventArgs)
        {
            _isGridDetailOnCrudMode = eventArgs.Enable;
        }

        private void JournalDet_SetEditGridColumn(R_SetEditGridColumnEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loCenterColumn = eventArgs.Columns.FirstOrDefault(x => x.FieldName == nameof(CBT01110ParamDTO.CCENTER_CODE));
                if (loCenterColumn != null)
                {
                    loCenterColumn.Enabled = !(_TransactionEntryViewModel.VAR_GSM_COMPANY.LENABLE_CENTER_IS && (R_FrontUtility.ConvertObjectToObject<CBT01110ParamDTO>(eventArgs.Data).CBSIS == "B"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion Detail

        #region Process

        private async Task ApproveJournalProcess()
        {
            var loEx = new R_Exception();
            R_eMessageBoxResult loValidate;

            try
            {
                var loData = (CBT01100JournalHDParam)_conductorRef.R_GetCurrentData();

                loValidate = await R_MessageBox.Show("", _localizer["Q08"], R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.No)
                    goto EndBlock;

                if (!loData.LALLOW_APPROVE)
                {
                    loEx.Add("", _localizer["N04"]);
                    goto EndBlock;
                }

                var loParam = R_FrontUtility.ConvertObjectToObject<CBT01100UpdateStatusDTO>(loData);
                loParam.LAUTO_COMMIT = false;
                loParam.LUNDO_COMMIT = false;
                loParam.CNEW_STATUS = "30";

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
                var loData = (CBT01100JournalHDParam)_conductorRef.R_GetCurrentData();

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

                var loParam = R_FrontUtility.ConvertObjectToObject<CBT01100UpdateStatusDTO>(loData);
                loParam.LAUTO_COMMIT = false;
                loParam.LUNDO_COMMIT = loData.CSTATUS == "80" ? true : false;
                loParam.CNEW_STATUS = loData.CSTATUS == "80" ? (_TransactionEntryViewModel.VAR_GSM_TRANSACTION_CODE.LAPPROVAL_FLAG ? "10" : "00") : "80";

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
                var loData = (CBT01100JournalHDParam)_conductorRef.R_GetCurrentData();

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

                if (llValidate == true)
                {
                    goto EndBlock;
                }
                else
                {
                    if (loData.CSTATUS == "10")
                    {
                        loResult = await R_MessageBox.Show("", _localizer["Q05"], R_eMessageBoxButtonType.YesNo);
                        if (loResult == R_eMessageBoxResult.No)
                            goto EndBlock;
                    }
                    else
                    {
                        loResult = await R_MessageBox.Show("", _localizer["Q06"], R_eMessageBoxButtonType.YesNo);
                        if (loResult == R_eMessageBoxResult.No)
                            goto EndBlock;
                    }

                    var loParam = R_FrontUtility.ConvertObjectToObject<CBT01100UpdateStatusDTO>(loData);
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

        #endregion Process

        #region Print
        private bool EnabledPrintButton = false;
        private void Before_Open_lookupPrint(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loData = (CBT01100DTO)_conductorRef.R_GetCurrentData();
            var loParam = R_FrontUtility.ConvertObjectToObject<CBR00600FRONT.CBR00600CallerParameterDTO>(loData);
            loParam.CPROGRAM_ID = "CBT01100";

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(CBR00600FRONT.CBR00600);
        }

        #endregion Print

        #region lookupDept

        private async Task DeptCode_OnLostFocus(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (CBT01100DTO)_conductorRef.R_GetCurrentData();
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

            var loData = (CBT01100DTO)_conductorRef.R_GetCurrentData();
            loData.CDEPT_CODE = loTempResult.CDEPT_CODE;
            loData.CDEPT_NAME = loTempResult.CDEPT_NAME;
        }

        #endregion lookupDept

        #region lookupCashCode

        private async Task CashCode_OnLostFocus(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (CBT01100DTO)_conductorRef.R_GetCurrentData();
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
            var loData = (CBT01100DTO)_conductorRef.R_GetCurrentData();
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

            var loData = (CBT01100DTO)_conductorRef.R_GetCurrentData();
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
                var loData = (CBT01100DTO)_conductorRef.R_GetCurrentData();
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
            var loData = (CBT01100DTO)_conductorRef.R_GetCurrentData();
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

            var loData = (CBT01100DTO)_conductorRef.R_GetCurrentData();
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

        #endregion lookupCashCode
    }
}