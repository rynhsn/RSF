using CBT02200COMMON.DTO.CBT02200;
using CBT02200COMMON;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using CBT02200COMMON.DTO.CBT02210;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;

namespace CBT02200MODEL
{
    public class CBT02210DetailModel : R_BusinessObjectServiceClientBase<CBT02210DetailParameterDTO>, ICBT02210Detail
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlCB";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/CBT02210Detail";
        private const string DEFAULT_MODULE = "CB";

        public CBT02210DetailModel(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<GetCenterListDTO> GetCenterList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetCenterListResultDTO> GetCenterListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GetCenterListDTO> loResult = null;
            GetCenterListResultDTO loRtn = new GetCenterListResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetCenterListDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT02210Detail.GetCenterList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public IAsyncEnumerable<CBT02210DetailDTO> GetChequeEntryDetailList()
        {
            throw new NotImplementedException();
        }
        public async Task<CBT02210DetailResultDTO> GetChequeEntryDetailListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<CBT02210DetailDTO> loResult = null;
            CBT02210DetailResultDTO loRtn = new CBT02210DetailResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CBT02210DetailDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT02210Detail.GetChequeEntryDetailList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}
