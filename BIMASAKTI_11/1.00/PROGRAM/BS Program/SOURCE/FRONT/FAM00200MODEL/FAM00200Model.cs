using FAM00200Common;
using FAM00200Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace FAM00200Model
{
    public class FAM00200Model : R_BusinessObjectServiceClientBase<FAM00200DTO>, IFAM00200
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlFA";
        private const string DEFAULT_ENDPOINT = "api/FAM00200";
        private const string DEFAULT_MODULE = "FA";

        public FAM00200Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<FAM00200DTO>> GetListTaxTypeAsync()
        {
            var loEx = new R_Exception();
            List<FAM00200DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAM00200DTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAM00200.GetListTaxType),
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

        public async Task<FAM00200DTO> GetTaxTypeAsync(FAM00200DTO poEntity)
        {
            var loEx = new R_Exception();
            FAM00200DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<FAM00200SingleResult<FAM00200DTO>, FAM00200DTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAM00200.GetTaxType),
                    poEntity,
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

            return loResult;
        }
        public async Task<FAM00200DTO> SaveTaxTypeAsync(FAM00200SaveParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            FAM00200DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<FAM00200SingleResult<FAM00200DTO>, FAM00200SaveParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAM00200.SaveTaxType),
                    poEntity,
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

            return loResult;
        }

        #region Not Implement
        public IAsyncEnumerable<FAM00200GSBCodeDTO> GetListGSBCode()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<FAM00200DTO> GetListTaxType()
        {
            throw new NotImplementedException();
        }
        Task<FAM00200SingleResult<FAM00200DTO>> IFAM00200.GetTaxType(FAM00200DTO poEntity)
        {
            throw new NotImplementedException();
        }

        Task<FAM00200SingleResult<FAM00200DTO>> IFAM00200.SaveTaxType(FAM00200SaveParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
