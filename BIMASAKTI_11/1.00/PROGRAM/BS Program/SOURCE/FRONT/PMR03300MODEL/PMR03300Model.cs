using PMR03300COMMON;
using PMR03300COMMON.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMR03300MODEL
{
    public class PMR03300Model : R_BusinessObjectServiceClientBase<PMR03300GetReportDTO>, IPMR03300
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMR03300";
        private const string DEFAULT_MODULE = "PM";

        public PMR03300Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<PMR03300SingleDTO<PMR03300GetCompanyInfoDTO>> GetCompanyInfo()
        {
            var loEx = new R_Exception();
            var loResult = new PMR03300SingleDTO<PMR03300GetCompanyInfoDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMR03300SingleDTO<PMR03300GetCompanyInfoDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMR03300.GetCompanyInfo),
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

        public async Task<PMR03300ListDTO<PMR03300GetPeriodDtListDTO>> GetPeriodDtList()
        {
            var loEx = new R_Exception();
            var loResult = new PMR03300ListDTO<PMR03300GetPeriodDtListDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMR03300ListDTO<PMR03300GetPeriodDtListDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMR03300.GetPeriodDtList),
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

        public async Task<PMR03300SingleDTO<PMR03300GetPeriodeYearRangeDTO>> GetPeriodeYearRange()
        {
            var loEx = new R_Exception();
            var loResult = new PMR03300SingleDTO<PMR03300GetPeriodeYearRangeDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMR03300SingleDTO<PMR03300GetPeriodeYearRangeDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMR03300.GetPeriodeYearRange),
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

        public async Task<PMR03300SingleDTO<PMR03300GetSystemParamDTO>> GetSystemParam()
        {
            var loEx = new R_Exception();
            var loResult = new PMR03300SingleDTO<PMR03300GetSystemParamDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMR03300SingleDTO<PMR03300GetSystemParamDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMR03300.GetSystemParam),
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


        public async Task<PMR03300ListDTO<PMR03300PropertyDTO>> PMR03300GetPropertyList()
        {
            var loEx = new R_Exception();
            var loResult = new PMR03300ListDTO<PMR03300PropertyDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMR03300ListDTO<PMR03300PropertyDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMR03300.PMR03300GetPropertyList),
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
