using BlazorClientHelper;
using GST00500Common;
using GST00500FrontResources;
using GST00500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;

namespace GST00500Front
{
    public partial class GST00500RejectPopUp
    {
        private R_ConductorGrid _conductorRejectPopUp;
        private R_Grid<GST00500RejectDTO> _gridRejectPopUpRef;

        private GST00500InboxViewModel _viewModelGST00500Inbox = new();

        private bool IsRejectModalHidden = true;
        [Inject] IClientHelper clientHelper { get; set; }

        private void StateChangeInvoke()
        {
            StateHasChanged();
        }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                _viewModelGST00500Inbox.CCOMPANYID = clientHelper.CompanyId;
                _viewModelGST00500Inbox.CUSERID = clientHelper.UserId;
                await PropertyDropdown_ServiceGetListRecord(null);
                _viewModelGST00500Inbox.loInboxApprovaltBatchList = (List<GST00500DTO>)poParameter;
                IsRejectModalHidden = false;
                _viewModelGST00500Inbox.StateChangeAction = StateChangeInvoke;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task PropertyDropdown_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModelGST00500Inbox.GetReasonRejectTransaction();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }


        private async Task OnClickOKRejectButton()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrEmpty(_viewModelGST00500Inbox.ParamRejectTransactionStatus.CREASON_CODE))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GST00500_Class), "Error_02");
                    loEx.Add(loErr);
                }
               else if (string.IsNullOrEmpty(_viewModelGST00500Inbox.ParamRejectTransactionStatus.TNOTES))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GST00500_Class), "Error_03");
                    loEx.Add(loErr);
                }
                else
                {
                    List<GST00500DataRejectDTO> oResult  =  _viewModelGST00500Inbox.GetSelectedDataToRejectApproval();
                    Thread.Sleep(2500);
                  //  var oResult = _viewModelGST00500Inbox._InboxRejectListSelected;
                    //TO CLose Rejection PopUp
                    await Close(true, oResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickCancelButton()
        {
            await Close(true, null);
        }



    }
}
