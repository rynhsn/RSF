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
    public class PMT05001Model : R_BusinessObjectServiceClientBase<AgreementChrgDiscParamDTO>, IPMM05000
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_CHECKPOINT_NAME = "api/PMT05001";
        private const string DEFAULT_MODULE = "PM";
        public PMT05001Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
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
        public async Task ProcessAgreementChargeDiscountAsync(AgreementChrgDiscParamDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loResult = await R_HTTPClientWrapper.R_APIRequestObject<AgreementChrgDiscResultDTO, AgreementChrgDiscParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM05000.ProcessAgreementChargeDiscount),
                    poParam,
                    DEFAULT_MODULE
                    , _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task<List<AgreementChrgDiscDetailDTO>> GetAgreementChargesDiscountListAsync()
        {
            var loEx = new R_Exception();
            List<AgreementChrgDiscDetailDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<AgreementChrgDiscDetailDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM05000.GetAgreementChargesDiscountList),
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

        public IAsyncEnumerable<AgreementChrgDiscDetailDTO> GetAgreementChargesDiscountList()
        {
            throw new NotImplementedException();
        }

        public AgreementChrgDiscResultDTO ProcessAgreementChargeDiscount(AgreementChrgDiscParamDTO poParam)
        {
            throw new NotImplementedException();
        }
    }
}
