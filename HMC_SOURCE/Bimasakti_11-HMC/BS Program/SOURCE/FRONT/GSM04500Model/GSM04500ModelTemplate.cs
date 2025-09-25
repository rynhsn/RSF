
using GSM04500Common;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using R_BlazorFrontEnd;

namespace GSM04500Model
{
    public class GSM04500ModelTemplate : R_BusinessObjectServiceClientBase<GSM04500DTO>, IGSM04500Template
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrl";
        private const string DEFAULT_ENDPOINT = "api/GSM04500UploadTemplate";
        private const string DEFAULT_MODULE = "GS";

        public GSM04500ModelTemplate(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public GSM04500UploadFileDTO DownloadTemplateFile()
        {
            throw new NotImplementedException();
        }
        
        public async Task<GSM04500UploadFileDTO> DownloadTemplateFileAsync()
        {
            var loEx = new R_Exception();
            GSM04500UploadFileDTO loResult = new GSM04500UploadFileDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM04500UploadFileDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04500Template.DownloadTemplateFile),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                //loResult = loTempResult;
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
