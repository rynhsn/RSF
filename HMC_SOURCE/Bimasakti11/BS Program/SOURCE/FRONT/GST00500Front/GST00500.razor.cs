using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorClientHelper;
using GST00500Common;
using GST00500FrontResources;
using GST00500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_APICommonDTO;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Extensions;
using R_BlazorFrontEnd.Helpers;

namespace GST00500Front
{
    public partial class GST00500 : R_Page
    {
        #region Declare ViewModel
        private GST00500InboxViewModel _viewModelGST00500Inbox = new();
        #endregion

        private R_Grid<GST00500DTO>? _gridInboxTransRef;

        private R_Conductor? _conductorGetUserName;
        private R_ConductorGrid? _conductorInboxTrans;
        [Inject] IClientHelper clientHelper { get; set; }
        private bool isApprove = true;
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
            var type = _viewModelGST00500Inbox.TipeApprove == GST00500InboxViewModel.TYPE_APPROVE.Approve ? "Approve" : "Reject";

            var loValidate = await R_MessageBox.Show("", $"Data Successfully {type}", R_eMessageBoxButtonType.OK);
            if (loValidate == R_eMessageBoxResult.OK)
            {
                await this.Close(true, true);
            }
            await _gridInboxTransRef!.R_RefreshGrid(null);
        }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await ServiceGetUserName(null);
                _viewModelGST00500Inbox.CCOMPANYID = clientHelper.CompanyId;
                _viewModelGST00500Inbox.CUSERID = clientHelper.UserId;

                _viewModelGST00500Inbox.StateChangeAction = StateChangeInvoke;
                _viewModelGST00500Inbox.DisplayErrorAction = DisplayErrorInvoke;
                _viewModelGST00500Inbox.ShowSuccessAction = async () =>
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
        private async Task ServiceGetUserName(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelGST00500Inbox.GetUserName();
                await _gridInboxTransRef!.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #region Inbox

        private async Task ServiceGetListInboxTransaction(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelGST00500Inbox.GetAllInboxTransaction();
                eventArgs.ListEntityResult = _viewModelGST00500Inbox.InboxTransactionList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void Grid_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loParam = (GST00500DTO)eventArgs.Data;
                    _viewModelGST00500Inbox._currentRecord = R_FrontUtility.ConvertObjectToObject<GST00500ParamToOthersProgramDTO>(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region Btn Refresh
        private async Task BtnRefresh()
        {
            var loEx = new R_Exception();
            try
            {
                await _gridInboxTransRef!.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        #endregion
        #region ApproveButton
        private async Task OnClickApprove()
        {
            var loEx = new R_Exception();
            try
            {

                isApprove = true;
                if (!_viewModelGST00500Inbox.IsDataSelectedExist())
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GST00500_Class), "Error_04");
                    loEx.Add(loErr);
                    goto EndBlock;
                }
                await _gridInboxTransRef!.R_SaveBatch();
                await this.Close(true, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        #endregion

        #region RejectButton
        //OnClickReject()
        private async Task R_Before_Open_Reject(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                isApprove = false;

                //_viewModelGST00500Inbox.ValidationField();
                if (!_viewModelGST00500Inbox.IsDataSelectedExist())
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GST00500_Class), "Error_04");
                    loEx.Add(loErr);
                    goto EndBlock;
                }

                var loTemp = await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_GST00500_Class), "validationProcess"),
                    R_eMessageBoxButtonType.OKCancel);

                if (loTemp == R_eMessageBoxResult.OK)
                {
                    await _gridInboxTransRef!.R_SaveBatch();

                    eventArgs.Parameter = _viewModelGST00500Inbox.loInboxApprovaltBatchList;
                    eventArgs.TargetPageType = typeof(GST00500RejectPopUp);
                }

