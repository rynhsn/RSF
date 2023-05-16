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

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loViewModel.loParameter.ACTION_CODE = (string)poParameter;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
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
