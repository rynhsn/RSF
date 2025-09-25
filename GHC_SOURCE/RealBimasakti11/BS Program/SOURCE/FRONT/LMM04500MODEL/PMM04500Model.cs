using PMM04500COMMON;
using PMM04500COMMON.DTO_s;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMM04500MODEL
{
    public class PMM04500Model : R_BusinessObjectServiceClientBase<PricingSaveParamDTO>, IPMM04500
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_CHECKPOINT_NAME = "api/PMM04500";
        private const string DEFAULT_MODULE = "PM";
        public PMM04500Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }

        public async Task<List<PropertyDTO>> GetPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<PropertyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM04500.GetPropertyList),
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

        public async Task<List<PricingDTO>> GetPricingDateListAsync()
        {
            var loEx = new R_Exception();
            List<PricingDTO> loResult = new List<PricingDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PricingDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM04500.GetPricingDateList),
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

        public async Task<List<UnitTypeCategoryDTO>> GetUnitTypeCategoryListAsync()
        {
            var loEx = new R_Exception();
            List<UnitTypeCategoryDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UnitTypeCategoryDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM04500.GetUnitTypeCategoryList),
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

        public async Task<List<PricingDTO>> GetPricingListAsync()
        {
            var loEx = new R_Exception();
            List<PricingDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PricingDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM04500.GetPricingList),
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

        public async Task<List<TypeDTO>> GetRftGSBCodeInfoListAsync()
        {
            var loEx = new R_Exception();
            List<TypeDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<TypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM04500.GetRftGSBCodeInfoList),
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

        public async Task SavePricingAsync(PricingSaveParamDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                await R_HTTPClientWrapper.R_APIRequestObject<PricingDumpResultDTO, PricingSaveParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM04500.SavePricing),
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

        public async Task ActiveInactivePricingAsync(PricingParamDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                await R_HTTPClientWrapper.R_APIRequestObject<PricingDumpResultDTO, PricingParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM04500.ActiveInactivePricing),
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
        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<UnitTypeCategoryDTO> GetUnitTypeCategoryList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PricingDTO> GetPricingDateList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PricingDTO> GetPricingList()
        {
            throw new NotImplementedException();
        }

        public PricingDumpResultDTO SavePricing(PricingSaveParamDTO poParam)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<TypeDTO> GetRftGSBCodeInfoList()
        {
            throw new NotImplementedException();
        }

        public PricingDumpResultDTO ActiveInactivePricing(PricingParamDTO poParam)
        {
            throw new NotImplementedException();
        }
    }
}
