using APR00700COMMON;
using APR00700COMMON.DTO_s;
using APR00700COMMON.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APR00700MODEL
{
    public class APR00700Model : R_BusinessObjectServiceClientBase<APR00700SPResultDTO>, IAPR00700General
    {
        public APR00700Model(string pcHttpClientName = APR00700ContextConstant.DEFAULT_HTTP_NAME, 
            string pcRequestServiceEndPoint = APR00700ContextConstant.DEFAULT_CHECKPOINT_NAME, 
            string pcModuleName = APR00700ContextConstant.DEFAULT_MODULE, 
            bool plSendWithContext = true, 
            bool plSendWithToken = true
            ) : base(pcHttpClientName, 
                pcRequestServiceEndPoint, 
                pcModuleName, 
                plSendWithContext, 
                plSendWithToken)
        {
        }

        public IAsyncEnumerable<PeriodDtDTO> GetPeriodList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<PropertyDTO>> GetPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<PropertyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = APR00700ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPR00700General.GetPropertyList),
                    APR00700ContextConstant.DEFAULT_MODULE, _SendWithContext,
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
                R_HTTPClientWrapper.httpClientName = APR00700ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PeriodDtDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPR00700General.GetPeriodList),
                    APR00700ContextConstant.DEFAULT_MODULE, _SendWithContext,
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
                R_HTTPClientWrapper.httpClientName = APR00700ContextConstant.DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APR00700ResultBaseDTO<PeriodYearDTO>, PeriodYearDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPR00700General.GetPeriodYearRecord),
                    poParam,
                    APR00700ContextConstant.DEFAULT_MODULE,
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

        public async Task<APR00700SingleDTO<APR00700SystemParamDTO>> APR00700GetSystemParam(APR00700SystemParamDTO loParam)
        {
            var loEx = new R_Exception();
            APR00700SingleDTO<APR00700SystemParamDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = APR00700ContextConstant.DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APR00700SingleDTO<APR00700SystemParamDTO>, APR00700SystemParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPR00700General.APR00700GetSystemParam),
                    loParam,
                    APR00700ContextConstant.DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        Task<APR00700ResultBaseDTO<PeriodYearDTO>> IAPR00700General.GetPeriodYearRecord(PeriodYearDTO poParam)
        {
            throw new NotImplementedException();
        }
    }
}