                await this.Close(true, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_After_Open_Reject(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.Result != null)
                {
                    var resultObject = (List<GST00500DataRejectDTO>)eventArgs.Result;
                    await _viewModelGST00500Inbox.ProcessRejectTransaction(resultObject);
                }
                await _gridInboxTransRef!.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Save Batch
        private void BeforeSaveBatch(R_BeforeSaveBatchEventArgs events)
        {
            var loEx = new R_Exception();
            var tempData = (List<GST00500DTO>)events.Data;


            if (tempData.Count < 1)
            {
                var loErr = R_FrontUtility.R_GetError(typeof(Resources_GST00500_Class), "Error_01");
                loEx.Add(loErr);
                events.Cancel = true;
            }
        }
        private async Task ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                _viewModelGST00500Inbox.loInboxApprovaltBatchList = (List<GST00500DTO>)eventArgs.Data;
                if (isApprove)
                {
                    await _viewModelGST00500Inbox.ProcessApproval();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void AfterSaveBatch(R_AfterSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                _gridInboxTransRef!.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #endregion

        #region ChangeTab
        private R_TabPage _tabPageOutbox;
        private R_TabPage _tabPageDraft;

        private async Task ChangeTab(R_TabStripTab arg)
        {
            var loEx = new R_Exception();
            try
            {
                if (arg.Id == "TabInbox")
                {
                    await _gridInboxTransRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void R_Before_Open_TabPageOutbox(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var param = new GST00500DTO()
            {
                CCOMPANY_ID = clientHelper.CompanyId,
                CUSER_ID = clientHelper.UserId
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GST00500Outbox);
        }
        private void R_Before_Open_TabPageDraft(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(GST00500Draft);
        }
        private void R_After_Open_TabPage(R_AfterOpenTabPageEventArgs eventArgs)
        {

        }

        #endregion
        #region ButtonView
        #region PopUP
        //POPUP        
        private void R_Before_ServiceOpenOthersProgram(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParameter = _viewModelGST00500Inbox._currentRecord;

                //InvoiceEntryPredifineParameterDTO loConvert = new InvoiceEntryPredifineParameterDTO()
                //{
                //    CCOMPANY_ID = loParameter.CCOMPANY_ID!,
                //    CTRANSACTION_CODE = loParameter.CTRANS_CODE!,
                //    CDEPT_CODE = loParameter.CDEPT_CODE!,
                //    CREFERENCE_NO = loParameter.CREF_NO!,
                //    CPROPERTY_ID = loParameter.CPROPERTY_ID!,
                //    LOPEN_AS_PAGE = false
                //};

                //    var lcProgramId = _viewModelGST00500Inbox._currentRecord.CPROGRAM_ID;
                //  var loParam200 = R_FrontUtility.ConvertObjectToObject<PurchaseReturnEntryPredifineParameterDTO>(loConvert);
                //  eventArgs.Parameter = loParam200;
                //eventArgs.TargetPageType = typeof(APT00210);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void R_After_ServiceOpenOthersProgram(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        #endregion
        #region DETAIL
        private async Task R_Before_BtnView(R_BeforeOpenDetailEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                //  string codeSpec = "GLT00100"; //GLT00100 (Journal Entries)
                //var abc = R_FrontUtility.ConvertObjectToObject<GST00500DTO>(_conductorInboxTrans.R_GetCurrentData());
                string programNamespace = "";
                string programPage = "";

                var loParameter = _viewModelGST00500Inbox._currentRecord;

                string programId = loParameter.CPROGRAM_ID;

                // Memisahkan program ID menjadi dua bagian berdasarkan karakter '|'
                string[] programParts = programId.Split('|');
                if (programParts.Count()>0)
                {
                    programNamespace = programParts[0];
                    programPage = programParts[1];
                }
                // loParameter.CCALLER_ACTION = "VIEW";       
                eventArgs.Parameter = loParameter;
                eventArgs.FormAccess = R_eFormAccess.View.ToDescription();
                eventArgs.PageNamespace = $"{programNamespace}FRONT.{programPage}";

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            await R_DisplayExceptionAsync(loEx);
        }
        private void R_After_BtnAView(R_AfterOpenDetailEventArgs eventArgs)
        {

        }
        #endregion
        #endregion
        #region implement library for checkbox select
        private void R_CheckBoxSelectValueChanged(R_CheckBoxSelectValueChangedEventArgs eventArgs)
        {
        }
        #endregion

    }
}