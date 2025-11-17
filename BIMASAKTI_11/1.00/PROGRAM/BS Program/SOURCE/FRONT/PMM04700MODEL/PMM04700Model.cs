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
    public class PMM04700Model  : R_BusinessObjectServiceClientBase<PricingSaveParamDTO>, IPMM04700
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMM04700";
        private const string DEFAULT_MODULE = "PM";

        public PMM04700Model() :
            base(DEFAULT_HTTP_NAME, DEFAULT_SERVICEPOINT_NAME, DEFAULT_MODULE, true, true)
        {
        }


        #region Implemented
        public async Task<List<PropertyDTO>> GetPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<PropertyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM04700.GetPropertyList),
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
                    nameof(IPMM04700.GetPricingDateList),
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

        public async Task<List<OtherUnitDTO>> GetOtherUnitListAsync()
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

        public async Task<List<PricingDTO>> GetPricingListAsync()
        {
            var loEx = new R_Exception();
            List<PricingDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PricingDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM04700.GetPricingList),
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

        public async Task<List<TypeDTO>> GetPriceChargesTypeAsync()
        {
            var loEx = new R_Exception();
            List<TypeDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<TypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM04700.GetPriceChargesType),
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
                    nameof(IPMM04700.SavePricing),
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
                    nameof(IPMM04700.ActiveInactivePricing),
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

        public async Task<List<TypeDTO>> GetRftGSBCodeInfoListAsync()
        {
            var loEx = new R_Exception();
            List<TypeDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<TypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM04700.GetRftGSBCodeInfoList),
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
        #endregion

        #region Not Implemented
        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<OtherUnitDTO> GetOtherUnitList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PricingDTO> GetPricingList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PricingDTO> GetPricingDateList()
        {
            throw new NotImplementedException();
        }

        public PricingDumpResultDTO SavePricing(PricingSaveParamDTO poParam)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<TypeDTO> GetPriceChargesType()
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
        #endregion
       
    }
}