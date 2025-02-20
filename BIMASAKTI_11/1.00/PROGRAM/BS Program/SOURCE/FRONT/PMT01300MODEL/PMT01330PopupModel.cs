using PMT01300COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMT01300MODEL
{
    public class PMT01330PopupModel : R_BusinessObjectServiceClientBase<PMT01331DTO>, IPMT01330Popup
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT01330Popup";
        private const string DEFAULT_MODULE = "PM";

        public PMT01330PopupModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<PMT01331DTO>> GetLOIChargeRevenueHDListStreamAsync(PMT01331DTO poEntity)
        {
            var loEx = new R_Exception();
            List<PMT01331DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poEntity.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, poEntity.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREF_NO, poEntity.CREF_NO);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGE_SEQ_NO, poEntity.CCHARGE_SEQ_NO);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01331DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01330Popup.GetLOIChargeRevenueHDListStream),
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
        public async Task<List<PMT01332DTO>> GetLOIChargeRevenueListStreamAsync(PMT01332DTO poEntity)
        {
            var loEx = new R_Exception();
            List<PMT01332DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poEntity.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, poEntity.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREF_NO, poEntity.CREF_NO);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGE_SEQ_NO, poEntity.CCHARGE_SEQ_NO);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREVENUE_SHARING_ID, poEntity.CREVENUE_SHARING_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01332DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01330Popup.GetLOIChargeRevenueListStream),
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
        public async Task<List<PMT01333DTO>> GetRevenueMintRentListStreamAsync(PMT01333DTO poEntity)
        {
            var loEx = new R_Exception();
            List<PMT01333DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poEntity.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, poEntity.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREF_NO, poEntity.CREF_NO);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGE_SEQ_NO, poEntity.CCHARGE_SEQ_NO);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREVENUE_SHARING_ID, poEntity.CREVENUE_SHARING_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01333DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01330Popup.GetRevenueMintRentListStream),
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
        public async Task SaveDeleteLOIChargeRevenueHDAsync(PMT01300SaveDTO<PMT01331DTO> poParam)
        {
            var loEx = new R_Exception();

            try
            {
                //Set Context
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01300SingleResult<PMT01331DTO>, PMT01300SaveDTO<PMT01331DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01330Popup.SaveDeleteLOIChargeRevenueHD),
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
        }
        public async Task<PMT01332DTO> SaveDeleteLOIChargeRevenueAsync(PMT01300SaveDTO<PMT01332DTO> poParam)
        {
            var loEx = new R_Exception();
            PMT01332DTO loRtn = null;

            try
            {
                //Set Context
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01300SingleResult<PMT01332DTO>, PMT01300SaveDTO<PMT01332DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01330Popup.SaveDeleteLOIChargeRevenue),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loRtn = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        public async Task SaveRevenueMintRentAsync(PMT01300SaveDTO<PMT01333DTO> poParam)
        {
            var loEx = new R_Exception();

            try
            {
                //Set Context
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01300SingleResult<PMT01333DTO>, PMT01300SaveDTO<PMT01333DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01330Popup.SaveRevenueMintRent),
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
        }

        #region Not Implment
        public IAsyncEnumerable<PMT01331DTO> GetLOIChargeRevenueHDListStream()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMT01332DTO> GetLOIChargeRevenueListStream()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMT01333DTO> GetRevenueMintRentListStream()
        {
            throw new NotImplementedException();
        }
        public PMT01300SingleResult<PMT01332DTO> SaveDeleteLOIChargeRevenue(PMT01300SaveDTO<PMT01332DTO> poEntity)
        {
            throw new NotImplementedException();
        }
        public PMT01300SingleResult<PMT01331DTO> SaveDeleteLOIChargeRevenueHD(PMT01300SaveDTO<PMT01331DTO> poEntity)
        {
            throw new NotImplementedException();
        }
        public PMT01300SingleResult<PMT01333DTO> SaveRevenueMintRent(PMT01300SaveDTO<PMT01333DTO> poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
