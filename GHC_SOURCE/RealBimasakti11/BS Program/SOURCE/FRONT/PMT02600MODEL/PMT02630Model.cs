using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PMT02600COMMON.DTOs.PMT02630;
using PMT02600COMMON;
using PMT02600COMMON.DTOs;

namespace PMT02600MODEL
{
    public class PMT02630Model : R_BusinessObjectServiceClientBase<PMT02630DTO>, IPMT02630
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT02630";
        private const string DEFAULT_MODULE = "PM";

        public PMT02630Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<PMT02630DTO>> GetAgreementDepositStreamAsync(PMT02630DTO poEntity)
        {
            var loEx = new R_Exception();
            List<PMT02630DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT02630_AGREEMENT_DEPOSIT_STREAMING_CONTEXT, new PMT02630DTO()
                {
                    CPROPERTY_ID = poEntity.CPROPERTY_ID,
                    CDEPT_CODE = poEntity.CDEPT_CODE,
                    CREF_NO = poEntity.CREF_NO,
                    CCHARGE_MODE = poEntity.CCHARGE_MODE,
                    CUNIT_ID = string.IsNullOrWhiteSpace(poEntity.CUNIT_ID) ? "" : poEntity.CUNIT_ID,
                    CFLOOR_ID = string.IsNullOrWhiteSpace(poEntity.CFLOOR_ID) ? "" : poEntity.CFLOOR_ID,
                    CBUILDING_ID = string.IsNullOrWhiteSpace(poEntity.CBUILDING_ID) ? "" : poEntity.CBUILDING_ID,
                    CTRANS_CODE = poEntity.CTRANS_CODE
                });

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02630DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02630.GetAgreementDepositStream),
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

        #region Not Implment
        public IAsyncEnumerable<PMT02630DTO> GetAgreementDepositStream()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
