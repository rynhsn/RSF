using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Threading.Tasks;
using GSM06000Common;

namespace GSM06000Model
{
    public class UploadCenterModel : R_BusinessObjectServiceClientBase<GSM06000DTO>, IUploadCenter
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM06000Upload";
        private const string DEFAULT_MODULE = "GS";

        public UploadCenterModel(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<GSM06000UploadFileDTO> DownloadTemplateFileAsync()
        {
            var loEx = new R_Exception();
            GSM06000UploadFileDTO loResult = new GSM06000UploadFileDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM06000UploadFileDTO>(
                    _RequestServiceEndPoint,
                    nameof(IUploadCenter.DownloadTemplateFile),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        

        #region NotUsed!
        public GSM06000UploadFileDTO DownloadTemplateFile()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
