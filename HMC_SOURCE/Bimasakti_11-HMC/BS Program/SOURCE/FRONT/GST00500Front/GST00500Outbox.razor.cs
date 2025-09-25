using GST00500Common;
using GST00500Model.ViewModel;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Helpers;

namespace GST00500Front
{
    public partial class GST00500Outbox : R_Page
    {
        private GST00500OutboxViewModel _viewModelGST00500Outbox = new();
        private R_Grid<GST00500DTO> _gridOutboxTransRef;
        private R_Grid<GST00500ApprovalStatusDTO> _gridOutboxTransStatusRef;
        private R_ConductorGrid _conductorOutboxTrans;
        private R_ConductorGrid _conductorOutboxTransStatus;


        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await _gridOutboxTransRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #region Outbox

        private async Task ServiceGetListOutboxTransaction(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModelGST00500Outbox.GetAllOutboxTransaction();
                GST00500DTO ParamTransactionStatus = _viewModelGST00500Outbox.OutboxTransactionList.FirstOrDefault();
                eventArgs.ListEntityResult = _viewModelGST00500Outbox.OutboxTransactionList;

                //TO DISPLAY GRID APPROVE BY, ON THE BOTTOM OF MAIN GRID
                //if (_viewModelGST00500Outbox.OutboxTransactionList.Count > 0)
                //{
                //    await _gridOutboxTransStatusRef.R_RefreshGrid(ParamTransactionStatus);
                //}
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task Grid_DisplayOutbox(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal 
                    && _viewModelGST00500Outbox.OutboxTransactionList.Count > 0)
                {
                    GST00500DTO ParamTransactionStatus = (GST00500DTO)eventArgs.Data;
                    await _gridOutboxTransStatusRef.R_RefreshGrid(ParamTransactionStatus);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task ServiceGetListOutboxTransactionStatus(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                GST00500DTO ParamTransactionStatus = (GST00500DTO)eventArgs.Parameter;
                await _viewModelGST00500Outbox.GetAllApprovalStatus(ParamTransactionStatus);
                eventArgs.ListEntityResult = _viewModelGST00500Outbox.OutboxApprovalStatusTransactionList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion
        #region Btn Refresh
        private async Task BtnRefresh()
        {
            var loEx = new R_Exception();
            try
            {
                await _gridOutboxTransRef!.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        #endregion
        #region ButtonView
        //POPUP        
        private void R_Before_ServiceOpenOthersProgram(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParameter = _viewModelGST00500Outbox._currentRecord;
              /*  InvoiceEntryPredifineParameterDTO loConvert = new InvoiceEntryPredifineParameterDTO()
                {
                    CCOMPANY_ID = loParameter.CCOMPANY_ID!,
                    CTRANSACTION_CODE = loParameter.CTRANS_CODE!,
                    CDEPT_CODE = loParameter.CDEPT_CODE!,
                    CREFERENCE_NO = loParameter.CREF_NO!,
                    CPROPERTY_ID = loParameter.CPROPERTY_ID!,
                    LOPEN_AS_PAGE = false
                };

                var lcProgramId = _viewModelGST00500Outbox._currentRecord.CPROGRAM_ID;

                switch (lcProgramId)
                {
                    case "APT00100":
                        eventArgs.Parameter = loConvert;
                        eventArgs.TargetPageType = typeof(APT00110);
                        break;
                    case "APT00200":
                        var loParam200 = R_FrontUtility.ConvertObjectToObject<PurchaseReturnEntryPredifineParameterDTO>(loConvert);
                        eventArgs.Parameter = loParam200;
                        eventArgs.TargetPageType = typeof(APT00210);
                        break;
                }
              */
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
    }
}
