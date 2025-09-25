using PMR00600COMMON;
using PMR00600COMMON.DTO_s;
using PMR00600COMMON.DTO_s.Print;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMR00600MODEL
{
    public class PMR00600Model : R_BusinessObjectServiceClientBase<PMR00600ParamDTO>, IPMR00600General
    {

        public PMR00600Model(string pcHttpClientName = PMR00600ContextConstant.DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = PMR00600ContextConstant.DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                PMR00600ContextConstant.DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }

        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public PMR00600Result<TodayDTO> GetTodayDate()
        {
            throw new NotImplementedException();
        }

        public PMR00600Result<PeriodYearDTO> GetPeriodYearRecord(PeriodYearDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PropertyDTO>> GetPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<PropertyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = PMR00600ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR00600General.GetPropertyList),
                    PMR00600ContextConstant.DEFAULT_MODULE, _SendWithContext,
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
                R_HTTPClientWrapper.httpClientName = PMR00600ContextConstant.DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMR00600Result<TodayDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMR00600General.GetTodayDate),
                    PMR00600ContextConstant.DEFAULT_MODULE,
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
                R_HTTPClientWrapper.httpClientName = PMR00600ContextConstant.DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMR00600Result<PeriodYearDTO>, PeriodYearDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR00600General.GetPeriodYearRecord),
                    poParam,
                    PMR00600ContextConstant.DEFAULT_MODULE,
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
