using APT00200COMMON.DTOs.APT00210;
using APT00200COMMON;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using APT00200COMMON.DTOs.APT00211;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;

namespace APT00200MODEL
{
    public class APT00211Model : R_BusinessObjectServiceClientBase<APT00211ListParameterDTO>, IAPT00211
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlAP";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/APT00211";
        private const string DEFAULT_MODULE = "AP";

        public APT00211Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public APT00211DetailResultDTO GetDetailInfo(APT00211DetailParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task<APT00211DetailResultDTO> GetDetailInfoAsync(APT00211DetailParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            APT00211DetailResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<APT00211DetailResultDTO, APT00211DetailParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00211.GetDetailInfo),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }


        public APT00211HeaderResultDTO GetHeaderInfo(APT00211HeaderParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task<APT00211HeaderResultDTO> GetHeaderInfoAsync(APT00211HeaderParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            APT00211HeaderResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<APT00211HeaderResultDTO, APT00211HeaderParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00211.GetHeaderInfo),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }


        public IAsyncEnumerable<APT00211ListDTO> GetPurchaseReturnItemList()
        {
            throw new NotImplementedException();
        }
        public async Task<APT00211ListResultDTO> GetPurchaseReturnItemListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<APT00211ListDTO> loResult = null;
            APT00211ListResultDTO loRtn = new APT00211ListResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APT00211ListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00211.GetPurchaseReturnItemList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}
