using System;
using System.Threading.Tasks;
using PMR03000Common;
using PMR03000Common.DTOs;
using PMR03000Common.Params;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace PMR03000Model
{
    public class PMR03000Model : R_BusinessObjectServiceClientBase<PMR03000ReportParamDTO>, IPMR03000
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMR03000";
        private const string DEFAULT_MODULE = "pm";

        public PMR03000Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<PMR03000ListDTO<PMR03000PropertyDTO>> PMR03000GetPropertyList()
        {
            var loEx = new R_Exception();
            var loResult = new PMR03000ListDTO<PMR03000PropertyDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMR03000ListDTO<PMR03000PropertyDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMR03000.PMR03000GetPropertyList),
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

        public async Task<PMR03000ListDTO<PMR03000ReportTemplateDTO>> PMR03000GetReportTemplateList(PMR03000ReportTemplateParam poParam)
        {
            var loEx = new R_Exception();
            var loResult = new PMR03000ListDTO<PMR03000ReportTemplateDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper
                    .R_APIRequestObject<PMR03000ListDTO<PMR03000ReportTemplateDTO>, PMR03000ReportTemplateParam>(
                        _RequestServiceEndPoint,
                        nameof(IPMR03000.PMR03000GetReportTemplateList),
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

        public async Task<PMR03000ListDTO<PMR03000PeriodDTO>> PMR03000GetPeriodList(PMR03000PeriodParam poParam)
        {
            var loEx = new R_Exception();
            var loResult = new PMR03000ListDTO<PMR03000PeriodDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper
                    .R_APIRequestObject<PMR03000ListDTO<PMR03000PeriodDTO>, PMR03000PeriodParam>(
                        _RequestServiceEndPoint,
                        nameof(IPMR03000.PMR03000GetPeriodList),
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
    }
}