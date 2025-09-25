using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using GSM02500COMMON.DTOs;
using GSM02500COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs.GSM02501;

namespace GSM02500MODEL
{
    public class GSM02501Model : R_BusinessObjectServiceClientBase<GSM02501DetailDTO>, IGSM02501
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM02501";
        private const string DEFAULT_MODULE = "gs";

        public GSM02501Model(string pcHttpClientName = DEFAULT_HTTP_NAME, 
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME, 
            bool plSendWithContext = true, 
            bool plSendWithToken = true) : 
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<GSM02501PropertyDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }
        public async Task<GSM02501PropertyListDTO> GetPropertyListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GSM02501PropertyDTO> loResult = null;
            GSM02501PropertyListDTO loRtn = new GSM02501PropertyListDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM02501PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02501.GetPropertyList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public GSM02500ActiveInactiveResultDTO RSP_GS_ACTIVE_INACTIVE_PROPERTYMethod(GSM02500ActiveInactiveParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task RSP_GS_ACTIVE_INACTIVE_PROPERTYMethodAsync(GSM02500ActiveInactiveParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            GSM02500ActiveInactiveResultDTO loRtn = new GSM02500ActiveInactiveResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GSM02500ActiveInactiveResultDTO, GSM02500ActiveInactiveParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02501.RSP_GS_ACTIVE_INACTIVE_PROPERTYMethod),
                    poParam,
                    DEFAULT_MODULE,
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
