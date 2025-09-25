using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GST00500Common;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GST00500Model
{
    public class GST00500InboxModel : R_BusinessObjectServiceClientBase<GST00500DTO>, IGST00500
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrl";
        private const string DEFAULT_ENDPOINT = "api/GST00500Inbox";
        private const string DEFAULT_MODULE = "GS";

        public GST00500InboxModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<GST00500DTO> ApprovalInboxListStream()
        {
            throw new NotImplementedException();
        }

        public GST00500UserNameDTO GetUserName()
        {
            throw new NotImplementedException();
        }

        public GST00500RejectListDTO ReasonRejectList()
        {
            throw new NotImplementedException();
        }

        //public IAsyncEnumerable<GST00500ApprovalTransactionDTO> GetError(GST00500ParameterDBDTO loParam)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<List<GST00500DTO>> GetInboxListAsyncModel()
        {
            var loEx = new R_Exception();
            List<GST00500DTO> loResult = new List<GST00500DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GST00500DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGST00500.ApprovalInboxListStream),
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

        public async Task<GST00500UserNameDTO> GetUserNameAsyncModel()
        {
            var loEx = new R_Exception();
            GST00500UserNameDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GST00500UserNameDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGST00500.GetUserName),
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

        public async Task<GST00500RejectListDTO> GetReasonRejectListAsyncModel()
        {
            var loEx = new R_Exception();
            GST00500RejectListDTO loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GST00500RejectListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGST00500.ReasonRejectList),
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

        //public async Task<List<GST00500ApprovalTransactionDTO>> GetErroListAsync(GST00500ParameterDBDTO loParam)
        //{
        //    var loEx = new R_Exception();
        //    List<GST00500ApprovalTransactionDTO> loReturn = new List<GST00500ApprovalTransactionDTO>();
        //    try
        //    {
        //        R_HTTPClientWrapper.httpClientName = _HttpClientName;
        //        loReturn = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GST00500ApprovalTransactionDTO, GST00500ParameterDBDTO>(
        //            _RequestServiceEndPoint,
        //            nameof(IGST00500.GetError),
        //            loParam,
        //            DEFAULT_MODULE,
        //            _SendWithContext,
        //            _SendWithToken);
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    EndBlock:
        //    loEx.ThrowExceptionIfErrors();
        //    return loReturn;
        //}
    }
}
