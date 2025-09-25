using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PMT02600COMMON.DTOs.PMT02620;
using PMT02600COMMON;
using PMT02600COMMON.DTOs;

namespace PMT02600MODEL
{
    public class PMT02620UnitModel : R_BusinessObjectServiceClientBase<PMT02620UnitDTO>, IPMT02620Unit
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT02620Unit";
        private const string DEFAULT_MODULE = "PM";

        public PMT02620UnitModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<PMT02620UnitDTO>> GetAgreementUnitListStreamAsync(PMT02620UnitDTO poEntity)
        {
            var loEx = new R_Exception();
            List<PMT02620UnitDTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT02620_UNIT_GET_AGREEMENT_UNIT_LIST_STREAMING_CONTEXT, new PMT02620UnitDTO()
                {
                    CPROPERTY_ID = poEntity.CPROPERTY_ID,
                    CDEPT_CODE = poEntity.CDEPT_CODE,
                    CREF_NO = poEntity.CREF_NO,
                    CTRANS_CODE = poEntity.CTRANS_CODE
                });

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02620UnitDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02620Unit.GetAgreementUnitListStream),
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
        public IAsyncEnumerable<PMT02620UnitDTO> GetAgreementUnitListStream()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
