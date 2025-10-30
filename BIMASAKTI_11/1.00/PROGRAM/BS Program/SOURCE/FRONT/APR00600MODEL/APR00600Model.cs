using APR00600COMMON;
using APR00600COMMON.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APR00600MODEL
{
    public class APR00600Model : R_BusinessObjectServiceClientBase<APR00600GetReportDTO>, IAPR00600
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlAP";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/APR00600";
        private const string DEFAULT_MODULE = "AP";

        public APR00600Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<APR00600SingleDTO<APR00600GetCompanyInfoDTO>> GetCompanyInfo()
        {
            var loEx = new R_Exception();
            var loResult = new APR00600SingleDTO<APR00600GetCompanyInfoDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<APR00600SingleDTO<APR00600GetCompanyInfoDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IAPR00600.GetCompanyInfo),
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

        public async Task<APR00600ListDTO<APR00600GetPeriodDtListDTO>> GetPeriodDtList()
        {
            var loEx = new R_Exception();
            var loResult = new APR00600ListDTO<APR00600GetPeriodDtListDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<APR00600ListDTO<APR00600GetPeriodDtListDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IAPR00600.GetPeriodDtList),
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

        public async Task<APR00600SingleDTO<APR00600GetPeriodeYearRangeDTO>> GetPeriodeYearRange()
        {
            var loEx = new R_Exception();
            var loResult = new APR00600SingleDTO<APR00600GetPeriodeYearRangeDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<APR00600SingleDTO<APR00600GetPeriodeYearRangeDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IAPR00600.GetPeriodeYearRange),
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

        public async Task<APR00600SingleDTO<APR00600GetSystemParamDTO>> GetSystemParam()
        {
            var loEx = new R_Exception();
            var loResult = new APR00600SingleDTO<APR00600GetSystemParamDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<APR00600SingleDTO<APR00600GetSystemParamDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IAPR00600.GetSystemParam),
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


        public async Task<APR00600ListDTO<APR00600PropertyDTO>> APR00600GetPropertyList()
        {
            var loEx = new R_Exception();
            var loResult = new APR00600ListDTO<APR00600PropertyDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<APR00600ListDTO<APR00600PropertyDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IAPR00600.APR00600GetPropertyList),
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
    }
}
