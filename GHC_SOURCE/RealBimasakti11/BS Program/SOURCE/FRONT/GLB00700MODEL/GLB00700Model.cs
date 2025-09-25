using GLB00700COMMON.DTO_s;
using GLB00700COMMON.DTO_s.Helper;
using GLB00700COMMON.Interfaces;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Threading.Tasks;

namespace GLB00700MODEL
{
    public class GLB00700Model : R_BusinessObjectServiceClientBase<GeneralParamDTO>, IGLB00700
    {
        //variables & constructors
        private const string DEFAULT_CHECKPOINT_NAME = "api/GLB00700";
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlGL";
        private const string DEFAULT_MODULE = "GL";
        public GLB00700Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) : base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        //real methods
        public async Task<GLSysParamDTO> GetGLSysParamRecordAsync()
        {
            var loEx = new R_Exception();
            GLSysParamDTO loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GeneralAPIResultDTO<GLSysParamDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGLB00700.GetGLSysParamRecord),
                    DEFAULT_MODULE,
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
        public async Task<TodayDTO> GetTodayRecordAsync()
        {
            var loEx = new R_Exception();
            TodayDTO loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GeneralAPIResultDTO<TodayDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGLB00700.GetTodayRecord),
                    DEFAULT_MODULE,
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
        public async Task<LastRateRevaluationDTO> GetLastRateRevaluationRecordAsync(RateRevaluationParamDTO poParam)
        {
            var loEx = new R_Exception();
            LastRateRevaluationDTO loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GeneralAPIResultDTO<LastRateRevaluationDTO>, RateRevaluationParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLB00700.GetLastRateRevaluationRecord),
                    poParam,
                    DEFAULT_MODULE,
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

        //implement methods
        public GeneralAPIResultDTO<GLSysParamDTO> GetGLSysParamRecord()
        {
            throw new NotImplementedException();
        }

        public GeneralAPIResultDTO<TodayDTO> GetTodayRecord()
        {
            throw new NotImplementedException();
        }

        public GeneralAPIResultDTO<LastRateRevaluationDTO> GetLastRateRevaluationRecord(LastRateRevaluationParamDTO poParam)
        {
            throw new NotImplementedException();
        }
    }
}
