using BlazorClientHelper;
using PMT04100COMMON;
using PMT04100FrontResources;
using PMT04100MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System.Globalization;
using PMF00100Model.ViewModel;
using PMF00100COMMON.DTOs.PMF00100;
using System.Xml.Linq;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMModel.ViewModel.LML00600;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00800;
using PMF00100FRONT;
using APF00100COMMON.DTOs.APF00100;

namespace PMT04100FRONT
{
    public partial class PMT04110 : R_Page
    {
        private PMT04110ViewModel _JournalEntryViewModel = new();
        private PMF00100ViewModel _AllocationViewModel = new();
        private R_Conductor _conductorRef;
        private R_Grid<PMF00100ListDTO> _gridDetailRef;

        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_ILocalizer<PMT04100FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        #region Private Property
        private string lcLabelSubmit = "Submit";
        private bool EnableStatusDraft = false;
        private bool EnableAllocation = false;
        private bool EnableRedraft = false;
        private bool EnableHaveRecId = false;
        private bool EnableHeaderNormalMode;
        private R_TextBox _DeptCode_TextBox;
        private bool EnableCenterList = true;
        private PMT04100InputParameterDTO _Data;
        private bool HiddenButtonDisplay = false;
        #endregion

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<PMT04100InputParameterDTO>(poParameter);
                loData.CPROPERTY_ID = string.IsNullOrWhiteSpace(loData.CPROPERTY_ID) ? loData.PARAM_CALLER_PROPERTY_ID : loData.CPROPERTY_ID;
                await _JournalEntryViewModel.GetAllUniversalData(loData);
                _Data = loData;

                if (string.IsNullOrWhiteSpace(loData.PARAM_CALLER_ACTION))
                {
                    if (!string.IsNullOrWhiteSpace(loData.CREC_ID))
                    {
                        await _conductorRef.R_GetEntity(loData);
                    }
                }
                else
                {
                    HiddenButtonDisplay = loData.PARAM_CALLER_ACTION == "VIEW_ONLY";
                    if (loData.PARAM_CALLER_ACTION == "NEW")
                    {
                        await _conductorRef.Add();
                    }
                    else
                    {
                        await _conductorRef.R_GetEntity(loData);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Locking
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_MODULE_NAME = "PM";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (PMT04100DTO)eventArgs.Data;

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
                        Program_Id = "PMT04100",
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
                        Program_Id = "PMT04100",
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
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT04100DTO>(eventArgs.Data);

                await _JournalEntryViewModel.GetJournal(loParam);
                eventArgs.Result = _JournalEntryViewModel.Journal;
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
                var loCRUDMode = (eCRUDMode)eventArgs.ConductorMode;
                var loData = (PMT04100DTO)eventArgs.Data;
                loData.CREF_DATE = _JournalEntryViewModel.RefDate.Value.ToString("yyyyMMdd");
                loData.CDOC_DATE = _JournalEntryViewModel.DocDate.Value.ToString("yyyyMMdd");

                await _JournalEntryViewModel.SaveJournal(loData, loCRUDMode);
                eventArgs.Result = _JournalEntryViewModel.Journal;
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

                var loData = (PMT04100DTO)_conductorRef.R_GetCurrentData();
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT04100UpdateStatusDTO>(loData);
                loParam.LAUTO_COMMIT = false;
                loParam.LUNDO_COMMIT = false;
                loParam.CNEW_STATUS = "99";

                await _JournalEntryViewModel.UpdateJournalStatus(loParam);
                await _conductorRef.R_GetEntity(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        private async Task JournalForm_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var data = (PMT04100DTO)eventArgs.Data;

                data.CCREATE_BY = clientHelper.UserId;
                data.CUPDATE_BY = clientHelper.UserId;
                data.DUPDATE_DATE = _JournalEntryViewModel.VAR_TODAY.DTODAY;
                data.DCREATE_DATE = _JournalEntryViewModel.VAR_TODAY.DTODAY;
                data.CPROPERTY_ID = string.IsNullOrWhiteSpace(_Data.CPROPERTY_ID) ? _Data.PARAM_CALLER_PROPERTY_ID : _Data.CPROPERTY_ID;
                _JournalEntryViewModel.RefDate = _JournalEntryViewModel.VAR_TODAY.DTODAY;
                _JournalEntryViewModel.DocDate = null;
                data.CREF_NO = "";
                data.NLBASE_RATE = 1;
                data.NLCURRENCY_RATE = 1;
                data.NBBASE_RATE = 1;
                data.NBCURRENCY_RATE = 1;
                
                await _DeptCode_TextBox.FocusAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private void JournalForm_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (PMT04100DTO)eventArgs.Data;

                if (string.IsNullOrWhiteSpace(loParam.CPROPERTY_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT04100FrontResources.Resources_Dummy_Class),
                        "V00"));
                }

                if (string.IsNullOrWhiteSpace(loParam.CDEPT_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT04100FrontResources.Resources_Dummy_Class),
                        "V01"));
                }

