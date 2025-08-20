﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMT03500Common;
using PMT03500Common.DTOs;
using PMT03500Common.Params;
using R_APIClient;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace PMT03500Model
{
    public class PMT03500UpdateMeterModel : R_BusinessObjectServiceClientBase<PMT03500UtilityMeterDetailDTO>,
        IPMT03500UpdateMeter
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT03500UpdateMeter";
        private const string DEFAULT_MODULE = "pm";

        public PMT03500UpdateMeterModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName,
            plSendWithContext,
            plSendWithToken)
        {
        }

        public IAsyncEnumerable<PMT03500UtilityMeterDTO> PMT03500GetUtilityMeterListStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT03500BuildingUnitDTO> PMT03500GetBuildingUnitListStream()
        {
            throw new NotImplementedException();
        }

        public PMT03500ListDTO<PMT03500MeterNoDTO> PMT03500GetMeterNoList(PMT03500MeterNoParam poParam)
        {
            throw new NotImplementedException();
        }

        public PMT03500ListDTO<PMT03500PeriodRangeDTO> PMT03500GetPeriodRangeList(PMT03500PeriodRangeParam poParam)
        {
            throw new NotImplementedException();
        }

        public PMT03500SingleDTO<PMT03500BuildingUnitDTO> PMT03500GetBuildingUnitRecord(PMT03500SearchTextDTO poParam)
        {
            throw new NotImplementedException();
        }

        public PMT03500SingleDTO<PMT03500UtilityMeterDetailDTO> PMT03500GetUtilityMeterDetail(PMT03500UtilityMeterDetailParam poParam)
        {
            throw new NotImplementedException();
        }

        public PMT03500SingleDTO<PMT03500AgreementUtilitiesDTO> PMT03500GetAgreementUtilities(PMT03500AgreementUtilitiesParam poParam)
        {
            throw new NotImplementedException();
        }

        public PMT03500UtilityMeterDetailDTO PMT03500UpdateMeterNo(PMT03500UpdateChangeMeterNoParam poParam)
        {
            throw new NotImplementedException();
        }

        public PMT03500UtilityMeterDetailDTO PMT03500ChangeMeterNo(PMT03500UpdateChangeMeterNoParam poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<PMT03500UtilityMeterDetailDTO> PMT03500CloseMeterNo(PMT03500CloseMeterNoParam poParam)
        {
            throw new NotImplementedException();
        }

        //Untuk fetch data streaming dari controller  
        public async Task<List<T>> GetListStreamAsync<T>(string pcNameOf)
        {
            var loEx = new R_Exception();
            var loResult = new List<T>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<T>(
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