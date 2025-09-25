using BlazorClientHelper;
using PMT01500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using PMT01500Common.DTO._6._Deposit;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using PMT01500Common.Utilities;
using Lookup_PMFRONT;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Controls.MessageBox;
using PMT01500FrontResources;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMModel.ViewModel.LML00600;
using Lookup_PMModel.ViewModel.LML00200;
using PMT01500Common.Utilities.Front;
using PMT01500Common.DTO._4._Charges_Info;
using R_BlazorFrontEnd.Controls.Enums;
using R_LockingFront;
using PMT01500Common.DTO._2._Agreement;

namespace PMT01500Front
{
    public partial class PMT01500Deposit
    {

        private readonly PMT01500DepositViewModel _depositViewModel = new();
        [Inject] private IClientHelper? _clientHelper { get; set; }
        private R_Conductor? _conductorDeposit;
        private R_Grid<PMT01500DepositListDTO>? _gridDeposit;

        PMT01500EventCallBackDTO _oEventCallBack = new PMT01500EventCallBackDTO();
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _lControlContractor = false;
                _depositViewModel.loParameterList = (PMT01500GetHeaderParameterDTO)poParameter;

                if (!string.IsNullOrEmpty(_depositViewModel.loParameterList.CREF_NO))
                {
                    await _depositViewModel.GetDepositHeader();
                    await _gridDeposit.R_RefreshGrid(null);
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
                var loData = (PMT01500FrontDepositDetailDTO)eventArgs.Data;

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
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CREF_NO)
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
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CREF_NO)
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


        private async Task GetListDeposit(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _depositViewModel.GetDepositList();
                eventArgs.ListEntityResult = _depositViewModel.loListPMT01500Deposit;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }

        #region External Function

        private void OnChangeLCONTRACTOR(bool poParam)
        {
            var loException = new R_Exception();

            try
            {
                var loData = (PMT01500FrontDepositDetailDTO)_conductorDeposit.R_GetCurrentData();
                loData.LCONTRACTOR = poParam;

                if (poParam)
                {
                    if (_depositViewModel._lUsingContractor_ID)
                    {
                        loData.CCONTRACTOR_ID = _depositViewModel._oTempCCONTRACTOR_ID.CCONTRACTOR_ID;
                        loData.CCONTRACTOR_NAME = _depositViewModel._oTempCCONTRACTOR_ID.CCONTRACTOR_NAME;
                    }
                    else
                    {
                        loData.CCONTRACTOR_ID = "";
                        loData.CCONTRACTOR_NAME = "";
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(loData.CCONTRACTOR_ID))
                    {
                        _depositViewModel._oTempCCONTRACTOR_ID.CCONTRACTOR_ID = loData.CCONTRACTOR_ID;
                        _depositViewModel._oTempCCONTRACTOR_ID.CCONTRACTOR_NAME = loData.CCONTRACTOR_NAME!;
                        _depositViewModel._lUsingContractor_ID = true;
                    }
                    loData.CCONTRACTOR_ID = "";
                    loData.CCONTRACTOR_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

        }

        #endregion

        #region Conductor Function

        private bool _lControlContractor = true;

        private async Task SetOtherAsync(R_SetEventArgs eventArgs)
        {
            _lControlContractor = !eventArgs.Enable;
            _depositViewModel._lUsingContractor_ID = false;
            _oEventCallBack.LContractorOnCRUDmode = eventArgs.Enable;
            //_oEventCallBack.CREF_NO = _depositViewModel.loParameterList.CREF_NO!;
            await InvokeTabEventCallbackAsync(_oEventCallBack);
        }

        public async Task AfterDelete()
        {
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }

        public void ServiceAfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                var loData = (PMT01500FrontDepositDetailDTO)eventArgs.Data;
                loData.DDEPOSIT_DATE = DateTime.Now;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        private void R_Validation(R_ValidationEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                var loData = (PMT01500FrontDepositDetailDTO)eventArgs.Data;
                //await LostFocusCUNIT_ID();

                if (loData.LCONTRACTOR)
                {
                    if (string.IsNullOrWhiteSpace(loData.CCONTRACTOR_ID))
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01500_Class), "ValidationContractor");
                        loException.Add(loErr);
                    }
                }

                if (string.IsNullOrWhiteSpace(loData.CDEPOSIT_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01500_Class), "ValidationDepositID");
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

