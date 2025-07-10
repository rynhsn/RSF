using GSM00300MODEL.View_Model;
using GSM00300COMMON.DTO_s;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Interfaces;
using BlazorClientHelper;
using GSM00300FrontResources;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Controls.Tab;
using GSM00300COMMON;
using R_BlazorFrontEnd.Controls.Enums;
using R_LockingFront;

namespace GSM00300FRONT
{
    public partial class GSM00301 : R_Page
    {
        //variables
        private GSM00301ViewModel _viewModel = new();
        private R_Conductor _conRef;
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IClientHelper _clientHelper { get; set; }

        //method
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.InitProcess(_localizer);
                await _conRef.R_GetEntity(new CompanyParamRecordDTO());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        protected override async Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult? loLockResult = null;

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<CompanyParamRecordDTO>(eventArgs.Data);

                var loCls = new R_LockingServiceClient(pcModuleName: GSM00300ContextConstant.DEFAULT_MODULE,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: GSM00300ContextConstant.DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = GSM00300ContextConstant.CPROGRAM_ID,
                        Table_Name = GSM00300ContextConstant.TABLE_NAME,
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CCOMPANY_ID)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = GSM00300ContextConstant.CPROGRAM_ID,
                        Table_Name = GSM00300ContextConstant.TABLE_NAME,
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CCOMPANY_ID)
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
        private async Task TaxInfo_GetRecordAsync(R_ServiceGetRecordEventArgs eventArgs)
        {

            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.GetTaxInfoGetRecordAsync();
                eventArgs.Result = _viewModel.TaxInfo;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void TaxInfo_Display(R_DisplayEventArgs eventArgs)
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
        private void TaxInfo_Saving(R_SavingEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (TaxInfoDTO)eventArgs.Data;
                loData.CID_EXPIRED_DATE = loData.DID_EXPIRED_DATE.Value.ToString("yyyyMMdd");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task TaxInfo_SaveAsync(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {

                await _viewModel.SaveTaxInfoAsync(eventArgs.Data as TaxInfoDTO, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel.TaxInfo ?? new TaxInfoDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task TaxInfo_SetOtherAsync(R_SetEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await InvokeTabEventCallbackAsync(eventArgs.Enable);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

    }
}
