using PMB01600Common;
using PMB01600Common.DTOs;
using PMB01600Common.Params;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMB01600Model
{
    public class PMB01600Model : R_BusinessObjectServiceClientBase<PMB01600BillingStatementHeaderDTO>, IPMB01600
    {

        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMB01600";
        private const string DEFAULT_MODULE = "pm";

        public PMB01600Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName,
            plSendWithContext,
            plSendWithToken)
        {
        }

        public IAsyncEnumerable<PMB01600BillingStatementDetailDTO> PMB01600GetBillingStatementDetailListStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMB01600BillingStatementHeaderDTO> PMB01600GetBillingStatementHeaderListStream()
        {
            throw new NotImplementedException();
        }

        public async Task<PMB01600ListDTO<PMB01600PropertyDTO>> PMB01600GetPropertyList()
        {
            var loEx = new R_Exception();
            var loResult = new PMB01600ListDTO<PMB01600PropertyDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMB01600ListDTO<PMB01600PropertyDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMB01600.PMB01600GetPropertyList),
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

        public async Task<PMB01600SingleDTO<PMB01600SystemParamDTO>> PMB01600GetSystemParam(PMB01600SystemParamParam poParam)
        {
            var loEx = new R_Exception();
            var loResult = new PMB01600SingleDTO<PMB01600SystemParamDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMB01600SingleDTO<PMB01600SystemParamDTO>, PMB01600SystemParamParam>(
                    _RequestServiceEndPoint,
                    nameof(IPMB01600.PMB01600GetSystemParam),
                    poParam,
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

        public async Task<PMB01600ListDTO<PMB01600PeriodDTO>> PMB01600GetPeriodList(PMB01600PeriodParam poParam)
        {
            var loEx = new R_Exception();
            var loResult = new PMB01600ListDTO<PMB01600PeriodDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMB01600ListDTO<PMB01600PeriodDTO>, PMB01600PeriodParam>(
                    _RequestServiceEndPoint,
                    nameof(IPMB01600.PMB01600GetPeriodList),
                    poParam,
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

        public async Task<PMB01600SingleDTO<PMB01600YearRangeDTO>> PMB01600GetYearRange()
        {
            var loEx = new R_Exception();
            var loResult = new PMB01600SingleDTO<PMB01600YearRangeDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMB01600SingleDTO<PMB01600YearRangeDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMB01600.PMB01600GetYearRange),
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

        public async Task<List<T>> GetListStreamAsync<T>(string pcNameOf)
        {
            var loEx = new R_Exception();
            List<T> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<T>(
                    _RequestServiceEndPoint,
                    pcNameOf,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken,
                    null);
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
