using LMM06500COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMM06500MODEL
{
    public class LMM06501Model : R_BusinessObjectServiceClientBase<LMM06502DTO>, ILMM06501
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM06501";
        private const string DEFAULT_MODULE = "LM";

        public LMM06501Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public LMM06500UploadFileDTO DownloadTemplateFile()
        {
            throw new NotImplementedException();
        }

        public async Task<LMM06500UploadFileDTO> DownloadTemplateFileAsync()
        {
            var loEx = new R_Exception();
            LMM06500UploadFileDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM06500UploadFileDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06501.DownloadTemplateFile),
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

        public IAsyncEnumerable<LMM06501ErrorValidateDTO> GetErrorProcess()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LMM06501ErrorValidateDTO>> GetErrorProcessAsync(string pcKeyGuid)
        {
            var loEx = new R_Exception();
            List<LMM06501ErrorValidateDTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext("UploadStaffKeyGuid", pcKeyGuid); //key change to constant

                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM06501ErrorValidateDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06501.GetErrorProcess),
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

        public IAsyncEnumerable<LMM06500DTO> GetStaffUploadList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<LMM06500DTO>> GetStaffUploadListAsync(string poPropertyId)
        {
            var loEx = new R_Exception();
            List<LMM06500DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poPropertyId);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM06500DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06501.GetStaffUploadList),
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
