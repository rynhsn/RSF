using PMT50600COMMON.DTOs.PMT50610;
using PMT50600COMMON;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using PMT50600COMMON.DTOs.PMT50611;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;

namespace PMT50600MODEL
{
    public class PMT50611Model : R_BusinessObjectServiceClientBase<PMT50611ListParameterDTO>, IPMT50611
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT50611";
        private const string DEFAULT_MODULE = "PM";

        public PMT50611Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public PMT50611DetailResultDTO GetDetailInfo(PMT50611DetailParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task<PMT50611DetailResultDTO> GetDetailInfoAsync(PMT50611DetailParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            PMT50611DetailResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<PMT50611DetailResultDTO, PMT50611DetailParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT50611.GetDetailInfo),
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


        public PMT50611HeaderResultDTO GetHeaderInfo(PMT50611HeaderParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task<PMT50611HeaderResultDTO> GetHeaderInfoAsync(PMT50611HeaderParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            PMT50611HeaderResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<PMT50611HeaderResultDTO, PMT50611HeaderParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT50611.GetHeaderInfo),
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


        public IAsyncEnumerable<PMT50611ListDTO> GetInvoiceItemList()
        {
            throw new NotImplementedException();
        }
        public async Task<PMT50611ListResultDTO> GetInvoiceItemListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<PMT50611ListDTO> loResult = null;
            PMT50611ListResultDTO loRtn = new PMT50611ListResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT50611ListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT50611.GetInvoiceItemList),
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
