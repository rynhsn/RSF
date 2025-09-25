using CBB00300COMMON;
using CBB00300COMMON.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CBB00300MODEL
{
    public class CBB00300Model : R_BusinessObjectServiceClientBase<CBB00300DTO>, ICBB00300
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlCB";
        private const string DEFAULT_ENDPOINT = "api/CBB00300";
        private const string DEFAULT_MODULE = "CB";

        public CBB00300Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public GenerateCashflowResultDTO GenerateCashflowProcess(GenerateCashflowParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task GenerateCashflowProcessAsync(GenerateCashflowParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                //Set Context
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GenerateCashflowResultDTO, GenerateCashflowParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBB00300.GenerateCashflowProcess),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public CBB00300ResultDTO GetCashflowInfo()
        {
            throw new NotImplementedException();
        }
        public async Task<CBB00300ResultDTO> GetCashflowInfoAsync()
        {
            R_Exception loEx = new R_Exception();
            CBB00300ResultDTO loResult = null;
            try
            {
                //Set Context
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<CBB00300ResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBB00300.GetCashflowInfo),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
    }
}
