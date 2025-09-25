using PMB04000COMMON.DTO.DTOs;
using PMB04000COMMON.DTO.Utilities;
using PMB04000COMMON.Interface;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMB04000MODEL
{
    public class PMB04000Model :R_BusinessObjectServiceClientBase<PMB04000DTO>, IPMB04000
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMB04000";
        private const string DEFAULT_MODULE = "PM";

        public PMB04000Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                pcModule,
                plSendWithContext,
                plSendWithToken)
        {
        }
        public async Task<PMB04000GenericList<PMB04000DTO>> GetInvReceiptListAsync()
        {
            var loEx = new R_Exception();
            var loResult = new PMB04000GenericList<PMB04000DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMB04000DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMB04000.GetInvReceipt_List),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<PeriodYearDTO> GetPeriodYearRangeAsync()
        {
            var loEx = new R_Exception();
            var loResult = new PeriodYearDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PeriodYearDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMB04000.GetPeriodYearRange),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<PMB04000GenericList<TemplateDTO>> GetTemplateListAsync()
        {
            var loEx = new R_Exception();
            var loResult = new PMB04000GenericList<TemplateDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<TemplateDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMB04000.GetTemplateList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #region just Implement
        public IAsyncEnumerable<PMB04000DTO> GetInvReceipt_List()
        {
            throw new NotImplementedException();
        }

        public PeriodYearDTO GetPeriodYearRange()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<TemplateDTO> GetTemplateList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
