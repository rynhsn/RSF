using PMT02000COMMON.Interface;
using PMT02000COMMON.LOI_List;
using PMT02000COMMON.Utility;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMT02000MODEL
{
    public class PMT02000LOIModel : R_BusinessObjectServiceClientBase<PMT02000LOIHeader_DetailDTO>, IPMT02000
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT02000LOI";
        private const string DEFAULT_MODULE = "PM";
        public PMT02000LOIModel(
         string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }
        #region Implements Library

        public IAsyncEnumerable<PMT02000LOIDTO> GetLOIListStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02000PropertyDTO> GetPropertyListStream()
        {
            throw new NotImplementedException();
        }
        public PMT02000LOIHeader GetLOIHeader(PMT02000DBParameter poParam)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02000LOIDetailListDTO> GetLOIDetailListStream()
        {
            throw new NotImplementedException();
        }
        public PMT02000LOIHeader ProcessSubmitRedraft(PMT02000DBParameter poParam)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Implements
        public async Task<PMT02000GenericList<PMT02000PropertyDTO>> PropertyListStreamAsyncModel()
        {
            var loEx = new R_Exception();
            PMT02000GenericList<PMT02000PropertyDTO> loResult = new PMT02000GenericList<PMT02000PropertyDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02000PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02000.GetPropertyListStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = temp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<PMT02000GenericList<PMT02000LOIDTO>> LOIListStreamAsyncModel()
        {
            var loEx = new R_Exception();
            PMT02000GenericList<PMT02000LOIDTO> loResult = new PMT02000GenericList<PMT02000LOIDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02000LOIDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02000.GetLOIListStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = temp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
         public async Task<PMT02000LOIHeader> GetLOIHeaderModel(PMT02000DBParameter poParam)
        {
            var loEx = new R_Exception();
            PMT02000LOIHeader loResult = new PMT02000LOIHeader();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT02000LOIHeader, PMT02000DBParameter>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02000.GetLOIHeader),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                // loResult = temp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<PMT02000LOIHeader> ProcessSubmitRedraftAsyncMosel(PMT02000DBParameter poParam)
        {
            var loEx = new R_Exception();
            PMT02000LOIHeader loResult = new PMT02000LOIHeader();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT02000LOIHeader, PMT02000DBParameter>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02000.ProcessSubmitRedraft),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                // loResult = temp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<PMT02000GenericList<PMT02000LOIDetailListDTO>> LOIListDetailStreamAsyncModel()
        {
            var loEx = new R_Exception();
            PMT02000GenericList<PMT02000LOIDetailListDTO> loResult = new PMT02000GenericList<PMT02000LOIDetailListDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02000LOIDetailListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02000.GetLOIDetailListStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = temp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }



        #endregion
    }
}
