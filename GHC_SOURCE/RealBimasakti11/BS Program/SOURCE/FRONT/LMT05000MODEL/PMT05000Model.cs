using PMT05000COMMON;
using PMT05000COMMON.DTO_s;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT05000MODEL
{
    public class PMT05000Model : R_BusinessObjectServiceClientBase<AgreementChrgDiscParamDTO>, IPMM05000Init
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_CHECKPOINT_NAME = "api/PMT05000";
        private const string DEFAULT_MODULE = "PM";
        public PMT05000Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }

        public async Task<List<PropertyDTO>> GetPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<PropertyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM05000Init.GetPropertyList),
                    DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;

        }

        public async Task<List<GSB_CodeInfoDTO>> GetGSBCodeInfoAsync()
        {
            var loEx = new R_Exception();
            List<GSB_CodeInfoDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSB_CodeInfoDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM05000Init.GetGSBCodeInfoList),
                    DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;

        }
        
        public async Task<List<GSPeriodDT_DTO>> GetGSPeriodDTAsync()
        {
            var loEx = new R_Exception();
            List<GSPeriodDT_DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSPeriodDT_DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM05000Init.GetGSPeriodDTList),
                    DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;

        }

        public IAsyncEnumerable<GSB_CodeInfoDTO> GetGSBCodeInfoList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<GSPeriodDT_DTO> GetGSPeriodDTList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }
    }
}
