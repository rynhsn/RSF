using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using FAF00100COMMON;
using FAF00100COMMON.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace FAF00100Model
{
    public class FAF00100Model : R_BusinessObjectServiceClientBase<FAF00100GetAssetResultDTO>, IFAF00100
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlFA";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/FAF00100";
        private const string DEFAULT_MODULE = "FA"; 

        public FAF00100Model
            (string pcHttpClientName = DEFAULT_HTTP_NAME, 
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME, 
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true, 
            bool plSendWithToken = true) : 
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<FAF00100GetAssetAllocResultDTO> GetListAssetAlloc()
        {
            throw new NotImplementedException();
        }

        public async Task<FAF00100ResultDTO<List<FAF00100GetAssetAllocResultDTO>>> GetListAssetAllocAsync()
        {
            var loEx = new R_Exception();
            var loRtn = new FAF00100ResultDTO<List<FAF00100GetAssetAllocResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAF00100GetAssetAllocResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAF00100.GetListAssetAlloc),
                    _ModuleName,
                    true,
                    true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
    }
}
