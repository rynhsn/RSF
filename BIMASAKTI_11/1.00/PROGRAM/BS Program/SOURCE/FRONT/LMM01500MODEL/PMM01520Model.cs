using PMM01500COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMM01500MODEL
{
    public class PMM01520Model : R_BusinessObjectServiceClientBase<PMM01520DTO>, IPMM01520
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMM01520";
        private const string DEFAULT_MODULE = "PM";

        public PMM01520Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }


        public async Task<List<PMM01520DTO>> GetPinaltyDateListStreamAsync(PMM01520DTO poEntity)
        {
            var loEx = new R_Exception();
            List<PMM01520DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poEntity.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CINVGRP_CODE, poEntity.CINVGRP_CODE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01520DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01520.GetPinaltyDateListStream),
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
        public async Task<PMM01520DTO> GetPinaltyDateAsync(PMM01520DTO poParam)
        {
            var loEx = new R_Exception();
            PMM01520DTO llRtn = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempRtn = await R_HTTPClientWrapper.R_APIRequestObject<PMM01500SingleResult<PMM01520DTO>, PMM01520DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01520.GetPinaltyDate),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                llRtn = loTempRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
            return llRtn;
        }
        public async Task<PMM01520DTO> GetPinaltyAsync(PMM01520DTO poParam)
        {
            var loEx = new R_Exception();
            PMM01520DTO llRtn = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempRtn = await R_HTTPClientWrapper.R_APIRequestObject<PMM01500SingleResult<PMM01520DTO>, PMM01520DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01520.GetPinalty),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                llRtn = loTempRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
            return llRtn;
        }
        public async Task<PMM01520DTO> SaveDeletePinaltyAsync(PMM01520SaveParameterDTO<PMM01520DTO> poParam)
        {
            var loEx = new R_Exception();
            PMM01520DTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempRtn = await R_HTTPClientWrapper.R_APIRequestObject<PMM01500SingleResult<PMM01520DTO>, PMM01520SaveParameterDTO<PMM01520DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01520.SaveDeletePinalty),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loRtn = loTempRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        #region Not Implement
        public IAsyncEnumerable<PMM01520DTO> GetPinaltyDateListStream()
        {
            throw new NotImplementedException();
        }
        public PMM01500SingleResult<PMM01520DTO> GetPinaltyDate(PMM01520DTO poEntity)
        {
            throw new NotImplementedException();
        }
        public PMM01500SingleResult<PMM01520DTO> GetPinalty(PMM01520DTO poEntity)
        {
            throw new NotImplementedException();
        }
        public PMM01500SingleResult<PMM01520DTO> SaveDeletePinalty(PMM01520SaveParameterDTO<PMM01520DTO> poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
