using APR00100COMMON;
using APR00100COMMON.DTO_s;
using APR00100COMMON.DTO_s.Print;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APR00100MODEL
{
    public class APR00100Model : R_BusinessObjectServiceClientBase<ReportParamDTO>, IAPR00100
    {

        public APR00100Model(string pcHttpClientName = APR00100ContextConstant.DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = APR00100ContextConstant.DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                APR00100ContextConstant.DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }

        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public APR00100ResultDTO<TodayDTO> GetTodayDate()
        {
            throw new NotImplementedException();
        }

        public APR00100ResultDTO<PeriodYearDTO> GetPeriodYearRecord(PeriodYearDTO poParam)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<CustomerTypeDTO> GetCustomerTypeList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<TransTypeSuppCattDTO> GetTransTypeList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<TransTypeSuppCattDTO> GetSuppCattegoryList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<PropertyDTO>> GetPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<PropertyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = APR00100ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPR00100.GetPropertyList),
                    APR00100ContextConstant.DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;

        }

        public async Task<TodayDTO> GetTodayDateAsync()
        {
            var loEx = new R_Exception();
            TodayDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = APR00100ContextConstant.DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APR00100ResultDTO<TodayDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IAPR00100.GetTodayDate),
                    APR00100ContextConstant.DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<PeriodYearDTO> GetPeriodYearRecordAsync(PeriodYearDTO poParam)
        {
            var loEx = new R_Exception();
            PeriodYearDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = APR00100ContextConstant.DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APR00100ResultDTO<PeriodYearDTO>, PeriodYearDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPR00100.GetPeriodYearRecord),
                    poParam,
                    APR00100ContextConstant.DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<List<CustomerTypeDTO>> GetCustomerTypeListAsync()
        {
            var loEx = new R_Exception();
            List<CustomerTypeDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = APR00100ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CustomerTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPR00100.GetCustomerTypeList),
                    APR00100ContextConstant.DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;

        }
        public async Task<List<TransTypeSuppCattDTO>> GetTransTypeListAsync()
        {
            var loEx = new R_Exception();
            List<TransTypeSuppCattDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = APR00100ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<TransTypeSuppCattDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPR00100.GetTransTypeList),
                    APR00100ContextConstant.DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;

        }
        public async Task<List<TransTypeSuppCattDTO>> GetSuppCattegoryListAsync()
        {
            var loEx = new R_Exception();
            List<TransTypeSuppCattDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = APR00100ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<TransTypeSuppCattDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPR00100.GetSuppCattegoryList),
                    APR00100ContextConstant.DEFAULT_MODULE, _SendWithContext,
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
