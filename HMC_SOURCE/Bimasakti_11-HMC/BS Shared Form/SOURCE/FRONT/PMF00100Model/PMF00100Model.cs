using PMF00100COMMON;
using PMF00100COMMON.DTOs.PMF00100;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMF00100Model
{
    public class PMF00100Model : R_BusinessObjectServiceClientBase<PMF00100HeaderParameterDTO>, IPMF00100
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMF00100";
        private const string DEFAULT_MODULE = "PM";

        public PMF00100Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<PMF00100ListDTO> GetAllocationList()
        {
            throw new NotImplementedException();
        }
        public async Task<PMF00100ListResultDTO> GetAllocationListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<PMF00100ListDTO> loResult = null;
            PMF00100ListResultDTO loRtn = new PMF00100ListResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMF00100ListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMF00100.GetAllocationList),
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

        //public PMF00100HeaderResultDTO GetCallerTrxInfo(PMF00100HeaderParameterDTO poParam)
        //{
        //    throw new NotImplementedException();
        //}
        //public async Task<PMF00100HeaderResultDTO> GetCallerTrxInfoAsync(PMF00100HeaderParameterDTO poParam)
        //{
        //    R_Exception loEx = new R_Exception();
        //    PMF00100HeaderResultDTO loRtn = null;

        //    try
        //    {
        //        R_HTTPClientWrapper.httpClientName = _HttpClientName;

        //        loRtn = await R_HTTPClientWrapper.R_APIRequestObject<PMF00100HeaderResultDTO, PMF00100HeaderParameterDTO>(
        //            _RequestServiceEndPoint,
        //            nameof(IPMF00100.GetCallerTrxInfo),
        //            poParam,
        //            DEFAULT_MODULE,
        //            _SendWithContext,
        //            _SendWithToken);
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //EndBlock:
        //    loEx.ThrowExceptionIfErrors();
        //    return loRtn;
        //}

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
                    nameof(IPMF00100.GetCompanyInfo),
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
                    nameof(IPMF00100.GetGLSystemParam),
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


        public PMF00100HeaderResultDTO GetHeader(PMF00100HeaderParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<PMF00100HeaderResultDTO> GetHeaderAsync(PMF00100HeaderParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            PMF00100HeaderResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<PMF00100HeaderResultDTO, PMF00100HeaderParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMF00100.GetHeader),
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


        public PMF00100HeaderResultDTO GetCAWTCustReceipt(PMF00100HeaderParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<PMF00100HeaderResultDTO> GetCAWTCustReceiptAsync(PMF00100HeaderParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            PMF00100HeaderResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<PMF00100HeaderResultDTO, PMF00100HeaderParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMF00100.GetCAWTCustReceipt),
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


        public PMF00100HeaderResultDTO GetCQCustReceipt(PMF00100HeaderParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<PMF00100HeaderResultDTO> GetCQCustReceiptAsync(PMF00100HeaderParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            PMF00100HeaderResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<PMF00100HeaderResultDTO, PMF00100HeaderParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMF00100.GetCQCustReceipt),
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
                    nameof(IPMF00100.GetPeriod),
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
                    nameof(IPMF00100.GetTransactionFlag),
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
