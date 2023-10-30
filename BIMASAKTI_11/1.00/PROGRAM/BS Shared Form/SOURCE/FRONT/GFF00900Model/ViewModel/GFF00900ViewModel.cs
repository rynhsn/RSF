using GFF00900COMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GFF00900Model.ViewModel
{
    public class GFF00900ViewModel : R_ViewModel<ValidationDTO>
    {
        GFF00900Model loModel = new GFF00900Model();

        public ValidationDTO loParameter = new ValidationDTO();

        public RSP_ACTIVITY_VALIDITYResultDTO loRspActivityValidityResult = null;

        public List<RSP_ACTIVITY_VALIDITYDataDTO> loRspActivityValidityList = null;
/*
        public RSP_ACTIVITY_VALIDITYDataDTO loSelectedUser = null;*/

        public string ACTIVATE_INACTIVE_ACTIVITY_CODE = "";

        public string DETAIL_ACTION = "";

        public async Task RSP_ACTIVITY_VALIDITYMethodAsync()
        {
            R_Exception loEx = new R_Exception();
            RSP_ACTIVITY_VALIDITYParameterDTO loParam = null;

            try
            {
                //R_FrontContext.R_SetContext(ContextConstant.ACTIVITY_CODE_CONTEXT, ACTIVATE_INACTIVE_ACTIVITY_CODE);
                loParam = new RSP_ACTIVITY_VALIDITYParameterDTO()
                {
                    ACTIVITY_CODE = ACTIVATE_INACTIVE_ACTIVITY_CODE
                };

                loRspActivityValidityResult = await loModel.RSP_ACTIVITY_VALIDITYMethodAsync(loParam);
                loRspActivityValidityList = loRspActivityValidityResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void UserPasswordFieldValidation()
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (string.IsNullOrWhiteSpace(loParameter.USER))
                {
                    loException.Add("", "Username is required");
                }
                if (string.IsNullOrWhiteSpace(loParameter.PASSWORD))
                {
                    loException.Add("", "Password is required");
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        /*public async Task RSP_CREATE_ACTIVITY_APPROVAL_LOGMethodAsync()
        {
            R_Exception loException = new R_Exception();

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.ACTION_DETAIL_CONTEXT, DETAIL_ACTION);

                await loModel.RSP_CREATE_ACTIVITY_APPROVAL_LOGMethodAsync();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }*/


        public async Task UsernameAndPasswordValidationMethod()
        {
            R_Exception loException = new R_Exception();
            GFF00900DTO loParam = null;

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.VALIDATION_USER_CONTEXT, loParameter.USER);
                R_FrontContext.R_SetContext(ContextConstant.VALIDATION_PASSWORD_CONTEXT, loParameter.PASSWORD);
                //R_FrontContext.R_SetContext(ContextConstant.VALIDATION_ACTION_CODE_CONTEXT, loParameter.ACTION_CODE);
                //R_FrontContext.R_SetContext(ContextConstant.ACTION_DETAIL_CONTEXT, DETAIL_ACTION);
                loParam = new GFF00900DTO()
                {
                    CACTION_CODE = loParameter.ACTION_CODE,
                    DETAIL_ACTION = DETAIL_ACTION
                };

                await loModel.UsernameAndPasswordValidationMethodAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
