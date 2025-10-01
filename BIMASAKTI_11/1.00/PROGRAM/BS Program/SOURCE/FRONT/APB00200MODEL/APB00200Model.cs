using APB00200COMMON;
using APB00200COMMON.DTO_s;
using APB00200COMMON.DTO_s.Helper;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APB00200MODEL
{
    public class APB00200Model : R_BusinessObjectServiceClientBase<ClosePeriodDTO>, IAPB00200
    {
        //variable & constructors
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlAP";
        private const string DEFAULT_ENDPOINT = "api/APB00200";
        private const string DEFAULT_MODULE = "AP";
        public APB00200Model(
            string pcHttpClientName = APB00200Model.DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        //real methods
        public async Task<RecordResultAPI<ClosePeriodDTO>> GetClosePeriodAsync(ClosePeriodParam poParam)
        {
            var loEx = new R_Exception();
            RecordResultAPI<ClosePeriodDTO> loResult = new RecordResultAPI<ClosePeriodDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<RecordResultAPI<ClosePeriodDTO>, ClosePeriodParam>(
                    _RequestServiceEndPoint,
                    nameof(IAPB00200.GetRecord_ClosePeriod),
                    poParam,
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
        public async Task<List<ErrorCloseAPProcessDTO>> GetList_ErrorProcessCloseAPPeriodAsync(CloseAPProcessParam poParam)
        {
            var loEx = new R_Exception();
            List<ErrorCloseAPProcessDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<ErrorCloseAPProcessDTO, CloseAPProcessParam>(
                    _RequestServiceEndPoint,
                    nameof(IAPB00200.GetList_ErrorProcessCloseAPPeriod),
                    poParam,
                    DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<RecordResultAPI<CloseAPProcessResultDTO>> ProcessCloseAPPeriodAsync(CloseAPProcessParam poParam)
        {
            var loEx = new R_Exception();
            RecordResultAPI<CloseAPProcessResultDTO> loResult = new RecordResultAPI<CloseAPProcessResultDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<RecordResultAPI<CloseAPProcessResultDTO>, CloseAPProcessParam>(
                    _RequestServiceEndPoint,
                    nameof(IAPB00200.ProcessCloseAPPeriod),
                    poParam,
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

        //implement only
        public RecordResultAPI<ClosePeriodDTO> GetRecord_ClosePeriod(ClosePeriodParam poParam)
        {
            throw new NotImplementedException();
        }
        public RecordResultAPI<CloseAPProcessResultDTO> ProcessCloseAPPeriod(CloseAPProcessParam poParam)
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<ErrorCloseAPProcessDTO> GetList_ErrorProcessCloseAPPeriod(CloseAPProcessParam poParam)
        {
            throw new NotImplementedException();
        }
    }
}
