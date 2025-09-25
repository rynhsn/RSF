using System;
using System.Threading.Tasks;
using GLExcelTestCommon;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GLM00501Model
{
    public class GLM00501Model : R_BusinessObjectServiceClientBase<GLExcelTestFileDTO>
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlGL";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GLExcelTest";
        private const string DEFAULT_MODULE = "gl";

        public GLM00501Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

    
        public async Task<GLExcelTestFileDTO> GLM00500DownloadTemplateFileModel()
        {
            var loEx = new R_Exception();
            var loResult = new GLExcelTestFileDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLExcelTestFileDTO>(
                    _RequestServiceEndPoint,
                    "GLExcelTestDownloadTemplateFile",
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