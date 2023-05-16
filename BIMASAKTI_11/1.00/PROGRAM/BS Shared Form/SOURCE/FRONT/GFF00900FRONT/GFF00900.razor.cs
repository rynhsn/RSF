using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using GFF00900Model;
using GFF00900Model.ViewModel;
using GFF00900COMMON.DTOs;
using System;
using System.Diagnostics.Tracing;
using Microsoft.AspNetCore.Components;
using R_CrossPlatformSecurity;
using R_BlazorFrontEnd.Helpers;

namespace GFF00900FRONT
{   
    public partial class GFF00900 : R_Page
    {
        private GFF00900ViewModel loViewModel = new();

        private R_Conductor _conductorRef;
        [Inject] R_ISymmetricJSProvider _encryptProvider { get; set; }

        private bool ReasonVisibility = true;

        private bool AccessValidationVisibility = true;


        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loViewModel.loParameter.ACTION_CODE = (string)poParameter;
                loViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = (string)poParameter;

                R_Exception loException = new R_Exception();
                try
                {
                    await loViewModel.RSP_ACTIVITY_VALIDITYMethodAsync();
                    if (loException.HasError == false)
                    {
                        ReasonVisibility = false;
                        if (loViewModel.loRspActivityValidityResult.Data.IAPPROVAL_MODE == 1)
                        {
                            await this.Close(true, true);
                        }
                        else if (loViewModel.loRspActivityValidityResult.Data.IAPPROVAL_MODE == 2)
                        {

                        }
                        else if (loViewModel.loRspActivityValidityResult.Data.IAPPROVAL_MODE == 3)
                        {
                            await this.Close(true, false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }
                loException.ThrowExceptionIfErrors();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void OnOKReason()
        {
            ReasonVisibility = true;
            AccessValidationVisibility = false;
        }

        private async Task OnCancelReason()
        {
            await this.Close(true, false);
        }

        private async Task OnOK()
        {
            R_Exception loException = new R_Exception();
            try
            {
                string lcEncryptedPassword = await _encryptProvider.TextEncrypt(loViewModel.loParameter.PASSWORD, loViewModel.loParameter.USER);
                loViewModel.loParameter.PASSWORD = lcEncryptedPassword;
                await loViewModel.UsernameAndPasswordValidationMethod();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

            await this.Close(true, true);
        }

        private async Task OnCancel()
        {
            await this.Close(true, false);
        }
    }
}
