using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lookup_HDCOMMON;
using Lookup_HDCOMMON.DTOs;
using Lookup_HDCOMMON.DTOs.HDL00100;
using Lookup_HDCOMMON.DTOs.HDL00200;
using Lookup_HDCOMMON.DTOs.HDL00300;
using Lookup_HDCOMMON.DTOs.HDL00400;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace Lookup_HDModel
{
    public class PublicHDLookupModel : R_BusinessObjectServiceClientBase<HDL00100DTO>, IPublicHDLookup
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlHD";
        private const string DEFAULT_ENDPOINT = "api/PublicLookupHD";
        private const string DEFAULT_MODULE = "HD";

        public PublicHDLookupModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<HDL00100DTO> HDL00100PriceListLookup()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<HDL00100DTO>> HDL00100PriceListLookupAsync(HDL00100ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<HDL00100DTO> loResult = null;

            try
            {
                //context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CTAXABLE, poParam.CTAXABLE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<HDL00100DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicHDLookup.HDL00100PriceListLookup),
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


        public IAsyncEnumerable<HDL00100DTO> HDL00100PriceListDetailLookup()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<HDL00100DTO>> HDL00100PriceListDetailLookupAsync(HDL00100ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<HDL00100DTO> loResult = null;

            try
            {
                //context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CTAXABLE, poParam.CTAXABLE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CSTATUS, poParam.CSTATUS);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPRICELIST_ID, poParam.CPRICELIST_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CLOOKUP_DATE, poParam.CLOOKUP_DATE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CSLA_CALL_TYPE_ID,
                    poParam.CSLA_CALL_TYPE_ID);
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<HDL00100DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicHDLookup.HDL00100PriceListDetailLookup),
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

        public IAsyncEnumerable<HDL00200DTO> HDL00200PriceListItemLookup()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<HDL00200DTO>> HDL00200PriceListItemLookupAsync(HDL00200ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<HDL00200DTO> loResult = null;

            try
            {
                //context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CTAXABLE, poParam.CTAXABLE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CSTATUS, poParam.CSTATUS);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPRICELIST_ID, poParam.CPRICELIST_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CLOOKUP_DATE, poParam.CLOOKUP_DATE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CSLA_CALL_TYPE_ID,
                    poParam.CSLA_CALL_TYPE_ID);
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<HDL00200DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicHDLookup.HDL00200PriceListItemLookup),
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

        public IAsyncEnumerable<HDL00300DTO> HDL00300PublicLocationLookup()
        {
            throw new NotImplementedException();
        }



        public async Task<List<HDL00300DTO>> HDL00300PublicLocationLookupAsync(HDL00300ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<HDL00300DTO> loResult = null;

            try
            {
                //context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.LACTIVE, poParam.LACTIVE);
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<HDL00300DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicHDLookup.HDL00300PublicLocationLookup),
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

        public IAsyncEnumerable<HDL00400DTO> HDL00400AssetLookup()
        {
            throw new NotImplementedException();
        }

        public async Task<List<HDL00400DTO>> HDL00400AssetLookupAsync(HDL00400ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<HDL00400DTO> loResult = null;

            try
            {
                //context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CBUILDING_ID, poParam.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CFLOOR_ID, poParam.CFLOOR_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CUNIT_ID, poParam.CUNIT_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CLOCATION_ID, poParam.CLOCATION_ID);
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<HDL00400DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicHDLookup.HDL00400AssetLookup),
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