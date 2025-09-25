using CBT02300COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBT02300MODEL
{
    public class CBT02300BankInChequeModel : R_BusinessObjectServiceClientBase<CBT02300ChequeInfoDTO>, ICBT02300BankInCheque
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlCB";
        private const string DEFAULT_ENDPOINT = "api/CBT02300BankInCheque";
        private const string DEFAULT_MODULE = "CB";
        public CBT02300BankInChequeModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }
        #region Implements Library
        public CBT02300ChequeInfoDTO BankInChequeInfo(CBT02300DBParamDetailDTO paramDetailDTO)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<CBT02300BankInChequeDTO> BankInChequeListStream()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Implements
        public async Task<CBT02300GenericList<CBT02300BankInChequeDTO>> GetbankInChequeStreamAsyncModel()
        {
            var loEx = new R_Exception();
            CBT02300GenericList<CBT02300BankInChequeDTO> loResult = new CBT02300GenericList<CBT02300BankInChequeDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CBT02300BankInChequeDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT02300BankInCheque.BankInChequeListStream),
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

        public async Task<CBT02300ChequeInfoDTO> GetBankInChequeInfoModel(CBT02300DBParamDetailDTO paramDetailDTO)
        {
            var loEx = new R_Exception();
            CBT02300ChequeInfoDTO loResult = new CBT02300ChequeInfoDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestObject<CBT02300ChequeInfoDTO, CBT02300DBParamDetailDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT02300BankInCheque.BankInChequeInfo),
                    paramDetailDTO,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult = temp;
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
