using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Lookup_TXCOMMON;
using Lookup_TXCOMMON.DTOs;
using Lookup_TXCOMMON.DTOs.TXL00100;
using Lookup_TXCOMMON.DTOs.TXL00200;
using Lookup_TXCOMMON.DTOs.Utilities;
using Lookup_TXCOMMON.Interface;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace Lookup_TXModel
{
    public class PublicLookupTXModel : R_BusinessObjectServiceClientBase<TXL00100DTO>, IPublicLookupTX
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlTX";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PublicLookupTX";
        private const string DEFAULT_MODULE = "TX";

        public PublicLookupTXModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }


        #region TXL00100 

        public async Task<TXLGenericList<TXL00100DTO>> TXL00100BranchLookUpAsync()
        {
            var loEx = new R_Exception();
            TXLGenericList<TXL00100DTO> loResult = new TXLGenericList<TXL00100DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<TXL00100DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupTX.TXL00100BranchLookUp),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        #endregion

        #region TXL00200 

        public async Task<TXLGenericList<TXL00200DTO>> TXL00200TaxNoLookUpAsync(TXL00200ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            TXLGenericList<TXL00200DTO> loResult = new TXLGenericList<TXL00200DTO>();
            try
            {
                R_FrontContext.R_SetStreamingContext(TXL00200ContextDTO.CBRANCH_CODE, poParameter.CBRANCH_CODE);
                R_FrontContext.R_SetStreamingContext(TXL00200ContextDTO.LREUSE_FLAG, poParameter.LREUSE_FLAG);
                R_FrontContext.R_SetStreamingContext(TXL00200ContextDTO.CTENANT_ID, poParameter.CTENANT_ID);
                R_FrontContext.R_SetStreamingContext(TXL00200ContextDTO.CTAX_ID, poParameter.CTAX_ID);
                R_FrontContext.R_SetStreamingContext(TXL00200ContextDTO.LREPLACEMENT, poParameter.LREPLACEMENT);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<TXL00200DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupTX.TXL00200TaxNoLookUp),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        #endregion

        #region Not Used!

        public IAsyncEnumerable<TXL00100DTO> TXL00100BranchLookUp()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<TXL00200DTO> TXL00200TaxNoLookUp()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}