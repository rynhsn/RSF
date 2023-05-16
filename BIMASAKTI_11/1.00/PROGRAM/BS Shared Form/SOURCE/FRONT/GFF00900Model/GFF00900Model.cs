using GFF00900COMMON;
using GFF00900COMMON.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GFF00900Model
{
    public class GFF00900Model : R_BusinessObjectServiceClientBase<ValidationDTO>, IGFF00900
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl"; 
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GFF00900";

        public GFF00900Model(string pcHttpClientName = DEFAULT_HTTP_NAME, 
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME, 
            bool plSendWithContext = true, 
            bool plSendWithToken = true) : 
            base(pcHttpClientName, pcRequestServiceEndPoint, plSendWithContext, plSendWithToken)
        {
        }

        public ValidationResultDTO UsernameAndPasswordValidationMethod()
        {
            throw new NotImplementedException();
        }

        public async Task UsernameAndPasswordValidationMethodAsync()
        {
            var loEx = new R_Exception();
            ValidationResultDTO loRtn = new ValidationResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<ValidationResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGFF00900.UsernameAndPasswordValidationMethod),
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
    }
}
