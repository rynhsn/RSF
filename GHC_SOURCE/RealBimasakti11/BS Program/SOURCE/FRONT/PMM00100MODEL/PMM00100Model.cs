using PMM00100COMMON;
using PMM00100COMMON.DTO_s;
using PMM00100COMMON.DTO_s.General;
using PMM00100COMMON.DTO_s.Helper;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMM00100MODEL
{
    public class PMM00100Model : R_BusinessObjectServiceClientBase<GeneralParamDTO>, IPMM00100
    {
        private const string DEFAULT_CHECKPOINT_NAME = "api/PMM00100General";
        public PMM00100Model(
            string pcHttpClientName = PMM00100ContextConstant.DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                PMM00100ContextConstant.DEFAULT_MODULE,
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
                R_HTTPClientWrapper.httpClientName = PMM00100ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM00100.GetPropertyList),
                    PMM00100ContextConstant.DEFAULT_MODULE, _SendWithContext,
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
                R_HTTPClientWrapper.httpClientName = PMM00100ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PeriodDtDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM00100.GetPeriodDtList),
                    PMM00100ContextConstant.DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;

        }

        public async Task<List<GeneralTypeDTO>> GetGSBCodeInfoListAsync()
        {
            var loEx = new R_Exception();
            List<GeneralTypeDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = PMM00100ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GeneralTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM00100.GetGSBCodeInfoList),
                    PMM00100ContextConstant.DEFAULT_MODULE,
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

        public async Task<PeriodYearRangeDTO> GetPeriodYearRangeRecordAsync(PeriodYearRangeParamDTO poParam)
        {
            var loEx = new R_Exception();
            PeriodYearRangeDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = PMM00100ContextConstant.DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GeneralAPIResultDTO<PeriodYearRangeDTO>, PeriodYearRangeParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM00100.GetPeriodYearRangeRecord),
                    poParam,
                    PMM00100ContextConstant.DEFAULT_MODULE,
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


        public IAsyncEnumerable<GeneralTypeDTO> GetGSBCodeInfoList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PeriodDtDTO> GetPeriodDtList()
        {
            throw new NotImplementedException();
        }

        public GeneralAPIResultDTO<PeriodYearRangeDTO> GetPeriodYearRangeRecord(PeriodYearRangeParamDTO poParam)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }
    }
}
