using PMT02600COMMON;
using PMT02600COMMON.DTOs.PMT02600;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMT02600MODEL
{
    public class PMT02600Model : R_BusinessObjectServiceClientBase<PMT02600ParameterDTO>, IPMT02600
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT02600";
        private const string DEFAULT_MODULE = "PM";

        public PMT02600Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }


        public async Task<PMT02600ResultDTO> GetAgreementListStreamAsync()
        {
            var loEx = new R_Exception();
            PMT02600ResultDTO loResult = new PMT02600ResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02600DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02600.GetAgreementList),
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

        public async Task<PMT02600DetailResultDTO> GetAgreementDetailListStreamAsync()
        {
            var loEx = new R_Exception();
            PMT02600DetailResultDTO loResult = new PMT02600DetailResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02600DetailDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02600.GetAgreementDetailList),
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

        public async Task<GetPropertyListResultDTO> GetPropertyListStreamAsync()
        {
            var loEx = new R_Exception();
            GetPropertyListResultDTO loResult = new GetPropertyListResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetPropertyListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02600.GetPropertyList),
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

        public async Task UpdateAgreementTransStatusAsync(UpdateStatusDTO poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                //Set Context
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT02600ResultDTO, UpdateStatusDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02600.UpdateAgreementTransStatus),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<GetTransCodeInfoResultDTO> GetTransCodeInfoAsync()
        {
            R_Exception loEx = new R_Exception();
            GetTransCodeInfoResultDTO loRtn = null;

            try
            {
                //Set Context
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GetTransCodeInfoResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02600.GetTransCodeInfo),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        #region Not Implemented

        public IAsyncEnumerable<PMT02600DetailDTO> GetAgreementDetailList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02600DTO> GetAgreementList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<GetPropertyListDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public GetTransCodeInfoResultDTO GetTransCodeInfo()
        {
            throw new NotImplementedException();
        }

        public UpdateStatusResultDTO UpdateAgreementTransStatus(UpdateStatusDTO poParameter)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
