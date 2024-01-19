using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GSM05000Common;
using GSM05000Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;

namespace GSM05000Model
{
    public class GSM05000Model : R_BusinessObjectServiceClientBase<GSM05000DTO>, IGSM05000
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM05000";
        private const string DEFAULT_MODULE = "gs";

        public GSM05000Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<GSM05000GridDTO> GetTransactionCodeListStream()
        {
            throw new NotImplementedException();
        }

        public GSM05000ListDTO<GSM05000DelimiterDTO> GetDelimiterList()
        {
            throw new NotImplementedException();
        }

        public GSM05000ExistDTO CheckExistData(GSM05000TrxCodeParamsDTO poParams)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GSM05000GridDTO>> GetAllStreamAsync()
        {
            var loEx = new R_Exception();
            List<GSM05000GridDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM05000GridDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05000.GetTransactionCodeListStream),
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

        public async Task<GSM05000ListDTO<GSM05000DelimiterDTO>> GetDelimiterAsync()
        {
            var loEx = new R_Exception();
            GSM05000ListDTO<GSM05000DelimiterDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM05000ListDTO<GSM05000DelimiterDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05000.GetDelimiterList),
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

        public async Task<GSM05000ExistDTO> CheckExistDataAsync(GSM05000TrxCodeParamsDTO poParams)
        {
            var loEx = new R_Exception();
            GSM05000ExistDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM05000ExistDTO, GSM05000TrxCodeParamsDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05000.CheckExistData),
                    poParams,
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
    }
}