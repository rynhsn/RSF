using PMM01000COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMM01000MODEL
{
    public class PMM01000Model : R_BusinessObjectServiceClientBase<PMM01000DTO>, IPMM01000
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMM01000";
        private const string DEFAULT_MODULE = "PM";

        public PMM01000Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }


        public IAsyncEnumerable<PMM01002DTO> GetChargesUtilityList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<PMM01002DTO>> GetChargesUtilityListAsync(PMM01002DTO poParam)
        {
            var loEx = new R_Exception();
            List<PMM01002DTO> loRtn = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_TYPE, poParam.CCHARGES_TYPE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01002DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01000.GetChargesUtilityList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public PMM01000DTO PMM01000ActiveInactive(PMM01000DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task PMM01000ActiveInactiveAsync(PMM01000DTO poParam)
        {
            var loEx = new R_Exception();
            PMM01000DTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<PMM01000DTO, PMM01000DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01000.PMM01000ActiveInactive),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        public PMM01003DTO PMM01000CopyNewCharges(PMM01003DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task PMM01000CopyNewChargesAsync(PMM01003DTO poParam)
        {
            var loEx = new R_Exception();
            PMM01003DTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<PMM01003DTO, PMM01003DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01000.PMM01000CopyNewCharges),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        public PMM01000Record<PMM01000BeforeDeleteDTO> ValidateBeforeDelete(PMM01000DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<PMM01000BeforeDeleteDTO> ValidateBeforeDeleteAsync(PMM01000DTO poParam)
        {
            var loEx = new R_Exception();
            PMM01000BeforeDeleteDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTmpRtn = await R_HTTPClientWrapper.R_APIRequestObject<PMM01000Record<PMM01000BeforeDeleteDTO>, PMM01000DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01000.ValidateBeforeDelete),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loRtn = loTmpRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
    }
}
