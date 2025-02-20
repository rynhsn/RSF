using PMT01300COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMT01300MODEL
{
    public class PMT01300Model : R_BusinessObjectServiceClientBase<PMT01300DTO>, IPMT01300
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT01300";
        private const string DEFAULT_MODULE = "PM";

        public PMT01300Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<PMT01300DTO>> GetLOIListStreamAsync(string pcPropertyID, string pcTransStatusList)
        {
            var loEx = new R_Exception();
            List<PMT01300DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, pcPropertyID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPAR_TRANS_STS, pcTransStatusList);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01300DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01300.GetLOIListStream),
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
        public async Task<PMT01300DTO> GetLOIAsync(PMT01300DTO poParam)
        {
            var loEx = new R_Exception();
            PMT01300DTO loRtn = null;
            try
            {
                //Set Context
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01300SingleResult<PMT01300DTO>, PMT01300DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01300.GetLOI),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loRtn = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        public async Task<PMT01300DTO> SaveLOIAsync(PMT01300SaveDTO<PMT01300DTO> poParam)
        {
            var loEx = new R_Exception();
            PMT01300DTO loRtn = null;

            try
            {
                //Set Context
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01300SingleResult<PMT01300DTO>, PMT01300SaveDTO<PMT01300DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01300.SaveLOI),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loRtn = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        public async Task<PMT01300DTO> SubmitRedraftAgreementTransAsync(PMT01300SubmitRedraftDTO poParam)
        {
            var loEx = new R_Exception();
            PMT01300DTO loRtn = null;

            try
            {
                //Set Context
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01300SingleResult<PMT01300DTO>, PMT01300SubmitRedraftDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01300.SubmitRedraftAgreementTrans),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loRtn = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        public async Task<PMT01300UploadFileDTO> DownloadTemplateFileAsync()
        {
            var loEx = new R_Exception();
            PMT01300UploadFileDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01300UploadFileDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01300.DownloadTemplateFile),
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

        #region Not Implment
        public IAsyncEnumerable<PMT01300DTO> GetLOIListStream()
        {
            throw new NotImplementedException();
        }
        public PMT01300SingleResult<PMT01300DTO> GetLOI(PMT01300DTO poEntity)
        {
            throw new NotImplementedException();
        }
        public PMT01300SingleResult<PMT01300DTO> SaveLOI(PMT01300SaveDTO<PMT01300DTO> poEntity)
        {
            throw new NotImplementedException();
        }
        public PMT01300SingleResult<PMT01300DTO> SubmitRedraftAgreementTrans(PMT01300SubmitRedraftDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public PMT01300UploadFileDTO DownloadTemplateFile()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
