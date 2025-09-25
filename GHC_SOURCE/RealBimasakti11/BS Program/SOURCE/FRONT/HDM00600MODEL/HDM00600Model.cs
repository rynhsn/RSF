using HDM00600COMMON.DTO;
using HDM00600COMMON.DTO.General;
using HDM00600COMMON.DTO.Helper;
using HDM00600COMMON.DTO_s.Helper;
using HDM00600COMMON.Interfaces;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HDM00600MODEL
{
    public class HDM00600Model : R_BusinessObjectServiceClientBase<PricelistDTO>, IHDM00600
    {
        //var & const
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlHD";
        private const string DEFAULT_CHECKPOINT_NAME = "api/HDM00600";
        private const string DEFAULT_MODULE = "HD";
        public HDM00600Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
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

        //realmethod
        public async Task<List<CurrencyDTO>> GetCurrencyListAsync()
        {
            var loEx = new R_Exception();
            List<CurrencyDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CurrencyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IHDM00600.GetCurrencyList),
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
        public async Task<List<PropertyDTO>> GetPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<PropertyDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IHDM00600.GetPropertyList),
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
        public async Task ActiveInactive_PricelistAsync(ActiveInactivePricelistParam poParam)
        {
            var loEx = new R_Exception();
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                await R_HTTPClientWrapper.R_APIRequestObject<GeneralAPIResultBaseDTO<ActiveInactivePricelistParam>, ActiveInactivePricelistParam>(
                    _RequestServiceEndPoint,
                    nameof(IHDM00600.ActiveInactive_Pricelist),
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
        public async Task<List<PricelistDTO>> GetList_PricelistAsync()
        {
            var loEx = new R_Exception();
            List<PricelistDTO> loResult = new List<PricelistDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PricelistDTO>(
                    _RequestServiceEndPoint,
                    nameof(IHDM00600.GetList_Pricelist),
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
        public async Task<GeneralFileByteDTO> DownloadFile_TemplateExcelUploadAsync()
        {
            var loEx = new R_Exception();
            GeneralFileByteDTO loResult = new GeneralFileByteDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loRtnTemp = await R_HTTPClientWrapper.R_APIRequestObject<GeneralAPIResultBaseDTO<GeneralFileByteDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IHDM00600.DownloadFile_TemplateExcelUpload),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult = loRtnTemp.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        //implement only
        public GeneralAPIResultBaseDTO<ActiveInactivePricelistParam> ActiveInactive_Pricelist(ActiveInactivePricelistParam poParam)
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<CurrencyDTO> GetCurrencyList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PricelistDTO> GetList_Pricelist()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }
        public GeneralAPIResultBaseDTO<GeneralFileByteDTO> DownloadFile_TemplateExcelUpload()
        {
            throw new NotImplementedException();
        }
    }
}
