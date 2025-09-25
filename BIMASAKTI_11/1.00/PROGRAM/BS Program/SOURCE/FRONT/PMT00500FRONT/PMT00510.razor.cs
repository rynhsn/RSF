using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;
using R_BlazorFrontEnd.Interfaces;
using PMT00500COMMON;
using PMT00500MODEL;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMModel.ViewModel.LML00600;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00500;
using Lookup_PMCOMMON.DTOs.LML01000;
using Lookup_PMModel.ViewModel.LML01000;
using Lookup_PMModel.ViewModel.LML01100;
using Lookup_PMCOMMON.DTOs.LML01100;
using Lookup_PMModel.ViewModel.LML00800;
using R_BlazorFrontEnd.Controls.MessageBox;
using PMT01300COMMON;

namespace PMT00500FRONT
{
    public partial class PMT00510 : R_Page, R_ITabPage
    {
        private PMT00510ViewModel _viewModel = new PMT00510ViewModel();
        private R_Conductor _conductorRef;
        #region Inject
        [Inject] private R_ILocalizer<PMT00500FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }
        #endregion

        private R_DatePicker<DateTime?> RefDate_DatePicker;
        private R_TextBox DeptCode_TextBox;
        private R_TextBox TenantCode_TextBox;

        private bool EnableNormalMode = false;
        private bool EnableDraftSts = false;
        private bool EnableOpenSts = false;
        private bool EnableApproveSts = false;
        private bool EnableAddMode = false;
        private bool IsAddData = false;
        private string PropertyID = "";
        private string CurrencyCode = "";
        private string _StyleCallPopup = "width: auto;";
        private bool HiddenButtonProgram = false;
        private PMT00500LOICallParameterDTO loCallerParameter = new PMT00500LOICallParameterDTO();
        
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetInitialVar();
                var loData = R_FrontUtility.ConvertObjectToObject<PMT00500LOICallParameterDTO>(poParameter);
                PropertyID = loData.CPROPERTY_ID;
                CurrencyCode = loData.CCURRENCY_CODE;
                IsAddData = loData.LIS_ADD_DATA_LOI;
                loCallerParameter = loData;
                    
                if (string.IsNullOrWhiteSpace(loData.CALLER_ACTION))
                {
                    if (string.IsNullOrWhiteSpace(loData.CREF_NO) == false)
                    {
                        await _conductorRef.R_GetEntity(loData);
                    }
                }
                else
                {
                    HiddenButtonProgram = loData.CALLER_ACTION == "VIEW";
                    if (loData.LPOP_UP_MODE)
                    {
                        _StyleCallPopup = "width: 1250px;";
                    }
                    if (loData.CALLER_ACTION == "ADD")
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

            R_DisplayException(loEx);
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
                var loData = (PMT00500DTO)eventArgs.Data;

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
                        Program_Id = "PMT00500",
                        Table_Name = "PMT_AGREEMENT",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMT00500",
                        Table_Name = "PMT_AGREEMENT",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO)
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

        #region Tab Refresh
        public async Task RefreshTabPageAsync(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT00500DTO)poParam;
                PropertyID = loData.CPROPERTY_ID;
                CurrencyCode = loData.CCURRENCY_CODE;
                IsAddData = false;

