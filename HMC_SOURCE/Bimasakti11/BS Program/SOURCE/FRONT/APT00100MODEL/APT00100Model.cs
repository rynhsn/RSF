using APT00100COMMON;
using APT00100COMMON.DTOs.APT00100;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APT00100MODEL
{
    public class APT00100Model : R_BusinessObjectServiceClientBase<APT00100ParameterDTO>, IAPT00100
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlAP";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/APT00100";
        private const string DEFAULT_MODULE = "AP";

        public APT00100Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public GetAPSystemParamResultDTO GetAPSystemParam()
        {
            throw new NotImplementedException();
        }
        public async Task<GetAPSystemParamResultDTO> GetAPSystemParamAsync()
        {
            R_Exception loEx = new R_Exception();
            GetAPSystemParamResultDTO loRtn = new GetAPSystemParamResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GetAPSystemParamResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00100.GetAPSystemParam),
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


        public GetCompanyInfoResultDTO GetCompanyInfo()
        {
            throw new NotImplementedException();
        }
        public async Task<GetCompanyInfoResultDTO> GetCompanyInfoAsync()
        {
            R_Exception loEx = new R_Exception();
            GetCompanyInfoResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GetCompanyInfoResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00100.GetCompanyInfo),
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

        public GetGLSystemParamResultDTO GetGLSystemParam()
        {
            throw new NotImplementedException();
        }
        public async Task<GetGLSystemParamResultDTO> GetGLSystemParamAsync()
        {
            R_Exception loEx = new R_Exception();
            GetGLSystemParamResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GetGLSystemParamResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00100.GetGLSystemParam),
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

        public IAsyncEnumerable<APT00100DetailDTO> GetInvoiceList()
        {
            throw new NotImplementedException();
        }

        public async Task<APT00100ResultDTO> GetInvoiceListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<APT00100DetailDTO> loResult = null;
            APT00100ResultDTO loRtn = new APT00100ResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APT00100DetailDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00100.GetInvoiceList),
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

        public IAsyncEnumerable<GetPropertyListDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public async Task<GetPropertyListResultDTO> GetPropertyListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GetPropertyListDTO> loResult = null;
            GetPropertyListResultDTO loRtn = new GetPropertyListResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetPropertyListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00100.GetPropertyList),
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

        public GetPeriodYearRangeResultDTO GetPeriodYearRange()
        {
            throw new NotImplementedException();
        }
        public async Task<GetPeriodYearRangeResultDTO> GetPeriodYearRangeAsync()
        {
            R_Exception loEx = new R_Exception();
            GetPeriodYearRangeResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GetPeriodYearRangeResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00100.GetPeriodYearRange),
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


        public GetTransCodeInfoResultDTO GetTransCodeInfo()
        {
            throw new NotImplementedException();
        }
        public async Task<GetTransCodeInfoResultDTO> GetTransCodeInfoAsync()
        {
            R_Exception loEx = new R_Exception();
            GetTransCodeInfoResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GetTransCodeInfoResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00100.GetTransCodeInfo),
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

    }
}
