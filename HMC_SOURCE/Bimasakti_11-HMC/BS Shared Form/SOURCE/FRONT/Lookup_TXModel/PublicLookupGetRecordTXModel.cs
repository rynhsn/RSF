using Lookup_TXCOMMON;
using R_BusinessObjectFront;
using System;
using Lookup_TXCOMMON.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;
using Lookup_TXCOMMON.DTOs.TXL00100;
using Lookup_TXCOMMON.Interface;
using Lookup_TXCOMMON.DTOs.Utilities;
using Lookup_TXCOMMON.DTOs.TXL00200;

namespace Lookup_TXModel
{
    public class PublicLookupGetRecordTXModel : R_BusinessObjectServiceClientBase<TXL00100DTO>, IGetRecordPublicLookupTX
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlTX";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PublicLookupTXGetRecord";
        private const string DEFAULT_MODULE = "TX";

        public PublicLookupGetRecordTXModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }


        #region TXL00100GetRecord

        public async Task<TXL00100DTO> TXL00100BranchLookUpAsync(TXL00100ParameterGetRecordDTO poParam)
        {

            var loEx = new R_Exception();
            TXL00100DTO? loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<TXLGenericRecord<TXL00100DTO>, TXL00100ParameterGetRecordDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGetRecordPublicLookupTX.TXL00100BranchLookUp),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult!;
        }

        #endregion


        #region TXL00200GetRecord

        public async Task<TXL00200DTO> TXL00200TaxNoLookUpAsync(TXL00200ParameterGetRecordDTO poParam)
        {

            var loEx = new R_Exception();
            TXL00200DTO? loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<TXLGenericRecord<TXL00200DTO>, TXL00200ParameterGetRecordDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGetRecordPublicLookupTX.TXL00200TaxNoLookUp),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult!;
        }

        #endregion


        #region Not Used!

        public TXLGenericRecord<TXL00100DTO> TXL00100BranchLookUp(TXL00100ParameterGetRecordDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public TXLGenericRecord<TXL00200DTO> TXL00200TaxNoLookUp(TXL00200ParameterGetRecordDTO poParameter)
        {
            throw new NotImplementedException();
        }


        #endregion

    }
}
