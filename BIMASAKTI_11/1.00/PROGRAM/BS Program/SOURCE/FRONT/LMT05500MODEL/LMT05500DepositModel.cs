using PMT05500COMMON.DTO;
using PMT05500COMMON.Interface;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMT05500Model
{
    public class LMT05500DepositModel : R_BusinessObjectServiceClientBase<LMT05500DepositInfoDTO>, ILMT05500Deposit
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/LMT05500Deposit";
        private const string DEFAULT_MODULE = "PM";
        public LMT05500DepositModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        #region implementsLibrary
        public LMT05500DepositHeaderDTO DepositHeader(LMT05500DBParameter poParam)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMT05500DepositListDTO> DepositListStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMT05500DepositDetailListDTO> DepositDetailListStream()
        {
            throw new NotImplementedException();
        }
        #endregion

        public async Task<LMT05500DepositHeaderDTO> GetDepositHeaderAsyncModel(LMT05500DBParameter poParam)
        {
            var loEx = new R_Exception();
            LMT05500DepositHeaderDTO loResult = new LMT05500DepositHeaderDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<LMT05500DepositHeaderDTO,LMT05500DBParameter > (
                    _RequestServiceEndPoint,
                    nameof(ILMT05500Deposit.DepositHeader),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
               // loResult = temp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<LMT05500GenericList<LMT05500DepositListDTO>> DepositListStreamAsyncModel()
        {
            var loEx = new R_Exception();
            LMT05500GenericList<LMT05500DepositListDTO> loResult = new LMT05500GenericList<LMT05500DepositListDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMT05500DepositListDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMT05500Deposit.DepositListStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = temp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<LMT05500GenericList<LMT05500DepositDetailListDTO>> DepositDetailListAsyncModel()
        {
            var loEx = new R_Exception();
            LMT05500GenericList<LMT05500DepositDetailListDTO> loResult = new LMT05500GenericList<LMT05500DepositDetailListDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMT05500DepositDetailListDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMT05500Deposit.DepositDetailListStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = temp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
    }
}