                if (string.IsNullOrWhiteSpace(loParam.CREF_NO) && _JournalEntryViewModel.VAR_GSM_TRANSACTION_CODE.LINCREMENT_FLAG == false && _JournalEntryViewModel.VAR_CB_SYSTEM_PARAM.LCB_NUMBERING == false)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT04100FrontResources.Resources_Dummy_Class),
                        "V02"));
                }

                if (_JournalEntryViewModel.RefDate == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT04100FrontResources.Resources_Dummy_Class),
                        "V03"));
                }
                else
                {
                    if (_JournalEntryViewModel.RefDate > _JournalEntryViewModel.VAR_TODAY.DTODAY)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(PMT04100FrontResources.Resources_Dummy_Class),
                            "V04"));
                    }

                    if (int.Parse(_JournalEntryViewModel.RefDate.Value.ToString("yyyyMMdd")) < int.Parse(_JournalEntryViewModel.VAR_SOFT_PERIOD_START_DATE.CEND_DATE))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(PMT04100FrontResources.Resources_Dummy_Class),
                            "V06"));
                    }
                }

                if (string.IsNullOrWhiteSpace(loParam.CCUST_SUPP_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT04100FrontResources.Resources_Dummy_Class),
                        "V20"));
                }

                if (string.IsNullOrWhiteSpace(loParam.CCB_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT04100FrontResources.Resources_Dummy_Class),
                        "V07"));
                }

                if (loParam.NTRANS_AMOUNT <= 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT04100FrontResources.Resources_Dummy_Class),
                        "V08"));
                }

                if (string.IsNullOrWhiteSpace(loParam.CDOC_NO))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT04100FrontResources.Resources_Dummy_Class),
                        "V09"));
                }

                if (_JournalEntryViewModel.DocDate.HasValue == false)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT04100FrontResources.Resources_Dummy_Class),
                        "V10"));
                }
                else
                {
                    int loRefDate = _JournalEntryViewModel.RefDate.HasValue ? int.Parse(_JournalEntryViewModel.RefDate.Value.ToString("yyyyMMdd")) : 0;

                    if (int.Parse(_JournalEntryViewModel.DocDate.Value.ToString("yyyyMMdd")) > loRefDate)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(PMT04100FrontResources.Resources_Dummy_Class),
                            "V11"));
                    }

                    if (int.Parse(_JournalEntryViewModel.DocDate.Value.ToString("yyyyMMdd")) < int.Parse(_JournalEntryViewModel.VAR_SOFT_PERIOD_START_DATE.CEND_DATE))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(PMT04100FrontResources.Resources_Dummy_Class),
                            "V12"));
                    }
                }

                if (string.IsNullOrWhiteSpace(loParam.CTRANS_DESC))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT04100FrontResources.Resources_Dummy_Class),
                        "V13"));
                }

                if (loParam.NLBASE_RATE <= 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT04100FrontResources.Resources_Dummy_Class),
                        "V14"));
                }

                if (loParam.NLCURRENCY_RATE <= 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT04100FrontResources.Resources_Dummy_Class),
                        "V15"));
                }

                if (loParam.NBBASE_RATE <= 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT04100FrontResources.Resources_Dummy_Class),
                        "V16"));
                }

                if (loParam.NBCURRENCY_RATE <= 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT04100FrontResources.Resources_Dummy_Class),
                        "V17"));
                }

                if (string.IsNullOrWhiteSpace(loParam.CCASH_FLOW_CODE) && _JournalEntryViewModel.VAR_GSM_COMPANY.LCASH_FLOW == true)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT04100FrontResources.Resources_Dummy_Class),
                        "V18"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task JournalForm_RDisplay(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var data = (PMT04100DTO)eventArgs.Data;
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    if (data != null)
                    {
                        EnableHaveRecId = !string.IsNullOrWhiteSpace(data.CREC_ID);

                        if (!string.IsNullOrWhiteSpace(data.CSTATUS))
                        {
                            EnableStatusDraft = data.CSTATUS == "00";
                            EnableAllocation = data.CSTATUS == "80";
                            EnableRedraft = data.CSTATUS == "10";
                        }
                        if (!string.IsNullOrWhiteSpace(data.CREC_ID))
                        {
                            await _gridDetailRef.R_RefreshGrid(data);
                        }
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
            if (res == R_eMessageBoxResult.Yes)
            {
                if (_Data.PARAM_CALLER_ACTION == "NEW")
                {
                    await this.CloseDetailAsync();
                }
            }
        }
        private void JournalForm_SetOther(R_SetEventArgs eventArgs)
        {
            EnableHeaderNormalMode = eventArgs.Enable;
        }
        private void JournalForm_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Add && _Data.PARAM_CALLER_ACTION == "NEW")
            {
                _Data.PARAM_CALLER_ACTION = "VIEW";
            }
        }
        #endregion

        #region Detail
        private async Task JournalDet_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                _AllocationViewModel.loAllocationParameter = R_FrontUtility.ConvertObjectToObject<OpenAllocationParameterDTO>(eventArgs.Parameter);

                await _AllocationViewModel.GetAllocationListStreamAsync();

                eventArgs.ListEntityResult = _AllocationViewModel.loAllocationList;
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
                var loResult = await _JournalEntryViewModel.GetLastCurrencyRate();

                if (loResult is null)
                {
                    _JournalEntryViewModel.Data.NLBASE_RATE = 1;
                    _JournalEntryViewModel.Data.NLCURRENCY_RATE = 1;
                    _JournalEntryViewModel.Data.NBBASE_RATE = 1;
                    _JournalEntryViewModel.Data.NBCURRENCY_RATE = 1;
                }
                else
                {
                    _JournalEntryViewModel.Data.NLBASE_RATE = loResult.NLBASE_RATE_AMOUNT;
                    _JournalEntryViewModel.Data.NLCURRENCY_RATE = loResult.NLCURRENCY_RATE_AMOUNT;
                    _JournalEntryViewModel.Data.NBBASE_RATE = loResult.NBBASE_RATE_AMOUNT;
                    _JournalEntryViewModel.Data.NBCURRENCY_RATE = loResult.NBCURRENCY_RATE_AMOUNT;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Value Change
        private async Task RefDate_ValueChange(DateTime? poParam)
        {
            var loEx = new R_Exception();
            try
            {
                _JournalEntryViewModel.RefDate = poParam;
                if (!string.IsNullOrWhiteSpace(_JournalEntryViewModel.Data.CCURRENCY_CODE) &&
                    (_JournalEntryViewModel.Data.CCURRENCY_CODE != _JournalEntryViewModel.VAR_GSM_COMPANY.CLOCAL_CURRENCY_CODE
                    || _JournalEntryViewModel.Data.CCURRENCY_CODE != _JournalEntryViewModel.VAR_GSM_COMPANY.CBASE_CURRENCY_CODE))
                {
                    await RefreshCurrency();
                }
                else
                {
                    _JournalEntryViewModel.Data.NLBASE_RATE = 1;
                    _JournalEntryViewModel.Data.NLCURRENCY_RATE = 1;
                    _JournalEntryViewModel.Data.NBBASE_RATE = 1;
                    _JournalEntryViewModel.Data.NBCURRENCY_RATE = 1;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region BTN Process
        private async Task SubmitJournalProcess()
        {
            var loEx = new R_Exception();
            R_eMessageBoxResult loResult;
            bool llValidate = false;
            try
            {
                var loData = (PMT04100DTO)_conductorRef.R_GetCurrentData();

                if (loData.CSTATUS == "00" && int.Parse(loData.CREF_PRD) < int.Parse(_JournalEntryViewModel.VAR_CB_SYSTEM_PARAM.CSOFT_PERIOD))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT04100FrontResources.Resources_Dummy_Class),
                        "V19"));
                    llValidate = true;
                }

                if (llValidate == true)
                {
                    goto EndBlock;
                }
                else
                {
                    loResult = await R_MessageBox.Show("", _localizer["Q06"], R_eMessageBoxButtonType.YesNo);
                    if (loResult == R_eMessageBoxResult.No)
                        goto EndBlock;

                    var loParam = R_FrontUtility.ConvertObjectToObject<PMT04100UpdateStatusDTO>(loData);

                    await _JournalEntryViewModel.SubmitCashReceipt(loParam);
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
        private async Task RedraftProcess()
        {
            var loEx = new R_Exception();
            R_eMessageBoxResult loResult;
            bool llValidate = false;
            try
            {
                var loData = (PMT04100DTO)_conductorRef.R_GetCurrentData();

                if (llValidate == true)
                {
                    goto EndBlock;
                }
                else
                {
                    loResult = await R_MessageBox.Show("", _localizer["Q07"], R_eMessageBoxButtonType.YesNo);
                    if (loResult == R_eMessageBoxResult.No)
                        goto EndBlock;

                    var loParam = R_FrontUtility.ConvertObjectToObject<PMT04100UpdateStatusDTO>(loData);
                    loParam.LAUTO_COMMIT = false;
                    loParam.LUNDO_COMMIT = false;
                    loParam.CNEW_STATUS = "00";

                    await _JournalEntryViewModel.UpdateJournalStatus(loParam);
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

        #region lookup Dept
        private async Task DeptCode_OnLostFocus(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT04100DTO)_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loData.CDEPT_CODE) == false)
                {
                    GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
                    {
                        CSEARCH_TEXT = loData.CDEPT_CODE,
                        CPROPERTY_ID = loData.CPROPERTY_ID
                    };

                    LookupGSL00710ViewModel loLookupViewModel = new LookupGSL00710ViewModel();

                    var loResult = await loLookupViewModel.GetDepartmentProperty(loParam);

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
            var param = new GSL00710ParameterDTO
            {
                CPROPERTY_ID = _JournalEntryViewModel.Data.CPROPERTY_ID
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00710);
        }
        private void After_Open_lookupDept(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00710DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loData = (PMT04100DTO)_conductorRef.R_GetCurrentData();
            loData.CDEPT_CODE = loTempResult.CDEPT_CODE;
            loData.CDEPT_NAME = loTempResult.CDEPT_NAME;
        }
        #endregion

        #region lookup Customer
        private async Task CustomerCode_OnLostFocus(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT04100DTO)_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loData.CCUST_SUPP_ID) == false)
                {
                    LML00600ParameterDTO loParam = new LML00600ParameterDTO()
                    {
                        CSEARCH_TEXT = loData.CCUST_SUPP_ID,
                        CPROPERTY_ID = loData.CPROPERTY_ID
                    };

                    LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel();

                    var loResult = await loLookupViewModel.GetTenant(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loData.CCUST_SUPP_NAME = "";
                        loData.CCUSTOMER_TYPE_NAME = "";
                        loData.CLOI_AGRMT_NO = "";
                        loData.CLOI_AGRMT_ID = "";
                        goto EndBlock;
                    }
                    loData.CCUST_SUPP_ID = loResult.CTENANT_ID;
                    loData.CCUST_SUPP_NAME = loResult.CTENANT_NAME;
                    loData.CCUSTOMER_TYPE_NAME = loResult.CCUSTOMER_TYPE_NAME;
                    loData.CLOI_AGRMT_NO = "";
                    loData.CLOI_AGRMT_ID = "";
                }
                else
                {
                    loData.CCUST_SUPP_NAME = "";
                    loData.CCUSTOMER_TYPE_NAME = "";
                    loData.CLOI_AGRMT_NO = "";
                    loData.CLOI_AGRMT_ID = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void Before_Open_lookupCustomer(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new LML00600ParameterDTO
            {
                CPROPERTY_ID = _JournalEntryViewModel.Data.CPROPERTY_ID
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00600);
        }
        private void After_Open_lookupCustomer(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00600DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loData = (PMT04100DTO)_conductorRef.R_GetCurrentData();
            loData.CCUST_SUPP_ID = loTempResult.CTENANT_ID;
            loData.CCUST_SUPP_NAME = loTempResult.CTENANT_NAME;
            loData.CCUSTOMER_TYPE_NAME = loTempResult.CCUSTOMER_TYPE_NAME;
            loData.CLOI_AGRMT_NO = "";
            loData.CLOI_AGRMT_ID = "";
        }
        #endregion

        #region lookup LOIDept
        private async Task LOIDeptCode_OnLostFocus(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT04100DTO)_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loData.CLOI_DEPT_CODE) == false)
                {
                    GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
                    {
                        CSEARCH_TEXT = loData.CLOI_DEPT_CODE,
                        CPROPERTY_ID = loData.CPROPERTY_ID
                    };

                    LookupGSL00710ViewModel loLookupViewModel = new LookupGSL00710ViewModel();

                    var loResult = await loLookupViewModel.GetDepartmentProperty(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loData.CDEPT_NAME = "";
                        loData.CLOI_AGRMT_NO = "";
                        loData.CLOI_AGRMT_ID = "";
                        goto EndBlock;
                    }
                    loData.CLOI_DEPT_CODE = loResult.CDEPT_CODE;
                    loData.CLOI_DEPT_NAME = loResult.CDEPT_NAME;
                    loData.CLOI_AGRMT_NO = "";
                    loData.CLOI_AGRMT_ID = "";
                }
                else
                {
                    loData.CLOI_DEPT_NAME = "";
                    loData.CLOI_AGRMT_NO = "";
                    loData.CLOI_AGRMT_ID = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void Before_Open_lookupLOIDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00710ParameterDTO
            {
                CPROPERTY_ID = _JournalEntryViewModel.Data.CPROPERTY_ID
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00710);
        }
        private void After_Open_lookupLOIDept(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00710DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loData = (PMT04100DTO)_conductorRef.R_GetCurrentData();
            loData.CLOI_DEPT_CODE = loTempResult.CDEPT_CODE;
            loData.CLOI_DEPT_NAME = loTempResult.CDEPT_NAME;
            loData.CLOI_AGRMT_NO = "";
            loData.CLOI_AGRMT_ID = "";
        }
        #endregion

        #region lookup LOI
        private async Task LOICode_OnLostFocus(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT04100DTO)_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loData.CLOI_AGRMT_NO) == false)
                {
                    LML00800ParameterDTO loParam = new LML00800ParameterDTO()
                    {
                        CSEARCH_TEXT = loData.CLOI_AGRMT_NO,
                        CDEPT_CODE = loData.CLOI_DEPT_CODE,
                        CTENANT_ID = loData.CCUST_SUPP_ID,
                        CTRANS_CODE = PMT04100COMMON.ContextConstant.VAR_TRANS_CODE,
                        CREF_NO = loData.CLOI_AGRMT_NO,
                        CTRANS_STATUS = "30,80",
                        CPROPERTY_ID = loData.CPROPERTY_ID
                    };

                    LookupLML00800ViewModel loLookupViewModel = new LookupLML00800ViewModel();

                    var loResult = await loLookupViewModel.GetAgreement(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loData.CUNIT_DESCRIPTION = "";
                        goto EndBlock;
                    }
                    loData.CLOI_AGRMT_NO = loResult.CREF_NO;
                    loData.CLOI_AGRMT_ID = loResult.CREC_ID;
                    loData.CUNIT_DESCRIPTION = loResult.CUNIT_DESCRIPTION;
                    loData.CLOI_DEPT_CODE = loResult.CDEPT_CODE;
                    loData.CLOI_DEPT_NAME = loResult.CDEPT_NAME;
                }
                else
                {
                    loData.CUNIT_DESCRIPTION = "";
                    loData.CLOI_DEPT_CODE = "";
                    loData.CLOI_DEPT_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void Before_Open_lookupLOI(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loData = (PMT04100DTO)_conductorRef.R_GetCurrentData();
            var param = new LML00800ParameterDTO
            {
                CSEARCH_TEXT = loData.CLOI_AGRMT_NO,
                CDEPT_CODE = loData.CLOI_DEPT_CODE,
                CTENANT_ID = loData.CCUST_SUPP_ID,
                CTRANS_CODE = PMT04100COMMON.ContextConstant.VAR_TRANS_CODE,
                CREF_NO = loData.CLOI_AGRMT_NO,
                CPROPERTY_ID = loData.CPROPERTY_ID,
                CTRANS_STATUS = "30,80",
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00800);
        }
        private void After_Open_lookupLOI(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00800DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loData = (PMT04100DTO)_conductorRef.R_GetCurrentData();
            loData.CLOI_AGRMT_NO = loTempResult.CREF_NO;
            loData.CLOI_AGRMT_ID = loTempResult.CREC_ID;
            loData.CUNIT_DESCRIPTION = loTempResult.CUNIT_DESCRIPTION;
            loData.CLOI_DEPT_CODE = loTempResult.CDEPT_CODE;
            loData.CLOI_DEPT_NAME = loTempResult.CDEPT_NAME;
        }
        #endregion

        #region lookup Cash Code
        private async Task CashCode_OnLostFocus(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT04100DTO)_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loData.CCB_CODE) == false)
                {
                    GSL02500ParameterDTO loParam = new GSL02500ParameterDTO()
                    {
                        CDEPT_CODE = loData.CDEPT_CODE,
                        CCB_TYPE = "C",
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
                    (loData.CCURRENCY_CODE != _JournalEntryViewModel.VAR_GSM_COMPANY.CLOCAL_CURRENCY_CODE
                    || loData.CCURRENCY_CODE != _JournalEntryViewModel.VAR_GSM_COMPANY.CBASE_CURRENCY_CODE))
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
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void Before_Open_lookupCashCode(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loData = (PMT04100DTO)_conductorRef.R_GetCurrentData();
            GSL02500ParameterDTO loParam = new GSL02500ParameterDTO()
            {
                CDEPT_CODE = loData.CDEPT_CODE,
                CCB_TYPE = "C",
                CBANK_TYPE = "I"
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL02500);
        }
        private async Task After_Open_lookupCashCode(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL02500DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loData = (PMT04100DTO)_conductorRef.R_GetCurrentData();
            loData.CCB_CODE = loTempResult.CCB_CODE;
            loData.CCB_NAME = loTempResult.CCB_NAME;
            loData.CCURRENCY_CODE = loTempResult.CCURRENCY_CODE;
            loData.CCB_ACCOUNT_NO = loTempResult.CCB_ACCOUNT_NO;
            if (!string.IsNullOrWhiteSpace(loData.CCURRENCY_CODE) &&
                    (loData.CCURRENCY_CODE != _JournalEntryViewModel.VAR_GSM_COMPANY.CLOCAL_CURRENCY_CODE
                    || loData.CCURRENCY_CODE != _JournalEntryViewModel.VAR_GSM_COMPANY.CBASE_CURRENCY_CODE))
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

        #region lookup CashFlow
        private async Task CashFlowCode_OnLostFocus(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT04100DTO)_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loData.CCASH_FLOW_CODE) == false)
                {
                    GSL01500ParameterGroupDTO loParam = new GSL01500ParameterGroupDTO()
                    {
                        CSEARCH_TEXT = loData.CCASH_FLOW_CODE,
                    };

                    LookupGSL01500ViewModel loLookupViewModel = new LookupGSL01500ViewModel();

                    var loResult = await loLookupViewModel.GetCashFlow(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loData.CCASH_FLOW_NAME = "";
                        loData.CCASH_FLOW_GROUP_CODE = "";

                        goto EndBlock;
                    }
                    loData.CCASH_FLOW_CODE = loResult.CCASH_FLOW_CODE;
                    loData.CCASH_FLOW_NAME = loResult.CCASH_FLOW_NAME;
                    loData.CCASH_FLOW_GROUP_CODE = loResult.CCASH_FLOW_GROUP_CODE;
                }
                else
                {
                    loData.CCASH_FLOW_NAME = "";
                    loData.CCASH_FLOW_GROUP_CODE = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void Before_Open_lookupCashFlow(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL01500ParameterGroupDTO
            {
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL01500);
        }
        private void After_Open_lookupCashFlow(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = R_FrontUtility.ConvertObjectToObject<GSL01500DTO>(eventArgs.Result);
            if (loTempResult == null)
            {
                return;
            }

            var loData = (PMT04100DTO)_conductorRef.R_GetCurrentData();
            loData.CCASH_FLOW_CODE = loTempResult.CCASH_FLOW_CODE;
            loData.CCASH_FLOW_NAME = loTempResult.CCASH_FLOW_NAME;
            loData.CCASH_FLOW_GROUP_CODE = loTempResult.CCASH_FLOW_GROUP_CODE;
        }
        #endregion

        #region Print
        private void Before_Open_lookupPrint(R_BeforeOpenLookupEventArgs eventArgs)
        {
            //var loData = (PMT04100DTO)_conductorRef.R_GetCurrentData();
            //var param = new GLTR00100DTO()
            //{
            //    CREC_ID = loData.CREC_ID
            //};
            //eventArgs.Parameter = param;
            //eventArgs.TargetPageType = typeof(GLTR00100);
        }
        #endregion

        #region Allocate Popup
        private void Before_Open_Allocate_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loData = (PMT04100DTO)_conductorRef.R_GetCurrentData();
            eventArgs.Parameter = new OpenAllocationParameterDTO()
            {
                CPROPERTY_ID = loData.CPROPERTY_ID,
                CREC_ID = loData.CREC_ID,
                CDEPT_CODE = loData.CDEPT_CODE,
                CTRANS_CODE = PMT04100COMMON.ContextConstant.VAR_TRANS_CODE,
                CREF_NO = loData.CREF_NO,
                LDISPLAY_ONLY = false
            };
            eventArgs.TargetPageType = typeof(PMF00100);
        }
        private async Task After_Open_Allocate_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loData = _conductorRef.R_GetCurrentData();
                if (eventArgs.Success == false)
                {
                    return;
                }
                await _gridDetailRef.R_RefreshGrid(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion
    }
}
