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
        private const string DEFAULT_MODULE_NAME = "GS";

        public GFF00900Model(string pcHttpClientName = DEFAULT_HTTP_NAME, 
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME, 
            bool plSendWithContext = true, 
            bool plSendWithToken = true) : 
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE_NAME, plSendWithContext, plSendWithToken)
        {
        }

        public RSP_ACTIVITY_VALIDITYResultDTO RSP_ACTIVITY_VALIDITYMethod()
        {
            throw new NotImplementedException();
        }

        public async Task<RSP_ACTIVITY_VALIDITYResultDTO> RSP_ACTIVITY_VALIDITYMethodAsync()
        {
            var loEx = new R_Exception();
            RSP_ACTIVITY_VALIDITYResultDTO loRtn = new RSP_ACTIVITY_VALIDITYResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<RSP_ACTIVITY_VALIDITYResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGFF00900.RSP_ACTIVITY_VALIDITYMethod),
                    DEFAULT_MODULE_NAME,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
/*
        public RSP_CREATE_ACTIVITY_APPROVAL_LOGResultDTO RSP_CREATE_ACTIVITY_APPROVAL_LOGMethod()
        {
            throw new NotImplementedException();
        }

        public async Task RSP_CREATE_ACTIVITY_APPROVAL_LOGMethodAsync()
        {
            var loEx = new R_Exception();
            RSP_CREATE_ACTIVITY_APPROVAL_LOGResultDTO loRtn = new RSP_CREATE_ACTIVITY_APPROVAL_LOGResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<RSP_CREATE_ACTIVITY_APPROVAL_LOGResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGFF00900.RSP_CREATE_ACTIVITY_APPROVAL_LOGMethod),
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
*/
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
                    DEFAULT_MODULE_NAME,
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
