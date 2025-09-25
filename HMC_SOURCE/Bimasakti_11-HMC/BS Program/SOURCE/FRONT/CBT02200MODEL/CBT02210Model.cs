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
    internal class CBT02210Model : R_BusinessObjectServiceClientBase<CBT02210ParameterDTO>, ICBT02210
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlCB";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/CBT02210";
        private const string DEFAULT_MODULE = "CB";

        public CBT02210Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public RefreshCurrencyRateResultDTO RefreshCurrencyRate(RefreshCurrencyRateParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task<RefreshCurrencyRateResultDTO> RefreshCurrencyRateAsync(RefreshCurrencyRateParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            RefreshCurrencyRateResultDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestObject<RefreshCurrencyRateResultDTO, RefreshCurrencyRateParameterDTO> (
                    _RequestServiceEndPoint,
                    nameof(ICBT02210.RefreshCurrencyRate),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public UpdateStatusResultDTO UpdateStatus(UpdateStatusParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task UpdateStatusAsync(UpdateStatusParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            UpdateStatusResultDTO loRtn = new UpdateStatusResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<UpdateStatusResultDTO, UpdateStatusParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT02210.UpdateStatus),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
    }
}
