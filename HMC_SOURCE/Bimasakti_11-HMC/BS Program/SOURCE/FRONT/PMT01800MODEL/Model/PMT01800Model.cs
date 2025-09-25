using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMT01800COMMON;
using PMT01800COMMON.DTO;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace PMT01800Model.Model
{
    public class PMT01800Model : R_BusinessObjectServiceClientBase<PMT01800DTO>, IPMT01800
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT01800";
        private const string DEFAULT_MODULE = "PM";

        public PMT01800Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<PMT01800PropertyListDTO> GetPropertySreamAsync()
        {
            var loEx = new R_Exception();
            PMT01800PropertyListDTO loResult = new PMT01800PropertyListDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01800PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01800.GetPropertySream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = loTemp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<PMT01800ListDTO> GetPMT01800StreamAsync()
        {
            var loEx = new R_Exception();
            PMT01800ListDTO loResult = new PMT01800ListDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01800DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01800.GetPMT01800Stream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = loTemp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<PMT01800ListDTO> GetPMT01800DetailStreamAsync()
        {
            var loEx = new R_Exception();
            PMT01800ListDTO loResult = new PMT01800ListDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01800DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01800.GetPMT01800DetailStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = loTemp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<PMT01800InitialProcessDTO> GetInitDayStreamAsync()
        {
            var loEx = new R_Exception();
            PMT01800InitialProcessDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01800InitialProcessDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01800.GetInitDayStream),
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

        public IAsyncEnumerable<PMT01800PropertyDTO> GetPropertySream()
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<PMT01800DTO> GetPMT01800Stream()
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<PMT01800DTO> GetPMT01800DetailStream()
        {
            throw new System.NotImplementedException();
        }

        public PMT01800InitialProcessDTO GetInitDayStream()
        {
            throw new System.NotImplementedException();
        }
    }
}