using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ICB00100Common;
using ICB00100Common.DTOs;
using ICB00100Common.Params;
using R_APIClient;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace ICB00100Model
{
    public class ICB00100Model : R_BusinessObjectServiceClientBase<ICB00100SystemParamDTO>, IICB00100
    {
    
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlIC";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/ICB00100";
        private const string DEFAULT_MODULE = "ic";

        public ICB00100Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName,
            plSendWithContext,
            plSendWithToken)
        {
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
        
        //Untuk fetch data streaming dari controller dengan parameter
        public async Task<List<T>> GetListStreamAsync<T, T1>(string pcNameOf, T1 poParameter)
        {
            var loEx = new R_Exception();
            List<T> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<T, T1>(
                    _RequestServiceEndPoint,
                    pcNameOf,
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken,
                    null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        //Untuk fetch data object dari controller
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

        //Untuk fetch data object dari controller dengan parameter
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

        public ICB00100ListDTO<ICB00100PropertyDTO> ICB00100GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public ICB00100SingleDTO<ICB00100SystemParamDTO> ICB00100GetSystemParam(ICB00100SystemParamParam poParam)
        {
            throw new NotImplementedException();
        }

        public ICB00100SingleDTO<ICB00100PeriodYearRangeDTO> ICB00100GetPeriodYearRange()
        {
            throw new NotImplementedException();
        }

        public ICB00100SingleDTO<ICB00100PeriodParam> ICB00100UpdateSoftPeriod(ICB00100PeriodParam poParams)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<ICB00100ValidateSoftCloseDTO> ICB00100ValidateSoftPeriod()
        {
            throw new NotImplementedException();
        }

        public ICB00100SingleDTO<ICB00100SoftClosePeriodDTO> ICB00100ProcessSoftPeriod(ICB00100PeriodParam poParams)
        {
            throw new NotImplementedException();
        }
    }
}