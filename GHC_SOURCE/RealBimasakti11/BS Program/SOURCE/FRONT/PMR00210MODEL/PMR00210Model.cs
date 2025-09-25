using PMR00210COMMON;
using PMR00210COMMON.DTO_s;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMR00210MODEL
{
    public class PMR00210Model : R_BusinessObjectServiceClientBase<PMR00210SPResultDTO>, IPMR00210General
    {


        public PMR00210Model(string pcHttpClientName = PMR00210ContextConstant.DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = PMR00210ContextConstant.DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                PMR00210ContextConstant.DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }

        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public PMR00210ResultBaseDTO<TodayDTO> GetTodayDate()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PeriodDtDTO> GetPeriodList()
        {
            throw new NotImplementedException();
        }

        public PMR00210ResultBaseDTO<PeriodYearDTO> GetPeriodYearRecord(PeriodYearDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PropertyDTO>> GetPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<PropertyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = PMR00210ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR00210General.GetPropertyList),
                    PMR00210ContextConstant.DEFAULT_MODULE, _SendWithContext,
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
                R_HTTPClientWrapper.httpClientName = PMR00210ContextConstant.DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMR00210ResultBaseDTO<TodayDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMR00210General.GetTodayDate),
                    PMR00210ContextConstant.DEFAULT_MODULE,
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

        public async Task<List<PeriodDtDTO>> GetPeriodDtListAsync()
        {
            var loEx = new R_Exception();
            List<PeriodDtDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = PMR00210ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PeriodDtDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR00210General.GetPeriodList),
                    PMR00210ContextConstant.DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
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
                R_HTTPClientWrapper.httpClientName = PMR00210ContextConstant.DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMR00210ResultBaseDTO<PeriodYearDTO>, PeriodYearDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR00210General.GetPeriodYearRecord),
                    poParam,
                    PMR00210ContextConstant.DEFAULT_MODULE,
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
    }
}
