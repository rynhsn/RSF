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

        public RSP_ACTIVITY_VALIDITYResultDTO loRspActivityValidityResult = new RSP_ACTIVITY_VALIDITYResultDTO();

        public string ACTIVATE_INACTIVE_ACTIVITY_CODE = "";

        public string DETAIL_ACTION = "";

        public async Task RSP_ACTIVITY_VALIDITYMethodAsync()
        {
            R_Exception loEx = new R_Exception();
            RSP_ACTIVITY_VALIDITYResultDTO loRtn = new RSP_ACTIVITY_VALIDITYResultDTO();

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.ACTIVITY_CODE_CONTEXT, ACTIVATE_INACTIVE_ACTIVITY_CODE);
                loRtn = await loModel.RSP_ACTIVITY_VALIDITYMethodAsync();
                loRspActivityValidityResult = loRtn;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
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

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.VALIDATION_USER_CONTEXT, loParameter.USER);
                R_FrontContext.R_SetContext(ContextConstant.VALIDATION_PASSWORD_CONTEXT, loParameter.PASSWORD);
                R_FrontContext.R_SetContext(ContextConstant.VALIDATION_ACTION_CODE_CONTEXT, loParameter.ACTION_CODE);
                R_FrontContext.R_SetContext(ContextConstant.ACTION_DETAIL_CONTEXT, DETAIL_ACTION);

                await loModel.UsernameAndPasswordValidationMethodAsync();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
