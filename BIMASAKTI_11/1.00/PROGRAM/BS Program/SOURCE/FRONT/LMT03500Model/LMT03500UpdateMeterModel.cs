using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LMT03500Common;
using LMT03500Common.DTOs;
using LMT03500Common.Params;
using R_APIClient;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace LMT03500Model
{
    public class LMT03500UpdateMeterModel : R_BusinessObjectServiceClientBase<LMT03500UtilityMeterDTO>,
        ILMT03500UpdateMeter
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMT03500UpdateMeter";
        private const string DEFAULT_MODULE = "lm";

        public LMT03500UpdateMeterModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName,
            plSendWithContext,
            plSendWithToken)
        {
        }

        public IAsyncEnumerable<LMT03500UtilityMeterDTO> LMT03500GetUtilityMeterListStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMT03500BuildingUnitDTO> LMT03500GetBuildingUnitListStream()
        {
            throw new NotImplementedException();
        }

        public LMT03500SingleDTO<LMT03500UtilityMeterDetailDTO> LMT03500GetUtilityMeterDetail(LMT03500UtilityMeterDetailParam poParam)
        {
            throw new NotImplementedException();
        }

        public LMT03500SingleDTO<LMT03500AgreementUtilitiesDTO> LMT03500GetAgreementUtilities(LMT03500AgreementUtilitiesParam poParam)
        {
            throw new NotImplementedException();
        }

        //Untuk fetch data streaming dari controller  
        public async Task<List<T>> GetListStreamAsync<T>(string pcNameOf)
        {
            var loEx = new R_Exception();
            List<T> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<T>(
                    _RequestServiceEndPoint,
                    pcNameOf,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken,
                    null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        
        //Untuk fetch data streaming dari controller dengan parameter
        public async Task<List<T>> GetListStreamAsync<T, T1>(string pcNameOf, T1 poParameter)
        {
            var loEx = new R_Exception();
            List<T> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<T, T1>(
                    _RequestServiceEndPoint,
                    pcNameOf,
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken,
                    null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        //Untuk fetch data object dari controller
        public async Task<T> GetAsync<T>(string pcNameOf) where T : R_APIResultBaseDTO
        {
            var loEx = new R_Exception();
            var loResult = default(T);

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<T>(
                    _RequestServiceEndPoint,
                    pcNameOf,
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

        //Untuk fetch data object dari controller dengan parameter
        public async Task<T> GetAsync<T, T1>(string pcNameOf, T1 poParameter) where T : R_APIResultBaseDTO
        {
            var loEx = new R_Exception();
            var loResult = default(T);

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<T, T1>(
                    _RequestServiceEndPoint,
                    pcNameOf,
                    poParameter,
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