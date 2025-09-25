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
using BlazorClientHelper;
using System.Linq;
using R_BlazorFrontEnd.Controls.MessageBox;

namespace GFF00900FRONT
{   
    public partial class GFF00900 : R_Page
    {
        private GFF00900ViewModel loViewModel = new();

        private R_Conductor _conductorRef;
        [Inject] R_ISymmetricJSProvider _encryptProvider { get; set; }

        private bool IsReasonHidden = true;

        private bool IsAccessValidationHidden = true;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            GFF00900ParameterDTO loParam = null;

            try
            {
                loParam = (GFF00900ParameterDTO)poParameter;

                loViewModel.loParameter.ACTION_CODE = loParam.IAPPROVAL_CODE;
                loViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = loParam.IAPPROVAL_CODE;
                loViewModel.loRspActivityValidityList = loParam.Data;

                IsReasonHidden = false;
    }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnOKReason()
        {
            if (string.IsNullOrWhiteSpace(loViewModel.DETAIL_ACTION))
            {
                var loValidate = await R_MessageBox.Show("", "Please Input Reason First", R_eMessageBoxButtonType.OK);
            }
            else
            {
                IsReasonHidden = true;
                IsAccessValidationHidden = false;
            }
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
                loViewModel.UserPasswordFieldValidation();

                //await loViewModel.RSP_ACTIVITY_VALIDITYMethodAsync();

                if (loViewModel.loRspActivityValidityList.Any(x => x.CAPPROVAL_USER.Equals(loViewModel.loParameter.USER, StringComparison.OrdinalIgnoreCase)))
                {/*
                    loViewModel.loSelectedUser = loViewModel.loRspActivityValidityList.
                        Where(x => x.CAPPROVAL_USER.Equals(loViewModel.loParameter.USER, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();*/

                    string lcEncryptedPassword = await _encryptProvider.TextEncrypt(loViewModel.loParameter.PASSWORD, loViewModel.loParameter.USER);
                    loViewModel.loParameter.PASSWORD = lcEncryptedPassword;
                    await loViewModel.UsernameAndPasswordValidationMethod();

                    await this.Close(true, true);
                }
                else
                {
                    var loValidate = await R_MessageBox.Show("", "User Not Allowed", R_eMessageBoxButtonType.OK);
                    if (loValidate == R_eMessageBoxResult.OK)
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

        private async Task OnCancel()
        {
            await this.Close(true, false);
        }
    }
}
