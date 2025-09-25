using PMR00800COMMON;
using PMR00800COMMON.DTO_s;
using PMR00800COMMON.DTO_s.General;
using PMR00800COMMON.DTO_s.Helper;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMR00800MODEL
{
    public class PMR00800Model : R_BusinessObjectServiceClientBase<PMR00800ParamDTO>, IPMR00800Init
    {
        public const string DEFAULT_CHECKPOINT_NAME = "api/PMR00800";
        public PMR00800Model(string pcHttpClientName = PMR00800ContextConstant.DEFAULT_HTTP_NAME,
    string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
    bool plSendWithContext = true,
    bool plSendWithToken = true
    ) : base(
        pcHttpClientName,
        pcRequestServiceEndPoint,
        PMR00800ContextConstant.DEFAULT_MODULE,
        plSendWithContext,
        plSendWithToken)
        {
        }
        public async Task<List<PropertyDTO>> GetPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<PropertyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = PMR00800ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR00800Init.GetPropertyList),
                    PMR00800ContextConstant.DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
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
                R_HTTPClientWrapper.httpClientName = PMR00800ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PeriodDtDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR00800Init.GetPeriodDtList),
                    PMR00800ContextConstant.DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;

        }

        public async Task<PeriodYearRangeDTO> GetPeriodYearRangeRecordAsync(PeriodYearRangeParamDTO poParam)
        {
            var loEx = new R_Exception();
            PeriodYearRangeDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = PMR00800ContextConstant.DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GeneralAPIResultDTO<PeriodYearRangeDTO>, PeriodYearRangeParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR00800Init.GetPeriodYearRangeRecord),
                    poParam,
                    PMR00800ContextConstant.DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }


        //implement only

        public GeneralAPIResultDTO<PeriodYearRangeDTO> GetPeriodYearRangeRecord(PeriodYearRangeParamDTO poParam)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PeriodDtDTO> GetPeriodDtList()
        {
            throw new NotImplementedException();
        }
    }
}
