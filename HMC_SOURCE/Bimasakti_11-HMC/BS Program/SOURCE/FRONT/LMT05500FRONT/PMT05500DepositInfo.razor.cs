using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls;
using PMT05500COMMON.DTO;
using PMT05500Model;
using PMT05500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using BlazorClientHelper;
using Lookup_PMModel.ViewModel.LML00200;
using Lookup_PMModel.ViewModel.LML00600;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Controls.Enums;
using R_LockingFront;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;

namespace PMT05500Front
{
    public partial class PMT05500DepositInfo
    {
        private LMT05500DepositViewModel _depositViewModel = new();

        private R_Conductor? _conductDepositInfo;
        private R_Lookup? R_Lookup_Button_Contr;
        private R_Lookup? R_Lookup_Button_Deposit;
        [Inject] IClientHelper? _clientHelper { get; set; }
        private R_TextBox? FocusLabel;
        private bool _lEnableFieldContractor;
        private bool _isDataExist;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                LMT05500DepositListDTO loParam = (LMT05500DepositListDTO)poParameter;
                _depositViewModel._currentDeposit = loParam;
               
                 if (!string.IsNullOrEmpty(loParam.CSEQ_NO))
                {
                    var loDataconvert = R_FrontUtility.ConvertObjectToObject<LMT05500DepositInfoFrontDTO>(loParam);
                    await _conductDepositInfo?.R_GetEntity(loDataconvert)!;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region CRUD
        private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            LMT05500DepositInfoFrontDTO currentData = (LMT05500DepositInfoFrontDTO)eventArgs.Data;

            try
            {
                if (currentData != null)
                {
                    LMT05500DepositInfoFrontDTO loParam = new LMT05500DepositInfoFrontDTO()
                    {
                        CPROPERTY_ID = currentData.CPROPERTY_ID,
                        CDEPT_CODE = currentData.CDEPT_CODE,
                        CTRANS_CODE = currentData.CTRANS_CODE,
                        CREF_NO = currentData.CREF_NO,
                        CSEQ_NO = currentData.CSEQ_NO
                    };

                    await _depositViewModel.GetEntity(loParam);
                    _isDataExist = !string.IsNullOrEmpty(_depositViewModel._depositInfoData.CSEQ_NO);
                    eventArgs.Result = _depositViewModel._depositInfoData;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void SetOther(R_SetEventArgs eventArgs)
        {
            _lEnableFieldContractor = !eventArgs.Enable;
            if (!string.IsNullOrEmpty(_depositViewModel._depositInfoData.CSEQ_NO))
                _isDataExist = eventArgs.Enable;
            else
                _isDataExist = false;

        }
        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMT05500DepositInfoFrontDTO>(eventArgs.Data);
                await _depositViewModel.ServiceSave(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _depositViewModel._depositInfoData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void ServiceSaving(R_SavingEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {

                var loParam = (LMT05500DepositInfoFrontDTO)eventArgs.Data;
                if (!loParam.LCONTRACTOR)
                {
                    loParam.CCONTRACTOR_ID = "";
                    loParam.CCONTRACTOR_NAME = "";
                }
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
                var loData = (LMT05500DepositInfoFrontDTO)eventArgs.Data;

                await _depositViewModel.ServiceDelete(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void R_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loCurrentData = (LMT05500DepositInfoFrontDTO)eventArgs.Data;

                _depositViewModel.ValidationFieldEmpty(loCurrentData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            eventArgs.Cancel = loEx.HasError;
            loEx.ThrowExceptionIfErrors();
        }

        #endregion
        #region MyRegion
        private async Task AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            eventArgs.Data = new LMT05500DepositInfoFrontDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CUSER_ID = _clientHelper.UserId,
                CPROPERTY_ID = _depositViewModel._currentDeposit.CPROPERTY_ID,
                LCONTRACTOR = false,
                CDEPT_CODE = _depositViewModel._currentDeposit.CDEPT_CODE,
                CTRANS_CODE = _depositViewModel._currentDeposit.CTRANS_CODE,
                CREF_NO = _depositViewModel._currentDeposit.CREF_NO,
                // CLOCAL_CURRENCY = _depositViewModel._currentDeposit.CC
            };
            // Focus Async
            await FocusLabel!.FocusAsync();

        }
        private void AfterDelete()
        {
            var loEx = new R_Exception();
            try
            {
                _isDataExist = false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private async Task ServiceDisplay(R_DisplayEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                switch (eventArgs.ConductorMode)
                {
                    case R_eConductorMode.Edit:
                        //Focus Async
                        await FocusLabel!.FocusAsync();
                        break;
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        #endregion
        #region checbox
        private void CheckboxContractor(object poParam)
        {
            var loEx = new R_Exception();
            bool lbCheckbox = (bool)poParam;
            try
            {
                #region BookingFee
                //when contractor have value
                if (!string.IsNullOrEmpty(_depositViewModel.Data.CCONTRACTOR_ID))
                {
                    _depositViewModel._IsDataNull = false;

                    _depositViewModel.TemporaryData = new LMT05500DepositInfoFrontDTO()
                    {
                        CCONTRACTOR_ID = _depositViewModel.Data.CCONTRACTOR_ID,
                        CCONTRACTOR_NAME = _depositViewModel.Data.CCONTRACTOR_NAME
                    };
                }
                if (!lbCheckbox)
                {
                    _depositViewModel.Data.CCONTRACTOR_ID = "";
                    _depositViewModel.Data.CCONTRACTOR_NAME = "";
                }
                //re assign value from user
                if (lbCheckbox && !_depositViewModel._IsDataNull)
                {
                    _depositViewModel.Data.CCONTRACTOR_ID = _depositViewModel.TemporaryData.CCONTRACTOR_ID;
                    _depositViewModel.Data.CCONTRACTOR_NAME = _depositViewModel.TemporaryData.CCONTRACTOR_NAME;
                }
                #endregion
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion
        #region Lookup
        private void BeforeOpenLookUp_Contractor(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new LML00600ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper!.CompanyId,
                CUSER_ID = _clientHelper.UserId,
                CPROPERTY_ID = _depositViewModel._currentDeposit.CPROPERTY_ID,
                CCUSTOMER_TYPE = "02"
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00600);

        }
        private void AfterOpenLookUp_Contractor(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00600DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            var loGetData = (LMT05500DepositInfoFrontDTO)_conductDepositInfo!.R_GetCurrentData();

            loGetData.CCONTRACTOR_ID = loTempResult.CTENANT_ID!;
            loGetData.CCONTRACTOR_NAME = loTempResult.CTENANT_NAME!;
        }
        private void BeforeOpenLookUp_Deposit(R_BeforeOpenLookupEventArgs eventArgs)
        {

            var param = new LML00200ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CUSER_ID = _clientHelper.UserId,
                CPROPERTY_ID = _depositViewModel._currentDeposit.CPROPERTY_ID,
                CCHARGE_TYPE_ID = "03"
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00200);
        }
        private void AfterOpenLookUp_Deposit(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00200DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            var loGetData = (LMT05500DepositInfoFrontDTO)_conductDepositInfo!.R_GetCurrentData();

            loGetData.CDEPOSIT_ID = loTempResult.CCHARGES_ID;
            loGetData.CDEPOSIT_NAME = loTempResult.CCHARGES_NAME;
            loGetData.CLOCAL_CURRENCY = _depositViewModel._currentDeposit.CCURRENCY_CODE;
        }
        #endregion

        #region OnLostFokus
        private async Task LostFocusLookupContractor()
        {
            var loEx = new R_Exception();

            try
            {
                var loGetData = _depositViewModel.Data;
                if (!string.IsNullOrWhiteSpace(loGetData.CCONTRACTOR_ID))
                {
                    LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel();
                    var param = new LML00600ParameterDTO
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CPROPERTY_ID = _depositViewModel._currentDeposit.CPROPERTY_ID,
                        CUSER_ID = _clientHelper.UserId,
                        CCUSTOMER_TYPE = "02",
                        CSEARCH_TEXT = _depositViewModel.Data.CCONTRACTOR_ID!,
                    };
                    var loResult = await loLookupViewModel.GetTenant(param);


                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                                "_ErrLookup01"));
                        loGetData.CCONTRACTOR_ID = "";
                        loGetData.CCONTRACTOR_NAME = "";
                    }
                    else
                    {
                        loGetData.CCONTRACTOR_ID = loResult.CTENANT_ID;
                        loGetData.CCONTRACTOR_NAME = loResult.CTENANT_NAME;
                    }

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task LostFocusLookupDeposit()
        {
            var loEx = new R_Exception();

            try
            {
                var loGetData = _depositViewModel.Data;
                if (!string.IsNullOrWhiteSpace(loGetData.CDEPOSIT_ID))
                {
                    LookupLML00200ViewModel loLookupViewModel = new LookupLML00200ViewModel();
                    var param = new LML00200ParameterDTO
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CPROPERTY_ID = _depositViewModel._currentDeposit.CPROPERTY_ID,
                        CUSER_ID = _clientHelper.UserId,
                        CCHARGE_TYPE_ID = "03",
                        CSEARCH_TEXT = _depositViewModel.Data.CDEPOSIT_ID!,
                    };
                    LML00200DTO loResult = await loLookupViewModel.GetUnitCharges(param);


                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                                "_ErrLookup01"));
                        loGetData.CDEPOSIT_ID = "";
                        loGetData.CDEPOSIT_NAME = "";
                        loGetData.CLOCAL_CURRENCY = "";
                    }
                    else
                    {
                        loGetData.CDEPOSIT_ID = loResult.CCHARGES_ID;
                        loGetData.CDEPOSIT_NAME = loResult.CCHARGES_NAME;
                        loGetData.CLOCAL_CURRENCY = _depositViewModel._currentDeposit.CCURRENCY_CODE;
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
                var loData = (LMT05500DepositInfoFrontDTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper!.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "PMT05500",
                        Table_Name = "PMT_AGREEMENT_DEPOSIT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO, loData.CSEQ_NO)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper!.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "PMT05500",
                        Table_Name = "PMT_AGREEMENT_DEPOSIT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO, loData.CSEQ_NO)
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
