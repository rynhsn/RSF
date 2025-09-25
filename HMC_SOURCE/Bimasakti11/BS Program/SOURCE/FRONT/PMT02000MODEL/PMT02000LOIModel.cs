using PMT02000COMMON.Interface;
using PMT02000COMMON.LOI_List;
using PMT02000COMMON.Upload;
using PMT02000COMMON.Utility;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMT02000MODEL
{
    public class PMT02000LOIModel : R_BusinessObjectServiceClientBase<PMT02000LOIHeader_DetailDTO>, IPMT02000
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT02000LOI";
        private const string DEFAULT_MODULE = "PM";
        public PMT02000LOIModel(
         string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }
        #region Implements Library

        public IAsyncEnumerable<PMT02000LOIDTO> GetLOIListStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02000PropertyDTO> GetPropertyListStream()
        {
            throw new NotImplementedException();
        }
        public PMT02000LOIHeader GetLOIHeader(PMT02000DBParameter poParam)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02000LOIHandoverUtilityDTO> GetLOIDetailListStream()
        {
            throw new NotImplementedException();
        }
        public PMT02000LOIHeader ProcessSubmitRedraft(PMT02000DBParameter poParam)
        {
            throw new NotImplementedException();
        }
        public PMT0200MultiListDataDTO GetHOTemplate(PMT02000DBParameter poParam)
        {
            throw new NotImplementedException();
        }
        public PMT02000VarGsmTransactionCodeDTO GetVAR_GSM_TRANSACTION_CODE()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Implements
        public async Task<PMT02000GenericList<PMT02000PropertyDTO>> PropertyListStreamAsyncModel()
        {
            var loEx = new R_Exception();
            PMT02000GenericList<PMT02000PropertyDTO> loResult = new PMT02000GenericList<PMT02000PropertyDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02000PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02000.GetPropertyListStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = temp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<PMT02000GenericList<PMT02000LOIDTO>> LOIListStreamAsyncModel()
        {
            var loEx = new R_Exception();
            PMT02000GenericList<PMT02000LOIDTO> loResult = new PMT02000GenericList<PMT02000LOIDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02000LOIDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02000.GetLOIListStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = temp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<PMT02000LOIHeader> ProcessSubmitRedraftAsyncMosel(PMT02000DBParameter poParam)
        {
            var loEx = new R_Exception();
            PMT02000LOIHeader loResult = new PMT02000LOIHeader();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT02000LOIHeader, PMT02000DBParameter>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02000.ProcessSubmitRedraft),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                // loResult = temp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<PMT02000GenericList<PMT02000LOIHandoverUtilityDTO>> LOIListDetailStreamAsyncModel()
        {
            var loEx = new R_Exception();
            PMT02000GenericList<PMT02000LOIHandoverUtilityDTO> loResult = new PMT02000GenericList<PMT02000LOIHandoverUtilityDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02000LOIHandoverUtilityDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02000.GetLOIDetailListStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = temp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<PMT02000VarGsmTransactionCodeDTO> GetVAR_GSM_TRANSACTION_CODEAsync()
        {
            var loEx = new R_Exception();
            var loResult = new PMT02000VarGsmTransactionCodeDTO();
            try
            {
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT02000VarGsmTransactionCodeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02000.GetVAR_GSM_TRANSACTION_CODE),
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

        public async Task<PMT0200MultiListDataDTO> GetHOTemplateDataAsync(PMT02000DBParameter poParam)
        {

            var loEx = new R_Exception();
            var loResult = new PMT0200MultiListDataDTO();
            try
            {
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT0200MultiListDataDTO, PMT02000DBParameter>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02000.GetHOTemplate),
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
            return loResult;
        }



        #endregion
    }
}
