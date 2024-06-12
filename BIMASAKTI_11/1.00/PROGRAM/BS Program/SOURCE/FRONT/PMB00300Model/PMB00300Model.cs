using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMB00300Common;
using PMB00300Common.DTOs;
using PMB00300Common.Params;
using R_APIClient;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace PMB00300Model
{
    public class PMB00300Model : R_BusinessObjectServiceClientBase<PMB00300RecalcDTO>, IPMB00300
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMB00300";
        private const string DEFAULT_MODULE = "pm";

        public PMB00300Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName,
            plSendWithContext,
            plSendWithToken)
        {
        }

        public PMB00300ListDTO<PMB00300PropertyDTO> PMB00300GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMB00300RecalcDTO> PMB00300GetRecalcListStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMB00300RecalcChargesDTO> PMB00300GetRecalcChargesListStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMB00300RecalcRuleDTO> PMB00300GetRecalcRuleListStream()
        {
            throw new NotImplementedException();
        }

        public PMB00300SingleDTO<bool> PMB00300RecalcBillingRuleProcess(PMB00300RecalcProcessParam poEntity)
        {
            throw new NotImplementedException();
        }
        
        
        public async Task<List<T>> GetListStreamAsync<T>(string pcNameOf)
        {
            var loEx = new R_Exception();
            List<T> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<T>(
                    _RequestServiceEndPoint,
                    pcNameOf,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken,
                    null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<T> GetAsync<T>(string pcNameOf) where T : R_APIResultBaseDTO
        {
            var loEx = new R_Exception();
            var loResult = default(T);

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<T>(
                    _RequestServiceEndPoint,
                    pcNameOf,
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