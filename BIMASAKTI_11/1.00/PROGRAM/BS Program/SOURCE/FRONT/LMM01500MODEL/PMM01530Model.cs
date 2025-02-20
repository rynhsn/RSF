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
    public class PMM01530Model : R_BusinessObjectServiceClientBase<PMM01530DTO>, IPMM01530
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMM01530";
        private const string DEFAULT_MODULE = "PM";

        public PMM01530Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }


        public IAsyncEnumerable<PMM01530DTO> GetAllOtherChargerList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<PMM01530DTO>> GetAllOtherChargerListAsync(string poPropertyId, string poInvGrpId)
        {
            var loEx = new R_Exception();
            List<PMM01530DTO> loResult = new List<PMM01530DTO>();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poPropertyId);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CINVGRP_CODE, poInvGrpId);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01530DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01530.GetAllOtherChargerList),
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


        public IAsyncEnumerable<PMM01531DTO> GetOtherChargesLookup()
        {
            throw new NotImplementedException();
        }

        public async Task<List<PMM01531DTO>> GetOtherChargesLookupAsync(PMM01531DTO poParam)
        {
            var loEx = new R_Exception();
            List<PMM01531DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poParam.CPROPERTY_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01531DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01530.GetOtherChargesLookup),
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

        public PMM01500SingleResult<PMM01531DTO> GetOtherChargesRecordLookup(PMM01531DTO poEntity)
        {
            throw new NotImplementedException();
        }
        public async Task<PMM01531DTO> GetOtherChargesRecordLookupAsync(PMM01531DTO poParam)
        {
            var loEx = new R_Exception();
            PMM01531DTO loResult = null;

            try
            {

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMM01500SingleResult<PMM01531DTO>, PMM01531DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01530.GetOtherChargesRecordLookup),
                    poParam,
                    DEFAULT_MODULE,
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