                if (string.IsNullOrWhiteSpace(loData.CREF_NO) == false)
                {
                    await _conductorRef.R_GetEntity(loData);
                }
                else
                {
                    await _conductorRef.R_SetCurrentData(new PMT00500DTO());
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region LOI Form
        private async Task LOI_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<PMT00500DTO>(eventArgs.Data);

                await _viewModel.GetLOI(loData);

                eventArgs.Result = _viewModel.LOI;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task LOI_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
            {
                await RefDate_DatePicker.FocusAsync();
            }
            else if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Normal)
            {
                var loData = (PMT00500DTO)eventArgs.Data;
                if (string.IsNullOrWhiteSpace(loData.CTRANS_STATUS) == false)
                {
                    EnableDraftSts = loData.CTRANS_STATUS == "00";
                    EnableOpenSts = loData.CTRANS_STATUS == "10";
                    EnableApproveSts = loData.CTRANS_STATUS == "30";
                }
            }
        }
        private void LOI_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;
                var loData = (PMT00500DTO)eventArgs.Data;

                lCancel = string.IsNullOrWhiteSpace(loData.CDEPT_CODE);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "V001"));
                }

                lCancel = string.IsNullOrWhiteSpace(loData.CSALESMAN_ID);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "V002"));
                }

                lCancel = string.IsNullOrWhiteSpace(loData.CREF_NO) && _viewModel.VAR_GSM_TRANS_CODE_LOI.LINCREMENT_FLAG == false;
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "V003"));
                }

                lCancel = loData.DREF_DATE == null;
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "V004"));
                }
                else
                {
                    if (int.Parse(loData.DREF_DATE.Value.ToString("yyyyMMdd")) > int.Parse(DateTime.Now.ToString("yyyyMMdd")))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(PMT00500FrontResources.Resources_Dummy_Class),
                            "V033"));
                    }
                }

                lCancel = loData.DHO_PLAN_DATE == null;
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "V007"));
                }

                lCancel = loData.DSTART_DATE == null;
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "V005"));
                }

                if (loData.DHO_PLAN_DATE != null && loData.DSTART_DATE != null)
                {
                    lCancel = int.Parse(loData.DSTART_DATE.Value.ToString("yyyyMMdd")) < int.Parse(loData.DHO_PLAN_DATE.Value.ToString("yyyyMMdd"));
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(PMT00500FrontResources.Resources_Dummy_Class),
                            "V031"));
                    }
                }

                lCancel = string.IsNullOrWhiteSpace(loData.CUNIT_DESCRIPTION);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "V008"));
                }

                lCancel = string.IsNullOrWhiteSpace(loData.CCURRENCY_CODE);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "V009"));
                }

                lCancel = string.IsNullOrWhiteSpace(loData.CCHARGE_MODE);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "V011"));
                }

                lCancel = loData.NBOOKING_FEE < 0;
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "V013"));
                }                

                 lCancel = string.IsNullOrWhiteSpace(loData.CTENANT_ID);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "V017"));
                }

                lCancel = string.IsNullOrWhiteSpace(loData.CBUILDING_ID);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "V018"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        private async Task LOI_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.SaveLOI(
                    (PMT00500DTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                IsAddData = eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Add;

                eventArgs.Result = _viewModel.LOI;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task LOI_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            var res = await R_MessageBox.Show("", _localizer["Q04"],
                R_eMessageBoxButtonType.YesNo);

            eventArgs.Cancel = res == R_eMessageBoxResult.No;
            if (res == R_eMessageBoxResult.Yes && loCallerParameter.CALLER_ACTION == "ADD" && loCallerParameter.LPOP_UP_MODE)
            {
                await this.Close(false, null);
            }
        }
        private async Task Grid_R_SetOther(R_SetEventArgs eventArgs)
        {
            PMT00500DTO loDataLOI = null;
            var loConductor = _conductorRef;
            if (loConductor != null)
            {
                loDataLOI = (PMT00500DTO)loConductor.R_GetCurrentData();
            }
            EnableNormalMode = eventArgs.Enable;
            PMT00500LOICallBackParameterDTO loData = new PMT00500LOICallBackParameterDTO { CRUD_MODE = eventArgs.Enable, SELECTED_DATA_TAB_LOI = loDataLOI, LIS_ADD_DATA_LOI = IsAddData };
            await InvokeTabEventCallbackAsync(loData);
        }
        private void LOI_SetAdd(R_SetEventArgs eventArgs)
        {
            EnableAddMode = eventArgs.Enable;
        }
        private async Task LOI_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loData = (PMT00500DTO)eventArgs.Data;
            loData.CLINK_TRANS_CODE = loCallerParameter.VAR_LINK_TRANS_CODE;
            loData.CLINK_REF_NO = loCallerParameter.VAR_LINK_REF_NO;
            loData.CPROPERTY_ID = PropertyID;
            loData.CCURRENCY_CODE = CurrencyCode;
            
            if (loCallerParameter.BUTTON_FROM_TAB_UNIT_LIST == "NEW_OFFER")
            {
                loData.CBUILDING_ID = loCallerParameter.CBUILDING_ID;
                loData.CBUILDING_NAME = loCallerParameter.CBUILDING_NAME;
            }
            
            if (loCallerParameter.BUTTON_FROM_TAB_UNIT_LIST == "CHANGE_OWNER")
            {
                loData.CREF_NO = loCallerParameter.VAR_LINK_REF_NO;
                loData.CDEPT_CODE = loCallerParameter.VAR_LINK_DEPT_CODE;
                loData.CTRANS_CODE = loCallerParameter.VAR_LINK_TRANS_CODE;
                
                var loTempData = await _viewModel.GetLOIWithResult(loData);
                
                loData.CCHARGE_MODE = loTempData.CCHARGE_MODE;
                loData.DREF_DATE = loTempData.DREF_DATE;
                loData.CDEPT_CODE = loTempData.CDEPT_CODE;
                loData.CDEPT_NAME = loTempData.CDEPT_NAME;
                loData.CBUILDING_ID = loTempData.CBUILDING_ID;
                loData.CBUILDING_NAME = loTempData.CBUILDING_NAME;
                loData.CSALESMAN_ID = loTempData.CSALESMAN_ID;
                loData.CSALESMAN_NAME = loTempData.CSALESMAN_NAME;
                loData.DHO_PLAN_DATE = loTempData.DHO_PLAN_DATE;
                loData.LWITH_FO = loTempData.LWITH_FO;
                loData.DHO_ACTUAL_DATE = loTempData.DHO_ACTUAL_DATE;
                loData.CAGREEMENT_STATUS_DESCR = loTempData.CAGREEMENT_STATUS_DESCR;
                loData.CAGREEMENT_STATUS = loTempData.CAGREEMENT_STATUS;
                loData.CCURRENCY_CODE = loTempData.CCURRENCY_CODE;
                loData.CBILLING_RULE_CODE = loTempData.CBILLING_RULE_CODE;
                loData.NBOOKING_FEE = loTempData.NBOOKING_FEE;
                loData.CTC_CODE = loTempData.CTC_CODE;
                loData.VAR_AGRMT_ID = loCallerParameter.VAR_AGRMT_ID;

                await TenantCode_TextBox.FocusAsync();
            }
            else
            {
                loData.CBILLING_RULE_CODE = "";
                loData.CCHARGE_MODE = "01";
                loData.CTC_CODE = "";
                loData.DREF_DATE = DateTime.Now;
                loData.DFOLLOW_UP_DATE = DateTime.Now;
                loData.DSTART_DATE = DateTime.Now;
                loData.DHO_PLAN_DATE = DateTime.Now;
                
                await DeptCode_TextBox.FocusAsync();
            }

            if (loCallerParameter.VAR_SELECTED_UNIT_LIST_DATA != null)
            {
                loData.OUNIT_LITS_DATA = loCallerParameter.VAR_SELECTED_UNIT_LIST_DATA;
            }
            loData.CREF_NO = "";
            loData.CLEASE_MODE = "";
            
            _viewModel.oControlYMD.LYEAR = true;
            _viewModel.oControlYMD.LMONTH = true;
            _viewModel.oControlYMD.LMONTH = true;

        }
        #endregion

        #region Tenant Lookup
        private async Task Tenant_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT00500DTO)_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loData.CTENANT_ID) == false)
                {
                    LML00600ParameterDTO loParam = new LML00600ParameterDTO()
                    {
                        CCOMPANY_ID = "",
                        CPROPERTY_ID = loData.CPROPERTY_ID,
                        CUSER_ID = "",
                        CCUSTOMER_TYPE = "01",
                        CSEARCH_TEXT = loData.CTENANT_ID
                    };

                    LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel();

                    var loResult = await loLookupViewModel.GetTenant(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loData.CTENANT_NAME = "";
                        goto EndBlock;
                    }
                    loData.CTENANT_ID = loResult.CTENANT_ID;
                    loData.CTENANT_NAME = loResult.CTENANT_NAME;
                }
                else
                {
                    loData.CTENANT_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void Tenant_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00600ParameterDTO loParam = new LML00600ParameterDTO()
            {
                CCOMPANY_ID = "",
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                CCUSTOMER_TYPE = "01",
                CUSER_ID = ""
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(LML00600);
        }
        private void Tenant_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                LML00600DTO loTempResult = (LML00600DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }
                var loData = (PMT00500DTO)_conductorRef.R_GetCurrentData();
                loData.CTENANT_ID = loTempResult.CTENANT_ID;
                loData.CTENANT_NAME = loTempResult.CTENANT_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Building Lookup
        private async Task Building_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT00500DTO)_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loData.CBUILDING_ID) == false)
                {
                    GSL02200ParameterDTO loParam = new GSL02200ParameterDTO()
                    {
                        CPROPERTY_ID = loData.CPROPERTY_ID,
                        CSEARCH_TEXT = loData.CBUILDING_ID
                    };

                    LookupGSL02200ViewModel loLookupViewModel = new LookupGSL02200ViewModel();

                    var loResult = await loLookupViewModel.GetBuilding(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loData.CBUILDING_NAME = "";
                        goto EndBlock;
                    }
                    loData.CBUILDING_ID = loResult.CBUILDING_ID;
                    loData.CBUILDING_NAME = loResult.CBUILDING_NAME;
                }
                else
                {
                    loData.CBUILDING_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void Building_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL02200ParameterDTO loParam = new GSL02200ParameterDTO()
            {
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL02200);
        }
        private void Building_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSL02200DTO loTempResult = (GSL02200DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }
                var loData = (PMT00500DTO)_conductorRef.R_GetCurrentData();
                loData.CBUILDING_ID = loTempResult.CBUILDING_ID;
                loData.CBUILDING_NAME = loTempResult.CBUILDING_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Dept Lookup
        private async Task DeptCode_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT00500DTO)_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loData.CDEPT_CODE) == false)
                {
                    GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
                    {
                        CPROPERTY_ID = loData.CPROPERTY_ID,
                        CSEARCH_TEXT = loData.CDEPT_CODE
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
        private void R_Before_Open_LookupDepartment(R_BeforeOpenLookupEventArgs eventArgs)
        {
            if (string.IsNullOrWhiteSpace(_viewModel.Data.CPROPERTY_ID))
            {
                return;
            }
            GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
            {
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00710);
        }
        private void R_After_Open_LookupDepartment(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00710DTO loTempResult = (GSL00710DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            var loData = (PMT00500DTO)_conductorRef.R_GetCurrentData();
            loData.CDEPT_CODE = loTempResult.CDEPT_CODE;
            loData.CDEPT_NAME = loTempResult.CDEPT_NAME;
        }
        #endregion

        #region Salesman Lookup
        private async Task Salesman_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT00500DTO)_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loData.CSALESMAN_ID) == false)
                {
                    LML00500ParameterDTO loParam = new LML00500ParameterDTO()
                    {
                        CCOMPANY_ID = "",
                        CPROPERTY_ID = loData.CPROPERTY_ID,
                        CUSER_ID = "",
                        CSEARCH_TEXT = loData.CSALESMAN_ID
                    };

                    LookupLML00500ViewModel loLookupViewModel = new LookupLML00500ViewModel();

                    var loResult = await loLookupViewModel.GetSalesman(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loData.CSALESMAN_NAME = "";
                        goto EndBlock;
                    }
                    loData.CSALESMAN_ID = loResult.CSALESMAN_ID;
                    loData.CSALESMAN_NAME = loResult.CSALESMAN_NAME;
                }
                else
                {
                    loData.CSALESMAN_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void Salesman_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00500ParameterDTO loParam = new LML00500ParameterDTO()
            {
                CCOMPANY_ID = "",
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                CUSER_ID = ""
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(LML00500);
        }
        private void Salesman_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                LML00500DTO loTempResult = (LML00500DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }
                var loData = (PMT00500DTO)_conductorRef.R_GetCurrentData();
                loData.CSALESMAN_ID = loTempResult.CSALESMAN_ID;
                loData.CSALESMAN_NAME = loTempResult.CSALESMAN_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region LOC Ref No Lookup
        private async Task LOCRefNo_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT00500DTO)_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loData.CLINK_REF_NO) == false)
                {
                    LML00800ParameterDTO loParam = new LML00800ParameterDTO()
                    {
                        CCOMPANY_ID = "",
                        CPROPERTY_ID = loData.CPROPERTY_ID,
                        CDEPT_CODE = string.IsNullOrWhiteSpace(loData.CDEPT_CODE) ? "" : loData.CDEPT_CODE,
                        CAGGR_STTS = "00,02",
                        CTRANS_CODE = "802051",
                        CTRANS_STATUS = "30",
                        CUSER_ID = clientHelper.UserId,
                        CSEARCH_TEXT = loData.CLINK_REF_NO,
                    };

                    LookupLML00800ViewModel loLookupViewModel = new LookupLML00800ViewModel();

                    var loResult = await loLookupViewModel.GetAgreement(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loData.CLINK_TRANS_CODE = "";
                        loData.CLINK_REF_NO = "";
                        loData.CLINK_REF_NO = "";
                        loData.CLINK_TRANS_CODE = "";
                        loData.CDEPT_CODE = "";
                        loData.CDEPT_NAME = "";
                        loData.CTENANT_ID = "";
                        loData.CTENANT_NAME = "";
                        loData.CBUILDING_ID = "";
                        loData.CBUILDING_NAME = "";
                        loData.CSALESMAN_ID = "";
                        loData.CSALESMAN_NAME = "";
                        loData.CUNIT_DESCRIPTION = "";
                        loData.CNOTES = "";
                        loData.CCURRENCY_CODE = "";
                        loData.LWITH_FO = false;
                        loData.CLEASE_MODE = "";
                        loData.CCHARGE_MODE = "";
                        loData.CBILLING_RULE_CODE = "";
                        loData.NBOOKING_FEE = 0;
                        loData.CTC_CODE = "";
                        loData.DSTART_DATE = null;
                        loData.DEND_DATE = null;
                        loData.DHO_PLAN_DATE = null;
                        loData.DACTUAL_END_DATE = null;
                        loData.DACTUAL_START_DATE = null;
                        loData.IYEARS = 0;
                        loData.IMONTHS = 0;
                        loData.IDAYS = 0;
                        goto EndBlock;
                    }

                    var loGetResultParam = R_FrontUtility.ConvertObjectToObject<PMT00500DTO>(loResult);
                    loGetResultParam.CPROPERTY_ID = loData.CPROPERTY_ID;

                    var loResultData = await _viewModel.GetLOIWithResult(loGetResultParam);
                    loData.CLINK_REF_NO = loResultData.CREF_NO;
                    loData.CLINK_TRANS_CODE = loResultData.CTRANS_CODE;
                    loData.CDEPT_CODE = loResultData.CDEPT_CODE;
                    loData.CDEPT_NAME = loResultData.CDEPT_NAME;
                    loData.CTENANT_ID = loResultData.CTENANT_ID;
                    loData.CTENANT_NAME = loResultData.CTENANT_NAME;
                    loData.CBUILDING_ID = loResultData.CBUILDING_ID;
                    loData.CBUILDING_NAME = loResultData.CBUILDING_NAME;
                    loData.CSALESMAN_ID = loResultData.CSALESMAN_ID;
                    loData.CSALESMAN_NAME = loResultData.CSALESMAN_NAME;
                    loData.CUNIT_DESCRIPTION = loResultData.CUNIT_DESCRIPTION;
                    loData.CNOTES = loResultData.CNOTES;
                    loData.CCURRENCY_CODE = loResultData.CCURRENCY_CODE;
                    loData.LWITH_FO = loResultData.LWITH_FO;
                    loData.CLEASE_MODE = loResultData.CLEASE_MODE;
                    loData.CCHARGE_MODE = loResultData.CCHARGE_MODE;
                    loData.CBILLING_RULE_CODE = loResultData.CBILLING_RULE_CODE;
                    loData.NBOOKING_FEE = loResultData.NBOOKING_FEE;
                    loData.CTC_CODE = loResultData.CTC_CODE;
                    loData.DSTART_DATE = loResultData.DSTART_DATE;
                    loData.DEND_DATE = loResultData.DEND_DATE;
                    loData.DHO_PLAN_DATE = loResultData.DHO_PLAN_DATE;
                    loData.DACTUAL_END_DATE = loResultData.DACTUAL_END_DATE;
                    loData.DACTUAL_START_DATE = loResultData.DACTUAL_START_DATE;
                    loData.IYEARS = loResultData.IYEARS;
                    loData.IMONTHS = loResultData.IMONTHS;
                    loData.IDAYS = loResultData.IDAYS;
                }
                else
                {
                    loData.CLINK_REF_NO = "";
                    loData.CLINK_TRANS_CODE = "";
                    loData.CDEPT_CODE = "";
                    loData.CDEPT_NAME = "";
                    loData.CTENANT_ID = "";
                    loData.CTENANT_NAME = "";
                    loData.CBUILDING_ID = "";
                    loData.CBUILDING_NAME = "";
                    loData.CSALESMAN_ID = "";
                    loData.CSALESMAN_NAME = "";
                    loData.CUNIT_DESCRIPTION = "";
                    loData.CNOTES = "";
                    loData.CCURRENCY_CODE = "";
                    loData.LWITH_FO = false;
                    loData.CLEASE_MODE = "";
                    loData.CCHARGE_MODE = "";
                    loData.CBILLING_RULE_CODE = "";
                    loData.NBOOKING_FEE = 0;
                    loData.CTC_CODE = "";
                    loData.DSTART_DATE = null;
                    loData.DEND_DATE = null;
                    loData.DHO_PLAN_DATE = null;
                    loData.DACTUAL_END_DATE = null;
                    loData.DACTUAL_START_DATE = null;
                    loData.IYEARS = 0;
                    loData.IMONTHS = 0;
                    loData.IDAYS = 0;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void LOCRefNo_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00800ParameterDTO loParam = new LML00800ParameterDTO()
            {
                CCOMPANY_ID = "",
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                CDEPT_CODE = string.IsNullOrWhiteSpace(_viewModel.Data.CDEPT_CODE) ? "" : _viewModel.Data.CDEPT_CODE,
                CTRANS_STATUS = "30",
                CAGGR_STTS = "00,02",
                CTRANS_CODE = "802051",
                CUSER_ID = clientHelper.UserId,
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(LML00800);
        }
        private async Task LOCRefNo_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                LML00800DTO loTempResult = (LML00800DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }
                var loData = (PMT00500DTO)_conductorRef.R_GetCurrentData();
                var loGetResultParam = R_FrontUtility.ConvertObjectToObject<PMT00500DTO>(loTempResult);
                loGetResultParam.CPROPERTY_ID = loData.CPROPERTY_ID;

                var loResultData = await _viewModel.GetLOIWithResult(loGetResultParam);
                loData.CLINK_REF_NO = loResultData.CREF_NO;
                loData.CLINK_TRANS_CODE = loResultData.CTRANS_CODE;
                loData.CDEPT_CODE = loResultData.CDEPT_CODE;
                loData.CDEPT_NAME = loResultData.CDEPT_NAME;
                loData.CTENANT_ID = loResultData.CTENANT_ID;
                loData.CTENANT_NAME = loResultData.CTENANT_NAME;
                loData.CBUILDING_ID = loResultData.CBUILDING_ID;
                loData.CBUILDING_NAME = loResultData.CBUILDING_NAME;
                loData.CSALESMAN_ID = loResultData.CSALESMAN_ID;
                loData.CSALESMAN_NAME = loResultData.CSALESMAN_NAME;
                loData.CUNIT_DESCRIPTION = loResultData.CUNIT_DESCRIPTION;
                loData.CNOTES = loResultData.CNOTES;
                loData.CCURRENCY_CODE = loResultData.CCURRENCY_CODE;
                loData.LWITH_FO = loResultData.LWITH_FO;
                loData.CLEASE_MODE = loResultData.CLEASE_MODE;
                loData.CCHARGE_MODE = loResultData.CCHARGE_MODE;
                loData.CBILLING_RULE_CODE = loResultData.CBILLING_RULE_CODE;
                loData.NBOOKING_FEE = loResultData.NBOOKING_FEE;
                loData.CTC_CODE = loResultData.CTC_CODE;
                loData.DSTART_DATE = loResultData.DSTART_DATE;
                loData.DEND_DATE = loResultData.DEND_DATE;
                loData.DHO_PLAN_DATE = loResultData.DHO_PLAN_DATE;
                loData.DACTUAL_END_DATE = loResultData.DACTUAL_END_DATE;
                loData.DACTUAL_START_DATE = loResultData.DACTUAL_START_DATE;
                loData.IYEARS = loResultData.IYEARS;
                loData.IMONTHS = loResultData.IMONTHS;
                loData.IDAYS = loResultData.IDAYS;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Currency Lookup
        private async Task Currency_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT00500DTO)_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loData.CCURRENCY_CODE) == false)
                {
                    GSL00300ParameterDTO loParam = new GSL00300ParameterDTO()
                    {
                        CSEARCH_TEXT = loData.CCURRENCY_CODE
                    };

                    LookupGSL00300ViewModel loLookupViewModel = new LookupGSL00300ViewModel();

                    var loResult = await loLookupViewModel.GetCurrency(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loData.CCURRENCY_NAME = "";
                        goto EndBlock;
                    }
                    loData.CCURRENCY_CODE = loResult.CCURRENCY_CODE;
                    loData.CCURRENCY_NAME = loResult.CCURRENCY_NAME;
                }
                else
                {
                    loData.CCURRENCY_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void Currency_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00300ParameterDTO loParam = new GSL00300ParameterDTO()
            {
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00300);
        }
        private void Currency_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSL00300DTO loTempResult = (GSL00300DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }
                var loData = (PMT00500DTO)_conductorRef.R_GetCurrentData();
                loData.CCURRENCY_CODE = loTempResult.CCURRENCY_CODE;
                loData.CCURRENCY_NAME = loTempResult.CCURRENCY_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region BillingRule Lookup
        private async Task BillingRule_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT00500DTO)_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loData.CBILLING_RULE_CODE) == false)
                {
                    LML01000ParameterDTO loParam = new LML01000ParameterDTO()
                    {
                        CCOMPANY_ID = "",
                        CPROPERTY_ID = loData.CPROPERTY_ID,
                        CBILLING_RULE_TYPE = "01",
                        CUNIT_TYPE_CTG_ID = "",
                        LACTIVE_ONLY = true,
                        CUSER_ID = "",
                        CSEARCH_TEXT = loData.CBILLING_RULE_CODE
                    };

                    LookupLML01000ViewModel loLookupViewModel = new LookupLML01000ViewModel();

                    var loResult = await loLookupViewModel.GetBillingRule(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        goto EndBlock;
                    }
                    loData.CBILLING_RULE_CODE = loResult.CBILLING_RULE_CODE;
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void BillingRule_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML01000ParameterDTO loParam = new LML01000ParameterDTO()
            {
                CCOMPANY_ID = "",
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                CBILLING_RULE_TYPE = "01",
                CUNIT_TYPE_CTG_ID = "",
                LACTIVE_ONLY = true,
                CUSER_ID = ""
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(LML01000);
        }
        private void BillingRule_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                LML01000DTO loTempResult = (LML01000DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }
                var loData = (PMT00500DTO)_conductorRef.R_GetCurrentData();
                loData.CBILLING_RULE_CODE = loTempResult.CBILLING_RULE_CODE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region TC Lookup
        private async Task TC_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT00500DTO)_conductorRef.R_GetCurrentData();
                if (string.IsNullOrWhiteSpace(loData.CTC_CODE) == false)
                {
                    LML01100ParameterDTO loParam = new LML01100ParameterDTO()
                    {
                        CCOMPANY_ID = "",
                        CPROPERTY_ID = loData.CPROPERTY_ID,
                        CUSER_ID = "",
                        CSEARCH_TEXT = loData.CTC_CODE
                    };

                    LookupLML01100ViewModel loLookupViewModel = new LookupLML01100ViewModel();

                    var loResult = await loLookupViewModel.GetTermNCondition(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        goto EndBlock;
                    }
                    loData.CTC_CODE = loResult.CTC_CODE;
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void TC_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML01100ParameterDTO loParam = new LML01100ParameterDTO()
            {
                CCOMPANY_ID = "",
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                CUSER_ID = ""
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(LML01100);
        }
        private void TC_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                LML01100DTO loTempResult = (LML01100DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }
                var loData = (PMT00500DTO)_conductorRef.R_GetCurrentData();
                loData.CTC_CODE = loTempResult.CTC_CODE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Value Change
        DateTime tStartDate = DateTime.Now.AddDays(-1);
        private async Task PlanStartDate_ValueChanged(DateTime? poParam)
        {
            R_Exception loException = new R_Exception();
            _viewModel.Data.DSTART_DATE = poParam;

            try
            {
                var loData = _viewModel.Data;
                if (poParam.HasValue)
                {
                    DateTime adjustedValue = new DateTime(poParam.Value.Year, poParam.Value.Month, poParam.Value.Day, poParam.Value.Hour, 0, 0);
                    loData.DSTART_DATE = poParam;
                }

                tStartDate = poParam ?? DateTime.Now;

                if (loData.DEND_DATE == null)
                {
                    loData.DEND_DATE = loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0
                        ? loData.DSTART_DATE
                        : loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                }
                else
                {
                    CalculateYMD(poStartDate: loData.DSTART_DATE, poEndDate: loData.DEND_DATE, plStart: true);
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);


        }
        private void YearInt_ValueChanged(int poParam)
        {
            var loData = _viewModel.Data;
            var llControl = _viewModel.oControlYMD;
            loData.IYEARS = poParam;

            if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
            {
                if (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0)
                {
                    loData.DEND_DATE = loData.DSTART_DATE;
                }
                else
                {
                    loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                }
            }
            else
            {
                llControl.LYEAR = true;
                loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddDays(-1);
            }

        }
        private void MonthInt_ValueChanged(int poParam)
        {
            _viewModel.Data.IMONTHS = poParam;
            var loData = _viewModel.Data;
            var llControl = _viewModel.oControlYMD;

            if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
            {
                if (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0)
                {
                    loData.DEND_DATE = loData.DSTART_DATE;
                }
                else
                {
                    loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                }
            }
            else
            {
                llControl.LMONTH = true;
                loData.DEND_DATE = loData.DSTART_DATE!.Value.AddMonths(loData.IMONTHS).AddDays(-1);
            }

        }
        private void DayInt_ValueChanged(int poParam)
        {
            _viewModel.Data.IDAYS = poParam;
            var loData = _viewModel.Data;
            var llControl = _viewModel.oControlYMD;

            if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
            {
                if (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0)
                {
                    loData.DEND_DATE = loData.DSTART_DATE;
                }
                else
                {
                    loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                }
            }
            else
            {
                llControl.LYEAR = true;
                loData.DEND_DATE = loData.DSTART_DATE!.Value.AddDays(loData.IDAYS).AddDays(-1);
            }

        }
        private async Task PlanEndDate_ValueChanged(DateTime? poParam)
        {
            R_Exception loException = new R_Exception();

            try
            {
                _viewModel.Data.DEND_DATE = poParam;
                var loData = _viewModel.Data;

                if (loData.DSTART_DATE == null)
                {
                    loData.DSTART_DATE = loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0
                        ? loData.DEND_DATE
                        : loData.DEND_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                }
                else
                {
                    CalculateYMD(poStartDate: loData.DSTART_DATE, poEndDate: loData.DEND_DATE);
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }
        #endregion

        #region Helper
        private void CalculateYMD(DateTime? poStartDate, DateTime? poEndDate, bool plStart = false)
        {
            R_Exception loException = new R_Exception();
            var loData = _viewModel.Data;

            try
            {
                if (poEndDate != null && poStartDate != null)
                {
                    if (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0)
                    {
                        loData.IDAYS = 1;
                        loData.IMONTHS = loData.IYEARS = 0;
                        if (plStart)
                            loData.DSTART_DATE = loData.DEND_DATE;
                        else
                            loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        if (plStart)
                        {
                            loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                        }
                        else
                        {
                            loData.DSTART_DATE = loData.DEND_DATE!.Value.AddYears(-loData.IYEARS).AddMonths(-loData.IMONTHS).AddDays(-loData.IDAYS).AddDays(1);
                        }
                    }
                }
                else
                {
                    if (poStartDate != null)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddDays(1);
                        loData.IYEARS = loData.IMONTHS = 0;
                        loData.IDAYS = 2;
                    }
                }

            }

            //loData.IYEARS = dValueEndDate.Year - poStartDate!.Value.Year;
            //loData.IMONTHS = dValueEndDate.Month - poStartDate!.Value.Month;}
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            //EndBlocks:

            R_DisplayException(loException);
        }
        #endregion

        #region BTN Process
        private async Task SubmitProcess()
        {
            var loEx = new R_Exception();
            R_eMessageBoxResult loResult;
            bool llValidate = false;
            try
            {
                var loData = _conductorRef.R_GetCurrentData();
                PMT00502ViewModel loViewModel = new PMT00502ViewModel();

                PMT00510DTO loUnitParam = R_FrontUtility.ConvertObjectToObject<PMT00510DTO>(loData);
                await loViewModel.GetLOIUnitList(loUnitParam);

                if (loViewModel.LHAS_UNIT == false)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "N08"));
                    llValidate = true;
                }

                if (llValidate == false)
                {
                    loResult = await R_MessageBox.Show("", _localizer["Q02"], R_eMessageBoxButtonType.YesNo);
                    if (loResult == R_eMessageBoxResult.No)
                        goto EndBlock;

                    var loParam = R_FrontUtility.ConvertObjectToObject<PMT00500SubmitRedraftDTO>(loData);
                    loParam.CNEW_STATUS = "10";

                    var loUpdateStatusData = await _viewModel.SubmitRedraftLOI(loParam);

                    await R_MessageBox.Show("", _localizer["N02"], R_eMessageBoxButtonType.OK);

                    await _conductorRef.R_GetEntity(loUpdateStatusData);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            R_DisplayException(loEx);
        }
        private async Task RedraftProcess()
        {
            var loEx = new R_Exception();
            R_eMessageBoxResult loResult;
            bool llValidate = false;
            try
            {
                var loData = _conductorRef.R_GetCurrentData();

                loResult = await R_MessageBox.Show("", _localizer["Q03"], R_eMessageBoxButtonType.YesNo);
                if (loResult == R_eMessageBoxResult.No)
                    goto EndBlock;

                var loParam = R_FrontUtility.ConvertObjectToObject<PMT00500SubmitRedraftDTO>(loData);
                loParam.CNEW_STATUS = "00";

                var loUpdateStatusData = await _viewModel.SubmitRedraftLOI(loParam);

                await R_MessageBox.Show("", _localizer["N03"], R_eMessageBoxButtonType.OK);

                await _conductorRef.R_GetEntity(loUpdateStatusData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            R_DisplayException(loEx);
        }
        private async Task CloseLOIProcess()
        {
            var loEx = new R_Exception();
            R_eMessageBoxResult loResult;
            bool llValidate = false;
            try
            {
                var loData = _conductorRef.R_GetCurrentData();

                loResult = await R_MessageBox.Show("", _localizer["Q06"], R_eMessageBoxButtonType.YesNo);
                if (loResult == R_eMessageBoxResult.No)
                    goto EndBlock;

                var loParam = R_FrontUtility.ConvertObjectToObject<PMT00500SubmitRedraftDTO>(loData);
                loParam.CNEW_STATUS = "80";

                var loUpdateStatusData = await _viewModel.SubmitRedraftLOI(loParam);

                await R_MessageBox.Show("", _localizer["N05"], R_eMessageBoxButtonType.OK);

                await _conductorRef.R_GetEntity(loUpdateStatusData);

                if (loCallerParameter.LCLOSE_ONLY)
                {
                    await this.Close(true, true);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            R_DisplayException(loEx);
        }
        #endregion
    }
}
