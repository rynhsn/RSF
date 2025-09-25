using PMR03100COMMON;
using PMR03100COMMON.DTO_s;
using PMR03100COMMON.DTO_s.General;
using PMR03100COMMON.DTO_s.Helper;
using PMR03100COMMON.DTO_s.Print;
using PMR03100COMMON.Interfaces;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMR03100MODEL
{
    public class PMR03100Model : R_BusinessObjectServiceClientBase<ParamDTO>, IPMR03100
    {
        public const string DEFAULT_CHECKPOINT_NAME = "api/PMR03100";
        public PMR03100Model(
            string pcHttpClientName = ContextConstant.DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                ContextConstant.DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken
                )
        {
        }

        public async Task<List<PropertyDTO>> GetPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<PropertyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR03100.GetPropertyList),
                    ContextConstant.DEFAULT_MODULE, _SendWithContext,
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
                R_HTTPClientWrapper.httpClientName = ContextConstant.DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GeneralAPIResultDTO<PeriodYearRangeDTO>, PeriodYearRangeParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR03100.GetPeriodYearRangeRecord),
                    poParam,
                    ContextConstant.DEFAULT_MODULE,
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
    }
}
