using CBT02300COMMON;
using CBT02300MODEL.ViewModel;
using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using Microsoft.AspNetCore.Components;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Enums;
using CBT02300COMMON.Master_DTO;
using BlazorClientHelper;
using R_BlazorFrontEnd.Controls.MessageBox;
using CBT02300FrontResources;
using R_APICommonDTO;

namespace CBT02300FRONT
{
    public partial class CBT02300
    {
        private CBT02300BankInChequeViewModel _viewModel = new();

        private R_ConductorGrid? _conductorBankInChequeRef;
        private R_Grid<CBT02300BankInChequeFrontDTO>? _gridBankInChequeRef;
        [Inject] IClientHelper? _clientHelper { get; set; }
        private bool enableBtnBankIn = false;
        string? _lcType;
        private void StateChangeInvoke()
        {
            StateHasChanged();
        }
        private void DisplayErrorInvoke(R_APIException poException)
        {
            var loEx = R_FrontUtility.R_ConvertFromAPIException(poException);
            this.R_DisplayException(loEx);
        }
        public async Task ShowSuccessInvoke()
        {
            await R_MessageBox.Show("", $"{_lcType} cheque(s) processed successfully!”", R_eMessageBoxButtonType.OK);
        }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {

                _viewModel._typeBankIn = "BANK_IN";
                _viewModel.StateChangeAction = StateChangeInvoke;
                _viewModel.DisplayErrorAction = DisplayErrorInvoke;
                _viewModel.ShowSuccessAction = async () =>
                {
                    await ShowSuccessInvoke();
                };
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        #region BankInCheque
        private async Task GetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {

                await _viewModel.GetBankInChequeList("BANK_IN");
                eventArgs.ListEntityResult = _viewModel.BankInChequeList;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Service_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loParam = (CBT02300BankInChequeFrontDTO)eventArgs.Data;
                    if (loParam.CSTATUS_NAME.Equals("Bank In", StringComparison.OrdinalIgnoreCase))
                    {
                        enableBtnBankIn = false;
                    }
                    else
                    {
                        enableBtnBankIn = true;
                    }
                    await ServiceGetRecord(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task ServiceGetRecord(CBT02300BankInChequeFrontDTO loParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loParamTemp = R_FrontUtility.ConvertObjectToObject<CBT02300ChequeInfoFrontDTO>(loParam);
                await _viewModel.GetChequeInfo(loParamTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Open Another Tab
        private void PreDock_TabHold_InstantiateDock(R_InstantiateDockEventArgs eventArgs)
        {
            eventArgs.Parameter = "Hold";
            eventArgs.TargetPageType = typeof(CBT02300Hold);
        }

        private void PreDock_TabClear_InstantiateDock(R_InstantiateDockEventArgs eventArgs)
        {
            eventArgs.Parameter = "Clear";
            eventArgs.TargetPageType = typeof(CBT02300Clear);
        }
        private void PreDock_TabBounce_InstantiateDock(R_InstantiateDockEventArgs eventArgs)
        {
            eventArgs.Parameter = "Bounce";
            eventArgs.TargetPageType = typeof(CBT02300Bounce);
        }
        private void PreDock_TabUndoClear_InstantiateDock(R_InstantiateDockEventArgs eventArgs)
        {
            eventArgs.Parameter = "Undo Clear";
            eventArgs.TargetPageType = typeof(CBT02300UndoClear);
        }
        private void AfterOpenPredifinedDock(R_AfterOpenPredefinedDockEventArgs eventArgs)
        {
            _viewModel.BankInChequeList = new();
            _viewModel.BankInChequeInfo = new();
            _viewModel.loParamaterFilter = new();
            _viewModel._enableBtn = false;
        }
        #endregion
        #region Button
        private async Task BtnRefresh()
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.ValidationFieldEmpty();
                await _gridBankInChequeRef!.R_RefreshGrid(null)!;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task BankInBtn()
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel._typeBankIn = "BANK_IN";
                _viewModel.leTypeBank = CBT02300BankInChequeViewModel.TYPE_BANK_IM_CHEQUE.Bank_in;

                await _gridBankInChequeRef!.R_SaveBatch();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task UndoBankInBtn()
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel._typeBankIn = "UNDO_BANK_IN";
                _viewModel.leTypeBank = CBT02300BankInChequeViewModel.TYPE_BANK_IM_CHEQUE.Undo_Bank_In;

                await _gridBankInChequeRef!.R_SaveBatch();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion
        #region Save Batch
        private async Task ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loList = (List<CBT02300BankInChequeFrontDTO>)eventArgs.Data;
                var lcAction = _viewModel._typeBankIn;
                CBT02300ProcessDataDTO poParam = new()
                {
                    CCOMPANY_ID = _clientHelper?.CompanyId,
                    CUSER_ID = _clientHelper?.UserId,
                    CACTION = lcAction,
                    CPROCESS_DATE = _viewModel.loParamaterFilter?.CDATE_FRONT?.ToString("yyyyMMdd"),
                    CREASON = ""
                };

                _viewModel.GetSelectedData(poParam, loList);

                _lcType = (_viewModel._typeBankIn == "BANK_IN") ? "Bank In" : "Undo Bank In";

                string lcResourcesMessage = _lcType == "Bank In" ? "ConfirmationBankIn" : "ConfirmationUndoBankIn";

                var lcMessage = R_FrontUtility.R_GetMessage(typeof(Resources_CBT02300_Class), lcResourcesMessage);

                if (await R_MessageBox.Show("Confirmation",
                  lcMessage, R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.Yes)
                {
                    await _viewModel.ProcessData();

                    _gridBankInChequeRef!.DataSource.Clear();
                    _viewModel.BankInChequeInfo = new();
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task AfterSaveBatch(R_AfterSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _gridBankInChequeRef!.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Lookup
        private void BeforeOpenLookUp_Dept(R_BeforeOpenLookupEventArgs eventArgs)
        {

            var param = new GSL00700ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper!.CompanyId,
                CUSER_ID = _clientHelper!.UserId,
                CPROGRAM_ID = "",
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00700);
        }
        private void AfterOpenLookUp_Dept(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            var loGetData = _viewModel.loParamaterFilter!;

            loGetData.CDEPT_CODE = loTempResult.CDEPT_CODE;
            loGetData.CDEPT_NAME = loTempResult.CDEPT_NAME;

            //this Object for handle on lost focus in the lookup
            _viewModel.loTempParamaterFilter!.CDEPT_CODE = _viewModel.loParamaterFilter!.CDEPT_CODE;

            _gridBankInChequeRef!.DataSource.Clear();
            _viewModel.BankInChequeInfo = new();
        }
        private void BeforeOpenLookUp_BankCode(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL02500ParameterDTO()
            {
                CDEPT_CODE = _viewModel.loParamaterFilter!.CDEPT_CODE!,
                CCB_TYPE = "B",
                CBANK_TYPE = "I"
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL02500);
        }
        private void AfterOpenLookUp_BankCode(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL02500DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            var loGetData = _viewModel.loParamaterFilter!;

            loGetData.CCB_CODE = loTempResult.CCB_CODE;
            loGetData.CCB_NAME = loTempResult.CCB_NAME;

            //this Object for handle on lost focus in the lookup
            _viewModel.loTempParamaterFilter!.CCB_CODE = _viewModel.loParamaterFilter!.CCB_CODE;

            _gridBankInChequeRef!.DataSource.Clear();
            _viewModel.BankInChequeInfo = new();
        }
        private void BeforeOpenLookUp_AccountNo(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL02600ParameterDTO()
            {
                CDEPT_CODE = _viewModel.loParamaterFilter!.CDEPT_CODE!,
                CCB_CODE = _viewModel.loParamaterFilter!.CCB_CODE!,
                CCB_TYPE = "B",
                CBANK_TYPE = "I"
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL02600);
        }
        private void AfterOpenLookUp_AccountNo(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL02600DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            var loGetData = _viewModel.loParamaterFilter!;

            loGetData.CCB_ACCOUNT_NO = loTempResult.CCB_ACCOUNT_NO;
            loGetData.CCB_ACCOUNT_NAME = loTempResult.CCB_ACCOUNT_NAME;

            //this Object for handle on lost focus in the lookup
            _viewModel.loTempParamaterFilter!.CCB_ACCOUNT_NO = _viewModel.loParamaterFilter!.CCB_ACCOUNT_NO;

            _gridBankInChequeRef!.DataSource.Clear();
            _viewModel.BankInChequeInfo = new();
        }
        #endregion

        #region onLostFocus
        private async Task LostFocusLookupDept()
        {
            var loEx = new R_Exception();

            try
            {
                var loGetData = _viewModel.loParamaterFilter;
                var loTemp = _viewModel.loTempParamaterFilter;

                if (string.IsNullOrWhiteSpace(loGetData!.CDEPT_CODE) || loGetData.CDEPT_CODE == _viewModel.loTempParamaterFilter!.CDEPT_CODE)
                {
                    goto EndBlock;
                }
                else if (!string.IsNullOrWhiteSpace(loGetData!.CDEPT_CODE))
                {
                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel();
                    var param = new GSL00700ParameterDTO
                    {
                        CCOMPANY_ID = _clientHelper!.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                        CPROGRAM_ID = "",
                        CSEARCH_TEXT = _viewModel.loParamaterFilter!.CDEPT_CODE!,
                    };
                    var loResult = await loLookupViewModel.GetDepartment(param);


                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loGetData!.CDEPT_CODE = "";
                        loGetData.CDEPT_NAME = "";
                    }
                    else
                    {
                        loGetData.CDEPT_CODE = loResult.CDEPT_CODE;
                        loGetData.CDEPT_NAME = loResult.CDEPT_NAME;

                        //this Object for handle on lost focus in the lookup
                        _viewModel.loTempParamaterFilter!.CDEPT_CODE = _viewModel.loParamaterFilter!.CDEPT_CODE;
                    }
                    //CLEAR ALL GRID
                    _gridBankInChequeRef!.DataSource.Clear();
                    _viewModel.BankInChequeInfo = new();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private async Task LostFocusLookupBankCode()
        {
            var loEx = new R_Exception();
            try
            {
                var loGetData = _viewModel.loParamaterFilter;

                if (string.IsNullOrWhiteSpace(loGetData!.CCB_CODE) || loGetData.CCB_CODE == _viewModel.loTempParamaterFilter!.CCB_CODE)
                {
                    goto EndBlock;
                }
                else if (!string.IsNullOrWhiteSpace(loGetData!.CCB_CODE))
                {
                    LookupGSL02500ViewModel loLookupViewModel = new LookupGSL02500ViewModel();
                    var param = new GSL02500ParameterDTO
                    {
                        CDEPT_CODE = _viewModel.loParamaterFilter!.CDEPT_CODE!,
                        CCB_TYPE = "B",
                        CBANK_TYPE = "I",
                        CSEARCH_TEXT = _viewModel.loParamaterFilter!.CCB_CODE!,
                    };
                    var loResult = await loLookupViewModel.GetCB(param);


                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loGetData!.CCB_CODE = "";
                        loGetData.CCB_NAME = "";
                    }
                    else
                    {
                        loGetData.CCB_CODE = loResult.CCB_CODE;
                        loGetData.CCB_NAME = loResult.CCB_NAME;
                        //this Object for handle on lost focus in the lookup
                        _viewModel.loTempParamaterFilter!.CCB_CODE = _viewModel.loParamaterFilter!.CCB_CODE;
                    }
                    //CLEAR ALL GRID
                    _gridBankInChequeRef!.DataSource.Clear();
                    _viewModel.BankInChequeInfo = new();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private async Task LostFocusLookupAccountNo()
        {
            var loEx = new R_Exception();
            try
            {
                var loGetData = _viewModel.loParamaterFilter;

                if (string.IsNullOrWhiteSpace(loGetData!.CCB_ACCOUNT_NO) || loGetData.CCB_ACCOUNT_NO == _viewModel.loTempParamaterFilter!.CCB_ACCOUNT_NO)
                {
                    goto EndBlock;
                }
                else if (!string.IsNullOrWhiteSpace(loGetData!.CCB_ACCOUNT_NO))
                {
                    LookupGSL02600ViewModel loLookupViewModel = new LookupGSL02600ViewModel();
                    var param = new GSL02600ParameterDTO
                    {
                        CDEPT_CODE = _viewModel.loParamaterFilter!.CDEPT_CODE!,
                        CCB_CODE = _viewModel.loParamaterFilter!.CCB_CODE!,
                        CCB_TYPE = "B",
                        CBANK_TYPE = "I",
                        CSEARCH_TEXT = _viewModel.loParamaterFilter!.CCB_ACCOUNT_NO!,
                    };
                    var loResult = await loLookupViewModel.GetCBAccount(param);


                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loGetData!.CCB_ACCOUNT_NO = "";
                        loGetData.CCB_ACCOUNT_NAME = "";
                    }
                    else
                    {
                        loGetData.CCB_ACCOUNT_NO = loResult.CCB_ACCOUNT_NO;
                        loGetData.CCB_ACCOUNT_NAME = loResult.CCB_ACCOUNT_NAME;
                        //this Object for handle on lost focus in the lookup
                        _viewModel.loTempParamaterFilter!.CCB_ACCOUNT_NO = _viewModel.loParamaterFilter!.CCB_ACCOUNT_NO;
                    }
                    //CLEAR ALL GRID
                    _gridBankInChequeRef!.DataSource.Clear();
                    _viewModel.BankInChequeInfo = new();
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
        #region implement library for checkbox select
        private void R_CheckBoxSelectValueChanged(R_CheckBoxSelectValueChangedEventArgs eventArgs)
        {
        }
        #endregion
    }
}
