using LMM03700Common;
using LMM03700Common.DTO_s;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMM03700Model
{
    public class LMM03700Model : R_BusinessObjectServiceClientBase<TenantClassificationGroupDTO>, ILMM03700
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_CHECKPOINT_NAME = "api/LMM03700";
        private const string DEFAULT_MODULE = "LM";
        public LMM03700Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }

        public IAsyncEnumerable<TenantClassificationGroupDTO> GetTenantClassGroupList()
        {
            //this is dump implement method 
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PropertyDTO> LMM03700GetPropertyData()
        {
            //this is dump implement method 
            throw new NotImplementedException();
        }

        public async Task<List<TenantClassificationGroupDTO>> GetTenantClassRecord()
        {
            var loEx = new R_Exception();
            List<TenantClassificationGroupDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<TenantClassificationGroupDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03700.GetTenantClassGroupList),
                    DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;

        }

        public async Task<List<PropertyDTO>> GetPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<PropertyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03700.LMM03700GetPropertyData),
                    DEFAULT_MODULE, _SendWithContext,
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
