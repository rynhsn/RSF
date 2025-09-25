using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMT02500Common.Context;
using PMT02500Common.DTO._7._Document;
using PMT02500Common.Interface;
using PMT02500Common.Utilities;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace PMT02500Model
{
    public class PMT02500DocumentModel : R_BusinessObjectServiceClientBase<PMT02500DocumentDetailDTO>, IPMT02500Document
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT02500Document";
        private const string DEFAULT_MODULE = "PM";

        public PMT02500DocumentModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<PMT02500DocumentHeaderDTO> GetDocumentHeaderAsync(PMT02500GetHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loResult = new PMT02500DocumentHeaderDTO();
            PMT02500GetHeaderParameterDTO? loParam;


            try
            {
                if (!string.IsNullOrEmpty(poParameter.CPROPERTY_ID))
                {
                    loParam = new PMT02500GetHeaderParameterDTO()
                    {
                        CPROPERTY_ID = poParameter.CPROPERTY_ID,
                        CDEPT_CODE = poParameter.CDEPT_CODE,
                        CREF_NO = poParameter.CREF_NO,
                        CTRANS_CODE = poParameter.CTRANS_CODE
                    };

                    R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                    loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT02500DocumentHeaderDTO, PMT02500GetHeaderParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT02500Document.GetDocumentHeader),
                        loParam,
                        DEFAULT_MODULE,
                        _SendWithContext,
                        _SendWithToken
                    );
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<List<PMT02500DocumentListDTO>> GetDocumentListAsync(PMT02500GetHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<PMT02500DocumentListDTO>? loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(PMT02500GetHeaderParameterContextConstantDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMT02500GetHeaderParameterContextConstantDTO.CDEPT_CODE, poParameter.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(PMT02500GetHeaderParameterContextConstantDTO.CREF_NO, poParameter.CREF_NO);
                R_FrontContext.R_SetStreamingContext(PMT02500GetHeaderParameterContextConstantDTO.CTRANS_CODE, poParameter.CTRANS_CODE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02500DocumentListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02500Document.GetDocumentList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

#pragma warning disable CS8603 // Possible null reference return.
            return loResult;
#pragma warning restore CS8603 // Possible null reference return.
        }

        #region Not Used!
        public PMT02500DocumentHeaderDTO GetDocumentHeader(PMT02500GetHeaderParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02500DocumentListDTO> GetDocumentList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}