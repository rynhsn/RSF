using BlazorClientHelper;
using ICT00900COMMON.DTO;
using ICT00900COMMON.Param;
using ICT00900FrontResources;
using ICT00900MODEL.ICT00900ViewModel;
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
using R_CommonFrontBackAPI;
using R_LockingFront;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ICT00900FRONT
{
    public partial class ICT00900Detail : R_Page
    {
        private ICT00900ViewModel _viewModel = new();
        private R_Conductor? _conducorAdjDetail;

        [Inject] IClientHelper? _clientHelper { get; set; }
        private R_TextBox? FocusLabel;
        private R_TextBox? FocusLabelEdit;
        private bool _isDataExist;

        private R_Lookup? R_Lookup_Button_Dept;
        private R_Lookup? R_Lookup_Button_Product;
        private R_Lookup? R_Lookup_Button_Allocation;
        private string? TransactionCode = "505010";
        private string? TransactionCodeDesc = "Adjustment";
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                ICT00900AdjustmentDTO loParam = R_FrontUtility.ConvertObjectToObject<ICT00900AjustmentDetailDTO>(poParameter);
                _viewModel.ParameterGetDetail = R_FrontUtility.ConvertObjectToObject<ICT00900AjustmentDetailDTO>(loParam);
                await _viewModel.GetVarTransactionCode();
                await _viewModel.GetVarCompanyInfo();
                await _viewModel.GetCurrencyList();
                await InvokeTabEventCallbackAsync(false);
                if (!string.IsNullOrEmpty(loParam.CREF_NO))
                {
                    await _conducorAdjDetail?.R_GetEntity(_viewModel.ParameterGetDetail)!;
                }
                else
                {
                    _viewModel.lControlButtonRedraft = false;
                    _viewModel.lControlButtonSubmit = false;
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
            ICT00900AjustmentDetailDTO currentData = R_FrontUtility.ConvertObjectToObject<ICT00900AjustmentDetailDTO>(eventArgs.Data);
            ICT00900AjustmentDetailDTO loParam;
            try
            {
                loParam = new ICT00900AjustmentDetailDTO();
                if (eventArgs.Data != null)
                {
                    loParam = new ICT00900AjustmentDetailDTO()
                    {
                        CPROPERTY_ID = currentData.CPROPERTY_ID,
                        CREF_NO = currentData.CREF_NO
                    };
                }
                else
                {
                    loParam = new ICT00900AjustmentDetailDTO()
                    {
                        CPROPERTY_ID = _viewModel.ParameterGetDetail.CPROPERTY_ID,
                        CREF_NO = _viewModel.ParameterGetDetail.CREF_NO
                    };
                }
                await _viewModel.GetEntity(loParam);
                _isDataExist = !string.IsNullOrEmpty(_viewModel.Data.CREF_NO);
                eventArgs.Result = _viewModel.oEntityAdjustmentDetail;

                switch (_viewModel.oEntityAdjustmentDetail.CTRANS_STATUS)
                {
                    case "00":
                        _viewModel.lControlButtonRedraft = false;
                        _viewModel.lControlButtonSubmit = true;
                        break;
                    case "10":
                        _viewModel.lControlButtonSubmit = false;
                        _viewModel.lControlButtonRedraft = true;
                        break;
                    case "30":
                        _viewModel.lControlButtonRedraft =
                        _viewModel.lControlButtonSubmit = false;
                        break;
                    case "80":
                    case "98":
                        _viewModel.lControlButtonRedraft =
                        _viewModel.lControlButtonSubmit = false;
                        break;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void SetOtherAsync(R_SetEventArgs eventArgs)
        {
            _viewModel.lControlCRUDMode = eventArgs.Enable;
            //await InvokeTabEventCallbackAsync(rolCRUDMode);
            if (!string.IsNullOrEmpty(_viewModel.oEntityAdjustmentDetail.CREF_NO))
                _isDataExist = eventArgs.Enable;
            else
                _isDataExist = false;

        }
        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<ICT00900AjustmentDetailDTO>(eventArgs.Data);
                await _viewModel.ServiceSave(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel.oEntityAdjustmentDetail;
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
                var loData = (ICT00900AjustmentDetailDTO)eventArgs.Data;
                await _viewModel.ServiceDelete(loData);
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
                var loCurrentData = (ICT00900AjustmentDetailDTO)eventArgs.Data;

                _viewModel.ValidationFieldEmpty(loCurrentData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            eventArgs.Cancel = loEx.HasError;
            loEx.ThrowExceptionIfErrors();
        }
        private async Task AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            _viewModel._lDataCREF_NO = !_viewModel.VarTransaction.LINCREMENT_FLAG;
            eventArgs.Data = new ICT00900AjustmentDetailDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CUSER_ID = _clientHelper.UserId,
                CPROPERTY_ID = _viewModel.ParameterGetDetail.CPROPERTY_ID,
                CTRANS_CODE = TransactionCode,
                CTRANS_CODE_DESCR = TransactionCodeDesc,
                CLOCAL_CURRENCY_CODE = _viewModel.VarCompanyInfo.CLOCAL_CURRENCY_CODE,
                CBASE_CURRENCY_CODE = _viewModel.VarCompanyInfo.CBASE_CURRENCY_CODE,

            };
            // Focus Async
            await FocusLabel!.FocusAsync();
            #endregion
        }
        private async Task AfterDelete()
        {
            var loEx = new R_Exception();
            try
            {
                _isDataExist = false;
                await R_MessageBox.Show("", @_localizer["Delete_Success"], R_eMessageBoxButtonType.OK);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private void ServiceDisplay(R_DisplayEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                switch (eventArgs.ConductorMode)
                {
                    case R_eConductorMode.Edit:
                        break;
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
        public async Task R_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                var res = await R_MessageBox.Show("", @_localizer["ValidationBeforeCancel"],
                    R_eMessageBoxButtonType.YesNo);
                if (res == R_eMessageBoxResult.No)
                {
                    eventArgs.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        private async Task R_SetEditAsync(R_SetEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                _viewModel._lDataCREF_NO = false;
                _viewModel.lControlCRUDMode = eventArgs.Enable;
                //_oEventCallBack.LCRUD_MODE = _viewModel.lControlCRUDMode = eventArgs.Enable;
                //await InvokeTabEventCallbackAsync(_viewModel.lControlCRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }
        private void R_CheckEdit(R_CheckEditEventArgs eventArgs)
        {
            var loException = new R_Exception();
            //Task loTask;

            try
            {
                eventArgs.Allow = _viewModel.oEntityAdjustmentDetail.CTRANS_STATUS == "00";
            }
            catch (Exception Ex)
            {
                loException.Add(Ex);
            }

            R_DisplayException(loException);
        }
        private void R_CheckDelete(R_CheckDeleteEventArgs eventArgs)
        {
            var loException = new R_Exception();
            try
            {
                eventArgs.Allow = _viewModel.oEntityAdjustmentDetail.CTRANS_STATUS == "00";
            }
            catch (Exception Ex)
            {
                loException.Add(Ex);
            }

            R_DisplayException(loException);
        }
        private void OnChangedCurrencyCode(string pcParam)
        {
            R_Exception loException = new R_Exception();
            ICT00900AjustmentDetailDTO? loData = _viewModel.Data;

            try
            {
                loData.CCURRENCY_CODE = pcParam;
                loData.CCURRENCY_NAME = _viewModel.CurrencyList.FirstOrDefault(c => c.CCURRENCY_CODE == pcParam)?.CCURRENCY_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }
        #region Button
        private async Task SubmitBtn()
        {
            var loEx = new R_Exception();

            await lockingButton(true);
            try
            {
                //SUBMIT CODE == "10"
                bool llConfirmation = await R_MessageBox.Show("Confirmation",
                    R_FrontUtility.R_GetMessage(typeof(Resources_ICT00900_Class), "_ConfirmationSubmit"),
                    R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.Yes;

                if (llConfirmation)
                {
                    _viewModel.ParameterChangeStatus = R_FrontUtility.ConvertObjectToObject<ICT00900ParameterChangeStatusDTO>(_conducorAdjDetail.R_GetCurrentData());
                    _viewModel.ParameterChangeStatus.CTRANS_CODE = TransactionCode;


                    var loReturn = await _viewModel.ChangeStatusAdjustment(lcNewStatus: "10");
                    if (loReturn.IS_PROCESS_CHANGESTS_SUCCESS)
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_ICT00900_Class), "_SuccessMessageOfferSubmit"));
                        await _conducorAdjDetail.R_GetEntity(null);
                    }
                    else
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_ICT00900_Class), "_FailedUpdate"));
                    }
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            finally
            {
                await lockingButton(false);
            }
            R_DisplayException(loEx);
        }
        private async Task RedraftBtn()
        {
            var loEx = new R_Exception();

            await lockingButton(true);
            try
            {
                //REDRAFT CODE == "00"
                bool llConfirmation = await R_MessageBox.Show("Confirmation",
                  R_FrontUtility.R_GetMessage(typeof(Resources_ICT00900_Class), "_ConfirmationRedraft"),
                  R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.Yes;

                if (llConfirmation)
                {
                    _viewModel.ParameterChangeStatus = R_FrontUtility.ConvertObjectToObject<ICT00900ParameterChangeStatusDTO>(_conducorAdjDetail.R_GetCurrentData());
                    _viewModel.ParameterChangeStatus.CTRANS_CODE = TransactionCode;


                    var loReturn = await _viewModel.ChangeStatusAdjustment(lcNewStatus: "00");
                    if (loReturn.IS_PROCESS_CHANGESTS_SUCCESS)
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_ICT00900_Class), "_SuccessMessageOfferRedraft"));
                        await _conducorAdjDetail.R_GetEntity(null);
                    }
                    else
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_ICT00900_Class), "_FailedUpdate"));
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            finally
            {
                await lockingButton(false);

            }
            R_DisplayException(loEx);
        }
        #endregion

        #region LookUp
        private void BeforeOpenLookUp_Department(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00700ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper!.CompanyId,
                CUSER_ID = _clientHelper.UserId,
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00700);

        }
        private void AfterOpenLookUpDepartment(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL00700DTO? loTempResult = null;
            //PMT01100LOO_Offer_SelectedOfferDTO? loGetData = null;


            try
            {
                loTempResult = (GSL00700DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //loGetData = (PMT01100LOO_Offer_SelectedOfferDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _viewModel.Data.CDEPT_CODE = loTempResult.CDEPT_CODE;
                _viewModel.Data.CDEPT_NAME = loTempResult.CDEPT_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }
        private async Task OnLostFocusDepartment()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                ICT00900AjustmentDetailDTO loGetData = _viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CDEPT_CODE))
                {
                    loGetData.CDEPT_CODE = "";
                    loGetData.CDEPT_NAME = "";
                    return;
                }

                LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel();
                GSL00700ParameterDTO loParam = new GSL00700ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CSEARCH_TEXT = loGetData.CDEPT_CODE ?? "",
                };
                var loResult = await loLookupViewModel.GetDepartment(loParam);
                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CDEPT_CODE = "";
                    loGetData.CDEPT_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CDEPT_CODE = loResult.CDEPT_CODE;
                    loGetData.CDEPT_NAME = loResult.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private void BeforeOpenLookUp_Product(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL03000ParameterDTO()
            {
                CCATEGORY_ID = "0",
                CTAXABLE_TYPE = "0",
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL03000);

        }
        private void AfterOpenLookProduct(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL03000DTO? loTempResult = null;
            //PMT01100LOO_Offer_SelectedOfferDTO? loGetData = null;


            try
            {
                loTempResult = (GSL03000DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //loGetData = (PMT01100LOO_Offer_SelectedOfferDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _viewModel.Data.CPRODUCT_ID = loTempResult.CPRODUCT_ID;
                _viewModel.Data.CPRODUCT_NAME = loTempResult.CPRODUCT_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }
        private async Task OnLostFocusProduct()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                ICT00900AjustmentDetailDTO loGetData = _viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CPRODUCT_ID))
                {
                    loGetData.CPRODUCT_ID = "";
                    loGetData.CPRODUCT_NAME = "";
                    return;
                }

                LookupGSL03000ViewModel loLookupViewModel = new LookupGSL03000ViewModel();
                GSL03000ParameterDTO loParam = new GSL03000ParameterDTO()
                {
                    CCATEGORY_ID = "0",
                    CTAXABLE_TYPE = "0",
                    CSEARCH_TEXT = loGetData.CPRODUCT_ID ?? "",
                };
                var loResult = await loLookupViewModel.GetProduct(loParam);
                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CPRODUCT_ID = "";
                    loGetData.CPRODUCT_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CPRODUCT_ID = loResult.CPRODUCT_ID;
                    loGetData.CPRODUCT_NAME = loResult.CPRODUCT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private void BeforeOpenLookUp_Allocation(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL03200ParameterDTO()
            {
                CACTIVE_TYPE = "Active",
                CDEPT_CODE = _viewModel.Data.CDEPT_CODE ?? "",
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL03200);

        }
        private void AfterOpenLookAllocation(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL03200DTO? loTempResult = null;
            //PMT01100LOO_Offer_SelectedOfferDTO? loGetData = null;


            try
            {
                loTempResult = (GSL03200DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //loGetData = (PMT01100LOO_Offer_SelectedOfferDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _viewModel.Data.CALLOC_ID = loTempResult.CALLOC_ID;
                _viewModel.Data.CALLOC_NAME = loTempResult.CALLOC_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }
        private async Task OnLostFocusAlloaction()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                ICT00900AjustmentDetailDTO loGetData = _viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CDEPT_CODE))
                {
                    loGetData.CALLOC_ID = "";
                    loGetData.CALLOC_NAME = "";
                    return;
                }

                LookupGSL03200ViewModel loLookupViewModel = new LookupGSL03200ViewModel();
                GSL03200ParameterDTO loParam = new GSL03200ParameterDTO()
                {
                    CACTIVE_TYPE = "Active",
                    CDEPT_CODE = _viewModel.Data.CDEPT_CODE ?? "",
                    CSEARCH_TEXT = loGetData.CALLOC_ID ?? "",
                };
                var loResult = await loLookupViewModel.GetProductAllocation(loParam);
                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CALLOC_ID = "";
                    loGetData.CALLOC_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CALLOC_ID = loResult.CALLOC_ID;
                    loGetData.CALLOC_NAME = loResult.CALLOC_NAME;
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

        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlIC";
        private const string DEFAULT_MODULE_NAME = "IC";

        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult? loLockResult = null;

            try
            {
                var loData = (ICT00900AjustmentDetailDTO)eventArgs.Data;

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
                        Program_Id = "ICT00900",
                        Table_Name = "ICT_TRANS_HD",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, TransactionCode, loData.CREF_NO)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "ICT00900",
                        Table_Name = "ICT_TRANS_HD",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, TransactionCode, loData.CREF_NO)
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


        private async Task lockingButton(bool param)
        {
            var loEx = new R_Exception();
            R_LockingFrontResult loLockResult = null;
            try
            {
                var loData = _viewModel.oEntityAdjustmentDetail;
                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                plSendWithContext: true,
                plSendWithToken: true,
                pcHttpClientName: DEFAULT_HTTP_NAME);
                if (param) // Lock
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "ICT00900",
                        Table_Name = "ICT_TRANS_HD",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, TransactionCode, loData.CREF_NO)
                    };
                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else // Unlock
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "ICT00900",
                        Table_Name = "ICT_TRANS_HD",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, TransactionCode, loData.CREF_NO)
                    };
                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }
                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                    throw loLockResult.Exception;
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
