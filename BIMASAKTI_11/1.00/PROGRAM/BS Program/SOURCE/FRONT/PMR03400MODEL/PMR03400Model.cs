using PMR03400COMMON;
using PMR03400COMMON.DTO_s;
using PMR03400COMMON.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMR03400MODEL
{
    public class PMR03400Model : R_BusinessObjectServiceClientBase<PMR03400SPResultDTO>, IPMR03400General
    {
        public PMR03400Model(string pcHttpClientName = PMR03400ContextConstant.DEFAULT_HTTP_NAME, 
            string pcRequestServiceEndPoint = PMR03400ContextConstant.DEFAULT_CHECKPOINT_NAME, 
            string pcModuleName = PMR03400ContextConstant.DEFAULT_MODULE, 
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
                R_HTTPClientWrapper.httpClientName = PMR03400ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR03400General.GetPropertyList),
                    PMR03400ContextConstant.DEFAULT_MODULE, _SendWithContext,
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
                R_HTTPClientWrapper.httpClientName = PMR03400ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PeriodDtDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR03400General.GetPeriodList),
                    PMR03400ContextConstant.DEFAULT_MODULE, _SendWithContext,
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
                R_HTTPClientWrapper.httpClientName = PMR03400ContextConstant.DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMR03400ResultBaseDTO<PeriodYearDTO>, PeriodYearDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR03400General.GetPeriodYearRecord),
                    poParam,
                    PMR03400ContextConstant.DEFAULT_MODULE,
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

        public async Task<PMR03400SingleDTO<PMR03400SystemParamDTO>> PMR03400GetSystemParam(PMR03400SystemParamDTO loParam)
        {
            var loEx = new R_Exception();
            PMR03400SingleDTO<PMR03400SystemParamDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = PMR03400ContextConstant.DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMR03400SingleDTO<PMR03400SystemParamDTO>, PMR03400SystemParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR03400General.PMR03400GetSystemParam),
                    loParam,
                    PMR03400ContextConstant.DEFAULT_MODULE,
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

        Task<PMR03400ResultBaseDTO<PeriodYearDTO>> IPMR03400General.GetPeriodYearRecord(PeriodYearDTO poParam)
        {
            throw new NotImplementedException();
        }
    }
}
