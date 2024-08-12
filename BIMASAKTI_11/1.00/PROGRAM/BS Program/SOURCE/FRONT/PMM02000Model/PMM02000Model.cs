using System;
using System.Threading.Tasks;
using PMM02000Common;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace PMM02000Model
{
    public class PMM02000Model : R_BusinessObjectServiceClientBase<PMM02000ExcelDTO>
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMM02000";
        private const string DEFAULT_MODULE = "pm";

        public PMM02000Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

    
        public async Task<PMM02000ExcelDTO> DownloadTemplateFileModel()
        {
            var loEx = new R_Exception();
            var loResult = new PMM02000ExcelDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMM02000ExcelDTO>(
                    _RequestServiceEndPoint,
                    "PMM02000DownloadTemplateFile",
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