using BaseAOC_BS11Common.DTO.Response.GridList;
using BlazorClientHelper;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00200;
using Microsoft.AspNetCore.Components;
using PMT01900Common.DTO.CRUDBase;
using PMT01900Common.DTO.Front;
using PMT01900FrontResources;
using PMT01900Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace PMT01900Front
{
    public  partial class PMT01900Deposit : R_Page
    {
        #region Master Page

        readonly PMT01900DepositViewModel _viewModel = new();
        R_Conductor? _conductorDeposit;
        R_Grid<BaseAOCResponseAgreementDepositListDTO>? _gridDeposit;
        PMT01900EventCallBackDTO _oEventCallBack = new PMT01900EventCallBackDTO();
        [Inject] IClientHelper? _clientHelper { get; set; }
        #endregion

        #region Front Control


        private void OnChangedDDEPOSIT_DATE(DateTime? poParameter)
        {
            PMT01900Deposit_DepositDetailDTO loData = _viewModel.Data;
            loData.DDEPOSIT_DATE = poParameter;
        }


        private void OnChangedCurrencyCode(string pcParam)
        {
            R_Exception loException = new R_Exception();
            PMT01900Deposit_DepositDetailDTO? loData = _viewModel.Data;

            try
            {
                loData.CCURRENCY_CODE = pcParam;
                loData.CCURRENCY_NAME = _viewModel.oComboBoxDataCurrency.FirstOrDefault(c => c.CCURRENCY_CODE == pcParam)?.CCURRENCY_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        #endregion
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {

                _viewModel.oParameter = R_FrontUtility.ConvertObjectToObject<PMT01900ParameterFrontChangePageDTO>(poParameter);
                if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
                {
                    await _viewModel.GetDepositHeader();
                    await _viewModel.GetComboBoxDataCurrency();
                    //if (!string.IsNullOrEmpty(_viewModel.oParameter.CREF_NO))
                    if (!string.IsNullOrEmpty(_viewModel.oParameter.CREF_NO))
                    {
                        await _gridDeposit.R_RefreshGrid(null);
                    }
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Master Conductor 

        public void ServiceAfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                var loData = (PMT01900Deposit_DepositDetailDTO)eventArgs.Data;
                loData.DDEPOSIT_DATE = DateTime.Now;
                if (!string.IsNullOrEmpty(_viewModel.oHeaderEntity.CCURRENCY_CODE))
                {
                    loData.CCURRENCY_CODE = _viewModel.oHeaderEntity.CCURRENCY_CODE;
                    loData.CCURRENCY_NAME = _viewModel.oHeaderEntity.CCURRENCY_NAME;
                }
                else
                {
                    loData.CCURRENCY_CODE = _viewModel.oComboBoxDataCurrency.FirstOrDefault().CCURRENCY_CODE;
                    loData.CCURRENCY_NAME = _viewModel.oComboBoxDataCurrency.FirstOrDefault().CCURRENCY_NAME;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        private async Task SetOtherAsync(R_SetEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                //_viewModel.lControlCRUDMode = eventArgs.Enable;
                _oEventCallBack.LCRUD_MODE = eventArgs.Enable;

                await InvokeTabEventCallbackAsync(_oEventCallBack);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        public async Task AfterDelete()
        {
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }

        private void R_Validation(R_ValidationEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                var loData = (PMT01900Deposit_DepositDetailDTO)eventArgs.Data;
                //await LostFocusCUNIT_ID();

                if (string.IsNullOrWhiteSpace(loData.CDEPOSIT_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01900_Class), "ValidationDepositID");
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

        #region Deposit List

        private async Task R_ServiceGetListDepositListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetDepositList();
                eventArgs.ListEntityResult = _viewModel.oListDeposit;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        #region Master CRUD


        private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            PMT01900Deposit_DepositDetailDTO loParam;

            try
            {
                loParam = new PMT01900Deposit_DepositDetailDTO();
                if (eventArgs.Data != null)
                {
                    loParam = R_FrontUtility.ConvertObjectToObject<PMT01900Deposit_DepositDetailDTO>(eventArgs.Data);
                }
                /*
                else
                {
                    loParam.CREF_NO = _viewModel.oParameter.CREF_NO;
                    loParam.CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID;
                    loParam.CDEPT_CODE = _viewModel.oParameter.CDEPT_CODE;
                    loParam.CTRANS_CODE = "802041";
                    loParam.CCOMPANY_ID = _clientHelper.CompanyId;
                    loParam.CUSER_ID = _clientHelper.UserId;
                };
                */
                await _viewModel.GetEntity(loParam);

                eventArgs.Result = _viewModel.oEntity;
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
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01900Deposit_DepositDetailDTO>(eventArgs.Data);

                await _viewModel.ServiceSave(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel.oEntity;
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
                var loData = (PMT01900Deposit_DepositDetailDTO)eventArgs.Data;

                //   await _viewModel.GetEntity(loData);

                if (_viewModel.oEntity != null)
                    await _viewModel.ServiceDelete(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion
        #endregion
        #region Master LookUp


        #region Lookup Button Deposit Lookup

        private R_Lookup? R_LookupDepositLookup;

        private void BeforeOpenLookUpDepositLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00200ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
            {
                param = new LML00200ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID,
                    CCHARGE_TYPE_ID = "03",
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00200);
        }

        private void AfterOpenLookUpDepositLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            LML00200DTO? loTempResult = null;
            //PMT01100LOO_Offer_SelectedOfferDTO? loGetData = null;

            try
            {
                loTempResult = (LML00200DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;
                //loGetData = (PMT01100LOO_Offer_SelectedOfferDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _viewModel.Data.CDEPOSIT_ID = loTempResult.CCHARGES_ID;
                _viewModel.Data.CDEPOSIT_NAME = loTempResult.CCHARGES_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private R_TextBox? _componentCDepositTextBox;

        private async Task OnLostFocusDeposit()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT01900Deposit_DepositDetailDTO loGetData = _viewModel.Data;

                if (string.IsNullOrWhiteSpace(loGetData.CDEPOSIT_ID))
                {

                    loGetData.CDEPOSIT_ID = "";
                    loGetData.CDEPOSIT_NAME = "";
                    return;
                }

                LookupLML00200ViewModel loLookupViewModel = new LookupLML00200ViewModel();
                LML00200ParameterDTO loParam = new LML00200ParameterDTO()
                {

                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID!,
                    CCHARGE_TYPE_ID = "03",
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
