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

        public async Task UsernameAndPasswordValidationMethod()
        {
            R_Exception loException = new R_Exception();

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.VALIDATION_USER_CONTEXT, loParameter.USER);
                R_FrontContext.R_SetContext(ContextConstant.VALIDATION_PASSWORD_CONTEXT, loParameter.PASSWORD);
                R_FrontContext.R_SetContext(ContextConstant.VALIDATION_ACTION_CODE_CONTEXT, loParameter.ACTION_CODE);

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
