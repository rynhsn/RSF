using Lookup_FACommon;
using Lookup_FACommon.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lookup_FAModel
{
    public class PublicLookupFAModel : R_BusinessObjectServiceClientBase<FAL00100DTO>, IPublicLookupFA
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlFA";
        private const string DEFAULT_ENDPOINT = "api/PublicLookupFA";
        private const string DEFAULT_MODULE = "FA";

        public PublicLookupFAModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region FAL00100
        public IAsyncEnumerable<FAL00100DTO> FAL00100TaxTypeLookup()
        {
            throw new NotImplementedException();
        }

        public async Task<List<FAL00100DTO>> FAL00100TaxTypeLookupAsync()
        {
            var loEx = new R_Exception();
            List<FAL00100DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAL00100DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupFA.FAL00100TaxTypeLookup),
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
        #endregion

        #region FAL00200
        public IAsyncEnumerable<FAL00200DTO> FAL00200TaxCategoryLookup()
        {
            throw new NotImplementedException();
        }

        public async Task<List<FAL00200DTO>> FAL00200TaxCategoryLookupAsync()
        {
            var loEx = new R_Exception();
            List<FAL00200DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAL00200DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupFA.FAL00200TaxCategoryLookup),
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
        #endregion
    }
}