using BlazorClientHelper;
using PMT01500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using PMT01500Common.DTO._3._Unit_Info;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_CommonFrontBackAPI;
using R_BlazorFrontEnd.Controls.Tab;
using PMT01500Common.Utilities;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using PMT01500FrontResources;
using PMT01500Common.Utilities.Front;
using PMT01500Common.DTO._7._Document;
using R_BlazorFrontEnd.Controls.Enums;
using R_LockingFront;
using PMT01500Common.DTO._2._Agreement;

namespace PMT01500Front
{
    public partial class PMT01500UnitInfo
    {
        private readonly PMT01500UnitInfo_UnitInfoViewModel _viewModelPMT01500UnitInfo = new();

        private R_Conductor? _conductorUnitInfo_UnitInfo;
        private R_Grid<PMT01500UnitInfoUnitInfoListDTO>? _gridUnitInfo_UnitInfo;

        private bool _isDataExist;

        PMT01500EventCallBackDTO _oEventCallBack = new PMT01500EventCallBackDTO();
        //private bool _pageAgreementListOnCRUDmode;

        [Inject] private IClientHelper? _clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModelPMT01500UnitInfo.loParameterList = (PMT01500GetHeaderParameterDTO)poParameter;

                if (!string.IsNullOrEmpty(_viewModelPMT01500UnitInfo.loParameterList.CREF_NO))
                {
                    await _viewModelPMT01500UnitInfo.GetUnitInfoHeader();
                    await _gridUnitInfo_UnitInfo.R_RefreshGrid(null);
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
            R_LockingFrontResult? loLockResult = null;

            try
            {
                var loData = (PMT01500UnitInfoUnitInfoDetailDTO)eventArgs.Data;

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
                        Program_Id = "PMT01500",
                        Table_Name = "PMT_AGREEMENT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, _viewModelPMT01500UnitInfo.loParameterForUtilitiesPage.CPROPERTY_ID, _viewModelPMT01500UnitInfo.loParameterForUtilitiesPage.CREF_NO)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "PMT01500",
                        Table_Name = "PMT_AGREEMENT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, _viewModelPMT01500UnitInfo.loParameterForUtilitiesPage.CPROPERTY_ID, _viewModelPMT01500UnitInfo.loParameterForUtilitiesPage.CREF_NO)
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

        private async Task GetListUnitInfo(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModelPMT01500UnitInfo.GetUnitInfoList();
                _isDataExist = _viewModelPMT01500UnitInfo.loListPMT01500UnitInfo_UnitInfo.Any();
                eventArgs.ListEntityResult = _viewModelPMT01500UnitInfo.loListPMT01500UnitInfo_UnitInfo;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region Utilities

        private async Task AfterSaveAsync(R_AfterSaveEventArgs eventArgs)
        {
            var loException = new R_Exception();
            //Task loTask;

            try
            {
                if (_viewModelPMT01500UnitInfo.loListPMT01500UnitInfo_UnitInfo.Count == 1)
                {
                    _isDataExist = true;    
                    _oEventCallBack.LContractorOnCRUDmode = true;
                    _oEventCallBack.LACTIVEUnitInfoHasData = true;
                    _oEventCallBack.CFlagUnitInfoHasData = "UI_ADD";//Meaning of Unit Info Add
                    _oEventCallBack.CFLOOR_ID = _viewModelPMT01500UnitInfo.loListPMT01500UnitInfo_UnitInfo.First().CFLOOR_ID;
                    _oEventCallBack.CUNIT_ID = _viewModelPMT01500UnitInfo.loListPMT01500UnitInfo_UnitInfo.First().CUNIT_ID;
                    _oEventCallBack.CUNIT_NAME = _viewModelPMT01500UnitInfo.loListPMT01500UnitInfo_UnitInfo.First().CUNIT_NAME;

                    // Lakukan pemanggilan async
                    await InvokeTabEventCallbackAsync(_oEventCallBack);

                    // Setelah pemanggilan selesai, lanjutkan dengan kode selanjutnya
                    _oEventCallBack.CFlagUnitInfoHasData = "";
                }
            }
            catch (Exception Ex)
            {
                loException.Add(Ex);
            }

            R_DisplayException(loException);

        }

        private async Task SetOther(R_SetEventArgs eventArgs)
        {
            _oEventCallBack.LContractorOnCRUDmode = eventArgs.Enable;
            _oEventCallBack.CFlagUnitInfoHasData = "";
            //_oEventCallBack.CREF_NO = _viewModelPMT01500UnitInfo.loParameterList.CREF_NO!;
            await InvokeTabEventCallbackAsync(_oEventCallBack);
        }

        #endregion

        #region TabStripTAB

        private R_TabStrip? _tabStrip;
        private R_TabPage? _tabPageUtilities;
        private void onTabChange(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            //journalGroupViewModel.DropdownProperty = true;
            //journalGroupViewModel.DropdownGroupType = true;

            //if (eventArgs.TabStripTab.Id == "Tab_AccountSetting")
            //{
            //    journalGroupViewModel.DropdownProperty = false;
            //    journalGroupViewModel.DropdownGroupType = false;
            //}
        }
        private void Before_Open_TabUtilities(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            PMT01500UnitInfoUnitInfoDetailDTO loData = (PMT01500UnitInfoUnitInfoDetailDTO)_viewModelPMT01500UnitInfo.R_GetCurrentData();
            try
            {
                _viewModelPMT01500UnitInfo.loParameterForUtilitiesPage = new PMT01500FrontParameterForUnitInfo_UtilitiesDTO()
                {
                    CDEPT_CODE = _viewModelPMT01500UnitInfo.loParameterList.CDEPT_CODE,
                    CUNIT_NAME = _viewModelPMT01500UnitInfo.Data.CUNIT_NAME,
                    CUNIT_ID = _viewModelPMT01500UnitInfo.Data.CUNIT_ID,
                    CTRANS_CODE = _viewModelPMT01500UnitInfo.loParameterList.CTRANS_CODE,
                    CREF_NO = _viewModelPMT01500UnitInfo.loParameterList.CREF_NO,
                    CPROPERTY_ID = _viewModelPMT01500UnitInfo.loParameterList.CPROPERTY_ID,
                    CFLOOR_ID = _viewModelPMT01500UnitInfo.Data.CFLOOR_ID,
                    CBUILDING_ID = _viewModelPMT01500UnitInfo.Data.CBUILDING_ID
                };
                eventArgs.Parameter = _viewModelPMT01500UnitInfo.loParameterForUtilitiesPage;
                eventArgs.TargetPageType = typeof(PMT01500UnitInfo_Utilities);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }
        
        #endregion

        private async Task ServiceGetOneRecord_UnitInfo(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<PMT01500UnitInfoUnitInfoListDTO>(eventArgs.Data);
                PMT01500UnitInfoUnitInfoDetailDTO loParam = new PMT01500UnitInfoUnitInfoDetailDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _viewModelPMT01500UnitInfo.loParameterList.CPROPERTY_ID,
                    CDEPT_CODE = _viewModelPMT01500UnitInfo.loParameterList.CDEPT_CODE,
                    CREF_NO = _viewModelPMT01500UnitInfo.loParameterList.CREF_NO,
                    CTRANS_CODE = _viewModelPMT01500UnitInfo.loParameterList.CTRANS_CODE,
                    CUNIT_ID = loData.CUNIT_ID,
                    CFLOOR_ID = loData.CFLOOR_ID,
                    CBUILDING_ID = loData.CBUILDING_ID,
                    CUSER_ID = _clientHelper.UserId
                };
                await _viewModelPMT01500UnitInfo.GetEntity(loParam);

                _oEventCallBack.CFlagUnitInfoHasData = "UI_DISPLAY";
                _oEventCallBack.CFLOOR_ID = loData.CFLOOR_ID;
                _oEventCallBack.CUNIT_ID = loData.CUNIT_ID;
                _oEventCallBack.CUNIT_NAME = loData.CUNIT_NAME;
                await InvokeTabEventCallbackAsync(_oEventCallBack);
                _oEventCallBack.CFlagUnitInfoHasData = "";
                eventArgs.Result = _viewModelPMT01500UnitInfo.loEntityUnitInfo_UnitInfo;

                //var temp = (LMM06000BillingRuleDetailDTO)eventArgs.Result;
                //await PropertyDropdown_GetPeriodeList(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ServiceR_Display(R_DisplayEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                switch (eventArgs.ConductorMode)
                {
                    case R_eConductorMode.Edit:
                        //Focus Async
                        await _NCOMMON_AREA_SIZENumericTextBox.FocusAsync();
                        break;
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        #region Delete
        
        public async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (PMT01500UnitInfoUnitInfoDetailDTO)eventArgs.Data;
                await _viewModelPMT01500UnitInfo.ServiceDelete(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceAfterDelete()
        {
            if (_viewModelPMT01500UnitInfo.loListPMT01500UnitInfo_UnitInfo.Count == 0)
            {
                _isDataExist = false;
                _oEventCallBack.LContractorOnCRUDmode = true;
                _oEventCallBack.LACTIVEUnitInfoHasData = false;
                _oEventCallBack.CFlagUnitInfoHasData = "UI_DELETE";
                await InvokeTabEventCallbackAsync(_oEventCallBack);

                // Setelah pemanggilan selesai, lanjutkan dengan kode selanjutnya
                _oEventCallBack.CFlagUnitInfoHasData = "";
            }
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }

        #endregion


        #region SAVE


        private R_TextBox? _CUNIT_IDTextBox;
        private R_NumericTextBox<decimal>? _NCOMMON_AREA_SIZENumericTextBox;

        public async Task ServiceAfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                _isDataExist = false;
                await _CUNIT_IDTextBox.FocusAsync();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        private void BeforeEditAsync(R_BeforeEditEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                _isDataExist = false;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        private void ServiceBeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                _isDataExist = _viewModelPMT01500UnitInfo.loListPMT01500UnitInfo_UnitInfo.Any();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (PMT01500UnitInfoUnitInfoDetailDTO)eventArgs.Data;
                await _viewModelPMT01500UnitInfo.ServiceSave(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModelPMT01500UnitInfo.loEntityUnitInfo_UnitInfo;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_ValidationAsync(R_ValidationEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                var loData = (PMT01500UnitInfoUnitInfoDetailDTO)eventArgs.Data;
                //await LostFocusCUNIT_ID();

                if (string.IsNullOrWhiteSpace(loData.CUNIT_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01500_Class), "ValidationUnitId");
                    loException.Add(loErr);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            eventArgs.Cancel = loException.HasError;


            loException.ThrowExceptionIfErrors();
        }

        #endregion

        #region Lookup_UNIT_ID

        private R_Lookup? R_Lookup_Unit_Id;

        private async Task LostFocusCUNIT_ID()
        {
            var loEx = new R_Exception();

            try
            {

                var loGetData = (PMT01500UnitInfoUnitInfoDetailDTO)_viewModelPMT01500UnitInfo.Data;
                LookupGSL02300ViewModel loLookupViewModel = new LookupGSL02300ViewModel();
                var param = new GSL02300ParameterDTO()
                {
                    CPROPERTY_ID = _viewModelPMT01500UnitInfo.loParameterList.CPROPERTY_ID!,
                    CBUILDING_ID = _viewModelPMT01500UnitInfo.loParameterList.CBUILDING_ID!,
                    CFLOOR_ID = "",
                    CSEARCH_TEXT = loGetData.CUNIT_ID!
                };

                if (string.IsNullOrWhiteSpace(loGetData.CUNIT_ID) || string.IsNullOrEmpty(loGetData.CUNIT_ID))
                    goto EndBlock;

                var loResult = await loLookupViewModel.GetBuildingUnit(param);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CUNIT_ID = "";
                    loGetData.CUNIT_NAME = "";
                    loGetData.CUNIT_TYPE_ID = "";
                    loGetData.CUNIT_TYPE_NAME = "";
                    loGetData.CFLOOR_ID = "";
                    loGetData.CBUILDING_ID = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CUNIT_ID = loResult.CUNIT_ID;
                    loGetData.CUNIT_NAME = loResult.CUNIT_NAME;
                    loGetData.CUNIT_TYPE_ID = loResult.CUNIT_TYPE_ID;
                    loGetData.CUNIT_TYPE_NAME = loResult.CUNIT_TYPE_NAME;
                    loGetData.CFLOOR_ID = loResult.CFLOOR_ID;
                    loGetData.CBUILDING_ID = loResult.CBUILDING_ID;

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }

        private void BeforeOpenLookUp_UnitID(R_BeforeOpenLookupEventArgs eventArgs)
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            var param = new GSL02300ParameterDTO()
            {
                CPROPERTY_ID = _viewModelPMT01500UnitInfo.loParameterList.CPROPERTY_ID,
                CBUILDING_ID = _viewModelPMT01500UnitInfo.loParameterList.CBUILDING_ID,
                CFLOOR_ID = ""
            };
#pragma warning restore CS8601 // Possible null reference assignment.
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL02300);
        }

        private void AfterOpenLookUp_UnitID(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL02300DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            var loGetData = (PMT01500UnitInfoUnitInfoDetailDTO)_conductorUnitInfo_UnitInfo.R_GetCurrentData();

            loGetData.CUNIT_ID = loTempResult.CUNIT_ID;
            loGetData.CUNIT_NAME = loTempResult.CUNIT_NAME;
            loGetData.CUNIT_TYPE_ID = loTempResult.CUNIT_TYPE_ID;
            loGetData.CUNIT_TYPE_NAME = loTempResult.CUNIT_TYPE_NAME;
            loGetData.CFLOOR_ID = loTempResult.CFLOOR_ID;
            loGetData.CBUILDING_ID = loTempResult.CBUILDING_ID;

        }

        #endregion
    }
}
