using PMT00500COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMT00500MODEL
{
    public class PMT00500Model : R_BusinessObjectServiceClientBase<PMT00500DTO>, IPMT00500
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT00500";
        private const string DEFAULT_MODULE = "PM";

        public PMT00500Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<PMT00500DTO>> GetLOIListStreamAsync(string pcPropertyID, string pcTransStatusList)
        {
            var loEx = new R_Exception();
            List<PMT00500DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, pcPropertyID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPAR_TRANS_STS, pcTransStatusList);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT00500DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT00500.GetLOIListStream),
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
        public async Task<PMT00500DTO> GetLOIAsync(PMT00500DTO poParam)
        {
            var loEx = new R_Exception();
            PMT00500DTO loRtn = null;
            try
            {
                //Set Context
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT00500SingleResult<PMT00500DTO>, PMT00500DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT00500.GetLOI),
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
        public async Task<PMT00500DTO> SaveLOIAsync(PMT00500SaveDTO<PMT00500DTO> poParam)
        {
            var loEx = new R_Exception();
            PMT00500DTO loRtn = null;

            try
            {
                //Set Context
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT00500SingleResult<PMT00500DTO>, PMT00500SaveDTO<PMT00500DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMT00500.SaveLOI),
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
        public async Task<PMT00500DTO> SubmitRedraftAgreementTransAsync(PMT00500SubmitRedraftDTO poParam)
        {
            var loEx = new R_Exception();
            PMT00500DTO loRtn = null;

            try
            {
                //Set Context
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT00500SingleResult<PMT00500DTO>, PMT00500SubmitRedraftDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT00500.SubmitRedraftAgreementTrans),
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
        public async Task<PMT00500UploadFileDTO> DownloadTemplateFileAsync()
        {
            var loEx = new R_Exception();
            PMT00500UploadFileDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT00500UploadFileDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT00500.DownloadTemplateFile),
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
        public async Task<PMT00500LeaseDTO> LeaseProcessAsync(PMT00500LeaseDTO poParam)
        {
            var loEx = new R_Exception();
            PMT00500LeaseDTO loRtn = null;

            try
            {
                //Set Context
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT00500SingleResult<PMT00500LeaseDTO>, PMT00500LeaseDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT00500.LeaseProcess),
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

        #region Not Implment
        public IAsyncEnumerable<PMT00500DTO> GetLOIListStream()
        {
            throw new NotImplementedException();
        }
        public PMT00500SingleResult<PMT00500DTO> GetLOI(PMT00500DTO poEntity)
        {
            throw new NotImplementedException();
        }
        public PMT00500SingleResult<PMT00500DTO> SaveLOI(PMT00500SaveDTO<PMT00500DTO> poEntity)
        {
            throw new NotImplementedException();
        }
        public PMT00500SingleResult<PMT00500DTO> SubmitRedraftAgreementTrans(PMT00500SubmitRedraftDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public PMT00500UploadFileDTO DownloadTemplateFile()
        {
            throw new NotImplementedException();
        }
        public PMT00500SingleResult<PMT00500LeaseDTO> LeaseProcess(PMT00500LeaseDTO poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
