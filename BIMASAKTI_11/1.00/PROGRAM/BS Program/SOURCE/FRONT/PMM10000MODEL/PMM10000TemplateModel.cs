using PMM10000COMMON.Interface;
using PMM10000COMMON.SLA_Call_Type;
using PMM10000COMMON.Upload;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMM10000MODEL
{
    public class PMM10000TemplateModel : R_BusinessObjectServiceClientBase<PMM10000SLACallTypeDTO>, IPMM10000Template
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMM10000Template";
        private const string DEFAULT_MODULE = "PM";
        public PMM10000TemplateModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<PMM10000TemplateDTO> DownloadTemplateFileAsync()
        {
            var loEx = new R_Exception();
            PMM10000TemplateDTO loResult = new PMM10000TemplateDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMM10000TemplateDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM10000Template.DownloadTemplateFile),
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

        public PMM10000TemplateDTO DownloadTemplateFile()
        {
            throw new NotImplementedException();
        }
    }
}
