using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using R_BlazorFrontEnd.Helpers;
using BlazorClientHelper;
using PMF00200COMMON;
using PMF00200Model;
using R_APICommonDTO;
using R_BlazorFrontEnd.Controls.MessageBox;

namespace PMF00200FRONT
{
    public partial class PMF00200 : R_Page
    {

        private PMF00200ViewModel _viewModel = new PMF00200ViewModel();

        #region Inject
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<PMF00200FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }
        #endregion

        #region Private Property
        private string _TitleName = "";
        PMF00200InputParameterDTO _InputParameter = new PMF00200InputParameterDTO();
        #endregion

        #region ComboBox 
        private List<KeyValuePair<string, string>> _PrintOptionList { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("CR", R_FrontUtility.R_GetMessage(typeof(PMF00200FrontResources.Resources_Dummy_Class), "_CustomerReceipt")),
            new KeyValuePair<string, string>("VC", R_FrontUtility.R_GetMessage(typeof(PMF00200FrontResources.Resources_Dummy_Class), "_Voucher")),
        };
        #endregion

        #region Upload Method
        // Create Method Action StateHasChange
        private void StateChangeInvoke()
        {
            StateHasChanged();
        }

        // Create Method Action if proses is Complete Success
        private async Task ActionFuncIsCompleteSuccess()
        {
            await R_MessageBox.Show("", _localizer["_NotifSuccesUpload"], R_eMessageBoxButtonType.OK);
            await this.Close(true, true);
        }
        // Create Method Action For Error Unhandle
        private void ShowErrorInvoke(R_APIException poEx)
        {
            var loEx = R_FrontUtility.R_ConvertFromAPIException(poEx);
            //var loEx = new R_Exception(poEx.ErrorList.Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.ErrNo, x.ErrDescp)).ToList());
            this.R_DisplayException(loEx);
        }
        #endregion

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                #region Initialized Method
                // Set Param Company to viewmodel
                _viewModel.CompanyID = clientHelper.CompanyId;
                _viewModel.UserId = clientHelper.UserId;

                //Assign Action
                _viewModel.StateChangeAction = StateChangeInvoke;
                _viewModel.ShowErrorAction = ShowErrorInvoke;
                _viewModel.ActionIsCompleteSuccess = ActionFuncIsCompleteSuccess;
                #endregion

                var loData = R_FrontUtility.ConvertObjectToObject<PMF00200InputParameterDTO>(poParameter);
                _InputParameter = loData;
                _TitleName = loData.PARAM_RECEIPT_TYPE == "CA" ? _localizer["_CATitleName"]
                           : loData.PARAM_RECEIPT_TYPE == "WT" ? _localizer["_WTTitleName"]
                           : loData.PARAM_RECEIPT_TYPE == "CQ" ? _localizer["_CQTitleName"]
                           : _localizer["_NullTitleName"];

                await _viewModel.GetAllUniversalData(loData);
                await _viewModel.GetJournalRecord(loData, clientHelper.Culture.DateTimeFormat.LongDatePattern);
                if (_viewModel.VAR_REPORT_TEMPLATE_LIST.Count > 0)
                {
                    _InputParameter.RECEIPT_TEMPLATE = _viewModel.VAR_REPORT_TEMPLATE_LIST.FirstOrDefault().CTEMPLATE_ID;
                }
                _InputParameter.PRINT_OPTION = "CR";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task PrintBtn_OnClick()
        {
            R_Exception loEx = new R_Exception();
            bool llValidate = false;
            try
            {
                if (string.IsNullOrWhiteSpace(_InputParameter.RECEIPT_TEMPLATE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                                        typeof(PMF00200FrontResources.Resources_Dummy_Class),
                                        "V01"));
                    llValidate = true;
                }

                if (llValidate == false)
                {
                    await _reportService.GetReport(
                           "R_DefaultServiceUrlPM",
                           "PM",
                           "rpt/PMF00200Print/AllRecepitPrintPost",
                           "rpt/PMF00200Print/AllReceiptPrintGet",
                           _InputParameter);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            
            await R_DisplayExceptionAsync(loEx);
        }
        private async Task SendEmailBtn_OnClick()
        {
            R_Exception loEx = new R_Exception();
            bool llValidate = false;
            try
            {
                if (string.IsNullOrWhiteSpace(_InputParameter.RECEIPT_TEMPLATE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                                        typeof(PMF00200FrontResources.Resources_Dummy_Class),
                                        "V01"));
                    llValidate = true;
                }

                if (llValidate == false)
                {
                    await _viewModel.ProcessSendEmail(_InputParameter);

                    await R_MessageBox.Show("", _localizer["N01"], R_eMessageBoxButtonType.OK);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            if (loEx.HasError)
            {
                await R_MessageBox.Show("", _localizer["N02"], R_eMessageBoxButtonType.OK);
            }
        }
    }
}