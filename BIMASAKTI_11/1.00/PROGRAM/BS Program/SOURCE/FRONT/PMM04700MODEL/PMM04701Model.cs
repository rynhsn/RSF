using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMM04700Common;
using PMM04700Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;

namespace PMM4700MODEL
{
    public class PMM04701Model : R_BusinessObjectServiceClientBase<PricingSaveParamDTO>, IPMM04701
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMM04701";
        private const string DEFAULT_MODULE = "PM";

        public PMM04701Model() :
            base(DEFAULT_HTTP_NAME, DEFAULT_SERVICEPOINT_NAME, DEFAULT_MODULE, true, true)
        {
        }


        public async Task<List<OtherUnitDTO>> GetUnitTypeCategoryListAsync()
        {
            var loEx = new R_Exception();
            List<OtherUnitDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<OtherUnitDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM04700.GetOtherUnitList),
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

        public async Task<List<PricingRateDTO>> GetPricingRateDateListAsync()
        {
            var loEx = new R_Exception();
            List<PricingRateDTO> loResult = new List<PricingRateDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PricingRateDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM04701.GetPricingRateDateList),
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

        public async Task<List<PricingRateDTO>> GetPricingRateListAsync()
        {
            var loEx = new R_Exception();
            List<PricingRateDTO> loResult = new List<PricingRateDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PricingRateDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM04701.GetPricingRateList),
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

        public async Task SavePricingRateAsync(PricingRateSaveParamDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                await R_HTTPClientWrapper.R_APIRequestObject<PricingDumpResultDTO, PricingRateSaveParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM04701.SavePricingRate),
                    poParam,
                    DEFAULT_MODULE
                    , _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //NotImplementedException (Method)

        public IAsyncEnumerable<PricingRateDTO> GetPricingRateDateList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PricingRateDTO> GetPricingRateList()
        {
            throw new NotImplementedException();
        }

        public PricingDumpResultDTO SavePricingRate(PricingRateSaveParamDTO poParam)
        {
            throw new NotImplementedException();
        }
    }
}