        #region Master CRUD
        private void ServiceR_DisplayAsync(R_DisplayEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                var loData = (PMT01500FrontDepositDetailDTO)eventArgs.Data;
                switch (eventArgs.ConductorMode)
                {
                    case R_eConductorMode.Normal:
                        //await _gridDeposit.R_RefreshGrid(null);
                        break;
                    case R_eConductorMode.Edit:
                        //Focus Async
                        //await _NCOMMON_AREA_SIZENumericTextBox.FocusAsync();
                        break;
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                PMT01500FrontDepositDetailDTO loData = R_FrontUtility
                    .ConvertObjectToObject<PMT01500FrontDepositDetailDTO>(eventArgs.Data);
                PMT01500FrontDepositDetailDTO loParam = new PMT01500FrontDepositDetailDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _depositViewModel.loParameterList.CPROPERTY_ID,
                    CDEPT_CODE = _depositViewModel.loParameterList.CDEPT_CODE,
                    CREF_NO = _depositViewModel.loParameterList.CREF_NO,
                    CTRANS_CODE = _depositViewModel.loParameterList.CTRANS_CODE,
                    CSEQ_NO = loData.CSEQ_NO,
                    CUSER_ID = _clientHelper.UserId
                };
                await _depositViewModel.GetEntity(loParam);
                eventArgs.Result = _depositViewModel.loEntityDeposit;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01500FrontDepositDetailDTO>(eventArgs.Data);

                await _depositViewModel.ServiceSave(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _depositViewModel.loEntityDeposit;
                //await _gridDeposit.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01500FrontDepositDetailDTO)eventArgs.Data;
                await _depositViewModel.ServiceDelete(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Master Look up

        #region Lookup Button Contractor Lookup

        private R_Lookup? R_LookupContractorLookup;

        private void BeforeOpenLookUpContractorLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00600ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_depositViewModel.loParameterList.CPROPERTY_ID))
            {
                param = new LML00600ParameterDTO
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _depositViewModel.loParameterList.CPROPERTY_ID,
                    CCUSTOMER_TYPE = "02",
                    CUSER_ID = _clientHelper.UserId
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00600);
        }

        private void AfterOpenLookUpContractorLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            LML00600DTO? loTempResult = null;
            //LMM01500AgreementDetailDTO? loGetData = null;


            try
            {
                loTempResult = (LML00600DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //loGetData = (LMM01500AgreementDetailDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _depositViewModel.Data.CCONTRACTOR_ID = loTempResult.CTENANT_ID;
                _depositViewModel.Data.CCONTRACTOR_NAME = loTempResult.CTENANT_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusContractor()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT01500FrontDepositDetailDTO loGetData = (PMT01500FrontDepositDetailDTO)_depositViewModel.Data;

                if (string.IsNullOrWhiteSpace(_depositViewModel.Data.CCONTRACTOR_ID))
                {
                    loGetData.CCONTRACTOR_ID = "";
                    loGetData.CCONTRACTOR_NAME = "";
                    return;
                }

                LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel();
                LML00600ParameterDTO loParam = new LML00600ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _depositViewModel.loParameterList.CPROPERTY_ID!,
                    CCUSTOMER_TYPE = "02",
                    CUSER_ID = _clientHelper.UserId,
                    CSEARCH_TEXT = loGetData.CCONTRACTOR_ID ?? "",
                };

                var loResult = await loLookupViewModel.GetTenant(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CCONTRACTOR_ID = "";
                    loGetData.CCONTRACTOR_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CCONTRACTOR_ID = loResult.CTENANT_ID;
                    loGetData.CCONTRACTOR_NAME = loResult.CTENANT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #region Lookup Button DepositJG Lookup

        private R_Lookup? R_LookupDepositJGLookup;

        private void BeforeOpenLookUpDepositJGLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00200ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_depositViewModel.loParameterList.CPROPERTY_ID))
            {
                param = new LML00200ParameterDTO
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _depositViewModel.loParameterList.CPROPERTY_ID,
                    CCHARGE_TYPE_ID = "03",
                    CUSER_ID = _clientHelper.UserId,
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00200);
        }

        private void AfterOpenLookUpDepositJGLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            LML00200DTO? loTempResult = null;
            //LMM01500AgreementDetailDTO? loGetData = null;


            try
            {
                loTempResult = (LML00200DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //loGetData = (LMM01500AgreementDetailDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _depositViewModel.Data.CDEPOSIT_ID = loTempResult.CCHARGES_ID;
                _depositViewModel.Data.CDEPOSIT_NAME = loTempResult.CCHARGES_NAME;
                //_depositViewModel.Data.CDEPOSIT_JRNGRP_TYPE = loTempResult.CC;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }
        
        private async Task OnLostFocusDepositJG()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT01500FrontDepositDetailDTO loGetData = (PMT01500FrontDepositDetailDTO)_depositViewModel.Data;

                if (string.IsNullOrWhiteSpace(_depositViewModel.Data.CDEPOSIT_ID))
                {
                    loGetData.CDEPOSIT_ID = "";
                    loGetData.CDEPOSIT_NAME = "";
                    return;
                }

                LookupLML00200ViewModel loLookupViewModel = new LookupLML00200ViewModel();
                LML00200ParameterDTO loParam = new LML00200ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _depositViewModel.loParameterList.CPROPERTY_ID!,
                    CCHARGE_TYPE_ID = "03",
                    CUSER_ID = _clientHelper.UserId,
                    CSEARCH_TEXT = loGetData.CDEPOSIT_ID ?? "",
                };

                var loResult = await loLookupViewModel.GetUnitCharges(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CDEPOSIT_ID = "";
                    loGetData.CDEPOSIT_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CDEPOSIT_ID = loResult.CCHARGES_ID;
                    loGetData.CDEPOSIT_NAME = loResult.CCHARGES_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #endregion
    }
}
