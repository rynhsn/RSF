using PMT02100COMMON;
using PMT02100COMMON.DTOs.PMT02100;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMT02100MODEL
{
    public class PMT02100Model : R_BusinessObjectServiceClientBase<PMT02100HandoverParameterDTO>, IPMT02100
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT02100";
        private const string DEFAULT_MODULE = "PM";

        public PMT02100Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }


        public async Task<PMT02100HandoverResultDTO> GetHandoverListStreamAsync()
        {
            var loEx = new R_Exception();
            PMT02100HandoverResultDTO loResult = new PMT02100HandoverResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02100HandoverDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02100.GetHandoverList),
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

        public async Task<PMT02100HandoverBuildingResultDTO> GetHandoverBuildingListStreamAsync()
        {
            var loEx = new R_Exception();
            PMT02100HandoverBuildingResultDTO loResult = new PMT02100HandoverBuildingResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02100HandoverBuildingDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02100.GetHandoverBuildingList),
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
                    nameof(IPMT02100.GetPropertyList),
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

        public async Task<GetPMSystemParamResultDTO> GetPMSystemParamAsync(GetPMSystemParamParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            GetPMSystemParamResultDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GetPMSystemParamResultDTO, GetPMSystemParamParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02100.GetPMSystemParam),
                    poParameter,
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

        #region Not Implemented

        public IAsyncEnumerable<PMT02100HandoverDTO> GetHandoverList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02100HandoverBuildingDTO> GetHandoverBuildingList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<GetPropertyListDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public GetPMSystemParamResultDTO GetPMSystemParam(GetPMSystemParamParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
