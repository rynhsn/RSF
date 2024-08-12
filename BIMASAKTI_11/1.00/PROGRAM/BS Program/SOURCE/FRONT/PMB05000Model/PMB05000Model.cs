using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMB05000Common;
using PMB05000Common.DTOs;
using PMB05000Common.Params;
using R_APIClient;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace PMB05000Model
{
    public class PMB05000Model : R_BusinessObjectServiceClientBase<PMB05000SystemParamDTO>, IPMB05000
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMB05000";
        private const string DEFAULT_MODULE = "pm";

        public PMB05000Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName,
            plSendWithContext,
            plSendWithToken)
        {
        }

        public PMB05000SingleDTO<PMB05000SystemParamDTO> PMB05000GetSystemParam()
        {
            throw new NotImplementedException();
        }

        public PMB05000SingleDTO<PMB05000PeriodYearRangeDTO> PMB05000GetPeriod()
        {
            throw new NotImplementedException();
        }

        public PMB05000SingleDTO<PMB05000PeriodParam> PMB05000UpdateSoftPeriod(PMB05000PeriodParam poParams)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMB05000ValidateSoftCloseDTO> PMB05000ValidateSoftPeriod()
        {
            throw new NotImplementedException();
        }

        public PMB05000SingleDTO<PMB05000SoftClosePeriodDTO> PMB05000ProcessSoftPeriod(PMB05000PeriodParam poParams)
        {
            throw new NotImplementedException();
        }
        
        
        //Untuk fetch data streaming dari controller  
        public async Task<List<T>> GetListStreamAsync<T>(string pcNameOf)
        {
            var loEx = new R_Exception();
            var loResult = new List<T>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<T>(
                    _RequestServiceEndPoint,
                    pcNameOf,
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

        public async Task<T> GetAsync<T>(string pcNameOf) where T : R_APIResultBaseDTO
        {
            var loEx = new R_Exception();
            var loResult = default(T);

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<T>(
                    _RequestServiceEndPoint,
                    pcNameOf,
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

        public async Task<T> GetAsync<T, T1>(string pcNameOf, T1 poParameter) where T : R_APIResultBaseDTO
        {
            var loEx = new R_Exception();
            var loResult = default(T);

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<T, T1>(
                    _RequestServiceEndPoint,
                    pcNameOf,
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
    }
}