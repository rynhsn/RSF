using APF00100COMMON;
using APF00100COMMON.DTOs.APF00100;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APF00100Model
{
    public class APF00100Model : R_BusinessObjectServiceClientBase<APF00100HeaderParameterDTO>, IAPF00100
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlAP";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/APF00100";
        private const string DEFAULT_MODULE = "AP";

        public APF00100Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<APF00100ListDTO> GetAllocationList()
        {
            throw new NotImplementedException();
        }
        public async Task<APF00100ListResultDTO> GetAllocationListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<APF00100ListDTO> loResult = null;
            APF00100ListResultDTO loRtn = new APF00100ListResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APF00100ListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPF00100.GetAllocationList),
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

        public GetCallerTrxInfoResultDTO GetCallerTrxInfo(GetCallerTrxInfoParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<GetCallerTrxInfoResultDTO> GetCallerTrxInfoAsync(GetCallerTrxInfoParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            GetCallerTrxInfoResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GetCallerTrxInfoResultDTO, GetCallerTrxInfoParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPF00100.GetCallerTrxInfo),
                    poParam,
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
                    nameof(IAPF00100.GetCompanyInfo),
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
                    nameof(IAPF00100.GetGLSystemParam),
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


        public APF00100HeaderResultDTO GetHeader(APF00100HeaderParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<APF00100HeaderResultDTO> GetHeaderAsync(APF00100HeaderParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            APF00100HeaderResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<APF00100HeaderResultDTO, APF00100HeaderParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPF00100.GetHeader),
                    poParam,
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

        public GetPeriodResultDTO GetPeriod(GetPeriodParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<GetPeriodResultDTO> GetPeriodAsync(GetPeriodParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            GetPeriodResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GetPeriodResultDTO, GetPeriodParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPF00100.GetPeriod),
                    poParam,
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

        public GetTransactionFlagResultDTO GetTransactionFlag(GetTransactionFlagParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<GetTransactionFlagResultDTO> GetTransactionFlagAsync(GetTransactionFlagParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            GetTransactionFlagResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GetTransactionFlagResultDTO, GetTransactionFlagParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPF00100.GetTransactionFlag),
                    poParam,
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
