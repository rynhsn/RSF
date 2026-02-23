using GSM06500Common;
using GSM06500Model;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using System;
using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.Enums;
using R_CommonFrontBackAPI;
using R_LockingFront;

namespace GSM06500Front
{
    public partial class GSM06500
    {
        private GSM06500ViewModel PaymentTermViewModel = new();
        private R_ConductorGrid? _conGridPaymentRef;
        private R_Grid<GSM06500DTO> _gridRef;
        private R_Conductor _conductorRef;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await PaymentTermViewModel.GetAllTermOfPaymentAsync();

                eventArgs.ListEntityResult = PaymentTermViewModel.PaymentOfTermList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_ServiceGetRecordAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM06500DTO)eventArgs.Data;
                eventArgs.Result = eventArgs.Data;
                eventArgs.Result = await PaymentTermViewModel.GetTermOfPaymentOneRecord(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM06500DTO)eventArgs.Data;
                await PaymentTermViewModel.DeleteTermOfPayment(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceAfterDelete()
        {
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }

        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM06500DTO)eventArgs.Data;
                await PaymentTermViewModel.SaveTermOfPayment(loParam, eventArgs.ConductorMode);
                // eventArgs.Result = loParam;
                eventArgs.Result = PaymentTermViewModel.PaymentOfTerm;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        /*
        private async Task ServiceDisplay(R_DisplayEventArgs eventsArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var abc = (GSM06500DTO)eventsArgs.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    
        private void AfterAdd (R_AfterAddEventArgs eventArgs)
        {
            var data = (GSM06500DTO)eventArgs.Data;
            eventArgs.Data = data;
        }    */
        public void ServiceValidation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                PaymentTermViewModel.ValidationFieldEmpty((GSM06500DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            eventArgs.Cancel = loEx.HasError;
            R_DisplayException(loEx);
        }

        #region UserLocking
        [Inject] IClientHelper clientHelper { get; set; }
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_MODULE_NAME = "GS";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (GSM06500DTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {

                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "GSM06500",
                        Table_Name = "GSM_PAYMENT_TERM",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CPAY_TERM_CODE)

                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "GSM06500",
                        Table_Name = "GSM_PAYMENT_TERM",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CPAY_TERM_CODE)
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