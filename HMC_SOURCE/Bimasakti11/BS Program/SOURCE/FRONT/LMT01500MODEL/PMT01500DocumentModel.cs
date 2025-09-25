using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMT01500Common.Context;
using PMT01500Common.DTO._7._Document;
using PMT01500Common.Interface;
using PMT01500Common.Utilities;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace PMT01500Model
{
    public class PMT01500DocumentModel : R_BusinessObjectServiceClientBase<PMT01500DocumentDetailDTO>, IPMT01500Document
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT01500Document";
        private const string DEFAULT_MODULE = "PM";

        public PMT01500DocumentModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<PMT01500DocumentHeaderDTO> GetDocumentHeaderAsync(PMT01500GetHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loResult = new PMT01500DocumentHeaderDTO();
            PMT01500GetHeaderParameterDTO? loParam;


            try
            {
                if (!string.IsNullOrEmpty(poParameter.CPROPERTY_ID))
                {
                    loParam = new PMT01500GetHeaderParameterDTO()
                    {
                        CPROPERTY_ID = poParameter.CPROPERTY_ID,
                        CDEPT_CODE = poParameter.CDEPT_CODE,
                        CREF_NO = poParameter.CREF_NO,
                        CTRANS_CODE = poParameter.CTRANS_CODE
                    };

                    R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                    loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01500DocumentHeaderDTO, PMT01500GetHeaderParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT01500Document.GetDocumentHeader),
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

        public async Task<List<PMT01500DocumentListDTO>> GetDocumentListAsync(PMT01500GetHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<PMT01500DocumentListDTO>? loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(PMT01500GetHeaderParameterContextConstantDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMT01500GetHeaderParameterContextConstantDTO.CDEPT_CODE, poParameter.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(PMT01500GetHeaderParameterContextConstantDTO.CREF_NO, poParameter.CREF_NO);
                R_FrontContext.R_SetStreamingContext(PMT01500GetHeaderParameterContextConstantDTO.CTRANS_CODE, poParameter.CTRANS_CODE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01500DocumentListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01500Document.GetDocumentList),
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
        public PMT01500DocumentHeaderDTO GetDocumentHeader(PMT01500GetHeaderParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01500DocumentListDTO> GetDocumentList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}