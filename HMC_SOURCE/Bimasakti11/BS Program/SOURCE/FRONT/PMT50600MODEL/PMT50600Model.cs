using PMT50600COMMON;
using PMT50600COMMON.DTOs.PMT50600;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMT50600MODEL
{
    public class PMT50600Model : R_BusinessObjectServiceClientBase<PMT50600ParameterDTO>, IPMT50600
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT50600";
        private const string DEFAULT_MODULE = "PM";

        public PMT50600Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public GetPMSystemParamResultDTO GetPMSystemParam(GetPMSystemParamParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task<GetPMSystemParamResultDTO> GetPMSystemParamAsync(GetPMSystemParamParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            GetPMSystemParamResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GetPMSystemParamResultDTO, GetPMSystemParamParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT50600.GetPMSystemParam),
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
                    nameof(IPMT50600.GetCompanyInfo),
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
                    nameof(IPMT50600.GetGLSystemParam),
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

        public IAsyncEnumerable<PMT50600DetailDTO> GetInvoiceList()
        {
            throw new NotImplementedException();
        }

        public async Task<PMT50600ResultDTO> GetInvoiceListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<PMT50600DetailDTO> loResult = null;
            PMT50600ResultDTO loRtn = new PMT50600ResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT50600DetailDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT50600.GetInvoiceList),
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
                    nameof(IPMT50600.GetPropertyList),
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
                    nameof(IPMT50600.GetPeriodYearRange),
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
                    nameof(IPMT50600.GetTransCodeInfo),
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
